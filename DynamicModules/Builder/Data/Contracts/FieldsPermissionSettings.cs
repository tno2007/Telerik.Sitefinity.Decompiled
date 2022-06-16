// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Data.Contracts.FieldsPermissionSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.DynamicModules.Builder.Data.Contracts
{
  [DataContract]
  public class FieldsPermissionSettings
  {
    /// <summary>Gets or sets the name of the field.</summary>
    public string FieldName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this field is editable.
    /// </summary>
    public bool IsEditable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this field is visible.
    /// </summary>
    public bool IsVisible { get; set; }
  }
}
