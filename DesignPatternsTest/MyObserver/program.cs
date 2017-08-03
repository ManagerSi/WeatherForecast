using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest.MyObserver {
  public class MyObserverProgram {
    public void Main() {
      Console.WriteLine("----------普通方法---------");
      Baby b = new Baby();
      b.Cry();
      //简要观察者
      Console.WriteLine("---------简要观察者----------");
      b.AddObserver(new Father());
      b.AddObserver(new Mother());
      b.AddObserver(new Cat());
      b.NewCry();

      Console.WriteLine("---------event----------");
      b.BabyCryHandler += new Father().Go;
      b.BabyCryHandler += new Mother().Hi;
      b.BabyCryHandler += new Cat().Run;
      b.BabyCryHandler += new Mouse().Hiden;

      b.CryEvent();

      Console.ReadKey();
    }
  }
}
