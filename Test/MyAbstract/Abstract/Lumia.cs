using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract.Abstract {
  public class Lumia :BasePhone{
   
    public override string Brand() {
      return "品牌:Lumia";
    }
    public override string System() {
      return ("WP");
    }
    public override void Call() {
      Console.WriteLine("user {0},{1},{2} call",this.GetType().Name,this.Brand(),this.System());
    }

    public override void Photo() {
      Console.WriteLine("user {0},{1},{2} Photo",this.GetType().Name,this.Brand(),this.System());
    }
  }
}
