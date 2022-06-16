// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.UserProviderPair
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// Class used to transfer information about the user in WCF.
  /// </summary>
  [DataContract]
  [DebuggerDisplay("UserProviderPair: Remove={Remove},  UserId={UserId}, ProviderName={ProviderName}")]
  public class UserProviderPair
  {
    /// <summary>Gets or sets the pageId of the user.</summary>
    [DataMember]
    public virtual Guid UserId { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    [DataMember]
    public virtual string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether user has been marked (on the client) for removal.
    /// </summary>
    [DataMember]
    public virtual bool Remove { get; set; }
  }
}
