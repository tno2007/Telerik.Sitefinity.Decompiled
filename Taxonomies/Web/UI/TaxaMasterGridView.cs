// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.TaxaMasterGridView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  public class TaxaMasterGridView : MasterGridView
  {
    private bool? supportsMultilingual;
    private TaxonomyManager taxonomyManager;
    private Taxonomy taxonomy;
    private MultisiteTaxonomiesResolver taxonomyResolver;
    internal const string taxaMasterGridViewScript = "Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.TaxaMasterGridView.js";

    /// <summary>Gets the current taxonomy.</summary>
    /// <value>The current taxonomy.</value>
    protected Taxonomy CurrentTaxonomy
    {
      get
      {
        if (this.taxonomy == null)
          this.taxonomy = this.GetTaxonomy();
        return this.taxonomy;
      }
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Taxonomies.Web.UI.TaxaMasterGridView.TaxonomyManager" /> to be used by the
    /// control
    /// </summary>
    protected virtual TaxonomyManager TaxonomyManager
    {
      get
      {
        if (this.taxonomyManager == null)
          this.taxonomyManager = TaxonomyManager.GetManager(this.Host.ControlDefinition.ProviderName);
        return this.taxonomyManager;
      }
    }

    /// <summary>Gets the taxonomy resolver.</summary>
    /// <value>The taxonomy resolver.</value>
    internal MultisiteTaxonomiesResolver TaxonomyResolver
    {
      get
      {
        if (this.taxonomyResolver == null)
          this.taxonomyResolver = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(this.TaxonomyManager);
        return this.taxonomyResolver;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this control supports multilingual.
    /// </summary>
    /// <value><c>true</c> if supports multilingual; otherwise, <c>false</c>.</value>
    public override bool SupportsMultilingual
    {
      get
      {
        if (!this.supportsMultilingual.HasValue)
          this.supportsMultilingual = new bool?(AppSettings.CurrentSettings.Multilingual);
        return this.supportsMultilingual.Value;
      }
      set => this.supportsMultilingual = new bool?(value);
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
      if (definition.ViewType.Equals(typeof (TaxaMasterGridView)) && definition is IMasterViewDefinition masterViewDefinition)
      {
        string str1 = HttpUtility.HtmlEncode((string) this.CurrentTaxonomy.Title);
        string str2 = (string.IsNullOrEmpty((string) this.CurrentTaxonomy.Title) ? this.CurrentTaxonomy.Name : str1) + this.GetSharedSitesCountTitle();
        masterViewDefinition.Title = str2;
        foreach (IDecisionScreenDefinition decisionScreen in masterViewDefinition.DecisionScreens)
        {
          if (decisionScreen.DecisionType == DecisionType.NoItemsExist)
          {
            int num = this.Page.Request.QueryString["status"] == null ? 0 : (this.Page.Request.QueryString["status"] == "created" ? 1 : 0);
            decisionScreen.ResourceClassId = (string) null;
            if (num != 0)
            {
              decisionScreen.MessageType = MessageType.Positive;
              decisionScreen.MessageText = Res.Get<TaxonomyResources>().ClassificationHasBeenSuccessfullyCreated.Arrange((object) str1);
            }
            else
            {
              decisionScreen.MessageType = MessageType.Neutral;
              decisionScreen.MessageText = Res.Get<TaxonomyResources>().NoTaxaExistsYet.Arrange((object) str1.ToLower());
            }
            foreach (ICommandWidgetDefinition action in decisionScreen.Actions)
            {
              if (action.CommandName == "create")
                action.Text = Res.Get<TaxonomyResources>().CreateATaxonName.Arrange((object) HttpUtility.HtmlEncode(this.CurrentTaxonomy.TaxonName.ToLower()));
            }
          }
        }
        Type taxonType;
        string defName;
        string viewName;
        TaxaDefinitionsHelper.GetTaxonTypeDlgDefNameAndViewNameFromTaxonomy((ITaxonomy) this.CurrentTaxonomy, out taxonType, out defName, out viewName, true);
        bool flag = this.ShouldSkipSiteContext();
        foreach (IDialogDefinition dialog in masterViewDefinition.Dialogs)
        {
          if (dialog.Name == "ModulePermissionsDialog")
          {
            Guid guid = this.CurrentTaxonomy.Id;
            if (!flag)
              guid = this.TaxonomyResolver.ResolveSiteTaxonomyId(this.CurrentTaxonomy.Id);
            dialog.Parameters = string.Format("?securedObjectID={0}&title={1}&securedObjectTypeName={2}&managerClassName={3}", (object) guid, (object) string.Format(Res.Get<SecurityResources>().GeneralPermissionsTitle, (object) str1), (object) this.CurrentTaxonomy.GetType().AssemblyQualifiedName, (object) typeof (TaxonomyManager).AssemblyQualifiedName);
          }
          if (dialog.Name == "ContentViewInsertDialog" && dialog.OpenOnCommandName == "create")
          {
            dialog.Parameters = "?ControlDefinitionName=" + defName + "&ViewName=" + viewName + "&TaxonomyId={0}&TaxonType={1}&skipSiteContext={2}";
            dialog.Parameters = dialog.Parameters.Arrange((object) this.CurrentTaxonomy.Id, (object) taxonType, (object) flag);
          }
          if (dialog.Name == "FlatTaxaBulkEditForm")
            dialog.Parameters = string.Format("?TaxonomyId={0}&TaxonomyTitle={1}&TaxonName={2}", (object) this.CurrentTaxonomy.Id, (object) str1, (object) this.CurrentTaxonomy.TaxonName);
        }
      }
      base.InitializeControls(container, definition);
    }

    private bool ShouldSkipSiteContext()
    {
      bool result = false;
      return bool.TryParse(this.Page.Request.QueryString["skipSiteContext"], out result) & result;
    }

    private string GetSharedSitesCountTitle()
    {
      if (SystemManager.CurrentContext.IsOneSiteMode)
        return string.Empty;
      int num = !this.ShouldSkipSiteContext() ? this.taxonomyManager.GetRelatedSitesInContext((ITaxonomy) this.taxonomy).Count<ISite>() : this.TaxonomyManager.GetRelatedSites((ITaxonomy) this.taxonomy).Count<ISite>();
      string sharedSitesCountTitle;
      switch (num)
      {
        case 0:
          sharedSitesCountTitle = string.Format(" {0}", (object) Res.Get<TaxonomyResources>().NotUsedTitle);
          break;
        case 1:
          sharedSitesCountTitle = string.Format(" {0}", (object) Res.Get<TaxonomyResources>().ThisSiteOnlyTitle);
          break;
        default:
          sharedSitesCountTitle = string.Format(Res.Get<TaxonomyResources>().UsedInSites, (object) num);
          break;
      }
      return sharedSitesCountTitle;
    }

    /// <summary>Sets the custom client parameters.</summary>
    /// <param name="customClientParams">The custom client params.</param>
    protected override void SetCustomClientParameters(Dictionary<string, string> customClientParams)
    {
      base.SetCustomClientParameters(customClientParams);
      customClientParams["taxonomyId"] = this.CurrentTaxonomy.Id.ToString();
      customClientParams["singleTaxonName"] = (string) this.CurrentTaxonomy.TaxonName;
      customClientParams["taxonomyName"] = this.CurrentTaxonomy.Name;
      customClientParams["isMultiLingual"] = this.AppSettings.Multilingual.ToString();
      customClientParams["taxonomyTitle"] = HttpUtility.HtmlEncode((string) this.CurrentTaxonomy.Title);
      customClientParams["moveItemUpServiceUrl"] = this.ResolveClientUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/moveup/");
      customClientParams["moveItemDownServiceUrl"] = this.ResolveClientUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/movedown/");
      customClientParams["batchMoveItemsUpServiceUrl"] = this.ResolveClientUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/batchmoveup/");
      customClientParams["batchMoveItemsDownServiceUrl"] = this.ResolveClientUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/batchmovedown/");
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
      string webServiceBaseUrl = masterDefinition.WebServiceBaseUrl;
      string str1 = webServiceBaseUrl;
      if (!str1.EndsWith("/"))
        str1 += "/";
      string str2 = str1 + this.CurrentTaxonomy.Id.ToString() + "/";
      string str3 = this.Page.Request.QueryString["skipSiteContext"];
      bool flag = true;
      ref bool local = ref flag;
      bool.TryParse(str3, out local);
      if (flag)
        str2 += "?siteContextMode=skipSiteContext";
      itemsListBase.ServiceBaseUrl = str2;
      itemsListBase.ManagerType = typeof (TaxonomyManager).FullName;
      itemsListBase.Binder.ManagerType = typeof (TaxonomyManager).FullName;
      if (!(itemsListBase is ItemsTreeTable itemsTreeTable))
        return;
      string str4 = "subtaxa/";
      if (!webServiceBaseUrl.EndsWith("/"))
        str4 = "/" + str4;
      string str5 = "predecessor/";
      if (!webServiceBaseUrl.EndsWith("/"))
        str5 = "/" + str5;
      itemsTreeTable.ServiceChildItemsBaseUrl = webServiceBaseUrl + str4;
      itemsTreeTable.ServicePredecessorBaseUrl = webServiceBaseUrl + str5;
      itemsTreeTable.DataKeyNames = "Id";
      itemsTreeTable.ParentDataKeyName = "ParentTaxonId";
    }

    /// <summary>Gets the taxonomy.</summary>
    /// <returns></returns>
    protected virtual Taxonomy GetTaxonomy()
    {
      int num = ((IEnumerable<string>) SystemManager.CurrentHttpContext.Request.Url.Segments).Count<string>();
      for (int index = num; index > 0; --index)
      {
        string taxonomyName = SystemManager.CurrentHttpContext.Request.Url.Segments[index - 1].ToString();
        if (taxonomyName.EndsWith("/"))
          taxonomyName = taxonomyName.Substring(0, taxonomyName.Length - 1);
        taxonomyName = HttpUtility.UrlDecode(taxonomyName);
        Taxonomy taxonomy = this.TaxonomyManager.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Name == taxonomyName)).SingleOrDefault<Taxonomy>();
        if (taxonomy != null)
          return taxonomy;
      }
      string str = VirtualPathUtility.RemoveTrailingSlash(SystemManager.CurrentHttpContext.Request.Url.Segments[num - 1]);
      if (str == SiteInitializer.FlatTaxonomyUrlName)
        return this.GetTaxa<FlatTaxonomy>("Tags");
      if (str == SiteInitializer.HierarchicalTaxonomyUrlName)
        return this.GetTaxa<HierarchicalTaxonomy>("Categories");
      if (this.Page != null)
        this.Page.Response.Redirect(BackendSiteMap.FindSiteMapNode(SiteInitializer.TaxonomiesNodeId, false).Url);
      return this.TaxonomyManager.GetTaxonomies<Taxonomy>().FirstOrDefault<Taxonomy>();
    }

    /// <summary>Gets the taxonomy with the specified name.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="taxonomyName">Name of the taxonomy.</param>
    /// <returns>The taxonomy with the specified name.</returns>
    protected virtual Taxonomy GetTaxa<T>(string taxonomyName) where T : Taxonomy => (Taxonomy) this.TaxonomyResolver.ResolveSiteTaxonomy<T>(Queryable.SingleOrDefault<T>(this.TaxonomyManager.GetTaxonomies<T>().Where<T>((Expression<Func<T, bool>>) (t => t.Name == taxonomyName))) ?? Queryable.FirstOrDefault<T>(this.TaxonomyManager.GetTaxonomies<T>()));

    /// <inheritdoc />
    protected internal override void AddCulturesSpecificValues(
      ScriptControlDescriptor scriptDescriptor)
    {
      if (!this.SupportsMultilingual)
        return;
      string name = AppSettings.CurrentSettings.DefaultFrontendLanguage.Name;
      CultureInfo[] frontendLanguages = AppSettings.CurrentSettings.DefinedFrontendLanguages;
      scriptDescriptor.AddProperty("_defaultLanguage", (object) name);
      scriptDescriptor.AddProperty("_definedLanguages", (object) ((IEnumerable<CultureInfo>) frontendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (ci => ci.Name)));
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.Type = typeof (TaxaMasterGridView).FullName;
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.TaxaMasterGridView.js", typeof (TaxaMasterGridView).Assembly.FullName)
    };
  }
}
