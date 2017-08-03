using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract {
  /// <summary>
  /// 口技
  /// </summary>
  public abstract class Ventriloquism:IObserver<Fire> {
    public string People { get; set; }
    public string Table { get; set; }
    public string Chair { get; set; }
    /// <summary>
    /// 扇子
    /// </summary>
    public string Fan { get; set; }
    /// <summary>
    /// 抚尺
    /// </summary>
    public string Gavel { get; set; }


    public void StartShow() {
      Console.WriteLine("{0}的StartShow",typeof(Ventriloquism));
    }
    /// <summary>
    /// 抽象方法 无实现，子类必须覆写
    /// </summary>
    public abstract void DogVoid();
    public abstract void PeopleVoid();
    public abstract void WindVoid();
    
    /// <summary>
    /// 开场白 虚方法子类可以不覆写
    /// </summary>
    public virtual void Prologue() {
      Console.WriteLine("{0}的开场白",typeof(Ventriloquism));
    }
    public virtual void Closing() {
      Console.WriteLine("{0}的结束语",typeof(Ventriloquism));
    }



    //public event Action TriggerEvent;
    //public virtual void BaginFire() { 
    //  //TriggerEvent();
    //  TriggerEvent.Invoke();
    //}

    public virtual void OnCompleted() {
      throw new NotImplementedException();
    }

    public void OnError(Exception error) {
      throw new NotImplementedException();
    }

    public virtual void OnNext(Fire value) {
      if(value.Temperature > 100) {
        Console.WriteLine("{0}的着火了",typeof(Ventriloquism));
      }
    }
    //保存订阅的新闻站点，方便以后取消订阅
    Dictionary<IObservable<Fire>,IDisposable> sbscribes = new Dictionary<IObservable<Fire>,IDisposable>();

    public void Subscribe(IObservable<Fire> fire) {
      IDisposable unsub = fire.Subscribe(this);
      sbscribes[fire] = unsub;
    }

    public void UnSubscribe(IObservable<Fire> fire) {
      if(sbscribes.ContainsKey(fire)) {
        sbscribes[fire].Dispose();
        sbscribes.Remove(fire);
      }
    }

  }
}
