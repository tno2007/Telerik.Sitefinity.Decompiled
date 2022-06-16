// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Services.TwitterUrlShortConfigService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Basic;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UrlShorteners;

namespace Telerik.Sitefinity.Publishing.Twitter.Services
{
  [ServiceContract]
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class TwitterUrlShortConfigService
  {
    [WebHelp(Comment = "Get the twitter url shortenning settings.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "twitter/")]
    [OperationContract]
    public CollectionContext<UrlShorteningSettingsModel> GetUrlShorteningBasicSettings()
    {
      ServiceUtility.RequestBackendUserAuthentication();
      IEnumerable<UrlShorteningSettingsModel> shorteningSettingsModels = Config.Get<SystemConfig>().UrlShortenerSettings.Values.Where<UrlShortenerElement>((Func<UrlShortenerElement, bool>) (setting => setting.ShortenerServiceUrl == "http://api.bit.ly/v3/shorten?login={0}&apiKey={1}&longUrl={2}&format=xml")).Select<UrlShortenerElement, UrlShorteningSettingsModel>((Func<UrlShortenerElement, UrlShorteningSettingsModel>) (setting => new UrlShorteningSettingsModel(setting)));
      ServiceUtility.DisableCache();
      return new CollectionContext<UrlShorteningSettingsModel>(shorteningSettingsModels)
      {
        TotalCount = shorteningSettingsModels.Count<UrlShorteningSettingsModel>()
      };
    }

    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "twitter/")]
    public void SaveAcc(
      DataItemContext<UrlShorteningSettingsModel> applicationContext)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ConfigManager manager = ConfigManager.GetManager();
      SystemConfig section = manager.GetSection<SystemConfig>();
      UrlShortenerElement shortenerElement = section.UrlShortenerSettings.Values.Where<UrlShortenerElement>((Func<UrlShortenerElement, bool>) (setting => setting.ShortenerServiceUrl == applicationContext.Item.ShortenningServiceUrl)).FirstOrDefault<UrlShortenerElement>();
      shortenerElement.Parameters["apiKey"] = applicationContext.Item.ApiKey;
      shortenerElement.Parameters["login"] = applicationContext.Item.AccountName;
      manager.SaveSection((ConfigSection) section, true);
    }
  }
}
