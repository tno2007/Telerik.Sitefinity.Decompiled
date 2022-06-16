// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.MirrorTextField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// This control is used for inserting/editing/viewing simple text with the built in option to mirror the value
  /// of another text field.
  /// </summary>
  public class MirrorTextField : TextField
  {
    private string regularExpressionFilter;
    private string replaceWith;
    private string mirroredControlId;
    private const string mirrorFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.MirrorTextField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.MirrorTextField" /> class.
    /// </summary>
    public MirrorTextField() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.MirrorTextField.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the regular expression for filtering .</summary>
    /// <value>The filter regular expression.</value>
    public virtual string RegularExpressionFilter
    {
      get => this.regularExpressionFilter;
      set => this.regularExpressionFilter = value;
    }

    /// <summary>
    /// Gets or sets the value that will be replaced for every Regular expression filter match.
    /// </summary>
    /// <value>The value to replace with.</value>
    public virtual string ReplaceWith
    {
      get => this.replaceWith;
      set => this.replaceWith = value;
    }

    /// <summary>Gets the pageId of the mirrored control.</summary>
    /// <value>The pageId of the mirrored control.</value>
    public virtual string MirroredControlId
    {
      get => this.mirroredControlId;
      set => this.mirroredControlId = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show a button that must be clicked in order to change
    /// the value of the control.
    /// </summary>
    public virtual bool EnableChangeButton { get; set; }

    /// <summary>Gets or sets the text of the change button.</summary>
    /// <value>The text of the change button.</value>
    public virtual string ChangeButtonText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to lower the
    /// the value of the control.
    /// </summary>
    public virtual bool ToLower { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to trim the value of this control.
    /// </summary>
    /// <value></value>
    public virtual bool Trim { get; set; }

    /// <summary>
    /// Gets or sets the prefix that will be appended to the mirrored value.
    /// </summary>
    /// <value>The text that will be appended to the mirrored text.</value>
    public virtual string PrefixText { get; set; }

    /// <summary>
    /// Gets the reference the control that represents the web control for edinitng the mirrored value.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal virtual WebControl ChangeControl => (WebControl) this.ChangeLink;

    /// <summary>
    /// Gets the reference to the control that represents the web control that shows the value of the mirroring control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal virtual WebControl MirroredValueLabelControl => (WebControl) this.MirroredValueLabel;

    /// <summary>
    /// Gets the reference the control that represents the link button for edinitng the mirrored value.
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual LinkButton ChangeLink => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<LinkButton>("changeButton_write", true) : this.Container.GetControl<LinkButton>("changeButton_read", false);

    /// <summary>
    /// Gets the reference to the label displaying the mirrored value.
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual Label MirroredValueLabel => this.Container.GetControl<Label>(this.DisplayMode != FieldDisplayMode.Read ? "mirroredValueLabel_write" : "mirroredValueLabel_read", true);

    /// <summary>
    /// Gets the reference to the label displaying the change button text.
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual Literal ChangeButtonTextLiteral => this.Container.GetControl<Literal>("changeButtonTextLiteral", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Configures the field control by the specified definition.
    /// </summary>
    /// <param name="definition">The specified definition.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      MirrorTextFieldDefinition textFieldDefinition = (MirrorTextFieldDefinition) definition;
      this.RegularExpressionFilter = textFieldDefinition.RegularExpressionFilter;
      this.MirroredControlId = textFieldDefinition.MirroredControlId;
      this.ReplaceWith = textFieldDefinition.ReplaceWith;
      this.EnableChangeButton = textFieldDefinition.EnableChangeButton;
      this.ChangeButtonText = textFieldDefinition.ChangeButtonText;
      bool? nullable = textFieldDefinition.ToLower;
      this.ToLower = ((int) nullable ?? 0) != 0;
      nullable = textFieldDefinition.Trim;
      this.Trim = ((int) nullable ?? 0) != 0;
      this.PrefixText = textFieldDefinition.PrefixText;
      this.ShowCharacterCounter = textFieldDefinition.ShowCharacterCounter;
      this.RecommendedCharactersCount = textFieldDefinition.RecommendedCharactersCount;
      this.CharacterCounterDescription = textFieldDefinition.CharacterCounterDescription;
    }

    /// <summary>
    /// The method that is used to set the properties of the contained controls.
    /// </summary>
    protected internal override void ConstructControl()
    {
      base.ConstructControl();
      if (this.DisplayMode != FieldDisplayMode.Write)
        return;
      if (this.EnableChangeButton)
      {
        this.TextBoxControl.Style.Add("display", "none");
      }
      else
      {
        this.MirroredValueLabelControl.Style.Add("display", "none");
        this.ChangeControl.Style.Add("display", "none");
      }
      this.ChangeButtonTextLiteral.Text = this.ChangeButtonText;
      if (this.RecommendedCharactersCount == 0 || this.CharacterCounterDescriptionLabel == null || this.CharacterCounterDescription == null)
        return;
      this.CharacterCounterDescriptionLabel.Text = string.Format(this.CharacterCounterDescription, (object) this.RecommendedCharactersCount);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor scriptDescriptor = this.GetLastScriptDescriptor();
      scriptDescriptor.AddProperty("_replaceWith", (object) this.ReplaceWith);
      scriptDescriptor.AddProperty("_regularExpressionFilter", (object) this.RegularExpressionFilter);
      scriptDescriptor.AddProperty("_mirroredControlId", (object) this.MirroredControlId);
      scriptDescriptor.AddProperty("_enableChangeButton", (object) this.EnableChangeButton);
      scriptDescriptor.AddProperty("toLower", (object) this.ToLower);
      scriptDescriptor.AddProperty("trim", (object) this.Trim);
      scriptDescriptor.AddProperty("prefixText", (object) this.PrefixText);
      if (this.ChangeControl != null)
        scriptDescriptor.AddElementProperty("changeControl", this.ChangeControl.ClientID);
      if (this.MirroredValueLabelControl != null)
        scriptDescriptor.AddElementProperty("mirroredValueLabel", this.MirroredValueLabelControl.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        scriptDescriptor
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
        Assembly = typeof (MirrorTextField).Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.MirrorTextField.js"
      }
    }.ToArray();

    /// <summary>Gets the last script descriptor from the base class.</summary>
    /// <returns></returns>
    protected internal virtual ScriptControlDescriptor GetLastScriptDescriptor() => (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
  }
}
