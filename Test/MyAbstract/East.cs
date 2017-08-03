using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract {
  public  class East : Ventriloquism,IPay{
    public string EastName { get; set; }
    public int Age;

    /// <summary>
    /// 开场白
    /// </summary>
    //public override void Prologue() {
    //  Console.WriteLine("{0}的开场白",typeof(North));
    //}
    //public override void Closing() {
    //  Console.WriteLine("{0}的结束语",typeof(North));
    //}
    public override void DogVoid() {
      Console.WriteLine("{0}的犬吠",typeof(East));
    }
    public override void PeopleVoid() {
      Console.WriteLine("{0}的人声",typeof(East));
    }
    public override void WindVoid() {
      Console.WriteLine("{0}的风声",typeof(East));
    }

    public override void Prologue() {
      Console.WriteLine("{0}的开场白",typeof(East));
      base.Prologue();
    }
    
    public void Pay() {
      Console.WriteLine("{0}的收钱",typeof(East));
    }
    public override void OnNext(Fire value) {
      if(value.Temperature > 120) {
        //BaginFire();
        Console.WriteLine("{0}的着火了",typeof(East));
      }
    }

  }
}
