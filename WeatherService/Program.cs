using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Jobs;
using WeatherService.Task.WeatherSync;

namespace WeatherService {
  class Program {
    private static readonly ILog log = LogManager.GetLogger(typeof(Program));
    static void Main(string[] args) {
      log4net.Config.XmlConfigurator.Configure();

      //Init(false);
      ServiceBase[] ServicesToRun;
      ServicesToRun = new ServiceBase[]
      {
          new WeatherService()
      };
      ServiceBase.Run(ServicesToRun);

    #region Quartz测试
      NameValueCollection properties = new NameValueCollection();
      properties["quartz.scheduler.instanceName"] = "WeatherService";
      // set thread pool info
      properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
      properties["quartz.threadPool.threadCount"] = "5";
      properties["quartz.threadPool.threadPriority"] = "Normal";
      var schedFact = new StdSchedulerFactory(properties);
      var sched = schedFact.GetScheduler();
      sched.Start();
      sched.ScheduleJob(JobBuilder.Create<NaiveTaskJob<WeatherSyncTask>>().WithIdentity("WeatherSyncTask","WeatherSync").Build(),
          TriggerBuilder.Create()
          .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(18,41).WithMisfireHandlingInstructionFireAndProceed())
          .Build());
    #endregion

      //WeatherCitySyncTask wc = new WeatherCitySyncTask();
      //wc.Execute();
      //WeatherSyncTask wst = new WeatherSyncTask();
      //wst.Execute();
    }
  }
}
