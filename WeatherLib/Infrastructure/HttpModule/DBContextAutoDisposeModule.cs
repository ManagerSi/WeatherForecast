using WeatherLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WeatherLib.Infrastructure.HttpModule {
  public class DBContextAutoDisposeModule : IHttpModule {
    private HttpApplication _context;
    #region IHttpModule 成员

    public void Dispose() {
      _context = null;
    }

    public void Init(HttpApplication context) {
      _context = context;
      _context.PostRequestHandlerExecute += new EventHandler(PostRequestHandlerExecute);
    }

   public void PostRequestHandlerExecute(object sender, EventArgs e) {
      try {
        WeatherDBContext.TryDisposeCurrentHttpContext();
      } finally {

      }
      
    }
    #endregion
  }
}