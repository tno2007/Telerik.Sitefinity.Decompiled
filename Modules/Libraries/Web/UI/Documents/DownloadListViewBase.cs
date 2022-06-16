// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.DownloadListViewBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents
{
  /// <summary>
  /// Base class for all documents views of DownloadListView control.
  /// </summary>
  public abstract class DownloadListViewBase : ViewBase, IBrowseAndEditable
  {
    private bool isControlDefinitionProviderCorrect = true;
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
          this.InitializeManager();
        return this.manager;
      }
    }

    /// <summary>Gets or sets the query.</summary>
    /// <value>The query.</value>
    protected IQueryable<Document> Query { get; set; }

    /// <summary>Gets or sets the total count.</summary>
    /// <value>The total count.</value>
    protected int TotalCount { get; set; }

    /// <summary>Gets or sets the type of the thumbnail.</summary>
    /// <value>The type of the thumbnail.</value>
    protected ThumbnailType ThumbnailType { get; set; }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("title", false);

    private void InitializeManager()
    {
      try
      {
        this.manager = LibrariesManager.GetManager(this.Host.ControlDefinition.ProviderName);
      }
      catch (ConfigurationErrorsException ex)
      {
        this.manager = LibrariesManager.GetManager();
        this.isControlDefinitionProviderCorrect = false;
      }
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
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
      if (this.TitleLabel != null && this.Host is MediaContentView)
        this.TitleLabel.Text = (this.Host as MediaContentView).Title;
      this.InitializeManager();
      if (!this.isControlDefinitionProviderCorrect)
        return;
      if (definition != null)
        this.ConfigureDocumentsQuery(definition);
      this.IsEmptyView = this.TotalCount == 0;
      if (!SystemManager.IsBrowseAndEditMode || this.BrowseAndEditToolbar == null)
        return;
      this.SetDefaultBrowseAndEditCommands();
      this.BrowseAndEditToolbar.Commands.AddRange((IEnumerable<BrowseAndEditCommand>) this.commands);
      BrowseAndEditManager.GetCurrent(this.Page)?.Add((IBrowseAndEditToolbar) this.BrowseAndEditToolbar);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.DataBindDocumentList();
    }

    /// <summary>Configures the documents query.</summary>
    /// <param name="definition">The content view definition for DowloadListView control.</param>
    protected abstract void ConfigureDocumentsQuery(IContentViewDefinition definition);

    /// <summary>
    /// Handles the DataBound event of the DocumentsGrid control.
    /// </summary>
    public abstract void DataBindDocumentList();

    /// <summary>
    /// Analyse the number of bytes and depending on the range, represents the them at corresponding range i.e bytes, KB, MB, GB, TB.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    /// <returns></returns>
    public string ConfigureBytes(long bytes)
    {
      string empty = string.Empty;
      string str;
      if (bytes == 0L)
      {
        str = "0 KB";
      }
      else
      {
        string[] strArray = new string[6]
        {
          nameof (bytes),
          "KB",
          "MB",
          "GB",
          "TB",
          "PB"
        };
        double d1 = Math.Floor(Math.Log((double) bytes) / Math.Log(1024.0));
        if (d1 == 0.0)
        {
          double d2 = d1 + 1.0;
          str = Math.Ceiling((double) bytes / Math.Pow(1024.0, Math.Floor(d2))).ToString() + " " + strArray[(int) d2];
        }
        else
          str = string.Format("{0:0.00}", (object) ((double) bytes / Math.Pow(1024.0, Math.Floor(d1)))) + " " + strArray[(int) d1];
      }
      return str;
    }

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
    /// Represents the browse and edit toolbar for the control
    /// </summary>
    protected virtual BrowseAndEditToolbar BrowseAndEditToolbar
    {
      get
      {
        if (this.browseAndEditToolbar == null)
          this.browseAndEditToolbar = this.Container.GetControl<BrowseAndEditToolbar>(nameof (BrowseAndEditToolbar), false);
        return this.browseAndEditToolbar;
      }
    }

    BrowseAndEditToolbar IBrowseAndEditable.BrowseAndEditToolbar => this.BrowseAndEditToolbar;

    /// <summary>
    /// Adds browse and edit commands to be executed by the toolbar
    /// </summary>
    /// <param name="commands">The commands.</param>
    public void AddCommands(IList<BrowseAndEditCommand> commands) => this.commands.AddRange((IEnumerable<BrowseAndEditCommand>) commands);

    /// <summary>
    /// Gets the information needed to configure this instance.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public BrowseAndEditableInfo BrowseAndEditableInfo { get; set; }
  }
}
