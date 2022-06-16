// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.NlbCheck
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.LoadBalancing.Web.Services;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// Defines a functionality that checks if Nlb is working.
  /// </summary>
  internal class NlbCheck : HealthCheckBase
  {
    public const string NlbCheckCacheKey = "sf_NlbCheckCacheKey";
    public const string RequestTimeoutParamName = "requestTimeout";
    public const int DefaultRequestTimeout = 1000;
    public readonly string HealthyMsg = "Nlb is up";
    public readonly string UnhealthyMsg = "Nlb is not working or response time is too long";

    /// <inheritdoc />
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Not needed")]
    protected override async Task<HealthCheckResult> RunCore()
    {
      NlbCheck nlbCheck = this;
      int result = 0;
      // ISSUE: explicit non-virtual call
      if (!int.TryParse(__nonvirtual (nlbCheck.Parameters)["requestTimeout"], out result) || result < 1)
        result = 1000;
      SystemManager.Cache.Add("sf_NlbCheckCacheKey", (object) DateTime.MinValue);
      nlbCheck.SendPingMessage(result);
      Thread.Sleep(result);
      DateTime? data = SystemManager.Cache.GetData("sf_NlbCheckCacheKey") as DateTime?;
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      return !data.HasValue || !(data.Value > DateTime.MinValue) ? HealthCheckResult.Unhealthy(__nonvirtual (nlbCheck.Name), nlbCheck.UnhealthyMsg) : HealthCheckResult.Healthy(__nonvirtual (nlbCheck.Name), nlbCheck.HealthyMsg);
    }

    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Making this static doesn't actually improve perf.")]
    protected void SendPingMessage(int timeout)
    {
      DateTime utcNow = DateTime.UtcNow;
      string str = JsonConvert.SerializeObject((object) new NlbCheckMessage.NlbCheckMessageInfo()
      {
        RequestType = "ping",
        TimeoutTime = utcNow.AddSeconds((double) timeout).ToFileTimeUtc(),
        OriginalSenderId = SystemWebService.LocalId
      });
      NlbCheckMessage msg = new NlbCheckMessage();
      msg.MessageData = str;
      SystemMessageDispatcher.QueueSystemMessage((SystemMessageBase) msg);
    }
  }
}
