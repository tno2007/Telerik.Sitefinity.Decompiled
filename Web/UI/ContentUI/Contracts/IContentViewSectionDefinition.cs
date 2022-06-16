// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewSectionDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Contracts
{
  /// <summary>Base interface for content view section</summary>
  public interface IContentViewSectionDefinition : IDefinition
  {
    string ControlDefinitionName { get; set; }

    string ViewName { get; set; }

    /// <summary>Gets or sets the section's CSS class.</summary>
    /// <value>The CSS class.</value>
    string CssClass { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewSectionDefinition.DisplayMode" />.
    /// </summary>
    /// <value>The display mode.</value>
    FieldDisplayMode? DisplayMode { get; set; }

    /// <summary>
    /// Defines a collection of field definitions that belong to this section.
    /// </summary>
    /// <value>The fields.</value>
    IEnumerable<IFieldDefinition> Fields { get; }

    /// <summary>Gets or sets the name of the section.</summary>
    string Name { get; set; }

    /// <summary>Gets or sets the ordinal position of the section.</summary>
    /// <value>The ordinal.</value>
    int? Ordinal { get; set; }

    /// <summary>
    /// Gets or sets the global resource class ID to use for localized strings.
    /// </summary>
    string ResourceClassId { get; set; }

    /// <summary>Gets or sets the title of the section.</summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <value>The control id.</value>
    string ControlId { get; set; }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    HtmlTextWriterTag WrapperTag { get; set; }

    /// <summary>
    /// Gets or sets whether the ContentViewSection should be hidden in read-only mode
    /// when a translation is being added/edited.
    /// </summary>
    bool IsHiddenInTranslationMode { get; set; }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    IExpandableControlDefinition ExpandableDefinition { get; }
  }
}
