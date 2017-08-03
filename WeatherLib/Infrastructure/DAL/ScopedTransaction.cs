using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IsolationLevel = System.Data.IsolationLevel;

namespace WeatherLib.Infrastructure.DAL {
  public class ScopedTransaction : IDisposable {
    protected UnitOfWork unitOfWork;
    protected DbContext ctx;
    protected TransactionScope transaction;
    public ScopedTransaction(UnitOfWork uw, TransactionOptions option) {
      unitOfWork = uw;
      transaction = new TransactionScope(TransactionScopeOption.Required, option);
      ctx = unitOfWork.NewContext(false);
    }

    public void Complete() {
      transaction.Complete();
    }

    public void Dispose() {
      unitOfWork.context.Dispose();
      unitOfWork.context = ctx;
      transaction.Dispose();
      
    }
  }
}
