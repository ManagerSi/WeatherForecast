using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherLib.Infrastructure.Factory;
namespace WeatherLib.Web.SessionState {
  public class SessionFactory : InterfaceFactory {
    static SessionFactory() {
      RegisterSession("IBLSessionPersisiter", typeof(IBLSessionPersisiter), typeof(BLSessionPersisiter));
      RegisterSession("IClientSessionPersisiter", typeof(IClientSessionPersisiter), typeof(ClientSessionPersisiter));
    }

    public static  void  RegisterSession(string SessionName,Type ISession, Type Session) {
      Sessions.Add(ISession, Session);
      SessionsByName.Add(SessionName, Session);
    }
    protected static Dictionary<Type, Type> Sessions = new Dictionary<Type, Type>();
    protected static Dictionary<string, Type> SessionsByName = new Dictionary<string, Type>();
    public static I Create<I>(params object[] args) where I : class,ISessionPersisiter {
      return CreateNewFromDict<I>(Sessions, args);
    }
    public static object CreateByName(string name, params object[] args) {
      return CreateNewFromDictByName(SessionsByName, name, args);
    }
  }
}
