#if USING_MONGODB
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Mobizone.TSIC.Infrastructure.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Mobizone.TSIC.Infrastructure.DAL {
  public class MongoDBRepository<TEntity> : IDocumentDBRepository<TEntity> where TEntity : IBaseDocumentObject {

    UnitOfWork unitOfWork;

    public MongoDBRepository(UnitOfWork unitOfWork) {
      this.unitOfWork = unitOfWork;
    }

    public void WhereAndDisable(Expression<Func<TEntity, bool>> exp) {
      var query = Query<TEntity>.Where(exp);
      var update = Update<TEntity>.Set(i => i.State, false);
      unitOfWork.DocCollection<TEntity>().Update(query, update, new MongoUpdateOptions {
        Flags = UpdateFlags.Multi
      });
    }

    public void DeleteDisabledRecord() {
      var query = Query<TEntity>.EQ(i => i.State, false);
      unitOfWork.DocCollection<TEntity>().Remove(query);
    }
  }
}
#endif