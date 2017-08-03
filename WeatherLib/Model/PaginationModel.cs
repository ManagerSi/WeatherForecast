using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using WeatherLib.Utility;
namespace WeatherLib.Model {
  public interface IRemoveNotQueryUrlParams {
  }
  public class PaginationModel<T> where T : class {
    public IEnumerable<T> SubItemList;
    public int ItemCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public RouteValueDictionary routeDict { get; set; }
    public RouteValueDictionary GetRouteDictByPage(int pageNumber) {
      if (pageNumber < 0 || (PageCount > 0 && pageNumber > PageCount - 1)) {
        return null;
      }
      RouteValueDictionary dict = new RouteValueDictionary(routeDict);
      dict.Update(new { page = pageNumber });
      return dict;
    }

    public RouteValueDictionary GetRouteDictWithUpdate(object obj) {
      if (obj == null) {
        return routeDict;
      }
      RouteValueDictionary dict = new RouteValueDictionary(routeDict);
      return dict.Update(obj);

    }
    public bool HasPreviousPage {
      get {
        return CurrentPage > 0;
      }
    }
    public bool HasNextPage {
      get {
        return CurrentPage < PageCount - 1;
      }
    }

  }
}
