// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  /// <summary>
  /// Designer control for the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockBase" /> control.
  /// </summary>
  public class ContentBlockDesignerBase : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.GenericContent.ContentBlockDesignerBase.ascx");
    internal const string designerScriptName = "Telerik.Sitefinity.Modules.GenericContent.Web.UI.Scripts.ContentBlockDesignerBase.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentBlockDesignerBase.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the name of the javascript type that the designer will use.
    /// The designers can reuse for exampel the base class implementation and just customize some labels
    /// </summary>
    /// <value>The name of the script descriptor type.</value>
    protected override string ScriptDescriptorTypeName => typeof (ContentBlockDesignerBase).FullName;

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.HtmlField" /> control that is used
    /// for editing the HTML content of <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock" /> control.
    /// </summary>
    protected virtual HtmlField HtmlEditor => this.Container.GetControl<HtmlField>("htmlEditor", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.DesignerMode = ControlDesignerModes.Simple;
      this.AdvancedModeIsDefault = false;
      if (this.PropertyEditor == null)
        return;
      this.HtmlEditor.UICulture = this.PropertyEditor.PropertyValuesCulture;
    }

    /// <summary>Gets the script descriptors.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ((ScriptComponentDescriptor) source.Last<ScriptDescriptor>()).AddComponentProperty("htmlEditor", this.HtmlEditor.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>Gets the script references.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ContentBlockDesignerBase).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.GenericContent.Web.UI.Scripts.ContentBlockDesignerBase.js", fullName)
      }.ToArray();
    }
  }
}
