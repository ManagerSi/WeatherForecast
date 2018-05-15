using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTest.MyAbstract.Abstract;

namespace TaskTest.MyAbstract {
  public  class StartProgram {
    public static void startMain() {
      East east = new East() {
        Age = 123,
        EastName = "east"
      };
      North north = new North() {
        Age = 123,
        NorthName = "North"
      };
      TaskTest.MyAbstract.StartProgram.Display(east);
      TaskTest.MyAbstract.StartProgram.Display(north);

      FireAlarm fireAlarm = new FireAlarm();
      fireAlarm.Subscribe(east);
      fireAlarm.Subscribe(north);

      fireAlarm.AddFire(new Fire() { Temperature = 100 });

    }
    public static void Display<T>(T t) where T : Ventriloquism,IPay {
      Type type = t.GetType();
      Console.WriteLine("------打印字段----------");
      foreach(var item in type.GetFields()) {
        Console.WriteLine("{0}={1}",item.Name,item.GetValue(t));
      } 
      Console.WriteLine("------打印属性----------");
      foreach(var item in type.GetProperties()) {
        Console.WriteLine("{0}={1}",item.Name,item.GetValue(t));
      }

      t.StartShow();
      t.Prologue();
      t.DogVoid();
      t.PeopleVoid();
      t.WindVoid();
      t.Closing();

      t.Pay();

    }



    public static void startMain1() {
      Student st = new Student() { Id = 123,Name = "张三" };
      {
        BasePhone mi = new Mi();
        st.PlayPhone(mi);
      }
      {
        IExcent iphone = new Iphone();
        //st.PlayPhone(iphone);
      }
      {
        BasePhone b = new Iphone();
        b.Show();//普通方法是由编译时类型决定  调用父类
        b.ShowVirtual();//虚方法是由运行时类型决定的 调用子类
        b.Call();//抽象也是 调用子类
      }
    }
  }
}
