using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest.FactoryMethod {
  public interface Car {
    void Drive();
  }
  public class Benz : Car {
    public void Drive() {
      Console.WriteLine("Drive Benz Car");
    }
  }
  public class Audi : Car {
    public void Drive() {
      Console.WriteLine("Drive Audi Car");
    }
  }

  public interface Driver {
    Car DriverCar();
  }
  public class BenzDriver :Driver{
    public Car DriverCar() {
      return new Benz();
    }
  }
  public class AudiDriver : Driver {
    public Car DriverCar() {
      return new Audi();
    }
  }
}
