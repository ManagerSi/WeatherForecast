using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WeatherLib.Web.SessionState;
using WeatherLib.Model;
using WeatherLib.Utility;
namespace WeatherLib.Security {
  public class SampleIdentity : System.Security.Principal.IIdentity {

    protected const string sessionUserIDKey = "s_s_userid";
    protected const string sessionRolesKey = "s_s_roles";
    protected const string sessionBaseEmployeeKey = "s_s_baseemp";
    protected const string sessionDataOrgAuthKey = "s_s_dataorgauth";
    protected const string sessionDataCenterCityAuthKey = "s_s_datacentercityauth";
    protected const string sessionProdTypeKey = "s_s_prodtype";
    SessionHelper session = new SessionHelper();
    public decimal UserID {get; set;}
    //负责产品
    public decimal? ProdType
    {
      get { return session.ProdType; }
      
    }

    public SampleIdentity(decimal userID) {
      this.UserID = userID;
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

    //public WeatherClientType ClientType {
    //  get { return clientSession.ClientType; }
    //}

    //public string ClientVersion {
    //  get { return clientSession.ClientVersion; }
    //}
    //public string ClientImgPrefix {
    //  get { return clientSession.ClientImgPrefix; }
    //}

    #region IIdentity 成员

    public string AuthenticationType {
      get { return "TSIC"; }
    }

    public bool IsAuthenticated {
      get { return UserID > 0; }
    }

    public string Name { //对应 Authorize(Roles="管理员",Users="123") 中的users
      get { return session.UserName; }
    }

    #endregion
    #region unitl
    protected T GetValue<T>(string key,T defaultValue) {
      if(HttpContext.Current == null) {
        return defaultValue;
      }
      if(HttpContext.Current.Session == null) {
        return defaultValue;
      }
      object obj = HttpContext.Current.Session[key];
      if(null == obj) {
        return defaultValue;
      }
      return (T)obj;
    }

    protected void SetValue<T>(string key,T value) {
      if(HttpContext.Current == null) {
        throw new InvalidOperationException();
      }
      HttpContext.Current.Session.Add(key,value);
    }
    #endregion

  }
}
