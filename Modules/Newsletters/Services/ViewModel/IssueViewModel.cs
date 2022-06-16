// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>
  /// ViewModel class for the campaign type of the newsletter module.
  /// </summary>
  [DataContract]
  public class IssueViewModel : CampaignViewModel
  {
    /// <summary>
    /// Gets or sets the id of the root campaign of this issue.
    /// </summary>
    [DataMember]
    public Guid RootCampaignId { get; set; }

    /// <summary>Gets or sets the name of the root campaign.</summary>
    [DataMember]
    public string RootCampaignName { get; set; }
  }
}
