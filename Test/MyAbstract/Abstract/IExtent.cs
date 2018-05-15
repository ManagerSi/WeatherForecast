using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract.Abstract {
  /// <summary>
  /// 接口不是一个类，里面可以包含属性、事件、方法   不能包含委托 字段
  /// 接口只能包含没有实现的方法，默认为public
  /// 继承接口后（可继承多个接口），必须实现成员
  /// 接口不能直接实例化，
  /// </summary>
  public interface IExcent {
     int Id { get; set; }
     //string Name = "123";
    // delegate void DoNothing();
     event Action DoNothingEvent;
     string brand { get; set; }

  
    void Game();
   
  }
}
