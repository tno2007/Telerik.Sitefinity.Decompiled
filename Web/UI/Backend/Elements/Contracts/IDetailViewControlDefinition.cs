// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IDetailViewControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  internal interface IDetailViewControlDefinition : IViewDefinition, IDefinition
  {
    /// <summary>Defines the name of the CSS class for all sections</summary>
    string SectionCssClass { get; set; }

    /// <summary>
    /// Gets the <see cref="!:ContentViewSectionDefinition" /> sections of the view.
    /// </summary>
    IEnumerable<IContentViewSectionDefinition> Sections { get; }

    /// <summary>Gets or sets whether to show sections.</summary>
    bool? ShowSections { get; set; }

    IWidgetBarDefinition Toolbar { get; }

    IWidgetBarDefinition Sidebar { get; }
  }
}
