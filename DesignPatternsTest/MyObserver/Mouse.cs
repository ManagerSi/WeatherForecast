using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest.MyObserver {
  public class Mouse :IObserver{
    public void Hiden (){
      Console.WriteLine("{0},{1}",this.GetType(),"Hiden");
    }

    public void Notify() {
      this.Hiden();
    }
  }
}
