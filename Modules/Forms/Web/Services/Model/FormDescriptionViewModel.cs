// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FormDescriptionViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Forms.Web.Services.Model
{
  /// <summary>
  /// Defines the properties for the form description type and is used for transferring the data through WCF.
  /// </summary>
  [KnownType(typeof (FormDescriptionViewModel))]
  [DataContract]
  public class FormDescriptionViewModel
  {
    private List<string> tags;
    private List<string> category;

    public FormDescriptionViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FormDescriptionViewModel" /> class.
    /// </summary>
    /// <param name="form">The form description.</param>
    public FormDescriptionViewModel(FormDescription form)
      : this(form, (FormsDataProvider) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FormDescriptionViewModel" /> class.
    /// </summary>
    /// <param name="form">The form description.</param>
    /// <param name="provider">The provider.</param>
    public FormDescriptionViewModel(FormDescription form, FormsDataProvider provider)
    {
      if (provider == null)
        provider = form.Provider as FormsDataProvider;
      this.Id = form.Id;
      this.Title = (string) form.Title;
      this.Name = form.Name;
      this.DateCreated = new DateTime?(form.DateCreated);
      this.LastModified = new DateTime?(form.LastModified);
      this.Owner = this.GetUser(form.Owner);
      this.UrlName = (string) form.UrlName;
      this.EditUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Forms/" + this.Name);
      this.ViewUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Forms/" + this.Name + "/Preview");
      bool flag = form.IsGranted("Forms", "ViewResponses");
      if (flag)
        this.ResponsesUrl = VirtualPathUtility.ToAbsolute(BackendSiteMap.FindSiteMapNode(FormsModule.EntriesPageID, false)?.Url + "/" + this.Name);
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        this.ViewUrl = VirtualPathUtility.ToAbsolute(string.Format("~/Sitefinity/Forms/{0}/Preview/{1}", (object) this.Name, (object) culture.Name));
        if (flag)
          this.ResponsesUrl = VirtualPathUtility.ToAbsolute(BackendSiteMap.FindSiteMapNode(FormsModule.EntriesPageID, false)?.Url + "/" + this.Name + "?lang=" + culture.Name);
      }
      string statusKey = (string) null;
      string statusText = (string) null;
      LifecycleExtensions.GetOverallStatus((ILifecycleDataItemLive) form, culture, ref statusKey, ref statusText);
      this.Status = statusKey;
      this.StatusText = statusText;
      this.AvailableLanguages = form.AvailableLanguages;
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      this.LifecycleStatus = new WcfContentLifecycleStatus()
      {
        IsAdmin = currentIdentity.IsUnrestricted,
        IsLocked = form.LockedBy != Guid.Empty,
        IsLockedByMe = form.LockedBy == currentIdentity.UserId,
        IsPublished = form.Status == ContentLifecycleStatus.Live,
        LockedByUsername = CommonMethods.GetUserName(form.LockedBy),
        Message = this.StatusText,
        SupportsContentLifecycle = form.SupportsContentLifecycle
      };
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      this.SubscribedEmails = notificationService.GetSubscribers(serviceContext, form.SubscriptionListId, (QueryParameters) null).Select<ISubscriberResponse, string>((Func<ISubscriberResponse, string>) (s => s.Email)).ToArray<string>();
      this.SubscribedEmailsAfterFormUpdate = notificationService.GetSubscribers(serviceContext, form.SubscriptionListIdAfterFormUpdate, (QueryParameters) null).Select<ISubscriberResponse, string>((Func<ISubscriberResponse, string>) (s => s.Email)).ToArray<string>();
      this.FormLabelPlacement = form.FormLabelPlacement;
      this.RedirectPageUrl = form.RedirectPageUrl;
      this.SubmitAction = form.SubmitAction;
      this.SubmitRestriction = form.SubmitRestriction;
      this.SuccessMessage = (string) form.SuccessMessage;
      this.SendConfirmationEmail = form.SendConfirmationEmail;
      this.CssClass = form.CssClass;
      this.SubmitActionAfterUpdate = form.SubmitActionAfterUpdate;
      this.RedirectPageUrlAfterUpdate = form.RedirectPageUrlAfterUpdate;
      this.SuccessMessageAfterFormUpdate = (string) form.SuccessMessageAfterFormUpdate;
      this.Attributes = new DictionaryObjectViewModel(form.Attributes);
      this.AdditionalStatus = StatusResolver.Resolve(typeof (FormDescription), provider.Name, this.Id);
      this.Framework = form.Framework;
      this.IsEditable = form.IsEditable("Forms", "Modify");
      this.IsUnlockable = form.IsGranted("Forms", "Unlock");
    }

    /// <summary>Contains information about the content lifecycle.</summary>
    [DataMember]
    public WcfContentLifecycleStatus LifecycleStatus { get; set; }

    /// <summary>Gets or sets the form description id</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title of the form description</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the name of the form description</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the date on which the <see cref="!:FormView" /> was created
    /// </summary>
    [DataMember]
    public DateTime? DateCreated { get; set; }

    /// <summary>
    /// Gets or sets the date on which the <see cref="!:FormView" /> was modified
    /// </summary>
    [DataMember]
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Gets or sets the Status of the page. If the Page property of the <see cref="!:PageNode" />
    /// is null (in case of group page), the status is discarded
    /// </summary>
    [DataMember]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the Status of the page. If the Page property of the <see cref="!:PageNode" />
    /// is null (in case of group page), the status is discarded
    /// </summary>
    [DataMember]
    public string StatusText { get; set; }

    [DataMember]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Telerik.Sitefinity.Services.Status AdditionalStatus { get; set; }

    /// <summary>
    /// Gets or sets the Owner of the <see cref="!:PageNode" /> if the Page property is not null
    /// </summary>
    [DataMember]
    public string Owner { get; set; }

    /// <summary>Gets or sets the url of the form description</summary>
    [DataMember]
    public string UrlName { get; set; }

    /// <summary>Gets or sets the url of the form description</summary>
    [DataMember]
    public string EditUrl { get; set; }

    /// <summary>Gets or sets the view url of the form description</summary>
    [DataMember]
    public string ViewUrl { get; set; }

    /// <summary>Gets or sets the view url of the form description</summary>
    [DataMember]
    public string ResponsesUrl { get; set; }

    /// <summary>Gets or sets the tags.</summary>
    /// <value>The tags.</value>
    [DataMember]
    public List<string> Tags
    {
      get
      {
        if (this.tags == null)
          this.tags = new List<string>();
        return this.tags;
      }
      set => this.tags = value;
    }

    /// <summary>Gets or sets the category.</summary>
    /// <value>The category.</value>
    [DataMember]
    public List<string> Category
    {
      get
      {
        if (this.category == null)
          this.category = new List<string>();
        return this.category;
      }
      set => this.category = value;
    }

    /// <summary>
    /// Success message to be displayed when the form is submitted
    /// </summary>
    [DataMember]
    public string SuccessMessage { get; set; }

    /// <summary>Form submit restrictions</summary>
    [DataMember]
    public SubmitRestriction SubmitRestriction { get; set; }

    /// <summary>Label placement</summary>
    [DataMember]
    public FormLabelPlacement FormLabelPlacement { get; set; }

    /// <summary>Redirection url upon submit</summary>
    [DataMember]
    public string RedirectPageUrl { get; set; }

    /// <summary>Action to be taken on form submit</summary>
    [DataMember]
    public SubmitAction SubmitAction { get; set; }

    /// <summary>
    /// Gets or sets how many entries the given form description has.
    /// </summary>
    /// <value>The entries count.</value>
    [DataMember]
    public int EntriesCount { get; set; }

    /// <summary>CssClass of the form</summary>
    [DataMember]
    public string CssClass { get; set; }

    /// <summary>
    /// Gets or sets the id of the pageData to which the data in this object is a translation.
    /// Only used in multilingual environment. Only set when creating a translated version of
    /// the page.
    /// </summary>
    /// <value>The source language page id.</value>
    [DataMember]
    public Guid SourceLanguageObjectId { get; set; }

    /// <summary>Gets languages available for this item.</summary>
    /// <value>The available languages.</value>
    [DataMember]
    public virtual string[] AvailableLanguages { get; set; }

    [DataMember]
    public bool HasSubscription { get; set; }

    /// <summary>Gets or sets the emails subscribed for the form</summary>
    /// <value>The emails subscribed for the form</value>
    [DataMember]
    public string[] SubscribedEmails { get; set; }

    /// <summary>Gets or sets the subscribed emails after form update.</summary>
    /// <value>The subscribed emails after form update.</value>
    [DataMember]
    public string[] SubscribedEmailsAfterFormUpdate { get; set; }

    /// <summary>Gets or sets the has subscription after form update.</summary>
    /// <value>The has subscription after form update.</value>
    [DataMember]
    public bool HasSubscriptionAfterFormUpdate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to send a confirmation email to the user who submitted the form
    /// </summary>
    [DataMember]
    public bool SendConfirmationEmail { get; set; }

    /// <summary>Gets or sets the attributes.</summary>
    /// <value>The attributes.</value>
    [DataMember]
    public DictionaryObjectViewModel Attributes { get; set; }

    /// <summary>Gets the title when in duplicate mode.</summary>
    [DataMember]
    public string DuplicateTitle { get; set; }

    /// <summary>Gets the name when in duplicate mode.</summary>
    [DataMember]
    public string DuplicateName { get; set; }

    /// <summary>Gets or sets the submit action after update.</summary>
    /// <value>The submit action after update.</value>
    [DataMember]
    public SubmitAction SubmitActionAfterUpdate { get; set; }

    /// <summary>Gets or sets the redirect page URL after update.</summary>
    /// <value>The redirect page URL after update.</value>
    [DataMember]
    public string RedirectPageUrlAfterUpdate { get; set; }

    /// <summary>Gets or sets the success message after update.</summary>
    /// <value>The success message after update.</value>
    [DataMember]
    public string SuccessMessageAfterFormUpdate { get; set; }

    /// <summary>Gets or sets the form framework.</summary>
    /// <value>The framework.</value>
    [DataMember]
    public FormFramework Framework { get; set; }

    [DataMember]
    public bool IsEditable { get; set; }

    [DataMember]
    public bool IsUnlockable { get; set; }

    /// <summary>Gets or sets the available page template frameworks.</summary>
    [DataMember]
    public PageTemplatesAvailability AvailableFrameworks { get; set; }

    private string GetUser(Guid id) => UserProfilesHelper.GetUserDisplayName(id);
  }
}
