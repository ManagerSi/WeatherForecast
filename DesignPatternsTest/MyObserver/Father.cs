using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest.MyObserver {
  public class Father:IObserver {
    public void Go (){
      Console.WriteLine("{0},{1}",this.GetType(),"Go");
    }

    public void Notify() {
      this.Go();
    }
  }
}
