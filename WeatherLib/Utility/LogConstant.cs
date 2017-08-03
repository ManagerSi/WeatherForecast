using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherForecast.Utility {
  public class LogConstant {
    /// <summary>
    /// 手机端登录
    /// </summary>
    public const string ActionWapLogin = "WapLogin";
    
    /// <summary>
    /// 手机端门店新客上报
    /// </summary>
    public const string ActionWapStoreCustomers = "WapStoreCustomers";

    /// <summary>
    /// 手机端门店VIP客户上报
    /// </summary>
    public const string ActionWapStoreVIPCustomers = "WapStoreVIPCustomers";

    /// <summary>
    /// 手机端登录失败
    /// </summary>
    public const string ActionWapLoginFailed = "WapLoginFailed";
    /// <summary>
    /// 手机端修改密码
    /// </summary>
    public const string ActionWapChangePassword = "WapChPwd";

    /// <summary>
    /// 手机端修改密码失败
    /// </summary>
    public const string ActionWapChangePasswordFailed = "WapChPwdFailed";

    /// <summary>
    /// 手机端密码重置
    /// </summary>
    public const string ActionWapResetPassword = "WapRstPwd";

    /// <summary>
    /// 手机端密码重置
    /// </summary>
    public const string ActionWapResetPasswordFailed = "WapRstPwdFailed";

    /// <summary>
    /// 手机端密码重置
    /// </summary>
    public const string ActionWapResetPasswordLocked = "WapRstPwdLocked";

    /// <summary>
    /// 手机端密码重置
    /// </summary>
    public const string ActionWapResetMessaged = "WapRstPwdMsg";
    /// <summary>
    /// 账户锁定
    /// </summary>
    public const string ActionWapAccountLocked = "WapLocked";

    /// <summary>
    /// 进店
    /// </summary>
    public const string ActionWapStoreEnter = "ActionWapStoreEnter";


    /// <summary>
    /// 在线学习
    /// </summary>
    public const string ActionAdminEleaning = "AdminEleaning";

    /// <summary>
    /// 消息
    /// </summary>
    public const string ActionAdminMessage = "AdminMessage";

    /// <summary>
    /// 用户信息
    /// </summary>
    public const string ActionAdminUser = "AdminUser";

    /// <summary>
    /// 修改密码
    /// </summary>
    public const string ActionAdminUpdatePwd = "AdminUpdatePwd";

    /// <summary>
    /// 新客报表
    /// </summary>
    public const string ActionCustomersReport = "CustomersReport";

    /// <summary>
    ///  新客周报表
    /// </summary>
    public const string  ActionCustomersWeeklyReport = "CustomersWeeklyReport";

    /// <summary>
    /// 新客月报表
    /// </summary>
    public const string ActionCustomersMonthlyReport = "CustomersMonthlyReport";

    /// <summary>
    /// 产品价格异常报表
    /// </summary>
    public const string ActionProductPriceStateReport = "ProductPriceStateReport";

    /// <summary>
    /// 未分销产品报表
    /// </summary>
    public const string ActionProductNotDistReport = "ProductNotDistReport";

    /// <summary>
    /// 拜访统计报表
    /// </summary>
    public const string ActionVstSummaryReport = "VstSummaryReport";

    /// <summary>
    /// 店内照片报表
    /// </summary>
    public const string ActionStoreImageReport = "StoreImageReport";

    /// <summary>
    ///  促销活动报表
    /// </summary>
    public const string ActionDisplayReport = "DisplayReport";

    /// <summary>
    /// 订单报表
    /// </summary>
    public const string ActionOrderReport = "OrderReport";

    /// <summary>
    /// 库存报表
    /// </summary>
    public const string ActionStocksReport = "StocksReport";

    /// <summary>
    /// 拜访总结报表
    /// </summary>
    public const string ActionSummaryReport = "SummaryReport";

    /// <summary>
    /// 调查问卷报表
    /// </summary>
    public const string ActionVstQuesReport = "VstQuesReport";

    /// <summary>
    /// 系统端-数据查询-分销修改
    /// </summary>
    public const string ActionAdminVstDistEdit = "AdminVstDistEdit";

    /// <summary>
    /// 系统端-数据查询-分销删除
    /// </summary>
    public const string ActionAdminVstDistDelete = "AdminVstDistDelete";

    /// <summary>
    /// 系统端-数据查询-分销导出
    /// </summary>
    public const string ActionAdminVstDistReport = "AdminVstDistReport";

    /// <summary>
    /// 系统端-数据查询-价格修改
    /// </summary>
    public const string ActionAdminVstPriceEdit = "AdminVstPriceEdit";

    /// <summary>
    /// 系统端-数据查询-价格删除
    /// </summary>
    public const string ActionAdminVstPriceDelete = "AdminVstPriceDelete";

    /// <summary>
    /// 系统端-数据查询-价格导出
    /// </summary>
    public const string ActionAdminVstPriceReport = "AdminVstPriceReport";

    /// <summary>
    /// 系统端-数据查询-PGO销量修改
    /// </summary>
    public const string ActionAdminVstSaleEdit = "AdminVstSaleEdit";

    /// <summary>
    /// 系统端-数据查询-PGO销量删除
    /// </summary>
    public const string ActionAdminVstSaleDelete = "AdminVstSaleDelete";

    /// <summary>
    /// 系统端-数据查询-PGO销量导出
    /// </summary>
    public const string ActionAdminVstSaleReport = "AdminVstSaleReport";

    /// <summary>
    /// 系统端-数据查询-新客资料修改
    /// </summary>
    public const string ActionAdminVstCustomersEdit = "AdminVstCustomersEdit";

    /// <summary>
    /// 系统端-数据查询-新客资料删除
    /// </summary>
    public const string ActionAdminVstCustomersDelete = "AdminVstCustomersDelete";

    /// <summary>
    /// 系统端-数据查询-新客资料导出
    /// </summary>
    public const string ActionAdminVstCustomersReport = "AdminVstCustomersReport";

    /// <summary>
    /// 系统端-数据查询-主货架照片删除
    /// </summary>
    public const string ActionAdminVstShelfPicDelete = "AdminVstShelfPicDelete";

    /// <summary>
    /// 系统端-数据查询-主货架照片导出
    /// </summary>
    public const string ActionAdminVstShelfPicReport = "AdminVstShelfPicReport";

    /// <summary>
    /// 系统端-数据查询-主货架陈列修改
    /// </summary>
    public const string ActionAdminVstShelfDisplayEdit = "AdminVstShelfDisplayEdit";

    /// <summary>
    /// 系统端-数据查询-主货架陈列删除
    /// </summary>
    public const string ActionAdminVstShelfDisplayDelete = "AdminVstShelfDisplayDelete";

    /// <summary>
    /// 系统端-数据查询-主货架陈列导出
    /// </summary>
    public const string ActionAdminVstShelfDisplayReport = "AdminVstShelfDisplayReport";

    /// <summary>
    /// 系统端-数据查询-库存修改
    /// </summary>
    public const string ActionAdminVstStocksEdit = "AdminVstStocksEdit";

    /// <summary>
    /// 系统端-数据查询-库存删除
    /// </summary>
    public const string ActionAdminVstStocksDelete = "AdminVstStocksDelete";

    /// <summary>
    /// 系统端-数据查询-库存导出
    /// </summary>
    public const string ActionAdminVstStocksReport = "AdminVstStocksReport";

    /// <summary>
    /// 系统端-数据查询-竞品分销修改
    /// </summary>
    public const string ActionAdminVstCompetDistEdit = "AdminVstCompetDistEdit";

    /// <summary>
    /// 系统端-数据查询-竞品分销删除
    /// </summary>
    public const string ActionAdminVstCompetDistDelete = "AdminVstCompetDistDelete";

    /// <summary>
    /// 系统端-数据查询-竞品分销导出
    /// </summary>
    public const string ActionAdminVstCompetDistReport = "AdminVstCompetDistReport";

    /// <summary>
    /// 系统端-数据查询-竞品价格修改
    /// </summary>
    public const string ActionAdminVstCompetPriceEdit = "AdminVstCompetPriceEdit";

    /// <summary>
    /// 系统端-数据查询-竞品价格删除
    /// </summary>
    public const string ActionAdminVstCompetPriceDelete = "AdminVstCompetPriceDelete";

    /// <summary>
    /// 系统端-数据查询-竞品价格导出
    /// </summary>
    public const string ActionAdminVstCompetPriceReport = "AdminVstCompetPriceReport";

    /// <summary>
    /// 系统端-数据查询-竞品PG和促销修改
    /// </summary>
    public const string ActionAdminVstCompetPGEdit = "AdminVstCompetPGEdit";

    /// <summary>
    /// 系统端-数据查询-竞品PG和促销删除
    /// </summary>
    public const string ActionAdminVstCompetPGDelete = "AdminVstCompetPGDelete";

    /// <summary>
    /// 系统端-数据查询-竞品PG和促销导出
    /// </summary>
    public const string ActionAdminVstCompetPGReport = "AdminVstCompetPGReport";

    /// <summary>
    /// 系统端-数据查询-竞品照片导出
    /// </summary>
    public const string ActionAdminVstCompetPicReport = "AdminVstCompetPicReport";

    /// <summary>
    /// 系统端-数据查询-缺货修改
    /// </summary>
    public const string ActionAdminVstOOSEdit = "AdminVstOOSEdit";

    /// <summary>
    /// 系统端-数据查询-缺货删除
    /// </summary>
    public const string ActionAdminVstOOSDelete = "AdminVstOOSDelete";

    /// <summary>
    /// 系统端-数据查询-缺货导出
    /// </summary>
    public const string ActionAdminVstOOSReport = "AdminVstOOSReport";

    /// <summary>
    /// 系统端-数据查询-拜访总结修改
    /// </summary>
    public const string ActionAdminVstSummaryEdit = "AdminVstSummaryEdit";

    /// <summary>
    /// 系统端-数据查询-拜访总结删除
    /// </summary>
    public const string ActionAdminVstSummaryDelete = "AdminVstSummaryDelete";

    /// <summary>
    /// 系统端-数据查询-拜访总结导出
    /// </summary>
    public const string ActionAdminVstSummaryReport = "AdminVstSummaryReport";

    /// <summary>
    /// 系统端-数据查询-建议订单修改
    /// </summary>
    public const string ActionAdminVstOrderEdit = "AdminVstOrderEdit";

    /// <summary>
    /// 系统端-数据查询-建议订单删除
    /// </summary>
    public const string ActionAdminVstOrderDelete = "AdminVstOrderDelete";

    /// <summary>
    /// 系统端-数据查询-建议订单导出
    /// </summary>
    public const string ActionAdminVstOrderReport = "AdminVstOrderReport";

    /// <summary>
    /// 系统端-数据查询-促销活动修改
    /// </summary>
    public const string ActionAdminVstDisplayEdit = "AdminVstDisplayEdit";

    /// <summary>
    /// 系统端-数据查询-促销活动删除
    /// </summary>
    public const string ActionAdminVstDisplayDelete = "AdminVstDisplayDelete";

    /// <summary>
    /// 货架顺位陈列面及占比报告
    /// </summary>
    public const string ActionAdminVstShelfPositionReport = "AdminVstShelfPositionReport";

    /// <summary>
    /// 主货架陈列达标检查报告
    /// </summary>
    public const string ActionAdminVstShelfDistReport = "AdminVstShelfDistReport";

    /// <summary>
    /// 系统端-数据查询-促销活动导出
    /// </summary>
    public const string ActionAdminVstDisplayReport = "AdminVstDisplayReport";

    /// <summary>
    /// 系统端-数据查询-导出销量精准性报告
    /// </summary>
    public const string ActionPGMReportSaleExceptionReport = "PGMReportSaleExceptionReport";

    /// <summary>
    /// 系统端-PGM管理-导出基本薪资范围报告
    /// </summary>
    public const string ActionPGMReportChargeRangeReport = "PGMReportChargeRangeReport";

    /// <summary>
    /// 系统端-PGM管理-导出额度管理报告
    /// </summary>
    public const string ActionPGMReportHeadcountReport = "PGMReportHeadcountReport";

     /// <summary>
    /// 系统端-PGM管理- 人员信息总表
    /// </summary>
    public const string ActionPGMReportEmployeeSummary = "PGMReportEmployeeSummary";

    /// <summary>
    /// 系统端-PGM管理- 人员变更表
    /// </summary>
    public const string ActionPGMReportEmployeeChange = "PGMReportEmployeeChange";
	
    /// <summary>
    /// 系统端-PGM管理-导出商务渠道日报
    /// </summary>
    public const string ActionPGMCRMTradeChannelDailyReport = "PGMCRMTradeChannelDailyReport";

    /// <summary>
    /// 系统端-PGM管理-大区指标设置
    /// </summary>
    public const string ActionAdminPGMSaleTargetArea1Edit = "PGMCRMTradeChannelDailyReport";

    /// <summary>
    /// 系统端-PGM管理-指标设置
    /// </summary>
    public const string ActionAdminReportPGMSaleTargetReport = "PGMSaleTargetReport";

    /// <summary>
    /// 门店拜访覆盖报表
    /// </summary>
    public const string ActionAdminReportCoachDetailReport = "AdminCoachDetailMonthlyReport";

    /// <summary>
    /// 区域新客汇总报表
    /// </summary>
    public const string ActionAdminReportCustomersDailyByOrgReport = "AdminReportCustomersDailyByOrgReport";

    /// <summary>
    /// 区域新客汇总报表
    /// </summary>
    public const string ActionAdminReportGEStoreCustomersDailyByOrgReport = "AdminReportGEStoreCustomersDailyByOrgReport";

    /// <summary>
    /// KPI SellIn/Out报表
    /// </summary>
    public const string ActionAdminReportSellInOutReport = "KPISellInOutReport";
    /// <summary>
    /// 综合报表
    /// </summary>
    public const string ActionAdminKPIReport = "AdminKPIReport";
    /// <summary>
    /// 门店产品价格明细报表
    /// </summary>
    public const string ActionAdminReportStorePriceTrackReport = "ReportStorePriceTrackReport";
    /// <summary>
    /// 区域PGO达成情况月报表
    /// </summary>
    public const string ActionAdminReportSaleMonthlyDetailByOrgReport = "ReportSaleMonthlyDetailByOrgReport";
    /// <summary>
    /// GE City Score Card报表 (按照城市导出)
    /// </summary>
    public const string ActionAdminReportGECityScoreCardCityDetailReport = "ReportGECityScoreCardCityDetailReport";
    /// <summary>
    /// GE City Score Card报表  (按照时间导出)
    /// </summary>
    public const string ActionAdminReportGECityScoreCardDateDetailReport = "ReportGECityScoreCardDateDetailReport";
    /// <summary>
    /// KPI直接和间接指标按人员汇总
    /// </summary>
    public const string ActionAdminReportKPIGatherByPersonReport = "ActionAdminReportKPIGatherByPersonReport";
    /// <summary>
    /// KPI直接和间接指标按城市汇总
    /// </summary>
    public const string ActionAdminReportKPIGatherByCitynReport = "ActionAdminReportKPIGatherByCitynReport";
    /// <summary>
    /// KPI人员列表
    /// </summary>
    public const string ActionAdminReportKPIPersonReport = "ActionAdminReportKPIPersonReport";
    /// <summary>
    /// 根据Token重定向
    /// </summary>
    public const string ActionAPIRedirectByToken = "ActionAPIRedirectByToken";
    /// <summary>
    /// 签到
    /// </summary>
    public const string ActionApiStoreVst = "ActionApiStoreVst";

    /// <summary>
    /// 签到
    /// </summary>
    public const string ActionApiReviewSignIn = "ActionApiReviewSignIn";
    /// <summary>
    /// 系统端-数据查询-上月销量修改
    /// </summary>
    public const string ActionAdminVstStoreSaleMonthlyEdit = "AdminVstStoreSaleMonthlyEdit";

    /// <summary>
    /// 系统端-数据查询-上月销量删除
    /// </summary>
    public const string ActionAdminVstStoreSaleMonthlyDelete = "AdminVstStoreSaleMonthlyDelete";

    /// <summary>
    /// 系统端-数据查询-上月销量导出
    /// </summary>
    public const string ActionAdminVstStoreSaleMonthlyReport = "AdminVstStoreSaleMonthlyReport";

    /// <summary>
    /// 主货架管理
    /// </summary>
    public const string ActionReportStoreShelfDailyReport = "ReportStoreShelfDailyReport";

    /// <summary>
    /// 主货架管理
    /// </summary>
    public const string ActionReportStoreStockExpDailyReport = "ReportStoreStockExpDailyReport";

    /// <summary>
    ///  门店达标
    /// </summary>
    public const string ActionReportShelfTargetReport = "ReportShelfTargetReport";

    /// <summary>
    ///  月末库存上报追踪报告
    /// </summary>
    public const string ActionReportStockEXPDateTrackingMonthlyReport = "ReportStockEXPDateTrackingMonthlyReport";

    public const string ActionReportPGNarrowVIPByPGStoreReport = "ReportPGNarrowVIPByPGStoreReport";
    public const string ActionReportPGNarrowVIPByAreaReport = "ReportPGNarrowVIPByAreaReport";
    public const string ActionReportPGNarrowVIPByAbotCityReport = "ReportPGNarrowVIPByCityReport";
    public const string ActionReportBICRMPGVIPDetailReport = "ReportBICRMPGVIPDetailReport";
    /// <summary>
    /// 报表查询-在线考试报表-考试成绩查询
    /// </summary>
    public const string ActionReportBizExamResultReport = "ReportBizExamResultReport";
    public const string ActionReportEmpExamedReport = "ReportEmpExamedReport";
    public const string ActionReportEmpUnExamedReport = "ReportEmpUnExamedReport";
    public const string ActionReportExamedByOrgReport = "ReportExamedByOrgReport";
    public const string ActionReportExamedByCityReport = "ReportExamedByCityReport";
    /// <summary>
    /// 532数据管理
    /// </summary>
    public const string ActionVst532StoreSaleEdit = "Vst532StoreSaleEdit";
    public const string ActionVst532StoreSaleDelete = "Vst532StoreSaleDelete";

    /// <summary>
    /// TBBS客户管理
    /// </summary>
    public const string ActionTBBSStoreSaleEdit = "VstTBBSAccountSaleEdit";
  }
}
