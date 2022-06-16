// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ParentOperationInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// Contains information regarding the parent relationship of an operation with another operation.
  /// </summary>
  [DataContract]
  internal class ParentOperationInfo
  {
    /// <summary>Gets or sets the name of the operation.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this operation is required for the child operation.
    /// </summary>
    [DataMember]
    public bool Required { get; set; }
  }
}
