// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.StringValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class StringValidator : RangeValidator
  {
    public override void Init(NameValueCollection parameters)
    {
      base.Init(parameters);
      int? nullable;
      if (this.Min.HasValue)
      {
        nullable = this.Min;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
        {
          nullable = new int?();
          this.Min = nullable;
        }
      }
      nullable = this.Max;
      if (nullable.HasValue)
      {
        nullable = this.Max;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
        {
          nullable = new int?();
          this.Max = nullable;
        }
      }
      int result;
      if (int.TryParse(parameters["RecommendedCharacters"], out result))
        this.RecommendedCharacters = new int?(result);
      string parameter = parameters["SanitizeRegex"];
      if (string.IsNullOrEmpty(parameter))
        return;
      this.SanitizeRegex = parameter;
    }

    internal override bool Validate(object value, out object errMsg)
    {
      if (!base.Validate(value, out errMsg))
        return false;
      string str = value as string;
      if (!string.IsNullOrEmpty(str))
        str = str.Trim();
      if (string.IsNullOrEmpty(str))
      {
        if (!this.Required)
          return true;
        errMsg = (object) "Required";
        return false;
      }
      if (this.Min.HasValue)
      {
        int? min1 = this.Min;
        int num = 0;
        if (min1.GetValueOrDefault() > num & min1.HasValue)
        {
          int length = str.Length;
          int? min2 = this.Min;
          int valueOrDefault = min2.GetValueOrDefault();
          if (length < valueOrDefault & min2.HasValue)
          {
            errMsg = (object) "MinValue";
            return false;
          }
        }
      }
      if (this.Max.HasValue)
      {
        int? max1 = this.Max;
        int num = 0;
        if (max1.GetValueOrDefault() > num & max1.HasValue)
        {
          int length = str.Length;
          int? max2 = this.Max;
          int valueOrDefault = max2.GetValueOrDefault();
          if (length > valueOrDefault & max2.HasValue)
          {
            errMsg = (object) "MaxValue";
            return false;
          }
        }
      }
      return true;
    }

    internal override IList<VocabularyAnnotation> GetValidatorAnnotation(
      string annotationNamespace)
    {
      IList<VocabularyAnnotation> validatorAnnotation = base.GetValidatorAnnotation(annotationNamespace);
      if (this.RecommendedCharacters.HasValue)
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".RecommendedCharacters", (object) this.RecommendedCharacters);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      if (!string.IsNullOrEmpty(this.SanitizeRegex))
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".SanitizeRegex", (object) this.SanitizeRegex);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      return validatorAnnotation;
    }

    internal int? RecommendedCharacters { get; set; }

    internal string SanitizeRegex { get; set; }

    internal new class Constants
    {
      internal const string Regex = "RegularExpression";
      internal const string RecommendedCharacters = "RecommendedCharacters";
    }
  }
}
