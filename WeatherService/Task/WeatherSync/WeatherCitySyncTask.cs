using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLib.BLL;
using WeatherLib.Model;

namespace WeatherService.Task.WeatherSync {
  /// <summary>
  /// 从本地文件同步城市文件，
  /// 网址：https://www.heweather.com/documents/city
  /// </summary>
  public class WeatherCitySyncTask : BaseSimpleTask {
    private static readonly ILog log = LogManager.GetLogger(typeof(WeatherCitySyncTask));
    public WeatherCitySyncTask()
      : base("WeatherCitySyncTask") {
    }

    public IWeatherCityBL bl;
    public Dictionary<string,WeatherCity> AllCityDict = new Dictionary<string,WeatherCity>();
    public Dictionary<string,decimal> KeyCityDict = new Dictionary<string,decimal>();
    /// <summary>
    /// 准备计算数据
    /// 加载一些外部数据，如商店名
    /// </summary>
    protected override void PrepareCompute() {
      base.PrepareCompute();
      bl = BLLFactory.Create<IWeatherCityBL>(UnitOfWork);
      AllCityDict = bl.GetAllCity().ToDictionary(i => i.CityCode.Trim(),i => i);
    }
    /// <summary>
    /// 计算完成，释放内存
    /// </summary>
    protected override void FinishCompute() {
      base.FinishCompute();
      AllCityDict = null;
      KeyCityDict = null;
    }

    protected override void DoTask() {
      var filePath = @"F:\ManagersiGitHub\WeatherForecast\DataBase\china-city-list\view-source_https___cdn.heweather.com_china-city-list.csv";
      OpenCSV(filePath);

      AllCityDict = bl.GetAllCity().ToDictionary(i => i.CityCode.Trim(),i => i);
      var topCity = AllCityDict.Values.Where(i => i.ParentID == null).ToList();
      var level = 1;
      UpdateLevel(topCity,level);
    }

    /// <summary>
    /// 更新层级
    /// </summary>
    /// <param name="updateCity">目标城市</param>
    /// <param name="level">目标层级</param>
    private void UpdateLevel(IEnumerable<WeatherCity> updateCity,int level) {

      List<WeatherCity> childrnCity = new List<WeatherCity>();
      foreach(var item in updateCity) {
        item.Level = level.ToString();
        childrnCity.AddRange(AllCityDict.Values.Where(i => i.ParentID == item.ID).ToList());
      }
      UpdateItems(UnitOfWork, updateCity);

      if(childrnCity.Count > 0) {
        UpdateLevel(childrnCity.ToList(),level + 1);
      }
    }


    public void OpenCSV(string filePath)//从csv读取数据返回table  
    {
      //filePath = @"F:\ManagersiGitHub\WeatherForecast\DataBase\china-city-list\view-source_https___cdn.heweather.com_china-city-list.csv";

      System.Text.Encoding encoding = GetType(filePath); //Encoding.ASCII;//  
      //DataTable dt = new DataTable();
      System.IO.FileStream fs = new System.IO.FileStream(filePath,System.IO.FileMode.Open,
          System.IO.FileAccess.Read);

      System.IO.StreamReader sr = new System.IO.StreamReader(fs,encoding);

      //记录每次读取的一行记录  
      string strLine = "";
      //记录每行记录中的各字段内容  
      string[] aryLine = null;
      string[] tableHead = null;
      //标示列数  
      int columnCount = 0;
      //标示是否是读取的第一行  
      bool IsFirst = true;
      //逐行读取CSV中的数据  
      while((strLine = sr.ReadLine()) != null) {
        if(IsFirst == true) {
          tableHead = strLine.Split('\t');
          IsFirst = false;
          columnCount = tableHead.Length;
          //创建列  
          for(int i = 0;i < columnCount;i++) {
            Console.WriteLine(tableHead[i]);
            //DataColumn dc = new DataColumn(tableHead[i]); //赋值给datatable
            //dt.Columns.Add(dc);
          }
        } else {
          aryLine = strLine.Split('\t');
          //DataRow dr = dt.NewRow();    //赋值给datarow
          //for(int j = 0;j < columnCount;j++) {
          //  dr[j] = aryLine[j];
          //}
          //dt.Rows.Add(dr);
          if(AllCityDict.ContainsKey(aryLine[0].Trim())) {
            //数据库已存在 -- 赋值给类
            var city = AllCityDict[aryLine[0].Trim()];
            city.CityCode = aryLine[0].Trim();
            city.NameEN = aryLine[1].Trim();
            city.Name_CN = aryLine[2].Trim();
            city.CountryCode = aryLine[3].Trim();
            city.CountryEN = aryLine[4].Trim();
            city.CountryCN = aryLine[5].Trim();
            city.ProvinceEN = aryLine[6].Trim();
            city.ProvinceCN = aryLine[7].Trim();
            city.ParentEN = aryLine[8].Trim();
            city.ParentCN = aryLine[9].Trim();
            if(KeyCityDict.ContainsKey(city.ParentCN)) {
              city.ParentID = KeyCityDict[city.ParentCN];
            } else if(KeyCityDict.ContainsKey(city.ProvinceCN)) {
              city.ParentID = KeyCityDict[city.ProvinceCN];
            }
            city.Latitude = aryLine[10].Trim();
            city.Longitude = aryLine[11].Trim();
            city.State = "1";
            //添加到数据库
            bl.Update(city);
            if(!KeyCityDict.ContainsKey(city.Name_CN)) {
              KeyCityDict.Add(city.Name_CN,city.ID);
            }
          } else {
            //赋值给类
            var city = new WeatherCity();
            city.CityCode = aryLine[0].Trim();
            city.NameEN = aryLine[1].Trim();
            city.Name_CN = aryLine[2].Trim();
            city.CountryCode = aryLine[3].Trim();
            city.CountryEN = aryLine[4].Trim();
            city.CountryCN = aryLine[5].Trim();
            city.ProvinceEN = aryLine[6].Trim();
            city.ProvinceCN = aryLine[7].Trim();
            city.ParentEN = aryLine[8].Trim();
            city.ParentCN = aryLine[9].Trim();
            if(KeyCityDict.ContainsKey(city.ParentCN)) {
              city.ParentID = KeyCityDict[city.ParentCN];
            } else if(KeyCityDict.ContainsKey(city.ProvinceCN)) {
              city.ParentID = KeyCityDict[city.ProvinceCN];
            }
            city.Latitude = aryLine[10].Trim();
            city.Longitude = aryLine[11].Trim();

            city.State = "1";
            //添加到数据库
            bl.Insert(city);
            if(!KeyCityDict.ContainsKey(city.Name_CN)) {
              KeyCityDict.Add(city.Name_CN,city.ID);
            }
          }
 
        }
      }
      //if(aryLine != null && aryLine.Length > 0) {
      //  dt.DefaultView.Sort = tableHead[0] + " " + "asc";
      //}
      sr.Close();
      fs.Close();
      //return dt;
    }
    /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型  
    /// <param name="FILE_NAME">文件路径</param>  
    /// <returns>文件的编码类型</returns>  

    public static System.Text.Encoding GetType(string FILE_NAME) {
      System.IO.FileStream fs = new System.IO.FileStream(FILE_NAME,System.IO.FileMode.Open,
          System.IO.FileAccess.Read);
      System.Text.Encoding r = GetType(fs);
      fs.Close();
      return r;
    }

    /// 通过给定的文件流，判断文件的编码类型  
    /// <param name="fs">文件流</param>  
    /// <returns>文件的编码类型</returns>  
    public static System.Text.Encoding GetType(System.IO.FileStream fs) {
      byte[] Unicode = new byte[] { 0xFF,0xFE,0x41 };
      byte[] UnicodeBIG = new byte[] { 0xFE,0xFF,0x00 };
      byte[] UTF8 = new byte[] { 0xEF,0xBB,0xBF }; //带BOM  
      System.Text.Encoding reVal = System.Text.Encoding.Default;

      System.IO.BinaryReader r = new System.IO.BinaryReader(fs,System.Text.Encoding.Default);
      int i;
      int.TryParse(fs.Length.ToString(),out i);
      byte[] ss = r.ReadBytes(i);
      if(IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF)) {
        reVal = System.Text.Encoding.UTF8;
      } else if(ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00) {
        reVal = System.Text.Encoding.BigEndianUnicode;
      } else if(ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41) {
        reVal = System.Text.Encoding.Unicode;
      }
      r.Close();
      return reVal;
    }

    /// 判断是否是不带 BOM 的 UTF8 格式  
    /// <param name="data"></param>  
    /// <returns></returns>  
    private static bool IsUTF8Bytes(byte[] data) {
      int charByteCounter = 1;　 //计算当前正分析的字符应还有的字节数  
      byte curByte; //当前分析的字节.  
      for(int i = 0;i < data.Length;i++) {
        curByte = data[i];
        if(charByteCounter == 1) {
          if(curByte >= 0x80) {
            //判断当前  
            while(((curByte <<= 1) & 0x80) != 0) {
              charByteCounter++;
            }
            //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　  
            if(charByteCounter == 1 || charByteCounter > 6) {
              return false;
            }
          }
        } else {
          //若是UTF-8 此时第一位必须为1  
          if((curByte & 0xC0) != 0x80) {
            return false;
          }
          charByteCounter--;
        }
      }
      if(charByteCounter > 1) {
        throw new Exception("非预期的byte格式");
      }
      return true;
    }  
  }
}
