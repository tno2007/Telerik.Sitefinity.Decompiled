// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.ConfigSectionItems
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text.RegularExpressions;
using System.Xml;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Configuration.Basic;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Newsletters.BasicSettings;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Basic;
using Telerik.Sitefinity.SiteSettings.Configuration;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI.ClientBinders;

namespace Telerik.Sitefinity.Configuration.Web
{
  /// <summary>WCF Rest service for config policies.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  [ServiceContract]
  public class ConfigSectionItems
  {
    private Dictionary<Type, bool> typeInfos;
    private static readonly Regex cultureFilterRegex = new Regex("[a-zA-Z0-9]+\\.Contains\\(\"(.*)\"\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public const string CollectionContextReadOnlyFlagKey = "isReadOnlyEnabled";
    public const string CollectionContextAutoStorageModeFlagKey = "isAutoStorageModeEnabled";
    public const string CollectionContextParametersCollectionFlagKey = "isParametersCollection";
    public const string CollectionContextParametersCollectionParentSaveLocationKey = "parametersCollectionParentSaveLocation";
    public const string CollectionContextCanBeMovedFlagKey = "canBeMoved";
    public const string ContextCanBeModifiedFlagKey = "canBeModified";
    public const string MigratedErrorAppDataRelativePath = "sitefinity/configuration/migrated.error";
    public const string MigratedSuccessAppDataRelativePath = "sitefinity/configuration/migrated.success";
    private const string AutoModeNotSetAfterMigrationMessage = "Auto configuration storage mode must be set in web.config after successful migration. Go to web.config and add:\r\n1. In <configuration><configSections><sectionGroup name=\"telerik\"> add the following tag: <section name = \"sitefinity\" type = \"Telerik.Sitefinity.Configuration.SectionHandler, Telerik.Sitefinity\" requirePermission = \"false\" />\r\n2. In <configuration><telerik><sitefinity> add the following tag: <sitefinityConfig storageMode=\"Auto\" restrictionLevel=\"Default\" />";
    private const string NoEncryptionKey = "None";
    internal const string MigrationNotSuccessfulMessage = "There was an error while the system was being migrated to Auto storage mode. You must restore Sitefinity to a pre-migration state. See Error.log for details.";
    internal static readonly string ConfigSectionDelimiter = "|".Base64Encode();
    private string TextEditorToolSetResult;

    /// <summary>Gets the details for a particular config section.</summary>
    /// <param name="nodeName">Node under which items will be populated.
    /// The node parameter is a string that is the full path from the config section root to the target config element.
    /// For elements in collection the node type name is followed by '_' concatenated with the value of the Key property of the element in the collection.
    /// e.x. for the path FakeConfigSection.FakeConfigCollection.FakeConfigElement the node string would be: 'FakeConfigElement_{the value of the key proeprty},FakeConfigCollection,FakeConfigSection'
    /// </param>
    /// <param name="policyHandlerName">Name of the policy handler.</param>
    /// <param name="policyName">Name of the policy.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="mode">Result options.</param>
    /// <param name="mode">The id of the site. Will load the defaults for all sites if empty guid.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Get config section elements.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "?nodeName={nodeName}&provider={provider}&mode={mode}&type={typeName}&siteId={siteId}")]
    [OperationContract]
    public CollectionContext<ConfigSectionItems.UISectionItem> GetConfigSetionItems(
      string nodeName,
      string provider,
      string mode,
      string typeName,
      Guid siteId)
    {
      this.DemandViewPermissions();
      if (!ClaimsManager.GetCurrentIdentity().IsGlobalUser && siteId == Guid.Empty)
        siteId = SystemManager.CurrentContext.CurrentSite.Id;
      SecurityManager.EnsureNonGlobalAdminHasSiteAccess(siteId);
      using (new ConfigSiteContextRegion(siteId))
      {
        Type type = (Type) null;
        if (!string.IsNullOrEmpty(typeName))
          type = TypeResolutionService.ResolveType(typeName);
        ConfigSectionItems.SectionContext context = new ConfigSectionItems.SectionContext()
        {
          NodePath = nodeName,
          Provider = provider,
          Mode = this.GetUIMode(mode),
          ElementType = type,
          CollectionElementTypes = (IEnumerable<Type>) null
        };
        IList<ConfigSectionItems.UISectionItem> uiSectionItemList = this.ProcessSection(context);
        this.SetUISectionItemsEditability(uiSectionItemList);
        CollectionContext<ConfigSectionItems.UISectionItem> configSetionItems = new CollectionContext<ConfigSectionItems.UISectionItem>((IEnumerable<ConfigSectionItems.UISectionItem>) uiSectionItemList)
        {
          TotalCount = uiSectionItemList.Count
        };
        configSetionItems.Context = (IDictionary<string, string>) new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(nodeName))
          configSetionItems.Context.Add("nodekey", nodeName);
        if (context.CollectionElementTypes != null)
          configSetionItems.Context.Add("collectionElementTypes", string.Join<Type>(",", context.CollectionElementTypes));
        bool flag = !SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile);
        if (flag)
          configSetionItems.Context.Add("isReadOnlyEnabled", "true");
        if (Config.ConfigStorageMode == ConfigStorageMode.Auto)
        {
          configSetionItems.Context.Add("isAutoStorageModeEnabled", "true");
          if (context.CanBeMoved && !flag)
            configSetionItems.Context.Add("canBeMoved", "true");
        }
        if (context.IsParametersCollection)
        {
          configSetionItems.Context.Add("isParametersCollection", "true");
          configSetionItems.Context.Add("parametersCollectionParentSaveLocation", context.ParametersCollectionParentSaveLocation.ToString());
        }
        if (this.GetUIMode(mode) == ConfigSectionItems.ConfigUIMode.New)
          configSetionItems.Context.Add("isNew", "true");
        if (context.CanBeModified)
          configSetionItems.Context.Add("canBeModified", "true");
        if (context.PersistsSiteSpecificValues)
          configSetionItems.Context.Add("persistsSiteSpecificValues", "true");
        ServiceUtility.DisableCache();
        return configSetionItems;
      }
    }

    /// <summary>Gets all nodes in the path of a particular setting.</summary>
    /// <param name="nodeName">Node under which items will be populated.
    /// The nodeName parameter is a string that is the full path from the config section root to the target config element.
    /// For elements in a collection, the collection node type name is followed by '_' concatenated with the value of the CollectionKey property of the element in the collection.
    /// e.x. for the path FakeConfigSection.FakeConfigCollection.FakeConfigElement the nodeName string would be: 'FakeConfigCollectionType_{the value of the CollectionKey property}fA==FakeConfigCollectionfA==FakeConfigSection'
    /// "fA==" is the Base64 encoded version of the "|" character.
    /// </param>
    /// <param name="policyHandlerName">Name of the policy handler.</param>
    /// <param name="policyName">Name of the policy.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="mode">Result options.</param>
    /// <returns>A collection of config elements that are in the full path of the requested setting.</returns>
    [WebHelp(Comment = "Get all nodes in the path of a setting.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "hierarchy/?nodeName={nodeName}&provider={provider}&mode={mode}&type={typeName}")]
    [OperationContract]
    public IEnumerable<CollectionContext<ConfigSectionItems.UISectionItem>> GetConfigElementFullPath(
      string nodeName,
      string provider,
      string mode,
      string typeName)
    {
      this.DemandViewPermissions();
      List<CollectionContext<ConfigSectionItems.UISectionItem>> configElementFullPath = new List<CollectionContext<ConfigSectionItems.UISectionItem>>();
      string[] nodes = this.GetNodes(nodeName);
      Stack<string> values = new Stack<string>();
      for (int index = 0; index < nodes.Length; ++index)
      {
        values.Push(nodes[index]);
        string nodeName1 = string.Join(ConfigSectionItems.ConfigSectionDelimiter, (IEnumerable<string>) values);
        string provider1 = provider;
        ConfigSectionItems.ConfigUIMode configUiMode = ConfigSectionItems.ConfigUIMode.Navigation;
        string mode1 = configUiMode.ToString();
        string typeName1 = typeName;
        Guid siteId1 = new Guid();
        CollectionContext<ConfigSectionItems.UISectionItem> configSetionItems1 = this.GetConfigSetionItems(nodeName1, provider1, mode1, typeName1, siteId1);
        configElementFullPath.Add(configSetionItems1);
        if (index == nodes.Length - 1)
        {
          string nodeName2 = string.Join(ConfigSectionItems.ConfigSectionDelimiter, (IEnumerable<string>) values);
          string provider2 = provider;
          configUiMode = ConfigSectionItems.ConfigUIMode.Form;
          string mode2 = configUiMode.ToString();
          string typeName2 = typeName;
          Guid siteId2 = new Guid();
          CollectionContext<ConfigSectionItems.UISectionItem> configSetionItems2 = this.GetConfigSetionItems(nodeName2, provider2, mode2, typeName2, siteId2);
          configElementFullPath.Add(configSetionItems2);
        }
      }
      return (IEnumerable<CollectionContext<ConfigSectionItems.UISectionItem>>) configElementFullPath;
    }

    /// <summary>Gets the details for a particular config section.</summary>
    /// <param name="propertyBag">Contains the key-value pair items.</param>
    /// <param name="nodeName">Node under which items will be populated</param>
    /// <param name="policyHandlerName">Name of the policy handler.</param>
    /// <param name="policyName">Name of the policy.</param>
    /// <param name="mode">Result options.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="siteId">The id of the site for which to save any site specific propertys` values.</param>
    /// <param name="inherit">If set to true will remove any site specific values for the siteId. If false will not remove any site specific values.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Saves the config section as a batch.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?nodeName={nodeName}&policyHandlerName={policyHandlerName}&policyName={policyName}&provider={provider}&mode={mode}&type={typeName}&siteId={siteId}&inherit={inherit}")]
    [OperationContract]
    public bool SaveBatchConfigSection(
      string[][] propertyBag,
      string nodeName,
      string policyHandlerName,
      string policyName,
      string provider,
      string mode,
      string typeName,
      Guid siteId,
      bool inherit)
    {
      this.DemandEditPermissions();
      SecurityManager.EnsureNonGlobalAdminHasSiteAccess(siteId);
      using (new ConfigSiteContextRegion(siteId))
      {
        Type elementType = (Type) null;
        if (!string.IsNullOrEmpty(typeName))
          elementType = TypeResolutionService.ResolveType(typeName);
        IList<IDictionary<string, string>> source = (IList<IDictionary<string, string>>) new List<IDictionary<string, string>>();
        int num = 0;
        string str1 = (string) null;
        for (int index = 0; index < propertyBag.GetLength(0); ++index)
        {
          if (str1 == null)
            str1 = propertyBag[index][0];
          else if (propertyBag[index][0] == str1)
            break;
          ++num;
        }
        for (int index1 = 0; index1 < propertyBag.GetLength(0); index1 += num)
        {
          Dictionary<string, string> properties = new Dictionary<string, string>();
          for (int index2 = 0; index2 < num; ++index2)
            this.AddNewProperty(index1 + index2, propertyBag, properties);
          source.Add((IDictionary<string, string>) properties);
        }
        try
        {
          KeyValuePair<string, string>[] nodePairs = this.GetNodePairs(nodeName);
          if (nodePairs.Length != 0)
          {
            ConfigManager manager = Config.GetManager(provider);
            ConfigSection section = this.GetSection(manager, nodePairs[0].Key, true);
            if (section == null)
              return false;
            object collection = (object) null;
            bool isNew = this.GetUIMode(mode) == ConfigSectionItems.ConfigUIMode.New;
            ConfigElement parentElement;
            object obj1 = this.GetTarget(nodePairs, section, isNew, ref collection, out parentElement, elementType);
            bool flag1 = false;
            if (obj1 != null)
            {
              if (collection != null && collection is NameValueCollection)
              {
                ConfigSectionItems.UINameValue uiNameValue = (ConfigSectionItems.UINameValue) obj1;
                IDictionary<string, string> dictionary1 = source.First<IDictionary<string, string>>();
                IDictionary<string, string> dictionary2 = source.Last<IDictionary<string, string>>();
                string str2 = dictionary1["Value"];
                string key = dictionary2["Value"];
                if (string.IsNullOrEmpty(str2) || key == null)
                  throw new Exception("Must provide a valid key and value");
                if (uiNameValue.IsNew && !string.IsNullOrEmpty(((NameValueCollection) collection)[str2]))
                  throw new Exception("Item with same key already defined.");
                ISecretDataResolver resolver1 = (ISecretDataResolver) null;
                string secretResolverName;
                if (dictionary2.TryGetValue("Encryption", out secretResolverName) && !secretResolverName.IsNullOrEmpty() && !secretResolverName.Equals("None", StringComparison.OrdinalIgnoreCase))
                  Config.TryGetSecretResolver(secretResolverName, out resolver1);
                if (resolver1 != null)
                {
                  string resolver2;
                  string unresolvedParameter = parentElement.GetUnresolvedParameter(str2, out resolver2);
                  if (resolver2 == null || resolver2 != resolver1.Name || key != unresolvedParameter)
                    key = resolver1.GenerateKey(key);
                }
                parentElement.SetParameter(str2, key, resolver1?.Name);
              }
              else
              {
                if (obj1 is ConfigSectionItems.UIConfigElement)
                {
                  isNew = ((ConfigSectionItems.UIConfigElement) obj1).IsNew;
                  obj1 = (object) ((ConfigSectionItems.UIConfigElement) obj1).Element;
                }
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj1);
                foreach (IDictionary<string, string> dictionary in (IEnumerable<IDictionary<string, string>>) source)
                {
                  PropertyDescriptor propertyDescriptor = properties[dictionary["Key"]];
                  if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly && propertyDescriptor.Converter.CanConvertFrom(typeof (string)))
                  {
                    string text = dictionary["Value"];
                    if (!text.IsNullOrEmpty() && obj1 is ConfigElement el)
                    {
                      el.PersistsSiteSpecificValues = siteId != Guid.Empty && !inherit;
                      ConfigProperty configProperty;
                      if (propertyDescriptor.Attributes[typeof (ConfigurationPropertyAttribute)] is ConfigurationPropertyAttribute attribute && el.Properties.TryGetValue(attribute.Name, out configProperty))
                      {
                        if (configProperty.IsSiteSpecific(el))
                          flag1 = true;
                        ISecretDataResolver resolver = (ISecretDataResolver) null;
                        string secretResolverName;
                        if (dictionary.TryGetValue("Encryption", out secretResolverName) && !secretResolverName.IsNullOrEmpty() && !secretResolverName.Equals("None", StringComparison.OrdinalIgnoreCase))
                          Config.TryGetSecretResolver(secretResolverName, out resolver);
                        if (el.GetRawValue(configProperty) is LazyValue rawValue && rawValue.Key.Equals(text))
                        {
                          SecretValue secretValue = rawValue as SecretValue;
                          if ((resolver != null || secretValue != null) && (resolver == null || secretValue == null || !(secretValue.ResolverName == resolver.Name)) && !(secretValue.ResolverName == "EnvVariables"))
                          {
                            if (secretValue != null && resolver == null)
                              el.SetRawValue(configProperty, (object) null);
                          }
                          else
                            continue;
                        }
                        if (resolver != null)
                        {
                          string key = resolver.GenerateKey(text);
                          el.SetRawValue(configProperty, (object) new SecretValue(key, configProperty, resolver.Name));
                          continue;
                        }
                      }
                    }
                    object obj2 = propertyDescriptor.Converter.ConvertFromInvariantString(text);
                    if (obj2 == null && !text.IsNullOrEmpty())
                      throw new NullReferenceException(string.Format("The value '{0}' for the property '{1}' cannot be converted to type '{2}'", (object) text, (object) propertyDescriptor.Name, (object) propertyDescriptor.PropertyType.FullName));
                    propertyDescriptor.SetValue(obj1, obj2);
                  }
                }
                if (isNew && collection is ConfigElementCollection)
                  ((ConfigElementCollection) collection).Add((ConfigElement) obj1);
              }
              ConfigElement configElement = obj1 as ConfigElement;
              bool flag2 = false;
              if (configElement != null)
              {
                configElement.Validate();
                section = configElement.Section;
                flag2 = configElement.Source == ConfigSource.Database;
              }
              else if (parentElement != null)
                flag2 = parentElement.Source == ConfigSource.Database;
              if (!flag1 && siteId != Guid.Empty)
                throw new NotSupportedException("Element cannot be persisted per site as it does not have any properties avbailable for persistence per site.");
              bool saveInFileSystemMode = !flag2 && (!flag1 || !(siteId != Guid.Empty));
              manager.SaveSection(section, saveInFileSystemMode);
              return true;
            }
          }
        }
        catch (Exception ex)
        {
          throw new WebProtocolException(HttpStatusCode.InternalServerError, SitefinityExtensions.RemoveCRLF(ex.Message), ex);
        }
      }
      return false;
    }

    /// <summary>Changes section save location.</summary>
    /// <param name="nodeName">Node which will be moved.</param>
    /// <returns>True when successfull, false on failure</returns>
    [WebHelp(Comment = "Changes section save location.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "move/?nodeName={nodeName}&targetLocation={targetLocation}&provider={provider}&policyHandlerName={policyHandlerName}&policyName={policyName}")]
    [OperationContract]
    public void MoveSection(
      string nodeName,
      int targetLocation,
      string provider,
      string policyHandlerName,
      string policyName)
    {
      this.DemandEditPermissions();
      try
      {
        KeyValuePair<string, string>[] nodePairs = this.GetNodePairs(nodeName);
        if (nodePairs.Length == 0)
          throw new InvalidOperationException("Invalid node name in request.");
        ConfigManager manager = Config.GetManager(provider);
        ConfigSection section = this.GetSection(manager, nodePairs[0].Key, true);
        if (section == null)
          throw new InvalidOperationException("Section not found.");
        object collection = (object) null;
        object obj = this.GetTarget(nodePairs, section, false, ref collection, out ConfigElement _);
        if (obj == null)
          throw new InvalidOperationException("Target config element not found.");
        if (collection != null && collection is NameValueCollection)
          throw new NotSupportedException("Cannot move config element parameters in NameValueCollections.");
        if (obj is ConfigSectionItems.UIConfigElement)
          obj = (object) ((ConfigSectionItems.UIConfigElement) obj).Element;
        ConfigElement element = obj as ConfigElement;
        if (element.Source == (ConfigSource) targetLocation)
          return;
        manager.MoveElement(element, (ConfigSource) targetLocation);
        SystemManager.RestartApplication("Config element moved.");
      }
      catch (InvalidOperationException ex)
      {
        throw new WebProtocolException(HttpStatusCode.BadRequest, ex.Message, (Exception) ex);
      }
      catch (NotSupportedException ex)
      {
        throw new WebProtocolException(HttpStatusCode.BadRequest, ex.Message, (Exception) ex);
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
    }

    /// <summary>
    ///  Deletes a policy item and returns true if the policy item has been deleted; otherwise false.
    ///  Result is returned in JSON format.
    /// </summary>
    /// <param name="nodeName">Name of the tree node</param>
    /// <param name="mode">result options.</param>
    /// <param name="provider">Current provider.</param>
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "?nodeName={nodeName}&policyHandlerName={policyHandlerName}&policyName={policyName}&provider={provider}&mode={mode}")]
    [OperationContract]
    public bool DeleteSection(
      string nodeName,
      string policyHandlerName,
      string policyName,
      string provider,
      string mode)
    {
      this.DemandEditPermissions();
      return this.DeleteSectionInternal(nodeName, policyHandlerName, policyName, provider, mode);
    }

    /// <summary>
    ///  Deletes a policy item and returns true if the policy item has been deleted; otherwise false.
    ///  Result is returned in XML format.
    /// </summary>
    /// <param name="nodeName">Name of the tree node</param>
    /// <param name="mode">result options.</param>
    /// <param name="provider">Current provider.</param>
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?nodeName={nodeName}&policyHandlerName={policyHandlerName}&policyName={policyName}&provider={provider}&mode={mode}")]
    [OperationContract]
    public bool DeleteSectionInXml(
      string nodeName,
      string policyHandlerName,
      string policyName,
      string provider,
      string mode)
    {
      this.DemandEditPermissions();
      return this.DeleteSectionInternal(nodeName, policyHandlerName, policyName, provider, mode);
    }

    /// <summary>Gets the comments basic settings.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Get the comments basic settings.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "comments/")]
    [OperationContract]
    public CommentsSettingsContract GetCommentsBasicSettings()
    {
      this.DemandViewPermissions();
      CommentsSettingsContract commentsBasicSettings = new CommentsSettingsContract();
      commentsBasicSettings.LoadDefaults(false);
      return commentsBasicSettings;
    }

    /// <summary>Saves the comments basic settings.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Saves the comments basic settings.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "comments/{key}/")]
    [OperationContract]
    public void SaveCommentsBasicSettings(
      ItemContext<CommentsSettingsContract> settings,
      string key)
    {
      this.DemandEditPermissions();
      settings.Item.SaveDefaults();
    }

    /// <summary>Gets the authentication mode.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Get the authentication mode.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "authentication/")]
    [OperationContract]
    public UserAuthenticationSettingsContract GetAuthenticationModeSettings()
    {
      this.DemandViewPermissions();
      UserAuthenticationSettingsContract authenticationModeSettings = new UserAuthenticationSettingsContract();
      authenticationModeSettings.LoadDefaults(false);
      return authenticationModeSettings;
    }

    /// <summary>Saves the authentication mode.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Saves the authentication mode.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "authentication/{key}/")]
    [OperationContract]
    public void SaveAuthenticationMode(
      ItemContext<UserAuthenticationSettingsContract> settings,
      string key)
    {
      this.DemandEditPermissions();
      settings.Item.SaveDefaults();
    }

    /// <summary>Gets the localization basic settings.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Get the localization basic settings.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "localization/?includeSitesNames={includeSitesNames}")]
    [OperationContract]
    public LocalizationSettingsModel GetLocalizationBasicSettings(
      bool includeSitesNames = false)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      LocalizationSettingsModel localizationBasicSettings = new LocalizationSettingsModel();
      localizationBasicSettings.Configure(ConfigManager.GetManager().GetSection<ResourcesConfig>(), includeSitesNames);
      return localizationBasicSettings;
    }

    /// <summary>Saves the localization basic settings.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Saves the localization basic settings.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "localization/{key}/")]
    [OperationContract]
    public void SaveLocalizationBasicSettings(
      ItemContext<LocalizationSettingsModel> settings,
      string key)
    {
      this.DemandEditPermissions();
      ConfigManager manager = ConfigManager.GetManager();
      ResourcesConfig section1 = manager.GetSection<ResourcesConfig>();
      List<string> second = section1.Cultures.Values.Select<CultureElement, string>((Func<CultureElement, string>) (c => c.Culture)).ToList<string>();
      if (second.Count == 1)
        second = new List<string>();
      settings.Item.Apply(ref section1);
      manager.SaveSection((ConfigSection) section1, true);
      if (!OpenAccessConnection.GetConnections().Where<OpenAccessConnection>((Func<OpenAccessConnection, bool>) (x => x.Name == "Sitefinity" && x.DbType == DatabaseType.MySQL)).Any<OpenAccessConnection>())
      {
        DataConfig section2 = manager.GetSection<DataConfig>();
        List<string> stringList = new List<string>();
        if (!section2.DatabaseMappingOptions.SplitTablesIgnoredCultures.IsNullOrEmpty())
        {
          string[] collection = section2.DatabaseMappingOptions.SplitTablesIgnoredCultures.Replace(" ", "").Split(new char[1]
          {
            ','
          }, StringSplitOptions.RemoveEmptyEntries);
          stringList.AddRange((IEnumerable<string>) collection);
        }
        if (stringList.Count < 5)
        {
          IEnumerable<string> strings = section1.Cultures.Values.Select<CultureElement, string>((Func<CultureElement, string>) (c => c.Culture)).Except<string>((IEnumerable<string>) second).Except<string>((IEnumerable<string>) stringList).Take<string>(5 - stringList.Count);
          if (strings.Any<string>())
          {
            stringList.AddRange(strings);
            section2.DatabaseMappingOptions.SplitTablesIgnoredCultures = string.Join(",", (IEnumerable<string>) stringList);
            manager.SaveSection((ConfigSection) section2, true);
          }
        }
      }
      SystemManager.RestartApplication(OperationReason.FromKey("LocalizationChange"), SystemRestartFlags.ResetModel);
    }

    /// <summary>Gets the predefined neutral cultures in Sitefinity.</summary>
    /// <param name="filter">The filter that shoud be applied to the cultures.</param>
    [WebHelp(Comment = "Retrieves all cultures predefined and user defined alike.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "languages/?filter={filter}&useGlobal={useGlobal}")]
    [OperationContract]
    public CollectionContext<CultureViewModel> GetLanguages(
      string filter,
      bool useGlobal = false)
    {
      this.DemandViewPermissions();
      if (useGlobal)
      {
        LocalizationSettingsModel localizationBasicSettings = this.GetLocalizationBasicSettings();
        return new CollectionContext<CultureViewModel>((IEnumerable<CultureViewModel>) localizationBasicSettings.Cultures)
        {
          TotalCount = ((IEnumerable<CultureViewModel>) localizationBasicSettings.Cultures).Count<CultureViewModel>()
        };
      }
      ConfigElementList<CultureElement> predefinedLanguages = Config.Get<CulturesConfig>().PredefinedLanguages;
      return this.GetCulturesInternal(filter, predefinedLanguages, false);
    }

    /// <summary>Gets the predefined specific cultures in Sitefinity.</summary>
    /// <param name="filter">The filter that shoud be applied to the cultures.</param>
    [WebHelp(Comment = "Retrieves all cultures predefined and user defined alike.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "cultures/?filter={filter}&useGlobal={useGlobal}")]
    [OperationContract]
    public CollectionContext<CultureViewModel> GetCultures(
      string filter,
      bool useGlobal = false)
    {
      this.DemandViewPermissions();
      if (useGlobal)
      {
        LocalizationSettingsModel localizationBasicSettings = this.GetLocalizationBasicSettings();
        foreach (CultureViewModel culture in localizationBasicSettings.Cultures)
          culture.ShowSpecificName = true;
        return new CollectionContext<CultureViewModel>((IEnumerable<CultureViewModel>) localizationBasicSettings.Cultures)
        {
          TotalCount = ((IEnumerable<CultureViewModel>) localizationBasicSettings.Cultures).Count<CultureViewModel>()
        };
      }
      ConfigElementList<CultureElement> predefinedCultures = Config.Get<CulturesConfig>().PredefinedCultures;
      return this.GetCulturesInternal(filter, predefinedCultures, true);
    }

    /// <summary>Gets the general basic settings.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Get the general basic settings.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "general/?siteId={siteId}")]
    [OperationContract]
    public GeneralSettingsModel GetGeneralBasicSettings(string siteId)
    {
      this.DemandViewPermissions();
      return new GeneralSettingsModel();
    }

    /// <summary>Saves the general basic settings.</summary>
    /// <param name="settings">The general settings.</param>
    /// <param name="key">The key.</param>
    [WebHelp(Comment = "Saves the general basic settings.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "general/{key}/")]
    [OperationContract]
    public void SaveGeneralBasicSettings(ItemContext<GeneralSettingsModel> settings, string key)
    {
      this.DemandEditPermissions();
      settings.Item.SaveChanges();
    }

    /// <summary>Gets the basic newsletters settings.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Gets the basic newsletters settings.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/newsletters/")]
    [OperationContract]
    public NewslettersSettingsContract GetNewslettersBasicSettings()
    {
      this.DemandViewPermissions();
      NewslettersSettingsContract newslettersBasicSettings = new NewslettersSettingsContract();
      newslettersBasicSettings.LoadDefaults(false);
      return newslettersBasicSettings;
    }

    /// <summary>Saves the basic newsletters settings.</summary>
    /// <param name="settings"></param>
    /// <param name="key"></param>
    [WebHelp(Comment = "Saves the basic newsletters settings.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/newsletters/{key}/")]
    [OperationContract]
    public void SaveNewslettersBasicSettings(
      ItemContext<NewslettersSettingsContract> settings,
      string key)
    {
      this.DemandEditPermissions();
      settings.Item.SaveDefaults();
    }

    /// <summary>Get a single text editor tool set.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Get a single text editor tool set.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "texteditortoolset/?toolSetName={toolSetName}")]
    [OperationContract]
    public CollectionContext<TextEditorToolsetViewModel> GetTextEditorToolset(
      string toolSetName)
    {
      this.DemandViewPermissions();
      List<TextEditorToolsetViewModel> items = new List<TextEditorToolsetViewModel>();
      string str;
      if (string.IsNullOrEmpty(toolSetName))
        str = this.GetDefaultResourceXml("Telerik.Sitefinity.Resources.Themes.StandardToolsFile.xml");
      else if (toolSetName == "Default tool set")
      {
        string resourceValue = Config.Get<AppearanceConfig>().StandardEditorConfiguration;
        if (string.IsNullOrEmpty(resourceValue))
          resourceValue = "Telerik.Sitefinity.Resources.Themes.StandardToolsFile.xml";
        str = this.GetDefaultResourceXml(resourceValue);
      }
      else if (toolSetName == "Tool set for comments")
      {
        string resourceValue = Config.Get<AppearanceConfig>().MinimalEditorConfiguration;
        if (string.IsNullOrEmpty(resourceValue))
          resourceValue = "Telerik.Sitefinity.Resources.Themes.MinimalToolsFile.xml";
        str = this.GetDefaultResourceXml(resourceValue);
      }
      else if (toolSetName == "Tool set for forums")
      {
        string resourceValue = Config.Get<AppearanceConfig>().ForumsEditorConfiguration;
        if (string.IsNullOrEmpty(resourceValue))
          resourceValue = "Telerik.Sitefinity.Resources.Themes.ForumsToolsFile.xml";
        str = this.GetDefaultResourceXml(resourceValue);
      }
      else
      {
        ConfigValueDictionary editorConfigurations = Config.Get<AppearanceConfig>().EditorConfigurations;
        str = editorConfigurations.ContainsKey(toolSetName) ? editorConfigurations[toolSetName] : "";
      }
      items.Add(new TextEditorToolsetViewModel()
      {
        ToolSetName = toolSetName,
        ToolSetXml = str
      });
      return new CollectionContext<TextEditorToolsetViewModel>((IEnumerable<TextEditorToolsetViewModel>) items);
    }

    /// <summary>Get text editor tool sets for the grinds.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Get text editor tool sets for the grinds.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "texteditor/?getDefault={getDefault}")]
    [OperationContract]
    public CollectionContext<TextEditorToolsetViewModel> GetTextEditorToolsets(
      bool getDefault)
    {
      this.DemandViewPermissions();
      List<TextEditorToolsetViewModel> items = new List<TextEditorToolsetViewModel>();
      if (getDefault)
      {
        items.Add(new TextEditorToolsetViewModel()
        {
          ToolSetName = "Default tool set"
        });
        items.Add(new TextEditorToolsetViewModel()
        {
          ToolSetName = "Tool set for comments"
        });
        items.Add(new TextEditorToolsetViewModel()
        {
          ToolSetName = "Tool set for forums"
        });
      }
      else
      {
        ConfigValueDictionary editorConfigurations = Config.Get<AppearanceConfig>().EditorConfigurations;
        foreach (ConfigElement configElement in (ConfigElementCollection) editorConfigurations)
        {
          string key = configElement.GetKey();
          if (editorConfigurations[key].TrimStart().StartsWith("<"))
            items.Add(new TextEditorToolsetViewModel()
            {
              ToolSetName = key
            });
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<TextEditorToolsetViewModel>((IEnumerable<TextEditorToolsetViewModel>) items);
    }

    /// <summary>Saves the text editor tool set.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Saves the text editor tool set.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "texteditor/{key}/")]
    [OperationContract]
    public string SaveTextEditorToolsets(TextEditorToolsetViewModel toolSet, string key)
    {
      this.DemandEditPermissions();
      this.TextEditorToolSetResult = "";
      this.SaveTextEditorToolSetConfiguration(toolSet.ToolSetName, toolSet.ToolSetXml);
      return this.TextEditorToolSetResult;
    }

    /// <summary>Deletes the text editor tool set.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Deletes a text editor tool set.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "texteditor/batch/")]
    [OperationContract]
    public void DeleteTextEditorToolsets(string[] keys)
    {
      this.DemandEditPermissions();
      this.DeleteTextEditorToolSet(keys[0]);
    }

    /// <summary>Deletes the text editor tool set.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "Upload a text editor tool set.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "texteditorupload/")]
    [OperationContract]
    public string UploadTextEditorToolset()
    {
      this.TextEditorToolSetResult = "";
      this.DemandEditPermissions();
      if (SystemManager.CurrentHttpContext.Request.Files.Count > 0)
      {
        using (StreamReader streamReader = new StreamReader(SystemManager.CurrentHttpContext.Request.Files[0].InputStream))
          this.SaveTextEditorToolSetConfiguration(Path.GetFileNameWithoutExtension(SystemManager.CurrentHttpContext.Request.Files[0].FileName), streamReader.ReadToEnd());
      }
      return this.TextEditorToolSetResult;
    }

    [WebHelp(Comment = "")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "migrate/")]
    [OperationContract]
    public IEnumerable<string> GetConfigurations()
    {
      this.DemandViewPermissions();
      return ((IEnumerable<ConfigSection>) ConfigManager.GetManager().GetAllConfigSections()).Select<ConfigSection, string>((Func<ConfigSection, string>) (c => c.TagName));
    }

    [WebHelp(Comment = "")]
    [WebInvoke(Method = "POST", UriTemplate = "migrate/")]
    [OperationContract]
    public void MigrateConfigurations()
    {
      this.DemandEditPermissions();
      if (Config.ConfigStorageMode == ConfigStorageMode.Auto)
        throw new InvalidOperationException("System ia already running in Auto config storage mode. Migration of configs is required when switching existing Sitefinity in FileSystem or Database config storage mode to Auto.");
      try
      {
        this.MigrateAllConfigurations();
        this.ExportConfigsForDeployment();
      }
      catch (Exception ex)
      {
        using (System.IO.File.Create(Path.Combine(SystemManager.AppDataFolderPhysicalPath, "sitefinity/configuration/migrated.error")))
          ;
        throw ex;
      }
      using (System.IO.File.Create(Path.Combine(SystemManager.AppDataFolderPhysicalPath, "sitefinity/configuration/migrated.success")))
        ;
    }

    [WebHelp(Comment = "")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "migrate/{tagName}/")]
    [OperationContract]
    public string MigrateConfiguration(string tagName)
    {
      this.DemandEditPermissions();
      ConfigManager manager = ConfigManager.GetManager();
      ConfigSection section = !(ObjectFactory.GetNamedType(tagName, typeof (ConfigSection)) == (Type) null) ? manager.GetSection(tagName) : throw WebProtocolException.NotFound(Telerik.Sitefinity.Localization.Res.Get<MultisiteResources>().NotFound);
      manager.MigrateSection(section);
      return section.TagName;
    }

    internal static void CheckConfigurationsConsistency()
    {
      if (System.IO.File.Exists(Path.Combine(SystemManager.AppDataFolderPhysicalPath, "sitefinity/configuration/migrated.error")))
        throw new NotSupportedException("There was an error while the system was being migrated to Auto storage mode. You must restore Sitefinity to a pre-migration state. See Error.log for details.");
      if (System.IO.File.Exists(Path.Combine(SystemManager.AppDataFolderPhysicalPath, "sitefinity/configuration/migrated.success")) && Config.ConfigStorageMode != ConfigStorageMode.Auto)
        throw new NotSupportedException("Auto configuration storage mode must be set in web.config after successful migration. Go to web.config and add:\r\n1. In <configuration><configSections><sectionGroup name=\"telerik\"> add the following tag: <section name = \"sitefinity\" type = \"Telerik.Sitefinity.Configuration.SectionHandler, Telerik.Sitefinity\" requirePermission = \"false\" />\r\n2. In <configuration><telerik><sitefinity> add the following tag: <sitefinityConfig storageMode=\"Auto\" restrictionLevel=\"Default\" />");
    }

    private void DemandViewPermissions() => ServiceUtility.RequestBackendUserAuthentication("Backend", "ViewConfigurations");

    private void DemandEditPermissions() => ServiceUtility.RequestBackendUserAuthentication("Backend", "ChangeConfigurations");

    private void ExportConfigsForDeployment()
    {
      OpenAccessXmlConfigStorageProvider databaseStorageProvider = ConfigManager.GetManager().Provider.GetDatabaseStorageProvider() as OpenAccessXmlConfigStorageProvider;
      using (SitefinityOAContext context = OpenAccessConnection.GetContext((IOpenAccessMetadataProvider) databaseStorageProvider, databaseStorageProvider.ConnectionName))
        new ConfigMigrationExporter().Handle((OpenAccessContext) context);
      MetadataManager manager = MetadataManager.GetManager();
      ModuleVersion moduleVersion = manager.GetModuleVersion("f2984670-c099-4157-9fad-f6915db28ad6");
      if (moduleVersion == null)
        return;
      manager.DeleteModuleVersion(moduleVersion);
      manager.SaveChanges();
    }

    private void MigrateAllConfigurations()
    {
      ConfigManager manager = ConfigManager.GetManager();
      foreach (string configuration in this.GetConfigurations())
      {
        ConfigSection section = manager.GetSection(configuration);
        manager.MigrateSection(section);
      }
    }

    private void AddNewProperty(
      int index,
      string[][] propertyBag,
      Dictionary<string, string> properties)
    {
      if (properties.ContainsKey(propertyBag[index][0]))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().WCFPropertyDuplicate.Arrange((object) propertyBag[index][0]), (Exception) null);
      properties.Add(propertyBag[index][0], propertyBag[index][1]);
    }

    private bool DeleteSectionInternal(
      string nodeName,
      string policyHandlerName,
      string policyName,
      string provider,
      string mode)
    {
      ConfigManager manager = Config.GetManager(provider);
      KeyValuePair<string, string>[] nodePairs = this.GetNodePairs(nodeName);
      ConfigSection section = this.GetSection(manager, nodePairs[0].Key);
      object collection = (object) null;
      if (section == null)
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Section not found", (Exception) null);
      ConfigElement parentElement;
      object target = this.GetTarget(nodePairs, section, this.GetUIMode(mode) == ConfigSectionItems.ConfigUIMode.New, ref collection, out parentElement);
      ConfigSource source = parentElement.Source;
      if (target != null)
      {
        if (collection != null && collection is NameValueCollection)
        {
          ConfigSectionItems.UINameValue uiNameValue = (ConfigSectionItems.UINameValue) target;
          ((NameValueCollection) collection).Remove(uiNameValue.Key);
          uiNameValue.ContainingProperty.SetValue(uiNameValue.BaseObject, collection, (object[]) null);
        }
        else if (collection != null && collection is ConfigElementCollection)
        {
          ConfigElement configElement = !(target is ConfigSectionItems.UIConfigElement) ? (ConfigElement) target : ((ConfigSectionItems.UIConfigElement) target).Element;
          elementCollection = (ConfigElementCollection) collection;
          if (!elementCollection.Section.Equals((object) section))
          {
            bool flag = false;
            try
            {
              section = this.GetSection(manager, elementCollection.Section.TagName);
              string[] strArray = elementCollection.GetPath().Split(new char[1]
              {
                '/'
              }, 2);
              if (strArray.Length == 2)
              {
                if (strArray[0] == elementCollection.Section.TagName)
                {
                  if (Config.GetByPath<ConfigElement>(strArray[1], (ConfigElement) section) is ConfigElementCollection elementCollection)
                  {
                    configElement = elementCollection.GetElementByKey(configElement.GetKey());
                    if (configElement != null)
                      flag = true;
                  }
                }
              }
            }
            catch
            {
            }
            if (!flag)
              throw new WebProtocolException(HttpStatusCode.InternalServerError, "Unable to update linked element from this section. Please update it from '{0}' section, the element path is '{1}'.".Arrange((object) ConfigUtils.GetPrintableName((object) elementCollection.Section), (object) elementCollection.GetPath()), (Exception) null);
          }
          elementCollection.Remove(configElement);
          source = configElement.Source;
        }
      }
      manager.SaveSection(section, source != ConfigSource.Database);
      return true;
    }

    private IList<ConfigSectionItems.UISectionItem> ProcessSection(
      ConfigSectionItems.SectionContext context)
    {
      ConfigManager manager = Config.GetManager(context.Provider);
      IList<ConfigSectionItems.UISectionItem> list = (IList<ConfigSectionItems.UISectionItem>) new List<ConfigSectionItems.UISectionItem>();
      if (context.Mode == ConfigSectionItems.ConfigUIMode.Auto)
        context.Mode = ConfigSectionItems.ConfigUIMode.Navigation;
      if (string.IsNullOrEmpty(context.NodePath))
      {
        if (context.Mode != ConfigSectionItems.ConfigUIMode.Navigation)
          return list;
        foreach (ConfigSection section in ((IEnumerable<ConfigSection>) manager.GetAllConfigSections()).Where<ConfigSection>((Func<ConfigSection, bool>) (x => this.IsItemVisibleByUser(x.TagName))))
        {
          if (section.VisibleInUI)
          {
            ConfigSectionItems.UISectionItem sectionItem = this.CreateSectionItem(section);
            sectionItem.SaveLocation = (int) section.Source;
            this.AddItemToList(list, sectionItem);
          }
        }
      }
      else
      {
        KeyValuePair<string, string>[] nodePairs = this.GetNodePairs(context.NodePath);
        if (nodePairs.Length == 0)
          return list;
        context.Section = this.GetSection(manager, nodePairs[0].Key);
        if (context.Section == null)
          return list;
        if (nodePairs.Length > 1)
        {
          object collection = (object) null;
          ConfigElement parentElement;
          object obj = this.GetTarget(nodePairs, context.Section, context.Mode == ConfigSectionItems.ConfigUIMode.New, ref collection, out parentElement, context.ElementType);
          context.DefaultSection = ConfigUtils.CreateInstance<ConfigSection>(context.Section.GetType());
          if (obj != null)
          {
            if (context.Mode != ConfigSectionItems.ConfigUIMode.New)
              context.TargetElementWithDefaults = this.GetDefaultTarget(nodePairs, obj, context);
            if (obj is ConfigSectionItems.UIConfigElement)
              obj = (object) ((ConfigSectionItems.UIConfigElement) obj).Element;
            Type type = obj.GetType();
            if (obj is ConfigElementCollection configCollection)
            {
              context.CollectionElementTypes = this.GetTypeImplementations(configCollection.ElementType);
              this.FillCollectionItems(list, nodePairs[nodePairs.Length - 2].Key, configCollection, context);
            }
            else if (obj is NameValueCollection)
            {
              ConfigSource source = context.Section.Source;
              if (parentElement != null)
                source = parentElement.Source;
              context.IsParametersCollection = true;
              context.ParametersCollectionParentSaveLocation = (int) source;
              this.FillParameterItems(list, nodePairs[nodePairs.Length - 2].Key, (NameValueCollection) obj, source, context);
            }
            else
            {
              if (type.IsValueType || type.IsPrimitive || type.FullName.IndexOf("System", StringComparison.OrdinalIgnoreCase) != -1)
              {
                switch (obj)
                {
                  case ConfigElementCollection _:
                  case NameValueCollection _:
                    break;
                  default:
                    ConfigSectionItems.UISectionItem sectionItem = this.CreateSectionItem(nodePairs[nodePairs.Length - 2].Key, nodePairs[nodePairs.Length - 1].Key);
                    sectionItem.Value = Convert.ToString(obj);
                    sectionItem.ItemTypeName = type.FullName;
                    sectionItem.SaveLocation = parentElement == null ? (int) context.Section.Source : (int) parentElement.Source;
                    this.AddItemToList(list, sectionItem);
                    goto label_36;
                }
              }
              this.FillItems(list, obj, type, context.Mode, context);
            }
          }
        }
        else
        {
          if (context.Mode != ConfigSectionItems.ConfigUIMode.New)
            context.TargetElementWithDefaults = (object) ConfigUtils.CreateInstance<ConfigSection>(context.Section.GetType());
          this.FillItems(list, (object) context.Section, context.Section.GetType(), context.Mode, context);
        }
      }
label_36:
      return list;
    }

    private bool DoesElementPersistSiteSpecificValues(object target) => typeof (ConfigElement).IsAssignableFrom(target.GetType()) && !target.GetType().ImplementsInterface(typeof (IEnumerable)) && (target as ConfigElement).PersistsSiteSpecificValues;

    private object GetDefaultTarget(
      KeyValuePair<string, string>[] nodes,
      object target,
      ConfigSectionItems.SectionContext context)
    {
      object collection = (object) null;
      object defaultTarget1 = this.GetTarget(nodes, context.DefaultSection, false, ref collection, out ConfigElement _);
      if (defaultTarget1 == context.DefaultSection || defaultTarget1 == null)
      {
        defaultTarget1 = (object) null;
        if (typeof (ConfigElement).IsAssignableFrom(target.GetType()) && ((ConfigElement) target).Source == ConfigSource.Default)
        {
          defaultTarget1 = this.GetDefaultTarget((ConfigElement) target);
        }
        else
        {
          Type type = target.GetType();
          int num1 = 0;
          if (target is ConfigSectionItems.UINameValue)
          {
            type = ((ConfigSectionItems.UINameValue) target).ContainingProperty.GetType();
            num1 = 1;
          }
          if (typeof (NameValueCollection).IsAssignableFrom(type))
          {
            int num2 = ((IEnumerable<KeyValuePair<string, string>>) nodes).Count<KeyValuePair<string, string>>() - 2 - num1;
            object target1 = this.GetTarget(nodes, 0, num2, (ConfigElement) context.Section);
            if (typeof (ConfigElement).IsAssignableFrom(target1.GetType()) && ((ConfigElement) target1).Source == ConfigSource.Default)
            {
              object defaultTarget2 = this.GetDefaultTarget((ConfigElement) target1);
              if (defaultTarget2 != null)
              {
                int targetNodeIndex = ((IEnumerable<KeyValuePair<string, string>>) nodes).Count<KeyValuePair<string, string>>() - 1;
                defaultTarget1 = this.GetTarget(nodes, num2, targetNodeIndex, (ConfigElement) defaultTarget2);
              }
            }
          }
        }
      }
      return defaultTarget1;
    }

    private object GetDefaultTarget(ConfigElement target)
    {
      ConfigElement instance = (ConfigElement) ConfigUtils.CreateInstance(target.Section.GetType(), (Type) null);
      string path = target.GetPath();
      return (object) (Config.GetByPath<ConfigElement>(path.Substring(path.IndexOf('/') + 1), instance) ?? this.GetDynamicallyAddedDefaultElement(target));
    }

    private ConfigElement GetDynamicallyAddedDefaultElement(ConfigElement target)
    {
      ConfigElement addedDefaultElement = (ConfigElement) null;
      ConfigElement parent = target.Parent;
      ConfigElement configElement = target;
      while (!typeof (IHasDefaultConfigElements).IsAssignableFrom(parent.GetType()))
      {
        parent = parent.Parent;
        configElement = parent;
        if (parent == null)
          return (ConfigElement) null;
      }
      foreach (ConfigProperty property in (Collection<ConfigProperty>) parent.Properties)
      {
        if (property.Name.ToLower() == configElement.TagName)
        {
          ConfigElement defaultConfig = ((IHasDefaultConfigElements) parent).GetDefaultConfig(property);
          string path = defaultConfig.GetPath();
          Config.GetByPath<ConfigElement>(target.GetPath().Remove(0, path.Length), defaultConfig);
        }
      }
      return addedDefaultElement;
    }

    private bool CanElementBeMoved(object target)
    {
      if (typeof (ConfigElement).IsAssignableFrom(target.GetType()) && !target.GetType().ImplementsInterface(typeof (IEnumerable)) && ((ConfigElement) target).Source == ConfigSource.Database)
      {
        ConfigElement parent = ((ConfigElement) target).Parent;
        if (parent != null && parent.Source != ConfigSource.Database)
          return true;
      }
      return false;
    }

    private bool CanElementBeModified(object target)
    {
      if (typeof (ConfigElement).IsAssignableFrom(target.GetType()) && !target.GetType().ImplementsInterface(typeof (IEnumerable)))
      {
        ConfigElement configElement = (ConfigElement) target;
        switch (configElement.Source)
        {
          case ConfigSource.Default:
            for (ConfigElement parent = configElement.Parent; parent != null; parent = parent.Parent)
            {
              if (parent.Source == ConfigSource.Database || parent.Source == ConfigSource.FileSystem)
                return true;
              switch (parent)
              {
                case ConfigElementCollection _:
                  return false;
                case ConfigSection _:
                  return true;
                default:
                  continue;
              }
            }
            break;
          case ConfigSource.FileSystem:
          case ConfigSource.Database:
            return true;
        }
      }
      return false;
    }

    private IEnumerable<Type> GetTypeImplementations(Type type)
    {
      IEnumerable<Type> source = Config.Get<SystemConfig>().GetTypeImplementations(type);
      if (type.IsAbstract && source == null)
        source = type.GetAssignableTypes().Where<Type>((Func<Type, bool>) (t => t != type && !t.IsAbstract));
      if (!type.IsAbstract && source != null)
      {
        List<Type> list = source.ToList<Type>();
        list.Insert(0, type);
        source = (IEnumerable<Type>) list;
      }
      return source;
    }

    private void FillItems(
      IList<ConfigSectionItems.UISectionItem> list,
      object target,
      Type targetType,
      ConfigSectionItems.ConfigUIMode mode,
      ConfigSectionItems.SectionContext context)
    {
      context.CanBeMoved = this.CanElementBeMoved(target);
      context.CanBeModified = this.CanElementBeModified(target);
      context.PersistsSiteSpecificValues = this.DoesElementPersistSiteSpecificValues(target);
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(targetType))
      {
        if (ConfigUtils.ValidateProperty(property, targetType) && this.IsItemVisibleByUser(property, target, mode))
        {
          bool flag1 = false;
          bool isSecretProperty = false;
          string selectedEncryption = (string) null;
          ConfigurationPropertyAttribute attribute = property.Attributes[typeof (ConfigurationPropertyAttribute)] as ConfigurationPropertyAttribute;
          ConfigSectionItems.UISectionItem configItem;
          if (typeof (ConfigElement).IsAssignableFrom(property.PropertyType) || typeof (NameValueCollection).IsAssignableFrom(property.PropertyType))
          {
            if (mode != ConfigSectionItems.ConfigUIMode.Form && mode != ConfigSectionItems.ConfigUIMode.New && attribute != null)
            {
              configItem = this.CreateSectionItem(targetType.Name, property.Name);
              configItem.ItemTypeName = targetType.FullName;
              if (typeof (ConfigElementCollection).IsAssignableFrom(property.PropertyType) || typeof (NameValueCollection).IsAssignableFrom(property.PropertyType))
              {
                configItem.IsCollection = true;
                bool flag2 = false;
                object obj = property.GetValue(target);
                switch (obj)
                {
                  case ConfigElementCollection _:
                    flag2 = ((ConfigElementCollection) obj).Count > 0;
                    break;
                  case NameValueCollection _:
                    flag2 = ((NameObjectCollectionBase) obj).Count > 0;
                    break;
                }
                configItem.HasChildren = flag2;
              }
              else
                configItem.HasChildren = this.HasChildren((object) property.PropertyType);
            }
            else
              continue;
          }
          else if (mode != ConfigSectionItems.ConfigUIMode.Navigation)
          {
            configItem = new ConfigSectionItems.UISectionItem()
            {
              Parent = targetType.Name,
              Key = property.Name,
              HasChildren = false,
              Disabled = false,
              ItemTypeName = property.PropertyType.FullName,
              Description = string.Empty,
              SaveLocation = -1,
              IsConfigElementKey = false,
              IsModifiedDefault = false,
              IsExternallyDefined = false
            };
            if (target is ConfigSectionItems.UINameValue)
            {
              ConfigSectionItems.UINameValue uiNameValue = (ConfigSectionItems.UINameValue) target;
              if (!(uiNameValue.BaseObject is ConfigElement baseObject))
                throw new ArgumentNullException("UINameValue item must have base object.");
              configItem.SaveLocation = (int) baseObject.Source;
              bool flag3 = property.Name.ToLower() == "value";
              if (baseObject.Source == ConfigSource.Default)
              {
                if (context.TargetElementWithDefaults != null)
                {
                  if (flag3)
                  {
                    ConfigSectionItems.UINameValue elementWithDefaults = (ConfigSectionItems.UINameValue) context.TargetElementWithDefaults;
                    configItem.IsModifiedDefault = uiNameValue.Value != elementWithDefaults.Value;
                    if (configItem.IsModifiedDefault)
                      configItem.DefaultValue = property.Converter.ConvertToInvariantString((object) elementWithDefaults.Value);
                  }
                }
                else
                  configItem.SaveLocation = 2;
              }
              if (flag3)
              {
                selectedEncryption = uiNameValue.Encryption;
                flag1 = true;
              }
              configItem.Disabled = !uiNameValue.IsNew && string.Compare(property.Name, "Key") == 0;
              if (string.Compare(property.Name, "Value") == 0)
              {
                if (selectedEncryption == "EnvVariables")
                {
                  configItem.Disabled = true;
                  configItem.IsExternallyDefined = true;
                }
                configItem.PropertyPath = string.Format("{0}:{1}", (object) baseObject.GetPath(), (object) uiNameValue.Key);
              }
            }
            else if (!typeof (NameValueCollection).IsAssignableFrom(targetType))
            {
              if (attribute != null)
                flag1 = flag1 || property.PropertyType.Equals(typeof (string));
              else
                continue;
            }
            else if (property.Converter.CanConvertTo(typeof (string)))
              configItem.Disabled = property.IsReadOnly || !property.Converter.CanConvertFrom(typeof (string));
            else
              continue;
            if (typeof (ConfigElement).IsAssignableFrom(targetType))
            {
              configItem.SaveLocation = (int) ((ConfigElement) target).Source;
              if (attribute != null && attribute.IsKey)
                configItem.IsConfigElementKey = true;
            }
            string str1 = (string) null;
            ConfigProperty prop;
            if (attribute != null && target is ConfigElement el && el.Properties.TryGetValue(attribute.Name, out prop))
            {
              if (prop.IsSecret)
              {
                flag1 = true;
                isSecretProperty = true;
              }
              configItem.IsSiteSpecific = prop.IsSiteSpecific(el);
              configItem.PropertyPath = string.Format("{0}:{1}", (object) el.GetPath(), (object) attribute.Name);
              if (el.GetRawValue(prop) is LazyValue rawValue)
              {
                str1 = rawValue.Key;
                if (rawValue is SecretValue secretValue)
                {
                  if (secretValue.ResolverName == "EnvVariables")
                  {
                    configItem.ItemTypeName = typeof (string).FullName;
                    configItem.Disabled = true;
                    configItem.IsExternallyDefined = true;
                  }
                  flag1 = true;
                  selectedEncryption = secretValue.ResolverName;
                }
              }
            }
            if (str1 == null)
            {
              object obj = property.GetValue(target);
              if (obj != null)
                str1 = property.Converter.ConvertToInvariantString(obj);
            }
            if (str1 != null)
            {
              if (property.PropertyType.IsEnum && !property.PropertyType.CustomAttributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>) (a => a.AttributeType == typeof (FlagsAttribute))) && !flag1)
              {
                string[] names = System.Enum.GetNames(property.PropertyType);
                object obj1 = (object) new ExpandoObject();
                // ISSUE: reference to a compiler-generated field
                if (ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__0 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj2 = ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__0.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__0, obj1, str1);
                // ISSUE: reference to a compiler-generated field
                if (ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__1 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string[], object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Options", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj3 = ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__1.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__1, obj1, names);
                ConfigSectionItems.UISectionItem uiSectionItem = configItem;
                // ISSUE: reference to a compiler-generated field
                if (ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__3 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (ConfigSectionItems)));
                }
                // ISSUE: reference to a compiler-generated field
                Func<CallSite, object, string> target1 = ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__3.Target;
                // ISSUE: reference to a compiler-generated field
                CallSite<Func<CallSite, object, string>> p3 = ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__3;
                // ISSUE: reference to a compiler-generated field
                if (ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__2 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__2 = CallSite<Func<CallSite, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "SerializeObject", (IEnumerable<Type>) null, typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj4 = ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__2.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__41.\u003C\u003Ep__2, typeof (JsonConvert), obj1);
                string str2 = target1((CallSite) p3, obj4);
                uiSectionItem.Value = str2;
              }
              else
                configItem.Value = str1;
            }
            if (isSecretProperty && configItem.Value.IsNullOrEmpty() && selectedEncryption == null)
            {
              ISecretDataResolver defaultSecretResolver = Config.GetDefaultSecretResolver();
              if (defaultSecretResolver != null && defaultSecretResolver.Mode == SecretDataMode.Encrypt)
                selectedEncryption = Config.GetDefaultSecretResolver().Name;
            }
            if (!(target is ConfigSectionItems.UINameValue) && configItem.SaveLocation == 1 && mode != ConfigSectionItems.ConfigUIMode.New && context.TargetElementWithDefaults != null)
            {
              ConfigSectionItems.UISectionItem uiSectionItem = new ConfigSectionItems.UISectionItem();
              object obj = property.GetValue(context.TargetElementWithDefaults);
              if (obj != null)
                uiSectionItem.Value = property.Converter.ConvertToInvariantString(obj);
              configItem.DefaultValue = uiSectionItem.Value;
              if (uiSectionItem.Value != str1)
                configItem.IsModifiedDefault = true;
            }
          }
          else
            continue;
          if (configItem != null)
          {
            if (flag1 && !configItem.IsConfigElementKey)
              configItem.Encryption = this.GetEncryptionOptions(isSecretProperty, selectedEncryption);
            if (!this.FillTitleAndDescriptionFromResources(configItem, (MemberDescriptor) property))
              configItem.Title = ConfigUtils.GetPrintableName(property.Name);
            this.AddItemToList(list, configItem);
          }
        }
      }
    }

    private string GetEncryptionOptions(bool isSecretProperty, string selectedEncryption = null)
    {
      if (selectedEncryption == null)
        selectedEncryption = "None";
      object obj1 = (object) new ExpandoObject();
      // ISSUE: reference to a compiler-generated field
      if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__0.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__0, obj1, selectedEncryption);
      List<object> objectList = new List<object>();
      IEnumerable<ISecretDataResolver> secretResolvers = (IEnumerable<ISecretDataResolver>) Config.SecretResolvers;
      IEnumerable<ISecretDataResolver> secretDataResolvers;
      if (selectedEncryption == "EnvVariables")
      {
        secretDataResolvers = Config.SecretResolvers.Where<ISecretDataResolver>((Func<ISecretDataResolver, bool>) (r => r.Name == "EnvVariables"));
      }
      else
      {
        if (selectedEncryption == null || !isSecretProperty)
        {
          object obj3 = (object) new ExpandoObject();
          // ISSUE: reference to a compiler-generated field
          if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Text", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj4 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__1.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__1, obj3, Telerik.Sitefinity.Localization.Res.Get<Labels>().ConfigPlainTextOption);
          // ISSUE: reference to a compiler-generated field
          if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj5 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__2.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__2, obj3, "None");
          // ISSUE: reference to a compiler-generated field
          if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Icon", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj6 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__3.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__3, obj3, "fa fa-file-text-o");
          // ISSUE: reference to a compiler-generated field
          if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__4 = CallSite<System.Action<CallSite, List<object>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__4.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__4, objectList, obj3);
        }
        secretDataResolvers = Config.SecretResolvers.Where<ISecretDataResolver>((Func<ISecretDataResolver, bool>) (r => r.Name != "EnvVariables"));
      }
      foreach (ISecretDataResolver secretDataResolver in secretDataResolvers)
      {
        object obj7 = (object) new ExpandoObject();
        // ISSUE: reference to a compiler-generated field
        if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Text", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__5.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__5, obj7, secretDataResolver.Title);
        // ISSUE: reference to a compiler-generated field
        if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj9 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__6.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__6, obj7, secretDataResolver.Name);
        // ISSUE: reference to a compiler-generated field
        if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Icon", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__7.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__7, obj7, secretDataResolver.Mode == SecretDataMode.Encrypt ? "fa fa-key" : "fa fa-link");
        // ISSUE: reference to a compiler-generated field
        if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "IsReadOnly", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj11 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__8.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__8, obj7, secretDataResolver.IsReadOnly);
        // ISSUE: reference to a compiler-generated field
        if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__9 = CallSite<System.Action<CallSite, List<object>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__9.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__9, objectList, obj7);
      }
      // ISSUE: reference to a compiler-generated field
      if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, List<object>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Options", typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj12 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__10.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__10, obj1, objectList);
      // ISSUE: reference to a compiler-generated field
      if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (ConfigSectionItems)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__12.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p12 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__12;
      // ISSUE: reference to a compiler-generated field
      if (ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__11 = CallSite<Func<CallSite, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "SerializeObject", (IEnumerable<Type>) null, typeof (ConfigSectionItems), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj13 = ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__11.Target((CallSite) ConfigSectionItems.\u003C\u003Eo__42.\u003C\u003Ep__11, typeof (JsonConvert), obj1);
      return target((CallSite) p12, obj13);
    }

    private void FillCollectionItems(
      IList<ConfigSectionItems.UISectionItem> list,
      string parentKey,
      ConfigElementCollection configCollection,
      ConfigSectionItems.SectionContext context)
    {
      if (configCollection.Count <= 0)
        return;
      ConfigSectionItems.DefaultProperty defaultProperty = new ConfigSectionItems.DefaultProperty("Item {0}");
      int num = 0;
      foreach (ConfigElement config in configCollection)
      {
        if (this.IsItemVisibleByUser(config.GetPath()))
        {
          bool flag = this.IsDeletionAllowed(config.Source);
          ConfigSectionItems.UISectionItem configItem = new ConfigSectionItems.UISectionItem()
          {
            Parent = parentKey,
            Key = configCollection.ElementType.Name,
            Title = defaultProperty.GetValue((object) config),
            HasChildren = this.HasChildrenCached((object) config),
            CanDelete = flag,
            ItemTypeName = configCollection.ElementType.FullName,
            SaveLocation = (int) config.Source,
            CollectionKey = config.GetKey()
          };
          this.FillDescriptionFromObjectInfo(configItem, TypeDescriptor.GetAttributes(config.GetType()));
          this.AddItemToList(list, configItem);
          ++num;
        }
      }
    }

    private void FillParameterItems(
      IList<ConfigSectionItems.UISectionItem> list,
      string parentKey,
      NameValueCollection nameValueItems,
      ConfigSource parentSaveLocation,
      ConfigSectionItems.SectionContext context)
    {
      bool flag = this.IsDeletionAllowed(parentSaveLocation);
      foreach (string key in nameValueItems.Keys)
      {
        ConfigSource configSource = parentSaveLocation;
        ConfigSectionItems.UISectionItem uiSectionItem = new ConfigSectionItems.UISectionItem()
        {
          Title = key,
          Parent = parentKey,
          Key = nameValueItems.GetType().Name,
          HasChildren = false,
          CanDelete = flag,
          ItemTypeName = typeof (string).FullName,
          SaveLocation = (int) configSource,
          CollectionKey = key
        };
        this.AddItemToList(list, uiSectionItem);
      }
    }

    private bool IsDeletionAllowed(ConfigSource saveLocation)
    {
      bool flag = true;
      if (!SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile) && saveLocation != ConfigSource.Database)
        flag = false;
      return flag;
    }

    private KeyValuePair<string, string>[] GetNodePairs(string node)
    {
      string[] nodes = this.GetNodes(node);
      IList<KeyValuePair<string, string>> keyValuePairList = (IList<KeyValuePair<string, string>>) new List<KeyValuePair<string, string>>();
      foreach (string str in nodes)
      {
        char[] separator = new char[1]{ '_' };
        string[] source = str.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
        if (((IEnumerable<string>) source).Count<string>() > 1)
          keyValuePairList.Add(new KeyValuePair<string, string>(source[0], source[1]));
        else
          keyValuePairList.Add(new KeyValuePair<string, string>(source[0], string.Empty));
      }
      KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[keyValuePairList.Count];
      keyValuePairList.CopyTo(array, 0);
      return array;
    }

    private ConfigSectionItems.ConfigUIMode GetUIMode(string modeString) => !string.IsNullOrEmpty(modeString) ? (ConfigSectionItems.ConfigUIMode) System.Enum.Parse(typeof (ConfigSectionItems.ConfigUIMode), modeString) : ConfigSectionItems.ConfigUIMode.Auto;

    private ConfigSectionItems.UISectionItem CreateSectionItem(
      ConfigSection section)
    {
      ConfigSectionItems.UISectionItem sectionItem = this.CreateSectionItem("Configuration", section.TagName, ConfigUtils.GetPrintableName((object) section));
      sectionItem.HasChildren = this.HasChildren((object) section);
      return sectionItem;
    }

    private ConfigSectionItems.UISectionItem CreateSectionItem(
      string parent,
      string key)
    {
      return this.CreateSectionItem(parent, key, ConfigUtils.GetPrintableName(key));
    }

    private ConfigSectionItems.UISectionItem CreateSectionItem(
      string parent,
      string key,
      string title)
    {
      return new ConfigSectionItems.UISectionItem()
      {
        Parent = parent,
        Key = key,
        Title = title,
        Description = string.Empty
      };
    }

    private void AddItemToList(
      IList<ConfigSectionItems.UISectionItem> list,
      ConfigSectionItems.UISectionItem item)
    {
      if (item.ItemTypeName != null)
        item.PrepareFormElement();
      list.Add(item);
    }

    private bool HasChildrenCached(object item)
    {
      if (this.typeInfos == null)
        this.typeInfos = new Dictionary<Type, bool>();
      bool flag;
      if (!this.typeInfos.TryGetValue(item.GetType(), out flag))
      {
        flag = this.HasChildren(item);
        this.typeInfos.Add(item.GetType(), flag);
      }
      return flag;
    }

    private bool HasChildren(object item)
    {
      Type type = (object) (item as Type) == null ? item.GetType() : (Type) item;
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(type))
      {
        if (ConfigUtils.ValidateProperty(property, type) && this.IsItemVisibleByUser(property, item) && (typeof (ConfigElement).IsAssignableFrom(property.PropertyType) || typeof (NameValueCollection).IsAssignableFrom(property.PropertyType)))
          return true;
      }
      return false;
    }

    private bool IsItemVisibleByUser(
      PropertyDescriptor prop,
      object target,
      ConfigSectionItems.ConfigUIMode mode = ConfigSectionItems.ConfigUIMode.Navigation)
    {
      if (ClaimsManager.GetCurrentIdentity().IsGlobalUser)
        return true;
      if (prop == null || target == null || !(prop.Attributes[typeof (ConfigurationPropertyAttribute)] is ConfigurationPropertyAttribute attribute))
        return false;
      string path = string.Empty;
      if (target is ConfigElement)
        path = mode != ConfigSectionItems.ConfigUIMode.Navigation ? string.Format("{0}:{1}", (object) ((ConfigElement) target).GetPath(), (object) attribute.Name) : ((ConfigElement) target).GetPropertyPath(attribute.Name);
      return this.IsItemVisibleByUser(path);
    }

    private bool IsItemVisibleByUser(string path)
    {
      if (ClaimsManager.GetCurrentIdentity().IsGlobalUser)
        return true;
      ConfigElementDictionary<string, PropertyPath> specificProperties = Config.Get<SiteSettingsConfig>().SiteSpecificProperties;
      if (specificProperties == null || specificProperties.Count <= 0)
        return false;
      return path.Contains(":") ? specificProperties.Keys.Any<string>((Func<string, bool>) (x => x == path)) : specificProperties.Keys.Any<string>((Func<string, bool>) (x => x.StartsWith(path)));
    }

    private bool FillTitleAndDescriptionFromResources(
      ConfigSectionItems.UISectionItem configItem,
      MemberDescriptor member)
    {
      return this.FillTitleAndDescriptionFromObjectInfo(configItem, member.Attributes) || this.FillTitleAndDescriptionFromDescriptionResource(configItem, member);
    }

    private bool FillTitleAndDescriptionFromObjectInfo(
      ConfigSectionItems.UISectionItem configItem,
      AttributeCollection attributes)
    {
      ObjectInfoAttribute attribute = (ObjectInfoAttribute) attributes[typeof (ObjectInfoAttribute)];
      if (attribute == null)
        return false;
      configItem.Title = attribute.Title;
      configItem.Description = attribute.Description;
      return true;
    }

    private bool FillDescriptionFromObjectInfo(
      ConfigSectionItems.UISectionItem configItem,
      AttributeCollection attributes)
    {
      ObjectInfoAttribute attribute = (ObjectInfoAttribute) attributes[typeof (ObjectInfoAttribute)];
      if (attribute == null)
        return false;
      configItem.Description = attribute.Description;
      return true;
    }

    private bool FillTitleAndDescriptionFromDescriptionResource(
      ConfigSectionItems.UISectionItem configItem,
      MemberDescriptor member)
    {
      DescriptionResourceAttribute attribute = (DescriptionResourceAttribute) member.Attributes[typeof (DescriptionResourceAttribute)];
      if (attribute == null)
        return false;
      configItem.Title = ConfigUtils.GetPrintableName(member.Name);
      configItem.Description = attribute.Description;
      return true;
    }

    private void SetUISectionItemsEditability(IList<ConfigSectionItems.UISectionItem> list)
    {
      if (SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile))
        return;
      foreach (ConfigSectionItems.UISectionItem uiSectionItem in (IEnumerable<ConfigSectionItems.UISectionItem>) list)
      {
        if (uiSectionItem.SaveLocation != 3)
          uiSectionItem.Disabled = true;
      }
    }

    /// <summary>
    /// Gets the target node (the last node in nodes) from the config section.
    /// </summary>
    /// <param name="nodes">The full path of the target node</param>
    /// <param name="section">The section from which to get the target element.</param>
    /// <param name="isNew"></param>
    /// <param name="collection">Returns the collection if target is collection or part of collection</param>
    /// <param name="parentElement">Returns the parent element of the target.</param>
    /// <param name="elementType">Returns the elementType if item is part of collection with different types</param>
    /// <returns>The section element or Null if target is not found</returns>
    private object GetTarget(
      KeyValuePair<string, string>[] nodes,
      ConfigSection section,
      bool isNew,
      ref object collection,
      out ConfigElement parentElement,
      Type elementType = null)
    {
      return this.GetTarget(nodes, 0, ((IEnumerable<KeyValuePair<string, string>>) nodes).Count<KeyValuePair<string, string>>() - 1, (ConfigElement) section, isNew, ref collection, out parentElement, elementType);
    }

    /// <summary>
    /// Gets the target element of the node path in a given path index range from the config element.
    /// </summary>
    /// <param name="nodes">Indicates the path to the target element starting from the section root.</param>
    /// <param name="elementNodeIndex">The node in nodes from which to start searching its children for the target. The element that is searched must be at this node level in the config section.</param>
    /// <param name="targetNodeIndex">The node in nodes that that is the target in the config element</param>
    /// <param name="section">The section from which to get the target element.</param>
    /// <param name="isNew"></param>
    /// <param name="collection">Returns the collection if target is collection or part of collection</param>
    /// <param name="parentElement">Returns the parent element of the target.</param>
    /// <param name="elementType">Returns the elementType if item is part of collection with different types</param>
    /// <returns>The section element or Null if target is not found</returns>
    private object GetTarget(
      KeyValuePair<string, string>[] nodes,
      int startNodeIndex,
      int endNodeIndex,
      ConfigElement section,
      bool isNew,
      ref object collection,
      out ConfigElement parentElement,
      Type elementType = null)
    {
      object target = (object) null;
      object parent = (object) section;
      parentElement = section;
      for (int currentIndex = startNodeIndex; currentIndex <= endNodeIndex; ++currentIndex)
      {
        switch (target)
        {
          case ConfigElementCollection _:
          case NameValueCollection _:
            collection = target;
            target = this.FindTargetFromCollection(target, nodes, currentIndex, parent, elementType);
            break;
          default:
            collection = (object) null;
            parent = target;
            if (parent is ConfigElement)
              parentElement = (ConfigElement) parent;
            target = this.FindTarget(section, target, nodes[currentIndex].Key);
            break;
        }
      }
      if (isNew)
      {
        switch (target)
        {
          case NameValueCollection _:
          case ConfigElementCollection _:
            collection = target;
            target = this.CreateNewCollectionItem(collection, parent, nodes[nodes.Length - 1].Key, elementType);
            break;
        }
      }
      return target;
    }

    /// <summary>
    /// Gets the target element of the node path in a given path index range from the config element.
    /// </summary>
    /// <param name="nodes">Indicates the path to the target element starting from the section root.</param>
    /// <param name="elementNodeIndex">The node in nodes from which to start searching its children for the target. The element that is searched must be at this node level in the config section.</param>
    /// <param name="targetNodeIndex">The node in nodes that that is the target in the config element</param>
    /// <param name="element">The parent element from which to get the target element</param>
    /// <returns>The section element or Null if target is not found</returns>
    private object GetTarget(
      KeyValuePair<string, string>[] nodes,
      int elementNodeIndex,
      int targetNodeIndex,
      ConfigElement element)
    {
      object collection = (object) null;
      ConfigElement parentElement = (ConfigElement) null;
      bool isNew = false;
      if (elementNodeIndex == ((IEnumerable<KeyValuePair<string, string>>) nodes).Count<KeyValuePair<string, string>>() - 1)
        return (object) element;
      object target = this.GetTarget(nodes, elementNodeIndex, targetNodeIndex, element, isNew, ref collection, out parentElement);
      return target == element ? (object) null : target;
    }

    private object FindTargetFromCollection(
      object collection,
      KeyValuePair<string, string>[] nodes,
      int currentIndex,
      object parent,
      Type elementType)
    {
      if (currentIndex > 0)
      {
        KeyValuePair<string, string> node = nodes[currentIndex];
        switch (collection)
        {
          case NameValueCollection _:
            IEnumerator enumerator = ((NameObjectCollectionBase) collection).Keys.GetEnumerator();
            try
            {
              while (enumerator.MoveNext())
              {
                string current = (string) enumerator.Current;
                if (current == node.Value)
                {
                  string resolver = (string) null;
                  PropertyInfo property = parent.GetType().GetProperty(nodes[currentIndex - 1].Key);
                  string unresolvedParameter = ((NameValueCollection) collection)[current];
                  if (parent is ConfigElement)
                    unresolvedParameter = ((ConfigElement) parent).GetUnresolvedParameter(current, out resolver);
                  return (object) new ConfigSectionItems.UINameValue()
                  {
                    Key = current,
                    Value = unresolvedParameter,
                    BaseObject = parent,
                    ContainingProperty = property,
                    Encryption = resolver
                  };
                }
              }
              break;
            }
            finally
            {
              if (enumerator is IDisposable disposable)
                disposable.Dispose();
            }
          case ConfigElementCollection _:
            IConfigElementItem itemByKey = ((ConfigElementCollection) collection).GetItemByKey(node.Value);
            if (itemByKey != null)
              return (object) itemByKey.Element;
            break;
        }
      }
      return (object) null;
    }

    private object CreateNewCollectionItem(
      object collection,
      object parent,
      string propertyName,
      Type elementType)
    {
      switch (collection)
      {
        case NameValueCollection _:
          PropertyInfo property = parent.GetType().GetProperty(propertyName);
          return (object) new ConfigSectionItems.UINameValue()
          {
            IsNew = true,
            BaseObject = parent,
            ContainingProperty = property
          };
        case ConfigElementCollection _:
          return elementType != (Type) null ? (object) ((ConfigElementCollection) collection).CreateNew(elementType) : (object) ((ConfigElementCollection) collection).CreateNew();
        default:
          return (object) null;
      }
    }

    private string[] GetNodes(string nodeName) => ((IEnumerable<string>) nodeName.Split(new string[1]
    {
      ConfigSectionItems.ConfigSectionDelimiter
    }, StringSplitOptions.RemoveEmptyEntries)).Reverse<string>().ToArray<string>();

    private ConfigSection GetSection(
      ConfigManager manager,
      string sectionName,
      bool skipGetFromCache = false)
    {
      return skipGetFromCache ? manager.GetSection(sectionName) : Config.GetConfigSection(sectionName);
    }

    private object FindTarget(ConfigSection section, object parent, string nodeName) => this.FindTarget(section, parent, nodeName);

    internal object FindTarget(ConfigElement section, object parent, string nodeName)
    {
      if (parent == null)
        parent = (object) section;
      if (parent != null)
      {
        Type type = parent.GetType();
        if (nodeName != null)
        {
          PropertyInfo property = type.GetProperty(nodeName);
          if (property != (PropertyInfo) null)
            return property.GetValue(parent, (object[]) null);
        }
      }
      return parent;
    }

    private CollectionContext<CultureViewModel> GetCulturesInternal(
      string filter,
      ConfigElementList<CultureElement> cultures,
      bool showSpecificName)
    {
      IQueryable<CultureViewModel> source = cultures.Select<CultureElement, CultureViewModel>((Func<CultureElement, CultureViewModel>) (c => new CultureViewModel(c)
      {
        ShowSpecificName = showSpecificName
      })).AsQueryable<CultureViewModel>();
      if (!string.IsNullOrEmpty(filter))
      {
        string queryString = (string) null;
        if (this.MatchCultureFilter(ref filter, out queryString))
          source = source.Where<CultureViewModel>((Expression<Func<CultureViewModel, bool>>) (cvm => cvm.DisplayName.IndexOf(queryString, StringComparison.InvariantCultureIgnoreCase) > -1));
        else
          source = source.Where<CultureViewModel>(filter);
      }
      IEnumerable<string> culturesKeys = ConfigSectionItems.GetFilteredCulturesKeys();
      if (culturesKeys != null)
        source = source.Where<CultureViewModel>((Expression<Func<CultureViewModel, bool>>) (c => culturesKeys.Contains<string>(c.Key)));
      CultureViewModel[] array = source.OrderBy<CultureViewModel, string>((Expression<Func<CultureViewModel, string>>) (c => c.DisplayName)).ToArray<CultureViewModel>();
      return new CollectionContext<CultureViewModel>((IEnumerable<CultureViewModel>) array)
      {
        TotalCount = array.Length
      };
    }

    private static IEnumerable<string> GetFilteredCulturesKeys() => Config.RestrictionLevel == RestrictionLevel.ReadOnlyConfigFile ? (IEnumerable<string>) Config.Get<ResourcesConfig>().Cultures.Keys : (IEnumerable<string>) null;

    private bool MatchCultureFilter(ref string filter, out string query)
    {
      query = (string) null;
      if (string.IsNullOrEmpty(filter))
        return false;
      Match match = ConfigSectionItems.cultureFilterRegex.Match(filter);
      int num = match == null || !match.Groups[0].Success ? 0 : (match.Groups[1].Success ? 1 : 0);
      if (num == 0)
        return num != 0;
      query = match.Groups[1].ToString();
      filter = ConfigSectionItems.cultureFilterRegex.Replace(filter, "");
      return num != 0;
    }

    private string ReadDefaultResourceXml(string resourceName)
    {
      Assembly assembly = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly;
      using (Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName))
        return manifestResourceStream != null ? new StreamReader(manifestResourceStream).ReadToEnd() : throw new FileNotFoundException("The file: \"" + resourceName + "\" in the assembly: \"" + assembly.FullName + "\" was not found!");
    }

    /// <summary>Gets the XML content from the specified URL.</summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <returns></returns>
    private string GetResourceXml(string virtualPath)
    {
      string resourceXml = "";
      if (VirtualPathManager.FileExists(virtualPath))
      {
        using (Stream stream = VirtualPathManager.OpenFile(virtualPath))
          resourceXml = stream != null ? new StreamReader(stream).ReadToEnd() : throw new FileNotFoundException("The file: \"" + virtualPath + "\" was not found!");
      }
      else
      {
        string path = SystemManager.CurrentHttpContext.Server.MapPath(virtualPath);
        resourceXml = System.IO.File.Exists(path) ? new StreamReader(path).ReadToEnd() : throw new FileNotFoundException("The file: \"" + virtualPath + "\" was not found!");
      }
      return resourceXml;
    }

    private string GetDefaultResourceXml(string resourceValue) => !resourceValue.Trim().StartsWith("<") ? (!resourceValue.StartsWith("~") ? this.ReadDefaultResourceXml(resourceValue) : this.GetResourceXml(resourceValue)) : resourceValue;

    private void SaveTextEditorToolSetConfiguration(string toolSetName, string toolSetXml)
    {
      string key = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(toolSetName);
      if (!this.ValidateTextEditorToolSet(toolSetXml))
        return;
      ConfigManager manager = ConfigManager.GetManager();
      AppearanceConfig section = manager.GetSection<AppearanceConfig>();
      if (key == "Default tool set")
        section.StandardEditorConfiguration = toolSetXml;
      else if (key == "Tool set for comments")
        section.MinimalEditorConfiguration = toolSetXml;
      else if (key == "Tool set for forums")
      {
        section.ForumsEditorConfiguration = toolSetXml;
      }
      else
      {
        ConfigValueDictionary editorConfigurations = section.EditorConfigurations;
        if (editorConfigurations.ContainsKey(key))
          editorConfigurations[key] = toolSetXml;
        else
          editorConfigurations.Add(key, toolSetXml);
      }
      manager.SaveSection((ConfigSection) section, true);
    }

    private bool ValidateTextEditorToolSet(string toolSetXml)
    {
      try
      {
        new XmlDocument().LoadXml(toolSetXml);
        return true;
      }
      catch (Exception ex)
      {
        this.TextEditorToolSetResult = ex.Message;
        return false;
      }
    }

    private void DeleteTextEditorToolSet(string toolSetName)
    {
      ConfigManager manager = ConfigManager.GetManager();
      AppearanceConfig section = manager.GetSection<AppearanceConfig>();
      ConfigValueDictionary editorConfigurations = section.EditorConfigurations;
      bool flag = true;
      if (toolSetName == "Default tool set")
        section.StandardEditorConfiguration = "Telerik.Sitefinity.Resources.Themes.StandardToolsFile.xml";
      else if (toolSetName == "Tool set for comments")
        section.MinimalEditorConfiguration = "Telerik.Sitefinity.Resources.Themes.MinimalToolsFile.xml";
      else if (toolSetName == "Tool set for forums")
        section.ForumsEditorConfiguration = "Telerik.Sitefinity.Resources.Themes.ForumsToolsFile.xml";
      else if (editorConfigurations.ContainsKey(toolSetName))
        editorConfigurations.Remove(toolSetName);
      else
        flag = false;
      if (!flag)
        return;
      manager.SaveSection((ConfigSection) section, true);
    }

    [System.Flags]
    internal enum ConfigActionOptions
    {
      Defaults = 0,
      GetNested = 1,
      SkipAttributes = 2,
      SkipElements = 4,
      FullInfo = 8,
    }

    internal enum ConfigUIMode
    {
      Auto,
      Navigation,
      Form,
      New,
      Collection,
    }

    internal class FormValueWrapper
    {
      public FormValueWrapper(object value, string encryption)
      {
        this.Value = value;
        this.Encryption = encryption;
      }

      public object Value { get; private set; }

      public string Encryption { get; private set; }
    }

    /// <summary>UI Config element class.</summary>
    internal class UIConfigElement
    {
      internal ConfigElement Element { get; set; }

      internal bool IsNew { get; set; }
    }

    internal class UINameValue
    {
      public string Key { get; set; }

      public string Value { get; set; }

      internal bool IsNew { get; set; }

      internal object BaseObject { get; set; }

      internal PropertyInfo ContainingProperty { get; set; }

      internal string Encryption { get; set; }
    }

    /// <summary>Config section item class for use in the UI.</summary>
    public class UISectionItem : IFormElement
    {
      /// <summary>Creates a instance of UISectionItem</summary>
      public UISectionItem() => this.Description = string.Empty;

      /// <summary>Gets/Sets the title of node.</summary>
      public string Title { get; set; }

      /// <summary>Gets/Sets the parent node.</summary>
      public string Parent { get; set; }

      /// <summary>Gets/Sets the key for config item.</summary>
      public string Key { get; set; }

      /// <summary>Gets/Sets the value for config item.</summary>
      public string Value { get; set; }

      /// <summary>Gets/ Sets if the node has more items.</summary>
      public bool HasChildren { get; set; }

      /// <summary>Marks if the item is new.</summary>
      public bool IsNew { get; set; }

      /// <summary>Marks if a property / element is disabled.</summary>
      public bool Disabled { get; set; }

      /// <summary>Defines if the item can be deleted.</summary>
      public bool CanDelete { get; set; }

      /// <summary>Gets or sets the description.</summary>
      /// <value>The description.</value>
      public string Description { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is collection.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if this instance is collection; otherwise, <c>false</c>.
      /// </value>
      public bool IsCollection { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance can order.
      /// </summary>
      /// <value><c>true</c> if this instance can order; otherwise, <c>false</c>.</value>
      public bool CanOrder { get; set; }

      /// <summary>
      /// Gets or sets the element
      /// CSS class.
      /// </summary>
      /// <value>The element CSS class.</value>
      public string ElementCssClass { get; set; }

      /// <summary>Gets or sets the name of the item type.</summary>
      /// <value>The name of the item type.</value>
      public string ItemTypeName { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [needs editor].
      /// </summary>
      /// <value><c>true</c> if [needs editor]; otherwise, <c>false</c>.</value>
      public bool NeedsEditor { get; set; }

      /// <summary>
      /// Gets or sets a value indicating where the item is persited.
      /// </summary>
      /// <value>The location where the item is persited</value>
      public int SaveLocation { get; set; }

      /// <summary>Gets or sets the default value for the config item.</summary>
      /// <value>The default value for the config item.</value>
      public string DefaultValue { get; set; }

      /// <summary>
      /// Gets or sets whether the current section item is modified config property with default value from assembly.
      /// </summary>
      /// <value>True or false depending on whether the ui item is modified config property with default value from assembly.</value>
      public bool IsModifiedDefault { get; set; }

      /// <summary>
      /// Gets or sets whether the current section item is key property for the config element.
      /// </summary>
      /// <value>True if current property is key for the config element, false if not</value>
      public bool IsConfigElementKey { get; set; }

      /// <summary>
      /// Gets or sets the collection key if item is part of collection.
      /// </summary>
      /// <value>The collection key.</value>
      public string CollectionKey { get; set; }

      /// <summary>Gets or sets the encryption.</summary>
      /// <value>The encryption.</value>
      public string Encryption { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is externally defined.
      /// </summary>
      /// <value>
      ///   <c>true</c> if this instance is externally defined; otherwise, <c>false</c>.
      /// </value>
      public bool IsExternallyDefined { get; set; }

      /// <summary>Gets or sets the property path.</summary>
      /// <value>The property path.</value>
      public string PropertyPath { get; set; }

      /// <summary>
      /// Gets or sets whether the property can persist values for different sites.
      /// </summary>
      public bool IsSiteSpecific { get; set; }
    }

    internal class DefaultProperty
    {
      private int index;
      private bool initialized;
      private string itemNameFormat;
      private PropertyDescriptor defaultProp;

      public DefaultProperty(string itemNameFormat) => this.itemNameFormat = itemNameFormat;

      public string GetValue(object target)
      {
        this.Init(target);
        return this.defaultProp != null ? this.defaultProp.GetValue(target).ToString() : string.Format(this.itemNameFormat, (object) this.index++);
      }

      private void Init(object target)
      {
        if (this.initialized)
          return;
        this.defaultProp = TypeDescriptor.GetDefaultProperty(target);
        if (this.defaultProp == null)
        {
          foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(target))
          {
            ConfigurationPropertyAttribute attribute = (ConfigurationPropertyAttribute) property.Attributes[typeof (ConfigurationPropertyAttribute)];
            if (attribute != null && attribute.IsKey)
            {
              this.defaultProp = property;
              break;
            }
          }
        }
        this.initialized = true;
      }
    }

    private class SectionContext
    {
      public string NodePath { get; set; }

      public object TargetElementWithDefaults { get; set; }

      public ConfigSection Section { get; set; }

      public bool IsParametersCollection { get; set; }

      public int ParametersCollectionParentSaveLocation { get; set; }

      public bool CanBeMoved { get; set; }

      public bool CanBeModified { get; set; }

      public string Provider { get; internal set; }

      public ConfigSectionItems.ConfigUIMode Mode { get; internal set; }

      public Type ElementType { get; internal set; }

      public IEnumerable<Type> CollectionElementTypes { get; internal set; }

      public ConfigSection DefaultSection { get; internal set; }

      public bool PersistsSiteSpecificValues { get; internal set; }
    }
  }
}
