// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.FolderBreadcrumbWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>
  /// Represents a definition for the FolderBreadcrumbWidget
  /// </summary>
  public class FolderBreadcrumbWidgetDefinition : 
    WidgetDefinition,
    IFolderBreadcrumbWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private Type managerType;
    private Guid navigationPageId;
    private Guid rootPageId;
    private string rootTitle;
    private bool appendRootUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.FolderBreadcrumbWidgetDefinition" /> class.
    /// </summary>
    public FolderBreadcrumbWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.FolderBreadcrumbWidgetDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public FolderBreadcrumbWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>
    /// Gets or sets the type of the manager that is going to be used to get folders.
    /// </summary>
    public Type ManagerType
    {
      get => this.ResolveProperty<Type>(nameof (ManagerType), this.managerType);
      set => this.managerType = value;
    }

    /// <summary>Gets or sets the navigation page id.</summary>
    public Guid NavigationPageId
    {
      get => this.ResolveProperty<Guid>(nameof (NavigationPageId), this.navigationPageId);
      set => this.navigationPageId = value;
    }

    /// <summary>Gets or sets the root page id.</summary>
    public Guid RootPageId
    {
      get => this.ResolveProperty<Guid>(nameof (RootPageId), this.rootPageId);
      set => this.rootPageId = value;
    }

    /// <summary>Gets or sets the title for the root link.</summary>
    public string RootTitle
    {
      get => this.ResolveProperty<string>(nameof (RootTitle), this.rootTitle);
      set => this.rootTitle = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to append the root item URL.
    /// </summary>
    public bool AppendRootUrl
    {
      get => this.ResolveProperty<bool>(nameof (AppendRootUrl), this.appendRootUrl);
      set => this.appendRootUrl = value;
    }
  }
}
