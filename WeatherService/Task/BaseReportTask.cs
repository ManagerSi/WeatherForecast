using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using log4net;
using WeatherLib.DAL;

namespace WeatherService.Task.ReportTask {
  public class BaseReportTask : BaseTask {
    private static readonly ILog log = LogManager.GetLogger(typeof(BaseReportTask));
    public BaseReportTask(string taskName)
      : base(taskName) {

    }
    
    protected UnitOfWork UnitOfWork { get; set; }


    /// <summary>
    /// 经过换算的BizDate
    /// 周销量 2012-01-29，这个Key应该为2012-01-23
    /// 月销量 2012-01-29，Key为 2012-01-01
    /// </summary>
    protected DateTime BizDateKey { get; set; }

    /// <summary>
    /// 将BizDate转换到 对应的Key上 ，转换结果保存在BizDateKey中
    /// </summary>
    /// <returns></returns>
    protected virtual DateTime ConverterBizDate() {
      return BizDate;
    }

    /// <summary>
    /// 通过一些先决任务决定是否需要计算
    /// </summary>
    /// <returns></returns>
    protected virtual bool NeedCompute() {
      return true;
    }

    private bool prepared = false;
    /// <summary>
    /// 准备计算数据
    /// 加载一些外部数据，如商店名
    /// </summary>
    protected virtual void PrepareCompute() {

      prepared = true;
    }

    /// <summary>
    /// 删除相同Key的报表
    /// </summary>
    protected virtual void CleanReport() {
    }

    /// <summary>
    /// 计算
    /// </summary>
    protected virtual void ComputeReport() {
    }

    /// <summary>
    /// 计算完成，释放内存
    /// </summary>
    protected virtual void FinishReport() {

    }

    protected override void DoExecution() {
      DoReportTask();
    }

    protected bool DoReportTask() {
      UnitOfWork = GetNewUnitOfWork();
      try {

        prepared = false;
        //BizDate = bizDate.Date;
        log.Info("Start Report Task:" + TaskName);
        BizDateKey = ConverterBizDate();
        if (!NeedCompute()) {
          log.Info("Skip Task： NeedCompute() = false");
          return true;
        }
        UnitOfWork.ContextCommandTimeOut = 600;
        UnitOfWork.DisableAutoDetectChanges();
        //using (TransactionScope scopt = new TransactionScope()) {
        log.Info("Begin Transcation");
        log.Info("PrepareCompute");
        PrepareCompute();
        log.Info("CleanReport");
        CleanReport();
        log.Info("ComputeReport");
        if (!prepared) {
          throw new System.Exception("Unprepared Task");
        }
        ComputeReport();
        log.Info("FinishReport");
        FinishReport();
        log.Info("Committing");
        //scopt.Complete();
        log.Info("Commit Transcation");
        //}
        UnitOfWork.EnableAutoDetectChanges();
        log.Info("Report Task Done:" + TaskName);
        return true;
      } finally {
        UnitOfWork = null;
      }
    }

    
    protected void SaveNewItems<T>(IEnumerable<T> values, int savePerRecord = kMaxInsertOnce) where T : class {
      base.SaveNewItems(UnitOfWork, values, savePerRecord);
    }
    protected void UpdateItems<T>(IEnumerable<T> values, int savePerRecord = kMaxInsertOnce) where T : class {
      base.UpdateItems(UnitOfWork, values, savePerRecord);
    }
    protected void DeleteItems<T>(IEnumerable<T> values, int savePerRecord = kMaxInsertOnce) where T : class {
      base.DeleteItems(UnitOfWork, values, savePerRecord);
    }
    ///// <summary>
    ///// 返回当前org的所有父亲org，不包含自身
    ///// </summary>
    ///// <param name="baseOrgDict"></param>
    ///// <param name="baseOrgID"></param>
    ///// <returns></returns>
    //protected IEnumerable<decimal> GetOrgPathFromParent(Dictionary<decimal,BaseOrg> baseOrgDict, decimal baseOrgID) {
    //  if (baseOrgID < 0) {
    //    //return new List<decimal>();
    //    yield break;
    //  }
    //  var rst = new List<decimal>();
    //  if (!baseOrgDict.ContainsKey(baseOrgID))
    //  {
    //    log.Info("KA key:" + baseOrgID);
    //    yield break;
    //  }
    //  decimal? curOrgID = baseOrgDict[baseOrgID].ParentOrgID;
    //  // 不包含自身
    //  while (curOrgID != null) {
    //    yield return curOrgID.Value;
    //    if (!baseOrgDict.ContainsKey(curOrgID.Value)) {
    //      yield break;
    //    }
    //    curOrgID = baseOrgDict[curOrgID.Value].ParentOrgID;
    //  }
    //}

    //protected IEnumerable<decimal> GetKAStorePathFromParent(Dictionary<decimal,BaseDictItem> storeKADict, decimal KAID)
    //{
    //  var rst = new List<decimal>();
    //  if (!storeKADict.ContainsKey(KAID))
    //  {
    //    log.Info("KA key:"+KAID);
    //    return rst;
    //  }
    //  var item = storeKADict[KAID];
    //  if (null != item)
    //  {
    //    rst.Add(item.ID);
    //    if (item.ParentDictItemID.HasValue)
    //    {
    //    rst.Add(item.ParentDictItemID.Value);
    //    }
    //  }
    //  return rst;
    //}
  }
}
