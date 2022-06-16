// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.RSSOutboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Summary;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  [PipeDesigner(null, typeof (RssAtomPipeDesignerView))]
  public class RSSOutboundPipe : BasePipe<RssPipeSettings>, IOutboundPipe, IPullPipe
  {
    private Dictionary<string, object> additionalSettings;
    private IDefinitionField[] definition;
    public const string PipeName = "RSSOutboundPipe";

    /// <summary>Gets the content settings.</summary>
    /// <value>The content settings.</value>
    protected virtual SitefinityContentPipeSettings ContentSettings => (SitefinityContentPipeSettings) this.GetDefaultSettings();

    /// <summary>
    /// Gets or sets a collection of context items for the pipe.
    /// These are usually runtime settings that affect the pipe execution or temporary items that are valid only for the lifetime of the pipe.
    /// For example for a universal syndication pipe - this can specify whether the syndication clients requires the feed output in Rss or Atom.
    /// </summary>
    /// <value>The pipe context.</value>
    public virtual Dictionary<string, object> PipeContext
    {
      get
      {
        if (this.additionalSettings == null)
          this.additionalSettings = new Dictionary<string, object>();
        return this.additionalSettings;
      }
    }

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => nameof (RSSOutboundPipe);

    /// <summary>
    /// Used to get the default settings for this pipe, without creating persistent object
    /// </summary>
    /// <returns>The settings</returns>
    public static RssPipeSettings GetTemplatePipeSettings()
    {
      RssPipeSettings templatePipeSettings = new RssPipeSettings();
      templatePipeSettings.PipeName = nameof (RSSOutboundPipe);
      templatePipeSettings.MaxItems = 25;
      templatePipeSettings.OutputSettings = RssContentOutputSetting.TitleAndContent;
      templatePipeSettings.FormatSettings = RssFormatOutputSettings.RssOnly;
      templatePipeSettings.IsActive = true;
      templatePipeSettings.IsInbound = false;
      templatePipeSettings.InvocationMode = PipeInvokationMode.Pull;
      return templatePipeSettings;
    }

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public static List<Mapping> GetDefaultMappings() => new List<Mapping>()
    {
      PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, "Title"),
      PublishingSystemFactory.CreateMapping("PublicationDate", "TransparentTranslator", false, "PublicationDate"),
      PublishingSystemFactory.CreateMapping("Link", "TransparentTranslator", true, "Link"),
      PublishingSystemFactory.CreateMapping("Summary", "concatenationtranslator", true, "Summary"),
      PublishingSystemFactory.CreateMapping("Content", "concatenationtranslator", true, "Content"),
      PublishingSystemFactory.CreateMapping("Categories", "TransparentTranslator", false, "Categories"),
      PublishingSystemFactory.CreateMapping("Username", "TransparentTranslator", false, "Username"),
      PublishingSystemFactory.CreateMapping("OwnerFirstName", "TransparentTranslator", false, "OwnerFirstName"),
      PublishingSystemFactory.CreateMapping("OwnerLastName", "TransparentTranslator", false, "OwnerLastName"),
      PublishingSystemFactory.CreateMapping("OwnerEmail", "TransparentTranslator", false, "OwnerEmail"),
      PublishingSystemFactory.CreateMapping("OriginalItemId", "TransparentTranslator", true, "OriginalItemId"),
      PublishingSystemFactory.CreateMapping("OriginalParentId", "TransparentTranslator", false, "OriginalParentId"),
      PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", false, "PipeId")
    };

    /// <summary>
    /// Defines the data structure of the medium this pipe works with
    /// </summary>
    /// <value></value>
    public override IDefinitionField[] Definition
    {
      get
      {
        if (this.definition == null)
          this.definition = PublishingSystemFactory.GetPipeDefinitions(this.Name);
        return this.definition;
      }
    }

    /// <summary>Gets the converted items for mapping.</summary>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    public virtual IEnumerable<WrapperObject> GetConvertedItemsForMapping(
      params object[] items)
    {
      List<WrapperObject> convertedItemsForMapping = new List<WrapperObject>();
      foreach (object theInstance in items)
      {
        WrapperObject wrapperObject = new WrapperObject(theInstance);
        IEnumerable<SyndicationPerson> authors = this.GetAuthors((IEnumerable<WrapperObject>) new List<WrapperObject>()
        {
          new WrapperObject((object) items)
        });
        wrapperObject.AddProperty("Contributors", (object) new List<SyndicationPerson>());
        foreach (SyndicationPerson syndicationPerson in authors)
          ((List<SyndicationPerson>) wrapperObject.GetProperty("Contributors")).Add(syndicationPerson);
        wrapperObject.AddProperty("Categories", (object) new List<SyndicationCategory>());
        foreach (SyndicationCategory category in this.GetCategories((IEnumerable<WrapperObject>) new List<WrapperObject>()
        {
          new WrapperObject((object) items)
        }))
          ((List<SyndicationCategory>) wrapperObject.GetProperty("Categories")).Add(category);
        wrapperObject.AddProperty("Description", (object) SyndicationContent.CreatePlaintextContent((string) this.PipeSettingsInternal.PublishingPoint.Description));
        wrapperObject.AddProperty("Title", (object) SyndicationContent.CreatePlaintextContent(this.PipeSettingsInternal.PublishingPoint.Name));
        wrapperObject.AddProperty("Items", (object) this.BuildSyndicationItems((IEnumerable<WrapperObject>) new List<WrapperObject>()
        {
          new WrapperObject((object) items)
        }));
        convertedItemsForMapping.Add(wrapperObject);
      }
      return (IEnumerable<WrapperObject>) convertedItemsForMapping;
    }

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public override void Initialize(PipeSettings pipeSettings) => this.PipeSettingsInternal = pipeSettings is RssPipeSettings ? (RssPipeSettings) pipeSettings : throw new ArgumentException("Expected pipe settings of type RssPipeSettings.");

    /// <summary>
    /// Determines whether this instance [can process item] the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// 	<c>true</c> if this instance [can process item] the specified item; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanProcessItem(object item) => true;

    /// <summary>Gets the item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="itemHash">The item hash.</param>
    /// <returns></returns>
    protected virtual IContent GetItem(Type itemType, IManager manager, string itemHash)
    {
      if (string.IsNullOrEmpty(itemHash))
        return manager.CreateItem(itemType) as IContent;
      IEnumerator enumerator = manager.GetItems(itemType, "SourceKey=\"" + itemHash + "\"", "", 0, 0).GetEnumerator();
      return enumerator.MoveNext() ? (IContent) enumerator.Current : manager.CreateItem(itemType) as IContent;
    }

    /// <summary>Applies the lang filter.</summary>
    /// <param name="wp">The wp.</param>
    /// <returns></returns>
    protected virtual bool ApplyLangFilter(WrapperObject wp)
    {
      if (this.PipeSettingsInternal.LanguageIds.Count<string>() <= 0)
        return true;
      foreach (string languageId in (IEnumerable<string>) this.PipeSettingsInternal.LanguageIds)
      {
        if (wp.Language == languageId || string.IsNullOrEmpty(wp.Language))
          return true;
      }
      return false;
    }

    /// <summary>Gets the data.</summary>
    /// <returns></returns>
    public virtual IQueryable<WrapperObject> GetData()
    {
      IPublishingPointBusinessObject publishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettingsInternal.PublishingPoint);
      IQueryable<WrapperObject> source1 = !(publishingPoint is IQueryablePublishingPoint queryablePublishingPoint) ? publishingPoint.GetPublishingPointItems() : queryablePublishingPoint.GetItems((string) null, (string) null, 0, this.PipeSettings.MaxItems);
      IQueryable<WrapperObject> source2;
      if (source1 == null)
        source2 = Enumerable.Empty<WrapperObject>().AsQueryable<WrapperObject>();
      else
        source2 = source1.Where<WrapperObject>((Expression<Func<WrapperObject, bool>>) (i => this.ApplyLangFilter(i)));
      if (this.PipeSettings.MaxItems > 0)
        source2 = source2.Take<WrapperObject>(this.PipeSettings.MaxItems);
      return new List<WrapperObject>()
      {
        new WrapperObject((object) this.CreateFeedFormatter(this.BuildSyndicationFeed((IEnumerable<WrapperObject>) source2.Select<WrapperObject, WrapperObject>((Expression<Func<WrapperObject, WrapperObject>>) (i => new WrapperObject(this.PipeSettings, i)
        {
          Language = i.Language
        })))))
      }.AsQueryable<WrapperObject>();
    }

    /// <summary>Builds a feed from the given data.</summary>
    /// <param name="internalFormat">The internal format of the data.</param>
    /// <returns>A new SyndicationFeed object</returns>
    protected virtual SyndicationFeed BuildSyndicationFeed(
      IEnumerable<WrapperObject> items)
    {
      SyndicationFeed syndicationFeed = new SyndicationFeed();
      List<WrapperObject> list = items.ToList<WrapperObject>();
      foreach (SyndicationPerson author in this.GetAuthors((IEnumerable<WrapperObject>) list))
        syndicationFeed.Contributors.Add(author);
      foreach (SyndicationCategory category in this.GetCategories((IEnumerable<WrapperObject>) list))
        syndicationFeed.Categories.Add(category);
      syndicationFeed.Description = SyndicationContent.CreatePlaintextContent((string) this.PipeSettingsInternal.PublishingPoint.Description);
      syndicationFeed.Title = SyndicationContent.CreatePlaintextContent(this.PipeSettingsInternal.PublishingPoint.Name);
      syndicationFeed.Items = this.BuildSyndicationItems((IEnumerable<WrapperObject>) list);
      syndicationFeed.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(RouteHelper.ResolveUrl("~/", UrlResolveOptions.Absolute))));
      return syndicationFeed;
    }

    /// <summary>Gets the pipe settings.</summary>
    /// <param name="pipeSettingsId">The pipe settings id.</param>
    /// <returns></returns>
    protected virtual PipeSettings GetPipeSettings(Guid pipeSettingsId)
    {
      PipeSettings pipeSettings = (PipeSettings) null;
      foreach (DataProviderBase staticProvider in (Collection<PublishingDataProviderBase>) PublishingManager.GetManager().StaticProviders)
      {
        pipeSettings = PublishingManager.GetManager(staticProvider.Name).GetPipeSettings().Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (ps => ps.Id == pipeSettingsId)).FirstOrDefault<PipeSettings>();
        if (pipeSettings != null)
          break;
      }
      return pipeSettings;
    }

    /// <summary>
    /// Creates a collection of SyndicationItem objects from the given data.
    /// </summary>
    /// <param name="internalFormat">The internal format of the data.</param>
    /// <returns>A collection of SyndicationItem objects.</returns>
    protected virtual IEnumerable<SyndicationItem> BuildSyndicationItems(
      IEnumerable<WrapperObject> internalFormat)
    {
      IList<SyndicationItem> source = (IList<SyndicationItem>) new List<SyndicationItem>();
      foreach (WrapperObject values in internalFormat)
        source.Add(this.BuildSyndicationItem(values));
      return (IEnumerable<SyndicationItem>) source.OrderByDescending<SyndicationItem, DateTimeOffset>((Func<SyndicationItem, DateTimeOffset>) (s => s.PublishDate));
    }

    private PageSiteNode GetPageNode(Guid? pageId) => SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(pageId.ToString()) as PageSiteNode;

    private bool TryGetPipeSettings(WrapperObject parsedItem, out PipeSettings pipeSetting)
    {
      if (parsedItem.HasProperty("PipeId"))
      {
        object property = parsedItem.GetProperty("PipeId");
        Guid result;
        if (property != null && Guid.TryParse(property.ToString(), out result))
        {
          pipeSetting = this.GetPipeSettings(result);
          return pipeSetting != null;
        }
      }
      pipeSetting = (PipeSettings) null;
      return false;
    }

    private bool TryGetBaseUrl(
      WrapperObject parsedItem,
      PipeSettings pipeSetting,
      out string baseUrl)
    {
      if (pipeSetting is SitefinityContentPipeSettings)
      {
        Guid? backLinksPageId = ((SitefinityContentPipeSettings) pipeSetting).BackLinksPageId;
        if (backLinksPageId.HasValue)
        {
          PageSiteNode pageNode = this.GetPageNode(new Guid?(backLinksPageId.Value));
          if (pageNode != null)
          {
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;
            if (!string.IsNullOrEmpty(parsedItem.Language))
              cultureInfo = CultureInfo.GetCultureInfo(parsedItem.Language);
            CultureInfo culture = SystemManager.CurrentContext.Culture;
            try
            {
              SystemManager.CurrentContext.Culture = cultureInfo;
              baseUrl = RouteHelper.ResolveUrl(pageNode.UrlWithoutExtension, UrlResolveOptions.Absolute);
              return true;
            }
            finally
            {
              SystemManager.CurrentContext.Culture = culture;
            }
          }
        }
      }
      baseUrl = (string) null;
      return false;
    }

    /// <summary>Gets the item URL.</summary>
    /// <param name="parsedItem">The parsed item.</param>
    /// <returns></returns>
    protected virtual string GetItemUrl(WrapperObject parsedItem)
    {
      string url = (string) null;
      if (parsedItem.HasProperty("Link"))
        url = parsedItem.GetProperty("Link") as string;
      if (!string.IsNullOrEmpty(url))
      {
        if (url.StartsWith("~/"))
          return RouteHelper.ResolveUrl(url, UrlResolveOptions.Absolute);
        PipeSettings pipeSetting;
        string baseUrl;
        return this.TryGetPipeSettings(parsedItem, out pipeSetting) && this.TryGetBaseUrl(parsedItem, pipeSetting, out baseUrl) ? baseUrl + url : url;
      }
      object obj = (object) null;
      if (parsedItem.HasProperty("OriginalItemId"))
        obj = parsedItem.GetProperty("OriginalItemId");
      PipeSettings pipeSetting1;
      string baseUrl1;
      if (obj != null && this.TryGetPipeSettings(parsedItem, out pipeSetting1) && pipeSetting1 is SitefinityContentPipeSettings contentPipeSettings && this.TryGetBaseUrl(parsedItem, pipeSetting1, out baseUrl1))
      {
        IManager mappedManager1 = ManagerBase.GetMappedManager(contentPipeSettings.ContentTypeName);
        Type itemType = TypeResolutionService.ResolveType(contentPipeSettings.ContentTypeName, false);
        foreach (string providerName in mappedManager1.GetProviderNames(ProviderBindingOptions.NoFilter))
        {
          IManager mappedManager2 = ManagerBase.GetMappedManager(((SitefinityContentPipeSettings) pipeSetting1).ContentTypeName, providerName);
          IEnumerator enumerator = mappedManager2.GetItems(itemType, "OriginalContentId == " + obj.ToString() + " and Status==Live", "", 0, 0).GetEnumerator();
          if (enumerator.MoveNext())
          {
            string itemUrl = ((UrlDataProviderBase) mappedManager2.Provider).GetItemUrl((ILocatable) enumerator.Current);
            return baseUrl1 + itemUrl;
          }
        }
      }
      return url != null ? url.ToString() : string.Empty;
    }

    /// <summary>Creates a SyndicationItem object from the given data.</summary>
    /// <param name="values">The data used to build a SyndicationItem object.</param>
    /// <returns>A SyndicationItem object.</returns>
    protected virtual SyndicationItem BuildSyndicationItem(WrapperObject values)
    {
      SyndicationItem syndicationItem = new SyndicationItem();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) values);
      string text1 = PublishingHelper.SanitizeStringForXml(this.GetPropertyValue((object) values, properties, "Title"));
      syndicationItem.Title = new TextSyndicationContent(text1);
      if (this.PipeSettingsInternal.OutputSettings != RssContentOutputSetting.TitleOnly && properties.Find("Content", true) != null)
      {
        string str = this.GetPropertyValue((object) values, properties, "Content");
        try
        {
          str = LinkParser.ResolveLinks(str, new Telerik.Sitefinity.Web.Utilities.GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, false, true);
        }
        catch (Exception ex)
        {
        }
        if (this.PipeSettingsInternal.OutputSettings == RssContentOutputSetting.TitleAndTruncatedContent)
        {
          str = SummaryParser.GetSummary(str, new SummarySettings(SummaryMode.None, -1, true, Array.Empty<string>()));
          if (str.Length > this.PipeSettingsInternal.ContentSize)
            str = this.PipeSettingsInternal.ContentSize != 0 ? str.Substring(0, this.PipeSettingsInternal.ContentSize) + "..." : string.Empty;
        }
        string text2 = PublishingHelper.SanitizeStringForXml(str);
        syndicationItem.Content = (SyndicationContent) new TextSyndicationContent(text2);
      }
      if (properties.Find("Link", true) != null)
      {
        string str = this.GetItemUrl(values);
        if (!string.IsNullOrEmpty(str))
        {
          if (str.IndexOf(':') == -1)
            str = RouteHelper.ResolveUrl(str, UrlResolveOptions.Absolute);
          syndicationItem.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(str)));
        }
      }
      PropertyDescriptor propertyDescriptor1 = properties.Find("OwnerFirstName", true);
      PropertyDescriptor propertyDescriptor2 = properties.Find("OwnerLastName", true);
      if (propertyDescriptor1 != null && propertyDescriptor2 != null)
      {
        string empty = string.Empty;
        PropertyDescriptor propertyDescriptor3 = properties.Find("OwnerEmail", true);
        if (Config.Get<PublishingConfig>().ExposeAuthorEmailInFeeds && propertyDescriptor3 != null)
          empty = (string) propertyDescriptor3.GetValue((object) values);
        syndicationItem.Authors.Add(new SyndicationPerson(empty, (string) propertyDescriptor1.GetValue((object) values) + " " + (string) propertyDescriptor2.GetValue((object) values), (string) null));
      }
      PropertyDescriptor propertyDescriptor4 = properties.Find("PublicationDate", true);
      if (propertyDescriptor4 != null)
      {
        object obj = propertyDescriptor4.GetValue((object) values);
        if (obj != null)
          syndicationItem.PublishDate = new DateTimeOffset(((DateTime) obj).ToUniversalTime());
      }
      PropertyDescriptor propertyDescriptor5 = properties.Find("OriginalItemId", true);
      if (propertyDescriptor5 != null)
      {
        object obj = propertyDescriptor5.GetValue((object) values);
        if (obj != null)
          syndicationItem.Id = string.Format("urn:uuid:{0}", (object) obj.ToString());
      }
      PropertyDescriptor propertyDescriptor6 = properties.Find("Categories", true);
      if (propertyDescriptor6 != null)
      {
        object obj = propertyDescriptor6.GetValue((object) values);
        if (obj != null)
        {
          foreach (string name in (IEnumerable<string>) obj)
            syndicationItem.Categories.Add(new SyndicationCategory(name));
        }
      }
      PropertyDescriptor propertyDescriptor7 = properties.Find("Summary", true);
      if (propertyDescriptor7 != null)
      {
        object obj = propertyDescriptor7.GetValue((object) values);
        if (obj != null && !obj.ToString().IsNullOrEmpty())
        {
          string text3 = PublishingHelper.SanitizeStringForXml(obj.ToString());
          syndicationItem.Summary = new TextSyndicationContent(text3);
        }
      }
      return syndicationItem;
    }

    /// <summary>Gets the property value.</summary>
    /// <param name="values">The values.</param>
    /// <param name="properties">The properties.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    protected virtual string GetPropertyValue(
      object values,
      PropertyDescriptorCollection properties,
      string propertyName)
    {
      object obj = properties.Find(propertyName, true).GetValue(values);
      return obj != null ? obj.ToString() : string.Empty;
    }

    /// <summary>
    /// Extracts and returns the authors from the internal format list by
    /// finding non null and not empty values with key PublishingConstants.FieldUsername.
    /// </summary>
    /// <param name="internalFormat">The internal format list.</param>
    /// <returns>A collection with the feed authors.</returns>
    protected virtual IEnumerable<SyndicationPerson> GetAuthors(
      IEnumerable<WrapperObject> internalFormat)
    {
      List<string> stringList = new List<string>();
      List<SyndicationPerson> authors = new List<SyndicationPerson>();
      foreach (WrapperObject wrapperObject in internalFormat)
      {
        if (wrapperObject.HasProperty("Username"))
        {
          string property = (string) wrapperObject.GetProperty("Username");
          if (!string.IsNullOrEmpty(property) && !stringList.Contains(property) && wrapperObject.HasProperty("OwnerFirstName") && wrapperObject.HasProperty("OwnerLastName"))
          {
            string email = string.Empty;
            if (wrapperObject.HasProperty("OwnerEmail") && Config.Get<PublishingConfig>().ExposeAuthorEmailInFeeds)
              email = (string) wrapperObject.GetProperty("OwnerEmail");
            SyndicationPerson syndicationPerson = new SyndicationPerson(email, (string) wrapperObject.GetProperty("OwnerFirstName") + " " + (string) wrapperObject.GetProperty("OwnerLastName"), (string) null);
            authors.Add(syndicationPerson);
            stringList.Add(property);
          }
        }
      }
      return (IEnumerable<SyndicationPerson>) authors;
    }

    /// <summary>
    /// Extracts and returns the categories from the internal format list by
    /// finding non null and not empty comma separated values with key PublishingConstants.FieldCategories.
    /// </summary>
    /// <param name="internalFormat">The internal format list.</param>
    /// <returns>A collection with the feed categories.</returns>
    protected virtual IEnumerable<SyndicationCategory> GetCategories(
      IEnumerable<WrapperObject> internalFormat)
    {
      List<string> categories = new List<string>();
      internalFormat.Where<WrapperObject>((Func<WrapperObject, bool>) (vals => vals.HasProperty("Categories") && vals.GetProperty("Categories") != null)).ToList<WrapperObject>().ForEach((Action<WrapperObject>) (vals =>
      {
        foreach (string str in vals.GetProperty<IEnumerable<string>>("Categories").Distinct<string>())
        {
          if (!categories.Contains(str))
            categories.Add(str);
        }
      }));
      List<SyndicationCategory> categories1 = new List<SyndicationCategory>(categories.Count);
      foreach (string name in categories)
        categories1.Add(new SyndicationCategory(name));
      return (IEnumerable<SyndicationCategory>) categories1;
    }

    /// <summary>
    /// Return appropriate feed formatter according to the pipe settings and the pipe runtime context
    /// </summary>
    /// <param name="feed">The feed that needs a formatter.</param>
    /// <returns>The formatter for the given feed.</returns>
    protected virtual SyndicationFeedFormatter CreateFeedFormatter(
      SyndicationFeed feed)
    {
      SyndicationFeedFormatter feedFormatter = (SyndicationFeedFormatter) null;
      switch (this.PipeSettingsInternal.FormatSettings)
      {
        case RssFormatOutputSettings.SmartFeed:
        case RssFormatOutputSettings.RssAndAtom:
          if (this.PipeContext.ContainsKey("FeedType"))
          {
            feedFormatter = (RssFormatOutputSettings) this.PipeContext["FeedType"] != RssFormatOutputSettings.RssOnly ? (SyndicationFeedFormatter) new Atom10FeedFormatter(feed) : (SyndicationFeedFormatter) new Rss20FeedFormatter(feed);
            break;
          }
          break;
        case RssFormatOutputSettings.RssOnly:
          feedFormatter = (SyndicationFeedFormatter) new Rss20FeedFormatter(feed);
          break;
        case RssFormatOutputSettings.AtomOnly:
          feedFormatter = (SyndicationFeedFormatter) new Atom10FeedFormatter(feed);
          break;
      }
      if (feedFormatter == null)
        throw new NotSupportedException("The rss output format is not supported");
      string uriString = RouteHelper.ResolveUrl("~/" + Config.Get<PublishingConfig>().FeedsBaseURl + "/" + this.PipeSettingsInternal.UrlName, UrlResolveOptions.Absolute);
      string mediaType = feedFormatter is Rss20FeedFormatter ? "application/rss+xml" : "application/atom+xml";
      feedFormatter.Feed.Links.Add(SyndicationLink.CreateSelfLink(new Uri(uriString), mediaType));
      return feedFormatter;
    }

    /// <summary>Gets the feed XML reader.</summary>
    /// <param name="feedUrl">The feed URL.</param>
    /// <returns></returns>
    protected virtual XmlReader GetFeedXmlReader(string feedUrl) => XmlReader.Create(feedUrl);

    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="initSettings">The init settings.</param>
    /// <returns></returns>
    public override string GetPipeSettingsShortDescription(PipeSettings initSettings)
    {
      RssPipeSettings rssPipeSettings = initSettings as RssPipeSettings;
      if (string.IsNullOrEmpty(rssPipeSettings.UrlName))
        return Telerik.Sitefinity.Localization.Res.Get<PublishingMessages>().PipeSettingsUrlNameNotSet;
      if (initSettings.IsInbound)
        return string.Format("{0}<a href=\"{1}\" target=\"_blank\">{1}</a>", (object) Telerik.Sitefinity.Localization.Res.Get<PublishingMessages>().PipeSettingsImporShortDescriptionBase, (object) rssPipeSettings.UrlName);
      string str = VirtualPathUtility.AppendTrailingSlash(PublishingManager.GetFeedsBaseURl()) + HttpUtility.UrlPathEncode(rssPipeSettings.UrlName);
      return string.Format("{0} <a href=\"{1}\" target=\"_blank\">{1}</a>", (object) Telerik.Sitefinity.Localization.Res.Get<PublishingMessages>().PipeSettingsShortDescriptionBase, (object) str);
    }
  }
}
