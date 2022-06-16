// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical
{
  /// <summary>
  /// Control which displays the list of hierarchical taxons for a given hierarchical taxonomy
  /// </summary>
  public class HierarchicalTaxonList : TaxonList
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.Hierarchical.HierarchicalTaxonList.ascx");
    private const string moveItemUpServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/moveup/";
    private const string moveItemDownServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/movedown/";
    private const string batchMoveItemsUpServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/batchmoveup/";
    private const string batchMoveItemsDownServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/batchmovedown/";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? HierarchicalTaxonList.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the control which is used to display hierarchical items in the
    /// the grid / tree table control.
    /// </summary>
    protected ItemsListBase HierarchicalTaxonGrid => this.Container.GetControl<ItemsListBase>("hierarchicalTaxa", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the url of the webservice
    /// used to manage hierarchical taxonomies.
    /// </summary>
    protected HiddenField ServiceBaseUrl => this.Container.GetControl<HiddenField>("serviceBaseUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the pageId of the currently
    /// managed hierarchical taxonomy.
    /// </summary>
    protected HiddenField TaxonomyId => this.Container.GetControl<HiddenField>("taxonomyId", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the name of the single taxon
    /// as defined for the currently managed taxonomy.
    /// </summary>
    protected HiddenField SingleTaxonName => this.Container.GetControl<HiddenField>("singleTaxonName", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the name of the single taxon
    /// as defined for the currently managed taxonomy.
    /// </summary>
    protected HiddenField TaxonomyName => this.Container.GetControl<HiddenField>("taxonomyName", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the url of the web service
    /// used to move a hierarchical taxon one place up inside of its level.
    /// </summary>
    protected HiddenField MoveItemUpServiceUrl => this.Container.GetControl<HiddenField>("moveItemUpServiceUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the url of the web service
    /// used to move a hierarchical taxon one place down inside of its level.
    /// </summary>
    protected HiddenField MoveItemDownServiceUrl => this.Container.GetControl<HiddenField>("moveItemDownServiceUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the url of the web service
    /// used to move a collection of hierarchical taxa one place up inside of their respective
    /// levels.
    /// </summary>
    protected HiddenField BatchMoveItemsUpServiceUrl => this.Container.GetControl<HiddenField>("batchMoveItemsUpServiceUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the url of the web service
    /// used to move a collection of hierarchical taxa one place down inside of their respective
    /// levels.
    /// </summary>
    protected HiddenField BatchMoveItemsDownServiceUrl => this.Container.GetControl<HiddenField>("batchMoveItemsDownServiceUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the isMultilingual flag.
    /// </summary>
    protected HiddenField IsMultilingual => this.Container.GetControl<HiddenField>("isMultilingual", true);

    /// <summary>Gets the permissions for all parameters.</summary>
    /// <value>The permissions for all parameters.</value>
    protected HiddenField PermissionsForAllParameters => this.Container.GetControl<HiddenField>("hPermissionsForAllParameters", true);

    /// <summary>Gets the tree table containing the command items.</summary>
    /// <value>The tree table containing the command items.</value>
    protected ItemsTreeTable TreeTable => this.Container.GetControl<ItemsTreeTable>("hierarchicalTaxa", true);

    /// <summary>Gets the main decision screen.</summary>
    /// <value>The main decision screen.</value>
    protected DecisionScreen MainDecisionScreen => this.Container.GetControl<DecisionScreen>("decisionScreen", true);

    /// <summary>
    /// Gets the delete prompt dialog when multilingual is off, or item has only one translation.
    /// </summary>
    /// <value>The prompt dialog.</value>
    public PromptDialog DeletePromptDialogSingle { get; set; }

    /// <summary>
    /// Gets the delete prompt dialog when multilingual is on.
    /// </summary>
    /// <value>The prompt dialog.</value>
    public PromptDialog DeletePromptDialogAdvanced { get; set; }

    /// <summary>Gets the items list.</summary>
    /// <value>The items list.</value>
    public override ItemsListBase ItemsList => this.HierarchicalTaxonGrid;

    /// <summary>
    /// Gets the reference to the hidden field containg the client id of the delete prompt dialog when multilingual is off.
    /// </summary>
    protected HiddenField DeletePromptDialogSingleHiddenField => this.Container.GetControl<HiddenField>("hDeletePromptDialogSingle", true);

    /// <summary>
    /// Gets the reference to the hidden field containg the client id of the delete prompt dialog when multilingual is on.
    /// </summary>
    protected HiddenField DeletePromptDialogAdvancedHiddenField => this.Container.GetControl<HiddenField>("hDeletePromptDialogAdvanced", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(Control container)
    {
      base.InitializeControls(container);
      this.HierarchicalTaxonGrid.ServiceBaseUrl = this.GenerateServiceUrl(this.HierarchicalTaxonGrid.ServiceBaseUrl);
      this.ServiceBaseUrl.Value = this.HierarchicalTaxonGrid.ServiceBaseUrl;
      this.TaxonomyId.Value = this.Host.Taxonomy.Id.ToString();
      this.SingleTaxonName.Value = (string) this.Host.Taxonomy.TaxonName;
      this.TaxonomyName.Value = this.Host.Taxonomy.Name;
      this.MoveItemUpServiceUrl.Value = this.GenerateServiceUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/moveup/");
      this.MoveItemDownServiceUrl.Value = this.GenerateServiceUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/movedown/");
      this.BatchMoveItemsUpServiceUrl.Value = this.GenerateServiceUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/batchmoveup/");
      this.BatchMoveItemsDownServiceUrl.Value = this.GenerateServiceUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/batchmovedown/");
      this.IsMultilingual.Value = this.AppSettings.Multilingual.ToString();
      this.DeletePromptDialogSingle = ItemsListBase.GetStandartDeleteDialog(Res.Get<Labels>().AreYouSureYouWantToDeleteItem);
      this.DeletePromptDialogSingle.ID = "deletePromptDialogSingle";
      this.DeletePromptDialogAdvanced = ItemsListBase.GetLanguageAwareDeleteDialog(Res.Get<Labels>().WhatDoYouWantToDelete);
      this.DeletePromptDialogAdvanced.ID = "deletePromptDialogAdvanced";
      this.Controls.Add((Control) this.DeletePromptDialogSingle);
      this.Controls.Add((Control) this.DeletePromptDialogAdvanced);
      this.DeletePromptDialogSingleHiddenField.Value = this.DeletePromptDialogSingle.ClientID;
      this.DeletePromptDialogAdvancedHiddenField.Value = this.DeletePromptDialogAdvanced.ClientID;
      this.SetCreateFunctionality();
      bool flag = this.Host.Taxonomy.IsGranted("Taxonomies", "ModifyTaxonomyAndSubTaxons");
      foreach (ToolboxItemBase toolboxItemBase in (IEnumerable<ToolboxItemBase>) this.TreeTable.ToolboxItems.Where<ToolboxItemBase>((Func<ToolboxItemBase, bool>) (item => item is CommandToolboxItem)).Cast<CommandToolboxItem>().Where<CommandToolboxItem>((Func<CommandToolboxItem, bool>) (item => item.CommandName == "create" || item.CommandName == "groupDelete")))
        toolboxItemBase.Visible = flag;
      this.MainDecisionScreen.ActionItems.Where<ActionItem>((Func<ActionItem, bool>) (item => item.CommandName == "NewTaxon")).FirstOrDefault<ActionItem>().Visible = flag;
      this.SetEditFunctionality();
      this.SetMoveFunctionality();
      this.SetChangeParentFunctionality();
      this.SetPermissionsFunctionality();
      ILinkDefinition link = this.GetLink("viewItems");
      if (link == null)
        return;
      link.NavigateUrl = this.GetMarkedItemsUrl();
    }

    private ILinkDefinition GetLink(string commandName) => this.HierarchicalTaxonGrid.Links.Where<ILinkDefinition>((Func<ILinkDefinition, bool>) (l => l.CommandName == commandName)).FirstOrDefault<ILinkDefinition>();

    private void SetCreateFunctionality()
    {
      CommandToolboxItem commandToolboxItem = this.HierarchicalTaxonGrid.ToolboxItems.Where<ToolboxItemBase>((Func<ToolboxItemBase, bool>) (ti => ti is CommandToolboxItem)).Cast<CommandToolboxItem>().Where<CommandToolboxItem>((Func<CommandToolboxItem, bool>) (ti => ti.CommandName == "create")).FirstOrDefault<CommandToolboxItem>();
      if (commandToolboxItem != null)
        commandToolboxItem.Text = Res.Get<TaxonomyResources>().CreateATaxonName.Arrange((object) this.Host.Taxonomy.TaxonName);
      IDialogDefinition dialogDefinition = this.HierarchicalTaxonGrid.Dialogs.Where<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.OpenOnCommandName == "create")).FirstOrDefault<IDialogDefinition>();
      if (dialogDefinition == null)
        return;
      string parametersToAppend = string.Format("TaxonomyTitle={0}&TaxonomyId={1}&TaxonName={2}", (object) this.Host.Taxonomy.Title, (object) this.Host.Taxonomy.Id, (object) this.Host.Taxonomy.TaxonName);
      dialogDefinition.Parameters = this.AppendParameters(dialogDefinition.Parameters, parametersToAppend);
    }

    private void SetEditFunctionality()
    {
      IDialogDefinition dialogDefinition1 = this.HierarchicalTaxonGrid.Dialogs.Where<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.OpenOnCommandName == "edit")).FirstOrDefault<IDialogDefinition>();
      if (dialogDefinition1 != null)
      {
        string parametersToAppend = string.Format("TaxonomyTitle={0}&TaxonomyId={1}&TaxonName={2}", (object) this.Host.Taxonomy.Title, (object) this.Host.Taxonomy.Id, (object) this.Host.Taxonomy.TaxonName);
        dialogDefinition1.Parameters = this.AppendParameters(dialogDefinition1.Parameters, parametersToAppend);
      }
      IDialogDefinition dialogDefinition2 = this.HierarchicalTaxonGrid.Dialogs.Where<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.OpenOnCommandName == "editTaxonomy")).FirstOrDefault<IDialogDefinition>();
      if (dialogDefinition2 == null)
        return;
      string parametersToAppend1 = string.Format("TaxonomyId={0}", (object) this.Host.Taxonomy.Id);
      dialogDefinition2.Parameters = this.AppendParameters(dialogDefinition2.Parameters, parametersToAppend1);
    }

    private string AppendParameters(string parametersToAppendTo, string parametersToAppend)
    {
      string str = parametersToAppendTo == null || !parametersToAppendTo.Contains("?") ? "?" : "&";
      return parametersToAppendTo + str + parametersToAppend;
    }

    private void SetMoveFunctionality()
    {
      MenuToolboxItem menuToolboxItem = this.HierarchicalTaxonGrid.ToolboxItems.Where<ToolboxItemBase>((Func<ToolboxItemBase, bool>) (ti => ti is MenuToolboxItem)).Cast<MenuToolboxItem>().FirstOrDefault<MenuToolboxItem>();
      if (menuToolboxItem == null)
        return;
      MenuCommandItem menuCommandItem = menuToolboxItem.CommandItems.Where<MenuCommandItem>((Func<MenuCommandItem, bool>) (mi => mi.CommandName == "batchChangeParent")).FirstOrDefault<MenuCommandItem>();
      if (menuCommandItem == null)
        return;
      menuCommandItem.Text = menuCommandItem.Text.Arrange((object) this.Host.Taxonomy.TaxonName.ToLower());
    }

    private void SetChangeParentFunctionality()
    {
      IDialogDefinition dialogDefinition = this.HierarchicalTaxonGrid.Dialogs.Where<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.Name == "ChangeParentDialog")).Single<IDialogDefinition>();
      string parametersToAppend = string.Format("TaxonomyTitle={0}&TaxonomyId={1}&TaxonName={2}", (object) this.Host.Taxonomy.Title, (object) this.Host.Taxonomy.Id, (object) this.Host.Taxonomy.TaxonName);
      dialogDefinition.Parameters = this.AppendParameters(dialogDefinition.Parameters, parametersToAppend);
    }

    private string GenerateServiceUrl(string baseUrl) => VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.AppendTrailingSlash(baseUrl) + this.Host.Taxonomy.Id.ToString()));

    private void SetPermissionsFunctionality()
    {
      IDialogDefinition dialogDefinition = this.HierarchicalTaxonGrid.Dialogs.Where<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.OpenOnCommandName == "permissionsForAllItems")).FirstOrDefault<IDialogDefinition>();
      if (dialogDefinition == null)
        return;
      dialogDefinition.Parameters = string.Format("?securedObjectID={0}&title={1}&securedObjectTypeName={2}&managerClassName={3}", (object) this.Host.Taxonomy.Id, (object) string.Format(Res.Get<SecurityResources>().GeneralPermissionsTitle, (object) this.Host.Taxonomy.Title), (object) typeof (HierarchicalTaxonomy).AssemblyQualifiedName, (object) typeof (TaxonomyManager).AssemblyQualifiedName);
    }
  }
}
