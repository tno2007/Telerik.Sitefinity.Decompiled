// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteConfigurationViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Multisite.Model;

namespace Telerik.Sitefinity.Multisite.Web.Services.ViewModel
{
  [DataContract]
  public class SiteConfigurationViewModel : SitePropertiesViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteConfigurationViewModel" /> class.
    /// </summary>
    public SiteConfigurationViewModel()
      : this((Site) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteConfigurationViewModel" /> class.
    /// </summary>
    /// <param name="site">The site.</param>
    public SiteConfigurationViewModel(Site site)
      : base(site)
    {
      this.DataSources = (ICollection<SiteDataSourceConfigViewModel>) new List<SiteDataSourceConfigViewModel>();
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteDataSourceConfigViewModel" /> objects.
    /// </summary>
    [DataMember]
    public ICollection<SiteDataSourceConfigViewModel> DataSources { get; set; }
  }
}
