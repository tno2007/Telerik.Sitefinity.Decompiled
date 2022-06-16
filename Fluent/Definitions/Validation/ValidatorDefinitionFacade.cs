// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Sitefinity.Web.UI.Validation.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Validation
{
  /// <summary>
  /// Fluent API facade that defines a definition for validator element.
  /// </summary>
  public class ValidatorDefinitionFacade<TParentFacade> where TParentFacade : class
  {
    private FieldControlDefinitionElement parentElement;
    private TParentFacade parentFacade;
    private string resourceClassId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="resourceClassId">The resource class id.</param>
    public ValidatorDefinitionFacade(
      FieldControlDefinitionElement parentElement,
      TParentFacade parentFacade,
      string resourceClassId)
    {
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      this.parentElement = parentElement != null ? parentElement : throw new ArgumentNullException(nameof (parentElement));
      this.parentFacade = parentFacade;
      this.resourceClassId = resourceClassId;
      this.Validator = new ValidatorDefinitionElement((ConfigElement) parentElement)
      {
        ResourceClassId = resourceClassId
      };
      parentElement.ValidatorConfig = this.Validator;
    }

    /// <summary>Gets or sets the current validator element.</summary>
    /// <value>The validator.</value>
    protected ValidatorDefinitionElement Validator { get; set; }

    /// <summary>
    /// Gets this <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Config.ValidatorDefinitionElement" /> instance.
    /// </summary>
    /// <returns></returns>
    public ValidatorDefinitionElement Get() => this.Validator;

    /// <summary>
    /// Sets the message shown when alpha numeric validation has failed.
    /// </summary>
    /// <param name="message">The alpha numeric violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetAlphaNumericViolationMessage(
      string message)
    {
      this.Validator.AlphaNumericViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when currency validation has failed.
    /// </summary>
    /// <param name="message">The currency violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetCurrencyViolationMessage(
      string message)
    {
      this.Validator.CurrencyViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when email address validation has failed.
    /// </summary>
    /// <param name="message">The email address violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetEmailAddressViolationMessage(
      string message)
    {
      this.Validator.EmailAddressViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when integer validation has failed.
    /// </summary>
    /// <param name="message">The integer violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetIntegerViolationMessage(
      string message)
    {
      this.Validator.IntegerViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when internet url validation has failed.
    /// </summary>
    /// <param name="message">The internet URL violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetInternetUrlViolationMessage(
      string message)
    {
      this.Validator.InternetUrlViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when max length validation has failed.
    /// </summary>
    /// <param name="message">The max length violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMaxLengthViolationMessage(
      string message)
    {
      this.Validator.MaxLengthViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when max value validation has failed.
    /// </summary>
    /// <param name="message">The max value violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMaxValueViolationMessage(
      string message)
    {
      this.Validator.MaxValueViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when minimum length validation has failed.
    /// </summary>
    /// <param name="message">The min length violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMinLengthViolationMessage(
      string message)
    {
      this.Validator.MinLengthViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when minimum value validation has failed.
    /// </summary>
    /// <param name="message">The min value violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMinValueViolationMessage(
      string message)
    {
      this.Validator.MinValueViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when non alphanumeric validation has failed.
    /// </summary>
    /// <param name="message">The non alpha numeric violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetNonAlphaNumericViolationMessage(
      string message)
    {
      this.Validator.NonAlphaNumericViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when numeric validation has failed.
    /// </summary>
    /// <param name="message">The numeric violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetNumericViolationMessage(
      string message)
    {
      this.Validator.NumericViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when percentage validation has failed.
    /// </summary>
    /// <param name="message">The percentage violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetPercentageViolationMessage(
      string message)
    {
      this.Validator.PercentageViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when regular expression validation has failed.
    /// </summary>
    /// <param name="message">The regular expression violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetRegularExpressionViolationMessage(
      string message)
    {
      this.Validator.RegularExpressionViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when required validation has failed.
    /// </summary>
    /// <param name="message">The required violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetRequiredViolationMessage(
      string message)
    {
      this.Validator.RequiredViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when US social security number validation has failed.
    /// </summary>
    /// <param name="message">The US social security number violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetUSSocialSecurityNumberViolationMessage(
      string message)
    {
      this.Validator.USSocialSecurityNumberViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets the message shown when US zip code validation has failed.
    /// </summary>
    /// <param name="message">The US zip code violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetUSZipCodeViolationMessage(
      string message)
    {
      this.Validator.USZipCodeViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>
    /// Sets an expected <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat" /> format of the
    /// validated value.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetExpectedFormat(
      ValidationFormat format)
    {
      this.Validator.ExpectedFormat = format;
      return this;
    }

    /// <summary>Sets the maximum length.</summary>
    /// <param name="length">The max length.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMaxLength(int length)
    {
      this.Validator.MaxLength = length;
      return this;
    }

    /// <summary>Sets the minimum length.</summary>
    /// <param name="length">The min length.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMinLength(int length)
    {
      this.Validator.MinLength = length;
      return this;
    }

    /// <summary>Sets the maximum value to use when validating data.</summary>
    /// <param name="maxValue">The max value.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMaxValue(
      object maxValue)
    {
      this.Validator.MaxValue = maxValue != null ? maxValue : throw new ArgumentNullException(nameof (maxValue));
      return this;
    }

    /// <summary>Sets the minimum value to use when validating data.</summary>
    /// <param name="minValue">The min value.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMinValue(
      object minValue)
    {
      this.Validator.MinValue = minValue != null ? minValue : throw new ArgumentNullException(nameof (minValue));
      return this;
    }

    /// <summary>
    /// Sets the regular expression to use when evaluating string.
    /// </summary>
    /// <param name="expression">The regular expression.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetRegularExpression(
      string expression)
    {
      this.Validator.RegularExpression = !string.IsNullOrEmpty(expression) ? expression : throw new ArgumentNullException(nameof (expression));
      return this;
    }

    /// <summary>
    /// Sets the separator to use when evaluating string with custom regular expression.
    /// </summary>
    /// <param name="separator">The separator.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetRegularExpressionSeparator(
      string separator)
    {
      this.Validator.RegularExpressionSeparator = !string.IsNullOrEmpty(separator) ? separator : throw new ArgumentNullException(nameof (separator));
      return this;
    }

    /// <summary>
    /// Sets that this field control's data must have a value.
    /// </summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> MakeRequired()
    {
      this.Validator.Required = new bool?(true);
      this.Validator.MessageCssClass = "sfError";
      return this;
    }

    /// <summary>Makes field control optional.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> MakeOptional()
    {
      this.Validator.Required = new bool?(false);
      return this;
    }

    /// <summary>Sets the violation message CSS class.</summary>
    /// <param name="cssClass">The message CSS class.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMessageCssClass(
      string cssClass)
    {
      this.Validator.MessageCssClass = !string.IsNullOrEmpty(cssClass) ? cssClass : throw new ArgumentNullException(nameof (cssClass));
      return this;
    }

    /// <summary>Sets the name of the violation message tag.</summary>
    /// <param name="tagName">The tag name.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> SetMessageTagName(
      string tagName)
    {
      this.Validator.MessageTagName = !string.IsNullOrEmpty(tagName) ? tagName : throw new ArgumentNullException(nameof (tagName));
      return this;
    }

    /// <summary>Validates if the validated component is invisible.</summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> ValidateIfInvisible()
    {
      this.Validator.ValidateIfInvisible = new bool?(true);
      return this;
    }

    /// <summary>
    /// Does not validate if the validated component is invisible.
    /// </summary>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.
    /// </returns>
    public ValidatorDefinitionFacade<TParentFacade> DoNotValidateIfInvisible()
    {
      this.Validator.ValidateIfInvisible = new bool?(false);
      return this;
    }

    /// <summary>
    /// Sets the localization class for the validator that should be used
    /// for localizing the properties. When this method is called, all properties
    /// will start to behave as resource keys.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// The type of the class which should be used to localize the string properties.
    /// </typeparam>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.</returns>
    public ValidatorDefinitionFacade<TParentFacade> LocalizeUsing<TResourceClass>() where TResourceClass : Resource
    {
      this.Validator.ResourceClassId = typeof (TResourceClass).Name;
      return this;
    }

    /// <summary>
    /// Sets the localization class for the validator to empty string. When this method is called, the default error messages will be displayed.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ValidatorDefinitionFacade`1" />.</returns>
    public ValidatorDefinitionFacade<TParentFacade> LocalizeUsingDefaultErrorMessages()
    {
      this.Validator.ResourceClassId = string.Empty;
      return this;
    }

    /// <summary>Adds the comparing validator.</summary>
    /// <returns></returns>
    public ComparingValidatorDefinitionFacade<ValidatorDefinitionFacade<TParentFacade>> AddComparingValidator() => new ComparingValidatorDefinitionFacade<ValidatorDefinitionFacade<TParentFacade>>(this.Validator.ComparingValidatorDefinitionsConfig, this);

    /// <summary>Returns to parent facade.</summary>
    /// <returns>The parent facade which initialized this facade.</returns>
    public TParentFacade Done() => this.parentFacade;
  }
}
