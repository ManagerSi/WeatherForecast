using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mobizone.TSIC.FOC.Cache {
  public class MockCacheProvider : ICacheProvider{
    #region ICacheProvider 成员

    public object GetCachedObject(string key) {
      return null;
    }

    public void SetCachedObject(string key, object obj, int lifeTimeSeconds) {
      
    }

    public void RemoveCachedObject(string key) {
      
    }

    public void RemoveCachedObjectWithPrefix(string prefix) {
      
    }

    public IDisposable AcquireLock(string key, TimeSpan? t = null) { return null; }

    #endregion
  }
}
