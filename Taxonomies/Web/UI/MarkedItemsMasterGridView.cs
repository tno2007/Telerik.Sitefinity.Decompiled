// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.MarkedItemsMasterGridView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.Services.Common;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>
  /// A control which displays a list with the items associated with a specific taxon in the backend.
  /// </summary>
  public class MarkedItemsMasterGridView : MasterGridView
  {
    private Taxonomy taxonomy;
    private Taxon taxon;
    private static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.MarkedItemsMasterGridView.ascx");

    /// <summary>Gets the stats binder.</summary>
    /// <value>The stats binder.</value>
    protected virtual ClientBinder StatsBinder => this.Container.GetControl<ClientBinder>("statsBinder", true);

    /// <summary>
    /// Control whose innerHTML is set on the client to the total number of marked items
    /// </summary>
    protected virtual Control TotalCountControl => this.Container.GetControl<Control>("totalCount", true);

    /// <summary>
    /// Value of LayoutTemplatePath if LayoutTemplateName and LayoutTemplatePath both would otherwize have null or empty values
    /// </summary>
    /// <value></value>
    protected override string DefaultLayoutTemplatePath => MarkedItemsMasterGridView.TemplatePath;

    /// <inheritdoc />
    protected override void SetCustomClientParameters(Dictionary<string, string> customClientParams)
    {
      base.SetCustomClientParameters(customClientParams);
      this.EnsureTaxonomy();
      customClientParams["statsBinderClientId"] = this.StatsBinder.ClientID;
      customClientParams["taxonId"] = this.taxon.Id.ToString();
      customClientParams["totalCountClientId"] = this.TotalCountControl.ClientID;
      string str = new JavaScriptSerializer().Serialize((object) TaxonServiceHelper.CreateFullWcfTaxonObject(this.taxon, (TaxonomyManager) this.Manager, this.taxon.GetType()));
      customClientParams["wcfTaxon"] = str;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <param name="definition"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (definition is IMasterViewDefinition masterViewDefinition)
      {
        this.EnsureTaxonomy();
        masterViewDefinition.Title = Res.Get<TaxonomyResources>().MarkedItemsTitle.Arrange((object) HttpUtility.HtmlEncode((string) this.taxon.Title));
        masterViewDefinition.ResourceClassId = (string) null;
        foreach (IWidgetDefinition titleWidget in masterViewDefinition.TitleWidgets)
        {
          if (titleWidget.Name == "BackTo")
            titleWidget.Text = Res.Get<TaxonomyResources>().BackToTaxonomyName.Arrange((object) HttpUtility.HtmlEncode((string) this.taxonomy.Title));
        }
        masterViewDefinition.WebServiceBaseUrl = "~/Sitefinity/Services/Taxonomies/MarkedItems.svc/items/" + (object) this.taxon.Id + "/";
        foreach (ILinkDefinition link in masterViewDefinition.Links)
        {
          if (link.Name == "BackTo")
          {
            Type type = this.taxonomy.GetType();
            if (typeof (HierarchicalTaxonomy).IsAssignableFrom(type))
              link.NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.HierarchicalTaxonomyPageId) + string.Format("/{0}", (object) this.taxonomy.Name);
            else if (typeof (FlatTaxonomy).IsAssignableFrom(type))
              link.NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.FlatTaxonomyPageId) + string.Format("/{0}", (object) this.taxonomy.Name);
            else if (typeof (NetworkTaxonomy).IsAssignableFrom(type))
              link.NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.NetworkTaxonomyPageId);
            else if (typeof (FacetTaxonomy).IsAssignableFrom(type))
              link.NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.FacetTaxonomyPageId);
          }
        }
        foreach (IWidgetBarSectionDefinition section in masterViewDefinition.Toolbar.Sections)
        {
          foreach (IWidgetDefinition widgetDefinition1 in section.Items)
          {
            if (widgetDefinition1 is ICommandWidgetDefinition widgetDefinition2)
            {
              if (widgetDefinition2.CommandName == "groupRemove")
                widgetDefinition2.Text = string.Format(Res.Get<TaxonomyResources>().UnmarkItems, (object) this.GetInternalTitle());
              if (widgetDefinition2.CommandName == "edit")
                widgetDefinition2.Text = string.Format(Res.Get<TaxonomyResources>().TaxonProperties, (object) this.GetInternalTitle());
            }
          }
        }
        Type taxonType;
        string defName;
        string viewName;
        TaxaDefinitionsHelper.GetTaxonTypeDlgDefNameAndViewNameFromTaxonomy((ITaxonomy) this.taxonomy, out taxonType, out defName, out viewName, false);
        foreach (IDialogDefinition dialog in masterViewDefinition.Dialogs)
        {
          if (dialog.OpenOnCommandName == "edit")
          {
            dialog.Parameters = "?ControlDefinitionName=" + defName + "&ViewName=" + viewName + "&TaxonomyId=" + (object) this.taxonomy.Id + "&TaxonType=" + (object) taxonType;
            dialog.Name = "ContentViewEditDialog";
          }
        }
      }
      base.InitializeControls(container, definition);
    }

    /// <summary>Initializes the items list base.</summary>
    /// <param name="itemsListBase">The items list base.</param>
    /// <param name="masterDefinition">The master definition.</param>
    protected override void InitializeItemsListBase(
      ItemsListBase itemsListBase,
      IMasterViewDefinition masterDefinition)
    {
      this.EnsureTaxonomy();
      base.InitializeItemsListBase(itemsListBase, masterDefinition);
      itemsListBase.BindOnLoad = false;
      itemsListBase.Binder.BindOnLoad = false;
    }

    /// <summary>
    /// Determines the base service url that should be used for the service calls in this view
    /// </summary>
    /// <param name="itemsListBase">The items list base.</param>
    /// <param name="masterDefinition">The master definition.</param>
    protected override void DetermineServiceUrl(
      ItemsListBase itemsListBase,
      IContentViewMasterDefinition masterDefinition)
    {
      this.EnsureTaxonomy();
      string webServiceBaseUrl = masterDefinition.WebServiceBaseUrl;
      if (!webServiceBaseUrl.EndsWith("/"))
        webServiceBaseUrl += "/";
      itemsListBase.ServiceBaseUrl = webServiceBaseUrl;
      itemsListBase.ManagerType = typeof (TaxonomyManager).FullName;
      itemsListBase.Binder.ManagerType = typeof (TaxonomyManager).FullName;
    }

    private string GetInternalTitle()
    {
      this.EnsureTaxonomy();
      return Res.Get<TaxonomyResources>().TaxonNameTaxonTitleMarkup.Arrange((object) HttpUtility.HtmlEncode((string) this.taxonomy.TaxonName), (object) HttpUtility.HtmlEncode((string) this.taxon.Title));
    }

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
                this.taxon = (Taxon) ((TaxonomyManager) this.Manager).GetTaxon(guid);
                if (this.taxon != null)
                  this.taxonomy = (Taxonomy) TaxonomyManager.GetTaxonomy((ITaxon) this.taxon);
              }
            }
            else if (strArray.Length > 1)
            {
              string taxonomyName = strArray[0];
              string taxonName = strArray[1];
              this.taxonomy = ((TaxonomyManager) this.Manager).GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Name == taxonomyName)).SingleOrDefault<Taxonomy>();
              if (this.taxonomy != null)
              {
                if (this.taxonomy is FlatTaxonomy)
                  this.taxon = (Taxon) ((TaxonomyManager) this.Manager).GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.Taxonomy.Name == taxonomyName && t.UrlName == (Lstring) taxonName)).FirstOrDefault<FlatTaxon>();
                else if (this.taxonomy is HierarchicalTaxonomy)
                {
                  this.taxon = (Taxon) ((TaxonomyManager) this.Manager).GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Taxonomy.Name == taxonomyName && t.UrlName == (Lstring) taxonName)).FirstOrDefault<HierarchicalTaxon>();
                  if (this.taxon != null && strArray.Length > 2)
                  {
                    for (int index = 2; index < strArray.Length; ++index)
                    {
                      string parentTaxonName = this.taxon.Name;
                      taxonName = strArray[index];
                      this.taxon = (Taxon) ((TaxonomyManager) this.Manager).GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent.Name == parentTaxonName && t.UrlName == (Lstring) taxonName)).FirstOrDefault<HierarchicalTaxon>();
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
