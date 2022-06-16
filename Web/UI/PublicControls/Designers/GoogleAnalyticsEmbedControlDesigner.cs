// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Designers.GoogleAnalyticsEmbedControlDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Designers
{
  /// <summary>
  /// </summary>
  public class GoogleAnalyticsEmbedControlDesigner : ControlDesignerBase
  {
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.EmbedControls.GoogleAnalyticsEmbedControlDesigner.ascx");
    internal const string designerViewJs = "Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.GoogleAnalyticsEmbedControlDesigner.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? GoogleAnalyticsEmbedControlDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Represents the text filed that holds the CSS styles</summary>
    protected internal virtual TextField GoogleAnalyticsCodeTextField => this.Container.GetControl<TextField>("googleAnalyticsCodeTextField", true);

    /// <summary>Represents the script position embed options</summary>
    protected internal virtual ChoiceField ScriptEmbedPositionChoiceField => this.Container.GetControl<ChoiceField>("scriptEmbedPositionChoiceField", true);

    protected override void InitializeControls(GenericContainer container)
    {
      this.DesignerMode = ControlDesignerModes.Simple;
      this.AdvancedModeIsDefault = false;
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
      List<ScriptDescriptor> scriptDescriptors = this.GetBaseScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) scriptDescriptors.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("googleAnalyticsCodeTextField", this.GoogleAnalyticsCodeTextField.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = this.GetBaseScriptReferences();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.GoogleAnalyticsEmbedControlDesigner.js", this.GetType().Assembly.GetName().ToString()));
      return (IEnumerable<ScriptReference>) scriptReferences.ToArray();
    }

    protected internal virtual List<ScriptDescriptor> GetBaseScriptDescriptors() => new List<ScriptDescriptor>(base.GetScriptDescriptors());

    protected internal virtual List<ScriptReference> GetBaseScriptReferences() => new List<ScriptReference>(base.GetScriptReferences());
  }
}
