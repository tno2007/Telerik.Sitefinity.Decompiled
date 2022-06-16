// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.InitializeWorkflowActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow.Activities
{
  public class InitializeWorkflowActivity : CodeActivity
  {
    protected override void Execute(CodeActivityContext context)
    {
      WorkflowDataContext dataContext = context.DataContext;
      Dictionary<string, string> dictionary = dataContext.GetProperties()["contextBag"].GetValue((object) dataContext) as Dictionary<string, string>;
      string name;
      if (dictionary.TryGetValue("Language", out name))
      {
        CultureInfo language = (CultureInfo) null;
        if (!string.IsNullOrEmpty(name))
        {
          language = CultureInfo.GetCultureInfo(name);
          if (((IEnumerable<CultureInfo>) SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages).All<CultureInfo>((Func<CultureInfo, bool>) (x => !x.Equals((object) language))))
            language = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
          SystemManager.CurrentContext.Culture = language;
        }
      }
      else
        LocalizationBehavior.ApplyLocalizationBehaviorFromCurrentRequest();
      if (dictionary["ContentType"] == typeof (PageNode).FullName)
      {
        string str = (string) null;
        if (dictionary.TryGetValue("OptimizedCopy", out str))
          SystemManager.CurrentHttpContext.Items[(object) "OptimizedCopy"] = (object) bool.Parse(str);
      }
      Guid id = (Guid) dataContext.GetProperties()["workflowDefinitionId"].GetValue((object) dataContext);
      if (dataContext.GetProperties()["workflowExecutionDefinition"] != null)
      {
        IWorkflowExecutionDefinition executionDefinition = !(id == Guid.Empty) ? WorkflowManager.GetWorkflowExecutionDefinition(id) : (IWorkflowExecutionDefinition) WorkflowDefinitionProxy.DefaultWorkflow;
        dataContext.GetProperties()["workflowExecutionDefinition"].SetValue((object) dataContext, (object) executionDefinition);
      }
      if (dataContext.GetProperties()["workflowDefinition"] == null)
        return;
      WorkflowDefinition workflowDefinition = !(id == Guid.Empty) ? WorkflowDefinitionExtensions.FromIWorkflowExecutionDefinition(WorkflowManager.GetWorkflowExecutionDefinition(id)) : new WorkflowDefinition();
      dataContext.GetProperties()["workflowDefinition"].SetValue((object) dataContext, (object) workflowDefinition);
    }
  }
}
