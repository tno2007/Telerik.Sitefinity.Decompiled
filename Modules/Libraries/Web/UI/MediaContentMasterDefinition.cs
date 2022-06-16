// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.MediaContentMasterDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of the respective master view control.
  /// </summary>
  public class MediaContentMasterDefinition : 
    ContentViewMasterDefinition,
    IMediaContentMasterDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    private int? singleItemWidth;
    private int? thumbnailsWidth;
    private string thumbnailsName;
    private string singleItemThumbnailsName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.MediaContentMasterDefinition" /> class.
    /// </summary>
    public MediaContentMasterDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.MediaContentMasterDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public MediaContentMasterDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public MediaContentMasterDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the width of the single item. The single item could be an image or video.
    /// </summary>
    /// <value></value>
    [Obsolete("Use SingleItemThumbnailsName to define the thumbnails settings(profile/size) you want to use.")]
    public int? SingleItemWidth
    {
      get => this.ResolveProperty<int?>(nameof (SingleItemWidth), this.singleItemWidth);
      set => this.singleItemWidth = value;
    }

    /// <summary>Gets or sets the width of the thumbnails.</summary>
    /// <value></value>
    [Obsolete("Use ThumbnailsName to define the thumbnails settings(profile/size) you want to use.")]
    public int? ThumbnailsWidth
    {
      get => this.ResolveProperty<int?>(nameof (ThumbnailsWidth), this.thumbnailsWidth);
      set => this.thumbnailsWidth = value;
    }

    /// <summary>Gets or sets the name of the thumbnail.</summary>
    /// <value>The name of the thumbnail.</value>
    public string ThumbnailsName
    {
      get => this.ResolveProperty<string>(nameof (ThumbnailsName), this.thumbnailsName);
      set => this.thumbnailsName = value;
    }

    /// <summary>Gets or sets the name of the single item thumbnails.</summary>
    /// <value>The name of the single item thumbnails.</value>
    public string SingleItemThumbnailsName
    {
      get => this.ResolveProperty<string>(nameof (SingleItemThumbnailsName), this.singleItemThumbnailsName);
      set => this.singleItemThumbnailsName = value;
    }
  }
}
