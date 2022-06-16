// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicListViewModeDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts
{
  /// <summary>
  /// An interface which provides all information needed to construct list view with dynamic item template.
  /// </summary>
  public interface IDynamicListViewModeDefinition : 
    IListViewModeDefinition,
    IViewModeDefinition,
    IDefinition,
    IActionMenuDefinition
  {
    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="T:Telerik.Sitefinity.Web.UI.ClientTemplate" /> is dynamic.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the <see cref="T:Telerik.Sitefinity.Web.UI.ClientTemplate" /> is dynamic; otherwise, <c>false</c>.
    /// </value>
    bool IsClientTemplateDynamic { get; set; }

    /// <summary>
    /// Gets or sets the virtual path of the template.
    /// This is one of the ways for defining the template.
    /// Other ways are setting the <see cref="T:Telerik.Sitefinity.Web.UI.ClientTemplate" /> to dynamic markup and setting the <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicListViewModeDefinition.IsClientTemplateDynamic" /> to <c>true</c>;
    /// or specifying <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicListViewModeDefinition.ResourceFileName" /> and <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicListViewModeDefinition.AssemblyInfo" /> or <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicListViewModeDefinition.AssemblyName" />.
    /// </summary>
    string VirtualPath { get; set; }

    /// <summary>Gets or sets the resource name of the template.</summary>
    string ResourceFileName { get; set; }

    /// <summary>
    /// Gets or sets the name of the assembly with the resource containing the template.
    /// </summary>
    string AssemblyName { get; set; }

    /// <summary>
    /// Gets or sets a type from the assembly containing the resource file.
    /// </summary>
    Type AssemblyInfo { get; set; }
  }
}
