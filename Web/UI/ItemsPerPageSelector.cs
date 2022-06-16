// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ItemsPerPageSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Defines a ItemsPerPage control</summary>
  public class ItemsPerPageSelector : SimpleScriptView, IPostBackDataHandler
  {
    public const int NoSelectedItemCount = 0;
    public const int ShowAllItemsItemCountValue = -1;
    private const string viewStateSuffix = "_ItemsPerPage";
    private const string defaultPagerQueryParamKey = "page";
    private const string defaultQueryParamKey = "show";
    private const string controlPath = "Telerik.Sitefinity.Resources.Templates.PublicControls.ItemsPerPageSelector.ascx";
    private const string ShowAllQueryString = "all";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.ItemsPerPageSelector.ascx");
    private string groupingKey;
    private string baseUrl;

    public ItemsPerPageSelector()
    {
      this.QueryParamKey = "show";
      this.PagerQueryParamKey = "page";
    }

    protected virtual HyperLink ItemsTimesOne => this.Container.GetControl<HyperLink>("itemsTimesOne", true);

    protected virtual HyperLink ItemsTimesTwo => this.Container.GetControl<HyperLink>("itemsTimesTwo", true);

    protected virtual HyperLink ItemsTimesThree => this.Container.GetControl<HyperLink>("itemsTimesThree", true);

    protected virtual HyperLink ViewAll => this.Container.GetControl<HyperLink>("viewAll", true);

    /// <summary>Gets or sets the query param key.</summary>
    /// <value>The query param key.</value>
    public string QueryParamKey { get; set; }

    /// <summary>Gets or sets the pager query param key.</summary>
    /// <value>The pager query param key.</value>
    public string PagerQueryParamKey { get; set; }

    /// <summary>Gets or sets the items per page.</summary>
    /// <value>The items per page.</value>
    public int ItemsPerPage { get; set; }

    /// <summary>Gets or sets the item count.</summary>
    /// <value>The item count.</value>
    public int ItemCount { get; set; }

    /// <summary>Gets or sets the grouping key.</summary>
    /// <value>The grouping key.</value>
    public string GroupingKey
    {
      get => string.IsNullOrEmpty(this.groupingKey) ? this.ClientID : this.groupingKey;
      set => this.groupingKey = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ItemsPerPage <= 0)
      {
        this.Visible = false;
      }
      else
      {
        int selectedItemCount = this.DetermineSelectedItemCount();
        if (selectedItemCount == 0)
          selectedItemCount = this.ItemsPerPage;
        this.InitilizeSelectorLink(this.ItemsTimesOne, 1, selectedItemCount);
        this.InitilizeSelectorLink(this.ItemsTimesTwo, 2, selectedItemCount);
        this.InitilizeSelectorLink(this.ItemsTimesThree, 3, selectedItemCount);
        this.ViewAll.NavigateUrl = this.BuildNavigateUrl(-1) + this.Page.GetQueryString();
        if (selectedItemCount != -1)
          return;
        this.ViewAll.Enabled = false;
      }
    }

    private void InitilizeSelectorLink(
      HyperLink selectorLink,
      int multiplier,
      int selectedItemCount)
    {
      int itemNavigationCount = this.ItemsPerPage * multiplier;
      selectorLink.Text = itemNavigationCount.ToString();
      selectorLink.NavigateUrl = this.BuildNavigateUrl(itemNavigationCount) + this.Page.GetQueryString();
      if (selectedItemCount != itemNavigationCount)
        return;
      selectorLink.Enabled = false;
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ItemsPerPageSelector.layoutTemplatePath : base.LayoutTemplatePath;
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
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.Empty;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>();

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>();

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
      string key = this.GroupingKey + "_ItemsPerPage";
      if (this.ViewState[key] != null)
        this.ItemsPerPage = Convert.ToInt32(this.ViewState[key]);
      int num = postCollection[postDataKey] != string.Empty ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.ItemsPerPage = Convert.ToInt32(postCollection[postDataKey]);
      return num != 0;
    }

    /// <summary>
    /// When implemented by a class, signals the server control to notify the ASP.NET application that the state of the control has changed.
    /// </summary>
    public virtual void RaisePostDataChangedEvent()
    {
    }

    private string GetBaseUrl()
    {
      if (this.baseUrl == null)
      {
        SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
        if (currentProvider != null)
        {
          SiteMapNode currentNode = currentProvider.CurrentNode;
          if (currentNode != null)
          {
            if (currentNode is PageSiteNode node)
            {
              PageSiteNode firstPageDataNode = RouteHelper.GetFirstPageDataNode(node, true);
              this.baseUrl = !node.IsGroupPage || !(firstPageDataNode.Url != currentNode.Url) ? node.UrlWithoutExtension : firstPageDataNode.UrlWithoutExtension;
            }
            else
              this.baseUrl = currentNode.Url;
            this.baseUrl = RouteHelper.GetAbsoluteUrl(this.baseUrl);
            return this.baseUrl;
          }
        }
        this.baseUrl = this.Page.Request.Path;
      }
      return this.baseUrl;
    }

    private string BuildNavigateUrl(int itemNavigationCount)
    {
      string baseUrl = this.GetBaseUrl();
      string empty = string.Empty;
      string str = string.Empty;
      if (this.ItemsPerPage > 0)
      {
        this.DetermineCurrentPage();
        str = string.Format("{0}/{1}/", (object) this.QueryParamKey, itemNavigationCount > 0 ? (object) itemNavigationCount.ToString() : (object) "all");
      }
      return this.ResolveUrl(baseUrl + "/" + empty + str);
    }

    private int DetermineCurrentPage() => this.GetPageNumber(this.GetUrlEvaluationMode(), string.Empty, 1);

    private int DetermineSelectedItemCount() => this.GetItemsPerPage(this.GetUrlEvaluationMode(), string.Empty);
  }
}
