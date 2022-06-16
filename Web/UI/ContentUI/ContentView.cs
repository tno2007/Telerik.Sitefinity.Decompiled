// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Taxonomies.Extensions;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Events;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;
using Telerik.Sitefinity.Web.UI.ContentUI.Extensions;
using Telerik.Sitefinity.Web.UI.ContentUI.Views;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// This control acts as a container for the specific views that display
  /// all types implementing the <see cref="!:Telerik.Sitefinity.GenericContent.Model.IContent" /> interface.
  /// </summary>
  public class ContentView : 
    CompositeControl,
    IContentView,
    ICustomWidgetVisualization,
    IHasCacheDependency,
    IBreadcrumExtender,
    IContentLocatableView,
    IRelatedDataView
  {
    private RelatedDataDefinition relatedDataDefinition;
    private const string PageInfoResolvedFromDetailItemKey = "content_view_page_info_resolved";
    private string viewNameUrlKey = "ViewName";
    private bool enableViewState;
    private string controlDefinitionName;
    private ContentViewControlDefinition controlDefinition;
    private IContentViewDefinition currentView;
    private const string widgetNameRegularExpression = "/!(?<urlPrefix>\\w+)/.*";
    private IContentViewMasterDefinition masterViewDefinition;
    private IContentViewDetailDefinition detailViewDefinition;
    private string masterViewName;
    private string detailViewName;
    private IContentViewConfig contentViewConfig;
    private const string ViewNameKey = "ViewName";
    private object[] dataSource;
    private bool isControlDefinitionProviderCorrect = true;
    private const string controlDefinitionDoesNotExist = "The ContentViewDefinition with name '{0}' does not exist in configuration files. \r\n            You should either add the ContentViewDefiniton with such name to the configuration;\r\n            change ControlDefinitionName or load ControlDefinition programmatically by setting \r\n            the ControlDefinition property.";
    private const string noMasterViews = "ContentView control is in Master mode and no MasterViews exist in the Views collection \r\n            defined by this control.";
    private const string noDetailViews = "ContentView control is in Detail mode and no DetailViews exist in the Views collection \r\n            defined by this control.";
    internal const string urlViewNotFound = "The view specified in the url cannot be found in the Views collection of the current control.";
    internal const string detailItemContextKey = "detailItem";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentView" /> class.
    /// </summary>
    public ContentView() => this.SetPaginationUrls = new bool?(true);

    public override bool Visible
    {
      get => base.Visible;
      set
      {
        if (value && !base.Visible)
          this.ChildControlsCreated = false;
        base.Visible = value;
      }
    }

    /// <summary>
    /// Gets or sets the name of the query string parameter that passes the template to be used.
    /// </summary>
    public virtual string ViewNameUrlKey
    {
      get => this.viewNameUrlKey;
      set => this.viewNameUrlKey = value;
    }

    /// <summary>
    /// Gets or sets the name of the module which initialization should be ensured prior to rendering this control.
    /// </summary>
    /// <value>The name of the module.</value>
    public virtual string ModuleName { get; set; }

    /// <inheritdoc />
    public virtual bool? DisableCanonicalUrlMetaTag { get; set; }

    /// <summary>
    /// Specifies if the content view should personalize page properties or not.
    /// </summary>
    public virtual bool DisableModifyPageInfo { get; set; }

    /// <summary>
    /// Gets or sets the name of the configuration definition for the whole control. From this definition
    /// control can find out all other configurations needed in order to construct views.
    /// </summary>
    /// <value>The name of the control definition.</value>
    [Category("Definitions")]
    [PropertyPersistence(IsKey = true)]
    public virtual string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set
      {
        if (!(this.controlDefinitionName != value))
          return;
        this.controlDefinitionName = value;
        this.controlDefinition = (ContentViewControlDefinition) null;
      }
    }

    /// <summary>Gets or sets the control definition object.</summary>
    /// <value>The definition.</value>
    [Category("Definitions")]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual ContentViewControlDefinition ControlDefinition
    {
      get
      {
        if (this.controlDefinition == null)
        {
          if (string.IsNullOrEmpty(this.ControlDefinitionName))
          {
            this.controlDefinition = new ContentViewControlDefinition();
          }
          else
          {
            if (this.contentViewConfig == null)
              this.contentViewConfig = this.GetContentViewConfig();
            if (!this.contentViewConfig.ContentViewControls.ContainsKey(this.ControlDefinitionName))
              throw new InvalidOperationException(string.Format("The ContentViewDefinition with name '{0}' does not exist in configuration files. \r\n            You should either add the ContentViewDefiniton with such name to the configuration;\r\n            change ControlDefinitionName or load ControlDefinition programmatically by setting \r\n            the ControlDefinition property.", (object) this.ControlDefinitionName));
            this.controlDefinition = ContentViewControlDefinition.Initialize(this.ControlDefinitionName);
          }
        }
        return this.controlDefinition;
      }
      protected internal set
      {
        if (this.controlDefinition != value)
          this.ChildControlsCreated = false;
        this.controlDefinition = value;
      }
    }

    /// <summary>
    /// Gets or sets the data source for the MasterView. When this
    /// property is set, all other filtering and sorting options will be
    /// disregarded.
    /// </summary>
    public object[] DataSource
    {
      get => this.dataSource;
      set
      {
        this.dataSource = value;
        this.ChildControlsCreated = false;
      }
    }

    protected virtual IContentViewConfig GetContentViewConfig() => (IContentViewConfig) Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>();

    protected override void AddParsedSubObject(object obj) => base.AddParsedSubObject(obj);

    /// <summary>Gets or sets the display mode of the content view.</summary>
    /// <remarks>
    /// Note that this enumeration differs from the FieldDisplayMode.
    /// </remarks>
    [Category("Views")]
    public virtual ContentViewDisplayMode ContentViewDisplayMode { get; set; }

    /// <summary>Gets the definition of the currently loaded view.</summary>
    [Browsable(false)]
    public virtual IContentViewDefinition CurrentView => this.currentView;

    /// <summary>
    /// Gets or sets the name of the master view to be loaded when
    /// control is in the ContentViewDisplayMode.Master
    /// </summary>
    [Category("Views")]
    public virtual string MasterViewName
    {
      get => this.masterViewName;
      set
      {
        if (this.masterViewName != value)
          this.masterViewDefinition = (IContentViewMasterDefinition) null;
        this.masterViewName = value;
      }
    }

    /// <summary>
    /// Gets or sets the name of the detail view to be loaded when
    /// control is in the ContentViewDisplayMode.Detail
    /// </summary>
    [Category("Views")]
    public virtual string DetailViewName
    {
      get => this.detailViewName;
      set
      {
        if (this.detailViewName != value)
          this.detailViewDefinition = (IContentViewDetailDefinition) null;
        this.detailViewName = value;
      }
    }

    /// <summary>
    /// Gets the definition for the master view specified through the
    /// MasterViewName property.
    /// </summary>
    public virtual IContentViewMasterDefinition MasterViewDefinition
    {
      get
      {
        if (this.masterViewDefinition == null)
        {
          IContentViewDefinition view;
          if (this.MasterViewName != null && this.ControlDefinition.TryGetView(this.MasterViewName, out view))
            this.masterViewDefinition = view as IContentViewMasterDefinition;
          if (this.masterViewDefinition == null)
            this.masterViewDefinition = (IContentViewMasterDefinition) new ContentViewMasterDefinition();
        }
        return this.masterViewDefinition;
      }
    }

    /// <summary>
    /// Gets the definition for the detail view specified through the
    /// DetailViewName property.
    /// </summary>
    public virtual IContentViewDetailDefinition DetailViewDefinition
    {
      get
      {
        if (this.detailViewDefinition == null)
        {
          IContentViewDefinition view;
          if (this.ControlDefinition.TryGetView(this.DetailViewName, out view))
            this.detailViewDefinition = view as IContentViewDetailDefinition;
          if (this.detailViewDefinition == null)
            this.detailViewDefinition = (IContentViewDetailDefinition) new ContentViewDetailDefinition();
        }
        return this.detailViewDefinition;
      }
    }

    /// <summary>
    /// Gets or sets the detail item. This is the content item that is shown when the ContentView is in detail mode
    /// The item is initialized in the ResolveDetailItem method
    /// </summary>
    /// <value>The detail item.</value>
    public virtual IDataItem DetailItem { get; set; }

    /// <summary>Gets or sets the LayoutTemplatePath</summary>
    /// <value>The LayoutTemplatePath.</value>
    public string LayoutTemplatePath { get; set; }

    /// <summary>Gets or sets the ViewControl</summary>
    /// <value>The ViewControl.</value>
    public Control ViewControl { get; set; }

    /// <summary>
    /// Gets or sets the property name used to build the filter expression when evaluating URLs by date. The default value is EventStart.
    /// </summary>
    /// <value>The property name used to build the filter expression when evaluating URLs by date.</value>
    public virtual string DateEvaluatorPropertyName { get; set; }

    /// <summary>Gets or sets the page title modes.</summary>
    /// <value>The page title modes.</value>
    public ContentView.PageTitleModes PageTitleMode { get; set; }

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

    /// <summary>
    /// Gets the FullName of the ViewType.
    /// It is included in the propertyBag and can be used in designers to get the mapping
    /// between the designer and the control that is designed.
    /// </summary>
    public Type ViewType => this.GetType();

    /// <summary>
    /// Gets or sets the access key that allows you to quickly navigate to the Web server control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The access key for quick navigation to the Web server control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// The specified access key is neither null, <see cref="F:System.String.Empty" /> nor a single character string.
    /// </exception>
    [Browsable(false)]
    public override string AccessKey
    {
      get => base.AccessKey;
      set => base.AccessKey = value;
    }

    /// <summary>
    /// Gets or sets the background color of the Web server control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.
    /// </returns>
    [Browsable(false)]
    public override Color BackColor
    {
      get => base.BackColor;
      set => base.BackColor = value;
    }

    /// <summary>Gets or sets the border color of the Web control.</summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that represents the border color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.
    /// </returns>
    [Browsable(false)]
    public override Color BorderColor
    {
      get => base.BorderColor;
      set => base.BorderColor = value;
    }

    /// <summary>
    /// Gets or sets the border style of the Web server control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values. The default is NotSet.
    /// </returns>
    [Browsable(false)]
    public override BorderStyle BorderStyle
    {
      get => base.BorderStyle;
      set => base.BorderStyle = value;
    }

    /// <summary>
    /// Gets or sets the border width of the Web server control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width of a Web server control. The default value is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    /// The specified border width is a negative value.
    /// </exception>
    [Browsable(false)]
    public override Unit BorderWidth
    {
      get => base.BorderWidth;
      set => base.BorderWidth = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Web server control is enabled.
    /// </summary>
    /// <value></value>
    /// <returns>true if control is enabled; otherwise, false. The default is true.</returns>
    [Browsable(false)]
    public override bool Enabled
    {
      get => base.Enabled;
      set => base.Enabled = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether themes apply to this control.
    /// </summary>
    /// <value></value>
    /// <returns>true to use themes; otherwise, false. The default is false.</returns>
    [Browsable(false)]
    public override bool EnableTheming
    {
      get => base.EnableTheming;
      set => base.EnableTheming = value;
    }

    /// <summary>
    /// Gets the font properties associated with the Web server control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that represents the font properties of the Web server control.
    /// </returns>
    [Browsable(false)]
    public override FontInfo Font => base.Font;

    /// <summary>
    /// Gets or sets the foreground color (typically the color of the text) of the Web server control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />.
    /// </returns>
    [Browsable(false)]
    public override Color ForeColor
    {
      get => base.ForeColor;
      set => base.ForeColor = value;
    }

    /// <summary>Gets or sets the height of the Web server control.</summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the height of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    /// The height was set to a negative value.
    /// </exception>
    [Browsable(false)]
    public override Unit Height
    {
      get => base.Height;
      set => base.Height = value;
    }

    /// <summary>Gets or sets the skin to apply to the control.</summary>
    /// <value></value>
    /// <returns>
    /// The name of the skin to apply to the control. The default is <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    /// The skin specified in the <see cref="P:System.Web.UI.WebControls.WebControl.SkinID" /> property does not exist in the theme.
    /// </exception>
    [Browsable(false)]
    public override string SkinID
    {
      get => base.SkinID;
      set => base.SkinID = value;
    }

    /// <summary>Gets or sets the tab index of the Web server control.</summary>
    /// <value></value>
    /// <returns>
    /// The tab index of the Web server control. The default is 0, which indicates that this property is not set.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// The specified tab index is not between -32768 and 32767.
    /// </exception>
    [Browsable(false)]
    public override short TabIndex
    {
      get => base.TabIndex;
      set => base.TabIndex = value;
    }

    /// <summary>
    /// Gets or sets the text displayed when the mouse pointer hovers over the Web server control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The text displayed when the mouse pointer hovers over the Web server control. The default is <see cref="F:System.String.Empty" />.
    /// </returns>
    [Browsable(false)]
    public override string ToolTip
    {
      get => base.ToolTip;
      set => base.ToolTip = value;
    }

    /// <summary>Gets or sets the width of the Web server control.</summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the width of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    /// The width of the Web server control was set to a negative value.
    /// </exception>
    [Browsable(false)]
    public override Unit Width
    {
      get => base.Width;
      set => base.Width = value;
    }

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

    /// <summary>
    /// Gets or sets a value indicating whether the server control persists its view state, and the view state of any child controls it contains, to the requesting client.
    /// </summary>
    /// <value></value>
    /// <returns>true if the server control maintains its view state; otherwise false. The default is true.</returns>
    [DefaultValue(false)]
    public override bool EnableViewState
    {
      get => this.enableViewState;
      set => this.enableViewState = value;
    }

    public bool? SetPaginationUrls { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the current view is in a single item mode.
    /// </summary>
    private bool IsSpecificItemOnly { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the module is enabled.
    /// </summary>
    private bool IsModuleEnabledForCurrentSite { get; set; }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      this.AddAttributesToRender(writer);
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      base.RenderEndTag(writer);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.IsModuleEnabledForCurrentSite = true;
      if (string.IsNullOrEmpty(this.ModuleName))
        return;
      SystemManager.GetApplicationModule(this.ModuleName);
      if (string.IsNullOrEmpty(this.RelatedDataDefinition.RelatedItemType))
        return;
      if (this.ControlDefinition.ProviderName == "sf-site-default-provider" && this.ControlDefinition.ContentType != (Type) null)
        this.ControlDefinition.ProviderName = RelatedDataHelper.ResolveProvider(this.ControlDefinition.ContentType.FullName);
      this.IsModuleEnabledForCurrentSite = RelatedDataHelper.IsModuleEnabledForCurrentSite(this.ControlDefinition.ContentType, this.ControlDefinition.ProviderName);
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based
    /// implementation to create any child controls they contain in preparation for posting back
    /// or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      if (!this.Visible)
        return;
      if (this.IsModuleEnabledForCurrentSite)
      {
        this.ResolveDetailItem();
        string currentViewName = this.DetermineCurrentViewName();
        this.ResolvePageInfo();
        this.LoadView(currentViewName);
        this.SubscribeCacheDependency();
      }
      else
      {
        if (!this.IsDesignMode())
          return;
        this.Controls.Add(this.GetModuleErrorMessageControl());
      }
    }

    private void ResolvePageInfo()
    {
      if (this.DisableModifyPageInfo || this.DetailItem == null || this.Page == null || this.IsNestedContentLocatableView() || this.Page.Items[(object) "content_view_page_info_resolved"] != null && (bool) this.Page.Items[(object) "content_view_page_info_resolved"] || this.IsSpecificItemOnly && !Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().ContentLocationsSettings.EnableSingleItemModeWidgetsBackwardCompatibilityMode)
        return;
      this.ResolvePageTitle();
      this.ResolvePageMetaTags();
      this.AddCanonicalUrlTagIfEnabled(this.Page, this.DetailItem);
      this.Page.RegisterBreadcrumbExtender((IBreadcrumExtender) this);
      this.Page.Items[(object) "content_view_page_info_resolved"] = (object) true;
    }

    /// <summary>
    /// Tries to set the page title to the content title.
    /// Depends on ContentViewDisplayMode property.
    /// </summary>
    protected virtual void ResolvePageTitle()
    {
      if (this.DetailItem == null || this.Page == null || this.PageTitleMode == ContentView.PageTitleModes.DoNotSet || !(this.DetailItem is IContent detailItem) || this.ContentViewDisplayMode != ContentViewDisplayMode.Detail)
        return;
      string s = detailItem.Title.Value;
      if (!string.IsNullOrEmpty(this.MetaTitleField))
      {
        string metaValue = this.GetMetaValue((object) this.DetailItem, this.MetaTitleField);
        if (!string.IsNullOrEmpty(metaValue))
          s = metaValue;
      }
      string str = HttpUtility.HtmlEncode(s);
      switch (this.PageTitleMode)
      {
        case ContentView.PageTitleModes.Replace:
          if (detailItem.Title != (Lstring) null)
          {
            this.Page.Title = str;
            break;
          }
          this.Page.Title = "";
          break;
        case ContentView.PageTitleModes.Append:
          if (!(detailItem.Title != (Lstring) null))
            break;
          this.Page.Title = string.Format("{0} {1}", (object) this.Page.Title, (object) str);
          break;
      }
    }

    /// <summary>
    /// Resolves and sets the page meta tags (keywords , description)
    /// </summary>
    /// <summary>
    /// Resolves and sets the page meta tags (keywords , description)
    /// </summary>
    protected virtual void ResolvePageMetaTags()
    {
      if (this.DetailItem == null || this.Page == null)
        return;
      if (!string.IsNullOrEmpty(this.MetaKeywordsField))
      {
        string metaValue = this.GetMetaValue((object) this.DetailItem, this.MetaKeywordsField);
        if (!string.IsNullOrEmpty(metaValue))
          this.Page.MetaKeywords = metaValue;
      }
      if (string.IsNullOrEmpty(this.MetaDescriptionField))
        return;
      string metaValue1 = this.GetMetaValue((object) this.DetailItem, this.MetaDescriptionField);
      if (string.IsNullOrEmpty(metaValue1))
        return;
      this.Page.MetaDescription = metaValue1;
    }

    private string GetMetaValue(object detailItem, string fieldName)
    {
      PropertyDescriptor property1 = TypeDescriptor.GetProperties(detailItem)[fieldName];
      if (property1 == null)
        return (string) null;
      if (property1 is TaxonomyPropertyDescriptor property2)
        return property2.GetTaxaText(detailItem);
      return property1.GetValue(detailItem)?.ToString();
    }

    IEnumerable<SiteMapNode> IBreadcrumExtender.GetVirtualNodes(
      SiteMapProvider provider)
    {
      List<SiteMapNode> virtualNodes = new List<SiteMapNode>();
      if (this.DetailItem is IContent detailItem)
      {
        Lstring title = detailItem.Title;
        if (!string.IsNullOrEmpty((string) title))
        {
          SiteMapNode siteMapNode = new SiteMapNode(provider, detailItem.Id.ToString(), string.Empty, (string) title, (string) detailItem.Description);
          virtualNodes.Add(siteMapNode);
        }
      }
      return (IEnumerable<SiteMapNode>) virtualNodes;
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
      Type contentType = this.ControlDefinition.ContentType;
      if (!(contentType != (Type) null))
        return;
      IContentManager contentManager = this.ResolveContentManager();
      string providerName = contentManager == null || contentManager.Provider == null ? string.Empty : contentManager.Provider.Name;
      PageRouteHandler.RegisterContentListCacheVariation(contentType, providerName);
    }

    /// <summary>
    /// Resolves the DetailItem from URL or Definition. If the item Url requires redirection, this method will redirect the request
    /// This method is executed just for automatic and detail mode
    /// </summary>
    protected virtual void ResolveDetailItem()
    {
      if (this.ContentViewDisplayMode != ContentViewDisplayMode.Automatic && this.ContentViewDisplayMode != ContentViewDisplayMode.Detail)
        return;
      if (this.DisplayRelatedData())
      {
        if (this.ContentViewDisplayMode != ContentViewDisplayMode.Detail)
          return;
        this.DetailItem = (IDataItem) (this.GetRelatedItem() as IContent);
        this.IsSpecificItemOnly = true;
      }
      else
      {
        IContentManager contentManager = this.ResolveContentManager();
        if (!this.isControlDefinitionProviderCorrect)
          return;
        if (this.ContentViewDisplayMode == ContentViewDisplayMode.Detail)
        {
          if (this.DetailViewDefinition.DataItemId != Guid.Empty)
          {
            try
            {
              IContent content = contentManager.GetItem(this.ControlDefinition.ContentType, this.DetailViewDefinition.DataItemId) as IContent;
              if (content is Telerik.Sitefinity.GenericContent.Model.Content lifecycleItem)
              {
                if (lifecycleItem.SupportsContentLifecycle)
                {
                  object resultItem;
                  if (this.TryGetItemWithRequestedStatus(lifecycleItem as ILifecycleDataItem, contentManager as ILifecycleManager, out resultItem))
                    this.DetailItem = resultItem as IDataItem;
                  else if (lifecycleItem.Visible && lifecycleItem.Status == ContentLifecycleStatus.Live)
                    this.DetailItem = (IDataItem) lifecycleItem;
                }
                else
                  this.DetailItem = (IDataItem) lifecycleItem;
              }
              else
                this.DetailItem = (IDataItem) content;
              this.IsSpecificItemOnly = true;
            }
            catch (Exception ex)
            {
              if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                return;
              throw;
            }
          }
          else
          {
            try
            {
              this.ResolveDetailItemFromUrl();
            }
            catch (UnauthorizedAccessException ex)
            {
              this.ThrowNotAuthorisedException(this.ControlDefinition.ContentType.Name);
            }
          }
        }
        else
        {
          if (!(this.ControlDefinition.ContentType != (Type) null))
            return;
          if (!(this.ControlDefinition.ContentType.GetInterface(typeof (ILocatable).FullName) != (Type) null))
            return;
          try
          {
            this.ResolveDetailItemFromUrl();
          }
          catch (UnauthorizedAccessException ex)
          {
            this.ThrowNotAuthorisedException(this.ControlDefinition.ContentType.Name);
          }
        }
      }
    }

    protected void ThrowNotAuthorisedException(string eventOrigin)
    {
      PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      RedirectStrategyType redirectStrategy = RedirectStrategyType.None;
      string empty = string.Empty;
      if (actualCurrentNode != null)
      {
        string frontEndLogin = RouteHelper.GetFrontEndLogin(currentHttpContext, out redirectStrategy, actualCurrentNode.Provider);
        this.RaiseUnauthorizedPageAccessEvent(currentHttpContext, actualCurrentNode, redirectStrategy, frontEndLogin, eventOrigin);
      }
      SystemManager.CurrentHttpContext.Response.StatusCode = !SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated ? 401 : 403;
      SystemManager.CurrentHttpContext.Response.End();
    }

    private void RaiseUnauthorizedPageAccessEvent(
      HttpContextBase httpContext,
      PageSiteNode node,
      RedirectStrategyType redirectStrategy,
      string redirectUrl,
      string eventOrigin)
    {
      EventHub.Raise((IEvent) new UnauthorizedPageAccessEvent(httpContext, node, redirectStrategy, eventOrigin, redirectUrl));
    }

    /// <summary>Resolves the detail item from URL.</summary>
    protected internal virtual void ResolveDetailItemFromUrl()
    {
      string itemUrl = this.GetUrlParameterString(true);
      if (itemUrl == null)
        return;
      if (itemUrl.StartsWith("/Action/Edit") || itemUrl.StartsWith("/Action/Preview") || this.ControlDefinition.ContentType == typeof (Comment))
        return;
      IManager manager = this.ResolveManager();
      NameValueCollection stringParameters = this.GetQueryStringParameters();
      string g = (string) null;
      if (stringParameters != null)
        g = stringParameters["sf-itemId"];
      IDataItem detailItem;
      string redirectUrl;
      if (g != null)
      {
        Guid itemId = new Guid(g);
        detailItem = this.GetItemById(manager, itemId, out redirectUrl);
      }
      else
      {
        detailItem = this.GetItemFromUrl(manager, itemUrl, out redirectUrl);
        if (detailItem == null && !this.IsSpecificItemOnly)
        {
          ILifecycleDataItem resolvedItem = (ILifecycleDataItem) null;
          if (this.TryGetFallbackItem((Func<ILifecycleDataItem>) (() => this.GetItemFromUrl(manager, itemUrl, out redirectUrl) as ILifecycleDataItem), out resolvedItem))
            detailItem = (IDataItem) resolvedItem;
        }
      }
      if (!string.IsNullOrEmpty(redirectUrl))
        this.Redirect(redirectUrl);
      if (detailItem == null || !this.MatchesParent(detailItem))
        return;
      string urlParameterString = this.GetUrlParameterString(false);
      if (string.IsNullOrEmpty(urlParameterString))
        return;
      MatchCollection matchCollection = Regex.Matches(urlParameterString, "/!(?<urlPrefix>\\w+)/.*");
      if ((matchCollection.Count != 1 || !(matchCollection[0].Groups["urlPrefix"].Value == this.UrlKeyPrefix)) && (matchCollection.Count != 0 || !string.IsNullOrEmpty(this.UrlKeyPrefix)))
        return;
      SystemManager.CurrentHttpContext.Items[(object) "detailItem"] = (object) detailItem;
      this.DetailItem = detailItem;
      RouteHelper.SetUrlParametersResolved();
    }

    private bool MatchesParent(IDataItem detailItem)
    {
      if (!(detailItem is IHasParent hasParent) || this.MasterViewDefinition == null || !(this.MasterViewDefinition.ItemsParentId != Guid.Empty) || hasParent.Parent.Id == this.MasterViewDefinition.ItemsParentId)
        return true;
      if (detailItem is IFolderItem folderItem && folderItem.FolderId.HasValue)
      {
        Guid? folderId = folderItem.FolderId;
        Guid itemsParentId = this.MasterViewDefinition.ItemsParentId;
        if ((folderId.HasValue ? (folderId.HasValue ? (folderId.GetValueOrDefault() == itemsParentId ? 1 : 0) : 1) : 0) != 0)
          return true;
      }
      return false;
    }

    /// <summary>Resolves the content manager.</summary>
    /// <returns></returns>
    public virtual IContentManager ResolveContentManager(string transactionName = null)
    {
      if (this.ControlDefinition.ManagerType != (Type) null)
        return string.IsNullOrEmpty(transactionName) ? ManagerBase.GetManager(this.ControlDefinition.ManagerType.FullName, this.ControlDefinition.ProviderName) as IContentManager : ManagerBase.GetManagerInTransaction(this.ControlDefinition.ManagerType, this.ControlDefinition.ProviderName, transactionName) as IContentManager;
      IContentManager contentManager = (IContentManager) null;
      if (typeof (IContent).IsAssignableFrom(this.ControlDefinition.ContentType))
      {
        try
        {
          contentManager = !string.IsNullOrEmpty(transactionName) ? ManagerBase.GetMappedManagerInTransaction(this.ControlDefinition.ContentType, this.ControlDefinition.ProviderName, transactionName) as IContentManager : ManagerBase.GetMappedManager(this.ControlDefinition.ContentType, this.ControlDefinition.ProviderName) as IContentManager;
        }
        catch (Exception ex)
        {
          this.isControlDefinitionProviderCorrect = false;
        }
      }
      return contentManager;
    }

    /// <summary>
    /// Resolves the manager specified in the control definition.
    /// </summary>
    internal virtual IManager ResolveManager()
    {
      if (this.ControlDefinition.ManagerType != (Type) null)
        return ManagerBase.GetManager(this.ControlDefinition.ManagerType.FullName, this.ControlDefinition.ProviderName);
      IManager manager;
      return ManagerBase.TryGetMappedManager(this.ControlDefinition.ContentType, this.ControlDefinition.ProviderName, out manager) ? manager : (IManager) null;
    }

    /// <summary>
    /// Determines the name of the current view name depending on the other settings of the control.
    /// </summary>
    /// <remarks>
    /// The logic of this method goes as follows:
    /// 1) if ContentViewDisplayMode is Master, gets the name of MasterViewName
    /// 1) a) if MasterViewName is set returns MasterViewName
    /// 1) b) if MasterViewName is NOT set, returns the name of the first view from Views collection that
    ///       implements IContentViewMasterDefinition
    /// 1) c) if no such view as described in 1) b) throws an exception
    /// 2) if ContentViewDisplayMode is Detail, gets the name of DetailViewName
    /// 2) a) if DetailViewName is set returns DetailViewName
    /// 2) b) if DetailViewName is NOT set, returns the name of the first view from Views collection that
    ///       implement IContentViewDetailDefinition
    /// 2) c) if no such view as described in 2) b) throws an exception
    /// 3) if ContentViewDisplayMode is Automatic, gets the first url parameter and treats is as a view name
    /// 3) a) if view from 3) exists it returns that view name and also sets the appropriate ContentViewDisplayMode property
    /// 3) b) if view from 3) does not exist it switches to ContentViewDisplayMode MASTER and goes to 1
    /// </remarks>
    /// <returns>Name of the current view (view to be loaded).</returns>
    protected internal virtual string DetermineCurrentViewName()
    {
      if (this.ContentViewDisplayMode == ContentViewDisplayMode.Automatic)
      {
        string viewNameFromUrl = this.GetViewNameFromUrl();
        if (!string.IsNullOrEmpty(viewNameFromUrl))
        {
          string viewName = this.ValidateViewIsFromControlDefinition(viewNameFromUrl);
          IContentViewDefinition view = (IContentViewDefinition) null;
          if (!this.ControlDefinition.TryGetView(viewName, out view))
            throw new ArgumentOutOfRangeException("The view specified in the url cannot be found in the Views collection of the current control.");
          this.ContentViewDisplayMode = view.IsMasterView ? ContentViewDisplayMode.Master : ContentViewDisplayMode.Detail;
          return viewName;
        }
      }
      if (this.ContentViewDisplayMode == ContentViewDisplayMode.Automatic)
        this.ContentViewDisplayMode = this.DetailItem == null ? ContentViewDisplayMode.Master : ContentViewDisplayMode.Detail;
      if (this.ContentViewDisplayMode == ContentViewDisplayMode.Master)
      {
        if (!string.IsNullOrEmpty(this.MasterViewName))
          return this.MasterViewName;
        return (this.ControlDefinition.GetDefaultMasterView() ?? throw new InvalidOperationException("ContentView control is in Master mode and no MasterViews exist in the Views collection \r\n            defined by this control.")).ViewName;
      }
      if (!string.IsNullOrEmpty(this.DetailViewName))
        return this.DetailViewName;
      return (this.ControlDefinition.GetDefaultDetailView() ?? throw new InvalidOperationException("ContentView control is in Detail mode and no DetailViews exist in the Views collection \r\n            defined by this control.")).ViewName;
    }

    /// <summary>
    /// Verifies that ControlDefinition contains view with the provider viewName
    /// </summary>
    /// <param name="viewName">The name of the view for witch to check the control definition</param>
    protected internal virtual string ValidateViewIsFromControlDefinition(string viewName) => this.ControlDefinition.ContainsView(viewName) ? viewName : throw new ArgumentOutOfRangeException("The view specified in the url cannot be found in the Views collection of the current control.");

    /// <summary>Gets the name of the view from the url parameters</summary>
    /// <returns>Name of the view if found; otherwise null</returns>
    protected internal virtual string GetViewNameFromUrl() => SystemManager.CurrentHttpContext.Request.QueryString[this.ViewNameUrlKey];

    /// <summary>
    /// Loads the view specified through the name of the view.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
    protected internal virtual void LoadView(string viewName)
    {
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      if (!this.ControlDefinition.TryGetView(viewName, out this.currentView))
        throw new ArgumentOutOfRangeException(nameof (viewName));
      Control control = (Control) null;
      if (!string.IsNullOrEmpty(this.currentView.ViewVirtualPath))
        control = this.LoadUserControl(this.currentView.ViewVirtualPath);
      else if (this.currentView.ViewType != (Type) null)
        control = this.LoadControl(this.currentView.ViewType);
      if (control == null)
        return;
      string name = string.IsNullOrEmpty(this.currentView.ControlId) ? this.currentView.ViewName : this.currentView.ControlId;
      if (!string.IsNullOrEmpty(name))
        ControlUtilities.SetControlIdFromName(name, control);
      if (control.GetIndexRenderMode() == IndexRenderModes.Normal)
      {
        if (control is IViewControl viewControl)
        {
          viewControl.Host = this;
          viewControl.Definition = this.currentView;
          if (!string.IsNullOrEmpty(this.LayoutTemplatePath))
            viewControl.LayoutTemplatePath = this.LayoutTemplatePath;
        }
        if (control is MasterViewBase masterViewBase)
          masterViewBase.DataSource = this.DataSource;
        if (control is DownloadMasterViewBase downloadMasterViewBase)
          downloadMasterViewBase.DataSource = this.DataSource;
        this.Controls.Add(control);
        if (viewControl != null)
          this.IsEmpty = viewControl.IsEmptyView;
        this.ViewControl = control;
        this.ViewControl.EnableViewState = this.EnableViewState;
        this.ConfigureViewControl(control);
      }
      else
        this.ViewControl = (Control) new Literal();
    }

    /// <summary>Configures the current view control.</summary>
    /// <param name="control">The current view control.</param>
    protected virtual void ConfigureViewControl(Control control)
    {
    }

    /// <summary>
    /// Loads the control from the virtual path (user control).
    /// </summary>
    /// <param name="virtualPath">Virtual path of the control to be loaded.</param>
    /// <returns>An instance of the control.</returns>
    internal virtual Control LoadUserControl(string virtualPath) => (Control) ControlUtilities.LoadControl(virtualPath);

    /// <summary>
    /// Performs permanent redirect to the specified URL and ends the request.
    /// </summary>
    /// <param name="url">The URL to redirect to.</param>
    protected internal virtual void Redirect(string url)
    {
      if (string.IsNullOrEmpty(url))
        throw new ArgumentNullException(nameof (url));
      if (this.Page == null || this.Page.Response == null)
        return;
      url = VirtualPathUtility.RemoveTrailingSlash((SiteMapBase.GetCurrentProvider().CurrentNode as PageSiteNode).UrlWithoutExtension) + url;
      this.Page.Response.RedirectPermanent(UrlPath.ResolveUrl(url), true);
    }

    /// <summary>Loads the control from the type.</summary>
    /// <param name="type">Type of the control that should be loaded.</param>
    /// <returns>An instance of the control.</returns>
    internal virtual Control LoadControl(Type type) => this.Page.LoadControl(type, (object[]) null);

    /// <summary>
    /// Returns the selected single content item or null if in Master mode
    /// </summary>
    /// <returns></returns>
    public IContent GetSelectedContent() => (IContent) null;

    /// <summary>
    /// Gets the taxonomy filters being applied using the TaxonomyEvaluator
    /// logic for constructing a List of <see cref="!:TaxnomyFilterInfo" /> objects.
    /// </summary>
    public List<TaxonomyFilterInfo> GetTaxonomyFilters()
    {
      TaxonomyEvaluator taxonomyEvaluator = this.GetConfiguredTaxonomyEvaluator();
      string taxonomyName = (string) null;
      string taxonName = (string) null;
      string filteringUrl = this.GetFilteringUrl(string.Empty);
      if (string.IsNullOrWhiteSpace(filteringUrl))
        return (List<TaxonomyFilterInfo>) null;
      taxonomyEvaluator.ParseTaxonomyParams(this.UrlEvaluationMode, filteringUrl, this.UrlKeyPrefix, out taxonName, out taxonomyName);
      if (string.IsNullOrWhiteSpace(taxonName) || string.IsNullOrWhiteSpace(taxonomyName))
        return (List<TaxonomyFilterInfo>) null;
      Taxon taxonByName = taxonomyEvaluator.GetTaxonByName(taxonomyName, taxonName);
      return taxonByName == null ? (List<TaxonomyFilterInfo>) null : this.ConstructTaxonomyFilters(taxonByName);
    }

    /// <summary>
    /// Gets URL for displaying a specific view in the current page.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns></returns>
    public string GetViewUrl(string viewName)
    {
      Url url = new Url(SystemManager.CurrentHttpContext.Request.RawUrl);
      if (viewName != null)
        url.Query[this.ViewNameUrlKey] = viewName;
      else if (url.Query.ContainsKey(this.ViewNameUrlKey))
        url.Query.Remove(this.ViewNameUrlKey);
      return url.ToString();
    }

    protected internal virtual IDataItem GetItemFromUrl(
      IManager manager,
      string itemUrl,
      out string redirectUrl)
    {
      IDataItem lifecycleItem;
      if (manager is IContentManager)
      {
        bool published = !this.IsPreviewRequested() || this.GetRequestedItemStatus() == ContentLifecycleStatus.Live;
        lifecycleItem = ((IContentManager) manager).GetItemFromUrl(this.ControlDefinition.ContentType, itemUrl, published, out redirectUrl);
      }
      else
      {
        if (!(manager.Provider is IUrlProvider))
          throw new ArgumentException("The specified manager does not support GetItemFromUrl method.");
        lifecycleItem = ((IUrlProvider) manager.Provider).GetItemFromUrl(this.ControlDefinition.ContentType, itemUrl, out redirectUrl);
      }
      object resultItem;
      if (this.TryGetItemWithRequestedStatus(lifecycleItem as ILifecycleDataItem, manager as ILifecycleManager, out resultItem))
        lifecycleItem = resultItem as IDataItem;
      return lifecycleItem;
    }

    protected internal virtual IDataItem GetItemById(
      IManager manager,
      Guid itemId,
      out string redirectUrl)
    {
      IDataItem itemById = manager.GetItem(this.ControlDefinition.ContentType, itemId) as IDataItem;
      redirectUrl = (string) null;
      return itemById;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is empty.
    /// </summary>
    /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
    public bool IsEmpty { get; protected set; }

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public virtual string EmptyLinkText => Res.Get<Labels>().Edit;

    /// <summary>
    /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <returns></returns>
    /// <value>A collection of  instances of type <see cref="!:CacheDependencyNotifiedObject" />.</value>
    public virtual IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      List<CacheDependencyKey> cacheDependencyNotifiedObjects = new List<CacheDependencyKey>();
      Type contentType = this.ControlDefinition.ContentType;
      if (this.DetailItem != null && this.DetailItem.Id != Guid.Empty)
      {
        cacheDependencyNotifiedObjects.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(contentType, this.DetailItem.Id));
      }
      else
      {
        IContentManager contentManager = this.ResolveContentManager();
        string appName = contentManager == null || contentManager.Provider == null ? string.Empty : contentManager.Provider.ApplicationName;
        cacheDependencyNotifiedObjects.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(contentType, appName));
      }
      if (contentType.ImplementsInterface(typeof (ICommentable)))
        this.AddCachedItem(cacheDependencyNotifiedObjects, (string) null, typeof (IThread));
      return (IList<CacheDependencyKey>) cacheDependencyNotifiedObjects;
    }

    public virtual IEnumerable<IContentLocationInfo> GetLocations()
    {
      if (this.MasterViewDefinition.DetailsPageId != Guid.Empty)
        return (IEnumerable<IContentLocationInfo>) null;
      if (this.DisableModifyPageInfo)
        return (IEnumerable<IContentLocationInfo>) null;
      ContentViewControlDefinition controlDefinition = this.ControlDefinition;
      if (controlDefinition == null)
        return (IEnumerable<IContentLocationInfo>) null;
      ContentLocationInfo location = new ContentLocationInfo();
      Type contentType = controlDefinition.ContentType;
      location.ContentType = contentType;
      string str = this.ControlDefinition.ProviderName;
      IManager contentManager = (IManager) null;
      Type managerType;
      if (ManagerBase.TryGetMappedManagerType(contentType, out managerType))
        contentManager = ManagerBase.GetManager(managerType);
      if (str.IsNullOrEmpty() && contentManager != null)
        str = contentManager.Provider.Name;
      location.ProviderName = str;
      switch (this.ContentViewDisplayMode)
      {
        case ContentViewDisplayMode.Automatic:
          this.AddMasterViewFilters(location, this.MasterViewDefinition, contentType, contentManager);
          break;
        case ContentViewDisplayMode.Detail:
          this.AddDetailViewFilter(location, this.DetailViewDefinition, contentType, contentManager);
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
    public string GetProviderName() => this.ControlDefinition.ProviderName;

    /// <summary>Adds the cached item.</summary>
    /// <param name="cacheDependencyNotifiedObjects">The cache dependency notified objects.</param>
    /// <param name="key">The key.</param>
    /// <param name="type">The type.</param>
    protected void AddCachedItem(
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

    private TaxonomyEvaluator GetConfiguredTaxonomyEvaluator()
    {
      TaxonomyEvaluator taxonomyEvaluator = new TaxonomyEvaluator();
      taxonomyEvaluator.Initialize(Telerik.Sitefinity.Configuration.Config.Get<DataConfig>().UrlEvaluators["Taxonomy"].Parameters);
      return taxonomyEvaluator;
    }

    private string GetFilteringUrl(string url)
    {
      switch (this.UrlEvaluationMode)
      {
        case UrlEvaluationMode.QueryString:
          url = this.GetQueryString();
          break;
        default:
          url = this.GetUrlParameterString(false);
          break;
      }
      return url;
    }

    private List<TaxonomyFilterInfo> ConstructTaxonomyFilters(Taxon taxon) => new List<TaxonomyFilterInfo>(1)
    {
      new TaxonomyFilterInfo()
      {
        TaxonomyName = taxon.Taxonomy.Name,
        SingleTaxonName = (string) taxon.Taxonomy.TaxonName,
        Taxons = new List<TaxonInfo>()
        {
          new TaxonInfo(taxon)
        }
      }
    };

    /// <summary>
    /// Defines identifiers that indicate if and how the page title should be set.
    /// </summary>
    public enum PageTitleModes
    {
      /// <summary>
      /// Indicates that the page title should be replaced by the item title.
      /// </summary>
      Replace,
      /// <summary>Indicates that the page title should not be altered.</summary>
      DoNotSet,
      /// <summary>
      /// Indicates that the item title should be appended to the page title.
      /// </summary>
      Append,
      /// <summary>
      /// Indicates that the page title should be build by the item's title and its parent's title.
      /// </summary>
      Hierarchy,
    }
  }
}
