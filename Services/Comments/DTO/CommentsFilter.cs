// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CommentsFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CommentsFilter</c> Represent a composite filter based on which a <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.CommentResponse" /> collection will be returned.
  /// </summary>
  public class CommentsFilter : IReturn<CollectionResponse<CommentResponse>>, IReturn
  {
    /// <summary>
    /// Gets or sets the key of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object that the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects are associated with.
    /// </summary>
    /// <value>List of thread keys.</value>
    public string ThreadKey { get; set; }

    /// <summary>Gets or sets the skip.</summary>
    /// <value>The skip.</value>
    public int Skip { get; set; }

    /// <summary>Gets or sets the take.</summary>
    /// <value>The take.</value>
    public int Take { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" />objects should be sorted in descending order.
    /// This sort direction is used to sort <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" />objects by date created field.
    /// </summary>
    /// <value>
    ///   <c>true</c> if sort in descending order; otherwise, <c>false</c>.
    /// </value>
    public bool SortDescending { get; set; }

    /// <summary>
    /// Gets or sets the date before <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" />objects are created.
    /// </summary>
    /// <value>From date.</value>
    public DateTime NewerThan { get; set; }

    /// <summary>
    /// Gets or sets the date after <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" />objects are created.
    /// </summary>
    /// <value>To date.</value>
    public DateTime OlderThan { get; set; }
  }
}
