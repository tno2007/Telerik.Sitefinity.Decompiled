// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetBarDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Defines the mandated members that need to be implemented by every type that
  /// represents a definition for the controls that implements <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget" /> interface.
  /// </summary>
  public interface IWidgetBarDefinition : IDefinition
  {
    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    string Title { get; set; }

    /// <summary>Gets or sets the title wrapper tag name.</summary>
    /// <value>The title wrapper tag name.</value>
    string TitleWrapperTagName { get; set; }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The CSS class.</value>
    string ResourceClassId { get; set; }

    /// <summary>Gets the collection of widget section definitions.</summary>
    IEnumerable<IWidgetBarSectionDefinition> Sections { get; }

    /// <summary>Gets or sets the wrapper tag pageId.</summary>
    /// <value>The wrapper tag pageId.</value>
    string WrapperTagId { get; set; }

    /// <summary>Gets or sets the key of the wrapper tag.</summary>
    /// <value>The key of the wrapper tag.</value>
    HtmlTextWriterTag WrapperTagKey { get; set; }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    string CssClass { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance has sections.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has sections; otherwise, <c>false</c>.
    /// </value>
    bool HasSections { get; }
  }
}
