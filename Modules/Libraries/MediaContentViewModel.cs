// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.MediaContentViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>Represents common view model for all media content</summary>
  [DataContract]
  [KnownType(typeof (ImageViewModel))]
  [KnownType(typeof (AlbumItemViewModel))]
  [KnownType(typeof (DocumentViewModel))]
  [KnownType(typeof (DocumentLibraryItemViewModel))]
  [KnownType(typeof (VideoViewModel))]
  [KnownType(typeof (VideoLibraryItemViewModel))]
  public abstract class MediaContentViewModel : 
    HierarchicalContentViewModelBase,
    IDynamicFieldsContainer
  {
    private List<string> tmbNames;
    private string blobStorageProvider;
    private string libraryUrl;
    private string libraryTitle;
    private string extension;
    private long totalSize;
    private Guid parentId;
    private string mediaUrl;
    private string embedUrl;
    private float ordinal;
    private string description;
    private DateTime? lastModified;
    private string folderTitle;
    private Guid originalContentId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.MediaContentViewModel" /> class.
    /// </summary>
    public MediaContentViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.MediaContentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public MediaContentViewModel(MediaContent contentItem, ContentDataProviderBase provider)
      : base((Content) contentItem, provider)
    {
      this.OriginalContentId = contentItem.OriginalContentId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.MediaContentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live content item related to the master content item.</param>
    /// <param name="tempItem">The temp content item related to the master content item.</param>
    public MediaContentViewModel(
      MediaContent contentItem,
      ContentDataProviderBase provider,
      MediaContent live,
      MediaContent temp)
      : base((Content) contentItem, provider, (Content) live, (Content) temp)
    {
      this.OriginalContentId = contentItem.OriginalContentId;
    }

    /// <summary>Gets or sets the thumbnail names.</summary>
    /// <value>The thumbnail names.</value>
    [DataMember]
    public List<string> ThumbnailNames
    {
      get
      {
        if (this.tmbNames == null && this.ContentItem != null)
        {
          this.tmbNames = new List<string>();
          foreach (Thumbnail thumbnail in ((MediaContent) this.ContentItem).GetThumbnails())
          {
            if (thumbnail.Name != "0")
              this.tmbNames.Add(thumbnail.Name);
          }
        }
        return this.tmbNames;
      }
      set => this.tmbNames = value;
    }

    /// <summary>Gets or sets the blob storage provider name.</summary>
    /// <value>The blob storage provider name.</value>
    [DataMember]
    public string BlobStorageProvider
    {
      get
      {
        if (string.IsNullOrEmpty(this.blobStorageProvider) && this.ContentItem != null)
          this.blobStorageProvider = ((MediaContent) this.ContentItem).BlobStorageProvider;
        return this.blobStorageProvider;
      }
      set => this.blobStorageProvider = value;
    }

    /// <summary>Gets or sets the URL of the album.</summary>
    [DataMember]
    public string LibraryUrl
    {
      get
      {
        if (this.ContentItem == null)
          return this.libraryUrl;
        ILocatable parent = (ILocatable) ((MediaContent) this.ContentItem).Parent;
        return parent == null ? string.Empty : this.provider.GetItemUrl(parent);
      }
      set => this.libraryUrl = value;
    }

    /// <summary>Gets or sets the library title.</summary>
    [DataMember]
    public string LibraryTitle
    {
      get => this.ContentItem != null ? ((MediaContent) this.ContentItem).Parent.Title.Value : this.libraryTitle;
      set
      {
        if (this.ContentItem != null)
          ((MediaContent) this.ContentItem).Parent.Title.Value = value;
        else
          this.libraryTitle = value;
      }
    }

    /// <summary>Gets or sets the extension of the media content file.</summary>
    [DataMember]
    public string Extension
    {
      get => this.ContentItem != null ? ((MediaContent) this.ContentItem).Extension ?? "unknown" : this.extension;
      set
      {
        if (this.ContentItem != null)
          ((MediaContent) this.ContentItem).Extension = value;
        else
          this.extension = value;
      }
    }

    /// <summary>Total size, in bytes, of all chunks</summary>
    [DataMember]
    public long TotalSize
    {
      get => this.ContentItem != null ? ((MediaContent) this.ContentItem).TotalSize / 1024L : this.totalSize;
      set => this.totalSize = value;
    }

    /// <summary>Gets or sets the parent pageId.</summary>
    [DataMember]
    public Guid ParentId
    {
      get => this.ContentItem != null ? ((MediaContent) this.ContentItem).Parent.Id : this.parentId;
      set
      {
        if (this.ContentItem != null)
          ((MediaContent) this.ContentItem).Parent.Id = value;
        else
          this.parentId = value;
      }
    }

    /// <summary>Gets or sets the media URL.</summary>
    [DataMember]
    public string MediaUrl
    {
      get => this.ContentItem != null ? ((MediaContent) this.ContentItem).MediaUrl : this.mediaUrl;
      set => this.mediaUrl = value;
    }

    /// <summary>Gets or sets the embed URL.</summary>
    [DataMember]
    public string EmbedUrl
    {
      get => this.ContentItem != null ? ((MediaContent) this.ContentItem).EmbedUrl : this.embedUrl;
      set => this.embedUrl = value;
    }

    /// <summary>Gets or sets the ordinal number.</summary>
    /// <value>The ordinal.</value>
    [DataMember]
    public float Ordinal
    {
      get => this.ContentItem != null ? ((MediaContent) this.ContentItem).Ordinal : this.ordinal;
      set => this.ordinal = value;
    }

    /// <summary>Gets or sets the description</summary>
    /// <value>The description.</value>
    [DataMember]
    public string Description
    {
      get => this.ContentItem != null ? (string) this.ContentItem.Description : this.description;
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.Description = (Lstring) value;
        else
          this.description = value;
      }
    }

    /// <summary>
    /// Gets or sets the date on which the <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" /> was modified
    /// </summary>
    [DataMember]
    public DateTime? LastModified
    {
      get => this.ContentItem != null ? new DateTime?(this.ContentItem.LastModified) : this.lastModified;
      set => this.lastModified = value;
    }

    /// <summary>
    /// Gets or sets the value indicating whether this item is  manageable (editable/deleable)
    /// </summary>
    [DataMember]
    public abstract bool IsManageable { get; set; }

    /// <summary>Gets or sets the folder title.</summary>
    [DataMember]
    public string FolderTitle
    {
      get
      {
        if (this.folderTitle == null)
          this.folderTitle = this.GetFolderTitle(this.ContentItem as MediaContent);
        return this.folderTitle;
      }
      set => this.folderTitle = value;
    }

    /// <summary>Gets or sets the Id of the master item.</summary>
    [DataMember]
    public Guid OriginalContentId
    {
      get => this.ContentItem != null ? this.ContentItem.OriginalContentId : this.originalContentId;
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.OriginalContentId = value;
        else
          this.originalContentId = value;
      }
    }

    internal static string GetTaxaText(
      Content item,
      string propertyName,
      string commandName,
      Type mediaContentSpecificType)
    {
      if (OrganizerBase.GetProperty(mediaContentSpecificType, propertyName) is TaxonomyPropertyDescriptor property)
      {
        Guid[] ids = ((IEnumerable<Guid>) property.GetValue((object) item)).ToArray<Guid>();
        if (ids != null && ids.Length != 0)
        {
          IOrderedQueryable<Taxon> orderedQueryable = TaxonomyManager.GetManager(property.TaxonomyProvider).GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (t => ids.Contains<Guid>(t.Id))).OrderBy<Taxon, string>((Expression<Func<Taxon, string>>) (t => t.Name));
          StringBuilder stringBuilder = new StringBuilder();
          foreach (Taxon taxon in (IEnumerable<Taxon>) orderedQueryable)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(", ");
            stringBuilder.AppendFormat("<a sys:href='javascript:void(0);' class='sf_binderCommand_{0}[{1}]({2})'>{3}</a>", (object) commandName, (object) taxon.Id, (object) propertyName, (object) HttpUtility.HtmlEncode((string) taxon.Title));
          }
          return stringBuilder.ToString();
        }
      }
      return string.Empty;
    }

    internal static string CreateThumbnailUrl(MediaContent item) => item.ResolveThumbnailUrl();

    /// <summary>Resolves the library full URL.</summary>
    /// <param name="mediaContent">Content of the media.</param>
    /// <param name="libraryPageId">The library page id.</param>
    /// <param name="resolveAsAbsoluteUrl">if set to <c>true</c> [resolve as absolute URL].</param>
    /// <returns></returns>
    internal string ResolveLibraryFullUrl(MediaContent contentItem, Guid libraryPageId)
    {
      string url = string.Empty;
      LibrariesDataProvider provider = contentItem.Provider as LibrariesDataProvider;
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(libraryPageId, false);
      if (siteMapNode != null)
      {
        url = MediaContentViewModel.AddQueryParameter(RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + this.LibraryUrl, "provider=" + provider.Name);
        Guid? folderId = contentItem.FolderId;
        if (folderId.HasValue)
        {
          folderId = contentItem.FolderId;
          if (folderId.Value != Guid.Empty)
          {
            folderId = contentItem.FolderId;
            string parameter = "folderId=" + (object) folderId.Value;
            url = MediaContentViewModel.AddQueryParameter(url, parameter);
          }
        }
      }
      return url;
    }

    /// <summary>Adds the query parameter.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns></returns>
    private static string AddQueryParameter(string url, string parameter) => url.Contains("?") ? url + "&" + parameter : url + "?" + parameter;

    /// <summary>Gets the folder title.</summary>
    /// <param name="libraryPageId">The library page id.</param>
    /// <returns></returns>
    internal string GetFolderTitle(MediaContent contentItem) => contentItem != null && contentItem.FolderId.HasValue && contentItem.FolderId.Value != Guid.Empty ? (string) FolderExtensions.GetFolder(LibrariesManager.GetManager((contentItem.Provider as LibrariesDataProvider).Name), contentItem.FolderId.Value).Title : (string) null;

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Content GetLive() => throw new NotImplementedException();

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Content GetTemp() => throw new NotImplementedException();
  }
}
