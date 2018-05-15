using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WeatherForecast
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
          log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


#if USING_RAVENDB
      Mobizone.TSIC.Models.TSICRavenDocContext.InitDocumentStore();
#elif USING_MONGODB 
      //网站启动后启动mongodb
      //Mobizone.TSIC.Models.TSICMongoDocContext.InitClient();
#endif
        }

        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        protected void Application_Error(object sender,EventArgs ex) {
          var error = Server.GetLastError();
          
        }
    }
}