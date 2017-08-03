using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLib.Utility {
  public enum WeatherClientType {
    Undefined = 0,
    Unknown = 1,
    Wap = 2,
    Web = 4,
    iPhone = 8,
    Android = 16,
    WeChat = 24
  }
}
