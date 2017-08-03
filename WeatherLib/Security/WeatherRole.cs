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
      PGBetaUserEmpCode = new HashSet<string>();
      PGMBetaUserEmpCode = new HashSet<string>();
      KPIBetaUserEmpCode = new HashSet<string>();
      JasonBetaUserEmpCode = new HashSet<string>();
      KPISellInOutBetaUserEmpCode = new HashSet<string>();
      KPIKABetaUserEmpCode = new HashSet<string>();
      VIPBetaUserEmpCode = new HashSet<string>();
      FOCBetaUserEmpCode = new HashSet<string>();
      PGMCustomerBetaUserEmpCode = new HashSet<string>();
      CRMCustomerEmpCode = new HashSet<string>();
      ReportBetaUserEmpCode = new HashSet<string>();

      if (Configuration.Flag_EnableBetaUser) {
        try {
          // 系统管理员就是一个测试账户
          BetaUserEmpCode.Add("system");
          /*
          //雅培城市
          HashSet<decimal> orgCity = new HashSet<decimal>();
          orgCity.Add(520); //崇明
          orgCity.Add(511); //黄石
          orgCity.Add(521); //金山
          orgCity.Add(332); //荆州
          orgCity.Add(386); //上海
          orgCity.Add(334); //武汉
          orgCity.Add(335); //襄樊
          orgCity.Add(336); //宜昌
          // PG FOR CRM测试
          //BetaUserEmpCode.AddAll(new string[] { "P00080", "P03632" });
          //将上海的所有DSR添加到测试列表中
          using (var context = new TSICDBContext()) {
            var uw = new UnitOfWork(context);
            var orgBL = BLLFactory.CreateNew<IBaseOrgBL>(uw);
            var orgSH = orgBL.ExpendOrgToAbtCityByCached(260);
            var dsrList = uw.BaseEmployeeRepository.GetAllEmpTypeDSR()
              .Where(i => orgSH.Contains(i.OrgID.Value) && i.EmpCode != null)
              .Select(i => i.EmpCode);
            BetaUserEmpCode.AddAll(dsrList);
            // 指标分配测试用户
            //添加PG测试人员
            var pgList = uw.BaseEmployeeRepository.GetAllEmpTypePG()
              .Where(i => orgCity.Contains(i.OrgID.Value) && i.EmpCode != null)
              .Select(i => i.EmpCode);
            BetaUserEmpCode.AddAll(pgList);

            
          }
           * BetaUserEmpCode.AddAll(new string[] { "P01751", "P09144", "P02445" });
          */



          log.Info("BetaUser Count = " + BetaUserEmpCode.Count);
        } catch (Exception e) {
          log.Fatal(e);
        }

        try {
          // 系统管理员就是一个测试账户
          PGBetaUserEmpCode.Add("system");
          PGBetaUserEmpCode.AddAll(new string[] { "P01751", "P09144", "P02445" });

          log.Info("PGBetaUser Count = " + PGBetaUserEmpCode.Count);
        } catch (Exception e) {
          log.Fatal(e);
        }

        try {
          ReportBetaUserEmpCode.AddAll(new string[] { "P03048", "P04045", "system" });

          //添加首页测试用户
          // Jason Lucy Angela
          JasonBetaUserEmpCode.AddAll(new string[] { "P03048", "P04045", "system", "P12097", "P08607", "P03047","bond.wang" });
          log.Info("Jason BetaUser Count = " + JasonBetaUserEmpCode.Count);
        } catch (Exception e) {
          log.Fatal(e);
        }

        try {

          // 添加PGM BetaUser

          PGMBetaUserEmpCode.Add("system");
          var shBetaPGM = "P04861,P00001,P00920,P04860,P00020,P00010,P07499,P00024,P00689,P13378,P07800,P12493,P02841,P12491,P12459,P03406";
          var AddPGM = "P03054,P03064,P02841,P03244,P12459,P04892,P07800,P14124" +
            ",P00027" //员工系统号：P00027，手机：13641947802 
            + ",P18039" //员工系统号：P18039，手机：13817801500  黄明军 --08-23
            + ",P19832" //员工系统号：P19832，手机：13816982296  李佳轶
            ;
          PGMBetaUserEmpCode.AddAll(shBetaPGM.Split(','));
          PGMBetaUserEmpCode.AddAll(AddPGM.Split(','));
          log.Info("PGMBetaUser Count = " + PGMBetaUserEmpCode.Count);
        } catch (Exception e) {
          log.Fatal(e);
        }

        try {
          // 添加KPI BetaUser
          KPIBetaUserEmpCode.Add("system");
         // var shBetaPGM = "P03076,P03046,P03054,P03047,P03244,P03066,P04529,P05076,P03050,P03055";
          //2013-05-17 Bond
          var shBetaPGM = "P16292,P04892,P05076,P11051,P16497,P03076,P13422,P03067,P15203,P03244,P04825,P03185,P08779,P04845,P03190,P03054"; 
          KPIBetaUserEmpCode.AddAll(shBetaPGM.Split(','));
          log.Info("KPIBetaUser Count = " + KPIBetaUserEmpCode.Count);

          PGMCustomerBetaUserEmpCode.AddAll(new string[] { "P05076", "P03433", "P03142" });
          log.Info("PGMCustomerBetaUser Count = " + PGMCustomerBetaUserEmpCode.Count);
        } catch (Exception e) {
          log.Fatal(e);
        }

        try
        {
            // 添加KPI BetaUser
            KPISellInOutBetaUserEmpCode.Add("system");
            //2013-05-17 Bond  P03047
            var shBetaKPISellInOut = "P03048,system,P16292,P04892,P05076,P09425,P16497,P03076,P03054,P10414,P03067,P04789,P03071";
            KPISellInOutBetaUserEmpCode.AddAll(shBetaKPISellInOut.Split(','));
            log.Info("KPISellInOutBetaUser Count = " + KPISellInOutBetaUserEmpCode.Count);

            KPIKABetaUserEmpCode.Add("system");
            //2013-05-17 Bond
            var shBetaKPIKA = "P15203,P13422";
            KPIKABetaUserEmpCode.AddAll(shBetaKPIKA.Split(','));
            log.Info("KPIKABetaUser Count = " + KPIKABetaUserEmpCode.Count);
        }
        catch (Exception e)
        {
            log.Fatal(e);
        }

        try {
          //VIP 导出VIP明细带手机号码
            VIPBetaUserEmpCode.AddAll(new string[] { "P03048", "P06041", "P08607", "P12104", "system", "P14124", "P15153", "P12191", "P09685", "P04475", "P11561", "P00632", "P03068", "P03200", "P17777", "P19832" });
          log.Info("VIP BetaUser Count = " + VIPBetaUserEmpCode.Count);

          FOCBetaUserEmpCode.AddAll(new string[]{"P08607","P17777"});
          log.Info("FOC BetaUser Count = " + FOCBetaUserEmpCode.Count);
        } catch (Exception e) {
          log.Fatal(e);
        }

        //CRM 新客信息
        try {
           CRMCustomerEmpCode.AddAll(new string[] { "P07010", "P00734", "P01478", "P00508", "P15664", "P09059", "P01312", "P01402", "P01408", "P00016"});
           log.Info("CRMCustomer BetaUser Count = " + CRMCustomerEmpCode.Count);
        } catch (Exception e) {
          log.Fatal(e);
        }

      }
    }
    public const string WapTypeDateQuery = "EmpType:DateQuery";
    public const string WapTypeDSR = "EmpType:DSR";
    public const string WapTypePG = "EmpType:PG";
    public const string WapTypePrefix = "EmpType:";
    public const string BindStorePrefix = "BindStore:";
    public const string BindAnyStore = BindStorePrefix + "any";
    public const string InStorePrefix = "InStore:";
    public const string RoleAdmin = "系统管理员";
    public const string RoleStandQuery = "标准查询";
    public const string RoleAdvQuery = "高级查询";
    public const string UserTypePrefix = "UserType:";
    public const string UserTypeWeb = "UserType:web";// + TSICConstant.WebAccount;
    public const string UserTypeWap = "UserType:wap";// + TSICConstant.WapAccount;

    public const string Role532DSR = "532数据上报";
    public const string RoleCityManagment = "城市经理";
    public const string RoleTBBSAccount = "TBBS城市经理";
    //后台登陆
    //public const string WebLogin = "后台登录";
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
    /// <summary>
    /// RTMK
    /// </summary>
    public const string PGM_RTMK = "PGM-RTMK"; // 职责范围等价于与RSM
    /// <summary>
    /// NTMK
    /// </summary>
    public const string PGM_NTMK = "PGM-NTMK"; // 提供新客汇总报表
    /// <summary>
    /// PGM-大区助理
    /// </summary>
    public const string PGM_RNGAssistant = "PGM-大区助理";
    /// <summary>
    /// 
    /// </summary>
    public const string PGM_RBD = "PGM-RBD";
    /// <summary>
    /// PGM-总部
    /// </summary>
    public const string PGM_HC = "PGM-总部";
    // Coach相关角色
    public const string CoachTypePrefix = "Coach-";
    /// <summary>
    /// 商店拜访
    /// </summary>
    public const string CoachDSR = CoachTypePrefix + "商店拜访";
    /// <summary>
    /// 拜访审批
    /// </summary>
    public const string CoachcReviewer = CoachTypePrefix + "拜访审批";
    /// <summary>
    /// 拜访查看
    /// </summary>
    public const string CoachViewer = CoachTypePrefix + "拜访查看";
    // KPI相关角色
    public const string KPITypePrefix = "KPI-";
    public const string KPIPerson = "KPI-考核人员";
    public const string KPIOrgViewer = "KPI-按区域查看";
    public const string KPIHCTopListViewer = "KPI-总部KA排名查看";
    public const string KPIAreaListViewer = "KPI-区域排名查看";
    public const string KPIDataSetting = "KPI-数据设置";

    //医务
    public const string RoleEAdmin = "医务";

    //CRM审核
    public const string RoleCRMCheck = "CRM审核";


    public const string BetaUser = "BetaUser:";
    // 用于测试的帐号
    public readonly static HashSet<string> BetaUserEmpCode;
    public const string PGBetaUser = BetaUser + "PG";
    public readonly static HashSet<string> PGBetaUserEmpCode;
    public const string PGMBetaUser = BetaUser + "PGM";
    public readonly static HashSet<string> PGMBetaUserEmpCode;
    public const string KPIBetaUser = BetaUser + "KPI";
    public readonly static HashSet<string> KPIBetaUserEmpCode;
    public const string KPISellInOutBetaUser = BetaUser + "KPISellInOut";
    public readonly static HashSet<string> KPISellInOutBetaUserEmpCode;
    public const string KPIKABetaUser = BetaUser + "KPIKA";
    public readonly static HashSet<string> KPIKABetaUserEmpCode;

    public const string JasonBetaUser = BetaUser + "Jason";
    public readonly static HashSet<string> JasonBetaUserEmpCode;

    public const string FOCSettingUser = BetaUser + "FOC";
    public readonly static HashSet<string> FOCBetaUserEmpCode;
    //VIP　
    public const string VIPBetaUser = BetaUser + "VIP";
    public readonly static HashSet<string> VIPBetaUserEmpCode;

    public const string  ReportBetaUser = BetaUser + "Report";
    public readonly static HashSet<string> ReportBetaUserEmpCode;

    //CRM新客信息测试组
    public const string CRMCustomerBetaUser = BetaUser + "CRMCustomer";
    public readonly static HashSet<string> CRMCustomerEmpCode;

    public const string PGMCustomerBetaUser = BetaUser + "PGMCustomer";
    public readonly static HashSet<string> PGMCustomerBetaUserEmpCode;
    

    public const string MsgUser = "短信群发";

    public const string MessageUser = "信息发布";
    public const string ELearnUser = "知识库发布";

    public const string ReviewShare = "点评分享";

    public const string Marketing = "Marketing";

    // iLead WebViewer
    // 由Url Token自动登录的iLeadWebViewer
    public const string iLeadWebViewer = "iLeadWebViewer";
    public static string BindStore(decimal storeID) {
      return BindStorePrefix + storeID.ToString();
    }

    public static string InStore(decimal storeID) {
      return InStorePrefix + storeID.ToString();
    }
  }
}
