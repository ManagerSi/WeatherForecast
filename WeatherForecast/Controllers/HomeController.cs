using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherLib.BLL;
using WeatherLib.Controllers;
using WeatherLib.Model;

namespace WeatherForecast.Controllers
{
    public class HomeController : BaseController {
      private static readonly ILog log = LogManager.GetLogger(typeof(BaseController));

      public readonly string HeWeatherUrl = ConfigurationManager.AppSettings["HeWeatherUrl"];
      private readonly string AppKey = ConfigurationManager.AppSettings["AppKey"];

      [NonAction]
      public BaseRequest initBaseRequest(string function) {
        return new BaseRequest() { 
          key = AppKey,
          Function = function,
          lang = "zh-cn",
        };
      }
        public ActionResult Weather() {
          return View();
        }

        public ActionResult WeatherByheweatherPartial_ajax() {
          return View();
        }
        public ActionResult WeatherByPluginPartial() {
          return View();
        }

        public ActionResult API_WeatherByheweatherPartial_All(string city="beijing") {
          var request = initBaseRequest(WeatherFunction.weather.ToString());
          request.city = city;
          var httpBL = new HttpBL();
          var resp = httpBL.GetWeather(HeWeatherUrl,request);
          return View(resp);
        }
        public ActionResult Index()
        {
          return RedirectToAction("Weather");
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #region kindedit 编辑器

        public ActionResult OnlineEditor() {
          return View();
        }
        [HttpPost]
        public ActionResult OnlineEditor(string content) {
          return View();
        }
        #endregion
    }
}
