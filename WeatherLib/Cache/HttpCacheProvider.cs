using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Magnum.Concurrency.Internal;
using Mobizone.TSIC.FOC.Utility;
namespace Mobizone.TSIC.FOC.Cache {
  public class HttpCacheProvider : ICacheProvider {
    #region ICacheProvider 成员
    protected const string kBaseKey = "_hc_";
    protected const int kDefaultMaxlifeTimeSeconds = 10 * 60;
    protected const int kTimeout = 1;

    public object GetCachedObject(string key) {
      var cache = HttpRuntime.Cache;
      if (null == cache) {
        throw new NotSupportedException();
      }
      return cache[kBaseKey + key];
    }

    public void SetCachedObject(string key, object obj, int lifeTimeSeconds = -1) {
      if (obj == null) {
        RemoveCachedObject(key);
        return;
      }
      var cache = HttpRuntime.Cache;
      if (null == cache) {
        throw new NotSupportedException();
      }
      if (lifeTimeSeconds < 0) {
        lifeTimeSeconds = kDefaultMaxlifeTimeSeconds;
      }

      if (lifeTimeSeconds > 60) {
        // 随机扰动，防止cahce同时失效
        var rnd = new Random();
        lifeTimeSeconds += rnd.Next(lifeTimeSeconds/10);
      }

      // cur.Cache[kBaseKey + key] = obj;
      cache.Insert(kBaseKey + key, obj, null,
        Util.RPCNow.AddSeconds(lifeTimeSeconds),
        //TimeSpan.FromMinutes(kTimeout),
        TimeSpan.Zero,
        CacheItemPriority.Normal, null);
    }

    public void RemoveCachedObject(string key) {
      var cache = HttpRuntime.Cache;
      if (null == cache) {
        throw new NotSupportedException();
      }
      cache.Remove(kBaseKey + key);
    }

    public void RemoveCachedObjectWithPrefix(string prefix) {
      var keys = new List<string>();
      var cache = HttpRuntime.Cache;
      if (null == cache) {
        throw new NotSupportedException();
      }
      var iter = cache.GetEnumerator();
      while (iter.MoveNext()) {
        string key = iter.Key.ToString();
        if (key.StartsWith(kBaseKey + prefix)) {
          keys.Add(key);
        }
      }
      foreach (var key in keys) {
        cache.Remove(key);
      }
    }

    public class Lock : IDisposable {
      public Lock(object locker, Action action) {
        this.locker = locker;
        this.postAction = action;
        Monitor.Enter(locker);
      }

      protected object locker;
      protected Action postAction;

      public void Dispose() {
        Monitor.Exit(locker);
        if (postAction != null)postAction();
      }
    }


    public IDisposable AcquireLock(string key, TimeSpan? t = null) {
      Tuple<object, AtomicInt32> pair;
      lock (lockers) {
        if (!lockers.TryGetValue(key, out pair)) {
          lockers[key] = pair = Tuple.Create(new object(), new AtomicInt32(0));
        }
        pair.Item2.Set(i => i + 1);
      }

      return new Lock(pair.Item1, () => {
        lock (lockers) {
          pair.Item2.Set(i => i - 1);
          if (pair.Item2.Value == 0) {
            lockers.Remove(key);
          }
        }
      });
    }

    protected Dictionary<string, Tuple<object, AtomicInt32>> lockers =
      new Dictionary<string, Tuple<object, AtomicInt32>>();

    #endregion
  }
}

