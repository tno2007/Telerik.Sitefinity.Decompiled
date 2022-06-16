// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.ThreadUpdateRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>ThreadUpdateRequest</c> Used to provide the new thread values for the update.
  /// </summary>
  public class ThreadUpdateRequest
  {
    /// <summary>Gets or sets the thread unique key.</summary>
    /// <value>The unique key of the thread.</value>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this thread is closed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
    /// </value>
    public bool IsClosed { get; set; }
  }
}
