using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherLib.Security;
namespace WeatherLib.Security {
  public class SamplePrincipal : System.Security.Principal.IPrincipal {
    protected SampleIdentity identity;
    //private WeatherAuthorizationService AuthorizationService;
    //public TSICPrincipal(TSICIdentity identity) {
    //  this.identity = identity;
    //}

    public SamplePrincipal(SampleIdentity identity) {
      // TODO: Complete member initialization
      this.identity = identity;
    }
    #region IPrincipal 成员

    public System.Security.Principal.IIdentity Identity {
      get { return this.identity; }
    }

    public bool IsInRole(string role) {
      return identity.Roles.Contains(role);
    }

    #endregion
  }
}
