// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.MessageTemplates.ItemAwaitingForActionMessageTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Workflow.Activities.MessageTemplates
{
  internal class ItemAwaitingForActionMessageTemplate : WorkflowActionMessageTemplate
  {
    protected override string GetSubject() => string.Format(Res.Get("ApprovalWorkflowResources", "ItemAwaitingForAction", SystemManager.CurrentContext.AppSettings.DefaultBackendLanguage), (object) ("{|" + WorkflowActionMessageTemplate.PlaceholderFields.ApprovalStatus.FieldName + "|}"));
  }
}
