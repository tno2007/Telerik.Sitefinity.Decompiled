// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.MediaFileLinkViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>Represents view model for media file links</summary>
  [DataContract]
  public class MediaFileLinkViewModel
  {
    private bool isDefault;
    private string snapshotUrl;
    private string totalSize;
    private string extension;
    private string providerName;
    private Guid fileId;
    private string mediaUrl;
    private string title;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.MediaFileLinkViewModel" /> class.
    /// </summary>
    public MediaFileLinkViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.MediaFileLinkViewModel" /> class.
    /// </summary>
    /// <param name="link">The link.</param>
    public MediaFileLinkViewModel(MediaFileLink link)
    {
      using (new CultureRegion(link.Culture))
      {
        this.fileId = link.FileId;
        this.title = (string) link.MediaContent.MediaFileUrlName + link.Extension;
        this.totalSize = new FileSizeStringifier().GetStringFromFileSize(link.TotalSize);
        this.extension = link.Extension;
        this.snapshotUrl = link.MediaContent.ResolveThumbnailUrl();
        this.mediaUrl = link.MediaContent.ThumbnailUrl;
      }
    }

    /// <summary>Gets or sets the provider name.</summary>
    /// <value>The provider name.</value>
    [DataMember]
    public virtual string ProviderName
    {
      get => this.providerName;
      set => this.providerName = value;
    }

    /// <summary>Gets or sets the id of the file.</summary>
    /// <value>The id of the file.</value>
    [DataMember]
    public Guid FileId
    {
      get => this.fileId;
      set => this.fileId = value;
    }

    /// <summary>Gets or sets the URL of the media content.</summary>
    [DataMember]
    public string MediaUrl
    {
      get => this.mediaUrl;
      set => this.mediaUrl = value;
    }

    /// <summary>Gets or sets the title of the image.</summary>
    [DataMember]
    public string Title
    {
      get => this.title;
      set => this.title = value;
    }

    /// <summary>
    /// Gets or sets the user friendly total size of the media content item.
    /// </summary>
    [DataMember]
    public string TotalSize
    {
      get => this.totalSize;
      set => this.totalSize = value;
    }

    /// <summary>Gets or sets the extension of the media content item.</summary>
    [DataMember]
    public string Extension
    {
      get => this.extension;
      set => this.extension = value;
    }

    /// <summary>
    /// Gets or sets the url of the snapshot that is shown in edit.
    /// </summary>
    [DataMember]
    public string SnapshotUrl
    {
      get => this.snapshotUrl;
      set => this.snapshotUrl = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the current file link is the default.
    /// </summary>
    [DataMember]
    public bool IsDefault
    {
      get => this.isDefault;
      set => this.isDefault = value;
    }
  }
}
