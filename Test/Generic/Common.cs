using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.Generic {
  public class Common {

    public static void ShowDateTime(DateTime dtParameter) {
      Console.WriteLine("这里是{0},parameter={1},parameterType={2}",typeof(Common).Name, dtParameter,dtParameter.GetType());
    }

    public static void ShowObject(object oParameter) {
      Console.WriteLine("这里是{0},parameter={1},parametertype={2}",typeof(Common).Name,oParameter,oParameter.GetType());
    }
  }

  
}
