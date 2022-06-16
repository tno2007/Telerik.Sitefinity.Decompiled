// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.SitefinityMiddlewareExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Extensions;
using Owin;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Sitefinity.Owin
{
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
  public static class SitefinityMiddlewareExtensions
  {
    /// <summary>Registers default Sitefinity middlewares.</summary>
    /// <param name="app">An IAppBuilder object.</param>
    /// <returns>The IAppBuilder object.</returns>
    /// <exception cref="T:System.ArgumentNullException">If the app object is null.</exception>
    public static IAppBuilder UseSitefinityMiddleware(this IAppBuilder app)
    {
      if (app == null)
        throw new ArgumentNullException(nameof (app));
      app.Use<SitefinityMiddleware>((object) app);
      app.UseStageMarker(PipelineStage.Authenticate);
      return app;
    }
  }
}
