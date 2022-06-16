// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.ViewModel.AvailableLinkViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Multisite.Web.Services.ViewModel
{
  /// <summary>View model used by content source selector.</summary>
  [DataContract]
  public class AvailableLinkViewModel
  {
    /// <summary>Gets or sets the data source link.</summary>
    [DataMember]
    public SiteDataSourceLinkViewModel Link { get; set; }

    /// <summary>
    /// Gets or sets an array of sites (their names) that use this provider.
    /// </summary>
    [DataMember]
    public string[] UsedAlsoBy { get; set; }

    /// <summary>
    /// Gets or sets the boolean value indicating whether this link can be deleted.
    /// </summary>
    [DataMember]
    public bool IsDeletable { get; set; }
  }
}
