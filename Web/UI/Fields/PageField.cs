// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.PageField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>A field control for selecting a specific page</summary>
  [FieldDefinitionElement(typeof (PageFieldElement))]
  public class PageField : FieldControl
  {
    private bool bindOnLoad = true;
    private const string ControlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.PageField.js";
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    /// <summary>The layout template path</summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.PageField.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.PageField" /> class.
    /// </summary>
    public PageField() => this.LayoutTemplatePath = PageField.layoutTemplatePath;

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the label control that displays the title of the pipe settings list field.
    /// </summary>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label control that displays the description of the pipe settings list field.
    /// </summary>
    protected internal virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", true);

    /// <summary>
    /// Gets the reference to the label control that displays the example of the pipe settings list fields.
    /// </summary>
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", true);

    /// <summary>Gets the reference to the page selector control.</summary>
    protected internal virtual GenericPageSelector PageSelector => this.Container.GetControl<GenericPageSelector>("pageSelector", true, TraverseMethod.DepthFirst);

    /// <summary>Gets the command bar.</summary>
    /// <value>The command bar.</value>
    protected internal virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true, TraverseMethod.DepthFirst);

    /// <summary>Gets the button that opens the selector.</summary>
    /// <value>The button.</value>
    protected internal virtual LinkButton OpenSelector => this.Container.GetControl<LinkButton>("openSelector", true);

    /// <summary>Gets the page URL control.</summary>
    /// <value>The page URL control.</value>
    protected internal virtual SitefinityLabel PageUrl => this.Container.GetControl<SitefinityLabel>("pageUrl", true);

    /// <summary>Gets the selector window.</summary>
    protected HtmlGenericControl SelectorWindow => this.Container.GetControl<HtmlGenericControl>("selectorWindow", true);

    /// <summary>Gets the page title.</summary>
    /// <value>The page title.</value>
    protected internal virtual SitefinityLabel PageTitle => this.Container.GetControl<SitefinityLabel>("pageTitle", true);

    /// <summary>Gets the page status.</summary>
    /// <value>The page status.</value>
    protected internal virtual SitefinityLabel PageStatus => this.Container.GetControl<SitefinityLabel>("pageStatus", true);

    /// <summary>Gets the page culture.</summary>
    /// <value>The page culture.</value>
    protected internal virtual SitefinityLabel PageCulture => this.Container.GetControl<SitefinityLabel>("pageCulture", true);

    /// <summary>Gets the page live URL.</summary>
    /// <value>The page live URL.</value>
    protected internal virtual HtmlAnchor PageLiveUrl => this.Container.GetControl<HtmlAnchor>("pageLiveUrl", true);

    /// <summary>Gets the page details container.</summary>
    protected internal virtual HtmlControl PageDetailsContainer => this.Container.GetControl<HtmlControl>("selectedPageDetails", false);

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IPageFieldDefinition pageFieldDefinition))
        return;
      this.RootNodeID = pageFieldDefinition.RootNodeID;
      this.WebServiceUrl = pageFieldDefinition.WebServiceUrl;
      this.ProviderName = pageFieldDefinition.ProviderName;
      this.SortExpression = pageFieldDefinition.SortExpression;
      bool? bindOnLoad = pageFieldDefinition.BindOnLoad;
      if (!bindOnLoad.HasValue)
        return;
      bindOnLoad = pageFieldDefinition.BindOnLoad;
      this.BindOnLoad = bindOnLoad.Value;
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.RootNodeID == Guid.Empty)
        this.RootNodeID = SiteInitializer.CurrentFrontendRootNodeId;
      this.ClientIDMode = ClientIDMode.AutoID;
      this.TitleLabel.SetTextOrHide(this.Title);
      this.DescriptionLabel.SetTextOrHide(this.Description);
      this.ExampleLabel.SetTextOrHide(this.Example);
      this.PageSelector.RootNodeID = this.RootNodeID;
      this.PageSelector.WebServiceUrl = this.WebServiceUrl;
      this.PageSelector.Provider = this.ProviderName;
      this.PageSelector.MarkItemsWithoutTranslation = this.MarkItemsWithoutTranslation;
      this.PageSelector.BindOnLoad = this.BindOnLoad;
      this.PageSelector.IncludeLanguageSelector = this.IncludeLanguageSelector;
      this.PageSelector.IncludeSiteSelector = this.IncludeSiteSelector;
      this.PageSelector.ShowPagesDetails = this.ShowPagesDetails;
      this.OpenSelector.Visible = this.DisplayMode == FieldDisplayMode.Write;
      if (this.PageDetailsContainer == null)
        return;
      if (this.ShowSelectedPagesDetails)
        this.PageUrl.Visible = false;
      else
        this.PageDetailsContainer.Visible = false;
    }

    /// <summary>
    /// Checks if the container contains any conditional templates.
    /// </summary>
    /// <param name="container">The container.</param>
    protected internal override void CheckConditionalTemplates(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor1 = base.GetScriptDescriptors().LastOrDefault<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor1.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor1.AddElementProperty("openSelector", this.OpenSelector.ClientID);
      controlDescriptor1.AddElementProperty("selectorWindow", this.SelectorWindow.ClientID);
      if (this.ShowSelectedPagesDetails && this.PageDetailsContainer != null)
      {
        controlDescriptor1.AddElementProperty("pageDetailsContainer", this.PageDetailsContainer.ClientID);
        controlDescriptor1.AddElementProperty("pageTitle", this.PageTitle.ClientID);
        controlDescriptor1.AddElementProperty("pageStatus", this.PageStatus.ClientID);
        controlDescriptor1.AddElementProperty("pageCulture", this.PageCulture.ClientID);
        controlDescriptor1.AddElementProperty("pageLiveUrl", this.PageLiveUrl.ClientID);
      }
      else
        controlDescriptor1.AddElementProperty("pageUrl", this.PageUrl.ClientID);
      controlDescriptor1.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
      controlDescriptor1.AddProperty("providerName", (object) this.ProviderName);
      string absolute = VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash(this.WebServiceUrl));
      controlDescriptor1.AddProperty("webServiceUrl", (object) absolute);
      controlDescriptor1.AddProperty("sortExpression", (object) this.SortExpression);
      int? selectorDialogZindex = this.SelectorDialogZIndex;
      if (selectorDialogZindex.HasValue)
      {
        ScriptControlDescriptor controlDescriptor2 = controlDescriptor1;
        selectorDialogZindex = this.SelectorDialogZIndex;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) selectorDialogZindex.Value;
        controlDescriptor2.AddProperty("selectorDialogZIndex", (object) local);
      }
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          "Change",
          Res.Get<Labels>().Change
        },
        {
          "SelectPage",
          Res.Get<Labels>().SelectPage
        }
      };
      controlDescriptor1.AddProperty("resources", (object) dictionary);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor1
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference()
      {
        Assembly = typeof (TaxonField).Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.PageField.js"
      },
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources")
    }.ToArray();

    /// <summary>
    /// Gets or sets a value indicating whether to grey the items that have not a translation for the current language.
    /// The default is false. Works only in multilingual mode.
    /// </summary>
    public bool MarkItemsWithoutTranslation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [include language selector].
    /// </summary>
    /// <value>
    /// <c>true</c> if [include language selector]; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IncludeLanguageSelector { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [include site selector].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [include site selector]; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IncludeSiteSelector { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically bind the selector when the control loads
    /// </summary>
    public virtual bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the ID of the page node from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    public Guid RootNodeID { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public virtual string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider from which the page node ought to be selected.
    /// </summary>
    public virtual string ProviderName { get; set; }

    /// <summary>Gets or sets the sort expression.</summary>
    /// <value>The sort expression.</value>
    public virtual string SortExpression { get; set; }

    /// <summary>Gets or sets the index Z of the selector dialog.</summary>
    public virtual int? SelectorDialogZIndex { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [show selected pages details].
    /// </summary>
    /// <value>
    /// <c>true</c> if [show selected pages details]; otherwise, <c>false</c>.
    /// </value>
    public bool ShowSelectedPagesDetails { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [show pages details].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [show pages details]; otherwise, <c>false</c>.
    /// </value>
    public bool ShowPagesDetails { get; set; }
  }
}
