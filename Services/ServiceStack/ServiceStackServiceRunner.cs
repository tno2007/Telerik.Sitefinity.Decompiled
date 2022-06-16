// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.ServiceStackServiceRunner`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Host;
using ServiceStack.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.ServiceStack
{
  /// <summary>Service stack service runner</summary>
  /// <typeparam name="TRequest">The request type</typeparam>
  public class ServiceStackServiceRunner<TRequest> : ServiceRunner<TRequest>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.ServiceStack.ServiceStackServiceRunner`1" /> class.
    /// </summary>
    /// <param name="appHost">The app host</param>
    /// <param name="actionContext">The action context</param>
    public ServiceStackServiceRunner(IAppHost appHost, ActionContext actionContext)
      : base(appHost, actionContext)
    {
    }

    /// <inheritdoc />
    public override void OnBeforeExecute(IRequest req, TRequest request)
    {
      LocalizationBehavior.ApplyLocalizationBehaviorFromCurrentRequest();
      base.OnBeforeExecute(req, request);
    }
  }
}
