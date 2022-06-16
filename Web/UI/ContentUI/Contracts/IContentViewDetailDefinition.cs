// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewDetailDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Contracts
{
  /// <summary>Base interface for content view detail</summary>
  public interface IContentViewDetailDefinition : IContentViewDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the name of the tag in which the view should be wrapped.
    /// </summary>
    /// <remarks>Default value is UL.</remarks>
    string WrapperTagName { get; set; }

    /// <summary>
    /// Gets or sets additional data that may be used by some controls.
    /// </summary>
    string AdditionalControlData { get; set; }

    /// <summary>
    /// Gets or sets the CSS class that should be applied to the wrapper tag.
    /// </summary>
    string WrapperCssClass { get; set; }

    /// <summary>Defines the name of the CSS class for all fields.</summary>
    string FieldCssClass { get; set; }

    /// <summary>Defines the name of the CSS class for all sections.</summary>
    string SectionCssClass { get; set; }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewSectionDefinition" /> sections of the view.
    /// </summary>
    IEnumerable<IContentViewSectionDefinition> Sections { get; }

    /// <summary>Gets or sets whether to show sections.</summary>
    bool? ShowSections { get; set; }

    /// <summary>
    /// Gets or sets the ID of the page that should display the master view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId.</value>
    Guid MasterPageId { get; set; }

    /// <summary>
    /// Gets or sets the data ID of the content item that should be displayed.
    /// </summary>
    /// <value>The data item pageId.</value>
    Guid DataItemId { get; set; }
  }
}
