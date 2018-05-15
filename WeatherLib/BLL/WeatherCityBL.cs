using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLib.Model;
using WeatherLib.Security;
using WeatherLib.Utility;

namespace WeatherLib.BLL {

  public partial interface IWeatherCityBL {
    IQueryable<WeatherCity> GetAllCity();
  }

  public partial class WeatherCityBL {
    public IQueryable<WeatherCity> GetAllCity() {
      return Repository.Get(item => item.State == "1");//.AsNoTracking();
    }

  }
}
