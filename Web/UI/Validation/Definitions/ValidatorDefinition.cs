// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;
using Telerik.Sitefinity.Web.UI.Validation.Enums;

namespace Telerik.Sitefinity.Web.UI.Validation.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of a
  /// <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Validator" />
  /// </summary>
  [DataContract]
  [FieldDefinitionElement(typeof (ValidatorDefinitionElement))]
  [ParseChildren(false, DefaultProperty = "ComparingValidatorDefinitions")]
  [KnownType(typeof (ComparingValidatorDefinition))]
  [KnownType(typeof (ComparingValidatorElement))]
  public class ValidatorDefinition : 
    DefinitionBase,
    IValidatorDefinition,
    IDefinition,
    IValidatorDefinitionBase
  {
    private string controlDefinitionName;
    private string viewName;
    private string sectionName;
    private string fieldName;
    private ValidationFormat expectedFormat;
    private int maxLength;
    private int minLength;
    private object minValue;
    private object maxValue;
    private string regularExpression;
    private int? recommendedCharactersCount;
    private bool? required;
    private string alphaNumericViolationMessage;
    private string currencyViolationMessage;
    private string emailAddressViolationMessage;
    private string integerViolationMessage;
    private string internetUrlViolationMessage;
    private string maxLengthViolationMessage;
    private string maxValueViolationMessage;
    private string messageCssClass;
    private string messageTagName;
    private string minLengthViolationMessage;
    private string minValueViolationMessage;
    private string nonAlphaNumericViolationMessage;
    private string numericViolationMessage;
    private string percentageViolationMessage;
    private string regularExpressionViolationMessage;
    private string requiredViolationMessage;
    private string usSocialSecurityNumberViolationMessage;
    private string usZipCodeViolationMessage;
    private bool? validateIfInvisible;
    private Collection<IComparingValidatorDefinition> comparingValidatorDefinitions;
    private string regularExpressionSeparator;
    private string resourceClassId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition" /> class.
    /// </summary>
    public ValidatorDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ValidatorDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value>The name of the section.</value>
    public string SectionName
    {
      get => this.sectionName;
      set => this.sectionName = value;
    }

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    public string FieldName
    {
      get => this.fieldName;
      set => this.fieldName = value;
    }

    /// <summary>Gets the default violation messages.</summary>
    /// <value>The default violation messages.</value>
    public virtual IValidationErrorMessages DefaultViolationMessages => (IValidationErrorMessages) Res.Get<ErrorMessages>();

    /// <summary>
    /// Gets or sets an expected <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat" /> format of the
    /// validated value.
    /// </summary>
    /// <value>The expected format.</value>
    [DataMember]
    public ValidationFormat ExpectedFormat
    {
      get => this.ResolveProperty<ValidationFormat>(nameof (ExpectedFormat), this.expectedFormat);
      set => this.expectedFormat = value;
    }

    /// <summary>Gets or sets the maximum length.</summary>
    /// <value></value>
    [DataMember]
    public int MaxLength
    {
      get => this.ResolveProperty<int>(nameof (MaxLength), this.maxLength);
      set => this.maxLength = value;
    }

    /// <summary>Gets or sets the minimum length.</summary>
    /// <value></value>
    [DataMember]
    public int MinLength
    {
      get => this.ResolveProperty<int>(nameof (MinLength), this.minLength);
      set => this.minLength = value;
    }

    /// <summary>
    /// Gets or sets the minimum value to use when validating data.
    /// </summary>
    /// <value></value>
    [DataMember]
    public object MinValue
    {
      get => this.ResolveProperty<object>(nameof (MinValue), this.minValue);
      set => this.minValue = value;
    }

    /// <summary>
    /// Gets or sets the maximum value to use when validating data.
    /// </summary>
    /// <value>The max value.</value>
    [DataMember]
    public object MaxValue
    {
      get => this.ResolveProperty<object>(nameof (MaxValue), this.maxValue);
      set => this.maxValue = value;
    }

    /// <summary>Gets or sets the recommended characters count.</summary>
    [DataMember]
    public int? RecommendedCharactersCount
    {
      get => this.ResolveProperty<int?>(nameof (RecommendedCharactersCount), this.recommendedCharactersCount);
      set => this.recommendedCharactersCount = value;
    }

    /// <summary>
    /// Gets or sets the regular expression to use when evaluating string.
    /// </summary>
    /// <value></value>
    [DataMember]
    public string RegularExpression
    {
      get => this.ResolveProperty<string>(nameof (RegularExpression), this.regularExpression);
      set => this.regularExpression = value;
    }

    /// <summary>
    /// Gets or sets the separator to use when evaluating string with custom regular expression.
    /// </summary>
    /// <value></value>
    [DataMember]
    public string RegularExpressionSeparator
    {
      get => this.ResolveProperty<string>(nameof (RegularExpressionSeparator), this.regularExpressionSeparator);
      set => this.regularExpressionSeparator = value;
    }

    /// <summary>
    /// Gets or sets whether this field control's data must have a value.
    /// </summary>
    /// <value><c>true</c> if value is required; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool? Required
    {
      get => this.ResolveProperty<bool?>(nameof (Required), this.required);
      set => this.required = value;
    }

    /// <summary>Gets or sets the violation message CSS class.</summary>
    /// <value>The message CSS class.</value>
    [DataMember]
    public string MessageCssClass
    {
      get => this.ResolveProperty<string>(nameof (MessageCssClass), this.messageCssClass);
      set => this.messageCssClass = value;
    }

    /// <summary>Gets or sets the name of the violation message tag.</summary>
    /// <value>The name of the message tag.</value>
    [DataMember]
    public string MessageTagName
    {
      get
      {
        string messageTagName = this.ResolveProperty<string>(nameof (MessageTagName), this.messageTagName);
        if (string.IsNullOrEmpty(messageTagName))
          messageTagName = "div";
        return messageTagName;
      }
      set => this.messageTagName = value;
    }

    /// <summary>
    /// Gets or sets the message shown when alpha numeric validation has failed.
    /// </summary>
    /// <value>The alpha numeric violation message.</value>
    [DataMember]
    public string AlphaNumericViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (AlphaNumericViolationMessage), this.alphaNumericViolationMessage, this.DefaultViolationMessages.AlphaNumericViolationMessage);
      set => this.alphaNumericViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when currency validation has failed.
    /// </summary>
    /// <value>The currency violation message.</value>
    [DataMember]
    public string CurrencyViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (CurrencyViolationMessage), this.currencyViolationMessage, this.DefaultViolationMessages.CurrencyViolationMessage);
      set => this.currencyViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when email address validation has failed.
    /// </summary>
    /// <value>The email address violation message.</value>
    [DataMember]
    public string EmailAddressViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (EmailAddressViolationMessage), this.emailAddressViolationMessage, this.DefaultViolationMessages.EmailAddressViolationMessage);
      set => this.emailAddressViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when integer validation has failed.
    /// </summary>
    /// <value>The integer violation message.</value>
    [DataMember]
    public string IntegerViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (IntegerViolationMessage), this.integerViolationMessage, this.DefaultViolationMessages.IntegerViolationMessage);
      set => this.integerViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when internet url validation has failed.
    /// </summary>
    /// <value>The internet URL violation message.</value>
    [DataMember]
    public string InternetUrlViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (InternetUrlViolationMessage), this.internetUrlViolationMessage, this.DefaultViolationMessages.InternetUrlViolationMessage);
      set => this.internetUrlViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when max length validation has failed.
    /// </summary>
    /// <value>The max length violation message.</value>
    [DataMember]
    public string MaxLengthViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (MaxLengthViolationMessage), this.maxLengthViolationMessage, this.DefaultViolationMessages.MaxLengthViolationMessage);
      set => this.maxLengthViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when max value validation has failed.
    /// </summary>
    /// <value>The max value violation message.</value>
    [DataMember]
    public string MaxValueViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (MaxValueViolationMessage), this.maxValueViolationMessage, this.DefaultViolationMessages.MaxValueViolationMessage);
      set => this.maxValueViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when minimum length validation has failed.
    /// </summary>
    /// <value>The min length violation message.</value>
    [DataMember]
    public string MinLengthViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (MinLengthViolationMessage), this.minLengthViolationMessage, this.DefaultViolationMessages.MinLengthViolationMessage);
      set => this.minLengthViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when minimum value validation has failed.
    /// </summary>
    /// <value>The min value violation message.</value>
    [DataMember]
    public string MinValueViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (MinValueViolationMessage), this.minValueViolationMessage, this.DefaultViolationMessages.MinValueViolationMessage);
      set => this.minValueViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when non alphanumeric validation has failed.
    /// </summary>
    /// <value>The non alpha numeric violation message.</value>
    [DataMember]
    public string NonAlphaNumericViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (NonAlphaNumericViolationMessage), this.nonAlphaNumericViolationMessage, this.DefaultViolationMessages.NonAlphaNumericViolationMessage);
      set => this.nonAlphaNumericViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when numeric validation has failed.
    /// </summary>
    /// <value>The numeric violation message.</value>
    [DataMember]
    public string NumericViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (NumericViolationMessage), this.numericViolationMessage, this.DefaultViolationMessages.NumericViolationMessage);
      set => this.numericViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when percentage validation has failed.
    /// </summary>
    /// <value>The percentage violation message.</value>
    [DataMember]
    public string PercentageViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (PercentageViolationMessage), this.percentageViolationMessage, this.DefaultViolationMessages.PercentageViolationMessage);
      set => this.percentageViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when regular expression validation has failed.
    /// </summary>
    /// <value>The regular expression violation message.</value>
    [DataMember]
    public string RegularExpressionViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (RegularExpressionViolationMessage), this.regularExpressionViolationMessage, this.DefaultViolationMessages.RegularExpressionViolationMessage);
      set => this.regularExpressionViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when required validation has failed.
    /// </summary>
    /// <value>The required violation message.</value>
    [DataMember]
    public string RequiredViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (RequiredViolationMessage), this.requiredViolationMessage, this.DefaultViolationMessages.RequiredViolationMessage);
      set => this.requiredViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when US social security number validation has failed.
    /// </summary>
    /// <value>The US social security number violation message.</value>
    [DataMember]
    public string USSocialSecurityNumberViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (USSocialSecurityNumberViolationMessage), this.usSocialSecurityNumberViolationMessage, this.DefaultViolationMessages.USSocialSecurityNumberViolationMessage);
      set => this.usSocialSecurityNumberViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message shown when US zip code validation has failed.
    /// </summary>
    /// <value>The US zip code violation message.</value>
    [DataMember]
    public string USZipCodeViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (USZipCodeViolationMessage), this.usZipCodeViolationMessage, this.DefaultViolationMessages.USZipCodeViolationMessage);
      set => this.usZipCodeViolationMessage = value;
    }

    /// <summary>
    /// Gets or sets whether to validate if the validated component is invisible
    /// </summary>
    /// <value>The validate if invisible.</value>
    [DataMember]
    public bool? ValidateIfInvisible
    {
      get => this.ResolveProperty<bool?>(nameof (ValidateIfInvisible), this.validateIfInvisible, new bool?(true));
      set => this.validateIfInvisible = value;
    }

    /// <summary>
    /// Gets or set validation requirements that are going to be used when comparing against other controls' values.
    /// </summary>
    /// <value>The comparing validator definition.</value>
    [DataMember]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<IComparingValidatorDefinition> ComparingValidatorDefinitions
    {
      get
      {
        this.comparingValidatorDefinitions = this.ResolveProperty<Collection<IComparingValidatorDefinition>>(nameof (ComparingValidatorDefinitions), this.comparingValidatorDefinitions);
        if (this.comparingValidatorDefinitions == null)
          this.comparingValidatorDefinitions = new Collection<IComparingValidatorDefinition>();
        return this.comparingValidatorDefinitions;
      }
      set => this.comparingValidatorDefinitions = value;
    }

    /// <summary>
    /// Gets or sets the resource class used to localize message text and title.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If this property is set; text properties will be treated as
    /// keys of localized entry in the localization class; otherwise they will be displayed
    /// as is.
    /// </remarks>
    [DataMember]
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId, string.Empty);
      set => this.resourceClassId = value;
    }

    protected override ConfigElement GetConfigurationDefinition() => string.IsNullOrEmpty(this.controlDefinitionName) ? (ConfigElement) null : (ConfigElement) ((FieldControlDefinitionElement) ((ContentViewDetailElement) Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls[this.controlDefinitionName].ViewsConfig[this.viewName]).Sections[this.sectionName].Fields[this.fieldName]).ValidatorConfig;
  }
}
