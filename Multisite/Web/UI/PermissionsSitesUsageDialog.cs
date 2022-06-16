// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.PermissionsSitesUsageDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>A dialog listing all sites using a given provider.</summary>
  public class PermissionsSitesUsageDialog : AjaxDialogBase
  {
    /// <summary>
    /// Gets the name of resource file representing the dialog.
    /// </summary>
    private static readonly string DialogTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.PermissionsSitesUsageDialog.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.PermissionsSitesUsageDialog" /> class.
    /// </summary>
    public PermissionsSitesUsageDialog() => this.LayoutTemplatePath = PermissionsSitesUsageDialog.DialogTemplatePath;

    /// <summary>Gets the name of the data provider.</summary>
    /// <value>The name of the data provider.</value>
    public string DataProviderName => this.Page.Request.QueryString["dataProviderName"].UrlDecode();

    /// <summary>Gets the data provider title.</summary>
    /// <value>The data provider title.</value>
    public string DataProviderTitle => this.Page.Request.QueryString["dataProviderTitle"].UrlDecode();

    /// <summary>Gets the name of manager.</summary>
    /// <value>The name of the manager.</value>
    public string ManagerClassName => this.Page.Request.QueryString["managerClassName"].UrlDecode();

    /// <summary>Gets the dynamic module title.</summary>
    /// <value>The dynamic module title.</value>
    public string DynamicModuleTitle => this.Page.Request.QueryString["dynamicModuleTitle"].UrlDecode();

    /// <summary>Gets the type of the secured object.</summary>
    /// <value>The type of the secured object.</value>
    public string SecuredObjectType => this.Page.Request.QueryString["securedObjectType"].UrlDecode();

    /// <summary>Gets the sitesRepeater control.</summary>
    /// <value>The sitesRepeater control.</value>
    protected virtual Repeater SitesRepeater => this.Container.GetControl<Repeater>("sitesRepeater", true);

    /// <summary>Gets the title literal.</summary>
    protected virtual ITextControl TitleLiteral => this.Container.GetControl<ITextControl>("titleLiteral", true);

    /// <summary>Gets the description literal.</summary>
    protected virtual ITextControl DescriptionLiteral => this.Container.GetControl<ITextControl>("descriptionLiteral", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container of the instantiated template.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      IEnumerable<ISite> sites = this.GetSites(!(TypeResolutionService.ResolveType(this.SecuredObjectType, false, true) == typeof (DynamicModule)) || this.DynamicModuleTitle.IsNullOrEmpty() ? this.GetManager(this.ManagerClassName, this.DataProviderName).GetType().FullName : this.DynamicModuleTitle, this.DataProviderName);
      this.SetHeaderText(this.DataProviderTitle, sites.Count<ISite>());
      this.SitesRepeater.ItemCreated += new RepeaterItemEventHandler(this.SitesRepeater_ItemCreated);
      this.SitesRepeater.DataSource = (object) sites;
      this.SitesRepeater.DataBind();
    }

    /// <summary>Sets the text in the header.</summary>
    /// <param name="providerTitle">The provider title.</param>
    /// <param name="sitesCount">The count of the sites.</param>
    private void SetHeaderText(string providerTitle, int sitesCount) => this.TitleLiteral.Text = (sitesCount == 1 ? Res.Get<MultisiteResources>().PermissionsSitesUsageSingle : Res.Get<MultisiteResources>().PermissionsSitesUsageMultiple).Arrange((object) providerTitle, (object) sitesCount);

    /// <summary>Gets the sites.</summary>
    /// <param name="dataSourceName">Name of the data source.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <returns>List of sites.</returns>
    private IEnumerable<ISite> GetSites(
      string dataSourceName,
      string dataProviderName)
    {
      IQueryable<SiteDataSourceLink> links = MultisiteManager.GetManager().GetSiteDataSourceLinks().Where<SiteDataSourceLink>((Expression<Func<SiteDataSourceLink, bool>>) (l => l.DataSource.Name == dataSourceName && l.DataSource.Provider == dataProviderName));
      return (IEnumerable<ISite>) SystemManager.CurrentContext.MultisiteContext.GetSites().Where<ISite>((Func<ISite, bool>) (s => links.Any<SiteDataSourceLink>((Expression<Func<SiteDataSourceLink, bool>>) (l => l.Site.Id == s.Id)))).OrderBy<ISite, string>((Func<ISite, string>) (s => s.Name));
    }

    private IManager GetManager(string managerClassName, string dataProviderName) => ManagerBase.GetManager(managerClassName, dataProviderName);

    /// <summary>Sets the item text.</summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    /// <inheritdoc />
    private void SetItemText(RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.DataItem is MultisiteContext.SiteProxy dataItem) || !(e.Item.FindControl("siteName") is Literal control))
        return;
      control.Text = dataItem.Name;
    }

    /// <summary>
    /// Handles the ItemCreated event of the SitesRepeater control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    private void SitesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      this.SetItemText(e);
    }
  }
}
