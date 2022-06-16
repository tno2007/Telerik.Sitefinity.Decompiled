// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Web.Services.BasicSettingsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.SiteSettings.Web.Services
{
  /// <summary>WCF Rest service for basic settings.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class BasicSettingsService : IBasicSettingsService
  {
    internal const string WebServiceUrl = "Sitefinity/Services/BasicSettings.svc";

    /// <inheritdoc />
    public SettingsItemContext GetSettings(string itemType, string siteId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type type = itemType != null ? TypeResolutionService.ResolveType(itemType) : throw new ArgumentNullException(nameof (itemType));
      if (!typeof (ISettingsDataContract).IsAssignableFrom(type))
        throw new Exception("The settings type specified by 'itemType' parameter must implement 'ISettingsDataContract' interface");
      bool inherit = false;
      ISettingsDataContract settingsDataContract = (ISettingsDataContract) null;
      if (!string.IsNullOrEmpty(siteId) && SiteSettingsManager.HasSetting(type, "sitepolicy", siteId))
        settingsDataContract = (ISettingsDataContract) SiteSettingsManager.GetSetting(type, "sitepolicy", siteId);
      if (settingsDataContract == null)
      {
        settingsDataContract = (ISettingsDataContract) Activator.CreateInstance(type);
        settingsDataContract.LoadDefaults(true);
        inherit = true;
      }
      return new SettingsItemContext(settingsDataContract, inherit);
    }

    /// <inheritdoc />
    public void SaveSettings(
      SettingsItemContext context,
      string key,
      string itemType,
      string siteId,
      string inheritanceState)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (itemType == null)
        throw new ArgumentNullException(nameof (itemType));
      ISettingsDataContract settingObject = context.Item;
      Type type = settingObject.GetType();
      if (!string.IsNullOrEmpty(siteId))
      {
        if (!string.IsNullOrEmpty(inheritanceState) && inheritanceState == "inherit")
          SiteSettingsManager.DeleteSetting(type, "sitepolicy", siteId);
        else
          SiteSettingsManager.SaveSetting(type, "sitepolicy", siteId, (object) settingObject);
      }
      else
      {
        using (new FileSystemModeRegion())
          settingObject.SaveDefaults();
      }
    }
  }
}
