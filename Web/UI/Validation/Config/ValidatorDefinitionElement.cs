// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.Config.ValidatorDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Enums;

namespace Telerik.Sitefinity.Web.UI.Validation.Config
{
  /// <summary>
  /// The configuration element for the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Validator" />.
  /// </summary>
  public class ValidatorDefinitionElement : 
    ConfigElement,
    IValidatorDefinition,
    IDefinition,
    IValidatorDefinitionBase
  {
    private string controlDefinitionName;
    private string viewName;
    private string sectionName;
    private string fieldName;
    private Collection<IComparingValidatorDefinition> comparingDefinitions;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ValidatorDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    public string SectionName
    {
      get => this.sectionName;
      set => this.sectionName = value;
    }

    public string FieldName
    {
      get => this.fieldName;
      set => this.fieldName = value;
    }

    /// <summary>
    /// Gets or sets an expected <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat" /> format of the
    /// validated value
    /// </summary>
    /// <value>The expected format.</value>
    [ConfigurationProperty("expectedFormat", DefaultValue = ValidationFormat.None)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpectedFormatDescription", Title = "ExpectedFormatCaption")]
    public ValidationFormat ExpectedFormat
    {
      get => (ValidationFormat) this["expectedFormat"];
      set => this["expectedFormat"] = (object) value;
    }

    /// <summary>Gets or sets the maximum length.</summary>
    /// <value></value>
    [ConfigurationProperty("maxLength", DefaultValue = 0)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxLengthDescription", Title = "MaxLengthCaption")]
    public int MaxLength
    {
      get => (int) this["maxLength"];
      set => this["maxLength"] = (object) value;
    }

    /// <summary>Gets or sets the minimum length.</summary>
    /// <value></value>
    [ConfigurationProperty("minLength", DefaultValue = 0)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MinLengthDescription", Title = "MinLengthCaption")]
    public int MinLength
    {
      get => (int) this["minLength"];
      set => this["minLength"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the maximum value to use when validating data.
    /// </summary>
    /// <value>The max value.</value>
    [ConfigurationProperty("maxValue")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxValueDescription", Title = "MaxValueCaption")]
    public object MaxValue
    {
      get => this["maxValue"];
      set => this["maxValue"] = value;
    }

    /// <summary>
    /// Gets or sets the minimum value to use when validating data.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("minValue")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MinValueDescription", Title = "MinValueCaption")]
    public object MinValue
    {
      get => this["minValue"];
      set => this["minValue"] = value;
    }

    /// <summary>
    /// Gets or sets the minimum value to use when validating data.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("recommendedCharactersCount")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RecommendedCharactersDescription", Title = "RecommendedCharactersCaption")]
    public int? RecommendedCharactersCount
    {
      get => (int?) this["recommendedCharactersCount"];
      set => this["recommendedCharactersCount"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the regular expression to use when evaluating a string.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("regularExpression", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RegularExpressionDescription", Title = "RegularExpressionCaption")]
    public string RegularExpression
    {
      get => (string) this["regularExpression"];
      set => this["regularExpression"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the regular expression to use when evaluating a string.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("regularExpressionSeparator", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RegularExpressionSeparatorDescription", Title = "RegularExpressionSeparatorCaption")]
    public string RegularExpressionSeparator
    {
      get => (string) this["regularExpressionSeparator"];
      set => this["regularExpressionSeparator"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether this field control's data must have a value.
    /// </summary>
    /// <value><c>true</c> if value is required; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("required", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RequiredDescription", Title = "RequiredCaption")]
    public bool? Required
    {
      get => (bool?) this["required"];
      set => this["required"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when alpha numeric validation has failed.
    /// </summary>
    /// <value>The alpha numeric violation message.</value>
    [ConfigurationProperty("alphaNumericViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AlphaNumericViolationMessageDescription", Title = "AlphaNumericViolationMessageCaption")]
    public string AlphaNumericViolationMessage
    {
      get => (string) this["alphaNumericViolationMessage"];
      set => this["alphaNumericViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when currency validation has failed.
    /// </summary>
    /// <value>The currency violation message.</value>
    [ConfigurationProperty("currencyViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CurrencyViolationMessageDescription", Title = "CurrencyViolationMessageCaption")]
    public string CurrencyViolationMessage
    {
      get => (string) this["currencyViolationMessage"];
      set => this["currencyViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when email address validation has failed.
    /// </summary>
    /// <value>The email address violation message.</value>
    [ConfigurationProperty("emailAddressViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EmailAddressViolationMessageDescription", Title = "EmailAddressViolationMessageCaption")]
    public string EmailAddressViolationMessage
    {
      get => (string) this["emailAddressViolationMessage"];
      set => this["emailAddressViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when integer validation has failed.
    /// </summary>
    /// <value>The integer violation message.</value>
    [ConfigurationProperty("integerViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IntegerViolationMessageDescription", Title = "IntegerViolationMessageCaption")]
    public string IntegerViolationMessage
    {
      get => (string) this["integerViolationMessage"];
      set => this["integerViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when internet url validation has failed.
    /// </summary>
    /// <value>The internet URL violation message.</value>
    [ConfigurationProperty("interneturlviolationmessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "InternetUrlViolationMessageDescription", Title = "InternetUrlViolationMessageCaption")]
    public string InternetUrlViolationMessage
    {
      get => (string) this["interneturlviolationmessage"];
      set => this["interneturlviolationmessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when max length validation has failed.
    /// </summary>
    /// <value>The max length violation message.</value>
    [ConfigurationProperty("maxLengthViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxLengthViolationMessageDescription", Title = "MaxLengthViolationMessageCaption")]
    public string MaxLengthViolationMessage
    {
      get => (string) this["maxLengthViolationMessage"];
      set => this["maxLengthViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when max value validation has failed.
    /// </summary>
    /// <value>The max value violation message.</value>
    [ConfigurationProperty("maxValueViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxValueViolationMessageDescription", Title = "MaxValueViolationMessageCaption")]
    public string MaxValueViolationMessage
    {
      get => (string) this["maxValueViolationMessage"];
      set => this["maxValueViolationMessage"] = (object) value;
    }

    /// <summary>Gets or sets the violation message CSS class.</summary>
    /// <value>The message CSS class.</value>
    [ConfigurationProperty("messageCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MessageCssClassDescription", Title = "MessageCssClassCaption")]
    public string MessageCssClass
    {
      get => (string) this["messageCssClass"];
      set => this["messageCssClass"] = (object) value;
    }

    /// <summary>Gets or sets the name of the violation message tag.</summary>
    /// <value>The name of the message tag.</value>
    [ConfigurationProperty("messageTagName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MessageTagNameDescription", Title = "MessageTagNameCaption")]
    public string MessageTagName
    {
      get => (string) this["messageTagName"];
      set => this["messageTagName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when minimum length validation has failed.
    /// </summary>
    /// <value>The min length violation message.</value>
    [ConfigurationProperty("minLengthViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MinLengthViolationMessageDescription", Title = "MinLengthViolationMessageCaption")]
    public string MinLengthViolationMessage
    {
      get => (string) this["minLengthViolationMessage"];
      set => this["minLengthViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when minimum value validation has failed.
    /// </summary>
    /// <value>The min value violation message.</value>
    [ConfigurationProperty("minValueViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MinValueViolationMessageDescription", Title = "MinValueViolationMessageCaption")]
    public string MinValueViolationMessage
    {
      get => (string) this["minValueViolationMessage"];
      set => this["minValueViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when non alphanumeric validation has failed.
    /// </summary>
    /// <value>The non alpha numeric violation message.</value>
    [ConfigurationProperty("nonAlphaNumericViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NonAlphaNumericViolationMessageDescription", Title = "NonAlphaNumericViolationMessageCaption")]
    public string NonAlphaNumericViolationMessage
    {
      get => (string) this["nonAlphaNumericViolationMessage"];
      set => this["nonAlphaNumericViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when numeric validation has failed.
    /// </summary>
    /// <value>The numeric violation message.</value>
    [ConfigurationProperty("numericViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NumericViolationMessageDescription", Title = "NumericViolationMessageCaption")]
    public string NumericViolationMessage
    {
      get => (string) this["numericViolationMessage"];
      set => this["numericViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when percentage validation has failed.
    /// </summary>
    /// <value>The percentage violation message.</value>
    [ConfigurationProperty("percentageViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PercentageViolationMessageDescription", Title = "PercentageViolationMessageCaption")]
    public string PercentageViolationMessage
    {
      get => (string) this["percentageViolationMessage"];
      set => this["percentageViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when regular expression validation has failed.
    /// </summary>
    /// <value>The regular expression violation message.</value>
    [ConfigurationProperty("regularExpressionViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RegularExpressionViolationMessageDescription", Title = "RegularExpressionViolationMessageCaption")]
    public string RegularExpressionViolationMessage
    {
      get => (string) this["regularExpressionViolationMessage"];
      set => this["regularExpressionViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when required validation has failed.
    /// </summary>
    /// <value>The required violation message.</value>
    [ConfigurationProperty("requiredViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RequiredViolationMessageDescription", Title = "RequiredViolationMessageCaption")]
    public string RequiredViolationMessage
    {
      get => (string) this["requiredViolationMessage"];
      set => this["requiredViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when US social security number validation has failed.
    /// </summary>
    /// <value>The US social security number violation message.</value>
    [ConfigurationProperty("usSocialSecurityNumberViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "USSocialSecurityNumberViolationMessageDescription", Title = "USSocialSecurityNumberViolationMessageCaption")]
    public string USSocialSecurityNumberViolationMessage
    {
      get => (string) this["usSocialSecurityNumberViolationMessage"];
      set => this["usSocialSecurityNumberViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message shown when US zip code validation has failed.
    /// </summary>
    /// <value>The US zip code violation message.</value>
    [ConfigurationProperty("usZipCodeViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "USZipCodeViolationMessageDescription", Title = "USZipCodeViolationMessageCaption")]
    public string USZipCodeViolationMessage
    {
      get => (string) this["usZipCodeViolationMessage"];
      set => this["usZipCodeViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether to validate if the validated component is invisible
    /// </summary>
    /// <value>The validate if invisible.</value>
    [ConfigurationProperty("validateIfInvisible", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ValidateIfInvisibleDescription", Title = "ValidateIfInvisibleCaption")]
    public bool? ValidateIfInvisible
    {
      get => (bool?) this["validateIfInvisible"];
      set => this["validateIfInvisible"] = (object) value;
    }

    /// <summary>
    /// Gets or set validation requirements that are going to be used when comparing against other controls' values.
    /// </summary>
    /// <value>The comparing validator definition.</value>
    [ConfigurationProperty("comparingValidatorDefinitions")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ComparingValidatorDefinitionsDescription", Title = "ComparingValidatorDefinitionsCaption")]
    public ConfigElementList<ComparingValidatorElement> ComparingValidatorDefinitionsConfig
    {
      get => (ConfigElementList<ComparingValidatorElement>) this["comparingValidatorDefinitions"];
      set => this["comparingValidatorDefinitions"] = (object) value;
    }

    public Collection<IComparingValidatorDefinition> ComparingValidatorDefinitions
    {
      get
      {
        if (this.comparingDefinitions == null)
          this.comparingDefinitions = new Collection<IComparingValidatorDefinition>((IList<IComparingValidatorDefinition>) this.ComparingValidatorDefinitionsConfig.Elements.Select<ComparingValidatorElement, IComparingValidatorDefinition>((Func<ComparingValidatorElement, IComparingValidatorDefinition>) (v => (IComparingValidatorDefinition) v.GetDefinition())).ToList<IComparingValidatorDefinition>());
        return this.comparingDefinitions;
      }
      set => this.comparingDefinitions = value;
    }

    /// <summary>
    /// Gets or sets the resource class used to localize messages text.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If this property is set; message properties will be treated as
    /// keys of localized entry in the localization class; otherwise they will be displayed
    /// as is.
    /// </remarks>
    [ConfigurationProperty("resourceClassId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public DefinitionBase GetDefinition() => (DefinitionBase) new ValidatorDefinition((ConfigElement) this);

    /// <summary>Gets the definition.</summary>
    /// <typeparam name="TDefinition">The type of the definition.</typeparam>
    /// <returns></returns>
    public TDefinition GetDefinition<TDefinition>() where TDefinition : DefinitionBase, new() => (TDefinition) this.GetDefinition();

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string ExpectedFormat = "expectedFormat";
      public const string MaxLength = "maxLength";
      public const string MinLength = "minLength";
      public const string MaxValue = "maxValue";
      public const string MinValue = "minValue";
      public const string RegularExpression = "regularExpression";
      public const string RegularExpressionSeparator = "regularExpressionSeparator";
      public const string Required = "required";
      public const string AlphaNumericViolationMessage = "alphaNumericViolationMessage";
      public const string CurrencyViolationMessage = "currencyViolationMessage";
      public const string EmailAddressViolationMessage = "emailAddressViolationMessage";
      public const string IntegerViolationMessage = "integerViolationMessage";
      public const string InternetUrlViolationMessage = "interneturlviolationmessage";
      public const string MaxLengthViolationMessage = "maxLengthViolationMessage";
      public const string MaxValueViolationMessage = "maxValueViolationMessage";
      public const string MessageCssClass = "messageCssClass";
      public const string MessageTagName = "messageTagName";
      public const string MinLengthViolationMessage = "minLengthViolationMessage";
      public const string MinValueViolationMessage = "minValueViolationMessage";
      public const string NonAlphaNumericViolationMessage = "nonAlphaNumericViolationMessage";
      public const string NumericViolationMessage = "numericViolationMessage";
      public const string PercentageViolationMessage = "percentageViolationMessage";
      public const string RegularExpressionViolationMessage = "regularExpressionViolationMessage";
      public const string RequiredViolationMessage = "requiredViolationMessage";
      public const string USSocialSecurityNumberViolationMessage = "usSocialSecurityNumberViolationMessage";
      public const string USZipCodeViolationMessage = "usZipCodeViolationMessage";
      public const string ValidateIfInvisible = "validateIfInvisible";
      public const string ComparingValidatorDefinitions = "comparingValidatorDefinitions";
      public const string ResourceClassId = "resourceClassId";
      public const string RecommendedCharactersCount = "recommendedCharactersCount";
      public const string ShowCharacterCounter = "showCharacterCounter";
    }
  }
}
