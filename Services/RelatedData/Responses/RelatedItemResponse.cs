// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Responses.RelatedItemResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Services.RelatedData.Responses
{
  /// <summary>Represents the response returned for related item.</summary>
  public class RelatedItemResponse
  {
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>Gets or sets the status.</summary>
    /// <value>The status.</value>
    public ContentLifecycleStatus Status { get; set; }

    /// <summary>
    /// Indicates if item is related to previously provided item.
    /// </summary>
    public bool IsRelated { get; set; }

    /// <summary>
    /// Indicates if the current user has permissions to edit the item.
    /// </summary>
    public bool IsEditable { get; set; }

    /// <summary>Gets or sets the ordinal.</summary>
    /// <value>The ordinal.</value>
    public float Ordinal { get; set; }

    /// <summary>Gets or sets related item's lifecycle status.</summary>
    /// <value>The lifecycle status.</value>
    public LifecycleResponse LifecycleStatus { get; set; }

    /// <summary>Gets or sets the content type name of the item.</summary>
    /// <value>The content type name.</value>
    public string ContentTypeName { get; set; }

    /// <summary>Gets or sets the details view URL.</summary>
    public string DetailsViewUrl { get; set; }

    /// <summary>
    /// Gets or sets the list of all listed languages. Used to determine if item has translation for a language.
    /// </summary>
    /// <value>The available languages.</value>
    public string[] AvailableLanguages { get; set; }
  }
}
