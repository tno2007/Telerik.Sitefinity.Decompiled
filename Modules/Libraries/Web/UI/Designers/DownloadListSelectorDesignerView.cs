// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSelectorDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A control representing designer view for rendering library selector when upload an image at image control.
  /// </summary>
  public class DownloadListSelectorDesignerView : ContentViewDesignerView
  {
    private string uploadDialogUrl;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Documents.DownloadListSelectorDesignerView.ascx");
    internal const string selectorViewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.DownloadListSelectorDesignerView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    private const string filterSelectorHelperScript = "Telerik.Sitefinity.Web.Scripts.FilterSelectorHelper.js";
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DownloadListSelectorDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => nameof (DownloadListSelectorDesignerView);

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<LibrariesResources>().DocumentsAndFiles;

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected LibrariesManager Manager => this.ContentManager as LibrariesManager;

    /// <summary>Gets a value indicating if there are libraries.</summary>
    /// <value><c>true</c> if there are libraries; otherwise, <c>false</c>.</value>
    protected internal virtual bool HaveLibraries
    {
      get
      {
        try
        {
          return this.Manager.GetDocumentLibraries().Count<DocumentLibrary>() > 0;
        }
        catch (ConfigurationErrorsException ex)
        {
        }
        return false;
      }
    }

    /// <summary>Gets the url of the upload dialog.</summary>
    protected string UploadDialogUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.uploadDialogUrl))
        {
          string str = string.Format("?contentType={0}&providerName={1}&itemName={2}&itemsName={3}&libraryTypeName={4}&libraryType={5}&childServiceUrl={6}&parentServiceUrl={7}", (object) typeof (Document).FullName, this.CurrentContentView.ControlDefinition.ProviderName.IsNullOrEmpty() ? (object) this.Manager.Provider.Name : (object) this.CurrentContentView.ControlDefinition.ProviderName, (object) Res.Get<DocumentsResources>().Document, (object) Res.Get<DocumentsResources>().Documents, (object) Res.Get<DocumentsResources>().Library, (object) typeof (DocumentLibrary).FullName, (object) HttpUtility.UrlEncode("~/Sitefinity/Services/Content/DocumentService.svc/"), (object) HttpUtility.UrlEncode("~/Sitefinity/Services/Content/DocumentLibraryService.svc/")) + "&LibraryId={{Id}}";
          this.uploadDialogUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/" + typeof (UploadDialog).Name) + str;
        }
        return this.uploadDialogUrl;
      }
    }

    /// <summary>Gets all documents radio button.</summary>
    /// <value>All documents radio button.</value>
    protected internal virtual RadioButton AllDocumentsRadioButton => this.Container.GetControl<RadioButton>("rbAllDocuments", true);

    /// <summary>Gets the library radio button.</summary>
    /// <value>The library radio button.</value>
    protected internal virtual RadioButton LibraryRadioButton => this.Container.GetControl<RadioButton>("rbLibraryDocuments", true);

    /// <summary>Gets the upload radio button.</summary>
    /// <value>The upload radio button.</value>
    protected internal virtual RadioButton UploadRadioButton => this.Container.GetControl<RadioButton>("rbUpload", false);

    /// <summary>Gets the file upload button</summary>
    protected internal virtual LinkButton UploadButton => this.Container.GetControl<LinkButton>("btnUpload", false);

    /// <summary>Gets the folder selector.</summary>
    protected internal virtual FolderSelector FolderSelector => this.Container.GetControl<FolderSelector>("folderSelector", true);

    /// <summary>Gets the selected album label.</summary>
    protected internal virtual Label SelectedLibrary => this.Container.GetControl<Label>("selectedLibrary", true);

    /// <summary>
    /// Represents the choice field with the different sorting options
    /// </summary>
    protected internal virtual ChoiceField SortExpressionChoiceField => this.Container.GetControl<ChoiceField>("cfSorting", true);

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
          this.ProvidersSelector.Manager = (IManager) this.Manager;
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
      parentDesigner.TopMessageText = !this.IsControlDefinitionProviderCorrect ? Res.Get<Labels>().DefinedProviderNotAvailable : string.Format(Res.Get<LibrariesResources>().SelectAnotherLibrary, (object) Res.Get<DocumentsResources>().Library.ToLower(), (object) Res.Get<DocumentsResources>().Library.ToLower());
      parentDesigner.TopMessageType = MessageType.Negative;
    }

    /// <summary>Gets the selected library.</summary>
    /// <returns></returns>
    protected internal virtual Library GetSelectedLibrary()
    {
      Library selectedLibrary = (Library) null;
      Guid parentId = this.CurrentContentView.MasterViewDefinition.ItemsParentId;
      if (parentId != Guid.Empty)
        selectedLibrary = (Library) this.Manager.GetDocumentLibraries().SingleOrDefault<DocumentLibrary>((Expression<Func<DocumentLibrary, bool>>) (l => l.Id == parentId));
      return selectedLibrary;
    }

    /// <summary>Gets the selected folder.</summary>
    /// <returns></returns>
    private Folder GetSelectedFolder()
    {
      Folder selectedFolder = (Folder) null;
      Guid parentId = this.CurrentContentView.MasterViewDefinition.ItemsParentId;
      if (parentId != Guid.Empty)
      {
        try
        {
          selectedFolder = this.Manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == parentId));
        }
        catch (ConfigurationErrorsException ex)
        {
        }
      }
      return selectedFolder;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (DownloadListSelectorDesignerView).FullName, this.ClientID);
      controlDescriptor.AddProperty("_allDocumentsRadioClientID", (object) this.AllDocumentsRadioButton.ClientID);
      controlDescriptor.AddProperty("_libraryRadioClientID", (object) this.LibraryRadioButton.ClientID);
      controlDescriptor.AddProperty("_uploadRadioClientID", (object) this.UploadRadioButton.ClientID);
      controlDescriptor.AddElementProperty("uploadButton", this.UploadButton.ClientID);
      controlDescriptor.AddElementProperty("selectedLibrary", this.SelectedLibrary.ClientID);
      controlDescriptor.AddComponentProperty("folderSelector", this.FolderSelector.ClientID);
      controlDescriptor.AddComponentProperty("sortExpressionChoiceField", this.SortExpressionChoiceField.ClientID);
      controlDescriptor.AddProperty("_uploadDialogUrl", (object) this.UploadDialogUrl);
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
        string empty = string.Empty;
        Folder selectedFolder = this.GetSelectedFolder();
        if (selectedFolder != null)
        {
          string folderTitlePath = this.Manager.GetFolderTitlePath<DocumentLibrary>(selectedFolder);
          controlDescriptor.AddProperty("_selectedLibraryTitle", (object) (folderTitlePath ?? selectedFolder.Title.Value));
          controlDescriptor.AddProperty("_selectedLibraryId", (object) selectedFolder.Id);
        }
        else if (this.HaveLibraries)
        {
          if (this.CurrentContentView.MasterViewDefinition.ItemsParentId != Guid.Empty)
          {
            string str = string.Format(Res.Get<LibrariesResources>().SelectedLibraryWasDeleted, (object) Res.Get<DocumentsResources>().Library.ToLower());
            controlDescriptor.AddProperty("_selectedLibraryTitle", (object) str);
          }
        }
        else
        {
          string str = string.Format(Res.Get<LibrariesResources>().NoLibrariesWereCreated, (object) Res.Get<DocumentsResources>().Libraries.ToLower());
          controlDescriptor.AddProperty("_selectedLibraryTitle", (object) str);
        }
      }
      controlDescriptor.AddProperty("_haveLibraries", (object) this.HaveLibraries);
      controlDescriptor.AddComponentProperty("filterSelector", this.ContentFilterSelector.FilterSelector.ClientID);
      if (this.NarrowSelectionButton != null)
        controlDescriptor.AddElementProperty("btnNarrowSelection", this.NarrowSelectionButton.ClientID);
      if (this.NarrowSelectionContainer != null)
        controlDescriptor.AddElementProperty("narrowSelection", this.NarrowSelectionContainer.ClientID);
      if (this.ProvidersSelector != null && this.ProvidersSelector.Visible)
        controlDescriptor.AddComponentProperty("providersSelector", this.ProvidersSelector.ClientID);
      controlDescriptor.AddProperty("noContentSelectedLabel", (object) Res.Get<DocumentsResources>().NoLibrarySelected);
      if (this.SelectLibraryButtonWrapper != null)
        controlDescriptor.AddElementProperty("selectLibraryButtonWrapper", this.SelectLibraryButtonWrapper.ClientID);
      if (this.IncludesChildLibraryItemsLabel != null)
        controlDescriptor.AddElementProperty("includesChildLibraryItemsLabel", this.IncludesChildLibraryItemsLabel.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
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
      string fullName = typeof (DownloadListSelectorDesignerView).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.FilterSelectorHelper.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.DownloadListSelectorDesignerView.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources"));
      ScriptReference scriptReference = PageManager.GetScriptReferences(ScriptRef.DialogManager).SingleOrDefault<ScriptReference>();
      if (scriptReference != null)
        scriptReferences.Add(scriptReference);
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
