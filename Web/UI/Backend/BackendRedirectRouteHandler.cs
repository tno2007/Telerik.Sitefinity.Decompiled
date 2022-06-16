// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.BackendRedirectRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>Redirects the request to the specified URL.</summary>
  public class BackendRedirectRouteHandler : RedirectRoutingHandler
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.BackendRedirectRouteHandler" /> with the provided redirectKey.
    /// </summary>
    /// <param name="redirectKey">The key used to determine the redirection location.</param>
    /// <param name="handlerName">The name of this handler.</param>
    public BackendRedirectRouteHandler(string redirectKey, string handlerName)
      : this(redirectKey, handlerName, true)
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.BackendRedirectRouteHandler" /> with the provided redirectKey.
    /// </summary>
    /// <param name="redirectKey">The key used to determine the redirection location.</param>
    /// <param name="handlerName">The name of this handler.</param>
    /// <param name="permenentRedirect">if set to <c>true</c> [permenent redirect].</param>
    public BackendRedirectRouteHandler(
      string redirectKey,
      string handlerName,
      bool permenentRedirect)
      : base("_", handlerName, permenentRedirect)
    {
      this.RedirectKey = redirectKey;
      this.ResourceClassId = typeof (PageResources).Name;
    }

    /// <summary>Gets the root path for this handler.</summary>
    public override string Root => this.Name == "Sitefinity" ? string.Empty : "Sitefinity";

    /// <summary>Gets (or protected sets) the URL to redirect to.</summary>
    public override string RedirectUrl
    {
      get
      {
        if (base.RedirectUrl != "_")
          return base.RedirectUrl;
        int num = this.RedirectKey.IndexOf('?');
        string str = string.Empty;
        if (num != -1)
        {
          str = this.RedirectKey.Substring(num);
          this.RedirectKey = this.RedirectKey.Substring(0, num);
        }
        string redirectKey = this.RedirectKey;
        if (redirectKey == "Startup")
          return "~/Sitefinity/Startup";
        if (redirectKey == "Licensing")
          return "~/Sitefinity/Licensing" + str;
        IModule module;
        if (SystemManager.ApplicationModules.TryGetValue(this.RedirectKey, out module))
        {
          string landingPageUrl = module.GetLandingPageUrl();
          if (!string.IsNullOrEmpty(landingPageUrl))
            return landingPageUrl;
        }
        throw new InvalidOperationException(Res.Get<ErrorMessages>().InvalidRedirectKey);
      }
    }

    /// <summary>
    /// Gets the key used to determine the redirection location.
    /// </summary>
    public virtual string RedirectKey { get; private set; }
  }
}
