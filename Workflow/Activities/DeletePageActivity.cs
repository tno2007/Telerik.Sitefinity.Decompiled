// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.DeletePageActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Activities;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RelatedData;

namespace Telerik.Sitefinity.Workflow.Activities
{
  public class DeletePageActivity : CodeActivity
  {
    private const string LanguageKey = "Language";
    private const string SegmentIdKey = "SegmentId";

    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
      WorkflowDataContext dataContext = context.DataContext;
      IWorkflowItem workflowItem = (IWorkflowItem) dataContext.GetProperties()["workflowItem"].GetValue((object) dataContext);
      Dictionary<string, string> dictionary = dataContext.GetProperties()["contextBag"].GetValue((object) dataContext) as Dictionary<string, string>;
      if (workflowItem == null)
        return;
      RelatedDataHelper.CheckRelatingData(dictionary, workflowItem.Id, workflowItem.GetType().FullName);
      PageFacade pageFacade = dataContext.GetProperties()["fluent"].GetValue((object) dataContext) as PageFacade;
      PageNode dataItem = pageFacade.Get();
      CultureInfo languageOrDefault = this.GetLanguageOrDefault(dictionary);
      string segmentIdOrDefault = this.GetSegmentIdOrDefault(dictionary);
      bool flag = false;
      string str = (string) null;
      dictionary.TryGetValue("SkipRecycleBin", out str);
      if (string.IsNullOrEmpty(str) || str.ToLowerInvariant() != "true")
      {
        string name = languageOrDefault?.Name;
        flag = pageFacade.PageManager.RecycleBin.ShouldMoveToRecycleBin((object) dataItem, name);
      }
      if (segmentIdOrDefault == null & flag)
      {
        pageFacade.PageManager.RecycleBin.MoveToRecycleBin((IRecyclableDataItem) dataItem);
        pageFacade.SaveChanges();
      }
      else
      {
        pageFacade.IfStandardPage().CheckOut().Undo();
        pageFacade.Delete(languageOrDefault, (object) segmentIdOrDefault).SaveChanges();
      }
    }

    private CultureInfo GetLanguageOrDefault(Dictionary<string, string> contextBag) => contextBag.ContainsKey("Language") ? CultureInfo.GetCultureInfo(contextBag["Language"]) : (CultureInfo) null;

    private string GetSegmentIdOrDefault(Dictionary<string, string> contextBag) => contextBag.ContainsKey("SegmentId") ? contextBag["SegmentId"] : (string) null;
  }
}
