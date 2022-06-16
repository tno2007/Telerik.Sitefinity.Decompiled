// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.GenericData.Responses.DataItemResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Services.RelatedData.Responses;

namespace Telerik.Sitefinity.Services.GenericData.Responses
{
  /// <summary>Represents the response returned for data item.</summary>
  public class DataItemResponse
  {
    /// <summary>Gets or sets the identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title.</summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the subtitle for the item (path to the item usually).
    /// </summary>
    public string SubTitle { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the status.</summary>
    public ContentLifecycleStatus Status { get; set; }

    /// <summary>Gets or sets related item's lifecycle status.</summary>
    public LifecycleResponse LifecycleStatus { get; set; }

    /// <summary>
    /// Indicates if item is related to previously provided item.
    /// </summary>
    public bool IsRelated { get; set; }

    /// <summary>
    /// Indicates if the current user has permissions to edit the item.
    /// </summary>
    public bool IsEditable { get; set; }

    /// <summary>Gets or sets the owner.</summary>
    public string Owner { get; set; }

    /// <summary>Gets or sets the last modified date.</summary>
    public DateTime LastModified { get; set; }

    /// <summary>Gets or sets the details view URL.</summary>
    public string DetailsViewUrl { get; set; }

    /// <summary>Gets or sets the preview URL.</summary>
    public string PreviewUrl { get; set; }

    /// <summary>
    /// Gets or sets the list of all listed languages. Used to determine if item has translation for a language.
    /// </summary>
    public string[] AvailableLanguages { get; set; }
  }
}
