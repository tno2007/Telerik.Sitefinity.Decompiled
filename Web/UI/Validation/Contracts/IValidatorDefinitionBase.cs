// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.Contracts.IValidatorDefinitionBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Validation.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the Validator definition
  /// </summary>
  public interface IValidatorDefinitionBase
  {
    /// <summary>
    /// Gets or sets the message shown when alpha numeric validation has failed.
    /// </summary>
    /// <value>The alpha numeric violation message.</value>
    string AlphaNumericViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when currency validation has failed.
    /// </summary>
    /// <value>The currency violation message.</value>
    string CurrencyViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when email address validation has failed.
    /// </summary>
    /// <value>The email address violation message.</value>
    string EmailAddressViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when integer validation has failed.
    /// </summary>
    /// <value>The integer violation message.</value>
    string IntegerViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when internet url validation has failed.
    /// </summary>
    /// <value>The internet URL violation message.</value>
    string InternetUrlViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when max length validation has failed.
    /// </summary>
    /// <value>The max length violation message.</value>
    string MaxLengthViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when max value validation has failed.
    /// </summary>
    /// <value>The max value violation message.</value>
    string MaxValueViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when minimum length validation has failed.
    /// </summary>
    /// <value>The min length violation message.</value>
    string MinLengthViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when minimum value validation has failed.
    /// </summary>
    /// <value>The min value violation message.</value>
    string MinValueViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when non alphanumeric validation has failed.
    /// </summary>
    /// <value>The non alpha numeric violation message.</value>
    string NonAlphaNumericViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when numeric validation has failed.
    /// </summary>
    /// <value>The numeric violation message.</value>
    string NumericViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when percentage validation has failed.
    /// </summary>
    /// <value>The percentage violation message.</value>
    string PercentageViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when regular expression validation has failed.
    /// </summary>
    /// <value>The regular expression violation message.</value>
    string RegularExpressionViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when required validation has failed.
    /// </summary>
    /// <value>The required violation message.</value>
    string RequiredViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when US social security number validation has failed.
    /// </summary>
    /// <value>The US social security number violation message.</value>
    string USSocialSecurityNumberViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when US zip code validation has failed.
    /// </summary>
    /// <value>The US zip code violation message.</value>
    string USZipCodeViolationMessage { get; set; }
  }
}
