using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using log4net;
using WeatherLib.BLL;
using WeatherLib.Model;
using WeatherLib.DAL;


namespace WeatherService.Task {
  public class BaseSimpleTask : BaseTask {
    private static readonly ILog log = LogManager.GetLogger(typeof(BaseSimpleTask));
    public BaseSimpleTask(string taskName)
      : base(taskName) {

    }

    protected virtual void DoTask() {
    }
    private bool prepared = false;
    /// <summary>
    /// 准备计算数据
    /// 加载一些外部数据，如商店名
    /// </summary>
    protected virtual void PrepareCompute() {
      UnitOfWork.DisableAutoDetectChanges();
      prepared = true;
    }
    /// <summary>
    /// 计算完成，释放内存
    /// </summary>
    protected virtual void FinishCompute() {
      UnitOfWork.EnableAutoDetectChanges();
    }
    protected UnitOfWork UnitOfWork { get; set; }
    //public const int kMaxReportInsertOnce = 100;

    protected override void DoExecution() {
      UnitOfWork = GetNewUnitOfWork();
      try {
        log.Info("Start Task:" + TaskName);
        log.Info("PrepareCompute");
        PrepareCompute();
        if (prepared) {
          log.Info("DoTask");
          DoTask();
          log.Info("FinishCompute");

          FinishCompute();
        }
      } finally {
        UnitOfWork = null;
      }
      //return true;
    }

    //protected void SaveItems(TEntity entity) {
    //  entity = this.Repository.Insert(entity);
    //  unitOfWork.Save();
      
    //}

  }
}
