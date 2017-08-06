using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecast.Models;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Bson;
using WeatherLib.Model;
using WeatherLib.BLL;
using WeatherLib.Controllers;
using WeatherLib.Security;
using WeatherLib.Utility;
namespace WeatherForecast.Controllers
{
    public class AccountController : BaseController
    {
      //protected WeatherAuthorizationService AuthorizationService { get; set; }
        //public  string conStr
        //{
        //    get
        //    {
        //        return System.Configuration.ConfigurationManager.ConnectionStrings["SqlDBContext"].ToString();
        //    }
        //}
        //public WeatherDBContext sqldb
        //{
        //    get
        //    {
        //        return new WeatherDBContext(conStr);
        //    }
        //}
        //public MongoDatabase mongodb
        //{
        //  get {
        //    return TSICMongoDocContext.ConnectDB();
        //  }
        //}


        //
        // GET: /Account/
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
              var u = BLLFactory.Create<IBaseUserBL>();
              
                //var user = sqldb.BASE_USER.Where(i => i.State == "1" && i.UserAccount == model.Account && i.UserPWD == model.PassWord).FirstOrDefault();
                var user = u.GetUserInWeb(model.Account,model.PassWord);
                if(user != null) {

                  //if(AuthorizationService.SetAuthSession(model.Account,model.PassWord,WeatherClientType.Web)) {
                  //  return RedirectToAction("index","home");
                  //}
                  if(u.SetAuthSession(model.Account,model.PassWord,WeatherClientType.Web)) {
                    return RedirectToAction("index","Dashboard",new { Area = "Admin" });
                  }
                  
                }
                ModelState.AddModelError("PassWord", "用户名或密码错误");
            }
            return View(model);
        }
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Account/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Create

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
        // GET: /Account/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Account/Edit/5

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
        // GET: /Account/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Account/Delete/5

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
