// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberIssueClickViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>
  /// Represents a view model for subscriber clicks per issue.
  /// </summary>
  [DataContract]
  public class SubscriberIssueClickViewModel
  {
    /// <summary>Gets or sets the name of the subscriber.</summary>
    [DataMember]
    public string SubscriberName { get; set; }

    /// <summary>Gets or sets the link URL.</summary>
    [DataMember]
    public string Url { get; set; }

    /// <summary>Gets or sets the date time the link has been clicked.</summary>
    [DataMember]
    public DateTime DateTimeClicked { get; set; }
  }
}
