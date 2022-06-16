// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.Designers.LanguageSelectorDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Localization.Web.UI.Designers
{
  /// <summary>A designer for the LanguageSelector control</summary>
  public class LanguageSelectorDesigner : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.LanguageSelectorDesigner.ascx");
    private const string designerScriptName = "Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguageSelectorDesigner.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Web.UI.Designers.LanguageSelectorDesigner" /> class.
    /// </summary>
    public LanguageSelectorDesigner() => this.LayoutTemplatePath = LanguageSelectorDesigner.layoutTemplatePath;

    /// <summary>
    /// Gets the checkbox indicating whether to include the current language in the list of available languages.
    /// </summary>
    /// <value>The checkbox indicating whether to include the current language in the list of available languages.</value>
    protected ChoiceField IncludeCurrentCheckbox => this.Container.GetControl<ChoiceField>("includeCurrentCheckbox", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ((ScriptComponentDescriptor) source.Last<ScriptDescriptor>()).AddComponentProperty("includeCurrentCheckbox", this.IncludeCurrentCheckbox.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Localization.Web.UI.Scripts.LanguageSelectorDesigner.js", typeof (LanguageSelectorDesigner).Assembly.FullName)
    }.ToArray();
  }
}
