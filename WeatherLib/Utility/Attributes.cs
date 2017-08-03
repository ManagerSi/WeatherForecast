using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherLib.Utility {
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)] 
  public class NonQueryUrlEncodeAttribute : Attribute {
  }
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)] 
  public class RPCAttribute:Attribute {
    
  }


}
