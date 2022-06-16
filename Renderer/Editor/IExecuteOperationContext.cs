// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.DefaultExecuteOperationContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Renderer.Editor
{
  /// <summary>
  /// A class containing information for the currently executed operation.
  /// </summary>
  public class DefaultExecuteOperationContext : 
    DefaultOperationsContext,
    IExecuteOperationContext,
    IGetOperationsContext,
    IEditorContext
  {
    /// <summary>Gets or sets the name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the action name.</summary>
    [DataMember]
    public string ActionName { get; set; }

    /// <summary>Gets or sets the parameters.</summary>
    [DataMember]
    public IEnumerable<PropertyValueContainer> Parameters { get; set; }

    /// <summary>Gets or sets the property metadata.</summary>
    [DataMember]
    public IEnumerable<PropertyContainer> PropertyMetadata { get; set; }
  }
}
