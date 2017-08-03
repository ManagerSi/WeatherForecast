using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest {
  class Program {
    static void Main(string[] args) {
      //Observer
      DesignPatternsTest.MyObserver.MyObserverProgram p = new DesignPatternsTest.MyObserver.MyObserverProgram();
      p.Main();
      ////SimpleFactory
      //var car = DesignPatternsTest.SimpleFactory.Driver.DriveCar("BMW");
      //car.Drive();

      ////Factory Method
      //var driver = new DesignPatternsTest.FactoryMethod.BenzDriver();
      //var benzcar = driver.DriverCar();
      //benzcar.Drive();
      //Console.ReadKey();
    }
  }
}
