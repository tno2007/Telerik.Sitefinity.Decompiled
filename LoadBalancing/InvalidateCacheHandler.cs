// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.InvalidateCacheHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// A class responsible for handling cache invalidation system message sent to Sitefinity instances at load balanced environment.
  ///  </summary>
  public class InvalidateCacheHandler : ISystemMessageHandler
  {
    /// <summary>
    /// Determines whether this instance [can process system message] the specified messasge.
    /// </summary>
    /// <param name="messasge">The messasge.</param>
    /// <returns>
    /// 	<c>true</c> if this instance [can process system message] the specified messasge; otherwise, <c>false</c>.
    /// </returns>
    public bool CanProcessSystemMessage(SystemMessageBase messasge) => messasge.Key == "invalidateCacheKey" || messasge.Key == "InvalidateAllCacheKey";

    /// <summary>Processes the system message.</summary>
    /// <param name="messasge">The messasge.</param>
    public void ProcessSystemMessage(SystemMessageBase messasge)
    {
      try
      {
        InvalidateCacheMessage invalidateCacheMessage = new InvalidateCacheMessage(messasge.MessageData);
        if (messasge.AdditionalInfo == null || !messasge.AdditionalInfo.ContainsKey("SentFromDifferentRegion"))
          CacheDependency.NotifyWithoutSendingSystemMessage((IList<CacheDependencyKey>) invalidateCacheMessage.ExpiredItems);
        else
          CacheDependency.NotifyAndRaiseEvent((IList<CacheDependencyKey>) invalidateCacheMessage.ExpiredItems);
      }
      catch
      {
      }
    }
  }
}
