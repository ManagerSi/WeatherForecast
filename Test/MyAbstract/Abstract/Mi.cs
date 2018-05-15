using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract.Abstract {
  public class Mi:BasePhone {
    public string Name { get; set; }
    public override string Brand() {
      return "品牌:小米";
    }
    public override string System() {
      return ("android");
    }
    public override void Call() {
      Console.WriteLine("user {0},{1},{2} call",this.GetType().Name,this.Brand(),this.System());
    }

    public override void Photo() {
      Console.WriteLine("user {0},{1},{2} Photo",this.GetType().Name,this.Brand(),this.System());
    }
  }
}
