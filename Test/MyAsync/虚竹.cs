using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAsync {
  public class 虚竹 {
    public void 小和尚() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"小和尚");
    }
    public void 逍遥掌门() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"逍遥掌门");
    }
    public void 灵鹫宫宫主() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"灵鹫宫宫主");
    }
    public void 西夏驸马() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"西夏驸马");
    }
  }
}
