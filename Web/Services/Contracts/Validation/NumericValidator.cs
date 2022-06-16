// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.NumericValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class NumericValidator : RangeValidator
  {
    private string decimalPlaces;

    public override void Init(NameValueCollection parameters)
    {
      base.Init(parameters);
      this.decimalPlaces = parameters["DecimalPlaces"];
    }

    internal override bool Validate(object value, out object errMsg)
    {
      if (!base.Validate(value, out errMsg))
        return false;
      if (value == null)
        return true;
      Decimal result;
      if (!Decimal.TryParse(value.ToString(), out result))
        return false;
      int? nullable1;
      Decimal? nullable2;
      if (this.Min.HasValue)
      {
        Decimal num = result;
        nullable1 = this.Min;
        nullable2 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
        Decimal valueOrDefault = nullable2.GetValueOrDefault();
        if (num < valueOrDefault & nullable2.HasValue)
        {
          errMsg = (object) "MinValue";
          return false;
        }
      }
      nullable1 = this.Max;
      if (nullable1.HasValue)
      {
        Decimal num = result;
        nullable1 = this.Max;
        nullable2 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
        Decimal valueOrDefault = nullable2.GetValueOrDefault();
        if (num > valueOrDefault & nullable2.HasValue)
        {
          errMsg = (object) "MaxValue";
          return false;
        }
      }
      return true;
    }

    internal override IList<VocabularyAnnotation> GetValidatorAnnotation(
      string annotationNamespace)
    {
      IList<VocabularyAnnotation> validatorAnnotation = base.GetValidatorAnnotation(annotationNamespace);
      if (!string.IsNullOrEmpty(this.decimalPlaces))
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".DecimalPlaces", (object) this.decimalPlaces);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      return validatorAnnotation;
    }

    internal new class Constants
    {
      internal const string DecimalPlaces = "DecimalPlaces";
    }
  }
}
