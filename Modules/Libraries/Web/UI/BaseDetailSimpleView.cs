// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BaseDetailSimpleView`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// Represents the base class for media content detail view.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class BaseDetailSimpleView<T> : ViewBase, IBrowseAndEditable
    where T : MediaContent
  {
    private LibrariesManager manager;
    private List<BrowseAndEditCommand> commands = new List<BrowseAndEditCommand>();
    private BrowseAndEditToolbar browseAndEditToolbar;

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected LibrariesManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = LibrariesManager.GetManager(this.Host.ControlDefinition.ProviderName);
        return this.manager;
      }
    }

    /// <summary>Gets the details repeater.</summary>
    /// <value>The repeater.</value>
    protected internal virtual RadListView DetailsView => this.Container.GetControl<RadListView>(nameof (DetailsView), true);

    /// <summary>
    /// Represents the browse and edit toolbar for the control
    /// </summary>
    protected virtual BrowseAndEditToolbar BrowseAndEditToolbar
    {
      get
      {
        if (this.browseAndEditToolbar == null)
          this.browseAndEditToolbar = this.Container.GetControl<BrowseAndEditToolbar>("browseAndEditToolbar", false);
        return this.browseAndEditToolbar;
      }
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
      if (definition is IContentViewDetailDefinition)
      {
        if (!(this.Host.DetailItem is T detailItem))
        {
          if (!this.IsDesignMode())
            return;
          this.Controls.Clear();
          this.Controls.Add((Control) new LiteralControl("An item item was not selected or has been deleted. Please select another one."));
          return;
        }
        this.DetailsView.ItemCreated += new EventHandler<RadListViewItemEventArgs>(this.DetailsView_ItemCreated);
        this.DetailsView.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(this.DetailsView_ItemDataBound);
        this.DetailsView.DataSource = (object) new T[1]
        {
          detailItem
        };
      }
      if (!SystemManager.IsBrowseAndEditMode || this.BrowseAndEditToolbar == null)
        return;
      this.SetDefaultBrowseAndEditCommands();
      this.BrowseAndEditToolbar.Commands.AddRange((IEnumerable<BrowseAndEditCommand>) this.commands);
      BrowseAndEditManager.GetCurrent(this.Page)?.Add((IBrowseAndEditToolbar) this.BrowseAndEditToolbar);
    }

    /// <summary>Configures the detail control.</summary>
    /// <param name="listViewItem">The list view item.</param>
    protected abstract void ConfigureDetailControl(RadListViewItem listViewItem);

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
    /// Handles the ItemCreated event of the DetailsView control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadListViewItemEventArgs" /> instance containing the event data.</param>
    private void DetailsView_ItemCreated(object sender, RadListViewItemEventArgs e)
    {
      if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem || !(e.Item.FindControl("commentsListView") is ContentView control))
        return;
      control.DetailItem = this.Host.DetailItem;
    }

    /// <summary>
    /// Handles the ItemDataBound event of the DetailsView control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadListViewItemEventArgs" /> instance containing the event data.</param>
    private void DetailsView_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
        return;
      if (e.Item.FindControl("commentsDetailsView") is ContentView control)
        control.DetailItem = this.Host.DetailItem;
      this.ConfigureDetailControl(e.Item);
    }

    BrowseAndEditToolbar IBrowseAndEditable.BrowseAndEditToolbar => this.BrowseAndEditToolbar;

    public void AddCommands(IList<BrowseAndEditCommand> commands) => this.commands.AddRange((IEnumerable<BrowseAndEditCommand>) commands);

    /// <summary>
    /// Gets the information needed to configure this instance.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public BrowseAndEditableInfo BrowseAndEditableInfo { get; set; }
  }
}
