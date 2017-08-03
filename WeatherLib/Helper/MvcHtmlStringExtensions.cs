using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebPages;
using System.Web.Mvc;
using Mobizone.TSIC.Utility;
namespace WeatherForecast.Helper {
  public static class MvcHtmlStringExtensions {
    public static MvcHtmlString Replace(this MvcHtmlString str, string oldValue, string newValue) {
      return new MvcHtmlString(str.ToString().Replace(oldValue, newValue));
    }
    public static MvcHtmlString ReplaceThisWith(this MvcHtmlString str, string newValue) {
      return str.Replace(TSICConstant.ReplaceThisHolder, newValue);
    }
    //http://haacked.com/archive/2011/02/27/templated-razor-delegates.aspx
    public static MvcHtmlString ReplaceWithTemplate(this MvcHtmlString str, Func<dynamic, HelperResult> template) {
      return str.Replace("[replacethis]", template(null).ToString());
    }
  }
}
