// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Services.CacheServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using Telerik.Sitefinity.Web.OutputCache.Services.Model;

namespace Telerik.Sitefinity.Web.OutputCache.Services
{
  internal class CacheServiceStackPlugin : IPlugin
  {
    internal const string RouteClear = "/clear";
    internal const string ServiceRoute = "/cache";

    public void Register(IAppHost appHost)
    {
      if (appHost == null)
        return;
      appHost.RegisterService(typeof (CacheService));
      appHost.Routes.Add<ClearCacheRequest>("/cache" + "/" + "/clear", ApplyTo.Post);
    }
  }
}
