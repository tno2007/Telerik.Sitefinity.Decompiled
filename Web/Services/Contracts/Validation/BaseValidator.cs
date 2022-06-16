// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.BaseValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class BaseValidator
  {
    public virtual void Init(NameValueCollection parameters)
    {
      this.Required = this.ExtractParameter<bool>(parameters, "IsRequired");
      this.Updatable = this.ExtractParameter<bool>(parameters, "Updatable");
      string parameter = parameters["RegularExpression"];
      if (string.IsNullOrEmpty(parameter))
        return;
      this.RegularExpression = parameter;
    }

    internal virtual bool Validate(object value, out object error)
    {
      error = (object) null;
      if (this.Required && value == null)
      {
        error = (object) "Required";
        return false;
      }
      return string.IsNullOrEmpty(this.RegularExpression) || this.ValidateRegularExpression(value.ToString(), ref error);
    }

    internal virtual bool ValidateRegularExpression(string value, ref object errMsg)
    {
      if (Regex.IsMatch(value, this.RegularExpression, RegexOptions.IgnoreCase))
        return true;
      errMsg = (object) "RegularExpression";
      return false;
    }

    internal virtual IList<VocabularyAnnotation> GetValidatorAnnotation(
      string annotationNamespace)
    {
      List<VocabularyAnnotation> validatorAnnotation = new List<VocabularyAnnotation>();
      if (this.Required)
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".IsRequired", (object) this.Required);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      if (!this.Updatable)
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".Updatable", (object) this.Updatable);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      if (!string.IsNullOrEmpty(this.RegularExpression))
      {
        VocabularyAnnotation vocabularyAnnotation = new VocabularyAnnotation(annotationNamespace, BaseValidator.Constants.Validation + ".RegularExpression", (object) this.RegularExpression);
        validatorAnnotation.Add(vocabularyAnnotation);
      }
      return (IList<VocabularyAnnotation>) validatorAnnotation;
    }

    protected T ExtractParameter<T>(NameValueCollection parameters, string paramName)
    {
      string parameter1 = parameters[paramName];
      T parameter2 = default (T);
      try
      {
        parameter2 = (T) Convert.ChangeType((object) parameter1, typeof (T));
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
      }
      return parameter2;
    }

    internal bool Required { get; private set; }

    internal bool Updatable { get; private set; }

    internal string RegularExpression { get; set; }

    internal class Constants
    {
      internal const string Required = "IsRequired";
      internal const string Updatable = "Updatable";
      internal static readonly string Validation = nameof (Validation);
    }
  }
}
