using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mobizone.TSIC.Utility {
  public class TSICConstant {
    static TSICConstant() {
      ShortNamesDict = new Dictionary<int, string>();
      for (int i = 0; i < 26; ++i) {
        ShortNamesDict[ShortNamesDict.Count] = ((char)('a' + i)).ToString();
      }
      for (int i = 0; i < 26; ++i) {
        for (int j = 0; j < 26; ++j) {
          ShortNamesDict[ShortNamesDict.Count] = ((char)('a' + i)).ToString() + ((char)('a' + j)).ToString();
        }
      }
    }

    public const decimal SystemEmpID = 1;

    public const string BizBrandAbtCode = "ABT";
    public const string BizBrandOtherCode = "OTH";

    public const string SrouceNWap = "NWAP";
    public const string SrouceWap = "WAP";
    public const string SrouceiLEAD = "APP";
    public const string SroucePGTool = "CRM";
    public const string DSR = "DSR";
    public const string PG = "PG";
    public const string StateDisable = "0";
    public const string StateEnable = "1";
    public const string WapAccount = "2";
    public const string WebAccount = "1";

    public const string IsDown = "0";
    public const string IsNotDown = "1";

    public const string FlagCustomerVIP = "CustomerVIP";
    public const string IsDevMode = "IsDevMode";
    public const string FlagWapTaskOpen = "WapTaskOpen";
    public const string FlagWapTaskUpdateOpen = "WapTaskUpdateOpen";
    public const string FlagWapCoachOpen = "WapCoachOpen";
    public const string FlagWapAutoOffline = "WapAutoOffline";
    //2013-11-12 关闭Wap端 DSR /数据查询用户
    public const string FlagWapClose = "WapClose";

    public const string FlagNewWap = "NewWap";
    public const string FlagWeChat = "WeChat";

    //public const string FlagCoachImprovement = "CoachImprovement";
    public const string FlagEnableBetaUser = "EnableBetaUser";
    public const string FlagPGMUpdateDuringImport = "PGMUpdateDuringImport";
    public const string FlagPGMRSMReview = "PGMRSMReview";
    public const string FlagParallelEnable = "FlagParallelEnable";
    public const string FlagServiceBusEnable = "ServiceBusEnable";
    public const string FlagiLeadTraningVersion = "iLeadTraningVersion";
    public const string FlagAPICompressionEnable = "APICompressionEnable";
    public const string CfgHostUrl = "HostUrl";
    public const string SftpHost = "SftpHost";
    public const string BCodeBatchUrl = "BCodeBatchFile";
    public const string GraphStoreFile = "GraphStoreFile";
    public const string APILoginTokenTimeoutMins = "APILoginTokenTimeoutMins";


    public const string APNSCertFile = "APNSCertFile";
    public const string APNSCertFilePassword = "1234";
    public const string APNSProduction = "APNSProduction";


    public const string ReplaceThisHolder = "[replacethis]";

    public const int SMSPriorityLow = 10;
    public const int SMSPriorityNormal = 5;
    public const int SMSPriorityHigh = 4;
    public const string HeaderToken = "token";

    /// <summary>
    /// AttributeID
    /// </summary>
    public const string AttributeBase = "1";
    public const string AttributeGE = "0";

    /// <summary>
    /// 一箱的罐数
    /// </summary>
    public const decimal OnePackNum = 24;

    public const decimal OneDTNum = 12;
    /// <summary>
    /// 一大听的罐数
    /// </summary>
    public const decimal OneTinnedNum = 2;

    //用于压缩字典等的快捷方式
    public static readonly Dictionary<int, string> ShortNamesDict;

    //指标组发布状态
    public const string NonPubliced = "未发布";
    public const string IsPubliced = "已发布";
    //考试状态
    public const string ExamResult = "ExamResult";
    public const string ExamPre = "上一题";
    public const string ExamNext = "下一题";
    public const string ExamCommit = "提交";

    public static string TooManyExportRecord {
      get { return string.Format("当前查询结果超过{0}条，无法导出。", Configuration.MaxExportRecord.ToString()); }
    }

    public static string TooManyDetailExportRecord {
      get { return string.Format("当前查询结果主表记录超过{0}条，从表记录过多无法导出。", Configuration.MaxDetailExportRecord.ToString()); }
    }

    public const int PGCRMChannel = 2;

    public const string CallInterfaceError = "第三方CRM系统暂时无法连接：";
    public const string CallInterfaceErrorBack = "第三方CRM系统错误信息：";
    //532锁定日期
    public const string ConfigLockDate = "LockDate";
    //and crm 
    public const string ANDCRMSuccResult = "00000000";
    public const string ANDCRMErrResult = "FFFFFFFF";

    public const string FlagChYes = "是";
    public const string FlagChNo = "否";
    public const string FlagChExist = "有";
    public const string FlagChNone = "无";
    public const string FlagYes = "YES";
    public const string FlagNo = "NO";
    public const string FlagN = "N";
    public const string FlagY = "Y";

    public const string FlagNoAudit = "未审核";
    public const string FlagAuditPending = "待审核";
    public const string FlagAuditPass = "通过";
    public const string FlagAuditNot = "未通过";


    public const string FlagOOS = "FlagOOS";
    public const string FlagWapLoginClose = "WapLoginClose";

    public const string FlagNoMsg = "暂无数据";
  }
}
