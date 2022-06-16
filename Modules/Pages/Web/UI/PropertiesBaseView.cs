// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PropertiesBaseView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// Represents the base view for creating/editing pages, sectons and templates.
  /// </summary>
  public class PropertiesBaseView : SimpleView
  {
    private TaxonomyManager taxonomyManager;
    private DialogModes mode;
    private string layoutTemplateName;
    private string taxonName;
    private string taxonomyName;
    private string selectedNodeUrl;
    private ScriptManager scriptManager;
    private static readonly object EventSaveCommand;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => this.layoutTemplateName;

    /// <summary>Gets or sets the mode of the Properties dialog.</summary>
    /// <value>The mode.</value>
    public DialogModes Mode
    {
      get => this.mode;
      set => this.mode = value;
    }

    /// <summary>Gets the taxonomy manager.</summary>
    /// <value>The taxonomy manager.</value>
    public TaxonomyManager TaxonomyManager
    {
      get => this.taxonomyManager;
      set => this.taxonomyManager = value;
    }

    /// <summary>Gets or sets the name of the taxon.</summary>
    /// <value>The name of the taxon.</value>
    public string TaxonName
    {
      get => this.taxonName;
      set => this.taxonName = value;
    }

    /// <summary>Gets or sets the name of the taxonomy.</summary>
    /// <value>The name of the taxonomy.</value>
    public string TaxonomyName
    {
      get => this.taxonomyName;
      set => this.taxonomyName = value;
    }

    /// <summary>Gets or sets the URL of the selected node.</summary>
    /// <value>The selected node URL.</value>
    public string SelectedNodeUrl
    {
      get => this.selectedNodeUrl;
      set => this.selectedNodeUrl = value;
    }

    /// <summary>Occurs when Save button control is clicked.</summary>
    public event CommandEventHandler SaveCommand
    {
      add => this.Events.AddHandler(PropertiesBaseView.EventSaveCommand, (Delegate) value);
      remove => this.Events.RemoveHandler(PropertiesBaseView.EventSaveCommand, (Delegate) value);
    }

    /// <summary>Gets or sets the text of menuName control.</summary>
    /// <value>The menu name text.</value>
    public string MenuNameText
    {
      get => this.MenuName.Text;
      set => this.MenuName.Text = value;
    }

    /// <summary>Gets or sets the text of urlName control.</summary>
    /// <value>The URL name text.</value>
    public string UrlNameText
    {
      get => this.UrlName.Text;
      set => this.UrlName.Text = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether showInNavigation control is checked.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if showInNavigation control is checked; otherwise, <c>false</c>.
    /// </value>
    public bool ShowInNavigationChecked
    {
      get => this.ShowInNavigation.Checked;
      set => this.ShowInNavigation.Checked = value;
    }

    /// <summary>Gets or sets the text of title control.</summary>
    /// <value>The title text.</value>
    public string TitleText
    {
      get => this.Title.Text;
      set => this.Title.Text = value;
    }

    /// <summary>Gets or sets the text of description control.</summary>
    /// <value>The description text.</value>
    public string DescriptionText
    {
      get => this.Description.Text;
      set => this.Description.Text = value;
    }

    /// <summary>Gets or sets the text of keywords control.</summary>
    /// <value>The keywords text.</value>
    public string KeywordsText
    {
      get => this.Keywords.Text;
      set => this.Keywords.Text = value;
    }

    /// <summary>Gets or sets the cancel button text.</summary>
    /// <value>The cancel button text.</value>
    public string CancelText
    {
      get => this.Cancel.InnerHtml;
      set => this.Cancel.InnerHtml = value;
    }

    /// <summary>Gets the client ID of topLevelRadio control.</summary>
    /// <value>The top level radio client ID.</value>
    public string TopLevelRadioClientID => ((Control) this.TopLevelRadio).ClientID;

    private ScriptManager CurrentScriptManager
    {
      get
      {
        if (this.scriptManager == null)
          this.scriptManager = ScriptManager.GetCurrent(this.Page);
        return this.scriptManager;
      }
    }

    /// <summary>Gets the menuName text box.</summary>
    /// <value>The menuName text box.</value>
    protected virtual ITextControl MenuName => this.Container.GetControl<ITextControl>("menuName", true);

    /// <summary>Gets the urlName text box.</summary>
    /// <value>The urlName text box.</value>
    protected virtual ITextControl UrlName => this.Container.GetControl<ITextControl>("urlName", true);

    /// <summary>Gets the generatedUrlName label.</summary>
    /// <value>The generatedUrlName label.</value>
    protected virtual ITextControl GeneratedUrlName => this.Container.GetControl<ITextControl>("generatedUrlName", true);

    /// <summary>Gets the topLevelRadio radio button.</summary>
    /// <value>The topLevelRadio radio button.</value>
    protected virtual ICheckBoxControl TopLevelRadio => this.Container.GetControl<ICheckBoxControl>("topLevelRadio", true);

    /// <summary>Gets the selectedSectionRadio radio button.</summary>
    /// <value>The selectedSectionRadio radio button.</value>
    protected virtual ICheckBoxControl SelectedSectionRadio => this.Container.GetControl<ICheckBoxControl>("selectedSectionRadio", true);

    /// <summary>Gets the sectionsTreeview control.</summary>
    /// <value>The sectionsTreeview control.</value>
    public virtual RadTreeView SectionsTreeview => this.Container.GetControl<RadTreeView>("sectionsTreeview", true);

    /// <summary>Gets the selectedNodeValue hidden field.</summary>
    /// <value>The selectedNodeValue hidden field.</value>
    protected virtual HiddenField SelectedNodeValueField => this.Container.GetControl<HiddenField>("selectedNodeValue", true);

    /// <summary>Gets the selectedNodeUrl hidden field.</summary>
    /// <value>The selectedNodeUrl hidden field.</value>
    protected virtual HiddenField SelectedNodeUrlField => this.Container.GetControl<HiddenField>("selectedNodeUrl", true);

    /// <summary>Gets the saveLink1 button.</summary>
    /// <value>The saveLink1 button.</value>
    protected virtual IButtonControl SaveLink1 => this.Container.GetControl<IButtonControl>("saveLink1", true);

    /// <summary>Gets the saveLink2 button.</summary>
    /// <value>The saveLink2 button.</value>
    protected virtual IButtonControl SaveLink2 => this.Container.GetControl<IButtonControl>("saveLink2", false);

    /// <summary>Gets the showInNavigation check box.</summary>
    /// <value>The showInNavigation control.</value>
    protected virtual ICheckBoxControl ShowInNavigation => this.Container.GetControl<ICheckBoxControl>("showInNavigation", false);

    /// <summary>Gets the title control.</summary>
    /// <value>The title control.</value>
    protected virtual ITextControl Title => this.Container.GetControl<ITextControl>("title", false);

    /// <summary>Gets the description control.</summary>
    /// <value>The description control.</value>
    protected virtual ITextControl Description => this.Container.GetControl<ITextControl>("description", false);

    /// <summary>Gets the keywords control.</summary>
    /// <value>The keywords control.</value>
    protected virtual ITextControl Keywords => this.Container.GetControl<ITextControl>("keywords", false);

    /// <summary>Gets the message control.</summary>
    /// <value>The message control.</value>
    protected virtual Message MsgControl => this.Container.GetControl<Message>("message", true);

    /// <summary>Gets the cancel control.</summary>
    /// <value>The cancel control.</value>
    protected virtual HtmlContainerControl Cancel => this.Container.GetControl<HtmlContainerControl>("cancel", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "setAppPath", "var appPath = '" + this.Context.Request.ApplicationPath + "';", true);
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      string str1 = this.Page.Request.QueryString["currDir"];
      string str2 = this.Page.Request.QueryString["initialPath"];
      if (!string.IsNullOrEmpty(str1))
      {
        int num = str1.IndexOf("/");
        if (num > -1)
        {
          this.TaxonName = string.IsNullOrEmpty(str2) || str2.IndexOf("/") <= -1 ? str1.Substring(0, num) : str2;
          this.SelectedNodeUrl = str1.Substring(num);
        }
        else
          this.TaxonName = str1;
      }
      else
        this.TaxonName = str2;
      RadTreeNodeBinding radTreeNodeBinding = new RadTreeNodeBinding();
      radTreeNodeBinding.TextField = "Name";
      radTreeNodeBinding.ValueField = "Id";
      this.SectionsTreeview.EnableDragAndDrop = false;
      this.SectionsTreeview.PersistLoadOnDemandNodes = true;
      this.SectionsTreeview.NodeExpand += new RadTreeViewEventHandler(this.SectionsTreeview_NodeExpand);
      this.SectionsTreeview.DataBindings.Add((NavigationItemBinding) radTreeNodeBinding);
      if (this.SectionsTreeview.Nodes.Count == 0)
      {
        bool recursively = !string.IsNullOrEmpty(this.SelectedNodeUrl);
        this.BindTreeview(recursively);
        if (recursively)
        {
          RadTreeNode nodeByAttribute = this.SectionsTreeview.FindNodeByAttribute("Url", this.SelectedNodeUrl);
          if (nodeByAttribute != null)
          {
            this.SelectedNodeUrlField.Value = this.SelectedNodeUrl;
            this.SelectedNodeValueField.Value = nodeByAttribute.Value;
            nodeByAttribute.Selected = true;
            nodeByAttribute.ExpandParentNodes();
          }
        }
      }
      this.CurrentScriptManager.RegisterAsyncPostBackControl((Control) this.SaveLink1);
      this.SaveLink1.Command += new CommandEventHandler(this.SaveLink_Command);
      this.SaveLink1.CommandName = this.Mode != DialogModes.Create ? "SaveChanges" : "Create";
      if (this.SaveLink2 != null)
      {
        this.CurrentScriptManager.RegisterAsyncPostBackControl((Control) this.SaveLink2);
        this.SaveLink2.Command += new CommandEventHandler(this.SaveLink_Command);
        this.SaveLink2.CommandName = "CreateAndAddAnother";
      }
      this.SetProperties();
    }

    private RadTreeNode CreateTreeNode(ITaxon taxon)
    {
      RadTreeNode treeNode = new RadTreeNode();
      treeNode.Text = (string) taxon.Title;
      treeNode.Value = taxon.Id.ToString();
      treeNode.Attributes.Add("Url", ((HierarchicalTaxon) taxon).GetPath(true, true));
      return treeNode;
    }

    private void PopulateTreeNode(RadTreeNode currNode, IList<HierarchicalTaxon> list)
    {
      if (currNode == null)
        return;
      currNode.Nodes.Clear();
      currNode.ExpandMode = TreeNodeExpandMode.ClientSide;
      currNode.Expanded = true;
      foreach (ITaxon taxon in (IEnumerable<HierarchicalTaxon>) list)
      {
        RadTreeNode treeNode = this.CreateTreeNode(taxon);
        if (((HierarchicalTaxon) taxon).Subtaxa.Count > 0)
          treeNode.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
        currNode.Nodes.Add(treeNode);
      }
    }

    private void FillTreeview(
      IList<HierarchicalTaxon> taxa,
      RadTreeNodeCollection pNodes,
      bool recursively)
    {
      pNodes.Clear();
      foreach (HierarchicalTaxon taxon in (IEnumerable<HierarchicalTaxon>) taxa)
      {
        RadTreeNode treeNode = this.CreateTreeNode((ITaxon) taxon);
        pNodes.Add(treeNode);
        if (recursively)
          this.FillTreeview(taxon.Subtaxa, treeNode.Nodes, recursively);
        else
          treeNode.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
      }
    }

    /// <summary>Binds the treeview.</summary>
    /// <param name="recursively">if set to <c>true</c> the treeview will be binded recursively.</param>
    /// <param name="clearNodes">if set to <c>true</c> all treeview nodes will be removed.</param>
    public void BindTreeview(bool recursively)
    {
      HierarchicalTaxon navigationTaxon = PageBrowserContentProvider.GetNavigationTaxon(this.TaxonName, this.TaxonomyName, this.TaxonomyManager);
      if (navigationTaxon == null)
        return;
      RadTreeNodeCollection nodes = this.SectionsTreeview.Nodes;
      this.FillTreeview(navigationTaxon.Subtaxa, nodes, recursively);
    }

    /// <summary>Gets the section path.</summary>
    /// <param name="taxonName">Name of the taxon.</param>
    /// <param name="taxManager">The taxonomy manager.</param>
    /// <returns></returns>
    public string GetSectionPath(string taxonName, TaxonomyManager taxManager)
    {
      string url = taxonName;
      if (this.SelectedSectionRadio.Checked && Utility.IsGuid(this.SelectedNodeValueField.Value))
      {
        Guid id = new Guid(this.SelectedNodeValueField.Value);
        HierarchicalTaxon taxon = (HierarchicalTaxon) taxManager.GetTaxon(id);
        if (taxon != null)
          url = taxon.GetPath(false, true);
      }
      return RouteHelper.ResolveUrl(url, UrlResolveOptions.AppendTrailingSlash);
    }

    /// <summary>Sets the properties.</summary>
    protected virtual void SetProperties()
    {
    }

    /// <summary>
    /// Raises the <see cref="E:SaveCommand" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
    protected virtual void OnSaveCommand(CommandEventArgs e)
    {
      CommandEventHandler commandEventHandler = (CommandEventHandler) this.Events[PropertiesBaseView.EventSaveCommand];
      if (commandEventHandler == null)
        return;
      commandEventHandler((object) this, e);
    }

    /// <summary>Shows the positive message.</summary>
    /// <param name="message">The message.</param>
    public void ShowPositiveMessage(string message) => this.MsgControl.ShowPositiveMessage(message);

    /// <summary>Shows the negative message.</summary>
    /// <param name="message">The message.</param>
    public void ShowNegativeMessage(string message) => this.MsgControl.ShowNegativeMessage(message);

    private void SectionsTreeview_NodeExpand(object sender, RadTreeNodeEventArgs e)
    {
      RadTreeNode node = e.Node;
      HierarchicalTaxon taxon = (HierarchicalTaxon) this.TaxonomyManager.GetTaxon(new Guid(node.Value));
      if (node.Nodes.Count != 0 || taxon == null)
        return;
      this.PopulateTreeNode(node, taxon.Subtaxa);
    }

    private void SaveLink_Command(object sender, CommandEventArgs e) => this.OnSaveCommand(e);
  }
}
