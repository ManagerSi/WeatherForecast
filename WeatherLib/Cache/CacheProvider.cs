using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mobizone.TSIC.FOC.Cache {
  public interface ICacheProvider {
    object GetCachedObject(string key);
    // -1 表示使用默认值
    void SetCachedObject(string key, object obj, int lifeTimeSeconds = -1);
    void RemoveCachedObject(string key);
    void RemoveCachedObjectWithPrefix(string prefix);
    IDisposable AcquireLock(string key, TimeSpan? t = null);
  }
}
