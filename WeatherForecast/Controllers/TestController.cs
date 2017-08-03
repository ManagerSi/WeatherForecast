using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeatherForecast.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Test/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Test/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Test/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult 搜索提示框() {
          return View();
        }
        public ActionResult Carousel1() {
          return View();
        }
        public ActionResult Carousel2() {
          return View();
        }
        public ActionResult HTMLBaseLeason() {
          return View();
        }
        public ActionResult FlyingBird() {
          return View();
        }
        public ActionResult canvas时钟制作() {
          return View();
        }
    }
}
