using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text.RegularExpressions;
//using Mobizone.TSIC.Cache;
using Encoder = System.Text.Encoder;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using Senparc.Weixin.QY.CommonAPIs;
//using Senparc.Weixin.QY.AdvancedAPIs;
//using Senparc.Weixin;
using WeatherLib.BLL;

namespace WeatherLib.Utility {
  public class Util {

    public static DateTime RPCNow {
      get {
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
      }
    }

    public static DateTime LocalNow {
      get {
        return DateTime.Now;
      }
    }

    public static decimal? ParseNullableDecimal(string d) {
      if(string.IsNullOrEmpty(d)) {
        return null;
      }
      decimal de;
      if(!decimal.TryParse(d,out de)) {
        return null;
      }
      return de;
    }

    public static void UploadAPIFile(string jpgBase64Data,string fileName,string filePath) {
      byte[] data = Convert.FromBase64String(jpgBase64Data);
      using(var ms = new MemoryStream(data)) {
        var img = Image.FromStream(ms);
        var qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
        var quality = 80L;
        var ratio = new EncoderParameter(qualityEncoder,quality);
        var codecParams = new EncoderParameters(1);
        codecParams.Param[0] = ratio;
        ImageCodecInfo jpgEncoder = GetImgEncoder(ImageFormat.Jpeg);

        if(!System.IO.Directory.Exists(filePath)) {
          System.IO.Directory.CreateDirectory(filePath);
        }

        img.Save(filePath + fileName,jpgEncoder,codecParams);
      }
    }

    public static ImageCodecInfo GetImgEncoder(ImageFormat format) {

      ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

      foreach(ImageCodecInfo codec in codecs) {
        if(codec.FormatID == format.Guid) {
          return codec;
        }
      }
      return null;
    }

    public static void UploadFile(HttpPostedFileBase file,string fileName,string filePath) {
      if(!System.IO.Directory.Exists(filePath)) {
        System.IO.Directory.CreateDirectory(filePath);
      }
      filePath += fileName;
      file.SaveAs(filePath);
    }

    public static async void UploadFileAsync(HttpPostedFileBase file,string fileName,string filePath) {
      await System.Threading.Tasks.Task.Run(() => {
        if(!System.IO.Directory.Exists(filePath)) {
          System.IO.Directory.CreateDirectory(filePath);
        }
        filePath += fileName;
        file.SaveAs(filePath);
      });
    }
    public static async Task<string> UploadFileAsyncString(HttpPostedFileBase file,string fileName,string filePath) {
      return await System.Threading.Tasks.Task.Run(() => {
        if(!System.IO.Directory.Exists(filePath)) {
          System.IO.Directory.CreateDirectory(filePath);
        }
        filePath += fileName;
        file.SaveAs(filePath);
        return filePath;
      });
    }
    public static void MoveFile(string mapFilePath,string fileName,string saveFilePath) {
      if(!System.IO.Directory.Exists(saveFilePath)) {
        System.IO.Directory.CreateDirectory(saveFilePath);
      }
      //复制文件
      System.IO.File.Copy(mapFilePath,saveFilePath + fileName);
      //删除被复制文件
      System.IO.File.Delete(mapFilePath);
    }

    public static decimal FileSize(string mapFilePath,string fileName) {
      long fileSize = 0;
      var server = HttpContext.Current.Server;
      var mappedPath = server.MapPath(mapFilePath + "" + fileName);
      if(System.IO.File.Exists(mappedPath)) {
        fileSize = new System.IO.FileInfo(mappedPath).Length;
      }
      return (decimal)fileSize;
    }

    public static string FormatSize(decimal bytes) {

      string[] unit = new string[6] { "kB","MB","GB","TB","PB","EB" };

      var i = -1;
      do {
        bytes = bytes / 1024;
        i++;
      } while(bytes > 99);

      return Math.Round(Math.Max(bytes,(decimal)0.1),1).ToString() + unit[i];
    }


    /// <summary>
    /// 将临时文件移动指定类型的文件系统中
    /// 如Util.MoveUploadTempFileTo(model.FileName, BaseDictType.BizMsgFileUploadPath);
    /// 成功时，返回文件名，否则返回空字符串
    /// </summary>
    /// <param name="tempFileName"></param>
    /// <param name="UploadPathType"></param>
    /// <returns></returns>
    public static string MoveUploadTempFileTo(string tempFileName,string UploadPathType) {
      var appSettings = System.Configuration.ConfigurationManager.AppSettings;
      var tempPath = tempFileName;// +appSettings[BaseDictType.TempFileUploadPath];
      var server = HttpContext.Current.Server;
      var mappedPath = server.MapPath(tempPath);
      if(!File.Exists(mappedPath)) {
        throw new FileNotFoundException("Temp File:" + tempFileName);
      }
      var uploadPath = appSettings[UploadPathType];
      var mappedUploadPath = server.MapPath(uploadPath);
      if(!System.IO.Directory.Exists(mappedUploadPath)) {
        System.IO.Directory.CreateDirectory(mappedUploadPath);
      }
      System.IO.File.Copy(mappedPath,mappedUploadPath + tempFileName,true);
      System.IO.File.Delete(mappedPath);
      return tempFileName;
    }

    public static bool FileExists(string tempFileName,string UploadPathType) {
      var appSettings = System.Configuration.ConfigurationManager.AppSettings;
      var tempPath = tempFileName;//appSettings[BaseDictType.TempFileUploadPath] + 
      var server = HttpContext.Current.Server;
      var mappedPath = server.MapPath(tempPath);
      return File.Exists(mappedPath);
    }

    public static void DeletePic(string Path) {
      try {
        var server = HttpContext.Current.Server;
        string address = server.MapPath(Path);
        if(System.IO.File.Exists(address)) {
          System.IO.File.Delete(address);
        }
      } catch(Exception e) {
        string err = e.Message;
      }
    }

    public static string ValidationFile(HttpPostedFileBase file) {
      if(file == null || file.ContentLength == 0) {
        return "上传图片为空，请上传图片";
      } else if(string.IsNullOrEmpty(GetExtend(file))) {
        return "上传文件类型错误，请上传图片(JPG、GIF或PNG)" + file.ContentType;
      } else if(file.ContentLength == 0) {
        return "上传图片有误，请重新上传图片";
      } else if(file.ContentLength > 3145728) { //由200KB调整为3MB  --字节214400
        return "图片大小应小于3MB ";
      } else {
        return null;
      }
    }

    public static string GetExtend(HttpPostedFileBase file) {
      switch(file.ContentType) {
        case "image/jpeg": return ".jpg";
        case "image/x-jpeg": return ".jpg";
        case "image/pjpeg": return ".jpg";
        case "image/x-pjpeg": return ".jpg";
        case "image/png": return ".png";
        case "image/x-png": return ".png";
        case "image/gif": return ".gif";
        case "image/x-gif": return ".gif";
      }
      return "";
    }
    public static string GetPresentImagePath(string imageType,string path,bool startWithHost = true) {
      if(string.IsNullOrEmpty(path)) {
        return "";
      }
      string imgPath = "";
      if(startWithHost) {
        imgPath = System.Configuration.ConfigurationManager.AppSettings["HostUrl"];
      } else {
        imgPath = "~";
      }

      if(string.IsNullOrWhiteSpace(path) || path.StartsWith("uploadPic/") || path.StartsWith("/uploadPic/")) {
        imgPath += "/Content/images/history.jpg";
      } else {
        var paths = path.Split(',');
        var url = System.Configuration.ConfigurationManager.AppSettings[imageType] + "/";
        if(paths.Length == 1) {
          imgPath += url;
          imgPath += path;
        } else {
          imgPath += "/Admin/ImgDisplay?url=" + url + "&images=";
          imgPath += path;
        }
      }
      return imgPath;
    }
    public static string GetImagePath(string imageType,string storeID,string path,bool startWithHost = true) {
      if(string.IsNullOrEmpty(path)) {
        return "";
      }
      string imgPath = "";
      if(startWithHost) {
        imgPath = System.Configuration.ConfigurationManager.AppSettings["HostUrl"];
      } else {
        imgPath = "~";
      }

      if(string.IsNullOrWhiteSpace(path) || path.StartsWith("uploadPic/") || path.StartsWith("/uploadPic/")) {
        imgPath += "/Content/images/history.jpg";
      } else {
        var paths = path.Split(',');
        var url = System.Configuration.ConfigurationManager.AppSettings[imageType] + "" + storeID + "/";
        if(paths.Length == 1) {
          imgPath += url;
          imgPath += path;
        } else {
          imgPath += "/Admin/ImgDisplay?url=" + url + "&images=";
          imgPath += path;
        }
      }
      return imgPath;
    }
    //public static string EmpTypeToString(decimal? empType) {
    //  return BLLFactory.Create<IBaseDictBL>().GetItemNameByCached(BaseDictType.DictTypeEmpType,empType ?? -1);
    //}
    //public static decimal? ParseEmpType(string type) {
    //  var rst = BLLFactory.Create<IBaseDictBL>().GetItemIDByCached(BaseDictType.DictTypeEmpType,type);
    //  return rst >= 0 ? (decimal?)rst : null;
    //}
    public static bool isValidSqlDate(DateTime date) {
      return ((date >= (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue) && (date <= (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue));
    }

    #region 验证手机号和电话号码
    /// <summary>
    /// 验证手机号
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsMobile(string source) {
      return Regex.IsMatch(source,@"^1\d{10}$",RegexOptions.IgnoreCase);
    }
    public static bool HasMobile(string source) {
      return Regex.IsMatch(source,@"^1\d{10}$",RegexOptions.IgnoreCase);
    }

    /**/
    /// <summary>
    /// 是不是中国电话，格式010-85849685
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsTel(string source) {
      return Regex.IsMatch(source,@"^\d{3,4}-?\d{6,8}$",RegexOptions.IgnoreCase);
    }
    #endregion
    /// <summary>
    /// 身份证验证
    /// </summary>
    /// <param name="Id">身份证号</param>
    /// <returns></returns>
    public static bool CheckIDCard(string Id) {
      if(Id.Length == 18) {
        return CheckIDCard18(Id);
      } else if(Id.Length == 15) {
        return CheckIDCard15(Id);
      }
      return false;
    }

    /// <summary>
    /// 18位身份证验证
    /// </summary>
    /// <param name="Id">身份证号</param>
    /// <returns></returns>
    private static bool CheckIDCard18(string Id) {
      long n = 0;
      if(long.TryParse(Id.Remove(17),out n) == false
        || n < Math.Pow(10,16)
        || long.TryParse(Id.Replace('x','0').Replace('X','0'),out n) == false) {
        return false;//数字验证
      }
      string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
      if(address.IndexOf(Id.Remove(2)) == -1) {
        return false;//省份验证
      }
      string birth = Id.Substring(6,8).Insert(6,"-").Insert(4,"-");
      DateTime time = new DateTime();
      if(DateTime.TryParse(birth,out time) == false) {
        return false;//生日验证
      }
      string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
      string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
      char[] Ai = Id.Remove(17).ToCharArray();
      int sum = 0;
      for(int i = 0;i < 17;i++) {
        sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
      }
      int y = -1;
      Math.DivRem(sum,11,out y);
      if(arrVarifyCode[y] != Id.Substring(17,1).ToLower()) {
        return false;//校验码验证
      }
      return true;//符合GB11643-1999标准
    }
    /// <summary>
    /// 15位身份证验证
    /// </summary>
    /// <param name="Id">身份证号</param>
    /// <returns></returns>
    private static bool CheckIDCard15(string Id) {
      long n = 0;
      if(long.TryParse(Id,out n) == false || n < Math.Pow(10,14)) {
        return false;//数字验证
      }
      string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
      if(address.IndexOf(Id.Remove(2)) == -1) {
        return false;//省份验证
      }
      string birth = Id.Substring(6,6).Insert(4,"-").Insert(2,"-");
      DateTime time = new DateTime();
      if(DateTime.TryParse(birth,out time) == false) {
        return false;//生日验证
      }
      return true;//符合15位身份证标准
    }

    /// <summary>
    /// 获得星期
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string GetWeek(DateTime date) {
      string week = null;
      switch(date.DayOfWeek) {
        case DayOfWeek.Sunday:
        week = "星期天";
        break;
        case DayOfWeek.Monday:
        week = "星期一";
        break;
        case DayOfWeek.Tuesday:
        week = "星期二";
        break;
        case DayOfWeek.Wednesday:
        week = "星期三";
        break;
        case DayOfWeek.Thursday:
        week = "星期四";
        break;
        case DayOfWeek.Friday:
        week = "星期五";
        break;
        case DayOfWeek.Saturday:
        week = "星期六";
        break;
      }
      return week;
    }

    //public static string FormatNumber(decimal number,int count)
    //{ 
    //    System.Globalization.NumberFormatInfo GN = new System.Globalization.CultureInfo("zh-CN", false).NumberFormat;        
    //    GN.NumberDecimalDigits =count;
    //    return number.ToString("N", GN);
    //}

    public static string Base64Encode(string plainText) {
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
      return System.Convert.ToBase64String(plainTextBytes);
    }
    public static string Base64Decode(string base64EncodedData) {
      var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
      return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string Base64MD5(string base64MD5) {
      byte[] b = System.Text.Encoding.UTF8.GetBytes(base64MD5);
      b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
      var strBase64 = System.Convert.ToBase64String(b);
      return strBase64;
    }


    public static string GenerateSerialNumber() {
      return RPCNow.Year + "" + RPCNow.Month + "" + RPCNow.Day + ""
        + RPCNow.Hour + "" + RPCNow.Minute + "" + RPCNow.Second;
    }

    public static string PasswordMD5(string code) {
      string cl = code;
      string pwd = "";
      MD5 md5 = MD5.Create();//实例化一个md5对像
      // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
      byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
      // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
      for(int i = 0;i < s.Length;i++) {
        // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
        pwd = pwd + s[i].ToString("X");
      }
      return pwd;
    }

    //public static string PasswordMD5ToLower(string code) {
    //  return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(code,"MD5").ToLower();
    //}

    //public static void PushWechatMsg(string msg,string empCode) {
    //  string CorpID = Configuration.WeChatCorpID;
    //  string Secret = Configuration.WeChatSecret;
    //  var token = AccessTokenContainer.TryGetToken(CorpID,Secret,true);
    //  var result = MassApi.SendText(token,empCode,null,null,"3",msg,0);

    //}
  }
}
