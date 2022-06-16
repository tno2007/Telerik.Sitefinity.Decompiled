// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Extensions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;
using Telerik.Sitefinity.Web.UI.ContentUI.Extensions;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend
{
  /// <summary>
  /// This is the frontend widget for displaying the data of dynamic modules.
  /// </summary>
  [RequireScriptManager]
  [ParseChildren(true)]
  [ControlDesigner(typeof (DynamicContentViewDesigner))]
  public class DynamicContentView : 
    Control,
    IContentView,
    IHasCacheDependency,
    IContentLocatableView,
    IRelatedDataView,
    IDynamicContentWidget
  {
    private RelatedDataDefinition relatedDataDefinition;
    protected internal const string defaultMasterViewName = "DynamicContentMasterView";
    protected internal const string defaultDetailViewName = "DynamicContentDetailView";
    protected internal const int defaultPageSize = 20;
    internal const string defaultSortExpression = "PublicationDate DESC";
    private const string widgetNameRegularExpression = "/!(?<urlPrefix>[a-zA-Z0-9_\\-]+)/.*";
    private const string ModuleErrorTemplate = "<p class=\"sfFailure\">{0}</p>";
    private ModuleBuilderManager moduleBuilderManager;
    private DynamicModuleManager dynamicManager;
    private DynamicModuleType dynamicModuleType;
    private string dynamicContentTypeName;
    private Type dynamicContentType;
    private Type relatedDataType;
    private bool? hasValidRelatedDataConfiguration;
    private bool fetchedArrayOfItems;
    private Guid[] relatedItemsIds;
    private Guid relatedItemId;
    private ContentViewControlDefinition definition;
    private string masterViewName;
    private string detailViewName;
    private DynamicContentViewDetail detailViewControl;
    private DynamicContentViewMaster masterViewControl;
    private ContentView.PageTitleModes pageTitleMode = ContentView.PageTitleModes.DoNotSet;
    private bool isControlDefinitionProviderCorrect = true;
    private DynamicContent itemFromUrl;
    private List<string> childContentTypeNames;
    private List<string> parentContentTypeNames;
    private bool? isSingleItem;
    private bool detailViewControlBound;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentView" /> class.
    /// </summary>
    public DynamicContentView() => this.SetPaginationUrls = new bool?(true);

    /// <summary>
    /// Gets or sets the type of the dynamic content to display.
    /// </summary>
    public virtual string DynamicContentTypeName
    {
      get => this.dynamicContentTypeName;
      set
      {
        if (!(this.dynamicContentTypeName != value))
          return;
        this.dynamicContentTypeName = value;
        this.ControlDefinition.ContentType = this.DynamicContentType;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the title of the control.</summary>
    /// <value>The title of the control.</value>
    public virtual string Title { get; set; }

    /// <summary>Gets or sets the type name of the related data type.</summary>
    public virtual string RelatedDataTypeName { get; set; }

    /// <summary>Gets or sets the type of the related data field name.</summary>
    public string RelatedDataFieldName { get; set; }

    /// <summary>
    /// Gets or sets the default template key for the master view.
    /// </summary>
    public virtual string DefaultMasterTemplateKey { get; set; }

    /// <summary>
    /// Gets or sets the default template key for the detail view.
    /// </summary>
    public virtual string DefaultDetailTemplateKey { get; set; }

    /// <summary>
    /// Gets or sets the URL evaluation mode - URL segments or query string.
    /// This property is used by all controls on a page that have URL Evaluators. Information for interpreting a url
    /// for a specific item or page is passed either through the URL itself or through the QueryString. The
    /// value of this property indicates which one is used.
    /// </summary>
    [PropertyPersistence(true, PagePropertyName = "UrlEvaluationMode")]
    public UrlEvaluationMode UrlEvaluationMode
    {
      get
      {
        if (this.Page != null)
        {
          object urlEvaluationMode = this.Page.Items[(object) "SF_PageUrlEvaluationMode"];
          if (urlEvaluationMode != null)
            return (UrlEvaluationMode) urlEvaluationMode;
        }
        return UrlEvaluationMode.Default;
      }
    }

    /// <summary>
    /// Gets or sets the URL key prefix. Used when building or evaluating URLs for paging and filtering
    /// </summary>
    /// <value>The URL key prefix.</value>
    public string UrlKeyPrefix { get; set; }

    /// <summary>Gets or sets the page title modes.</summary>
    /// <value>The page title modes.</value>
    public ContentView.PageTitleModes PageTitleMode
    {
      get => this.pageTitleMode;
      set => this.pageTitleMode = value;
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> to be used by this widget.
    /// </summary>
    protected virtual ModuleBuilderManager ModuleManager
    {
      get
      {
        if (this.moduleBuilderManager == null)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager();
        return this.moduleBuilderManager;
      }
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" /> to be used by this widget.
    /// </summary>
    protected virtual DynamicModuleManager DynamicManager
    {
      get
      {
        if (this.dynamicManager == null)
          this.InitializeManager();
        return this.dynamicManager;
      }
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentView.DynamicContentType" /> displayed by this widget.
    /// </summary>
    protected virtual Type DynamicContentType
    {
      get
      {
        if (this.dynamicContentType == (Type) null)
          this.dynamicContentType = this.ResolveDynamicType(this.DynamicContentTypeName);
        return this.dynamicContentType;
      }
    }

    /// <summary>Gets the related date type displayed by this widget.</summary>
    protected virtual Type RelatedDataType
    {
      get
      {
        if (this.relatedDataType == (Type) null)
        {
          try
          {
            this.relatedDataType = !string.IsNullOrEmpty(this.RelatedDataTypeName) ? this.ModuleManager.ResolveDynamicClrType(this.RelatedDataTypeName) : typeof (InvalidDynamicContentType);
          }
          catch (ArgumentException ex)
          {
            this.relatedDataType = typeof (InvalidDynamicContentType);
          }
        }
        return this.relatedDataType;
      }
    }

    /// <summary>
    /// Gets or sets the related item ids displayed in the master list view of the widget.
    /// </summary>
    protected virtual Guid[] RelatedItemsIds
    {
      get
      {
        if (this.relatedItemsIds == null)
          this.relatedItemsIds = new Guid[0];
        return this.relatedItemsIds;
      }
      set => this.relatedItemsIds = value;
    }

    /// <summary>
    /// Determines whether RelatedDataTypeName and RelatedDataFieldName properties are populated and the related type is valid.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if related data properties are populated and valid; otherwise, <c>false</c>.
    /// </returns>
    protected virtual bool HasValidRelatedDataConfiguration
    {
      get
      {
        if (!this.hasValidRelatedDataConfiguration.HasValue)
          this.hasValidRelatedDataConfiguration = new bool?(this.CheckRelatedDataConfiguration());
        return this.hasValidRelatedDataConfiguration.Value;
      }
    }

    /// <summary>
    /// Gets or sets the control that displays the detail view of the dynamic content.
    /// By default returns an instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentViewDetail" />.
    /// </summary>
    /// <value>The detail view control.</value>
    protected DynamicContentViewDetail DetailViewControl
    {
      get
      {
        if (this.detailViewControl == null)
        {
          this.detailViewControl = new DynamicContentViewDetail(this.DynamicManager);
          this.detailViewControl.Host = this;
        }
        return this.detailViewControl;
      }
      set => this.detailViewControl = value;
    }

    /// <summary>
    /// Gets or sets the control that displays the master view of the dynamic content.
    /// By default returns an instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentViewMaster" />.
    /// </summary>
    /// <value>The master view control.</value>
    protected DynamicContentViewMaster MasterViewControl
    {
      get
      {
        if (this.masterViewControl == null)
        {
          this.masterViewControl = new DynamicContentViewMaster(this.DynamicManager);
          this.masterViewControl.Host = this;
        }
        return this.masterViewControl;
      }
      set => this.masterViewControl = value;
    }

    /// <summary>Gets or sets the detail item.</summary>
    /// <value>The detail item.</value>
    protected DynamicContent DetailItem { get; set; }

    /// <summary>Gets the type of the dynamic module.</summary>
    /// <value>The type of the dynamic module.</value>
    protected DynamicModuleType DynamicModuleType
    {
      get
      {
        if (this.dynamicModuleType == null)
          this.dynamicModuleType = this.ModuleManager.GetDynamicModuleType(this.DynamicContentTypeName);
        return this.dynamicModuleType;
      }
    }

    /// <summary>
    /// Gets or sets the meta keywords field name. The runtime value of this field will be used to set the page meta keywords tag.
    /// If this is not set the meta keywords tag will remain as set in the page properties.
    /// This setting is effective in detail mode of the content view and the field should exist as a property on the detail item type.
    /// </summary>
    public string MetaKeywordsField { get; set; }

    /// <summary>
    /// Gets or sets the meta description field name. The runtime value of this field will be used to set the page meta description tag
    /// If this is not set the meta description tag will remain as set in the page properties.
    /// This setting is effective in detail mode of the content view and the field should exist as a property on the detail item type.
    /// </summary>
    public string MetaDescriptionField { get; set; }

    /// <summary>
    /// Gets or sets the meta title field name. The runtime value of this field will be used to set the page title tag
    /// If this is not set the title tag will be set to the default title.
    /// This setting is effective in detail mode of the content view and the field should exist as a property on the detail item type.
    /// </summary>
    public string MetaTitleField { get; set; }

    private bool IsPreviewMode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to apply the pagination URLs to the header of the page.
    /// </summary>
    public bool? SetPaginationUrls { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether filter by parent URL is enabled.
    /// </summary>
    public bool FilterByParentUrl { get; set; }

    /// <summary>
    /// Checks if the parent item used for filtering should be resolved from currently opened child item URL.
    /// </summary>
    public bool EnableExtendedParentUrlResolving { get; set; }

    /// <summary>
    /// Checks if filtering by parent on multiple levels is enabled.
    /// </summary>
    public bool EnableParentMultipleLevelFiltering { get; set; }

    /// <summary>
    /// Gets or sets the parent type name used for filtering items. If multiple level filtering is enabled,
    /// parent type name will be used as end filtering level.
    /// </summary>
    public string FilterParentTypeName { get; set; }

    /// <summary>
    /// Checks if detail item should be displayed when child details view is opened.
    /// </summary>
    public bool ShowDetailsViewOnChildDetailsView { get; set; }

    /// <summary>
    /// Checks if the content list view should be hidden when child details view is opened.
    /// </summary>
    public bool HideListViewOnChildDetailsView { get; set; }

    /// <summary>
    /// Checks if list view should be displayed if FilterByParentUrl is enabled, but parent cannot be resolved from the URL.
    /// </summary>
    public bool ShowListViewOnEmpyParentFilter { get; set; }

    /// <summary>
    /// Checks if master list view should mark currently resolved from URL item.
    /// </summary>
    public bool SelectCurrentItem { get; set; }

    /// <summary>
    /// Gets child content type names for the DynamicModuleType.
    /// </summary>
    protected List<string> ChildContentTypeNames
    {
      get
      {
        if (this.childContentTypeNames == null)
          this.childContentTypeNames = this.ModuleManager.GetContentTypesSuccessors(this.DynamicModuleType, getMultipleChildren: true).Select<DynamicModuleType, string>((Func<DynamicModuleType, string>) (c => c.GetFullTypeName())).ToList<string>();
        return this.childContentTypeNames;
      }
    }

    /// <summary>
    /// Gets the parent content type names that will determine if parent item is resolved.
    /// </summary>
    protected List<string> ParentContentTypeNames
    {
      get
      {
        if (this.parentContentTypeNames == null)
          this.parentContentTypeNames = new List<string>();
        return this.parentContentTypeNames;
      }
      set => this.parentContentTypeNames = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if the current view is in a single item mode.
    /// </summary>
    private bool IsSpecificItemOnly { get; set; }

    private void InitializeManager()
    {
      string defaultPoviderName;
      this.dynamicManager = DynamicContentView.ResolveManagerWithProvider(this.GetProviderName(out defaultPoviderName));
      if (this.dynamicManager != null)
        return;
      this.dynamicManager = DynamicContentView.ResolveManagerWithProvider(defaultPoviderName);
      this.isControlDefinitionProviderCorrect = false;
    }

    private string GetProviderName(out string defaultPoviderName)
    {
      defaultPoviderName = DynamicModuleManager.GetDefaultProviderName(this.DynamicModuleType.ModuleName);
      return this.ControlDefinition != null && !string.IsNullOrEmpty(this.ControlDefinition.ProviderName) ? this.ControlDefinition.ProviderName : defaultPoviderName;
    }

    private static DynamicModuleManager ResolveManagerWithProvider(
      string providerName)
    {
      try
      {
        return DynamicModuleManager.GetManager(providerName);
      }
      catch (ConfigurationErrorsException ex)
      {
        return (DynamicModuleManager) null;
      }
    }

    /// <inheritdoc />
    [Category("Definitions")]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual ContentViewControlDefinition ControlDefinition
    {
      get
      {
        if (this.definition == null)
        {
          this.definition = new ContentViewControlDefinition();
          this.definition.Views.Add((IContentViewDefinition) new DynamicContentViewMasterDefinition());
          this.definition.Views.Add((IContentViewDefinition) new DynamicContentViewDetailDefinition());
        }
        return this.definition;
      }
      protected internal set
      {
        if (this.definition != value)
          this.ChildControlsCreated = false;
        this.definition = value;
      }
    }

    /// <inheritdoc />
    public virtual IContentViewMasterDefinition MasterViewDefinition => this.ControlDefinition.Views["DynamicContentMasterView"] as IContentViewMasterDefinition;

    /// <inheritdoc />
    public virtual IContentViewDetailDefinition DetailViewDefinition => this.ControlDefinition.Views["DynamicContentDetailView"] as IContentViewDetailDefinition;

    /// <inheritdoc />
    public virtual ContentViewDisplayMode ContentViewDisplayMode { get; set; }

    /// <summary>Gets or sets the name of the master view.</summary>
    /// <value>The name of the master view.</value>
    public virtual string MasterViewName
    {
      get
      {
        if (string.IsNullOrEmpty(this.masterViewName))
          this.masterViewName = "DynamicContentMasterView";
        return this.masterViewName;
      }
      set => this.masterViewName = value;
    }

    /// <summary>Gets or sets the name of the detail view.</summary>
    /// <value>The name of the detail view.</value>
    public virtual string DetailViewName
    {
      get
      {
        if (string.IsNullOrEmpty(this.detailViewName))
          this.detailViewName = "DynamicContentDetailView";
        return this.detailViewName;
      }
      set => this.detailViewName = value;
    }

    /// <inheritdoc />
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.EnsureChildControls();
    }

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.DisplayRelatedData(this.RelatedDataDefinition.RelatedItemSource == RelatedItemSource.DataItemContainer))
      {
        if (this.ControlDefinition.ProviderName == "sf-site-default-provider" && this.ControlDefinition.ContentType != (Type) null)
          this.ControlDefinition.ProviderName = RelatedDataHelper.ResolveProvider(this.ControlDefinition.ContentType.FullName);
        if (this.IsModuleEnabledForCurrentSite())
          this.DisplayRelatedItems();
        else
          this.HandleInvalidContentType(Res.Get<DynamicModuleResources>().DeletedModuleWarning);
      }
      if (this.detailViewControlBound)
        return;
      this.RebindDetailView();
    }

    /// <inheritdoc />
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      if (!this.CanResolveDynamicContentType() || !this.IsModuleActive())
      {
        this.HandleInvalidContentType(Res.Get<DynamicModuleResources>().DeletedModuleWarning);
      }
      else
      {
        if (this.DisplayRelatedData() || this.HasValidRelatedDataConfiguration && this.InitializeRelatedData())
          return;
        this.InitializeManager();
        if (!this.isControlDefinitionProviderCorrect)
          return;
        if (this.IsSingleItem())
        {
          if (!this.IsDesignMode())
          {
            if (this.DetailItem == null)
              return;
            if (!this.IsPreviewMode && this.DetailItem.Status != ContentLifecycleStatus.Live)
            {
              this.DetailItem = this.DynamicManager.Lifecycle.GetLive((ILifecycleDataItem) this.DetailItem) as DynamicContent;
              if (this.DetailItem != null)
                this.DynamicManager.Provider.ApplyFilters((IDataItem) this.DetailItem);
            }
            if (this.DetailItem == null || !this.DetailItem.Visible && !this.IsPreviewMode)
              return;
            this.ResolvePageInfo();
          }
          else
          {
            if (this.DetailItem == null)
            {
              this.Controls.Clear();
              this.Controls.Add((Control) new LiteralControl(Res.Get<Labels>().SelectedItemWasDeletedSelectAnotherItem));
              return;
            }
            if (this.DetailItem.Status == ContentLifecycleStatus.Master)
              this.DynamicManager.Provider.ApplyViewFieldPermissions((IDataItem) this.DetailItem);
          }
          this.InitializeDetailView(this.DetailItem);
        }
        else if (this.ContentViewDisplayMode != ContentViewDisplayMode.Detail && !this.HasValidRelatedDataConfiguration)
          this.PreInitializeMasterView();
        if (!this.Visible)
          return;
        this.SubscribeCacheDependency();
      }
    }

    /// <summary>
    /// Resolves and sets the page properties (meta tags - keywords , description, canonical url tags etc.)
    /// </summary>
    protected virtual void ResolvePageInfo()
    {
      if (this.DetailItem == null || this.Page == null || this.IsNestedContentLocatableView() || this.IsSpecificItemOnly && !Config.Get<SystemConfig>().ContentLocationsSettings.EnableSingleItemModeWidgetsBackwardCompatibilityMode)
        return;
      if (!string.IsNullOrEmpty(this.MetaKeywordsField))
      {
        string metaValue = DynamicContentView.GetMetaValue((object) this.DetailItem, this.MetaKeywordsField);
        if (!string.IsNullOrEmpty(metaValue))
          this.Page.MetaKeywords = metaValue;
      }
      if (!string.IsNullOrEmpty(this.MetaDescriptionField))
      {
        string metaValue = DynamicContentView.GetMetaValue((object) this.DetailItem, this.MetaDescriptionField);
        if (!string.IsNullOrEmpty(metaValue))
          this.Page.MetaDescription = metaValue;
      }
      this.AddCanonicalUrlTagIfEnabled(this.Page, (IDataItem) this.DetailItem);
    }

    private static string GetMetaValue(object detailItem, string fieldName)
    {
      PropertyDescriptor property1 = TypeDescriptor.GetProperties(detailItem)[fieldName];
      if (property1 == null)
        return (string) null;
      if (property1 is TaxonomyPropertyDescriptor property2)
        return property2.GetTaxaText(detailItem);
      return property1.GetValue(detailItem)?.ToString();
    }

    /// <summary>Subscribes the cache dependency.</summary>
    protected virtual void SubscribeCacheDependency()
    {
      if (this.IsBackend())
        return;
      IList<CacheDependencyKey> dependencyObjects = this.GetCacheDependencyObjects();
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "PageDataCacheDependencyName"))
        SystemManager.CurrentHttpContext.Items.Add((object) "PageDataCacheDependencyName", (object) new List<CacheDependencyKey>());
      ((List<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) "PageDataCacheDependencyName"]).AddRange((IEnumerable<CacheDependencyKey>) dependencyObjects);
      if (this.DetailItem != null)
        return;
      DynamicModuleManager dynamicManager = this.DynamicManager;
      PageRouteHandler.RegisterContentListCacheVariation(typeof (DynamicContent), dynamicManager == null || dynamicManager.Provider == null ? string.Empty : dynamicManager.Provider.Name);
    }

    /// <summary>
    /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <returns></returns>
    /// <value>A collection of  instances of type <see cref="!:CacheDependencyNotifiedObject" />.</value>
    public virtual IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      List<CacheDependencyKey> cacheDependencyNotifiedObjects = new List<CacheDependencyKey>();
      Type dynamicContentType = this.DynamicContentType;
      this.AddCachedItem(cacheDependencyNotifiedObjects, dynamicContentType.FullName, typeof (DynamicModule));
      if (this.DetailItem != null && this.DetailItem.Id != Guid.Empty)
      {
        cacheDependencyNotifiedObjects.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(dynamicContentType, this.DetailItem.Id));
      }
      else
      {
        string appName = this.DynamicManager == null || this.DynamicManager.Provider == null ? string.Empty : this.DynamicManager.Provider.ApplicationName;
        cacheDependencyNotifiedObjects.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(dynamicContentType, appName));
      }
      return (IList<CacheDependencyKey>) cacheDependencyNotifiedObjects;
    }

    /// <summary>Gets a resolved from the URL item.</summary>
    protected virtual DynamicContent ItemFromUrl
    {
      get
      {
        if (this.itemFromUrl == null)
        {
          DynamicContent resolvedItem;
          if (!this.TryGetItemFromUrlQueryId(out resolvedItem))
            resolvedItem = this.TryGetItemFromUrl();
          this.itemFromUrl = resolvedItem;
        }
        return this.itemFromUrl;
      }
    }

    /// <summary>
    /// Tries to resolve dynamic content item from the URL for the current dynamic type.
    /// </summary>
    /// <returns>
    ///  	<c>true</c> if dynamic content is resolved; otherwise, <c>false</c>.
    /// </returns>
    protected virtual bool IsSingleItem()
    {
      if (!this.isSingleItem.HasValue || !this.isSingleItem.HasValue)
        this.isSingleItem = new bool?(this.ResolveSngleItem());
      return this.isSingleItem.Value;
    }

    private bool ResolveSngleItem()
    {
      if (this.ContentViewDisplayMode == ContentViewDisplayMode.Master)
        return false;
      this.IsPreviewMode = this.IsPreviewRequested();
      if (this.DetailViewDefinition.DataItemId != Guid.Empty)
        return this.GetItemFromDefinition();
      DynamicContent dynamicContent = this.ItemFromUrl;
      if (dynamicContent != null)
      {
        string fullName = this.DynamicContentType.FullName;
        if (this.ShowDetailsViewOnChildDetailsView)
        {
          while (dynamicContent.GetType().FullName != fullName && dynamicContent.SystemParentItem != null)
            dynamicContent = dynamicContent.SystemParentItem;
        }
        if (dynamicContent.GetType().FullName == fullName && !string.IsNullOrEmpty(this.GetUrlParameterString(false)))
        {
          MatchCollection matchCollection = Regex.Matches(this.GetUrlParameterString(false), "/!(?<urlPrefix>[a-zA-Z0-9_\\-]+)/.*");
          if (matchCollection.Count == 1 && matchCollection[0].Groups["urlPrefix"].Value == this.UrlKeyPrefix || matchCollection.Count == 0 && string.IsNullOrEmpty(this.UrlKeyPrefix))
          {
            SystemManager.CurrentHttpContext.Items[(object) "detailItem"] = (object) dynamicContent;
            this.DetailItem = dynamicContent;
            RouteHelper.SetUrlParametersResolved();
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>
    /// Attempts to get item by id for the current dynamic type. The id is specified as a query string parameter.
    /// </summary>
    /// <param name="resolvedItem">The resolved dynamic content.</param>
    /// <returns><c>true</c> if an item is resolved, otherwise <c>false</c></returns>
    protected bool TryGetItemFromUrlQueryId(out DynamicContent resolvedItem)
    {
      NameValueCollection stringParameters = this.GetQueryStringParameters();
      string input = (string) null;
      if (stringParameters != null)
        input = stringParameters["sf-itemId"];
      if (input != null)
      {
        Guid itemIdGuid;
        if (Guid.TryParse(input, out itemIdGuid))
        {
          resolvedItem = this.DynamicManager.GetDataItems(this.DynamicContentType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.Id == itemIdGuid)).FirstOrDefault<DynamicContent>();
          return resolvedItem != null;
        }
      }
      resolvedItem = (DynamicContent) null;
      return false;
    }

    /// <summary>
    /// Tries the get item from URL. Child or parent item types are used for resolving the item.
    /// </summary>
    protected DynamicContent TryGetItemFromUrl()
    {
      lifecycleItem = (DynamicContent) null;
      string[] urlParameters = this.GetUrlParameters();
      if (urlParameters != null && urlParameters.Length != 0)
      {
        List<string> itemTypes = new List<string>();
        if (this.DynamicModuleType.ParentModuleType != null)
        {
          DynamicModuleType type = this.DynamicModuleType.ParentModuleType;
          this.ParentContentTypeNames.Add(type.GetFullTypeName());
          if (this.EnableParentMultipleLevelFiltering || !string.IsNullOrEmpty(this.FilterParentTypeName))
          {
            DynamicModuleType parentModuleType = this.DynamicModuleType.ParentModuleType;
            while (parentModuleType.ParentModuleType != null)
            {
              parentModuleType = parentModuleType.ParentModuleType;
              string fullTypeName = parentModuleType.GetFullTypeName();
              this.ParentContentTypeNames.Add(fullTypeName);
              if (!string.IsNullOrEmpty(this.FilterParentTypeName) && fullTypeName == this.FilterParentTypeName)
                break;
            }
            type = parentModuleType;
          }
          itemTypes.Add(type.GetFullTypeName());
          itemTypes.AddRange((IEnumerable<string>) this.ModuleManager.GetContentTypesSuccessors(type, getMultipleChildren: true).Select<DynamicModuleType, string>((Func<DynamicModuleType, string>) (m => m.GetFullTypeName())).ToList<string>());
        }
        else
        {
          itemTypes.Add(this.DynamicModuleType.GetFullTypeName());
          itemTypes.AddRange((IEnumerable<string>) this.ChildContentTypeNames);
        }
        string urlParams = this.GetUrlParameterString(true);
        bool isPublished = !this.IsDesignMode() && (!this.IsPreviewRequested() || this.GetRequestedItemStatus() == ContentLifecycleStatus.Live);
        urlParams = this.RemovePagingFromUrl(urlParams);
        if (!string.IsNullOrEmpty(urlParams))
        {
          if (!(this.DynamicManager.Provider.GetItemsFromUrl(urlParams, (IEnumerable<string>) itemTypes, isPublished).FirstOrDefault<IDataItem>() is DynamicContent lifecycleItem) && !this.IsSpecificItemOnly)
          {
            ILifecycleDataItem resolvedItem = (ILifecycleDataItem) null;
            if (this.TryGetFallbackItem((Func<ILifecycleDataItem>) (() => this.DynamicManager.Provider.GetItemsFromUrl(urlParams, (IEnumerable<string>) itemTypes, isPublished).FirstOrDefault<IDataItem>() as ILifecycleDataItem), out resolvedItem))
              lifecycleItem = resolvedItem as DynamicContent;
          }
          object resultItem;
          if (lifecycleItem != null && this.TryGetItemWithRequestedStatus((ILifecycleDataItem) lifecycleItem, (ILifecycleManager) this.DynamicManager, out resultItem))
          {
            lifecycleItem = resultItem as DynamicContent;
            this.DynamicManager.Provider.ApplyFilters((IDataItem) lifecycleItem);
          }
        }
      }
      return lifecycleItem;
    }

    /// <summary>
    /// Gets a value indicating whether a parent item can be resolved from the URL. IF ExtendedParentUrlResolving is enabled, tries to get up in the hierarchy of items until reaches
    /// the item from the current parent type.
    /// </summary>
    protected bool CanResolveParentItemFromUrl(out DynamicContent parentItem)
    {
      if (this.DynamicModuleType.ParentModuleType != null)
      {
        DynamicContent itemFromUrl = this.ItemFromUrl;
        if (itemFromUrl != null)
        {
          string parentContentTypeFullName = this.DynamicModuleType.ParentModuleType.GetFullTypeName();
          if (!this.EnableParentMultipleLevelFiltering && !string.IsNullOrEmpty(this.FilterParentTypeName))
            parentContentTypeFullName = this.FilterParentTypeName;
          DynamicContent dynamicContent = itemFromUrl;
          if (this.EnableExtendedParentUrlResolving)
          {
            while (!this.IsParentTypeValid(dynamicContent, parentContentTypeFullName) && dynamicContent.SystemParentItem != null)
              dynamicContent = dynamicContent.SystemParentItem;
          }
          if (this.IsParentTypeValid(dynamicContent, parentContentTypeFullName) && !string.IsNullOrEmpty(this.GetUrlParameterString(false)))
          {
            object resultItem;
            if (this.TryGetItemWithRequestedStatus((ILifecycleDataItem) dynamicContent, (ILifecycleManager) this.DynamicManager, out resultItem))
            {
              dynamicContent = resultItem as DynamicContent;
              this.DynamicManager.Provider.ApplyFilters((IDataItem) dynamicContent);
            }
            RouteHelper.SetUrlParametersResolved();
            parentItem = dynamicContent;
            return true;
          }
        }
      }
      parentItem = (DynamicContent) null;
      return false;
    }

    /// <summary>
    /// Gets a value indicating whether a child item can be resolved from the URL. Tries to resolve the item from all possible type successors.
    /// </summary>
    /// <returns>Value indicating whether a child item can be resolved from the URL</returns>
    protected bool CanResolveChildItemFromUrl()
    {
      DynamicContent itemFromUrl = this.ItemFromUrl;
      return itemFromUrl != null && this.ChildContentTypeNames.Contains(itemFromUrl.GetType().FullName);
    }

    /// <summary>
    /// Loads the related data and initialize the corresponding view - master or details.
    /// </summary>
    protected virtual bool InitializeRelatedData()
    {
      DynamicContent detailItem;
      if (!this.IsDesignMode() && this.TryGetRelatedDataItem(out detailItem))
      {
        if (detailItem != null && this.ContentViewDisplayMode != ContentViewDisplayMode.Master)
        {
          this.DetailItem = detailItem;
          this.InitializeDetailView(detailItem);
          return true;
        }
        if (this.fetchedArrayOfItems && this.ContentViewDisplayMode != ContentViewDisplayMode.Detail)
        {
          this.PreInitializeMasterView();
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Tries to resolve the related dynamic item from url and to get the value of related property.
    /// </summary>
    /// <returns>
    ///   	<c>true</c> if related item is successfully resolved and related data is fetched; otherwise, <c>false</c>.
    /// </returns>
    protected bool TryGetRelatedDataItem(out DynamicContent detailItem)
    {
      detailItem = (DynamicContent) null;
      this.InitializeManager();
      if (this.HasValidRelatedDataConfiguration && this.isControlDefinitionProviderCorrect)
      {
        string urlParameterString = this.GetUrlParameterString(true);
        if (urlParameterString.IsNullOrWhitespace())
          return false;
        bool published = !this.IsDesignMode();
        if (this.DynamicManager.Provider.GetItemFromUrl(this.RelatedDataType, urlParameterString, published, out string _) is DynamicContent itemFromUrl)
        {
          PropertyDescriptor property = TypeDescriptor.GetProperties((object) itemFromUrl)[this.RelatedDataFieldName];
          if (property != null)
          {
            if (property.PropertyType.FullName == typeof (Guid).FullName)
            {
              this.relatedItemId = itemFromUrl.GetValue<Guid>(this.RelatedDataFieldName);
              detailItem = this.DynamicManager.GetDataItems(this.DynamicContentType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (r => r.OriginalContentId == this.relatedItemId)).FirstOrDefault<DynamicContent>();
              return true;
            }
            if (property.PropertyType.FullName == typeof (Guid[]).FullName)
            {
              this.fetchedArrayOfItems = true;
              this.RelatedItemsIds = itemFromUrl.GetValue<Guid[]>(this.RelatedDataFieldName);
              return true;
            }
          }
        }
      }
      return false;
    }

    /// <summary>Setups property values for the master view control.</summary>
    protected virtual void InitializeMasterView()
    {
      if (this.HasValidRelatedDataConfiguration && this.RelatedItemsIds != null)
        this.MasterViewControl.SourceItemsIds = this.RelatedItemsIds;
      this.MasterViewControl.TemplateKey = string.IsNullOrEmpty(this.MasterViewDefinition.TemplateKey) ? this.DefaultMasterTemplateKey : this.MasterViewDefinition.TemplateKey;
      this.MasterViewControl.DynamicContentType = this.DynamicContentType;
      this.MasterViewControl.MasterViewDefinition = this.MasterViewDefinition;
      this.MasterViewControl.UrlEvaluationMode = this.UrlEvaluationMode;
      this.MasterViewControl.UrlKeyPrefix = this.UrlKeyPrefix;
      this.MasterViewControl.Title = this.Title;
      this.Controls.Add((Control) this.MasterViewControl);
    }

    /// <summary>Initializes the detail view.</summary>
    /// <param name="dataItem">The data item.</param>
    protected virtual void InitializeDetailView(DynamicContent dataItem)
    {
      this.DetailViewControl.DataItem = dataItem;
      this.DetailViewControl.DynamicContentType = dataItem.GetType();
      this.DetailViewControl.TemplateKey = string.IsNullOrEmpty(this.DetailViewDefinition.TemplateKey) ? this.DefaultDetailTemplateKey : this.DetailViewDefinition.TemplateKey;
      this.DetailViewControl.DetailViewDefinition = this.DetailViewDefinition;
      this.DetailViewControl.PageTitleMode = this.PageTitleMode;
      this.DetailViewControl.MetaTitleField = this.MetaTitleField;
      this.DetailViewControl.DynamicModuleType = this.DynamicModuleType;
      this.DetailViewControl.Title = this.Title;
      this.Controls.Add((Control) this.DetailViewControl);
      this.RebindDetailView();
    }

    /// <summary>Rebinds the detail view.</summary>
    protected virtual void RebindDetailView()
    {
      if (this.DetailItem == null || !this.IsDesignMode() && !this.DetailItem.Visible && !this.IsPreviewMode || this.ContentViewDisplayMode == ContentViewDisplayMode.Master)
        return;
      this.DetailViewControl.DataBind();
      this.detailViewControlBound = true;
    }

    /// <summary>
    /// Set up support for child types in the MasterViewControl.
    /// </summary>
    protected virtual void PreInitializeMasterView()
    {
      if (this.HideListViewOnChildDetailsView && this.CanResolveChildItemFromUrl())
        return;
      if (this.FilterByParentUrl)
      {
        if (!this.IsDesignMode() || SystemManager.IsInlineEditingMode)
        {
          DynamicContent parentItem;
          if (this.CanResolveParentItemFromUrl(out parentItem))
          {
            this.TrySetSelectedItem();
            this.InitializeMasterView();
            this.MasterViewControl.ParentItem = parentItem;
            this.MasterViewControl.PagerBaseUrl = this.GetPagerBaseUrl();
          }
          else
          {
            if (!this.ShowListViewOnEmpyParentFilter)
              return;
            this.TrySetSelectedItem();
            this.InitializeMasterView();
          }
        }
        else
        {
          string plural = PluralsResolver.Instance.ToPlural(this.DynamicModuleType.DisplayName);
          string str1 = this.DynamicModuleType.ParentModuleType == null || this.EnableParentMultipleLevelFiltering ? "parent" : this.DynamicModuleType.ParentModuleType.DisplayName;
          string str2 = string.Format(Res.Get<DynamicModuleResources>().FilteredByParentText, (object) plural, (object) str1);
          this.Controls.Add((Control) new LiteralControl()
          {
            Text = str2
          });
        }
      }
      else
      {
        this.TrySetSelectedItem();
        this.InitializeMasterView();
      }
    }

    /// <summary>
    /// Determines whether the dynamic content type can be resolved.
    /// </summary>
    internal bool CanResolveDynamicContentType() => this.DynamicContentType != typeof (InvalidDynamicContentType);

    /// <summary>Determines whether the related module is active.</summary>
    internal bool IsModuleActive() => this.ModuleManager.GetDynamicModule(this.DynamicModuleType.ParentModuleId).Status == DynamicModuleStatus.Active;

    /// <summary>
    /// Show user friendly message on the backend if the module or dynamic type no longer exists.
    /// </summary>
    internal void HandleInvalidContentType(string errorMessage)
    {
      if (this.IsDesignMode())
        this.Controls.Add((Control) new LiteralControl()
        {
          Text = string.Format("<p class=\"sfFailure\">{0}</p>", (object) errorMessage)
        });
      else
        this.Visible = false;
    }

    /// <summary>
    /// Determines whether RelatedDataTypeName and RelatedDataFieldName properties are populated and the related type is valid.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if related data properties are populated and valid; otherwise, <c>false</c>.
    /// </returns>
    internal bool CheckRelatedDataConfiguration() => this.RelatedDataType.FullName != typeof (InvalidDynamicContentType).FullName && !string.IsNullOrEmpty(this.RelatedDataFieldName);

    /// <summary>
    /// Gets the base URL used for the Pager.
    /// Paging is appended to this URL.
    /// </summary>
    internal string GetPagerBaseUrl()
    {
      string url = SiteMapBase.GetActualCurrentNode().Url;
      string urlParameterString = this.GetUrlParameterString(true);
      string str = urlParameterString.IsNullOrEmpty() ? string.Empty : this.RemovePagingFromUrl(urlParameterString);
      return RouteHelper.ResolveUrl(url + str, UrlResolveOptions.Absolute);
    }

    /// <summary>Removes the paging from the specified URL.</summary>
    /// <param name="url">The URL.</param>
    /// <returns>The URL without paging</returns>
    internal string RemovePagingFromUrl(string url)
    {
      int pageNumber = this.GetPageNumber(UrlEvaluationMode.Default, this.UrlKeyPrefix);
      if (!url.IsNullOrEmpty() && pageNumber > 0)
      {
        string str = this.BuildUrl(PageNumberEvaluator.Name, (object) pageNumber, string.Empty).TrimEnd("/".ToCharArray());
        int length = url.IndexOf(str);
        if (length > -1)
          url = url.Substring(0, length);
      }
      url = url.TrimEnd("/".ToCharArray());
      return url;
    }

    /// <summary>Adds the cached item.</summary>
    /// <param name="cacheDependencyNotifiedObjects">The cache dependency notified objects.</param>
    /// <param name="key">The key.</param>
    /// <param name="type">The type.</param>
    protected virtual void AddCachedItem(
      List<CacheDependencyKey> cacheDependencyNotifiedObjects,
      string key,
      Type type)
    {
      if (cacheDependencyNotifiedObjects.Any<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (itm => itm.Key == key && itm.Type == type)))
        return;
      cacheDependencyNotifiedObjects.Add(new CacheDependencyKey()
      {
        Key = key,
        Type = type
      });
    }

    private void DisplayRelatedItems()
    {
      if (this.ContentViewDisplayMode == ContentViewDisplayMode.Detail)
      {
        this.DetailItem = this.GetRelatedItem() as DynamicContent;
        if (this.DetailItem != null)
          this.InitializeDetailView(this.DetailItem);
      }
      else
        this.PreInitializeMasterView();
      this.SubscribeCacheDependency();
    }

    /// <summary>
    /// Returns resolved type from FullTypeName. If Type cannot be resolved, returns null.
    /// </summary>
    private Type ResolveDynamicType(string typeFullName)
    {
      Type type1 = typeof (InvalidDynamicContentType);
      if (SystemManager.GetModule("ModuleBuilder") != null)
      {
        Type type2 = this.ModuleManager.ResolveDynamicClrType(typeFullName, false);
        if (type2 != (Type) null)
          return type2;
      }
      return type1;
    }

    /// <summary>
    /// Determines if parent item is correctly resolved. If multiple level filtering is enabled, checks if resolved parent is in ParentContentTypeNames collection.
    /// </summary>
    private bool IsParentTypeValid(DynamicContent parentItem, string parentContentTypeFullName) => this.EnableParentMultipleLevelFiltering ? this.ParentContentTypeNames.Contains(parentItem.GetType().FullName) : parentItem.GetType().FullName == parentContentTypeFullName;

    /// <summary>Gets item from set in definition DataItemId</summary>
    private bool GetItemFromDefinition()
    {
      this.DetailItem = this.DynamicManager.GetDataItems(this.DynamicContentType).FirstOrDefault<DynamicContent>((Expression<Func<DynamicContent, bool>>) (t => t.Id == this.DetailViewDefinition.DataItemId));
      if (this.DetailItem == null)
      {
        if (this.DesignMode)
          throw new Exception(string.Format("An item with ID: {0}", (object) this.DetailViewDefinition.DataItemId));
        this.Visible = false;
        return true;
      }
      object resultItem;
      if (this.TryGetItemWithRequestedStatus((ILifecycleDataItem) this.DetailItem, (ILifecycleManager) this.DynamicManager, out resultItem))
        this.DetailItem = resultItem as DynamicContent;
      this.DynamicManager.Provider.ApplyFilters((IDataItem) this.DetailItem);
      this.IsSpecificItemOnly = true;
      return true;
    }

    /// <summary>
    /// Tries to set selected item to the MasterViewControl if SelectCurrentItem setting is enabled.
    /// </summary>
    private void TrySetSelectedItem()
    {
      DynamicContent dynamicContent;
      if (!this.SelectCurrentItem || !this.TryGetSelectedItem(out dynamicContent))
        return;
      this.MasterViewControl.SelectedItem = dynamicContent;
      RouteHelper.SetUrlParametersResolved();
    }

    private bool TryGetSelectedItem(out DynamicContent item)
    {
      item = (DynamicContent) null;
      DynamicContent dynamicContent = this.ItemFromUrl;
      while (dynamicContent != null && dynamicContent.GetType().FullName != this.DynamicContentTypeName)
        dynamicContent = dynamicContent.SystemParentItem;
      item = dynamicContent;
      return item != null;
    }

    /// <inheritdoc />
    public virtual bool? DisableCanonicalUrlMetaTag { get; set; }

    /// <inheritdoc />
    public virtual IEnumerable<IContentLocationInfo> GetLocations()
    {
      if (this.ControlDefinition == null)
        return (IEnumerable<IContentLocationInfo>) null;
      ContentLocationInfo location = new ContentLocationInfo();
      Type type = TypeResolutionService.ResolveType(this.DynamicContentTypeName);
      location.ContentType = type;
      string providerName = this.GetProviderName(out string _);
      IManager manager;
      ManagerBase.TryGetMappedManager(type, providerName, out manager);
      if (providerName.IsNullOrEmpty() && manager != null)
        providerName = manager.Provider.Name;
      location.ProviderName = providerName;
      switch (this.ContentViewDisplayMode)
      {
        case ContentViewDisplayMode.Automatic:
          this.AddMasterViewFilters(location, this.MasterViewDefinition, type, manager, new ContentLocationFilterExpressionSettings("SystemParentId"));
          break;
        case ContentViewDisplayMode.Detail:
          this.AddDetailViewFilter(location, this.DetailViewDefinition, type, manager);
          break;
        default:
          return (IEnumerable<IContentLocationInfo>) null;
      }
      return (IEnumerable<IContentLocationInfo>) new ContentLocationInfo[1]
      {
        location
      };
    }

    /// <inheritdoc />
    public bool? DisplayRelatedData { get; set; }

    /// <inheritdoc />
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public RelatedDataDefinition RelatedDataDefinition
    {
      get
      {
        if (this.relatedDataDefinition == null)
          this.relatedDataDefinition = new RelatedDataDefinition();
        return this.relatedDataDefinition;
      }
      set => this.relatedDataDefinition = value;
    }

    /// <inheritdoc />
    public string GetContentType() => this.ControlDefinition.ContentType.FullName;

    /// <inheritdoc />
    public string GetProviderName()
    {
      string defaultPoviderName = string.Empty;
      return this.GetProviderName(out defaultPoviderName);
    }
  }
}
