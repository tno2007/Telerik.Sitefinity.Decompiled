// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.All.TaxonomiesList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Taxonomies.Web.UI.All
{
  /// <summary>Displays a list of taxonomies items.</summary>
  public class TaxonomiesList : ViewModeControl<TaxonomiesPanel>
  {
    private bool? supportsMultiligual;
    private IAppSettings appSettings;
    /// <summary>
    ///  Gets the name of resource file representing the Dashboard View.
    /// </summary>
    public static readonly string DashboardViewName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.TaxonomiesList.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TaxonomiesList.DashboardViewName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this control supports multiligual.
    /// </summary>
    /// <value><c>true</c> if supports multiligual; otherwise, <c>false</c>.</value>
    public bool SupportsMultiligual
    {
      get
      {
        if (!this.supportsMultiligual.HasValue)
          this.supportsMultiligual = new bool?(this.AppSettings.Multilingual);
        return this.supportsMultiligual.Value;
      }
      set => this.supportsMultiligual = new bool?(value);
    }

    protected virtual IAppSettings AppSettings
    {
      get
      {
        if (this.appSettings == null)
          this.appSettings = (IAppSettings) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;
        return this.appSettings;
      }
    }

    /// <summary>
    /// Gets the reference to the control which displays the list of taxonomies inside
    /// of a grid.
    /// </summary>
    protected virtual ItemsGrid TaxonomiesGrid => this.Container.GetControl<ItemsGrid>("classificationsList", true);

    /// <summary>Gets the permissions per item parameters.</summary>
    /// <value>The permissions per item parameters.</value>
    protected HiddenField PermissionsPerItemParameters => this.Container.GetControl<HiddenField>("hPermissionsPerItemParameters", true);

    /// <summary>Gets the main decision screen.</summary>
    /// <value>The main decision screen.</value>
    protected DecisionScreen MainDecisionScreen => this.Container.GetControl<DecisionScreen>("decisionScreenEmpty", true);

    /// <summary>Gets the show translations link.</summary>
    /// <value>The show translations link.</value>
    protected virtual HyperLink ShowTranslationsLink => this.Container.GetControl<HyperLink>("showMoreTranslations", false);

    /// <summary>Gets the hide translations link.</summary>
    /// <value>The hide translations link.</value>
    protected virtual HyperLink HideTranslationsLink => this.Container.GetControl<HyperLink>("hideMoreTranslations", false);

    /// <summary>Gets the show/hide translations links container.</summary>
    protected virtual Panel TranslationsLinksContainer => this.Container.GetControl<Panel>("translationsLinksContainer", false);

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

    /// <summary>
    /// Gets the reference to the hidden field containg the client id of the delete prompt dialog when multilingual is off.
    /// </summary>
    protected HiddenField DeletePromptDialogSingleHiddenField => this.Container.GetControl<HiddenField>("hDeletePromptDialogSingle", true);

    /// <summary>
    /// Gets the reference to the hidden field containg the client id of the delete prompt dialog when multilingual is on.
    /// </summary>
    protected HiddenField DeletePromptDialogAdvancedHiddenField => this.Container.GetControl<HiddenField>("hDeletePromptDialogAdvanced", true);

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
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
      this.SetCommandPermissions();
      this.SetTranslations();
      this.SetLocalizationControlsVisibility();
    }

    protected virtual void SetCreateFunctionality()
    {
      CommandToolboxItem commandToolboxItem = this.TaxonomiesGrid.ToolboxItems.Where<ToolboxItemBase>((Func<ToolboxItemBase, bool>) (ti => ti is CommandToolboxItem)).Cast<CommandToolboxItem>().Where<CommandToolboxItem>((Func<CommandToolboxItem, bool>) (ti => ti.CommandName == "create")).FirstOrDefault<CommandToolboxItem>();
      if (commandToolboxItem != null)
        commandToolboxItem.Text = Res.Get<TaxonomyResources>().CreateAClassificationName;
      IDialogDefinition dialog = this.GetDialog("create");
      if (dialog == null)
        return;
      dialog.Parameters = string.Format("?TaxonomyId={0}", (object) Guid.Empty);
    }

    private void SetSearchFunctionality() => ((SearchPanelToolboxItem) this.TaxonomiesGrid.ToolboxItems.Where<ToolboxItemBase>((Func<ToolboxItemBase, bool>) (ti => ti.GetType() == typeof (SearchPanelToolboxItem))).First<ToolboxItemBase>()).BinderClientID = this.TaxonomiesGrid.Binder.ClientID;

    private IDialogDefinition GetDialog(string commandName) => this.TaxonomiesGrid.Dialogs.Where<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.OpenOnCommandName == commandName)).FirstOrDefault<IDialogDefinition>();

    private void SetCommandPermissions()
    {
      this.PermissionsPerItemParameters.Value = "{ \"managerClassName\" : \"" + typeof (TaxonomyManager).AssemblyQualifiedName + "\"}";
      bool flag = TaxonomyManager.GetManager(this.Host.ProviderName).GetSecurityRoot().IsGranted("Taxonomies", "CreateTaxonomy");
      ToolboxItemBase toolboxItemBase = this.TaxonomiesGrid.ToolboxItems.Where<ToolboxItemBase>((Func<ToolboxItemBase, bool>) (item => ((CommandToolboxItem) item).CommandName == "create")).FirstOrDefault<ToolboxItemBase>();
      ActionItem actionItem = this.MainDecisionScreen.ActionItems.Where<ActionItem>((Func<ActionItem, bool>) (item => item.CommandName == "NewTaxonomy")).FirstOrDefault<ActionItem>();
      toolboxItemBase.Visible = flag;
      int num = flag ? 1 : 0;
      actionItem.Visible = num != 0;
    }

    /// <summary>Sets the translations column.</summary>
    private void SetTranslations()
    {
      ItemDescription itemDescription = this.TaxonomiesGrid.Items.Where<ItemDescription>((Func<ItemDescription, bool>) (i => i.Name == "Translations")).FirstOrDefault<ItemDescription>();
      if (itemDescription == null)
        return;
      if (this.SupportsMultiligual)
        itemDescription.Markup = new LanguagesColumnMarkupGenerator()
        {
          LanguageSource = LanguageSource.Frontend,
          ItemsInGroupCount = 6,
          ContainerTag = "div",
          GroupTag = "div",
          ItemTag = "div",
          ContainerClass = string.Empty,
          GroupClass = string.Empty,
          ItemClass = string.Empty
        }.GetMarkup();
      else
        this.TaxonomiesGrid.Items.Remove(itemDescription);
    }

    private void SetLocalizationControlsVisibility()
    {
      if (this.ShowTranslationsLink == null || this.HideTranslationsLink == null)
        return;
      if (this.AppSettings.DefinedFrontendLanguages.Length > 6)
      {
        this.ShowTranslationsLink.Visible = this.SupportsMultiligual;
        this.HideTranslationsLink.Visible = this.SupportsMultiligual;
        if (!this.SupportsMultiligual)
          return;
        this.HideTranslationsLink.Style.Add(HtmlTextWriterStyle.Display, "none");
      }
      else
      {
        if (this.TranslationsLinksContainer == null)
          return;
        this.TranslationsLinksContainer.Visible = false;
      }
    }
  }
}
