using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WeatherLib.Model;
namespace WeatherLib.Web.SessionState {
  public interface ISessionPersisiter {
    bool RemoveSession();
  }
  public abstract class SessionPersisiter : ISessionPersisiter {

    protected T GetValue<T>(string key, T defaultValue) {
      if (HttpContext.Current == null) {
        return defaultValue;
      }
      if (HttpContext.Current.Session == null) {
        return defaultValue;
      }
      object obj = HttpContext.Current.Session[key];
      if (null == obj) {
        return defaultValue;
      }
      return (T)obj;
    }

    protected void SetValue<T>(string key, T value) {
      if (HttpContext.Current == null) {
        throw new InvalidOperationException();
      }
      HttpContext.Current.Session.Add(key, value);
    }

    public abstract bool RemoveSession();



  }
}
