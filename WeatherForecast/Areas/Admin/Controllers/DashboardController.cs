using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherLib.Controllers;
using WeatherLib.Security;

namespace WeatherForecast.Areas.Admin.Controllers
{
    //[SampleAuthorizeAttribute(Users = new string[] { "SI" },Roles = new string[] { "管理员" })] //role--对应SamplePrincipal中的IsInRole；Users--对应SamplePrincipal.identity.Name
    [SampleAuthorizeAttribute(Roles = new string[] { "管理员","报表查询" })]
    /*
     *同等效果
     *[Authorize]
     *[Authorize(Roles = "管理员,报表查询")] //失败后需自定义跳转
     *[Authorize(Users = "SI",Roles="管理员,报表查询")]
     */
  public class DashboardController : BaseController
    {
        //
        // GET: /Area/Dashbord/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Area/Dashbord/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Area/Dashbord/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Area/Dashbord/Create

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

        //
        // GET: /Area/Dashbord/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Area/Dashbord/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Area/Dashbord/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Area/Dashbord/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
