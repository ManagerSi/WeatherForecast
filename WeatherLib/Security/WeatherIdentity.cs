using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WeatherLib.Web.SessionState;
using WeatherLib.Model;
using WeatherLib.Utility;
namespace WeatherLib.Security {
  public class WeatherIdentity : System.Security.Principal.IIdentity {
    public decimal UserID {get; set;}
    protected IBLSessionPersisiter session;
    protected IClientSessionPersisiter clientSession;
    //负责产品
    public decimal? ProdType
    {
      get { return session.ProdType; }
      
    }

    public WeatherIdentity(decimal userID) {
      this.UserID = userID;
      session = SessionFactory.Create<IBLSessionPersisiter>();
      clientSession = SessionFactory.Create<IClientSessionPersisiter>();
    }

    public decimal Base { get; set; }
    public BaseEmployee BaseEmployee {
      get {
        return session.BaseEmployee;
      }
    }
    public ICollection<string> Roles {

      get {
        return session.Roles;
      }
    }
    public ICollection<decimal> DataOrgAuth {
      get {
        return session.DataOrgAuth;
      }
    }

    public ICollection<decimal> DataCenterCityAuth {
      get {
        return session.DataCenterCityAuth;
      }
    }
    public static WeatherIdentity Current {
      get {
        if(HttpContext.Current == null) {
          return null;
        }
        return HttpContext.Current.User.Identity as WeatherIdentity;
      }
    }

    public WeatherClientType ClientType {
      get { return clientSession.ClientType; }
    }

    public string ClientVersion {
      get { return clientSession.ClientVersion; }
    }
    public string ClientImgPrefix {
      get { return clientSession.ClientImgPrefix; }
    }

    #region IIdentity 成员

    public string AuthenticationType {
      get { return "TSIC"; }
    }

    public bool IsAuthenticated {
      get { return UserID > 0; }
    }

    public string Name {
      get { return "Test user"; }
    }

    #endregion
  }
}
