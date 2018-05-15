using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Text;
using System.Transactions;
using log4net;
using WeatherLib.Utility;
using WeatherLib.DAL;

namespace WeatherService.Task.SyncTask {
  public class BaseSyncTask : BaseTask {
    private static readonly ILog log = LogManager.GetLogger(typeof(BaseSyncTask));
    public BaseSyncTask(string taskName)
      : base(taskName) {

      LastSyncTime = Util.RPCNow.Date.AddDays(-8);
      //  LastSyncTime = Util.RPCNow.Date.AddDays(-8000);
      //UsingTransaction = false;
    }
    public const int kMaxSyncUpdateOnce = 100;
    protected const int TSICSystemID = 1;
    protected string DataSrc { get; set; }
    protected UnitOfWork UnitOfWork { get; set; }
    protected DateTime LastSyncTime { get; set; }
    /// <summary>
    /// 需要同步
    /// </summary>
    /// <returns></returns>
    protected virtual bool NeedSync() {
      return true;
    }

    /// <summary>
    /// 准备计算数据
    /// 加载一些外部数据，如商店名
    /// </summary>
    protected virtual void PrepareSync() {
      UnitOfWork.DisableAutoDetectChanges();
    }
    /// <summary>
    /// 进行数据同步
    /// </summary>
    protected virtual void DoSyncWork() {
    }
    /// <summary>
    /// 完成同步
    /// </summary>
    protected virtual void FinishSync() {
      UnitOfWork.EnableAutoDetectChanges();
    }

    protected SqlConnection defaultSqlCnt = null;
    protected SqlConnection GetSqlCnt(string contextName, bool createNew = false) {
      if (!createNew) {
        return defaultSqlCnt;
      }
      var cntString = System.Configuration.ConfigurationManager.ConnectionStrings[contextName].ConnectionString;
      var cnt = new SqlConnection(cntString);
      cnt.Open();
      return cnt;
    }
    protected bool UsingTransaction { get; set; }

    protected override void DoExecution() {
      DoSync();
    }

    protected bool DoSync() {

      log.Info("Start Sync Task:" + TaskName);
      if (!NeedSync()) {
        return true;
      }
      if (!UsingTransaction) {
        Sync(null);
        return true;
      }
      var transactionOptions = new System.Transactions.TransactionOptions();
      transactionOptions.Timeout = TimeSpan.FromMinutes(10);
      //set it to read uncommited
      transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
      using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew, transactionOptions)) {
        Sync(transactionScope);
      }

      return true;

    }

    private void Sync(TransactionScope transactionScope) {
      UnitOfWork = GetNewUnitOfWork();
      try {
        if (null != transactionScope) {
          log.Info("Begin Transcation");
        }
        log.Info("Prepare Sync");
        PrepareSync();
        log.Info("Do Sync");
        DoSyncWork();
        log.Info("Finish Sync");
        FinishSync();
        if (null != transactionScope) {
          log.Info("Committing");
          transactionScope.Complete();
          log.Info("Commit Transcation");
        }
      } finally {
        UnitOfWork = null;
      }
    }

    protected static DateTime? GetDateTimeOrNull(IDataReader r, string col) {
      var idx = r.GetOrdinal(col);
      if (r.IsDBNull(idx)) {
        return null;
      } else {
        return r.GetDateTime(idx);
      }
    }
    protected static int? GetIntOrNull(IDataReader r, string col) {
      var idx = r.GetOrdinal(col);
      if (r.IsDBNull(idx)) {
        return null;
      } else {
        return r.GetInt32(idx);
      }
    }

    protected static decimal? GetDecimalOrNull(IDataReader r, string col) {
      var idx = r.GetOrdinal(col);
      if (r.IsDBNull(idx)) {
        return null;
      } else {
        return r.GetDecimal(idx);
      }
    }
  }
}
