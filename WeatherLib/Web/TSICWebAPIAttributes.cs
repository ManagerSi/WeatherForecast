using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobizone.TSIC.Utility;
namespace Mobizone.TSIC.Web {

  public class NeverOffline : Attribute {

  }

  public class AppClientViewAttribute : Attribute {
    //public AppClientViewAttribute(string name) { ViewName = name; }
    public string Name { get; set; }
  }

  /*public abstract class AppDataExpriedAttribute : Attribute {
    public abstract DateTime ExpriedTime();
  }

  public class ExpriedNextDayAttribute : AppDataExpriedAttribute {

    public override DateTime ExpriedTime() { return Util.RPCNow.LastMinuteOfDay().AddSeconds(59); }
  }*/
}
