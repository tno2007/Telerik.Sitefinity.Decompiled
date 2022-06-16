// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.WcfContentLifecycleStatus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>Contains information</summary>
  [DataContract]
  public class WcfContentLifecycleStatus
  {
    /// <summary>
    /// Indicatates that this master has a live version (was published)
    /// </summary>
    [DataMember]
    public bool IsPublished { get; set; }

    /// <summary>
    /// Status message (e.g. "Published", or "Master newer than published"
    /// </summary>
    [DataMember]
    public string Message { get; set; }

    /// <summary>The full name of the user who locked the record</summary>
    [DataMember]
    public string LockedByUsername { get; set; }

    /// <summary>
    /// Indicates whether the currently logged in user is admin
    /// </summary>
    [DataMember]
    public bool IsAdmin { get; set; }

    /// <summary>Indicates whether the item is locked</summary>
    [DataMember]
    public bool IsLocked { get; set; }

    /// <summary>
    /// Indicates, if the item is locked, whether the owner is the currently logged in user
    /// </summary>
    [DataMember]
    public bool IsLockedByMe { get; set; }

    /// <summary>Indicates whether the content supports lifecycle</summary>
    [DataMember]
    public bool SupportsContentLifecycle { get; set; }

    /// <summary>Defines an error message related to Content Lifecycle</summary>
    /// <remarks>
    /// If this is not empty on the client side, it is assumed than an event (error) has occurred
    /// that should stop the user from doing the current CLC-related operation. The value itself
    /// is considered to be the localized error message sent from the server.
    /// </remarks>
    [DataMember]
    public string ErrorMessage { get; set; }

    [DataMember]
    public DateTime? LockedSince { get; set; }

    [DataMember]
    public DateTime? LastModified { get; set; }

    [DataMember]
    public string LastModifiedBy { get; set; }

    [DataMember]
    public DateTime? PublicationDate { get; set; }

    [DataMember]
    public string WorkflowStatus { get; set; }

    [DataMember]
    public bool HasLiveVersion { get; set; }
  }
}
