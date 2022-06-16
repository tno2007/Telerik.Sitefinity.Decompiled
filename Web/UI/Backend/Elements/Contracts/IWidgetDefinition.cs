// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Defines the mandated members that need to be implemented by every type that
  /// represents a definition for the controls that implements <see cref="!:IWidget" /> interface.
  /// </summary>
  public interface IWidgetDefinition : IDefinition
  {
    /// <summary>Gets or sets the name of the widget.</summary>
    /// <remarks>
    /// This name has to be unique inside of a collection of widgets.
    /// </remarks>
    string Name { get; set; }

    /// <summary>Gets or sets the text of the command button.</summary>
    string Text { get; set; }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    string CssClass { get; set; }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The CSS class.</value>
    string ResourceClassId { get; set; }

    /// <summary>Gets or sets the virtual path.</summary>
    /// <value>The item virtual path.</value>
    string WidgetVirtualPath { get; set; }

    /// <summary>Gets or sets the wrapper tag pageId.</summary>
    /// <value>The wrapper tag pageId.</value>
    string WrapperTagId { get; set; }

    /// <summary>Gets or sets the name of the wrapper tag.</summary>
    /// <value>The name of the wrapper tag.</value>
    HtmlTextWriterTag WrapperTagKey { get; set; }

    /// <summary>Gets or sets the type of the widget.</summary>
    /// <value>The type of the widget.</value>
    [TypeConverter(typeof (StringTypeConverter))]
    Type WidgetType { get; set; }

    /// <summary>
    /// Gets or sets the indication if it is a widget separator.
    /// </summary>
    /// <value>The container pageId.</value>
    bool IsSeparator { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether widget is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    bool? Visible { get; set; }
  }
}
