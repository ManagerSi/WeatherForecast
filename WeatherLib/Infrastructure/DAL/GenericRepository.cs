using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Transactions;
using System.Linq.Expressions;

namespace WeatherLib.Infrastructure.DAL {
  /// <summary>
  /// This code from 
  /// http://www.asp.net/entity-framework/tutorials/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
  /// 
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class {
    private UnitOfWork unitOfWork;
    internal DbSet<TEntity> dbSet {
      get { return unitOfWork.context.Set<TEntity>(); }
    }
    //protected DbQuery<TEntity> DBQuery {
    //  get {
    //    return NoTracking ? DBSet : DBSet.AsNoTracking();
    //  }
    //}
    //public bool NoTracking = false;

    public GenericRepository(UnitOfWork unitOfWork) {
      this.unitOfWork = unitOfWork;
    }


    public virtual decimal GetNextID() {
      throw new NotImplementedException();
    }

    public virtual IQueryable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "") {
          IQueryable<TEntity> query = dbSet;

      if (filter != null) {
        query = query.Where(filter);
      }

      foreach (var includeProperty in includeProperties.Split
          (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
        query = query.Include(includeProperty);
      }

      if (orderBy != null) {
        return orderBy(query);
      } else {
        return query;
      }
    }

    public virtual TEntity Create() {
      return dbSet.Create();
    }
    public virtual TEntity GetByID(object id) {
      return dbSet.Find(id);
    }

    public virtual TEntity Insert(TEntity entity) {
      return dbSet.Add(entity);
    }

    public virtual void Delete(object id) {
      TEntity entityToDelete = dbSet.Find(id);
      Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete) {
      if (unitOfWork.context.Entry(entityToDelete).State == EntityState.Detached) {
        dbSet.Attach(entityToDelete);
      }
      dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate) {
      dbSet.Attach(entityToUpdate);
      unitOfWork.context.Entry(entityToUpdate).State = EntityState.Modified;
    }
  }
}