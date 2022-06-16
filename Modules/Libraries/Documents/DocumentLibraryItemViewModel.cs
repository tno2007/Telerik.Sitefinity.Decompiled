// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Documents.DocumentLibraryItemViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Modules.Libraries.Documents
{
  /// <summary>
  /// Represents an item or folder that is in a document library.
  /// </summary>
  [DataContract]
  public class DocumentLibraryItemViewModel : DocumentViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentLibraryItemViewModel" /> class.
    /// </summary>
    public DocumentLibraryItemViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentLibraryItemViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public DocumentLibraryItemViewModel(Document contentItem, ContentDataProviderBase provider)
      : base(contentItem, provider)
    {
      this.IsFolder = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentLibraryItemViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live content related to the master content.</param>
    /// <param name="tempItem">The temp content related to the master content.</param>
    public DocumentLibraryItemViewModel(
      Document contentItem,
      ContentDataProviderBase provider,
      Document live,
      Document temp)
      : base(contentItem, provider, live, temp)
    {
      this.IsFolder = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentLibraryItemViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isEditable">if set to <c>true</c> if the content is editable.</param>
    public DocumentLibraryItemViewModel(
      Document contentItem,
      ContentDataProviderBase provider,
      bool isEditable)
      : base(contentItem, provider, isEditable)
    {
      this.IsFolder = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentLibraryItemViewModel" /> class.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <param name="provider">The provider.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public DocumentLibraryItemViewModel(IFolder folder, ContentDataProviderBase provider)
    {
      this.ProviderName = provider.Name;
      this.IsFolder = true;
      this.Id = folder.Id;
      this.Title = (string) folder.Title;
      this.isDeletable = true;
      this.IsManageable = true;
      this.Url = folder.UrlName.ToString();
      if (provider is IFolderOAProvider provider1)
      {
        int num = provider1.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == (Guid?) folder.Id)).Count<Folder>();
        string str;
        if (num <= 0)
          str = "";
        else
          str = Res.Get<LibrariesResources>().LibrariesCountFormat.Arrange((object) num);
        this.LibrariesCount = str;
      }
      if (provider is LibrariesDataProvider librariesDataProvider)
      {
        int num = librariesDataProvider.GetDocuments().Where<Document>((Expression<Func<Document, bool>>) (m => m.FolderId == (Guid?) folder.Id && (int) m.Status == 0)).Count<Document>();
        string str;
        if (num <= 0)
          str = "";
        else
          str = Res.Get<LibrariesResources>().DocumentsCountFormat.Arrange((object) num);
        this.DocumentsCount = str;
      }
      this.LastModified = new DateTime?(DateTime.UtcNow);
      this.DateCreated = DateTime.UtcNow;
      this.DateModified = DateTime.UtcNow;
      this.ExpirationDate = DateTime.UtcNow;
      this.PublicationDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is folder.
    /// </summary>
    [DataMember]
    public bool IsFolder { get; set; }

    /// <summary>Gets or sets the libraries count.</summary>
    [DataMember]
    public string LibrariesCount { get; set; }

    /// <summary>Gets or sets the documents count.</summary>
    [DataMember]
    public string DocumentsCount { get; set; }
  }
}
