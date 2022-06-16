// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Mapping
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Publishing.Web.UI
{
  /// <summary>
  /// User interface for <see cref="T:Telerik.Sitefinity.Publishing.Model.MappingSettings" />
  /// </summary>
  public class Mapping : SimpleScriptView
  {
    /// <summary>Name of the javascript component</summary>
    internal const string JsComponenName = "Telerik.Sitefinity.Publishing.Web.UI.Mapping";
    /// <summary>
    /// Path to the embedded resource file that contains the javascript component source
    /// </summary>
    internal const string JsComponentPath = "Telerik.Sitefinity.Publishing.Web.UI.Scripts.Mapping.js";
    /// <summary>
    /// Path to the embedded resource file that contains the ascx layout template
    /// </summary>
    public static readonly string AscxComponentPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.Mapping.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.UI.Mapping" /> class.
    /// </summary>
    public Mapping() => this.LayoutTemplatePath = Mapping.AscxComponentPath;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptDescriptor[] scriptDescriptors = new ScriptDescriptor[1];
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor("Telerik.Sitefinity.Publishing.Web.UI.Mapping", this.ClientID);
      controlDescriptor.AddProperty("_titleElem", (object) this.TitleElement.ClientID);
      controlDescriptor.AddProperty("_templateElem", (object) this.TemplateElement.ClientID);
      controlDescriptor.AddProperty("_targetElem", (object) this.TargetElement.ClientID);
      scriptDescriptors[0] = (ScriptDescriptor) controlDescriptor;
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.Mapping.js", typeof (Mapping).Assembly.GetName().Name)
    };

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Reference to the template element that holds the title
    /// </summary>
    protected virtual Control TitleElement => this.Container.GetControl<Control>("title", true);

    /// <summary>
    /// Reference to the template element that holds the template
    /// </summary>
    protected virtual Control TemplateElement => this.Container.GetControl<Control>("template", true);

    /// <summary>
    /// Reference to the template element that holds the template instantiation element (target)
    /// </summary>
    protected virtual Control TargetElement => this.Container.GetControl<Control>("target", true);
  }
}
