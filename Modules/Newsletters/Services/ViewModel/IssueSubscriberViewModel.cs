// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueSubscriberViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>Represents a view model for subscribers per issue.</summary>
  [DataContract]
  public class IssueSubscriberViewModel
  {
    /// <summary>Gets or sets the subscriber id.</summary>
    /// <value>The subscriber id.</value>
    [DataMember]
    public Guid SubscriberId { get; set; }

    /// <summary>Gets or sets the subscriber name.</summary>
    /// <value>The subscriber name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the subscriber email.</summary>
    /// <value>The subscriber email.</value>
    [DataMember]
    public string Email { get; set; }

    /// <summary>Gets or sets the state of the delivery.</summary>
    /// <value>The state of the delivery.</value>
    [DataMember]
    public string DeliveryState { get; set; }

    /// <summary>Gets or sets the date opened.</summary>
    /// <value>The date opened.</value>
    [DataMember]
    public DateTime? DateOpened { get; set; }
  }
}
