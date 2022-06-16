// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions.CodeQuality;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  ///  A field control used for displaying and editing choices from a drop down list of values.
  /// </summary>
  [ApprovedBy("Boyan Rabchev", "2012/02/03")]
  public class BindableChoiceField : FieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.BindableChoiceField.ascx");
    internal const string script = "Telerik.Sitefinity.Web.UI.Fields.Scripts.BindableChoiceField.js";
    private const string parentSelectorFieldScript = "Telerik.Sitefinity.Web.Scripts.IParentSelectorField.js";
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private const string iRequiresProviderScript = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js";
    private const string iRequiresUiCultureScript = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField" /> class.
    /// </summary>
    public BindableChoiceField() => this.LayoutTemplatePath = BindableChoiceField.layoutTemplatePath;

    /// <summary>Gets or sets the web service url.</summary>
    protected internal string WebServiceUrl { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    protected internal string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bind on load.
    /// </summary>
    protected internal bool? BindOnLoad { get; set; }

    /// <summary>Gets or sets the binder default filter expression.</summary>
    public string FilterExpression { get; set; }

    /// <summary>Gets or sets the binder default sort expression.</summary>
    public string SortExpression { get; set; }

    /// <summary>Gets or sets the create hyper link title.</summary>
    protected string CreateHyperLinkTitle { get; set; }

    /// <summary>Gets or sets the example hyper link title.</summary>
    /// <value>The example hyper link title.</value>
    protected string ExampleHyperLinkTitle { get; set; }

    /// <summary>
    /// Gets or sets the title of the "create new item" prompt dialog.
    /// </summary>
    protected string CreatePromptTitle { get; set; }

    /// <summary>
    /// Gets or sets the title of the textbox in the"create new item" prompt dialog.
    /// </summary>
    protected string CreatePromptTextFieldTitle { get; set; }

    /// <summary>
    /// Gets or sets the example text in the"create new item" prompt dialog.
    /// </summary>
    protected string CreatePromptExampleText { get; set; }

    /// <summary>
    /// Gets or sets the Create button text in the"create new item" prompt dialog.
    /// </summary>
    protected string CreatePromptCreateButtonTitle { get; set; }

    /// <summary>
    /// Gets or sets URL to be used by the"create new item" prompt dialog to create a new item.
    /// The URL params should support "provider" and "title".
    /// </summary>
    protected internal string CreateNewItemServiceUrl { get; set; }

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    public override object Value { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

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
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", true);

    /// <summary>
    /// Gets the reference to the label control that displays the example for this
    /// field control.
    /// </summary>
    /// <remarks>This control is mandatory only in the write mode.</remarks>
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", true);

    /// <summary>
    /// Gets the text box control which should be displayed at Write Mode
    /// </summary>
    /// <value>The text box control.</value>
    protected internal virtual DropDownList ListControl => this.Container.GetControl<DropDownList>("ddlChoices", true);

    /// <summary>Gets the hidden control.</summary>
    /// <value>The hidden control.</value>
    protected internal virtual HiddenField HiddenControl => this.Container.GetControl<HiddenField>("idHidden", true);

    /// <summary>Gets the binder.</summary>
    /// <value>The binder.</value>
    protected internal virtual ClientBinder Binder => this.Container.GetControl<ClientBinder>("genericBinder", true);

    /// <summary>Gets the create hyperlink.</summary>
    protected internal virtual HyperLink CreateHyperLink => this.Container.GetControl<HyperLink>("createHyperLink", true);

    /// <summary>Gets the example hyperlink.</summary>
    protected internal virtual HyperLink ExampleHyperLink => this.Container.GetControl<HyperLink>("exampleHyperLink", true);

    /// <summary>Gets or sets the client side templates container.</summary>
    /// <value>The client side templates container.</value>
    protected internal virtual Panel ClientSideTemplatesContainer { get; set; }

    /// <summary>Gets the create item prompt dialog.</summary>
    /// <value>The create item prompt dialog.</value>
    protected internal virtual PromptDialog CreateItemPrompt => this.Container.GetControl<PromptDialog>("createItemPrompt", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows the migration log during the migration process
    /// </summary>
    public HtmlGenericControl MigrationLogDialog => this.Container.GetControl<HtmlGenericControl>("migrationLogDialog", true);

    /// <summary>
    /// Gets the close link button of migration log dialog which is used to close the dialog after the migration process has been finished
    /// </summary>
    protected LinkButton CloseMigrationDlgBtn => this.Container.GetControl<LinkButton>("closeMigrationDlgBtn", true);

    /// <summary>Gets a reference of the Or literal.</summary>
    protected virtual Literal OrLiteral => this.Container.GetControl<Literal>("orLiteral", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConstructControl();

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_serviceBaseUrl", (object) VirtualPathUtility.ToAbsolute(this.WebServiceUrl));
      controlDescriptor.AddElementProperty("selectElement", this.ListControl.ClientID);
      controlDescriptor.AddComponentProperty("binder", this.Binder.ClientID);
      controlDescriptor.AddElementProperty("createHyperLink", this.CreateHyperLink.ClientID);
      controlDescriptor.AddElementProperty("exampleHyperLink", this.ExampleHyperLink.ClientID);
      controlDescriptor.AddComponentProperty("createItemPrompt", this.CreateItemPrompt.ClientID);
      controlDescriptor.AddProperty("_createPromptTitle", (object) this.CreatePromptTitle);
      controlDescriptor.AddProperty("_createPromptTextFieldTitle", (object) this.CreatePromptTextFieldTitle);
      controlDescriptor.AddProperty("_createPromptExampleText", (object) this.CreatePromptExampleText);
      controlDescriptor.AddProperty("_createPromptCreateButtonTitle", (object) this.CreatePromptCreateButtonTitle);
      controlDescriptor.AddProperty("_createPromptCreateButtonTitle", (object) this.CreatePromptCreateButtonTitle);
      controlDescriptor.AddProperty("_createNewItemServiceUrl", (object) VirtualPathUtility.ToAbsolute(this.CreateNewItemServiceUrl));
      controlDescriptor.AddElementProperty("migrationLogDialog", this.MigrationLogDialog.ClientID);
      controlDescriptor.AddElementProperty("closeMigrationDlgBtn", this.CloseMigrationDlgBtn.ClientID);
      scriptDescriptorList.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (BindableChoiceField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.IParentSelectorField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources"),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.BindableChoiceField.js", fullName)
      };
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IBindableChoiceFieldDefinition choiceFieldDefinition))
        return;
      this.WebServiceUrl = choiceFieldDefinition.WebServiceUrl;
      this.ProviderName = choiceFieldDefinition.ProviderName;
      this.BindOnLoad = choiceFieldDefinition.BindOnLoad;
      this.CreateHyperLinkTitle = Res.ResolveLocalizedValue(choiceFieldDefinition.ResourceClassId, choiceFieldDefinition.CreateHyperLinkTitle);
      this.ExampleHyperLinkTitle = Res.ResolveLocalizedValue(choiceFieldDefinition.ResourceClassId, choiceFieldDefinition.ExampleHyperLinkTitle);
      this.FilterExpression = choiceFieldDefinition.FilterExpression;
      this.SortExpression = choiceFieldDefinition.SortExpression;
      this.CreatePromptTitle = Res.ResolveLocalizedValue(choiceFieldDefinition.ResourceClassId, choiceFieldDefinition.CreatePromptTitle);
      this.CreatePromptTextFieldTitle = Res.ResolveLocalizedValue(choiceFieldDefinition.ResourceClassId, choiceFieldDefinition.CreatePromptTextFieldTitle);
      this.CreatePromptExampleText = Res.ResolveLocalizedValue(choiceFieldDefinition.ResourceClassId, choiceFieldDefinition.CreatePromptExampleText);
      this.CreatePromptCreateButtonTitle = Res.ResolveLocalizedValue(choiceFieldDefinition.ResourceClassId, choiceFieldDefinition.CreatePromptCreateButtonTitle);
      if (!string.IsNullOrWhiteSpace(choiceFieldDefinition.CreateNewItemServiceUrl))
        this.CreateNewItemServiceUrl = choiceFieldDefinition.CreateNewItemServiceUrl;
      else
        this.CreateNewItemServiceUrl = choiceFieldDefinition.WebServiceUrl;
    }

    /// <summary>
    /// The method that is used to set the properties of the contained controls.
    /// </summary>
    protected internal virtual void ConstructControl()
    {
      this.TitleLabel.Text = this.Title;
      this.ExampleLabel.Text = this.Example;
      this.TitleLabel.AssociatedControlID = this.ListControl.ID;
      this.DescriptionLabel.Text = this.Description;
      this.CreateHyperLink.Text = this.CreateHyperLinkTitle;
      this.ExampleHyperLink.Text = this.ExampleHyperLinkTitle;
      this.Binder.ServiceUrl = VirtualPathUtility.ToAbsolute(this.WebServiceUrl);
      this.Binder.DefaultSortExpression = this.SortExpression;
      this.Binder.FilterExpression = this.FilterExpression;
      if (this.BindOnLoad.HasValue)
        this.Binder.BindOnLoad = this.BindOnLoad.Value;
      if (!string.IsNullOrEmpty(this.CreateHyperLinkTitle) || this.OrLiteral == null)
        return;
      this.OrLiteral.Visible = false;
    }
  }
}
