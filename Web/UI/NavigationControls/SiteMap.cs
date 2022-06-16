// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.SiteMap
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  /// <summary>
  /// Wrapper around Telerik's <see cref="T:Telerik.Web.UI.RadSiteMap" /> control that provides a navigational
  /// control inside of a Sitefinity.
  /// </summary>
  public class SiteMap : RadSiteMap
  {
    private SiteMapDataSource siteMapDataSource;
    private string siteMapProvider;
    private bool showStartNode;
    private bool startFormCurrentNode;
    private int startNodeOffset;
    private string startNodeUrl;
    private string skin;

    /// <summary>
    /// Gets or sets the sitemap provider. Default is "SitefinitySiteMap"
    /// </summary>
    /// <value>The sitemap provider.</value>
    [Category("DataSource")]
    public string SitemapProvider
    {
      get
      {
        if (string.IsNullOrEmpty(this.siteMapProvider))
          this.siteMapProvider = "SitefinitySiteMap";
        return this.siteMapProvider;
      }
      set => this.siteMapProvider = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to [show starting node].
    /// </summary>
    /// <value><c>true</c> if [show starting node]; otherwise, <c>false</c>.</value>
    [Category("DataSource")]
    public bool ShowStartingNode
    {
      get => this.showStartNode;
      set => this.showStartNode = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to [start from current node].
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [start from current node]; otherwise, <c>false</c>.
    /// </value>
    [Category("DataSource")]
    public bool StartFromCurrentNode
    {
      get => this.startFormCurrentNode;
      set => this.startFormCurrentNode = value;
    }

    /// <summary>Gets or sets the start node offset.</summary>
    /// <value>The start node offset.</value>
    [Category("DataSource")]
    public int StartNodeOffset
    {
      get => this.startNodeOffset;
      set => this.startNodeOffset = value;
    }

    /// <summary>Gets or sets the start node URL.</summary>
    /// <value>The start node URL.</value>
    [Category("DataSource")]
    public string StartNodeUrl
    {
      get => this.startNodeUrl;
      set => this.startNodeUrl = value;
    }

    /// <summary>
    /// Gets or sets the skin name for the control user interface.
    /// </summary>
    /// <value>
    /// A string containing the skin name for the control user interface. The default is string.Empty.
    /// </value>
    /// <remarks>
    /// If this property is not set, the control will render using the skin named "Default".
    /// If EnableEmbeddedSkins is set to false, the control will not render skin.
    /// </remarks>
    [Category("Appearance")]
    public override string Skin
    {
      get => string.IsNullOrEmpty(this.skin) ? "Default" : this.skin;
      set => this.skin = value;
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls() => this.Initialize();

    /// <summary>Initializes the DataBind.</summary>
    protected internal virtual void Initialize()
    {
      this.siteMapDataSource = (SiteMapDataSource) new SitefinitySiteMapDataSource();
      base.Skin = this.Skin;
      this.SetSiteMap();
      this.DataBind();
    }

    /// <summary>Sets the SiteMapDataSource control properties.</summary>
    protected internal virtual void SetSiteMap()
    {
      this.siteMapDataSource.SiteMapProvider = this.SitemapProvider;
      this.DataSource = (object) this.siteMapDataSource;
      this.siteMapDataSource.ShowStartingNode = this.showStartNode;
      this.siteMapDataSource.StartFromCurrentNode = this.startFormCurrentNode;
      this.siteMapDataSource.StartingNodeOffset = this.startNodeOffset;
      this.siteMapDataSource.StartingNodeUrl = this.startNodeUrl;
    }
  }
}
