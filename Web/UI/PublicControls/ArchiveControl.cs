// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.ArchiveControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Archive;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>
  /// The archive control displays list of archived content items for a specified content type.
  /// </summary>
  [IndexRenderMode(IndexRenderModes.NoOutput)]
  public class ArchiveControl : SimpleView, ICustomWidgetVisualization, IHasCacheDependency
  {
    private Type contentType;
    private List<DateTime> dateRangeValues;
    private string dateEvaluatorPropertyName;
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.ArchiveControl.ascx");
    private const string selectedCssClass = "sfSel";

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the archive grouping value that will be used to group the content at archieve control.
    /// </summary>
    /// <value>The archive frequency.</value>
    public DateBuildOptions DateBuildOptions { get; set; }

    /// <summary>
    /// Gets or sets the type of the content that Archieve control is going to display.
    /// </summary>
    /// <value>The type of the content.</value>
    public virtual string ContentType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show item count for content items at archieve control.
    /// </summary>
    /// <value><c>true</c> if [show item count]; otherwise, <c>false</c>.</value>
    public bool ShowItemCount { get; set; }

    /// <summary>Gets or sets the title of the archieve control.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the base URL of the page that will be used to display content items.
    /// </summary>
    /// <value>The base URL.</value>
    public string BaseUrl { get; set; }

    /// <summary>Gets or sets the provider.</summary>
    /// <value>The provider.</value>
    public string Provider { get; set; }

    /// <summary>
    /// Gets or sets the URL key prefix. Used when building and evaluating URLs together with ContentView controls
    /// </summary>
    /// <value>The URL key prefix.</value>
    public string UrlKeyPrefix { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ArchiveControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the property name used to build the filter expression when evaluating URLs by date. The default value is PublicationDate.
    /// </summary>
    /// <value>The property name used to build the filter expression when evaluating URLs by date.</value>
    public virtual string DateEvaluatorPropertyName
    {
      get => string.IsNullOrEmpty(this.dateEvaluatorPropertyName) ? "PublicationDate" : this.dateEvaluatorPropertyName;
      set => this.dateEvaluatorPropertyName = value;
    }

    /// <summary>
    /// Gets the reference to the label that displays the title of the archieve field.
    /// </summary>
    protected internal virtual Literal TitleLabel => this.Container.GetControl<Literal>("titleLabel", false);

    /// <summary>Gets the repeater that will render content items.</summary>
    /// <value>The grid.</value>
    protected internal virtual Repeater ArchiveRepeater => this.Container.GetControl<Repeater>("rptArchive", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.RenderTitle();
      this.dateRangeValues = this.GetDateRangeValues();
      this.ConstructArchiveItems();
      this.SubscribeCacheDependency();
    }

    /// <summary>Subscribes the cache dependency.</summary>
    protected virtual void SubscribeCacheDependency()
    {
      if (this.IsBackend() || this.ContentType == null)
        return;
      IList<CacheDependencyKey> dependencyObjects = this.GetCacheDependencyObjects();
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "DataItemTypeCacheDependencyName"))
        SystemManager.CurrentHttpContext.Items.Add((object) "DataItemTypeCacheDependencyName", (object) new List<CacheDependencyKey>());
      ((List<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) "DataItemTypeCacheDependencyName"]).AddRange((IEnumerable<CacheDependencyKey>) dependencyObjects);
    }

    /// <summary>
    /// Resolves the display date text of each archieve group according to different grouping display options.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns></returns>
    public virtual string ResolveDisplayText(DateTime date)
    {
      string empty = string.Empty;
      switch (this.DateBuildOptions)
      {
        case DateBuildOptions.YearMonthDay:
          string monthName1 = SystemManager.CurrentContext.Culture.DateTimeFormat.GetMonthName(date.Month);
          return string.Format("{0} {1} {2}", (object) date.Year, (object) monthName1, (object) date.Day);
        case DateBuildOptions.YearMonth:
          string monthName2 = SystemManager.CurrentContext.Culture.DateTimeFormat.GetMonthName(date.Month);
          return string.Format("{0} {1}", (object) date.Year, (object) monthName2);
        case DateBuildOptions.Year:
          return string.Format("{0}", (object) date.Year);
        default:
          throw new NotImplementedException("This date build opition has not been implemented.");
      }
    }

    /// <summary>Constructs the archive items.</summary>
    public virtual void ConstructArchiveItems()
    {
      ContentManager manager = ContentManager.GetManager();
      if (this.ContentType == null)
        return;
      Type contentType = this.ResolveContentType();
      this.DataBindArchiveRepeater(manager.GetArchieveItems(contentType, this.Provider, this.DateBuildOptions, this.DateEvaluatorPropertyName));
    }

    /// <summary>Binds the <see cref="T:System.Web.UI.WebControls.Repeater" /> control
    /// and all its child controls to the specified data source.</summary>
    /// <param name="items">The list of items to be bound.</param>
    public virtual void DataBindArchiveRepeater(List<ArchiveItem> items)
    {
      if (this.ArchiveRepeater == null)
        return;
      this.ArchiveRepeater.DataSource = (object) items;
      this.ArchiveRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ArchiveRepeater_ItemDataBound);
      this.ArchiveRepeater.DataBind();
    }

    /// <summary>Gets the date range values.</summary>
    /// <returns></returns>
    public virtual List<DateTime> GetDateRangeValues()
    {
      IUrlEvaluator evaluator = UrlEvaluator.GetEvaluator("Date");
      List<DateTime> values = new List<DateTime>();
      if (!this.IsDesignMode())
      {
        string urlParameterString = this.GetUrlParameterString(false);
        if (!string.IsNullOrEmpty(urlParameterString))
          (evaluator as DateEvaluator).Evaluate(urlParameterString, this.DateEvaluatorPropertyName, this.GetUrlEvaluationMode(), this.UrlKeyPrefix, out values);
      }
      return values;
    }

    /// <summary>Resolves the URL.</summary>
    /// <param name="archieveItemDate">The archieve item date.</param>
    /// <returns></returns>
    protected virtual string ResolveUrl(DateTime archieveItemDate)
    {
      string virtualPath = this.BaseUrl;
      if (string.IsNullOrEmpty(virtualPath))
      {
        SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
        if (currentProvider == null || currentProvider != null && currentProvider.CurrentNode == null)
          return string.Empty;
        if (currentProvider.CurrentNode is PageSiteNode currentNode)
          virtualPath = currentNode.GetUrl(true, true);
      }
      if (string.IsNullOrEmpty(virtualPath))
        throw new ArgumentNullException("BaseUrl property could not be resolved.");
      if (VirtualPathUtility.IsAppRelative(virtualPath))
        virtualPath = VirtualPathUtility.ToAbsolute(virtualPath);
      string str = new DateEvaluator().BuildUrl(archieveItemDate, this.DateBuildOptions, this.GetUrlEvaluationMode(), this.UrlKeyPrefix);
      return virtualPath + str;
    }

    /// <summary>Renders the title.</summary>
    public virtual void RenderTitle()
    {
      PlaceHolder control = this.Container.GetControl<PlaceHolder>("plhTitle", false);
      if (!string.IsNullOrEmpty(this.Title))
      {
        control.Visible = true;
        this.TitleLabel.Text = this.Title;
      }
      else
        control.Visible = false;
    }

    /// <summary>
    /// Determines whether [is date in range] [the specified t].
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>
    /// 	<c>true</c> if [is date in range] [the specified t]; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsDateInRange(DateTime t)
    {
      bool flag = false;
      if (this.dateRangeValues.Count > 0)
      {
        DateTime dateRangeValue1 = this.dateRangeValues[0];
        DateTime dateRangeValue2 = this.dateRangeValues[1];
        flag = t.IsDateInRange(dateRangeValue1, dateRangeValue2);
      }
      return flag;
    }

    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>Indicates if the control is empty.</summary>
    /// <value></value>
    public bool IsEmpty => this.ArchiveRepeater.Items.Count == 0;

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public string EmptyLinkText => Res.Get<PublicControlsResources>().SetArchive;

    /// <summary>
    /// Data binds a repeater specified at template to an ArchieveItem list.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Repeater data binding event argument</param>
    protected virtual void ArchiveRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      ArchiveItem dataItem = e.Item.DataItem as ArchiveItem;
      if (!(e.Item.FindControl("linkArchive") is HyperLink control))
        return;
      if (!this.IsDesignMode())
      {
        control.NavigateUrl = this.ResolveUrl(dataItem.Date);
        if (this.IsDateInRange(dataItem.Date))
          control.CssClass = "sfSel";
      }
      control.Text = this.ResolveDisplayText(dataItem.Date);
      if (!this.ShowItemCount)
        return;
      control.Text += string.Format("  <span class='sfCount'>({0})</span>", (object) dataItem.ItemsCount);
    }

    /// <summary>Resolves the type of the content.</summary>
    /// <returns></returns>
    public virtual Type ResolveContentType()
    {
      if (this.contentType == (Type) null)
      {
        this.contentType = TypeResolutionService.ResolveType(this.ContentType);
        if (this.contentType == (Type) null)
          throw new ArgumentException(string.Format("Content type \"{0}\" cannot be resolved.", (object) this.ContentType));
      }
      return this.contentType;
    }

    /// <summary>
    /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <returns></returns>
    /// <value>An instance of type <see cref="!:CacheDependencyNotifiedObjects" />.</value>
    public virtual IList<CacheDependencyKey> GetCacheDependencyObjects() => (IList<CacheDependencyKey>) new List<CacheDependencyKey>()
    {
      new CacheDependencyKey()
      {
        Key = (string) null,
        Type = this.ResolveContentType()
      }
    };
  }
}
