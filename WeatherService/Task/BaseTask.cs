using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherLib.BLL;
using WeatherLib.Model;
using System.Transactions;
using log4net;
using WeatherLib.DAL;
using WeatherLib.Utility;


namespace WeatherService.Task {
  /// <summary>
  /// Task是任务的最小单位
  /// </summary>
  public class BaseTask {
    private static readonly ILog log = LogManager.GetLogger(typeof(BaseTask));
    public const int kMaxInsertOnce = 100;
    public BaseTask(string taskName) {
      this.TaskName = taskName;
      this.EventManager = null;
    }
    protected String TaskName { get; set; }
    /// <summary>
    /// 在这里设置依赖的任务
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerable<Type> GetDependentTasks() {
      return null;
    }

    public TaskEventManager EventManager { get; set; }

    /// <summary>
    /// 触发一个事件，该事件将在作业执行完成之后的某个时间执行
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="arg"></param>
    protected void OnLazyEvent(string eventName, object data) {
      if (EventManager != null) {
        EventManager.OnLazyEvent(this.GetType(), eventName, data);
      }
    }

    /// <summary>
    /// 触发一个事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="arg"></param>
    protected void OnEvent(string eventName, object data) {
      if (EventManager != null) {
        EventManager.OnEvent(this.GetType(), eventName, data);
      }
    }

    protected void ProcessLazyEvent() {
      if (EventManager != null) {
        EventManager.ProcessLazyEvent();
      }
    }
    /// <summary>
    /// 指定计算的日期
    /// 如果是周销量任务，
    /// 比如指定2012-01-29，表示计算从2012-01-23，到 2012-01-29
    /// 又如 月销量任务时
    /// 表示从2012-01-01 到 2012-01-29
    /// </summary>
    public DateTime BizDate { get; set; }
    /// <summary>
    /// 执行任务，默认时间为今天
    /// </summary>
    /// <param name="bizdate"></param>
    public void Execute(DateTime? bizdate = null) {
      if (null == bizdate) {
        BizDate = Util.RPCNow.Date;
      } else {
        BizDate = bizdate.Value.Date;
      }
      var start = Util.RPCNow;

      //PublishMsg(new Bus.Messages.TaskStatusChange() {
      //  Status = ServiceTaskMetadata.TaskStatus.TaskStatusRuning,
      //  TaskClassName = GetType().FullName,
      //  Time = start,
      //});

      try {
        try {
          DoExecution();
        } catch (ReflectionTypeLoadException ex) {
          StringBuilder sb = new StringBuilder();
          foreach (Exception exSub in ex.LoaderExceptions) {
            sb.AppendLine(exSub.Message);
            if (exSub is FileNotFoundException) {
              FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
              if (!string.IsNullOrEmpty(exFileNotFound.FusionLog)) {
                sb.AppendLine("Fusion Log:");
                sb.AppendLine(exFileNotFound.FusionLog);
              }
            }
            sb.AppendLine();
          }
          string errorMessage = sb.ToString();
          //Display or log the error based on your application.
          log.Fatal(errorMessage);
        }
      } catch (Exception e) {
        //PublishMsg(new Bus.Messages.TaskStatusChange() {
        //  Status = ServiceTaskMetadata.TaskStatus.TaskStatusFailed,
        //  TaskClassName = GetType().FullName,
        //  Time = Util.RPCNow,
        //  Data = e.ToString()
        //});
        log.Fatal(e);
        throw;
      }
      var end = Util.RPCNow;
      //PublishMsg(new Bus.Messages.TaskStatusChange() {
      //  Status = ServiceTaskMetadata.TaskStatus.TaskStatusDone,
      //  TaskClassName = GetType().FullName,
      //  Time = end,
      //  Data = string.Format("({0} - {1}) Time Cost: {2:0.0} ms", start, end, (end - start).TotalMilliseconds)
      //});
      log.Info(ServiceTaskMetadata.TaskStatus.TaskStatusDone);
    }

    protected void PublishMsg<T>(T msg, bool catchException = true) where T : class {
      //if (catchException) {
      //  try {
      //    Bus.ServiceBus.Publish<T>(msg);
      //  } catch (Exception e) {
      //    log.Warn("PublishMsg", e);
      //  }
      //} else {
      //  Bus.ServiceBus.Publish<T>(msg);
      //}
    }

    protected virtual void DoExecution() {
    }

    /// <summary>
    /// 分配一个新的UnitOfWork
    /// </summary>
    /// <returns></returns>
    protected UnitOfWork GetNewUnitOfWork() {
      var uw = new UnitOfWork(new WeatherDBContext());
      uw.UsingHttpCurrentSession = false;
      return uw;
    }



    protected void SaveNewItems<T>(UnitOfWork unitOfWork, IEnumerable<T> values, int savePerRecord = kMaxInsertOnce) where T : class {

      var repository = unitOfWork.Repository<T>();
      int count = 0;
      foreach (var v in values) {
        repository.Insert(v);
        if (++count % savePerRecord == 0) {
          count = 0;
          unitOfWork.Save();
        }
      }
      unitOfWork.Save();
    }

    protected void UpdateItems<T>(UnitOfWork unitOfWork, IEnumerable<T> values, int savePerRecord =kMaxInsertOnce) where T : class {

      var repository = unitOfWork.Repository<T>();
      int count = 0;
      foreach (var v in values) {
        repository.Update(v);
        if (++count % savePerRecord == 0) {
          count = 0;
          unitOfWork.Save();
        }
      }
      unitOfWork.Save();
    }

    protected void DeleteItems<T>(UnitOfWork unitOfWork, IEnumerable<T> values, int savePerRecord = kMaxInsertOnce) where T : class {

      var repository = unitOfWork.Repository<T>();
      int count = 0;
      foreach (var v in values) {
        repository.Delete(v);
        if (++count % savePerRecord == 0) {
          count = 0;
          unitOfWork.Save();
        }
      }
      unitOfWork.Save();
    }
  }
}
