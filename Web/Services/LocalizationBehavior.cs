// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.LocalizationBehavior
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Behaviour that checks the http request headers for specified cutltures (UI culture or Culture) and applies it to the current thread on all Operations invokings.
  /// This way we can assure that the strings are for the required culture
  /// </summary>
  public class LocalizationBehavior : IEndpointBehavior, ICallContextInitializer
  {
    internal const string CultureHeaderKey = "SF_CULTURE";
    internal const string UiCultureHeaderKey = "SF_UI_CULTURE";
    private const string RequestFallbackMode = "SF_FALLBACK_MODE";

    /// <summary>Implement to confirm that the endpoint meets some intended criteria.
    /// </summary>
    /// <param name="endpoint">The endpoint to validate.</param>
    public void Validate(ServiceEndpoint endpoint)
    {
    }

    /// <summary>Implement to pass data at runtime to bindings to support custom behavior.
    /// </summary>
    /// <param name="endpoint">The endpoint to modify.</param>
    /// <param name="bindingParameters">The objects that binding elements require to
    /// support the behavior.</param>
    public void AddBindingParameters(
      ServiceEndpoint endpoint,
      BindingParameterCollection bindingParameters)
    {
    }

    /// <summary>Implements a modification or extension of the service across an endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint that exposes the contract.</param>
    /// <param name="endpointDispatcher">The endpoint dispatcher to be modified or extended.
    /// </param>
    public void ApplyDispatchBehavior(
      ServiceEndpoint endpoint,
      EndpointDispatcher endpointDispatcher)
    {
      foreach (DispatchOperation operation in (SynchronizedCollection<DispatchOperation>) endpointDispatcher.DispatchRuntime.Operations)
        operation.CallContextInitializers.Add((ICallContextInitializer) this);
    }

    /// <summary>Implements a modification or extension of the client across an endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint that is to be customized.</param>
    /// <param name="clientRuntime">The client runtime to be customized.</param>
    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
    }

    public object BeforeInvoke(
      InstanceContext instanceContext,
      IClientChannel channel,
      Message message)
    {
      object obj;
      if (!message.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
        Telerik.Sitefinity.Abstractions.Log.Write((object) ("HttpRequestMessageProperty not found in the incoming request. Service local address: " + (object) channel.LocalAddress.Uri), TraceEventType.Critical);
      else
        LocalizationBehavior.ApplyLocalizationBehavior((NameValueCollection) (obj as HttpRequestMessageProperty).Headers);
      return (object) null;
    }

    public void AfterInvoke(object correlationState)
    {
    }

    /// <summary>
    /// Applies the localization thread settings based on the headers in the current request
    /// </summary>
    public static void ApplyLocalizationBehaviorFromCurrentRequest()
    {
      if (SystemManager.CurrentHttpContext == null)
        return;
      LocalizationBehavior.ApplyLocalizationBehavior(SystemManager.CurrentHttpContext.Request.Headers);
    }

    private static void ApplyLocalizationBehavior(NameValueCollection headers)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      string uiCulture = headers["SF_UI_CULTURE"] ?? headers["SF_CULTURE"];
      SystemManager.CurrentContext.Culture = string.IsNullOrEmpty(uiCulture) || !((IEnumerable<CultureInfo>) CultureInfo.GetCultures(CultureTypes.AllCultures)).Any<CultureInfo>((Func<CultureInfo, bool>) (c => c.Name.Equals(uiCulture))) ? appSettings.DefaultFrontendLanguage : CultureInfo.GetCultureInfo(uiCulture);
      FallbackMode result;
      if (Enum.TryParse<FallbackMode>(headers["SF_FALLBACK_MODE"], true, out result))
        SystemManager.RequestLanguageFallbackMode = result;
      SystemManager.CurrentContext.AllowConcurrentEditing = true;
    }
  }
}
