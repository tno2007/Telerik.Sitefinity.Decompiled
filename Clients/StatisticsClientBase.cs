// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Clients.StatisticsClientBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Statistics;

namespace Telerik.Sitefinity.Clients
{
  /// <summary>Base class for the statistics clients.</summary>
  public abstract class StatisticsClientBase
  {
    private IStatisticsService service;

    /// <summary>
    /// Gets the value indicating if current user is authenticated.
    /// </summary>
    protected virtual bool IsAuthenticated => ClaimsManager.GetCurrentIdentity().IsAuthenticated;

    /// <summary>Gets the id of the currently logged in user.</summary>
    protected virtual Guid UserId => ClaimsManager.GetCurrentIdentity().UserId;

    /// <summary>
    /// Gets the name of the membership provider through which the current
    /// user is logged in.
    /// </summary>
    protected virtual string MembershipProvider => ClaimsManager.GetCurrentIdentity().MembershipProvider;

    /// <summary>Gets the cookie identifier of the current visitor.</summary>
    /// <remarks>
    /// If the visitor does not have a cookie identifier, a new one will be generated.
    /// </remarks>
    protected virtual string CurrentVisitorTrackingCookieIdentifier
    {
      get
      {
        HttpCookieCollection cookies = this.CurrentHttpContext.Request.Cookies;
        return cookies["sf-trckngckie"] != null ? cookies["sf-trckngckie"].Value : Guid.NewGuid().ToString();
      }
    }

    /// <summary>Gets the current http context.</summary>
    protected virtual HttpContextBase CurrentHttpContext => SystemManager.CurrentHttpContext;

    /// <summary>Gets the current time.</summary>
    protected virtual DateTime CurrentTime => DateTime.Now;

    /// <summary>
    /// Gets an instance of the <see cref="T:Telerik.Sitefinity.Services.Statistics.IStatisticsService" />.
    /// </summary>
    protected internal virtual IStatisticsService Service
    {
      get
      {
        if (this.service == null)
          this.service = SystemManager.GetStatisticsService();
        return this.service;
      }
    }

    /// <summary>
    /// This method returns the subject for the current visitor.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Services.Statistics.ISentenceSubject" />.
    /// </returns>
    protected ISentenceSubject GetVisitorSubject() => this.IsAuthenticated ? this.Service.CreateSentenceSubject(this.UserId, this.MembershipProvider, typeof (SitefinityIdentity).FullName) : this.Service.CreateSentenceSubject(this.CurrentVisitorTrackingCookieIdentifier);
  }
}
