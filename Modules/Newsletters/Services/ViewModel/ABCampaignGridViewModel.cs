// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ABCampaignGridViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>View model class for the A/B tests per issue.</summary>
  [DataContract]
  public class ABCampaignGridViewModel
  {
    /// <summary>Gets or sets the id.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the A/B test.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the sample users</summary>
    [DataMember]
    public string SampleUsers { get; set; }

    /// <summary>Gets or sets the name of the winner.</summary>
    [DataMember]
    public string Winner { get; set; }

    /// <summary>Gets or sets the date when the A/B test is sent.</summary>
    [DataMember]
    public DateTime? DateSent { get; set; }

    /// <summary>Gets or sets the testing note.</summary>
    [DataMember]
    public string TestingNote { get; set; }

    /// <summary>Gets or sets the date when the A/B test is ended.</summary>
    [DataMember]
    public DateTime DateEnded { get; set; }

    /// <summary>Gets or sets the conclusion.</summary>
    [DataMember]
    public string Conclusion { get; set; }

    /// <summary>Gets or sets the last modified date.</summary>
    [DataMember]
    public DateTime LastModified { get; set; }
  }
}
