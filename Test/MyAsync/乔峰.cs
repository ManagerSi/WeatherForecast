using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAsync {
  public class 乔峰 {
    public void 丐帮帮主() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"丐帮帮主");
    }
    public void 契丹人() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"契丹人");
    }
    public void 南院大王() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"南院大王");
    }
    public void 挂印离开() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"挂印离开");
    }
  }
}
