// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.MediaContentDetailDefinition
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
  /// A definition class containing all information needed to construct an instance of the respective detail view control.
  /// </summary>
  public class MediaContentDetailDefinition : 
    ContentViewDetailDefinition,
    IMediaContentDetailDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    private bool? showDownloadLinkBelowDescription;
    private bool? showDownloadLinkAboveDescription;
    private int? singleItemWidth;
    private string singleItemThumbnailName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.MediaContentDetailDefinition" /> class.
    /// </summary>
    public MediaContentDetailDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.MediaContentDetailDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public MediaContentDetailDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public MediaContentDetailDefinition GetDefinition() => this;

    /// <summary>Displays a download link below the description</summary>
    public bool? ShowDownloadLinkBelowDescription
    {
      get => this.ResolveProperty<bool?>(nameof (ShowDownloadLinkBelowDescription), this.showDownloadLinkBelowDescription);
      set => this.showDownloadLinkBelowDescription = value;
    }

    /// <summary>Displays a download link above the description</summary>
    public bool? ShowDownloadLinkAboveDescription
    {
      get => this.ResolveProperty<bool?>(nameof (ShowDownloadLinkAboveDescription), this.showDownloadLinkAboveDescription);
      set => this.showDownloadLinkAboveDescription = value;
    }

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

    /// <summary>Gets or sets the name of the single item thumbnails.</summary>
    /// <value>The name of the single item thumbnails.</value>
    public string SingleItemThumbnailsName
    {
      get => this.ResolveProperty<string>(nameof (SingleItemThumbnailsName), this.singleItemThumbnailName);
      set => this.singleItemThumbnailName = value;
    }
  }
}
