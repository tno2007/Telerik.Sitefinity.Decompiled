// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Flat.FlatTaxonList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
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

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Flat
{
  /// <summary>
  /// Control which displays the list of flat taxons for a given flat taxonomy
  /// </summary>
  public class FlatTaxonList : TaxonList
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.Flat.FlatTaxonList.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FlatTaxonList.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the flat taxa grid.</summary>
    /// <value>The flat taxa grid.</value>
    protected ItemsGrid FlatTaxaGrid => this.Container.GetControl<ItemsGrid>("flatTaxa", true);

    /// <summary>
    /// Gets the reference to hPermissionsForAllParameters hidden field.
    /// </summary>
    protected HiddenField PermissionsForAllParameters => this.Container.GetControl<HiddenField>("hPermissionsForAllParameters", true);

    /// <summary>
    /// Gets the reference to the hidden field used to store the isMultilingual flag.
    /// </summary>
    protected HiddenField IsMultilingual => this.Container.GetControl<HiddenField>("isMultilingual", true);

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

    /// <summary>Gets the flat taxa items grid.</summary>
    /// <value>The flat taxa items grid.</value>
    protected ItemsGrid FlatTaxaItemsGrid => this.Container.GetControl<ItemsGrid>("flatTaxa", true);

    /// <summary>Gets the main decision screen.</summary>
    /// <value>The main decision screen.</value>
    protected DecisionScreen MainDecisionScreen => this.Container.GetControl<DecisionScreen>("decisionScreen", true);

    /// <summary>Gets the items list.</summary>
    /// <value>The items list.</value>
    public override ItemsListBase ItemsList => (ItemsListBase) this.FlatTaxaGrid;

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
      this.FlatTaxaGrid.ServiceBaseUrl = VirtualPathUtility.AppendTrailingSlash(this.FlatTaxaGrid.ServiceBaseUrl);
      this.FlatTaxaGrid.ServiceBaseUrl += this.Host.Taxonomy.Id.ToString();
      this.IsMultilingual.Value = this.AppSettings.Multilingual.ToString();
      this.DeletePromptDialogSingle = ItemsListBase.GetStandartDeleteDialog(Res.Get<Labels>().AreYouSureYouWantToDeleteItem);
      this.DeletePromptDialogSingle.ID = "deletePromptDialogSingle";
      this.DeletePromptDialogAdvanced = ItemsListBase.GetLanguageAwareDeleteDialog(Res.Get<Labels>().WhatDoYouWantToDelete);
      this.DeletePromptDialogAdvanced.ID = "deletePromptDialogAdvanced";
      this.Controls.Add((Control) this.DeletePromptDialogSingle);
      this.Controls.Add((Control) this.DeletePromptDialogAdvanced);
      this.DeletePromptDialogSingleHiddenField.Value = this.DeletePromptDialogSingle.ClientID;
      this.DeletePromptDialogAdvancedHiddenField.Value = this.DeletePromptDialogAdvanced.ClientID;
      this.SetCommands();
      this.SetCommandPermissions();
    }

    private void SetCommands()
    {
      CommandToolboxItem commandToolboxItem = this.FlatTaxaGrid.ToolboxItems.Where<ToolboxItemBase>((Func<ToolboxItemBase, bool>) (ti => ti is CommandToolboxItem)).Cast<CommandToolboxItem>().Where<CommandToolboxItem>((Func<CommandToolboxItem, bool>) (ti => ti.CommandName == "create")).FirstOrDefault<CommandToolboxItem>();
      if (commandToolboxItem != null)
        commandToolboxItem.Text = Res.Get<TaxonomyResources>().CreateATaxonName.Arrange((object) this.Host.Taxonomy.TaxonName.ToLower());
      IDialogDefinition dialog1 = this.GetDialog("create");
      if (dialog1 != null)
        dialog1.Parameters = string.Format("?TaxonomyId={0}&TaxonId={1}&TaxonomyTitle={2}&TaxonName={3}", (object) this.Host.Taxonomy.Id, (object) Guid.Empty, (object) this.Host.Taxonomy.Title, (object) this.Host.Taxonomy.TaxonName);
      IDialogDefinition dialog2 = this.GetDialog("edit");
      if (dialog2 != null)
        dialog2.Parameters += string.Format("&TaxonomyTitle={0}&TaxonName={1}", (object) this.Host.Taxonomy.Title, (object) this.Host.Taxonomy.TaxonName);
      IDialogDefinition dialog3 = this.GetDialog("editTaxonomy");
      if (dialog3 != null)
        dialog3.Parameters = string.Format("?TaxonomyId={0}", (object) this.Host.Taxonomy.Id);
      IDialogDefinition dialog4 = this.GetDialog("bulkEdit");
      if (dialog4 != null)
        dialog4.Parameters = string.Format("?TaxonomyId={0}&TaxonomyTitle={1}&TaxonName={2}", (object) this.Host.Taxonomy.Id, (object) this.Host.Taxonomy.Title, (object) this.Host.Taxonomy.TaxonName);
      IDialogDefinition dialog5 = this.GetDialog("permissionsForAllItems");
      if (dialog5 != null)
        dialog5.Parameters = string.Format("?securedObjectID={0}&title={1}&securedObjectTypeName={2}&managerClassName={3}", (object) this.Host.Taxonomy.Id, (object) string.Format(Res.Get<SecurityResources>().GeneralPermissionsTitle, (object) this.Host.Taxonomy.Title), (object) typeof (FlatTaxonomy).AssemblyQualifiedName, (object) typeof (TaxonomyManager).AssemblyQualifiedName);
      ILinkDefinition link = this.GetLink("viewItems");
      if (link == null)
        return;
      link.NavigateUrl = this.GetMarkedItemsUrl();
    }

    private void SetCommandPermissions()
    {
      bool flag = this.Host.Taxonomy.IsGranted("Taxonomies", "ModifyTaxonomyAndSubTaxons");
      foreach (ToolboxItemBase toolboxItemBase in this.FlatTaxaItemsGrid.ToolboxItems.Where<ToolboxItemBase>((Func<ToolboxItemBase, bool>) (item => ((CommandToolboxItem) item).CommandName == "create" || ((CommandToolboxItem) item).CommandName == "groupDelete" || ((CommandToolboxItem) item).CommandName == "bulkEdit")))
        toolboxItemBase.Visible = flag;
      this.MainDecisionScreen.ActionItems.Where<ActionItem>((Func<ActionItem, bool>) (item => item.CommandName == "NewTaxon")).FirstOrDefault<ActionItem>().Visible = flag;
    }

    private IDialogDefinition GetDialog(string commandName) => this.FlatTaxaGrid.Dialogs.Where<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.OpenOnCommandName == commandName)).FirstOrDefault<IDialogDefinition>();

    private ILinkDefinition GetLink(string commandName) => this.FlatTaxaGrid.Links.Where<ILinkDefinition>((Func<ILinkDefinition, bool>) (l => l.CommandName == commandName)).FirstOrDefault<ILinkDefinition>();
  }
}
