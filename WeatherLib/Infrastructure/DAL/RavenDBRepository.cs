#if USING_RAVENDB
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Mobizone.TSIC.Infrastructure.Models;
namespace Mobizone.TSIC.Infrastructure.DAL {
  public class RavenDBRepository<TEntity> : IDocumentDBRepository<TEntity> where TEntity : IBaseDocumentObject {

    UnitOfWork unitOfWork;

    public RavenDBRepository(UnitOfWork unitOfWork) {
      this.unitOfWork = unitOfWork;
    }

    public void WhereAndDisable(Expression<Func<TEntity, bool>> exp) {
      using (var session = unitOfWork.OpenSession()) {
        var rst = session.Query<TEntity>().Where(exp).Where(i=>i.State);
        foreach (var r in rst) {
          r.State = false;
        }
        session.SaveChanges();
      }
    }

    public void DeleteDisabledRecord() {
      using (var session = unitOfWork.OpenSession()) {
        var rst = session.Query<TEntity>().Where(i => !i.State);
        foreach (var r in rst) {
          session.Delete(r);
        }
        session.SaveChanges();
      }
    }
  }
}
#endif
