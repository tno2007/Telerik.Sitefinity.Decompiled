// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.SitefinityOwinStartup
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Owin;

namespace Telerik.Sitefinity.Owin
{
  /// <summary>Sitefinity startup class.</summary>
  public class SitefinityOwinStartup
  {
    /// <summary>Configures the specified application.</summary>
    /// <param name="app">The application.</param>
    public virtual void Configuration(IAppBuilder app) => app.UseSitefinityMiddleware();
  }
}
