// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteDataSourceConfigViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Multisite.Web.Services.ViewModel
{
  /// <summary>
  /// View model used for the configuration of data sources for sites in ConfigureModulesView.
  /// </summary>
  [DataContract]
  public class SiteDataSourceConfigViewModel
  {
    public SiteDataSourceConfigViewModel() => this.Links = (ICollection<SiteDataSourceLinkViewModel>) new List<SiteDataSourceLinkViewModel>();

    /// <summary>Gets or sets the name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the title.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is checked.
    /// </summary>
    [DataMember]
    public bool IsChecked { get; set; }

    /// <summary>
    /// Gets or sets an artificial link to be used by the client for creating a new link.
    /// </summary>
    [DataMember]
    public SiteDataSourceLinkViewModel SampleLink { get; set; }

    /// <summary>Gets or sets the links.</summary>
    [DataMember]
    public ICollection<SiteDataSourceLinkViewModel> Links { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this data source has the ability to select multiple providers.
    /// </summary>
    [DataMember]
    public bool AllowMultipleProviders { get; set; }

    /// <summary>
    /// An array of depentant data source names that this data source depends on.
    /// For example Ecommerce Orders is always selected together with Ecommerce Products.
    /// </summary>
    /// <value></value>
    [DataMember]
    public string[] DependantDataSources { get; set; }
  }
}
