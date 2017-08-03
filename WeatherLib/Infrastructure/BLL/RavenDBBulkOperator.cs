#if USING_RAVENDB
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using Mobizone.TSIC.FOC.Infrastructure.DAL;



namespace Mobizone.TSIC.FOC.Infrastructure.BLL {


  public class RavenDBBulkOperator : IBulkOperator, IDisposable {
    protected UnitOfWork unitOfWork;
    public RavenDBBulkOperator(UnitOfWork unitOfWork) {
      this.unitOfWork = unitOfWork;
    }
    public void Commit() {
      unitOfWork.DocSession.SaveChanges();
    }

    public void BulkInsert<TEntity>(TEntity entity) where TEntity : class {
      unitOfWork.DocSession.Store(entity);
    }

    public void BulkUpdate<TEntity>(TEntity entity) where TEntity : class {
      unitOfWork.DocSession.Store(entity);
    }

    public void BulkDelete<TEntity>(TEntity entity) where TEntity : class {
      unitOfWork.DocSession.Delete(entity);
    }

    public void Dispose() {
      Commit();
    }


  }
}
#endif