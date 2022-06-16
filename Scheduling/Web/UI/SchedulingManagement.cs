// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Web.UI.SchedulingManagement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Scheduling.Web.UI
{
  /// <summary>Grid for managing scheduled tasks</summary>
  public class SchedulingManagement : KendoView
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.SchedulingManagement.js";
    private readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Scheduling.SchedulingManagement.ascx");

    /// <summary>Gets the layout template path</summary>
    public override string LayoutTemplatePath => this.layoutTemplatePath;

    /// <summary>Gets the layout template name</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid.
    /// </summary>
    protected virtual HtmlContainerControl ScheduledTasksGrid => this.Container.GetControl<HtmlContainerControl>("scheduledTasksManagementGrid", true);

    /// <summary>Gets a reference to the searchWidget</summary>
    protected virtual SearchWidget SearchWidget => this.Container.GetControl<SearchWidget>("searchWidget", true);

    /// <summary>Gets the delete scheduled task confirmation dialog.</summary>
    protected virtual PromptDialog DeleteScheduledTaskPromptDialog => this.Container.GetControl<PromptDialog>("deleteScheduledTaskPromptDialog", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
    {
      List<System.Web.UI.ScriptReference> scriptReferences = new List<System.Web.UI.ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (SchedulingManagement).Assembly.FullName;
      scriptReferences.Add(new System.Web.UI.ScriptReference("Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js", fullName));
      scriptReferences.Add(new System.Web.UI.ScriptReference("Telerik.Sitefinity.Scheduling.Web.UI.Scripts.SchedulingManagement.js", fullName));
      return (IEnumerable<System.Web.UI.ScriptReference>) scriptReferences;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (SchedulingManagement).FullName, this.ClientID);
      string absolute = VirtualPathUtility.ToAbsolute(string.Format("{0}{1}{2}", (object) "~/", (object) "restapi", (object) "/sitefinity/scheduling"));
      controlDescriptor.AddProperty("webServiceUrl", (object) absolute);
      controlDescriptor.AddElementProperty("scheduledTasksGrid", this.ScheduledTasksGrid.ClientID);
      controlDescriptor.AddComponentProperty("searchWidget", this.SearchWidget.ClientID);
      controlDescriptor.AddComponentProperty("deleteScheduledTaskPromptDialog", this.DeleteScheduledTaskPromptDialog.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }
  }
}
