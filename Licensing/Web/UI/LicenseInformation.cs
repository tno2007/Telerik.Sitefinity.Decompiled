// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.UI.LicenseInformationControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Licensing.Web.UI
{
  /// <summary>
  ///  This control is used for viewing license information and adding/updating a license
  /// </summary>
  public class LicenseInformationControl : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Licensing.LicenseInformation.ascx");

    protected virtual BasicLicenseView BasiclicenseData => this.Container.GetControl<BasicLicenseView>("basicLicenseData", true);

    protected virtual LicenseDataView LicenseDataView => this.Container.GetControl<LicenseDataView>("licenseDataView", true);

    protected virtual Panel ShowWindow => this.Container.GetControl<Panel>("pnlWizzard", true);

    protected virtual LicenseRegistration PDownloadLicense => this.Container.GetControl<LicenseRegistration>("licDownloadLicense", true);

    protected virtual HtmlGenericControl ChangeLicense => this.Container.GetControl<HtmlGenericControl>("changeLicense", true);

    protected virtual LinkButton BtnNewLicense => this.Container.GetControl<LinkButton>("btnNewLicense", true);

    protected virtual LinkButton BtnGetNewLicense => this.Container.GetControl<LinkButton>("lbGetLicense", true);

    protected virtual LinkButton NewLicenseWindowClosed => this.Container.GetControl<LinkButton>("newLicenseWindowClosed", true, TraverseMethod.DepthFirst);

    protected virtual HiddenField NewLicenseWindowedShowed => this.Container.GetControl<HiddenField>("windowShowed", true, TraverseMethod.DepthFirst);

    protected virtual Panel TrialMessagePanel => this.Container.GetControl<Panel>("pnlTrialMessage", true, TraverseMethod.DepthFirst);

    protected virtual Panel HostedMessagePanel => this.Container.GetControl<Panel>("pnlHostedMessage", true, TraverseMethod.DepthFirst);

    protected virtual Literal DaysLeft => this.Container.GetControl<Literal>("daysLeft", true, TraverseMethod.DepthFirst);

    protected virtual Literal HostedLicenseDaysLeft => this.Container.GetControl<Literal>("hostedDaysLeft", true, TraverseMethod.DepthFirst);

    protected virtual Literal LEdition => this.Container.GetControl<Literal>("lEdition", true, TraverseMethod.DepthFirst);

    protected virtual LinkButton BtnInvalidateLicense => this.Container.GetControl<LinkButton>("btnInvalidateLicense", true);

    protected override void InitializeControls(GenericContainer container)
    {
      int num = (LicenseState.Current.LicenseInfo.ExpirationDate - DateTime.UtcNow).Days + 1;
      this.BindData();
      this.ShowWindow.Visible = true;
      if (LicenseState.Current.LicenseInfo.LicenseType == "CL")
      {
        this.ChangeLicense.Visible = false;
      }
      else
      {
        this.BtnInvalidateLicense.Click += (EventHandler) ((sender, eventArgs) =>
        {
          LicenseState.InvalidateLicense();
          this.Page.Response.Redirect(this.Page.Request.Url.AbsoluteUri);
        });
        this.BtnNewLicense.Click += new EventHandler(this.btnNewLicense_Click);
        this.NewLicenseWindowClosed.Click += new EventHandler(this.NewLicenseWindowClosed_Click);
        this.BtnGetNewLicense.Click += new EventHandler(this.BtnGetNewLicense_Click);
      }
      this.TrialMessagePanel.Visible = LicenseState.Current.MissingLicense || LicenseState.Current.LicenseInfo.IsTrial;
      this.HostedMessagePanel.Visible = LicenseState.Current.LicenseInfo.IsHosted;
      this.DaysLeft.Text = num.ToString();
      if (LicenseState.Current.LicenseInfo.IsHosted)
        this.HostedLicenseDaysLeft.Text = num.ToString();
      string str = string.Empty;
      switch (LicenseState.Current.LicenseInfo.LicenseType)
      {
        case "CL":
          str = "Cloud";
          break;
        case "ISE":
          str = string.Format("CMS {0} Intranet Standard Edition", (object) LicenseState.Current.LicenseInfo.ProductVersion);
          break;
        case "MS":
          str = string.Format("CMS {0} Multisite Edition", (object) LicenseState.Current.LicenseInfo.ProductVersion);
          break;
        case "OME":
          str = string.Format("CMS {0} Online Marketing Edition", (object) LicenseState.Current.LicenseInfo.ProductVersion);
          break;
        case "PE":
          str = string.Format("CMS {0} Professional Edition", (object) LicenseState.Current.LicenseInfo.ProductVersion);
          break;
        case "PU":
          str = string.Format("CMS {0} Enterprise Edition", (object) LicenseState.Current.LicenseInfo.ProductVersion);
          break;
        case "SB":
          str = string.Format("CMS {0} Small Business Edition", (object) LicenseState.Current.LicenseInfo.ProductVersion);
          break;
        case "SE":
          str = string.Format("CMS {0} Standard Edition", (object) LicenseState.Current.LicenseInfo.ProductVersion);
          break;
      }
      this.LEdition.Text = string.Format("Progress Sitefinity {0}", (object) str);
      if (!LicenseState.Current.IsInTrialMode && !LicenseState.Current.LicenseInfo.IsHosted)
        return;
      HtmlGenericControl control1 = this.Container.GetControl<HtmlGenericControl>("dtNewLicense", true, TraverseMethod.DepthFirst);
      HtmlGenericControl control2 = this.Container.GetControl<HtmlGenericControl>("ddNewLicense", true, TraverseMethod.DepthFirst);
      control1.Attributes.Add("style", "display:none");
      control2.Attributes.Add("style", "display:none");
    }

    private void BtnGetNewLicense_Click(object sender, EventArgs e) => this.PDownloadLicense.UpdateLicenseFromServer();

    protected override void Render(HtmlTextWriter writer)
    {
      this.ShowWindow.Visible = !(this.NewLicenseWindowedShowed.Value == false.ToString()) && !(this.NewLicenseWindowedShowed.Value == string.Empty);
      base.Render(writer);
    }

    private void NewLicenseWindowClosed_Click(object sender, EventArgs e)
    {
      this.NewLicenseWindowedShowed.Value = false.ToString();
      this.ShowWindow.Visible = false;
    }

    private void btnNewLicense_Click(object sender, EventArgs e)
    {
      this.NewLicenseWindowedShowed.Value = true.ToString();
      this.ShowWindow.Visible = true;
    }

    /// <summary>
    /// Shows the the license data and license validation messages.
    /// </summary>
    protected virtual void BindData()
    {
      this.BasiclicenseData.DataSource = this.GetCurrentLicense();
      this.BasiclicenseData.DataBind();
      this.LicenseDataView.DataSource = this.GetCurrentLicense();
      this.LicenseDataView.DataBind();
    }

    internal virtual LicenseState GetCurrentLicense() => LicenseState.Current;

    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LicenseInformationControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) null;

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) null;
  }
}
