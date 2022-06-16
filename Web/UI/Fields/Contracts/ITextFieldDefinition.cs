// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.ITextFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of text field element.
  /// </summary>
  public interface ITextFieldDefinition : IFieldControlDefinition, IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the number of rows displayed in a multiline text box.
    ///  </summary>
    /// <value>The rows.</value>
    int Rows { get; set; }

    /// <summary>
    /// Gets or sets the value which compared with the actual value of the Text Field, if equal hides the text.
    /// </summary>
    /// <value>The hide if value.</value>
    string HideIfValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the text field is used as password field.
    /// </summary>
    bool? IsPasswordMode { get; set; }

    /// <summary>
    /// Gets or sets the unit of the value. If set, will be displayed after the value.
    /// </summary>
    string Unit { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the text field is nullable.
    /// Empty text field will return null value instead of empty string.
    /// </summary>
    bool AllowNulls { get; set; }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    IExpandableControlDefinition ExpandableDefinition { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    bool IsLocalizable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show character counter label.
    /// </summary>
    /// <value>
    /// <c>true</c> if [show character counter]; otherwise, <c>false</c>.
    /// </value>
    bool ShowCharacterCounter { set; get; }

    /// <summary>Gets or sets the recomended characters count.</summary>
    /// <value>The recomended characters count.</value>
    int RecommendedCharactersCount { set; get; }

    /// <summary>Gets or sets the read only replacement.</summary>
    string ReadOnlyReplacement { set; get; }

    /// <summary>Gets or sets the max characters count.</summary>
    /// <value>The max characters count.</value>
    int MaxCharactersCount { get; set; }

    /// <summary>Gets or sets the character counter description.</summary>
    /// <value>The character counter description.</value>
    string CharacterCounterDescription { set; get; }

    /// <summary>
    /// Gets or sets a value indicating whether to trim spaces.
    /// </summary>
    bool TrimSpaces { set; get; }

    /// <summary>
    /// Gets or sets a value indicating whether to show a tooltip.
    /// </summary>
    bool ToolTipVisible { get; set; }

    /// <summary>Get or sets the text of the tooltip target.</summary>
    string ToolTipText { get; set; }

    /// <summary>Gets or sets the title of the tooltip.</summary>
    string ToolTipTitle { get; set; }

    /// <summary>Gets or sets the content of the tooltip.</summary>
    string ToolTipContent { get; set; }

    /// <summary>Gets or sets the css class of the tooltip.</summary>
    string ToolTipCssClass { get; set; }

    /// <summary>Gets or sets the css class of the tooltip target.</summary>
    string ToolTipTargetCssClass { get; set; }

    /// <summary>Gets or sets the autocomplete service url.</summary>
    string AutocompleteServiceUrl { get; set; }

    /// <summary>Gets or sets the autocomplete suggestions count.</summary>
    int AutocompleteSuggestionsCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the client side component should be initialized in Read mode.
    /// </summary>
    bool ServerSideOnly { get; set; }
  }
}
