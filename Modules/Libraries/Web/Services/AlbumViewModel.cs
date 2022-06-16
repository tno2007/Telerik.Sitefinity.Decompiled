// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.AlbumViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// View model class for the <see cref="T:Telerik.Sitefinity.Libraries.Model.Album" /> model.
  /// </summary>
  public class AlbumViewModel : LibraryViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.Services.AlbumViewModel" /> class.
    /// </summary>
    public AlbumViewModel() => this.Init((Telerik.Sitefinity.Libraries.Model.Album) null);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.Services.AlbumViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public AlbumViewModel(Telerik.Sitefinity.Libraries.Model.Album contentItem, ContentDataProviderBase provider)
      : base((Library) contentItem, provider)
    {
      this.Init(contentItem);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.Services.AlbumViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live album related to the master album.</param>
    /// <param name="tempItem">The temp album related to the master album.</param>
    public AlbumViewModel(
      Telerik.Sitefinity.Libraries.Model.Album contentItem,
      ContentDataProviderBase provider,
      Telerik.Sitefinity.Libraries.Model.Album live,
      Telerik.Sitefinity.Libraries.Model.Album temp)
      : base((Library) contentItem, provider, (Library) live, (Library) temp)
    {
      this.Init(contentItem);
    }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Content GetLive() => throw new NotSupportedException();

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Content GetTemp() => throw new NotSupportedException();

    /// <summary>Initializes properties default values for this album</summary>
    /// <param name="contentItem">The album object related to this item.</param>
    private void Init(Telerik.Sitefinity.Libraries.Model.Album contentItem)
    {
      if (contentItem != null)
      {
        this.IsManageable = contentItem.IsGranted("Image", "ManageImage");
        this.IsViewable = contentItem.IsGranted("Image", "ViewImage");
      }
      else
      {
        this.IsManageable = true;
        this.IsViewable = true;
      }
    }
  }
}
