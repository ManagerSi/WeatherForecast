using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using log4net;
using Mobizone.TSIC.DAL;
using Mobizone.TSIC.Models;
using Mobizone.TSIC.Utility;
using AutoMapper;
using System.Data;

namespace Mobizone.TSIC.Task.SnapshotTask {
  public class BaseSnapshotTask<T, S>
    : BaseSimpleTask where T: class where S : class,ISnapshotTable {
    private static readonly ILog log = LogManager.GetLogger("BaseSnapshotTask<T, S>");
    public BaseSnapshotTask(string taskName)
      : base(taskName) {
    }

    protected override void DoTask() {
      BizDateKey = GetSnapshotBizDate();
      this.DoSnapshot();
    }

    protected virtual IEnumerable<T> GetSnapshotList() {
      return UnitOfWork.Repository<T>().Get().AsNoTracking();
    }

    protected void DoSnapshot() {
      log.Info("Loading Snapshotting List.");
      var src = GetSnapshotList();
      var dal = UnitOfWork.Repository<S>() as ISnapshotRepository;
      var bizdatekey = BizDateKey.ToDateString();
      log.Info("Disable By Bizdate = " + bizdatekey);
      dal.DisableByBizDate(bizdatekey);      
      var dst = new List<S>();
      //var MapConfig = CreateMapConfig();
      foreach (var s in src) {
        var d = SnapshotMap(s);
        d.BizDate = bizdatekey;
        d.State = TSICConstant.StateEnable;
        d.SnapshotTime = Util.RPCNow;
        dst.Add(d);
      }
      log.Info("Mapping Done Count = " + dst.Count);
      SaveNewItems(UnitOfWork, dst, kMaxInsertOnce);
      dal.DeleteDisabledRecord();
    }
    
    protected virtual S SnapshotMap(T s) {
      //return AutoMapper.Mapper.Map<T, S>(s);
      var mapper = MapConfig.CreateMapper();
      return mapper.Map<T, S>(s);
    }

    protected virtual DateTime GetSnapshotBizDate() {
      return BizDate.AddDays(-1).Date.FirstDayOfMonth();
    }

    protected DateTime BizDateKey { get; set; }

    protected MapperConfiguration MapConfig { get; set; }
  }
}
