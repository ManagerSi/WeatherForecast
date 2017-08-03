#if USING_MONGODB
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using WeatherLib.Infrastructure.DAL;


using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

#if USING_RAVENDB
using Raven.Client;
#endif

namespace WeatherLib.Infrastructure.BLL {
  public interface IDocumentDBGenericBL<TEntity> : IBaseBL<TEntity>, IBulkBL<TEntity>
    where TEntity : class, Model.IBaseDocumentObject, new() {
    TEntity Insert(TEntity entity);
    void Insert(IEnumerable<TEntity> entities);
    TEntity Create();
    void Update(TEntity entity);
    bool Delete(TEntity entity);
    bool DeleteList(TEntity[] entities);
    void Disable(TEntity entity);
    TEntity GetByID(string id);
    TEntity InsertOrUpdate(TEntity entity);
    
  }



  public interface IMongoDBGenericBL<TEntity> : IDocumentDBGenericBL<TEntity>,IDocumentBL
    where TEntity : class, Models.IBaseDocumentObject, new() {
    
  }
  public class MongoDBGenericBL<TEntity> : IMongoDBGenericBL<TEntity> where TEntity : class, Models.IBaseDocumentObject, new() {
    protected UnitOfWork unitOfWork;
    public MongoDBGenericBL() {
    }

    public MongoDatabase DocDB {
      get {
        return unitOfWork.DocDB;
      }
    }

    protected virtual MongoCollection<TEntity> Collection {
      get {
        return unitOfWork.DocCollection<TEntity>();
      }
    }

    protected IDocDBSession _session = null;
    public void SetSession(IDocDBSession session) {
      _session = session;
    }

    public TEntity Insert(TEntity entity) {
      entity.State = true;
      entity.UpdateTime = Utility.Util.RPCNow;
      Collection.Insert(entity);
      return entity;
    }

    public void Insert(IEnumerable<TEntity> entities) {
      foreach (var e in entities) {
        e.State = true;
        e.UpdateTime = Utility.Util.RPCNow;
        
      }
      Collection.InsertBatch(entities);
      
    }

    public TEntity Create() {
      var e = new TEntity();
      e.State = true;
      return e;
    }

    public void Update(TEntity entity) {
      entity.UpdateTime = Utility.Util.RPCNow;
      Collection.Save(entity);
      
    }

    public void Update(IEnumerable<TEntity> entities) {
      
      using (var bulk = BeginBulk()) {
        foreach (var e in entities) {
          e.UpdateTime = Utility.Util.RPCNow;
          bulk.BulkUpdate(e);
        }
        bulk.Commit();
      }
      

    }

    public bool Delete(TEntity entity) {
      var query = Query<TEntity>.EQ(i => i.Id, entity.Id);
      Collection.Remove(query);
      return true;
    }

    public bool DeleteList(TEntity[] entities) {
      foreach (var e in entities) {
        Delete(e);
      }
      return true;
    }

    public TEntity GetByID(string id) {
      var query = Query<TEntity>.EQ(i => i.Id, id);
      return Collection.FindOne(query);
    }
    

    public TEntity InsertOrUpdate(TEntity entity) {
      entity.UpdateTime = Utility.Util.RPCNow;
      Collection.Save(entity);
      return entity;
    }

    public void Disable(TEntity entity) {
      entity.State = false;
      Update(entity);
    }

    public IBulkOperator BeginBulk() {
      return new MongoDBBulkOperator(unitOfWork);
    }

    protected IQueryable<TEntity> Query() {
      return QueryAll().Where(i => i.State);
    }
    protected IQueryable<TEntity> QueryAll() {
      return Collection.AsQueryable();
    }
  }
}
#endif