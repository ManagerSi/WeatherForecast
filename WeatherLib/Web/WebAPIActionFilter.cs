﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Runtime.Serialization;
﻿using System.Web.Http.Controllers;
﻿using System.Web.Http.Filters;
﻿using Mobizone.TSIC.BLL;
﻿using Mobizone.TSIC.Controllers;
using Mobizone.TSIC.Security;
using Mobizone.TSIC.Web.SessionState;
using Mobizone.TSIC.Utility;
using Mobizone.TSIC.Models;
using log4net;

namespace Mobizone.TSIC.Web {

  public class ProfileActionFilter : ActionFilterAttribute {
    private static readonly ILog log = LogManager.GetLogger(typeof(ProfileActionFilter));
    private DateTime startTime;
    private static int count = 0;
    private static object locker = new object();
    public override void OnActionExecuting(HttpActionContext actionContext) {
      startTime = DateTime.Now;
      if(Configuration.Flag_enable_request_track_log) {
        int local_count;
        lock(locker) {
          count++;
          local_count = count;
        };
        log.Info("executing uri={api:" + HttpContext.Current.Request.RawUrl + "},count =" + local_count);
      }
    }

    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext) {
      if(Configuration.Flag_enable_request_track_log) {
        int local_count;
        lock(locker) {
          count--;
          local_count = count;
        };
        log.Info("executed uri={api:" + HttpContext.Current.Request.RawUrl + "},count =" + local_count);
      }
      var delta = DateTime.Now - startTime;
      if (delta.TotalMilliseconds > 1000) {
        log.Fatal("slow executing api=" + delta.TotalMilliseconds + ",uri=" + HttpContext.Current.Request.RawUrl);
      }
    }
  }

  public class AutoOfflineActionFilter : ActionFilterAttribute {
    public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext) {
      if (!BaseController.IsWapOfflineTime) {
        return;
      }
      var controller = actionContext.ControllerContext.Controller as IAutoOffineSupportController;

      if (controller == null || actionContext.ActionDescriptor.GetCustomAttributes<NeverOffline>().Count > 0) {
        return;
      }

      actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                                                                    controller.CreateOfflineResponse());
    }

  }

  public class BaseAPIAuthorizationFilter : System.Web.Http.AuthorizeAttribute {
    public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext) {
      var controller = actionContext.ControllerContext.Controller as TSICApiController;
      if (null == controller) {
        return;
      }
      controller.AuthorizationService = new TSICAuthorizationService();
      var session = SessionFactory.Create<IBLSessionPersisiter>();

      // 尝试使用Token登录
      //if (Configuration.IsDevMode) {
        if (actionContext.Request.Headers.Contains(TSICConstant.HeaderToken)) {
          string tokenId = actionContext.Request.Headers.GetValues(TSICConstant.HeaderToken).FirstOrDefault();
          var tokenBL = BLLFactory.Create<IUserTokenBL>();
          var token = tokenBL.GetToken(tokenId);
          // 如果用户不一致，以新用户登录
          if (token != null && token.UserID != session.UserID) {
            if (controller.AuthorizationService.SetAuthSession(token.UserID, (TSICClientType)token.ClientType)) {
              var clientSession = SessionFactory.Create<IClientSessionPersisiter>();
              clientSession.ClientVersion = token.ClientVersion;

            }
          }
        }
      //}

      var user = new TSICPrincipal(
         new TSICIdentity(session.UserID), controller.AuthorizationService);

      System.Threading.Thread.CurrentPrincipal = user;
      HttpContext.Current.User = user;

      base.OnAuthorization(actionContext);
    }

    protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext) {
      var controller = actionContext.ControllerContext.Controller as TSICAppJsonAPIController;
      if (controller != null) {
        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new AppJsonResponse() {
          Meta = new JsonAsyncObjectMetaData() {
            Type = "UnauthorizedModel",
            Uri = "Unauthorized",
          },
          Data = new UnauthorizedModel() {
            RequireUri = controller.GetUriFromLocalPath(),
            Message = "未授权的请求",
          }
        });
      } else {
        base.HandleUnauthorizedRequest(actionContext);
      }

    }
  }

  public class APIHandleExceptionAttribute : ExceptionFilterAttribute {
    private static readonly ILog log = LogManager.GetLogger(typeof(APIHandleExceptionAttribute));
    public override void OnException(HttpActionExecutedContext actionExecutedContext) {
      if (actionExecutedContext.Exception == null) {
        return;
      }
      HttpContext.Current.Response.TrySkipIisCustomErrors = true;
      var exception = actionExecutedContext.Exception;
      try {
        var clientSession = SessionFactory.Create<IClientSessionPersisiter>();
        var session = SessionFactory.Create<IBLSessionPersisiter>();
        string info = clientSession.ClientType.ToString() + ";" + clientSession.ClientVersion;
        if (session.BaseEmployee != null) {
          info += ";" + session.BaseEmployee.ID;
        }
        log.Fatal(string.Format("ErrorInfo={{{2}}}\nurl={{{0}}}\n body={{{1}}}", actionExecutedContext.Request.RequestUri,
          actionExecutedContext.Request.Content.ReadAsStringAsync().Result,
          info));
      } catch {

      }
      log.Fatal(exception);
      if (exception.InnerException != null) {
        exception = exception.InnerException;
        log.Fatal(exception.InnerException);
      }
      var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as TSICAppJsonAPIController;
      if (controller != null) {
        actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new AppJsonResponse() {
          Meta = new JsonAsyncObjectMetaData() {
            Type = "ExceptionModel",
            Uri = "Exception",
          },
          Data = new ExceptionModel() {
            Message = exception.Message,
            Type = exception.GetType().FullName,
          }
        });
      } else {
        actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
          HttpStatusCode.InternalServerError, new ErrorList {
            Status = "Error",
            Message = exception.Message
          });
      }
    }

  }

  public class APIValidationActionFilter : ActionFilterAttribute {

    public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext) {
      HttpContext.Current.Response.TrySkipIisCustomErrors = true;
      if (!actionContext.ModelState.IsValid) {

        var controller = actionContext.ControllerContext.Controller as TSICAppJsonAPIController;
        if (controller != null) {
          actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, controller.CreateValidationResponse(actionContext.ModelState));
        } else {
          var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new Error {
              Name = e.Key,
              Message = e.Value.Errors.First().ErrorMessage
            }).ToArray();
          actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorList {
            Errors = errors,
            Status = "ValidationFailed",
            Message = "数据绑定检查失败"
          });
        }
      }
    }




  }
  class ErrorList {

    public Error[] Errors { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
  }
  class Error {
    public string Name { get; set; }
    public string Message { get; set; }
  }
}

