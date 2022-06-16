// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Services.Web.UI
{
  /// <summary>Represents the module management backend UI</summary>
  public class ModuleDetailsWindow : KendoWindow
  {
    internal new const string scriptReference = "Telerik.Sitefinity.Services.Web.UI.Scripts.ModuleDetailsWindow.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModulesAndServices.Modules.ModuleDetailsWindow.ascx");

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ModuleDetailsWindow.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets a reference to the outer div.</summary>
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("moduleDetailsWindow", true);

    /// <summary>Gets a reference to the module name text field.</summary>
    protected virtual TextField ModuleName => this.Container.GetControl<TextField>("moduleName", true);

    /// <summary>Gets a reference to the module description text area.</summary>
    protected virtual HtmlTextArea ModuleDescription => this.Container.GetControl<HtmlTextArea>("moduleDescription", true);

    /// <summary>Gets a reference to the module type text field.</summary>
    protected virtual TextField ModuleType => this.Container.GetControl<TextField>("moduleType", true);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("moduleName", this.ModuleName.ClientID);
      controlDescriptor.AddElementProperty("moduleDescription", this.ModuleDescription.ClientID);
      controlDescriptor.AddComponentProperty("moduleType", this.ModuleType.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Services.Web.UI.Scripts.ModuleDetailsWindow.js", typeof (ModuleDetailsWindow).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;
  }
}
