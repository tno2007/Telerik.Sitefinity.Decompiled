// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Webhooks.WebhookResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Webhooks
{
  [ObjectInfo("WebhookResources", ResourceClassId = "WebhookResources")]
  internal class WebhookResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Webhooks.WebhookResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public WebhookResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Webhooks.WebhookResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />The data provider</param>
    public WebhookResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Gets Web hook Config Title</summary>
    [ResourceEntry("WebhookConfigTitle", Description = "Webhook config title", LastModified = "2019/08/26", Value = "Webhooks")]
    public string WebhookConfigTitle => this[nameof (WebhookConfigTitle)];

    /// <summary>Gets Web hook Config Description</summary>
    [ResourceEntry("WebhookConfigDescription", Description = "Webhook config description", LastModified = "2019/08/26", Value = "Provides configuration setting for the webhook events.")]
    public string WebhookConfigDescription => this[nameof (WebhookConfigDescription)];

    /// <summary>Gets Events Title</summary>
    [ResourceEntry("EventsTitle", Description = "Webhook events title", LastModified = "2019/08/26", Value = "Webhook events")]
    public string EventsTitle => this[nameof (EventsTitle)];

    /// <summary>Gets Events Description</summary>
    [ResourceEntry("EventsDescription", Description = "Webhook events description", LastModified = "2019/08/26", Value = "Events for which a webhook is created.")]
    public string EventsDescription => this[nameof (EventsDescription)];

    /// <summary>Gets Event Type Title</summary>
    [ResourceEntry("EventTypeTitle", Description = "Event type title", LastModified = "2019/08/26", Value = "Event type")]
    public string EventTypeTitle => this[nameof (EventTypeTitle)];

    /// <summary>Gets Event Type Description</summary>
    [ResourceEntry("EventTypeDescription", Description = "Event type description", LastModified = "2019/08/26", Value = ".NET interface that represents the event.")]
    public string EventTypeDescription => this[nameof (EventTypeDescription)];

    /// <summary>Gets Url Title</summary>
    [ResourceEntry("UrlsTitle", Description = "Urls title", LastModified = "2019/08/26", Value = "URLs")]
    public string UrlsTitle => this[nameof (UrlsTitle)];

    /// <summary>Gets Url Description</summary>
    [ResourceEntry("UrlsDescription", Description = "Urls description", LastModified = "2019/08/26", Value = "Collection of URLs to which data will be sent for this event.")]
    public string UrlsDescription => this[nameof (UrlsDescription)];

    /// <summary>Gets Url Title</summary>
    [ResourceEntry("UrlTitle", Description = "Url title", LastModified = "2019/08/26", Value = "URL")]
    public string UrlTitle => this[nameof (UrlTitle)];

    /// <summary>Gets Url Description</summary>
    [ResourceEntry("UrlDescription", Description = "Url description", LastModified = "2019/08/26", Value = "URL to which data will be sent. Example: http://www.mysite.com/api/data")]
    public string UrlDescription => this[nameof (UrlDescription)];

    /// <summary>Gets secret title</summary>
    [ResourceEntry("SecretTitle", Description = "Secret title", LastModified = "2019/09/27", Value = "Secret")]
    public string SecretTitle => this[nameof (SecretTitle)];

    /// <summary>Gets secret description</summary>
    [ResourceEntry("SecretDescription", Description = "Secret description", LastModified = "2019/09/27", Value = "Secret key which will be used for computing the request signature header.")]
    public string SecretDescription => this[nameof (SecretDescription)];

    /// <summary>Gets event type error message</summary>
    [ResourceEntry("EventTypeErrorMessage", Description = "Event type error message", LastModified = "2019/09/25", Value = "Cannot create a webhook for type: {0}.")]
    public string EventTypeErrorMessage => this[nameof (EventTypeErrorMessage)];

    /// <summary>Gets event URL error message</summary>
    [ResourceEntry("EventUrlErrorMessage", Description = "Event URL error message", LastModified = "2020/10/23", Value = "{0} is not a valid URL.")]
    public string EventUrlErrorMessage => this[nameof (EventUrlErrorMessage)];
  }
}
