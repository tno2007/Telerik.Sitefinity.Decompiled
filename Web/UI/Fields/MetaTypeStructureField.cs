// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// The meta-type editor is used for editing dynamically generated types, which consits of a collection of fields.
  /// It consists of a LinkButton, when clicked it opens a dialog, where the dynamic type can be edited.
  /// </summary>
  public class MetaTypeStructureField : FieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.MetaTypeStructureField.ascx");
    private const string publishingPointStructureFieldJS = "Telerik.Sitefinity.Web.UI.Fields.Scripts.MetaTypeStructureField.js";

    /// <summary>Gets the open structure link.</summary>
    protected internal virtual LinkButton OpenStructureLink => this.Container.GetControl<LinkButton>("openStructureLink", true);

    /// <summary>Gets the fields count label.</summary>
    protected internal virtual Label FieldsCountLabel => this.Container.GetControl<Label>("fieldsCount", true);

    /// <summary>Gets the structure edit window.</summary>
    protected internal virtual Telerik.Web.UI.RadWindow StructureEditWindow => this.Container.GetControl<Telerik.Web.UI.RadWindow>("structureEditWindow", true);

    /// <summary>Gets the label manager.</summary>
    /// <value>The label manager.</value>
    protected internal virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField" /> class.
    /// </summary>
    public MetaTypeStructureField() => this.LayoutTemplatePath = MetaTypeStructureField.layoutTemplatePath;

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    /// <remarks>This control works in any mode.</remarks>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel_write", true);

    /// <summary>Gets or sets the edit button text resource.</summary>
    /// <value>The edit button text resource.</value>
    public string EditButtonText { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.OpenStructureLink.Text = this.EditButtonText;
      this.TitleLabel.Text = this.Title;
    }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) null;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IMetaTypeStructureFieldDefinition structureFieldDefinition) || string.IsNullOrWhiteSpace(structureFieldDefinition.EditButtonTextResource))
        return;
      this.EditButtonText = Res.Get(definition.ResourceClassId, structureFieldDefinition.EditButtonTextResource);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_openStructureLinkId", (object) this.OpenStructureLink.ClientID);
      controlDescriptor.AddProperty("_structureEditWindowId", (object) this.StructureEditWindow.ClientID);
      controlDescriptor.AddProperty("_fieldsCountLabelId", (object) this.FieldsCountLabel.ClientID);
      controlDescriptor.AddProperty("_clientLabelManagerId", (object) this.LabelManager.ClientID);
      return scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (MetaTypeStructureField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.MetaTypeStructureField.js", fullName)
      };
    }
  }
}
