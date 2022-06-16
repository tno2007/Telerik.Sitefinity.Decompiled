// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowVisualElementFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Activities;
using Telerik.Sitefinity.Workflow.Services.Data;

namespace Telerik.Sitefinity.Workflow.UI
{
  public static class WorkflowVisualElementFactory
  {
    public static WorkflowVisualElement ResolveVisualElement(
      string operationName,
      DecisionActivity decision,
      bool ignoreMultilingualWarnings = false)
    {
      string title;
      if (string.IsNullOrEmpty(decision.ResourceClass))
      {
        title = decision.Title;
      }
      else
      {
        try
        {
          title = Res.Get(decision.ResourceClass, decision.Title);
        }
        catch
        {
          title = decision.Title;
        }
      }
      string str = string.Empty;
      string key = (string) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (!ignoreMultilingualWarnings)
          key = decision.MultilingualWarning;
      }
      else
        key = decision.MonolingualWarning;
      if (!string.IsNullOrEmpty(key))
      {
        try
        {
          str = Res.Get(decision.ResourceClass, key);
        }
        catch
        {
          str = key;
        }
      }
      return new WorkflowVisualElement()
      {
        Title = title,
        VisualType = WorkflowVisualType.Link,
        CssClass = decision.CssClass,
        DecisionType = decision.Placeholder.ToString(),
        Ordinal = decision.Ordinal,
        ArgumentDialogName = string.IsNullOrEmpty(decision.ArgumentDialogName) ? (string) null : decision.ArgumentDialogName,
        OperationName = operationName,
        ContentCommandName = decision.ContentCommandName != null ? decision.ContentCommandName : "save",
        PersistOnDecision = decision.PersistOnDecision,
        ClosesForm = decision.ClosesForm,
        RunAsUICommand = decision.RunAsUICommand,
        WarningMessage = str
      };
    }
  }
}
