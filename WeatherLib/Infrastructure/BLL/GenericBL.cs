using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using WeatherLib.Infrastructure.DAL;

#if USING_RAVENDB
using Raven.Client;
#endif
namespace WeatherLib.Infrastructure.BLL {
  public abstract class GenericBL<TEntity> : IGenericBL<TEntity> where TEntity : class {
    protected UnitOfWork unitOfWork;
    private IRepository<TEntity> _repository;
    private IRepository<TEntity> Repository {
      get {
        if (_repository == null) {
          _repository = unitOfWork.Repository<TEntity>();
        }
        return _repository;
      }
      set {
        _repository = value;
      }
    }

    public GenericBL() {

    }
    public bool Exists(int id) {
      return null != this.Repository.GetByID(id);
    }
    public TEntity Create() {
      return this.Repository.Create();
    }

    public IBulkOperator BeginBulk() {
      return new BulkOperator(this.unitOfWork);
    }

    public TEntity BulkInsert(TEntity entity) {
      return this.Repository.Insert(entity);
    }
    public void BulkInsert(IEnumerable<TEntity> entities) {
      foreach(var e in entities) {
        BulkInsert(e);
      }
    }
    public void BulkUpdate(TEntity entity) {
      this.Repository.Update(entity);
    }
    public void BulkUpdate(IEnumerable<TEntity> entities) {
      foreach(var e in entities) {
        BulkUpdate(e);
      }
    }
    public void BulkDelete(TEntity entity) {
      this.Repository.Delete(entity);
    }
    public void BulkDelete(IEnumerable<TEntity> entities) {
      foreach(var e in entities) {
        BulkDelete(e);
      }
    }

    public TEntity Insert(TEntity entity) {
      entity = this.Repository.Insert(entity);
      unitOfWork.Save();
      return entity;
    }
    public void Insert(IEnumerable<TEntity> entities) {
      foreach (TEntity e in entities) {
        this.Repository.Insert(e);
      }
      unitOfWork.Save();
    }
    public void Update(TEntity entity) {
      this.Repository.Update(entity);
      unitOfWork.Save();
    }
    public bool Delete(int id) {
      this.Repository.Delete(id);
      unitOfWork.Save();
      return true;
    }
    public bool DeleteList(int[] ids) {
      foreach (int id in ids) {
        this.Repository.Delete(id);
      }
      unitOfWork.Save();
      return true;
    }
    public TEntity GetByID(int id) {
      return this.Repository.GetByID(id);
    }
    public abstract TEntity InsertOrUpdate(TEntity entity);


  }
}