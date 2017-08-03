using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAsync {
  public class 段誉 {
    public void 钟灵儿() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"钟灵儿");
    }
    public void 木婉清() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"木婉清");
    }
    public void 王语嫣() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"王语嫣");
    }
    public void 大理国王() {
      for(int i = 0;i < 10;i++) {
        System.Threading.Thread.CurrentThread.Join(1000);
      }
      Console.WriteLine("{0}-{1}",this.GetType(),"大理国王");
    }
  }
}
