// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.ThreadCreateRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>ThreadCreateRequest</c> Represents the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object that should be created.
  /// </summary>
  public class ThreadCreateRequest
  {
    /// <summary>
    /// Gets or sets the key of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object.
    /// </summary>
    /// <value>The thread key.</value>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the type of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object.
    /// </summary>
    /// <value>The type of the thread.</value>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the behavior of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object.
    /// </summary>
    /// <value>The behavior of the thread</value>
    public string Behavior { get; set; }

    /// <summary>
    /// Gets or sets the title of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object.
    /// </summary>
    /// <value>The thread title.</value>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the language of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object.
    /// </summary>
    /// <value>The language.</value>
    public string Language { get; set; }

    /// <summary>
    /// Gets or sets the data source of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object.
    /// </summary>
    /// <remarks>
    /// The default implementation uses the the provider name of the item associated with the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object.
    /// </remarks>
    /// <example>The provider name of a news item.</example>
    /// <value>The data source.</value>
    public string DataSource { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IGroup" /> object key.
    /// </summary>
    /// <value>The group key.</value>
    public string GroupKey { get; set; }

    /// <summary>
    /// Gets or sets the group information when the parent group should be created for this <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" />.
    /// </summary>
    /// <value>The group.</value>
    public GroupCreateRequest Group { get; set; }
  }
}
