// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSelectorsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Represents the designer view used to select and filter media content.
  /// You can select media content from a library or according to specified citeria.
  /// </summary>
  public abstract class MediaContentSelectorsDesignerView : ContentViewDesignerView
  {
    private string noLibrarySelectedText;
    private const string selectorViewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaContentSelectorsDesignerView.js";
    private const string designerViewControlScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    private const string filterSelectorHelperScript = "Telerik.Sitefinity.Web.Scripts.FilterSelectorHelper.js";
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";

    /// <summary>Gets a value indicating if there are libraries.</summary>
    /// <value><c>true</c> if there are libraries; otherwise, <c>false</c>.</value>
    protected abstract bool HaveLibraries { get; }

    /// <summary>
    /// Gets the localizable string that represents the name of the library.
    /// </summary>
    protected abstract string LibraryName { get; }

    /// <summary>
    /// Gets the localizable string that represents the content type.
    /// </summary>
    protected abstract string ContentType { get; }

    /// <summary>
    /// Gets the localizable string that represents the name of the library in plural.
    /// </summary>
    protected abstract string LibrariesName { get; }

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected LibrariesManager Manager => this.ContentManager as LibrariesManager;

    /// <summary>
    /// Gets or sets the text displayed when no library is selected.
    /// </summary>
    /// <value>The no library selected text.</value>
    public virtual string NoLibrarySelectedText
    {
      get
      {
        if (this.noLibrarySelectedText.IsNullOrEmpty())
          this.noLibrarySelectedText = Res.Get<LibrariesResources>().NoLibrarySelected;
        return this.noLibrarySelectedText;
      }
      set => this.noLibrarySelectedText = value;
    }

    /// <summary>Gets a reference to rbAllItems radio button.</summary>
    protected virtual RadioButton AllItemsRadioButton => this.Container.GetControl<RadioButton>("rbAllItems", true);

    /// <summary>Gets a reference to rbLibraryItems radio button.</summary>
    protected virtual RadioButton LibraryRadioButton => this.Container.GetControl<RadioButton>("rbLibraryItems", true);

    /// <summary>Gets a reference to rbUpload radio button.</summary>
    /// <value>The upload radio button.</value>
    protected virtual RadioButton UploadRadioButton => this.Container.GetControl<RadioButton>("rbUpload", false);

    /// <summary>Gets the library selector.</summary>
    protected internal virtual FolderSelector FolderSelector => this.Container.GetControl<FolderSelector>("folderSelector", true);

    /// <summary>Gets the selected library label.</summary>
    protected virtual Label SelectedLibrary => this.Container.GetControl<Label>("selectedLibrary", true);

    /// <summary>Gets the cfSorting field control.</summary>
    protected virtual ChoiceField SortingField => this.Container.GetControl<ChoiceField>("cfSorting", true);

    /// <summary>Gets a reference to the filter selector control.</summary>
    protected virtual ContentFilterSelector ContentFilterSelector => this.Container.GetControl<ContentFilterSelector>("contentFilterSelector", true);

    /// <summary>Gets the button for narrow selection.</summary>
    protected virtual LinkButton NarrowSelectionButton => this.Container.GetControl<LinkButton>("btnNarrowSelection", false);

    /// <summary>Gets the narrow selection container.</summary>
    /// <value>The narrow selection container.</value>
    protected virtual Control NarrowSelectionContainer => this.Container.GetControl<Control>("narrowSelection", false);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Choose library" dialog
    /// </summary>
    public HtmlGenericControl SelectorTag => this.Container.GetControl<HtmlGenericControl>("selectorTag", true);

    /// <summary>Gets the providers selector control.</summary>
    /// <value>The providers selector control.</value>
    protected ProvidersSelector ProvidersSelector => this.Container.GetControl<ProvidersSelector>("providersSelector", false);

    /// <summary>
    /// Gets the reference to the flat taxon selector results view.
    /// </summary>
    protected virtual FlatTaxonSelectorResultView FlatTaxonSelector
    {
      get
      {
        FilterSelectorItem filterSelectorItem = this.ContentFilterSelector.FilterSelector.Items.Find((Predicate<FilterSelectorItem>) (i => i.ActualSelectorResultView is FlatTaxonSelectorResultView));
        return filterSelectorItem != null ? filterSelectorItem.ActualSelectorResultView as FlatTaxonSelectorResultView : (FlatTaxonSelectorResultView) null;
      }
    }

    /// <summary>
    /// Gets the reference to the wrapper element of the select library button
    /// </summary>
    public HtmlGenericControl SelectLibraryButtonWrapper => this.Container.GetControl<HtmlGenericControl>("selectLibraryButtonWrapper", false);

    /// <summary>
    /// Gets the a reference to the "includes child library items" label.
    /// </summary>
    protected virtual SitefinityLabel IncludesChildLibraryItemsLabel => this.Container.GetControl<SitefinityLabel>("includesChildLibraryItemsLabel", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.IsControlDefinitionProviderCorrect && this.SelectLibraryButtonWrapper != null)
        this.SelectLibraryButtonWrapper.Style.Add("display", "none");
      this.ContentFilterSelector.FilterSelector.SetTaxonomyId("Categories", TaxonomyManager.CategoriesTaxonomyId);
      this.ContentFilterSelector.FilterSelector.SetTaxonomyId("Tags", TaxonomyManager.TagsTaxonomyId);
      if (this.FlatTaxonSelector != null)
        this.FlatTaxonSelector.UICulture = this.GetUICulture();
      this.ContentFilterSelector.ProviderTypeName = this.ProvidersSelector.SelectedProviderName;
      this.ContentFilterSelector.ItemTypeName = this.CurrentContentView.ControlDefinition.ContentType.FullName;
      if (this.ProvidersSelector != null)
      {
        if (this.ContentManager != null)
        {
          this.ProvidersSelector.Manager = this.ContentManager;
          this.ProvidersSelector.SelectedProviderName = this.CurrentContentView.ControlDefinition.ProviderName;
        }
        else
          this.ProvidersSelector.Visible = false;
      }
      if (this.FolderSelector == null)
        return;
      this.FolderSelector.ProviderName = this.CurrentContentView.ControlDefinition.ProviderName.IsNullOrEmpty() ? this.ContentManager.Provider.Name : this.CurrentContentView.ControlDefinition.ProviderName;
      this.FolderSelector.SelectorTitle = Res.Get<LibrariesResources>().SelectImageLibrary;
    }

    /// <summary>Initializes the view.</summary>
    /// <param name="designer"></param>
    public override void InitView(ControlDesignerBase designer)
    {
      base.InitView(designer);
      this.InitializeContentManager();
      Library selectedLibrary = this.GetSelectedLibrary();
      Folder folder = (Folder) null;
      if (selectedLibrary == null)
        folder = this.GetSelectedFolder();
      if (this.IsControlDefinitionProviderCorrect && (!(this.CurrentContentView.MasterViewDefinition.ItemsParentId != Guid.Empty) || selectedLibrary != null || folder != null))
        return;
      ContentViewDesignerBase parentDesigner = this.ParentDesigner as ContentViewDesignerBase;
      parentDesigner.TopMessageText = !this.IsControlDefinitionProviderCorrect ? Res.Get<Labels>().DefinedProviderNotAvailable : string.Format(Res.Get<LibrariesResources>().SelectAnotherLibrary, (object) this.LibraryName.ToLower(), (object) this.LibraryName.ToLower());
      parentDesigner.TopMessageType = MessageType.Negative;
    }

    /// <summary>Gets the selected library.</summary>
    /// <returns></returns>
    protected abstract Library GetSelectedLibrary();

    /// <summary>Gets the selected folder.</summary>
    /// <returns></returns>
    private Folder GetSelectedFolder()
    {
      Folder selectedFolder = (Folder) null;
      Guid itemsParentId = this.CurrentContentView.MasterViewDefinition.ItemsParentId;
      if (itemsParentId != Guid.Empty)
      {
        try
        {
          selectedFolder = FolderExtensions.GetFolder(this.Manager, itemsParentId);
        }
        catch (ItemNotFoundException ex)
        {
        }
        catch (ConfigurationErrorsException ex)
        {
        }
      }
      return selectedFolder;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (MediaContentSelectorsDesignerView).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("allItemsRadio", this.AllItemsRadioButton.ClientID);
      controlDescriptor.AddElementProperty("libraryRadio", this.LibraryRadioButton.ClientID);
      controlDescriptor.AddElementProperty("uploadRadio", this.UploadRadioButton.ClientID);
      controlDescriptor.AddComponentProperty("folderSelector", this.FolderSelector.ClientID);
      controlDescriptor.AddElementProperty("selectedLibrary", this.SelectedLibrary.ClientID);
      controlDescriptor.AddProperty("_backPhrase", (object) Res.Get<Labels>().BackToEditPage);
      controlDescriptor.AddElementProperty("selectorTag", this.SelectorTag.ClientID);
      Library selectedLibrary = this.GetSelectedLibrary();
      if (selectedLibrary != null)
      {
        controlDescriptor.AddProperty("_selectedLibraryTitle", (object) selectedLibrary.Title.Value);
        controlDescriptor.AddProperty("_selectedLibraryId", (object) selectedLibrary.Id);
      }
      else
      {
        Folder selectedFolder = this.GetSelectedFolder();
        if (selectedFolder != null)
        {
          string str = (string) null;
          if (this.ContentType == typeof (Album).FullName)
            str = this.Manager.GetFolderTitlePath<Album>(selectedFolder);
          else if (this.ContentType == typeof (VideoLibrary).FullName)
            str = this.Manager.GetFolderTitlePath<VideoLibrary>(selectedFolder);
          controlDescriptor.AddProperty("_selectedLibraryTitle", (object) (str ?? selectedFolder.Title.Value));
          controlDescriptor.AddProperty("_selectedLibraryId", (object) selectedFolder.Id);
        }
        else if (this.HaveLibraries)
        {
          if (this.CurrentContentView.MasterViewDefinition.ItemsParentId != Guid.Empty)
            controlDescriptor.AddProperty("_selectedLibraryTitle", (object) string.Format(Res.Get<LibrariesResources>().SelectedLibraryWasDeleted, (object) this.LibraryName.ToLower()));
        }
        else
          controlDescriptor.AddProperty("_selectedLibraryTitle", (object) string.Format(Res.Get<LibrariesResources>().NoLibrariesWereCreated, (object) this.LibrariesName.ToLower()));
      }
      controlDescriptor.AddProperty("_haveLibraries", (object) this.HaveLibraries);
      controlDescriptor.AddComponentProperty("sortingField", this.SortingField.ClientID);
      controlDescriptor.AddComponentProperty("filterSelector", this.ContentFilterSelector.FilterSelector.ClientID);
      if (this.NarrowSelectionButton != null)
        controlDescriptor.AddElementProperty("btnNarrowSelection", this.NarrowSelectionButton.ClientID);
      if (this.NarrowSelectionContainer != null)
        controlDescriptor.AddElementProperty("narrowSelection", this.NarrowSelectionContainer.ClientID);
      if (this.ProvidersSelector != null && this.ProvidersSelector.Visible)
        controlDescriptor.AddComponentProperty("providersSelector", this.ProvidersSelector.ClientID);
      controlDescriptor.AddProperty("noContentSelectedLabel", (object) this.NoLibrarySelectedText);
      if (this.SelectLibraryButtonWrapper != null)
        controlDescriptor.AddElementProperty("selectLibraryButtonWrapper", this.SelectLibraryButtonWrapper.ClientID);
      if (this.IncludesChildLibraryItemsLabel != null)
        controlDescriptor.AddElementProperty("includesChildLibraryItemsLabel", this.IncludesChildLibraryItemsLabel.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (MediaContentSelectorsDesignerView).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.FilterSelectorHelper.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaContentSelectorsDesignerView.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources"));
      ScriptReference scriptReference = PageManager.GetScriptReferences(ScriptRef.DialogManager).SingleOrDefault<ScriptReference>();
      if (scriptReference != null)
        scriptReferences.Add(scriptReference);
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
