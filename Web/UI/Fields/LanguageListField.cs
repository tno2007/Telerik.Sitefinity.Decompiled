// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.LanguageListField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  public class LanguageListField : FieldControl, ICommandField
  {
    private bool? isToRender;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.LanguageListField.ascx");
    private const string SelfExecutableScriptName = "Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js";
    private const string CommandFieldScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js";
    private const string LanguageListFieldScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.LanguageListField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.LanguageListField" /> class.
    /// </summary>
    public LanguageListField() => this.LayoutTemplatePath = LanguageListField.layoutTemplatePath;

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) this.LanguageListControl.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) null;

    /// <summary>Gets the language list control.</summary>
    /// <value>The language list control.</value>
    public LanguageListControl LanguageListControl => this.Container.GetControl<LanguageListControl>("list", true);

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.LanguageListControl.TitleLabel.Text = this.Title;

    /// <summary>
    /// Gets or sets a value that indicates whether a server control is rendered as UI on the page.
    /// </summary>
    /// <value></value>
    /// <returns>true if the control is visible on the page; otherwise false.</returns>
    public override bool Visible
    {
      get
      {
        if (!this.isToRender.HasValue)
          this.isToRender = new bool?(SystemManager.CurrentContext.AppSettings.Multilingual);
        return this.isToRender.Value;
      }
      set => this.isToRender = new bool?(value);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("languageListControl", this.LanguageListControl.ClientID);
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
      string fullName = typeof (LanguageListField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.LanguageListField.js", fullName)
      };
    }
  }
}
