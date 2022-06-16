// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Designers.JavaScriptEmbedControlDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Designers
{
  /// <summary>
  /// </summary>
  public class JavaScriptEmbedControlDesigner : ContentViewDesignerBase
  {
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.EmbedControls.CssJavaScriptEmbedControlsDesigner.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.Designers.JavaScriptEmbedControlDesigner" /> class.
    /// </summary>
    public JavaScriptEmbedControlDesigner() => this.LayoutTemplatePath = JavaScriptEmbedControlDesigner.layoutTemplatePath;

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.codemirror.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.htmlmixed.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.xml.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.css.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.javascript.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name)
    };

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      JavaScriptFileEmbedDesignerView embedDesignerView = new JavaScriptFileEmbedDesignerView();
      views.Add(embedDesignerView.ViewName, (ControlDesignerView) embedDesignerView);
      JavaScriptEditorDesignerView editorDesignerView = new JavaScriptEditorDesignerView();
      views.Add(editorDesignerView.ViewName, (ControlDesignerView) editorDesignerView);
    }

    protected override void InitializeControls(GenericContainer container)
    {
      this.SetPropertyEditorAdvancedMode();
      base.InitializeControls(container);
    }

    protected internal virtual void SetPropertyEditorAdvancedMode() => this.PropertyEditor.HideAdvancedMode = true;
  }
}
