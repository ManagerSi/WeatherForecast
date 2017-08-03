using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace Mobizone.TSIC.Web {
  public class TSICMetadataProvider : DataAnnotationsModelMetadataProvider {
    protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName) {
      var metaData = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

      if (attributes.OfType<UCWebCheckBox>().Any()) {
        metaData.AdditionalValues.Add("UCWebCheckBox", true);
      }
      return metaData;
    }
  }
}
