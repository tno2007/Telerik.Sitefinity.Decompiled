// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.UI.BasicLicenseView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Licensing.Web.UI
{
  /// <summary>
  /// a control that shows formatted licensing data (license key, domains, modules, number of users etc.)
  /// </summary>
  public class BasicLicenseView : SimpleView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Licensing.BasicLicenseView.ascx");

    /// <summary>Gets or sets the license data to show.</summary>
    /// <value>The data source.</value>
    public LicenseState DataSource { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to the show license key.
    /// </summary>
    public bool ShowLicenseKey { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BasicLicenseView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual RadListView RlvPurchaseInformation => this.Container.GetControl<RadListView>("rlvPurchaseInformation", true);

    protected virtual HtmlGenericControl InvalidLicenseMessages => this.Container.GetControl<HtmlGenericControl>("invalidLicenseMessages", true);

    protected virtual HtmlGenericControl InvalidLicenseInfoLabel => this.Container.GetControl<HtmlGenericControl>("invalidLicenseInfoLabel", true);

    protected virtual HtmlGenericControl InvalidLicenseInfoErrors => this.Container.GetControl<HtmlGenericControl>("invalidLicenseInfoErrors", true);

    protected virtual Repeater RptValidationErrors => this.Container.GetControl<Repeater>("rptValidationErrors", true);

    protected virtual HtmlGenericControl MissingLicense => this.Container.GetControl<HtmlGenericControl>("missingLicense", true);

    protected virtual HtmlGenericControl InvalidProductVersion => this.Container.GetControl<HtmlGenericControl>("invalidProductVersion", true);

    protected virtual HtmlGenericControl InvalidDomain => this.Container.GetControl<HtmlGenericControl>("invalidDomain", true);

    protected virtual HtmlGenericControl InvalidSignature => this.Container.GetControl<HtmlGenericControl>("invalidSignature", true);

    protected virtual HtmlGenericControl CorruptedLicense => this.Container.GetControl<HtmlGenericControl>("corruptedLicense", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    protected override void InitializeControls(GenericContainer container) => this.DataBind();

    /// <summary>
    /// Binds a data source to the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> and all its child controls.
    /// </summary>
    public new void DataBind()
    {
      if (this.DataSource == null)
        return;
      if (this.DataSource.IsLicenseDataOk)
        this.BindLicenseInfo();
      else
        this.BindErrors();
    }

    private void BindLicenseInfo()
    {
      this.RlvPurchaseInformation.RegisterWithScriptManager = false;
      this.RlvPurchaseInformation.DataSource = (object) new LicenseInfo[1]
      {
        this.DataSource.LicenseInfo
      };
      this.RlvPurchaseInformation.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(this.rlvProductInformation_ItemDataBound);
    }

    private void BindErrors()
    {
      LicenseState dataSource = this.DataSource;
      HtmlGenericControl invalidLicenseMessages = this.InvalidLicenseMessages;
      Res.Get<LicensingMessages>();
      if (dataSource.IsLicenseDataOk)
        return;
      invalidLicenseMessages.Visible = true;
      if (!dataSource.LicenseInfo.IsCorrupted)
      {
        if (!dataSource.LicenseInfo.IsValid && !dataSource.MissingLicense)
        {
          this.InvalidLicenseInfoLabel.Visible = true;
          this.InvalidLicenseInfoErrors.Visible = true;
          Repeater validationErrors = this.RptValidationErrors;
          validationErrors.Visible = true;
          validationErrors.DataSource = (object) ((IEnumerable<string>) dataSource.LicenseInfo.GetValidationErrors()).Select(u => new
          {
            ValidationError = u
          });
        }
        if (dataSource.MissingLicense)
          this.MissingLicense.Visible = true;
        if (dataSource.MissingLicense)
          return;
        if (dataSource.InvalidProductVersion)
        {
          this.InvalidProductVersion.Visible = true;
          string str = LicenseState.GetProductVersion.ToString(2);
          this.InvalidProductVersion.InnerText = string.Format(Res.Get<LicensingMessages>().InvalidLicenseVersion, (object) str);
        }
        if (dataSource.InvalidDomain)
          this.InvalidDomain.Visible = true;
        if (!dataSource.InvalidSignature)
          return;
        this.InvalidSignature.Visible = true;
      }
      else
        this.CorruptedLicense.Visible = true;
    }

    private void rlvProductInformation_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      Literal control = e.Item.FindControl("ddEditionType") as Literal;
      switch (this.DataSource.LicenseInfo.LicenseType)
      {
        case "CL":
          e.Item.FindControl("dtEditionTypeElement").Visible = false;
          e.Item.FindControl("ddEditionTypeElement").Visible = false;
          break;
        case "ISE":
          control.Text = "Intranet Standard Edition";
          break;
        case "MS":
          control.Text = "Multisite Edition";
          break;
        case "OME":
          control.Text = "Online Marketing Edition";
          break;
        case "PE":
          control.Text = "Professional Edition";
          break;
        case "PU":
          control.Text = "Enterprise Edition";
          break;
        case "SB":
          control.Text = "Small Business Edition";
          break;
        case "SE":
          control.Text = "Standard Edition";
          break;
      }
      if (this.DataSource.LicenseInfo.LicenseType != "CL" && !this.DataSource.LicenseInfo.IsRecurringBilling)
      {
        e.Item.FindControl("dtExpirationDateElement").Visible = false;
        e.Item.FindControl("ddExpirationDateElement").Visible = false;
      }
      (e.Item.FindControl("ddLicenseType") as Literal).Text = !this.DataSource.LicenseInfo.IsRecurringBilling ? Res.Get<LicensingMessages>().Perpetual : Res.Get<LicensingMessages>().Subscription;
      (e.Item.FindControl("sfProductVersion") as Literal).Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      (e.Item.FindControl("sfProductFileVersion") as Literal).Text = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
      if (!string.IsNullOrEmpty(this.DataSource.LicenseInfo.Customer.CompanyName))
        return;
      e.Item.FindControl("dtCompanyNameElement").Visible = false;
      e.Item.FindControl("ddCompanyNameElement").Visible = false;
    }
  }
}
