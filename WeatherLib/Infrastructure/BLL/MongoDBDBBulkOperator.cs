#if USING_MONGODB
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using WeatherLib.Infrastructure.DAL;
using WeatherLib.Infrastructure.Models;
//using WeatherLib.Utility;
using MongoDB.Driver.Builders;


namespace WeatherLib.Infrastructure.BLL {

  /// <summary>
  ///     不是线程安全的
  /// </summary>

  public class MongoDBBulkOperator : IBulkOperator, IDisposable {
    protected UnitOfWork unitOfWork;
    protected enum OperatorType{
      ActInsert,
      ActUpdate,
      ActDelete
    }
    protected List<Tuple<OperatorType, Type, IBaseDocumentObject>> optQueue = new List<Tuple<OperatorType, Type, IBaseDocumentObject>>();
    public MongoDBBulkOperator(UnitOfWork unitOfWork) {
      this.unitOfWork = unitOfWork;
    }
    public void Commit() {
      var c = unitOfWork.DocDB;
      HashSet<Type> types = optQueue.Select(i => i.Item2).ToHashSet();
      var dict = types.ToDictionary(i => i, i => c.GetCollection(i, i.Name));

      foreach (var opt in optQueue) {
        switch (opt.Item1) {
          case OperatorType.ActInsert:
          case OperatorType.ActUpdate:
            dict[opt.Item2].Save(opt.Item3);
            break;
          case OperatorType.ActDelete:
            var query = Query.EQ("_id", opt.Item3.Id);
            dict[opt.Item2].Remove(query);
            break;
        }
      }
      optQueue.Clear();
    }

    public void BulkInsert<TEntity>(TEntity entity) where TEntity : class {
      var obj = entity as IBaseDocumentObject;
      if (null == obj) {
        throw new NotSupportedException("entity no a IBaseDocumentObject");
      }
      optQueue.Add(Tuple.Create(OperatorType.ActInsert, typeof(TEntity), obj));
    }

    public void BulkUpdate<TEntity>(TEntity entity) where TEntity : class {
      var obj = entity as IBaseDocumentObject;
      if (null == obj) {
        throw new NotSupportedException("entity no a IBaseDocumentObject");
      }
      optQueue.Add(Tuple.Create(OperatorType.ActUpdate, typeof(TEntity), obj));
    }

    public void BulkDelete<TEntity>(TEntity entity) where TEntity : class {
      var obj = entity as IBaseDocumentObject;
      if (null == obj) {
        throw new NotSupportedException("entity no a IBaseDocumentObject");
      }
      optQueue.Add(Tuple.Create(OperatorType.ActDelete, typeof(TEntity), obj));
    }

    public void Dispose() {
      Commit();
    }


  }
}
#endif