using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLib.Model {
  public class BaseRequest {
    public string key { get; set; }
    public string Function { get; set; }
    public string city { get; set; }
    public string lang { get; set; }
  }

  public enum WeatherFunction {
    weather = 1,//全部天气
    forecast = 2,//3-10天预报
    now = 3,//实况天气
    hourly=4,
    suggestion=5,
    scenic = 6,//全国景点天气
    alarm = 7,//灾害预警-
    historical = 8,//历史天气-


  }
}
