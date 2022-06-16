// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.Services.CacheProfiles.CacheProfilesService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Configuration.Web.ViewModels;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Configuration.Web.Services.CacheProfiles
{
  /// <summary>
  /// This service provides endpoints for performing CRUD operations over Sitefinity cache profiles.
  /// These profiles include for both pages and media, i.e. output (server) cache and client (browser) cache profiles.
  /// </summary>
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [ServiceContract]
  internal class CacheProfilesService
  {
    internal const string Url = "Sitefinity/Services/CacheProfiles/Settings.svc";
    private const string CacheProfileNotFoundMessage = "Cache profile was not found.";
    private const int DefaultItemMaxSize = 500;
    private readonly CacheProfilesService.ICacheSettingsProvider cacheSettings;
    private readonly ConfigDescriptions labels;
    private readonly CacheProfilesService.ISecurityValidator security;
    private static readonly IDictionary<string, string> ItemTypeDefaultProfiles = (IDictionary<string, string>) new Dictionary<string, string>()
    {
      {
        "Image",
        "defaultClientProfile"
      },
      {
        "Document",
        "defaultDocumentProfile"
      },
      {
        "Video",
        "defaultVideoProfile"
      },
      {
        "Page",
        "defaultProfile"
      }
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.Services.CacheProfiles.CacheProfilesService" /> class.
    /// </summary>
    public CacheProfilesService()
      : this((CacheProfilesService.ICacheSettingsProvider) new CacheProfilesService.CacheSettingsProvider(), (CacheProfilesService.ISecurityValidator) new CacheProfilesService.AdminSecurityValidator(), Res.Get<ConfigDescriptions>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.Services.CacheProfiles.CacheProfilesService" /> class.
    /// </summary>
    /// <param name="cacheSettings">The cache settings.</param>
    /// <param name="security">The security validator.</param>
    /// <param name="labels">The labels.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// cacheSettings
    /// or
    /// security
    /// or
    /// labels
    /// </exception>
    public CacheProfilesService(
      CacheProfilesService.ICacheSettingsProvider cacheSettings,
      CacheProfilesService.ISecurityValidator security,
      ConfigDescriptions labels)
    {
      if (cacheSettings == null)
        throw new ArgumentNullException(nameof (cacheSettings));
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      if (labels == null)
        throw new ArgumentNullException(nameof (labels));
      this.cacheSettings = cacheSettings;
      this.security = security;
      this.labels = labels;
    }

    /// <summary>Gets all page cache profiles.</summary>
    /// <returns>The page cache profile grid view models.</returns>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "page")]
    [OperationContract]
    public CacheProfileGridViewModel[] PageProfiles()
    {
      this.security.Validate();
      ServiceUtility.DisableCache();
      OutputCacheElement cacheSettings = this.cacheSettings.Get();
      string defaultProfile = cacheSettings.DefaultProfile;
      IDictionary<string, string> defaults = this.GetDefaultProfilesPerType(CacheProfileViewModel.PageCacheItemTypes, cacheSettings);
      return cacheSettings.Profiles.Values.Select<OutputCacheProfileElement, CacheProfileGridViewModel>((Func<OutputCacheProfileElement, CacheProfileGridViewModel>) (p => this.CreateGridViewModel(p, defaults))).ToArray<CacheProfileGridViewModel>();
    }

    /// <summary>Gets the page cache profile.</summary>
    /// <param name="name">The cache profile name.</param>
    /// <returns>The cache profile view model.</returns>
    /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">Cache profile does not exist.</exception>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "page/{name}")]
    [OperationContract]
    public CacheProfileViewModel PageProfile(string name)
    {
      this.security.Validate();
      ServiceUtility.DisableCache();
      ConfigElementDictionary<string, OutputCacheProfileElement> profiles = this.cacheSettings.Get().Profiles;
      if (!profiles.ContainsKey(name))
        throw new ItemNotFoundException("Cache profile was not found.");
      return this.CreateViewModel(profiles[name], CacheProfileViewModel.PageCacheItemTypes);
    }

    /// <summary>Deletes the page cache profile.</summary>
    /// <param name="name">The cache profile name.</param>
    /// <returns>The result from the cache profile deletion.</returns>
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "page/{name}")]
    [OperationContract]
    public CacheProfileOperationResponse DeletePageProfile(string name)
    {
      this.security.Validate();
      CacheProfileOperationResponse result = new CacheProfileOperationResponse();
      this.cacheSettings.Update((Func<OutputCacheElement, bool>) (c =>
      {
        if (c.DefaultProfile == name)
        {
          result.Success = false;
          result.Message = this.labels.CacheProfileIsDefault.Arrange((object) name);
          return false;
        }
        result.Success = c.Profiles.Remove(name);
        if (result.Success)
          return true;
        result.Message = this.labels.CacheProfileNotFound.Arrange((object) name);
        return false;
      }));
      return result;
    }

    /// <summary>Creates a new page cache profile.</summary>
    /// <param name="data">The cache profile data.</param>
    /// <returns>The result from the cache profile creation.</returns>
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "page")]
    [OperationContract]
    public CacheProfileOperationResponse CreatePageProfile(
      CacheProfileViewModel data)
    {
      this.security.Validate();
      CacheProfileOperationResponse result = new CacheProfileOperationResponse();
      this.cacheSettings.Update((Func<OutputCacheElement, bool>) (c =>
      {
        if (data.Name.IsNullOrEmpty())
        {
          result.Success = false;
          result.Message = this.labels.CacheProfileNameRequired;
          return false;
        }
        if (c.Profiles.ContainsKey(data.Name))
        {
          result.Success = false;
          result.Message = this.labels.CacheProfileAlreadyExists.Arrange((object) data.Name);
          return false;
        }
        OutputCacheProfileElement element = new OutputCacheProfileElement((ConfigElement) c.Profiles);
        this.UpdateElementFromViewModel(c, element, data);
        c.Profiles.Add(element);
        result.Success = true;
        return true;
      }));
      return result;
    }

    /// <summary>Updates the page cache profile.</summary>
    /// <param name="name">The cache profile name.</param>
    /// <param name="data">The cache profile data.</param>
    /// <returns>The result from the cache profile update.</returns>
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "page/{name}")]
    [OperationContract]
    public CacheProfileOperationResponse UpdatePageProfile(
      string name,
      CacheProfileViewModel data)
    {
      this.security.Validate();
      CacheProfileOperationResponse result = new CacheProfileOperationResponse();
      this.cacheSettings.Update((Func<OutputCacheElement, bool>) (c =>
      {
        if (name != data.Name)
        {
          if (data.Name.IsNullOrEmpty())
          {
            result.Success = false;
            result.Message = this.labels.CacheProfileNameRequired;
            return false;
          }
          if (c.Profiles.ContainsKey(data.Name))
          {
            result.Success = false;
            result.Message = this.labels.CacheProfileAlreadyExists.Arrange((object) data.Name);
            return false;
          }
        }
        OutputCacheProfileElement element;
        result.Success = c.Profiles.TryGetValue(name, out element);
        if (result.Success)
        {
          int num = name != data.Name ? 1 : 0;
          if (num != 0)
            c.Profiles.Remove(element);
          this.UpdateElementFromViewModel(c, element, data);
          if (num != 0)
            c.Profiles.Add(element);
          if (c.DefaultProfile.IsNullOrEmpty() || !c.Profiles.ContainsKey(c.DefaultProfile))
          {
            OutputCacheProfileElement cacheProfileElement = c.Profiles.Values.FirstOrDefault<OutputCacheProfileElement>((Func<OutputCacheProfileElement, bool>) (p => p.Name != data.Name));
            c.DefaultProfile = cacheProfileElement != null ? cacheProfileElement.Name : data.Name;
          }
        }
        else
          result.Message = this.labels.CacheProfileNotFound.Arrange((object) name);
        return result.Success;
      }));
      return result;
    }

    /// <summary>Gets all media cache profiles.</summary>
    /// <returns>The cache profile grid view models.</returns>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "media")]
    [OperationContract]
    public CacheProfileGridViewModel[] MediaProfiles()
    {
      this.security.Validate();
      ServiceUtility.DisableCache();
      OutputCacheElement cacheSettings = this.cacheSettings.Get();
      IDictionary<string, string> defaults = this.GetDefaultProfilesPerType(CacheProfileViewModel.MediaCacheItemTypes, cacheSettings);
      return cacheSettings.MediaCacheProfiles.Values.Select<OutputCacheProfileElement, CacheProfileGridViewModel>((Func<OutputCacheProfileElement, CacheProfileGridViewModel>) (p => this.CreateGridViewModel(p, defaults))).ToArray<CacheProfileGridViewModel>();
    }

    /// <summary>Gets the media cache profile.</summary>
    /// <param name="name">The cache profile name.</param>
    /// <returns>The cache profile view model.</returns>
    /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">The cache profile was not found.</exception>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "media/{name}")]
    [OperationContract]
    public CacheProfileViewModel MediaProfile(string name)
    {
      this.security.Validate();
      ServiceUtility.DisableCache();
      ConfigElementDictionary<string, OutputCacheProfileElement> mediaCacheProfiles = this.cacheSettings.Get().MediaCacheProfiles;
      if (!mediaCacheProfiles.ContainsKey(name))
        throw new ItemNotFoundException("Cache profile was not found.");
      return this.CreateViewModel(mediaCacheProfiles[name], CacheProfileViewModel.MediaCacheItemTypes);
    }

    /// <summary>Deletes the media cache profile.</summary>
    /// <param name="name">The cache profile name.</param>
    /// <returns>The result from the cache profile deletion.</returns>
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "media/{name}")]
    [OperationContract]
    public CacheProfileOperationResponse DeleteMediaProfile(string name)
    {
      this.security.Validate();
      CacheProfileOperationResponse result = new CacheProfileOperationResponse();
      this.cacheSettings.Update((Func<OutputCacheElement, bool>) (c =>
      {
        if (c.DefaultImageProfile == name)
        {
          result.Success = false;
          result.Message = this.labels.CacheProfileIsDefault.Arrange((object) name);
          return false;
        }
        result.Success = c.MediaCacheProfiles.Remove(name);
        if (result.Success)
          return true;
        result.Message = this.labels.CacheProfileNotFound.Arrange((object) name);
        return false;
      }));
      return result;
    }

    /// <summary>Creates a new media cache profile.</summary>
    /// <param name="data">The cache profile data.</param>
    /// <returns>The result from the cache profile creation.</returns>
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "media")]
    [OperationContract]
    public CacheProfileOperationResponse CreateMediaProfile(
      CacheProfileViewModel data)
    {
      this.security.Validate();
      CacheProfileOperationResponse result = new CacheProfileOperationResponse();
      this.cacheSettings.Update((Func<OutputCacheElement, bool>) (c =>
      {
        if (data.Name.IsNullOrEmpty())
        {
          result.Success = false;
          result.Message = this.labels.CacheProfileNameRequired;
          return false;
        }
        if (c.MediaCacheProfiles.ContainsKey(data.Name))
        {
          result.Success = false;
          result.Message = this.labels.CacheProfileAlreadyExists.Arrange((object) data.Name);
          return false;
        }
        OutputCacheProfileElement element = new OutputCacheProfileElement((ConfigElement) c.MediaCacheProfiles);
        element.Enabled = true;
        this.UpdateElementFromViewModel(c, element, data);
        c.MediaCacheProfiles.Add(element);
        result.Success = true;
        return true;
      }));
      return result;
    }

    /// <summary>Updates the media cache profile.</summary>
    /// <param name="name">The cache profile name.</param>
    /// <param name="data">The cache profile data.</param>
    /// <returns>The result from the cache profile update.</returns>
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "media/{name}")]
    [OperationContract]
    public CacheProfileOperationResponse UpdateMediaProfile(
      string name,
      CacheProfileViewModel data)
    {
      this.security.Validate();
      CacheProfileOperationResponse result = new CacheProfileOperationResponse();
      this.cacheSettings.Update((Func<OutputCacheElement, bool>) (c =>
      {
        if (name != data.Name)
        {
          if (data.Name.IsNullOrEmpty())
          {
            result.Success = false;
            result.Message = this.labels.CacheProfileNameRequired;
            return false;
          }
          if (c.MediaCacheProfiles.ContainsKey(data.Name))
          {
            result.Success = false;
            result.Message = this.labels.CacheProfileAlreadyExists.Arrange((object) data.Name);
            return false;
          }
        }
        OutputCacheProfileElement element;
        result.Success = c.MediaCacheProfiles.TryGetValue(name, out element);
        if (result.Success)
        {
          int num = name != data.Name ? 1 : 0;
          if (num != 0)
            c.MediaCacheProfiles.Remove(element);
          this.UpdateElementFromViewModel(c, element, data);
          if (num != 0)
            c.MediaCacheProfiles.Add(element);
          if (c.DefaultImageProfile.IsNullOrEmpty() || !c.MediaCacheProfiles.ContainsKey(c.DefaultImageProfile))
          {
            OutputCacheProfileElement cacheProfileElement = c.MediaCacheProfiles.Values.FirstOrDefault<OutputCacheProfileElement>((Func<OutputCacheProfileElement, bool>) (p => p.Name != data.Name));
            c.DefaultImageProfile = cacheProfileElement != null ? cacheProfileElement.Name : data.Name;
          }
          if (c.DefaultDocumentProfile.IsNullOrEmpty() || !c.MediaCacheProfiles.ContainsKey(c.DefaultDocumentProfile))
          {
            OutputCacheProfileElement cacheProfileElement = c.MediaCacheProfiles.Values.FirstOrDefault<OutputCacheProfileElement>((Func<OutputCacheProfileElement, bool>) (p => p.Name != data.Name));
            c.DefaultDocumentProfile = cacheProfileElement != null ? cacheProfileElement.Name : data.Name;
          }
          if (c.DefaultVideoProfile.IsNullOrEmpty() || !c.MediaCacheProfiles.ContainsKey(c.DefaultVideoProfile))
          {
            OutputCacheProfileElement cacheProfileElement = c.MediaCacheProfiles.Values.FirstOrDefault<OutputCacheProfileElement>((Func<OutputCacheProfileElement, bool>) (p => p.Name != data.Name));
            c.DefaultVideoProfile = cacheProfileElement != null ? cacheProfileElement.Name : data.Name;
          }
        }
        else
          result.Message = this.labels.CacheProfileNotFound.Arrange((object) name);
        return result.Success;
      }));
      return result;
    }

    private CacheProfileViewModel CreateViewModel(
      OutputCacheProfileElement element,
      IEnumerable<string> availableTypes)
    {
      CacheProfileViewModel viewModel = new CacheProfileViewModel(element, availableTypes);
      List<string> stringList = new List<string>();
      OutputCacheElement outputCacheElement = this.cacheSettings.Get();
      foreach (string availableType in availableTypes)
      {
        string typeDefaultProfile = CacheProfilesService.ItemTypeDefaultProfiles[availableType];
        string str = outputCacheElement[typeDefaultProfile].ToString();
        if (element.Name == str)
          stringList.Add(availableType);
      }
      viewModel.DefaultItemTypes = (IEnumerable<string>) stringList;
      return viewModel;
    }

    private IDictionary<string, string> GetDefaultProfilesPerType(
      IEnumerable<string> availableTypes,
      OutputCacheElement cacheSettings)
    {
      return (IDictionary<string, string>) CacheProfilesService.ItemTypeDefaultProfiles.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => availableTypes.Contains<string>(x.Key))).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key), (Func<KeyValuePair<string, string>, string>) (x => cacheSettings[x.Value].ToString()));
    }

    private CacheProfileGridViewModel CreateGridViewModel(
      OutputCacheProfileElement element,
      IDictionary<string, string> defaultProfiles)
    {
      string str1 = string.Empty;
      IEnumerable<KeyValuePair<string, string>> source = defaultProfiles.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Value == element.Name));
      if (source.Count<KeyValuePair<string, string>>() > 0)
      {
        string str2;
        if (defaultProfiles.Count<KeyValuePair<string, string>>() <= 1)
          str2 = this.labels.Default;
        else
          str2 = this.labels.DefaultFor.Arrange((object) string.Join(", ", source.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (p => p.Key))));
        str1 = str2;
      }
      return new CacheProfileGridViewModel()
      {
        Name = element.Name,
        HttpHeader = this.CacheHeaderPreview(element),
        DefaultProfile = str1,
        Location = this.labels[element.Location.ToString()]
      };
    }

    private void UpdateElementFromViewModel(
      OutputCacheElement root,
      OutputCacheProfileElement element,
      CacheProfileViewModel viewModel,
      bool isMedia = false)
    {
      element.Name = viewModel.Name;
      OutputCacheLocation result;
      if (Enum.TryParse<OutputCacheLocation>(viewModel.Location, out result))
        element.Location = result;
      element.Duration = viewModel.MaxAge;
      OutputCacheProfileElement cacheProfileElement1 = element;
      int? nullable1 = viewModel.BrowserMaxAge;
      int? nullable2 = new int?(nullable1.GetValueOrDefault(172800));
      cacheProfileElement1.ClientMaxAge = nullable2;
      element.ProxyMaxAge = viewModel.ProxyCdnMaxAge;
      element.SlidingExpiration = viewModel.SlidingExpiration;
      OutputCacheProfileElement cacheProfileElement2 = element;
      nullable1 = viewModel.ItemMaxSize;
      int valueOrDefault = nullable1.GetValueOrDefault(500);
      cacheProfileElement2.MaxSize = valueOrDefault;
      foreach (string availableItemType in viewModel.AvailableItemTypes)
      {
        string typeDefaultProfile = CacheProfilesService.ItemTypeDefaultProfiles[availableItemType];
        if (viewModel.DefaultItemTypes.Contains<string>(availableItemType))
          root[typeDefaultProfile] = (object) element.Name;
        else if (root[typeDefaultProfile].ToString() == element.Name)
          root[typeDefaultProfile] = (object) null;
      }
    }

    private string CacheHeaderPreview(OutputCacheProfileElement element)
    {
      string controlHeaderValue = element.ToClientCacheControl().ToHttpCacheControlHeaderValue();
      return controlHeaderValue != null ? "Cache-Control" + ": " + controlHeaderValue : (string) null;
    }

    internal static class TypeNames
    {
      internal const string Image = "Image";
      internal const string Document = "Document";
      internal const string Video = "Video";
      internal const string Page = "Page";
    }

    public interface ICacheSettingsProvider
    {
      OutputCacheElement Get();

      void Update(Func<OutputCacheElement, bool> actionOnElement);
    }

    private class CacheSettingsProvider : CacheProfilesService.ICacheSettingsProvider
    {
      public OutputCacheElement Get() => Config.Get<SystemConfig>().CacheSettings;

      public void Update(Func<OutputCacheElement, bool> actionOnElement)
      {
        ConfigManager manager = ConfigManager.GetManager();
        SystemConfig section = manager.GetSection<SystemConfig>();
        if (!actionOnElement(section.CacheSettings))
          return;
        manager.SaveSection((ConfigSection) section, true);
      }
    }

    public interface ISecurityValidator
    {
      void Validate();
    }

    private class AdminSecurityValidator : CacheProfilesService.ISecurityValidator
    {
      public void Validate() => ServiceUtility.RequestAuthentication(true);
    }
  }
}
