using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Magnum.Concurrency;
using Magnum.Concurrency.Internal;
using Mobizone.TSIC.FOC.Utility;


namespace Mobizone.TSIC.FOC.Service {
  public class HttpRestfulService {
    //private static readonly ILog log = LogManager.GetLogger(typeof(HttpRestfulService));

    public HttpRestfulService(string baseUri) {
      BaseUri = baseUri;
      Timeout = 2000; // 2s
    }

    public string BaseUri { get; set; }

    /// <summary>
    /// timeout 毫秒
    /// </summary>
    public int Timeout { get; set; }

    // 请设置这里的编码
    public static readonly Encoding Encoder = Encoding.GetEncoding("GBK");

    protected string GetJson(string methodName, Dictionary<string, string> args, bool decode =true)
    {
      var queryUrl = BaseUri + "/" +
        methodName + "?" + args.ToQueryString();

      HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(queryUrl);
      request.Method = "GET";
      request.Timeout = 5000;
      try {
        using (HttpWebResponse respose = (HttpWebResponse)request.GetResponse()) {
          using (StreamReader sr = new StreamReader(respose.GetResponseStream(), Encoder)) {

            string json = sr.ReadToEnd();
            //// base 64 解码
            //{
            //var base64EncodedBytes = System.Convert.FromBase64String(json);
            //json =  System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            //}
            //// aes解码
            //{

            // clsCrypto aes = new clsCrypto();
            //  aes.IV = "this is your IV";     // your IV
            //  aes.KEY = "this is your KEY";    // your KEY      
            //  json = aes.Decrypt(json, CipherMode.CBC);
            //}
            if (decode)
            {
              // 解密
              AESHelper helper = new AESHelper();
              json = helper.AESDecrypt(json);
            }
            return json;
          }

        }
      } catch (Exception e) {
        //log.Fatal("HttpService Fail:", e);
        throw;
      }
    }



    public  T CallMethodGet<T>(string methodName, Dictionary<string, string> args, bool decode = true) where T : new()
    {
      var datetime = DateTime.Now;
      try {
        string json = GetJson(methodName, args, decode);
        try {
          return Serialization.JsonHelper.JsonDeserialize<T>(json);
        } catch (Exception e) {         
          throw;
        }
      } finally {
        
      }

    }

    public T CallMethodPost<T>(string methodName, string args) where T : new()
    {
        var datetime = DateTime.Now;
        try
        {
            string json = PostJson(methodName, args);
            try
            {
                return Serialization.JsonHelper.JsonDeserialize<T>(json);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        finally
        {
        }

    }

    protected string PostJson(string methodName, string args)
    {
        var queryUrl = BaseUri + "/" +
          methodName;

        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(queryUrl);
        request.Method = "POST";
        request.Timeout = 2000;
        byte[] btBodys = Encoding.UTF8.GetBytes(args);
        request.ContentLength = btBodys.Length;
        request.GetRequestStream().Write(btBodys,0,btBodys.Length);
        try
        {
            using (HttpWebResponse respose = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(respose.GetResponseStream(), Encoder))
                {
                    string json = sr.ReadToEnd();
                    return json;
                }
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public class AESHelper {
      //默认密钥向量   
      private byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
      private string strKey = "Abbott_TSIC_WebS";

      /// <summary>  
      /// AES解密  
      /// </summary>  
      /// <param name="cipherText">密文字节数组</param>  
      /// <param name="strKey">密钥</param>  
      /// <returns>返回解密后的字符串</returns>  
      public string AESDecrypt(string plainText) {
        byte[] cipherText = Convert.FromBase64String(plainText);
        using (SymmetricAlgorithm des = Rijndael.Create()) {
          des.Key = Encoding.GetEncoding("GB2312").GetBytes(strKey);
          des.IV = _key1;
          byte[] decryptBytes = new byte[cipherText.Length];
          using (MemoryStream ms = new MemoryStream(cipherText)) {
            using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read)) {
              cs.Read(decryptBytes, 0, decryptBytes.Length);
              cs.Close();
            }
            ms.Close();
          }
          return Encoding.GetEncoding("GB2312").GetString(decryptBytes);
        }
      }
    }

  }
}
