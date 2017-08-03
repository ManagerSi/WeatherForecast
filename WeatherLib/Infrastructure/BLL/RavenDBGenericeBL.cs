#if USING_RAVENDB
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using Mobizone.TSIC.FOC.Infrastructure.DAL;


using Raven.Client;

namespace Mobizone.TSIC.FOC.Infrastructure.BLL {
  public interface IDocumentDBGenericBL<TEntity> : IBaseBL<TEntity>, IBulkBL<TEntity>
    where TEntity : class, Models.IBaseDocumentObject, new() {
    TEntity Insert(TEntity entity);
    void Insert(IEnumerable<TEntity> entities);
    TEntity Create();
    void Update(TEntity entity);
    bool Delete(TEntity entity);
    bool DeleteList(TEntity[] entities);
    void Disable(TEntity entity);
    TEntity GetByID(object id);
    TEntity InsertOrUpdate(TEntity entity);
  }
  public interface IRavenDBGenericBL<TEntity> : IDocumentDBGenericBL<TEntity>
    where TEntity : class, Models.IBaseDocumentObject, new() {
 void SetSession(IDocumentSession session);
  }
  public class RavenDBGenericBL<TEntity> : IRavenDBGenericBL<TEntity> where TEntity : class, Models.IBaseDocumentObject, new() {
    protected UnitOfWork unitOfWork;
    public RavenDBGenericBL() {
    }
    private IDocumentSession _session = null;
    public IDocumentSession Session {
      get {
        return _session == null ? unitOfWork.DocSession : _session;
      }

    }

    public void SetSession(IDocumentSession session) {
      _session = session;
    }
    public TEntity Insert(TEntity entity) {
      entity.UpdateTime = Utility.Util.RPCNow;
      Session.Store(entity);
      Session.SaveChanges();
      return entity;
    }

    public void Insert(IEnumerable<TEntity> entities) {
      foreach (var e in entities) {
        e.UpdateTime = Utility.Util.RPCNow;
        Session.Store(e);
      }
      Session.SaveChanges();
    }

    public TEntity Create() {
      var e = new TEntity();
      e.State = true;
      return e;
    }

    public void Update(TEntity entity) {
      entity.UpdateTime = Utility.Util.RPCNow;
      Session.Store(entity);
      Session.SaveChanges();
    }

    public bool Delete(TEntity entity) {
      Session.Delete(entity);
      Session.SaveChanges();
      return true;
    }

    public bool DeleteList(TEntity[] entities) {
      foreach (var e in entities) {
        Session.Delete(e);
      }
      Session.SaveChanges();
      return true;
    }

    public TEntity GetByID(object id) {
      ValueType Id = (ValueType)id;
      return Session.Load<TEntity>(Id);
    }


    public TEntity InsertOrUpdate(TEntity entity) {
      entity.UpdateTime = Utility.Util.RPCNow;
      Session.Store(entity);
      Session.SaveChanges();
      return entity;
    }


    /// <summary>
    ///     返回有效的entity
    /// </summary>
    protected IQueryable<TEntity> Query() {
      return Session.Query<TEntity>().Where(i => i.State);
    }



    /// <summary>
    ///     返回所有entity
    /// </summary>
    protected IQueryable<TEntity> QueryAll() {
      return Session.Query<TEntity>();
    }
    public void Disable(TEntity entity) {
      entity.State = false;
      Update(entity);
    }

    public IBulkOperator BeginBulk() {
      return new RavenDBBulkOperator(unitOfWork);
    }
  }
}
#endif