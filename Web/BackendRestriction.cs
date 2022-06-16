// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.BackendRestriction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Net;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Defines the default back end restriction behavior</summary>
  public class BackendRestriction : IBackendRestrictionBehavior
  {
    private bool isBackendDisabled;
    private static IBackendRestrictionBehavior currentRestriction = ObjectFactory.IsTypeRegistered<IBackendRestrictionBehavior>() ? ObjectFactory.Resolve<IBackendRestrictionBehavior>() : (IBackendRestrictionBehavior) new BackendRestriction();
    private const string SiteSyncModuleName = "Synchronization";
    private const string SiteSyncRoute = "Sitefinity/CMIS/RestAtom/";
    private const string SiteSyncTestConnectionRoute = "/Sitefinity/Services/SiteSync/SiteSyncService.svc/";

    internal static IBackendRestrictionBehavior Current => BackendRestriction.currentRestriction;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.BackendRestriction" /> class.
    /// </summary>
    public BackendRestriction() => this.isBackendDisabled = Config.Get<SystemConfig>().DisableBackendUI;

    /// <inheritdoc />
    public virtual void EndRequestIfForbidden(HttpContextBase context)
    {
      HttpContextBase context1 = context.ApplicationInstance != null ? context : SystemManager.CurrentHttpContext;
      if (!this.IsBackendDisabled || this.AllowRequest(context1))
        return;
      context1.Response.StatusCode = (int) this.DefaultStatusCode;
      context1.Response.End();
    }

    /// <summary>Allows the request, when the back end is disabled.</summary>
    /// <param name="context">The context.</param>
    /// <returns>True, if the request should be allowed, otherwise - false.</returns>
    protected virtual bool AllowRequest(HttpContextBase context)
    {
      string str = context.Request.AppRelativeCurrentExecutionFilePath.Substring(1) + context.Request.PathInfo;
      return SystemManager.IsInLoadBalancingMode && str.StartsWith("/Sitefinity/Services/LoadBalancing/SystemWebService.svc/", StringComparison.OrdinalIgnoreCase) || SystemManager.IsModuleEnabled("Synchronization") && (str.StartsWith("Sitefinity/CMIS/RestAtom/", StringComparison.OrdinalIgnoreCase) || str.StartsWith("/Sitefinity/Services/SiteSync/SiteSyncService.svc/", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the default HTTP status code to return when the request is forbidden.
    /// </summary>
    /// <value>The default status code.</value>
    protected virtual HttpStatusCode DefaultStatusCode => HttpStatusCode.NotFound;

    /// <summary>
    /// Gets a value indicating whether the back end is disabled.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is back end disabled; otherwise, <c>false</c>.
    /// </value>
    protected virtual bool IsBackendDisabled => this.isBackendDisabled;
  }
}
