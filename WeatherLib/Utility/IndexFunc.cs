using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherForecast.Utility {
  /// <summary>
  /// 产生一个可以index访问的属性
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  public class IndexFunc<TKey, TValue> {
    protected Func<TKey, TValue> GetFunc;
    protected Action<TKey, TValue> SetFunc;
    public IndexFunc(Func<TKey, TValue> getFunc, Action<TKey, TValue> setFunc) {
      this.GetFunc = getFunc;
      this.SetFunc = setFunc;
    }
    public TValue this[TKey key] {
      get {
        return this.GetFunc(key);
      }
      set {
        this.SetFunc(key, value);
      }
    }
  }
}
