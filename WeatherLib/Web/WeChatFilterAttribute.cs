using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

 namespace Mobizone.TSIC.Web {
  public class WeChatInternalFilterAttribute :ActionFilterAttribute{
    public override void OnActionExecuting(ActionExecutingContext filterContext) {
      if (!IsWeChat(filterContext.HttpContext.Request)) {
        filterContext.Result = new ContentResult() {
          Content = "请使用微信客户端访问。"
        };
      }
    }

    public static IList<string> wechatAgnet = new[] {"MicroMessenger", "Windows Phone"};

    public static bool IsWeChat(HttpRequestBase request) {
      return IsWeChat(request.UserAgent);
    }


    public static bool IsWeChat(string userAgent) {
      return !string.IsNullOrEmpty(userAgent) && wechatAgnet.Any(userAgent.Contains);
    }
  }
}