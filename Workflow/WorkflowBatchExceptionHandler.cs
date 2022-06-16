// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowBatchExceptionHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Workflow
{
  public class WorkflowBatchExceptionHandler
  {
    private IList<WorkflowBatchExceptionHandler.ExceptionInformation> ThrownExceptions;

    public WorkflowBatchExceptionHandler() => this.ThrownExceptions = (IList<WorkflowBatchExceptionHandler.ExceptionInformation>) new List<WorkflowBatchExceptionHandler.ExceptionInformation>();

    public virtual void RegisterException(Exception ex, string currentItemTitle)
    {
      WorkflowBatchExceptionHandler.ExceptionInformation exceptionInformation = new WorkflowBatchExceptionHandler.ExceptionInformation()
      {
        ExceptionMessage = ex.Message,
        ItemTitle = currentItemTitle
      };
      exceptionInformation.ExceptionType = !(ex is FaultException<ExceptionDetail>) ? ex.GetType().FullName : ((FaultException<ExceptionDetail>) ex).Detail.Type;
      this.ThrownExceptions.Add(exceptionInformation);
    }

    public virtual void ThrowAccumulatedErrorForContent(
      int proccessItemsCount,
      string operationName)
    {
      this.ThrowAccumulatedError(proccessItemsCount, operationName, this.ResolveResource("ContentResources", "ContentItemsSingleTypeName"), this.ResolveResource("ContentResources", "ContentPluralItemName"));
    }

    public virtual void ThrowAccumulatedErrorForPages(int proccessItemsCount, string operationName) => this.ThrowAccumulatedError(proccessItemsCount, operationName, this.ResolveResource("PageResources", "Page"), this.ResolveResource("PageResources", "Pages"));

    public virtual void ThrowAccumulatedError(
      int proccessItemsCount,
      string operationName,
      string singularItemTypeName,
      string pluralItemTypeName)
    {
      if (this.ThrownExceptions.Count > 0)
      {
        StringBuilder stringBuilder1 = new StringBuilder();
        int count = this.ThrownExceptions.Count;
        int num = proccessItemsCount - count;
        if (!(operationName == "Publish"))
        {
          if (!(operationName == "Unpublish"))
          {
            if (operationName == "Delete")
              stringBuilder1.Append(string.Format(this.ResolveResource("ContentResources", "BatchDeleteErrorHeading"), (object) num, (object) count));
          }
          else
            stringBuilder1.Append(string.Format(this.ResolveResource("ContentResources", "BatchUnpublishErrorHeading"), (object) num, (object) count));
        }
        else
          stringBuilder1.Append(string.Format(this.ResolveResource("ContentResources", "BatchPublishErrorHeading"), (object) num, (object) count));
        stringBuilder1.Append("\n\n");
        StringBuilder stringBuilder2 = new StringBuilder();
        IEnumerable<WorkflowBatchExceptionHandler.ExceptionInformation> source1 = this.FromOther(this.ThrownExceptions);
        IEnumerable<WorkflowBatchExceptionHandler.ExceptionInformation> source2 = this.FromWorkflow(this.ThrownExceptions);
        if (source1.Count<WorkflowBatchExceptionHandler.ExceptionInformation>() > 0)
        {
          for (int index = 0; index < source1.Count<WorkflowBatchExceptionHandler.ExceptionInformation>(); ++index)
          {
            stringBuilder2.Append(singularItemTypeName).Append(" ").Append(source1.ElementAt<WorkflowBatchExceptionHandler.ExceptionInformation>(index).ItemTitle).Append(": ").Append(source1.ElementAt<WorkflowBatchExceptionHandler.ExceptionInformation>(index).ExceptionMessage);
            if (index != source1.Count<WorkflowBatchExceptionHandler.ExceptionInformation>() - 1)
              stringBuilder2.Append("\n");
          }
          stringBuilder1.Append((object) stringBuilder2);
          if (source2.Count<WorkflowBatchExceptionHandler.ExceptionInformation>() > 0)
            stringBuilder1.Append("\n");
        }
        IEnumerable<WorkflowBatchExceptionHandler.ExceptionInformation> source3 = this.FromLicensing(this.ThrownExceptions);
        if (source2.Count<WorkflowBatchExceptionHandler.ExceptionInformation>() > 0)
        {
          StringBuilder stringBuilder3 = new StringBuilder();
          for (int index = 0; index < source2.Count<WorkflowBatchExceptionHandler.ExceptionInformation>(); ++index)
          {
            stringBuilder3.Append(source2.ElementAt<WorkflowBatchExceptionHandler.ExceptionInformation>(index).ItemTitle);
            if (index != source2.Count<WorkflowBatchExceptionHandler.ExceptionInformation>() - 1)
              stringBuilder3.Append(", ");
          }
          if (!(operationName == "Publish"))
          {
            if (!(operationName == "Unpublish"))
            {
              if (operationName == "Delete")
                stringBuilder1.Append(string.Format(this.ResolveResource("ContentResources", "WorkflowRulesDoNotAllowToDelete"), (object) stringBuilder3));
            }
            else
              stringBuilder1.Append(string.Format(this.ResolveResource("ContentResources", "WorkflowRulesDoNotAllowToUnpublish"), (object) stringBuilder3));
          }
          else
            stringBuilder1.Append(string.Format(this.ResolveResource("ContentResources", "WorkflowRulesDoNotAllowToPublish"), (object) stringBuilder3));
          if (source3.Count<WorkflowBatchExceptionHandler.ExceptionInformation>() > 0)
            stringBuilder1.Append("\n");
        }
        if (source3.Count<WorkflowBatchExceptionHandler.ExceptionInformation>() > 0)
        {
          StringBuilder stringBuilder4 = new StringBuilder();
          if (source3.Count<WorkflowBatchExceptionHandler.ExceptionInformation>() == 1)
            stringBuilder1.Append(singularItemTypeName).Append(" ");
          else
            stringBuilder1.Append(pluralItemTypeName).Append(" ");
          for (int index = 0; index < source3.Count<WorkflowBatchExceptionHandler.ExceptionInformation>(); ++index)
          {
            stringBuilder4.Append(source3.ElementAt<WorkflowBatchExceptionHandler.ExceptionInformation>(index).ItemTitle);
            if (index != source3.Count<WorkflowBatchExceptionHandler.ExceptionInformation>() - 1)
              stringBuilder4.Append(", ");
          }
          stringBuilder1.Append((object) stringBuilder4).Append(": ").Append(source3.First<WorkflowBatchExceptionHandler.ExceptionInformation>().ExceptionMessage);
        }
        throw new Exception(stringBuilder1.ToString());
      }
    }

    public virtual string ResolveResource(string classId, string key)
    {
      try
      {
        return Res.Get(classId, key);
      }
      catch
      {
        return string.Empty;
      }
    }

    private IEnumerable<WorkflowBatchExceptionHandler.ExceptionInformation> FromLicensing(
      IList<WorkflowBatchExceptionHandler.ExceptionInformation> that)
    {
      return that.Where<WorkflowBatchExceptionHandler.ExceptionInformation>((Func<WorkflowBatchExceptionHandler.ExceptionInformation, bool>) (i => i.ExceptionType == "Telerik.Sitefinity.Licensing.TotalContentItemsLimitExceeded" || i.ExceptionType == "Telerik.Sitefinity.Licensing.TotalPublicPagesLimitExceeded"));
    }

    private IEnumerable<WorkflowBatchExceptionHandler.ExceptionInformation> FromWorkflow(
      IList<WorkflowBatchExceptionHandler.ExceptionInformation> that)
    {
      return that.Where<WorkflowBatchExceptionHandler.ExceptionInformation>((Func<WorkflowBatchExceptionHandler.ExceptionInformation, bool>) (i => i.ExceptionType == "Telerik.Sitefinity.Workflow.InvalidWorkflowOperationException" || i.ExceptionType == "Telerik.Sitefinity.Workflow.Exceptions.WorkflowSecurityException"));
    }

    private IEnumerable<WorkflowBatchExceptionHandler.ExceptionInformation> FromOther(
      IList<WorkflowBatchExceptionHandler.ExceptionInformation> that)
    {
      return that.Where<WorkflowBatchExceptionHandler.ExceptionInformation>((Func<WorkflowBatchExceptionHandler.ExceptionInformation, bool>) (i => i.ExceptionType != "Telerik.Sitefinity.Workflow.InvalidWorkflowOperationException" && i.ExceptionType != "Telerik.Sitefinity.Workflow.Exceptions.WorkflowSecurityException" && i.ExceptionType != "Telerik.Sitefinity.Licensing.TotalContentItemsLimitExceeded" && i.ExceptionType != "Telerik.Sitefinity.Licensing.TotalPublicPagesLimitExceeded"));
    }

    private class ExceptionInformation
    {
      public string ExceptionType { get; set; }

      public string ExceptionMessage { get; set; }

      public string ItemTitle { get; set; }
    }
  }
}
