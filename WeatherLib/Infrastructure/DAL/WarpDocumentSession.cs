#if USING_RAVENDB
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
namespace Mobizone.TSIC.Infrastructure.DAL {
  public class WarpDocumentSession : IDocumentSession, IDisposable {
    public WarpDocumentSession(IDocumentSession session, bool skipDisposing = false) {
      this.Session = session;
      this.SkipDisposing = skipDisposing;
    }
    protected IDocumentSession Session { get; set; }
    protected bool SkipDisposing { get; set; }
    public void Dispose() {
      if (!SkipDisposing) {
        Session.Dispose();
        Session = null;
      }
    }

    public ISyncAdvancedSessionOperation Advanced {
      get { return Session.Advanced; }
    }

    public void Delete<T>(T entity) {
      Session.Delete<T>(entity);
    }

    public Raven.Client.Document.ILoaderWithInclude<T> Include<T, TInclude>(System.Linq.Expressions.Expression<Func<T, object>> path) {
      return Session.Include<T, TInclude>(path);
    }

    public Raven.Client.Document.ILoaderWithInclude<T> Include<T>(System.Linq.Expressions.Expression<Func<T, object>> path) {
      return Session.Include<T>(path);
    }

    public Raven.Client.Document.ILoaderWithInclude<object> Include(string path) {
      return Session.Include(path);
    }

    public T Load<T>(ValueType id) {
      return Session.Load<T>(id);
    }

    public T[] Load<T>(IEnumerable<string> ids) {
      return Session.Load<T>(ids);
    }

    public T[] Load<T>(params string[] ids) {
      return Session.Load<T>(ids);
    }

    public T Load<T>(string id) {
      return Session.Load<T>(id);
    }

    public Raven.Client.Linq.IRavenQueryable<T> Query<T, TIndexCreator>() where TIndexCreator : Raven.Client.Indexes.AbstractIndexCreationTask, new() {
      return Session.Query<T, TIndexCreator>();
    }

    public Raven.Client.Linq.IRavenQueryable<T> Query<T>() {
      return Session.Query<T>();
    }

    public Raven.Client.Linq.IRavenQueryable<T> Query<T>(string indexName) {
      return Session.Query<T>(indexName);
    }

    public void SaveChanges() {
      Session.SaveChanges();
    }

    public void Store(dynamic entity, string id) {
      Session.Store(entity, id);

    }

    public void Store(dynamic entity) {
      Session.Store(entity);
    }

    public void Store(object entity, Guid etag, string id) {
      Session.Store(entity, etag, id);
    }

    public void Store(object entity, Guid etag) {
      Session.Store(entity, etag);
    }
  }
}
#endif