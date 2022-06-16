// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CommentsFilterExtended
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CommentsFilterExtended</c> Represents an extended composite filter based on which a <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.CommentResponse" /> collection will be returned.
  /// </summary>
  public class CommentsFilterExtended : IReturn<CollectionResponse<CommentResponse>>, IReturn
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.CommentsFilterExtended" /> class.
    /// </summary>
    public CommentsFilterExtended()
    {
      this.Language = new List<string>();
      this.AuthorKey = new List<string>();
      this.Status = new List<string>();
      this.ThreadType = new List<string>();
      this.Behavior = new List<string>();
      this.CommentKey = new List<string>();
      this.ThreadKey = new List<string>();
      this.GroupKey = new List<string>();
    }

    /// <summary>
    /// Gets or sets list of <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects keys filter.
    /// </summary>
    /// <value>List of comment keys.</value>
    public List<string> CommentKey { get; set; }

    /// <summary>
    /// Gets or sets list of <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> objects keys filter.
    /// Exposes a way for retrieving <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects associated with certain <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> objects.
    /// </summary>
    /// <value>List of thread keys.</value>
    public List<string> ThreadKey { get; set; }

    /// <summary>
    /// Gets or sets list of <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> objects languages filter.
    /// Exposes a way for retrieving <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects by the language of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> objects associated with.
    /// </summary>
    /// <value>List of language.</value>
    public List<string> Language { get; set; }

    /// <summary>
    /// Gets or set list of <see cref="T:Telerik.Sitefinity.Services.Comments.IAuthor" /> keys to filter the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects that are associated with.
    /// </summary>
    /// <value>List of comments author keys.</value>
    public List<string> AuthorKey { get; set; }

    /// <summary>
    /// Gets or set list of statuses to filter the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects.
    /// </summary>
    /// <value>List of statuses.</value>
    public List<string> Status { get; set; }

    /// <summary>
    /// Gets or sets list of <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> objects types filter.
    /// Exposes a way for retrieving <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects associated with certain <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> objects.
    /// </summary>
    /// <value>List of thread types.</value>
    public List<string> ThreadType { get; set; }

    /// <summary>
    /// Gets or sets list of behaviors filter.
    /// Exposes a way for retrieving <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects associated with certain <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> objects with certain behavior.
    /// </summary>
    /// <value>List of behaviors.</value>
    public List<string> Behavior { get; set; }

    /// <summary>
    /// Gets or sets a list of <see cref="T:Telerik.Sitefinity.Services.Comments.IGroup" /> objects keys to filter thread.
    /// Exposes a way for retrieving <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects in one or more groups.
    /// </summary>
    /// <remarks>An <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object is associated with an <see cref="T:Telerik.Sitefinity.Services.Comments.IGroup" /> object through its parent <see cref="T:Telerik.Sitefinity.Services.Comments.IThread"> object</see>/&gt;</remarks>
    /// <value>List of group keys.</value>
    public List<string> GroupKey { get; set; }

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
