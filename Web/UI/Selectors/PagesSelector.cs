// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PagesSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Selectors;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Control for selecting multiple pages either internal or external.
  /// </summary>
  public class PagesSelector : SimpleScriptView
  {
    private bool includeSiteSelector;
    private bool includeLanguageSelector;
    private string treeViewCssClass = "sfTreeView";
    private string sfTreeViewSingleSelectCss = "sfTreeViewSingle";
    private GenericPageSelector pageSelector;
    private ExternalPagesSelector extPagesSelector;
    private bool allowExternalPagesSelection = true;
    private bool allowMultipleSelection = true;
    private bool markItemsWithoutTranslation = true;
    private const string selectorScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.PagesSelector.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.PagesSelector.ascx");

    public PagesSelector()
    {
      this.LayoutTemplatePath = PagesSelector.layoutTemplatePath;
      this.BindOnLoad = true;
    }

    /// <summary>
    /// Gets or sets a value indicating whether client binding will be performed on page load.
    /// If set to false, you need to call databind() on the client component instance manually.
    /// Default: true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [bind on load]; otherwise, <c>false</c>.
    /// </value>
    public bool BindOnLoad { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the name of the provider from which the page node ought to be selected.
    /// </summary>
    public string PagesProvider { get; set; }

    /// <summary>
    /// Gets or sets the ID of the page node from which the binding should start. (applied to GenericPageSelector)
    /// </summary>
    /// <value>The root page node ID.</value>
    public Guid RootNodeID { get; set; }

    /// <summary>
    /// Gets or sets the web service URL. (applied to GenericPageSelector)
    /// </summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether external pages can be selected.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if external pages can be selected; otherwise, <c>false</c>.
    /// </value>
    public bool AllowExternalPagesSelection
    {
      get => this.allowExternalPagesSelection;
      set => this.allowExternalPagesSelection = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow multiple selection.
    /// </summary>
    public virtual bool AllowMultipleSelection
    {
      get => this.allowMultipleSelection;
      set => this.allowMultipleSelection = value;
    }

    /// <summary>
    /// Get a filter expression that is always applied to the generic page selector
    /// </summary>
    public string ConstantFilter { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to append the <see cref="P:Telerik.Sitefinity.Web.UI.PagesSelector.ConstantFilter" /> value.
    /// </summary>
    public bool AppendConstantFilter { get; set; }

    /// <summary>
    /// Gets or sets the include site selector setting. False by default
    /// </summary>
    public bool IncludeSiteSelector
    {
      get => this.includeSiteSelector;
      set => this.includeSiteSelector = value;
    }

    /// <summary>
    /// Gets or sets the include language selector setting. False by default
    /// </summary>
    public bool IncludeLanguageSelector
    {
      get => this.includeLanguageSelector;
      set => this.includeLanguageSelector = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to grey the items that have not a translation for the current language.
    /// The default is false. Works only in multilingual mode.
    /// </summary>
    internal bool MarkItemsWithoutTranslation
    {
      get => this.markItemsWithoutTranslation;
      set => this.markItemsWithoutTranslation = value;
    }

    /// <summary>Gets the page selector.</summary>
    /// <value>The page selector.</value>
    protected virtual GenericPageSelector PageSelector
    {
      get
      {
        if (this.pageSelector == null)
          this.pageSelector = this.Container.GetControl<GenericPageSelector>("selector", true);
        return this.pageSelector;
      }
    }

    /// <summary>Gets the external pages selector control.</summary>
    /// <value>The page selector.</value>
    protected virtual ExternalPagesSelector ExtPagesSelector
    {
      get
      {
        if (this.extPagesSelector == null)
          this.extPagesSelector = this.Container.GetControl<ExternalPagesSelector>("extPagesSelector", true);
        return this.extPagesSelector;
      }
    }

    /// <summary>Gets the "Done selecting" button.</summary>
    /// <value>The "Done selecting" button.</value>
    private LinkButton DoneSelectingButton => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>Gets the cancel button.</summary>
    /// <value>The cancel button.</value>
    private LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>Gets the tab strip for switching between page types.</summary>
    protected virtual RadTabStrip PageTabs => this.Container.GetControl<RadTabStrip>("RadTabStrip1", true);

    /// <summary>
    /// Gets or sets the name of the CSS class applied to the tree view.
    /// </summary>
    /// <value>The CSS class.</value>
    public string TreeViewCssClass
    {
      get => this.treeViewCssClass;
      set => this.treeViewCssClass = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.PageSelector.IncludeLanguageSelector = this.IncludeLanguageSelector;
      this.PageSelector.IncludeSiteSelector = this.IncludeSiteSelector;
      if (this.RootNodeID != Guid.Empty)
        this.PageSelector.RootNodeID = this.RootNodeID;
      if (!string.IsNullOrEmpty(this.WebServiceUrl))
        this.PageSelector.WebServiceUrl = this.WebServiceUrl;
      this.PageSelector.Provider = this.PagesProvider;
      this.PageSelector.UICulture = this.UICulture;
      this.PageSelector.AllowMultipleSelection = this.AllowMultipleSelection;
      this.ExtPagesSelector.AllowMultiplePages = this.AllowMultipleSelection;
      if (!string.IsNullOrEmpty(this.ConstantFilter))
        this.PageSelector.SetConstantFilter(this.ConstantFilter);
      this.PageSelector.AppendConstantFilter = this.AppendConstantFilter;
      this.PageSelector.MarkItemsWithoutTranslation = this.MarkItemsWithoutTranslation;
      if (this.TreeViewCssClass != null)
        this.PageSelector.TreeViewCssClass = this.TreeViewCssClass;
      if (this.AllowMultipleSelection)
        return;
      this.PageSelector.TreeViewCssClass = this.PageSelector.TreeViewCssClass + " " + this.sfTreeViewSingleSelectCss;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.PageTabs.Visible = this.AllowExternalPagesSelection;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (PagesSelector).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("tabstrip", this.PageTabs.ClientID);
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
      controlDescriptor.AddComponentProperty("extPagesSelector", this.ExtPagesSelector.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneSelectingButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddProperty("uiCulture", (object) this.UICulture);
      controlDescriptor.AddProperty("_bindOnLoad", (object) this.BindOnLoad);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Selectors.Scripts.PagesSelector.js", typeof (PagesSelector).Assembly.FullName)
    };
  }
}
