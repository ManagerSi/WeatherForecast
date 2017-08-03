using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace WeatherLib.Serialization {
  public class JsonHelper {
    public static string JsonSerializer<T>(T t) {
      return Newtonsoft.Json.JsonConvert.SerializeObject(t);
    }

    public static T JsonDeserialize<T>(string jsonString) {     
      return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
    }
  }
}
