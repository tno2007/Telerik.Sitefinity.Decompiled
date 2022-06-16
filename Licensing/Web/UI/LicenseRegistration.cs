// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.UI.LicenseRegistration
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Licensing.Web.Services;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Licensing.Web.UI
{
  /// <summary>User control for licensing registration</summary>
  public class LicenseRegistration : SimpleView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Licensing.LicenseRegistration.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LicenseRegistration.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      LicenseState current = LicenseState.Current;
      if (current != null && !current.MissingLicense && !current.InvalidLicense && current.LicenseInfo != null && !current.LicenseInfo.IsCorrupted && current.LicenseInfo.IsValid && !current.HasExpired)
      {
        if (!AppPermission.IsGranted(AppAction.ManageLicenses))
          ProtectedRoute.HandleItemViewNotAllowed(SystemManager.CurrentHttpContext, "Access Denied");
      }
      this.BindData();
    }

    protected virtual void BindData()
    {
      this.BindDefaultChoises();
      this.BindSitefinityEditions();
      this.AttachEvents();
      this.SetupPanels();
      this.RbAutomatic.Checked = true;
      this.DisplayHeaderMessage.Visible = this.DisplayHeaderInformation;
      this.WhayIsTelerikAccountHint.Text = string.Format(Res.Get<LicensingMessages>().WhayIsTelerikAccountHint, (object) Res.Get<LicensingMessages>().ExternalLinkTelerikAccount, (object) Res.Get<LicensingMessages>().ExternalLinkForgotPassword);
      if (!LicenseState.Current.MissingLicense && !LicenseState.Current.LicenseInfo.IsCorrupted && LicenseState.Current.LicenseInfo.ExpirationDate < DateTime.UtcNow)
      {
        if (LicenseState.Current.LicenseInfo.IsTrial)
        {
          this.TrialMessage.Text = "Your license has expired";
          this.TrialExtensionPanel.Visible = true;
          this.ActivateNewLicenseLiteral.Visible = true;
          this.ToRunYouNeedToActivateLiteral.Visible = false;
        }
        else if (LicenseState.Current.LicenseInfo.IsHosted)
        {
          this.HostedMessage.Text = "Your license has expired";
          this.IfNeedExtendHosted.Text = Res.Get<LicensingMessages>().IfYouNeedToExtendHosted.Arrange((object) Res.Get<LicensingMessages>().ExternalLinkIfYouNeedToExtendHosted);
          this.HostedEditionExtensionPanel.Visible = true;
          this.ActivateNewLicenseLiteral.Visible = false;
          this.ToRunYouNeedToActivateLiteral.Visible = false;
          this.HideGetLicensePanel = true;
        }
      }
      if (this.LitSitefinityVersionMessage != null)
        this.LitSitefinityVersionMessage.Text = string.Format(Res.Get<LicensingMessages>().SitefinityVersionMessage, (object) LicenseState.GetProductVersion.ToString(2));
      this.PnlContent.Visible = !this.HideGetLicensePanel;
    }

    protected virtual void BindDefaultChoises()
    {
      this.RblLicenseType.Items.Add(new ListItem()
      {
        Text = "Purchased",
        Value = "False"
      });
      this.RblLicenseType.Items.Add(new ListItem()
      {
        Text = "Evaluation (Free Trial)",
        Value = "True"
      });
      this.RblLicenseType.SelectedIndex = 0;
      this.RblLicenseType.EnableViewState = true;
      if (!LicenseState.Current.MissingLicense)
      {
        this.RblLicenseType.Visible = false;
        this.RblLicenseType.Items.Clear();
      }
      this.RblLicenseType.Attributes.Add("onChange", "return hideTrialChoiseEditions();");
      this.RblLicenseType.DataBind();
    }

    protected virtual void SetupPanels()
    {
      this.PanelLincensingMode.Visible = true;
      this.PanelManualMode.Visible = false;
    }

    protected virtual void BindSitefinityEditions()
    {
      this.DddlEditionsLists.Items.Add(new ListItem()
      {
        Text = "Standard",
        Value = "SE"
      });
      this.DddlEditionsLists.Items.Add(new ListItem()
      {
        Text = "Professional",
        Value = "PE"
      });
      this.DddlEditionsLists.Items.Add(new ListItem()
      {
        Text = "Online Marketing Edition",
        Value = "OME"
      });
      this.DddlEditionsLists.Items.Add(new ListItem()
      {
        Text = "Enterprise",
        Value = "PU"
      });
      this.DddlEditionsLists.Items.Add(new ListItem()
      {
        Text = "Intranet Standard",
        Value = "ISE"
      });
      this.DddlEditionsLists.DataBind();
      this.DddlEditionsLists.Attributes.Add("onChange", "return hideLicenseChoiseForComunity();");
      this.DddlEditionsLists.SelectedIndex = 2;
    }

    protected virtual void AttachEvents()
    {
      this.RbAutomatic.CheckedChanged += new EventHandler(this.ModeCheckedChanged);
      this.RbManual.CheckedChanged += new EventHandler(this.ModeCheckedChanged);
      this.BtnActivateLicenseManual.Click += new EventHandler(this.BtnActivateLicenseManual_Click);
      this.BtnActivateLicenseAutomatic.Click += new EventHandler(this.BtnActivateLicenseAutomatic_Click);
      this.BtnUploadLicense.Click += new EventHandler(this.BtnUploadLicense_Click);
      this.BtnCancelUploadLicense.Click += new EventHandler(this.BtnCancelUploadLicense_Click);
      this.BtnGetNewLicense.Click += new EventHandler(this.BtnGetNewLicense_Click);
      this.BtnExtendHostedLicense.Click += new EventHandler(this.BtnExtendHostedLicense_Click);
    }

    private void BtnExtendHostedLicense_Click(object sender, EventArgs e)
    {
      LicenseState.InvalidateLicense(false);
      string str = HttpUtility.UrlDecode(this.Page.Request.QueryString[SecurityManager.AuthenticationReturnUrl]);
      IRedirectUriValidator redirectUriValidator = ObjectFactory.Resolve<IRedirectUriValidator>();
      if (!string.IsNullOrEmpty(str) && redirectUriValidator.IsValid(str))
        this.Page.Response.Redirect(str);
      else
        this.Page.Response.Redirect("~/");
    }

    private void BtnGetNewLicense_Click(object sender, EventArgs e) => this.UpdateLicenseFromServer();

    private void BtnCancelUploadLicense_Click(object sender, EventArgs e)
    {
    }

    private void BtnUploadLicense_Click(object sender, EventArgs e) => this.SaveLicense();

    private void BtnActivateLicenseAutomatic_Click(object sender, EventArgs e) => this.DownloadLicense();

    private void BtnActivateLicenseManual_Click(object sender, EventArgs e) => this.GetLicenseFromFile();

    public void ModeCheckedChanged(object sender, EventArgs e)
    {
      if (this.RbAutomatic.Checked)
      {
        this.PanelLincensingMode.Visible = true;
        this.PanelManualMode.Visible = false;
      }
      else
      {
        this.PanelLincensingMode.Visible = false;
        this.PanelManualMode.Visible = true;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool DisplayHeaderInformation { get; set; }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool HideGetLicensePanel { get; set; }

    protected virtual Panel PanelLincensingMode => this.Container.GetControl<Panel>("pLicensingMode", true);

    protected virtual Panel PanelManualMode => this.Container.GetControl<Panel>("pManualMode", true);

    protected virtual Panel PnlContent => this.Container.GetControl<Panel>("pnlContent", true, TraverseMethod.DepthFirst);

    protected virtual HiddenField hfLicenseInfo => this.Container.GetControl<HiddenField>(nameof (hfLicenseInfo), true);

    protected virtual Literal TrialMessage => this.Container.GetControl<Literal>("trialMessage", true);

    protected virtual Literal WhayIsTelerikAccountHint => this.Container.GetControl<Literal>("lWhayIsTelerikAccountHint", true);

    protected virtual Panel DisplayHeaderMessage => this.Container.GetControl<Panel>("displayHeaderMessage", true);

    protected virtual RadioButton RbManual => this.Container.GetControl<RadioButton>("rbManualMode", true);

    protected virtual RadioButton RbAutomatic => this.Container.GetControl<RadioButton>("rbAutomaticMode", true);

    protected virtual RadioButtonList RblLicenseType => this.Container.GetControl<RadioButtonList>("rbLicenseType", true);

    protected virtual TextBox TbUsername => this.Container.GetControl<TextBox>("tbUsername", true);

    protected virtual TextBox TbPassword => this.Container.GetControl<TextBox>("tbPassword", true);

    protected virtual DropDownList DddlEditionsLists => this.Container.GetControl<DropDownList>("ddlEditionsList", true);

    protected virtual FileUpload FuLicenseFile => this.Container.GetControl<FileUpload>("fuLicenseFile", true);

    protected virtual LinkButton BtnActivateLicenseManual => this.Container.GetControl<LinkButton>("btnUploadFile", true, TraverseMethod.BreadthFirst);

    protected virtual LinkButton BtnActivateLicenseAutomatic => this.Container.GetControl<LinkButton>("btnActivate", true);

    protected virtual LinkButton BtnUploadLicense => this.Container.GetControl<LinkButton>("btnContinueWithUpdate", true, TraverseMethod.DepthFirst);

    protected virtual LinkButton BtnCancelUploadLicense => this.Container.GetControl<LinkButton>("btnCancelUpdate", true, TraverseMethod.DepthFirst);

    protected virtual Telerik.Web.UI.RadWindow ConfirmWindow => this.Container.GetControl<Telerik.Web.UI.RadWindow>("confirmWindow", true);

    protected virtual BasicLicenseView BasicLicenseData => this.Container.GetControl<BasicLicenseView>("basicLicenseData", true, TraverseMethod.DepthFirst);

    protected virtual LicenseDataView LicDataDialog => this.Container.GetControl<LicenseDataView>("licData2", true, TraverseMethod.DepthFirst);

    protected virtual Telerik.Web.UI.RadWindow ErrorDialog => this.Container.GetControl<Telerik.Web.UI.RadWindow>("promptWindow", true);

    protected virtual Literal ErrorMessage => this.Container.GetControl<Literal>("errorMessage", true, TraverseMethod.DepthFirst);

    protected Literal LicenseInfoLabel => this.Container.GetControl<Literal>("lLicenseInfoLabel", true, TraverseMethod.DepthFirst);

    protected Literal LtrlLicenseSaveOr => this.Container.GetControl<Literal>("ltrlLicenseSaveOr", true, TraverseMethod.DepthFirst);

    protected virtual LinkButton BtnGetNewLicense => this.Container.GetControl<LinkButton>("extendLicenseButton", true, TraverseMethod.DepthFirst);

    protected virtual Panel TrialExtensionPanel => this.Container.GetControl<Panel>("trialExtensionPanel", true, TraverseMethod.DepthFirst);

    protected virtual Label ActivateNewLicenseLiteral => this.Container.GetControl<Label>("activateNew", true, TraverseMethod.DepthFirst);

    protected virtual Label ToRunYouNeedToActivateLiteral => this.Container.GetControl<Label>("toRunYouNeedToActivate", true, TraverseMethod.DepthFirst);

    protected virtual Panel HostedEditionExtensionPanel => this.Container.GetControl<Panel>("hostedEditionExtensionPanel", true, TraverseMethod.DepthFirst);

    protected virtual LinkButton BtnExtendHostedLicense => this.Container.GetControl<LinkButton>("extendHostedLicenseButton", true, TraverseMethod.DepthFirst);

    protected virtual Literal HostedMessage => this.Container.GetControl<Literal>("hostedMessage", true);

    protected virtual Literal IfNeedExtendHosted => this.Container.GetControl<Literal>("ifNeedExtendHosted", true);

    protected virtual Literal LitSitefinityVersionMessage => this.Container.GetControl<Literal>("litSitefinityVersionMessage", false);

    protected Literal LtrlConfirmationTitle => this.Container.GetControl<Literal>("ltrlConfirmationTitle", false, TraverseMethod.DepthFirst);

    protected virtual void GetLicenseFromFile()
    {
      string empty = string.Empty;
      try
      {
        empty = new UTF8Encoding(false, true).GetString(this.FuLicenseFile.FileBytes);
        this.hfLicenseInfo.Value = this.Page.Server.HtmlEncode(empty);
      }
      catch (Exception ex)
      {
        this.HandleError(new Exception("There is a problem with the parsing of the license", ex));
      }
      this.ShowLicenseConfirmation(empty);
    }

    protected virtual void SaveLicense()
    {
      try
      {
        LicenseState.SaveLicense(this.Page.Server.HtmlDecode(this.hfLicenseInfo.Value));
        LicenseState.InvalidateLicense();
        string str = HttpUtility.UrlDecode(this.Page.Request.QueryString[SecurityManager.AuthenticationReturnUrl]);
        IRedirectUriValidator redirectUriValidator = ObjectFactory.Resolve<IRedirectUriValidator>();
        if (!string.IsNullOrEmpty(str) && redirectUriValidator.IsValid(str))
          this.Page.Response.Redirect(str);
        else
          this.Page.Response.Redirect("~/Sitefinity");
      }
      catch (Exception ex)
      {
        this.HandleError(ex);
      }
    }

    public virtual void UpdateLicenseFromServer()
    {
      string empty = string.Empty;
      string sitefinityLicense;
      try
      {
        sitefinityLicense = LicenseUpdater.GetSitefinityLicense(LicenseState.Current.LicenseInfo.LicenseId);
      }
      catch (Exception ex)
      {
        this.HandleError(ex);
        return;
      }
      if (!(sitefinityLicense != string.Empty))
        return;
      this.hfLicenseInfo.Value = this.Page.Server.HtmlEncode(sitefinityLicense);
      this.ShowLicenseConfirmation(sitefinityLicense);
    }

    /// <summary>
    /// Shows the license confirmation screen. Includes warnings for unregistered domain and changes of key
    /// </summary>
    /// <param name="licenseData">The license data.</param>
    protected virtual void ShowLicenseConfirmation(string licenseData)
    {
      Telerik.Web.UI.RadWindow confirmWindow = this.ConfirmWindow;
      confirmWindow.Visible = true;
      confirmWindow.Width = Unit.Pixel(500);
      confirmWindow.Height = Unit.Pixel(500);
      LicenseDataView licDataDialog = this.LicDataDialog;
      LicenseState license = LicenseState.ParseLicense(licenseData);
      if (!license.InvalidProductVersion)
      {
        string str = "";
        foreach (object obj in this.DddlEditionsLists.Items)
        {
          if (((ListItem) obj).Value == license.LicenseInfo.LicenseType)
            str = ((ListItem) obj).Text;
        }
        this.LicenseInfoLabel.Text = string.Format("You will activate a license for Progress Sitefinity CMS {0}.<br />Check the license details", (object) str);
        this.LicDataDialog.Visible = true;
        licDataDialog.DataSource = license;
        licDataDialog.DataBind();
      }
      else
      {
        licDataDialog.Visible = false;
        this.BtnCancelUploadLicense.Text = Res.Get<Labels>().Close;
        this.BtnCancelUploadLicense.CssClass = "sfLinkBtn";
        if (this.LtrlConfirmationTitle != null)
          this.LtrlConfirmationTitle.Text = Res.Get<LicensingMessages>().LicenseCannotBeActivated;
      }
      this.BasicLicenseData.Visible = true;
      this.BasicLicenseData.DataSource = license;
      this.BasicLicenseData.DataBind();
      this.LtrlLicenseSaveOr.Visible = this.BtnUploadLicense.Visible = !license.InvalidSignature && !license.LicenseInfo.IsCorrupted && !license.MissingLicense && !license.InvalidProductVersion;
    }

    protected virtual void DownloadLicense()
    {
      string empty = string.Empty;
      string str;
      try
      {
        bool result = false;
        bool.TryParse(this.RblLicenseType.SelectedValue, out result);
        if (result)
          this.DddlEditionsLists.SelectedValue = "PU";
        str = LicenseUpdater.ActivateSitefinity(this.TbUsername.Text, this.TbPassword.Text, this.DddlEditionsLists.SelectedValue, result);
      }
      catch (Exception ex)
      {
        this.HandleError(ex);
        return;
      }
      if (!(str != string.Empty))
        return;
      this.hfLicenseInfo.Value = this.Page.Server.HtmlEncode(str);
      this.ShowLicenseConfirmation(str);
    }

    protected override void Render(HtmlTextWriter writer) => base.Render(writer);

    protected virtual void HandleError(Exception ex)
    {
      Telerik.Web.UI.RadWindow errorDialog = this.ErrorDialog;
      errorDialog.Visible = true;
      errorDialog.Width = Unit.Pixel(500);
      errorDialog.Height = Unit.Pixel(500);
      this.ErrorMessage.Text = ex.Message;
    }
  }
}
