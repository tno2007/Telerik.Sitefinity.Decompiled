// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Workflow.Services.Data;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// A static class containing helper methods for workflow.
  /// </summary>
  public static class WorkflowHelper
  {
    internal static string WorkflowSendForApprovalDialogName = "WorkflowSendForApprovalDialog";
    internal static string VisualElementDetailedTitle = "DetailedTitle";
    internal static string VisualElementHint = "Hint";

    /// <summary>Gets a dictionary containing all content types.</summary>
    /// <param name="addAllContent">if set to <c>true</c> [add all content].</param>
    /// <returns></returns>
    public static IDictionary<string, string> GetContentTypes(bool addAllContent)
    {
      Dictionary<string, string> contentTypes = new Dictionary<string, string>();
      contentTypes.Add("Pages", Res.Get<PageResources>().ModuleTitle);
      contentTypes.Add("Blogs", "Blogs");
      contentTypes.Add("Events", "Events");
      contentTypes.Add("News", "News");
      if (addAllContent)
        contentTypes.Add("AllContent", Res.Get<Labels>().AllContent);
      contentTypes.Add("Videos", Res.Get<VideosResources>().ModuleTitle);
      contentTypes.Add("Images", Res.Get<ImagesResources>().ModuleTitle);
      contentTypes.Add("Documents", Res.Get<DocumentsResources>().ModuleTitle);
      contentTypes.Add("Forms", Res.Get<FormsResources>().FormsTitle);
      contentTypes.Add("GenericContent", Res.Get<ContentResources>().ModuleTitle);
      return (IDictionary<string, string>) contentTypes;
    }

    internal static string InternalOperationDialog(string operation)
    {
      string str = (string) null;
      if (operation == "SendForApproval" || operation == "SendForPublishing" || operation == "SendForReview")
        str = WorkflowHelper.WorkflowSendForApprovalDialogName;
      return str;
    }

    internal static bool IsInternalOperationDialog(string operation, string dialogName)
    {
      bool flag = false;
      if (operation == "SendForApproval" || operation == "SendForPublishing" || operation == "SendForReview")
        flag = dialogName == WorkflowHelper.WorkflowSendForApprovalDialogName;
      return flag;
    }

    internal static void UpdateWorkflowVisualElement(
      WorkflowVisualElement workflowVisualElement,
      IWorkflowExecutionDefinition wed,
      string status,
      CultureInfo cultureInfo)
    {
      workflowVisualElement.Parameters = new Hashtable();
      string operationName = workflowVisualElement.OperationName;
      if (!(operationName == "Reject"))
      {
        if (!(operationName == "SendForApproval"))
        {
          if (!(operationName == "SendForPublishing"))
          {
            if (!(operationName == "SendForReview") || !WorkflowHelper.IsInternalOperationDialog(workflowVisualElement.OperationName, workflowVisualElement.ArgumentDialogName))
              return;
            workflowVisualElement.Parameters[(object) WorkflowHelper.VisualElementDetailedTitle] = (object) Res.Get<WorkflowResources>().SendForReviewAction.ToLower().UpperFirstLetter();
          }
          else
          {
            if (!WorkflowHelper.IsInternalOperationDialog(workflowVisualElement.OperationName, workflowVisualElement.ArgumentDialogName))
              return;
            workflowVisualElement.Parameters[(object) WorkflowHelper.VisualElementDetailedTitle] = (object) Res.Get<WorkflowResources>().SendForPublishingAction.ToLower().UpperFirstLetter();
          }
        }
        else
        {
          if (!WorkflowHelper.IsInternalOperationDialog(workflowVisualElement.OperationName, workflowVisualElement.ArgumentDialogName))
            return;
          workflowVisualElement.Parameters[(object) WorkflowHelper.VisualElementDetailedTitle] = (object) Res.Get<WorkflowResources>().SendForApprovalAction.ToLower().UpperFirstLetter();
        }
      }
      else
      {
        workflowVisualElement.Parameters[(object) WorkflowHelper.VisualElementDetailedTitle] = (object) Res.Get<WorkflowResources>().RejectPublishing;
        string str = Res.Get<WorkflowResources>().ReturnedToAuthorLabel;
        IOrderedEnumerable<IWorkflowExecutionLevel> source = wed.Levels.OrderBy<IWorkflowExecutionLevel, float>((Func<IWorkflowExecutionLevel, float>) (p => p.Ordinal));
        if (!(status == "AwaitingApproval") && !(status == "RejectedForPublishing"))
        {
          if (status == "AwaitingPublishing")
            str = string.Format((IFormatProvider) cultureInfo, Res.Get<WorkflowResources>().ReturnedForReviewFormatLabel, (object) "approval");
        }
        else if (wed.Levels.Count<IWorkflowExecutionLevel>() == 3)
          str = string.Format((IFormatProvider) cultureInfo, Res.Get<WorkflowResources>().ReturnedForReviewFormatLabel, (object) source.ElementAt<IWorkflowExecutionLevel>(0).ActionName.ToLower());
        workflowVisualElement.Parameters[(object) WorkflowHelper.VisualElementHint] = (object) str;
      }
    }
  }
}
