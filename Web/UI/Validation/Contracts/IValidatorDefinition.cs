// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.Contracts.IValidatorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.ObjectModel;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Enums;

namespace Telerik.Sitefinity.Web.UI.Validation.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the Validator definition
  /// </summary>
  public interface IValidatorDefinition : IDefinition, IValidatorDefinitionBase
  {
    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    string ControlDefinitionName { get; set; }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    string ViewName { get; set; }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value>The name of the section.</value>
    string SectionName { get; set; }

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    string FieldName { get; set; }

    /// <summary>
    /// Gets or sets an expected <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat" /> format of the
    /// validated value.
    /// </summary>
    /// <value>The expected format.</value>
    ValidationFormat ExpectedFormat { get; set; }

    /// <summary>Gets or sets the maximum length.</summary>
    int MaxLength { get; set; }

    /// <summary>Gets or sets the minimum length.</summary>
    int MinLength { get; set; }

    /// <summary>
    /// Gets or sets the maximum value to use when validating data.
    /// </summary>
    /// <value>The max value.</value>
    object MaxValue { get; set; }

    /// <summary>
    /// Gets or sets the minimum value to use when validating data.
    /// </summary>
    object MinValue { get; set; }

    /// <summary>Gets or sets the recommended characters count.</summary>
    int? RecommendedCharactersCount { get; set; }

    /// <summary>
    /// Gets or sets the regular expression to use when evaluating string.
    /// </summary>
    string RegularExpression { get; set; }

    /// <summary>
    /// Gets or sets the separator to use when evaluating string with custom regular expression.
    /// </summary>
    string RegularExpressionSeparator { get; set; }

    /// <summary>
    /// Gets or sets whether this field control's data must have a value.
    /// </summary>
    /// <value><c>true</c> if value is required; otherwise, <c>false</c>.</value>
    bool? Required { get; set; }

    /// <summary>Gets or sets the violation message CSS class.</summary>
    /// <value>The message CSS class.</value>
    string MessageCssClass { get; set; }

    /// <summary>Gets or sets the name of the violation message tag.</summary>
    /// <value>The name of the message tag.</value>
    string MessageTagName { get; set; }

    /// <summary>
    /// Gets or sets whether to validate if the validated component is invisible
    /// </summary>
    /// <value>The validate if invisible.</value>
    bool? ValidateIfInvisible { get; set; }

    /// <summary>
    /// Gets or set validation requirements that are going to be used when comparing against other controls' values.
    /// </summary>
    /// <value>The comparing validator definition.</value>
    Collection<IComparingValidatorDefinition> ComparingValidatorDefinitions { get; set; }

    /// <summary>
    /// Gets or sets the resource class used to localize message text and title.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If this property is set; text properties will be treated as
    /// keys of localized entry in the localization class; otherwise they will be displayed
    /// as is.
    /// </remarks>
    string ResourceClassId { get; set; }
  }
}
