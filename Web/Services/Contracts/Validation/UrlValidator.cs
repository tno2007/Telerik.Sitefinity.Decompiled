// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.UrlValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Web.Services.Contracts.Validation;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class UrlValidator : StringValidator, IItemValidator
  {
    public IManager Manager { get; set; }

    public object Item { get; set; }

    public override void Init(NameValueCollection parameters)
    {
      base.Init(parameters);
      string parameter1 = parameters["SanitizeRegex"];
      if (!string.IsNullOrEmpty(parameter1))
        this.SanitizeRegex = parameter1;
      string parameter2 = parameters["RegularExpressionReplaceCharacter"];
      if (string.IsNullOrEmpty(parameter2))
        return;
      this.SanitizeRegexReplaceCharacter = parameter2;
    }

    internal override bool Validate(object value, out object errMsg)
    {
      errMsg = (object) null;
      if (!base.Validate(value, out errMsg))
        return false;
      try
      {
        if (this.Manager == null || this.Item == null || !this.IsDuplicateUrl(this.Manager, this.Item))
          return true;
        errMsg = (object) "UrlExists";
        return false;
      }
      catch (Exception ex)
      {
        errMsg = (object) ex.Message;
        return false;
      }
    }

    internal override IList<VocabularyAnnotation> GetValidatorAnnotation(
      string annotationNamespace)
    {
      IList<VocabularyAnnotation> validatorAnnotation = base.GetValidatorAnnotation(annotationNamespace);
      if (!string.IsNullOrEmpty(this.SanitizeRegex))
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".SanitizeRegex", (object) this.SanitizeRegex);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      if (!string.IsNullOrEmpty(this.SanitizeRegexReplaceCharacter))
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".RegularExpressionReplaceCharacter", (object) this.SanitizeRegexReplaceCharacter);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      return validatorAnnotation;
    }

    internal virtual bool IsDuplicateUrl(IManager manager, object item) => CommonMethods.IsUrlDuplicate(manager.Provider, item);

    internal new string SanitizeRegex { get; set; }

    internal string SanitizeRegexReplaceCharacter { get; set; }

    internal new class Constants
    {
      internal const string Manager = "Manager";
      internal const string Item = "Item";
      internal const string SanitizeRegex = "SanitizeRegex";
      internal const string RegexReplaceCharacter = "RegularExpressionReplaceCharacter";
    }
  }
}
