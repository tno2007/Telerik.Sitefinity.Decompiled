// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Composition.Composer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Modules.Newsletters.Composition
{
  /// <summary>
  /// This class composes the email message to be sent to the client.
  /// </summary>
  public static class Composer
  {
    private const string UnsubscribeWithIssueIdLink = "<a href=\"[{0}]{1}?issueId={2}&subscriberId={3}\">{4}</a>";
    private const string UnsubscribeWithListIdLink = "<a href=\"[{0}]{1}?listId={2}&subscriberId={3}\">{4}</a>";

    /// <summary>Composes the email message for the subscriber.</summary>
    /// <param name="campaign">An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Campaign" /> for which the message ought to be composed.</param>
    /// <param name="subscriber">An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> for which the message ought to be composed.</param>
    /// <param name="rawMessage">The raw message.</param>
    /// <returns>
    /// An instance of the <see cref="T:System.Net.Mail.MailMessage" /> that will be sent.
    /// </returns>
    public static MailMessage ComposeMessage(
      Campaign campaign,
      Subscriber subscriber,
      RawMessageSource rawMessage)
    {
      if (campaign == null)
        throw new ArgumentNullException(nameof (campaign));
      if (subscriber == null)
        throw new ArgumentNullException(nameof (subscriber));
      Campaign campaign1 = campaign;
      Guid id = subscriber.Id;
      string subscriberId = id.ToString();
      MergeContextItems mergeContextItems = Composer.ResolveUnsubscribeLink(campaign1, subscriberId);
      rawMessage.EnhanceLinks(campaign, subscriber);
      MailMessage mailMessage;
      try
      {
        mailMessage = Composer.ComposeMessageInternal(campaign.ReplyToEmail, subscriber, campaign.MessageSubject, rawMessage, campaign.FromName, (object) campaign.List, (object) campaign, (object) subscriber, (object) mergeContextItems);
        NameValueCollection headers = mailMessage.Headers;
        id = campaign.Id;
        string str = id.ToString();
        headers.Add("X-Sitefinity-Campaign", str);
      }
      finally
      {
        rawMessage.RestoreOriginalLinks();
      }
      return mailMessage;
    }

    internal static MergeContextItems ResolveUnsubscribeLink(
      Campaign campaign,
      string subscriberId)
    {
      return campaign.List != null && campaign.List.UnsubscribePageId != Guid.Empty ? Composer.ResolveUnsubscribeLink(campaign.List.UnsubscribePageId, subscriberId, campaign.Id) : new MergeContextItems();
    }

    internal static MergeContextItems ResolveUnsubscribeLink(
      Guid unsubscribePageId,
      string subscriberId,
      Guid campaignId = default (Guid),
      Guid listId = default (Guid))
    {
      MergeContextItems mergeContextItems = new MergeContextItems();
      PageNode pageNode = (PageNode) null;
      if (unsubscribePageId != Guid.Empty)
        pageNode = PageManager.GetManager().GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == unsubscribePageId));
      if (pageNode != null)
      {
        string format = "<a href=\"[{0}]{1}?issueId={2}&subscriberId={3}\">{4}</a>";
        Guid guid = campaignId;
        if (campaignId == Guid.Empty)
        {
          format = "<a href=\"[{0}]{1}?listId={2}&subscriberId={3}\">{4}</a>";
          guid = listId;
        }
        string html = string.Format(format, (object) pageNode.RootNodeId, (object) pageNode.Id, (object) guid, (object) subscriberId, (object) Res.Get<NewslettersResources>().UnsubscribeLink);
        mergeContextItems.UnsubscribeLink = LinkParser.ResolveLinks(html, new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, false, true);
      }
      else
        mergeContextItems.UnsubscribeLink = string.Empty;
      return mergeContextItems;
    }

    /// <summary>Composes the email message.</summary>
    /// <param name="fromEmail">Email address of the person that sends the email.</param>
    /// <param name="subscriber">An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> to whom the email ought to be sent.</param>
    /// <param name="subject">Subject of the email message.</param>
    /// <param name="messageBody">An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MessageBody" /> that represents the content of the email message.</param>
    /// <param name="fromName">Name of the person that sends the email.</param>
    /// <param name="mergeContext">Objects that contain information that ough to be replaced through merging with merge tags.</param>
    /// <returns>An instance of the <see cref="T:System.Net.Mail.MailMessage" /> that will be sent.</returns>
    public static MailMessage ComposeMessage(
      string fromEmail,
      Subscriber subscriber,
      string subject,
      RawMessageSource rawMessage,
      string fromName,
      params object[] mergeContext)
    {
      return Composer.ComposeMessageInternal(fromEmail, subscriber, subject, rawMessage, fromName, mergeContext);
    }

    /// <summary>Composes the email message.</summary>
    /// <param name="fromEmail">Email address of the person that sends the email.</param>
    /// <param name="subscriber">An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> to whom the email ought to be sent.</param>
    /// <param name="subject">Subject of the email message.</param>
    /// <param name="messageBody">An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MessageBody" /> that represents the content of the email message.</param>
    /// <param name="mergeContext">Objects that contain information that ough to be replaced through merging with merge tags.</param>
    /// <returns>An instance of the <see cref="T:System.Net.Mail.MailMessage" /> that will be sent.</returns>
    public static MailMessage ComposeMessage(
      string fromEmail,
      Subscriber subscriber,
      string subject,
      RawMessageSource rawMessage,
      params object[] mergeContext)
    {
      return Composer.ComposeMessageInternal(fromEmail, subscriber, subject, rawMessage, "", mergeContext);
    }

    /// <summary>Composes the email message.</summary>
    /// <param name="fromEmail">Email address of the person that sends the email.</param>
    /// <param name="subscriber">An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> to whom the email ought to be sent.</param>
    /// <param name="subject">Subject of the email message.</param>
    /// <param name="messageBody">An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MessageBody" /> that represents the content of the email message.</param>
    /// <param name="fromName">Name of the person that sends the email.</param>
    /// <param name="mergeContext">Objects that contain information that ough to be replaced through merging with merge tags.</param>
    /// <returns>An instance of the <see cref="T:System.Net.Mail.MailMessage" /> that will be sent.</returns>
    private static MailMessage ComposeMessageInternal(
      string fromEmail,
      Subscriber subscriber,
      string subject,
      RawMessageSource rawMessage,
      string fromName,
      params object[] mergeContext)
    {
      if (subscriber == null)
        throw new ArgumentNullException(nameof (subscriber));
      if (string.IsNullOrEmpty(subject))
        throw new ArgumentNullException(nameof (subject));
      MailMessage mailMessage = new MailMessage();
      mailMessage.From = new MailAddress(fromEmail, fromName);
      mailMessage.To.Add(new MailAddress(subscriber.Email));
      mailMessage.Subject = subject;
      string str = Merger.MergeTags(rawMessage.Source, mergeContext);
      if (!string.IsNullOrEmpty(rawMessage.OpeningTracker))
        str += rawMessage.OpeningTracker;
      AlternateView alternateView = string.IsNullOrEmpty(rawMessage.PlainTextSource) ? AlternateView.CreateAlternateViewFromString(HtmlStripper.StripTagsRegexCompiled(str), (Encoding) null, "text/plain") : AlternateView.CreateAlternateViewFromString(rawMessage.PlainTextSource, (Encoding) null, "text/plain");
      mailMessage.AlternateViews.Add(alternateView);
      if (rawMessage.IsHtml)
      {
        AlternateView alternateViewFromString = AlternateView.CreateAlternateViewFromString(str, (Encoding) null, "text/html");
        mailMessage.AlternateViews.Add(alternateViewFromString);
      }
      mailMessage.IsBodyHtml = rawMessage.IsHtml;
      mailMessage.Headers.Add("X-Sitefinity-Subscriber", subscriber.Id.ToString());
      return mailMessage;
    }

    internal static string GetMessagePlainTextBody(MailMessage message)
    {
      string empty = string.Empty;
      AlternateView alternateView = message.AlternateViews.FirstOrDefault<AlternateView>((Func<AlternateView, bool>) (view => view.ContentType.MediaType == "text/plain"));
      if (alternateView != null)
        empty = Encoding.GetEncoding(alternateView.ContentType.CharSet).GetString(((MemoryStream) alternateView.ContentStream).ToArray());
      return empty;
    }

    internal static string GetMessageHtmlBody(MailMessage message)
    {
      string str = (string) null;
      string messageHtmlBody = (string) null;
      AlternateView alternateView = message.AlternateViews.FirstOrDefault<AlternateView>((Func<AlternateView, bool>) (view => view.ContentType.MediaType == "text/html"));
      if (message.IsBodyHtml)
        messageHtmlBody = alternateView == null ? str : Encoding.GetEncoding(alternateView.ContentType.CharSet).GetString(((MemoryStream) alternateView.ContentStream).ToArray());
      return messageHtmlBody;
    }
  }
}
