// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Configuration.PublishingConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Publishing.Data;
using Telerik.Sitefinity.Publishing.Twitter;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Publishing.Configuration
{
  /// <summary>Main configuration for Sitefinity's publishing system</summary>
  [DescriptionResource(typeof (PublishingMessages), "PublishingConfig")]
  public class PublishingConfig : ConfigSection, IContentViewConfig
  {
    public const string DefaultProviderName = "OAPublishingProvider";
    public const string SearchProviderName = "SearchPublishingProvider";

    /// <summary>
    /// Gets or sets the name of the default data provider that is used to manage pages.
    /// </summary>
    [DescriptionResource(typeof (PublishingMessages), "DefaultProvider")]
    [ConfigurationProperty("defaultProvider", DefaultValue = "OAPublishingProvider")]
    public virtual string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>Gets or sets the default provider settings</summary>
    [DescriptionResource(typeof (PublishingMessages), "ProviderSettings")]
    [ConfigurationProperty("providerSettings")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> ProviderSettings
    {
      get => (ConfigElementDictionary<string, DataProviderSettings>) this["providerSettings"];
      set => this["providerSettings"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the query string key used to look up provider name from query string
    /// </summary>
    [DescriptionResource(typeof (PublishingMessages), "ProviderNameQueryKey")]
    [ConfigurationProperty("providerNameQueryKey", DefaultValue = "publishingProvider")]
    public virtual string ProviderNameQueryKey
    {
      get => (string) this["providerNameQueryKey"];
      set => this["providerNameQueryKey"] = (object) value;
    }

    /// <summary>Gets or sets the username template</summary>
    /// <remarks>{FirstName} is a placeholder for the first name and {LastName} is a placeholder for the last name.</remarks>
    [DescriptionResource(typeof (PublishingMessages), "UserNameFormat")]
    [ConfigurationProperty("userNameFormat", DefaultValue = "{FirstName} {LastName}")]
    public virtual string UserNameFormat
    {
      get => (string) this["userNameFormat"];
      set => this["userNameFormat"] = (object) value;
    }

    /// <summary>
    /// Gets or sets is the author's mail will be exposed in the RSS feed
    /// </summary>
    [DescriptionResource(typeof (PublishingMessages), "ExposeAuthorEmailInFeeds")]
    [ConfigurationProperty("exposeAuthorEmailInFeeds", DefaultValue = false)]
    public virtual bool ExposeAuthorEmailInFeeds
    {
      get => (bool) this["exposeAuthorEmailInFeeds"];
      set => this["exposeAuthorEmailInFeeds"] = (object) value;
    }

    /// <summary>Gets a collection of data Content View Controls.</summary>
    [ConfigurationProperty("contentViewControls")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlsDescription", Title = "ContentViewControls")]
    [ConfigurationCollection(typeof (ContentViewControlElement), AddItemName = "contentViewControl")]
    public ConfigElementDictionary<string, ContentViewControlElement> ContentViewControls => (ConfigElementDictionary<string, ContentViewControlElement>) this["contentViewControls"];

    /// <summary>Gets a collection of types.</summary>
    [ConfigurationProperty("contentPipeTypes")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentPipeTypesDescription", Title = "ContentPipeTypes")]
    public ConfigElementList<TypeConfigElement> ContentPipeTypes
    {
      get => (ConfigElementList<TypeConfigElement>) this["contentPipeTypes"];
      set => this["contentPipeTypes"] = (object) value;
    }

    [DescriptionResource(typeof (PublishingMessages), "feedsBaseUrl")]
    [ConfigurationProperty("feedsBaseUrl", DefaultValue = "feeds")]
    public virtual string FeedsBaseURl
    {
      get => (string) this["feedsBaseUrl"];
      set => this["feedsBaseUrl"] = (object) value;
    }

    [ObjectInfo(typeof (PublishingMessages), Title = "FeedsOutputCacheProfileName")]
    [ConfigurationProperty("feedsOutputCacheProfileName")]
    public virtual string FeedsOutputCacheProfileName
    {
      get => (string) this["feedsOutputCacheProfileName"];
      set => this["feedsOutputCacheProfileName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to transform RSS XML to HTML when render rss feed at a browser.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [transform RSS XML to HTML]; otherwise, <c>false</c>.
    /// </value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TransformRssXmlToHtmlDescription", Title = "TransformRssXmlToHtml")]
    [ConfigurationProperty("transformRssXmlToHtml", DefaultValue = false)]
    public virtual bool TransformRssXmlToHtml
    {
      get => (bool) this["transformRssXmlToHtml"];
      set => this["transformRssXmlToHtml"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to transform RSS XML to HTML when render rss feed at a browser.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [transform RSS XML to HTML]; otherwise, <c>false</c>.
    /// </value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableDtdProcessingDescription", Title = "EnableDtdProcessing")]
    [ConfigurationProperty("enabledtdprocessing", DefaultValue = false)]
    public virtual bool EnableDtdProcessing
    {
      get => (bool) this["enabledtdprocessing"];
      set => this["enabledtdprocessing"] = (object) value;
    }

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.ProviderSettings.Add(new DataProviderSettings((ConfigElement) this.ProviderSettings)
      {
        Name = "OAPublishingProvider",
        Description = "A provider that stores publishing data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessPublishingProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/Publishing"
          }
        }
      });
      this.ProviderSettings.Add(new DataProviderSettings((ConfigElement) this.ProviderSettings)
      {
        Name = "SearchPublishingProvider",
        Description = "A provider that stores search indexers publishing configuration.",
        ProviderType = typeof (OpenAccessPublishingProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/Search"
          }
        }
      });
      this.ContentViewControls.Add(PublishingDefinitions.DefineBackendContentView((ConfigElement) this.ContentViewControls));
      this.ContentViewControls.Add(TwitterDefinitions.DefineBackendContentView((ConfigElement) this.ContentViewControls));
      this.ContentViewControls.Add(TwitterUrlShorteningDefinitions.DefineBackendContentView((ConfigElement) this.ContentViewControls));
    }

    private static class PropertyNames
    {
      public const string DefaultProvider = "defaultProvider";
      public const string ProviderSettings = "providerSettings";
      public const string UserNameFormat = "userNameFormat";
      public const string ExposeAuthorEmailInFeeds = "exposeAuthorEmailInFeeds";
      public const string FeedsBaseUrl = "feedsBaseUrl";
      public const string ProviderNameQueryKey = "providerNameQueryKey";
      public const string ContentPipeTypes = "contentPipeTypes";
      public const string TransformRssXmlToHtml = "transformRssXmlToHtml";
      public const string FeedsOutputCacheProfileName = "feedsOutputCacheProfileName";
      public const string EnableDtdProcessing = "enabledtdprocessing";
    }
  }
}
