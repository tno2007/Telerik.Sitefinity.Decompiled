// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.DefaultAddWidgetContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Renderer.Editor
{
  /// <summary>
  /// Class containing information for the currently added widget.
  /// </summary>
  public class DefaultAddWidgetContext : DefaultEditorContext, IAddWidgetContext, IEditorContext
  {
    /// <summary>Gets or sets the id of the widget.</summary>
    [DataMember]
    public string Id { get; set; }

    /// <summary>Gets or sets the name of the widget.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the name of the widget.</summary>
    [DataMember]
    public string SiblingKey { get; set; }

    /// <summary>
    /// Gets or sets the parent placeholder key, which is the parent(layout) of the currently added widget.
    /// </summary>
    [DataMember]
    public string ParentPlaceholderKey { get; set; }

    /// <summary>Gets or sets the placeholder name.</summary>
    [DataMember]
    public string PlaceholderName { get; set; }

    /// <summary>Gets or sets the properties of the widget.</summary>
    [DataMember]
    public IEnumerable<PropertyValueContainer> Properties { get; set; }
  }
}
