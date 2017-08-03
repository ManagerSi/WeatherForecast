using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobizone.TSIC.FOC.Infrastructure.DAL {
  public interface IDocDBSession :IDisposable{
    UnitOfWork UnitOfWork { get; set; }
  }
  public class MongoDBSession : IDocDBSession {
    public MongoDBSession(UnitOfWork unitOfWork) { this.UnitOfWork = unitOfWork; }
    public UnitOfWork UnitOfWork { get; set; }
    public void Dispose() { this.UnitOfWork = null; }
  }
}
