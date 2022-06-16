// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ExpandableField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a control a part of which is hidden and can be expanded after the user clicks on a expand control.
  /// </summary>
  public class ExpandableField : FieldControl
  {
    private List<Control> expandableFields;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ExpandableField.ascx");
    private const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ExpandableField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ExpandableField" /> class.
    /// </summary>
    public ExpandableField() => this.LayoutTemplatePath = ExpandableField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets a collection of field controls.</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<Control> ExpandableFields
    {
      get
      {
        if (this.expandableFields == null)
          this.expandableFields = new List<Control>();
        return this.expandableFields;
      }
    }

    /// <summary>Gets or sets the text of the expand field choice.</summary>
    public virtual string ExpandFieldChoiceText { get; set; }

    /// <summary>
    /// Gets or sets the way in which choice of the expand field should be rendered.
    /// </summary>
    /// <value>One of the values of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Enums.RenderChoicesAs" /> enumeration.</value>
    public virtual RenderChoicesAs RenderExpandFieldChoiceAs { get; set; }

    /// <summary>Gets or sets the expand field definition.</summary>
    /// <value>The expand field definition.</value>
    public virtual IChoiceFieldDefinition ExpandFieldDefinition { get; set; }

    /// <summary>
    /// Invokes GetConditionalControlName and calls this.Container.GetControl with the resulting name
    /// </summary>
    /// <typeparam name="T">Type of the control to load</typeparam>
    /// <param name="originalName">Name of the control without suffixes</param>
    /// <param name="required">True if an exception should be thrown if the control is not found</param>
    /// <returns>Loaded control or null when not found and required is false</returns>
    protected T GetConditionalControl<T>(string originalName, bool required) => this.Container.GetControl<T>(this.GetConditionalControlName(originalName), required);

    /// <summary>
    /// Adds a suffix depending on this.DisplayMode and this.RenderChoiceAs, this making a unique System.Web.Control.ID
    /// </summary>
    /// <param name="originalName">The common part of the name (e.g. titleLabel)</param>
    /// <returns>Unique ID for the control within a conditional template depending on this.DisplayMode and this.RenderChoiceAs</returns>
    protected virtual string GetConditionalControlName(string originalName)
    {
      string str = this.DisplayMode.ToString();
      return originalName + "_" + str;
    }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control wrapping expandable fields.
    /// </summary>
    protected internal virtual PlaceHolder FieldsWrapper => this.GetConditionalControl<PlaceHolder>("fieldsWrapper", true);

    /// <summary>
    /// Gets or sets the reference to the control that is hidden when <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ExpandableField" /> is not expanded and displayed
    /// upon clicking of the <see cref="P:Telerik.Sitefinity.Web.UI.Fields.ExpandableField.ExpandField" />.
    /// </summary>
    /// <value></value>
    public WebControl ExpandableTarget => this.GetConditionalControl<WebControl>("expandableTarget", true);

    /// <summary>Gets the reference to the expand field control.</summary>
    /// <value>The expand field control.</value>
    public ChoiceField ExpandField => this.GetConditionalControl<ChoiceField>("expandField", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ExpandFieldDefinition != null)
      {
        this.ExpandField.Configure((IFieldDefinition) this.ExpandFieldDefinition);
      }
      else
      {
        this.ExpandField.DataFieldName = this.DataFieldName;
        this.ExpandField.CssClass = this.CssClass;
        this.ExpandField.RenderChoicesAs = this.RenderExpandFieldChoiceAs;
        this.ExpandField.DisplayMode = this.DisplayMode;
        ChoiceItem choice = this.ExpandField.Choices[0];
        if (choice != null)
          choice.Text = this.ExpandFieldChoiceText;
      }
      foreach (Control expandableField in this.ExpandableFields)
        this.FieldsWrapper.Controls.Add(expandableField);
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      if (!(definition is IExpandableFieldDefinition expandableFieldDefinition))
        return;
      this.ExpandFieldDefinition = expandableFieldDefinition.ExpandFieldDefinition;
      foreach (IFieldDefinition expandableField in expandableFieldDefinition.ExpandableFields)
        this.ExpandableFields.Add(this.GetConfiguredFieldControl(expandableField) as Control);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) this.GetBaseScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("expandField", this.ExpandField.ClientID);
      controlDescriptor.AddElementProperty("expandableTarget", this.ExpandableTarget.ClientID);
      scriptDescriptorList.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ExpandableField.js", typeof (ExpandableField).Assembly.FullName)
    };

    /// <summary>Configures the base definition.</summary>
    /// <param name="definition">The definition.</param>
    public virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    /// <summary>Gets the base script descriptors.</summary>
    /// <returns></returns>
    public virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    /// <summary>Gets the field control by the provided definition.</summary>
    /// <param name="definition">The definition.</param>
    /// <returns></returns>
    protected internal IField GetConfiguredFieldControl(IFieldDefinition definition) => ObjectFactory.Resolve<IFieldFactory>().GetFieldControl(definition);
  }
}
