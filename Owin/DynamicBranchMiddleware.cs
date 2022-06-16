// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.DynamicBranchMiddleware
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin;
using Owin;

namespace Telerik.Sitefinity.Owin
{
  /// <summary>
  /// A middleware that can define dynamically on first execution new pipeline branch.
  /// </summary>
  /// <seealso cref="!:Microsoft.Owin.OwinMiddleware" />
  internal abstract class DynamicBranchMiddleware : OwinMiddleware
  {
    public DynamicBranchMiddleware(OwinMiddleware next, IAppBuilder app)
      : base(next)
    {
      this.AppBuilder = app;
    }

    public IAppBuilder AppBuilder { get; private set; }
  }
}
