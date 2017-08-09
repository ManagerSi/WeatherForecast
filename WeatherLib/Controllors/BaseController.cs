using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
//using Mobizone.TSIC.Security;
//using Mobizone.TSIC.Web.SessionState;
using WeatherLib.Utility;
using log4net;
using WeatherLib.Model;
using WeatherLib.Security;
using WeatherLib.Web.SessionState;
namespace WeatherLib.Controllers {
  public class BaseController : System.Web.Mvc.Controller {
    private static readonly ILog log = LogManager.GetLogger(typeof(BaseController));
    protected WeatherAuthorizationService AuthorizationService { get; set; }
    
    public static bool IsWapOfflineTime {
      get {
        if (!true) {
          return false;
        }
        //离线逻辑 从 0:00到5:00离线
        //var now = Util.RPCNow;
        //var offstart = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings[BaseDictType.OffStartTime].ToString());//Mobizone.TSIC.Utility.Util.RPCNow.Date
        //var offend = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings[BaseDictType.OffEndTime].ToString());
        ////var offend = Mobizone.TSIC.Utility.Util.RPCNow.Date.AddHours(int.Parse(offendTime));
        //var offline = (now >= offstart || now <= offend);
        //return offline;
        return true;
      }
    }
    //权限验证失败后跳转的页面
    public bool HandleUnauthorizedRequest(AuthorizationContext filterContext) {
      //每次进入系统，自动跳到天气页面
      filterContext.Result = RedirectToRoute("Default",new { action = "Login",controller = "Account" }); 
      return false;
    }

    protected static int count = 0;
    protected static object locker = new object();
    protected override void OnActionExecuted(ActionExecutedContext filterContext) {
      if(true) {
        int local_count;
        lock(locker) {
          count--;
          local_count = count;
        };
        log.Info("executed uri={" + filterContext.RequestContext.HttpContext.Request.RawUrl + "},count =" + local_count);
      }
      var delta = DateTime.Now - startTime;
      if (delta.TotalMilliseconds > 1000) {
        log.Fatal("slow executing =" + delta.TotalMilliseconds + ",uri=" + filterContext.RequestContext.HttpContext.Request.RawUrl);
      }
      base.OnActionExecuted(filterContext);
      HttpContext.Trace.Write("OnActionExecuted");
    }
    protected bool IsAjaxRequest() {
      if (Request == null) {
        return false;
      } else {
        return Request.IsAjaxRequest();
      }
    }
    
    protected override void OnAuthorization(AuthorizationContext filterContext) {
      //AuthorizationService = new WeatherAuthorizationService();
      //var session = SessionFactory.Create<IBLSessionPersisiter>();
      //filterContext.HttpContext.User = new WeatherPrincipal(new WeatherIdentity(session.UserID),AuthorizationService);

      var session = new SessionHelper();
      filterContext.HttpContext.User = new SamplePrincipal(new SampleIdentity(session.UserID));
      base.OnAuthorization(filterContext);
    }

    private DateTime startTime;
    protected override void OnActionExecuting(ActionExecutingContext filterContext) {
      if(true) {
        int local_count;
        lock(locker) {
          count++;
          local_count = count;
        };
        log.Info("executing uri={" + filterContext.RequestContext.HttpContext.Request.RawUrl + "},count =" + local_count);
        //log.Info("executing uri(" + DateTime.Now + ")" + count + "=" + filterContext.RequestContext.HttpContext.Request.RawUrl);
      }
      startTime = DateTime.Now;
      HttpContext.Trace.Write("Action Executing");
      ViewBag.CurrentControllerName = filterContext.RouteData.Values["controller"];
      //if (filterContext.RouteData.DataTokens["area"].ToString() == "Wap") {
      filterContext.HttpContext.Response.Buffer = true;
      filterContext.HttpContext.Response.ExpiresAbsolute = DateTime.Now;// Util.LocalNow.AddSeconds(-1);
      filterContext.HttpContext.Response.Expires = 0;
      filterContext.HttpContext.Response.CacheControl = "no-cache";
      filterContext.HttpContext.Response.AppendHeader("Pragma", "No-Cache");
      filterContext.HttpContext.Response.AppendHeader("X-Wap-Proxy-Cookie", "none");
      filterContext.HttpContext.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
      //}
      base.OnActionExecuting(filterContext);
    }
    protected override void OnException(ExceptionContext filterContext) {
      base.OnException(filterContext);
      try {
        log.Fatal(_L(filterContext.ExceptionHandled ? "Handled" : "Unhandled") +
          "RAW_URL={" + filterContext.HttpContext.Request.RawUrl + "}", filterContext.Exception);
      } catch (Exception) {
      }
    }
    protected PaginationModel<T> Pagination<T>(IEnumerable<T> list,int pageNumber,RouteValueDictionary routeDict) where T : class {
      return Pagination(list,pageNumber,routeDict,item => item);
    }
    /// <summary>
    /// 用于分页
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="O"></typeparam>
    /// <param name="list"></param>
    /// <param name="pageNumber"></param>
    /// <param name="routeDict">需要传递的参数</param>
    /// <param name="selectFunc">映射函数</param>
    /// <returns></returns>
    protected PaginationModel<O> Pagination<I,O>(
        IEnumerable<I> list,int pageNumber,
        RouteValueDictionary routeDict,Func<I,O> selectFunc)
      where I : class
      where O : class {
      IQueryable<I> qlist = list.AsQueryable();
      PaginationModel<O> pagination = new PaginationModel<O>();
      // TODO（Moore）使用一个分页控件
      //HttpContext.Trace.Write("a");
      pagination.CurrentPage = pageNumber;
      pagination.ItemCount = qlist.Count();
      //HttpContext.Trace.Write("b");
      pagination.PageCount = pagination.ItemCount / Configuration.WapPageSize +
        (pagination.ItemCount % Configuration.WapPageSize == 0 ? 0 : 1);
      pagination.SubItemList =
        qlist.Skip(pageNumber * Configuration.WapPageSize)
        .Take(Configuration.WapPageSize)
        .Select(selectFunc).ToList();
      //HttpContext.Trace.Write("b2");

      pagination.routeDict = RouteData == null ? new RouteValueDictionary() : new RouteValueDictionary(RouteData.Values);
      pagination.routeDict.Add("page",pageNumber);
      if(null != routeDict) {
        pagination.routeDict.UpdateWith(routeDict);
      }
      //HttpContext.Trace.Write("c");
      return pagination;
    }

    protected PaginationModel<T> Pagination<T>(IEnumerable<T> list,int pageNumber) where T : class {
      return Pagination(
        list,pageNumber,
        null);
    }
    protected RouteValueDictionary GetPaginationRouteDict() {
      return new RouteValueDictionary();
      //return new RouteValueDictionary(RouteData.Values);
    }
    //protected RouteValueDictionary GetPaginationRouteDict(object obj) {
    //  return obj.ToRouteDict();
    //}

    protected string RenderPartialViewToString(string viewName, object model) {
      if (string.IsNullOrEmpty(viewName))
        viewName = ControllerContext.RouteData.GetRequiredString("action");

      ViewData.Model = model;

      using (StringWriter sw = new StringWriter()) {
        ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
        if (null == viewResult.View) {
          throw new FileNotFoundException();
        }
        ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
        viewResult.View.Render(viewContext, sw);

        return sw.GetStringBuilder().ToString();
      }
    }

    //protected TSICIdentity GetTSICIdentity() {
    //  if(null != User) {
    //    return User.Identity as TSICIdentity;
    //  }
    //  // api calling
    //  var user = System.Threading.Thread.CurrentPrincipal as TSICPrincipal;
    //  if (user == null) {
    //    return null;
    //  }
    //  return user.Identity as TSICIdentity;
    //}

    protected string GetLogPrefix() {
      //var i = GetTSICIdentity();
      //string prefix = "(";
      //if (null != i.BaseEmployee) {
      //  prefix += i.BaseEmployee.ID.ToString();
      //}
      //prefix += ") - ";
      //return prefix + Request.UserHostAddress + " - ";
      return null;
    }
    protected string _L(string actionName, params string[] msgs) {
      return GetLogPrefix() + actionName + " " + string.Join(" ", msgs);
    }

    // rewrite download files
    /// <summary>
    /// fileContent 是zip File的内容
    /// 如果文件过大，会将文件写入磁盘再下载
    /// </summary>
    /// <param name="fileContent"></param>
    /// <param name="contentType"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public ActionResult ZippedFile(byte[] fileContent, string contentType, string fileName = null) {

      if (fileContent.Length < 300 * 1024) {
        return File(fileContent, contentType);
      }
      // save into temp File
      Response.ClearHeaders();
      if (fileName == null) {
        fileName = DateTime.Now + ".zip";// Util.RPCNow.Ticks.ToString() + ".zip";
      }
      var vPath = "";// System.Configuration.ConfigurationManager.AppSettings[BaseDictType.FileDonwloadPath];
      var filePath = Server.MapPath(vPath);

      if (!System.IO.Directory.Exists(filePath)) {
        System.IO.Directory.CreateDirectory(filePath);
      }
      
      System.IO.File.WriteAllBytes(filePath + fileName, fileContent);
      
      return Redirect(vPath + fileName);
    }





  }

}
