// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.HierarchicalContentView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.UI;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend
{
  /// <summary>
  /// This is the frontend widget for displaying the data of hierarchical dynamic module types.
  /// </summary>
  [ParseChildren(true)]
  [ControlDesigner(typeof (HierarchicalContentViewDesigner))]
  public class HierarchicalContentView : DynamicContentView
  {
    private bool isSingleItem;
    private Type dynamicContentType;
    private string viewMode;
    private Dictionary<string, DynamicTypeBasicSettings> typesSettings;
    private IEnumerable<Type> contentTypeSuccessors;
    private bool childTypeIsLoaded;
    private IEnumerable<Type> childTypes;
    internal const string defaultViewMode = "Full";
    private const string widgetNameRegularExpression = "/!(?<urlPrefix>[a-zA-Z0-9_\\-]+)/.*";
    private ContentViewControlDefinition definition;

    /// <summary>
    /// Gets or sets the view mode.
    /// Determine whether the full hierarchy or only specified dynamic module type will be displayed.
    /// </summary>
    /// <value>
    /// The view mode - "Full" for the full functionality otherwise the type name of the specified dynamic module type.
    /// </value>
    public string ViewMode
    {
      get
      {
        if (this.viewMode == null)
          this.viewMode = "Full";
        return this.viewMode;
      }
      set => this.viewMode = value;
    }

    /// <summary>
    /// Contains the basic settings for all dynamic types from the hierarchy.
    /// Key is the type name of dynamic module type, value is an instance of  An <see cref="T:DynamicTypeBasicSettings" />.
    /// </summary>
    /// <value>The paged types.</value>
    [TypeConverter(typeof (JsonTypeConverter<Dictionary<string, DynamicTypeBasicSettings>>))]
    public Dictionary<string, DynamicTypeBasicSettings> TypesSettings
    {
      get
      {
        if (this.typesSettings == null)
          this.typesSettings = new Dictionary<string, DynamicTypeBasicSettings>();
        return this.typesSettings;
      }
      set => this.typesSettings = value;
    }

    /// <summary>
    /// Gets collection of all content type successors for hierarchical widget
    /// </summary>
    protected virtual IEnumerable<Type> ContentTypeSuccessors
    {
      get
      {
        if (this.contentTypeSuccessors == null)
        {
          Type type = this.ModuleManager.ResolveDynamicClrType(this.DynamicContentTypeName);
          this.contentTypeSuccessors = (IEnumerable<Type>) this.ModuleManager.GetContentTypesSuccessors(type);
          if (!this.IsFullFunctionality && type.FullName != this.DynamicContentType.FullName)
            this.contentTypeSuccessors = this.contentTypeSuccessors.SkipWhile<Type>((Func<Type, bool>) (t => t.FullName != this.DynamicContentType.FullName));
        }
        return this.contentTypeSuccessors;
      }
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.HierarchicalContentView.DynamicContentType" /> displayed by this widget.
    /// </summary>
    protected override Type DynamicContentType
    {
      get
      {
        if (this.dynamicContentType == (Type) null)
        {
          this.dynamicContentType = base.DynamicContentType;
          if (!this.IsFullFunctionality && this.dynamicContentType != typeof (InvalidDynamicContentType))
          {
            string fullTypeName = this.dynamicContentType.Namespace + "." + this.ViewMode;
            try
            {
              this.dynamicContentType = this.ModuleManager.ResolveDynamicClrType(fullTypeName);
            }
            catch (ArgumentException ex)
            {
              this.dynamicContentType = typeof (InvalidDynamicContentType);
            }
          }
          this.ChildControlsCreated = false;
        }
        return this.dynamicContentType;
      }
    }

    /// <summary>
    /// Gets or sets the type of the dynamic content to display.
    /// </summary>
    [PropertyPersistence(IsKey = true)]
    public override string DynamicContentTypeName { get; set; }

    /// <inheritdoc />
    [Category("Definitions")]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public override ContentViewControlDefinition ControlDefinition
    {
      get
      {
        if (this.definition == null)
        {
          this.definition = new ContentViewControlDefinition();
          if (this.DynamicContentType != (Type) null && this.DynamicContentType != typeof (InvalidDynamicContentType))
          {
            this.definition.ContentType = this.DynamicContentType;
            ViewDefinitionCollection views1 = this.definition.Views;
            ContentViewMasterDefinition masterDefinition1 = new ContentViewMasterDefinition();
            masterDefinition1.ViewName = "DynamicContentMasterView";
            masterDefinition1.AllowPaging = new bool?(true);
            masterDefinition1.ItemsPerPage = new int?(20);
            masterDefinition1.FilterExpression = "Visible = true AND Status = Live";
            views1.Add((IContentViewDefinition) masterDefinition1);
            ViewDefinitionCollection views2 = this.definition.Views;
            ContentViewDetailDefinition detailDefinition1 = new ContentViewDetailDefinition();
            detailDefinition1.ViewName = "DynamicContentDetailView";
            views2.Add((IContentViewDefinition) detailDefinition1);
            foreach (Type contentTypesSuccessor in (IEnumerable<Type>) this.ModuleManager.GetContentTypesSuccessors(this.ModuleManager.ResolveDynamicClrType(this.DynamicContentTypeName)))
            {
              ViewDefinitionCollection views3 = this.definition.Views;
              ContentViewMasterDefinition masterDefinition2 = new ContentViewMasterDefinition();
              masterDefinition2.ViewName = contentTypesSuccessor.Name + "DynamicContentMasterView";
              masterDefinition2.AllowPaging = new bool?(true);
              masterDefinition2.ItemsPerPage = new int?(20);
              masterDefinition2.FilterExpression = "Visible = true AND Status = Live";
              views3.Add((IContentViewDefinition) masterDefinition2);
              ViewDefinitionCollection views4 = this.definition.Views;
              ContentViewDetailDefinition detailDefinition2 = new ContentViewDetailDefinition();
              detailDefinition2.ViewName = contentTypesSuccessor.Name + "DynamicContentDetailView";
              views4.Add((IContentViewDefinition) detailDefinition2);
            }
          }
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
    public override IContentViewMasterDefinition MasterViewDefinition => this.DynamicContentType != typeof (InvalidDynamicContentType) && this.ChildTypes != null && this.ChildTypes.FirstOrDefault<Type>() != (Type) null && this.ControlDefinition.Views.Contains(this.ChildTypes.First<Type>().Name + "DynamicContentMasterView") ? this.ControlDefinition.Views[this.ChildTypes.First<Type>().Name + "DynamicContentMasterView"] as IContentViewMasterDefinition : this.ControlDefinition.Views["DynamicContentMasterView"] as IContentViewMasterDefinition;

    /// <inheritdoc />
    public override IContentViewDetailDefinition DetailViewDefinition => this.DynamicContentType != typeof (InvalidDynamicContentType) && this.DetailItem != null && this.DetailItem.GetType().FullName != this.DynamicContentType.FullName && this.ControlDefinition.Views.Contains(this.DetailItem.GetType().Name + "DynamicContentDetailView") ? this.ControlDefinition.Views[this.DetailItem.GetType().Name + "DynamicContentDetailView"] as IContentViewDetailDefinition : this.ControlDefinition.Views["DynamicContentDetailView"] as IContentViewDetailDefinition;

    /// <summary>
    /// Gets a value indicating whether the full hierarchy or one particular type will be shown.
    /// It depends on the ViewMode
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the ViewMode is Full; otherwise, <c>false</c>.
    /// </value>
    protected virtual bool IsFullFunctionality => this.ViewMode.Equals("Full");

    /// <summary>
    /// Determines whether the collection of children items will be rendered.
    /// </summary>
    protected bool ShouldRenderChildren => this.ContentTypeSuccessors != null && this.ContentTypeSuccessors.Count<Type>() > 0 && this.ChildTypes != null && this.ChildTypes.Count<Type>() > 0 && this.ContentTypeSuccessors.Contains<Type>(this.ChildTypes.First<Type>());

    /// <summary>Gets the type of the DataItem children.</summary>
    /// <value>The type of the child.</value>
    private IEnumerable<Type> ChildTypes
    {
      get
      {
        if (!this.childTypeIsLoaded)
        {
          if (this.DetailItem != null)
            this.childTypes = this.ModuleManager.GetChildTypes(this.DetailItem.GetType());
          this.childTypeIsLoaded = true;
        }
        return this.childTypes;
      }
    }

    /// <inheritdoc />
    protected override void CreateChildControls()
    {
      if (this.CanResolveDynamicContentType() && !this.IsFullFunctionality && this.DynamicContentType.FullName != this.DynamicContentTypeName && !this.ContentTypeSuccessors.Contains<Type>(this.DynamicContentType))
      {
        this.Controls.Clear();
        DynamicModuleType dynamicModuleType = this.ModuleManager.GetDynamicModuleType(this.DynamicContentType);
        this.HandleInvalidContentType(string.Format(Res.Get<DynamicModuleResources>().ThisWidgetCannotDisplayThisType, (object) PluralsResolver.Instance.ToPlural(dynamicModuleType.DisplayName)));
      }
      else
        base.CreateChildControls();
    }

    /// <inheritdoc />
    protected override void InitializeDetailView(DynamicContent dataItem)
    {
      DynamicTypeBasicSettings defaultTypeSettings1;
      if (!this.TypesSettings.TryGetValue(dataItem.GetType().Name, out defaultTypeSettings1))
        defaultTypeSettings1 = this.GetDefaultTypeSettings(dataItem.GetType().FullName);
      this.DetailViewControl.DataItem = dataItem;
      this.DetailViewControl.DynamicContentType = dataItem.GetType();
      this.DetailViewControl.TemplateKey = defaultTypeSettings1.DetailTemplateId == Guid.Empty ? this.DefaultDetailTemplateKey : defaultTypeSettings1.DetailTemplateId.ToString();
      this.DetailViewControl.DetailViewDefinition = this.DetailViewDefinition;
      this.DetailViewControl.PageTitleMode = this.PageTitleMode;
      this.DetailViewControl.MetaTitleField = this.MetaTitleField;
      this.Controls.Add((Control) this.DetailViewControl);
      if (!this.ShouldRenderChildren)
        return;
      DynamicTypeBasicSettings defaultTypeSettings2;
      if (!this.TypesSettings.TryGetValue(this.ChildTypes.First<Type>().Name, out defaultTypeSettings2))
        defaultTypeSettings2 = this.GetDefaultTypeSettings(this.ChildTypes.First<Type>().FullName);
      this.MasterViewControl.DynamicContentType = this.ChildTypes.First<Type>();
      this.MasterViewControl.DataSource = this.GetChildItemsDataSource();
      this.MasterViewControl.MasterViewDefinition = this.MasterViewDefinition;
      this.MasterViewControl.MasterViewDefinition.AllowPaging = new bool?(defaultTypeSettings2.AllowPaging);
      this.MasterViewControl.MasterViewDefinition.ItemsPerPage = new int?(defaultTypeSettings2.ItemsPerPage);
      if (!defaultTypeSettings2.SortExpression.IsNullOrEmpty())
        this.MasterViewControl.MasterViewDefinition.SortExpression = defaultTypeSettings2.SortExpression;
      this.MasterViewControl.TemplateKey = defaultTypeSettings2.MasterTemplateId == Guid.Empty ? this.DefaultMasterTemplateKey : defaultTypeSettings2.MasterTemplateId.ToString();
      this.MasterViewControl.UrlKeyPrefix = this.UrlKeyPrefix;
      this.MasterViewControl.PagerBaseUrl = this.GetPagerBaseUrl();
      this.MasterViewControl.UrlEvaluationMode = this.UrlEvaluationMode;
      this.Controls.Add((Control) this.MasterViewControl);
    }

    /// <inheritdoc />
    protected override void InitializeMasterView()
    {
      DynamicTypeBasicSettings defaultTypeSettings;
      if (!this.TypesSettings.TryGetValue(this.DynamicContentType.Name, out defaultTypeSettings))
        defaultTypeSettings = this.GetDefaultTypeSettings(this.DynamicContentType.FullName);
      this.MasterViewControl.DynamicContentType = this.DynamicContentType;
      this.MasterViewControl.MasterViewDefinition = this.MasterViewDefinition;
      this.MasterViewControl.MasterViewDefinition.AllowPaging = new bool?(defaultTypeSettings.AllowPaging);
      this.MasterViewControl.MasterViewDefinition.ItemsPerPage = new int?(defaultTypeSettings.ItemsPerPage);
      if (!defaultTypeSettings.SortExpression.IsNullOrEmpty())
        this.MasterViewControl.MasterViewDefinition.SortExpression = defaultTypeSettings.SortExpression;
      this.MasterViewControl.TemplateKey = defaultTypeSettings.MasterTemplateId == Guid.Empty ? this.DefaultMasterTemplateKey : defaultTypeSettings.MasterTemplateId.ToString();
      this.MasterViewControl.UrlKeyPrefix = this.UrlKeyPrefix;
      this.MasterViewControl.UrlEvaluationMode = this.UrlEvaluationMode;
      this.Controls.Add((Control) this.MasterViewControl);
    }

    /// <inheritdoc />
    protected override bool IsSingleItem()
    {
      this.isSingleItem = base.IsSingleItem();
      string urlParameterString = this.GetUrlParameterString(true);
      string redirectUrl = string.Empty;
      bool flag = !this.IsDesignMode();
      if (this.isSingleItem && this.DetailViewDefinition.DataItemId != Guid.Empty && !urlParameterString.IsNullOrWhitespace())
      {
        string urlParams = this.RemovePagingFromUrl(urlParameterString);
        DynamicContent dynamicContent = (DynamicContent) null;
        if (!urlParams.IsNullOrWhitespace())
          dynamicContent = this.TryGetFromTypeSuccessors(urlParams, flag, ref redirectUrl);
        this.ValidateResolvedItem(dynamicContent);
      }
      else if (!this.isSingleItem && !urlParameterString.IsNullOrWhitespace())
      {
        string str = this.RemovePagingFromUrl(urlParameterString);
        dynamicContent = (DynamicContent) null;
        if (!str.IsNullOrWhitespace() && !(this.DynamicManager.Provider.GetItemFromUrl(this.DynamicContentType, str, flag, out redirectUrl) is DynamicContent dynamicContent))
          dynamicContent = this.TryGetFromTypeSuccessors(str, flag, ref redirectUrl);
        this.ValidateResolvedItem(dynamicContent);
      }
      return this.isSingleItem;
    }

    /// <inheritdoc />
    public override IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      List<CacheDependencyKey> cacheDependencyNotifiedObjects = new List<CacheDependencyKey>();
      Type dynamicContentType = this.DynamicContentType;
      Type type = typeof (DynamicContent);
      if (this.DetailItem != null && this.DetailItem.Id != Guid.Empty)
      {
        this.AddCachedItem(cacheDependencyNotifiedObjects, this.DetailItem.Id.ToString().ToUpperInvariant(), type);
        if (this.ContentTypeSuccessors != null && this.ContentTypeSuccessors.Count<Type>() > 1)
        {
          foreach (Type contentTypeSuccessor in this.ContentTypeSuccessors)
            this.AddCachedItem(cacheDependencyNotifiedObjects, contentTypeSuccessor.FullName, type);
        }
      }
      else if (this.ContentTypeSuccessors != null)
      {
        if (this.DynamicContentType != (Type) null)
          this.AddCachedItem(cacheDependencyNotifiedObjects, dynamicContentType.FullName, type);
        foreach (Type contentTypeSuccessor in this.ContentTypeSuccessors)
          this.AddCachedItem(cacheDependencyNotifiedObjects, contentTypeSuccessor.FullName, type);
      }
      return (IList<CacheDependencyKey>) cacheDependencyNotifiedObjects;
    }

    public override IEnumerable<IContentLocationInfo> GetLocations()
    {
      IEnumerable<IContentLocationInfo> locations = base.GetLocations();
      if (locations != null)
      {
        IContentLocationInfo contentLocationInfo1 = locations.FirstOrDefault<IContentLocationInfo>();
        if (contentLocationInfo1 != null)
        {
          for (Type parentType = this.ModuleManager.GetChildTypes(contentLocationInfo1.ContentType).FirstOrDefault<Type>(); parentType != (Type) null; parentType = this.ModuleManager.GetChildTypes(parentType).FirstOrDefault<Type>())
          {
            ContentLocationInfo contentLocationInfo2 = new ContentLocationInfo()
            {
              ContentType = parentType,
              ProviderName = contentLocationInfo1.ProviderName
            };
            IContentViewDefinition view;
            if (this.ControlDefinition.TryGetView(parentType.Name + "DynamicContentMasterView", out view) && view is IContentViewMasterDefinition masterDefinition)
            {
              string filterExpression = DefinitionsHelper.GetFilterExpression(string.Empty, masterDefinition.AdditionalFilter);
              if (!string.IsNullOrEmpty(filterExpression))
                contentLocationInfo2.Filters.Add((IContentLocationFilter) new BasicContentLocationFilter(filterExpression));
            }
            locations = (IEnumerable<IContentLocationInfo>) new List<IContentLocationInfo>(locations)
            {
              (IContentLocationInfo) contentLocationInfo2
            };
          }
        }
      }
      return locations;
    }

    /// <summary>
    /// Tries to resolve dynamic item for any of the successors types.
    /// </summary>
    /// <param name="urlParams">The URL params.</param>
    /// <param name="isPublished">Determine whether to use live or master item.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns></returns>
    protected DynamicContent TryGetFromTypeSuccessors(
      string urlParams,
      bool isPublished,
      ref string redirectUrl)
    {
      foreach (Type contentTypeSuccessor in this.ContentTypeSuccessors)
      {
        if (this.DynamicManager.Provider.GetItemFromUrl(contentTypeSuccessor, urlParams, isPublished, out redirectUrl) is DynamicContent lifecycleItem)
        {
          object resultItem;
          if (this.TryGetItemWithRequestedStatus((ILifecycleDataItem) lifecycleItem, (ILifecycleManager) this.DynamicManager, out resultItem))
            lifecycleItem = resultItem as DynamicContent;
          return lifecycleItem;
        }
      }
      return (DynamicContent) null;
    }

    /// <summary>Gets the child items of the current dynamic item.</summary>
    /// <returns></returns>
    protected IQueryable<DynamicContent> GetChildItemsDataSource()
    {
      IQueryable<DynamicContent> query = Enumerable.Empty<DynamicContent>().AsQueryable<DynamicContent>();
      if (this.ChildTypes != null && this.ChildTypes.FirstOrDefault<Type>() != (Type) null)
      {
        ContentLifecycleStatus status = !this.IsDesignMode() || this.IsInlineEditingMode() ? ContentLifecycleStatus.Live : ContentLifecycleStatus.Master;
        Guid id = this.DetailViewControl.DataItem.Status == ContentLifecycleStatus.Master ? this.DetailViewControl.DataItem.Id : this.DetailViewControl.DataItem.OriginalContentId;
        query = this.DynamicManager.GetDataItems(this.ChildTypes.FirstOrDefault<Type>()).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (d => d.SystemParentId == id && (int) d.Status == (int) status && (d.Visible == true || (int) d.Status == 0)));
      }
      return this.EvaluateUrl<DynamicContent>(query, "Taxonomy", "", this.DynamicContentType, UrlEvaluationMode.Default, this.UrlKeyPrefix);
    }

    private void ValidateResolvedItem(DynamicContent item)
    {
      if (item == null || string.IsNullOrEmpty(this.GetUrlParameterString(false)))
        return;
      MatchCollection matchCollection = Regex.Matches(this.GetUrlParameterString(false), "/!(?<urlPrefix>[a-zA-Z0-9_\\-]+)/.*");
      if ((matchCollection.Count != 1 || !(matchCollection[0].Groups["urlPrefix"].Value == this.UrlKeyPrefix)) && (matchCollection.Count != 0 || !string.IsNullOrEmpty(this.UrlKeyPrefix)))
        return;
      this.DetailItem = item;
      RouteHelper.SetUrlParametersResolved();
      this.isSingleItem = true;
    }

    internal DynamicTypeBasicSettings GetDefaultTypeSettings(
      string typeFullName)
    {
      IQueryable<ControlPresentation> source = PageManager.GetManager().GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
      string predicate1 = string.Format("ControlType == \"{0}\" AND Condition == \"{1}\"", (object) typeof (DynamicContentViewDetail).FullName, (object) typeFullName);
      string predicate2 = string.Format("ControlType == \"{0}\" AND Condition == \"{1}\"", (object) typeof (DynamicContentViewMaster).FullName, (object) typeFullName);
      ControlPresentation controlPresentation1 = source.Where<ControlPresentation>(predicate1).OrderBy<ControlPresentation, DateTime>((Expression<Func<ControlPresentation, DateTime>>) (lt => lt.DateCreated)).FirstOrDefault<ControlPresentation>();
      ControlPresentation controlPresentation2 = source.Where<ControlPresentation>(predicate2).OrderBy<ControlPresentation, DateTime>((Expression<Func<ControlPresentation, DateTime>>) (lt => lt.DateCreated)).FirstOrDefault<ControlPresentation>();
      Guid guid1 = Guid.Empty;
      if (controlPresentation1 != null)
        guid1 = controlPresentation1.Id;
      Guid guid2 = Guid.Empty;
      if (controlPresentation2 != null)
        guid2 = controlPresentation2.Id;
      return new DynamicTypeBasicSettings()
      {
        DetailTemplateId = guid1,
        MasterTemplateId = guid2,
        AllowPaging = true,
        ItemsPerPage = 20
      };
    }
  }
}
