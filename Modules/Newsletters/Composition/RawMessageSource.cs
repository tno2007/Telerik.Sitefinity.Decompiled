// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Composition.RawMessageSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.ResourceCombining;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Modules.Newsletters.Composition
{
  /// <summary>
  /// This method represents the raw source of the message. It can be either plain text or html source.
  /// </summary>
  public class RawMessageSource
  {
    private MessageBody messageBody;
    private string sourceBeforeEnhance;

    /// <summary>
    /// Creates a new instance of the <see cref="!:RadMessageSource" /> type.
    /// </summary>
    /// <param name="messageBody">
    /// The message body persistent object from which the raw message ought to be generated.
    /// </param>
    public RawMessageSource(MessageBody messageBody, string rootUrl, bool isPreview = false)
    {
      this.messageBody = messageBody;
      this.RootUrl = VirtualPathUtility.AppendTrailingSlash(rootUrl);
      this.GenerateSource(isPreview);
    }

    public string Source { get; private set; }

    public string PlainTextSource { get; private set; }

    public string OriginalSource { get; private set; }

    public string OpeningTracker { get; private set; }

    public bool IsHtml { get; private set; }

    public string RootUrl { get; private set; }

    /// <summary>
    /// Enhances the links this raw message source for a specific campaign and a subscriber.
    /// </summary>
    /// <param name="campaign">The related campaign.</param>
    /// <param name="subscriber">The related subscriber.</param>
    public void EnhanceLinks(Campaign campaign, Subscriber subscriber) => this.EnhanceLinks(campaign, subscriber.ShortId);

    /// <summary>
    /// Enhances the links this raw message source for a specific campaign and a subscriber.
    /// </summary>
    /// <param name="campaign">The related campaign.</param>
    /// <param name="subscriberShortId">The short id of the related subscriber.</param>
    public void EnhanceLinks(Campaign campaign, string subscriberShortId)
    {
      this.sourceBeforeEnhance = this.Source;
      if (string.IsNullOrEmpty(this.Source))
        this.GenerateSource();
      this.Source = MessageEnhancer.EnhanceLinks(campaign, subscriberShortId, this, this.RootUrl);
      if (this.messageBody.MessageBodyType != MessageBodyType.PlainText)
        this.OpeningTracker = MessageEnhancer.GetOpeningTracker(campaign, subscriberShortId, this.RootUrl);
      else
        this.OpeningTracker = (string) null;
    }

    public void RestoreOriginalLinks()
    {
      if (this.sourceBeforeEnhance == null)
        return;
      this.Source = this.sourceBeforeEnhance;
    }

    private void GenerateSource(bool isPreview = false)
    {
      using (new HttpRequestRegion(this.RootUrl))
      {
        this.Source = string.Empty;
        switch (this.messageBody.MessageBodyType)
        {
          case MessageBodyType.PlainText:
            this.Source = this.messageBody.BodyText;
            this.IsHtml = false;
            break;
          case MessageBodyType.HtmlText:
            this.Source = this.messageBody.BodyText;
            this.PlainTextSource = this.messageBody.PlainTextVersion;
            this.IsHtml = true;
            break;
          case MessageBodyType.InternalPage:
            this.Source = HtmlProcessor.ProcessHtml(this.GetInternalPageSource(this.messageBody, isPreview), new Uri(this.RootUrl));
            this.PlainTextSource = this.messageBody.PlainTextVersion;
            this.IsHtml = true;
            break;
          default:
            throw new NotSupportedException();
        }
        this.OriginalSource = this.Source;
        if (this.Source == null)
          return;
        this.Source = this.ResolveRelativeUrls(this.Source);
      }
    }

    /// <summary>
    /// This method resolves the relative urls of the images and links in the email message to the absolute urls, so that they can be
    /// navigated to from the email clients that receive the message.
    /// </summary>
    /// <param name="source">The source of the message where the urls ought to be resolved.</param>
    /// <param name="rootUrl">The root url of the website where Sitefinity is installed.</param>
    /// <returns>The body of the message with the resolved urls.</returns>
    private string ResolveRelativeUrls(string source) => LinkParser.ResolveLinks(source, new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, false, true);

    private string GetInternalPageSource(MessageBody messageBody, bool isPreview = false)
    {
      try
      {
        return new InMemoryPageRender().RenderPage(PageManager.GetManager().GetPageNode(messageBody.Id), isPreview, false);
      }
      catch (Exception ex)
      {
        Log.Write((object) ex);
      }
      return "";
    }
  }
}
