// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>A field control for listing pipe settings</summary>
  public class ListPipeSettingsField : FieldControl
  {
    private const string controlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ListPipeSettingsField.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ListPipeSettingsField.ascx");
    private IListPipeSettingsFieldDefinition currentDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField" /> class.
    /// </summary>
    public ListPipeSettingsField()
    {
      this.PublishingPointDefinitionFieldName = "PublishingPointDefinition";
      this.PublishingPointNameFieldName = "PublishingPointName";
      this.LayoutTemplatePath = ListPipeSettingsField.layoutTemplatePath;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor descriptorPrivate = this.GetLastScriptDescriptor_Private();
      descriptorPrivate.AddProperty("dialogUrl", (object) RouteHelper.ResolveUrl("~/Sitefinity/Dialog/SettingsDialog", UrlResolveOptions.Rooted));
      descriptorPrivate.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      descriptorPrivate.AddComponentProperty("existingPipesBinder", this.ExistingPipesBinder.ClientID);
      descriptorPrivate.AddElementProperty("addSettingsButton", this.AddSettingsButton.ClientID);
      descriptorPrivate.AddProperty("showDefaultPipes", (object) this.ShowDefaultPipes);
      if (this.ShowDefaultPipes)
        descriptorPrivate.AddProperty("defaultPipes", (object) this.GetPreAddedPipeSettings());
      descriptorPrivate.AddProperty("disableRemoving", (object) this.DisableRemoving);
      descriptorPrivate.AddProperty("changePipeText", (object) this.ChangePipeText);
      descriptorPrivate.AddProperty("disableActivation", (object) this.DisableActivation);
      if (!string.IsNullOrEmpty(this.currentDefinition.DefaultPipeName))
        descriptorPrivate.AddProperty("defaultCreatePipe", (object) this.GetDefaultCreateSettings(this.currentDefinition.DefaultPipeName));
      descriptorPrivate.AddProperty("_outBoundPipesMode", (object) this.WorkWithOutboundPipes);
      descriptorPrivate.AddProperty("_publishingPointDefinitionFieldName", (object) this.PublishingPointDefinitionFieldName);
      descriptorPrivate.AddProperty("_publishingPointNameFieldName", (object) this.PublishingPointNameFieldName);
      descriptorPrivate.AddProperty("showContentLocation", (object) this.ShowContentLocation);
      if (this.BackLinksPagePicker != null)
        descriptorPrivate.AddComponentProperty("backLinksPagePicker", this.BackLinksPagePicker.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        descriptorPrivate
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
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (PublishingManager).Assembly.FullName;
      scriptReferenceList.Add(new ScriptReference()
      {
        Assembly = typeof (TaxonField).Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ListPipeSettingsField.js"
      });
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.FieldChangeNotifier.js", fullName));
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.IDataItemAccessField.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }

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

    /// <summary>
    /// Gets the reference to the the client binder that binds the existing pipes.
    /// </summary>
    /// <value></value>
    protected internal virtual ClientBinder ExistingPipesBinder => this.Container.GetControl<ClientBinder>("existingPipesBinder", true);

    /// <summary>Gets the RadWindowManager.</summary>
    /// <value>The RadWindowManager.</value>
    protected internal virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>Gets the add settings button.</summary>
    /// <value>The add settings button.</value>
    protected internal virtual WebControl AddSettingsButton => this.Container.GetControl<WebControl>("addSettingsButton", true);

    /// <summary>Gets the add settings button label.</summary>
    /// <value>The add settings button label.</value>
    protected internal virtual ITextControl AddSettingsButtonLabel => this.Container.GetControl<ITextControl>("addSettingsButtonLabel", true);

    /// <summary>Gets the back links page picker.</summary>
    /// <value>The back links page picker.</value>
    public PageField BackLinksPagePicker { get; set; }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      this.currentDefinition = definition as IListPipeSettingsFieldDefinition;
      if (this.currentDefinition == null)
        return;
      this.ResourceClassId = this.currentDefinition.ResourceClassId;
      this.DisableAdding = this.currentDefinition.DisableAdding;
      this.DisableRemoving = this.currentDefinition.DisableRemoving;
      this.AddPipeText = Res.ResolveLocalizedValue(this.currentDefinition.ResourceClassId, this.currentDefinition.AddPipeText);
      this.ChangePipeText = Res.ResolveLocalizedValue(this.currentDefinition.ResourceClassId, this.currentDefinition.ChangePipeText);
      this.DisableActivation = this.currentDefinition.DisableActivation;
      this.ShowDefaultPipes = this.currentDefinition.ShowDefaultPipes;
      this.WorkWithOutboundPipes = this.currentDefinition.WorkWithOutboundPipes;
      this.ShowContentLocation = this.currentDefinition.ShowContentLocation;
    }

    protected override void InitializeControls(GenericContainer container)
    {
      this.AddSettingsButton.Style[HtmlTextWriterStyle.Display] = this.DisableAdding ? "none" : (string) null;
      this.TitleLabel.SetTextOrHide(this.Title);
      this.DescriptionLabel.SetTextOrHide(this.Description);
      this.ExampleLabel.SetTextOrHide(this.Example);
      if (!string.IsNullOrEmpty(this.AddPipeText))
        this.AddSettingsButtonLabel.Text = this.AddPipeText;
      HtmlGenericControl control = this.Container.GetControl<HtmlGenericControl>("selectorContainer", true);
      PageField child = new PageField();
      child.RootNodeID = SiteInitializer.CurrentFrontendRootNodeId;
      child.WebServiceUrl = "~/Sitefinity/Services/Pages/PagesService.svc/";
      child.CssClass = "sfFormSeparator";
      this.BackLinksPagePicker = child;
      control.Controls.Add((Control) child);
    }

    /// <summary>
    /// Gets a list of pipe settings that should be added to the control
    /// </summary>
    /// <returns></returns>
    private IList<WcfPipeSettings> GetPreAddedPipeSettings() => (IList<WcfPipeSettings>) new List<WcfPipeSettings>()
    {
      new WcfPipeSettings(PublishingSystemFactory.GetPipe("RSSOutboundPipe").GetDefaultSettings(), this.ProviderName)
    };

    private WcfPipeSettings GetDefaultCreateSettings(string pipeName) => new WcfPipeSettings(pipeName, this.ProviderName);

    /// <summary>
    /// Name of the field in the viewmodel that holds the publishing point definition
    /// </summary>
    public string PublishingPointDefinitionFieldName { get; set; }

    /// <summary>
    /// Name of the field in the viewmodel that contains the tile of the publishing point
    /// </summary>
    public string PublishingPointNameFieldName { get; set; }

    /// <summary>Name of the publishing provider to use</summary>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the control will suggest outbound or inbound pipes when creating a new pipe setting
    /// </summary>
    /// <value><c>true</c> suggest outbound pipes; otherwise, inbound</value>
    public bool WorkWithOutboundPipes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether adding new pipes is possible.
    /// </summary>
    /// <value><c>true</c> if adding new pipes is possible; otherwise, <c>false</c>.</value>
    public bool DisableAdding { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether removing existing pipes is possible.
    /// </summary>
    /// <value><c>true</c> if removing existing pipes is possible; otherwise, <c>false</c>.</value>
    public bool DisableRemoving { get; set; }

    /// <summary>Gets or sets the text of the button for adding pipes.</summary>
    /// <value>The text for adding pipes.</value>
    public string AddPipeText { get; set; }

    /// <summary>
    /// Gets or sets the text of the button for changing pipes.
    /// </summary>
    /// <value>The text for changing pipes.</value>
    public string ChangePipeText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether activation of pipes is possible.
    /// </summary>
    /// <value><c>true</c> if activation of pipes is possible; otherwise, <c>false</c>.</value>
    public bool DisableActivation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show default pipes.
    /// </summary>
    /// <value><c>true</c> if default pipes are shown; otherwise, <c>false</c>.</value>
    public bool ShowDefaultPipes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show content location (default page url) inside the main list of pipes
    /// This is relevant only for the content pipes
    /// </summary>
    public bool ShowContentLocation { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    protected internal virtual ScriptControlDescriptor GetLastScriptDescriptor_Private() => (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
  }
}
