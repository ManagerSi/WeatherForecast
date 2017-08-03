using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using WeatherLib.Infrastructure.DAL;



namespace WeatherLib.Infrastructure.BLL {
  public interface IBulkOperator : IDisposable {
    void Commit();
    void BulkInsert<TEntity>(TEntity entity) where TEntity : class;
    void BulkUpdate<TEntity>(TEntity entity) where TEntity : class;
    void BulkDelete<TEntity>(TEntity entity) where TEntity : class;
  }

  public class BulkOperator : IBulkOperator, IDisposable {
    protected UnitOfWork unitOfWork;
    public BulkOperator(UnitOfWork unitOfWork) {
      unitOfWork.DisableAutoDetectChanges();
      this.unitOfWork = unitOfWork;
    }
    public void Commit() {
      unitOfWork.Save();
    }

    public void BulkInsert<TEntity>(TEntity entity) where TEntity : class {
      unitOfWork.Repository<TEntity>().Insert(entity);
    }

    public void BulkUpdate<TEntity>(TEntity entity) where TEntity : class {
      unitOfWork.Repository<TEntity>().Update(entity);
    }

    public void BulkDelete<TEntity>(TEntity entity) where TEntity : class {
      unitOfWork.Repository<TEntity>().Delete(entity);
    }

    public void Dispose() {
      Commit();
      unitOfWork.EnableAutoDetectChanges();
    }


  }
}
