// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormTextBox
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  [ControlDesigner(typeof (FormTextBoxDesigner))]
  [PropertyEditorTitle(typeof (FormsResources), "TextBox")]
  [DatabaseMapping(UserFriendlyDataType.ShortText)]
  public class FormTextBox : TextField, IMultiDisplayModesSupport, IFormFieldControl, IValidatable
  {
    internal const string fieldScript = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormTextBox.js";
    private IMetaField metaField;
    private bool isHidden;

    public FormTextBox()
    {
      this.LoadDefaultMetaField();
      this.DisplayMode = FieldDisplayMode.Write;
      this.TextBoxSize = FormControlSize.Medium;
      this.Title = Res.Get<FormsResources>().Untitled;
    }

    /// <summary>Metafield that represents the texbox control</summary>
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [NonMultilingual]
    public IMetaField MetaField
    {
      get
      {
        if (this.metaField == null)
          this.metaField = (IMetaField) this.LoadDefaultMetaField();
        return this.metaField;
      }
      set => this.metaField = value;
    }

    /// <summary>The default value of the control</summary>
    [MultilingualProperty]
    public string DefaultStringValue
    {
      get => (string) this.DefaultValue;
      set => this.DefaultValue = (object) value;
    }

    /// <summary>Represents the size of the text box</summary>
    public FormControlSize TextBoxSize { get; set; }

    /// <summary>Gets or sets the read only modes.</summary>
    /// <value>The read only modes.</value>
    [TypeConverter(typeof (StringArrayConverter))]
    public string[] ReadOnlyModes { get; set; }

    /// <summary>Gets or sets the hidden modes.</summary>
    /// <value>The hidden modes.</value>
    [TypeConverter(typeof (StringArrayConverter))]
    public string[] HiddenModes { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.AddCssClass("sfFormBox");
    }

    /// <summary>
    /// Raises the <see cref="E:PreRender" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      if (this.TextBoxSize != FormControlSize.None)
        this.AddCssClass("sfTxtBox" + (object) this.TextBoxSize);
      if (string.IsNullOrEmpty(this.Value as string))
        this.Value = this.DefaultValue;
      base.OnPreRender(e);
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormTextBox.js", typeof (FormTextBox).Assembly.FullName)
    };
  }
}
