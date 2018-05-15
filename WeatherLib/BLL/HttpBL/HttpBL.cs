using log4net;
using Magnum.Concurrency; //Atomic
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WeatherLib.Model;
using WeatherLib.Serialization;
using WeatherLib.Utility;

namespace WeatherLib.BLL {
  public class HttpBL {
    public HttpClient httpClient = null;
    protected const string kCacheKeyPrefix = "_bl_base_http";
    private static readonly ILog log = LogManager.GetLogger(typeof(HttpBL));
    public static Atomic<int> counter = Atomic.Create(0);
    public HttpBL() {
      httpClient = new HttpClient();
    }
    public object GetWeather(string url ,string city ) {
      Dictionary<string,string> d = new Dictionary<string,string>();
      d.Add("city",city);
      var res = httpClient.Get<object>(url,d);
      return res;
    }
    public HeWeather5 GetWeather(string url,BaseRequest arg) {
      var datetime = DateTime.Now;
      Dictionary<string,string> d = new Dictionary<string,string>();
      d.Add("key",arg.key);
      d.Add("city",arg.city);
      d.Add("lang",arg.lang);
      string baseUrl = url + "/" + arg.Function;
      log.Info(d);
      var r = new HeWeather5();
      try{
         var res = httpClient.Get<WeatherResponse>(baseUrl,d);
         if(res != null) {
           r = res.HeWeather5.FirstOrDefault();
         } else {
           r = new HeWeather5() { status = "接口返回为空!" };
         }
      }catch(Exception e){
        r.status=e.Message;
      }
      log.Info(r);
      log.Info("calling  " + baseUrl + " using " + (DateTime.Now - datetime).TotalMilliseconds + ",ms. concurrency = " + counter.Value + ",error=" + r.status);

      return r;
    }
    public HeWeather5 GetWeatherForS8(string url,BaseRequest arg) {
      var datetime = DateTime.Now;
      Dictionary<string,string> d = new Dictionary<string,string>();
      d.Add("key",arg.key);
      d.Add("lang",arg.lang);
      d.Add("location",arg.city);
      //d.Add("t",arg.t);
      //d.Add("username ",arg.username);
      //string sign = getSignature(d,arg.key);
      //d.Add("sign",sign);

      string baseUrl = url + "/" + arg.Function;
      var r = new HeWeather5();
      try {
        var res = httpClient.Get<WeatherResponse>(baseUrl,d);
        if(res != null) {
          r = res.HeWeather5.FirstOrDefault();
        } else {
          r = new HeWeather5() { status = "接口返回为空!" };
        }
      } catch(Exception e) {
        r.status = e.Message;
      }
      log.Info("calling  " + baseUrl + " using " + (DateTime.Now - datetime).TotalMilliseconds + ",ms. concurrency = " + counter.Value + ",error=" + r.status);

      return r;
    }

    public static String getSignature(Dictionary<String,String> param, String secret) {
      string pStr = param.ToQueryString(Encoding.UTF8);
      byte[] pByte = Encoding.UTF8.GetBytes(pStr + secret);
      MD5 md5 = new MD5CryptoServiceProvider();
      byte[] result = md5.ComputeHash(pByte);
      return Convert.ToBase64String(result);

    }




    /// <summary>
    /// 根据openid从雅培官微获取手机号码
    /// </summary>
    /// <param name="webServiceUrl">接口地址</param>
    /// <param name="openID"></param>
    /// <returns></returns>
    public async Task<object> GetMobileByOpenID(string webServiceUrl,string openID) {
      var datetime = DateTime.Now;
      var response = new object();
      string json = SerializerHelper.GetJsonString(new { openId = openID });

      try {
        response = await httpClient.PostJsonAsync<object>(webServiceUrl,json);
        if(response != null ) {
          log.Info("calling  " + webServiceUrl + " using " + (DateTime.Now - datetime).TotalMilliseconds + "ms. concurrency = " + counter.Value + " return errmsg = " );
        } else {
          log.Info("calling  " + webServiceUrl + " using " + (DateTime.Now - datetime).TotalMilliseconds + "ms. concurrency = " + counter.Value);
        }
      } catch(Exception ex) {
        log.Info("calling  " + webServiceUrl + " using " + (DateTime.Now - datetime).TotalMilliseconds + "ms. concurrency = " + counter.Value + " catch errmsg = " + ex.Message);
        //response.ErrCode = TSICConstant.ANDCRMErrResult;
        //response.ErrMsg = TSICConstant.CallInterfaceError + ex.Message;
        return response;
      } finally {

      }
      return response;
    }
  }

}
