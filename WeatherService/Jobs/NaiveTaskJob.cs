using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using log4net;
using log4net.Config;
using WeatherService.Task;

namespace WeatherService.Jobs {
  public class NaiveTaskJob<T> : BaseJob where T : BaseTask, new() {
    private static readonly ILog log = LogManager.GetLogger(typeof(T));
    protected override bool IsLogTaskStatus { get { return false; } }
    protected override void DoJob(Quartz.IJobExecutionContext context) {
      var task = new T();
      /*if (task is BaseReportTask) {
        (task as BaseReportTask).DoReportTask(Util.RPCNow);
      } else if (task is BaseSimpleTask) {
        (task as BaseSimpleTask).Execute();
      } else if (task is BaseSyncTask) {
        (task as BaseSyncTask).DoSync();
      }*/
      task.Execute();
      log.Info(typeof(T).Name + " Job Finish");
    }
  }
}
