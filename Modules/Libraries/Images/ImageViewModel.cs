// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Images.ImageViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Libraries.Images
{
  /// <summary>Represents image view model.</summary>
  [DataContract]
  public class ImageViewModel : MediaContentViewModel
  {
    private string thumbnailUrl;
    private int width;
    private int height;
    private string categoryText;
    private string tagsText;
    private string alternativeText;
    private bool isManageable;
    private string lilbraryFullUrl;
    private bool isVectorGraphics;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.ImageViewModel" /> class.
    /// </summary>
    public ImageViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.ImageViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public ImageViewModel(Telerik.Sitefinity.Libraries.Model.Image contentItem, ContentDataProviderBase provider)
      : base((MediaContent) contentItem, provider)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.ImageViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live image related to the master image.</param>
    /// <param name="tempItem">The temp image related to the master image.</param>
    public ImageViewModel(
      Telerik.Sitefinity.Libraries.Model.Image contentItem,
      ContentDataProviderBase provider,
      Telerik.Sitefinity.Libraries.Model.Image live,
      Telerik.Sitefinity.Libraries.Model.Image temp)
      : base((MediaContent) contentItem, provider, (MediaContent) live, (MediaContent) temp)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.ImageViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isEditable">if set to <c>true</c> if the content is editable.</param>
    public ImageViewModel(Telerik.Sitefinity.Libraries.Model.Image contentItem, ContentDataProviderBase provider, bool isEditable)
      : this(contentItem, provider)
    {
      this.isEditable = isEditable;
    }

    /// <summary>Gets or sets the thumbnail URL.</summary>
    [DataMember]
    public string ThumbnailUrl
    {
      get
      {
        if (this.ContentItem == null)
          return this.thumbnailUrl;
        string thumbnailUrl = string.Empty;
        try
        {
          thumbnailUrl = MediaContentViewModel.CreateThumbnailUrl((MediaContent) this.ContentItem);
        }
        catch (BlobStorageException ex)
        {
        }
        return thumbnailUrl;
      }
      set => this.thumbnailUrl = value;
    }

    /// <summary>Gets or sets the width of the image.</summary>
    [DataMember]
    public int Width
    {
      get => this.ContentItem != null ? ((Telerik.Sitefinity.Libraries.Model.Image) this.ContentItem).Width : this.width;
      set
      {
        if (this.ContentItem != null)
          ((Telerik.Sitefinity.Libraries.Model.Image) this.ContentItem).Width = value;
        this.width = value;
      }
    }

    /// <summary>Gets or sets the height of the image.</summary>
    [DataMember]
    public int Height
    {
      get => this.ContentItem != null ? ((Telerik.Sitefinity.Libraries.Model.Image) this.ContentItem).Height : this.height;
      set
      {
        if (this.ContentItem != null)
          ((Telerik.Sitefinity.Libraries.Model.Image) this.ContentItem).Height = value;
        else
          this.height = value;
      }
    }

    /// <summary>
    /// Helper Property for presenting the list of the selected Categories as string
    /// </summary>
    [DataMember]
    public string CategoryText
    {
      get => this.ContentItem != null ? MediaContentViewModel.GetTaxaText(this.ContentItem, "Category", "categoryFilter", typeof (Telerik.Sitefinity.Libraries.Model.Image)) : this.categoryText;
      set => this.categoryText = value;
    }

    /// <summary>
    /// Helper Property for presenting the list of selected Tags as string
    /// </summary>
    [DataMember]
    public string TagsText
    {
      get => this.ContentItem != null ? MediaContentViewModel.GetTaxaText(this.ContentItem, "Tags", "tagFilter", typeof (Telerik.Sitefinity.Libraries.Model.Image)) : this.tagsText;
      set => this.tagsText = value;
    }

    /// <summary>Gets or sets the alternative text.</summary>
    /// <value>The alternative text.</value>
    [DataMember]
    public string AlternativeText
    {
      get => this.ContentItem != null ? ((Telerik.Sitefinity.Libraries.Model.Image) this.ContentItem).AlternativeText.Value : this.alternativeText;
      set
      {
        if (this.ContentItem != null)
          ((Telerik.Sitefinity.Libraries.Model.Image) this.ContentItem).AlternativeText.Value = value;
        else
          this.alternativeText = value;
      }
    }

    /// <summary>
    /// Gets or sets the value indicating whether this item is  manageable (editable/deleable)
    /// </summary>
    [DataMember]
    public override bool IsManageable
    {
      get
      {
        if (this.ContentItem == null)
          return this.isManageable;
        return ((ISecuredObject) this.ContentItem).IsGranted("Image", "ManageImage");
      }
      set => this.isManageable = value;
    }

    /// <summary>Gets or sets the library full URL.</summary>
    [DataMember]
    public string LibraryFullUrl
    {
      get => this.ContentItem != null ? this.ResolveLibraryFullUrl((MediaContent) this.ContentItem, LibrariesModule.LibraryImagesPageId) : this.lilbraryFullUrl;
      set => this.lilbraryFullUrl = value;
    }

    /// <summary>
    /// Gets or sets the value indicating whether this item is vector graphics.
    /// </summary>
    [DataMember]
    public bool IsVectorGraphics
    {
      get => this.ContentItem != null ? ((MediaContent) this.ContentItem).IsVectorGraphics() : this.isVectorGraphics;
      set => this.isVectorGraphics = value;
    }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Content GetLive() => (Content) this.provider.GetLiveBase<Telerik.Sitefinity.Libraries.Model.Image>((Telerik.Sitefinity.Libraries.Model.Image) this.ContentItem);

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Content GetTemp() => (Content) this.provider.GetTempBase<Telerik.Sitefinity.Libraries.Model.Image>((Telerik.Sitefinity.Libraries.Model.Image) this.ContentItem);
  }
}
