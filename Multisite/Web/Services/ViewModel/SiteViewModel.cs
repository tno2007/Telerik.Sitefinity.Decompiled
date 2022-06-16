// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Multisite.Model;

namespace Telerik.Sitefinity.Multisite.Web.Services.ViewModel
{
  /// <summary>Represents the site view model.</summary>
  [DataContract]
  public class SiteViewModel : SitePropertiesViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteViewModel" /> class.
    /// </summary>
    public SiteViewModel()
      : this((Site) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteViewModel" /> class.
    /// </summary>
    /// <param name="site">The site.</param>
    public SiteViewModel(Site site)
      : base(site)
    {
      if (site == null)
        return;
      SiteDataSourceLink[] array = site.SiteDataSourceLinks.ToArray<SiteDataSourceLink>();
      this.SiteDataSourceLinks = (IList<SiteDataSourceLinkViewModel>) new List<SiteDataSourceLinkViewModel>();
      foreach (SiteDataSourceLink siteDataSourceLink in array)
      {
        SiteDataSourceLinkViewModel sourceLinkViewModel = (SiteDataSourceLinkViewModel) null;
        try
        {
          sourceLinkViewModel = new SiteDataSourceLinkViewModel(siteDataSourceLink);
        }
        catch (InvalidOperationException ex)
        {
          MultisiteManager manager = MultisiteManager.GetManager();
          site.SiteDataSourceLinks.Remove(siteDataSourceLink);
          manager.Delete(siteDataSourceLink);
          manager.SaveChanges();
        }
        if (sourceLinkViewModel != null)
          this.SiteDataSourceLinks.Add(sourceLinkViewModel);
      }
    }

    /// <summary>Gets or sets the content sources.</summary>
    [DataMember]
    public IList<SiteDataSourceLinkViewModel> SiteDataSourceLinks { get; set; }
  }
}
