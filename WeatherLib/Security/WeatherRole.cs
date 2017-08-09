using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherLib.Utility;
using WeatherLib.BLL;
using WeatherLib.DAL;
using WeatherLib.Model;
using log4net;
namespace WeatherLib.Security {
  public class WeatherRole {
    private static readonly ILog log = LogManager.GetLogger(typeof(WeatherRole));
    static WeatherRole() {
      BetaUserEmpCode = new HashSet<string>();

      ReportBetaUserEmpCode = new HashSet<string>();

      if (Configuration.Flag_EnableBetaUser) {
        try {
          // 系统管理员就是一个测试账户
          BetaUserEmpCode.Add("system");
         


          log.Info("BetaUser Count = " + BetaUserEmpCode.Count);
        } catch (Exception e) {
          log.Fatal(e);
        }
        
      }
    }
    public const string WapTypeDateQuery = "EmpType:DateQuery";
    public const string WapTypeDSR = "EmpType:DSR";
 
    //PGM相关角色
    public const string PGMTypePrefix = "PGM-";
    public const string PGM_PGS = "PGM-PGS";
    public const string PGM_PurePGS = "PGM-PurePGS";

    //是否有下属
    public const string ParentEmp = "IsParent";


    /// <summary>
    /// PGM-小区助理
    /// </summary>
    public const string PGM_AreaAssistant = "PGM-小区助理";
    /// <summary>
    /// RPGM
    /// </summary>
    public const string PGM_RPGM = "PGM-RPGM";
    /// <summary>
    /// RSM
    /// </summary>
    public const string PGM_RSM = "PGM-RSM"; // 负责小区
   
    // Coach相关角色
    public const string CoachTypePrefix = "Coach-";
    /// <summary>
    /// 商店拜访
    /// </summary>
    public const string CoachDSR = CoachTypePrefix + "商店拜访";
  

    public const string BetaUser = "BetaUser:SI";
    // 用于测试的帐号
    public readonly static HashSet<string> BetaUserEmpCode;
  
    public const string  ReportBetaUser = BetaUser + "Report";
    public readonly static HashSet<string> ReportBetaUserEmpCode;

    public const string MsgUser = "短信群发";

    public const string MessageUser = "信息发布";
    public const string ELearnUser = "知识库发布";

    public const string ReviewShare = "点评分享";

    public const string Marketing = "Marketing";
    public const string BindStorePrefix = "BindStore:";
    public const string BindAnyStore = BindStorePrefix + "any";
    public const string InStorePrefix = "InStore:";
    public const string RoleAdmin = "管理员";
    // iLead WebViewer
    // 由Url Token自动登录的iLeadWebViewer
    public const string iLeadWebViewer = "iLeadWebViewer";
  
  }
}
