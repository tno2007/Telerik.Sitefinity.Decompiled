// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicListViewModeDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions
{
  /// <summary>The definition class for dynamic list item template.</summary>
  public class DynamicListViewModeDefinition : 
    LisViewModeDefinition,
    IDynamicListViewModeDefinition,
    IListViewModeDefinition,
    IViewModeDefinition,
    IDefinition,
    IActionMenuDefinition
  {
    private bool isClientTemplateDynamic;
    private string virtualPath;
    private string resourceFileName;
    private string assemblyName;
    private Type assemblyInfo;
    private ICommandWidgetDefinition mainAction;
    private List<IWidgetDefinition> menuItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicListViewModeDefinition" /> class.
    /// </summary>
    public DynamicListViewModeDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicListViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DynamicListViewModeDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public DynamicListViewModeDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="T:Telerik.Sitefinity.Web.UI.ClientTemplate" /> is dynamic.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the <see cref="T:Telerik.Sitefinity.Web.UI.ClientTemplate" /> is dynamic; otherwise, <c>false</c>.
    /// </value>
    public bool IsClientTemplateDynamic
    {
      get => this.ResolveProperty<bool>(nameof (IsClientTemplateDynamic), this.isClientTemplateDynamic);
      set => this.isClientTemplateDynamic = value;
    }

    /// <summary>
    /// Gets or sets the virtual path of the template.
    /// This is one of the ways for defining the template.
    /// Other ways are setting the <see cref="T:Telerik.Sitefinity.Web.UI.ClientTemplate" /> to dynamic markup and setting the <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicListViewModeDefinition.IsClientTemplateDynamic" /> to <c>true</c>;
    /// or specifying <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicListViewModeDefinition.ResourceFileName" /> and <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicListViewModeDefinition.AssemblyInfo" /> or <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicListViewModeDefinition.AssemblyName" />.
    /// </summary>
    public string VirtualPath
    {
      get => this.ResolveProperty<string>(nameof (VirtualPath), this.virtualPath);
      set => this.virtualPath = value;
    }

    /// <summary>Gets or sets the resource name of the template.</summary>
    public string ResourceFileName
    {
      get => this.ResolveProperty<string>(nameof (ResourceFileName), this.resourceFileName);
      set => this.resourceFileName = value;
    }

    /// <summary>
    /// Gets or sets the name of the assembly with the resource containing the template.
    /// </summary>
    public string AssemblyName
    {
      get => this.ResolveProperty<string>(nameof (AssemblyName), this.assemblyName);
      set => this.assemblyName = value;
    }

    /// <summary>
    /// Gets or sets a type from the assembly containing the resource file.
    /// </summary>
    public Type AssemblyInfo
    {
      get => this.ResolveProperty<Type>(nameof (AssemblyInfo), this.assemblyInfo);
      set => this.assemblyInfo = value;
    }

    /// <summary>Gets or sets the main action.</summary>
    /// <value>The main action.</value>
    public ICommandWidgetDefinition MainAction
    {
      get => this.ResolveProperty<ICommandWidgetDefinition>(nameof (MainAction), this.mainAction);
      set => this.mainAction = value;
    }

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetDefinition" /> objects.
    /// </summary>
    /// <value>The menu items.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IWidgetDefinition> MenuItems
    {
      get
      {
        if (this.menuItems == null)
          this.menuItems = new List<IWidgetDefinition>();
        return this.ResolveProperty<List<IWidgetDefinition>>(nameof (MenuItems), this.menuItems);
      }
    }

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetDefinition" /> objects.
    /// </summary>
    /// <value>The menu items.</value>
    IEnumerable<IWidgetDefinition> IActionMenuDefinition.MenuItems => (IEnumerable<IWidgetDefinition>) this.MenuItems;
  }
}
