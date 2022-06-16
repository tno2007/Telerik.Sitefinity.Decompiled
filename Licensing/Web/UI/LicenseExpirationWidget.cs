// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.UI.LicenseExpirationWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Licensing.Web.UI
{
  public class LicenseExpirationWidget : SimpleView
  {
    private const string CssClassAttribute = "class";
    private const string ExpiredLicenseClass = "expiredLicense";
    private const string TrialLicenseClass = "trialLicense";
    private const string FirstPeriodLicenseClass = "firstPeriodLicense";
    private const string SecondPeriodLicenseClass = "secondPeriodLicense";
    private const string ThirdPeriodLicenseClass = "thirdPeriodLicense";
    private const int FirstPeriodMaxDays = 7;
    private const int SecondPeriodMaxDays = 30;
    private const int ThirdPeriodMaxDays = 60;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Licensing.LicenseExpirationWidget.ascx");

    public LicenseInfo CurrentLicense { get; set; }

    protected virtual ITextControl Header => this.Container.GetControl<ITextControl>("licenseWidgetHeader", true);

    protected virtual HtmlContainerControl RenewInfoWrapper => this.Container.GetControl<HtmlContainerControl>("renewInstructionsalTextWrapper", true);

    protected virtual ITextControl RenewInfo => this.Container.GetControl<ITextControl>("renewInstructionalTextHeader", true);

    protected virtual ITextControl AdditionalInfo => this.Container.GetControl<ITextControl>("additionalInfoLiteral", true);

    protected virtual ITextControl ExpirationDate => this.Container.GetControl<ITextControl>("expirationDate", true);

    protected virtual ITextControl LicenseHolder => this.Container.GetControl<ITextControl>("licenseHolder", true);

    protected virtual ITextControl DaysRemaining => this.Container.GetControl<ITextControl>("daysRemaining", true);

    protected virtual ITextControl DaysRemainingLabel => this.Container.GetControl<ITextControl>("daysRemainingLabel", true);

    protected virtual Label OrLabel => this.Container.GetControl<Label>("orLabel", true);

    protected virtual HtmlContainerControl DaysRemainingWrapper => this.Container.GetControl<HtmlContainerControl>("daysRemainingWrapper", true);

    protected virtual HyperLink PurchaseRenewalLink => this.Container.GetControl<HyperLink>("purchaseRenewalLink", false);

    protected virtual HyperLink PurchaseLicenseLink => this.Container.GetControl<HyperLink>("purchaseLicenseLink", false);

    /// <summary>
    /// Gets the learn more button link.
    /// This is used for recurring billing licenses.
    /// </summary>
    /// <value>The learn more button link.</value>
    protected virtual HyperLink WhyRenewLinkButton => this.Container.GetControl<HyperLink>("whyRenewLinkButton", false);

    /// <summary>
    /// Gets the learn more button link.
    /// This is used for perpetual billing licenses.
    /// </summary>
    /// <value>The learn more button link.</value>
    protected virtual HyperLink WhyRenewLink => this.Container.GetControl<HyperLink>("whyRenewLink", false);

    protected virtual LinkButton UpdateLicenseButton => this.Container.GetControl<LinkButton>("lbGetLicense", true);

    protected virtual LicenseRegistration PDownloadLicense => this.Container.GetControl<LicenseRegistration>("licDownloadLicense", true);

    protected virtual ListView ExpiredLicenseMessages => this.Container.GetControl<ListView>("expiredLicenseMessages", true);

    protected virtual Literal TrialLicenseExpiredMessage => this.Container.GetControl<Literal>("trialLicenseExpiredMessage", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      if (!AppPermission.IsGranted(AppAction.ManageLicenses))
      {
        this.Visible = false;
      }
      else
      {
        this.CurrentLicense = LicenseState.Current.LicenseInfo;
        DateTime expirationDate = this.CurrentLicense.ExpirationDate;
        this.ExpirationDate.Text = expirationDate.ToString("dd MMM yyyy");
        this.LicenseHolder.Text = string.Format("{0} ({1})", (object) this.CurrentLicense.Customer.Name, (object) this.CurrentLicense.Customer.Email);
        TimeSpan timeSpan = expirationDate - DateTime.UtcNow;
        bool flag = timeSpan.Ticks < 0L;
        if (this.CurrentLicense.IsRecurringBilling)
        {
          if (flag)
          {
            this.AddCssClassToRemainingDaysWrapper("expiredLicense");
            this.DaysRemaining.Text = string.Empty;
            this.Header.Text = string.Format(Res.Get<LicensingMessages>().LicenseExpiredMonthlySubscriptionMessage, (object) this.CurrentLicense.ProductVersion);
            int num = 60 + timeSpan.Days;
            if (num < 0)
              num = 0;
            this.RenewInfo.Text = Res.Get<LicensingMessages>().UnableToUpdateLicense;
            this.AdditionalInfo.Text = string.Format(Res.Get<LicensingMessages>().SiteWillStopWorking, (object) num);
            this.RenewInfoWrapper.Visible = true;
            this.WhyRenewLinkButton.Visible = true;
            this.WhyRenewLink.Visible = false;
          }
          else
            this.Visible = false;
        }
        else if (flag)
        {
          this.AddCssClassToRemainingDaysWrapper("expiredLicense");
          this.DaysRemaining.Text = string.Empty;
          this.Header.Text = string.Format(Res.Get<LicensingMessages>().LicenseExpiredSubscriptionMessage, (object) this.CurrentLicense.ProductVersion);
          this.UpdateLicenseButton.Click += new EventHandler(this.UpdateLicenseButton_Click);
          this.PurchaseRenewalLink.Visible = true;
          if (SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile))
          {
            this.OrLabel.Visible = true;
            this.UpdateLicenseButton.Visible = true;
          }
        }
        else
        {
          int num = timeSpan.Days + 1;
          this.DaysRemaining.Text = num.ToString();
          this.DaysRemainingLabel.Text = Res.Get<LicensingMessages>().DaysRemaining;
          if (this.CurrentLicense.IsTrial)
          {
            this.PurchaseLicenseLink.Visible = true;
            this.Header.Text = Res.Get<LicensingMessages>().LicenseFreeTrialHeader;
            this.TrialLicenseExpiredMessage.Text = Res.Get<LicensingMessages>().TrialLicenseExpiredMessage;
            this.TrialLicenseExpiredMessage.Visible = true;
            this.WhyRenewLink.Visible = false;
            this.ExpiredLicenseMessages.Visible = false;
            this.AddCssClassToRemainingDaysWrapper("trialLicense");
          }
          else
          {
            this.PurchaseRenewalLink.Visible = true;
            this.Header.Text = Res.Get<LicensingMessages>().LicenseExpirationDateApproaching;
            if (num <= 7)
              this.AddCssClassToRemainingDaysWrapper("firstPeriodLicense");
            else if (num <= 30)
              this.AddCssClassToRemainingDaysWrapper("secondPeriodLicense");
            else if (num <= 60)
              this.AddCssClassToRemainingDaysWrapper("thirdPeriodLicense");
            else
              this.Visible = false;
          }
        }
        if (!this.Visible)
          return;
        this.BindExipiredLicenseMessages();
      }
    }

    /// <summary>
    /// Retrieves error messages for expired license for modules that are included in it.
    /// </summary>
    /// <returns>Collection with the ExpiredLicenseMessage</returns>
    private IList<string> GetExipiredLicenseMessages()
    {
      List<string> exipiredLicenseMessages = new List<string>();
      foreach (ISubscriptionBasedModule subscriptionBasedModule in SystemManager.ApplicationModules.Values.OfType<ISubscriptionBasedModule>())
        exipiredLicenseMessages.Add(subscriptionBasedModule.GetSubscriptionMessage());
      return (IList<string>) exipiredLicenseMessages;
    }

    private void BindExipiredLicenseMessages()
    {
      IList<string> exipiredLicenseMessages = this.GetExipiredLicenseMessages();
      if (exipiredLicenseMessages.Count > 0)
        exipiredLicenseMessages.Insert(0, Res.Get<LicensingMessages>().ExpiredSitefinityLicenseMessage);
      this.ExpiredLicenseMessages.DataSource = (object) exipiredLicenseMessages;
      this.ExpiredLicenseMessages.DataBind();
    }

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = LicenseExpirationWidget.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => this.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Overridden. Cancels the rendering of a beginning HTML tag for the control.
    /// </summary>
    /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Overridden. Cancels the rendering of an ending HTML tag for the control.
    /// </summary>
    /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    private void AddCssClassToRemainingDaysWrapper(string cssClass) => this.DaysRemainingWrapper.Attributes.Add("class", cssClass);

    private void UpdateLicenseButton_Click(object sender, EventArgs e) => this.PDownloadLicense.UpdateLicenseFromServer();
  }
}
