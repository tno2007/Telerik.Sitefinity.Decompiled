// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.CommonDialogs.FlatSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.CommonDialogs
{
  internal abstract class FlatSelectorDialog : AjaxDialogBase
  {
    private const string LayoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Backend.Dialogs.FlatSelectorDialog.ascx";
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.CommonDialogs.Scripts.FlatSelectorDialog.js";

    public FlatSelectorDialog() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Dialogs.FlatSelectorDialog.ascx");

    public virtual bool AllowPaging { get; set; }

    protected virtual FlatSelector ItemSelector => this.Container.GetControl<FlatSelector>("itemSelector", true);

    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("doneButton", true);

    protected virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("titleLabel", true);

    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("cancelButton", true);

    public override string ClientComponentType => typeof (FlatSelectorDialog).FullName;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected override void InitializeControls(GenericContainer container) => this.ItemSelector.AllowPaging = this.AllowPaging;

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddComponentProperty("itemSelector", this.ItemSelector.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.CommonDialogs.Scripts.FlatSelectorDialog.js", typeof (FlatSelectorDialog).Assembly.FullName)
    };
  }
}
