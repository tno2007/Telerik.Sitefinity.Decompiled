// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowControlPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>Control panel control for the workflow module.</summary>
  public class WorkflowControlPanel : ControlPanel<Page>
  {
    public WorkflowControlPanel() => this.Title = Res.Get<WorkflowResources>().Workflow;

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews() => this.AddView<WorkflowDefinitionList>();

    /// <summary>
    /// When overridden this method returns a list of custom Command Panels.
    /// </summary>
    /// <param name="viewMode">Specifies the current View Mode</param>
    /// <param name="list">A list of custom command panels.</param>
    protected override void CreateCustomCommandPanels(string viewMode, IList<ICommandPanel> list)
    {
      base.CreateCustomCommandPanels(viewMode, list);
      list.Add((ICommandPanel) new WorkflowCommandPanel((IControlPanel) this));
    }

    /// <inheritdoc />
    protected override void GenerateViewCommands(IList<ICommandPanel> list)
    {
      if (!this.AutoGenerateViewCommands || this.Views.Count <= 0)
        return;
      list.Add((ICommandPanel) new CommandPanel()
      {
        Title = Res.Get<WorkflowResources>().ManageWorkflows,
        Name = "Views"
      });
    }
  }
}
