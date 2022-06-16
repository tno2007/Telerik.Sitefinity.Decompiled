// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.TwitterOutboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.Publishing.Model.Twitter;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Twitter;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UrlShorteners;
using Twitterizer;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Tweeter Pipe</summary>
  [Obsolete("Use TwitterFeedOutboundPupe instead")]
  [PipeDesigner(null, typeof (TwitterPipeDesignerView))]
  public class TwitterOutboundPipe : BasePipe<TwitterPipeSettings>, IPushPipe, IOutboundPipe
  {
    private IDefinitionField[] definitionFields;
    public const string PipeName = "Twitter";

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => "Twitter";

    /// <summary>
    /// Defines the data structure of the medium this pipe works with
    /// </summary>
    /// <value></value>
    public override IDefinitionField[] Definition
    {
      get
      {
        if (this.definitionFields == null)
          this.definitionFields = PublishingSystemFactory.GetPipeDefinitions(this.Name);
        return this.definitionFields;
      }
    }

    /// <summary>Gets or sets the publishing point.</summary>
    /// <value>The publishing point.</value>
    public virtual IPublishingPointBusinessObject PublishingPoint { get; set; }

    /// <summary>Pushes the data.</summary>
    /// <param name="items">The items.</param>
    public virtual void PushData(IList<PublishingSystemEventInfo> items)
    {
      List<WrapperObject> internalFormat = new List<WrapperObject>();
      List<WrapperObject> wrapperObjectList = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo publishingSystemEventInfo in (IEnumerable<PublishingSystemEventInfo>) items)
      {
        WrapperObject wrapperObject = new WrapperObject((PipeSettings) this.PipeSettingsInternal, publishingSystemEventInfo.Item);
        wrapperObject.Language = publishingSystemEventInfo.Language;
        if (this.PipeSettingsInternal.LanguageIds.Count <= 0 || this.PipeSettingsInternal.LanguageIds.Contains(wrapperObject.Language))
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

    /// <summary>Gets the converted items for mapping.</summary>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    public virtual IEnumerable<WrapperObject> GetConvertedItemsForMapping(
      params object[] items)
    {
      List<WrapperObject> convertedItemsForMapping = new List<WrapperObject>();
      foreach (object theInstance in items)
      {
        WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, theInstance);
        convertedItemsForMapping.Add(wrapperObject);
      }
      return (IEnumerable<WrapperObject>) convertedItemsForMapping;
    }

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = (TwitterPipeSettings) pipeSettings;
      this.PublishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettingsInternal.PublishingPoint);
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

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public static List<Mapping> GetDefaultMappings() => new List<Mapping>()
    {
      PublishingSystemFactory.CreateMapping("Content", string.Empty, true, "Title"),
      PublishingSystemFactory.CreateMapping("Link", "UrlShortenerTranslator", true, "Link")
    };

    /// <summary>
    /// Determines whether this instance [can process item] the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// 	<c>true</c> if this instance [can process item] the specified item; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanProcessItem(object item) => true;

    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="initSettings">The init settings.</param>
    /// <returns></returns>
    public override string GetPipeSettingsShortDescription(PipeSettings initSettings)
    {
      string str = string.Format("http://twitter.com/{0}", (object) this.PipeSettingsInternal.UserNameReference);
      return string.Format("{0} <a href=\"{1}\" target=\"_blank\">{1}</a>", (object) Res.Get<PublishingMessages>().TwitterPipeSettingsShortDescription, (object) str);
    }

    /// <summary>Publishes the twitter items.</summary>
    /// <param name="internalFormat">The internal format.</param>
    protected virtual void PublishTwitterItems(IEnumerable<WrapperObject> internalFormat)
    {
      TwitterConfigCredentialsManager credentialsManager = new TwitterConfigCredentialsManager();
      OAuthTokens tokensForReference;
      try
      {
        tokensForReference = credentialsManager.GetTokensForReference(this.PipeSettingsInternal.AppNameReference, this.PipeSettingsInternal.UserNameReference);
      }
      catch (Exception ex)
      {
        return;
      }
      foreach (WrapperObject values in internalFormat)
        this.PublishTwitterItem(values, tokensForReference);
    }

    /// <summary>Publishes the twitter item.</summary>
    /// <param name="values">The values.</param>
    /// <param name="tokens">The tokens.</param>
    protected virtual void PublishTwitterItem(WrapperObject values, OAuthTokens tokens)
    {
      string property = (string) values.GetProperty("Title");
      string str1 = property != null ? Regex.Replace(property, "<[^>]*>", string.Empty) : string.Empty;
      string str2 = (string) values.GetProperty("Link");
      if (string.IsNullOrEmpty(str2))
        str2 = PipeUtility.GetItemUrl(values);
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
      string text = str4 + str3;
      TwitterStatus.Update(tokens, text, new StatusUpdateOptions());
      string message = string.Format("Twitter pipe PublishTwitterItem called.");
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        message += string.Format(" {0}", (object) values.Language);
      Log.Write((object) message, ConfigurationPolicy.TestTracing);
    }
  }
}
