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
    
    public partial interface IWeatherDailyForecastBL: WeatherLib.Infrastructure.BLL.IGenericBL<WeatherDailyForecast>
    {
      }
    
    public partial class WeatherDailyForecastBL:
       WeatherLib.Infrastructure.BLL.GenericBL<WeatherDailyForecast>,
       IWeatherDailyForecastBL
    {
      public WeatherDailyForecastBL() : base() {
        unitOfWork = new WeatherLib.DAL.UnitOfWork();
    	OnCreate();
      }
      public WeatherDailyForecastBL(WeatherLib.DAL.UnitOfWork unitOfWork) : base() {
        this.unitOfWork = unitOfWork;
    	OnCreate();
      }
      partial void OnCreate();
      private WeatherLib.DAL.UnitOfWork UnitOfWork {
        get { return unitOfWork as WeatherLib.DAL.UnitOfWork;}
    	set { unitOfWork = value;}
      }
      private IWeatherDailyForecastRepository _repository;
      private IWeatherDailyForecastRepository Repository
      {
      	get
    	{
    	  if (_repository == null)
    	  {
    	    _repository = unitOfWork.Repository<WeatherDailyForecast>() as IWeatherDailyForecastRepository;
    	  }
    	  return _repository;
    	}
    	set
    	{
    	  _repository = value;
    	  }
      }
        
        public override WeatherDailyForecast InsertOrUpdate(WeatherDailyForecast entity) {
            if (entity.ID == 0) {
    	    return Insert(entity);
    	  }
    	  Update(entity);
    	  return entity;
          }
    
    }
}
