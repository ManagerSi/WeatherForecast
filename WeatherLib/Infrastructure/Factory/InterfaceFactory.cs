using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherLib.Infrastructure.Factory {
  public class InterfaceFactory {
    public static IType CreateNewFromDict<IType>(Dictionary<Type, Type> typeDict, params object[] args) where IType : class {
      Type iType = typeof(IType);
      if (!typeDict.ContainsKey(iType)) {
        return null;
      }
      Type type = typeDict[iType];
      return Activator.CreateInstance(type, args) as IType;
    }
    public static object CreateNewFromDictByName(Dictionary<string, Type> typeDict, string typeName, params object[] args) {
      if (!typeDict.ContainsKey(typeName)) {
        return null;
      }
      Type type = typeDict[typeName];
      return Activator.CreateInstance(type, args);
    }
  }
}
