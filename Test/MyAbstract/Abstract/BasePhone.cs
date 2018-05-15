using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract.Abstract {
  /// <summary>
  /// 抽象类是一个类，里面可以包含类一切可以包含的
  /// 抽象成员，必须包含在抽象类中
  /// 继承抽象类后，必须显示override其抽象成员
  /// 抽象类不能直接实例化，声明的对象只能使用抽象类里的方法 ，不能用子类新增的方法
  /// </summary>
  public abstract class BasePhone {
    public int Id { get; set; }
    public string Name = "123";
    public delegate void DoNothing();
    public event DoNothing DoNothingEvent;
    public string brand { get; set; }
    /// <summary>
    /// 不能被override
    /// </summary>
    public void Show() {
      Console.WriteLine("Show");
    }
    /// <summary>
    /// 重载
    /// </summary>
    /// <param name="abc"></param>
    public void Show(string abc) {
      Console.WriteLine("Show" + abc);
    }

    public void Show(string abc,decimal id=0) {
      Console.WriteLine("Show" + abc);
    }
    /// <summary>
    /// 虚方法,必须包含实现，但可以被覆写 override
    /// </summary>
    public virtual void ShowVirtual() {
      Console.WriteLine("ShowVirtual");
    }
    /// <summary>
    /// 抽象方法，没有实现，必须被覆写override
    /// </summary>
    /// <returns></returns>
    public abstract string Brand();
    public abstract string System();
    public abstract void Call();
    public abstract void Photo();
  }
}
