using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLib.Model;
using WeatherLib.BLL;
using System.Configuration;
using log4net;
using System.Data.SqlClient;

namespace WeatherService.Task.WeatherSync  {
  public class WeatherSyncTask: BaseSimpleTask  {

    private static readonly ILog log = LogManager.GetLogger(typeof(WeatherCitySyncTask));
    public WeatherSyncTask()
      : base("WeatherSyncTask") {
    }
    
    public readonly string HeWeatherUrl = ConfigurationManager.AppSettings["HeWeatherUrl"];
    private readonly string AppKey = ConfigurationManager.AppSettings["AppKey"];

    public HttpBL HeBL = new HttpBL();
    public IWeatherCityBL cityBL;
    IWeatherDailyForecastBL forecastBL;

    public List<string> CityList;
    /// <summary>
    /// 准备计算数据
    /// 加载一些外部数据，如商店名
    /// </summary>
    protected override void PrepareCompute() {
      base.PrepareCompute(); log.Info("PrepareCompute:" );
      cityBL = BLLFactory.Create<IWeatherCityBL>(UnitOfWork);
      forecastBL = BLLFactory.Create<IWeatherDailyForecastBL>(UnitOfWork);
      CityList = cityBL.GetAllCity().Select(i => i.CityCode).Take(2000).ToList();

      //将当前日期以后的数据无效
      DateTime dt = System.DateTime.Now.Date;
      DisableByDate(dt);
    }

    private void DisableByDate(DateTime dt) {
      string sql = "update Weather_Daily_Forecast set state=0 where date>=@dt";
      try {
        int count = UnitOfWork.Context.Database.ExecuteSqlCommand(sql,new SqlParameter("dt",dt.ToString("yyyy-MM-dd")));
        log.Info("disable count:" + count);
      } catch(Exception e) {
        log.Fatal(e);
      }

    }
    /// <summary>
    /// 计算完成，释放内存
    /// </summary>
    protected override void FinishCompute() {
      base.FinishCompute();
      //删除无效数据
      string sql = "delete Weather_Daily_Forecast where state=0";
      try {
        int count = UnitOfWork.Context.Database.ExecuteSqlCommand(sql);
        log.Info("disable count:" + count);
      } catch(Exception e) {
        log.Fatal(e);
      }
    }

    protected override void DoTask() {
      //GetCityWeather("北极");
      foreach(var city in CityList) {
        GetCityWeather(city);
        System.Threading.Thread.Sleep(100);
      }
    }

    
    public void GetCityWeather(string city,int retrycount=0) {
      //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970,1,1,0,0,0,0));
      //long t = (System.DateTime.Now.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位     
      if(retrycount > 2) { return; }

      log.Info("Get City Weather:" + city);
      BaseRequest request = new BaseRequest() {
        key = AppKey,
        Function = "forecast",
        lang = "zh-cn",
        city = city,
       // username = "HE1612111100301565"
      };

      var res = HeBL.GetWeather(HeWeatherUrl,request);
      if(res.status == "ok") {
        foreach(var forecast in res.daily_forecast) {
          WeatherDailyForecast df = new WeatherDailyForecast();
          df.CityCode = res.basic.ID;
          df.CityName = res.basic.City;
          df.AstroMR = forecast.astro.mr;
          df.AstroMS = forecast.astro.ms;
          df.AstroSR = forecast.astro.sr;
          df.AstroSS = forecast.astro.ss;
          df.CondCodeD = forecast.cond.code_d.ToString();
          df.CondCodeN = forecast.cond.code_n.ToString();
          df.CondTxtD = forecast.cond.txt_d.ToString();
          df.CondTxtN = forecast.cond.txt_n.ToString();
          df.Date = forecast.date;
          df.Hum = forecast.hum;
          df.Pcpn = (double)forecast.pcpn;
          df.Pop = (double)forecast.pop;
          df.Pres = (double)forecast.pres;
          df.TmpMax = (double)forecast.tmp.max;
          df.TmpMin = (double)forecast.tmp.min;
          df.Vis = (double)forecast.vis;
          df.WindDeg = (double)forecast.wind.deg;
          df.WindDir = forecast.wind.dir;
          df.WindSC = forecast.wind.sc;
          df.WindSpd = (double)forecast.wind.spd;
          df.State = "1";
          forecastBL.Insert(df);
        }

      } else {
        log.InfoFormat("get  Weather failed,city:{0},reason:{1}",city,res.status);
        System.Threading.Thread.Sleep(5000);
        GetCityWeather(city,retrycount+1);
      }

    }
  }
}
