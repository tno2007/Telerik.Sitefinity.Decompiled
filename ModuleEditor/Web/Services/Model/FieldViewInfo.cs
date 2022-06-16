// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.FieldViewInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>
  /// Data transfer object used to present information for a view to which a field belong.
  /// </summary>
  public class FieldViewInfo
  {
    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string FieldName { get; set; }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string ViewName { get; set; }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string SectionName { get; set; }

    /// <summary>Gets or sets the hidden.</summary>
    /// <value>The hidden.</value>
    public bool? Hidden { get; set; }

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName { get; set; }
  }
}
