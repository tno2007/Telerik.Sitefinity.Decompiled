// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ColorPickerField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  ///  A field control used for displaying and editing choices from RadColorPicker.
  /// </summary>
  public class ColorPickerField : FieldControl
  {
    private bool showIcon = true;
    private Collection<ChoiceItem> colorPickerItems;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ColorPickerField.ascx");
    internal const string script = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ColorPickerField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ColorPickerField" /> class.
    /// </summary>
    public ColorPickerField() => this.LayoutTemplatePath = ColorPickerField.layoutTemplatePath;

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    public override object Value { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets a value indicating whether to show icon.</summary>
    private bool ShowIcon { get; set; }

    /// <summary>
    /// Gets or sets a value indicating one of the 25 preset color palettes.
    /// </summary>
    private ColorPreset Preset { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show an empty color button.
    /// </summary>
    private bool ShowEmptyColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show color preview color.
    /// </summary>
    private bool EnableColorPreview { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the recently used colors.
    /// </summary>
    private bool ShowRecentlyUsedColors { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show custom color button.
    /// </summary>
    private bool EnableCustomColor { get; set; }

    /// <summary>Gets the color picker items</summary>
    /// <value>The collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ChoiceItem" /> objects.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual Collection<ChoiceItem> ColorPickerItems
    {
      get
      {
        if (this.colorPickerItems == null)
          this.colorPickerItems = new Collection<ChoiceItem>();
        return this.colorPickerItems;
      }
    }

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Web.UI.RadColorPicker" />.
    /// </summary>
    protected virtual RadColorPicker ColorPicker => this.Container.GetControl<RadColorPicker>("colorPicker", true);

    /// <summary>
    /// Gets the reference to the label control that displays the title of field.
    /// </summary>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

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
      controlDescriptor.AddComponentProperty("colorPicker", this.ColorPicker.ClientID);
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
      string fullName = typeof (ColorPickerField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ColorPickerField.js", fullName)
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
      if (!(definition is IColorPickerFieldDefinition pickerFieldDefinition))
        return;
      this.ShowIcon = pickerFieldDefinition.ShowIcon;
      this.ShowEmptyColor = pickerFieldDefinition.ShowEmptyColor;
      this.ShowRecentlyUsedColors = pickerFieldDefinition.ShowRecentlyUsedColors;
      this.EnableColorPreview = pickerFieldDefinition.EnableColorPreview;
      this.EnableCustomColor = pickerFieldDefinition.EnableCustomColor;
      if (pickerFieldDefinition.Preset != ColorPreset.None)
      {
        this.Preset = pickerFieldDefinition.Preset;
      }
      else
      {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof (Color));
        foreach (IChoiceDefinition colorPickerItem in pickerFieldDefinition.ColorPickerItems)
          this.ColorPicker.Items.Add(new ColorPickerItem((Color) converter.ConvertFromString(colorPickerItem.Value), colorPickerItem.Text));
      }
    }

    /// <summary>
    /// The method that is used to set the properties of the contained controls.
    /// </summary>
    protected internal virtual void ConstructControl()
    {
      this.TitleLabel.SetTextOrHide(this.Title);
      this.ColorPicker.ShowIcon = this.ShowIcon;
      this.ColorPicker.ShowEmptyColor = this.ShowEmptyColor;
      this.ColorPicker.ShowRecentColors = this.ShowRecentlyUsedColors;
      this.ColorPicker.PreviewColor = this.EnableColorPreview;
      this.ColorPicker.EnableCustomColor = this.EnableCustomColor;
      if (this.Preset == ColorPreset.None)
        return;
      this.ColorPicker.Preset = this.Preset;
    }
  }
}
