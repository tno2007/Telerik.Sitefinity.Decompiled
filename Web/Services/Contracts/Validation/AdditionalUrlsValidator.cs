// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.AdditionalUrlsValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Web.Services.Contracts.Validation;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class AdditionalUrlsValidator : BaseValidator, IItemValidator
  {
    private string validationRegex;

    public override void Init(NameValueCollection parameters)
    {
      base.Init(parameters);
      this.validationRegex = parameters["ValidateRegex"];
    }

    public IManager Manager { get; set; }

    public object Item { get; set; }

    internal override bool Validate(object value, out object error)
    {
      error = (object) null;
      if (!(value is IEnumerable<string> strings))
        throw new InvalidOperationException("Validator works with a collection of urls");
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      foreach (string str1 in strings)
      {
        string str2 = (string) null;
        if (!base.Validate((object) str1, out error))
          dictionary.Add(str1, (object) str2);
        else if (this.validationRegex != null && !Regex.IsMatch(str1, this.validationRegex))
          dictionary.Add(str1, (object) "InvalidUrlFormat");
        else if (CommonMethods.IsUrlDuplicate(this.Manager.Provider, this.Item, str1))
          dictionary.Add(str1, (object) "UrlExists");
      }
      if (dictionary.Count <= 0)
        return true;
      error = (object) dictionary;
      return false;
    }

    internal override IList<VocabularyAnnotation> GetValidatorAnnotation(
      string annotationNamespace)
    {
      IList<VocabularyAnnotation> validatorAnnotation = base.GetValidatorAnnotation(annotationNamespace);
      if (!string.IsNullOrEmpty(this.validationRegex))
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".RegularExpression", (object) this.validationRegex);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      return validatorAnnotation;
    }

    internal new class Constants
    {
      internal const string Manager = "Manager";
      internal const string Item = "Item";
      internal const string ValidateRegex = "ValidateRegex";
    }

    internal class ValidationMessages
    {
      internal const string InvalidUrlFormat = "InvalidUrlFormat";
    }
  }
}
