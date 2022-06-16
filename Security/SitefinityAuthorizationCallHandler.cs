// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SitefinityAuthorizationCallHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Threading;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Security;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// An <see cref="T:Telerik.Microsoft.Practices.Unity.InterceptionExtension.ICallHandler" /> that checks the Security Block for authorization
  /// before permitting the call to proceed to the target.
  /// </summary>
  public class SitefinityAuthorizationCallHandler : ICallHandler
  {
    private IAuthorizationProvider authorizationProvider;

    /// <summary>
    /// Implement this method to execute your handler processing.
    /// </summary>
    /// <param name="input">Inputs to the current call to the target.</param>
    /// <param name="getNext">Delegate to execute to get the next delegate in the handler
    /// chain.</param>
    /// <returns>Return value from the target.</returns>
    public IMethodReturn Invoke(
      IMethodInvocation input,
      GetNextHandlerDelegate getNext)
    {
      if (input.MethodBase.IsPublic && !input.MethodBase.IsSpecialName && (!(input.Target is DataProviderBase target) || !target.SuppressSecurityChecks) && !SystemManager.Initializing)
      {
        IAuthorizationProvider authorizationProvider = this.GetAuthorizationProvider();
        bool flag;
        if (authorizationProvider is AuthorizationPermissionProvider permissionProvider)
        {
          flag = permissionProvider.AuthorizeInput(input);
          if (flag)
          {
            IMethodReturn output = getNext()(input, getNext);
            flag = permissionProvider.AuthorizeOutput(input, output);
            if (flag)
              return output;
          }
        }
        else
        {
          ReplacementFormatter replacementFormatter = (ReplacementFormatter) new MethodInvocationFormatter(input);
          flag = authorizationProvider.Authorize(Thread.CurrentPrincipal, replacementFormatter.Format(this.Operation));
        }
        if (!flag)
        {
          string message = permissionProvider.DetailedErrorMessage;
          if (message.IsNullOrWhitespace())
            message = Res.Get<ErrorMessages>().AuthorizationFailed;
          UnauthorizedAccessException ex = new UnauthorizedAccessException(message);
          return input.CreateExceptionMethodReturn((Exception) ex);
        }
      }
      return getNext()(input, getNext);
    }

    private IAuthorizationProvider GetAuthorizationProvider()
    {
      if (this.authorizationProvider == null)
        this.authorizationProvider = ObjectFactory.Resolve<IAuthorizationProvider>();
      return this.authorizationProvider;
    }

    /// <summary>Gets or sets the operation.</summary>
    /// <value>The operation.</value>
    public string Operation { get; set; }

    /// <summary>Order in which the handler will be executed.</summary>
    /// <value></value>
    public int Order { get; set; }
  }
}
