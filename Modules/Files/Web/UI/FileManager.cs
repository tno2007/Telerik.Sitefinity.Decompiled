// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Files.Web.UI.FileManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;

namespace Telerik.Sitefinity.Modules.Files.Web.UI
{
  /// <summary>
  /// A class for manipulating the file system of the project.
  /// </summary>
  [ClientScriptResource("Telerik.Sitefinity.Modules.Files.Web.UI.FileManager", "Telerik.Sitefinity.Modules.Files.Web.UI.Scripts.FileManager.js")]
  public class FileManager : RadFileExplorer
  {
    /// <summary>
    /// Raises the <see cref="E:Init" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.ExplorerPopulated += new RadFileExplorerGridEventHandler(this.FileManager_ExplorerPopulated);
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

    /// <summary>Controls the pre render.</summary>
    protected override void ControlPreRender()
    {
      base.ControlPreRender();
      if (this.GridContextMenu == null)
        return;
      this.GridContextMenu.Visible = false;
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based
    /// implementation to create any child controls they contain in preparation for
    /// posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      this.TreeView.EnableDragAndDrop = false;
      this.TreeView.ShowLineImages = false;
      foreach (RadToolBarItem radToolBarItem in (StateManagedCollection) this.ToolBar.Items)
      {
        radToolBarItem.CssClass += " sfDisplayNoneImportant";
        radToolBarItem.Visible = false;
      }
      this.AddToolBarButton(this.ToolBar, "FolderUp", Res.Get<FilesResources>().FolderUpToolBarButtonText, "icnFolderUp");
      this.AddToolBarButton(this.ToolBar, "Upload", Res.Get<FilesResources>().UploadToolBarButtonText, "icnUpload");
      this.AddToolBarButton(this.ToolBar, "Download", Res.Get<FilesResources>().DownloadToolBarButtonText, "icnDownload");
      this.AddToolBarButton(this.ToolBar, "Delete", Res.Get<FilesResources>().DeleteToolBarButtonText, "icnDelete");
      this.AddToolBarButton(this.ToolBar, "Rename", Res.Get<FilesResources>().RenameToolBarButtonText, "icnRename");
      this.AddToolBarButton(this.ToolBar, "Copy", Res.Get<FilesResources>().CopyToolBarButtonText, "icnCopy");
      this.AddToolBarButton(this.ToolBar, "Paste", Res.Get<FilesResources>().PasteToolBarButtonText, "icnPaste");
      this.AddToolBarButton(this.ToolBar, "NewFolder", Res.Get<FilesResources>().NewFolderToolBarButtonText, "icnNewFolder");
      this.TreeView.ContextMenus[0].Items.Add(this.CreateContextMenuItem("Download", Res.Get<FilesResources>().DownloadContextMenuText, false));
    }

    /// <summary>Describes the component.</summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void DescribeComponent(IScriptDescriptor descriptor)
    {
      base.DescribeComponent(descriptor);
      string applicationPath = this.Context.Request.ApplicationPath;
      if (!applicationPath.EndsWith("/"))
        applicationPath += "/";
      descriptor.AddProperty("initialPath", (object) applicationPath);
    }

    /// <summary>
    /// Handles the ExplorerPopulated event of the FileExplorer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadFileExplorerPopulatedEventArgs" /> instance containing the event data.</param>
    protected void FileManager_ExplorerPopulated(object sender, RadFileExplorerPopulatedEventArgs e)
    {
      bool flag = false;
      string lower = e.SortColumnName.ToLower(CultureInfo.InvariantCulture);
      List<FileBrowserItem> list = e.List;
      if (lower == "datemodified")
      {
        list.Sort((IComparer<FileBrowserItem>) new FileManager.DateComparer());
        flag = true;
      }
      else if (lower == "type")
      {
        list.Sort((IComparer<FileBrowserItem>) new FileManager.TypeComparer());
        flag = true;
      }
      if (!flag || e.SortDirection.IndexOf("DESC", StringComparison.InvariantCultureIgnoreCase) == -1)
        return;
      list.Reverse();
    }

    /// <summary>
    /// Get the Grid data for the current selected folder in the file explorer tree
    /// </summary>
    /// <param name="path">path to current selected folder</param>
    /// <param name="sortExpression">sort argument (column and direction)</param>
    /// <param name="startIndex">the index of the first item to return (used for paging)</param>
    /// <param name="maxRowNumber">the number of items to return (used for paging)</param>
    /// <param name="includeFiles">if set to true, will return files and folders, otherwise only folders</param>
    /// <param name="control">the control that needs the data ("grid" or "tree")</param>
    /// <param name="itemsCount">out parameter - set to the number of items returned</param>
    /// <returns>a list of files and folders in the selected path</returns>
    protected override List<FileBrowserItem> GetExplorerData(
      string path,
      string sortExpression,
      int startIndex,
      int maxRowNumber,
      bool includeFiles,
      string control,
      out int itemsCount)
    {
      RadTreeNode nodeByValue = this.TreeView.FindNodeByValue(path, true);
      if (nodeByValue != null)
        nodeByValue.Attributes["Tag"] = string.Format("hasfiles={0};", (object) ((FileProvider) this.ContentProvider).HasFiles(path));
      return base.GetExplorerData(path, sortExpression, startIndex, maxRowNumber, includeFiles, control, out itemsCount);
    }

    private void SetGridControl()
    {
      this.AddGridColumn("GridClientSelectColumn", this.Grid, string.Empty, "ClientSelectColumn", string.Empty, false, new int?(25), 0);
      this.AddGridColumn("GridTemplateColumn", this.Grid, Res.Get<FilesResources>().TypeGridColumnHeaderText, "Type", "Type", true, new int?(), -1);
      this.AddGridColumn("GridTemplateColumn", this.Grid, Res.Get<FilesResources>().DateModifiedGridColumnHeaderText, "DateModified", "DateModified", true, new int?(), -1);
      GridColumn[] renderColumns = this.Grid.MasterTableView.RenderColumns;
      GridColumn byUniqueNameSafe1 = this.Grid.Columns.FindByUniqueNameSafe("Name");
      if (byUniqueNameSafe1 != null)
        renderColumns[byUniqueNameSafe1.OrderIndex].HeaderText = Res.Get<FilesResources>().FileNameText;
      if (this.Page.IsPostBack)
      {
        if (byUniqueNameSafe1 != null)
        {
          ((GridTemplateColumn) renderColumns[byUniqueNameSafe1.OrderIndex]).DataField = "Name";
          renderColumns[byUniqueNameSafe1.OrderIndex].SortExpression = "Name";
          renderColumns[byUniqueNameSafe1.OrderIndex].HeaderStyle.Width = Unit.Empty;
        }
        GridColumn byUniqueNameSafe2 = this.Grid.Columns.FindByUniqueNameSafe("Size");
        if (byUniqueNameSafe2 != null)
        {
          ((GridTemplateColumn) renderColumns[byUniqueNameSafe2.OrderIndex]).DataField = "Size";
          renderColumns[byUniqueNameSafe2.OrderIndex].SortExpression = "Size";
          renderColumns[byUniqueNameSafe2.OrderIndex].HeaderStyle.Width = Unit.Pixel(70);
        }
      }
      GridClientSettings clientSettings = this.Grid.ClientSettings;
      clientSettings.EnableRowHoverStyle = true;
      clientSettings.AllowRowsDragDrop = false;
      clientSettings.Resizing.AllowColumnResize = false;
    }

    /// <summary>Adds a new column to FileExplorer grid.</summary>
    /// <param name="columnType">Type of the grid column.</param>
    /// <param name="grid">The file explorer grid.</param>
    /// <param name="headerText">The header text of the grid column.</param>
    /// <param name="uniqueName">The unique name of the grid column.</param>
    /// <param name="dataField">The string specifying the field from the data source.</param>
    /// <param name="sortable">Indicates if the grid column is sortable.</param>
    /// <param name="width">The width of the grid column.</param>
    /// <param name="index">The index of the column in the collection.</param>
    private void AddGridColumn(
      string columnType,
      RadGrid grid,
      string headerText,
      string uniqueName,
      string dataField,
      bool sortable,
      int? width,
      int index)
    {
      this.RemoveGridColumn(grid, uniqueName);
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
        grid.Columns.AddAt(index, column);
      else
        grid.Columns.Add(column);
    }

    /// <summary>
    /// Removes the grid column from the GridColumnCollection.
    /// </summary>
    /// <param name="grid">The grid control.</param>
    /// <param name="uniqueName">The unique name of the column.</param>
    private void RemoveGridColumn(RadGrid grid, string uniqueName)
    {
      GridColumn byUniqueNameSafe = grid.Columns.FindByUniqueNameSafe(uniqueName);
      if (byUniqueNameSafe == null)
        return;
      grid.Columns.Remove(byUniqueNameSafe);
    }

    /// <summary>Creates a context menu item.</summary>
    /// <param name="value">The value associated with the menu item.</param>
    /// <param name="text">The text associated with the menu item.</param>
    /// <param name="postBack">A value indicating whether clicking on the item will postback.</param>
    /// <returns>RadMenuItem object.</returns>
    private RadMenuItem CreateContextMenuItem(string value, string text, bool postBack)
    {
      RadMenuItem contextMenuItem = new RadMenuItem();
      contextMenuItem.Value = value;
      contextMenuItem.Text = text;
      contextMenuItem.PostBack = postBack;
      contextMenuItem.EnableViewState = false;
      contextMenuItem.SelectedCssClass = "rfeNoClass";
      return contextMenuItem;
    }

    /// <summary>Adds the tool bar button.</summary>
    /// <param name="toolbar">The toolbar.</param>
    /// <param name="value">The value.</param>
    /// <param name="text">The text.</param>
    /// <param name="CssClass">The CSS class.</param>
    /// <returns></returns>
    private void AddToolBarButton(RadToolBar toolbar, string value, string text, string CssClass)
    {
      RadToolBarButton radToolBarButton = new RadToolBarButton(text);
      radToolBarButton.Value = value;
      radToolBarButton.Text = !string.IsNullOrEmpty(text) ? text : "&nbsp";
      radToolBarButton.ToolTip = value;
      radToolBarButton.CssClass = !string.IsNullOrEmpty(text) ? string.Empty : "rtbIconOnly ";
      radToolBarButton.CssClass += CssClass;
      radToolBarButton.EnableViewState = false;
      toolbar.Items.Add((RadToolBarItem) radToolBarButton);
    }

    /// <summary>Compares file browser items by date modified.</summary>
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
        DateTime t1 = DateTime.Parse(x.Attributes["DateModified"]);
        DateTime t2 = DateTime.Parse(y.Attributes["DateModified"]);
        return x is DirectoryItem ? (y is DirectoryItem ? DateTime.Compare(t1, t2) : -1) : (y is DirectoryItem ? 1 : DateTime.Compare(t1, t2));
      }
    }

    /// <summary>Compares file browser items by type.</summary>
    protected class TypeComparer : IComparer<FileBrowserItem>
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
        string attribute1 = x.Attributes["Type"];
        string attribute2 = y.Attributes["Type"];
        return x is DirectoryItem ? (y is DirectoryItem ? StringComparer.InvariantCultureIgnoreCase.Compare(attribute1, attribute2) : -1) : (y is DirectoryItem ? 1 : StringComparer.InvariantCultureIgnoreCase.Compare(attribute1, attribute2));
      }
    }
  }
}
