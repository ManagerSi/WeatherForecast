using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternsTest.Singleton {
  public class Singleton {
    private long lResult = 0;
    #region _singleton1
    private static Singleton _singleton1 = null;
    private Singleton(int a) {
      for(int i = 0;i < 1000000000;i++) {
        lResult += i;
      }
        Console.WriteLine("初始化singleton");
    }
    public static Singleton GetSingleton1() {
      if(_singleton1 != null) {
        _singleton1 = new Singleton(1);
      }
      return _singleton1;
    }
    #endregion


    #region _singleton2 单线程
    private static bool IsInit = false;
    public Singleton() {
      if (IsInit == false){
        IsInit = true;
        for(int i = 0;i < 1000000000;i++) {
          lResult += i;
        }
        Console.WriteLine("初始化singleton，threadid=" + Thread.CurrentThread.ManagedThreadId);
      } else {
        Console.WriteLine("已初始化singleton，threadid=" + Thread.CurrentThread.ManagedThreadId);
      }

    }
    #endregion

    #region _singleton3 终极版
    private static object _lock = new object();
    public static Singleton GetSingleton3(){
      if(_singleton1 == null) {
        lock(_lock) {
          if(IsInit == false) {
            _singleton1 = new Singleton();
          }
        }
      }    
      return _singleton1;
    }
    #endregion
    #region 静态构造函数
    private static Singleton _singleton4 = null;
    static Singleton() {
      _singleton4 = new Singleton();
      
    }
    public static Singleton GetSingleton4() {
      return _singleton4;
    }
    #endregion
    #region 静态属性
    private static Singleton _singleton5 = new Singleton();
      public static Singleton GetSingleton5() {
      return _singleton5;
    }
    #endregion
  }
}
