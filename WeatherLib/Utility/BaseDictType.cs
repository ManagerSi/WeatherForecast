using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WeatherForecast.BLL;
using WeatherForecast.Cache;
using WeatherForecast.Models;

namespace WeatherForecast.Utility {
  public partial class BaseDictType {
    /// <summary>
    /// 客户组织编号
    /// </summary>
    public const string DictTypeStoreAccount = "account";
    //NKA  LKA  EBBS Other B2C TBBS LBBS
    public const string DictTypeStoreKAAccount = "KAAccount";
    public const string DictTypeLKAAccount = "LKA";
    public const string DictTypeNKAAccount = "NKA";
    public const string DictTypeBBSAccount = "EBBS";
    public const string DictTypeLBBSAccount = "LBBS";
    public const string DictTypeB2CAccount = "B2C";
    public const string DictTypeTBBSAccount = "TBBS";
    public const string DictTypeOtherAccount = "Other";
    /// <summary>
    /// 渠道类型
    /// </summary>
    public const string DictTypeChannelType1 = "channelType1";

    public const string DictChannelTypeModern = "现代渠道";
    public const string DictChannelTypeMother = "母婴渠道";

    public const string DictTypeChannelType2 = "channelType2";
    public const string DictChannelType2DBBS = "钻石店(DBBS)";
    /// <summary>
    /// 商店类型 BBS/LSM/...
    /// </summary>
    public const string DictTypeStoreType1 = "storeType1";
    public const string DictTypeStoreType2 = "storeType2";
    public const string DictTypeStoreType3 = "storeType3";
    public const string DictStoreType3PG = "PG";
    public const string DictStoreType3NonPG = "Non-PG";
    public const string DictTypeEmpType = "empType";
    public const string DictEmpTypeDataQuery = "数据查询";
    public const string DictEmpTypePG = "促销员";
    public const string DictEmpTypeDSR = "商店拜访";
    public const string DictEmpTypeEW = "医务";

    public const string DictTypeDuty = "duty";
    public const string DictTypePosition = "position"; //职位名称
    public const string DictTypeCompet = "brand"; //PND竞品品牌
    public const string DictTypeMNDCompet = "mndbrand"; //MND竞品品牌
    public const string DictTypeStudy = "study";
    public const string DictTypeMessage = "message";
    public const string DictTypeProdLine = "prodLine";
    public const string DictTypeProdType = "prodType";
    public const string DictProdTypeS1 = "第1阶段";
    public const string DictProdTypeS2 = "第2阶段";
    public const string DictProdTypeS3 = "第3阶段";
    public const string DictProdTypeS4 = "第4阶段";
    public const string DictTypeProdCategory = "prodCategory";
    public const string DictTypePackID = "packId";
    public const string DictDutyDSR = "DSR";
    public const string DictDutyPG = "PG";
    public const string DictDutyPGS = "PGS";
    public const string DictTypeShelfType = "shelfType";
    public const string DictTypeSTPG = "pgStoreType";
    public const string DictTypeSpecID = "specId";
    public const string DictTypeAssitID = "TM助理";

    public const string DictTypePriceState = "priceState";
    public const string DictPriceStateLessThanRedPrice = "低于红线价";
    public const string DictPriceStateLessThanGreenPrice = "低于绿线价";
    public const string DictPriceStateLessThanYellowPrice = "低于黄线价";
    //2013-06-17 价格状态RSP2
    public const string DictPriceStateRSP2 = "RSP2";

    public const string DictTypeDivision = "division";
    public const string DictReport = "Report";
      
    //上传图片3种路径
    public const string PlanPicUploadPath = "PlanPicUploadPath";
    public const string CompetPicUploadPath = "CompetPicUploadPath";
    public const string ShelfPicUploadPath = "ShelfPicUploadPath";
    public const string QuesPicUploadPath = "QuesPicUploadPath";
    public const string TempFileUploadPath = "TempFileUploadPath";
    public const string BizMsgFileUploadPath = "BizMsgFileUploadPath";
    public const string ShelfTargetFileUploadPath = "ShelfTargetFileUploadPath";
    public const string PGMBudgetPicUploadPath = "PGMBudgetPicUploadPath";
    public const string KPIFileUploadPath = "KPIFileUploadPath";
    public const string FileDonwloadPath = "FileDonwloadPath";
    public const string ReviewFileUploadPath = "ReviewFileUploadPath";
    //用户头像
    public const string UserIconUploadPath = "UserIconUploadPath";

    //PGM 上传文件地址
    public const string PGMFileUploadPath = "PGMFileUploadPath";
    public const string PGMPicUploadPath = "PGMPicUploadPath";
    //CRM baby 
    public const string CRMBabyPicUploadPath = "CRMBabyPicUploadPath";
    //会员小票
    public const string CustomerBillUploadPath = "CustomerBillUploadPath";
    //foc小票路径
    public const string FOCUrl = "FOCUrl";
    //门店照片类型
    public const string ImageTypeShelf = "门店陈列照片";
    public const string ImageTypeCompet = "竞品照片";
    public const string ImageTypeDisplay = "促销活动照片";
    public const string ImageTypeQues = "调查问卷照片";
	public const string StoreImageTypeQues = "门店照片上报";
    //门店照片上报的ID

    public static decimal PhoteUploadQuesID = Configuration.IsDevMode ?  20157 : 188; //测试站点 20157 本地 20012 TSIC 188
	public static string ImagUploadCheckpoint1start = "checkpoint1start";
	public static string ImagUploadCheckpoint1end = "checkpoint1end";
	public static string ImagUploadCheckpoint2start = "checkpoint2start";
	public static string ImagUploadCheckpoint2end = "checkpoint2end";
	//调查问卷类型
	public const string DictTypeQuesType = "quesType";
    public const string DictQuesTypeRecordFinal = "记录结果";
    public const string DictQuesTypeRecordProcess = "记录过程";
    //public const string DictQuesTypeRcvSnd = "收发存";

    //调查问卷项类型
    public const string DictTypeQuesItemType = "quesItemType";
    public const string DictQuesItemTypeNumric = "数值";
    public const string DictQuesItemTypeText = "文本";
    public const string DictQuesItemTypeBool = "逻辑";
    public const string DictQuesItemTypeRcvSnd = "收发存";
    public const string DictQuesItemTypeImage = "照片";
    public const string DictQuesItemTypeMSelect = "多选";
    public const string DictQuesItemTypeSSelect = "单选";

    public const int DictQuesItemTypeSSelectID = 5500;
    public const int DictQuesItemTypeMSelectID = 5501;

    public static IList<string> GetQuesItemTypeList() {
        

            return new List<string>() {
    DictQuesItemTypeNumric, DictQuesItemTypeText,DictQuesItemTypeBool,DictQuesItemTypeRcvSnd,DictQuesItemTypeImage,DictQuesItemTypeSSelect,DictQuesItemTypeMSelect
    };
        
    //    return new List<string>() {
    //DictQuesItemTypeNumric, DictQuesItemTypeText,DictQuesItemTypeBool,DictQuesItemTypeRcvSnd,DictQuesItemTypeImage
    //};
    }

    //拜访审批状态
    public const string DictTypeCoachApproveState = "CoachApproveState";
    public const string DictCoachApproveStateEdit = "编辑";
    public const string DictCoachApproveStateApprove = "批准";
    public const string DictCoachApproveStateReject = "拒绝";
    public const string DictCoachApproveStateSubmit = "待审批";


    public static readonly Dictionary<string, string> CoachApproveStateDict = new Dictionary<string, string>() {
    {DictCoachApproveStateEdit,"未提交"},
    {DictCoachApproveStateSubmit,"已提交待审批"},
    {DictCoachApproveStateApprove,"已审批"},
    {DictCoachApproveStateReject,"被拒绝"},
    };
    //门店类型
    public const string DictTypeCoachStoreType = "StoreCoachType";
    public const string DictCoachStoreTypeA = "A";
    public const string DictCoachStoreTypeB = "B";
    public const string DictCoachStoreTypeC = "C";
    public const string DictCoachStoreTypeD = "D";
    public const string DictCoachStoreTypeDefault = DictCoachStoreTypeD;
    public static int GetCoachStoreRate(string coachStoreType) {
      int rate = 0;
      switch (coachStoreType) {
        case "A":
          rate = 8;
          break;
        case "B":
          rate = 4;
          break;
        case "C":
          rate = 2;
          break;
        default:
          rate = 1;
          break;
      }
      return rate;
    }

    //门店拜访状态
    public const string DictTypeStoreCoachState = "StoreCoachState";
    public const string DictStoreCoachStateVisited = "已拜访";
    public const string DictStoreCoachStateTurned = "转访";
    public const string DictStoreCoachStateMissed = "失访";
    public const string DictStoreCoachStateUnvisit = "未拜访";
    public const string DictStoreCoachStateCancel = "取消拜访";
    public const string DictStoreCoachStateVisiting = "拜访中";
    public const string DictStoreCoachStateInvalid = "无效拜访";

    //MI NPG店上报状态
    public const string DictMIStoreCoachStateUnvisit = "未上报";
    public const string DictMIStoreCoachStateVisited = "已上报";

    //public static Dictionary<int,string> GetCoachStoreTypeList() {
    //  Dictionary<int, string> dict = new Dictionary<int, string>();
    //  dict.Add(465, CoachStoreTypeA);
    //  dict.Add(466, CoachStoreTypeB);
    //  dict.Add(467, CoachStoreTypeC);
    //  dict.Add(468, CoachStoreTypeD);
    //  return dict;
    //}




    /// <summary>
    /// 竞品品牌
    /// </summary>
    public const string DictTypeCompeProductCate = "brand";
    /// <summary>
    /// 包装类型
    /// </summary>
    public const string DictTypeProdPackSpec = "packid";
    /// <summary>
    /// 消息
    /// </summary>
    public const int MessageTypeID = 403; //345

    /// <summary>
    /// 学习编号
    /// </summary>
    public const int MessageTypeELearning = 400; //342
    /// <summary>
    /// 产品知识
    /// </summary>
    public const int MsgProductTypeID = 401; //343
    /// <summary>
    /// 营销技巧
    /// </summary>
    public const int MsgSaleTypeID = 402;//344

    public const int MsgTypeCorpNewsID = 251;
    public const int MsgTypeProdNewsID = 252;
    public const int MsgTypeImportantNewsID = 253;
    /// <summary>
    /// 消息类别
    /// </summary>
    /// <returns></returns>
    public static IList<SelectListItem> MsgTypeList() {
      IList<SelectListItem> typeList = new List<SelectListItem>();
      typeList.Add(new SelectListItem() { Text = "公司消息", Value = "251" });
      typeList.Add(new SelectListItem() { Text = "产品消息", Value = "252" });
      typeList.Add(new SelectListItem() { Text = "重要消息", Value = "253" });
      return typeList;
    }

    /// <summary>
    /// 促销活动TypeID
    /// </summary>
    /// <returns></returns>
    public static IDictionary<int, string> DisplayTypdDict() {

      var dict = new Dictionary<int, string>(){
        {0, "地堆"},
        {1, "端架"},
        {2, "包柱"},
        {3, "其他"},
        
      };
      return dict;
    }
    public const int WapTypeDSR = 241;
    public const int WapTypePG = 240;

    public const int WapTypeSIS = 274;
    public const string SISEmpTag = "E";

    /// <summary>
    /// 短信息类别
    /// </summary>
    /// <returns></returns>
    public const string DictTypeMobileMgsType = "MobileMgsType";
    public const string DictMobileMsgTypeLowPrice = "低价报警";
    public const string DictMobileMsgTypePasswordReset = "密码重置";
    public const string DictMobileMsgTypeOOS = "缺货提醒";
    public const string DictMobileMsgTypeNewUser = "帐号开通";
    public const string DictMobileMsgTypeCoachPlan = "拜访计划";
    public const string DictMobileMsgTypeDefault = "其它类型";
    public const string DictMobileMsgTypeSaleException = "销量异动";
    public const string DictMobileMsgTypePGMApproval = "PGM审批";
    public const string DictMobileMsgTypeRemind = "信息提醒";
    public const string DictMobileMsgTypeLoc = "门店重定位";


    //public const int MobileMgsTypeVstPrise = 407;
    //public const int MobileMgsTypeVstOOS = 408;
    //public const int MobileMgsTypeRestPassword = 409;

    ///用户类型
    public const string DictTypeUserTypeSystem = "系统用户";
    public const string DictTypeUserTypeMobile = "手机用户";

    // KPI考核职位
    public const string DictTypeKPIType = "kpiType";
    /// <summary>
    /// 代表
    /// </summary>
    public const string DictKPITypeRepresentative = "代表";
    /// <summary>
    /// 主管
    /// </summary>
    public const string DictKPITypeDirector = "主管";
    /// <summary>
    /// 地区经理
    /// </summary>
    public const string DictKPITypeAreaManager = "地区经理";
    /// <summary>
    /// 大区经理/副总监
    /// </summary>
    public const string DictKPITypeRNGDirector = "大区经理/副总监";
    /// <summary>
    /// 总监
    /// </summary>
    public const string DictKPITypeHCDirector = "总监";
    /// <summary>
    /// 地区KA经理
    /// </summary>
    public const string DictKPITypeAreaKAManager = "地区KA经理";
    /// <summary>
    /// 地区KA主管
    /// </summary>
    public const string DictKPITypeAreaKADirector = "地区KA主管";
    /// <summary>
    /// 总部KA代表
    /// </summary>
    public const string DictKPITypeHCKARepresentative = "总部KA代表";
    /// <summary>
    /// 总部KA经理
    /// </summary>
    public const string DictKPITypeHCKAManager = "总部KA经理";
    /// <summary>
    /// 总部KA主管
    /// </summary>
    public const string DictKPITypeHCKADirector = "总部KA主管";

    /// <summary>
    /// 总部KA经理-2
    /// </summary>
    public const string DictKPITypeHCKAManagerType2 = "总部KA经理-2";
    
      /// <summary>
    /// 大区KA经理
      /// </summary>
    public const string DictKPITypeRNGKAManager = "大区KA经理";
    
      
    //NEW POSITION 2014 -- Jason.R 

    /// <summary>
    /// PG门店销量占比小于20%的人员
    /// </summary>
    public const string DictKPITypeT31 = "T31";
    /// <summary>
    /// 总部客户组KA经理  
    /// </summary>
    public const string DictKPITypeT32 = "T32";
    /// <summary>
    /// 总部非客户组KA经理
    /// </summary>
    public const string DictKPITypeT33 = "T33";

    /// <summary>
    /// 大区经理
    /// </summary>
    public const string DictKPITypeT34 = "T34";

    // 2013
    /// <summary>
    /// 总部KA代表-有AR数据
    /// </summary>
    public const string DictKPITypeT1 = "T1";
    /// <summary>
    /// 总部KA主管-有AR数据
    /// </summary>
    public const string DictKPITypeT2 = "T2";
    /// <summary>
    /// 总部非客户组KA经理-有AR数据
    /// </summary>
    public const string DictKPITypeT3 = "T3";
    /// <summary>
    /// 总部客户组KA经理-有AR数据
    /// </summary>
    public const string DictKPITypeT4 = "T4";
    /// <summary>
    /// 总部KA代表-无AR数据
    /// </summary>
    public const string DictKPITypeT5 = "T5";
    /// <summary>
    /// 总部KA主管-无AR数据
    /// </summary>
    public const string DictKPITypeT6 = "T6";
    /// <summary>
    /// 总部非客户组KA经理-无AR数据
    /// </summary>
    public const string DictKPITypeT7 = "T7";
    /// <summary>
    /// 总部客户组KA经理-无AR数据
    /// </summary>
    public const string DictKPITypeT8 = "T8";
    /// <summary>
    /// 地区KA代表-有DOH数据
    /// </summary>
    public const string DictKPITypeT9 = "T9";
    /// <summary>
    /// 地区KA主管-有DOH数据
    /// </summary>
    public const string DictKPITypeT10 = "T10";
    /// <summary>
    /// 地区非客户组KA经理-有DOH数据
    /// </summary>
    public const string DictKPITypeT11 = "T11";
    /// <summary>
    /// 地区客户组KA经理-有DOH数据
    /// </summary>
    public const string DictKPITypeT12 = "T12";
    /// <summary>
    /// 地区KA代表-无DOH数据
    /// </summary>
    public const string DictKPITypeT13 = "T13";
    /// <summary>
    /// 地区KA主管-无DOH数据
    /// </summary>
    public const string DictKPITypeT14 = "T14";
    /// <summary>
    /// 地区非客户组KA经理-无DOH数据
    /// </summary>
    public const string DictKPITypeT15 = "T15";
    /// <summary>
    /// 地区客户组KA经理-无DOH数据
    /// </summary>
    public const string DictKPITypeT16 = "T16";
    /// <summary>
    /// Non GE 代表
    /// </summary>
    public const string DictKPITypeT17 = "T17";
    /// <summary>
    /// Non GE 主管
    /// </summary>
    public const string DictKPITypeT18 = "T18";
    /// <summary>
    /// Non GE 地区经理
    /// </summary>
    public const string DictKPITypeT19 = "T19";
    /// <summary>
    /// Non GE 大区经理（CD&CQ）
    /// </summary>
    public const string DictKPITypeT20 = "T20";
    /// <summary>
    /// GE Mix 代表
    /// </summary>
    public const string DictKPITypeT21 = "T21";
    /// <summary>
    /// GE Mix 主管
    /// </summary>
    public const string DictKPITypeT22 = "T22";
    /// <summary>
    /// GE Mix 地区经理
    /// </summary>
    public const string DictKPITypeT23 = "T23";
    /// <summary>
    /// GE Mix 大区经理（exlude CD/CQ/SH/BJ/GZ/SZ)
    /// </summary>
    public const string DictKPITypeT24 = "T24";
    /// <summary>
    /// GE Mix 大区经理（SH/BJ/GZ/SZ）
    /// </summary>
    public const string DictKPITypeT25 = "T25";
    /// <summary>
    /// GE 代表
    /// </summary>
    public const string DictKPITypeT26 = "T26";
    /// <summary>
    /// GE 主管
    /// </summary>
    public const string DictKPITypeT27 = "T27";
    /// <summary>
    /// GE 地区经理
    /// </summary>
    public const string DictKPITypeT28 = "T28";
    /// <summary>
    /// 大区GE经理
    /// </summary>
    public const string DictKPITypeT29 = "T29";
    /// <summary>
    /// 其他
    /// </summary>
    public const string DictKPITypeT30 = "T30";

       

    public static readonly Dictionary<string, string> KPITypeDipslayNameDict = new Dictionary<string, string>() {
      {DictKPITypeRepresentative, DictKPITypeRepresentative},
      {DictKPITypeDirector, DictKPITypeDirector},
      {DictKPITypeAreaManager, DictKPITypeAreaManager},
      {DictKPITypeRNGDirector, DictKPITypeRNGDirector},
      {DictKPITypeHCDirector, DictKPITypeHCDirector},
      {DictKPITypeAreaKAManager, DictKPITypeAreaKAManager},
      {DictKPITypeAreaKADirector, DictKPITypeAreaKADirector},
      {DictKPITypeHCKARepresentative, DictKPITypeHCKARepresentative},
      {DictKPITypeHCKAManager, DictKPITypeHCKAManager},
      {DictKPITypeHCKADirector, DictKPITypeHCKADirector},
      {DictKPITypeHCKAManagerType2, DictKPITypeHCKAManagerType2},
      {DictKPITypeRNGKAManager, DictKPITypeRNGKAManager},      
 {DictKPITypeT1, "总部KA代表-有AR数据"},
 {DictKPITypeT2, "总部KA主管-有AR数据"},
 {DictKPITypeT3, "总部非客户组KA经理-有AR数据"},
 {DictKPITypeT4, "总部客户组KA经理-有AR数据"},
 {DictKPITypeT5, "总部KA代表-无AR数据"},
 {DictKPITypeT6, "总部KA主管-无AR数据"},
 {DictKPITypeT7, "总部非客户组KA经理-无AR数据"},
 {DictKPITypeT8, "总部客户组KA经理-无AR数据"},
 {DictKPITypeT9, "地区KA代表-有DOH数据"},
 {DictKPITypeT10, "地区KA主管-有DOH数据"},
 {DictKPITypeT11, "地区非客户组KA经理-有DOH数据"},
 {DictKPITypeT12, "地区客户组KA经理-有DOH数据"},
 {DictKPITypeT13, "地区KA代表-无DOH数据"},
 {DictKPITypeT14, "地区KA主管-无DOH数据"},
 {DictKPITypeT15, "地区非客户组KA经理-无DOH数据"},
 {DictKPITypeT16, "地区客户组KA经理-无DOH数据"},
 {DictKPITypeT17, "Non GE 代表"},
 {DictKPITypeT18, "Non GE 主管"},
 {DictKPITypeT19, "Non GE 地区经理"},
 {DictKPITypeT20, "Non GE 大区经理（CD&CQ）"},
 {DictKPITypeT21, "GE Mix 代表"},
 {DictKPITypeT22, "GE Mix 主管"},
 {DictKPITypeT23, "GE Mix 地区经理"},
 {DictKPITypeT24, "GE Mix 大区经理（exlude CD/CQ/SH/BJ/GZ/SZ)"},
 {DictKPITypeT25, "GE Mix 大区经理（SH/BJ/GZ/SZ）"},
 {DictKPITypeT26, "GE 代表"},
 {DictKPITypeT27, "GE 主管"},
 {DictKPITypeT28, "GE 地区经理"},
 {DictKPITypeT29, "大区GE经理"},
 {DictKPITypeT30, "非考核人员"},
 {DictKPITypeT31, "PG门店销量占比小于20%的人员"},
 {DictKPITypeT32, "总部客户组KA经理 "},
 {DictKPITypeT33, "总部非客户组KA经理"},
 {DictKPITypeT34, "大区经理"},
    };

    #region 2014
    /// <summary>
    /// 总部KA代表-有AR数据
    /// </summary>
    public const string DictKPIType2014T1 = "T1";
    /// <summary>
    /// 总部KA主管-有AR数据
    /// </summary>
    public const string DictKPIType2014T2 = "T2";
    /// <summary>
    /// 总部非客户组KA经理-有AR数据
    /// </summary>
    public const string DictKPIType2014T3 = "总部非客户组KA经理";
    /// <summary>
    /// 总部客户组KA经理-有AR数据
    /// </summary>
    public const string DictKPIType2014T4 = "总部客户组KA经理";
    /// <summary>
    /// 总部KA代表-无AR数据
    /// </summary>
    public const string DictKPIType2014T5 = "T5";
    /// <summary>
    /// 总部KA主管-无AR数据
    /// </summary>
    public const string DictKPIType2014T6 = "T6";
    /// <summary>
    /// 总部非客户组KA经理-无AR数据
    /// </summary>
    public const string DictKPIType2014T7 = "T7";
    /// <summary>
    /// 总部客户组KA经理-无AR数据
    /// </summary>
    public const string DictKPIType2014T8 = "T8";
    /// <summary>
    /// 地区KA代表-有DOH数据
    /// </summary>
    public const string DictKPIType2014T9 = "T9";
    /// <summary>
    /// 地区KA主管-有DOH数据
    /// </summary>
    public const string DictKPIType2014T10 = "T10";
    /// <summary>
    /// 地区非客户组KA经理-有DOH数据
    /// </summary>
    public const string DictKPIType2014T11 = "T11";
    /// <summary>
    /// 地区客户组KA经理-有DOH数据
    /// </summary>
    public const string DictKPIType2014T12 = "T12";
    /// <summary>
    /// 地区KA代表-无DOH数据
    /// </summary>
    public const string DictKPIType2014T13 = "T13";
    /// <summary>
    /// 地区KA主管-无DOH数据
    /// </summary>
    public const string DictKPIType2014T14 = "T14";
    /// <summary>
    /// 地区非客户组KA经理-无DOH数据
    /// </summary>
    public const string DictKPIType2014T15 = "T15";
    /// <summary>
    /// 地区客户组KA经理-无DOH数据
    /// </summary>
    public const string DictKPIType2014T16 = "T16";
    /// <summary>
    /// Non GE 代表
    /// </summary>
    public const string DictKPIType2014T17 = "T17";
    /// <summary>
    /// Non GE 主管
    /// </summary>
    public const string DictKPIType2014T18 = "T18";
    /// <summary>
    /// Non GE 地区经理
    /// </summary>
    public const string DictKPIType2014T19 = "T19";
    /// <summary>
    /// Non GE 大区经理（CD&CQ）
    /// </summary>
    public const string DictKPIType2014T20 = "T20";
    /// <summary>
    /// GE Mix 代表
    /// </summary>
    public const string DictKPIType2014T21 = "T21";
    /// <summary>
    /// GE Mix 主管
    /// </summary>
    public const string DictKPIType2014T22 = "T22";
    /// <summary>
    /// GE Mix 地区经理
    /// </summary>
    public const string DictKPIType2014T23 = "T23";
    /// <summary>
    /// GE Mix 大区经理（exlude CD/CQ/SH/BJ/GZ/SZ)
    /// </summary>
    public const string DictKPIType2014T24 = "T24";
    /// <summary>
    /// GE Mix 大区经理（SH/BJ/GZ/SZ）
    /// </summary>
    public const string DictKPIType2014T25 = "T25";
    /// <summary>
    /// GE 代表
    /// </summary>
    public const string DictKPIType2014T26 = "T26";
    /// <summary>
    /// GE 主管
    /// </summary>
    public const string DictKPIType2014T27 = "T27";
    /// <summary>
    /// GE 地区经理
    /// </summary>
    public const string DictKPIType2014T28 = "T28";
    /// <summary>
    /// 大区GE经理
    /// </summary>
    public const string DictKPIType2014T29 = "T29";
    /// <summary>
    /// 其他
    /// </summary>
    public const string DictKPIType2014T30 = "T30";
    /// <summary>
    /// PG门店销量占比小于20%的人员
    /// </summary>
    public const string DictKPI2014TypeT31 = "T31";
    /// <summary>
    /// 总部KA客户组经理
    /// </summary>
    public const string DictKPI2014TypeT32 = "T32";
    /// <summary>
    /// 总部KA非客户组经理
    /// </summary>
    public const string DictKPI2014TypeT33 = "T33";
    /// <summary>
    /// 大区经理
    /// </summary>
    public const string DictKPI2014TypeT34 = "大区经理";


    public static readonly Dictionary<string, string> KPIType2014DipslayNameDict = new Dictionary<string, string>() {
      {DictKPITypeRepresentative, DictKPITypeRepresentative},
      {DictKPITypeDirector, DictKPITypeDirector},
      {DictKPITypeAreaManager, DictKPITypeAreaManager},
      {DictKPITypeRNGDirector, DictKPITypeRNGDirector},
      {DictKPITypeHCDirector, DictKPITypeHCDirector},
      {DictKPITypeAreaKAManager, DictKPITypeAreaKAManager},
      {DictKPITypeAreaKADirector, DictKPITypeAreaKADirector},
      {DictKPITypeHCKARepresentative, DictKPITypeHCKARepresentative},
      {DictKPITypeHCKAManager, DictKPITypeHCKAManager},
      {DictKPITypeHCKADirector, DictKPITypeHCKADirector},
      {DictKPITypeHCKAManagerType2, DictKPITypeHCKAManagerType2},
      {DictKPITypeRNGKAManager, DictKPITypeRNGKAManager},
 {DictKPIType2014T1, "总部KA代表-有AR数据"}, //
 {DictKPIType2014T2, "总部KA主管"},      ///
 {DictKPIType2014T3, "总部非客户组KA经理"}, //
 {DictKPIType2014T4, "总部客户组KA经理"},  //
 {DictKPIType2014T5, "总部KA代表-无AR数据"},  //
 {DictKPIType2014T6, "总部KA主管"},   //
 {DictKPIType2014T7, "总部非客户组KA经理"},  //
 {DictKPIType2014T8, "总部客户组KA经理-无AR数据"},  //
 {DictKPIType2014T9, "地区KA代表-有DOH数据"},  //
 {DictKPIType2014T10, "地区KA主管-有DOH数据"},
 {DictKPIType2014T11, "地区非客户组KA经理"},    //地区非客户组KA经理
 {DictKPIType2014T12, "地区客户组KA经理"},       //
 {DictKPIType2014T13, "地区KA代表-无DOH数据"},
 {DictKPIType2014T14, "地区KA主管"},
 {DictKPIType2014T15, "地区非客户组KA经理"},
 {DictKPIType2014T16, "地区客户组KA经理-无DOH数据"},
 {DictKPIType2014T17, "代表"},    //
 {DictKPIType2014T18, "主管"},   //
 {DictKPIType2014T19, "地区经理"},    //
 {DictKPIType2014T20, "大区经理"},     //
 {DictKPIType2014T21, "GE Mix 代表"},
 {DictKPIType2014T22, "主管"},     //
 {DictKPIType2014T23, "地区经理"},    //
 {DictKPIType2014T24, "大区经理"},    //
 {DictKPIType2014T25, "大区经理"},    //
 {DictKPIType2014T26, "代表"},        //
 {DictKPIType2014T27, "主管"},   //
 {DictKPIType2014T28, "地区经理"},  //
 {DictKPIType2014T29, "大区GE经理/地区客户组KA经理"},
 {DictKPIType2014T30, "非考核人员"},   //区域销售总监、渠道拓展总监
 {DictKPI2014TypeT31, "PG门店销量占比小于20%的人员"},  //
 {DictKPI2014TypeT32, "总部KA客户组经理"},  //
 {DictKPI2014TypeT33, "总部KA非客户组经理"},  //
 {DictKPI2014TypeT34, "大区经理"}, 
    };



    #endregion
    public static readonly Dictionary<string,string> KPIType2016Dict = new Dictionary<string,string>() {
      {DictKPITypeRepresentative, DictKPITypeRepresentative},
      {DictKPITypeDirector, DictKPITypeDirector},
      {DictKPITypeAreaManager, DictKPITypeAreaManager},
      {DictKPITypeRNGDirector, DictKPITypeRNGDirector},
      {DictKPITypeHCDirector, DictKPITypeHCDirector},
      {DictKPITypeAreaKAManager, DictKPITypeAreaKAManager},
      {DictKPITypeAreaKADirector, DictKPITypeAreaKADirector},
      {DictKPITypeHCKARepresentative, DictKPITypeHCKARepresentative},
      {DictKPITypeHCKAManager, DictKPITypeHCKAManager},
      {DictKPITypeHCKADirector, DictKPITypeHCKADirector},
      {DictKPITypeHCKAManagerType2, DictKPITypeHCKAManagerType2},
      {DictKPITypeRNGKAManager, DictKPITypeRNGKAManager},
      {DictKPI2016TypeT4Name,DictKPIType2014T4},
      {DictKPI2016TypeT3Name,DictKPIType2014T3},
      {DictKPI2016TypeT34Name, DictKPI2014TypeT34}
    };
    public static readonly Dictionary<string,string> KPIType2016Q3Dict = new Dictionary<string,string>() {
      { DictKPITypeT1,"大区商务副经理"},
      { DictKPITypeT2,"大区商务经理"},
      { DictKPITypeT3,"地区商务副经理"},
      { DictKPITypeT4,"地区商务副主管"},
      { DictKPITypeT5,"地区商务经理"},
      { DictKPITypeT6,"地区商务主管"},
      { DictKPITypeT7,"高级地区商务经理"},
      { DictKPITypeT8,"高级地区商务主管"},
      { DictKPITypeT9,"高级商务代表"},
      { DictKPITypeT10,"高级大区商务经理"},
      { DictKPITypeT11,"商务代表"},
      { DictKPITypeT12,"大区重点客户经理"},
      { DictKPITypeT13,"大区重点客户副经理"},
      { DictKPITypeT14,"区域重点客户经理"},
      { DictKPITypeT15,"地区重点客户经理"},
      { DictKPITypeT16,"地区重点客户主管"},
      { DictKPITypeT17,"重点客户副总监"},
      { DictKPITypeT18,"高级重点客户群经理"},
      { DictKPITypeT19,"重点客户群经理"},
      { DictKPITypeT20,"高级重点客户经理"},
      { DictKPITypeT21,"重点客户经理"},
      { DictKPITypeT22,"重点客户副经理"},
      { DictKPITypeT23,"重点客户主管"}
    };

   

    public static readonly Dictionary<string,string> KPIType2016DipslayNameDict = new Dictionary<string,string>() {
      {DictKPITypeRepresentative, DictKPITypeRepresentative},
      {DictKPITypeDirector, DictKPITypeDirector},
      {DictKPITypeAreaManager, DictKPITypeAreaManager},
      {DictKPITypeRNGDirector, DictKPITypeRNGDirector},
      {DictKPITypeHCDirector, DictKPITypeHCDirector},
      {DictKPITypeAreaKAManager, DictKPITypeAreaKAManager},
      {DictKPITypeAreaKADirector, DictKPITypeAreaKADirector},
      {DictKPITypeHCKARepresentative, DictKPITypeHCKARepresentative},
      {DictKPITypeHCKAManager, DictKPITypeHCKAManager},
      {DictKPITypeHCKADirector, DictKPITypeHCKADirector},
      {DictKPITypeHCKAManagerType2, DictKPITypeHCKAManagerType2},
      {DictKPITypeRNGKAManager, DictKPITypeRNGKAManager},
      {DictKPI2016TypeT4Name,DictKPIType2014T4},
      {DictKPI2016TypeT3Name,DictKPIType2014T3},
      {DictKPI2016TypeT34Name, DictKPI2014TypeT34}
    };

    /// <summary>
    /// 大区经理
    /// </summary>
    public const string DictKPI2016TypeT34Name = "大区经理";
    /// <summary>
    /// 总部客户组KA经理
    /// </summary>
    public const string DictKPI2016TypeT4Name = "总部客户组KA经理";

    /// <summary>
    /// 总部非客户组KA经理
    /// </summary>
    public const string DictKPI2016TypeT3Name = "总部非客户组KA经理";
    

    ///2012KPI年排名组
    //public static string[] GetKPIType(string type) {
    //  string[] KPIType = null;
    //  switch (type) {
    //    case "1":
    //      KPIType = new string[] { DictKPITypeRNGDirector };
    //      break;
    //    case "2":
    //      KPIType = new string[] { DictKPITypeAreaKAManager, DictKPITypeAreaManager };
    //      break;
    //    case "3":
    //      KPIType = new string[] { DictKPITypeRepresentative, DictKPITypeHCKARepresentative };
    //      break;
    //    case "4":
    //      KPIType = new string[] { DictKPITypeDirector, DictKPITypeHCKADirector };
    //      break;
    //    case "5":
    //      KPIType = new string[] { DictKPITypeHCKARepresentative, DictKPITypeHCKAManager, DictKPITypeHCKADirector, DictKPITypeHCKAManagerType2 };
    //      break;
    //    default:
    //      KPIType = new string[] { DictKPITypeRNGDirector };
    //      break;
    //  }
    //  return KPIType;
    //}

    /// <summary>
    /// 2013KPI 排名
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string[] GetKPIType(string type) {
      string[] KPIType = null;
      switch (type) {
        case "1"://大区经理组
          KPIType = new string[] { DictKPITypeT12, DictKPITypeT16, DictKPITypeT20, DictKPITypeT24, DictKPITypeT25, DictKPITypeT29 };
          break;
        case "2"://地区经理组
          KPIType = new string[] { DictKPITypeT11, DictKPITypeT15, DictKPITypeT19, DictKPITypeT23, DictKPITypeT28 };
          break;
        case "4"://主管组
          KPIType = new string[] { DictKPITypeT10, DictKPITypeT14, DictKPITypeT18, DictKPITypeT22, DictKPITypeT27 };
          break;
        case "3"://代表组
          KPIType = new string[] { DictKPITypeT9, DictKPITypeT13, DictKPITypeT17, DictKPITypeT21, DictKPITypeT26 };
          break;
        case "5"://总部KA
          KPIType = new string[] { DictKPITypeT1, DictKPITypeT2, DictKPITypeT3, DictKPITypeT4, DictKPITypeT5, DictKPITypeT6, DictKPITypeT7, DictKPITypeT8 };
          break;
        default:
          KPIType = new string[] { DictKPITypeRNGDirector };
          break;
      }
      return KPIType;
    }



    public static string GetTopListTypeName(string type) {
      string KPIType = null;
      switch (type) {
        case "1":
          KPIType = "小区负责人排名";
          break;
        case "2":
          KPIType = "地区经理排名";
          break;
        case "3":
          KPIType = "代表排名";
          break;
        case "4":
          KPIType = "主管排名";
          break;
        case "5":
          KPIType = "总部KA排名";
          break;
      }
      return KPIType;
    }

    /// <summary>
    /// PGM第三方入离职管理
    /// </summary>
    public const string DictTypePGMEmployee = "pgmEmployeeType";

    public const string DictPGMEmployeeTypeEntrance = "入职申请";
    public const string DictPGMEmployeeTypeDimission = "离职申请";
    public const string DictPGMEmployeeTypePositionSalary = "职位薪资变更";
    /// <summary>
    /// PGM变更子类型
    /// </summary>
    public const string DictTypePGMEmployeeSubType = "pgmEmployeeSubType";
    public const string DictPGMEmployeeSubTypeAddEntrance = "新增";
    public const string DictPGMEmployeeSubTypeReplaceEntrance = "替换";
    public const string DictPGMEmployeeSubTypeResign = "辞职";
    public const string DictPGMEmployeeSubTypeFire = "解雇";
    public const string DictPGMEmployeeSubTypeDismiss = "遣散";
    public const string DictPGMEmployeeSubTypeBecomeFullMember = "转正";
    public const string DictPGMEmployeeSubTypePosition = "职位变更";
    public const string DictPGMEmployeeSubTypeSalary = "薪资变更";
    /// <summary>
    /// 审核状态
    /// </summary>
    public const string DictTypePGMReviewedState = "pgmReviewedState";
    /// <summary>
    /// 未审核
    /// </summary>
    public const string DictPGMReviewedStateTypeNotReviewed = "未审核";
    //  public const string DictPGMReviewedStateTypeUnderReviewed = "审核中";
    /// <summary>
    /// 已审核
    /// </summary>
    public const string DictPGMReviewedStateTypeHadReviewed = "已审核";
    /// <summary>
    /// 拒绝审核
    /// </summary>
    public const string DictPGMReviewedStateTypeRefuseReviewed = "拒绝审核";
    /// <summary>
    /// RSM审核通过
    /// </summary>
    public const string DictPGMReviewedStateTypeRSMReviewed = "RSM审核通过";
    /// <summary>
    /// RPGM审核拒绝
    /// </summary>
    public const string DictPGMReviewedStateTypeRPGMRefuse = "RPGM审核拒绝";

    /// <summary>
    /// 审核行为 pgmReview
    /// </summary>
    public const string DictTypePGMReviewState = "pgmReview";
    /// <summary>
    /// 待审批
    /// </summary>
    public const string DictPGMReviewTypeNotReview = "待审批";
    /// <summary>
    /// 批准
    /// </summary>
    public const string DictPGMReviewTypeApprove = "批准";
    /// <summary>
    /// 拒绝
    /// </summary>
    public const string DictPGMReviewTypeRefuse = "拒绝";

    /// <summary>
    /// PGM PG职位
    /// </summary>
    public const string DictTypePGMDuty = "pgmDuty";
    /// <summary>
    /// PG
    /// </summary>
    public const string DictPGMDutyTypePG = "PG";
    /// <summary>
    /// PGS
    /// </summary>
    public const string DictPGMDutyTypePGS = "PGS";
    /// <summary>
    /// 助理
    /// </summary>
    public const string DictPGMDutyTypeAssistant = "助理";
    /// <summary>
    /// PG 职位类型
    /// </summary>
    public const string DictTypePGMDutyType = "pgmDutyType";
    /// <summary>
    /// PG
    /// </summary>
    public const string DictPGMDutySubTypePG = "PG";
    /// <summary>
    /// DPG
    /// </summary>
    public const string DictPGMDutySubTypeDPG = "DPG";
    /// <summary>
    /// PGS
    /// </summary>
    public const string DictPGMDutySubTypePGS = "PGS";
    /// <summary>
    /// 高级PGS
    /// </summary>
    public const string DictPGMDutySubTypeAdvancedPGS = "高级PGS";
    /// <summary>
    /// 助理PGS
    /// </summary>
    public const string DictPGMDutySubTypeAssistantPGS = "助理PGS";
    /// <summary>
    ///  助理
    /// </summary>
    public const string DictPGMDutySubTypeAssistant = "助理";
    //public const string DictPGMDutySubTypeHeadquarterAssistant = "总部助理";
    //public const string DictPGMDutySubTypeAreaAssistant = "大区助理";
    //public const string DictPGMDutySubTypeDistrictAssistant = "小区助理";

    /// <summary>
    /// PGM 部门
    /// </summary>
    public const string DictTypePGMDept = "pgmDeptIDType";
    /// <summary>
    /// PND Trade
    /// </summary>
    public const string DictPGMDeptTypePNDTrade = "PND Trade";

    /// <summary>
    /// PGM 人员状态
    /// </summary>
    public const string DictTypePGMState = "pgmState";
    public const string DictPGMStateTypeEmployment = "在职";
    public const string DictPGMStateTypeDimission = "离职";

    /// <summary>
    /// PGM 属性
    /// </summary>
    public const string DictTypePGMAttribute = "pgmAttribute";
    /// <summary>
    /// Base
    /// </summary>
    public const string DictPGMAttributeTypeBase = "Base";
    /// <summary>
    /// GE
    /// </summary>
    public const string DictPGMAttributeTypeGE = "GE";
    /// <summary>
    /// GE2
    /// </summary>
    public const string DictPGMAttributeTypeGE2 = "GE2";

    /// <summary>
    ///MDM　BaseEmployee表中memo1对应 PGM 属性 
    /// </summary>
    public const string DictTypeMDMAttribute = "MDMAttribute";
    public const string DictMDMAttributeType2010HC = "2010HC";
    public const string DictMDMAttributeType2011HC = "2011HC";
    public const string DictMDMAttributeTypeDBBS = "DBBS";
    public const string DictMDMAttributeTypeBase = "Base";
    public const string DictMDMAttributeTypeGE = "GE";

    /// <summary>
    /// 学历
    /// </summary>
    public const string DictTypeDegree = "degree";
    /// <summary>
    /// 户口性质
    /// </summary>
    public const string DictTypeHouseHold = "household";
    /// <summary>
    /// 社保状态
    /// </summary>
    public const string DictTypeInsuranceStatus = "insuranceStatus";
    /// <summary>
    /// HC性质
    /// </summary>
    public const string DictTypePersonHC = "personHC";
    /// <summary>
    /// 社保方案
    /// </summary>
    public const string DictTypeInsuranceType = "insuranceType";

    /// <summary>
    /// PGM 页面编辑类型
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    public const string DictTypePGMEditType = "editType";
    /// <summary>
    /// 添加新申请add 
    ///     
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    public const string DictPGMEditTypeAdd = "add";
    /// <summary>
    ///     查看详细信息 Detail
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    public const string DictPGMEditTypeDetail = "detail";
    /// <summary>
    ///     修改页面内容  edit
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    public const string DictPGMEditTypeEdit = "edit";
    /// <summary>
    ///     审批页面 review
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    public const string DictPGMEditTypeReview = "review";

    /// <summary>
    /// KA
    /// </summary>
    public const int KAALL = 1;

    /// <summary>
    /// 全国
    /// </summary>
    public const int OrgChinaID = 1;

    /// <summary>
    /// 全国
    /// </summary>
    public const int OrgChina = 1;
    /// <summary>
    /// 大区
    /// </summary>
    public const int OrgRNG = 2;
    /// <summary>
    /// 小区
    /// </summary>
    public const int OrgArea = 3;
    /// <summary>
    /// 中心城市
    /// </summary>
    public const int OrgCenterCity = 4;
    /// <summary>
    /// 雅培城市
    /// </summary>
    public const int OrgAbtCity = 5;

    public const string OrgRNGAll = "全国";
    public const string OrgRNGEast = "华东区";
    public const string OrgRNGSouth = "华南区";
    public const string OrgRNGWest = "华西区";
    public const string OrgRNGNorth = "华北区";
    public const string OrgRNGCenter = "华中区";

    // Jason电话确认
    public static Dictionary<string, HashSet<string>> GetChannelCombination() {
      var combination = new Dictionary<string, HashSet<string>>();
      combination.Add("现代渠道", new HashSet<string>(new List<string> { 
        "大卖场",
        "中型超市",
        "小型超市",
        "C&C",
      }));

      combination.Add("母婴渠道", new HashSet<string>(new List<string> { 
        "钻石店(DBBS)",
        "大象店",
        "院内三产店",
        "其他宝宝店",
        "医院周边非钻石店"
      }));

      combination.Add("二级批发市场", new HashSet<string>(new List<string> { 
        "二级批发商",
      }));
      combination.Add("B2C", new HashSet<string>(new List<string> { 
        "B2C",
      }));
      return combination;
    }

    public const int DictSaleExceptionType2MoreThan = 1;
    public const int DictSaleExceptionType5MoreThan = 2;
    public const int DictSaleExceptionTypeIsEmpty = 3;
    public const int DictSaleExceptionTypeFallingBehind = 4;
    public static Dictionary<int, string> SaleExceptionType {
      get {
        return new Dictionary<int, string>(){
          {DictSaleExceptionType2MoreThan, "日PGO高于前三个月日均2箱"},
          {DictSaleExceptionType5MoreThan, "日PGO高于前三个月日均5箱"},
          {DictSaleExceptionTypeIsEmpty, "连续3天无销售PG门店"},
          {DictSaleExceptionTypeFallingBehind, "低于时间进度10%的门店进度"},
        };
      }
    }

    public const int DictPGMTargetTypeRngArea = 1;
    public const int DictPGMTargetTypePGS = 2;
    public const int DictPGMTargetTypePG = 3;
    public static Dictionary<int, string> PGMTargetType {
      get {
        return new Dictionary<int, string>(){
          {DictPGMTargetTypeRngArea, "大区/小区指标"},
          {DictPGMTargetTypePGS, "PGS指标"},
          {DictPGMTargetTypePG, "PG指标"},
        };
      }
    }

    public const int DictBizTargetTypePGO = 1;
    public const int DictBizTargetTypeNU = 2;
    public const int DictBizTargetTypeCRM = 3;
    public static Dictionary<int, string> BizTargetType
    {
        get
        {
            return new Dictionary<int, string>(){
          {DictBizTargetTypePGO, "PGO指标（小罐）"},
          {DictBizTargetTypeNU, "DA指标（人）"},
          {DictBizTargetTypeCRM, "CRM指标（人）"},
        };
        }
    }

    public const int DictPGMTargetLevelCodeRNG = 1;
    public const int DictPGMTargetLevelCodeArea = 2;
    public const int DictPGMTargetLevelCodePGS = 3;
    public const int DictPGMTargetLevelCodePG = 4;

    #region DictTypeKPIMatrix
    /// <summary>
    /// KPI Matrix
    /// </summary>
    public const string DictTypeKPIMatrix = "matrixType";
    /// <summary>
    /// "金装 sell In KA Purchasing"
    /// </summary>
    public const string DictKPIMatrixTypeM1 = "M1";
    /// <summary>
    /// "小安素PediaSure Sell In"
    /// </summary>
    public const string DictKPIMatrixTypeM2 = "M2";
    /// <summary>
    /// 金装 sell out KA Offtake
    /// </summary>
    public const string DictKPIMatrixTypeM3 = "M3";
    /// <summary>
    /// S1 Sell In 达成
    /// </summary>
    public const string DictKPIMatrixTypeM4 = "M4";
    /// <summary>
    /// DOH
    /// </summary>  
    public const string DictKPIMatrixTypeM5 = "M5";
    /// <summary>
    /// "带教Coaching"
    /// </summary>
    public const string DictKPIMatrixTypeM6 = "M6";
    public const string DictKPIMatrixTypeM7_SKU = "M7-必备SKU分销";
    public const string DictKPIMatrixTypeM7_OOS = "M7-门店缺货控制";
    public const string DictKPIMatrixTypeM7_Price = "M7-价格管理";
    /// <summary>
    /// "门店日常表现ISP"	
    /// </summary>
    public const string DictKPIMatrixTypeM7 = "M7";
    /// <summary>
    /// "季度工作重点Quarterly Priority"
    /// </summary>
    public const string DictKPIMatrixTypeM8 = "M8";
    /// <summary>
    /// "回款天数AR"
    /// </summary>
    public const string DictKPIMatrixTypeM9 = "M9";
    /// <summary>
    /// "GE Score Card"
    /// </summary>
    public const string DictKPIMatrixTypeM10 = "M10";
    /// <summary>
    /// "GE Monthly Review"
    /// </summary>
    public const string DictKPIMatrixTypeM11 = "M11";
    /// <summary>
    /// "DBBS NU"
    /// </summary>
    public const string DictKPIMatrixTypeM12 = "M12";
    /// <summary>
    /// "PG NU"
    /// </summary>
    public const string DictKPIMatrixTypeM13 = "M13";
    /// <summary>
    /// "Sell in (Eleva+TC)"
    /// </summary>
    public const string DictKPIMatrixTypeM14 = "M14";
    /// <summary>
    /// "PGO"
    /// </summary>
    public const string DictKPIMatrixTypeM15 = "M15";
    /// <summary>
    /// "New User"
    /// </summary>
    public const string DictKPIMatrixTypeM16 = "M16";

    /// <summary>
    /// "MarktShare"
    /// </summary>
    public const string DictKPIMatrixTypeM17 = "M17";

    /// <summary>
    /// "Memo2"
    /// </summary>
    public const string DictKPIMatrixTypeM18 = "M18";

    public const string DictKPIMatrixTypeM19 = "M19";

    /// <summary>
    /// "GE项目奖励"
    /// </summary>
    public const string DictKPIMatrixTypeMGE = "M-GE";
    /// <summary>
    /// "直营项目奖励"
    /// </summary>
    public const string DictKPIMatrixTypeMDA = "M-DA";
    /// <summary>
    /// 最终奖励未加上系数版本
    /// </summary>
    public const string DictKPIMatrixTypeTotalRawBonus = "M-TotalRawBonus";
    /// <summary>
    /// 最终奖励
    /// </summary>
    public const string DictKPIMatrixTypeTotalBonus = "M-TotalBonus";


    #region 2014-05-05 地区/个人指标、达成

    /// <summary>
    /// PGO by city
    /// </summary>
    public const string DictKPIMatrixTypeM15_City = "M15-C";
    /// <summary>
    /// NU by city
    /// </summary>
    public const string DictKPIMatrixTypeM16_City = "M16-C";

    /// <summary>
    /// PGO by person Raw
    /// </summary>
    public const string DictKPIMatrixTypeM15_Raw = "M15-R";
    /// <summary>
    /// NU  by Person Raw
    /// </summary>
    public const string DictKPIMatrixTypeM16_Raw = "M16-R";

    #endregion 

    /// <summary>
    /// Sell in by city
    /// </summary>
    public const string DictKPIMatrixTypeM1_City = "M1-C";
    /// <summary>
    /// Sell out by city
    /// </summary>
    public const string DictKPIMatrixTypeM3_City = "M3-C";
    /// <summary>
    /// Sell in by person Raw
    /// </summary>
    public const string DictKPIMatrixTypeM1_Raw = "M1-R";
    /// <summary>
    /// Sell out by Person Raw
    /// </summary>
    public const string DictKPIMatrixTypeM3_Raw = "M3-R";
    /// <summary>
    /// ISP 合计
    /// </summary>
    public const string DictKPIMatrixTypeM7_Total = "M7-合计";
    /// <summary>
    /// Sell in by KA
    /// </summary>
    public const string DictKPIMatrixTypeM1_KA = "M1-KA";
    /// <summary>
    /// Sell out by KA
    /// </summary>
    public const string DictKPIMatrixTypeM3_KA = "M3-KA";

    /// <summary>
    /// PGO By City
    /// </summary>
    public const string DictKPIMatrixTypeMPGO_City = "MPGO-City";
    /// <summary>
    /// 收卡数
    /// </summary>
    public const string DictKPIMatrixTypeMPG_Customers = "MPG-Customers";
    /// <summary>
    /// 热线新客数
    /// </summary>
    public const string DictKPIMatrixTypeMPG_NU = "MPG-NU";
    /// <summary>
    /// DBBS 1段销量
    /// </summary>
    public const string DictKPIMatrixTypeMPGO_DBBS_S1 = "MPGO-DBBS-S1";
    /// <summary>
    /// DBBS 1段新客数
    /// </summary>
    public const string DictKPIMatrixTypeMPG_Customers_DBBS_S1 = "MPG-Customers-DBBS-S1";
    /// <summary>
    /// "门店拜访覆盖 Store Call Cover"
    /// </summary>
    public const string DictKPIMatrixTypeGE_1 = "GE-1";
    /// <summary>
    /// "雅培产品市场份额占比 Abbott Market Share"
    /// </summary>
    public const string DictKPIMatrixTypeGE_2 = "GE-2";
    /// <summary>
    /// "雅培产品市场份额占比 Abbott Market Share（全年基数）"
    /// </summary>
    public const string DictKPIMatrixTypeGE_2Base = "GE-2Base";
    /// <summary>
    /// "S1 NU(M-1)"
    /// </summary>
    public const string DictKPIMatrixTypeGE_3 = "GE-3";
    /// <summary>
    /// "S1 PGO"
    /// </summary>
    public const string DictKPIMatrixTypeGE_4 = "GE-4";
    /// <summary>
    /// "IMS销量达标 IMS achievement""
    /// </summary>
    public const string DictKPIMatrixTypeGE_5 = "GE-5";
    /// <summary>
    /// "Sell-out达标 Sell-Out achievement"
    /// </summary>
    public const string DictKPIMatrixTypeGE_6 = "GE-6";
    /// <summary>
    /// "PGO销量达标 PGO achievement"
    /// </summary>
    public const string DictKPIMatrixTypeGE_7 = "GE-7";
    /// <summary>
    /// "PG NU达标(M-1) PG NU achievement"
    /// </summary>
    public const string DictKPIMatrixTypeGE_8 = "GE-8";
    /// <summary>
    /// "DOH"
    /// </summary>
    public const string DictKPIMatrixTypeGE_9 = "GE-9";




    public static readonly Dictionary<string, string> KPIMatrixTypeExportNameDict = new Dictionary<string, string>() { 
            {BaseDictType.DictKPIMatrixTypeM1, "Sell In" },
            {BaseDictType.DictKPIMatrixTypeM1_City, "Sell In" },
            {BaseDictType.DictKPIMatrixTypeM3, "Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM3_City, "Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM2, "小安素 Sell In" },
            {BaseDictType.DictKPIMatrixTypeM4, "S1 Sell In" },
            {BaseDictType.DictKPIMatrixTypeM7_SKU, "ISP得分率-分销" },
            {BaseDictType.DictKPIMatrixTypeM7_OOS, "ISP得分率-缺货控制" },
            {BaseDictType.DictKPIMatrixTypeM7_Price, "ISP得分率-价格" },
            {BaseDictType.DictKPIMatrixTypeM7_Total, "ISP得分率-合计" },
            {BaseDictType.DictKPIMatrixTypeMPGO_City, "PG Offtake"},
            {BaseDictType.DictKPIMatrixTypeMPG_Customers, "新客收卡数"},
            {BaseDictType.DictKPIMatrixTypeMPG_NU, "热线新客数"},
            {BaseDictType.DictKPIMatrixTypeMPGO_DBBS_S1,"DBBS一段销量"},
            {BaseDictType.DictKPIMatrixTypeMPG_Customers_DBBS_S1,"DBBS一段新客"},

            {BaseDictType.DictKPIMatrixTypeGE_1, "Store Call Cover" },
            {BaseDictType.DictKPIMatrixTypeGE_2, "Abbott Market Share" },
            {BaseDictType.DictKPIMatrixTypeGE_3, "S1 NU(M-1)" },
            {BaseDictType.DictKPIMatrixTypeGE_4, "S1 PGO" },
            {BaseDictType.DictKPIMatrixTypeGE_5, "IMS achievement" },
            {BaseDictType.DictKPIMatrixTypeGE_6, "Sell-Out achievement" },
            {BaseDictType.DictKPIMatrixTypeGE_7, "PGO achievement" },
            {BaseDictType.DictKPIMatrixTypeGE_8, "PG NU achievement" },
            {BaseDictType.DictKPIMatrixTypeGE_9, "DOH" },

            {BaseDictType.DictKPIMatrixTypeM1_Raw, "Sell In" },
            {BaseDictType.DictKPIMatrixTypeM3_Raw, "Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM1_KA, "Sell In" },
            {BaseDictType.DictKPIMatrixTypeM3_KA, "Sell Out" },

            {BaseDictType.DictKPIMatrixTypeM15_City, "PGO" },
            {BaseDictType.DictKPIMatrixTypeM16_City, "NU" },
            {BaseDictType.DictKPIMatrixTypeM15_Raw, "PGO" },
            {BaseDictType.DictKPIMatrixTypeM16_Raw, "NU" },

            {BaseDictType.DictKPIMatrixTypeM14, "Sell In(Eleva+TC)" },
            {BaseDictType.DictKPIMatrixTypeM17, "MarktShare" },
            {BaseDictType.DictKPIMatrixTypeM18, "Memo2" },


          };

    public static readonly Dictionary<string, string> KPIMatrixTypeExportTypeNameDict = new Dictionary<string, string>() { 
            {BaseDictType.DictKPIMatrixTypeM1, "M1:Sell In" },
            {BaseDictType.DictKPIMatrixTypeM1_City, "M1:Sell In" },
            {BaseDictType.DictKPIMatrixTypeM3, "M3:Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM3_City, "M3:Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM2, "M2:小安素" },
            {BaseDictType.DictKPIMatrixTypeM4, "M4:Stage1" },
            {BaseDictType.DictKPIMatrixTypeM7_SKU, "M7:店内表现" },
            {BaseDictType.DictKPIMatrixTypeM7_OOS, "M7:店内表现" },
            {BaseDictType.DictKPIMatrixTypeM7_Price, "M7:店内表现" },
            {BaseDictType.DictKPIMatrixTypeM7_Total, "M7:店内表现" },
            {BaseDictType.DictKPIMatrixTypeMPGO_City, "PGO"},
            {BaseDictType.DictKPIMatrixTypeMPG_Customers, "PG-NU"},
            {BaseDictType.DictKPIMatrixTypeMPG_NU, "PG-NU"},
            {BaseDictType.DictKPIMatrixTypeMPGO_DBBS_S1,"PGO"},
            {BaseDictType.DictKPIMatrixTypeMPG_Customers_DBBS_S1,"PG-NU"},

            {BaseDictType.DictKPIMatrixTypeGE_1, "GE_1:Store Call Cover" },
            {BaseDictType.DictKPIMatrixTypeGE_2, "GE_2:Abbott Market Share" },
            {BaseDictType.DictKPIMatrixTypeGE_3, "GE_3:S1 NU(M-1)" },
            {BaseDictType.DictKPIMatrixTypeGE_4, "GE_4:S1 PGO" },
            {BaseDictType.DictKPIMatrixTypeGE_5, "GE_5:IMS achievement" },
            {BaseDictType.DictKPIMatrixTypeGE_6, "GE_6:Sell-Out achievement" },
            {BaseDictType.DictKPIMatrixTypeGE_7, "GE_7:PGO achievement" },
            {BaseDictType.DictKPIMatrixTypeGE_8, "GE_8:PG NU achievement" },
            {BaseDictType.DictKPIMatrixTypeGE_9, "GE_9:DOH" },

            {BaseDictType.DictKPIMatrixTypeM1_KA, "Sell In" },
            {BaseDictType.DictKPIMatrixTypeM3_KA, "Sell Out" },
          };

    public static readonly Dictionary<string, string> KPIMatrixTypeDipslayNameDict = new Dictionary<string, string>() { 
            {BaseDictType.DictKPIMatrixTypeM1, "M1: 金装 Sell In/KA Purchasing" },
            {BaseDictType.DictKPIMatrixTypeM2, "M2: 小安素 Sell In" },
            {BaseDictType.DictKPIMatrixTypeM3, "M3: 金装 Sell Out/KA Offtake" },
            {BaseDictType.DictKPIMatrixTypeM4, "M4: S1 Sell In达成" },
            {BaseDictType.DictKPIMatrixTypeM5, "M5: DOH" },
            {BaseDictType.DictKPIMatrixTypeM6, "M6: 带教" },
            {BaseDictType.DictKPIMatrixTypeM7, "M7: 门店日常表现ISP" },
            {BaseDictType.DictKPIMatrixTypeM8, "M8: 季度工作重点" },
            {BaseDictType.DictKPIMatrixTypeM9, "M9: 回款天数" },
            {BaseDictType.DictKPIMatrixTypeM10, "M10: GE Score Card" },
            {BaseDictType.DictKPIMatrixTypeM11, "M11: GE Monthly Review" },
            {BaseDictType.DictKPIMatrixTypeM13, "M13: 新客 PG NU" },
            {BaseDictType.DictKPIMatrixTypeM12, "M12: 新客 DBBS NU" },
            {BaseDictType.DictKPIMatrixTypeMGE, "GE奖金" },
            {BaseDictType.DictKPIMatrixTypeMDA, "Dynasty奖金" },
            {BaseDictType.DictKPIMatrixTypeTotalRawBonus, "合计" },
            {BaseDictType.DictKPIMatrixTypeTotalBonus, "总奖金" },
          };

    public static readonly Dictionary<string, string> AreaKPIMatrixTypeDipslayNameDict = new Dictionary<string, string>() { 
            {BaseDictType.DictKPIMatrixTypeM1, "M1: 金装 Sell In" },
            {BaseDictType.DictKPIMatrixTypeM2, "M2: 小安素 Sell In" },
            {BaseDictType.DictKPIMatrixTypeM3, "M3: 金装 Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM4, "M4: S1 Sell In达成" },
            {BaseDictType.DictKPIMatrixTypeM5, "M5: DOH" },
            {BaseDictType.DictKPIMatrixTypeM6, "M6: 带教" },
            {BaseDictType.DictKPIMatrixTypeM7, "M7: 门店日常表现ISP" },
            {BaseDictType.DictKPIMatrixTypeM8, "M8: 季度工作重点" },
            {BaseDictType.DictKPIMatrixTypeM9, "M9: 回款天数" },
            {BaseDictType.DictKPIMatrixTypeM10, "M10: GE Score Card" },
            {BaseDictType.DictKPIMatrixTypeM11, "M11: GE Monthly Review" },
            {BaseDictType.DictKPIMatrixTypeM13, "M13: 新客 PG NU" },
            {BaseDictType.DictKPIMatrixTypeM12, "M12: 新客 DBBS NU" },
            {BaseDictType.DictKPIMatrixTypeMGE, "GE奖金" },
            {BaseDictType.DictKPIMatrixTypeMDA, "Dynasty奖金" },
            {BaseDictType.DictKPIMatrixTypeTotalRawBonus, "合计" },
            {BaseDictType.DictKPIMatrixTypeTotalBonus, "总奖金" },
          };

    public static readonly Dictionary<string, string> KaKPIMatrixTypeDipslayNameDict = new Dictionary<string, string>() { 
            {BaseDictType.DictKPIMatrixTypeM1, "M1: KA Purchasing" },
            {BaseDictType.DictKPIMatrixTypeM2, "M2: 小安素 Sell In" },
            {BaseDictType.DictKPIMatrixTypeM3, "M3: KA Offtake" },
            {BaseDictType.DictKPIMatrixTypeM4, "M4: S1 Sell In达成" },
            {BaseDictType.DictKPIMatrixTypeM5, "M5: DOH" },
            {BaseDictType.DictKPIMatrixTypeM6, "M6: 带教" },
            {BaseDictType.DictKPIMatrixTypeM7, "M7: 门店日常表现ISP" },
            {BaseDictType.DictKPIMatrixTypeM8, "M8: 季度工作重点" },
            {BaseDictType.DictKPIMatrixTypeM9, "M9: 回款天数" },
            {BaseDictType.DictKPIMatrixTypeM10, "M10: GE Score Card" },
            {BaseDictType.DictKPIMatrixTypeM11, "M11: GE Monthly Review" },
            {BaseDictType.DictKPIMatrixTypeM13, "M13: 新客 PG NU" },
            {BaseDictType.DictKPIMatrixTypeM12, "M12: 新客 DBBS NU" },
            {BaseDictType.DictKPIMatrixTypeMGE, "GE奖金" },
            {BaseDictType.DictKPIMatrixTypeMDA, "Dynasty奖金" },
            {BaseDictType.DictKPIMatrixTypeTotalBonus, "总奖金" },
          };

      //2014 NEW MATRIX FOR DISPLAY
    public static readonly Dictionary<string, string> KPIMatrixTypeDipslayNameDict2014 = new Dictionary<string, string>() { 
            {BaseDictType.DictKPIMatrixTypeM1, "Sell In" },
            {BaseDictType.DictKPIMatrixTypeM14, "Sell In(Eleva+TC)" },
            {BaseDictType.DictKPIMatrixTypeM3, "Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM4, "Sell In(S1)" },
            {BaseDictType.DictKPIMatrixTypeM5, "DOH" },
            {BaseDictType.DictKPIMatrixTypeM16, "New User" },
            {BaseDictType.DictKPIMatrixTypeM15, "PGO" },
            {BaseDictType.DictKPIMatrixTypeM17,"Market Share"},
            {BaseDictType.DictKPIMatrixTypeM18,"Memo2"},
            {BaseDictType.DictKPIMatrixTypeTotalRawBonus, "合计" },
            {BaseDictType.DictKPIMatrixTypeTotalBonus, "总奖金" },
          };

    public static readonly Dictionary<string, string> KPIMatrixTypeDipslayNameDict2015 = new Dictionary<string, string>() { 
            {BaseDictType.DictKPIMatrixType2015M1, "Sell In(PND)" },
            
            {BaseDictType.DictKPIMatrixType2015M3, "Sell Out(PND)" },
            {BaseDictType.DictKPIMatrixType2015M2, "Sell In(SSP)" },
            {BaseDictType.DictKPIMatrixType2015M4, "Sell Out(SSP)" },
            {BaseDictType.DictKPIMatrixType2015M5, "Market Share(Retail)" },
            {BaseDictType.DictKPIMatrixType2015M6, "Market Share(Retail + BBS)" },
            {BaseDictType.DictKPIMatrixType2015M7, "Market Share(by account)" },
            {BaseDictType.DictKPIMatrixType2015M8,"NU"},
            {BaseDictType.DictKPIMatrixType2015M9,"Sell In(AND)"},
            {BaseDictType.DictKPIMatrixType2015M10,"Sell Out(AND)"},
            {BaseDictType.DictKPIMatrixType2015M11,"Quarterly Priority(促销执行)"},
            {BaseDictType.DictKPIMatrixType2015M12,"Quarterly Priority(日常执行)"},
            {BaseDictType.DictKPIMatrixTypeTotalRawBonus, "合计" },
            {BaseDictType.DictKPIMatrixTypeTotalBonus, "总奖金" },
          };

    public static readonly Dictionary<string,string> KPIMatrixTypeDipslayNameDict2016 = new Dictionary<string,string>() {
            {BaseDictType.DictKPIMatrixTypeM1, "M1 PND Sell In" },
            {BaseDictType.DictKPIMatrixTypeM2, "M2 PND Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM3, "M3 PND Offtake" },
            {BaseDictType.DictKPIMatrixTypeM4, "M4 Eleva Sell In" },
            {BaseDictType.DictKPIMatrixTypeM5, "M5 Eleva Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM6, "M6 AND Sell In" },
            {BaseDictType.DictKPIMatrixTypeM7, "M7 AND Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM8,"M8 Euro QT Sell Out"},
            {BaseDictType.DictKPIMatrixTypeM9,"M9 Euro QT Offtake"},
            {BaseDictType.DictKPIMatrixTypeM10,"M10 Market Share (Retail)"},
            {BaseDictType.DictKPIMatrixTypeM11,"M11 Market Share (Baby)"},
            {BaseDictType.DictKPIMatrixTypeM12,"M12 Market Share (Baby K&A Cities)"},
            {BaseDictType.DictKPIMatrixTypeM13,"M13 Market Share (Baby K/A/B Chain)"},
            {BaseDictType.DictKPIMatrixTypeM14,"M14 Market Share (Baby Chain)"},
            {BaseDictType.DictKPIMatrixTypeM15,"M15 532 Share"},
            {BaseDictType.DictKPIMatrixTypeM16,"M16 Customer Share"},
            {BaseDictType.DictKPIMatrixTypeM17,"M17 New User"},
          };

    public static readonly Dictionary<string,string> KPIMatrixTypeDipslayNameDict2016Q3 = new Dictionary<string,string>() {
            {BaseDictType.DictKPIMatrixTypeM1, "M1 PND Sell In" },
            {BaseDictType.DictKPIMatrixTypeM2, "M2 PND Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM3, "M3 PND PGO" },
            {BaseDictType.DictKPIMatrixTypeM4, "M4 Eleva Sell In" },
            {BaseDictType.DictKPIMatrixTypeM5, "M5 Eleva Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM6, "M6 Eleva PGO" },
            {BaseDictType.DictKPIMatrixTypeM7, "M7 Euro QT Sell Out" },
            {BaseDictType.DictKPIMatrixTypeM8,"M8 AND Sell in"},
            {BaseDictType.DictKPIMatrixTypeM9,"M9 AND Top 600 PGO"},
            {BaseDictType.DictKPIMatrixTypeM10,"M10 NU"},
            
          };

    #endregion

    #region 产品7维度
    // 产品线
    public const string DictTypeProdSegment = "prodSegment";
    public const string DictProdSegmentTypeSuperSuperPremiun = "超高端";
    public const string DictProdSegmentTypeSuperPremiun = "高端";
    public const string DictProdSegmentTypeSuperAffordablePremiun = "中端";
    public const string DictProdSegmentTypeELEVA = "ELEVA";

    // 产品大类
    public const string DictTypeProdBigCategory = "prodBigCategory";
    public const string DictProdBigCategoryTypeOrganic = "有机配方";
    public const string DictProdBigCategoryTypeComfort = "亲护";
    public const string DictProdBigCategoryTypePrenatal = "产前";
    public const string DictProdBigCategoryTypePremium = "高端";
    public const string DictProdBigCategoryTypeCNS = "全营养配方";
    public const string DictProdBigCategoryTypePreterm = "早产儿";
    public const string DictProdBigCategoryTypeTolerance = "耐受配方";
    public const string DictProdBigCategoryTypePremilac = "培乐";
    public const string DictProdBigCategoryTypeEssence = "菁智";

    public const string DictTypeReport = "Report";
    public const string DictTypeProdBrand = "prodBrand";
    public const string DictProdBrandTypeGain = "金力";
    public const string DictProdBrandTypeGainPlus = "金幼";
    public const string DictProdBrandTypeGrow = "金学";
    public const string DictProdBrandTypePediasure = "小安素";
    public const string DictProdBrandTypePremilac = "培乐";
    public const string DictProdBrandTypeSimMon = "金妈";
    public const string DictProdBrandTypeSimilac = "金宝";
    public const string DictProdBrandTypeEssence = "菁智";
    public const string DictProdBrandTypeTotalComfort = "亲护";
    public const string DictProdBrandTypeSSC = "院内配方";
    public const string DictProdBrandTypeIsomil = "爱心美";
    public const string DictProdBrandTypeLF = "舒心美";
    public const string DictProdBrandTypeNeoSure = "出院后配方";
    public const string DictProdBrandTypeHMF = "母乳添加剂";
    public const string DictProdBrandTypeGainKid = "金童";
    public const string DictProdBrandTypeQinTi = "亲体";

    public const string DictTypeProdFormula = "prodFormula";
    public const string DictProdFormulaOrganic = "有机";
    public const string DictProdFormulaTC = "金装亲护";
    public const string DictProdFormulaLCP = "LCP";
    public const string DictProdFormulaNG = "NG";
    public const string DictProdFormulaNeoSure = "出院后配方";
    public const string DictProdFormulaHMF = "母乳添加剂";
    public const string DictProdFormulaIsomil = "爱心美";
    public const string DictProdFormulaLF = "舒心美";
    public const string DictProdFormulaPremilacPouch = "袋装培乐";
    public const string DictProdFormulaPremilacGold = "金装培乐";
    public const string DictProdFormulaSSC = "院内配方";
    public const string DictProdFormulaPurity = "纯净";


    public const string DictProdPackTinned = "听装";
//听装
//盒装
//袋装
//瓶装
//联包
    //public const string DictTypeProdStage = "prodStage";
    //public const string DictProdStageState1 = "第1阶段";
    //public const string DictProdStageState2 = "第2阶段";
    //public const string DictProdStageState3 = "第3阶段";
    //public const string DictProdStageState4 = "第4阶段";
    //public const string DictProdStagePrenatal = "产前";
    //public const string DictProdStagePediasure = "小安素";

    #endregion

    #region PGM薪资审批状态
    public const string DictTypeSalaryState = "pgmSalaryApproveState";
    public const string DictSalaryStateTypeEditable = "可编辑";
    public const string DictSalaryStateTypeSubmitted = "待审批";
    public const string DictSalaryStateTypeRSMApproved = "RSM审批通过";
    public const string DictSalaryStateTypeRPGMApproved = "RPGM审批通过";
    public const string DictSalaryStateTypeRBDMApproved = "RBD审批通过";
    public const string DictSalaryStateTypeHCApproved = "总部审批通过";
    #endregion

    //GE属性 Base  GE 调整成M Other
    public const string DictTypeGEAttribute = "geAttribute";
    public const string DictGEAttributeTypeBase = "M";
    public const string DictGEAttributeTypeGE = "Other";

    public const string DictTypeGpsApiType = "gpsApiType";
    public const string DictGpsApiTypeHigh = "high";
    public const string DictGpsApiTypeLow = "low";
    public const string DictGpsApiTypeUnknown = "unknown";

    //星期
    public const string Monday = "Monday";
    public const string Tuesday = "Tuesday";
    public const string Wednesday = "Wednesday";
    public const string Thursday = "Thursday";
    public const string Friday = "Friday";
    public const string Saturday = "Saturday";
    public const string Sunday = "Sunday";

    /// <summary>
    /// 预测将购买VIP客户
    /// </summary>
    public const string VIPCustomersListTypeBuy = "预测将购买VIP客户";
    public const string VIPCustomersListTypeUnBuy = "未购买VIP客户";
    public const string MyNewCustomerInfo = "我的新客资料";
    public const string MyHotlineNewCustomer = "我的热线新客查询";

    public const string KPIEnableM10M13Month = "2013-01-01";
    public const string KPIEnableM10M13Month2014 = "2014-01-01";
    public const string KPIEnableMonth2015 = "2015-01-01";

    //2016-05-30
    public const string DictTypeProjectType = "ProjectType";
    public const string DictProjectTypePND = "PND";
    public const string DictProjectTypeAND = "AND";

    //2014-03-28
    public const string MongoDBName = "MongoDBName";

    public const string TargetType = "BizTargetType";
    
    //ilead 图片获取途径
    /// <summary>
    /// 本地获取
    /// </summary>
    public const string ImgTypeLock = "1";
    public const string ImgTypeCamera = "2";
    public const string ImgTypeLockAndCamera = "3";


    public const string DivisionTypePND = "PND";
    public const string DivisionTypeMND = "AND";
    public const string DivisionTypePNDAndMND = "PND&AND";

    public const string SaleDivisionPND = "D01";
    public const string SaleDivisionMND = "D02";



  //知识库

    public const string DictTypeStudyProdInfo = "产品信息";
    public const string DictTypeStudySale = "营销技巧";
    public const string DictTypeStudySystem = "系统操作";
    //微信应用
    public const string WeChatUploadImagePath = "WeChatUploadImagePath";
    public const string WeChatUploadViocePath = "WeChatUploadViocePath";
    public const string WeChatUploadVideoPath = "WeChatUploadVideoPath";
    public const string WeChatUploadFilePath = "WeChatUploadFilePath";
    public const string WeChatCorpId = "CorpId";
    public const string WeChatCorpSecret = "CorpSecret";
    public const string WeChatToken = "Token";
    public const string WeChatEncodingAESKey = "EncodingAESKey";
    public const string WeChatCorpHost = "CorpHost";
    public const string MockUserId = "MockUserId";
    public const string CRMTest = "CRMTest";
    public const string CRMDbhost = "CRMDbhost";
    public const string CRMWfpUser = "CRMWfpUser";
    public const string CRMAppkey = "CRMAppkey";
    public const string CRMWebServiceURL = "CRMWebServiceURL";
    public const string ANDPGSignWebServiceURL = "ANDPGSignWebServiceURL";
    public const string ANDPGSignAppId = "ANDPGSignAppId";
    public const string ANDPGSignSecretKey = "ANDPGSignSecretKey";
    public const string SignLength = "SignLength";
    public const string CRMAgentID = "CRMAgentID";
    public const string CRMCorpUrl = "CorpUrl";

    //PG扫码签收赠品礼品
    public const string ScanQRCodeURL = "ScanQRCodeURL";
    public const string CommandIdQuery = "CommandIdQuery";
    public const string CommandIdVerified = "CommandIdVerified";
    public const string CommandIdResumed = "CommandIdResumed";
    public const string ScanQRCodeCorp = "ScanQRCodeCorp";

    //雅培宝贝1325至爱计划公众号 授权接口
    public const string CRMWechatAuthWebServiceURL = "CRMWechatAuthWebServiceURL";
    public const string CRMWechatCallBackURL = "CRMWechatCallBackURL";
    public const string CRMGetMobileURL = "CRMGetMobileURL";

    //AND - CRM
    public const string ANDCRMAPIServiceURL = "ANDCRMAPIServiceURL";
    public const string ANDPageCount = "ANDPageCount";
    public const string ANDQRCodeURL = "ANDQRCodeURL";
    public const string ANDQRCodeAppKey = "ANDQRCodeAppKey";

    public const string DictTypeWechatApp = "Agent";
    public const string DictTypeWechatMessageType = "ArticleType";
    public const string DictTypeArticleText = "文  本";
    public const string DictTypeArticleSingleImgText = "单图文";
    public const string DictTypeArticleMutiImgText = "多图文";
    public const string DictTypeArticleImage = "图  片";
    public const string DictTypeArticleVoice = "音  频";
    public const string DictTypeArticleVideo = "视  频";
    public const string DictTypeArticleFile = "文  件";
    public static Dictionary<decimal, string> ArticleTypeDict {
      get {
        return DataCache.GetOrDefault("base_dict,id=" + DictTypeWechatMessageType, () => {
          var dictBL = BLLFactory.Create<IBaseDictBL>();
          return dictBL.GetDictItemByTypeByCached(DictTypeWechatMessageType)
            .ToDictionary(i => i.ID, i => i.ItemName);
        });
      }
    }
    public static Dictionary<decimal, BaseDictItem> AgentTypeDict {
      get {
        return DataCache.GetOrDefault("base_dict,type=" + DictTypeWechatApp, () => {
          var dictBL = BLLFactory.Create<IBaseDictBL>();
          return dictBL.GetDictItemByTypeByCached(DictTypeWechatApp)
            .ToDictionary(i => i.ID);
        });
      }
    }
    public static Dictionary<string, BaseDictItem> AgentIDTypeDict {
      get {
        return DataCache.GetOrDefault("base_dict,id=" + DictTypeWechatApp, () => {
          var dictBL = BLLFactory.Create<IBaseDictBL>();
          return dictBL.GetDictItemByTypeByCached(DictTypeWechatApp)
            .ToDictionary(i => i.ItemCode);
        });
      }
    }
    public static Dictionary<decimal, BaseDictItem> CategoryDict {
      get {
        return DataCache.GetOrDefault("base_dict,id=" + DictTypeMessage, () => {
          var dictBL = BLLFactory.Create<IBaseDictBL>();
          return dictBL.GetDictItemByTypeByCached(DictTypeMessage)
            .ToDictionary(i => i.ID);
        });
      }
    }


    #region 2015年绩效考核职位
    public static readonly Dictionary<string, string> KPITypeDipslayNameDict2015 = new Dictionary<string, string>() {
      {DictKPITypeRepresentative, DictKPITypeRepresentative},
      {DictKPITypeDirector, DictKPITypeDirector},
      {DictKPITypeAreaManager, DictKPITypeAreaManager},
      {DictKPITypeRNGDirector, DictKPITypeRNGDirector},
      {DictKPITypeAreaKAManager, DictKPITypeAreaKAManager},
      {DictKPITypeAreaKADirector, DictKPITypeAreaKADirector},
      {DictKPITypeHCKARepresentative, DictKPITypeHCKARepresentative},
      {DictKPITypeHCKAManager, DictKPITypeHCKAManager},
      {DictKPITypeHCKADirector, DictKPITypeHCKADirector},
      {DictKPITypeRNGKAManager, DictKPITypeRNGKAManager},
      {DictKPITypeHCDirector, DictKPITypeHCDirector},
 {DictKPIType2014T1, "代表-负责AND有促销活动"}, //
 {DictKPIType2014T2, "代表-负责AND无促销活动"},     
 {DictKPIType2014T3, "主管-负责AND有促销活动"}, //
 {DictKPIType2014T4, "主管-负责AND无促销活动"},  //
 {DictKPIType2014T5, "地区经理-负责AND有促销活动"},  //
 {DictKPIType2014T6, "地区经理-负责AND无促销活动"},   //
 {DictKPIType2014T7, "大区经理-负责AND有促销活动"},  //
 {DictKPIType2014T8, "大区经理-负责AND无促销活动"},  //
 {DictKPIType2014T9, "大区经理"},//
 {DictKPIType2014T10, "大区经理/副总监-负责AND有促销活动"},//
 {DictKPIType2014T11, "大区经理/副总监-负责AND无促销活动"},//

 {DictKPIType2014T12, "地区KA经理-负责AND有促销活动"},//
 {DictKPIType2014T13, "地区KA经理-负责AND无促销活动"},
 {DictKPIType2014T14, "大区KA经理-负责AND有促销活动"},
 {DictKPIType2014T15, "大区KA经理-负责AND无促销活动"},
 {DictKPIType2014T16, "地区KA主管-负责AND有促销活动"},
 {DictKPIType2014T17, "地区KA主管-负责AND无促销活动"},//
 {DictKPIType2014T18, "总部KA代表（非B2C）-负责AND有促销活动"},   //
 {DictKPIType2014T19, "总部KA代表（非B2C）-负责AND无促销活动"},    //
 {DictKPIType2014T20, "总部KA主管（非B2C）-负责AND有促销活动"},     //
 {DictKPIType2014T21, "总部KA主管（非B2C）-负责AND无促销活动"},
 {DictKPIType2014T22, "总部客户组KA经理(非B2C)-负责AND有促销活动"},     //
 {DictKPIType2014T23, "总部客户组KA经理(非B2C)-负责AND无促销活动"},    //
 {DictKPIType2014T24, "总部非客户组KA经理(非B2C)-负责AND有促销活动"},    //
 {DictKPIType2014T25, "总部非客户组KA经理(非B2C)-负责AND无促销活动"},    //
 {DictKPIType2014T26, "总部KA代表(B2C)-负责AND"},        //
 {DictKPIType2014T27, "总部KA代表(B2C)"},   //
 {DictKPIType2014T28, "总部KA主管(B2C)-负责AND"},  //
 {DictKPIType2014T29, "总部KA主管(B2C)"},
 {DictKPIType2014T30, "总部客户组KA经理(B2C)-负责AND"}, 
 {DictKPI2014TypeT31, "总部客户组KA经理(B2C)"},  //
 {DictKPI2014TypeT32, "总部非客户组KA经理(B2C)-负责AND"},  
 {DictKPI2014TypeT33,"总部非客户组KA经理(B2C)"},
 { DictKPI2014TypeT34,"总部非客户组KA经理(非B2C)"},    //
 { DictKPITypeT35,"总部客户组KA经理(非B2C)"},   
    };

    
    public const string DictKPIMatrixType2015M1 = "M1";

    public const string DictKPIMatrixType2015M2 = "M2";

    public const string DictKPIMatrixType2015M3 = "M3";

    public const string DictKPIMatrixType2015M4 = "M4";

    public const string DictKPIMatrixType2015M5 = "M5";

    public const string DictKPIMatrixType2015M6 = "M6";

    public const string DictKPIMatrixType2015M7 = "M7";

    public const string DictKPIMatrixType2015M8 = "M8";

    public const string DictKPIMatrixType2015M9 = "M9";

    public const string DictKPIMatrixType2015M10 = "M10";
    
    public const string DictKPIMatrixType2015M11 = "M11";

    public const string DictKPIMatrixType2015M12 = "M12";

    /// <summary>
    /// 总部客户组KA经理(非B2C)
    /// </summary>
    public const string DictKPITypeT35= "T35";


    ///<summary>
    /// PND Sell in(PND) by city
    /// </summary>
    public const string DictKPIMatrixType2015M1_City = "M1-C";
    /// <summary>
    /// PND Sell In SSP
    /// </summary>
    public const string DictKPIMatrixType2015M2_City = "M2-C";

    /// <summary>
    /// pnd Sell out by city
    /// </summary>
    public const string DictKPIMatrixType2015M3_City = "M3-C";

    /// <summary>
    /// ssp Sell out by city
    /// </summary>
    public const string DictKPIMatrixType2015M4_City = "M4-C";

    /// <summary>
    /// NU
    /// </summary>
    public const string DictKPIMatrixType2015M5_City = "M5-C";

    /// <summary>
    /// AND SellIn
    /// </summary>
    public const string DictKPIMatrixType2015M6_City = "M6-C";

    /// <summary>
    /// AND Sell Out
    /// </summary>
    public const string DictKPIMatrixType2015M7_City = "M7-C";


    /// <summary>
    /// Sell in by person Raw
    /// </summary>
    public const string DictKPIMatrixType2015M1_Raw = "M1-R";
    public const string DictKPIMatrixType2015M2_Raw = "M2-R";
    public const string DictKPIMatrixType2015M3_Raw = "M3-R";
    public const string DictKPIMatrixType2015M4_Raw = "M4-R";
    public const string DictKPIMatrixType2015M5_Raw = "M5-R";
    public const string DictKPIMatrixType2015M6_Raw = "M6-R";
    /// <summary>
    /// Sell out by Person Raw
    /// </summary>
    public const string DictKPIMatrixType2015M7_Raw = "M7-R";


    public static readonly Dictionary<string, string> DictKPIType_City = new Dictionary<string, string>()
    {
      {DictKPIMatrixType2015M1_City,"PND-Sell In(PND)"},
      {DictKPIMatrixType2015M2_City,"PND-Sell In(SSP)"},
      {DictKPIMatrixType2015M3_City,"PND-Sell Out(PND)"},
      {DictKPIMatrixType2015M4_City,"PND-Sell Out(SSP)"},
      {DictKPIMatrixType2015M5_City,"PND-NU"},
      {DictKPIMatrixType2015M6_City,"AND-Sell In"},
      {DictKPIMatrixType2015M7_City,"AND-Sell Out"}
    };

    public static readonly Dictionary<string, string> DictKPIType_Raw = new Dictionary<string, string>()
    {
      {DictKPIMatrixType2015M1_Raw,"PND-Sell In(PND)"},
      {DictKPIMatrixType2015M2_Raw,"PND-Sell In(SSP)"},
      {DictKPIMatrixType2015M3_Raw,"PND-Sell Out(PND)"},
      {DictKPIMatrixType2015M4_Raw,"PND-Sell Out(SSP)"},
      {DictKPIMatrixType2015M5_Raw,"PND-NU"},
      {DictKPIMatrixType2015M6_Raw,"AND-Sell In"},
      {DictKPIMatrixType2015M7_Raw,"AND-Sell Out"}
    };

      
    public static readonly Dictionary<string, string> KPIType2015Dict= new Dictionary<string, string>() {
      {DictKPITypeRepresentative, DictKPITypeRepresentative},
      {DictKPITypeDirector, DictKPITypeDirector},
      {DictKPITypeAreaManager, DictKPITypeAreaManager},
      {DictKPITypeRNGDirector, DictKPITypeRNGDirector},
      {DictKPITypeAreaKAManager, DictKPITypeAreaKAManager},
      {DictKPITypeAreaKADirector, DictKPITypeAreaKADirector},
      {DictKPITypeHCKARepresentative, DictKPITypeHCKARepresentative},
      {DictKPITypeHCKAManager, DictKPITypeHCKAManager},
      {DictKPITypeHCKADirector, DictKPITypeHCKADirector},
      {DictKPITypeRNGKAManager, DictKPITypeRNGKAManager},
      {DictKPITypeHCDirector, DictKPITypeHCDirector},
 {"代表-负责AND有促销活动", DictKPIType2014T1}, //
 {"代表-负责AND无促销活动",DictKPIType2014T2},     
 {"主管-负责AND有促销活动",DictKPIType2014T3}, //
 {"主管-负责AND无促销活动",DictKPIType2014T4},  //
 { "地区经理-负责AND有促销活动",DictKPIType2014T5},  //
 { "地区经理-负责AND无促销活动",DictKPIType2014T6},   //
 { "大区经理-负责AND有促销活动",DictKPIType2014T7},  //
 { "大区经理-负责AND无促销活动",DictKPIType2014T8},  //
 { "大区经理",DictKPIType2014T9},//
 { "大区经理/副总监-负责AND有促销活动",DictKPIType2014T10},//
 { "大区经理/副总监-负责AND无促销活动",DictKPIType2014T11},//

 { "地区KA经理-负责AND有促销活动",DictKPIType2014T12},//
 { "地区KA经理-负责AND无促销活动",DictKPIType2014T13},
 { "大区KA经理-负责AND有促销活动",DictKPIType2014T14},
 { "大区KA经理-负责AND无促销活动",DictKPIType2014T15},
 { "地区KA主管-负责AND有促销活动",DictKPIType2014T16},
 { "地区KA主管-负责AND无促销活动",DictKPIType2014T17},//

 { "总部KA代表（非B2C）-负责AND有促销活动",DictKPIType2014T18},   //
 { "总部KA代表（非B2C）-负责AND无促销活动",DictKPIType2014T19},    //
 { "总部KA主管（非B2C）-负责AND有促销活动",DictKPIType2014T20},     //
 { "总部KA主管（非B2C）-负责AND无促销活动",DictKPIType2014T21},
 { "总部客户组KA经理(非B2C)-负责AND有促销活动",DictKPIType2014T22},     //
 { "总部客户组KA经理(非B2C)-负责AND无促销活动",DictKPIType2014T23},    //
 
 { "总部非客户组KA经理(非B2C)-负责AND有促销活动",DictKPIType2014T24},    //
 { "总部非客户组KA经理(非B2C)-负责AND无促销活动",DictKPIType2014T25},    //

 { "总部KA代表(B2C)-负责AND",DictKPIType2014T26},        //
 { "总部KA代表(B2C)",DictKPIType2014T27},   //
 { "总部KA主管(B2C)-负责AND",DictKPIType2014T28},  //
 { "总部KA主管(B2C)",DictKPIType2014T29},
 { "总部客户组KA经理(B2C)-负责AND",DictKPIType2014T30}, 
 { "总部客户组KA经理(B2C)",DictKPI2014TypeT31},  //
 { "总部非客户组KA经理(B2C)-负责AND",DictKPI2014TypeT32},  
 {"总部非客户组KA经理(B2C)",DictKPI2014TypeT33},
 { "总部非客户组KA经理(非B2C)",DictKPITypeT34},    //
 { "总部客户组KA经理(非B2C)",DictKPITypeT35},    //
    };
    #endregion


    /// <summary>
    /// 10个城市
    /// </summary>
    public static decimal[] AbtCityDict = new decimal[]
    {
      293,/*东莞*/299,/*福州*/312,/*南宁*/327,/*郑州*/405,/*苏州*/369,/*济南*/379,/*西安*/388,/*成都*/415,/*天津*/424 /*宁波*/,
      386/*上海*/
    };

    public const string OOSStateIn = "有";
    public const string OOSStateOut = "缺";
    public static IList<string> OOSStatus() {
      return new List<string>() { OOSStateIn, OOSStateOut };
    }

    public const string OffEndTime = "OffEndTime";
    public const string OffStartTime = "OffStartTime";
    public const string TargetSettingOffDay = "TargetSettingOffDay";

      /// <summary>
      /// 在线考试-题库类型
      /// </summary>
    public const string DictTopicType = "topicType";
    /// <summary>
    /// 在线考试-试题类型
    /// </summary>
    public const string DictTopicItemType = "topicItemType";
    public const string DictTopicItemTypeMultiSelect = "多选";
    public const string DictTopicItemTypeSingleSelect = "单选";

    public static IList<string> GetTopicItemTypeList()
    {
        return new List<string>()
          {
              DictTopicItemTypeMultiSelect,DictTopicItemTypeSingleSelect
          };
    }

    public const string DictTypeProdDisplayBigBrand = "prodDisplayBigBrand";
    public const string DictTypeProdDisplayBrand = "prodDisplayBrand";
    public const string DictTypeProdDisplayType = "prodDisplayType";
    public const string DictTypeProdDisplayPackSpec = "packDisplayid";
    public const string DictTypeProdDisplaySegment = "prodDisplaySegment";
    public const string DictTypeProdDisplayBigCategory = "prodDisplayBigCategory";
    public const string DictTypeProdDisplayFormula = "prodDisplayFormula";

    public const string DictTypeBenchmark = "Benchmark";
    public const string DictBenchmarkLm = "VS.LM";
    public const string DictBenchmarkL3m = "VS.L3M";
    public const string DictBenchmarkQ1 = "VS.Q1";
    public const string DictBenchmarkQ2 = "VS.Q2";
    public const string DictBenchmarkQ3 = "VS.Q3";
    public const string DictBenchmarkQ4 = "VS.Q4";
    public const string DictBenchmarkLY = "VS.LY";

    public const string DictReportRunStatusDone = "Done";

    public const string DictType532prodType = "532prodType";
    public const string DictType532prodTypeSSP = "SSP";
    public const string DictType532prodTypeSP = "SP";
    public const string DictType532prodTypeALL = "ALL";

    public const string DictType532brand = "532brand";
    public const string DictType532brandStore = "门店";
    public const string DictType532brandAbbott = "雅培";


    public const string DictType532brandWyeth = "惠氏";
    public const string DictType532brandMeadJohnson = "美赞臣";
    public const string DictType532brandFriso = "美素";
    public const string DictType532brandNutricia = "纽迪西亚（含牛栏/爱他美）";


    public const string DictType532brandBiostime = "合生元";
    public const string DictType532brandNestle = "雀巢";
    public const string DictType532brandFeihe = "飞鹤";
    public const string DictType532brandOther1 = "其他竞品1";
    public const string DictType532brandOther2 = "其他竞品2";


    public static Dictionary<string,string> BrandColor {

      get {
        return new Dictionary<string,string>() {
          { DictType532brandAbbott,"#0079db"},
           { DictType532brandWyeth,"#c71718"},
           { DictType532brandMeadJohnson,"#f59225"},
           { DictType532brandFriso,"#f6dc00"},
           { DictType532brandNutricia,"#c6db46"},
           { DictType532brandBiostime,"#60c882"},
           { DictType532brandNestle,"#5bb0e3"},
           { DictType532brandFeihe,"#f26469"},
           { DictType532brandOther1,"#cd8cbd"},
           { DictType532brandOther2,"#877ac2"}
        };
      }
    }

    public static readonly Dictionary<string,List<BaseDictItem>> subBrandDict = new Dictionary<string,List<BaseDictItem>>() {
      {"5462",new List<BaseDictItem>() { new BaseDictItem() { ItemName= "ELEVA-BLUE",ID= 505 },new BaseDictItem() {ItemName= "ELEVA-GREEN",ID=506 } } },
        {"5460",new List<BaseDictItem>() { new BaseDictItem() { ItemName= "QINTI-JX",ID= 507 },new BaseDictItem() {ItemName= "QINTI-AMS",ID=508 } ,
  new BaseDictItem() {ItemName= "QINTI-EURO",ID=509 },new BaseDictItem() {ItemName= "QINTI-USC",ID=510 }}},
        {"5465",new List<BaseDictItem>() { new BaseDictItem() { ItemName= "ABBOTT-SGOLD",ID= 511 } } },
        {"5463",new List<BaseDictItem>() { new BaseDictItem() { ItemName= "TC-TC",ID= 512 },new BaseDictItem() {ItemName= "TC-LF",ID= 705 } } },
        {"5461",new List<BaseDictItem>() {new BaseDictItem() { ItemName= "PEDIASURE",ID= 706 } }},
        {"5464",new List<BaseDictItem>() {new BaseDictItem() { ItemName= "OTHERS-Mama",ID= 717 },new BaseDictItem() {ItemName= "OTHERS-PRETERM",ID= 723 },new BaseDictItem() {ItemName= "OTHERS-OTHERS",ID= 5063 } }}
    };

    
    public static readonly Dictionary<string,string> ProdSegmentNameDict = new Dictionary<string,string>() {
      {"QINTI" , "亲体"},
      {"ELEVA", "菁智"},
      {"TC" , "亲护"},
      {"PEDIASURE", "小安素"},
      {"OTHERS" , "其它"}
    };

    public static readonly Dictionary<string,string> ActivityStatusDict = new Dictionary<string,string>() {
      { "S","未汇报" },
      { "R","已汇报"},
      { "C","已取消"}
    };

		public static Dictionary<string,List<string>> DictDAPRODBRAND = new Dictionary<string, List<string>>() {
        {"菁智有机",new List<string>() { "ELEVA-GREEN","B29"} },
		{"菁智纯净",new List<string>() { "ELEVA-BLUE" } },
        //{"菁智优纯",new List<string>() { "ELEVA-BLUE" } },
		{"亲护",new List<string>() { "TC-TC","TC-LF"} },
		{"欧版亲体",new List<string>() { "QINTI-EURO"} },
		{"嘉兴亲体",new List<string>() { "QINTI-JX"} },
		{"小安素",new List<string>() { "PEDIASURE" } },
		{"妈妈",new List<string>() { "OTHERS-Mama" } },
		{"其他",new List<string>() { "ABBOTT-SGOLD","OTHERS-OTHERS","OTHERS-PRETERM","QINTI-USC","QINTI-AMS"} }
        //{"菁智有机",new List<string>() { "B23","B29"} },
        //{"菁智纯净",new List<string>() { "B24" } },
        //{"亲护",new List<string>() { "B14","B20"} },
        //{"欧版亲体",new List<string>() { "B27"} },
        //{"嘉兴亲体",new List<string>() { "B27"} },
        //{"小安素",new List<string>() { "B07" } },
        //{"妈妈",new List<string>() { "B09" } },
        //{"其他",new List<string>() { "B16","B17","B28"} }
	};
        public static readonly Dictionary<string,string> OrderStatusDict = new Dictionary<string,string>() {
      { "1","待BI审核" },
      { "2","待客服审核"},
      { "3","BI审核未通过订单"},
      { "4","消费者未提交"},
      { "5","待PG审核"},
      { "6","PG拒绝"},
      { "7","客服拒绝"},
      { "8","BI审核成功"}
    };
    }
}

