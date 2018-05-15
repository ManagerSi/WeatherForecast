using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeatherForecast.Utility {
  /// <summary>
  /// 异常处理
  /// </summary>
  public class CustomHandleErrorAttribute :HandleErrorAttribute{
    public override void OnException(ExceptionContext filterContext) {
      if(!filterContext.ExceptionHandled){//异常没哟被处理
        string contollerName = filterContext.RouteData.Values["controller"].ToString();

        if(filterContext.HttpContext.Request.IsAjaxRequest()) { //ajax
          filterContext.Result = new JsonResult() {
            Data = "异常"
          };
        } else {
          filterContext.Result = new ViewResult() {
            ViewName = "~/Views/shared/error.cshtml",
            ViewData = new ViewDataDictionary<string>(){
             new KeyValuePair<string,object>("异常", filterContext.Exception)
            }
          };
        }
        filterContext.ExceptionHandled = true; //异常被处理
      }
      base.OnException(filterContext);
    }
  }
}