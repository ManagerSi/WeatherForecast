using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeatherLib.Controllers;
namespace WeatherLib.Security {
  /// <summary>
  /// 授权
  /// </summary>
  public class WeatherAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute {
    public new string[] Roles {
      get { return base.Roles.Split(','); }
      set { base.Roles = string.Join(",", value); }
    }
    public new string[] Users {
      get { return base.Users.Split(','); }
      set { base.Users = string.Join(",", value); }
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
      var controller = filterContext.Controller as BaseController;
      if(controller == null || !controller.HandleUnauthorizedRequest(filterContext)) {
        base.HandleUnauthorizedRequest(filterContext);
      }
    }
  }
  /// <summary>
  /// 授权Deny
  /// </summary>
  public class WeatherForecastAuthorizeDenyAttribute : WeatherAuthorizeAttribute {
    protected override bool AuthorizeCore(HttpContextBase httpContext) {
      return !base.AuthorizeCore(httpContext);
    }
  }
}
