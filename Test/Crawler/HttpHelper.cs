using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;//HttpWebRequest
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.Crawler {
  public class HttpHelper {
    private static ILog log = log4net.LogManager.GetLogger(typeof(HttpHelper));

    public static string DownloadHtml(string url,Encoding encod) {
      string html = string.Empty;
      try {
        //设置请求参数
        HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
        request.Timeout = 10 * 1000;//10s超时
        request.ContentType = "text/html;charset=utf-8";
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
        
        //request.Headers.Add("cookie","user-key=3dfc2ad9-90a0-4fcf-b041-39172aa91d98; cn=0; unpl=V2_ZzNtbUYCShYgAEBdf0tVBmIEQlURBRNCcgoRAH4aXFdiUxNUclRCFXMUR1BnGVkUZwIZXkdcRhBFCHZUehhdBGYFE1tAZ3Mldgh2XEsZVAFuARpeRVJLJUUPdmRLG1oNZAQaWEVWcxRFCHYfFRgRBW8HG19KVEQQfThHZHg%3d; __jdv=122270672|www.meijiechao.com|t_1000007073_|tuiguang|5d82d9785c8246a9bcaf63fe421c4a08|1494728726813; _jrda=1; wlfstk_smdl=i3ncr07s4ud8bgg3qspcwvz8qcqy11xj; TrackID=18YSQzZ0FgdlqIq8H8DBSyNgjrKbbQKmdqOvB9NP66yYCfETfOk8s87XviEXoY2QLaX28JuCdLu7h0oBfbZhHEw; pinId=Lk7Re23cB6PHsDqJQzoSug; pin=smallvv_m; unick=smallvvrra; _tp=JWbbOm0S7amJFP2iKHxynQ%3D%3D; _pst=smallvv_m; ceshi3.com=000; __jda=122270672.1958392649.1494512525.1494728727.1494734359.3; __jdb=122270672.1.1958392649|3.1494734359; __jdc=122270672; thor=976DC621E20D944BC53410B6B0767C319DE334BA9F3251DDE367D1030CF00B7335B7BD91A22BCEDE819811765A25DD44339055A3110925BBD179E51E5DF1DC539621F5D6795BECC6D17A70290973A5862A9ED67367E315048BE0E6D53659B19AC754B1DAC82979C025F26DBF7B8C60EF87E623FFC2F14E319EA6F5AE380731B36D29506F361EC987C48FEDB71C55ADBF; ipLoc-djd=1-72-2799-0; ipLocation=%u5317%u4EAC; __jdu=1958392649; 3AB9D23F7A4B3C9B=6RPGQIE5L57NCNXGU5H5UP6SY6EFU5EDQQQJJBPVURD4L523JZ5NXG5HPQS4MQLOVDGFWH6AEJRYECV75Z3JPVIPV4");
        //request.Headers.Add("","");
        //request.Headers.Add("","");

        //获取结果
        using(HttpWebResponse resp = request.GetResponse() as HttpWebResponse) {
          if(resp.StatusCode != HttpStatusCode.OK) {
            log.Fatal(string.Format("抓取{0}地址返回失败,response.StatusCode = {1}",url,resp.StatusCode));
          } else {
            try {
              StreamReader sr = new StreamReader(resp.GetResponseStream(),encod);
              html = sr.ReadToEnd();
              sr.Close();
            } catch(Exception e) {
              log.Fatal(string.Format("DownLoadHtml抓取html{0}保存失败",url),e);
              
            }
          }
        }

      } catch(Exception e) {
        if(e.Message.Equals("远程服务器返回错误：(306)。")) {

        }
        log.Fatal(e);
      } finally {

      }
      return html;
    }
  }
}
