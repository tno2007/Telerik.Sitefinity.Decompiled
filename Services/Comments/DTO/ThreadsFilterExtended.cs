// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.ThreadsFilterExtended
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>ThreadsFilterExtended</c> Represents the extended filter based on which the threads collection will be filtered.
  /// </summary>
  public class ThreadsFilterExtended : IReturn<CollectionResponse<ThreadResponse>>, IReturn
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.ThreadsFilterExtended" /> class.
    /// </summary>
    public ThreadsFilterExtended() => this.ThreadKey = new List<string>();

    /// <summary>
    /// Gets or set list of thread keys to filter the comments.
    /// </summary>
    /// <value>List of thread keys.</value>
    public List<string> ThreadKey { get; set; }
  }
}
