// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MailingListViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>
  /// ViewModel class for the list model of the newsletters module.
  /// </summary>
  [DataContract]
  public class MailingListViewModel
  {
    private IList<DynamicListSettingsViewModel> dynamicLists;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MailingListViewModel" /> class.
    /// </summary>
    public MailingListViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MailingListViewModel" /> class and initializes values based on the
    /// source <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> object that this view model represents.
    /// </summary>
    /// <param name="source">The source MailingList object.</param>
    public MailingListViewModel(MailingList source)
    {
      NewslettersManager manager = NewslettersManager.GetManager();
      this.Id = source.Id;
      this.ApplicationName = source.ApplicationName;
      this.DefaultFromName = (string) source.DefaultFromName;
      this.DefaultReplyToEmail = (string) source.DefaultReplyToEmail;
      this.DefaultSubject = (string) source.DefaultSubject;
      this.SubscriptionReminder = (string) source.SubscriptionReminder;
      this.Title = (string) source.Title;
      this.SubscribersCount = manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (sub => sub.Lists.Contains(source))).Count<Subscriber>().ToString();
    }

    /// <summary>Gets the unique identity of the data item.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the application to which this data item belongs to.
    /// </summary>
    /// <value>The name of the application.</value>
    [DataMember]
    public string ApplicationName { get; set; }

    /// <summary>
    /// Gets or sets the default name of the person/organization that is sending the newsletter.
    /// </summary>
    /// <value>The default name of the person/organization.</value>
    [DataMember]
    public string DefaultFromName { get; set; }

    /// <summary>
    /// Gets or sets the default email address to which recipients ought to reply.
    /// </summary>
    /// <value>The default reply-to email address.</value>
    [DataMember]
    public string DefaultReplyToEmail { get; set; }

    /// <summary>Gets or sets the default subject of the newsletter.</summary>
    /// <value>The default subject.</value>
    [DataMember]
    public string DefaultSubject { get; set; }

    /// <summary>
    /// Gets or sets the subscription reminder message that reminds users how did they subscribe to the newsletter.
    /// </summary>
    /// <value>The subscription reminder message.</value>
    [DataMember]
    public string SubscriptionReminder { get; set; }

    /// <summary>Gets or sets the title of the list.</summary>
    /// <value>The title of the list.</value>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the number of subscribers belonging to this list.
    /// </summary>
    [DataMember]
    public string SubscribersCount { get; set; }

    /// <summary>
    /// Gets or sets the text for the number of subscribers belonging to this list.
    /// </summary>
    [DataMember]
    public string SubscribersCountText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to send a welcome message.
    /// </summary>
    /// <value><c>true</c> if [send welcome message]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool SendWelcomeMessage { get; set; }

    /// <summary>Gets or sets the welcome message subject.</summary>
    /// <value>The welcome message subject.</value>
    [DataMember]
    public string WelcomeMessageSubject { get; set; }

    /// <summary>Gets or sets the welcome message email address.</summary>
    /// <value>The welcome message email address.</value>
    [DataMember]
    public string WelcomeMessageEmailAddress { get; set; }

    /// <summary>Gets or sets the welcome template id.</summary>
    /// <value>The welcome template id.</value>
    [DataMember]
    public Guid WelcomeTemplateId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to send a good bye message.
    /// </summary>
    /// <value><c>true</c> if [send good bye message]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool SendGoodByeMessage { get; set; }

    /// <summary>Gets or sets the good bye message subject.</summary>
    /// <value>The good bye message subject.</value>
    [DataMember]
    public string GoodByeMessageSubject { get; set; }

    /// <summary>Gets or sets the good bye message email address.</summary>
    /// <value>The good bye message email address.</value>
    [DataMember]
    public string GoodByeMessageEmailAddress { get; set; }

    /// <summary>Gets or sets the good bye template id.</summary>
    /// <value>The good bye template id.</value>
    [DataMember]
    public Guid GoodByeTemplateId { get; set; }

    /// <summary>Gets the dynamic lists.</summary>
    /// <value>The dynamic lists.</value>
    [DataMember]
    public IList<DynamicListSettingsViewModel> DynamicLists
    {
      get
      {
        if (this.dynamicLists == null)
          this.dynamicLists = (IList<DynamicListSettingsViewModel>) new List<DynamicListSettingsViewModel>();
        return this.dynamicLists;
      }
    }

    /// <summary>
    /// Gets or sets the count of campaigns using the mailing list.
    /// </summary>
    [DataMember]
    public int CampaignsCount { get; set; }

    /// <summary>
    /// Gets or sets the text for the count of campaigns using the mailing list.
    /// </summary>
    [DataMember]
    public string CampaignsText { get; set; }

    /// <summary>
    /// Gets or sets the identity of the page that contains the Unsubscribe widget.
    /// </summary>
    [DataMember]
    public Guid UnsubscribePageId { get; set; }
  }
}
