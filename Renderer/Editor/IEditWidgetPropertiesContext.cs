// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.DefaultEditPropertiesWidgetContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Renderer.Editor
{
  /// <summary>
  /// Container for group of properties
  /// Intended to use as set context for editing properties.
  /// </summary>
  public class DefaultEditPropertiesWidgetContext : 
    DefaultEditorContext,
    IEditWidgetPropertiesContext,
    IEditorContext
  {
    /// <summary>Gets or sets the id of the component.</summary>
    /// <value>The component id.</value>
    [DataMember]
    public PropertyValueGroupContainer PropertyGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to clean the persisted properties.
    /// </summary>
    public bool CleanPersistedProperties { get; set; }
  }
}
