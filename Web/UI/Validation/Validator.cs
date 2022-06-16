// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.Validator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;
using Telerik.Sitefinity.Web.UI.Validation.Enums;

namespace Telerik.Sitefinity.Web.UI.Validation
{
  /// <summary>Base class defining validation mechanism</summary>
  public class Validator : IValidatorDefinitionBase
  {
    /// <summary>Gets or sets function for comparing value.</summary>
    /// <value>The function for comparing value.</value>
    private readonly Func<IComparingValidatorDefinition, object> getComparingValue;
    private string errorMessage;
    private string regularExpression;
    /// <summary>Regex pattern for alpha numeric.</summary>
    public static readonly string AlphaNumericRegexPattern = "^[-_a-zA-Z0-9]*$";
    /// <summary>Regex pattern for currency.</summary>
    public static readonly string CurrencyRegexPattern = "^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\\.[0-9]{2})?|(?:\\.[0-9]{3})*(?:,[0-9]{2})?)$";
    /// <summary>
    /// Regex pattern for email address. Consider using <see cref="F:Telerik.Sitefinity.Constants.EmailAddressRegexPattern" />
    /// </summary>
    public static readonly string EmailAddressRegexPattern = "^[a-zA-Z0-9.!#$%&'*\\+\\-/=?^_`{|}~]+@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,63}$";
    /// <summary>Regex pattern for integer.</summary>
    public static readonly string IntegerRegexPattern = "^[-+]?\\d+$";
    /// <summary>Regex pattern for internet URL.</summary>
    public static readonly string InternetUrlRegexPattern = "\\b(?:(?:https?|ftp|file)://|www\\.|ftp\\.)[-a-zA-Z0-9+&@#/%=~_|$?!:,.]*[a-zA-Z0-9+&@#/%=~_|$]";
    /// <summary>Regex pattern for non alpha numeric.</summary>
    public static readonly string NonAlphaNumericRegexPattern = "^[^-_a-zA-Z0-9]+$";
    /// <summary>Regex pattern for numeric.</summary>
    public static readonly string NumericRegexPattern = "^^[-+]?[0-9]+((,|\\.)[0-9]+)?$";
    /// <summary>Regex pattern for percent.</summary>
    public static readonly string PercentRegexPattern = "^100$|^\\s*(\\d{0,2})((\\.|\\,)(\\d*))?\\s*\\%?\\s*$";
    /// <summary>Regex pattern for US social security.</summary>
    public static readonly string USSocialSecurityRegexPattern = "^(?!000)(?!666)(?:[0-6]\\d{2}|7(?:[0-356]\\d|7[012]))[- ](?!00)\\d{2}[- ](?!0000)\\d{4}$";
    /// <summary>Regex pattern for US ZIP code.</summary>
    public static readonly string USZipCodeRegexPattern = "^[0-9]{5}(?:-[0-9]{4})?$";
    /// <summary>Regex pattern for telephone number.</summary>
    public static readonly string TelRegexPattern = "^\\+?(\\d[\\d\\-\\.\\s]+)?(\\([\\d\\-\\.\\s]+\\))?[\\d\\-\\.\\s]+\\d$";
    /// <summary>Regex pattern for colors.</summary>
    public static readonly string ColorRegexPattern = "^#(?:[0-9a-fA-F]{3}){1,2}$";
    private string regularExpressionSeparator;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Validator" /> class
    /// with default values for all properties.
    /// </summary>
    public Validator()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Validator" /> class from a given <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Contracts.IValidatorDefinition" />.
    /// </summary>
    /// <param name="definition">The definition to use when initializing an instance.</param>
    public Validator(IValidatorDefinition definition)
      : this(definition, (Control) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Validator" /> class.
    /// </summary>
    /// <param name="definition">The definition.</param>
    /// <param name="comparingValidationNamingContainer">Sets in which naming container to search the control to compare.</param>
    public Validator(IValidatorDefinition definition, Control comparingValidationNamingContainer)
    {
      this.Configure(definition);
      this.ComparingValidationNamingContainer = comparingValidationNamingContainer;
      this.getComparingValue = new Func<IComparingValidatorDefinition, object>(this.GetComparingControlValue);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Validator" /> class.
    /// </summary>
    /// <param name="definition">The definition.</param>
    /// <param name="getComparingValue">Delegate that extracts the value for comparison.</param>
    public Validator(
      IValidatorDefinition definition,
      Func<IComparingValidatorDefinition, object> getComparingValue)
    {
      this.Configure(definition);
      this.getComparingValue = getComparingValue;
    }

    /// <summary>
    /// Gets the error message determined during the IsValid method call.
    /// </summary>
    /// <value>The error message determined during the IsValid method call.</value>
    /// <remarks>If the last validation was successful null is returned.</remarks>
    public string ErrorMessage => this.errorMessage;

    /// <summary>
    /// Gets or sets an expected <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat" /> format of the.
    /// validated value
    /// </summary>
    /// <value>The expected format.</value>
    public virtual ValidationFormat ExpectedFormat { get; set; }

    /// <summary>Gets or sets the maximum length.</summary>
    public virtual int MaxLength { get; set; }

    /// <summary>Gets or sets the minimum length.</summary>
    public virtual int MinLength { get; set; }

    /// <summary>
    /// Gets or sets the maximum value to use when validating data.
    /// </summary>
    /// <value>The max value.</value>
    public virtual object MaxValue { get; set; }

    /// <summary>
    /// Gets or sets the minimum value to use when validating data.
    /// </summary>
    public virtual object MinValue { get; set; }

    /// <summary>
    /// Gets or sets the regular expression to use when evaluating string.
    /// </summary>
    public virtual string RegularExpression
    {
      get => this.regularExpression;
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          try
          {
            Regex.IsMatch(string.Empty, value);
          }
          catch (Exception ex)
          {
            throw new ArgumentException(string.Format(Res.Get<ErrorMessages>().InvalidRegularExpression, (object) value), ex);
          }
        }
        this.regularExpression = value;
      }
    }

    /// <summary>
    /// Gets or sets the separator to use when evaluating string with custom regular expression.
    /// </summary>
    public virtual string RegularExpressionSeparator
    {
      get => this.regularExpressionSeparator;
      set => this.regularExpressionSeparator = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this field control's data must have a value.
    /// </summary>
    /// <value><c>true</c> if value is required; otherwise, <c>false</c>.</value>
    public virtual bool Required { get; set; }

    /// <summary>
    /// Gets or sets the message shown when alpha numeric validation has failed.
    /// </summary>
    /// <value>The alpha numeric violation message.</value>
    public string AlphaNumericViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when currency validation has failed.
    /// </summary>
    /// <value>The currency violation message.</value>
    public string CurrencyViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when email address validation has failed.
    /// </summary>
    /// <value>The email address violation message.</value>
    public string EmailAddressViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when integer validation has failed.
    /// </summary>
    /// <value>The integer violation message.</value>
    public string IntegerViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when internet url validation has failed.
    /// </summary>
    /// <value>The internet URL violation message.</value>
    public string InternetUrlViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when max length validation has failed.
    /// </summary>
    /// <value>The max length violation message.</value>
    public string MaxLengthViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when max value validation has failed.
    /// </summary>
    /// <value>The max value violation message.</value>
    public string MaxValueViolationMessage { get; set; }

    /// <summary>Gets or sets the violation message CSS class.</summary>
    /// <value>The message CSS class.</value>
    public string MessageCssClass { get; set; }

    /// <summary>Gets or sets the name of the violation message tag.</summary>
    /// <value>The name of the message tag.</value>
    public string MessageTagName { get; set; }

    /// <summary>
    /// Gets or sets the message shown when minimum length validation has failed.
    /// </summary>
    /// <value>The min length violation message.</value>
    public string MinLengthViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when minimum value validation has failed.
    /// </summary>
    /// <value>The min value violation message.</value>
    public string MinValueViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when non alphanumeric validation has failed.
    /// </summary>
    /// <value>The non alpha numeric violation message.</value>
    public string NonAlphaNumericViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when numeric validation has failed.
    /// </summary>
    /// <value>The numeric violation message.</value>
    public string NumericViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when percentage validation has failed.
    /// </summary>
    /// <value>The percentage violation message.</value>
    public string PercentageViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when regular expression validation has failed.
    /// </summary>
    /// <value>The regular expression violation message.</value>
    public string RegularExpressionViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when required validation has failed.
    /// </summary>
    /// <value>The required violation message.</value>
    public string RequiredViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when US social security number validation has failed.
    /// </summary>
    /// <value>The US social security number violation message.</value>
    public string USSocialSecurityNumberViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when US zip code validation has failed.
    /// </summary>
    /// <value>The US zip code violation message.</value>
    public string USZipCodeViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to validate if invisible.
    /// </summary>
    /// <value>Whether to validate if invisible.</value>
    public bool ValidateIfInvisible { get; set; }

    /// <summary>
    /// Gets or sets validation requirements that are going to be used when comparing against other controls' values.
    /// </summary>
    /// <value>The comparing validator definition.</value>
    public Collection<IComparingValidatorDefinition> ComparingValidatorDefinitions { get; set; }

    /// <summary>
    /// Gets or sets the resource class used to localize message text and title.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If this property is set; text properties will be treated as
    /// keys of localized entry in the localization class; otherwise they will be displayed
    /// as is.
    /// </remarks>
    public string ResourceClassId { get; set; }

    /// <summary>
    /// Gets or sets the in which naming container to search the control to compare.
    /// </summary>
    public Control ComparingValidationNamingContainer { get; set; }

    /// <summary>Occurs before the validator starts validating.</summary>
    public virtual event EventHandler<ValidatingEventArgs> Validating;

    /// <summary>Occurs when the validator has finished validating.</summary>
    public virtual event EventHandler<ValidatedEventArgs> Validated;

    /// <summary>Resolves the localized values.</summary>
    /// <param name="target">The target.</param>
    /// <param name="resourceClassId">The resource class identifier.</param>
    /// <param name="source">The source.</param>
    public static void ResolveLocalizedValues(
      IValidatorDefinitionBase target,
      string resourceClassId,
      IValidatorDefinitionBase source = null)
    {
      if (source == null)
        source = target;
      target.AlphaNumericViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.AlphaNumericViolationMessage);
      target.CurrencyViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.CurrencyViolationMessage);
      target.EmailAddressViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.EmailAddressViolationMessage);
      target.IntegerViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.IntegerViolationMessage);
      target.InternetUrlViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.InternetUrlViolationMessage);
      target.MaxLengthViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.MaxLengthViolationMessage);
      target.MaxValueViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.MaxValueViolationMessage);
      target.MinLengthViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.MinLengthViolationMessage);
      target.MinValueViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.MinValueViolationMessage);
      target.NonAlphaNumericViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.NonAlphaNumericViolationMessage);
      target.NumericViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.NumericViolationMessage);
      target.PercentageViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.PercentageViolationMessage);
      target.RegularExpressionViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.RegularExpressionViolationMessage);
      target.RequiredViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.RequiredViolationMessage);
      target.USSocialSecurityNumberViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.USSocialSecurityNumberViolationMessage);
      target.USZipCodeViolationMessage = Res.ResolveLocalizedValue(resourceClassId, source.USZipCodeViolationMessage);
    }

    /// <summary>Gets the validation property of the component.</summary>
    /// <param name="component">The component.</param>
    /// <returns>The validation property of the component.</returns>
    public static PropertyDescriptor GetValidationProperty(object component)
    {
      ValidationPropertyAttribute attribute = (ValidationPropertyAttribute) TypeDescriptor.GetAttributes(component)[typeof (ValidationPropertyAttribute)];
      return attribute != null && attribute.Name != null ? TypeDescriptor.GetProperties(component, (Attribute[]) null)[attribute.Name] : (PropertyDescriptor) null;
    }

    /// <summary>Determines whether the specified value is valid.</summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
    /// </returns>
    public bool IsValid(object value)
    {
      bool isValid = true;
      ValidatingEventArgs validatingEventArgs = this.GetValidatingEventArgs(value);
      this.OnValidating(validatingEventArgs);
      if (!validatingEventArgs.Cancel)
      {
        if (value == null)
        {
          if (this.Required)
            this.errorMessage = this.RequiredViolationMessage;
          isValid = !this.Required;
        }
        else
          isValid = this.Validate(value);
      }
      this.OnValidated(this.GetValidatedEventArgs(ref isValid, value));
      return isValid;
    }

    /// <summary>
    /// Configures the validation instance using <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Contracts.IValidatorDefinition" />
    /// </summary>
    /// <param name="definition">The <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Contracts.IValidatorDefinition" /> definition to use for configuring.</param>
    public virtual void Configure(IValidatorDefinition definition)
    {
      this.ResourceClassId = definition.ResourceClassId;
      this.ExpectedFormat = definition.ExpectedFormat;
      this.MinLength = definition.MinLength;
      this.MaxLength = definition.MaxLength;
      this.MinValue = definition.MinValue;
      this.MaxValue = definition.MaxValue;
      this.RegularExpression = definition.RegularExpression;
      bool? nullable;
      if (definition.Required.HasValue)
      {
        nullable = definition.Required;
        this.Required = nullable.Value;
      }
      nullable = definition.ValidateIfInvisible;
      if (nullable.HasValue)
      {
        nullable = definition.ValidateIfInvisible;
        this.ValidateIfInvisible = nullable.Value;
      }
      this.MessageCssClass = definition.MessageCssClass;
      this.MessageTagName = definition.MessageTagName;
      this.ComparingValidatorDefinitions = definition.ComparingValidatorDefinitions;
      Validator.ResolveLocalizedValues((IValidatorDefinitionBase) this, string.Empty, (IValidatorDefinitionBase) definition);
    }

    /// <summary>
    /// Checks if the validator's current settings are valid for the given value to validate type.
    /// </summary>
    /// <param name="valueType">The type of the value that will be validated.</param>
    /// <returns>True if the properties are valid and false otherwise.</returns>
    internal virtual bool ValidateProperties(Type valueType)
    {
      if (valueType == typeof (string))
        return this.ValidateStringProperties();
      if (valueType == typeof (DateTime))
        return this.ValidateDateTimeProperties();
      if (valueType == typeof (bool))
        return this.ValidateBoleanProperties();
      if (this.IsNumericType(valueType))
        return this.ValidateNumericProperties();
      if (valueType == typeof (List<string>))
        return this.ValidateListProperties();
      if (valueType == typeof (TrackedList<Guid>))
        return true;
      if (typeof (ContentLink).IsAssignableFrom(valueType))
        return this.ValidateContentLinkProperties();
      if (typeof (ChoiceOption).IsAssignableFrom(valueType) || typeof (ChoiceOption[]).IsAssignableFrom(valueType))
        return this.ValidateChoiceOptionProperties();
      throw new NotSupportedException("The validated value of type: " + valueType.FullName.ToString() + " is not supported.");
    }

    /// <summary>Validates the 'required' rule.</summary>
    /// <param name="value">The value.</param>
    /// <param name="valueType">Type of the value.</param>
    /// <returns>True if the value meets the 'required' rule and false otherwise.</returns>
    internal virtual bool ValidateRequired(object value, Type valueType)
    {
      if (valueType == typeof (string))
        return this.ValidateStringRequired(value);
      if (valueType == typeof (bool))
        return this.ValidateBoleanRequired(value);
      return valueType == typeof (List<string>) ? this.ValidateListItemRequired(value) : this.ValidateObjectRequered(value);
    }

    internal virtual bool ValidateRange(object value, Type valueType)
    {
      if (valueType == typeof (DateTime))
        return this.ValidateDateTimeRange(value);
      if (valueType == typeof (string))
        return this.ValidateStringRange(value);
      if (this.IsNumericType(valueType))
        return this.ValidateNumericRange(value);
      if (valueType == typeof (List<string>))
        return this.ValidateListRange(value);
      if (typeof (ContentLink).IsAssignableFrom(valueType))
        return this.ValidateContentLinkRange(value);
      if (valueType == typeof (TrackedList<Guid>) || valueType == typeof (bool) || typeof (ChoiceOption).IsAssignableFrom(valueType) || typeof (ChoiceOption[]).IsAssignableFrom(valueType))
        return true;
      throw new NotSupportedException("The validated value of type: " + valueType.FullName.ToString() + " is not supported.");
    }

    internal virtual bool ValidateRegex(object value, Type valueType) => !(valueType == typeof (string)) || this.ValidateStringRegex(value);

    internal virtual bool ValidateComparison(object value, Type valueType)
    {
      if (this.ComparingValidatorDefinitions != null && this.ComparingValidatorDefinitions.Count > 0 && this.getComparingValue != null)
      {
        foreach (IComparingValidatorDefinition validatorDefinition in this.ComparingValidatorDefinitions)
        {
          object obj = this.getComparingValue(validatorDefinition);
          if (obj != null)
          {
            if (validatorDefinition.ValidationDataType == "Number" && Decimal.TryParse(value.ToString(), NumberStyles.Currency, (IFormatProvider) null, out Decimal _))
            {
              bool validationResult = this.IsComparingResultValid(Decimal.Compare((Decimal) value, (Decimal) obj), validatorDefinition.Operator);
              this.ConditionalSetErrorMessage(validationResult, validatorDefinition.ValidationViolationMessage);
              return validationResult;
            }
            if (!(valueType == typeof (string)))
              throw new NotSupportedException("The validation of the specified value type is not supported.");
            bool validationResult1 = this.IsComparingResultValid(string.Compare((string) value, (string) obj), validatorDefinition.Operator);
            this.ConditionalSetErrorMessage(validationResult1, validatorDefinition.ValidationViolationMessage);
            return validationResult1;
          }
        }
      }
      return true;
    }

    internal virtual bool IsComparingResultValid(int comparingResult, ValidationCompareOperator op)
    {
      switch (op)
      {
        case ValidationCompareOperator.Equal:
          return comparingResult == 0;
        case ValidationCompareOperator.NotEqual:
          return (uint) comparingResult > 0U;
        case ValidationCompareOperator.GreaterThan:
          return comparingResult > 0;
        case ValidationCompareOperator.GreaterThanEqual:
          return comparingResult >= 0;
        case ValidationCompareOperator.LessThan:
          return comparingResult < 0;
        case ValidationCompareOperator.LessThanEqual:
          return comparingResult <= 0;
        default:
          throw new NotSupportedException(string.Format("Not supported ValidationCompareOperator '{0}'", (object) op.ToString()));
      }
    }

    internal virtual object GetComparingControlValue(
      IComparingValidatorDefinition comparingValidatorDefinition)
    {
      if (this.ComparingValidationNamingContainer == null)
        throw new ArgumentNullException("ComparingValidationNamingContainer", "The validator is configured with some Comparing validation but no Naming container is set. To use this feature you should specify a naming container that contains the ControlToCompare.");
      Control control = this.ComparingValidationNamingContainer.FindControl(comparingValidatorDefinition.ControlToCompare);
      return ((control != null ? Validator.GetValidationProperty((object) control) : throw new Exception(string.Format("No control with the specified:'{0}' was found for comparison.", (object) comparingValidatorDefinition.ControlToCompare))) ?? throw new Exception(string.Format("The specified control for comparison:'{0}' doesn't have a property marked with the 'ValidationPropertyAttribute'", (object) comparingValidatorDefinition.ControlToCompare))).GetValue((object) control);
    }

    internal virtual bool ValidateBoleanProperties()
    {
      if (this.MinLength > 0 || this.MinValue != null || this.MaxLength > 0 || this.MaxValue != null || this.ExpectedFormat != ValidationFormat.None || !string.IsNullOrEmpty(this.RegularExpression))
        throw new NotSupportedException("You have set a validation expectation that can't be verified against a boolean value.");
      return true;
    }

    internal virtual bool ValidateContentLinkProperties()
    {
      if (this.MinLength > 0 || this.MinValue != null || this.MaxLength > 0 || this.MaxValue != null || this.ExpectedFormat != ValidationFormat.None || !string.IsNullOrEmpty(this.RegularExpression))
        throw new NotSupportedException("You have set a validation expectation that can't be verified against a content link value.");
      return true;
    }

    internal virtual bool ValidateChoiceOptionProperties()
    {
      if (this.MinLength > 0 || this.MinValue != null || this.MaxLength > 0 || this.MaxValue != null || this.ExpectedFormat != ValidationFormat.None || !string.IsNullOrEmpty(this.RegularExpression))
        throw new NotSupportedException("You have set a validation expectation that can't be verified against a choice option value.");
      return true;
    }

    internal virtual bool ValidateBoleanRequired(object value) => !this.Required || Convert.ToBoolean(value);

    internal virtual bool ValidateDateTimeProperties()
    {
      if (!string.IsNullOrWhiteSpace(this.RegularExpression) || this.ExpectedFormat != ValidationFormat.None || this.MinLength != 0 || this.MaxLength != 0)
        throw new NotSupportedException("You have set a validation expectation that can't be verified against a DateTime value.");
      Type type = typeof (DateTime);
      if (this.MinValue != null && this.MinValue.GetType() != type)
        throw new ArgumentException("MinValue");
      bool flag1 = true;
      if (this.MaxValue != null && this.MaxValue.GetType() != type)
        throw new ArgumentException("MaxValue");
      bool flag2 = true;
      if (flag1 & flag2 && Convert.ToDateTime(this.MinValue) > Convert.ToDateTime(this.MaxValue))
        throw new ArgumentException("You have specified minimum value that is greater the the maximum value.");
      return true;
    }

    internal virtual bool ValidateObjectRequered(object value) => !this.Required || value != null;

    internal virtual bool ValidateDateTimeRange(object value)
    {
      if (value == null || this.MinValue == null && this.MaxValue == null)
        return true;
      bool validationResult1 = true;
      bool validationResult2 = true;
      DateTime dateTime1 = Convert.ToDateTime(value);
      if (this.MinValue != null)
      {
        DateTime dateTime2 = Convert.ToDateTime(this.MinValue);
        validationResult1 = dateTime1 >= dateTime2;
        this.ConditionalSetErrorMessage(validationResult1, this.MinValueViolationMessage);
      }
      if (this.MaxValue != null)
      {
        DateTime dateTime3 = Convert.ToDateTime(this.MaxValue);
        validationResult2 = dateTime1 <= dateTime3;
        this.ConditionalSetErrorMessage(validationResult2, this.MaxValueViolationMessage);
      }
      return validationResult1 & validationResult2;
    }

    internal virtual bool ValidateStringProperties()
    {
      if (this.MaxValue != null || this.MinValue != null)
        throw new NotSupportedException("You have set a validation expectation that can't be verified against a string value.");
      if (!string.IsNullOrEmpty(this.RegularExpression))
        this.ExpectedFormat = ValidationFormat.Custom;
      if (this.MaxLength > 0 && this.MinLength > 0 && this.MaxLength < this.MinLength)
        throw new ArgumentException("You have specified minimum length that is greater the the maximum length.");
      return true;
    }

    internal virtual bool ValidateStringRequired(object value)
    {
      if (!this.Required)
        return true;
      string str = (string) value;
      return str != null && !string.IsNullOrEmpty(str.Trim());
    }

    internal virtual bool ValidateStringRange(object value)
    {
      bool validationResult1 = true;
      bool validationResult2 = true;
      if (this.MinLength <= 0 && this.MaxLength <= 0)
        return true;
      string str = (string) value;
      if (this.MinLength > 0)
      {
        validationResult1 = str.Length >= this.MinLength;
        this.ConditionalSetErrorMessage(validationResult1, this.MinLengthViolationMessage);
      }
      if (this.MaxLength > 0)
      {
        validationResult2 = str.Length <= this.MaxLength;
        this.ConditionalSetErrorMessage(validationResult2, this.MaxLengthViolationMessage);
      }
      return validationResult1 & validationResult2;
    }

    internal virtual bool ValidateStringRegex(object value)
    {
      bool validationResult1 = true;
      string input = (string) value;
      if (input != null && input != string.Empty)
      {
        if (!string.IsNullOrEmpty(this.RegularExpression))
        {
          bool validationResult2 = Regex.IsMatch(input, this.RegularExpression);
          this.ConditionalSetErrorMessage(validationResult2, this.RegularExpressionViolationMessage);
          return validationResult2;
        }
        string pattern = string.Empty;
        string errorMessage = string.Empty;
        switch (this.ExpectedFormat)
        {
          case ValidationFormat.None:
            return true;
          case ValidationFormat.AlphaNumeric:
            pattern = Validator.AlphaNumericRegexPattern;
            errorMessage = this.AlphaNumericViolationMessage;
            break;
          case ValidationFormat.Currency:
            pattern = Validator.CurrencyRegexPattern;
            errorMessage = this.CurrencyViolationMessage;
            break;
          case ValidationFormat.EmailAddress:
            pattern = Validator.EmailAddressRegexPattern;
            errorMessage = this.EmailAddressViolationMessage;
            break;
          case ValidationFormat.Integer:
            pattern = Validator.IntegerRegexPattern;
            errorMessage = this.IntegerViolationMessage;
            break;
          case ValidationFormat.InternetUrl:
            pattern = Validator.InternetUrlRegexPattern;
            errorMessage = this.InternetUrlViolationMessage;
            break;
          case ValidationFormat.NonAlphaNumeric:
            pattern = Validator.NonAlphaNumericRegexPattern;
            errorMessage = this.NonAlphaNumericViolationMessage;
            break;
          case ValidationFormat.Numeric:
            pattern = Validator.NumericRegexPattern;
            errorMessage = this.NumericViolationMessage;
            break;
          case ValidationFormat.Percentage:
            pattern = Validator.PercentRegexPattern;
            errorMessage = this.PercentageViolationMessage;
            break;
          case ValidationFormat.USSocialSecurityNumber:
            pattern = Validator.USSocialSecurityRegexPattern;
            errorMessage = this.USSocialSecurityNumberViolationMessage;
            break;
          case ValidationFormat.USZipCode:
            pattern = Validator.USZipCodeRegexPattern;
            errorMessage = this.USZipCodeViolationMessage;
            break;
        }
        if (!string.IsNullOrEmpty(pattern))
        {
          validationResult1 = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
          this.ConditionalSetErrorMessage(validationResult1, errorMessage);
        }
      }
      return validationResult1;
    }

    internal virtual bool ValidateNumericProperties()
    {
      if (this.RegularExpression != null || this.ExpectedFormat != ValidationFormat.None || this.MinLength != 0 || this.MaxLength != 0)
        throw new NotSupportedException("You have set a validation expectation that can't be verified against a numerical value.");
      if (this.MinValue != null && !this.IsNumericType(this.MinValue.GetType()))
        throw new ArgumentException("The specified MinValue is not a known numeric format");
      bool flag1 = true;
      if (this.MaxValue != null && !this.IsNumericType(this.MaxValue.GetType()))
        throw new ArgumentException("The specified MaxValue is not a known numeric format");
      bool flag2 = true;
      if (flag1 & flag2 && Convert.ToDecimal(this.MinValue) > Convert.ToDecimal(this.MaxValue))
        throw new ArgumentException("You have specified minimum value that is greater the the maximum value.");
      return true;
    }

    internal virtual bool ValidateNumericRange(object value)
    {
      if (value == null || this.MinValue == null && this.MaxValue == null)
        return true;
      bool validationResult1 = true;
      bool validationResult2 = true;
      Decimal num1 = Convert.ToDecimal(value);
      if (this.MinValue != null)
      {
        Decimal num2 = Convert.ToDecimal(this.MinValue);
        validationResult1 = num1 >= num2;
        this.ConditionalSetErrorMessage(validationResult1, this.MinValueViolationMessage);
      }
      if (this.MaxValue != null)
      {
        Decimal num3 = Convert.ToDecimal(this.MaxValue);
        validationResult2 = num1 <= num3;
        this.ConditionalSetErrorMessage(validationResult2, this.MaxValueViolationMessage);
      }
      return validationResult1 & validationResult2;
    }

    internal virtual bool ValidateListProperties() => true;

    internal virtual bool ValidateListRange(object value) => true;

    internal virtual bool ValidateContentLinkRange(object value) => true;

    internal virtual bool ValidateListItemRequired(object value)
    {
      if (value == null)
        return false;
      return !this.Required || (value as IList).Count > 0;
    }

    internal virtual ValidatingEventArgs GetValidatingEventArgs(object value) => new ValidatingEventArgs(value);

    internal virtual ValidatedEventArgs GetValidatedEventArgs(
      ref bool isValid,
      object value)
    {
      return new ValidatedEventArgs(ref isValid, value, this.ErrorMessage);
    }

    internal virtual bool IsNumericType(Type type) => type == typeof (int) || type == typeof (short) || type == typeof (long) || type == typeof (float) || type == typeof (double) || type == typeof (Decimal) || type == typeof (byte);

    /// <summary>
    /// Raises the <see cref="E:Validating" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs" /> instance containing the event data.</param>
    /// <exception cref="T:System.ArgumentNullException">The 'e' should not be null.</exception>
    protected internal virtual void OnValidating(ValidatingEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (this.Validating == null)
        return;
      this.Validating((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:Validated" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs" /> instance containing the event data.</param>
    /// <exception cref="T:System.ArgumentNullException">The 'e' should not be null.</exception>
    protected internal virtual void OnValidated(ValidatedEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (this.Validated == null)
        return;
      this.Validated((object) this, e);
    }

    /// <summary>Validates the specified value.</summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>true if the passed value is valid or false if not.</returns>
    protected internal virtual bool Validate(object value)
    {
      this.errorMessage = (string) null;
      Type type = value.GetType();
      if (!this.ValidateProperties(type))
        return false;
      if (!this.ValidateRequired(value, type))
      {
        this.errorMessage = this.RequiredViolationMessage;
        return false;
      }
      return this.ValidateRange(value, type) && this.ValidateRegex(value, type) && this.ValidateComparison(value, type);
    }

    private void ConditionalSetErrorMessage(bool validationResult, string errorMessage)
    {
      if (validationResult)
        return;
      this.errorMessage = errorMessage;
    }
  }
}
