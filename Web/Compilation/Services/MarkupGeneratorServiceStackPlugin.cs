// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.Services.MarkupGeneratorServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using Telerik.Sitefinity.Web.Compilation.Model;

namespace Telerik.Sitefinity.Web.Compilation.Services
{
  internal class MarkupGeneratorServiceStackPlugin : IPlugin
  {
    internal const string ServiceRoute = "/markup";

    public void Register(IAppHost appHost)
    {
      appHost.RegisterService(typeof (MarkupGeneratorService));
      appHost.Routes.Add<PageMarkupRequest>("/markup" + "/" + "pages", "POST").Add<PageKeyRequest>("/markup" + "/" + "keys/{StrategyKey}", "GET").Add<TemplateKeyRequest>("/markup" + "/" + "template-keys/{StrategyKey}", "GET").Add<TemplateMarkupRequest>("/markup" + "/" + "templates", "POST");
    }
  }
}
