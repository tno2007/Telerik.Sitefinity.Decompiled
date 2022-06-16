// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.TextFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of a
  /// <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TextField" />
  /// </summary>
  public class TextFieldDefinition : 
    FieldControlDefinition,
    ITextFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private IExpandableControlDefinition expandableDefinition;
    private int rows;
    private string hideIfValue;
    private bool? isPasswordMode;
    private string unit;
    private bool isLocalizable;
    private bool showCharacterCounter;
    private int recommendedCharactersCount;
    private string readOnlyReplacement;
    private int maxCharactersCount;
    private string characterCounterDescription;
    private bool trimSpaces;
    private bool allowNulls;
    private bool tooltipVisible;
    private string tooltipText;
    private string tooltipTitle;
    private string tooltipContent;
    private string tooltipCssClass;
    private string tooltipTargetCssClass;
    private string autocompleteServiceUrl;
    private int autocompleteSuggestionsCount;
    private bool serverSideOnly;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.TextFieldDefinition" /> class.
    /// </summary>
    public TextFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.TextFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public TextFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets or sets the number of rows displayed in a multiline text box.
    /// </summary>
    /// <value>The rows.</value>
    public int Rows
    {
      get => this.ResolveProperty<int>(nameof (Rows), this.rows);
      set => this.rows = value;
    }

    /// <summary>
    /// Gets or sets the value which compared with the actual value of the Text Field, if equal hides the text.
    /// </summary>
    /// <value>The hide if value.</value>
    public string HideIfValue
    {
      get => this.ResolveProperty<string>(nameof (HideIfValue), this.hideIfValue);
      set => this.hideIfValue = value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition
    {
      get
      {
        if (this.expandableDefinition == null)
        {
          this.expandableDefinition = (IExpandableControlDefinition) new ExpandableControlDefinition();
          this.expandableDefinition.ControlDefinitionName = this.ControlDefinitionName;
          this.expandableDefinition.ViewName = this.ViewName;
          this.expandableDefinition.SectionName = this.SectionName;
          this.expandableDefinition.FieldName = this.FieldName;
        }
        return this.expandableDefinition;
      }
      set => this.expandableDefinition = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if the text field is used as password field.
    /// </summary>
    public bool? IsPasswordMode
    {
      get => this.ResolveProperty<bool?>(nameof (IsPasswordMode), this.isPasswordMode);
      set => this.isPasswordMode = value;
    }

    /// <summary>
    /// Gets or sets the unit of the value. If set, will be displayed after the value.
    /// </summary>
    public string Unit
    {
      get => this.ResolveProperty<string>(nameof (Unit), this.unit);
      set => this.unit = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    public bool IsLocalizable
    {
      get => this.ResolveProperty<bool>(nameof (IsLocalizable), this.isLocalizable);
      set => this.isLocalizable = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show character cunter.
    /// </summary>
    public bool ShowCharacterCounter
    {
      get => this.ResolveProperty<bool>(nameof (ShowCharacterCounter), this.showCharacterCounter);
      set => this.showCharacterCounter = value;
    }

    /// <summary>Gets or sets the recomended characters count.</summary>
    /// <value>The recomended characters count.</value>
    public int RecommendedCharactersCount
    {
      get => this.ResolveProperty<int>(nameof (RecommendedCharactersCount), this.recommendedCharactersCount);
      set => this.recommendedCharactersCount = value;
    }

    /// <summary>Gets or sets the max characters count.</summary>
    /// <value>The max characters count.</value>
    public int MaxCharactersCount
    {
      get => this.ResolveProperty<int>(nameof (MaxCharactersCount), this.maxCharactersCount);
      set => this.maxCharactersCount = value;
    }

    /// <summary>Gets or sets the character counter description.</summary>
    /// <value>The character counter description.</value>
    public string CharacterCounterDescription
    {
      get => this.ResolveProperty<string>(nameof (CharacterCounterDescription), this.characterCounterDescription);
      set => this.characterCounterDescription = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to trim spaces.
    /// </summary>
    public bool TrimSpaces
    {
      get => this.ResolveProperty<bool>(nameof (TrimSpaces), this.trimSpaces);
      set => this.trimSpaces = value;
    }

    /// <inheritdoc />
    public bool AllowNulls
    {
      get => this.ResolveProperty<bool>(nameof (AllowNulls), this.allowNulls);
      set => this.allowNulls = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show a tooltip.
    /// </summary>
    public bool ToolTipVisible
    {
      get => this.ResolveProperty<bool>(nameof (ToolTipVisible), this.tooltipVisible);
      set => this.tooltipVisible = value;
    }

    /// <summary>Get or sets the text of the tooltip target.</summary>
    public string ToolTipText
    {
      get => this.ResolveProperty<string>(nameof (ToolTipText), this.tooltipText);
      set => this.tooltipText = value;
    }

    /// <summary>Gets or sets the title of the tooltip.</summary>
    public string ToolTipTitle
    {
      get => this.ResolveProperty<string>(nameof (ToolTipTitle), this.tooltipTitle);
      set => this.tooltipTitle = value;
    }

    /// <summary>Gets or sets the content of the tooltip.</summary>
    public string ToolTipContent
    {
      get => this.ResolveProperty<string>(nameof (ToolTipContent), this.tooltipContent);
      set => this.tooltipContent = value;
    }

    /// <summary>Gets or sets the css class of the tooltip.</summary>
    public string ToolTipCssClass
    {
      get => this.ResolveProperty<string>(nameof (ToolTipCssClass), this.tooltipCssClass);
      set => this.tooltipCssClass = value;
    }

    /// <summary>Gets or sets the css class of the tooltip target.</summary>
    public string ToolTipTargetCssClass
    {
      get => this.ResolveProperty<string>(nameof (ToolTipTargetCssClass), this.tooltipTargetCssClass);
      set => this.tooltipTargetCssClass = value;
    }

    /// <summary>Gets or sets the read only replacement.</summary>
    public string ReadOnlyReplacement
    {
      get => this.ResolveProperty<string>(nameof (ReadOnlyReplacement), this.readOnlyReplacement);
      set => this.readOnlyReplacement = value;
    }

    /// <summary>Gets or sets the autocomplete service url.</summary>
    public string AutocompleteServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (AutocompleteServiceUrl), this.autocompleteServiceUrl);
      set => this.autocompleteServiceUrl = value;
    }

    /// <summary>Gets or sets the autocomplete suggestions count.</summary>
    public int AutocompleteSuggestionsCount
    {
      get => this.ResolveProperty<int>(nameof (AutocompleteSuggestionsCount), this.autocompleteSuggestionsCount);
      set => this.autocompleteSuggestionsCount = value;
    }

    /// <inheritdoc />
    public bool ServerSideOnly
    {
      get => this.ResolveProperty<bool>(nameof (ServerSideOnly), this.serverSideOnly);
      set => this.serverSideOnly = value;
    }
  }
}
