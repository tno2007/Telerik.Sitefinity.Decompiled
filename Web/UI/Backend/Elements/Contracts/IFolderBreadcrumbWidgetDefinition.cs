// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IFolderBreadcrumbWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Defines the contract implemented by the configuration elements and definition classes for FoldersBreadcrumb widget.
  /// </summary>
  public interface IFolderBreadcrumbWidgetDefinition : IWidgetDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the type of the manager that is going to be used to get folders.
    /// </summary>
    Type ManagerType { get; set; }

    /// <summary>Gets or sets the navigation page id.</summary>
    Guid NavigationPageId { get; set; }

    /// <summary>Gets or sets the root page id.</summary>
    Guid RootPageId { get; set; }

    /// <summary>Gets or sets the title for the root link.</summary>
    string RootTitle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to append the root item URL.
    /// </summary>
    bool AppendRootUrl { get; set; }
  }
}
