// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ItemLists.ItemsGrid
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ItemLists
{
  /// <summary>
  /// Implements <see cref="T:Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase" /> with <see cref="T:Telerik.Web.UI.RadGrid" /> and <see cref="T:Telerik.Sitefinity.Web.UI.RadGridBinder" />
  /// </summary>
  /// <remarks>
  /// <para>
  ///     ItemsGrid ignores WrapperTagName and WrapperClientId. Instead, it sets
  ///     WrapperTagCssClass to its inner RadGrid element.
  ///     Note that the class specified by WrapperCssClass is appended to the existing value
  ///     in RadGrid.CssClass.
  /// </para>
  /// </remarks>
  public class ItemsGrid : ItemsListBase
  {
    private bool allowMultipleSelection = true;
    private bool allowRowSelect = true;
    private const string scriptControlPath = "Telerik.Sitefinity.Web.UI.ItemLists.Scripts.ItemsGrid.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ItemLists.ItemsGrid.ascx");

    /// <summary>
    /// Determines wheter ItemsListBase should override Render and manually insert
    /// a wrapper tag that uses <see cref="!:WrapperTagName" />, <see cref="!:WrapperTagClientId" />
    /// and <see cref="!:WrapperTagCssClass" />
    /// </summary>
    protected override bool AutoInsertWrapperTag => false;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ItemsGrid.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow multiple selection.
    /// </summary>
    /// <value></value>
    public override bool AllowMultipleSelection
    {
      get => this.allowMultipleSelection;
      set => this.allowMultipleSelection = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether you will be able to select a grid row
    /// on the client by clicking on it with the mouse.
    /// </summary>
    /// <value>
    /// true, if you will be able to select a row on the client, otherwise false (the
    /// default value).
    /// </value>
    public virtual bool AllowRowSelect
    {
      get => this.allowRowSelect;
      set => this.allowRowSelect = value;
    }

    /// <summary>Reference to the grid containing the data items</summary>
    protected internal RadGrid Grid => this.Container.GetControl<RadGrid>("grid", true);

    /// <summary>
    /// Reference the grid binder that binds the data to the grid.
    /// </summary>
    /// <value>The grid binder.</value>
    protected internal RadGridBinder GridBinder => this.Container.GetControl<RadGridBinder>("gridBinder", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      RadGrid grid = this.Grid;
      grid.CssClass = grid.CssClass + " " + this.WrapperTagCssClass;
      this.Grid.AllowPaging = this.AllowPaging;
      this.Grid.AllowSorting = this.AllowSorting;
      this.Grid.PageSize = this.PageSize;
      this.Grid.AllowMultiRowSelection = this.AllowMultipleSelection;
      this.Grid.ClientSettings.Selecting.AllowRowSelect = this.AllowRowSelect;
      if (!this.AllowRowSelect)
      {
        GridColumn byUniqueNameSafe = this.Grid.Columns.FindByUniqueNameSafe("ClientSelectColumn");
        if (byUniqueNameSafe != null)
          this.Grid.Columns.Remove(byUniqueNameSafe);
      }
      this.Grid.ItemEvent += new GridItemEventHandler(this.ItemGrid_ItemEvent);
      this.ItemAddedToClientBinder += new EventHandler<ItemAddedToClientBinderEventArgs>(this.ItemGrid_ItemAddedToClientBinder);
      base.InitializeControls(container);
    }

    private void ItemGrid_ItemEvent(object sender, GridItemEventArgs e)
    {
      if (!(e.EventInfo is GridInitializePagerItem))
        return;
      e.Canceled = true;
      (e.Item as GridPagerItem).PagerContentCell.Controls.Add((Control) new ClientPager(this.GridBinder));
    }

    private void ItemGrid_ItemAddedToClientBinder(object sender, ItemAddedToClientBinderEventArgs e)
    {
      GridTemplateColumn column = new GridTemplateColumn();
      column.UniqueName = "BinderContainer" + (object) e.Index;
      column.HeaderText = e.Item.HeaderText;
      column.SortExpression = e.Item.SortExpression;
      column.ItemStyle.CssClass = e.Item.ItemCssClass;
      column.HeaderStyle.CssClass = e.Item.HeaderCssClass;
      this.Grid.MasterTableView.Columns.Add((GridColumn) column);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      (source.Last<ScriptDescriptor>() as ScriptControlDescriptor).AddProperty("_gridId", (object) this.Grid.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ItemLists.Scripts.ItemsGrid.js", this.GetType().Assembly.FullName)
    };
  }
}
