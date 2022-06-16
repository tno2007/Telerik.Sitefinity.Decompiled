// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.OutputCacheExpirationHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.OutputCache;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// A class responsible for handling in-memory pending output cache dependencies.
  /// </summary>
  public class OutputCacheExpirationHandler : ISystemMessageHandler
  {
    internal const string Key = "OutputCacheExpiration";

    /// <summary>
    /// Determines whether this instance [can process system message] the specified system message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>
    ///     <c>true</c> if this instance [can process system message] the specified message; otherwise, <c>false</c>.
    /// </returns>
    public bool CanProcessSystemMessage(SystemMessageBase message) => message.Key == "OutputCacheExpiration";

    /// <summary>Processes the system message.</summary>
    /// <param name="message">The message.</param>
    public void ProcessSystemMessage(SystemMessageBase message)
    {
      if (!(message.Key == "OutputCacheExpiration"))
        return;
      OutputCacheExpirationMessage expirationMessage = new OutputCacheExpirationMessage(message.MessageData);
      OutputCacheWorker.NotifyCacheDependencyQueue(expirationMessage.Types, expirationMessage.Keys, false);
    }
  }
}
