// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views.MasterView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Extensions;
using Telerik.Sitefinity.Web.UI.ContentUI.Views;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views
{
  /// <summary>Shows a list of generic content items</summary>
  public class MasterView : MasterViewBase
  {
    private string sortExpression;
    private int? itemsPerPage;
    private string mainBrowseAndEditToolbarId = "MainBrowseAndEditToolbar";
    private string itemBrowseAndEditToolbarId = "BrowseAndEditToolbar";
    private ContentManager manager;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.GenericContent.MasterView.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MasterView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected ContentManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = ContentManager.GetManager(this.Host.ControlDefinition.ProviderName);
        return this.manager;
      }
    }

    /// <summary>
    /// Reference to the rad list that lists generic content items
    /// </summary>
    protected virtual RadListView List => this.Container.GetControl<RadListView>(nameof (List), true);

    /// <summary>Gets the pager.</summary>
    /// <value>The pager.</value>
    protected virtual Pager Pager => this.Container.GetControl<Pager>("pager", true);

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

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      int? totalCount = new int?(0);
      this.InitializeListView(this.GetItemsList(ref totalCount), totalCount);
    }

    /// <summary>
    /// Initializes the list view control with the items specified in the query.
    /// </summary>
    /// <param name="query">The query.</param>
    protected virtual void InitializeListView(IQueryable<ContentItem> query, int? totalCount)
    {
      int? nullable1 = totalCount;
      int num1 = 0;
      this.IsEmptyView = nullable1.GetValueOrDefault() == num1 & nullable1.HasValue;
      int? nullable2 = totalCount;
      int num2 = 0;
      if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
      {
        this.List.Visible = false;
      }
      else
      {
        this.ConfigurePager(this.Pager, totalCount.Value);
        this.List.DataSource = (object) query;
        this.List.PreRender += new EventHandler(this.List_PreRender);
        this.List.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(this.List_ItemDataBound);
        this.List.DataBound += new EventHandler(this.List_DataBound);
      }
    }

    /// <summary>Gets the items list.</summary>
    /// <returns></returns>
    protected virtual IQueryable<ContentItem> GetItemsList(ref int? totalCount)
    {
      IQueryable<ContentItem> query = ContentManager.GetManager(this.Host.ControlDefinition.ProviderName).GetContent();
      bool? nullable;
      if (this.AllowUrlQueries.HasValue)
      {
        nullable = this.AllowUrlQueries;
        if (nullable.Value)
          query = this.EvaluateUrl<ContentItem>(this.EvaluateUrl<ContentItem>(this.EvaluateUrl<ContentItem>(query, "Date", "PublicationDate", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix), "Author", "Owner", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix), "Taxonomy", "", typeof (ContentItem), this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
      }
      int? skip = new int?(0);
      nullable = this.AllowPaging;
      if (nullable.HasValue)
      {
        nullable = this.AllowPaging;
        if (nullable.Value)
          skip = new int?(this.GetItemsToSkipCount(this.ItemsPerPage, this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix));
      }
      this.MasterViewDefinition.AdaptMultilingualFilterExpression();
      string filterExpression = DefinitionsHelper.GetFilterExpression(this.FilterExpression, this.AdditionalFilter);
      return DataProviderBase.SetExpressions<ContentItem>(query, filterExpression, this.SortExpression, skip, this.ItemsPerPage, ref totalCount);
    }

    private void List_DataBound(object sender, EventArgs e)
    {
      if (!SystemManager.IsBrowseAndEditMode || !(this.List.FindControl(this.mainBrowseAndEditToolbarId) is ContentBrowseAndEditToolbar control))
        return;
      control.TaxonomyFiltersInfo = this.Host.GetTaxonomyFilters();
      BrowseAndEditManager.GetCurrent(this.Page).AddConfiguredContentBrowseAndEditToolBar((Telerik.Sitefinity.GenericContent.Model.Content) null, this.Host, control);
    }

    private void List_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      if (!SystemManager.IsBrowseAndEditMode)
        return;
      BrowseAndEditManager.GetCurrent(this.Page).AddBrowseAndEditToolBar((Control) e.Item, this.Host, this.itemBrowseAndEditToolbarId);
    }

    /// <summary>
    /// Handles the PreRender event of the EventsList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void List_PreRender(object sender, EventArgs e) => this.SetCommentCounts<HyperLink>((ICommentsManager) this.Manager, this.List, (Action<HyperLink, int>) ((itemCommentsLink, commentsCount) =>
    {
      string str = commentsCount == 1 ? Res.Get<ContentResources>().Comment : Res.Get<ContentResources>().Comments;
      itemCommentsLink.Text = string.Format("{0} {1}", (object) commentsCount, (object) str);
      itemCommentsLink.NavigateUrl = string.Format("{0}#comments", (object) itemCommentsLink.NavigateUrl);
    }));
  }
}
