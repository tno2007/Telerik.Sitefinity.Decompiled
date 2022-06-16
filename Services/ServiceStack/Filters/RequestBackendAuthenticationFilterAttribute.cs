// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.Filters.RequestBackendAuthenticationFilterAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.ServiceStack.Filters
{
  /// <summary>
  /// Adding this filter to your ServiceStack service class this will request backend authentication while calling the service.
  /// </summary>
  public class RequestBackendAuthenticationFilterAttribute : RequestFilterAttribute
  {
    private bool needAdminRights;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.ServiceStack.Filters.RequestBackendAuthenticationFilterAttribute" /> class.
    /// </summary>
    /// <param name="needAdminRights">if set to <c>true</c> [requires admin rights].</param>
    public RequestBackendAuthenticationFilterAttribute(bool needAdminRights = false) => this.needAdminRights = needAdminRights;

    /// <summary>
    /// This method is only executed if the HTTP method matches the <see cref="P:ServiceStack.RequestFilterAttribute.ApplyTo" /> property.
    /// </summary>
    /// <param name="req">The http request wrapper</param>
    /// <param name="res">The http response wrapper</param>
    /// <param name="requestDto">The request DTO</param>
    public override void Execute(IRequest req, IResponse res, object requestDto) => ServiceUtility.RequestAuthentication(this.needAdminRights, true);
  }
}
