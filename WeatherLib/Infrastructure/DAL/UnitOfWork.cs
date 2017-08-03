using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Reflection;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;

namespace WeatherLib.Infrastructure.DAL {
  /// <summary>
  /// 这个类不是线程安全的
  /// </summary>
  [System.Runtime.InteropServices.GuidAttribute("8E0C2455-0E0A-4AF3-858A-B5FE1318D161")]
  public class UnitOfWork {
    internal DbContext context;
    protected static Dictionary<Type, PropertyInfo> _propertyCache;
    protected void InitPropertyCache() {

      var __propertyCache = new Dictionary<Type, PropertyInfo>();
      Type thisType = this.GetType();
      PropertyInfo[] ps = thisType.GetProperties(
          BindingFlags.FlattenHierarchy |
          BindingFlags.Public |
           BindingFlags.Instance);

      foreach (PropertyInfo p in ps) {
        Type t = p.PropertyType.GetInterface("IRepository`1");
        if (t == null) {
          continue;
        }

        Type arg = t.GetGenericArguments().Single();
        __propertyCache[arg] = p;
      }
      _propertyCache = __propertyCache;
    }
    public IRepository<TEntity> Repository<TEntity>() where TEntity : class {
      if (null == _propertyCache) {
        InitPropertyCache();
      }
      Type entityType = typeof(TEntity);
      if (_propertyCache.ContainsKey(entityType)) {
        PropertyInfo p = _propertyCache[entityType];
        return p.GetValue(this, null) as IRepository<TEntity>;
      }
      throw new InvalidOperationException("不支持该类型");

    }

    public UnitOfWork(DbContext context) {
      this.context = context;
      UsingHttpCurrentSession = true;
    }

    public UnitOfWork() :this(WeatherLib.Model.WeatherDBContext.CurrentHttpContext){      
    }

    public bool UsingHttpCurrentSession { get; set; }

    public DbContext NewContext(bool open) {
      var ctx = context;
      context = WeatherLib.Model.WeatherDBContext.NewContext(open);
      
      return ctx;
    }



    public void DisableLazyLoad() { context.Configuration.LazyLoadingEnabled = false; }
    public void EnableLazyLoad() { context.Configuration.LazyLoadingEnabled = true; }

    public void DisableAutoDetectChanges() {
      context.Configuration.AutoDetectChangesEnabled = false;
    }

    public void EnableAutoDetectChanges() {
      context.Configuration.AutoDetectChangesEnabled = true;
    }

    public void Save() {
      context.SaveChanges();
    }

        //public async void SaveAsync() {
        //    await context.SaveChangesAsync();
        //}

    //    public ScopedTransaction BeginTransaction(TransactionOptions option) {
    //  return new ScopedTransaction(this, option);
    //}
  }
}