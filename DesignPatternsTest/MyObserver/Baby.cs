using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest.MyObserver {
  public class Baby {
    public void Cry(){
      //正常情况
      Console.WriteLine("{0},{1}",this.GetType(),"cry");
      new Father().Go();
      new Mother().Hi();
      new Mouse().Hiden();
      new Cat().Run();
    }

    private List<IObserver> observerList = new List<IObserver>();
    public void AddObserver(IObserver observer) {
      observerList.Add(observer);
    }
    public void NewCry() {
      Console.WriteLine("{0},{1}",this.GetType(),"cry");
      if(observerList != null && observerList.Count > 0) {
        foreach(var item in observerList) {
          //item.Notify();同步调用
          //异步调用
          new Action(item.Notify).BeginInvoke(null,null);
        }
      }
    }

    //第三种方法
    public event Action BabyCryHandler;
    public void CryEvent() {
      Console.WriteLine("{0},{1}",this.GetType(),"CryEvent");
      if(BabyCryHandler != null) {
        //BabyCryHandler();
        BabyCryHandler.Invoke();
      }
    }
  }
}
