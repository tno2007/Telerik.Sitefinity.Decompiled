// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ToolboxItemBaseDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents a definition of the Sitefinity dialog</summary>
  [ParseChildren(true)]
  [DebuggerDisplay("ToolboxItemBaseDefinition")]
  public class ToolboxItemBaseDefinition : DefinitionBase, IToolboxItemBaseDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private string containerId;
    private string cssClass;
    private string itemTemplatePath;
    private bool visible;
    private string wrapperTagCssClass;
    private string wrapperTagId;
    private string wrapperTagName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.PromptDialogDefinition" /> class.
    /// </summary>
    public ToolboxItemBaseDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.PromptDialogDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ToolboxItemBaseDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ToolboxItemBaseDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    /// <summary>
    /// Gets or sets the name of the view. Used for resolving property values.
    /// </summary>
    /// <value>The name of the view.</value>
    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    public string ContainerId
    {
      get => this.ResolveProperty<string>(nameof (ContainerId), this.containerId);
      set => this.containerId = value;
    }

    public string CssClass
    {
      get => this.ResolveProperty<string>(nameof (CssClass), this.cssClass);
      set => this.cssClass = value;
    }

    public string ItemTemplatePath
    {
      get => this.ResolveProperty<string>(nameof (ItemTemplatePath), this.itemTemplatePath);
      set => this.itemTemplatePath = value;
    }

    public bool Visible
    {
      get => this.ResolveProperty<bool>(nameof (Visible), this.visible);
      set => this.visible = value;
    }

    public string WrapperTagCssClass
    {
      get => this.ResolveProperty<string>(nameof (WrapperTagCssClass), this.wrapperTagCssClass);
      set => this.wrapperTagCssClass = value;
    }

    public string WrapperTagId
    {
      get => this.ResolveProperty<string>(nameof (WrapperTagId), this.wrapperTagId);
      set => this.wrapperTagId = value;
    }

    public string WrapperTagName
    {
      get => this.ResolveProperty<string>(nameof (WrapperTagName), this.wrapperTagName);
      set => this.wrapperTagName = value;
    }
  }
}
