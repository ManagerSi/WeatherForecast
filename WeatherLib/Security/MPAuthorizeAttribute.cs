using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Mobizone.TSIC.Controllers;

namespace Mobizone.TSIC.Security {
  public class MPAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute {
    public new string[] Roles {
      get { return base.Roles.Split(','); }
      set { base.Roles = string.Join(",", value); }
    }
    public new string[] Users {
      get { return base.Users.Split(','); }
      set { base.Users = string.Join(",", value); }
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
      var controller = filterContext.Controller as MPBaseController;
      if (controller == null || !controller.HandleUnauthorizedRequest(filterContext)) {
        base.HandleUnauthorizedRequest(filterContext);
      }
    }
  }
}
