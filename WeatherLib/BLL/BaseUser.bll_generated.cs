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
using WeatherLib.DAL;
using WeatherLib.Utility;

namespace WeatherLib.BLL
{
    
    public partial interface IBaseUserBL: WeatherLib.Infrastructure.BLL.IGenericBL<BaseUser>
    {
            /// <summary>
          /// 隐藏一条记录
          /// </summary>
          /// <param name="id"></param>
          void Disable(decimal id);
    	  /// <summary>
          /// 显示一条记录
          /// </summary>
          /// <param name="id"></param>
    	  void Enable(decimal id);
      }
    
    public partial class BaseUserBL:
       WeatherLib.Infrastructure.BLL.GenericBL<BaseUser>,
       IBaseUserBL
    {
      public BaseUserBL() : base() {
        unitOfWork = new WeatherLib.DAL.UnitOfWork();
    	OnCreate();
      }
      public BaseUserBL(WeatherLib.DAL.UnitOfWork unitOfWork) : base() {
        this.unitOfWork = unitOfWork;
    	OnCreate();
      }
      partial void OnCreate();
      private WeatherLib.DAL.UnitOfWork UnitOfWork {
        get { return unitOfWork as WeatherLib.DAL.UnitOfWork;}
    	set { unitOfWork = value;}
      }
      private IBaseUserRepository _repository;
      private IBaseUserRepository Repository
      {
      	get
    	{
    	  if (_repository == null)
    	  {
    	    _repository = unitOfWork.Repository<BaseUser>() as IBaseUserRepository;
    	  }
    	  return _repository;
    	}
    	set
    	{
    	  _repository = value;
    	  }
      }
              public void Disable(decimal id) {
    	    BaseUser record = GetByID((int)id);
            record.State = "0";
            Update(record);
    	  }
    	  public void Enable(decimal id) {
    	    BaseUser record = GetByID((int)id);
            record.State = "1";
            Update(record);
    	  }
      
        public override BaseUser InsertOrUpdate(BaseUser entity) {
            if (entity.ID == 0) {
    	    return Insert(entity);
    	  }
    	  Update(entity);
    	  return entity;
          }
    
    }
}
