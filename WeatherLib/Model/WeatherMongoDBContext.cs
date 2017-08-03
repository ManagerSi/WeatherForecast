#if USING_MONGODB //项目属性-生成-设置if的条件
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Configuration;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Bson;
//using Mobizone.TSIC.Utility;

namespace WeatherForecast.Models {
  public partial class TSICMongoDocContext {

    public const string kMongDBCntString = "MongoDBCnt";
    public static string kDBName = "ManagerSi";

    static TSICMongoDocContext() {

      //DateTimeSerializationOptions.Defaults = DateTimeSerializationOptions.LocalInstance;
      //var dateTimeSerializer = new DateTimeSerializer(DateTimeSerializationOptions.LocalInstance);已过时
        var dateTimeSerializer = new DateTimeSerializer(DateTimeKind.Local);
      BsonSerializer.RegisterSerializer(typeof(DateTime), dateTimeSerializer);
      /* var myConventions = new ConventionProfile();
       myConventions.SetIgnoreIfDefaultConvention(new AlwaysIgnoreIfDefaultConvention());
       //myConventions.SetIgnoreIfNullConvention(new AlwaysIgnoreIfNullConvention());
       BsonClassMap.RegisterConventions(myConventions, (type) => true);*/
    }

    static MongoClient client = null;
    public static void InitClient() {
        var dbName = System.Configuration.ConfigurationManager.AppSettings["MongoDBName"].ToString();
      if (!string.IsNullOrEmpty(dbName)) {
        kDBName = dbName;
      }
      var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[kMongDBCntString].ConnectionString;
      client = new MongoClient(connectionString);
      Server();
    }


    private static MongoServer Server() {
      return client.GetServer(); 
        //提示已过时，使用
        //client = new MongoClient(connectionString);
        //client.GetDatabase(kDBName);
    }



    #region 单实例 (Per request)
    private const string kDBContextKEY = "weather_mg_db";

    static public MongoDatabase CurrentDB {
      get {
        HttpContext context = HttpContext.Current;
        if (context == null) {
          throw new ApplicationException("There is no Http Context available");
        }
        lock (context) {
          MongoDatabase cur = CurrentDatabaseWeak;
          if (cur == null) {
            cur = ConnectDB();
            CurrentDatabaseWeak = cur;
          }
          return cur;
        }
      }
    }

    public static MongoDatabase ConnectDB() {
        //return (MongoDatabase)client.GetDatabase(kDBName);
      return Server().GetDatabase(kDBName);
    }

    static private MongoDatabase CurrentDatabaseWeak {
      get { return HttpContext.Current.Items[kDBContextKEY] as MongoDatabase; }
      set { HttpContext.Current.Items[kDBContextKEY] = value; }
    }

    static internal void TryDisposeCurrentHttpContext() {
      HttpContext context = HttpContext.Current;
      if (context == null) {
        throw new ApplicationException("There is no Http Context available");
      }
      lock (context) {
        MongoDatabase cur = CurrentDatabaseWeak;
        if (cur != null) {
          //cur.Dispose();
          CurrentDatabaseWeak = null;
        }
      }
    }
    #endregion


  }
}
#endif