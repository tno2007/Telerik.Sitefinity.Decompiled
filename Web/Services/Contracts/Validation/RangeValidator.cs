// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.RangeValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class RangeValidator : BaseValidator
  {
    public override void Init(NameValueCollection parameters)
    {
      base.Init(parameters);
      int result;
      if (int.TryParse(parameters["MinValue"], out result))
        this.Min = new int?(result);
      if (!int.TryParse(parameters["MaxValue"], out result))
        return;
      this.Max = new int?(result);
    }

    internal override IList<VocabularyAnnotation> GetValidatorAnnotation(
      string annotationNamespace)
    {
      IList<VocabularyAnnotation> validatorAnnotation = base.GetValidatorAnnotation(annotationNamespace);
      if (this.Min.HasValue)
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".MinValue", (object) this.Min);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      if (this.Max.HasValue)
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".MaxValue", (object) this.Max);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      return validatorAnnotation;
    }

    internal int? Min { get; set; }

    internal int? Max { get; set; }

    internal new class Constants
    {
      internal const string Min = "MinValue";
      internal const string Max = "MaxValue";
    }
  }
}
