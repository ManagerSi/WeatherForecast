using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.Generic {
  /// <summary>
  /// 泛型约束
  /// </summary>
  public class GenericConstraint {
    public static T Get<T>(T t) 
      where T : class,new() { // 泛型约束,必须是引用类型，同时要有个无参的构造函数（string没有无参的构造函数）
      return default(T);
    }

    public static T GetPeople<T>(T p) where T : People {
      return default(T);
    }
    public static void SayHi<P>(P p) where P : People,new() {
      p.SayHi();
    }
  }
}
