// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.ScheduleDeliveryWindow
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  public class ScheduleDeliveryWindow : KendoWindow
  {
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.ScheduleDeliveryWindow.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.ScheduleDeliveryWindow.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.ScheduleDeliveryWindow" /> class.
    /// </summary>
    public ScheduleDeliveryWindow()
    {
      this.Width = 425;
      this.IsResizable = false;
      this.IsModal = true;
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ScheduleDeliveryWindow.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets a reference to the outer div.</summary>
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("scheduleDeliveryWindow", true);

    /// <summary>Gets a reference to the cancel button.</summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("cancelButton", true);

    /// <summary>Gets a reference to the schedule button.</summary>
    protected virtual LinkButton ScheduleButton => this.Container.GetControl<LinkButton>("scheduleButton", true);

    /// <summary>Gets a reference to the schedule delivery date.</summary>
    protected virtual DateField ScheduleDeliveryDate => this.Container.GetControl<DateField>("scheduleDeliveryDate", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("scheduleButton", this.ScheduleButton.ClientID);
      controlDescriptor.AddComponentProperty("scheduleDeliveryDate", this.ScheduleDeliveryDate.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.ScheduleDeliveryWindow.js", typeof (ScheduleDeliveryWindow).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;
  }
}
