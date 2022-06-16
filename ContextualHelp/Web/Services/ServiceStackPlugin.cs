// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContextualHelp.Web.Services.ServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using Telerik.Sitefinity.ContextualHelp.Web.Services.DTO.Request;

namespace Telerik.Sitefinity.ContextualHelp.Web.Services
{
  /// <summary>
  /// This class represents a Service Stack <see cref="T:ServiceStack.IPlugin" /> for registering the service endpoints of the <see cref="T:Telerik.Sitefinity.ContextualHelp.ContextualHelpModule" />.
  /// </summary>
  /// <seealso cref="T:ServiceStack.IPlugin" />
  internal class ServiceStackPlugin : IPlugin
  {
    internal const string ServiceRoute = "/contextual-help";
    internal const string MarkTooltipRoute = "/mark-tooltips";
    internal const string GetTooltipsRoute = "/get-tooltips";

    /// <summary>Registers the specified application host.</summary>
    /// <param name="appHost">The application host.</param>
    /// <exception cref="T:System.ArgumentNullException">The appHost parameter is empty. Cannot register service for AdminBridge module.</exception>
    public void Register(IAppHost appHost)
    {
      if (appHost == null)
        throw new ArgumentNullException("The appHost parameter is empty. Cannot register service for AdminBridge module.");
      appHost.RegisterService(typeof (WebService));
      appHost.Routes.Add<MarkTooltips>("/contextual-help" + "/mark-tooltips", "POST").Add<GetTooltips>("/contextual-help" + "/get-tooltips", "GET");
    }
  }
}
