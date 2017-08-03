﻿


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

namespace WeatherLib.BLL
{
    public partial class BLLFactory : WeatherLib.Infrastructure.BLL.BLLFactory
        {
            
            static BLLFactory()
            {
                RegisterBL("IBaseEmployeeBL", typeof(IBaseEmployeeBL), typeof(BaseEmployeeBL));
                RegisterBL("IBaseUserBL", typeof(IBaseUserBL), typeof(BaseUserBL));
                RegisterBL("IBASE_ROLEBL", typeof(IBASE_ROLEBL), typeof(BASE_ROLEBL));
                RegisterBL("IBASE_USER_ROLEBL", typeof(IBASE_USER_ROLEBL), typeof(BASE_USER_ROLEBL));
                RegisterBL("IJD_ProductBL", typeof(IJD_ProductBL), typeof(JD_ProductBL));
                ExtraInit(); 
    			//RegisterDocBL();
            }
    	    static partial void ExtraInit();
    		public static  void  RegisterBL(string name,Type intf, Type type) {
              BLLs.Add(intf, type);
              BLLsByName.Add(name, type);
            }
    
    		protected static Dictionary<Type, Type> BLLs = new Dictionary<Type, Type>();
    		protected static Dictionary<string, Type> BLLsByName = new Dictionary<string, Type>();
    	/// <summary>
            ///     创建对应BL,如果缓存可用，则使用缓存
            /// </summary>
    		public static IBL Create<IBL>(WeatherLib.DAL.UnitOfWork uw=null) where IBL : class {
    	      return null == uw ? CreateNew<IBL>() : CreateNew<IBL>(uw);
    	      // 现在还没遇到性能瓶颈，无需使用以下方法
       	      /*var service = CurrentService;
        	  if (service  == null) {
                return null == uw ? CreateNew<IBL>() : CreateNew<IBL>(uw);
        	  }
    		  if (null == uw) {
    	        return service.BL<IBL>() as IBL;
    	      }
              var cur_uw = service.UnitOfWork;
    		  if (uw == cur_uw) {
    	        return service.BL<IBL>() as IBL;
    	      } else {
    		    return CreateNew<IBL>(uw);
    		  }*/
    		}
    		//public static IBL Create<IBL>(WeatherLib.Infrastructure.DAL.IDocDBSession session) where IBL : class {
    	    //  var bl = CreateNew<IBL>(session.UnitOfWork);
        	//  var docBL = bl as WeatherLib.Infrastructure.BLL.IDocumentBL;
            //  if (docBL != null) {
            //    docBL.SetSession(session);
            //  }
        	//  return bl;
        	// }
        	
    	/// <summary>
            ///     创建一个新的BLL对象
            /// </summary>
    		public static IBL CreateNew<IBL>(params object[] args) where IBL : class {
    		  return CreateNewFromDict<IBL>(BLLs, args);
    		}
    		public static object CreateNewByName(string name, params object[] args) {
    		  return CreateNewFromDictByName(BLLsByName, name, args);
    		}
        }
    
    
}

