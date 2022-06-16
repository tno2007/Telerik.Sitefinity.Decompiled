// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.RSSInboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Xml;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Rss inbound pipe</summary>
  [PipeDesigner(typeof (RssAtomPipeImportDesignerView), null)]
  public class RSSInboundPipe : BasePipe<RssPipeSettings>, IPushPipe, IPullPipe, IInboundPipe
  {
    private IDefinitionField[] definition;
    private IPublishingPointBusinessObject publishingPoint;
    public const string PipeName = "RSSInboundPipe";

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => nameof (RSSInboundPipe);

    /// <summary>
    /// Used to get the default settings for this pipe, without creating persistent object
    /// </summary>
    /// <returns>The settings</returns>
    public static RssPipeSettings GetTemplatePipeSettings()
    {
      RssPipeSettings templatePipeSettings = new RssPipeSettings();
      templatePipeSettings.IsInbound = true;
      templatePipeSettings.PipeName = nameof (RSSInboundPipe);
      templatePipeSettings.IsActive = true;
      templatePipeSettings.MaxItems = 25;
      templatePipeSettings.InvocationMode = PipeInvokationMode.Push;
      return templatePipeSettings;
    }

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public static List<Mapping> GetDefaultMappings() => new List<Mapping>()
    {
      PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, "Title"),
      PublishingSystemFactory.CreateMapping("PublicationDate", "TransparentTranslator", false, "PublicationDate"),
      PublishingSystemFactory.CreateMapping("Link", "TransparentTranslator", true, "Link"),
      PublishingSystemFactory.CreateMapping("Content", "concatenationtranslator", true, "Content"),
      PublishingSystemFactory.CreateMapping("Summary", "concatenationtranslator", true, "Summary"),
      PublishingSystemFactory.CreateMapping("Categories", "TransparentTranslator", false, "Categories"),
      PublishingSystemFactory.CreateMapping("Username", "TransparentTranslator", false, "Username"),
      PublishingSystemFactory.CreateMapping("Owner", "TransparentTranslator", false, "Owner"),
      PublishingSystemFactory.CreateMapping("OwnerFirstName", "TransparentTranslator", false, "OwnerFirstName"),
      PublishingSystemFactory.CreateMapping("OwnerLastName", "TransparentTranslator", false, "OwnerLastName"),
      PublishingSystemFactory.CreateMapping("OwnerEmail", "TransparentTranslator", false, "OwnerEmail"),
      PublishingSystemFactory.CreateMapping("Id", "TransparentTranslator", true, "Id"),
      PublishingSystemFactory.CreateMapping("OriginalItemId", "TransparentTranslator", true, "OriginalContentId"),
      PublishingSystemFactory.CreateMapping("OriginalParentId", "TransparentTranslator", false, "OriginalParentId"),
      PublishingSystemFactory.CreateMapping("ExpirationDate", "TransparentTranslator", false, "ExpirationDate"),
      PublishingSystemFactory.CreateMapping("ItemHash", "TransparentTranslator", false, "ItemHash"),
      PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", false, "PipeId"),
      PublishingSystemFactory.CreateMapping("ContentType", "TransparentTranslator", false, "ContentType")
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

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = (RssPipeSettings) pipeSettings;
      this.publishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettingsInternal.PublishingPoint);
    }

    /// <summary>
    /// Determines whether this instance [can process item] the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// 	<c>true</c> if this instance [can process item] the specified item; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanProcessItem(object item) => false;

    /// <summary>Pushes the data.</summary>
    /// <param name="items">The items.</param>
    public virtual void PushData(IList<PublishingSystemEventInfo> items) => this.publishingPoint.AddItems((IList<WrapperObject>) items.Select<PublishingSystemEventInfo, WrapperObject>((Func<PublishingSystemEventInfo, WrapperObject>) (i =>
    {
      object theInstance = i.Item;
      if (theInstance is WrapperObject)
        return (WrapperObject) theInstance;
      return new WrapperObject(theInstance)
      {
        MappingSettings = this.PipeSettings.Mappings,
        Language = i.Language
      };
    })).ToList<WrapperObject>());

    /// <summary>Gets the data.</summary>
    /// <returns></returns>
    public virtual IQueryable<WrapperObject> GetData() => this.ExtractSyndicationItems().AsQueryable<WrapperObject>();

    /// <summary>Gets the feed XML reader.</summary>
    /// <param name="feedUrl">The feed URL.</param>
    /// <returns></returns>
    protected virtual XmlReader GetFeedXmlReader(string feedUrl)
    {
      if (!Config.Get<PublishingConfig>().EnableDtdProcessing)
        return XmlReader.Create(feedUrl);
      XmlReaderSettings settings = new XmlReaderSettings();
      settings.DtdProcessing = DtdProcessing.Parse;
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(feedUrl);
      httpWebRequest.Method = "GET";
      httpWebRequest.UserAgent = "Fiddler";
      return XmlReader.Create(httpWebRequest.GetResponse().GetResponseStream(), settings);
    }

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

    /// <summary>Toes the publishing point.</summary>
    public virtual void ToPublishingPoint()
    {
      List<PublishingSystemEventInfo> items = new List<PublishingSystemEventInfo>();
      foreach (WrapperObject syndicationItem in this.ExtractSyndicationItems())
        items.Add(new PublishingSystemEventInfo()
        {
          Item = (object) syndicationItem,
          ItemAction = "PublisingPointItemImported",
          Language = syndicationItem.Language
        });
      this.PushData((IList<PublishingSystemEventInfo>) items);
    }

    /// <summary>Converts to wraper object.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public virtual WrapperObject ConvertToWraperObject(SyndicationItem item)
    {
      WrapperObject wraperObject = new WrapperObject((object) null);
      wraperObject.MappingSettings = this.PipeSettings.Mappings;
      wraperObject.Language = this.PipeSettings.LanguageIds.FirstOrDefault<string>();
      wraperObject.AddProperty("Title", (object) item.Title.Text);
      wraperObject.AddProperty("PublicationDate", (object) item.PublishDate.UtcDateTime);
      string str1 = (string) null;
      string str2 = (string) null;
      if (item.Content != null && item.Content is TextSyndicationContent content)
        str1 = content.Text;
      if (item.Summary != null)
        str2 = item.Summary.Text;
      if (str1 != null)
      {
        wraperObject.AddProperty("Content", (object) str1);
        wraperObject.AddProperty("Summary", (object) str2);
      }
      else
        wraperObject.AddProperty("Content", (object) str2);
      string str3 = "";
      if (item.Links.Count > 0)
        str3 = item.Links[0].Uri.AbsoluteUri;
      wraperObject.AddProperty("Link", (object) str3);
      if (item.Authors.Count > 0)
      {
        SyndicationPerson author = item.Authors[0];
        wraperObject.AddProperty("OwnerEmail", (object) author.Email);
        wraperObject.AddProperty("OwnerLastName", (object) author.Name);
      }
      wraperObject.AddProperty("ItemHash", (object) this.GenerateItemHash(item, this.PipeSettingsInternal.UrlName));
      if (item.Categories.Count > 0)
        wraperObject.AddProperty("Categories", (object) item.Categories.Select<SyndicationCategory, string>((Func<SyndicationCategory, string>) (i => i.Name ?? i.Label)));
      wraperObject.AddProperty("ExpirationDate", (object) null);
      return wraperObject;
    }

    /// <summary>Generates the item hash.</summary>
    /// <param name="item">The item.</param>
    /// <param name="feedUrl">The feed URL.</param>
    /// <returns></returns>
    protected virtual string GenerateItemHash(SyndicationItem item, string feedUrl)
    {
      StringBuilder stringBuilder = new StringBuilder(1000);
      stringBuilder.Append(item.Title.Text);
      stringBuilder.Append("|");
      stringBuilder.Append(item.PublishDate.UtcDateTime.ToString("dd/MMM/yyyy hh:mm:ss"));
      if (item.Links.Count > 0)
      {
        stringBuilder.Append("|");
        stringBuilder.Append(item.Links[0].Uri.AbsoluteUri);
      }
      if (item.SourceFeed != null)
      {
        stringBuilder.Append("|");
        stringBuilder.Append(item.SourceFeed.Title.Text);
      }
      stringBuilder.Append("|");
      stringBuilder.Append(feedUrl);
      return Convert.ToBase64String(new SHA1CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(stringBuilder.ToString())));
    }

    /// <summary>Extracts the syndication items.</summary>
    /// <returns></returns>
    protected virtual IEnumerable<WrapperObject> ExtractSyndicationItems()
    {
      RSSInboundPipe rssInboundPipe = this;
      string urlName = rssInboundPipe.PipeSettingsInternal.UrlName;
      if (string.IsNullOrEmpty(rssInboundPipe.PipeSettingsInternal.UrlName))
        throw new ApplicationException("Missing feed url name in the pipe settings. SyndicationPipe cannot import the feed.");
      SyndicationFeed importFeed = SyndicationFeed.Load(rssInboundPipe.GetFeedXmlReader(urlName));
      foreach (SyndicationItem syndicationItem in rssInboundPipe.PipeSettingsInternal.MaxItems <= 0 ? importFeed.Items : importFeed.Items.Take<SyndicationItem>(rssInboundPipe.PipeSettingsInternal.MaxItems))
      {
        syndicationItem.SourceFeed = importFeed;
        yield return rssInboundPipe.ConvertToWraperObject(syndicationItem);
      }
    }
  }
}
