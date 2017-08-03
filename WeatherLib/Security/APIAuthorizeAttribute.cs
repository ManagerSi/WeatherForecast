using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Mobizone.TSIC.Web;
namespace Mobizone.TSIC.Security {
  /// <summary>
  /// 授权
  /// </summary>
  public class APIAuthorizeAttribute : BaseAPIAuthorizationFilter {
    public new string[] Roles {
      get { return base.Roles.Split(','); }
      set { base.Roles = string.Join(",", value); }
    }
    public new string[] Users {
      get { return base.Users.Split(','); }
      set { base.Users = string.Join(",", value); }
    }
  }
  /// <summary>
  /// 授权Deny
  /// </summary>
  public class APIAuthorizeDenyAttribute : BaseAPIAuthorizationFilter {

    protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext) {
      return !base.IsAuthorized(actionContext);
    }
  }
}
