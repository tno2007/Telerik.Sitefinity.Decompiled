// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.FolderSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A <see cref="T:Telerik.Sitefinity.Web.UI.FolderSelector" /> control that works with folders.
  /// </summary>
  public class FolderSelector : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.FolderSelector.ascx");
    private string treeViewCssClass = "sfTreeView";
    private const string sfTreeViewSingleSelectCss = "sfTreeViewSingle";
    private const string controlScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.FolderSelector.js";
    private bool allowSearch = true;
    private bool _showButtonsArea = true;
    private bool showIncludeChildLibraryItems;

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
      get => !string.IsNullOrEmpty(FolderSelector.layoutTemplatePath) ? FolderSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the web service URL. (applied to GenericPageSelector)
    /// </summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow multiple selection.
    /// </summary>
    public bool AllowMultipleSelection { get; set; }

    /// <summary>Gets or sets the selector title.</summary>
    public string SelectorTitle { get; set; }

    /// <summary>
    /// Gets the configured value of how many items should be displayed on the first load. This configuration enables the control to load items only when required.
    /// </summary>
    public int ItemsCount => Config.Get<LibrariesConfig>().ItemsCount;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds
    /// to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the name of the javascript type that the designer will use.
    /// The designers can reuse for exampel the base class implementation and just customize some labels
    /// </summary>
    /// <value>The name of the script descriptor type.</value>
    protected override string ScriptDescriptorTypeName => typeof (FolderSelector).FullName;

    /// <summary>
    /// Gets or sets a value indicating whether to allow the search option.
    /// </summary>
    public bool AllowSearch
    {
      get => this.allowSearch;
      set => this.allowSearch = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to bind the selector on load option.
    /// </summary>
    public bool BindOnLoad { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bind the selector on load option.
    /// </summary>
    public bool ShowButtonsArea
    {
      get => this._showButtonsArea;
      set => this._showButtonsArea = value;
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the done button text.</summary>
    public string DoneButtonText { get; set; }

    /// <summary>
    /// Gets or sets the name of the CSS class applied to the tree view.
    /// </summary>
    /// <value>The CSS class.</value>
    protected virtual string TreeViewCssClass
    {
      get => this.treeViewCssClass;
      set => this.treeViewCssClass = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the include child library items checkbox.
    /// </summary>
    public virtual bool ShowIncludeChildLibraryItems
    {
      get => this.showIncludeChildLibraryItems;
      set => this.showIncludeChildLibraryItems = value;
    }

    /// <summary>Gets the control that selects taxa.</summary>
    protected virtual GenericPageSelector FoldersGenericSelector => this.Container.GetControl<GenericPageSelector>("foldersGenericSelector", true);

    /// <summary>The LinkButton for "Done"</summary>
    protected virtual LinkButton DoneSelectingButton => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>The LinkButton for "Cancel"</summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>
    /// Gets the label that displays the title of the selector.
    /// </summary>
    protected virtual SitefinityLabel SelectorTitleLabel => this.Container.GetControl<SitefinityLabel>("lblSelectorTitle", true);

    /// <summary>Gets the control that selects taxa.</summary>
    protected virtual Panel ButtonAreaPanel => this.Container.GetControl<Panel>("buttonAreaPanel", true);

    /// <summary>Gets a reference to the done with selecting literal.</summary>
    protected virtual Literal DoneWithSelectingLiteral => this.Container.GetControl<Literal>("doneWithSelectingLiteral", false);

    /// <summary>
    /// Gets a reference to the include child library items checkbox.
    /// </summary>
    protected virtual CheckBox IncludeChildLibraryItemsCheckbox => this.Container.GetControl<CheckBox>("includeChildLibraryItemsCheckbox", false);

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.JQueryUI;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddComponentProperty("foldersGenericSelector", this.FoldersGenericSelector.ClientID);
      controlDescriptor.AddElementProperty("lnkDoneSelecting", this.DoneSelectingButton.ClientID);
      controlDescriptor.AddElementProperty("lnkCancel", this.CancelButton.ClientID);
      controlDescriptor.AddProperty("_itemsCount", (object) this.ItemsCount);
      if (this.IncludeChildLibraryItemsCheckbox != null && this.IncludeChildLibraryItemsCheckbox.Visible)
        controlDescriptor.AddElementProperty("includeChildLibraryItemsCheckbox", this.IncludeChildLibraryItemsCheckbox.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Selectors.Scripts.FolderSelector.js", typeof (FolderSelector).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.FoldersGenericSelector != null)
      {
        this.FoldersGenericSelector.AllowMultipleSelection = this.AllowMultipleSelection;
        string str = VirtualPathUtility.AppendTrailingSlash(this.WebServiceUrl);
        this.FoldersGenericSelector.BindOnLoad = this.BindOnLoad;
        this.FoldersGenericSelector.WebServiceUrl = str;
        this.FoldersGenericSelector.OrginalServiceBaseUrl = str;
        this.FoldersGenericSelector.ServiceChildItemsBaseUrl = str;
        this.FoldersGenericSelector.ServicePredecessorBaseUrl = str + "predecessors/";
        this.FoldersGenericSelector.ServiceTreeUrl = str + "tree/";
        this.FoldersGenericSelector.ConstantFilter = (string) null;
        this.FoldersGenericSelector.AllowSearch = this.AllowSearch;
        this.FoldersGenericSelector.Provider = this.ProviderName;
      }
      this.ButtonAreaPanel.Visible = this.ShowButtonsArea;
      this.SelectorTitleLabel.Text = this.SelectorTitle;
      if (this.DoneButtonText != null && this.DoneWithSelectingLiteral != null)
        this.DoneWithSelectingLiteral.Text = this.DoneButtonText;
      if (this.TreeViewCssClass != null)
        this.FoldersGenericSelector.TreeViewCssClass = this.TreeViewCssClass;
      if (!this.FoldersGenericSelector.AllowMultipleSelection)
        this.FoldersGenericSelector.TreeViewCssClass += " sfTreeViewSingle";
      if (this.IncludeChildLibraryItemsCheckbox == null)
        return;
      this.IncludeChildLibraryItemsCheckbox.Visible = this.ShowIncludeChildLibraryItems;
    }
  }
}
