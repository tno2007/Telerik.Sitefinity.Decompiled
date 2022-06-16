// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.NlbCheckHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.LoadBalancing.Web.Services;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// A class responsible for handling NlbCheck message sent to Sitefinity instances at load balanced environment.
  /// </summary>
  internal class NlbCheckHandler : ISystemMessageHandler
  {
    /// <summary>
    /// Determines whether this instance can process the specified message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>
    ///   <c>true</c> if this instance can process the specified message; otherwise, <c>false</c>.
    /// </returns>
    public bool CanProcessSystemMessage(SystemMessageBase message) => message != null && message.Key == "sf_NlbCheckMessage";

    /// <summary>Processes the system message.</summary>
    /// <param name="message">The message.</param>
    public void ProcessSystemMessage(SystemMessageBase message)
    {
      if (message == null)
        return;
      DateTime utcNow = DateTime.UtcNow;
      NlbCheckMessage.NlbCheckMessageInfo checkMessageInfo = JsonConvert.DeserializeObject<NlbCheckMessage.NlbCheckMessageInfo>(message.MessageData);
      if (checkMessageInfo == null)
        return;
      if (checkMessageInfo.RequestType == "ping")
      {
        string str = JsonConvert.SerializeObject((object) new NlbCheckMessage.NlbCheckMessageInfo()
        {
          RequestType = "pong",
          TimeoutTime = checkMessageInfo.TimeoutTime,
          OriginalSenderId = message.SenderId
        });
        NlbCheckMessage msg = new NlbCheckMessage();
        msg.MessageData = str;
        SystemMessageDispatcher.QueueSystemMessage((SystemMessageBase) msg);
      }
      else
      {
        if (!(checkMessageInfo.RequestType == "pong") || !(checkMessageInfo.OriginalSenderId == SystemWebService.LocalId) || checkMessageInfo.TimeoutTime < utcNow.ToFileTimeUtc())
          return;
        SystemManager.Cache.Add("sf_NlbCheckCacheKey", (object) utcNow);
      }
    }
  }
}
