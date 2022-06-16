// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.CampaignsStatisticsDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms
{
  /// <summary>
  /// This dialog shows statistics of issues related to newsletter module.
  /// </summary>
  public class CampaignsStatisticsDialog : AjaxDialogBase
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.CampaignsStatisticsDialog.js";
    private string laytoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Forms.CampaignsStatisticsDialog.ascx";

    /// <summary>Gets the type of the client component.</summary>
    public override string ClientComponentType => this.GetType().FullName;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    protected override string LayoutTemplateName => this.laytoutTemplateName;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the title for text at the head of the dialog.</summary>
    protected virtual Label CampaignStatsTitle => this.Container.GetControl<Label>("campaignStatsTitle", true);

    /// <summary>Gets the stats grid.</summary>
    protected virtual ItemsGrid StatsGrid => this.Container.GetControl<ItemsGrid>("statsGrid", true);

    /// <summary>Gets the client label manager.</summary>
    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("labelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>Gets the script descriptors.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("campaignStatsTitle", this.CampaignStatsTitle.ClientID);
      controlDescriptor.AddComponentProperty("statsGrid", this.StatsGrid.ClientID);
      controlDescriptor.AddComponentProperty("statsBinder", this.StatsGrid.Binder.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>Gets the script references.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.CampaignsStatisticsDialog.js", typeof (CampaignsStatisticsDialog).Assembly.FullName)
    };
  }
}
