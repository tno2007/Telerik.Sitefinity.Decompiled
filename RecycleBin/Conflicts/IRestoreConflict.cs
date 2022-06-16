// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Conflicts.IRestoreConflict
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;

namespace Telerik.Sitefinity.RecycleBin.Conflicts
{
  /// <summary>
  /// Represents a common interface for objects containing the reason for invalid item restorations.
  /// </summary>
  public interface IRestoreConflict
  {
    /// <summary>
    /// Gets or sets a value indicating whether the restore operation can automatically fix the
    /// specified conflict.
    /// </summary>
    /// <value>
    /// <c>true</c> if the restore operation can automatically recover from the contained
    /// invalid restoration reason ; otherwise, <c>false</c>.
    /// </value>
    bool IsRecoverable { get; set; }

    /// <summary>Gets or sets the reason for the restoration conflict.</summary>
    /// <value>The reason for the invalid restoration.</value>
    string Reason { get; set; }

    /// <summary>
    /// Gets or sets the reason arguments for the invalid restoration.
    /// </summary>
    /// <value>The reason arguments for the invalid restoration.</value>
    IDictionary ReasonArgs { get; set; }
  }
}
