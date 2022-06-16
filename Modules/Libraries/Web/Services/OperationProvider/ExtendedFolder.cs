// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider.ExtendedFolder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider
{
  internal class ExtendedFolder
  {
    private int foldersCount;
    private int itemsCount;
    private MediaContent[] previewItems;
    private IFolder folder;

    internal ExtendedFolder(
      IFolder folder,
      int foldersCount,
      IList<MediaContent> previewItems,
      int imagesCount)
    {
      this.folder = folder;
      this.FoldersCount = foldersCount;
      this.PreviewItems = previewItems.ToArray<MediaContent>();
      this.ChildrenCount = imagesCount;
      if (previewItems.Count > 0)
      {
        IUserDisplayNameBuilder displayNameBuilder = ObjectFactory.Resolve<IUserDisplayNameBuilder>();
        MediaContent mediaContent = previewItems.First<MediaContent>();
        this.LastUploaded = new DateTime?(mediaContent.DateCreated);
        this.LastUploadedBy = displayNameBuilder.GetUserDisplayName(mediaContent.Owner);
      }
      this.Breadcrumb = (IEnumerable<BreadcrumbItem>) new List<BreadcrumbItem>();
      if (!(folder is Library library))
        return;
      this.Storage = library.BlobStorageProvider;
      this.RunningTask = new Guid?(library.RunningTask);
    }

    [DataMember]
    public long MaxLibrarySizeInKb { get; set; }

    [DataMember]
    public long TotalLibrarySizeInKb { get; set; }

    [DataMember]
    public string LastUploadedBy { get; set; }

    [DataMember]
    public DateTime? LastUploaded { get; set; }

    [DataMember]
    public string Storage { get; set; }

    /// <summary>Gets or sets the FoldersCount.</summary>
    [DataMember]
    public int FoldersCount
    {
      get => this.foldersCount;
      set => this.foldersCount = value;
    }

    /// <summary>Gets or sets the ChildrenCount.</summary>
    [DataMember]
    public int ChildrenCount
    {
      get => this.itemsCount;
      set => this.itemsCount = value;
    }

    /// <summary>Gets or sets the PreviewItems.</summary>
    [DataMember]
    public MediaContent[] PreviewItems
    {
      get => this.previewItems;
      set => this.previewItems = value;
    }

    [DataMember]
    public Guid Id => this.folder == null ? Guid.Empty : this.folder.Id;

    [DataMember]
    public string Title => this.folder.Title.ToString();

    [DataMember]
    public string Description => this.folder.Description.ToString();

    [DataMember]
    public Guid ParentId => this.folder.ParentId;

    [DataMember]
    public Guid? RootId => this.folder is Folder folder ? new Guid?(folder.RootId) : new Guid?();

    [DataMember]
    public IEnumerable<BreadcrumbItem> Breadcrumb { get; set; }

    [DataMember]
    public Guid? RunningTask { get; set; }
  }
}
