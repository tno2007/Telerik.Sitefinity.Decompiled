// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.PageScheduleActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Collections.Generic;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>
  /// 
  /// </summary>
  public class PageScheduleActivity : CodeActivity
  {
    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
      WorkflowDataContext dataContext = context.DataContext;
      PageFacade pageFacade = (PageFacade) dataContext.GetProperties()["fluent"].GetValue((object) dataContext);
      Dictionary<string, string> contextBag = dataContext.GetProperties()["contextBag"].GetValue((object) dataContext) as Dictionary<string, string>;
      if (!contextBag.ContainsKey("PublicationDate"))
        return;
      DateTime publicationDate = DateTime.Parse(contextBag["PublicationDate"]);
      pageFacade.AsStandardPage().CheckOut().Publish().Do((Action<PageNode>) (p =>
      {
        p.GetPageData().PublicationDate = publicationDate;
        if (!contextBag.ContainsKey("ExpirationDate"))
          return;
        p.GetPageData().ExpirationDate = new DateTime?(DateTime.Parse(contextBag["PublicationDate"]));
      })).SaveChanges();
    }
  }
}
