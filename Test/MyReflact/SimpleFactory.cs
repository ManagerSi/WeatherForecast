using SI.DB.Interface;
using SI.DB.MySql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyReflact {
  public class SimpleFactory {
    private static string IDBHelperConfig = ConfigurationManager.AppSettings["MySql"];
    private static string DllName = IDBHelperConfig.Split(',')[1];
    private static string TypeName = IDBHelperConfig.Split(',')[0];

    public static IDBHelper CreateInstance() {
      Assembly ass = Assembly.Load(DllName);//获取当前路径下的dll，不带后缀
   
      Type dbType = ass.GetType(TypeName);
      object odbhelper = Activator.CreateInstance(dbType);
      IDBHelper idbhelper = odbhelper as MySqlHelper;
      return idbhelper;
    }

  }
}
