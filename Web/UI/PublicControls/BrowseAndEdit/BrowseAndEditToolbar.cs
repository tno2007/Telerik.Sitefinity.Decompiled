// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  /// <summary>
  /// Representing a class for toolbars used for browse and edit functionality
  /// </summary>
  public class BrowseAndEditToolbar : SimpleScriptView, IBrowseAndEditToolbar
  {
    private IDictionary<Control, BrowseAndEditCommand> toolbarControls;
    private List<BrowseAndEditCommand> commands;
    private bool currentSiteMapNodeSet;
    private PageSiteNode currentSiteMapNode;
    private bool currentPageNodeSet;
    private PageNode currentPageNode;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.BrowseAndEditToolbarControl.ascx");
    internal const string browseAndEditToolbarScript = "Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.Scripts.BrowseAndEditToolbar.js";
    private bool? visible;

    public IDictionary<Control, BrowseAndEditCommand> ToolbarControls
    {
      get
      {
        if (this.toolbarControls == null)
          this.toolbarControls = (IDictionary<Control, BrowseAndEditCommand>) new Dictionary<Control, BrowseAndEditCommand>();
        return this.toolbarControls;
      }
      set => this.toolbarControls = value;
    }

    /// <summary>Gets or sets the Commands for the toolbar.</summary>
    /// <value>The Commands.</value>
    public List<BrowseAndEditCommand> Commands
    {
      get
      {
        if (this.commands == null)
          this.commands = new List<BrowseAndEditCommand>();
        return this.commands;
      }
      set => this.commands = value;
    }

    /// <summary>Gets or sets the ProviderName for the toolbar.</summary>
    /// <value>The ProviderName.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the ItemId for the toolbar.</summary>
    /// <value>The ItemId.</value>
    public Guid ItemId { get; set; }

    /// <summary>Gets or sets the ParentId for the new items.</summary>
    /// <value>The ParentId.</value>
    public Guid ParentId { get; set; }

    /// <summary>Gets or sets the ItemType for the toolbar.</summary>
    /// <value>The ItemType.</value>
    public Type ItemType { get; set; }

    /// <summary>Gets or sets the ViewName for the toolbar.</summary>
    /// <value>The ViewName.</value>
    public string ViewName { get; set; }

    /// <summary>
    /// Gets or sets the toolbar mode.
    /// Setting this property will configure which buttons to be show.
    /// </summary>
    /// <value>The mode.</value>
    public BrowseAndEditToolbarMode Mode { get; set; }

    /// <summary>Gets the current site map node.</summary>
    /// <value>The current site map node.</value>
    protected PageSiteNode CurrentSiteMapNode
    {
      get
      {
        if (!this.currentSiteMapNodeSet)
        {
          this.currentSiteMapNode = SiteMapBase.GetActualCurrentNode();
          this.currentSiteMapNodeSet = true;
        }
        return this.currentSiteMapNode;
      }
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> object for the current site map node.
    /// </summary>
    /// <value>The current PageNode object.</value>
    protected PageNode CurrentPageNode
    {
      get
      {
        if (!this.currentPageNodeSet)
        {
          PageSiteNode currentSiteMapNode = this.CurrentSiteMapNode;
          if (currentSiteMapNode != null)
            this.currentPageNode = PageManager.GetManager().GetPageNode(currentSiteMapNode.Id);
          this.currentPageNodeSet = true;
        }
        return this.currentPageNode;
      }
    }

    /// <summary>Gets the toolbar container</summary>
    protected Panel ToolbarContainer => this.Container.GetControl<Panel>(nameof (BrowseAndEditToolbar), false);

    /// <summary>Gets or sets the visibility.</summary>
    /// <value>The visibility.</value>
    public override bool Visible
    {
      get => this.visible.HasValue ? this.visible.Value : SystemManager.IsBrowseAndEditMode;
      set => this.visible = new bool?(value);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (SystemManager.IsBrowseAndEditMode)
        base.OnInit(e);
      else
        this.Visible = false;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      if (this.Visible && this.Commands.Where<BrowseAndEditCommand>((Func<BrowseAndEditCommand, bool>) (c => c.Visible)).ToList<BrowseAndEditCommand>().Count == 0)
        this.Visible = false;
      base.OnPreRender(e);
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>Overriden method GetScriptDescriptors</summary>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      Dictionary<string, BrowseAndEditCommand> dictionary = new Dictionary<string, BrowseAndEditCommand>();
      foreach (KeyValuePair<Control, BrowseAndEditCommand> toolbarControl in (IEnumerable<KeyValuePair<Control, BrowseAndEditCommand>>) this.ToolbarControls)
        dictionary.Add(toolbarControl.Key.ClientID, toolbarControl.Value);
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (BrowseAndEditToolbar).FullName, this.ClientID);
      controlDescriptor.AddProperty("toolbarContainerID", (object) this.ToolbarContainer.ClientID);
      controlDescriptor.AddProperty("toolbarControls", (object) dictionary);
      if (this.ItemId != Guid.Empty)
        controlDescriptor.AddProperty("itemId", (object) this.ItemId);
      if (this.ItemType != (Type) null)
        controlDescriptor.AddProperty("itemType", (object) this.ItemType.FullName);
      controlDescriptor.AddProperty("providerName", (object) this.ProviderName);
      if (this.ParentId != Guid.Empty)
        controlDescriptor.AddProperty("parentId", (object) this.ParentId);
      controlDescriptor.AddProperty("visible", (object) false);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>Overriden method GetScriptReferences</summary>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string str = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.Scripts.BrowseAndEditToolbar.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Overriden method InitializeControls</summary>
    /// <param name="container">The container</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.Style.Add("display", "none");
      foreach (Control control in this.ToolbarContainer.Controls)
        control.Visible = false;
      this.InitializeCommands();
    }

    /// <summary>Gets the layout template name</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BrowseAndEditToolbar.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Initializes the commands, which will be used to open BrowseAndEdit dialogs.
    /// </summary>
    protected virtual void InitializeCommands()
    {
      int count = this.Commands.Count;
      while (--count >= 0)
      {
        if (!this.IsGrantedPermission(this.Commands[count]))
          this.Commands.RemoveAt(count);
      }
      List<BrowseAndEditCommand> list = this.Commands.Where<BrowseAndEditCommand>((Func<BrowseAndEditCommand, bool>) (c => c.Visible)).ToList<BrowseAndEditCommand>();
      if (list.Count > 0)
      {
        HtmlGenericControl child = new HtmlGenericControl("ul");
        this.ToolbarContainer.Controls.Add((Control) child);
        foreach (BrowseAndEditCommand browseAndEditCommand in list)
        {
          HtmlGenericControl htmlGenericControl = new HtmlGenericControl("li");
          htmlGenericControl.InnerText = browseAndEditCommand.CommandTitle;
          htmlGenericControl.Visible = true;
          child.Controls.Add((Control) htmlGenericControl);
          this.ToolbarControls.Add((Control) htmlGenericControl, browseAndEditCommand);
        }
      }
      foreach (BrowseAndEditCommand browseAndEditCommand1 in this.Commands.Where<BrowseAndEditCommand>((Func<BrowseAndEditCommand, bool>) (c => !c.Visible)))
      {
        IDictionary<Control, BrowseAndEditCommand> toolbarControls = this.ToolbarControls;
        Control key = new Control();
        key.ID = browseAndEditCommand1.CommandName;
        key.Visible = false;
        BrowseAndEditCommand browseAndEditCommand2 = browseAndEditCommand1;
        toolbarControls.Add(key, browseAndEditCommand2);
      }
    }

    /// <summary>
    /// Determines whether the specified command is granted permission.
    /// </summary>
    /// <param name="command">The command to be checked.</param>
    /// <returns>
    /// 	<c>true</c> if the specified command is granted permission; otherwise, <c>false</c>.
    /// </returns>
    protected virtual bool IsGrantedPermission(BrowseAndEditCommand command)
    {
      bool flag = true;
      if (command.UsesPagePermissions)
      {
        PageNode currentPageNode = this.CurrentPageNode;
        if (currentPageNode != null)
          flag = this.IsContentEditable(currentPageNode);
      }
      return flag;
    }

    /// <summary>
    /// Determines whether the user can edit the contents of the specified page.
    /// </summary>
    /// <param name="page">The page node to be checked.</param>
    /// <returns>
    /// 	<c>true</c> if the user can edit the contents of the specified page; otherwise, <c>false</c>.
    /// </returns>
    protected bool IsContentEditable(PageNode page)
    {
      if (page == null)
        return true;
      return page.IsGranted("Pages", "EditContent");
    }
  }
}
