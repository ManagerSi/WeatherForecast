using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Magnum.Cryptography.PKI;

namespace WeatherForecast.Utility {
  public class MPUrl {

    protected string Host { get; set; }

    public MPUrl(string host)
    {
      Host = host;
    }
    /// <summary>
    /// OAuth对应Url
    /// </summary>
    /// <returns></returns>
    public string OAuthCallbackUrl() {
      return string.Format("http://{0}/OAuthCallback/Index", Host);
    }

    public string AritcleViewUrl(string id, int? sub = null) {
      return string.Format("http://{0}/W/Article/V/{1}{2}", Host, id, sub.HasValue ? "?sub=" + sub : "");
    }
  }
}
