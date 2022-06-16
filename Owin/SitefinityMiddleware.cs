// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.SitefinityMiddleware
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Extensions;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Owin.Extensions;

namespace Telerik.Sitefinity.Owin
{
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
  internal class SitefinityMiddleware : DynamicBranchMiddleware
  {
    private readonly Lazy<Func<IDictionary<string, object>, Task>> sitefinityMiddlewarePipeline;

    public SitefinityMiddleware(OwinMiddleware next, IAppBuilder app)
      : base(next, app)
    {
      this.sitefinityMiddlewarePipeline = this.BuildSitefinityPipeline(app);
    }

    public override async Task Invoke(IOwinContext context)
    {
      SitefinityMiddleware sitefinityMiddleware = this;
      if (Bootstrapper.IsReady)
        await sitefinityMiddleware.sitefinityMiddlewarePipeline.Value(context.Environment);
      else
        await sitefinityMiddleware.Next.Invoke(context);
    }

    private static Func<IDictionary<string, object>, Task> BuildSitefinityCustomPipeline(
      IAppBuilder app,
      OwinMiddleware next)
    {
      IAppBuilder appBuilder = app.New();
      SitefinityMiddleware.UseSitefinityCoreMiddlewares(appBuilder);
      foreach (Tuple<Type, object[], PipelineStage> tuple in SitefinityMiddlewareFactory.Current.MiddlewareInitParamsStage)
      {
        Type middleware = tuple.Item1;
        object[] collection = tuple.Item2;
        PipelineStage stage = tuple.Item3;
        List<object> source = new List<object>();
        if (middleware.IsSubclassOf(typeof (DynamicBranchMiddleware)))
          source.Add((object) appBuilder);
        if (collection != null)
          source.AddRange((IEnumerable<object>) collection);
        if (source.Count<object>() > 0)
          appBuilder.Use((object) middleware, source.ToArray());
        else
          appBuilder.Use((object) middleware);
        appBuilder.UseStageMarker(stage);
      }
      appBuilder.Run((Func<IOwinContext, Task>) (ctx => next.Invoke(ctx)));
      return appBuilder.Build();
    }

    private static void UseSitefinityCoreMiddlewares(IAppBuilder newApp)
    {
      newApp.Use<GlobalExceptionMiddleware>();
      newApp.Use<HttpsRedirectMiddleware>();
      newApp.Use<SslOffloaderMiddleware>();
      newApp.UseStageMarker(PipelineStage.Authenticate);
    }

    private Lazy<Func<IDictionary<string, object>, Task>> BuildSitefinityPipeline(
      IAppBuilder app)
    {
      return new Lazy<Func<IDictionary<string, object>, Task>>((Func<Func<IDictionary<string, object>, Task>>) (() => SitefinityMiddleware.BuildSitefinityCustomPipeline(app, this.Next)), true);
    }
  }
}
