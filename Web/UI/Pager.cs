// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Pager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Defines a pager control</summary>
  public class Pager : SimpleScriptView, IPostBackDataHandler
  {
    private string urlEvaluatorName = PageNumberEvaluator.Name;
    private string baseUrl;
    private int pageCount = -1;
    private string numericFormat = "{0}";
    private string prevGroupText = "...";
    private string nextGroupText = "...";
    private string pageNumberCssClass;
    private string currentPageNumberCssClass = "sf_PagerCurrent";
    private string prevGroupCssClass = "sf_PagerPrevGroup";
    private string nextGroupCssClass = "sf_PagerNextGroup";
    private string queryParamKey = "page";
    private Control numeric;
    private int currentPage = -1;
    private int displayCount = 10;
    private int pageSize = 10;
    private int virtualItemCount;
    private PagerNavigationModes navigationMode = PagerNavigationModes.Auto;
    private PagerNavigationModes actualNavigationMode = PagerNavigationModes.Auto;
    private UrlEvaluationMode? urlEvaluationMode;
    private bool setPaginationUrls;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Pager.ascx");
    internal const string scriptName = "Telerik.Sitefinity.Web.Scripts.Pager.js";

    /// <summary>Gets or sets the query param key.</summary>
    /// <value>The query param key.</value>
    public string QueryParamKey
    {
      get => this.queryParamKey;
      set => this.queryParamKey = value;
    }

    /// <summary>Gets or sets the virtual item count.</summary>
    /// <value>The virtual item count.</value>
    public int VirtualItemCount
    {
      get => this.virtualItemCount;
      set
      {
        this.virtualItemCount = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the size of the page.</summary>
    /// <value>The size of the page.</value>
    public int PageSize
    {
      get => this.pageSize;
      set => this.pageSize = value;
    }

    /// <summary>Gets or sets the maximum number of pages displayed.</summary>
    /// <value>The display count.</value>
    public int DisplayCount
    {
      get => this.displayCount;
      set => this.displayCount = value;
    }

    /// <summary>Gets or sets the navigation mode.</summary>
    /// <value>The navigation mode.</value>
    public PagerNavigationModes NavigationMode
    {
      get => this.navigationMode;
      set => this.navigationMode = value;
    }

    public string UrlEvaluatorName
    {
      get => this.urlEvaluatorName;
      set => this.urlEvaluatorName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the first and last links.
    /// </summary>
    /// <value><c>true</c> if [show first and last]; otherwise, <c>false</c>.</value>
    public bool ShowFirstAndLast { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the previous and next links.
    /// </summary>
    /// <value><c>true</c> if [show prev and next]; otherwise, <c>false</c>.</value>
    public bool ShowPrevAndNext { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to hide numeric pages container.
    /// </summary>
    /// <value><c>true</c> if hide; otherwise, <c>false</c>.</value>
    public bool HideNumeric { get; set; }

    /// <summary>Gets or sets the current page number</summary>
    [Browsable(false)]
    public int CurrentPage
    {
      get
      {
        if (this.currentPage == -1)
          this.currentPage = this.DetermineCurrentPage();
        return this.currentPage;
      }
      set
      {
        this.currentPage = value;
        this.ViewState[this.ClientID + "_CurrentPage"] = (object) this.currentPage;
      }
    }

    /// <summary>Gets or sets the numeric format.</summary>
    public string NumericFormat
    {
      get => this.numericFormat;
      set => this.numericFormat = value;
    }

    /// <summary>The CSS class of a page number.</summary>
    public string PageNumberCssClass
    {
      get => this.pageNumberCssClass;
      set => this.pageNumberCssClass = value;
    }

    /// <summary>The CSS class of the current page number.</summary>
    public string CurrentPageNumberCssClass
    {
      get => this.currentPageNumberCssClass;
      set => this.currentPageNumberCssClass = value;
    }

    /// <summary>
    /// The text of the link to the previous page number group.
    /// </summary>
    public string PrevGroupText
    {
      get => this.prevGroupText;
      set => this.prevGroupText = value;
    }

    /// <summary>
    /// The CSS class of the link to the previous page number group.
    /// </summary>
    public string PrevGroupCssClass
    {
      get => this.prevGroupCssClass;
      set => this.prevGroupCssClass = value;
    }

    /// <summary>The text of the link to the next page number group.</summary>
    public string NextGroupText
    {
      get => this.nextGroupText;
      set => this.nextGroupText = value;
    }

    /// <summary>
    /// The CSS class of the link to the next page number group.
    /// </summary>
    public string NextGroupCssClass
    {
      get => this.nextGroupCssClass;
      set => this.nextGroupCssClass = value;
    }

    /// <summary>Gets or sets the client page changed.</summary>
    /// <value>The client page changed.</value>
    [Category("Behavior")]
    public string ClientPageChanged { get; set; }

    /// <summary>
    /// Gets or sets the base URL of the page that will be used to display content items.
    /// </summary>
    /// <value>The base URL.</value>
    public string BaseUrl
    {
      get => this.GetBaseUrl();
      set => this.baseUrl = value;
    }

    /// <summary>Gets or sets the URL evaluation mode for the pager.</summary>
    /// <value>The URL evaluation mode.</value>
    internal UrlEvaluationMode UrlEvaluationMode
    {
      get
      {
        if (!this.urlEvaluationMode.HasValue)
          this.urlEvaluationMode = new UrlEvaluationMode?(this.GetUrlEvaluationMode());
        return this.urlEvaluationMode.Value;
      }
      set => this.urlEvaluationMode = new UrlEvaluationMode?(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether to apply the pagination URLs
    /// to the header of the page. This property must be enabled in the parent <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentView" />.
    /// </summary>
    internal bool SetPaginationUrls
    {
      get => this.setPaginationUrls;
      set => this.setPaginationUrls = value;
    }

    /// <summary>
    /// Occurs when the user navigates to a page on this control
    /// </summary>
    public event EventHandler PageChanged;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e) => base.OnInit(e);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
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

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer) => base.Render(writer);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.InitPager();
      if (this.ActualNavigationMode != PagerNavigationModes.ClientSide && this.VirtualItemCount <= this.PageSize)
        this.Visible = false;
      if (!this.setPaginationUrls)
        return;
      this.AddPaginationUrls();
    }

    /// <summary>Inits the pager.</summary>
    protected virtual void InitPager()
    {
      if (this.QueryParamKey == null)
        this.QueryParamKey = string.Empty;
      HyperLink control1 = this.Container.GetControl<HyperLink>("cmdPrev", this.ShowPrevAndNext);
      if (control1 != null)
      {
        if (this.ShowPrevAndNext && this.CurrentPage > 1)
          this.SetNavigationCommand(control1, "'Prev'");
        else
          control1.Visible = false;
      }
      HyperLink control2 = this.Container.GetControl<HyperLink>("cmdNext", this.ShowPrevAndNext);
      if (control2 != null)
      {
        if (this.ShowPrevAndNext && this.CurrentPage < this.PageCount)
          this.SetNavigationCommand(control2, "'Next'");
        else
          control2.Visible = false;
      }
      HyperLink control3 = this.Container.GetControl<HyperLink>("cmdFirst", this.ShowFirstAndLast);
      if (control3 != null)
      {
        if (this.ShowFirstAndLast)
          this.SetNavigationCommand(control3, "'First'");
        else
          control3.Visible = false;
      }
      HyperLink control4 = this.Container.GetControl<HyperLink>("cmdLast", this.ShowFirstAndLast);
      if (control4 != null)
      {
        if (this.ShowFirstAndLast)
          this.SetNavigationCommand(control4, "'Last'");
        else
          control4.Visible = false;
      }
      Control control5 = this.Container.GetControl<Control>("numeric", !this.HideNumeric);
      if (control5 == null)
        return;
      if (this.HideNumeric)
        control5.Visible = false;
      else if (control5 is Repeater)
      {
        Repeater repeater = (Repeater) control5;
        repeater.DataSource = (object) this.GetNumericNavigation();
        repeater.ItemDataBound += new RepeaterItemEventHandler(this.repeater_ItemDataBound);
        repeater.DataBind();
      }
      else if (this.ActualNavigationMode == PagerNavigationModes.ClientSide)
        this.numeric = control5;
      else
        this.PopulateNumeric(control5);
    }

    private void PopulateNumeric(Control container)
    {
      foreach (Pager.PagerNumericItem pagerNumericItem in (IEnumerable<Pager.PagerNumericItem>) this.GetNumericNavigation())
      {
        HyperLink hyperLink = new HyperLink();
        hyperLink.Text = pagerNumericItem.Text;
        hyperLink.CssClass = pagerNumericItem.CssClass;
        this.SetNavigationLink(hyperLink, pagerNumericItem.PageNumber);
        container.Controls.Add((Control) hyperLink);
      }
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? Pager.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected override ScriptRef GetRequiredCoreScripts() => this.NavigationMode != PagerNavigationModes.ClientSide ? ScriptRef.Empty : ScriptRef.JQuery;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      if (this.NavigationMode == PagerNavigationModes.ClientSide)
      {
        ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
        behaviorDescriptor.AddProperty("_virtualItemCount", (object) this.VirtualItemCount);
        behaviorDescriptor.AddProperty("_pageSize", (object) this.PageSize);
        behaviorDescriptor.AddProperty("_displayCount", (object) this.DisplayCount);
        behaviorDescriptor.AddProperty("_currentPage", (object) this.CurrentPage);
        behaviorDescriptor.AddProperty("_navigationMode", (object) this.NavigationMode);
        behaviorDescriptor.AddProperty("_numericFormat", (object) this.numericFormat);
        behaviorDescriptor.AddProperty("_pageNumberCssClass", (object) this.PageNumberCssClass);
        behaviorDescriptor.AddProperty("_currentPageNumberCssClass", (object) this.CurrentPageNumberCssClass);
        behaviorDescriptor.AddProperty("_prevGroupText", (object) this.PrevGroupText);
        behaviorDescriptor.AddProperty("_prevGroupCssClass", (object) this.PrevGroupCssClass);
        behaviorDescriptor.AddProperty("_nextGroupText", (object) this.NextGroupText);
        behaviorDescriptor.AddProperty("_nextGroupCssClass", (object) this.NextGroupCssClass);
        if (this.numeric != null)
          behaviorDescriptor.AddProperty("_numericDivId", (object) this.numeric.ClientID);
        scriptDescriptors.Add((ScriptDescriptor) behaviorDescriptor);
      }
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>();
      if (this.NavigationMode == PagerNavigationModes.ClientSide)
        scriptReferenceList.Add(new ScriptReference()
        {
          Assembly = this.GetType().Assembly.GetName().ToString(),
          Name = "Telerik.Sitefinity.Web.Scripts.Pager.js"
        });
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }

    /// <summary>
    /// When implemented by a class, processes postback data for an ASP.NET server control.
    /// </summary>
    /// <param name="postDataKey">The key identifier for the control.</param>
    /// <param name="postCollection">The collection of all incoming name values.</param>
    /// <returns>
    /// true if the server control's state changes as a result of the postback; otherwise, false.
    /// </returns>
    public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
    {
      if (this.ViewState[this.ClientID + "_CurrentPage"] != null)
        this.currentPage = Convert.ToInt32(this.ViewState[this.ClientID + "_CurrentPage"]);
      int num = postCollection[postDataKey] != "" ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.CurrentPage = Convert.ToInt32(postCollection[postDataKey]);
      return num != 0;
    }

    /// <summary>
    /// When implemented by a class, signals the server control to notify the ASP.NET application that the state of the control has changed.
    /// </summary>
    public virtual void RaisePostDataChangedEvent() => this.OnPageChanged(EventArgs.Empty);

    /// <summary>
    /// Raises the <see cref="E:PageChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnPageChanged(EventArgs e)
    {
      if (this.PageChanged == null)
        return;
      this.PageChanged((object) this, e);
    }

    private int DetermineCurrentPage()
    {
      if (this.ActualNavigationMode == PagerNavigationModes.Evaluator)
      {
        int pageNumber = this.GetPageNumber(this.UrlEvaluationMode, this.QueryParamKey, 1, this.UrlEvaluatorName);
        return pageNumber != 0 ? pageNumber : 1;
      }
      string s = SystemManager.CurrentHttpContext.Request.QueryStringGet(this.QueryParamKey);
      int result;
      return !string.IsNullOrEmpty(s) && int.TryParse(s, out result) ? result : 1;
    }

    private int DetermineSelectedItemCount()
    {
      if (this.ActualNavigationMode == PagerNavigationModes.Evaluator)
        return this.GetItemsPerPage(this.UrlEvaluationMode, string.Empty, -1, ItemsPerPageEvaluator.Name);
      string s = SystemManager.CurrentHttpContext.Request.QueryStringGet("show");
      int result;
      return !string.IsNullOrEmpty(s) && int.TryParse(s, out result) ? result : -1;
    }

    private PagerNavigationModes ActualNavigationMode
    {
      get
      {
        if (this.actualNavigationMode == PagerNavigationModes.Auto)
          this.actualNavigationMode = this.NavigationMode != PagerNavigationModes.Auto ? this.NavigationMode : PagerNavigationModes.Evaluator;
        return this.actualNavigationMode;
      }
    }

    private void SetNavigationCommand(HyperLink link, string command)
    {
      if (this.ActualNavigationMode == PagerNavigationModes.Links || this.ActualNavigationMode == PagerNavigationModes.Evaluator)
      {
        int page = 0;
        command = command.Trim('\'');
        if (!(command == "Prev"))
        {
          if (!(command == "Next"))
          {
            if (!(command == "First"))
            {
              if (command == "Last")
                page = this.PageCount;
            }
            else
              page = 1;
          }
          else
            page = this.CurrentPage + 1;
        }
        else
          page = this.CurrentPage - 1;
        if (page > 0 && page <= this.PageCount)
          link.NavigateUrl = this.BuildNavigateUrl(page);
        else
          link.NavigateUrl = "javascript:void(0);";
      }
      else
      {
        link.NavigateUrl = "#";
        link.Attributes.Add("onclick", string.Format("Telerik.Sitefinity.Web.UI.NavigateToPage('{0}',{1}); return false;", (object) this.ClientID, (object) command));
      }
    }

    private void SetNavigationLink(HyperLink link, int page)
    {
      if (this.ActualNavigationMode == PagerNavigationModes.Links || this.ActualNavigationMode == PagerNavigationModes.Evaluator)
      {
        link.NavigateUrl = this.BuildNavigateUrl(page);
        if (page != this.CurrentPage)
          return;
        link.CssClass = this.CurrentPageNumberCssClass;
      }
      else
      {
        link.NavigateUrl = "#";
        if (page == this.CurrentPage)
        {
          link.CssClass = this.CurrentPageNumberCssClass;
          link.Attributes.Add("onclick", "return false;");
        }
        else
          link.Attributes.Add("onclick", string.Format("Telerik.Sitefinity.Web.UI.NavigateToPage('{0}',{1}); return false;", (object) this.ClientID, (object) page));
      }
    }

    private string GetBaseUrl(string paramString = null)
    {
      if (!this.baseUrl.IsNullOrEmpty())
        return this.baseUrl + paramString;
      PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
      return actualCurrentNode != null ? RouteHelper.GetAbsoluteUrl(actualCurrentNode.GetLiveUrl(paramString)) : this.Page.Request.Path + paramString;
    }

    private string BuildNavigateUrl(int page)
    {
      string baseUrl = this.GetBaseUrl(this.ActualNavigationMode != PagerNavigationModes.Evaluator ? QueryStringBuilder.Current.Add(this.queryParamKey, page.ToString(), true).ToString() : UrlEvaluator.BuildUrl(this.urlEvaluatorName, (object) page, this.UrlEvaluationMode, this.QueryParamKey));
      string str1 = string.Empty;
      int selectedItemCount = this.DetermineSelectedItemCount();
      if (selectedItemCount > 0)
        str1 = UrlEvaluator.BuildUrl(ItemsPerPageEvaluator.Name, (object) selectedItemCount, this.UrlEvaluationMode, string.Empty);
      string str2 = str1;
      return this.ResolveUrl(baseUrl + str2);
    }

    private IList<Pager.PagerNumericItem> GetNumericNavigation()
    {
      int num1 = 1;
      if (this.CurrentPage > this.DisplayCount)
        num1 = (int) Math.Floor((Decimal) ((this.CurrentPage - 1) / this.DisplayCount)) * this.DisplayCount + 1;
      int num2 = Math.Min(this.PageCount, num1 + this.DisplayCount - 1);
      List<Pager.PagerNumericItem> numericNavigation = new List<Pager.PagerNumericItem>();
      if (num1 > this.DisplayCount)
        numericNavigation.Add(new Pager.PagerNumericItem(Math.Max(num1 - 1, 1), this.PrevGroupText)
        {
          CssClass = this.PrevGroupCssClass
        });
      int num3 = num1;
      for (int index = num2; num3 <= index; ++num3)
      {
        string text = string.Format(this.NumericFormat, (object) num3);
        numericNavigation.Add(new Pager.PagerNumericItem(num3, text)
        {
          CssClass = this.PageNumberCssClass
        });
      }
      if (num2 < this.PageCount)
        numericNavigation.Add(new Pager.PagerNumericItem(num2 + 1, this.NextGroupText)
        {
          CssClass = this.NextGroupCssClass
        });
      return (IList<Pager.PagerNumericItem>) numericNavigation;
    }

    private void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.FindControl("cmdPage") is HyperLink control))
        return;
      Pager.PagerNumericItem dataItem = (Pager.PagerNumericItem) e.Item.DataItem;
      control.Text = dataItem.Text;
      control.CssClass = dataItem.CssClass;
      this.SetNavigationLink(control, dataItem.PageNumber);
    }

    private int PageCount
    {
      get
      {
        if (this.pageCount < 0)
          this.pageCount = (int) Math.Ceiling((double) this.VirtualItemCount / (double) this.PageSize);
        return this.pageCount;
      }
    }

    private void AddPaginationUrls()
    {
      string previousUrl;
      string nextUrl;
      this.GetPaginationUrls(out previousUrl, out nextUrl);
      this.Page.TryStorePaginationUrls(new PaginationUrls()
      {
        NextUrl = nextUrl,
        PreviousUrl = previousUrl
      });
    }

    private void GetPaginationUrls(out string previousUrl, out string nextUrl)
    {
      previousUrl = (string) null;
      nextUrl = (string) null;
      if (this.currentPage == 1 && this.pageCount > 1)
        nextUrl = this.BuildNavigateUrl(this.currentPage + 1);
      else if (this.currentPage > 1 && this.currentPage < this.pageCount)
      {
        previousUrl = this.BuildNavigateUrl(this.currentPage - 1);
        nextUrl = this.BuildNavigateUrl(this.currentPage + 1);
      }
      else
      {
        if (this.currentPage != this.pageCount || this.pageCount <= 1)
          return;
        previousUrl = this.BuildNavigateUrl(this.currentPage - 1);
      }
    }

    /// <summary>
    /// Defines a class used as a DataItem for populating a repeater with numeric values
    /// </summary>
    public class PagerNumericItem
    {
      private int value;
      private string text;

      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Pager.PagerNumericItem" /> class.
      /// </summary>
      /// <param name="value">The value.</param>
      public PagerNumericItem(int value)
        : this(value, value.ToString())
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Pager.PagerNumericItem" /> class.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <param name="text">The text.</param>
      public PagerNumericItem(int value, string text)
      {
        this.value = value;
        this.text = text;
      }

      /// <summary>Gets the page number (the value).</summary>
      public int PageNumber => this.value;

      /// <summary>Gets the text.</summary>
      public string Text => this.text;

      /// <summary>Gets or sets the CSS class name.</summary>
      public string CssClass { get; set; }
    }
  }
}
