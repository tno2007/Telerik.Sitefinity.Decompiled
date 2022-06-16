// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public
{
  /// <summary>Public form for unsubscribing from the newsletter.</summary>
  [ControlDesigner(typeof (UnsubscribeFormDesigner))]
  [PropertyEditorTitle(typeof (NewslettersResources), "UnsubscribeFormTitle")]
  public class UnsubscribeForm : SimpleView, ILicensedControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Newsletters.UnsubscribeForm.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeForm" /> class.
    /// </summary>
    public UnsubscribeForm() => this.MessageBody = Res.Get<NewslettersResources>().UnsubscribeMessageOnSuccess;

    /// <summary>
    /// Gets or sets the id of the list from which the user will unsubscribe.
    /// </summary>
    [Category("Data")]
    public Guid ListId { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider to be used to unsubscribe.
    /// </summary>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the title of the widget.</summary>
    public string WidgetTitle { get; set; }

    /// <summary>Gets or sets the description of the widget.</summary>
    public string WidgetDescription { get; set; }

    /// <summary>Gets or sets the message body.</summary>
    /// <remarks></remarks>
    public string MessageBody { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UnsubscribeForm.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Panel that represents the error messages</summary>
    protected Panel ErrorsPanel => this.Container.GetControl<Panel>("errorsPanel", true);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the reference to the form fields control.</summary>
    protected virtual HtmlGenericControl FormFieldset => this.Container.GetControl<HtmlGenericControl>("formFieldset", true);

    /// <summary>
    /// Gets the reference to the select list instruction panel.
    /// </summary>
    protected virtual WebControl SelectListInstructionPanel => this.Container.GetControl<WebControl>("selectListInstructionPanel", true);

    /// <summary>
    /// Gets the reference to the select list instruction panel.
    /// </summary>
    protected virtual Literal UnsubscribeMessageBody => this.Container.GetControl<Literal>("unsubscribeMessageBody", true);

    /// <summary>Gets the reference to the email address text box.</summary>
    protected virtual ITextControl EmailAddress => this.Container.GetControl<ITextControl>("emailAddress", true);

    /// <summary>Gets the reference to the unsubscribe button.</summary>
    protected virtual IButtonControl UnsubscribeButton => this.Container.GetControl<IButtonControl>("unsubscribeButton", true);

    /// <summary>Gets the reference to the widget title control.</summary>
    protected virtual ITextControl WidgetTitleControl => this.Container.GetControl<ITextControl>("widgetTitle", true);

    /// <summary>Gets the reference to the widget description control</summary>
    protected virtual ITextControl WidgetDescriptionControl => this.Container.GetControl<ITextControl>("widgetDescription", true);

    /// <summary>Gets the reference to the message control.</summary>
    public virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>
    /// Gets the reference to the required email address validator control.
    /// </summary>
    public virtual RequiredFieldValidator EmailValidator => this.Container.GetControl<RequiredFieldValidator>("emailValidator", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.IsLicensed)
      {
        this.ErrorsPanel.Visible = true;
        this.ErrorsPanel.Controls.Add((Control) new Label()
        {
          Text = this.LicensingMessage
        });
      }
      else
      {
        if (this.ListId == Guid.Empty)
        {
          this.FormFieldset.Visible = false;
          this.UnsubscribeMessageBody.Visible = true;
          if (!string.IsNullOrEmpty(this.MessageBody))
            this.UnsubscribeMessageBody.Text = this.MessageBody;
        }
        else
        {
          this.FormFieldset.Visible = true;
          this.UnsubscribeMessageBody.Visible = false;
          string str = "unsubscribeForm" + Guid.NewGuid().ToString();
          this.EmailValidator.ValidationGroup = str;
          this.UnsubscribeButton.ValidationGroup = str;
          this.WidgetTitleControl.Text = this.WidgetTitle;
          this.WidgetDescriptionControl.Text = this.WidgetDescription;
        }
        if (this.IsDesignMode())
          return;
        if (this.UnsubscribeButton != null)
          this.UnsubscribeButton.Click += new EventHandler(this.UnsubscribeButton_Click);
        this.RemoveSubscriber();
      }
    }

    [Obsolete("The unsupscription is done in InitializeControls")]
    protected void UnsubscribeButton_Click(object sender, EventArgs e) => this.Unsubscribe();

    private void RemoveSubscriber()
    {
      if (!(this.ListId == Guid.Empty))
        return;
      NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
      string input1 = this.Page.Request.QueryStringGet("subscriberId");
      string input2 = this.Page.Request.QueryStringGet("issueId");
      string input3 = this.Page.Request.QueryStringGet("listId");
      Guid subscriberGuid = Guid.Empty;
      Guid result = Guid.Empty;
      ref Guid local = ref subscriberGuid;
      if (!Guid.TryParse(input1, out local))
        return;
      Guid issueGuid = Guid.Empty;
      Campaign issue = (Campaign) null;
      Guid mailingListId = Guid.Empty;
      if (Guid.TryParse(input3, out result) && result != Guid.Empty)
        mailingListId = result;
      else if (Guid.TryParse(input2, out issueGuid) && issueGuid != Guid.Empty)
      {
        issue = manager.GetIssues().FirstOrDefault<Campaign>((Expression<Func<Campaign, bool>>) (i => i.Id == issueGuid));
        if (issue != null)
          mailingListId = issue.List.Id;
      }
      Subscriber subscriber = manager.GetSubscribers().FirstOrDefault<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Id == subscriberGuid));
      if (subscriber == null || mailingListId == Guid.Empty)
        this.MessageControl.ShowNegativeMessage(Res.Get<NewslettersResources>().YouCannotUnsubscribe);
      else if (this.Page.Request.QueryStringGet("subscribe") != null)
        this.Subscribe(manager, subscriber, mailingListId);
      else
        this.Unsubscribe(manager, subscriber, mailingListId, issue);
    }

    private void Subscribe(
      NewslettersManager newslettersManager,
      Subscriber subscriber,
      Guid mailingListId)
    {
      if (!subscriber.Lists.Any<MailingList>((Func<MailingList, bool>) (ml => ml.Id == mailingListId)))
      {
        if (!newslettersManager.Subscribe(subscriber, mailingListId))
          return;
        newslettersManager.SaveChanges();
        this.UnsubscribeMessageBody.Text = Res.Get<NewslettersResources>().SubscribeSuccessful;
      }
      else
        this.UnsubscribeMessageBody.Text = Res.Get<NewslettersResources>().EmailExistsInTheMailingList;
    }

    /// <summary>
    /// Unsubscribes by the provided in the query string subscriber id and issue id.
    /// </summary>
    /// <param name="newslettersManager">The newsletters manager.</param>
    /// <param name="subscriber">The subscriber.</param>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="issue">The issue.</param>
    private void Unsubscribe(
      NewslettersManager newslettersManager,
      Subscriber subscriber,
      Guid mailingListId,
      Campaign issue)
    {
      MergeContextItems mergeContextItems = new MergeContextItems();
      string pathAndQuery = SystemManager.CurrentHttpContext.Request.Url.PathAndQuery;
      string str1 = "<a href=\"{0}&subscribe={1}\">{2}</a>";
      mergeContextItems.SubscribeLink = str1.Arrange((object) pathAndQuery, (object) true, (object) Res.Get<NewslettersResources>().SubscribeLink);
      string str2;
      if (issue != null)
        str2 = Merger.MergeTags(this.MessageBody, (object) issue.List, (object) issue, (object) subscriber, (object) mergeContextItems);
      else
        str2 = Merger.MergeTags(this.MessageBody, (object) newslettersManager.GetMailingLists().FirstOrDefault<MailingList>((Expression<Func<MailingList, bool>>) (m => m.Id == mailingListId)), (object) subscriber, (object) mergeContextItems);
      this.UnsubscribeMessageBody.Text = str2;
      if (!newslettersManager.Unsubscribe(subscriber, mailingListId, issue))
        return;
      newslettersManager.SaveChanges();
    }

    /// <summary>Unsubscribes by specified email.</summary>
    private void Unsubscribe()
    {
      if (!(this.ListId != Guid.Empty))
        return;
      NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
      string email = this.EmailAddress.Text.ToLower();
      IQueryable<Subscriber> source = manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Email == email));
      if (source.Count<Subscriber>() == 0)
      {
        this.MessageControl.ShowNegativeMessage(Res.Get<NewslettersResources>().YouDontBelongToTheMailingList);
      }
      else
      {
        foreach (Subscriber subscriber in (IEnumerable<Subscriber>) source)
        {
          if (subscriber != null)
          {
            if (manager.Unsubscribe(subscriber, this.ListId))
            {
              manager.SaveChanges();
              this.MessageControl.ShowPositiveMessage(Res.Get<NewslettersResources>().UnsubscribedSuccessMessage);
            }
            else
              this.MessageControl.ShowNegativeMessage(Res.Get<NewslettersResources>().YouDontBelongToTheMailingList);
          }
        }
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance of the control is licensed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is licensed; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    public bool IsLicensed => LicenseState.CheckIsModuleLicensedInCurrentDomain("3D8A2051-6F6F-437C-865E-B3177689AC12");

    /// <summary>
    /// Gets custom the licensing message.If null the system will use a default message
    /// </summary>
    /// <value>The licensing message.</value>
    [Browsable(false)]
    public string LicensingMessage => Res.Get<NewslettersResources>().ModuleNotLicensed;
  }
}
