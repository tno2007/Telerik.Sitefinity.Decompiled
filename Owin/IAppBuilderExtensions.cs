// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.Extensions.IAppBuilderExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Owin;

namespace Telerik.Sitefinity.Owin.Extensions
{
  internal static class IAppBuilderExtensions
  {
    public static IAppBuilder UseSslOffloading(this IAppBuilder app)
    {
      app.Use<SslOffloaderMiddleware>();
      return app;
    }

    public static IAppBuilder LogGlobalExceptions(this IAppBuilder app)
    {
      app.Use<GlobalExceptionMiddleware>();
      return app;
    }
  }
}
