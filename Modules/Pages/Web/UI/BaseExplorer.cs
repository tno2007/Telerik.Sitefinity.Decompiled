// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.BaseExplorer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Base Sitefinity Explorer control.</summary>
  public abstract class BaseExplorer : RadFileExplorer
  {
    private RadContextMenu contextMenu;
    private RadBreadCrumb breadcrumb;
    private Message message;
    private string viewUrl;
    private ITemplate loadingPanelLayoutTemplate;
    private RootTaxonType rootTaxon;
    /// <summary>
    /// Specifies the name of the embeded LoadingPanel template.
    /// </summary>
    public static readonly string LoadingPanelTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.LoadingPanel.ascx");

    /// <summary>Gets or sets the PagesView URL.</summary>
    /// <value>The PagesView URL.</value>
    public string ViewUrl
    {
      get => this.viewUrl;
      set => this.viewUrl = value;
    }

    /// <summary>
    /// Gets or sets the taxon used as root for page navigation.
    /// </summary>
    /// <value>The root taxon.</value>
    public RootTaxonType RootTaxon
    {
      get => this.rootTaxon;
      set => this.rootTaxon = value;
    }

    /// <summary>Gets or sets the loading panel layout template.</summary>
    /// <value>The loading panel layout template.</value>
    public virtual ITemplate LoadingPanelLayoutTemplate
    {
      get
      {
        if (this.loadingPanelLayoutTemplate == null)
          this.loadingPanelLayoutTemplate = ControlUtilities.GetTemplate(new TemplateInfo()
          {
            TemplatePath = BaseExplorer.LoadingPanelTemplatePath,
            TemplateResourceInfo = Config.Get<ControlsConfig>().ResourcesAssemblyInfo,
            ControlType = this.GetType()
          });
        return this.loadingPanelLayoutTemplate;
      }
      set => this.loadingPanelLayoutTemplate = value;
    }

    /// <summary>
    /// Raises the <see cref="E:Init" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.ExplorerPopulated += new RadFileExplorerGridEventHandler(this.Explorer_ExplorerPopulated);
      this.SetLoadingPanelTemplate();
    }

    /// <summary>
    /// Raises the <see cref="E:Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.SetGridControl();
    }

    /// <summary>Creates the child controls.</summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      RadSplitter splitter = this.Splitter;
      splitter.VisibleDuringInit = false;
      RadSplitBar radSplitBar = (RadSplitBar) splitter.Items[1];
      radSplitBar.CollapseMode = SplitBarCollapseMode.Backward;
      RadPane radPane = (RadPane) splitter.Items[2];
      splitter.Items.Remove((SplitterItem) radPane);
      splitter.Items.Remove((SplitterItem) radSplitBar);
      splitter.Items.AddAt(0, (SplitterItem) radPane);
      splitter.Items.AddAt(1, (SplitterItem) radSplitBar);
      this.TreeView.ShowLineImages = false;
      this.TreeView.EnableDragAndDrop = false;
      this.ToolBar.Items.Clear();
      this.ToolBar.ExpandAnimation.Type = AnimationType.None;
      this.ToolBar.CollapseAnimation.Type = AnimationType.None;
      if (!this.DesignMode)
      {
        this.WindowManager.VisibleTitlebar = false;
        this.WindowManager.ReloadOnShow = true;
        this.WindowManager.ShowContentDuringLoad = false;
        this.Controls.Add((Control) this.CreateContextMenuControl());
      }
      this.Controls.Add((Control) this.CreateBreadcrumbControl());
      this.Controls.Add((Control) this.CreateMessageControl());
    }

    /// <summary>Describes the component.</summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void DescribeComponent(IScriptDescriptor descriptor)
    {
      base.DescribeComponent(descriptor);
      if (this.contextMenu != null)
        descriptor.AddComponentProperty("pagesContextMenu", this.contextMenu.ClientID);
      if (this.breadcrumb != null)
        descriptor.AddComponentProperty("breadcrumb", this.breadcrumb.ClientID);
      if (this.message != null)
        descriptor.AddComponentProperty("message", this.message.ClientID);
      if (this.Splitter != null)
        descriptor.AddComponentProperty("splitter", this.Splitter.ClientID);
      if (!(this.ContentProvider is SitefinityFileBrowserContentProviderBase))
        return;
      Dictionary<SitefinityFileBrowserContentProviderBase.LocalizationKeys, string> localizationMessages = ((SitefinityFileBrowserContentProviderBase) this.ContentProvider).GetOverriddenLocalizationMessages();
      if (localizationMessages == null)
        return;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (KeyValuePair<SitefinityFileBrowserContentProviderBase.LocalizationKeys, string> keyValuePair in localizationMessages)
        dictionary.Add(keyValuePair.Key.ToString(), keyValuePair.Value);
      string str = new JavaScriptSerializer().Serialize((object) dictionary);
      descriptor.AddProperty("_sitefinityLocalization", (object) str);
    }

    /// <summary>
    /// Handles the ExplorerPopulated event of the TemplateExplorer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadFileExplorerPopulatedEventArgs" /> instance containing the event data.</param>
    protected abstract void Explorer_ExplorerPopulated(
      object sender,
      RadFileExplorerPopulatedEventArgs e);

    /// <summary>Creates the context menu control.</summary>
    /// <returns></returns>
    protected virtual RadContextMenu CreateContextMenuControl()
    {
      this.contextMenu = new RadContextMenu();
      this.contextMenu.ID = "contextMenu";
      this.contextMenu.CssClass = "sfpeGridMoreActions";
      this.contextMenu.CollapseAnimation.Type = AnimationType.None;
      this.contextMenu.ExpandAnimation.Type = AnimationType.None;
      this.contextMenu.EnableViewState = false;
      return this.contextMenu;
    }

    /// <summary>Creates the RadBreadCrumb control.</summary>
    /// <returns></returns>
    protected virtual RadBreadCrumb CreateBreadcrumbControl()
    {
      this.breadcrumb = new RadBreadCrumb();
      this.breadcrumb.ID = "breadcrumb";
      this.breadcrumb.EnableViewState = false;
      this.breadcrumb.CssClass = "sfBreadCrumb";
      return this.breadcrumb;
    }

    /// <summary>Creates the Message control.</summary>
    /// <returns></returns>
    protected virtual Message CreateMessageControl()
    {
      this.message = new Message();
      this.message.ID = "message";
      this.message.ElementTag = HtmlTextWriterTag.Div;
      this.message.RemoveAfter = 5000;
      this.message.FadeDuration = 4000;
      return this.message;
    }

    /// <summary>Sets the grid control.</summary>
    protected virtual void SetGridControl()
    {
      GridColumn[] renderColumns = this.Grid.MasterTableView.RenderColumns;
      GridColumn byUniqueNameSafe1 = this.Grid.Columns.FindByUniqueNameSafe("Name");
      if (byUniqueNameSafe1 != null)
        renderColumns[byUniqueNameSafe1.OrderIndex].HeaderText = Res.Get<PageResources>().Name;
      GridColumn byUniqueNameSafe2 = this.Grid.Columns.FindByUniqueNameSafe("Size");
      if (byUniqueNameSafe2 != null)
        renderColumns[byUniqueNameSafe2.OrderIndex].HeaderText = "&nbsp;";
      GridClientSettings clientSettings = this.Grid.ClientSettings;
      clientSettings.EnableRowHoverStyle = true;
      clientSettings.AllowRowsDragDrop = false;
      clientSettings.Resizing.AllowColumnResize = false;
    }

    /// <summary>Adds a grid column.</summary>
    /// <param name="columnType">The column type.</param>
    /// <param name="headerText">The text for the header section of the column.</param>
    /// <param name="uniqueName">The unique name of the column.</param>
    /// <param name="dataField">The data field.</param>
    /// <param name="sortable">If set to <c>true</c> the column is sortable.</param>
    /// <param name="width">The width of the column.</param>
    /// <param name="index">The index in the column collection where the column will be added.</param>
    protected void AddGridColumn(
      string columnType,
      string headerText,
      string uniqueName,
      string dataField,
      bool sortable,
      int? width,
      int index)
    {
      this.RemoveGridColumn(uniqueName);
      GridColumn column = (GridColumn) null;
      if (columnType == "GridClientSelectColumn")
        column = (GridColumn) new GridClientSelectColumn();
      else if (columnType == "GridTemplateColumn")
        column = (GridColumn) new GridTemplateColumn();
      if (!string.IsNullOrEmpty(headerText))
        column.HeaderText = headerText;
      if (sortable)
        column.SortExpression = uniqueName;
      column.UniqueName = uniqueName;
      if (!string.IsNullOrEmpty(dataField) && columnType == "GridTemplateColumn")
        ((GridTemplateColumn) column).DataField = dataField;
      if (width.HasValue)
        column.HeaderStyle.Width = Unit.Pixel(width.Value);
      if (index != -1)
        this.Grid.Columns.AddAt(index, column);
      else
        this.Grid.Columns.Add(column);
    }

    /// <summary>Removes the grid column from the grid collection.</summary>
    /// <param name="uniqueName">The unique name of the column.</param>
    protected void RemoveGridColumn(string uniqueName)
    {
      GridColumn byUniqueNameSafe = this.Grid.Columns.FindByUniqueNameSafe(uniqueName);
      if (byUniqueNameSafe == null)
        return;
      this.Grid.Columns.Remove(byUniqueNameSafe);
    }

    /// <summary>Creates a RadMenuItem object.</summary>
    /// <param name="value">The value assosiated with the menu item.</param>
    /// <param name="text">The text caption for the menu item.</param>
    /// <param name="postBack">A value indicating whether clicking on the item will postback.</param>
    /// <returns></returns>
    protected RadMenuItem CreateContextMenuItem(
      string value,
      string text,
      bool postBack)
    {
      RadMenuItem contextMenuItem = new RadMenuItem();
      contextMenuItem.Value = value;
      contextMenuItem.Text = text;
      contextMenuItem.PostBack = postBack;
      contextMenuItem.EnableViewState = false;
      contextMenuItem.SelectedCssClass = "rfeNoClass";
      return contextMenuItem;
    }

    /// <summary>Creates a RadToolBarButton control.</summary>
    /// <param name="value">The value associated with the toolbar button.</param>
    /// <param name="text">The text displayed for the current item.</param>
    /// <param name="navigateUrl">The URL to link to when the button is clicked.</param>
    /// <param name="cssClass">The CSS class rendered on the client.</param>
    /// <param name="descriptionCssClass">The description CSS class.</param>
    /// <param name="description">The description associated with the toolbar button.</param>
    /// <returns></returns>
    protected RadToolBarButton CreateToolBarButton(
      string value,
      string text,
      string navigateUrl,
      string cssClass,
      string descriptionCssClass,
      string description)
    {
      RadToolBarButton toolBarButton = new RadToolBarButton();
      toolBarButton.Value = value;
      if (!string.IsNullOrEmpty(description))
      {
        toolBarButton.ItemTemplate = (ITemplate) new BaseExplorer.ToolBarButtonDescriptionTemplate(text, descriptionCssClass, description);
      }
      else
      {
        toolBarButton.Text = !string.IsNullOrEmpty(text) ? text : "&nbsp";
        toolBarButton.ToolTip = text;
      }
      if (!string.IsNullOrEmpty(navigateUrl))
        toolBarButton.NavigateUrl = navigateUrl;
      toolBarButton.EnableViewState = false;
      toolBarButton.CssClass += cssClass;
      return toolBarButton;
    }

    private void SetLoadingPanelTemplate()
    {
      RadAjaxLoadingPanel control = (RadAjaxLoadingPanel) this.FindControl("ajaxLoadingPanel");
      control.CssClass = string.Empty;
      control.Skin = string.Empty;
      this.LoadingPanelLayoutTemplate.InstantiateIn((Control) control);
    }

    private class ToolBarButtonDescriptionTemplate : ITemplate
    {
      private string buttonText;
      private string descriptionCssClass;
      private string description;

      public ToolBarButtonDescriptionTemplate(
        string buttonText,
        string descriptionCssClass,
        string description)
      {
        this.buttonText = buttonText;
        this.descriptionCssClass = descriptionCssClass;
        this.description = description;
      }

      public void InstantiateIn(Control container)
      {
        HtmlAnchor child1 = new HtmlAnchor();
        child1.InnerHtml = "<span class='rtbText'>" + this.buttonText + "</span>";
        child1.Title = this.buttonText;
        container.Controls.Add((Control) child1);
        Label child2 = new Label();
        child2.Text = this.description;
        child2.CssClass = this.descriptionCssClass;
        container.Controls.Add((Control) child2);
      }
    }

    /// <summary>Compares file browser items by attribute value.</summary>
    protected class AttributeValueComparer : IComparer<FileBrowserItem>
    {
      private string attrKey;

      /// <summary>Gets or sets the attribute key.</summary>
      /// <value>The attribute key.</value>
      public string AttrKey
      {
        get => this.attrKey;
        set => this.attrKey = value;
      }

      /// <summary>
      /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
      /// </summary>
      /// <param name="x">The first object to compare.</param>
      /// <param name="y">The second object to compare.</param>
      /// <returns>
      /// Value
      /// Condition
      /// Less than zero
      /// <paramref name="x" /> is less than <paramref name="y" />.
      /// Zero
      /// <paramref name="x" /> equals <paramref name="y" />.
      /// Greater than zero
      /// <paramref name="x" /> is greater than <paramref name="y" />.
      /// </returns>
      public int Compare(FileBrowserItem x, FileBrowserItem y)
      {
        string attribute1 = x.Attributes[this.attrKey];
        string attribute2 = y.Attributes[this.attrKey];
        return x is DirectoryItem ? (y is DirectoryItem ? StringComparer.InvariantCultureIgnoreCase.Compare(attribute1, attribute2) : -1) : (y is DirectoryItem ? 1 : StringComparer.InvariantCultureIgnoreCase.Compare(attribute1, attribute2));
      }
    }

    /// <summary>Compares file browser items by date.</summary>
    protected class DateComparer : IComparer<FileBrowserItem>
    {
      /// <summary>
      /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
      /// </summary>
      /// <param name="x">The first object to compare.</param>
      /// <param name="y">The second object to compare.</param>
      /// <returns>
      /// Value
      /// Condition
      /// Less than zero
      /// <paramref name="x" /> is less than <paramref name="y" />.
      /// Zero
      /// <paramref name="x" /> equals <paramref name="y" />.
      /// Greater than zero
      /// <paramref name="x" /> is greater than <paramref name="y" />.
      /// </returns>
      public int Compare(FileBrowserItem x, FileBrowserItem y)
      {
        DateTime t1 = DateTime.Parse(x.Attributes["Date"]);
        DateTime t2 = DateTime.Parse(y.Attributes["Date"]);
        return x is DirectoryItem ? (y is DirectoryItem ? DateTime.Compare(t1, t2) : -1) : (y is DirectoryItem ? 1 : DateTime.Compare(t1, t2));
      }
    }
  }
}
