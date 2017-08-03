using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Globalization;
namespace Mobizone.TSIC.Web {

  public class WapBoolModelBinder : DefaultModelBinder {
    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
      
      if (!bindingContext.ModelMetadata.AdditionalValues.ContainsKey("UCWebCheckBox")) {
        return base.BindModel(controllerContext, bindingContext);
      }
      var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
      if (value != null) {
        var values = value.AttemptedValue.Split(',');
        bool b1, b2;
        if (values.Length != 2 ||
          !Boolean.TryParse(values[0], out b1) ||
          !Boolean.TryParse(values[1], out b2)) {
          return base.BindModel(controllerContext, bindingContext);
        }

        // one is true and other is false
        if (b1 ^ b2) {
          return true;
        }

      }
      return base.BindModel(controllerContext, bindingContext);
    }
  }
  public class WapDateModelBinder : DefaultModelBinder {
    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
      var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
      var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

      if (bindingContext.ModelType == typeof(DateTime?) && string.IsNullOrEmpty(value.AttemptedValue)) {
        return null;
      }

      if (!string.IsNullOrEmpty(displayFormat) && value != null) {
        DateTime date;
        displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
        // use the format specified in the DisplayFormat attribute to parse the date
        if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) {
          return date;
        } else {
          bindingContext.ModelState.AddModelError(
              bindingContext.ModelName,
              string.Format("{0} 不是一个正确的日期格式", value.AttemptedValue)
          );
          bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);
          return null;
        }
      }
      var rst = base.BindModel(controllerContext, bindingContext);
      return rst;
    }
  }
}
