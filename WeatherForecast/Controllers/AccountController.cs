using WeatherLib.Model;
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

namespace WeatherForecast.Controllers
{
    public class AccountController : Controller
    {
        public  string conStr
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["SqlDBContext"].ToString();
            }
        }
        public WeatherDBContext sqldb
        {
            get
            {
                return new WeatherDBContext(conStr);
            }
        }
        public MongoDatabase mongodb
        {
            get
            {
                return Mobizone.TSIC.Models.TSICMongoDocContext.ConnectDB();
            }
        }

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
                var user = sqldb.BASE_USER.Where(i => i.State == "1" && i.UserAccount == model.Account && i.UserPWD == model.PassWord).FirstOrDefault();
                if(user != null)
                {
                    WeatherLib.Model.Documents.Login login= new WeatherLib.Model.Documents.Login();
                    login.State=true;
                    login.Time = System.DateTime.Now;
                    login.UpdateTime = login.Time;
                    mongodb.GetCollection<WeatherLib.Model.Documents.Login>(typeof(WeatherLib.Model.Documents.Login).Name).Insert(login);
                    return RedirectToAction("index", "home");
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
