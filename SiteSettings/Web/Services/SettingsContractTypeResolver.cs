// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Web.Services.SettingsContractTypeResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Configuration.Web.UI.Basic;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TrackingConsent.Configuration;

namespace Telerik.Sitefinity.SiteSettings.Web.Services
{
  /// <summary>
  /// This class provides a method for registering known product types for the ecommerce product service.
  /// </summary>
  public static class SettingsContractTypeResolver
  {
    private static WebServiceHost serviceHost;

    static SettingsContractTypeResolver() => SystemManager.ShuttingDown += new EventHandler<CancelEventArgs>(SettingsContractTypeResolver.SystemManager_ShuttingDown);

    private static void SystemManager_ShuttingDown(object sender, CancelEventArgs e) => SettingsContractTypeResolver.RestartServiceHost();

    internal static void RegisterServiceHost(WebServiceHost host) => SettingsContractTypeResolver.serviceHost = host;

    internal static void RestartServiceHost()
    {
      if (SettingsContractTypeResolver.serviceHost == null)
        return;
      SettingsContractTypeResolver.serviceHost.Close();
    }

    /// <summary>Registers the known types for Sitefinity ecommerce.</summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IEnumerable<Type> RegisterKnownTypes(
      ICustomAttributeProvider provider)
    {
      List<Type> typeList = new List<Type>();
      foreach (BasicSettingsRegistration settingsRegistration in SystemManager.GetBasicSettingsRegistrations().Where<BasicSettingsRegistration>((Func<BasicSettingsRegistration, bool>) (s => s.DataContractType != (Type) null)))
        typeList.Add(settingsRegistration.DataContractType);
      typeList.Add(typeof (TrackingConsentSettingsContract));
      return (IEnumerable<Type>) typeList;
    }
  }
}
