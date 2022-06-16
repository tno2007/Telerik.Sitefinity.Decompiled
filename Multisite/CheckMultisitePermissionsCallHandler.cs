// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.CheckMultisitePermissionsCallHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;

namespace Telerik.Sitefinity.Multisite
{
  public class CheckMultisitePermissionsCallHandler : ISitefinityCallHanlder
  {
    public void Invoke(EventHandlerArgs args)
    {
      if (args.Item == null || !(args.Item is Site))
        return;
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity != null && currentIdentity.IsUnrestricted)
        return;
      Site securedObject = (Site) args.Item;
      if (securedObject.Provider is DataProviderBase provider && provider.SuppressSecurityChecks)
        return;
      string propertyName = args.PropertyName;
      if (!(propertyName == "isOffline"))
      {
        if (!(propertyName == "siteDataSourceLinks"))
          return;
        securedObject.Demand("Site", "ConfigureModules");
      }
      else
        securedObject.Demand("Site", "StartStopSite");
    }
  }
}
