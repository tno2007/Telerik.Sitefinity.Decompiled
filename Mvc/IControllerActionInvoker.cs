// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Mvc.IControllerActionInvoker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Mvc.Proxy;

namespace Telerik.Sitefinity.Mvc
{
  /// <summary>
  /// Classes that implement this interface provide functionality for delegating request processing to an action of a MVC controller.
  /// </summary>
  public interface IControllerActionInvoker
  {
    /// <summary>
    /// Invokes an action of the specified <paramref name="proxyControl" /> MVC controller and outputs the resulting html. A return value indicates whether the invocation succeeded.
    /// </summary>
    /// <param name="proxyControl">A Web Forms control used as a proxy that contains the information regarding the associated MVC controller.</param>
    /// <param name="htmlOutput"> When this method returns, contains the html value equivalent to the response returned by the invoked action if the invocation succeeded,
    /// or null if the invocation failed.</param>
    /// <returns><c>true</c> if an action was invoked  successfully; otherwise, <c>false</c>. </returns>
    bool TryInvokeAction(MvcProxyBase proxyControl, out string htmlOutput);
  }
}
