// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.TaxonomyControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>
  /// Displays a list/cloud of taxa or just the selected taxa in when a content item is set
  /// </summary>
  [IndexRenderMode(IndexRenderModes.NoOutput)]
  public class TaxonomyControl : 
    SimpleView,
    ICustomWidgetVisualizationExtended,
    ICustomWidgetVisualization
  {
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.TaxonomyControl.ascx");
    private PropertyDescriptor fieldPropertyDescriptor;
    private string seeAllTextFormat = "All ({0}) {1}";
    private ITaxonomy taxonomy;
    private TaxonomyManager taxonomyManager;
    private string sortExpression = "Title ASC";
    internal const string CategoryWidgetCssClass = "sfHierarchicalTaxonIcn";
    internal const string TagWidgetCssClass = "sfFlatTaxonIcn";
    internal const string DepartmentWidgetCssClass = "sfHierarchicalTaxonIcn";
    private const string DefaultSortExpression = "Title ASC";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.TaxonomyControl" /> class.
    /// </summary>
    public TaxonomyControl() => this.LayoutTemplatePath = TaxonomyControl.layoutTemplatePath;

    /// <summary>
    /// Gets or sets the static type of the content that taxonomy control is going to display.
    /// </summary>
    /// <value>The type of the content.</value>
    [TypeConverter(typeof (StringTypeConverter))]
    public virtual Type ContentType { get; set; }

    /// <summary>
    /// Gets or sets the dynamic type name of the content that taxonomy control is going to display.
    /// </summary>
    /// <value>The dynamic type name of the content.</value>
    public virtual string DynamicContentType { get; set; }

    /// <summary>
    /// Gets or sets the provider name of specific content type.
    /// </summary>
    /// <value>The provider name of the content item.</value>
    public virtual string ContentProviderName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show item count for content items at taxonomy control.
    /// </summary>
    /// <value><c>true</c> if [show item count]; otherwise, <c>false</c>.</value>
    public virtual bool ShowItemCount { get; set; }

    /// <summary>Gets or sets the title of the taxonomy control.</summary>
    /// <value>The title.</value>
    public virtual string Title { get; set; }

    /// <summary>Gets or sets the provider.</summary>
    /// <value>The provider.</value>
    public virtual string Provider { get; set; }

    /// <summary>
    /// Gets or sets the base URL of the page that will be used to display content items.
    /// </summary>
    /// <value>The base URL.</value>
    public virtual string BaseUrl { get; set; }

    /// <summary>Specifies the rednering mode for the taxonomy control</summary>
    public virtual TaxonomyControl.RenderTaxonomyAs RenderAs { get; set; }

    /// <summary>Gets or sets the sort expression.</summary>
    /// <value>The sort expression.</value>
    public virtual string SortExpression
    {
      get => this.sortExpression;
      set => this.sortExpression = value;
    }

    /// <summary>
    /// Gets or sets the root taxon which children will be displayed as a top level in the widget.
    /// Used only if this display mode is selected.
    /// </summary>
    /// <value>The parent hierarchical taxon.</value>
    public virtual Guid RootTaxonId { get; set; }

    /// <summary>Gets or sets whether to show empty taxa.</summary>
    /// <value>Show empty taxonomies.</value>
    public virtual bool ShowEmptyTaxa { get; set; }

    /// <summary>Determines what taxa will be displayed by the widget.</summary>
    /// <value>The taxa to display.</value>
    public virtual TaxaToDisplay TaxaToDisplay { get; set; }

    /// <summary>
    /// Gets or sets the collection with the ids of the specific taxa that the widget will show.
    /// Used only if the display mode setting of the widget is set to show only specific items.
    /// </summary>
    /// <value>The collection with the selected taxa ids.</value>
    [TypeConverter(typeof (Telerik.Sitefinity.Utilities.TypeConverters.StringArrayConverter))]
    public virtual string[] SelectedTaxaIds { get; set; }

    /// <summary>
    /// Specifies what number of taxa(tags) will be displayed.
    /// </summary>
    public virtual int TaxaCount { get; set; }

    /// <summary>Gets or sets the template of the field control.</summary>
    /// <value>The template.</value>
    public virtual ConditionalTemplateContainer ConditionalTemplates { get; set; }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    public virtual string TaxonomyProvider { get; set; }

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

    /// <summary>Gets or sets the taxonomy pageId.</summary>
    /// <value>The taxonomy pageId.</value>
    public virtual Guid TaxonomyId { get; set; }

    /// <summary>
    /// Gets/sets the pageId of the content item for which the control should display the taxa
    /// </summary>
    /// <value>The content pageId.</value>
    public virtual Guid ContentId { get; set; }

    /// <summary>
    /// Gets or sets the name of the property that contains the taxonomy
    /// </summary>
    public virtual string FieldName { get; set; }

    /// <summary>
    /// Gets or sets the URL key prefix. Used when building and evaluating URLs together with ContentView controls
    /// </summary>
    /// <value>The URL key prefix.</value>
    public virtual string UrlKeyPrefix { get; set; }

    /// <summary>
    /// Gets or sets the text format of the link that is to be shown when the number of the taxa
    /// is bigger than the chosen limit in TaxaCount
    /// Must be something like <example>All ({0}) tags</example>
    /// </summary>
    public virtual string SeeAllTextFormat
    {
      get => this.seeAllTextFormat;
      set => this.seeAllTextFormat = value;
    }

    /// <summary>
    /// Gets the instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Model.ITaxonomy" /> representing the taxonomy to which the taxon field is bound to.
    /// </summary>
    protected internal virtual ITaxonomy Taxonomy
    {
      get
      {
        if (this.taxonomy == null)
          this.taxonomy = this.CurrentTaxonomyManager.GetTaxonomy(this.TaxonomyId);
        return this.taxonomy;
      }
    }

    /// <summary>
    /// Gets the taxonomy manager based on the specified provider or if no provider is set
    /// returns the default provider
    /// </summary>
    protected internal virtual TaxonomyManager CurrentTaxonomyManager
    {
      get
      {
        if (this.taxonomyManager == null)
          this.taxonomyManager = !string.IsNullOrEmpty(this.TaxonomyProvider) ? TaxonomyManager.GetManager(this.TaxonomyProvider) : TaxonomyManager.GetManager();
        return this.taxonomyManager;
      }
      set => this.taxonomyManager = value;
    }

    /// <summary>
    /// Returns the property descriptor of the specified FieldName
    /// </summary>
    protected internal virtual PropertyDescriptor FieldPropertyDescriptor
    {
      get
      {
        if (this.fieldPropertyDescriptor == null && !string.IsNullOrEmpty(this.FieldName))
          this.fieldPropertyDescriptor = TypeDescriptor.GetProperties(this.TaxonomyContentType)[this.FieldName];
        return this.fieldPropertyDescriptor;
      }
    }

    /// <summary>
    /// Gets or sets the type of the content that taxonomy control is going to display.
    /// If ContentType is set returns it otherwise try to resolve DynamicContentType.
    /// </summary>
    /// <value>The type of the content.</value>
    private Type TaxonomyContentType
    {
      get
      {
        if (this.ContentType != (Type) null)
          return this.ContentType;
        return !this.DynamicContentType.IsNullOrWhitespace() ? TypeResolutionService.ResolveType(this.DynamicContentType, false) : (Type) null;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!string.IsNullOrEmpty(this.FieldName))
        this.InitializeTaxonomyManagerFromFieldName();
      this.InitializeTaxaList();
    }

    /// <summary>Creates the container.</summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      ConditionalTemplateContainer control = container.GetControl<ConditionalTemplateContainer>();
      if (control == null)
        return container;
      control.Evaluate((object) this);
      return container;
    }

    /// <inheritdoc />
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Indicates if the control is empty.</summary>
    /// <value></value>
    [Browsable(false)]
    public bool IsEmpty { get; set; }

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    [Browsable(false)]
    public string EmptyLinkText
    {
      get
      {
        string str = (string) null;
        if (this.TaxonomyId == TaxonomyManager.TagsTaxonomyId)
          str = Res.Get<TaxonomyResources>().Tags;
        if (this.TaxonomyId == TaxonomyManager.DepartmentsTaxonomyId)
          str = Res.Get<TaxonomyResources>().Departments;
        return "Set " + (str ?? Res.Get<TaxonomyResources>().Categories);
      }
    }

    /// <summary>
    /// Gets or sets the Cascading Style Sheet (CSS) class to visualize the widget.
    /// </summary>
    [Browsable(false)]
    public string WidgetCssClass
    {
      get
      {
        if (this.TaxonomyId == TaxonomyManager.TagsTaxonomyId)
          return "sfFlatTaxonIcn";
        int num = this.TaxonomyId == TaxonomyManager.DepartmentsTaxonomyId ? 1 : 0;
        return "sfHierarchicalTaxonIcn";
      }
    }

    /// <summary>
    /// Gets the reference to the label that displays the title of the taxonomy field.
    /// </summary>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>Gets the repear that will render content items.</summary>
    /// <value>The grid.</value>
    protected internal virtual Repeater TaxaRepeater => this.Container.GetControl<Repeater>("repeater_" + this.RenderAs.ToString().ToLower(), true);

    /// <summary>Gets the link that shows the</summary>
    /// <value>The see all taxa link.</value>
    protected internal virtual HyperLink SeeAllTaxaLink => this.Container.GetControl<HyperLink>(nameof (SeeAllTaxaLink), true);

    /// <summary>Initializes the taxonomy manager from field name.</summary>
    protected internal virtual void InitializeTaxonomyManagerFromFieldName()
    {
      if (!(this.FieldPropertyDescriptor is TaxonomyPropertyDescriptor propertyDescriptor))
        return;
      this.CurrentTaxonomyManager = TaxonomyManager.GetManager(propertyDescriptor.MetaField.TaxonomyProvider);
    }

    /// <summary>Initializes the taxa list.</summary>
    protected internal virtual void InitializeTaxaList()
    {
      IDictionary<ITaxon, uint> dictionary;
      if (this.ContentId != Guid.Empty)
      {
        dictionary = (IDictionary<ITaxon, uint>) this.GetTaxaFromContentItem();
      }
      else
      {
        dictionary = (IDictionary<ITaxon, uint>) this.GetTaxaItemsCountForTaxonomy();
        int num = dictionary.Count<KeyValuePair<ITaxon, uint>>();
        if (num > this.TaxaCount && this.TaxaCount > 0)
        {
          this.SeeAllTaxaLink.Visible = true;
          this.SeeAllTaxaLink.Text = string.Format(this.SeeAllTextFormat, (object) num, (object) this.Taxonomy.Title.ToLower());
          this.SeeAllTaxaLink.NavigateUrl = this.BaseUrl;
        }
      }
      List<TaxonomyControl.TaxonData> data = this.PrepareData(dictionary);
      this.BindRepeater(data);
      this.IsEmpty = data.Count == 0;
      if (this.IsEmpty)
        return;
      this.TitleLabel.Text = this.Title;
    }

    /// <summary>
    /// Builds the full url for a particular taxon filter
    /// Override this method  to change the pattern of the url
    /// </summary>
    /// <param name="taxonRelativeUrl">The taxon relative URL.</param>
    /// <returns></returns>
    protected virtual string BuildUrl(string taxonRelativeUrl)
    {
      string virtualPath = this.BaseUrl;
      if (string.IsNullOrEmpty(virtualPath))
      {
        SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
        if (currentProvider == null || currentProvider != null && currentProvider.CurrentNode == null)
          return string.Empty;
        if (currentProvider.CurrentNode is PageSiteNode currentNode)
        {
          PageSiteNode firstPageDataNode = RouteHelper.GetFirstPageDataNode(currentNode, true);
          virtualPath = !currentNode.IsGroupPage || !(firstPageDataNode.Url != currentProvider.CurrentNode.Url) ? currentNode.GetUrl(true, true) : firstPageDataNode.Url;
        }
        else
          virtualPath = currentProvider.CurrentNode.Url;
      }
      if (string.IsNullOrEmpty(virtualPath))
        throw new ArgumentNullException("BaseUrl property could not be resolved.");
      if (string.IsNullOrEmpty(this.FieldName))
        throw new ArgumentNullException("FieldName property could not be resolved.");
      if (VirtualPathUtility.IsAppRelative(virtualPath))
        virtualPath = VirtualPathUtility.ToAbsolute(virtualPath);
      if (this.UrlEvaluationMode == UrlEvaluationMode.Default)
      {
        string currentNodeExtension = PageHelper.GetCurrentNodeExtension();
        if (!currentNodeExtension.IsNullOrEmpty() && virtualPath.EndsWith(currentNodeExtension))
          virtualPath = virtualPath.Substring(0, virtualPath.LastIndexOf(currentNodeExtension));
      }
      TaxonomyEvaluator taxonomyEvaluator = new TaxonomyEvaluator();
      TaxonBuildOptions taxonBuildOptions = TaxonBuildOptions.None;
      if (this.Taxonomy is HierarchicalTaxonomy)
        taxonBuildOptions = TaxonBuildOptions.Hierarchical;
      else if (this.Taxonomy is FlatTaxonomy)
        taxonBuildOptions = TaxonBuildOptions.Flat;
      string name = this.Taxonomy.Name;
      string taxon = taxonRelativeUrl;
      string fieldName = this.FieldName;
      int options = (int) taxonBuildOptions;
      int urlEvaluationMode = (int) this.UrlEvaluationMode;
      string urlKeyPrefix = this.UrlKeyPrefix;
      string str = taxonomyEvaluator.BuildUrl(name, taxon, fieldName, (TaxonBuildOptions) options, (UrlEvaluationMode) urlEvaluationMode, urlKeyPrefix);
      return virtualPath + str;
    }

    /// <summary>
    /// Returns a list with key-value elements, where the item key is the taxon title
    /// and the value is the size
    /// </summary>
    /// <param name="taxaCount"></param>
    /// <returns>Key is the generated taxon label, value is the size</returns>
    protected internal virtual List<TaxonomyControl.TaxonData> PrepareData(
      IDictionary<ITaxon, uint> taxaCount)
    {
      if (taxaCount.Count == 0)
        return new List<TaxonomyControl.TaxonData>();
      double average;
      double stdDev = this.StandardDeviation((ICollection<double>) taxaCount.Select<KeyValuePair<ITaxon, uint>, uint>((Func<KeyValuePair<ITaxon, uint>, uint>) (pair => pair.Value)).Select<uint, double>((Func<uint, double>) (t => (double) t)).ToList<double>(), out average);
      List<TaxonomyControl.TaxonData> taxonDataList = new List<TaxonomyControl.TaxonData>();
      foreach (KeyValuePair<ITaxon, uint> keyValuePair in (IEnumerable<KeyValuePair<ITaxon, uint>>) taxaCount)
      {
        int size = this.GetSize((double) keyValuePair.Value, average, stdDev);
        string str = keyValuePair.Key is HierarchicalTaxon ? (keyValuePair.Key as HierarchicalTaxon).FullUrl : keyValuePair.Key.UrlName.Value;
        taxonDataList.Add(new TaxonomyControl.TaxonData()
        {
          Title = (string) keyValuePair.Key.Title,
          Count = keyValuePair.Value,
          Size = size,
          Url = str
        });
      }
      return taxonDataList;
    }

    protected internal virtual void BindRepeater(List<TaxonomyControl.TaxonData> data)
    {
      if (this.IsDesignMode() && this.ContentId == Guid.Empty && this.TaxonomyId == Guid.Empty)
      {
        this.Title = "Please, specify the Taxonomy which you want to use. To do so, click on Edit and enter a value for TaxonomyId from this table: ";
        this.TaxaRepeater.DataSource = (object) TaxonomyManager.GetManager().GetTaxonomies<Telerik.Sitefinity.Taxonomies.Model.Taxonomy>().ToList<Telerik.Sitefinity.Taxonomies.Model.Taxonomy>();
        this.TaxaRepeater.ItemDataBound += new RepeaterItemEventHandler(this.DesignModeTaxonomyListItemDataBound);
        this.TaxaRepeater.DataBind();
      }
      else
      {
        this.TaxaRepeater.DataSource = (object) data;
        this.TaxaRepeater.ItemDataBound += new RepeaterItemEventHandler(this.TaxaRepeater_ItemDataBound);
        this.TaxaRepeater.DataBind();
      }
    }

    private void DesignModeTaxonomyListItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType == ListItemType.Header)
        e.Item.Controls.Clear();
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      e.Item.Controls.Clear();
      if (e.Item.ItemIndex == 0)
      {
        e.Item.Controls.Add((Control) new Literal()
        {
          Text = "<p style='margin-bottom: 10px;'><span style='font-size: 18px; color: #f00; '>Temporary:</span><br />Click <strong>Edit</strong> and enter a value for <strong>TaxonomyId</strong> from the table below in order to specify the Classification which you want to use.</p>"
        });
        Literal child = new Literal()
        {
          Text = "<table style='margin-bottom: 20px; width: 100%; '><tr><th style='padding: 3px 20px 3px 1px; border-bottom: 1px solid #ececec; font-weight: bold; '>Taxonomy</th><th style='padding: 3px 1px 3px 5px; border-bottom: 1px solid #ececec; font-weight: bold; '>TaxonomyId</th></tr>"
        };
        e.Item.Controls.Add((Control) child);
      }
      Telerik.Sitefinity.Taxonomies.Model.Taxonomy dataItem = e.Item.DataItem as Telerik.Sitefinity.Taxonomies.Model.Taxonomy;
      Literal child1 = new Literal()
      {
        Text = string.Format("<tr><td style='padding: 3px 20px 3px 1px; border-bottom: 1px solid #ececec;'>{0}</td><td style='padding: 3px 1px 3px 5px; border-bottom: 1px solid #ececec;'>{1}</td></tr>", (object) dataItem.Title, (object) dataItem.Id)
      };
      e.Item.Controls.Add((Control) child1);
      if (e.Item.ItemIndex != ((List<Telerik.Sitefinity.Taxonomies.Model.Taxonomy>) this.TaxaRepeater.DataSource).Count - 1)
        return;
      Literal child2 = new Literal()
      {
        Text = "</table>"
      };
      e.Item.Controls.Add((Control) child2);
    }

    /// <summary>
    /// Handles the ItemDataBound event of the TaxaRepeater control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    protected internal virtual void TaxaRepeater_ItemDataBound(
      object sender,
      RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      TaxonomyControl.TaxonData dataItem = e.Item.DataItem as TaxonomyControl.TaxonData;
      if (!(e.Item.FindControl("link") is HyperLink control))
        return;
      string html = dataItem.Title;
      if (this.ShowItemCount && this.ContentId == Guid.Empty)
      {
        if (control is SitefinityHyperLink sitefinityHyperLink)
        {
          sitefinityHyperLink.TextMode = TextMode.PassThrough;
          html = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(html);
        }
        html += string.Format("  <span class='sfCount'>({0})</span>", (object) dataItem.Count);
      }
      control.Text = html;
      if (this.RenderAs == TaxonomyControl.RenderTaxonomyAs.Cloud && this.ContentId == Guid.Empty)
        control.CssClass += (string) (object) dataItem.Size;
      control.NavigateUrl = this.BuildUrl(dataItem.Url);
    }

    /// <summary>
    /// Returns a dictionary where the key is the taxon and value is
    /// the number is the count of the times that the taxon is used(marked)
    /// </summary>
    protected internal virtual Dictionary<ITaxon, uint> GetTaxaItemsCountForTaxonomy()
    {
      IQueryable<ITaxon> queryable = this.GetAllTaxa();
      switch (this.TaxaToDisplay)
      {
        case TaxaToDisplay.TopLevel:
          queryable = this.GetTopLevelTaxa(queryable);
          break;
        case TaxaToDisplay.UnderParticularTaxon:
          queryable = this.GetTaxaByParent(queryable);
          break;
        case TaxaToDisplay.Selected:
          queryable = this.GetSpecificTaxa(queryable);
          break;
      }
      IQueryable<TaxonomyStatistic> taxonomyStatistics = this.GetTaxonomyStatistics();
      Dictionary<ITaxon, uint> source = this.GetTaxaItemsCount(this.Sort(queryable), taxonomyStatistics);
      if (this.TaxaCount > 0)
        source = source.OrderByDescending<KeyValuePair<ITaxon, uint>, uint>((Func<KeyValuePair<ITaxon, uint>, uint>) (r => r.Value)).Take<KeyValuePair<ITaxon, uint>>(this.TaxaCount).ToDictionary<KeyValuePair<ITaxon, uint>, ITaxon, uint>((Func<KeyValuePair<ITaxon, uint>, ITaxon>) (t => t.Key), (Func<KeyValuePair<ITaxon, uint>, uint>) (t => t.Value));
      return source;
    }

    private IQueryable<ITaxon> Sort(IQueryable<ITaxon> taxa)
    {
      int? totalCount = new int?(1);
      try
      {
        taxa = DataProviderBase.SetExpressions<ITaxon>(taxa, (string) null, this.SortExpression, new int?(), new int?(), ref totalCount);
      }
      catch (Exception ex)
      {
        taxa = DataProviderBase.SetExpressions<ITaxon>(taxa, (string) null, "Title ASC", new int?(), new int?(), ref totalCount);
      }
      return taxa;
    }

    private IQueryable<ITaxon> GetAllTaxa()
    {
      Guid taxonomyIdGuid = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(this.CurrentTaxonomyManager).ResolveSiteTaxonomyId(this.TaxonomyId);
      return (IQueryable<ITaxon>) this.CurrentTaxonomyManager.GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (t => t.Taxonomy.Id == taxonomyIdGuid));
    }

    private IQueryable<ITaxon> GetTopLevelTaxa(IQueryable<ITaxon> query) => (IQueryable<ITaxon>) Queryable.OfType<HierarchicalTaxon>(query).Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent == default (object)));

    private IQueryable<ITaxon> GetTaxaByParent(IQueryable<ITaxon> query) => (IQueryable<ITaxon>) Queryable.OfType<HierarchicalTaxon>(query).Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent.Id == this.RootTaxonId));

    private IQueryable<ITaxon> GetSpecificTaxa(IQueryable<ITaxon> query)
    {
      if (this.SelectedTaxaIds != null)
      {
        List<Guid> guids = new List<Guid>(this.SelectedTaxaIds.Length);
        for (int index = 0; index < this.SelectedTaxaIds.Length; ++index)
        {
          Guid result;
          if (Guid.TryParse(this.SelectedTaxaIds[index], out result))
            guids.Add(result);
        }
        if (guids.Count > 0)
        {
          query = (IQueryable<ITaxon>) Queryable.OfType<Taxon>(query).Where<Taxon>((Expression<Func<Taxon, bool>>) (t => guids.Contains(t.Id)));
          return query;
        }
      }
      return (IQueryable<ITaxon>) Enumerable.Empty<Taxon>().AsQueryable<Taxon>();
    }

    private Dictionary<ITaxon, uint> GetTaxaItemsCount(
      IQueryable<ITaxon> taxa,
      IQueryable<TaxonomyStatistic> statistics)
    {
      List<ITaxon> list = taxa.ToList<ITaxon>();
      Dictionary<ITaxon, uint> taxaItemsCount = new Dictionary<ITaxon, uint>(list.Count);
      foreach (ITaxon taxon1 in list)
      {
        ITaxon taxon = taxon1;
        if (this.HasTranslationInCurrentLanguage(taxon))
        {
          uint num = statistics.Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (s => s.TaxonId == taxon.Id)).Aggregate<TaxonomyStatistic, uint>(0U, (Expression<Func<uint, TaxonomyStatistic, uint>>) ((acc, stat) => acc + stat.MarkedItemsCount));
          if (num > 0U || this.ShowEmptyTaxa)
            taxaItemsCount.Add(taxon, num);
        }
      }
      return taxaItemsCount;
    }

    /// <summary>Gets the taxa from content item.</summary>
    /// <returns></returns>
    protected internal virtual Dictionary<ITaxon, uint> GetTaxaFromContentItem()
    {
      if (!(this.FieldPropertyDescriptor is TaxonomyPropertyDescriptor propertyDescriptor))
        throw new ArgumentException(string.Format("The specified field name \"{0}\" is not a taxonomy.", (object) this.FieldName));
      ContentManager manager = ContentManager.GetManager();
      bool isSingleTaxon = propertyDescriptor.MetaField.IsSingleTaxon;
      Guid contentId = this.ContentId;
      object obj = this.FieldPropertyDescriptor.GetValue((object) manager.GetContent(contentId));
      return obj != null ? this.GetTaxa(obj, isSingleTaxon).ToDictionary<ITaxon, ITaxon, uint>((Func<ITaxon, ITaxon>) (taxon => taxon), (Func<ITaxon, uint>) (taxon => 0U)) : (Dictionary<ITaxon, uint>) null;
    }

    /// <summary>Gets the taxa.</summary>
    /// <param name="value">The value.</param>
    /// <param name="isSingleTaxon">if set to <c>true</c> the value is a single taxon.</param>
    /// <returns></returns>
    protected internal virtual IList<ITaxon> GetTaxa(object value, bool isSingleTaxon)
    {
      IList<ITaxon> taxa = (IList<ITaxon>) new List<ITaxon>();
      if (isSingleTaxon)
      {
        taxa.Add(this.GetSingleTaxon(value));
      }
      else
      {
        foreach (object obj in value as IEnumerable)
          taxa.Add(this.GetSingleTaxon(obj));
      }
      return taxa;
    }

    /// <summary>Gets the single taxon.</summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    protected internal virtual ITaxon GetSingleTaxon(object value)
    {
      switch (value)
      {
        case ITaxon singleTaxon:
          return singleTaxon;
        case Guid id:
          return this.CurrentTaxonomyManager.GetTaxon(id);
        default:
          return (ITaxon) null;
      }
    }

    /// <Summary>Calculates standard deviation</Summary>
    protected internal virtual double StandardDeviation(
      ICollection<double> data,
      out double average)
    {
      double num1 = 0.0;
      average = data.Average();
      foreach (double num2 in (IEnumerable<double>) data)
        num1 += Math.Pow(num2 - average, 2.0);
      double count = (double) data.Count;
      return Math.Sqrt(num1 / (count - 1.0));
    }

    /// <summary>
    ///  The size is calculated by the occurrence (count) of the taxa
    /// in relation to the mean value and the standard deviation.
    /// </summary>
    protected internal virtual int GetSize(double count, double average, double stdDev)
    {
      double num = count - average;
      if (num != 0.0 && stdDev != 0.0)
        num /= stdDev;
      if (num > 2.0)
        return 6;
      if (num > 1.33 && num <= 2.0)
        return 5;
      if (num > 0.67 && num <= 1.33)
        return 4;
      if (num > -0.67 && num <= 0.67)
        return 3;
      return num > -1.33 && num <= -0.67 ? 2 : 1;
    }

    private string GetContentProviderName()
    {
      string contentProviderName = string.Empty;
      if (string.IsNullOrWhiteSpace(this.ContentProviderName))
      {
        if (this.ContentType != (Type) null)
          contentProviderName = SystemManager.CurrentContext.CurrentSite.GetDefaultProvider(SystemManager.DataSourceRegistry.GetDataSource(ManagerBase.GetMappedManager(this.ContentType).GetType().FullName).Name).ProviderName;
        else if (!string.IsNullOrWhiteSpace(this.DynamicContentType))
        {
          ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
          DynamicModuleType dynamicModuleType = manager.GetDynamicModuleType(manager.ResolveDynamicClrType(this.DynamicContentType));
          if (dynamicModuleType != null)
            contentProviderName = SystemManager.CurrentContext.CurrentSite.GetDefaultProvider(SystemManager.DataSourceRegistry.GetDataSource(dynamicModuleType.ModuleName).Name).ProviderName;
        }
      }
      else
        contentProviderName = this.ContentProviderName;
      return contentProviderName;
    }

    /// <summary>
    /// Determines whether the provided taxon has translation to the current language.
    /// </summary>
    /// <param name="taxon">The taxon.</param>
    /// <returns></returns>
    private bool HasTranslationInCurrentLanguage(ITaxon taxon)
    {
      Taxon taxon1 = (Taxon) taxon;
      if (((IEnumerable<string>) taxon1.AvailableLanguages).Contains<string>(taxon.Title.CurrentLanguage.Name))
        return true;
      return ((IEnumerable<string>) taxon1.AvailableLanguages).Count<string>() == 1 && taxon1.AvailableLanguages[0] == string.Empty;
    }

    /// <summary>
    /// Gets the taxonomy statistics for current site if it is in multisite mode and also for single site mode.
    /// </summary>
    /// <returns></returns>
    private IQueryable<TaxonomyStatistic> GetTaxonomyStatistics()
    {
      Guid taxonomyIdGuid = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(this.CurrentTaxonomyManager).ResolveSiteTaxonomyId(this.TaxonomyId);
      IQueryable<TaxonomyStatistic> source = this.CurrentTaxonomyManager.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (t => t.TaxonomyId == taxonomyIdGuid && t.MarkedItemsCount > 0U && (int) t.StatisticType == 2));
      if (this.TaxaToDisplay == TaxaToDisplay.All)
      {
        string defaultProviderName = this.GetContentProviderName();
        if (!string.IsNullOrWhiteSpace(defaultProviderName))
          source = source.Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (t => t.ItemProviderName == defaultProviderName));
        if (this.TaxonomyContentType != (Type) null)
        {
          string typeName = this.TaxonomyContentType.FullName;
          source = source.Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (t => t.DataItemType == typeName));
        }
      }
      return source;
    }

    /// <summary>
    /// Represents the different rednering modes for the taxonomy control
    /// </summary>
    public enum RenderTaxonomyAs
    {
      /// <summary>Reders as horizontal list - floating layout</summary>
      HorizontalList,
      /// <summary>Renders as vertical list</summary>
      VerticalList,
      /// <summary>
      /// Reder as a Clound control - floating with 6 increment steps for the size
      /// </summary>
      Cloud,
    }

    protected internal class TaxonData
    {
      public string Title { get; set; }

      public uint Count { get; set; }

      public int Size { get; set; }

      public string Url { get; set; }
    }
  }
}
