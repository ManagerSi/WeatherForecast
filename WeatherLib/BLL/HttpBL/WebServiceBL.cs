using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLib.Model;
using Mobizone.TSIC.FOC.Serialization;
using WeatherLib.Utility;
using Magnum.Concurrency;
using Mobizone.TSIC.FOC.Service;
using System.Configuration;
using log4net;

namespace Mobizone.TSIC.BLL.CRM
{
    public class WebServiceBL {
        public HttpClient httpClient = null;
        //protected const string kCacheKeyPrefix = "_bl_base_crm";
        //private static readonly ILog log = LogManager.GetLogger(typeof(CRMBL));
        private static readonly ILog log = LogManager.GetLogger(typeof(WebServiceBL));

        public readonly string Dbhost = ConfigurationManager.AppSettings[BaseDictType.CRMDbhost];
        public readonly string Wfpuser = ConfigurationManager.AppSettings[BaseDictType.CRMWfpUser];
        public readonly string Appkey = ConfigurationManager.AppSettings[BaseDictType.CRMAppkey];
        public readonly string WebServiceUrl = ConfigurationManager.AppSettings[BaseDictType.CRMWebServiceURL];
        class RestfulMsg<T>
        {
            public T Message { get; set; }
            public int Code { get; set; }
        }

        public WebServiceBL()
        {
            httpClient = new HttpClient();
        }

        public async Task<ProdQRInfo> GetProdQRInfo(ProdQRRequest request) {
            
            request.Dbhost = Dbhost;
            request.Appkey = Util.PasswordMD5(Wfpuser + Appkey);
            request.Wfpuser = Wfpuser;
            request.Channel = 2;
            request.Function = "QRCodeExchange";
            var response = new ProdQRInfo();
            string json = SerializerHelper.GetJsonString(request);
            log.Info("json:" + json);
            log.Info("web service url:" + WebServiceUrl);
            try {
                response = await httpClient.PostJsonAsync<ProdQRInfo>(WebServiceUrl+"/"+ request.Function,null,json);
                //log.Info("calling " + request.Function + " using " + (DateTime.Now - datetime).TotalMilliseconds + "ms. concurrency = " + counter.Value);
                //result=7 因网络延迟信息提交失败
                log.Info("response:"+ SerializerHelper.GetJsonString(response));
                return response;

            } catch(Exception ex) {
                response.Result = "2";
                response.Remark = "网络延迟请重试或者直接保存二维码信息";
                //response.Product_No = null;
                return response;
            }
            
        }

        [RPC]
        public ProdQRInfo GetProdQr(ProdQRRequest request)
        {
            request.Dbhost = Dbhost;
            request.Appkey = Util.PasswordMD5(Wfpuser + Appkey);
            request.Wfpuser = Wfpuser;
            request.Channel = 2;
            request.Function = "QRCodeExchange";
            try
            {
                string json = SerializerHelper.GetJsonString(request);
                var service = new HttpRestfulService(WebServiceUrl);
                var msg = service.CallMethodPost<ProdQRInfo>(request.Function, json);
                //if (msg.Result != 0)
                //{
                //    return new ProdQRInfo();
                //}

                return msg;
            }
            catch (Newtonsoft.Json.JsonReaderException e)
            {
                throw;
            }
        }
    }
}
