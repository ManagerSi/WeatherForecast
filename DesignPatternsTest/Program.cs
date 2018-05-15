using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternsTest {
  class Program {
    static void Main(string[] args) {
      //Observer
      //DesignPatternsTest.MyObserver.MyObserverProgram p = new DesignPatternsTest.MyObserver.MyObserverProgram();
      //p.Main();
      ////SimpleFactory
      //var car = DesignPatternsTest.SimpleFactory.Driver.DriveCar("BMW");
      //car.Drive();

      ////Factory Method
      //var driver = new DesignPatternsTest.FactoryMethod.BenzDriver();
      //var benzcar = driver.DriverCar();
      //benzcar.Drive();

      //Singleton
      #region singleton
      {
        //Stopwatch sw = new Stopwatch();
        //sw.Start();
        //for(int i = 0;i < 100000000;i++) {
        //  Singleton.Singleton s = Singleton.Singleton.GetSingleton1();
        //}
        //sw.Stop();
        //Console.WriteLine("耗时：" + sw.ElapsedMilliseconds);
      }
      {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        //多线程测试
        List<Task> taskList = new List<Task>();
        TaskFactory taskFactory = new TaskFactory();
        for(int i = 0;i < 10;i++) {
          var task = taskFactory.StartNew(() => {
            Singleton.Singleton singleton = new Singleton.Singleton();
            Console.WriteLine("singleton :" + i + " threadid: " + Thread.CurrentThread.ManagedThreadId);
          });
          taskList.Add(task);
        }
        Task.WaitAll(taskList.ToArray());

        sw.Stop();
        Console.WriteLine("耗时：" + sw.ElapsedMilliseconds);
      }
      {
        Console.WriteLine("----------------------");
        Stopwatch sw = new Stopwatch();
        sw.Start();
        //多线程测试
        List<Task> taskList = new List<Task>();
        TaskFactory taskFactory = new TaskFactory();
        for(int i = 0;i < 10;i++) {
          var task = taskFactory.StartNew(() => {
            Singleton.Singleton singleton = Singleton.Singleton.GetSingleton3();
            Console.WriteLine("singleton :" + i + " threadid: " + Thread.CurrentThread.ManagedThreadId);
          });
          taskList.Add(task);
        }
        Task.WaitAll(taskList.ToArray());

        sw.Stop();
        Console.WriteLine("耗时：" + sw.ElapsedMilliseconds);
      }
      #endregion

      //Console.ReadKey();
    }
  }
}
