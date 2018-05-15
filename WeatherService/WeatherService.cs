using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;
namespace WeatherService {
  partial class WeatherService : ServiceBase {
    private static readonly ILog log = LogManager.GetLogger(typeof(WeatherService));
    ISchedulerFactory schedFact;
    IScheduler sched;
    public WeatherService() {
      InitializeComponent();
    }
    public void debugStart() {
      OnStart(null);
    }
    protected override void OnStart(string[] args) {
      // TODO:  在此处添加代码以启动服务。
      try {
        SetupErrorHandle();

        log.Info("WeatherService Starting...");
        NameValueCollection properties = new NameValueCollection();
        properties["quartz.scheduler.instanceName"] = "WeatherService";

        // set thread pool info
        properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
        properties["quartz.threadPool.threadCount"] = "5";
        properties["quartz.threadPool.threadPriority"] = "Normal";

        schedFact = new StdSchedulerFactory(properties);
        sched = schedFact.GetScheduler();
        sched.Start();
        ConfigJobs();
        //ApplePushService.Start();
      } catch(Exception e) {
        log.Fatal("TSICService Fatal...",e);
        //Jobs.WatchDog.StatusWatchDogJob.SendWarningMsg(new List<string>() { "TSIC后台服务启动失败" });
        throw;
      }
    }

    private event UnhandledExceptionEventHandler exceptionHandler;
    private void SetupErrorHandle() {
      exceptionHandler = new UnhandledExceptionEventHandler(CurrentDomainOnUnhandledException);
      AppDomain.CurrentDomain.UnhandledException += exceptionHandler;
    }

    private void CurrentDomainOnUnhandledException(object sender,UnhandledExceptionEventArgs unhandledExceptionEventArgs) {
      try {
        log.Fatal("Domain Fatal");
        log.Fatal("IsTerminating = " + unhandledExceptionEventArgs.IsTerminating);
        log.Fatal(unhandledExceptionEventArgs.ExceptionObject);
      } catch(Exception) {

      }
    }


    protected void ConfigJobs() {
      Jobs.JobFactory.ConfigTSICJobs(sched);
    }

    protected override void OnStop() {
      // TODO:  在此处添加代码以执行停止服务所需的关闭操作。
      sched.Shutdown(false);
      sched = null;
      schedFact = null;
      //Mobizone.TSIC.Models.TSICRavenDocContext.DisposeDocumentStore();
      //ApplePushService.Stop();
      AppDomain.CurrentDomain.UnhandledException -= exceptionHandler;
      log.Info("WeatherService End...");

    }
  }
}
