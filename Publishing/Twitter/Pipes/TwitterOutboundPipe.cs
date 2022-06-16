// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.TwitterFeedOutboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using LinqToTwitter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model.Publishing.Model.Twitter;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UrlShorteners;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Twitter Outbound Pipe</summary>
  [PipeDesigner(null, typeof (TwitterPipeDesignerView))]
  public class TwitterFeedOutboundPipe : TwitterPipeBase, IPushPipe, IOutboundPipe
  {
    /// <summary>Pipe Name</summary>
    public const string PipeName = "Twitter";

    /// <summary>Pushes the data.</summary>
    /// <param name="items">The items.</param>
    public virtual void PushData(IList<PublishingSystemEventInfo> items)
    {
      List<WrapperObject> internalFormat = new List<WrapperObject>();
      List<WrapperObject> wrapperObjectList = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo publishingSystemEventInfo in (IEnumerable<PublishingSystemEventInfo>) items)
      {
        WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, publishingSystemEventInfo.Item);
        wrapperObject.Language = publishingSystemEventInfo.Language;
        if (this.PipeSettings.LanguageIds.Count <= 0 || wrapperObject.Language == null || this.PipeSettings.LanguageIds.Contains(wrapperObject.Language))
        {
          string itemAction = publishingSystemEventInfo.ItemAction;
          if (!(itemAction == "SystemObjectDeleted"))
          {
            if (!(itemAction == "SystemObjectAdded"))
            {
              if (itemAction == "SystemObjectModified")
              {
                internalFormat.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
                {
                  wrapperObject
                });
                wrapperObjectList.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
                {
                  wrapperObject
                });
              }
            }
            else
              internalFormat.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
              {
                wrapperObject
              });
          }
          else
            wrapperObjectList.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
            {
              wrapperObject
            });
        }
      }
      this.PublishTwitterItems((IEnumerable<WrapperObject>) internalFormat);
    }

    /// <summary>Send items to twitter</summary>
    /// <param name="internalFormat">items</param>
    protected void PublishTwitterItems(IEnumerable<WrapperObject> internalFormat)
    {
      TwitterContext ctx = new TwitterContext((ITwitterAuthorizer) this.GetAutorizer());
      foreach (WrapperObject values in internalFormat)
      {
        string status = this.PrepareTweet(values);
        ctx.UpdateStatus(status);
      }
    }

    /// <summary>Convert Publishing Point Item to tweet</summary>
    /// <param name="values"></param>
    /// <returns></returns>
    protected virtual string PrepareTweet(WrapperObject values)
    {
      string property = (string) values.GetProperty("Title");
      string str1 = property != null ? Regex.Replace(property, "<[^>]*>", string.Empty) : string.Empty;
      string str2 = (string) values.GetProperty("Link");
      if (string.IsNullOrEmpty(str2))
        str2 = this.GetItemUrl(values);
      if (!string.IsNullOrEmpty(str2))
      {
        if (str2.StartsWith("~/"))
          str2 = RouteHelper.ResolveUrl(str2, UrlResolveOptions.Absolute);
        try
        {
          IUrlShortener urlShortener = ObjectFactory.Resolve<IUrlShortener>("BitLy");
          urlShortener.Initialize();
          str2 = urlShortener.ShortenUrl(str2);
        }
        catch (Exception ex)
        {
        }
      }
      string str3 = string.Format(" {0}", (object) str2);
      int val2 = 140 - str3.Length;
      string str4 = "";
      if (val2 > 0)
        str4 = str1.Substring(0, Math.Min(str1.Length, val2));
      return str4 + str3;
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

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public static List<Mapping> GetDefaultMappings() => new List<Mapping>()
    {
      PublishingSystemFactory.CreateMapping("Content", string.Empty, true, "Title"),
      PublishingSystemFactory.CreateMapping("Link", "UrlShortenerTranslator", true, "Link")
    };

    /// <summary>Get Item URL</summary>
    /// <param name="parsedItem">item </param>
    /// <returns></returns>
    public string GetItemUrl(WrapperObject parsedItem)
    {
      object obj1 = (object) null;
      object obj2 = (object) null;
      object obj3 = (object) null;
      if (parsedItem.HasProperty("PipeId"))
        obj1 = parsedItem.GetProperty("PipeId");
      if (parsedItem.HasProperty("OriginalItemId"))
        obj2 = parsedItem.GetProperty("OriginalItemId");
      if (parsedItem.HasProperty("Link"))
        obj3 = parsedItem.GetProperty("Link");
      if (obj2 != null && obj1 != null && !obj1.ToString().IsNullOrEmpty() && obj1.ToString().IsGuid() && obj1 != (object) Guid.Empty.ToString() && (obj3 == null || string.IsNullOrEmpty((string) obj3)) && obj3 == null)
      {
        Guid result;
        if (!Guid.TryParse(obj1.ToString(), out result))
          return string.Empty;
        PipeSettings pipeSettings = this.GetPipeSettings(result);
        if (pipeSettings != null && pipeSettings is SitefinityContentPipeSettings)
        {
          Guid? backLinksPageId = ((SitefinityContentPipeSettings) pipeSettings).BackLinksPageId;
          if (backLinksPageId.HasValue && SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(backLinksPageId.ToString()) is PageSiteNode siteMapNodeFromKey)
          {
            IManager mappedManager1 = ManagerBase.GetMappedManager(((SitefinityContentPipeSettings) pipeSettings).ContentTypeName);
            Type itemType = TypeResolutionService.ResolveType(((SitefinityContentPipeSettings) pipeSettings).ContentTypeName, false);
            foreach (string providerName in mappedManager1.GetProviderNames(ProviderBindingOptions.NoFilter))
            {
              IManager mappedManager2 = ManagerBase.GetMappedManager(((SitefinityContentPipeSettings) pipeSettings).ContentTypeName, providerName);
              IEnumerator enumerator = mappedManager2.GetItems(itemType, "OriginalContentId == " + obj2.ToString() + " and Status==Live", "", 0, 0).GetEnumerator();
              if (enumerator.MoveNext())
              {
                string empty = string.Empty;
                CultureInfo cultureInfo = CultureInfo.GetCultureInfo(parsedItem.Language);
                CultureInfo culture = SystemManager.CurrentContext.Culture;
                string withoutExtension;
                try
                {
                  SystemManager.CurrentContext.Culture = cultureInfo;
                  withoutExtension = siteMapNodeFromKey.UrlWithoutExtension;
                }
                finally
                {
                  SystemManager.CurrentContext.Culture = culture;
                }
                return RouteHelper.ResolveUrl(withoutExtension, UrlResolveOptions.Absolute) + ((UrlDataProviderBase) mappedManager2.Provider).GetItemUrl((ILocatable) enumerator.Current);
              }
            }
          }
        }
      }
      return obj3 != null ? obj3.ToString() : string.Empty;
    }

    /// <summary>
    /// Used to get the default settings for this pipe, without creating persistent object
    /// </summary>
    /// <returns>The settings</returns>
    public static TwitterPipeSettings GetTemplatePipeSettings()
    {
      TwitterPipeSettings templatePipeSettings = new TwitterPipeSettings();
      templatePipeSettings.PipeName = "Twitter";
      templatePipeSettings.IsInbound = false;
      templatePipeSettings.IsActive = true;
      templatePipeSettings.InvocationMode = PipeInvokationMode.Push;
      return templatePipeSettings;
    }
  }
}
