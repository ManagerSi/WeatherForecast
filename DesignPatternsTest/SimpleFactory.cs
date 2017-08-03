using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest.SimpleFactory {
  /// <summary>
  /// 接口
  /// </summary>
  public interface Car {
    void Drive();
  }
  /// <summary>
  /// 具体实现
  /// </summary>
  public class Benz : Car {
    public void Drive() {
      Console.WriteLine("Drive Benz Car");
    }
  }
  public class BMW : Car {
    public void Drive() {
      Console.WriteLine("Drive BMW Car");
    }
  }
  public class Audi : Car {
    public void Drive() {
      Console.WriteLine("Drive Audi Car");
    }
  }
  /// <summary>
  /// 工厂
  /// </summary>
  public class Driver {
    public static Car DriveCar(string name) {
      if(name == "BMW") {
        return new BMW();
      }
      if(name == "Audi") {
        return new Audi();
      }
      if(name == "Benz") {
        return new Benz();
      }
      return null;
    }
  }

}
