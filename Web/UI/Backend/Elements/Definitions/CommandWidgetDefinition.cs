// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.CommandWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents a definition of the command widget</summary>
  public class CommandWidgetDefinition : 
    WidgetDefinition,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition,
    ISecuredCommandWidget
  {
    private string commandName;
    private string commandArgument;
    private CommandButtonType buttonType;
    private string navigateUrl;
    private string permissionSet;
    private string actionName;
    private string buttonCssClass;
    private bool isFilterCommand;
    private string toolTip;
    private string relatedSecuredObjectTypeName;
    private string relatedSecuredObjectId;
    private string relatedSecuredObjectProviderName;
    private string relatedSecuredObjectManagerTypeName;
    private bool openInSameWindow;
    private string condition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.CommandWidgetDefinition" /> class.
    /// </summary>
    public CommandWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.CommandWidgetDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public CommandWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
      if (!(configDefinition is CommandWidgetElement))
        return;
      this.permissionSet = ((CommandWidgetElement) configDefinition).PermissionSet;
      this.actionName = ((CommandWidgetElement) configDefinition).ActionName;
      this.relatedSecuredObjectId = ((CommandWidgetElement) configDefinition).RelatedSecuredObjectId;
      this.relatedSecuredObjectTypeName = ((CommandWidgetElement) configDefinition).RelatedSecuredObjectTypeName;
      this.relatedSecuredObjectProviderName = ((CommandWidgetElement) configDefinition).RelatedSecuredObjectProviderName;
      this.relatedSecuredObjectManagerTypeName = ((CommandWidgetElement) configDefinition).RelatedSecuredObjectManagerTypeName;
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    public string CommandName
    {
      get => this.ResolveProperty<string>(nameof (CommandName), this.commandName);
      set => this.commandName = value;
    }

    /// <summary>Gets or sets the command argument</summary>
    /// <value></value>
    public string CommandArgument
    {
      get => this.ResolveProperty<string>(nameof (CommandArgument), this.commandArgument);
      set => this.commandArgument = value;
    }

    /// <summary>
    /// Gets or sets the type of the commmand button that ought to represent the command widget
    /// </summary>
    public CommandButtonType ButtonType
    {
      get => this.ResolveProperty<CommandButtonType>(nameof (ButtonType), this.buttonType);
      set => this.buttonType = value;
    }

    /// <summary>
    /// Gets or sets the CSS class of the command button that ought to represent the command widget
    /// </summary>
    /// <value></value>
    public string ButtonCssClass
    {
      get => this.ResolveProperty<string>(nameof (ButtonCssClass), this.buttonCssClass);
      set => this.buttonCssClass = value;
    }

    /// <summary>
    /// Gets or sets the Navigate Url for the command button to redirect.
    /// </summary>
    /// <value></value>
    public string NavigateUrl
    {
      get => this.ResolveProperty<string>(nameof (NavigateUrl), this.navigateUrl);
      set => this.navigateUrl = value;
    }

    /// <summary>
    /// Determines this command is a filter command (e.g. a filter on the sidebar)
    /// </summary>
    public bool IsFilterCommand
    {
      get => this.ResolveProperty<bool>(nameof (IsFilterCommand), this.isFilterCommand);
      set => this.isFilterCommand = value;
    }

    /// <summary>
    /// Gets or sets the text displayed when the mouse pointer hovers over the
    /// Web server control.
    /// </summary>
    /// <value></value>
    /// <returns>The text displayed when the mouse pointer hovers over the Web server
    /// control. The default is <see cref="F:System.String.Empty" />.</returns>
    public string ToolTip
    {
      get => this.ResolveProperty<string>(nameof (ToolTip), this.toolTip);
      set => this.toolTip = value;
    }

    /// <summary>Gets or sets the provider name</summary>
    /// <value></value>
    /// <returns>The provider name</returns>
    public string ObjectProviderName
    {
      get => this.ResolveProperty<string>(nameof (ObjectProviderName), this.ObjectProviderName);
      set => this.ObjectProviderName = value;
    }

    /// <summary>
    /// Gets or set whether to redirect in same window when NavigateUrl is specified.
    /// </summary>
    public bool OpenInSameWindow
    {
      get => this.ResolveProperty<bool>(nameof (OpenInSameWindow), this.openInSameWindow);
      set => this.openInSameWindow = value;
    }

    /// <summary>Gets or sets the condition for the item to be shown.</summary>
    /// <value>The condition.</value>
    public string Condition
    {
      get => this.ResolveProperty<string>(nameof (Condition), this.condition);
      set => this.condition = value;
    }

    /// <summary>
    /// Gets or sets the permission set related to the security action which represents this widget's command.
    /// </summary>
    /// <value>
    /// The permission set related to the security action which represents this widget's command.
    /// </value>
    public string PermissionSet
    {
      get => this.ResolveProperty<string>(nameof (PermissionSet), this.permissionSet);
      set => this.permissionSet = value;
    }

    /// <summary>
    /// Gets or sets the name of the action which represents this widget's command.
    /// </summary>
    /// <value>The name of the action which represents this widget's command.</value>
    public string ActionName
    {
      get => this.ResolveProperty<string>(nameof (ActionName), this.actionName);
      set => this.actionName = value;
    }

    /// <summary>The type of the secured object related to the widget</summary>
    public string RelatedSecuredObjectTypeName
    {
      get => this.ResolveProperty<string>(nameof (RelatedSecuredObjectTypeName), this.relatedSecuredObjectTypeName);
      set => this.relatedSecuredObjectTypeName = value;
    }

    /// <summary>The Id of the secured object related to the widget</summary>
    public string RelatedSecuredObjectId
    {
      get => this.ResolveProperty<string>(nameof (RelatedSecuredObjectId), this.relatedSecuredObjectId);
      set => this.relatedSecuredObjectId = value;
    }

    /// <summary>
    /// The provider name of the secured object related to the widget
    /// </summary>
    public string RelatedSecuredObjectProviderName
    {
      get => this.ResolveProperty<string>(nameof (RelatedSecuredObjectProviderName), this.relatedSecuredObjectProviderName);
      set => this.relatedSecuredObjectProviderName = value;
    }

    /// <summary>
    /// (Optional) The type name of the manager of the secured object related to the widget.
    /// If no manager is given, the configured mapped manager is used.
    /// </summary>
    public string RelatedSecuredObjectManagerTypeName
    {
      get => this.ResolveProperty<string>(nameof (RelatedSecuredObjectManagerTypeName), this.relatedSecuredObjectManagerTypeName);
      set => this.relatedSecuredObjectManagerTypeName = value;
    }

    /// <summary>
    /// Determines whether this widget's command is allowed according to its permission set, action and secured object.
    /// </summary>
    /// <param name="relatedSecuredObject">The related secured object.</param>
    /// <returns>
    /// 	<c>true</c> if this widget's command is allowed according to the set permission set, action and secured object; otherwise, <c>false</c>
    /// </returns>
    public bool IsAllowed(ISecuredObject relatedSecuredObject)
    {
      bool flag = true;
      if (!string.IsNullOrEmpty(this.actionName) && !string.IsNullOrEmpty(this.permissionSet))
        flag = relatedSecuredObject.IsGranted(this.permissionSet, this.actionName);
      return flag;
    }
  }
}
