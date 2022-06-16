// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.SitesUsageDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>A dialog listing all sites using a given item.</summary>
  public class SitesUsageDialog : AjaxDialogBase
  {
    /// <summary>
    /// Gets the name of resource file representing the dialog.
    /// </summary>
    private static readonly string DialogTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.SitesUsageDialog.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.SitesUsageDialog" /> class.
    /// </summary>
    public SitesUsageDialog() => this.LayoutTemplatePath = SitesUsageDialog.DialogTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the sitesRepeater control.</summary>
    /// <value>The sitesRepeater control.</value>
    protected virtual Repeater SitesRepeater => this.Container.GetControl<Repeater>("sitesRepeater", true);

    /// <summary>Gets the title literal.</summary>
    protected virtual ITextControl TitleLiteral => this.Container.GetControl<ITextControl>("titleLiteral", true);

    /// <summary>Gets the description literal.</summary>
    protected virtual ITextControl DescriptionLiteral => this.Container.GetControl<ITextControl>("descriptionLiteral", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      Guid itemId = new Guid(this.Page.Request.QueryString["itemId"]);
      string itemType = this.Page.Request.QueryString["itemType"];
      IEnumerable<ISite> sites = this.GetSites((IMultisiteEnabledManager) ManagerBase.GetManager(this.Page.Request.QueryString["managerType"]), itemId, itemType);
      this.SetHeaderText(sites.Count<ISite>());
      this.SitesRepeater.ItemCreated += new RepeaterItemEventHandler(this.SitesRepeater_ItemCreated);
      this.SitesRepeater.DataSource = (object) sites;
      this.SitesRepeater.DataBind();
    }

    /// <summary>Sets the text in the header.</summary>
    /// <param name="sitesCount">The count of the sites.</param>
    internal virtual void SetHeaderText(int sitesCount)
    {
      string classId = this.Page.Request.QueryString["resourceClassId"];
      string key1 = this.Page.Request.QueryString["title"];
      string key2 = this.Page.Request.QueryString["titleSingular"];
      string key3 = this.Page.Request.QueryString["description"];
      ITextControl titleLiteral = this.TitleLiteral;
      string str;
      if (sitesCount != 1)
        str = Res.Get(classId, key1).Arrange((object) sitesCount);
      else
        str = Res.Get(classId, key2);
      titleLiteral.Text = str;
      this.DescriptionLiteral.Text = Res.Get(classId, key3);
    }

    /// <summary>Gets the sites.</summary>
    /// <param name="multisiteManager">The multisite manager.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    internal virtual IEnumerable<ISite> GetSites(
      IMultisiteEnabledManager multisiteManager,
      Guid itemId,
      string itemType)
    {
      IQueryable<SiteItemLink> links = multisiteManager.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == itemId && l.ItemType == itemType && l.SiteId != Guid.Empty));
      return SystemManager.CurrentContext.MultisiteContext.GetSites().Where<ISite>((Func<ISite, bool>) (s => links.Any<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == s.Id))));
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

    /// <summary>Sets the text in the item.</summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    protected virtual void SetItemText(RepeaterItemEventArgs e)
    {
      if (!(e.Item.DataItem is MultisiteContext.SiteProxy dataItem))
        return;
      if (e.Item.FindControl("siteLink") is HyperLink control1)
        control1.Text = dataItem.Name;
      if (e.Item.FindControl("statusTextLiteral") is HtmlGenericControl control2)
      {
        string str = dataItem.IsOffline ? Res.Get<MultisiteResources>().Offline : Res.Get<MultisiteResources>().Online;
        control2.InnerText = str;
      }
      if (!(e.Item.FindControl("itemContainer") is HtmlGenericControl control3))
        return;
      control3.Attributes.Add("class", "sfItemTitle sf" + (dataItem.IsOffline ? "notinstalled" : "active"));
    }
  }
}
