using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using WeatherService.Task.WeatherSync;


namespace WeatherService.Jobs {
  // 修改的同时，请修改
  //  Mobizone.TSIC.Task.ServiceTaskMetadata
  public class JobFactory {
    // 任务时间安排
    // 12: 00 Snapshot


    public static void ConfigTSICJobs(IScheduler sched) {
      ConfigReportJobs(sched);

      // 系统清理
      sched.ScheduleJob(JobBuilder.Create<NaiveTaskJob<WeatherSyncTask>>().WithIdentity("WeatherSyncTask","WeatherSync").Build(),
        TriggerBuilder.Create()
        .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(16,30).WithMisfireHandlingInstructionFireAndProceed())
        .Build());

      //// 系统清理
      //sched.ScheduleJob(JobBuilder.Create<NaiveTaskJob<DailyClearTask>>().WithIdentity("DailyClearTask", "System").Build(),
      //  TriggerBuilder.Create()
      //  .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(4, 55).WithMisfireHandlingInstructionFireAndProceed())
      //  .Build());

      //// 监控程序
      //sched.ScheduleJob(JobBuilder.Create<StatusWatchDogJob>().WithIdentity("StatusWatchDogJob", "WatchDog").Build(),
      //  TriggerBuilder.Create()
      //  .WithSchedule(CronScheduleBuilder.CronSchedule("0 0/1 7-22 * * ?").WithMisfireHandlingInstructionDoNothing())
      //  .Build());
      
    }

    private static void ConfigReportJobs(IScheduler sched) {
     
      //// 月销量报表
      //sched.ScheduleJob(JobBuilder.Create<MonthlySaleReportJob>().WithIdentity("MonthlySaleReport", "Report").Build(),
      //  TriggerBuilder.Create()
      //  .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(1, 1).WithMisfireHandlingInstructionFireAndProceed())
      //  .Build());

      //// 首页PGOSummary 
      //sched.ScheduleJob(JobBuilder.Create<NaiveTaskJob<ReportPGOSummaryTask>>().WithIdentity("ReportPGOSummaryTask", "Report").Build(),
      //  TriggerBuilder.Create()
      //  .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(2, 05).WithMisfireHandlingInstructionFireAndProceed())
      //  .Build());


    }

  }
}

