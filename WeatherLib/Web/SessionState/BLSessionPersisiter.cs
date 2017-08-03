using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WeatherLib.Model;
using WeatherLib.BLL;

namespace WeatherLib.Web.SessionState {
  public interface IBLSessionPersisiter : ISessionPersisiter {
    decimal UserID { get; set; }
    //string UserType { get; set; }
    BaseEmployee BaseEmployee { get; set; }

    decimal? ProdType { get; set; }
    ICollection<decimal> DataOrgAuth { get; set; }
    ICollection<decimal> DataCenterCityAuth { get; set; } // 
    ICollection<string> Roles { get; set; }

  }

  public class BLSessionPersisiter : SessionPersisiter, IBLSessionPersisiter {
    protected const string sessionUserIDKey = "_s_userid";
    protected const string sessionRolesKey = "_s_roles";
    protected const string sessionBaseEmployeeKey = "_s_baseemp";
    protected const string sessionDataOrgAuthKey = "_s_dataorgauth";
    protected const string sessionDataCenterCityAuthKey = "_s_datacentercityauth";
    protected const string sessionProdTypeKey = "_s_prodtype";
    // remove by one passport
    //protected const string sessionUserTypeKey = "_s_usertype";
    public decimal UserID {
      get {
        return GetValue<decimal>(sessionUserIDKey, -1);
      }
      set {
        SetValue(sessionUserIDKey, value);
      }
    }

    //2014-12-04 负责产品
    public decimal? ProdType
    {
      get
      {
        return GetValue<decimal>(sessionProdTypeKey, -1);
      }
      set
      {
        SetValue(sessionProdTypeKey, value);
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
        var id = GetValue<decimal>(sessionBaseEmployeeKey, -1);
        if (id < 0) {
          return null;
        }
        return BLLFactory.Create<IBaseEmployeeBL>().GetByID((int)id);
      }
      set {
        SetValue(sessionBaseEmployeeKey, value.ID);
      }
    }

    public ICollection<string> Roles {
      get {
        return GetValue<ICollection<string>>(sessionRolesKey, null);
      }
      set {
        SetValue(sessionRolesKey, value);
      }
    }
    public ICollection<decimal> DataOrgAuth {
      get {
        return GetValue<ICollection<decimal>>(sessionDataOrgAuthKey, null);
      }
      set {
        SetValue(sessionDataOrgAuthKey, value);
      }
    }
    public ICollection<decimal> DataCenterCityAuth {
      get {
        return GetValue<ICollection<decimal>>(sessionDataCenterCityAuthKey, null);
      }
      set {
        SetValue(sessionDataCenterCityAuthKey, value);
      }
    }

    public override bool RemoveSession() {
      if (HttpContext.Current == null) {
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
  }
}
