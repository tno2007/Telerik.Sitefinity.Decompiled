// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.ProviderUsageCollectionContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>Extends CollectionContext with</summary>
  [DataContract(Name = "ProviderUsageCollectionContext", Namespace = "Telerik.Sitefinity.Security.Web.Services")]
  public class ProviderUsageCollectionContext : CollectionContext<WcfProviderSite>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.ProviderUsageCollectionContext" /> class.
    /// </summary>
    /// <param name="sites">The sites.</param>
    /// <param name="securedObjectId">The secured object identifier.</param>
    public ProviderUsageCollectionContext(
      IEnumerable<WcfProviderSite> sites,
      string securedObjectId)
      : base(sites)
    {
      this.SecuredObjectId = securedObjectId;
    }

    /// <summary>Gets or sets the secured object identifier.</summary>
    /// <value>The secured object identifier.</value>
    [DataMember]
    public virtual string SecuredObjectId { get; set; }
  }
}
