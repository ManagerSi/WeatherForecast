using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherForecast.Utility {
  /// <summary>
  /// 比较器，只比较keySelector出来的属性
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TKey"></typeparam>
  public class ProjectionComparer<TElement, TKey> : IComparer<TElement> {
    private readonly Func<TElement, TKey> keySelector;
    private readonly IComparer<TKey> comparer;

    public ProjectionComparer(Func<TElement, TKey> keySelector,
        IComparer<TKey> comparer = null) {
      this.keySelector = keySelector;
      this.comparer = comparer ?? Comparer<TKey>.Default;
    }

    public int Compare(TElement x, TElement y) {
      TKey keyX = keySelector(x);
      TKey keyY = keySelector(y);
      return comparer.Compare(keyX, keyY);
    }
  }
  /// <summary>
  /// 相等比较器，只比较keySelector出来的属性
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TKey"></typeparam>
  public class ProjectionEqualityComparer<TElement, TKey> : IEqualityComparer<TElement> {
    private readonly Func<TElement, TKey> keySelector;
    private readonly IEqualityComparer<TKey> comparer;

    public ProjectionEqualityComparer(Func<TElement, TKey> keySelector,
        IEqualityComparer<TKey> comparer = null) {
      this.keySelector = keySelector;
      this.comparer = comparer ?? EqualityComparer<TKey>.Default;
    }

    #region IEqualityComparer<TElement> 成员

    public bool Equals(TElement x, TElement y) {
      TKey keyX = keySelector(x);
      TKey keyY = keySelector(y);
      return comparer.Equals(keyX, keyY);
    }

    public int GetHashCode(TElement obj) {
      return comparer.GetHashCode(keySelector(obj));
    }

    #endregion
  }
}
