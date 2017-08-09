using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.Mvc;
//using Mobizone.TSIC.Models.Documents;
//using Mobizone.TSIC.Web.SessionState;
//using Mobizone.TSIC.Models;
using WeatherLib.BLL;
using WeatherLib.Web.SessionState;
using WeatherLib.Utility;
using WeatherLib.Model;
//using Mobizone.TSIC.Utility;
namespace WeatherLib.Security {
  public class WeatherAuthorizationService {
    public bool UserInRole(WeatherIdentity identity,string role) {
      if (null == role) {
        return false;
      }
      if (null == identity.Roles) {
        return false;
      }
      // built-in Role: UserType 
      // one passport
      if(role == WeatherLib.Security.WeatherRole.BetaUser) {
        return true;
      }

      var session = SessionFactory.Create<IBLSessionPersisiter>();

      // built-in: BindStore: storeID
      //if(role.StartsWith(WeatherRole.BindStorePrefix)) {
      //  if(role == WeatherRole.BindAnyStore) {
      //    return BLLFactory.Create<IBizStoreBL>().GetBindStores(session.BaseEmployee.ID).Any() ;
      //  } else {
      //    var storeID = decimal.Parse(role.Substring(WeatherRole.BindStorePrefix.Length));
      //    return BLLFactory.Create<IBizStoreBL>().IsBindStoreByCached(session.BaseEmployee.ID, storeID);
      //  }
      //}

      // built-in: InStore: storeID
      //if (role.StartsWith(WeatherRole.InStorePrefix)) {
      //  var storeID = decimal.Parse(role.Substring(WeatherRole.InStorePrefix.Length));
      //  return BLLFactory.Create<IReportVstSummaryBL>().IsInStore(storeID, session.BaseEmployee.ID, Util.RPCNow.ToDateString());
      //}

      //// built-in Role: BetaUser
      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.BetaUser) {
      //  return Configuration.IsDevMode ||
      //    (null != session.BaseEmployee && WeatherRole.BetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}

      //// built-in Role: PGBetaUser
      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.PGBetaUser) {
      //  return Configuration.IsDevMode ||
      //    (null != session.BaseEmployee && WeatherRole.PGBetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}

      //// built-in Role: PGMBetaUser
      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.PGMBetaUser) {
      //  return Configuration.IsDevMode ||
      //    (null != session.BaseEmployee && WeatherRole.PGMBetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}

      //// built-in Role: KPIBetaUser
      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.KPIBetaUser) {
      //  return Configuration.IsDevMode ||
      //    (null != session.BaseEmployee && WeatherRole.KPIBetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}
      //// built-in Role: PGOBetaUser
      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.JasonBetaUser) {
      //  return Configuration.IsDevMode ||
      //    (null != session.BaseEmployee && WeatherRole.JasonBetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}

      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.KPISellInOutBetaUser)
      //{
      //    return (null != session.BaseEmployee && WeatherRole.KPISellInOutBetaUserEmpCode.Contains(session.BaseEmployee.EmpCode) );
      //}

      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.KPIKABetaUser)
      //{
      //    return (null != session.BaseEmployee && WeatherRole.KPIKABetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}

      ////VIP 
      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.VIPBetaUser) {
      //  return Configuration.IsDevMode ||
      //    (null != session.BaseEmployee && WeatherRole.VIPBetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}

      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.ReportBetaUser)
      //{
      //    return Configuration.IsDevMode ||
      //      (null != session.BaseEmployee && WeatherRole.ReportBetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}

      ////FOC 
      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.FOCSettingUser) {
      //  return Configuration.IsDevMode ||
      //    (null != session.BaseEmployee && WeatherRole.FOCBetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}

      ////CRM Customer
      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.CRMCustomerBetaUser) {
      //  return Configuration.IsDevMode ||
      //    (null != session.BaseEmployee && WeatherRole.CRMCustomerEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}
      ////PGM Customer Report Beta User
      //if (Configuration.Flag_EnableBetaUser && role == WeatherRole.PGMCustomerBetaUser) {
      //  return Configuration.IsDevMode ||
      //    (null != session.BaseEmployee && WeatherRole.PGMCustomerBetaUserEmpCode.Contains(session.BaseEmployee.EmpCode));
      //}

      //// built-in: 532数据上报
      //if (role.StartsWith(WeatherRole.Role532DSR))
      //{
      //    return BLLFactory.Create<IBiz532EmpStoreBL>().GetStoreByEmpID(session.BaseEmployee.ID) != null; 
      //}

      //// built-in: 532客户数据上报
      //if(role.StartsWith(WeatherRole.RoleCityManagment)) {
      //  return BLLFactory.Create<IBiz532EmpAccountBL>().GetEmpAccountByEmp(session.BaseEmployee.ID).Count != 0;
      //}

      //// built-in: TBBS客户数据上报
      //if (role.StartsWith(WeatherRole.RoleTBBSAccount))
      //{
      //    return BLLFactory.Create<IBizTBBSEmpAccountBL>().GetEmpAccountByEmp(session.BaseEmployee.ID).Count != 0;
      //}

      return identity.Roles.Contains(role);
    }

    private static ICollection<string> ExpendRole(ICollection<string> roles) {
      var rst = new HashSet<string>();
      rst.AddAll(roles);

      var session = SessionFactory.Create<IBLSessionPersisiter>();

      //// built-in Role: PGM-PGS
      //var dictBL = BLLFactory.Create<IBaseDictBL>();
      //var empBL = BLLFactory.Create<IBaseEmployeeBL>();
      //var dutyName = dictBL.GetItemNameByCached(BaseDictType.DictTypeDuty, session.BaseEmployee.DutyID);
      //if (dutyName == BaseDictType.DictDutyPGS || empBL.IsParentOfAnyPG(session.BaseEmployee.ID)) {
      //  rst.Add(WeatherRole.PGM_PGS);
      //  // built-in Role: PGM_PurePGS
      //  if (rst.Count(i => i.StartsWith(WeatherRole.PGMTypePrefix)) == 1) {
      //    rst.Add(WeatherRole.PGM_PurePGS);
      //  }
      //}

      //// built-in Role: EmpType:xxx
      //var empTypeName = dictBL.GetItemNameByCached(BaseDictType.DictTypeEmpType, session.BaseEmployee.EmpTypeID);
      //string curEmpTypeRole = WeatherRole.WapTypeDateQuery;
      //if (empTypeName == BaseDictType.DictEmpTypeDSR) {
      //  curEmpTypeRole = WeatherRole.WapTypeDSR;
      //} else if (empTypeName == BaseDictType.DictEmpTypePG) {
      //  curEmpTypeRole = WeatherRole.WapTypePG;
      //}
      //rst.Add(curEmpTypeRole);

      //// built-in Role: Coach
      //if (empTypeName == BaseDictType.DictEmpTypeDSR) {
      //  rst.Add(WeatherRole.CoachDSR);
      //}
      ////是否有下属
      //if (empBL.IsParentByCached(session.BaseEmployee.ID))
      //{
      //  rst.Add(WeatherRole.ParentEmp);
      //}

      //if (empBL.IsParentOfAnyDSR(session.BaseEmployee.ID)) {
      //  rst.Add(WeatherRole.CoachcReviewer);
      //}

      //// built-in Role: UserType 
      //// move to expendUser by one passport
      ////var userType = session.UserType;
      ////if (!string.IsNullOrEmpty(userType)) {
      ////  if (userType == TSICConstant.WapAccount) {
      ////    rst.Add(WeatherRole.UserTypeWap);
      ////  } else if (userType == TSICConstant.WebAccount) {
      ////    rst.Add(WeatherRole.UserTypeWeb);
      ////  }
      ////}
      //if (empTypeName != BaseDictType.DictEmpTypePG) {
      //  rst.Add(WeatherRole.UserTypeWeb);
      //}


      ////built-in Role : KPIPerson
      //if (empTypeName != BaseDictType.DictEmpTypePG) {
      //  var personBL = BLLFactory.Create<IKPIPersonBL>();
      //  var kpiPerson = personBL.GetLastKPIPerson(session.BaseEmployee.ID);
      //  if (null != kpiPerson) {
      //    rst.Add(WeatherRole.KPITypePrefix);
      //    if (kpiPerson.IsInKPI ?? false) {
      //      rst.Add(WeatherRole.KPIPerson);
      //    }
      //  }
      //}


      // built-in Rule: RoleAdmin
      if (rst.Contains(WeatherRole.RoleAdmin)) {
        //rst.Add(WeatherRole.PGM_HC);
        //rst.Add(WeatherRole.KPIOrgViewer);
        //rst.Add(WeatherRole.KPIHCTopListViewer);
        //rst.Add(WeatherRole.KPIAreaListViewer);
        //rst.Add(WeatherRole.CoachViewer);
        //rst.Add(WeatherRole.KPIDataSetting);
      }

      //2014-10-28 Rule:负责产品
      //var EmpProd = dictBL.GetItemNameByCached(BaseDictType.DictTypeEmpType, session.BaseEmployee.EmpTypeID);

      // 添加PrefixRole
      foreach (var r in rst.ToList()) {
        // built-in Role: PGM-
        if (r.StartsWith(WeatherRole.PGMTypePrefix)) {
          rst.Add(WeatherRole.PGMTypePrefix);
        } else if (r.StartsWith(WeatherRole.CoachTypePrefix)) { // built-in Role: Coach-
          rst.Add(WeatherRole.CoachTypePrefix);
        } 
      }
      return rst;
    }

    private static void ConfigAuthSession(BaseUser user,WeatherClientType clientType) {
      // clean all existing Cache
      //Mobizone.TSIC.Cache.DataCache.RemoveAllCacheBySession();

      var session = SessionFactory.Create<IBLSessionPersisiter>();
      var clientSession = SessionFactory.Create<IClientSessionPersisiter>();
      session.RemoveSession();
      clientSession.RemoveSession();

      IBaseUserBL bl = BLLFactory.Create<IBaseUserBL>();
      session.UserID = user.ID;
      session.BaseEmployee = user.BASE_EMPLOYEE;
      session.ProdType = 1; //登录成功默认PND

      //var division = BLLFactory.Create<IBaseDictBL>().GetItemNameByCached(BaseDictType.DictTypeDivision,user.BaseEmployee.DIVISION);
      //if(division == BaseDictType.DivisionTypeMND) {
      //  session.ProdType = 2;
      //}

      clientSession.ClientType = clientType;

      //session.UserType = user.UserType;

      session.Roles = ExpendRole(bl.GetUserRoles(user.ID)); // load all the role

      //// Data Auth
      //var orgBL = BLLFactory.Create<IBaseOrgBL>();
      ISet<decimal> authOrg = new HashSet<decimal>();
      //var splitDate = DateTime.Parse("2012-03-07 03:00:00");
      //if(user.Updated == null || user.Updated < splitDate) {
      //  // 老式的ETMS授权
      //  authOrg = orgBL.FilterAbtCityOrgByCached(user.OrgAuth);
      //} else {
      //  authOrg = orgBL.ExpendOrgToAbtCityByCached(user.OrgAuth);
      //}
      //var userOrg = user.BaseEmployee.OrgID;
      //if(null != userOrg) {
      //  authOrg.Add(userOrg.Value);

      //  //展开到城市一级
      //  session.DataCenterCityAuth = orgBL.ExpendOrgToCenterCityByCached(userOrg.Value);
      //}

      //// Data Auth: PGS 自动授权 所有PG所在雅培城市
      //if(session.Roles.Contains(WeatherRole.PGM_PGS)) {
      //  var empBL = BLLFactory.Create<IBaseEmployeeBL>();
      //  var orgs = empBL.GetChildAbtCityBind(session.BaseEmployee.ID);
      //  authOrg.AddAll(orgs);
      //}

      session.DataOrgAuth = authOrg;
    }
    /*
    public bool SetAuthSessionInWeb(string username, string password) {
      var bl = BLLFactory.Create<IBaseUserBL>();
      var user = bl.GetUserInWeb(username, password);
      if (null == user) {
        return false;
      }
      ConfigAuthSession(user);
      return true;
    }


    public bool SetAuthSessionInWap(string username, string password) {
      var bl = BLLFactory.Create<IBaseUserBL>();
      var user = bl.GetUserInWap(username, password);
      if (null == user) {
        return false;
      }
      ConfigAuthSession(user);
      return true;
    }*/


    public bool SetAuthSession(string username,string password,WeatherClientType clientType,string[] loginOnlyIfRole = null,string[] extrRoles = null) {
      var bl = BLLFactory.Create<IBaseUserBL>();
      var user = bl.GetUserInWeb(username, password);
      if (null == user) {
        return false;
      }
      ConfigAuthSession(user, clientType);

       var session = SessionFactory.Create<IBLSessionPersisiter>();
      if (loginOnlyIfRole != null) {
        var identity = new WeatherIdentity(session.UserID);
        if (loginOnlyIfRole.All(role => !this.UserInRole(identity, role))) {
          RemoveSession(); 
          return false;
        }
      }

      //string type = UserLog.LoginClientTypeWap;
      //switch (clientType) {
      //  case WeatherClientType.Wap:
      //    type = UserLog.LoginClientTypeWap;
      //    break;
      //  case WeatherClientType.Web:
      //    type = UserLog.LoginClientTypeWeb;
      //    break;
      //  case WeatherClientType.iPhone:
      //    type = UserLog.LoginClientTypeiPhone;
      //    break;
      //  case WeatherClientType.Android:
      //    type = UserLog.LoginClientTypeAndroid;
      //    break;
      //}

      if (extrRoles != null) {
        session.Roles.AddAll(extrRoles);
      }

      //Bus.ServiceBus.Publish(new Bus.Messages.LoginMsg() {
      //  EmpID = (int)session.BaseEmployee.ID,
      //  Time = Util.RPCNow,
      //  ClientType = type,
      //});

      return true;
    }

    public bool SetAuthSession(int userID,WeatherClientType clientType = WeatherClientType.Wap,string[] extrRoles = null) {
      var bl = BLLFactory.Create<IBaseUserBL>();
      var user = bl.GetUserByID(userID);
      if (null == user) {
        return false;
      }
      ConfigAuthSession(user, clientType);
      if (extrRoles != null) {
        var session = SessionFactory.Create<IBLSessionPersisiter>();
        session.Roles.AddAll(extrRoles);
      }

      return true;
    }

    public BaseUser ValidUser(string username, string password, string userType) {
      var bl = BLLFactory.Create<IBaseUserBL>();
      return bl.GetUserByType(username, password, userType);
    }

    public void PasswordChange(BaseUser model, string newPassword) {
      var bl = BLLFactory.Create<IBaseUserBL>();
      model.UserPWD = newPassword;
      bl.Update(model);
    }

    public ActionResult RedirectUnAuthorizationAccess() {
      return new RedirectResult("");
    }

    public BaseUser GetPasswordByUserAccountAndMobilePhone(string username, string empName, string mobilephone) {
      var bl = BLLFactory.Create<IBaseUserBL>();
      return bl.GetPasswordByUserAccountAndMobilePhone(username, empName, mobilephone);
    }

    public bool RemoveSession() {
      var session = SessionFactory.Create<IBLSessionPersisiter>();
      session.RemoveSession();
      var clientSession = SessionFactory.Create<IClientSessionPersisiter>();
      clientSession.RemoveSession();
      return true;
    }
    public string GetNewPassword(bool IsWap) {
      string RandomCode = "";
      //if (IsWap) {
        Random rand = new Random(Util.RPCNow.Millisecond);
        RandomCode = rand.Next(10000000, 99999999).ToString();
        /*} else {
          string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,M,N,P,Q,R,S,T,U,W,X,Y,Z";
          string[] allCharArray = allChar.Split(',');
          int temp = -1;

          Random rand = new Random();
          for (int i = 0; i < 8; i++) {
            if (temp != -1) {
              rand = new Random(temp * i * ((int)Util.RPCNow.Ticks));
            }

            int t = rand.Next(33);

            while (temp == t) {
              t = rand.Next(33);
            }

            temp = t;
            RandomCode += allCharArray[t];
          }
        }
        */
        return RandomCode;
    }
  }
}
