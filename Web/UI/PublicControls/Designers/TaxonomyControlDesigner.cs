// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Designers.TaxonomyControlDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Designers
{
  public class TaxonomyControlDesigner : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.PublicControls.TaxonomyControlDesigner.ascx");
    internal const string designerViewInterfaceScriptReference = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.TaxonomyControlDesignerView.js";

    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override string LayoutTemplateName => (string) null;

    public CheckBox ShowItemCountCheckBox => this.Container.GetControl<CheckBox>("showItemCountCheckBox", true);

    public CheckBox ShowEmptyItemsCheckBox => this.Container.GetControl<CheckBox>("showEmptyItemsCheckBox", true);

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TaxonomyControlDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("showItemCountCheckBox", this.ShowItemCountCheckBox.ClientID);
      controlDescriptor.AddElementProperty("showEmptyItemsCheckBox", this.ShowEmptyItemsCheckBox.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = this.GetType().Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.TaxonomyControlDesignerView.js", fullName)
      };
    }
  }
}
