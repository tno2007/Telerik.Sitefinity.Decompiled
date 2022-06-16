// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.MetaFieldNameTextBox
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  /// <summary>
  /// Represents the textbox that displayes the name of the metafield
  /// </summary>
  public class MetaFieldNameTextBox : SimpleScriptView
  {
    internal const string controlScript = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.MetaFieldNameTextBox.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Forms.MetaFieldNameTextBox.ascx");

    /// <summary>
    /// If true the change button will be visible and will allow changing of the text field
    /// </summary>
    public bool ReadOnly { get; set; }

    public string FieldName { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MetaFieldNameTextBox.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value></value>
    protected WebControl ExpandButton => (WebControl) this.Container.GetControl<LinkButton>("expandButton", true);

    /// <summary>
    /// Gets or sets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value></value>
    protected HtmlGenericControl ExpandableSection => this.Container.GetControl<HtmlGenericControl>("expandableSection", true);

    /// <summary>
    /// Represents the textbox containig custom image width if selected
    /// </summary>
    protected virtual TextField MetaFieldNameTextField => this.Container.GetControl<TextField>("metaFieldNameTextField", true);

    protected virtual LinkButton ChangeButton => this.Container.GetControl<LinkButton>("changeButton", true);

    /// <summary>Initializes the controls.</summary>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.ReadOnly)
        return;
      this.ChangeButton.Attributes.CssStyle.Add("display", "none");
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_fieldName", (object) this.FieldName);
      controlDescriptor.AddProperty("readOnly", (object) this.ReadOnly);
      controlDescriptor.AddElementProperty("changeButton", this.ChangeButton.ClientID);
      controlDescriptor.AddElementProperty("expandButton", this.ExpandButton.ClientID);
      controlDescriptor.AddElementProperty("expandableSection", this.ExpandableSection.ClientID);
      controlDescriptor.AddComponentProperty("metaFieldNameTextField", this.MetaFieldNameTextField.ClientID);
      return (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>()
      {
        (ScriptDescriptor) controlDescriptor
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
      new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.MetaFieldNameTextBox.js", this.GetType().Assembly.FullName)
    };
  }
}
