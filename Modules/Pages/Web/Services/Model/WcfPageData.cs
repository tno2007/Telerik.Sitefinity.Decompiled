// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>Communicatio object for page data</summary>
  [DataContract]
  public class WcfPageData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageData" /> class.
    /// </summary>
    public WcfPageData()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageData" /> class.
    /// </summary>
    /// <param name="pageData">The page data.</param>
    public WcfPageData(PageNode page)
    {
      IStatusInfo status;
      if (page.TryGetExternalStatus(out status))
      {
        if (status.Data is ISchedulingStatus data)
        {
          this.PublicationDate = !data.PublicationDate.HasValue ? page.GetPageData().PublicationDate : data.PublicationDate.Value;
          this.ExpirationDate = data.ExpirationDate;
        }
      }
      else
      {
        PageData pageData = page.GetPageData();
        this.PublicationDate = pageData.PublicationDate;
        this.ExpirationDate = pageData.ExpirationDate;
      }
      this.PageNodeId = page.Id;
    }

    /// <summary>Gets or sets the publication date.</summary>
    /// <value>The publication date.</value>
    [DataMember]
    public DateTime PublicationDate { get; set; }

    /// <summary>Gets or sets the expiration date.</summary>
    /// <value>The expiration date.</value>
    [DataMember]
    public DateTime? ExpirationDate { get; set; }

    /// <summary>Gets or sets the page node id.</summary>
    /// <value>The page node id.</value>
    [DataMember]
    public Guid PageNodeId { get; set; }

    /// <summary>Gets or sets the workflow note.</summary>
    /// <value>The workflow note.</value>
    [DataMember]
    public string Note { get; set; }

    [Obsolete("This method is not used by Sitefinity and will be depricated in v11.1")]
    public void CopyToPage(PageNode page)
    {
      PageData pageData = page.GetPageData();
      pageData.PublicationDate = this.PublicationDate;
      pageData.ExpirationDate = this.ExpirationDate;
    }
  }
}
