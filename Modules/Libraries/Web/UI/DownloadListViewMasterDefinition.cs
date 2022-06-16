// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.DownloadListViewMasterDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of the respective master view control.
  /// </summary>
  public class DownloadListViewMasterDefinition : 
    ContentViewMasterDefinition,
    IDownloadListViewMasterDefinition,
    IDownloadListViewDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private ThumbnailType thumbnailType;
    private bool? showDownloadLinkBelowDescription;
    private bool? showDownloadLinkAboveDescription;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.DownloadListViewMasterDefinition" /> class.
    /// </summary>
    public DownloadListViewMasterDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.DownloadListViewMasterDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DownloadListViewMasterDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public DownloadListViewMasterDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the size of the thumbnail - None - no thumbnail, Big and Small
    /// </summary>
    /// <value></value>
    public ThumbnailType ThumbnailType
    {
      get => this.ResolveProperty<ThumbnailType>(nameof (ThumbnailType), this.thumbnailType);
      set => this.thumbnailType = value;
    }

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
  }
}
