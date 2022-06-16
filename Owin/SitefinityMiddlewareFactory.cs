// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.SitefinityMiddlewareFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Owin
{
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
  public class SitefinityMiddlewareFactory
  {
    private List<Tuple<Type, object[], PipelineStage>> middlewareInitParamsStage;
    private static volatile SitefinityMiddlewareFactory instance;
    private static object syncRoot = new object();

    /// <summary>Gets the current.</summary>
    /// <value>The current.</value>
    public static SitefinityMiddlewareFactory Current
    {
      get
      {
        if (SitefinityMiddlewareFactory.instance == null)
        {
          lock (SitefinityMiddlewareFactory.syncRoot)
          {
            if (SitefinityMiddlewareFactory.instance == null)
              SitefinityMiddlewareFactory.instance = new SitefinityMiddlewareFactory();
          }
        }
        return SitefinityMiddlewareFactory.instance;
      }
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    internal IEnumerable<Tuple<Type, object[], PipelineStage>> MiddlewareInitParamsStage
    {
      get
      {
        foreach (Tuple<Type, object[], PipelineStage> tuple in (IEnumerable<Tuple<Type, object[], PipelineStage>>) this.middlewareInitParamsStage.OrderBy<Tuple<Type, object[], PipelineStage>, PipelineStage>((Func<Tuple<Type, object[], PipelineStage>, PipelineStage>) (t => t.Item3)))
          yield return tuple;
      }
    }

    /// <summary>
    /// Determines whether the Factory contains the specified middleware type.
    /// </summary>
    /// <param name="middlewareType">Type of the middleware.</param>
    /// <returns>True if the type is alredy registered. Otherwise false.</returns>
    internal bool Contains(Type middlewareType) => this.middlewareInitParamsStage.Select<Tuple<Type, object[], PipelineStage>, Type>((Func<Tuple<Type, object[], PipelineStage>, Type>) (t => t.Item1)).Contains<Type>(middlewareType);

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    public void AddMiddleware(Tuple<Type, object[], PipelineStage> middleware) => this.middlewareInitParamsStage.Add(middleware);

    /// <summary>Adds the middleware.</summary>
    /// <typeparam name="T">The Owin middleware.</typeparam>
    /// <param name="initParameters">The initialize parameters for the middleware constructor.</param>
    /// <param name="stage">The stage.</param>
    public void AddMiddleware<T>(object[] initParameters = null, PipelineStage stage = PipelineStage.PreHandlerExecute) where T : OwinMiddleware => this.middlewareInitParamsStage.Add(new Tuple<Type, object[], PipelineStage>(typeof (T), initParameters, stage));

    /// <summary>Adds the middleware if it is not already added.</summary>
    /// <typeparam name="T">The Owin middleware.</typeparam>
    /// <param name="initParameters">The initialize parameters for the middleware constructor.</param>
    /// <param name="stage">The stage.</param>
    public void AddIfNotPresentMiddleware<T>(object[] initParameters = null, PipelineStage stage = PipelineStage.PreHandlerExecute) where T : OwinMiddleware
    {
      if (this.Contains(typeof (T)))
        return;
      this.middlewareInitParamsStage.Add(new Tuple<Type, object[], PipelineStage>(typeof (T), initParameters, stage));
    }

    private SitefinityMiddlewareFactory() => this.middlewareInitParamsStage = new List<Tuple<Type, object[], PipelineStage>>();

    static SitefinityMiddlewareFactory() => SystemManager.Shutdown += new EventHandler<EventArgs>(SitefinityMiddlewareFactory.SystemManager_Shutdown);

    private static void SystemManager_Shutdown(object sender, EventArgs e) => SitefinityMiddlewareFactory.instance = (SitefinityMiddlewareFactory) null;
  }
}
