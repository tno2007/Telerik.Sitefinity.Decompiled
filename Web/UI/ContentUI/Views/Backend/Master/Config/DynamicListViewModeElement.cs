// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DynamicListViewModeElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>
  /// The configuration element for DynamicListViewModeDefinition
  /// </summary>
  public class DynamicListViewModeElement : 
    ListViewModeElement,
    IDynamicListViewModeDefinition,
    IListViewModeDefinition,
    IViewModeDefinition,
    IDefinition,
    IActionMenuDefinition
  {
    private const string IsClientTemplateDynamicPropertyName = "isClientTemplateDynamic";
    private const string VirtualPathPropertyName = "virtualPath";
    private const string ResourceFileNamePropertyName = "resourceFileName";
    private const string AssemblyNamePropertyName = "assemblyName";
    private const string AssemblyInfoPropertyName = "assemblyInfo";

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DynamicListViewModeElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new DynamicListViewModeDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="T:Telerik.Sitefinity.Web.UI.ClientTemplate" /> is dynamic.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the <see cref="T:Telerik.Sitefinity.Web.UI.ClientTemplate" /> is dynamic; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("isClientTemplateDynamic", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsClientTemplateDynamicDescription", Title = "IsClientTemplateDynamicCaption")]
    public bool IsClientTemplateDynamic
    {
      get => (bool) this["isClientTemplateDynamic"];
      set => this["isClientTemplateDynamic"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the virtual path of the template.
    /// This is one of the ways for defining the template.
    /// Other ways are setting the <see cref="T:Telerik.Sitefinity.Web.UI.ClientTemplate" /> to dynamic markup and setting the <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DynamicListViewModeElement.IsClientTemplateDynamic" /> to <c>true</c>;
    /// or specifying <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DynamicListViewModeElement.ResourceFileName" /> and <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DynamicListViewModeElement.AssemblyInfo" /> or <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DynamicListViewModeElement.AssemblyName" />.
    /// </summary>
    [ConfigurationProperty("virtualPath")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "VirtualPathDescription", Title = "VirtualPathCaption")]
    public string VirtualPath
    {
      get => (string) this["virtualPath"];
      set => this["virtualPath"] = (object) value;
    }

    /// <summary>Gets or sets the resource name of the template.</summary>
    [ConfigurationProperty("resourceFileName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceFileNameDescription", Title = "ResourceFileNameCaption")]
    public string ResourceFileName
    {
      get => (string) this["resourceFileName"];
      set => this["resourceFileName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the assembly with the resource containing the template.
    /// </summary>
    [ConfigurationProperty("assemblyName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AssemblyNameDescription", Title = "AssemblyNameCaption")]
    public string AssemblyName
    {
      get => (string) this["assemblyName"];
      set => this["assemblyName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a type from the assembly containing the resource file.
    /// </summary>
    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("assemblyInfo")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AssemblyInfoDescription", Title = "AssemblyInfoCaption")]
    public Type AssemblyInfo
    {
      get => (Type) this["assemblyInfo"];
      set => this["assemblyInfo"] = (object) value;
    }

    /// <summary>
    /// Defines a dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> configuration elements.
    /// </summary>
    /// <value>The dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> configuration element.</value>
    [ConfigurationProperty("menuItems")]
    [ConfigurationCollection(typeof (WidgetElement), AddItemName = "menuItem")]
    public ConfigElementList<WidgetElement> MenuItems => (ConfigElementList<WidgetElement>) this["menuItems"];

    /// <summary>Gets or sets the main action configuration element.</summary>
    /// <value>The main action configuration element.</value>
    [ConfigurationProperty("mainAction")]
    public CommandWidgetElement MainAction
    {
      get => (CommandWidgetElement) this["mainAction"];
      set => this["mainAction"] = (object) value;
    }

    ICommandWidgetDefinition IActionMenuDefinition.MainAction
    {
      get => (ICommandWidgetDefinition) this.MainAction;
      set => this.MainAction = (CommandWidgetElement) value;
    }

    IEnumerable<IWidgetDefinition> IActionMenuDefinition.MenuItems => this.MenuItems.Cast<IWidgetDefinition>();
  }
}
