using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mobizone.TSIC.DAL;
using Mobizone.TSIC.Models;

namespace Mobizone.TSIC.Linq {
  public class CascadedDeleter<T> where T : class, IBaseDataModel {
    public CascadedDeleter() { }


    public void Delete(UnitOfWork uw, T obj) {
      foreach (var d in deleter) {
        d(uw, obj);
      }
      var rep = uw.Repository<T>();
      rep.Delete(obj);
    }

    protected List<Action<UnitOfWork, T>> deleter = new List<Action<UnitOfWork, T>>();
    public List<LambdaExpression> DeleteExpr { get { return deleteExpr; }}
    public List<LambdaExpression> deleteExpr = new List<LambdaExpression>(); 

    internal CascadedDeleter<T> AddCascadedItem<TResult>(Expression<Func<T, ICollection<TResult>>> children) where TResult : class {
      Action<UnitOfWork, T> removeAct = (uw, obj) => {
        if (obj == null) { return;}
        var col = children.Compile()(obj);
        if (col == null) {
          return;
        }
        var list = col.ToList();
        if (!list.Any()) {
          return;
        }
        var rep = uw.Repository<TResult>();
        foreach (var c in list) {
          rep.Delete(c);
        }
      };
      deleter.Add(removeAct);
      deleteExpr.Add(children);
      return this;
    }
  }
  public class CascadedDeleter {
    private static readonly Dictionary<Type, object> typedDeleter;
    public static CascadedDeleter<T> Get<T>() where T : class, IBaseDataModel {
      var type = typeof(T);
      if (!typedDeleter.ContainsKey(type)) {
        return null;
      }
      return typedDeleter[type] as CascadedDeleter<T>;
    }

    static CascadedDeleter<T> CreateDeleter<T>() where T : class, IBaseDataModel {
      var deleter = new CascadedDeleter<T>();
      typedDeleter[typeof(T)] = deleter;
      return deleter;
    }

    static CascadedDeleter() {
      typedDeleter = new Dictionary<Type, object>();

      CreateDeleter<VstStocksBatch>().AddCascadedItem(i => i.VstItems);
      CreateDeleter<VstStocks>().AddCascadedItem(i => i.VstStocksItem);
    }
  }


}
