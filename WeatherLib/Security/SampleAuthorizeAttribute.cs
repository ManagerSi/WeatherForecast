using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeatherLib.Controllers;
namespace WeatherLib.Security {
  /// <summary>
  /// 授权
  /// </summary>
  public class SampleAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute {
    public new string[] Roles {
      get { return base.Roles.Split(',').Where(i=>!string.IsNullOrEmpty(i)).ToArray(); }
      set { base.Roles = string.Join(",", value); }
    }
    public new string[] Users {
      get { return base.Users.Split(',').Where(i => !string.IsNullOrEmpty(i)).ToArray(); }
      set { base.Users = string.Join(",", value); }
    }
    protected override bool AuthorizeCore(HttpContextBase httpContext) {
      if(httpContext == null) {
        throw new ArgumentNullException("httpContext");
      }
      IPrincipal user = httpContext.User;
      return user.Identity.IsAuthenticated && (this.Users.Length <= 0 || this.Users.Contains(user.Identity.Name,StringComparer.OrdinalIgnoreCase)) && (this.Roles.Length <= 0 || this.Roles.Any(new Func<string,bool>(user.IsInRole)));
    }
    private void CacheValidateHandler(HttpContext context,object data,ref HttpValidationStatus validationStatus) {
      validationStatus = this.OnCacheAuthorization(new HttpContextWrapper(context));
    }
    /// <summary>Called when a process requests authorization.</summary>
    /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />.</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="filterContext" /> parameter is null.</exception>
    public override void OnAuthorization(AuthorizationContext filterContext) {
      if(filterContext == null) {
        throw new ArgumentNullException("filterContext");
      }
      if(OutputCacheAttribute.IsChildActionCacheActive(filterContext)) {
        //throw new InvalidOperationException(MvcResources.AuthorizeAttribute_CannotUseWithinChildActionCache);
      }
      ActionDescriptor arg_34_0 = filterContext.ActionDescriptor;
      bool inherit = true;
      bool arg_5B_0;
      if(!arg_34_0.IsDefined(typeof(AllowAnonymousAttribute),inherit)) {
        ControllerDescriptor arg_53_0 = filterContext.ActionDescriptor.ControllerDescriptor;
        bool inherit2 = true;
        arg_5B_0 = arg_53_0.IsDefined(typeof(AllowAnonymousAttribute),inherit2);
      } else {
        arg_5B_0 = true;
      }
      bool flag = arg_5B_0;
      if(flag) {
        return;
      }
      if(this.AuthorizeCore(filterContext.HttpContext)) {
        HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
        cache.SetProxyMaxAge(new TimeSpan(0L));
        cache.AddValidationCallback(new HttpCacheValidateHandler(this.CacheValidateHandler),null);
        return;
      }
      this.HandleUnauthorizedRequest(filterContext);
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
  public class SampleAuthorizeDenyAttribute : SampleAuthorizeAttribute {
    protected override bool AuthorizeCore(HttpContextBase httpContext) {
      return !base.AuthorizeCore(httpContext);
    }
  }
}
