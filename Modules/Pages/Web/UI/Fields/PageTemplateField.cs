// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI.Fields
{
  /// <summary>
  /// Defines a field control for selecting and displaying a page template
  /// </summary>
  public class PageTemplateField : FieldControl
  {
    private string templateSelectorDialogUrl;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.PageTemplateField.ascx");
    private const string scriptFileName = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.PageTemplateField.js";
    private const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField" /> class.
    /// </summary>
    public PageTemplateField()
    {
      this.ShowEmptyTemplate = false;
      this.UseDefaultTemplate = true;
      this.LayoutTemplatePath = PageTemplateField.layoutTemplatePath;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the url of the new taxon dialog.</summary>
    protected string TemplateSelectorDialogUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.templateSelectorDialogUrl))
        {
          string virtualPath = "~/Sitefinity/Dialog/" + typeof (Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateSelectorDialog).Name;
          Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
          PageTemplateFramework? framework = this.Framework;
          if (framework.HasValue)
          {
            Dictionary<string, string> dictionary2 = dictionary1;
            framework = this.Framework;
            string str = framework.ToString();
            dictionary2.Add("framework", str);
          }
          if (this.ShowAllBasicTemplates)
            dictionary1.Add("showAllBasicTemplates", this.ShowAllBasicTemplates.ToString());
          if (dictionary1.Count > 0)
          {
            virtualPath += "?";
            foreach (KeyValuePair<string, string> keyValuePair in dictionary1)
              virtualPath = virtualPath + keyValuePair.Key + "=" + keyValuePair.Value + "&";
          }
          this.templateSelectorDialogUrl = VirtualPathUtility.ToAbsolute(virtualPath);
        }
        return this.templateSelectorDialogUrl;
      }
    }

    /// <summary>
    /// Specifies wheter to show or hide the otion for selecting blank (no template) in the dialog
    /// </summary>
    public bool ShowEmptyTemplate { get; set; }

    /// <summary>
    /// Specifies whether to create or not a template when master page is selected.
    /// When false template will be created.
    /// </summary>
    public bool ShouldNotCreateTemplateForMasterPage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is backend template.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is backend template; otherwise, <c>false</c>.
    /// </value>
    public bool IsBackendTemplate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [use default template].
    /// </summary>
    /// <value><c>true</c> if [use default template]; otherwise, <c>false</c>.</value>
    public bool UseDefaultTemplate { get; set; }

    /// <summary>
    /// Gets or sets the language for templates. If null, the default language is used.
    /// </summary>
    public CultureInfo Language { get; set; }

    /// <summary>Gets a value indicating whether [full mode].</summary>
    /// <value><c>true</c> if [full mode]; otherwise, <c>false</c>.</value>
    public bool FullMode => this.DisplayMode == FieldDisplayMode.Write;

    /// <summary>Gets or sets the selected template.</summary>
    /// <value>The selected template.</value>
    public WcfPageTemplate SelectedTemplate { get; set; }

    /// <summary>Gets or sets the current template id.</summary>
    /// <value>The current template id.</value>
    public Guid CurrentTemplateId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the field should show extended basic templates for all frameworks.
    /// </summary>
    public bool ShowAllBasicTemplates { get; set; }

    /// <summary>
    /// Converts a control ID used in conditional templates accoding to this.DisplayMode and this.FullMode
    /// </summary>
    /// <param name="originalName">Original ID of the control</param>
    /// <returns>Unique control ID</returns>
    protected virtual string GetConditionalControlName(string originalName)
    {
      string str = this.DisplayMode != FieldDisplayMode.Read ? (!this.FullMode ? "notfullmode" : "fullmode") : "read";
      return originalName + "_" + str;
    }

    /// <summary>
    /// Shortcut for this.Container.GetControl(this.GetConditionalControlName(originalName), required)
    /// </summary>
    /// <typeparam name="T">Type of the control to load</typeparam>
    /// <param name="originalName">Original ID of the control</param>
    /// <param name="required">Throw exception if control is not found and this parameter is true</param>
    /// <returns>Loaded control</returns>
    protected T GetConditionalControl<T>(string originalName, bool required) => this.Container.GetControl<T>(this.GetConditionalControlName(originalName), required);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label TitleLabel => this.GetConditionalControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label DescriptionLabel => this.GetConditionalControl<Label>("descriptionLabel", true);

    /// <summary>
    /// Gets the reference to the label control that displays the example for this
    /// field control.
    /// </summary>
    /// <remarks>This control is mandatory only in the write mode.</remarks>
    protected internal virtual Label ExampleLabel => this.GetConditionalControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the template label.</summary>
    /// <value>The template label.</value>
    protected internal virtual Label TemplateLabel => this.GetConditionalControl<Label>("templateLabel", true);

    /// <summary>Gets the framework label.</summary>
    /// <value>The framework label.</value>
    protected internal virtual HtmlGenericControl FrameworkLabel => this.GetConditionalControl<HtmlGenericControl>("frameworkLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the template icon.</summary>
    /// <value>The template icon.</value>
    protected internal virtual Image TemplateIcon => this.GetConditionalControl<Image>(nameof (TemplateIcon), true);

    /// <summary>Gets the template selector dialog.</summary>
    /// <value>The template selector dialog.</value>
    protected internal virtual RadWindow TemplateSelectorDialog => this.GetConditionalControl<RadWindow>("templateSelectorDialog", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the open template dialog button.</summary>
    /// <value>The open template dialog button.</value>
    protected internal virtual LinkButton OpenTemplateDialogButton => this.GetConditionalControl<LinkButton>("openTemplateDialogButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the select template panel.</summary>
    /// <value>The select template panel.</value>
    protected internal virtual HtmlGenericControl SelectTemplatePanel => this.GetConditionalControl<HtmlGenericControl>("selectTemplatePanel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the template preview panel.</summary>
    /// <value>The template preview panel.</value>
    protected internal virtual HtmlGenericControl TemplatePreviewPanel => this.GetConditionalControl<HtmlGenericControl>("templatePreviewPanel", this.DisplayMode == FieldDisplayMode.Write);

    internal PageTemplateFramework? Framework { get; set; }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition) => this.ConfigureBaseDefinition(definition);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConstructControl();

    protected internal virtual void ConstructControl()
    {
      this.TitleLabel.Text = this.Title;
      this.DescriptionLabel.Text = this.Description;
      switch (this.DisplayMode)
      {
        case FieldDisplayMode.Write:
          this.TemplateSelectorDialog.NavigateUrl = this.TemplateSelectorDialogUrl;
          break;
      }
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (!string.IsNullOrEmpty(this.TemplateIcon.ImageUrl))
        return;
      this.TemplateIcon.ImageUrl = this.GetEmptyTemplateIcon();
    }

    private string GetEmptyTemplateIcon() => ControlUtilities.ResolveResourceUrl("Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.NoTemplate.gif", this.Page);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_templateSelectorDialogUrl", (object) this.TemplateSelectorDialogUrl);
      controlDescriptor.AddProperty("_baseBackendUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/"));
      controlDescriptor.AddElementProperty("templateIcon", this.TemplateIcon.ClientID);
      controlDescriptor.AddElementProperty("templateLabel", this.TemplateLabel.ClientID);
      controlDescriptor.AddProperty("_emptyIconCache", (object) this.GetEmptyTemplateIcon());
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddElementProperty("openTemplateDialogButton", this.OpenTemplateDialogButton.ClientID);
        controlDescriptor.AddElementProperty("selectTemplatePanel", this.SelectTemplatePanel.ClientID);
        controlDescriptor.AddElementProperty("templatePreviewPanel", this.TemplatePreviewPanel.ClientID);
        controlDescriptor.AddComponentProperty("templateSelectorDialog", this.TemplateSelectorDialog.ClientID);
        controlDescriptor.AddElementProperty("frameworkLabel", this.FrameworkLabel.ClientID);
      }
      if (this.SelectedTemplate != null)
        controlDescriptor.AddProperty("_selectedTemplate", (object) this.SelectedTemplate);
      if (this.CurrentTemplateId != Guid.Empty)
        controlDescriptor.AddProperty("currentTemplateId", (object) this.CurrentTemplateId);
      controlDescriptor.AddProperty("showEmptyTemplate", (object) this.ShowEmptyTemplate);
      controlDescriptor.AddProperty("useDefaultTemplate", (object) this.UseDefaultTemplate);
      controlDescriptor.AddProperty("notCreateTemplateForMasterPage", (object) this.ShouldNotCreateTemplateForMasterPage);
      controlDescriptor.AddProperty("isBackendTemplate", (object) this.IsBackendTemplate);
      string str = this.IsBackendTemplate ? RootTaxonType.Backend.ToString() : RootTaxonType.Frontend.ToString();
      controlDescriptor.AddProperty("rootTaxonType", (object) str);
      controlDescriptor.AddProperty("language", this.Language != null ? (object) this.Language.Name : (object) "");
      scriptDescriptorList.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (TextField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.PageTemplateField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName)
      };
    }

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IPageTemplateFieldDefinition templateFieldDefinition))
        return;
      this.ShouldNotCreateTemplateForMasterPage = templateFieldDefinition.ShouldNotCreateTemplateForMasterPage;
      this.IsBackendTemplate = templateFieldDefinition.IsBackendTemplate;
      this.ShowEmptyTemplate = templateFieldDefinition.ShowEmptyTemplate;
      this.ShowAllBasicTemplates = templateFieldDefinition.ShowAllBasicTemplates;
    }

    /// <summary>Gets the base script descriptors.</summary>
    /// <returns></returns>
    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();
  }
}
