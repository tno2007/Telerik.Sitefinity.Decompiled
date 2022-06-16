// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Validation.TaxaValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts.Validation
{
  internal class TaxaValidator : BaseValidator
  {
    private bool allowMultiple;

    public override void Init(NameValueCollection parameters)
    {
      base.Init(parameters);
      this.allowMultiple = this.ExtractParameter<bool>(parameters, "AllowMultiple");
    }

    internal override bool Validate(object value, out object errMsg)
    {
      if (base.Validate(value, out errMsg))
      {
        IList<Guid> guidList = value as IList<Guid>;
        if (!this.allowMultiple && guidList.Count > 1)
        {
          errMsg = (object) "Multiple values are not allowed for this property.";
          return false;
        }
      }
      return true;
    }

    internal override IList<VocabularyAnnotation> GetValidatorAnnotation(
      string annotationNamespace)
    {
      IList<VocabularyAnnotation> validatorAnnotation = base.GetValidatorAnnotation(annotationNamespace);
      validatorAnnotation.Add(new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".AllowMultiple", (object) this.allowMultiple));
      return validatorAnnotation;
    }

    internal new class Constants
    {
      internal const string AllowMultiple = "AllowMultiple";
    }
  }
}
