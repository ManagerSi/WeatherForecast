using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI.DB.Interface;

namespace SI.DB.MySql
{
    public class MySqlHelper:IDBHelper
    {
      public MySqlHelper() {
        Console.WriteLine("Mysqlhelper is created");
      }
      public void Query() {
        Console.WriteLine("Mysqlhelper is Querying");
      }
    }
}
