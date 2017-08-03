using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI.DB.Interface;
using SI.DB.MySql;
using SI.DB.SqlServer;
using System.Reflection;

namespace TaskTest.MyReflact {
  public class MyReflact {
    public void Show() {
      #region common
      {
        Console.WriteLine("*************************common**********");
        IDBHelper dbh = new MySqlHelper();
        dbh.Query();
      }
      #endregion
      #region 读取dll
      //{
      //  Console.WriteLine("*************************reflection**********");
      //  Assembly ass = Assembly.Load("SI.DB.MySql");//获取当前路径下的dll，不带后缀
      //  Assembly ass1 = Assembly.LoadFile(@"F:\ManagersiGitHub\WeatherForecast\Test\bin\Debug\SI.DB.MySql.dll");
      //  Assembly ass2 = Assembly.LoadFrom("SI.DB.MySql.dll");

      //  Type dbType = ass.GetType("SI.DB.MySql.MySqlHelper");
      //  object odbhelper = Activator.CreateInstance(dbType);
      //  IDBHelper idbhelper = odbhelper as MySqlHelper;
      //}
      #endregion
      #region 简单工厂
      {
        IDBHelper idbh = SimpleFactory.CreateInstance();
        idbh.Query();
      }

      #endregion

    }
  }
}
