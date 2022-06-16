// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.MetaTypeStructureEditorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Web.UI
{
  public class MetaTypeStructureEditorDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.MetaTypeStructureEditorDialog.ascx");
    private const string FeedStructureDesignerJS = "Telerik.Sitefinity.Web.UI.Scripts.MetaTypeStructureEditorDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.MetaTypeStructureEditorDialog" /> class.
    /// </summary>
    public MetaTypeStructureEditorDialog() => this.LayoutTemplatePath = MetaTypeStructureEditorDialog.layoutTemplatePath;

    /// <summary>Represents the text filed that holds the CSS styles</summary>
    protected internal virtual MetaTypeEditor TypeEditor => this.Container.GetControl<MetaTypeEditor>("metaTypeEditor", true);

    private LinkButton SaveAndCloseButton => this.Container.GetControl<LinkButton>("btnSaveAndClose", true);

    private LinkButton CancelButton => this.Container.GetControl<LinkButton>("btnCancel", true);

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

    public override string ClientComponentType => typeof (MetaTypeStructureEditorDialog).FullName;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_metaTypeEditorID", (object) this.TypeEditor.ClientID);
      controlDescriptor.AddProperty("_saveAndCloseButtonID", (object) this.SaveAndCloseButton.ClientID);
      controlDescriptor.AddProperty("_cancelButtonID", (object) this.CancelButton.ClientID);
      return scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (MetaTypeStructureEditorDialog).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.MetaTypeStructureEditorDialog.js", fullName)
      };
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
