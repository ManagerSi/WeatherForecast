using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WeatherLib.BLL;
using WeatherLib.Model;

namespace WeatherLib.Security {
  public class SessionHelper {
    protected const string sessionUserIDKey = "_s_userid";
    protected const string sessionUserNameKey = "_s_username";
    protected const string sessionRolesKey = "_s_roles";
    protected const string sessionBaseEmployeeKey = "_s_baseemp";
    protected const string sessionDataOrgAuthKey = "_s_dataorgauth";
    protected const string sessionDataCenterCityAuthKey = "_s_datacentercityauth";
    protected const string sessionProdTypeKey = "_s_prodtype";
    // remove by one passport
    //protected const string sessionUserTypeKey = "_s_usertype";
    public decimal UserID {
      get {
        return GetValue<decimal>(sessionUserIDKey,-1);
      }
      set {
        SetValue(sessionUserIDKey,value);
      }
    }
    public string UserName {
      get {
        return GetValue<string>(sessionUserNameKey,"");
      }
      set {
        SetValue(sessionUserNameKey,value);
      }
    }

    //2014-12-04 负责产品
    public decimal? ProdType {
      get {
        return GetValue<decimal>(sessionProdTypeKey,-1);
      }
      set {
        SetValue(sessionProdTypeKey,value);
      }
    }

    //public string UserType {
    //  get {
    //    return GetValue<string>(sessionUserTypeKey, null);
    //  }
    //  set {
    //    SetValue(sessionUserTypeKey, value);
    //  }
    //}

    public BaseEmployee BaseEmployee {
      get {
        var id = GetValue<decimal>(sessionBaseEmployeeKey,-1);
        if(id < 0) {
          return null;
        }
        return BLLFactory.Create<IBaseEmployeeBL>().GetByID((int)id);
      }
      set {
        SetValue(sessionBaseEmployeeKey,value.ID);
      }
    }

    public ICollection<string> Roles {
      get {
        return GetValue<ICollection<string>>(sessionRolesKey,null);
      }
      set {
        SetValue(sessionRolesKey,value);
      }
    }
    public ICollection<decimal> DataOrgAuth {
      get {
        return GetValue<ICollection<decimal>>(sessionDataOrgAuthKey,null);
      }
      set {
        SetValue(sessionDataOrgAuthKey,value);
      }
    }
    public ICollection<decimal> DataCenterCityAuth {
      get {
        return GetValue<ICollection<decimal>>(sessionDataCenterCityAuthKey,null);
      }
      set {
        SetValue(sessionDataCenterCityAuthKey,value);
      }
    }

    public bool RemoveSession() {
      if(HttpContext.Current == null) {
        throw new InvalidOperationException();
      }
      var session = HttpContext.Current.Session;

      session.Remove(sessionUserIDKey);
      session.Remove(sessionBaseEmployeeKey);
      session.Remove(sessionRolesKey);
      session.Remove(sessionDataOrgAuthKey);
      session.Remove(sessionDataCenterCityAuthKey);
      session.Remove(sessionProdTypeKey);
      return true;
    }

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
    
  }
}
