using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.Generic {
  /// <summary>
  /// 泛型实际上是语法糖，由编译器提供的功能
  /// </summary>
  public class GenericClass {
    public static void ShowT<SPara>(SPara tParameter) {
      Console.WriteLine("这里是GenericClass.ShowT,parameter={0},parameterType={1}",tParameter,tParameter.GetType());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <param name="t"></param>
    /// <param name="s"></param>
    /// <param name="w"></param>
    /// <param name="iParameter"></param>
    /// <param name="sParameter"></param>
    private static void Show<T,S,W>(T t,S s,W w,int iParameter,string sParameter) {

    }
    /// <summary>
    /// 泛型返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static T Get<T>(T t) {
      return default(T);//会根据T的类型，会产生一个默认值
    }

    public class SomeGenericClass<W,S,T> {//泛型类
    }
    public interface GenericInterface<T,W> {//泛型接口
    }
    public delegate T GetDelete<T>();//泛型委托
  }


}
