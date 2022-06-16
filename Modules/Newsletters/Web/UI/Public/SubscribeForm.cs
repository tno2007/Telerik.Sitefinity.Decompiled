// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeForm
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
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public
{
  /// <summary>
  /// Form that visitors can use to subscribe for the newsletters.
  /// </summary>
  [ControlDesigner(typeof (SubscribeFormDesigner))]
  [PropertyEditorTitle(typeof (NewslettersResources), "SubscribeFormTitle")]
  public class SubscribeForm : SimpleView, ILicensedControl
  {
    private string widgetTitle;
    private string widgetDescription;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Newsletters.SubscribeForm.ascx");

    /// <summary>
    /// Gets or sets the id of the list to which visitors will subscribe using this form.
    /// </summary>
    [Category("Data")]
    public Guid ListId { get; set; }

    /// <summary>
    /// Gets or sets the name of the newsletters provider to be used by this control.
    /// </summary>
    [Category("Data")]
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the title of the widget.</summary>
    public string WidgetTitle
    {
      get => this.widgetTitle;
      set => this.widgetTitle = value;
    }

    /// <summary>Gets or sets the description of the widget.</summary>
    public string WidgetDescription
    {
      get => this.widgetDescription;
      set => this.widgetDescription = value;
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SubscribeForm.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Panel that represents the error messages</summary>
    protected Panel ErrorsPanel => this.Container.GetControl<Panel>("errorsPanel", true);

    /// <summary>
    /// Gets the reference to the form control that provides the user interface for subscribing
    /// to the list.
    /// </summary>
    protected virtual HtmlGenericControl FormFieldset => this.Container.GetControl<HtmlGenericControl>("formFieldset", true);

    /// <summary>
    /// Gets the reference to the control that displays the instruction in edit mode if
    /// list has not been selected.
    /// </summary>
    protected virtual WebControl SelectListInstructionPanel => this.Container.GetControl<WebControl>("selectListInstructionPanel", true);

    /// <summary>
    /// Gets the reference to the control that display the title of the widget.
    /// </summary>
    protected virtual ITextControl WidgetTitleControl => this.Container.GetControl<ITextControl>("widgetTitle", true);

    /// <summary>
    /// Gets the reference to the control that displays the description of the widget.
    /// </summary>
    protected virtual ITextControl WidgetDescriptionControl => this.Container.GetControl<ITextControl>("widgetDescription", true);

    /// <summary>
    /// Gets the reference to the control that holds the email of the subscriber.
    /// </summary>
    protected virtual ITextControl EmailAddress => this.Container.GetControl<ITextControl>("emailAddress", true);

    /// <summary>
    /// Gets the reference to the control that holds the first name of the subscriber.
    /// </summary>
    protected virtual ITextControl FirstName => this.Container.GetControl<ITextControl>("firstName", true);

    /// <summary>
    /// Gets the reference to the control that holds the last name of the subscriber.
    /// </summary>
    protected virtual ITextControl LastName => this.Container.GetControl<ITextControl>("lastName", true);

    /// <summary>
    /// Gets the reference to the subscribe to the list button.
    /// </summary>
    protected virtual IButtonControl SubscribeButton => this.Container.GetControl<IButtonControl>("subscribeButton", true);

    /// <summary>Gets the reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>
    /// Gets the reference to the required email address validator control.
    /// </summary>
    protected virtual RequiredFieldValidator EmailValidator => this.Container.GetControl<RequiredFieldValidator>("emailValidator", true);

    /// <summary>
    /// Gets the reference to the email address regular expression validator control.
    /// </summary>
    protected virtual RegularExpressionValidator EmailRegExp => this.Container.GetControl<RegularExpressionValidator>("emailRegExp", true);

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
        string str = "subscribeForm" + Guid.NewGuid().ToString();
        this.EmailValidator.ValidationGroup = str;
        this.EmailRegExp.ValidationGroup = str;
        this.EmailRegExp.ValidationExpression = "^[a-zA-Z0-9.!#$%&'*\\+\\-/=?^_`{|}~]+@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,63}$";
        this.SubscribeButton.ValidationGroup = str;
        this.FormFieldset.Visible = this.ListId != Guid.Empty;
        this.SelectListInstructionPanel.Visible = this.ListId == Guid.Empty && SystemManager.IsDesignMode && !SystemManager.IsPreviewMode;
        this.WidgetTitleControl.Text = this.WidgetTitle;
        this.WidgetDescriptionControl.Text = this.WidgetDescription;
        this.SubscribeButton.Click += new EventHandler(this.SubscribeButton_Click);
      }
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void SubscribeButton_Click(object sender, EventArgs e) => this.AddSubscriber();

    protected virtual void AddSubscriber()
    {
      if (!NewsletterValidator.IsValidEmail(this.EmailAddress.Text))
      {
        this.MessageControl.ShowNegativeMessage(Res.Get<ErrorMessages>().EmailAddressViolationMessage);
      }
      else
      {
        NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
        string email = this.EmailAddress.Text.ToLower();
        IQueryable<Subscriber> source = manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Email == email));
        bool flag = false;
        foreach (Subscriber subscriber in (IEnumerable<Subscriber>) source)
        {
          if (subscriber.Lists.Any<MailingList>((Func<MailingList, bool>) (ml => ml.Id == this.ListId)))
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          this.MessageControl.ShowNegativeMessage(Res.Get<NewslettersResources>().EmailExistsInTheMailingList);
        }
        else
        {
          Subscriber subscriber = source.FirstOrDefault<Subscriber>();
          if (subscriber == null)
          {
            subscriber = manager.CreateSubscriber(true);
            subscriber.Email = this.EmailAddress.Text;
            subscriber.FirstName = this.FirstName.Text;
            subscriber.LastName = this.LastName.Text;
          }
          if (!manager.Subscribe(subscriber, this.ListId))
            return;
          manager.SaveChanges();
          this.MessageControl.ShowPositiveMessage(Res.Get<NewslettersResources>().SuccessfulSubscription);
          this.ClearForm();
        }
      }
    }

    private void ClearForm()
    {
      this.EmailAddress.Text = string.Empty;
      this.FirstName.Text = string.Empty;
      this.LastName.Text = string.Empty;
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
