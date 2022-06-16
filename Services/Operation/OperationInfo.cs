// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.OperationInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// This class represents a DTO for the current operation.
  /// </summary>
  internal class OperationInfo
  {
    /// <summary>Gets or sets the operation identifier.</summary>
    /// <value>The operation identifier.</value>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the type of the operation.</summary>
    /// <value>The type of the operation.</value>
    public string Type { get; set; }

    /// <summary>Gets or sets the origin of the operation.</summary>
    /// <value>The origin of the operation.</value>
    public string Origin { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the operation and
    /// operation related items should be processed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the operation and operation related items should be processed;
    ///   otherwise, <c>false</c>.
    /// </value>
    public bool ShouldProcess { get; set; }

    /// <summary>Gets or sets the parent operation.</summary>
    /// <value>The parent operation.</value>
    public OperationInfo ParentOperation { get; set; }
  }
}
