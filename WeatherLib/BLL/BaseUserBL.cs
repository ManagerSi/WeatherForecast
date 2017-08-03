using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLib.Model;
using WeatherLib.Security;
using WeatherLib.Utility;

namespace WeatherLib.BLL {

  public partial interface IBaseUserBL {
    BaseUser GetUser(string userAccount,string userPassword);
    BaseUser GetUserByType(string userAccount,string userPassword,string userType);
    BaseUser GetUserInWap(string userAccount,string userPassword);
    BaseUser GetUserInWeb(string userAccount,string userPassword);
    bool ExistUser(string userAccount);
    ICollection<string> GetUserRoles(decimal userID);
    BaseUser GetPasswordByUserAccountAndMobilePhone(string userAccount,string empName,string mobilePhone);
    //IQueryable<BaseUser> QueryBaseUser(SettingQueryParams queryParams);
    IQueryable<BaseUser> GetAllBaseUser();
    BaseUser GetUser(decimal empID);
    BaseUser GetUserByID(decimal userID);

    BaseUser GetUserByEmpID(decimal empID);

    #region session
    bool SetAuthSession(string username,string password,WeatherClientType clientType,string[] loginOnlyIfRole = null,string[] extrRoles = null);
    bool SetAuthSession(int userID,WeatherClientType clientType = WeatherClientType.Wap,string[] extrRoles = null);

    #endregion 
  }

  public partial class BaseUserBL {
    public BaseUser GetUser(decimal empID) {
      var user = Repository
        .Get(u => u.Emp_ID == empID && u.State == "1")
        .FirstOrDefault();
      return user;
    }

    public BaseUser GetUserByID(decimal userID) {
      var user = Repository
        .Get(u => u.ID == userID && u.State == "1")
        .FirstOrDefault();
      return user;

    }
    public BaseUser GetUser(string userAccount,string userPassword) {
      var user = Repository
        .Get(u => u.UserAccount.ToLower() == userAccount.ToLower() && u.UserPWD == userPassword && u.State == "1")
        .FirstOrDefault();
      return user;
    }

    public BaseUser GetUserByType(string userAccount,string userPassword,string userType) {
      var user = Repository
        .Get(u => u.UserAccount.ToLower() == userAccount.ToLower() && u.UserPWD == userPassword && u.State == "1")
        // one passport 只有一个账户
        .FirstOrDefault();
      /*
      if (userAccount.StartsWith("a1")) {
        user = Repository
        .Get(u => u.UserAccount.ToLower() == userAccount.ToLower() && u.UserPassword == userPassword && u.State == "1" )
        .FirstOrDefault(u => u.UserType == WeatherConstant.WebAccount);
      }*/

      return VailedUserType(user,userType) ? user : null;
    }

    /// <summary>
    /// 确认该用户是否为指定类型
    /// </summary>

    private bool VailedUserType(BaseUser user,string userType) {
      if(user == null || !user.Emp_ID.HasValue) {
        return false;
      }
      /*var empID = user.EmpID.Value;
      var empBL = BLLFactory.Create<IBaseEmployeeBL>(UnitOfWork);
      var dictBL = BLLFactory.Create<IBaseDictBL>(UnitOfWork);

      var emp = empBL.GetEmployeeByCached(empID);
      var empType = dictBL.GetItemNameByCached(BaseDictType.DictTypeEmpType, emp.EmpTypeID);
      if (empType == BaseDictType.DictEmpTypePG) {
        // pg 只能是wap账号
        return userType == WeatherConstant.WapAccount;
      } else {
        // 其他人可是同时是wap和web
        return true;
      }*/
      return true;

    }

    public BaseUser GetUserInWap(string userAccount,string userPassword) {
      return GetUserByType(userAccount,userPassword,WeatherClientType.Wap.ToString());
    }
    public BaseUser GetUserInWeb(string userAccount,string userPassword) {
      return GetUserByType(userAccount,userPassword,WeatherClientType.Web.ToString());
    }
    public bool ExistUser(string userAccount) {
      return Repository.Get(u => u.UserAccount == userAccount && u.State == "1").Count() > 0;
    }
    public ICollection<string> GetUserRoles(decimal userID) {
      //var user = Repository.GetByID(userID);
      //var roles = user.BaseUserRoles.Select(u => u.BaseRole.RoleName);
      var roles = UnitOfWork.BASE_USER_ROLERepository.Get(i => i.USER_ID == userID).Select(i => i.BASE_ROLE.ROLE_NAME);
      return new HashSet<string>(roles);
    }
    public BaseUser GetPasswordByUserAccountAndMobilePhone(string userAccount,string empName,string mobilePhone) {
      //参照用户登录，使用用户账号和手机号取得用户信息
      var user = Repository
        .Get(u => u.UserAccount == userAccount && u.BASE_EMPLOYEE.Mobile == mobilePhone && u.BASE_EMPLOYEE.Name == empName)
        .FirstOrDefault(u=>u.State == "1");
      return user;
    }
    //public IQueryable<BaseUser> QueryBaseUser(SettingQueryParams queryParams) {
    //  return Repository.QueryBaseUser(queryParams);
    //}

    public IQueryable<BaseUser> GetAllBaseUser() {
      return Repository.Get(item => item.State == "1");//.AsNoTracking();
    }

    public BaseUser GetUserByEmpID(decimal empID) {
      return Repository.Get(i => i.State == "1" && i.Emp_ID == empID).FirstOrDefault();
    }

    #region configRole

    private ICollection<string> ExpendRole(ICollection<string> roles) {
      var rst = new HashSet<string>();
      rst.AddAll(roles);

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
      //if(rst.Contains(WeatherRole.RoleAdmin)) {
      //  rst.Add(WeatherRole.PGM_HC);
      //  rst.Add(WeatherRole.KPIOrgViewer);
      //  rst.Add(WeatherRole.KPIHCTopListViewer);
      //  rst.Add(WeatherRole.KPIAreaListViewer);
      //  rst.Add(WeatherRole.CoachViewer);
      //  rst.Add(WeatherRole.KPIDataSetting);
      //}

      //2014-10-28 Rule:负责产品
      //var EmpProd = dictBL.GetItemNameByCached(BaseDictType.DictTypeEmpType, session.BaseEmployee.EmpTypeID);

      // 添加PrefixRole
      //foreach(var r in rst.ToList()) {
      //  // built-in Role: PGM-
      //  if(r.StartsWith(WeatherRole.PGMTypePrefix)) {
      //    rst.Add(WeatherRole.PGMTypePrefix);
      //  } else if(r.StartsWith(WeatherRole.CoachTypePrefix)) { // built-in Role: Coach-
      //    rst.Add(WeatherRole.CoachTypePrefix);
      //  } else if(r.StartsWith(WeatherRole.KPITypePrefix)) { // built-in Role: KPI-
      //    rst.Add(WeatherRole.KPITypePrefix);
      //  }
      //}
      return rst;
    }

    private void ConfigAuthSession(BaseUser user,WeatherClientType clientType) {
      // clean all existing Cache
      //Mobizone.TSIC.Cache.DataCache.RemoveAllCacheBySession();
      var session = new SessionHelper();
      session.RemoveSession();

      session.UserID = user.ID;
      session.BaseEmployee = user.BASE_EMPLOYEE;
      session.UserName = session.BaseEmployee.Name;
      session.ProdType = 1; //登录成功默认PND

      //var division = BLLFactory.Create<IBaseDictBL>().GetItemNameByCached(BaseDictType.DictTypeDivision,user.BaseEmployee.DIVISION);
      //if(division == BaseDictType.DivisionTypeMND) {
      //  session.ProdType = 2;
      //}
      
      //session.UserType = user.UserType;

      session.Roles = ExpendRole(GetUserRoles(user.ID)); // load all the role

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
      //var bl = BLLFactory.Create<IBaseUserBL>();
      var user = GetUserInWeb(username,password);
      if(null == user) {
        return false;
      }
      ConfigAuthSession(user,clientType);

      var session = new SessionHelper();
      if(loginOnlyIfRole != null) {
        var identity = new WeatherIdentity(session.UserID);
        if(loginOnlyIfRole.All(role => !identity.Roles.Contains(role))) {
          session.RemoveSession();
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

      if(extrRoles != null) {
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
      if(null == user) {
        return false;
      }
      ConfigAuthSession(user,clientType);
      if(extrRoles != null) {
        var session = new SessionHelper();
        session.Roles.AddAll(extrRoles);
      }

      return true;
    }

    #endregion

  }
}
