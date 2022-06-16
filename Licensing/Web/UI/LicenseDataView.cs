// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.UI.LicenseDataView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Licensing.Web.UI
{
  /// <summary>
  /// a control that shows formatted licensing data (license key, domains, modules, number of users etc.)
  /// </summary>
  public class LicenseDataView : SimpleView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Licensing.LicenseDataView.ascx");

    /// <summary>Gets or sets the license data to show.</summary>
    /// <value>The data source.</value>
    public LicenseState DataSource { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LicenseDataView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the literal control displaying Sitefinity version.
    /// </summary>
    protected virtual Literal LblSitefinityVersion => this.Container.GetControl<Literal>("litVersion", true, TraverseMethod.DepthFirst);

    /// <summary>Gets the placeholder control displaying domains list.</summary>
    protected virtual PlaceHolder DomainsListPlaceholder => this.Container.GetControl<PlaceHolder>("domainsListPlaceholder", true);

    /// <summary>Gets the repeater control displaying domains list.</summary>
    protected virtual Repeater DomainsRepeater => this.Container.GetControl<Repeater>("domainsRepeater", true);

    /// <summary>
    /// Gets the literal control displaying the total user limit.
    /// </summary>
    protected virtual Literal TotalUsersLimitsLiteral => this.Container.GetControl<Literal>("totalUsersLimitsLiteral", true);

    /// <summary>
    /// Gets the literal control displaying the total content limit.
    /// </summary>
    protected virtual Literal TotalContentLimitsLiteral => this.Container.GetControl<Literal>("totalContentLimitsLiteral", true);

    /// <summary>
    /// Gets the literal control displaying the total pages limit.
    /// </summary>
    protected virtual Literal TotalPagesLimitsLiteral => this.Container.GetControl<Literal>("totalPagesLimitsLiteral", true);

    /// <summary>
    /// Gets the literal control displaying whether subdomains are allowed.
    /// </summary>
    protected virtual Literal SubDomainsAllowedLiteral => this.Container.GetControl<Literal>("subDomainsAllowedLiteral", true);

    /// <summary>
    /// Gets the repeater control displaying the common modules.
    /// </summary>
    protected virtual Repeater ModulesRepeater => this.Container.GetControl<Repeater>("modulesRepeater", true);

    /// <summary>Gets the repeater control displaying the addons.</summary>
    protected virtual Repeater AddonsRepeater => this.Container.GetControl<Repeater>("addonsRepeater", true);

    /// <summary>Gets the container displaying the addons label.</summary>
    protected virtual Control PackageAndAddonsLabel => this.Container.GetControl<Control>("packageAndAddonsLabelContainer", true);

    /// <summary>Gets the container displaying the addons.</summary>
    protected virtual Control PackageAndAddonsContainer => this.Container.GetControl<Control>("packageAndAddonsContainer", true);

    /// <summary>
    /// Gets the literal control displaying license information per domain.
    /// </summary>
    protected virtual Repeater DomainsLicenseInfoRepeater => this.Container.GetControl<Repeater>("domainsLicenseInfoRepeater", true);

    /// <summary>
    /// Gets the literal control displaying the header detail notes.
    /// </summary>
    protected virtual Literal LicenseDetailsHeaderNoteLiteral => this.Container.GetControl<Literal>("licenseDetailsHeaderNoteLiteral", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    protected override void InitializeControls(GenericContainer container) => this.DataBind();

    /// <summary>
    /// Binds a data source to the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> and all its child controls.
    /// </summary>
    public new void DataBind()
    {
      if (this.DataSource == null || !this.DataSource.IsLicenseDataOk)
        return;
      this.BindLicenseInfo();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      this.ModulesRepeater.DataBind();
      this.AddonsRepeater.DataBind();
      if (this.DataSource != null && this.DataSource.IsLicenseDataOk)
      {
        if (this.DataSource.LicenseInfo.LicenseType != "CL" || !this.DataSource.LicenseInfo.Addons.Any<string>())
        {
          this.PackageAndAddonsContainer.Visible = false;
          this.PackageAndAddonsLabel.Visible = false;
        }
        if (this.DataSource.LicenseInfo.IsMultiTenancyLicense)
          this.DomainsLicenseInfoRepeater.DataBind();
        else
          this.DomainsRepeater.DataBind();
      }
      base.OnPreRender(e);
    }

    private void BindLicenseInfo()
    {
      this.BindCommonInformation();
      if (!this.DataSource.LicenseInfo.IsMultiTenancyLicense)
        return;
      this.BindDomainsInformation();
    }

    private void BindCommonInformation()
    {
      LicensingMessages licResources = Res.Get<LicensingMessages>();
      if (!this.DataSource.LicenseInfo.IsMultiTenancyLicense)
      {
        this.DomainsListPlaceholder.Visible = true;
        this.DomainsRepeater.DataSource = (object) this.DataSource.LicenseInfo.Domains;
      }
      ArrayList moduleNames = new ArrayList();
      this.DataSource.LicenseInfo.LicensedModules.ToList<LicensedModuleInfo>().ForEach((Action<LicensedModuleInfo>) (m =>
      {
        if ("3D8A2051-6F6F-437C-865E-B3177689AC12".Equals(m.Sid, StringComparison.InvariantCultureIgnoreCase))
        {
          int subscribersCount = this.DataSource.LicenseInfo.TotalNewsLettersSubscribersCount;
          string str = subscribersCount > 0 ? subscribersCount.ToString() : licResources.Unlimited;
          moduleNames.Add((object) new
          {
            Name = string.Format(licResources.EmailCampaignsSubscribersCountFormat, (object) m.Name, (object) str)
          });
        }
        else
          moduleNames.Add((object) new{ Name = m.Name });
      }));
      this.ModulesRepeater.DataSource = (object) moduleNames;
      this.AddonsRepeater.DataSource = (object) this.DataSource.LicenseInfo.Addons;
      this.LicenseDetailsHeaderNoteLiteral.Visible = this.DataSource.LicenseInfo.IsMultiTenancyLicense;
      this.TotalUsersLimitsLiteral.Text = this.DataSource.LicenseInfo.Users == 0 ? licResources.Unlimited : this.DataSource.LicenseInfo.Users.ToString();
      this.TotalContentLimitsLiteral.Text = this.DataSource.LicenseInfo.TotalContentLimit == 0 ? licResources.Unlimited : this.DataSource.LicenseInfo.TotalContentLimit.ToString();
      this.TotalPagesLimitsLiteral.Text = this.DataSource.LicenseInfo.TotalPublicPagesLimit == 0 ? licResources.Unlimited : this.DataSource.LicenseInfo.TotalPublicPagesLimit.ToString();
      this.SubDomainsAllowedLiteral.Text = this.DataSource.LicenseInfo.AllowSubDomains ? licResources.Allowed : licResources.NotAllowed;
    }

    private void BindDomainsInformation()
    {
      LicensingMessages licensingMessages = Res.Get<LicensingMessages>();
      ArrayList arrayList = new ArrayList();
      arrayList.Add((object) new
      {
        Domains = this.DataSource.LicenseInfo.Domains,
        LicensedModules = Enumerable.Empty<LicensedModuleInfo>(),
        PagesLimit = string.Empty
      });
      IEnumerable<Guid> globalModulesIds = this.DataSource.LicenseInfo.LicensedModules.Select<LicensedModuleInfo, Guid>((Func<LicensedModuleInfo, Guid>) (m => m.Id));
      foreach (LicensedDomainItem licensedDomainItem in this.DataSource.LicenseInfo.LicensedDomainItems)
        arrayList.Add((object) new
        {
          Domains = licensedDomainItem.Domains,
          LicensedModules = licensedDomainItem.LicensedModules.Where<LicensedModuleInfo>((Func<LicensedModuleInfo, bool>) (m => !globalModulesIds.Contains<Guid>(m.Id))),
          PagesLimit = (licensedDomainItem.TotalPublicPagesLimit != this.DataSource.LicenseInfo.TotalPublicPagesLimit ? (licensedDomainItem.TotalPublicPagesLimit == 0 ? licensingMessages.Unlimited : licensedDomainItem.TotalPublicPagesLimit.ToString()) : string.Empty)
        });
      this.DomainsLicenseInfoRepeater.Visible = true;
      this.DomainsLicenseInfoRepeater.DataSource = (object) arrayList;
    }
  }
}
