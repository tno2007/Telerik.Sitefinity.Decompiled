// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>
  /// ViewModel class for the subscriber model of the newsletters module.
  /// </summary>
  [DataContract]
  public class SubscriberViewModel : IEquatable<SubscriberViewModel>
  {
    private List<MailingListViewModel> lists;
    private string subscriberReportUrl;
    private string email;

    /// <summary>Gets or sets the id of the subscriber.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the array of lists to which the user belongs to.
    /// </summary>
    [DataMember]
    public List<MailingListViewModel> Lists
    {
      get
      {
        if (this.lists == null)
          this.lists = new List<MailingListViewModel>();
        return this.lists;
      }
      set => this.lists = value;
    }

    /// <summary>Gets or sets the first name of the subscriber.</summary>
    [DataMember]
    public string FirstName { get; set; }

    /// <summary>Gets or sets the last name of the subscriber.</summary>
    [DataMember]
    public string LastName { get; set; }

    /// <summary>Gets or sets the name of the subscriber.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the email address of the subscriber.</summary>
    [DataMember]
    public string Email
    {
      get => this.email;
      set
      {
        if (!string.IsNullOrEmpty(value))
          value = value.ToLower();
        this.email = value;
      }
    }

    /// <summary>
    /// Gets or sets the url of the page with the campaign report.
    /// </summary>
    [DataMember]
    public string SubscriberReportUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.subscriberReportUrl))
        {
          SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(NewslettersModule.subscriberReportPageId, false);
          if (siteMapNode == null)
            return "#";
          this.subscriberReportUrl = VirtualPathUtility.ToAbsolute(RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) this.Id);
        }
        return this.subscriberReportUrl;
      }
      set => this.subscriberReportUrl = value;
    }

    bool IEquatable<SubscriberViewModel>.Equals(
      SubscriberViewModel other)
    {
      return other != null && this.Email.ToUpperInvariant() == other.Email.ToUpperInvariant();
    }

    public override int GetHashCode() => this.Email == null ? 0 : this.Email.ToUpperInvariant().GetHashCode();
  }
}
