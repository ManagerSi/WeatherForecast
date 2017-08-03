using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest.MyObserver {
  public class Mother:IObserver {
    public void Hi (){
      Console.WriteLine("{0},{1}",this.GetType(),"Hi");
    }

    public void Notify() {
      this.Hi();
    }
  }
}
