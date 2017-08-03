using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherLib.Security;
namespace WeatherLib.Security {
  public class WeatherPrincipal : System.Security.Principal.IPrincipal {
    protected WeatherIdentity identity;
    private WeatherAuthorizationService AuthorizationService;
    //public TSICPrincipal(TSICIdentity identity) {
    //  this.identity = identity;
    //}

    public WeatherPrincipal(WeatherIdentity identity,WeatherAuthorizationService AuthorizationService) {
      // TODO: Complete member initialization
      this.identity = identity;
      this.AuthorizationService = AuthorizationService;
    }
    #region IPrincipal 成员

    public System.Security.Principal.IIdentity Identity {
      get { return this.identity; }
    }

    public bool IsInRole(string role) {
      return AuthorizationService.UserInRole(identity,role);
      //return true;
    }

    #endregion
  }
}
