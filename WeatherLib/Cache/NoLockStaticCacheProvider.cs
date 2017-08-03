using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobizone.TSIC.FOC.Utility;
namespace Mobizone.TSIC.FOC.Cache {
  public class NoLockStaticCacheProvider : ICacheProvider {
    //http://msdn.microsoft.com/en-us/library/system.runtime.caching.memorycache.aspx
    static Dictionary<string, Tuple<DateTime, object>> cache = new Dictionary<string, Tuple<DateTime, object>>();
    #region ICacheProvider 成员
    static TimeSpan kCacheTimeLog = TimeSpan.FromMinutes(30);
    public object GetCachedObject(string key) {
      if (!cache.ContainsKey(key)) {
        return null;
      }
      var obj = cache[key];
      if (Util.RPCNow - obj.Item1 > kCacheTimeLog) {
        cache.Remove(key);
        return null;
      }
      return obj.Item2;
    }

    public void SetCachedObject(string key, object obj, int lifeTimeSeconds = -1) {
      cache[key] = Tuple.Create(Util.RPCNow, obj);
    }

    public void RemoveCachedObject(string key) {
      cache.Remove(key);
    }

    public void RemoveCachedObjectWithPrefix(string prefix) {
      foreach (var key in cache.Keys.ToList()) {
        if (key.StartsWith(prefix)) {
          RemoveCachedObject(key);
        }
      }
    }

    public IDisposable AcquireLock(string key, TimeSpan? t = null) { return null; }

    #endregion
  }
}
