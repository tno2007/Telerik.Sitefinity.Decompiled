// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.MarkedItemsPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>Represents the control panel for Taxonomy module.</summary>
  public class MarkedItemsPanel : SimpleView
  {
    private TaxonomyManager taxonomyManager;
    private Taxonomy taxonomy;
    private Taxon taxon;
    private string providerName;
    public static readonly string ViewName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.MarkedItems.ascx");

    /// <summary>
    /// Gets or sets the name of the data provider that will be used to retrieve and manipulate data.
    /// </summary>
    public virtual string ProviderName
    {
      get
      {
        if (this.providerName == null && SystemManager.CurrentHttpContext.Request.QueryString["DataProvider"] != null)
          this.providerName = SystemManager.CurrentHttpContext.Request.QueryString["DataProvider"];
        if (string.IsNullOrEmpty(this.providerName))
          this.providerName = string.Empty;
        return this.providerName;
      }
      set
      {
        if (!(value != this.providerName))
          return;
        this.providerName = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets the grid.</summary>
    /// <value>The grid.</value>
    protected internal virtual ItemsGrid Grid => this.Container.GetControl<ItemsGrid>("itemsGrid", true);

    /// <summary>Gets the title control.</summary>
    /// <value>The title control.</value>
    protected internal virtual ITextControl TitleControl => this.Container.GetControl<ITextControl>("PanelTitle", true);

    protected internal virtual TaxonomyManager TaxonomyManager
    {
      get
      {
        if (this.taxonomyManager == null)
          this.taxonomyManager = TaxonomyManager.GetManager(this.ProviderName);
        return this.taxonomyManager;
      }
    }

    protected internal virtual Taxonomy Taxonomy
    {
      get
      {
        if (this.taxonomy == null)
          this.EnsureTaxonomy();
        return this.taxonomy;
      }
    }

    protected internal virtual Taxon Taxon
    {
      get
      {
        if (this.taxonomy == null)
          this.EnsureTaxonomy();
        return this.taxon;
      }
    }

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MarkedItemsPanel.ViewName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected virtual HiddenField TaxonId => this.Container.GetControl<HiddenField>("taxonId", true);

    protected override void InitializeControls(GenericContainer container)
    {
      this.EnsureTaxonomy();
      this.TitleControl.Text = string.Format(Res.Get<TaxonomyResources>().MarkedItemsTitle, (object) this.GetInternalTitle());
      HyperLink control = this.Container.GetControl<HyperLink>("BackLink", false);
      if (control != null)
      {
        control.NavigateUrl = TaxonomyManager.GetTaxonomyEditUrl((ITaxonomy) this.Taxonomy);
        control.Text = string.Format(Res.Get<TaxonomyResources>().BackToTaxonomyName, (object) this.Taxonomy.Title);
      }
      this.Controls.Add((Control) new UserPreferences());
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        return;
      Taxonomy taxonomy = this.Taxonomy;
      Taxon taxon = this.Taxon;
      if (taxon != null)
        this.TaxonId.Value = taxon.Id.ToString();
      ItemsGrid control = this.Container.GetControl<ItemsGrid>("itemsGrid", false);
      if (control != null)
      {
        foreach (ToolboxItemBase toolboxItem in control.ToolboxItems)
        {
          if (toolboxItem is CommandToolboxItem)
          {
            CommandToolboxItem commandToolboxItem = toolboxItem as CommandToolboxItem;
            if (commandToolboxItem.CommandName == "groupRemove")
              commandToolboxItem.Text = string.Format(Res.Get<TaxonomyResources>().UnmarkItems, (object) this.GetInternalTitle());
            else if (commandToolboxItem.CommandName == "edit")
            {
              commandToolboxItem.Text = string.Format(Res.Get<TaxonomyResources>().TaxonProperties, (object) this.GetInternalTitle());
              foreach (DialogDefinition dialog in control.Dialogs)
              {
                if (dialog.OpenOnCommandName == "edit")
                {
                  dialog.Parameters = string.Format("?TaxonomyId={0}&TaxonId={1}&TaxonomyTitle={2}&TaxonName={3}", (object) taxonomy.Id, (object) taxon.Id, (object) taxonomy.Title, (object) taxonomy.TaxonName);
                  dialog.Name = !(taxonomy is FlatTaxonomy) ? "HierarchicalTaxonForm" : "FlatTaxonForm";
                }
              }
            }
          }
        }
      }
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.TelerikSitefinity);
    }

    private string GetInternalTitle() => string.Format("{0} <em>{1}</em>", (object) HttpUtility.HtmlEncode((string) this.Taxonomy.TaxonName), (object) HttpUtility.HtmlEncode((string) this.Taxon.Title));

    private void EnsureTaxonomy()
    {
      if (this.taxonomy == null)
      {
        RequestContext requestContext = this.Page.GetRequestContext();
        if (requestContext != null)
        {
          object obj = requestContext.RouteData.Values["Params"];
          if (obj != null)
          {
            string[] strArray = (string[]) obj;
            if (strArray.Length == 1)
            {
              Guid guid = Utility.StringToGuid(strArray[0]);
              if (guid != Guid.Empty)
              {
                this.taxon = (Taxon) this.TaxonomyManager.GetTaxon(guid);
                if (this.taxon != null)
                  this.taxonomy = (Taxonomy) TaxonomyManager.GetTaxonomy((ITaxon) this.taxon);
              }
            }
            else if (strArray.Length > 1)
            {
              string taxonomyName = strArray[0];
              string taxonName = strArray[1];
              this.taxonomy = this.TaxonomyManager.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Name == taxonomyName)).SingleOrDefault<Taxonomy>();
              if (this.taxonomy != null)
              {
                if (this.taxonomy is FlatTaxonomy)
                  this.taxon = (Taxon) this.TaxonomyManager.GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.Taxonomy.Name == taxonomyName && t.UrlName == (Lstring) taxonName)).FirstOrDefault<FlatTaxon>();
                else if (this.taxonomy is HierarchicalTaxonomy)
                {
                  this.taxon = (Taxon) this.TaxonomyManager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Taxonomy.Name == taxonomyName && t.UrlName == (Lstring) taxonName)).FirstOrDefault<HierarchicalTaxon>();
                  if (this.taxon != null && strArray.Length > 2)
                  {
                    for (int index = 2; index < strArray.Length; ++index)
                    {
                      string parentTaxonName = this.taxon.Name;
                      taxonName = strArray[index];
                      this.taxon = (Taxon) this.TaxonomyManager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent.Name == parentTaxonName && t.UrlName == (Lstring) taxonName)).FirstOrDefault<HierarchicalTaxon>();
                    }
                  }
                }
              }
            }
          }
        }
      }
      if (this.taxonomy == null || this.taxon == null)
        throw new HttpException(404, "Page not found");
    }
  }
}
