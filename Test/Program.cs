using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTest.Generic;

using System.Reflection;//反射
using log4net.Config;
using TaskTest.MyAbstract;
using TaskTest.MyAsync;

namespace TaskTest {
  class Program {
    static void Main(string[] args) {
      //log4net.Config 初始化配置文件
      XmlConfigurator.Configure();

      #region 爬虫
      //京东商品
      //TaskTest.Crawler.DomeProgrom dp = new Crawler.DomeProgrom();
      //dp.domeMain();
      //国家统计局省市信息
      //TaskTest.Crawler.省市县数据抓取 sheng = new Crawler.省市县数据抓取();
      //sheng.Save();


      #endregion
      #region 反射
      ////反射获取信息、创建对象、调用方法
      ////程序编译后生成dll、pdb;反射通过调用dll获取元数据
      //Assembly assembly = Assembly.Load("log4net");//加载dll
      //foreach(Module module in assembly.GetModules()) {
      //  Console.WriteLine("名称:{0}",module.FullyQualifiedName);
      //}
      //foreach(Type type in assembly.GetTypes()) {
      //  Console.WriteLine("方法:{0}",type.FullName);//类
      //  foreach(var item in type.GetConstructors()) { //构造函数
      //    Console.WriteLine(item.Name);
      //  }
      //  foreach(var item in type.GetMethods()) {
      //    Console.WriteLine(item.Name);
      //  }
      //  foreach(var item in type.GetProperties()) {
      //    Console.WriteLine(item.Name);
      //  }
      //  foreach(var item in type.GetCustomAttributes()) {
      //    Console.WriteLine(item.ToString());
      //  }

      //}
      //MyReflact.MyReflact mr = new MyReflact.MyReflact();
      //mr.Show();
      #endregion
      #region 泛型
      ////-----common
      //Common.ShowDateTime(new DateTime());
      //Common.ShowObject(2M);
      ////-----泛型
      //GenericClass.ShowT(new DateTime());
      ////----约束
      //GenericConstraint.Get<People>(new People());
      ////GenericConstraint.Get("123");
      ////GenericConstraint.Get(new DateTime());

      //var p = new People() { Name = "123",Age = 5 };
      //GenericConstraint.SayHi(p);
      #endregion
      #region abstract
      //TaskTest.MyAbstract.StartProgram.startMain();     
      #endregion
      #region IO
      //TaskTest.MySerialize.MyIO.Show();
      #endregion 

      #region async 
      TaskTest.MyAsync.AsyncMain asy = new AsyncMain();
      //asy.Example();
      #endregion 

      Console.ReadKey();
    }
  }
}
