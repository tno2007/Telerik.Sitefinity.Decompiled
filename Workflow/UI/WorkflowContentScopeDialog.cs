// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowContentScopeDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.CommonDialogs;
using Telerik.Sitefinity.Workflow.Services.Data;

namespace Telerik.Sitefinity.Workflow.UI
{
  internal class WorkflowContentScopeDialog : FlatSelectorDialog
  {
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.ItemSelector.DataMembers.Add(new DataMemberInfo()
      {
        Name = "Title",
        IsSearchField = true,
        HeaderText = "Content Type",
        ColumnTemplate = "<span>{{Title}}</span>"
      });
      this.ItemSelector.ServiceUrl = "~/Sitefinity/Services/Workflow/WorkflowDefinitionService.svc/content-scope";
      this.ItemSelector.BindOnLoad = false;
      this.ItemSelector.DataKeyNames = "ContentType";
      this.ItemSelector.ItemType = typeof (WorkflowScopeViewModel).FullName;
      this.TitleLabel.Text = Res.Get<WorkflowResources>().SelectContentType;
    }
  }
}
