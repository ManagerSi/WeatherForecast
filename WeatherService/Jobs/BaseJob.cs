using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using log4net;
using WeatherLib.DAL;
using WeatherLib.Model;
using WeatherService.Task;
namespace WeatherService.Jobs {
  /// <summary>
  /// 一个Job可以包含多个Task
  /// </summary>
  public class BaseJob : IJob {
    private static readonly ILog log = LogManager.GetLogger(typeof(BaseJob));
    protected virtual bool IsLogTriggerEvent { get { return true; } }
    protected virtual bool IsLogTaskStatus { get { return true; } }
    #region IJob 成员

    public void Execute(IJobExecutionContext context) {
      string key;
      if (null != context) {
        key = context.JobDetail.Key.ToString();
      } else {
        key = "Unknow ( type = " + this.GetType().ToString() + " )";
      }
      if (IsLogTriggerEvent) {
        log.Info("Trigger Job:" + key);
      }
      var start = System.DateTime.Now;
      if (IsLogTaskStatus) {
        //PublishMsg(new Bus.Messages.TaskStatusChange() {
        //  Status = ServiceTaskMetadata.TaskStatus.TaskStatusRuning,
        //  TaskClassName = GetType().FullName,
        //  Time = start,
        //});
      }

      try {
        DoJob(context);
        var end = System.DateTime.Now;
        // -- add more debug info
        if (IsLogTriggerEvent) {
          log.Info("Trigger Job Done:" + key);
        }
        if (IsLogTaskStatus) {
          //PublishMsg(new Bus.Messages.TaskStatusChange() {
          //  Status = ServiceTaskMetadata.TaskStatus.TaskStatusDone,
          //  TaskClassName = GetType().FullName,
          //  Time = end,
          //  Data = string.Format("({0} - {1}) Time Cost: {2:0.0} ms", start, end, (end - start).TotalMilliseconds)
          //});
        }

      } catch (Exception e) {

        if (IsLogTaskStatus) {
          //PublishMsg(new Bus.Messages.TaskStatusChange() {
          //  Status = ServiceTaskMetadata.TaskStatus.TaskStatusFailed,
          //  TaskClassName = GetType().FullName,
          //  Time = Util.RPCNow,
          //  Data = e.ToString()
          //});
        }

        log.Fatal(key, e);
      }
    }
    #endregion
    protected virtual void DoJob(IJobExecutionContext context) {
    }

    /// <summary>
    /// 分配一个新的UnitOfWork
    /// </summary>
    /// <returns></returns>
    protected UnitOfWork GetNewUnitOfWork() {
      var uw = new UnitOfWork(new WeatherLib.Model.WeatherDBContext());
      uw.UsingHttpCurrentSession = false;
      return uw;
    }

    protected void PublishMsg<T>(T msg, bool catchException = true) where T : class {
      if (catchException) {
        try {
          //Bus.ServiceBus.Publish<T>(msg);
        } catch (Exception e) {
          log.Warn("PublishMsg", e);
        }
      } else {
        //Bus.ServiceBus.Publish<T>(msg);
      }
    }
  }
}
