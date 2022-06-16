// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Validation.DateTimeValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts.Validation
{
  internal class DateTimeValidator : BaseValidator
  {
    public override void Init(NameValueCollection parameters)
    {
      base.Init(parameters);
      string parameter1 = parameters["MinValue"];
      if (parameter1 != null)
        this.MinValue = new DateTime?(DateTime.Parse(parameter1, (IFormatProvider) CultureInfo.InvariantCulture).ToUniversalTime());
      string parameter2 = parameters["MaxValue"];
      if (parameter2 == null)
        return;
      this.MaxValue = new DateTime?(DateTime.Parse(parameter2, (IFormatProvider) CultureInfo.InvariantCulture).ToUniversalTime());
    }

    internal override bool Validate(object value, out object error)
    {
      bool flag = false;
      if (base.Validate(value, out error))
      {
        DateTime? nullable1 = new DateTime?();
        if (value is string)
          nullable1 = new DateTime?(DateTime.Parse((string) value, (IFormatProvider) null, DateTimeStyles.RoundtripKind));
        else if (value != null)
          nullable1 = new DateTime?(((DateTimeOffset) value).DateTime);
        DateTime? nullable2;
        if (this.MinValue.HasValue)
        {
          DateTime dateTime = this.MinValue.Value;
          nullable2 = nullable1;
          if ((nullable2.HasValue ? (dateTime > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            error = (object) "MinValue";
            return false;
          }
        }
        nullable2 = this.MaxValue;
        if (nullable2.HasValue)
        {
          DateTime dateTime = this.MaxValue.Value;
          nullable2 = nullable1;
          if ((nullable2.HasValue ? (dateTime < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            error = (object) "MaxValue";
            return false;
          }
        }
        flag = true;
      }
      return flag;
    }

    internal override IList<VocabularyAnnotation> GetValidatorAnnotation(
      string annotationNamespace)
    {
      IList<VocabularyAnnotation> validatorAnnotation = base.GetValidatorAnnotation(annotationNamespace);
      if (this.MinValue.HasValue)
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".MinValue", (object) this.MinValue.Value.ToIsoFormat());
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      DateTime? maxValue = this.MaxValue;
      if (maxValue.HasValue)
      {
        string nameSpace = annotationNamespace;
        string name = BaseValidator.Constants.Validation + ".MaxValue";
        maxValue = this.MaxValue;
        string isoFormat = maxValue.Value.ToIsoFormat();
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(nameSpace, name, (object) isoFormat);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      return validatorAnnotation;
    }

    internal DateTime? MinValue { get; set; }

    internal DateTime? MaxValue { get; set; }

    internal new class Constants
    {
      internal const string Min = "MinValue";
      internal const string Max = "MaxValue";
    }
  }
}
