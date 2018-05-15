using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherLib.Serialization;
using Newtonsoft.Json;
using System.Web;

namespace WeatherLib.Utility {
  public class HttpClient {
    private const int timeout = 2 * 1000;
    private readonly Encoding encoder = Encoding.UTF8;

    public T Get<T>(string baseUrl, Dictionary<string, string> args) {
      var queryUrl = baseUrl + "?" + args.ToQueryString(Encoding.UTF8);

      var request = WebRequest.Create(queryUrl) as HttpWebRequest;
      request.Method = "GET";
      request.Timeout = timeout;
      using ( var response = request.GetResponse() ) {
        using ( var sr = new StreamReader(response.GetResponseStream()) ) {
          string json = sr.ReadToEnd();
          return JsonHelper.JsonDeserialize<T>(json);
        }
      }
    }

    public T PostJson<T>(string baseUrl, string json) {

      var request = WebRequest.Create(baseUrl) as HttpWebRequest;
      request.Method = "POST";
     // request.MediaType = "text/json";
      request.ContentType = "application/Json";
      request.Timeout = timeout;
      var bs = encoder.GetBytes(json);
      request.GetRequestStream().Write(bs, 0, bs.Length);
      using ( var response = request.GetResponse() ) {
        using ( var sr = new StreamReader(response.GetResponseStream()) ) {
          string strJson = sr.ReadToEnd();
          return JsonHelper.JsonDeserialize<T>(strJson);
        }
      }
    }

    public async Task<T> PostJsonAsync<T>(string baseUrl, Dictionary<string, string> args, string json, string appId, string accessToken)
    {
      var queryUrl = baseUrl + "?" + args.ToQueryString();
      var httpContent = System.Web.HttpContext.Current;
      var request = WebRequest.Create(queryUrl) as HttpWebRequest;
      request.Method = "POST";
      request.MediaType = "text/json";
      request.ContentType = "application/Json";
      request.Headers.Add("appId", appId);
      request.Headers.Add("accessToken", accessToken);
      request.Timeout = timeout;
      var bs = encoder.GetBytes(json);
      request.GetRequestStream().Write(bs, 0, bs.Length);
      using (var content = new MemoryStream())
      {
          using (var response = await request.GetResponseAsync())
          {
              using (Stream responseStream = response.GetResponseStream())
              {
                  await responseStream.CopyToAsync(content);
          }
          content.Position = 0;
          using ( var sr = new StreamReader(content) ) {
              string rjson = sr.ReadToEnd();
            return JsonHelper.JsonDeserialize<T>(rjson);
          }
        }
      }
    }

    public async Task<T> PostJsonAsync<T>(string baseUrl, Dictionary<string, string> args, string json) {
      var queryUrl = baseUrl + "?" + args.ToQueryString();
      var httpContent = System.Web.HttpContext.Current;
      var request = WebRequest.Create(queryUrl) as HttpWebRequest;
      request.Method = "POST";
      request.MediaType = "text/json";
      request.ContentType = "application/Json";
      request.Timeout = timeout;
      var bs = encoder.GetBytes(json);
      request.GetRequestStream().Write(bs, 0, bs.Length);
      using (var content = new MemoryStream())
      {
          using (var response = await request.GetResponseAsync())
          {
              using (Stream responseStream = response.GetResponseStream())
              {
                  await responseStream.CopyToAsync(content);
          }
          content.Position = 0;
          using ( var sr = new StreamReader(content) ) {
              string rjson = sr.ReadToEnd();
            return JsonHelper.JsonDeserialize<T>(rjson);
          }
        }
      }
    }

    public async Task<T> PostJsonAsync<T>(string baseUrl,string json) {
      var queryUrl = baseUrl;
      var httpContent = System.Web.HttpContext.Current;
      var request = WebRequest.Create(queryUrl) as HttpWebRequest;
      request.Method = "POST";
      request.MediaType = "text/json";
      request.ContentType = "application/Json";
      request.Timeout = timeout;
      var bs = encoder.GetBytes(json);
      request.GetRequestStream().Write(bs,0,bs.Length);
      using(var content = new MemoryStream()) {
        using(var response = await request.GetResponseAsync()) {
          using(Stream responseStream = response.GetResponseStream()) {
            await responseStream.CopyToAsync(content);
          }
          content.Position = 0;
          using(var sr = new StreamReader(content)) {
            string rjson = sr.ReadToEnd();
            return JsonHelper.JsonDeserialize<T>(rjson);
          }
        }
      }
    }
  
    public async Task<T> GetAsync<T>(string baseUrl, Dictionary<string, string> args) {
      var queryUrl = baseUrl + "?" + args.ToQueryString();

      var request = WebRequest.Create(queryUrl) as HttpWebRequest;
      request.Method = "GET";
      request.Timeout = timeout;

      using ( var content = new MemoryStream() ) {
        using ( var response = await request.GetResponseAsync() ) {
          using ( Stream responseStream = response.GetResponseStream() ) {
            await responseStream.CopyToAsync(content);
          }
          content.Position = 0;
          using ( var sr = new StreamReader(content) ) {
            string json = sr.ReadToEnd();
            return JsonHelper.JsonDeserialize<T>(json);
          }

        }
      }
    }
  }

}
