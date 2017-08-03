using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data;
using System.Data.Entity;
using WeatherLib.Infrastructure.DAL;

namespace WeatherLib.Infrastructure.BLL {
  public interface IBLLService {
    IBL BL<IBL>() where IBL : class;
    IBaseBL<TEntity> EntityBL<TEntity>() where TEntity : class;
  }
  /// <summary>
  /// 这个类不是线程安全的
  /// </summary>
  public class BLLService {
    private UnitOfWork unitOfWork;

    public BLLService(UnitOfWork uw) {
      this.unitOfWork = uw;
    }

    protected static Dictionary<Type, PropertyInfo> _entityPropertyCache;
    protected static Dictionary<Type, PropertyInfo> _blPropertyCache;
    protected void InitPropertyCache() {

      var __entityPropertyCache = new Dictionary<Type, PropertyInfo>();
      var __blPropertyCache = new Dictionary<Type, PropertyInfo>();
      Type thisType = this.GetType();
      PropertyInfo[] ps = thisType.GetProperties(
          BindingFlags.FlattenHierarchy |
          BindingFlags.Public |
           BindingFlags.Instance);

      foreach (PropertyInfo p in ps) {
        Type t = p.PropertyType.GetInterface("IBaseBL`1");
        if (t == null) {
          continue;
        }

        Type arg = t.GetGenericArguments().Single();
        __entityPropertyCache[arg] = p;
        __blPropertyCache[p.PropertyType] = p;
      }
      _entityPropertyCache = __entityPropertyCache;
      _blPropertyCache = __blPropertyCache;
    }

    public IBaseBL<TEntity> EntityBL<TEntity>() where TEntity : class {
      if (null == _entityPropertyCache ) {
        InitPropertyCache();
      }
      Type entityType = typeof(TEntity);
      if (_entityPropertyCache.ContainsKey(entityType)) {
        PropertyInfo p = _entityPropertyCache[entityType];
        return p.GetValue(this, null) as IBaseBL<TEntity>;
      }
      throw new InvalidOperationException("不支持该类型");
    }

    public IBL BL<IBL>() where IBL : class {
      if (null == _blPropertyCache) {
        InitPropertyCache();
      }
      Type entityType = typeof(IBL);
      if (_blPropertyCache.ContainsKey(entityType)) {
        PropertyInfo p = _blPropertyCache[entityType];
        return p.GetValue(this, null) as IBL;
      }
      throw new InvalidOperationException("不支持该类型");
    }

  }
}
