// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.Filters.RequestAdministrationAuthenticationFilterAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.ServiceStack.Filters
{
  /// <summary>
  /// Adding this filter to your ServiceStack service class this will request administration authentication while calling the service.
  /// </summary>
  public class RequestAdministrationAuthenticationFilterAttribute : RequestFilterAttribute
  {
    /// <inheritdoc />
    public override void Execute(IRequest req, IResponse res, object requestDto) => ServiceUtility.RequestAuthentication(true, true);
  }
}
