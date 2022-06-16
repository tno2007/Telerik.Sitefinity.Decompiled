// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.CommandWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents a configuration element for command widget definition
  /// </summary>
  public class CommandWidgetElement : 
    WidgetElement,
    ICommandWidgetDefinitionSecured,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.CommandWidgetElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public CommandWidgetElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new CommandWidgetDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("commandName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandNameDescription", Title = "CommandNameCaption")]
    public string CommandName
    {
      get => (string) this["commandName"];
      set => this["commandName"] = (object) value;
    }

    /// <summary>Gets or sets the required permission set.</summary>
    /// <value></value>
    [ConfigurationProperty("requiredPermissionSet", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RequiredPermissionSetDescription", Title = "RequiredPermissionSetCaption")]
    public string RequiredPermissionSet
    {
      get => (string) this["requiredPermissionSet"];
      set => this["requiredPermissionSet"] = (object) value;
    }

    /// <summary>Gets or sets the required actions.</summary>
    /// <value></value>
    [ConfigurationProperty("requiredActions", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RequiredActionsDescription", Title = "RequiredActionsCaption")]
    public string RequiredActions
    {
      get => (string) this["requiredActions"];
      set => this["requiredActions"] = (object) value;
    }

    /// <summary>Gets or sets the command argument</summary>
    /// <value></value>
    [ConfigurationProperty("commandArgument", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandArgumentDescription", Title = "CommandArgumentCaption")]
    public string CommandArgument
    {
      get => (string) this["commandArgument"];
      set => this["commandArgument"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the type of the commmand button that ought to represent the command widget
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("commandButtonType", DefaultValue = CommandButtonType.Standard)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandButtonTypeDescription", Title = "CommandButtonTypeCaption")]
    public CommandButtonType ButtonType
    {
      get => (CommandButtonType) this["commandButtonType"];
      set => this["commandButtonType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the Navigate Url for the command button to redirect.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("navigateUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandNavigateUrlDescription", Title = "CommandNavigateUrlCaption")]
    public string NavigateUrl
    {
      get => (string) this["navigateUrl"];
      set => this["navigateUrl"] = (object) value;
    }

    /// <summary>
    /// Determines this command is a filter command (e.g. a filter on the sidebar)
    /// </summary>
    [ConfigurationProperty("isFilter", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandIsFilterCommandDescription", Title = "CommandIsFilterCommandCaption")]
    public bool IsFilterCommand
    {
      get => (bool) this["isFilter"];
      set => this["isFilter"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the permission set related to the security action which represents this widget's command.
    /// </summary>
    [ConfigurationProperty("permissionSet")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandPermissionSetDescription", Title = "CommandPermissionSetCaption")]
    public string PermissionSet
    {
      get => (string) this["permissionSet"];
      set => this["permissionSet"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the action which represents this widget's command.
    /// </summary>
    [ConfigurationProperty("actionName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandActionNameDescription", Title = "CommandActionNameCaption")]
    public string ActionName
    {
      get => (string) this["actionName"];
      set => this["actionName"] = (object) value;
    }

    /// <summary>The type of the secured object related to the widget</summary>
    [ConfigurationProperty("relatedSecuredObjectTypeName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RelatedSecuredObjectTypeNameDescription", Title = "RelatedSecuredObjectTypeNameCaption")]
    public string RelatedSecuredObjectTypeName
    {
      get => (string) this["relatedSecuredObjectTypeName"];
      set => this["relatedSecuredObjectTypeName"] = (object) value;
    }

    /// <summary>The Id of the secured object related to the widget</summary>
    [ConfigurationProperty("relatedSecuredObjectId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RelatedSecuredObjectIdDescription", Title = "RelatedSecuredObjectIdCaption")]
    public string RelatedSecuredObjectId
    {
      get => (string) this["relatedSecuredObjectId"];
      set => this["relatedSecuredObjectId"] = (object) value;
    }

    /// <summary>
    /// (Optional) The provider of of the secured object related to the widget
    /// If no provider is given, the default one is used.
    /// </summary>
    [ConfigurationProperty("relatedSecuredObjectProviderName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RelatedSecuredObjectProviderNameDescription", Title = "RelatedSecuredObjectProviderNameCaption")]
    public string RelatedSecuredObjectProviderName
    {
      get => (string) this["relatedSecuredObjectProviderName"];
      set => this["relatedSecuredObjectProviderName"] = (object) value;
    }

    /// <summary>
    /// (Optional) The type name of the manager of the secured object related to the widget.
    /// If no manager is given, the configured mapped manager is used.
    /// </summary>
    [ConfigurationProperty("relatedSecuredObjectManagerTypeName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RelatedSecuredObjectManagerTypeNameDescription", Title = "RelatedSecuredObjectManagerTypeNameCaption")]
    public string RelatedSecuredObjectManagerTypeName
    {
      get => (string) this["relatedSecuredObjectManagerTypeName"];
      set => this["relatedSecuredObjectManagerTypeName"] = (object) value;
    }

    /// <summary>Gets or sets the CSS class for this widget's button.</summary>
    [ConfigurationProperty("buttonCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandButtonCssClassDescription", Title = "CommandButtonCssClassCaption")]
    public string ButtonCssClass
    {
      get => (string) this["buttonCssClass"];
      set => this["buttonCssClass"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the text displayed when the mouse pointer hovers over the
    /// Web server control.
    /// </summary>
    /// <value></value>
    /// <returns>The text displayed when the mouse pointer hovers over the Web server
    /// control. The default is <see cref="F:System.String.Empty" />.</returns>
    [ConfigurationProperty("toolTip")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolTipDescription", Title = "ToolTipCaption")]
    public string ToolTip
    {
      get => (string) this["toolTip"];
      set => this["toolTip"] = (object) value;
    }

    /// <summary>
    /// (Optional) The provider of of the secured object related to the widget
    /// If no provider is given, the default one is used.
    /// </summary>
    [ConfigurationProperty("objectProviderName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ObjectProviderNameDescription", Title = "ObjectProviderNameCaption")]
    public string ObjectProviderName
    {
      get => (string) this["objectProviderName"];
      set => this["objectProviderName"] = (object) value;
    }

    /// <summary>
    /// Gets or set whether to redirect in same window when NavigateUrl is specified.
    /// </summary>
    [ConfigurationProperty("openInSameWindow")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OpenInSameWindowDescription", Title = "OpenInSameWindowCaption")]
    public bool OpenInSameWindow
    {
      get => (bool) this["openInSameWindow"];
      set => this["openInSameWindow"] = (object) value;
    }

    /// <summary>Gets or sets the condition for the item to be shown.</summary>
    /// <value>The condition.</value>
    [ConfigurationProperty("condition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ConditionDescription", Title = "ConditionCaption")]
    public string Condition
    {
      get => (string) this["condition"];
      set => this["condition"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct CommandWidgetProps
    {
      public const string commandName = "commandName";
      public const string commandArgument = "commandArgument";
      public const string commandButtonType = "commandButtonType";
      public const string navigateUrl = "navigateUrl";
      public const string permissionSet = "permissionSet";
      public const string actionName = "actionName";
      public const string buttonCssClass = "buttonCssClass";
      public const string isFilterCommand = "isFilter";
      public const string toolTip = "toolTip";
      public const string relatedSecuredObjectTypeName = "relatedSecuredObjectTypeName";
      public const string relatedSecuredObjectId = "relatedSecuredObjectId";
      public const string relatedSecuredObjectProviderName = "relatedSecuredObjectProviderName";
      public const string relatedSecuredObjectManagerTypeName = "relatedSecuredObjectManagerTypeName";
      public const string objectProviderName = "objectProviderName";
      public const string openInSameWindow = "openInSameWindow";
      public const string condition = "condition";
      public const string requiredActions = "requiredActions";
      public const string requiredPermissionSet = "requiredPermissionSet";
    }
  }
}
