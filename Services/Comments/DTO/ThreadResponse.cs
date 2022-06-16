// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.ThreadResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>ThreadResponse</c> Represents the thread structure which is returned to the client.
  /// </summary>
  public class ThreadResponse
  {
    /// <summary>Gets or sets the thread unique key.</summary>
    /// <value>The unique key of the thread.</value>
    public string Key { get; set; }

    /// <summary>Gets or sets the thread type.</summary>
    /// <value>Full type name of the commented item</value>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the comments count in the thread. This count shows only the comments with status Published.
    /// </summary>
    /// <value>The comments count in this thread.</value>
    public int CommentsCount { get; set; }

    /// <summary>Gets or sets the title of the thread.</summary>
    /// <value>The thread title.</value>
    public string Title { get; set; }

    /// <summary>Gets or sets the language of the thread.</summary>
    /// <value>The language based on CurrentUICulture.</value>
    public string Language { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this thread is closed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
    /// </value>
    public bool IsClosed { get; set; }

    /// <summary>Gets or sets the parent group key of the thread.</summary>
    /// <value>The thread's group key.</value>
    public string GroupKey { get; set; }

    /// <summary>
    /// Gets or sets the default URL of the item related to this thread.
    /// </summary>
    /// <value>The commented item URL.</value>
    public string ItemUrl { get; set; }

    /// <summary>
    /// Gets or sets the data source of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object.
    /// </summary>
    /// <remarks>
    /// The default implementation uses the the provider name of the item associated with the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object.
    /// </remarks>
    /// <example>The provider name of a news item.</example>
    /// <value>The data source.</value>
    public string DataSource { get; set; }

    /// <summary>Gets or sets the average rating for this thread</summary>
    public Decimal? AverageRating { get; set; }
  }
}
