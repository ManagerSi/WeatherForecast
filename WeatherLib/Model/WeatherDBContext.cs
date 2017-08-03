using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.EntityClient;
using System.Data.Common;
using System.Transactions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace WeatherLib.Model
{
    public partial class WeatherDBContext : DbContext
    {
    public WeatherDBContext(EntityConnection cnt)
      : base(cnt, true) {
    }

    public WeatherDBContext(string nameOrContectingString)
      : base(nameOrContectingString) {
    }

    public override int SaveChanges() {
      return base.SaveChanges();
    }

    #region 单实例 (Per request)
    private const string kDBContextKEY = "weather_ctx";
    private const string kDBTranscationKEY = "weather_trans";



    static public WeatherDBContext CurrentHttpContext
    {
      get {

        HttpContext context = HttpContext.Current;
        if (context == null) {
          throw new ApplicationException("There is no Http Context available");
        }
        lock (context) {
            WeatherDBContext cur = CurrentHttpContextWeak;
          if (cur == null) {
            cur = NewContext(false);
            CurrentHttpContextWeak = cur;
          }
          return cur;
        }
      }
    }

    public static WeatherDBContext NewContext(bool open)
    {
        var cnt = new EntityConnection("name=WeatherDBContext");
      if (open) {
        cnt.Open();
      }
      return new WeatherDBContext(cnt);
    }

    static private WeatherDBContext CurrentHttpContextWeak
    {
        get
        {
            return HttpContext.Current.Items[kDBContextKEY] as WeatherDBContext;
        }
      set { HttpContext.Current.Items[kDBContextKEY] = value; }
    }

    static internal void TryDisposeCurrentHttpContext() {
      HttpContext context = HttpContext.Current;
      if (context == null) {
        throw new ApplicationException("There is no Http Context available");
      }
      lock (context) {
          WeatherDBContext cur = CurrentHttpContextWeak;
        if (cur != null) {
          //cur.Database.Connection.Close();
          cur.Dispose();
          CurrentHttpContextWeak = null;
          
          
        }
      }
    }
    public void SetCommandTimeout(int? timeOut) {
      var objectContext = (this as IObjectContextAdapter).ObjectContext;
      // Sets the command timeout for all the commands
      objectContext.CommandTimeout = timeOut;
    }
    public int? GetCommandTimeout() {
      var objectContext = (this as IObjectContextAdapter).ObjectContext;
      // Gets the command timeout for all the commands
      return objectContext.CommandTimeout;
    }
    #endregion
  }
}