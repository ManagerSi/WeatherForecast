using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace WeatherForecast.Helper {
  public static class StringExtensions {
    public static MvcHtmlString ToHtmlContent(this string content) {
      return new MvcHtmlString(content.Replace("\n", "<br/>").Replace(" ", "&nbsp;"));
    }
  }
}
