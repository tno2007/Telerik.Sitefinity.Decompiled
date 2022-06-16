// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.FolderBreadcrumbWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>Widget that is used to show path for folders.</summary>
  public class FolderBreadcrumbWidget : SimpleView, IWidget
  {
    private IFolderBreadcrumbWidgetDefinition definition;
    private static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Widgets.FolderBreadcrumbWidget.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FolderBreadcrumbWidget.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the type of the manager that is going to be used to get folders.
    /// </summary>
    protected virtual Type ManagerType { get; set; }

    /// <summary>Gets or sets the navigation page id.</summary>
    protected virtual Guid NavigationPageId { get; set; }

    /// <summary>Gets or sets the root page id.</summary>
    protected virtual Guid RootPageId { get; set; }

    /// <summary>Gets or sets the title for the root link.</summary>
    protected virtual string RootTitle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to append the root item URL.
    /// </summary>
    protected virtual bool AppendRootUrl { get; set; }

    /// <summary>Gets or sets the definition.</summary>
    public IWidgetDefinition Definition
    {
      get => (IWidgetDefinition) this.definition;
      set => this.definition = value as IFolderBreadcrumbWidgetDefinition;
    }

    /// <summary>Gets the name of the provider.</summary>
    protected virtual string ProviderName => this.Page.Request.QueryString["provider"];

    /// <summary>Gets the folder id.</summary>
    protected virtual Guid? FolderId
    {
      get
      {
        string input = this.Page.Request.QueryString["folderId"];
        Guid result;
        return !string.IsNullOrEmpty(input) && Guid.TryParse(input, out result) ? new Guid?(result) : new Guid?();
      }
    }

    /// <summary>Gets or sets the resource class id.</summary>
    protected virtual string ResourceClassId { get; set; }

    /// <summary>Gets a reference to the page path repeater.</summary>
    protected virtual Repeater FolderPath => this.Container.GetControl<Repeater>("folderPath", true);

    /// <summary>Gets a reference to the root link.</summary>
    protected virtual HyperLink RootLink => this.Container.GetControl<HyperLink>("rootLink", false);

    /// <summary>Configures the specified definition.</summary>
    /// <param name="definition">The definition.</param>
    public void Configure(IWidgetDefinition definition)
    {
      this.Definition = definition;
      if (this.definition == null)
        return;
      this.ManagerType = this.definition.ManagerType;
      this.NavigationPageId = this.definition.NavigationPageId;
      this.RootPageId = this.definition.RootPageId;
      this.RootTitle = this.definition.RootTitle;
      this.AppendRootUrl = this.definition.AppendRootUrl;
      this.ResourceClassId = this.definition.ResourceClassId;
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      Telerik.Sitefinity.GenericContent.Model.Content parentItem = (Telerik.Sitefinity.GenericContent.Model.Content) this.GetHostControl<MasterGridView>().ParentItem;
      if (parentItem == null)
        return;
      this.FolderPath.ItemDataBound += new RepeaterItemEventHandler(this.FolderPath_ItemDataBound);
      if (this.RootLink != null)
      {
        this.RootLink.Text = !string.IsNullOrEmpty(this.ResourceClassId) ? Res.Get(this.ResourceClassId, this.RootTitle) : this.RootTitle;
        string pageUrl = FolderBreadcrumbWidget.GetPageUrl(this.RootPageId);
        this.RootLink.NavigateUrl = this.ProviderName.IsNullOrEmpty() ? pageUrl : FolderBreadcrumbWidget.AddQueryParameter(pageUrl, "provider=" + this.ProviderName);
      }
      IFolderManager manager = (IFolderManager) ManagerBase.GetManager(this.ManagerType, this.ProviderName);
      string str1 = this.GetBaseUrl((IManager) manager, parentItem);
      List<FolderBreadcrumbWidget.FolderLink> result = new List<FolderBreadcrumbWidget.FolderLink>();
      if (this.FolderId.HasValue)
      {
        string str2 = this.Page.Request.QueryString["lang"];
        if (str2.IsNullOrEmpty())
        {
          this.FillResult(str1, result, manager);
        }
        else
        {
          str1 = FolderBreadcrumbWidget.AddQueryParameter(str1, "lang=" + str2);
          this.FillResult(str1, result, manager);
        }
        result.Add(new FolderBreadcrumbWidget.FolderLink()
        {
          Title = (string) parentItem.Title,
          Url = str1
        });
      }
      else
        result.Add(new FolderBreadcrumbWidget.FolderLink()
        {
          Title = (string) parentItem.Title,
          Url = ""
        });
      result.Reverse();
      this.FolderPath.DataSource = (object) result;
    }

    private void FolderPath_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
        return;
      if (string.IsNullOrEmpty(((FolderBreadcrumbWidget.FolderLink) e.Item.DataItem).Url))
        e.Item.FindControl("currentFolder").Visible = true;
      else
        e.Item.FindControl("normalFolder").Visible = true;
    }

    protected override void OnPreRender(EventArgs e)
    {
      this.FolderPath.DataBind();
      base.OnPreRender(e);
    }

    private static string GetPageUrl(Guid pageId)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(pageId, false);
      return siteMapNode != null ? RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) : "";
    }

    private void FillResult(
      string baseUrl,
      List<FolderBreadcrumbWidget.FolderLink> result,
      IFolderManager manager)
    {
      for (Folder folder = manager.GetFolder(this.FolderId.Value); folder != null; folder = folder.Parent)
      {
        string str = !(folder.Id != this.FolderId.Value) ? "" : FolderBreadcrumbWidget.AddQueryParameter(baseUrl, "folderId=" + folder.Id.ToString());
        result.Add(new FolderBreadcrumbWidget.FolderLink()
        {
          Title = (string) folder.Title,
          Url = str
        });
      }
    }

    private static string AddQueryParameter(string url, string parameter) => url.Contains("?") ? url + "&" + parameter : url + "?" + parameter;

    private string GetBaseUrl(IManager manager, Telerik.Sitefinity.GenericContent.Model.Content parentItem)
    {
      string url;
      if (this.AppendRootUrl)
      {
        string itemUrl = (manager.Provider as ContentDataProviderBase).GetItemUrl(parentItem as ILocatable);
        url = FolderBreadcrumbWidget.GetPageUrl(this.NavigationPageId) + itemUrl;
      }
      else
        url = FolderBreadcrumbWidget.GetPageUrl(this.NavigationPageId);
      if (!string.IsNullOrEmpty(this.ProviderName))
        url = FolderBreadcrumbWidget.AddQueryParameter(url, "provider=" + this.ProviderName);
      return url;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private struct FolderLink
    {
      public string Title { get; set; }

      public string Url { get; set; }
    }
  }
}
