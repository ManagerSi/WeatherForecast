using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherService.Task {
  public class ServiceTaskMetadata {


    public class TaskStatus {
      public const string TaskStatusRuning = "Running";
      public const string TaskStatusDone = "Done";
      public const string TaskStatusFailed = "Failed";
      public const string TaskStatusPending = "Pending";
    }

    public class ServiceTaskMetadataItem {
      public ServiceTaskMetadataItem() {
        //Logs = new List<string>();
      }
      public string ID { get; set; }
      public string TaskName { get; set; }
      public string TaskClassName { get; set; }

      //public string TaskStatus { get; set; }
      //public DateTime? LastRunTime { get; set; }
      //public string Exception { get; set; }
      //public IList<string> Logs { get; set; }
    }

    static ServiceTaskMetadata() {
      Metadata = new Dictionary<string, IList<ServiceTaskMetadataItem>>(){
          {"报表Job", 
            new List<ServiceTaskMetadataItem>(){ 
              new ServiceTaskMetadataItem{
              TaskName = "新客收卡报表",
              TaskClassName ="Mobizone.TSIC.Jobs.Report.CustomersReportJob"              
            },
              new ServiceTaskMetadataItem{
              TaskName = "日销量报表",
              TaskClassName ="Mobizone.TSIC.Jobs.Report.DaliySaleReportJob"              
            },
              new ServiceTaskMetadataItem{
              TaskName = "周销量报表",
              TaskClassName ="Mobizone.TSIC.Jobs.Report.WeeklySaleReportJob"              
            },
            new ServiceTaskMetadataItem{
              TaskName = "月销量报表",
              TaskClassName ="Mobizone.TSIC.Jobs.Report.MonthlySaleReportJob"              
            },
            new ServiceTaskMetadataItem{
              TaskName = "销量综合查询",
              TaskClassName ="Mobizone.TSIC.Jobs.Report.StoreReportJob"              
            },
                               new ServiceTaskMetadataItem{
              TaskName = "综合报表",
              TaskClassName ="Mobizone.TSIC.Jobs.Report.KPIMPGJob"              
            },
            new ServiceTaskMetadataItem{
              TaskName="VIP报表",
              TaskClassName="Mobizone.TSIC.Jobs.Report.VIPCustomersReportJob"
            }

              
          }
        },

        {"报表Task", new List<ServiceTaskMetadataItem>() {
              new ServiceTaskMetadataItem{
              TaskName = "价格跟踪",
              TaskClassName ="Mobizone.TSIC.Task.ReportTask.ReportStorePriceTrackTask"
            },

            new ServiceTaskMetadataItem{
              TaskName = "SIS 礼品发放报告",
              TaskClassName ="Mobizone.TSIC.Task.ReportTask.ReportSISGiftMonthlyTask"
            },


            new ServiceTaskMetadataItem{
              TaskName = "PG销量月报告",
              TaskClassName ="Mobizone.TSIC.Task.ReportTask.ReportSaleMonthlyTask"
            },
            new ServiceTaskMetadataItem{
              TaskName = "PG销量月明细报告",
              TaskClassName ="Mobizone.TSIC.Task.ReportTask.ReportSaleMonthlyDetailTask"
            },
           new ServiceTaskMetadataItem{
              TaskName = "区域PGO达成情况月报表",
              TaskClassName ="Mobizone.TSIC.Task.ReportTask.ReportSaleMonthlyDetailByOrgTask"
            },            
            new ServiceTaskMetadataItem{
              TaskName = "PG销量上报情况追踪表",
              TaskClassName ="Mobizone.TSIC.Task.ReportTask.ReportSaleTrackTask"
            }
        }},
        

         {"Mobile报表Task", 
            new List<ServiceTaskMetadataItem>(){ 
            new ServiceTaskMetadataItem{
              TaskName = "销量综合查询",
              TaskClassName ="Mobizone.TSIC.Task.ReportTask.MobileReport.ReportMobilePGODailyTask"
            }
          }
        },

        {"数据同步Job", 
            new List<ServiceTaskMetadataItem>(){ 

            new ServiceTaskMetadataItem{
              TaskName = "CRM数据同步以及计算",
              TaskClassName ="Mobizone.TSIC.Jobs.Sync.CRMSyncComputeJob"              
            },
             new ServiceTaskMetadataItem{
              TaskName = "CRM VIP 同步",
              TaskClassName ="Mobizone.TSIC.Jobs.Sync.CRMVIPSyncJob"              
            },
                 new ServiceTaskMetadataItem{
              TaskName = "MDM 同步",
              TaskClassName ="Mobizone.TSIC.Jobs.Sync.MDMSyncJob"              
            },

             new ServiceTaskMetadataItem{
              TaskName = "PGM Link",
              TaskClassName ="Mobizone.TSIC.Task.SyncTask.PGMSyncTask.PGMSummaryLinkTask"              
            },
            new ServiceTaskMetadataItem{
              TaskName = "MDM - PGM 同步",
              TaskClassName ="Mobizone.TSIC.Task.SyncTask.PGMSyncTask.PGMSummarySyncTask"              
            },
            new ServiceTaskMetadataItem{
              TaskName = "PGM Change - Summary 同步",
              TaskClassName ="Mobizone.TSIC.Task.SyncTask.PGMSyncTask.ChangeToSummarySyncTask"              
            },

          }
        },
        {"数据同步Task", 
            new List<ServiceTaskMetadataItem>(){ 
              new ServiceTaskMetadataItem{
                TaskName="新客",
                TaskClassName="Mobizone.TSIC.Task.SyncTask.CRMSyncTask.CRMTradeChannelDailyReportSyncTask"
              },
              new ServiceTaskMetadataItem{
                TaskName="VIP客户",
                TaskClassName="Mobizone.TSIC.Task.SyncTask.CRMSyncTask.CRMVIPCustomersBackDailyReportSyncTask"
              },
              new ServiceTaskMetadataItem{
                TaskName="人员信息",
                TaskClassName="Mobizone.TSIC.Task.SyncTask.MDMSyncTask.BaseEmployeeSyncTask"
              },
              new ServiceTaskMetadataItem{
                TaskName="BaseDictItemSyncTask",
                TaskClassName="Mobizone.TSIC.Task.SyncTask.MDMSyncTask.BaseDictItemSyncTask"
              },
              new ServiceTaskMetadataItem{
                TaskName="人店关系",
                TaskClassName="Mobizone.TSIC.Task.SyncTask.MDMSyncTask.BizEmpStoreSyncTask"
              },
               new ServiceTaskMetadataItem{
                TaskName="产品",
                TaskClassName="Mobizone.TSIC.Task.SyncTask.MDMSyncTask.BizProductSyncTask"
              },
               new ServiceTaskMetadataItem{
                TaskName="产品价格",
                TaskClassName="Mobizone.TSIC.Task.SyncTask.MDMSyncTask.BizProductPriceSyncTask"
              },
               new ServiceTaskMetadataItem{
                TaskName="门店",
                TaskClassName="Mobizone.TSIC.Task.SyncTask.MDMSyncTask.BizStoreSyncTask"
              },
            }
         },

         {"短信", 
            new List<ServiceTaskMetadataItem>(){ 
                 new ServiceTaskMetadataItem{
              TaskName = "短信推送",
              TaskClassName ="Mobizone.TSIC.Jobs.SMS.SMSSenderJob"              
            },
               new ServiceTaskMetadataItem{
              TaskName = "新用户欢迎短信",
              TaskClassName ="Mobizone.TSIC.Jobs.SMS.SMSNewUserJob"              
            },
                      new ServiceTaskMetadataItem{
              TaskName = "销量异常短信",
              TaskClassName ="Mobizone.TSIC.Task.PGMTask.SMSSaleExceptionTask"              
            },
                      new ServiceTaskMetadataItem{
              TaskName = "PGO Daily短信",
              TaskClassName ="Mobizone.TSIC.Task.NotificationTask.SMSSaleMonthlyDetailByOrgTask"            
            },
                     new ServiceTaskMetadataItem{
              TaskName = "PGO Daily Push",
              TaskClassName ="Mobizone.TSIC.Task.NotificationTask.PushSaleMonthlyDetailByOrgTask"
            },
            }
         },

         {"拜访管理Task", 
            new List<ServiceTaskMetadataItem>(){               
              new ServiceTaskMetadataItem{
                TaskName="短信通知制作拜访计划",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.NotifyCoachMsgTask"              
              },
              new ServiceTaskMetadataItem{
                TaskName="短信通知审批",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.NotifyCoachApproveMsgTask"              
              },
              new ServiceTaskMetadataItem{
                TaskName="从DSR今天的拜访计划中移除已经关闭的门店",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.AutoRemoveClosedStoreTask"              
              },
              new ServiceTaskMetadataItem{
                TaskName="每月锁定人店关系",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.LockEmpStoreTask"              
              },
              new ServiceTaskMetadataItem{
                TaskName="每月自动审批通过",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.AutoApproveTask"              
              },
              new ServiceTaskMetadataItem{
                TaskName="自动离店并更新状态",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.PGAutoLeaveStoreTask"              
              },
              new ServiceTaskMetadataItem{
                TaskName="拜访统计报告",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.ReportCoachDetailMonthlyTask"              
              },
              new ServiceTaskMetadataItem{
                TaskName="DSR wap首页报表",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.ReportTaskDSRMonthlyTask"              
              },
              new ServiceTaskMetadataItem{
                TaskName="PG Wap首页报表",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.ReportTaskPGMonthlyTask"              
              },
              new ServiceTaskMetadataItem{
                TaskName="更新拜访状态",
              TaskClassName ="Mobizone.TSIC.Task.CoachTask.UpdateDailyCoachStateTask"              
              }
            }
         },
         {"PGM Task", 
            new List<ServiceTaskMetadataItem>(){ 
              new ServiceTaskMetadataItem{
               TaskName="新客按雅培城市汇总报表",
               TaskClassName="Mobizone.TSIC.Task.PGMTask.ReportCustomersDaliyByOrgTask"
              },
              new ServiceTaskMetadataItem{
               TaskName="指标里的前3个月平均销量",
               TaskClassName="Mobizone.TSIC.Task.PGMTask.ReportPGMAvgSaleActualTask"
              },
              new ServiceTaskMetadataItem{
               TaskName="当月薪资的销量",
               TaskClassName="Mobizone.TSIC.Task.PGMTask.ReportPGMSaleActualMonthlyTask"
              },
              new ServiceTaskMetadataItem{
               TaskName="PG排班异常报告",
               TaskClassName="Mobizone.TSIC.Task.PGMTask.ReportPGScheduleExceptionTask"
              },
              new ServiceTaskMetadataItem{
               TaskName="销量异常报告",
               TaskClassName="Mobizone.TSIC.Task.PGMTask.ReportSaleExceptionTask"
              },
              new ServiceTaskMetadataItem{
               TaskName="销量汇总",
               TaskClassName="Mobizone.TSIC.Task.PGMTask.ReportSaleMonthlyByPersonTask"
              },
              new ServiceTaskMetadataItem{
               TaskName="门店月销量",
               TaskClassName="Mobizone.TSIC.Task.PGMTask.ReportStoreSaleMonthlyTask"
              },
              new ServiceTaskMetadataItem{
               TaskName="销量异常短信通知",
               TaskClassName="Mobizone.TSIC.Task.PGMTask.SMSSaleExceptionTask"
              },
              new ServiceTaskMetadataItem{
              TaskName="VIP回购率月报告",
               TaskClassName="Mobizone.TSIC.Task.ReportTask.ReportNextBuyRateMonthlyTask"
              },
              new ServiceTaskMetadataItem{
              TaskName="未上报VIP客户PG名单周报告",
               TaskClassName="Mobizone.TSIC.Task.PGMTask.ReportUnreportedVIPWeeklyTask"
              },
              new ServiceTaskMetadataItem{
              TaskName="VIP消费行为月报告",
               TaskClassName="Mobizone.TSIC.Task.ReportTask.ReportVIPSaleMonthlyByOrgTask"
              },
              
            }
         },
         {"Snapshot Task", 
            new List<ServiceTaskMetadataItem>(){ 
              new ServiceTaskMetadataItem{
               TaskName="人员信息",
               TaskClassName="Mobizone.TSIC.Task.SnapshotTask.SnapshotBaseEmployeeTask"
              },
              new ServiceTaskMetadataItem{
               TaskName="门店信息",
               TaskClassName="Mobizone.TSIC.Task.SnapshotTask.SnapshotBizStoreTask"
              },
              new ServiceTaskMetadataItem{
               TaskName="第三方人员信息",
               TaskClassName="Mobizone.TSIC.Task.SnapshotTask.SnapshotPGMEmployeeSummaryTask"
              },

            }
         },
         {"WatchDog", 
            new List<ServiceTaskMetadataItem>(){ 
                   new ServiceTaskMetadataItem{
              TaskName = "运行状态监控程序",
              TaskClassName ="Mobizone.TSIC.Jobs.WatchDog.StatusWatchDogJob"              
            },
                     new ServiceTaskMetadataItem{
              TaskName = "错误日志监控程序",
              TaskClassName ="Mobizone.TSIC.Jobs.WatchDog.FatalLogWatchDogJob"              
            },
            }
         },
         {"GeDataSetting",
             new List<ServiceTaskMetadataItem>(){
                  new ServiceTaskMetadataItem{
                  TaskName = "计算 GE Score Card",
                  TaskClassName ="Mobizone.TSIC.Task.GETask.GEScoreCardTask"              
                },
             }
         },
      };
    }

    public static readonly Dictionary<string, IList<ServiceTaskMetadataItem>> Metadata;
  }
}
