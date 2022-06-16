// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetBarSectionDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Specifies the common members for definitions of widget bar sections.
  /// </summary>
  public interface IWidgetBarSectionDefinition : IDefinition
  {
    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    string Title { get; set; }

    /// <summary>Gets or sets the title wrapper tag name.</summary>
    /// <value>The title wrapper tag name.</value>
    HtmlTextWriterTag TitleWrapperTagKey { get; set; }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The CSS class.</value>
    string ResourceClassId { get; set; }

    /// <summary>Gets or sets the wrapper tag pageId of the section</summary>
    /// <value>The wrapper tag pageId.</value>
    string WrapperTagId { get; set; }

    /// <summary>
    /// Gets or sets the name of the wrapper tag of the section
    /// </summary>
    /// <value>The name of the wrapper tag.</value>
    HtmlTextWriterTag WrapperTagKey { get; set; }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    string CssClass { get; set; }

    /// <summary>Gets the collection of widget definitions.</summary>
    IEnumerable<IWidgetDefinition> Items { get; }

    /// <summary>
    /// Gets or sets if the section will be initially visible.
    /// </summary>
    /// <value>The visible.</value>
    bool? Visible { get; set; }

    /// <summary>
    /// Gets or sets the unique name of the widget bar section.
    /// </summary>
    /// <value>The title.</value>
    string Name { get; set; }
  }
}
