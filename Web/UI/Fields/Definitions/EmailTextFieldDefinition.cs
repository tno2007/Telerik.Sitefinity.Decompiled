// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.EmailTextFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of a
  /// <see cref="T:Telerik.Sitefinity.Web.UI.Fields.EmailTextField" />
  /// </summary>
  public class EmailTextFieldDefinition : 
    FieldControlDefinition,
    IEmailTextFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private bool isLocalizable;
    private bool trimSpaces;
    private bool allowNulls;
    private bool tooltipVisible;
    private string tooltipText;
    private string tooltipTitle;
    private string tooltipContent;
    private string tooltipCssClass;
    private string tooltipTargetCssClass;
    private bool serverSideOnly;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.EmailTextFieldDefinition" /> class.
    /// </summary>
    public EmailTextFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.EmailTextFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public EmailTextFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
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

    /// <summary>Gets or sets the text of the tooltip target.</summary>
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

    /// <inheritdoc />
    public bool ServerSideOnly
    {
      get => this.ResolveProperty<bool>(nameof (ServerSideOnly), this.serverSideOnly);
      set => this.serverSideOnly = value;
    }
  }
}
