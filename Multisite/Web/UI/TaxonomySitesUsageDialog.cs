// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.TaxonomySitesUsageDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>A dialog listing all sites using a given taxonomy.</summary>
  public class TaxonomySitesUsageDialog : SitesUsageDialog
  {
    /// <summary>
    /// Gets the name of resource file representing the dialog.
    /// </summary>
    private static readonly string DialogTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.TaxonomySitesUsageDialog.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.TaxonomySitesUsageDialog" /> class.
    /// </summary>
    public TaxonomySitesUsageDialog() => this.LayoutTemplatePath = TaxonomySitesUsageDialog.DialogTemplatePath;

    /// <inheritdoc />
    internal override IEnumerable<ISite> GetSites(
      IMultisiteEnabledManager multisiteManager,
      Guid itemId,
      string itemType)
    {
      TaxonomyManager taxonomyManager = (TaxonomyManager) multisiteManager;
      ITaxonomy taxonomy = taxonomyManager.GetTaxonomy(itemId);
      this.TitleLiteral.Text = Res.Get(this.Page.Request.QueryString["resourceClassId"], this.Page.Request.QueryString["title"]).Arrange((object) taxonomy.Title);
      return taxonomyManager.GetRelatedSitesInContext(taxonomy);
    }

    internal override void SetHeaderText(int sitesCount)
    {
    }

    /// <inheritdoc />
    protected override void SetItemText(RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.DataItem is MultisiteContext.SiteProxy dataItem) || !(e.Item.FindControl("siteName") is Literal control))
        return;
      control.Text = HttpUtility.HtmlEncode(dataItem.Name);
    }
  }
}
