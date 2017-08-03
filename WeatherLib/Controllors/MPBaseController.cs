using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Mobizone.TSIC.BLL;
using Mobizone.TSIC.Models;
using Mobizone.TSIC.MP;
using Mobizone.TSIC.Utility;
using Mobizone.TSIC.Web;
using log4net;

namespace Mobizone.TSIC.Controllers {
  public class MPBaseController : BaseController {
    private static readonly ILog log = LogManager.GetLogger(typeof(MPBaseController));
    /// <summary>
    /// 判断是否为微信
    /// </summary>
    public bool IsWeChat
    {
        get { return WeChatInternalFilterAttribute.IsWeChat(Request); }
    }

    public bool HandleUnauthorizedRequest(AuthorizationContext filterContext) {
        if (!IsWeChat)
        {
            filterContext.Result = RedirectToAction("Login", "WapAccount", new { area = "Wap" });
            return false;
        }
        else {
            var Flag_MockUserId = ConfigurationManager.AppSettings[BaseDictType.MockUserId];
            string CorpID = ConfigurationManager.AppSettings[BaseDictType.WeChatCorpId];
            string Host = ConfigurationManager.AppSettings[BaseDictType.WeChatCorpHost];
            string test = null;// ConfigurationManager.AppSettings[BaseDictType.CRMTest];
            if (!string.IsNullOrEmpty(Flag_MockUserId) && !string.IsNullOrEmpty(test))
            {
                AuthorizationService.SetAuthSession(int.Parse(Flag_MockUserId));
                return true;
            }
            log.Info("sessionID:" + Session.SessionID);
        
            var wechat = new WeChatOAuth();
            var empCode = wechat.TryGetOpenIdFromOAuthState(CorpID, Session.SessionID);
            log.Info("empCode=" + empCode);
            if (!string.IsNullOrEmpty(empCode))
            {
                var empBL = BLLFactory.Create<IBaseEmployeeBL>();
                var emp = empBL.GetEmployeeByCode(empCode);
                if(emp == null) {
                  return false;
                }
                var userBL = BLLFactory.Create<IBaseUserBL>();
                BaseUser user = null;
                if (!string.IsNullOrEmpty(Flag_MockUserId))
                {
                    user = userBL.GetUser(int.Parse(Flag_MockUserId));
                }
                else
                {
                    user = userBL.GetUser(emp.ID);
                }
                AuthorizationService.SetAuthSession((int)user.ID);
                filterContext.Result = Redirect(Request.Url.ToString());
                return true;
            }

            var url = wechat.GetOAuthRedirectUrl(Session.SessionID, Request.Url.ToString(), Host, CorpID);
            filterContext.Result = Redirect(url);
            return true;
        }
    }

    protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext) {
      base.OnActionExecuting(filterContext);
      ViewBag.IsPND = true;
      //if ( IsWapOfflineTime ) {
      //  if ( filterContext.ActionDescriptor.GetCustomAttributes(typeof(NeverOffline), true).Length > 0 ) {
      //    return;
      //  }
      //  filterContext.Result = RedirectToAction("Index", "Offline");
      //}
    }

  }
}
