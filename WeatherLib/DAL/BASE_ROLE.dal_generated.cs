//------------------------------------------------------------------------------
// <auto-generated>
//     此代码是根据模板生成的。
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，则所做更改将丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WeatherLib.Model;
using WeatherLib.Utility;

namespace WeatherLib.DAL
{
    
    public partial interface IBASE_ROLERepository:
        WeatherLib.Infrastructure.DAL.IRepository<BASE_ROLE>
    {
      }
    
    public partial class BASE_ROLERepository:
       WeatherLib.Infrastructure.DAL.GenericRepository<BASE_ROLE>,
       WeatherLib.DAL.IBASE_ROLERepository
    {
      private WeatherLib.Model.WeatherDBContext context { get { return this.UnitOfWork.Context; } }
      private WeatherLib.DAL.UnitOfWork UnitOfWork {get;set;}
      public BASE_ROLERepository(WeatherLib.DAL.UnitOfWork unitOfWork) : base (unitOfWork)
      { 
    	this.UnitOfWork = unitOfWork;
      }
    
      protected static decimal cur_id = -1;
      protected static object locker = new object();
      public override decimal GetNextID() {
        lock (locker) {
        	
    	  // if (cur_id < 0)
    	  // {
    	  if (dbSet.Count() == 0)
    	  {
    	    return 1;
    	  }
    	  decimal maxid = dbSet.Max(r => r.ID);
    	  // }
    	  // cur_id += 1;
    	  // return cur_id;
    	  return maxid + 1;
    	      }
      }
        }
}
