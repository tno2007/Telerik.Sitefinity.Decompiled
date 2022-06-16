// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CommentsCountGetRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CommentsCountGetRequest</c>Represents a filter for retrieving the count of <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects.
  /// </summary>
  /// <remarks>
  /// Currently allows filtering based on collection of thread keys.
  /// </remarks>
  public class CommentsCountGetRequest
  {
    public CommentsCountGetRequest()
    {
      this.ThreadKey = new List<string>();
      this.Status = new List<string>();
    }

    /// <summary>
    /// Gets or sets the keys of <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> objects.
    /// </summary>
    public List<string> ThreadKey { get; set; }

    /// <summary>
    /// Gets or sets the status for which comments will be counted.
    /// </summary>
    /// <value>The status.</value>
    public List<string> Status { set; get; }
  }
}
