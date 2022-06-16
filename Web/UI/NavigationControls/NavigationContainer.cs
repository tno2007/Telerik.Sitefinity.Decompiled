// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.NavigationContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  [ParseChildren(true)]
  public class NavigationContainer : WebControl, INamingContainer
  {
    private bool hasWrappingTag;
    private HtmlTextWriterTag wrapperTag = HtmlTextWriterTag.Ul;
    private List<NavigationTemplate> templates;
    private SitefinitySiteMapDataSource dataSource;

    /// <summary>Gets the templates.</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual List<NavigationTemplate> Templates
    {
      get
      {
        if (this.templates == null)
          this.templates = new List<NavigationTemplate>();
        return this.templates;
      }
    }

    /// <summary>Gets the ParentItemTemplate.</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual ITemplate ParentItemTemplate { get; set; }

    /// <summary>Gets or sets the data source.</summary>
    public virtual SitefinitySiteMapDataSource DataSource
    {
      get
      {
        if (this.dataSource == null && !this.DataSourceID.IsNullOrEmpty())
          this.dataSource = this.FindDataSource();
        return this.dataSource;
      }
      set => this.dataSource = value;
    }

    /// <summary>Gets or sets the data source ID.</summary>
    public virtual string DataSourceID { get; set; }

    /// <summary>Gets or sets the wrapper tag.</summary>
    public virtual HtmlTextWriterTag WrapperTag
    {
      get => this.wrapperTag;
      set => this.wrapperTag = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control will have a wrapping tag.
    /// </summary>
    public virtual bool HasWrappingTag
    {
      get => this.hasWrappingTag;
      set => this.hasWrappingTag = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => this.WrapperTag;

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (!this.HasWrappingTag)
        return;
      base.RenderBeginTag(writer);
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (!this.HasWrappingTag)
        return;
      base.RenderEndTag(writer);
    }

    /// <summary>Occurs when item created.</summary>
    public event EventHandler<NavigationContainerItemEventArgs> ItemCreated;

    /// <summary>Occurs when item data bound.</summary>
    public event EventHandler<NavigationContainerItemEventArgs> ItemDataBound;

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      if (this.DataSource == null)
        return;
      if (this.DataSource.AddParentItem && this.DataSource.ParentNode != null)
        this.BindParentItem(this.DataSource.ParentNode, (Control) this);
      if (this.DataSource.Nodes.Count > 0)
      {
        foreach (SiteMapNode node in (IEnumerable<SiteMapNode>) this.DataSource.Nodes)
          this.BindNodesByLevel(node, (Control) this, 0);
      }
      else
      {
        if (this.DataSource.DoNotLoadNodesWhenEmpty)
          return;
        SiteMapNodeCollection nodes = this.DataSource.GetNodes();
        if (nodes == null)
          return;
        List<PageSiteNode> pageSiteNodeList = new List<PageSiteNode>();
        foreach (SiteMapNode node in nodes)
        {
          this.BindNodesByLevel(node, (Control) this, 0);
          if (node is PageSiteNode pageSiteNode)
            pageSiteNodeList.Add(pageSiteNode);
        }
        if (pageSiteNodeList.Count<PageSiteNode>() <= 0)
          return;
        PageSiteNode pageSiteNode1 = pageSiteNodeList.First<PageSiteNode>();
        ((IEnumerable<IDataItem>) PageNodeDataItem.GetPageNodeDataItemsList(pageSiteNodeList)).SetRelatedDataSourceContext(typeof (PageNode).FullName, pageSiteNode1.PageProviderName);
      }
    }

    /// <summary>
    /// Binds a data source to the invoked server control and all its child controls.
    /// </summary>
    public override void DataBind()
    {
      this.CreateChildControls();
      this.ChildControlsCreated = true;
      base.DataBind();
    }

    private SitefinitySiteMapDataSource FindDataSource()
    {
      for (Control parent = this.Parent; parent != null; parent = parent.Parent)
      {
        if (parent.FindControl(this.DataSourceID) is SitefinitySiteMapDataSource control)
          return control;
      }
      return (SitefinitySiteMapDataSource) null;
    }

    private bool HasSelectedChild(SiteMapNode node)
    {
      SiteMapNode currentNode = this.DataSource.Provider.CurrentNode;
      return currentNode != null && currentNode.IsDescendantOf(node);
    }

    private void BindParentItem(SiteMapNode node, Control parent)
    {
      if (node is PageSiteNode node1 && (!node1.ShowInNavigation || !this.IsPageVisible(node1)))
        return;
      NavigationNodeContainer navigationNodeContainer = new NavigationNodeContainer();
      parent.Controls.Add((Control) navigationNodeContainer);
      try
      {
        this.ParentItemTemplate.InstantiateIn((Control) navigationNodeContainer);
      }
      catch (NullReferenceException ex)
      {
        throw new Exception("There is no \"ParentItemTemplate\" included in your LightNavigationControl template");
      }
      navigationNodeContainer.DataItem = (object) node;
    }

    private bool BindNodesByLevel(SiteMapNode node, Control parent, int level)
    {
      if (node is PageSiteNode node1 && (!node1.ShowInNavigation || !this.IsPageVisible(node1)))
        return false;
      NavigationNodeContainer navigationNodeContainer = new NavigationNodeContainer();
      parent.Controls.Add((Control) navigationNodeContainer);
      NavigationTemplate navigationTemplate1 = this.Templates.FirstOrDefault<NavigationTemplate>((Func<NavigationTemplate, bool>) (t =>
      {
        int? level1 = t.Level;
        int num = level;
        return level1.GetValueOrDefault() == num & level1.HasValue;
      }));
      NavigationTemplate navigationTemplate2 = this.Templates.FirstOrDefault<NavigationTemplate>((Func<NavigationTemplate, bool>) (t => !t.Level.HasValue));
      bool flag = this.DataSource.Provider.CurrentNode != null && (this.DataSource.Provider.CurrentNode.Key == node.Key || this.HasSelectedChild(node));
      if (flag && navigationTemplate1 != null && navigationTemplate1.SelectedTemplate != null)
        navigationTemplate1.SelectedTemplate.InstantiateIn((Control) navigationNodeContainer);
      else if (flag && navigationTemplate2 != null && navigationTemplate2.SelectedTemplate != null)
        navigationTemplate2.SelectedTemplate.InstantiateIn((Control) navigationNodeContainer);
      else if (navigationTemplate1 != null)
        navigationTemplate1.Template.InstantiateIn((Control) navigationNodeContainer);
      else
        navigationTemplate2.Template.InstantiateIn((Control) navigationNodeContainer);
      navigationNodeContainer.DataItem = (object) node;
      if (this.ItemCreated != null)
        this.ItemCreated((object) this, new NavigationContainerItemEventArgs()
        {
          Item = navigationNodeContainer
        });
      navigationNodeContainer.DataBound += new EventHandler(this.container_DataBound);
      Control control = navigationNodeContainer.FindControl("childNodesContainer");
      if (control != null)
      {
        control.Visible = false;
        int? levelsToInclude = this.DataSource.LevelsToInclude;
        if (levelsToInclude.HasValue)
        {
          int num1 = level + 1;
          levelsToInclude = this.DataSource.LevelsToInclude;
          int num2 = levelsToInclude.Value;
          if (num1 >= num2)
            return true;
        }
        foreach (SiteMapNode childNode in node.ChildNodes)
        {
          if (!(childNode as PageSiteNode).IsHidden() && this.BindNodesByLevel(childNode, control, level + 1))
            control.Visible = true;
        }
      }
      return true;
    }

    private bool IsPageVisible(PageSiteNode node, bool ignoreShowInNavigation = false)
    {
      switch (node.NodeType)
      {
        case NodeType.Group:
          return RouteHelper.GetFirstPageDataNode(node, true) != null;
        case NodeType.InnerRedirect:
          bool flag = false;
          if (this.DataSource.Provider.FindSiteMapNodeFromKey(node.LinkedNodeId.ToString()) is PageSiteNode siteMapNodeFromKey)
            flag = this.IsPageVisible(siteMapNodeFromKey, true);
          if (!flag)
            return false;
          return ignoreShowInNavigation || node.ShowInNavigation;
        case NodeType.OuterRedirect:
          return !SystemManager.CurrentContext.AppSettings.Multilingual || ((IEnumerable<CultureInfo>) node.AvailableLanguages).Contains<CultureInfo>(SystemManager.CurrentContext.Culture);
        default:
          return RouteHelper.CheckSiteMapNode((SiteMapNode) node, ignoreShowInNavigation);
      }
    }

    private void container_DataBound(object sender, EventArgs e)
    {
      NavigationNodeContainer navigationNodeContainer = sender as NavigationNodeContainer;
      if (this.ItemDataBound == null)
        return;
      this.ItemDataBound((object) this, new NavigationContainerItemEventArgs()
      {
        Item = navigationNodeContainer
      });
    }
  }
}
