// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Services.CacheServiceAuthFilterAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Web;
using System.Net;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.ServiceStack.Filters;

namespace Telerik.Sitefinity.Web.OutputCache.Services
{
  internal class CacheServiceAuthFilterAttribute : RequestTokenAuthenticationFilterAttribute
  {
    private const string KEYAUTHHEADER = "SF_OUTPUTCACHE_AUTH";

    public override void Execute(IRequest req, IResponse res, object requestDto)
    {
      if (req == null)
        throw new HttpError(HttpStatusCode.NotFound);
      if (!Config.Get<SystemConfig>().CacheSettings.CacheService.Enabled)
        throw new HttpError(HttpStatusCode.NotFound);
      base.Execute(req, res, requestDto);
    }

    protected override string GetAuthHeaderName() => "SF_OUTPUTCACHE_AUTH";

    protected override string GetAuthenticationKey() => Config.Get<SystemConfig>().CacheSettings.CacheService.AuthenticationKey;

    protected override bool RequireHttps() => Config.Get<SystemConfig>().CacheSettings.CacheService.RequireHttpsForAllRequests;
  }
}
