using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract.Abstract {
  public class Iphone:BasePhone ,IExcent{
   
    public override string Brand() {
      return "品牌:Iphone";
    }
    public override string System() {
      return ("IOS");
    }
    /// <summary>
    /// 子方法和父类方法一样时，要用new 隐藏父类方法，不然会有提醒（可忽略）
    /// 隐藏父类方法不推荐，不要重写
    /// 普通方法是由编译时类型决定  
    /// </summary>
    public new void Show() {
      Console.WriteLine("iphone show");
    }
    /// <summary>
    /// 虚方法是由运行时类型决定的 调用子类
    /// </summary>
    public override void ShowVirtual() {
      Console.WriteLine("iphone ShowVirtual");
    }
    public override void Call() {
      Console.WriteLine("user {0},{1},{2} call",this.GetType().Name,this.Brand(),this.System());
    }

    public override void Photo() {
      Console.WriteLine("user {0},{1},{2} Photo",this.GetType().Name,this.Brand(),this.System());
    }
    public void Game (){
      Console.WriteLine("user {0},{1},{2} play Game",this.GetType().Name,this.Brand(),this.System());
    }


    public new event Action DoNothingEvent;
  }
}
