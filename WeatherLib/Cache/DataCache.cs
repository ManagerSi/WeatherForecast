using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mobizone.TSIC.FOC.Cache {
  public class DataCache {
    public const int CahceTimeLong = 60 * 60;
    public const int CahceTimeForMasterData = 60 * 60;
    public const int CahceTime15Min = 15 * 60;
    public const int CahceTime30Min = 30 * 60;
    public const int CahceTimeDefault = -1;
    public const int CahceTimeShort = 5 * 60;
    public const int CacheTimeVeryShort = 60;
    
    protected static ICacheProvider cacheProvider = new HttpCacheProvider();

    static DataCache() {
      InitCahce(new MemoryCacheProvider());
    }

    public static void InitCahce(ICacheProvider provider) {
      cacheProvider = provider;
    }

    //protected static ICacheProvider cacheProvider = new MockCacheProvider();

    public static T GetOrDefault<T>(Func<string> keyFunc, Func<T> loadFunc, int lifeTimeSeconds = CahceTimeDefault, bool autoLock = true) where T : class {
      return GetOrDefault(keyFunc(), loadFunc, lifeTimeSeconds, autoLock);
    }

    // autoLock 防止Cache失效的时候，大量请求Cache对象，重复调用Load使得服务器彻底失效
    public static T GetOrDefault<T>(string key, Func<T> loadFunc, int lifeTimeSeconds = CahceTimeDefault, bool autoLock = true) where T : class {
      T obj = cacheProvider.GetCachedObject(key) as T;
      if (null == obj) {
        if (autoLock) {
          using(var l = cacheProvider.AcquireLock(key)) {
            // re read，多线程环境下，如果第一次读取失败
            // 每个线程都会call load Func，所以会引起内存失效崩溃
            // 现在的代码只会有一个线程call对应的loadFunc
            obj = cacheProvider.GetCachedObject(key) as T;
            if (null != obj) {
              return obj;
            }
            obj = loadFunc();
            cacheProvider.SetCachedObject(key, obj, lifeTimeSeconds);
          }
        } else {
          obj = loadFunc();
          cacheProvider.SetCachedObject(key, obj, lifeTimeSeconds);
        }
      }    
      return obj;
    }

    public static T Update<T>(Func<string> keyFunc, Func<T, T> updateFunc, int lifeTimeSeconds = CahceTimeDefault) where T : class {
      return Update(keyFunc(), updateFunc, lifeTimeSeconds);
    }

    public static T Update<T>(string key, Func<T, T> updateFunc, int lifeTimeSeconds = CahceTimeDefault) where T : class {
      T obj = cacheProvider.GetCachedObject(key) as T;
      obj = updateFunc(obj);
      cacheProvider.SetCachedObject(key, obj, lifeTimeSeconds);
      return obj;
    }

    public static void Remove(Func<string> keyFunc) {
      Remove(keyFunc());
    }
    public static void Remove(string key) {

      cacheProvider.RemoveCachedObject(key);
    }
    public static void RemoveWithPrefix(Func<string> keyFunc) {
      RemoveWithPrefix(keyFunc());
    }
    public static void RemoveWithPrefix(string prefix) {
      cacheProvider.RemoveCachedObjectWithPrefix(prefix);
    }

    public static string GetSessionID() {
      var ctx = HttpContext.Current;
      if (null == ctx) {
        throw new NotSupportedException();
      }
      return ctx.Session.SessionID + "_";
    }

    public static void RemoveWithPrefixBySession(Func<string> keyFunc) {
      RemoveWithPrefixBySession(keyFunc());
    }
    public static void RemoveWithPrefixBySession(string prefix) {
      cacheProvider.RemoveCachedObjectWithPrefix(GetSessionID() + prefix);
    }
    public static void RemoveAllCacheBySession() {
      cacheProvider.RemoveCachedObjectWithPrefix(GetSessionID());
    }

    public static void RemoveBySession(Func<string> keyFunc) {
      RemoveBySession(keyFunc());
    }
    public static void RemoveBySession(string key) {
      cacheProvider.RemoveCachedObject(GetSessionID() + key);
    }

    public static T GetOrDefaultBySession<T>(Func<string> keyFunc, Func<T> loadFunc, int lifeTimeSeconds = CahceTimeDefault) where T : class {
      return GetOrDefaultBySession(keyFunc(), loadFunc, lifeTimeSeconds);
    }

    public static T GetOrDefaultBySession<T>(string key, Func<T> loadFunc, int lifeTimeSeconds = CahceTimeDefault) where T : class {
      return GetOrDefault(GetSessionID() + key, loadFunc, lifeTimeSeconds, false);
    }

    public static object GetOrNull<T>(string key) where T : class {
      return cacheProvider.GetCachedObject(key);
    }
  }
}
