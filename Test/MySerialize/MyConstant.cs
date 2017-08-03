using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MySerialize {
  public class MyConstant {
    public static string FilePath = ConfigurationManager.AppSettings["FilePath"];
    public static string FileMovePath = ConfigurationManager.AppSettings["FileMovePath"];
 
  }
  /// <summary>
  /// 命名空间下的全局枚举
  /// </summary>
  public enum TSICClientType {
    Undefined = 0,
    Unknown = 1,
    Wap = 2,
    Web = 4,
    iPhone = 8,
    Android = 16,
    WeChat = 24
  }

}
