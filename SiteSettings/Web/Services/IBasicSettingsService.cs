// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Web.Services.IBasicSettingsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.SiteSettings.Web.Services
{
  /// <summary>
  /// The WCF web service that is used to work with basic settings data contract items.
  /// </summary>
  [ServiceContract(Namespace = "SettingsService")]
  [AllowDynamicSettings]
  [ServiceKnownType("RegisterKnownTypes", typeof (SettingsContractTypeResolver))]
  public interface IBasicSettingsService
  {
    /// <summary>Gets the settings.</summary>
    /// <param name="contractTypeName">Name of the contract type.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "generic/?itemType={itemType}&siteId={siteId}")]
    SettingsItemContext GetSettings(string itemType, string siteId);

    /// <summary>Saves the settings.</summary>
    /// <param name="context">The context.</param>
    /// <param name="key">The key.</param>
    /// <param name="contractTypeName">Name of the contract type.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="inheritanceState">Determines if the inheritance should be broken or derived from the application level.
    /// (e.g. "inherit", "break")</param>
    [WebHelp(Comment = "Saves the settings generic.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "generic/{key}/?itemType={itemType}&siteId={siteId}&inheritanceState={inheritanceState}")]
    [OperationContract]
    void SaveSettings(
      SettingsItemContext context,
      string key,
      string itemType,
      string siteId,
      string inheritanceState);
  }
}
