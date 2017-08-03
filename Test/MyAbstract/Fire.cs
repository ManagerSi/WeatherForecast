using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract {
  public class Fire {

    public decimal? Temperature {
      get;
      set;
    }
  }
  public class FireAlarm:IObservable<Fire> {
    public List<Fire> Fires = new List<Fire>();
    public void AddFire(Fire f) {
      if(!Fires.Contains(f)) {
        Fires.Add(f);
        Update(f);
      }
    }


    #region IObservable
    private List<IObserver<Fire>> Observers = new List<IObserver<Fire>>();

    public List<IObserver<Fire>> ObserversList {
      get { return Observers; }
      set { 
        Observers = value;      
      }
    }


    public IDisposable Subscribe(IObserver<Fire> observer) {
      if(!ObserversList.Contains(observer)) {
        ObserversList.Add(observer);
      }
      return new unsubscribe(ObserversList,observer);
    }

    private class unsubscribe : IDisposable {
      private IObserver<Fire> _observer;
      private List<IObserver<Fire>> _observers;

      public unsubscribe(List<IObserver<Fire>> observers,IObserver<Fire> observer) {
        this._observer = observer;
        this._observers = observers;
      }

      public void Dispose() {
        if(this._observers != null && this._observers.Contains(this._observer)) {
          this._observers.Remove(this._observer);
        }
      }
    }

    public void Update(Fire f) {
      foreach(var item in ObserversList) {
        item.OnNext(f);
      }
    }
    #endregion

  }
}
