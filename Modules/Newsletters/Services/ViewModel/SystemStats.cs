// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SystemStats
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>
  /// This type provides the statistics of different objects of the newsletters module.
  /// </summary>
  [DataContract]
  public class SystemStats
  {
    /// <summary>
    /// Gets or sets the total number of campaigns in the system.
    /// </summary>
    [DataMember]
    public int CampaignCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of A/B campaigns in the system.
    /// </summary>
    [DataMember]
    public int ABCampaignCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of mailing lists in the system.
    /// </summary>
    [DataMember]
    public int MailingListsCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of subscribers in the system.
    /// </summary>
    [DataMember]
    public int SubscribersCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of message templates in the system.
    /// </summary>
    [DataMember]
    public int MessageTemplatesCount { get; set; }
  }
}
