using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using WeatherLib.Infrastructure.DAL;


namespace WeatherLib.Infrastructure.BLL {
  public interface IBaseBL<TEntity> where TEntity : class {
  }

  //public interface IDocumentBL {
  //  void SetSession(IDocDBSession session);
  //}

  public interface IBulkBL<TEntity> : IBaseBL<TEntity> where TEntity : class {
    // 批量操作
    IBulkOperator BeginBulk();
    //TEntity BulkInsert(TEntity entity);
    //void BulkInsert(IEnumerable<TEntity> entities);
    //void BulkUpdate(TEntity entity);
    //void BulkUpdate(IEnumerable<TEntity> entities);
    //void BulkDelete(TEntity entity);
    //void BulkDelete(IEnumerable<TEntity> entities);
  }
  public interface IGenericBL<TEntity> : IBulkBL<TEntity> where TEntity : class {
    bool Exists(int id);
    TEntity Insert(TEntity entity);
    void Insert(IEnumerable<TEntity> entities);
    /// <summary>
    /// 返回带有Proxy的POCO
    /// </summary>
    /// <returns></returns>
    TEntity Create();
    void Update(TEntity entity);
    bool Delete(int id);
    bool DeleteList(int[] ids);
    TEntity GetByID(int id);
    /// <summary>
    /// 根据ID决定使用Insert还是Update
    /// </summary>
    /// <param name="entity"></param>
    TEntity InsertOrUpdate(TEntity entity);

  }
}