using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract {
  public class North :Ventriloquism,IPay{
    public string NorthName { get; set; }
    public int Age;

    public override void DogVoid() {
      Console.WriteLine("{0}的犬吠",typeof(North));
    }
    public override void PeopleVoid() {
      Console.WriteLine("{0}的人声",typeof(North));
    }
    public override void WindVoid() {
      Console.WriteLine("{0}的风声",typeof(North));
    }
    public override void Closing() {
      Console.WriteLine("{0}的结束语",typeof(North));
    }
    public void Pay() {
      Console.WriteLine("{0}的收钱",typeof(North));
    }

    public override void OnNext(Fire value) {
      if(value.Temperature > 70) {
        //BaginFire();
        Console.WriteLine("{0}的着火了",typeof(North));
      }
    }
  }
}
