// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// Control that provides functionality for image/video emebedding
  /// </summary>
  [RequiresDataItem]
  public class EmbedControl : FieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.EmbedControl.ascx");
    internal const string script = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.EmbedControl.js";
    private string defaultHtml5EmbedStringTemplate;

    /// <summary>Gets or sets the sizes choice field definition.</summary>
    /// <value>The sizes choice field definition.</value>
    public virtual IChoiceFieldDefinition SizesChoiceFieldDefinition { get; set; }

    /// <summary>
    /// String template used to generate the embed html
    /// </summary>
    public virtual string EmbedStringTemplate { get; set; }

    /// <summary>
    /// Gets or sets the default HTML5 embed string template that will be provided for Html5 videos if EmbedStringTemplate is not set.
    /// </summary>
    /// <value>The default HTML5 embed string template.</value>
    public virtual string DefaultHtml5EmbedStringTemplate
    {
      get
      {
        if (this.defaultHtml5EmbedStringTemplate == null)
          this.DefaultHtml5EmbedStringTemplate = VideosDefinitions.GetHtml5VideoEmbedStringTemplate();
        return this.defaultHtml5EmbedStringTemplate;
      }
      set => this.defaultHtml5EmbedStringTemplate = value;
    }

    /// <summary>The title of the customize button</summary>
    public virtual string CustomizeButtonTitle { get; set; }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EmbedControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// If set will hide the text box with the code for embedding in a page
    /// </summary>
    public bool HideEmbedTextBox { get; set; }

    /// <summary>
    /// Gets or sets the mode of the content that will be embeded - documents/videos/images.
    /// </summary>
    /// <value>The mode.</value>
    public string Mode { get; set; }

    protected internal HtmlGenericControl RootPanel => this.Container.GetControl<HtmlGenericControl>("rootPanel", true);

    /// <summary>
    /// Gets the reference to the label that displays the title of the status field.
    /// </summary>
    protected internal override WebControl TitleControl => (WebControl) this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label that displays the description of the status field.
    /// </summary>
    protected internal override WebControl DescriptionControl => (WebControl) this.Container.GetControl<Label>("descriptionLabel", true);

    /// <summary>
    /// Gets the reference to the label that displays the example of the status field.
    /// </summary>
    protected internal override WebControl ExampleControl => (WebControl) this.Container.GetControl<Label>("exampleLabel", true);

    /// <summary>Panel that contains the link and embed fields</summary>
    protected internal virtual Panel LinkHtmlPanel => this.Container.GetControl<Panel>("linkHtmlPanel", true);

    /// <summary>Panel that contains the custom sizes radio buttons</summary>
    protected internal virtual Panel CustomizePanel => this.Container.GetControl<Panel>("customizePanel", true);

    /// <summary>
    /// Panel that contains the custom width and height fields
    /// </summary>
    protected internal virtual Panel CustomSizesPanel => this.Container.GetControl<Panel>("customSizesPanel", true);

    /// <summary>Text field control that displays the media url</summary>
    protected internal virtual TextField LinkTextField => this.Container.GetControl<TextField>("linkTextField", true);

    /// <summary>Text field control that displays the embed html</summary>
    protected internal virtual TextField EmbedTextField => this.Container.GetControl<TextField>("embedTextField", true);

    /// <summary>Text field control that contains the custom width</summary>
    protected internal virtual TextField CustomWidthTextField => this.Container.GetControl<TextField>("customWidthTextField", true);

    /// <summary>Text field control that contains the custom height</summary>
    protected internal virtual TextField CustomHeightTextField => this.Container.GetControl<TextField>("customHeightTextField", true);

    /// <summary>
    /// Text field control that displays the customized embed html code
    /// </summary>
    protected internal virtual TextField CustomizedEmbedTextField => this.Container.GetControl<TextField>("customizedEmbedTextField", true);

    /// <summary>
    /// Choice field control that represents the different sizes for embedding
    /// </summary>
    protected internal virtual ChoiceField SizesChoiceField => this.Container.GetControl<ChoiceField>("sizesChoiceField", true);

    /// <summary>The button that expands the customization panel</summary>
    protected internal virtual LinkButton CustomizeButton => this.Container.GetControl<LinkButton>("customizeButton", true);

    /// <summary>The button that closes the customization panel</summary>
    protected internal virtual LinkButton CloseButton => this.Container.GetControl<LinkButton>("closeButton", true);

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      this.ConfigureControl((IEmbedControlDefinition) definition);
    }

    /// <summary>Configures the fields with the definition.</summary>
    /// <param name="statusFieldDefinition">The status field definition.</param>
    internal virtual void ConfigureControl(IEmbedControlDefinition statusFieldDefinition)
    {
      this.SizesChoiceFieldDefinition = statusFieldDefinition.SizesChoiceFieldDefinition;
      this.EmbedStringTemplate = statusFieldDefinition.EmbedStringTemplate;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SizesChoiceField.Configure((IFieldDefinition) this.SizesChoiceFieldDefinition);
      this.SizesChoiceField.Choices.Insert(0, new ChoiceItem()
      {
        Text = Res.Get<ImagesResources>().Original,
        Value = "original"
      });
      this.SizesChoiceField.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<ImagesResources>().Custom,
        Value = "custom"
      });
      if (!string.IsNullOrEmpty(this.CustomizeButtonTitle))
        this.CustomizeButton.Text = this.CustomizeButtonTitle;
      this.CustomizePanel.Style.Add("display", "none");
      this.CustomSizesPanel.Style.Add("display", "none");
      if (!this.HideEmbedTextBox)
        return;
      this.EmbedTextField.Style.Add("display", "none");
      this.CustomizeButton.Style.Add("display", "none");
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_embedStringTemplate", (object) this.EmbedStringTemplate);
      controlDescriptor.AddProperty("_defaultHtml5EmbedStringTemplate", (object) this.DefaultHtml5EmbedStringTemplate);
      controlDescriptor.AddProperty("_domain", (object) this.GetDomainUrl());
      controlDescriptor.AddProperty("_mode", (object) this.Mode);
      controlDescriptor.AddElementProperty("rootPanel", this.RootPanel.ClientID);
      controlDescriptor.AddElementProperty("linkHtmlPanel", this.LinkHtmlPanel.ClientID);
      controlDescriptor.AddElementProperty("customizePanel", this.CustomizePanel.ClientID);
      controlDescriptor.AddElementProperty("customSizesPanel", this.CustomSizesPanel.ClientID);
      controlDescriptor.AddElementProperty("customizeButton", this.CustomizeButton.ClientID);
      controlDescriptor.AddElementProperty("closeButton", this.CloseButton.ClientID);
      controlDescriptor.AddComponentProperty("linkTextField", this.LinkTextField.ClientID);
      controlDescriptor.AddComponentProperty("embedTextField", this.EmbedTextField.ClientID);
      controlDescriptor.AddComponentProperty("customWidthTextField", this.CustomWidthTextField.ClientID);
      controlDescriptor.AddComponentProperty("customHeightTextField", this.CustomHeightTextField.ClientID);
      controlDescriptor.AddComponentProperty("sizesChoiceField", this.SizesChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("customizedEmbedTextField", this.CustomizedEmbedTextField.ClientID);
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
      string fullName = typeof (EmbedControl).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.EmbedControl.js", fullName)
      };
    }

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    internal virtual string GetDomainUrl() => UrlPath.GetDomainUrl();
  }
}
