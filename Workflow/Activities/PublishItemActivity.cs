// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.PublishItemActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle.Fluent;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>Used to publish the item.</summary>
  public class PublishItemActivity : CodeActivity
  {
    /// <inheritdoc />
    protected override void Execute(CodeActivityContext context)
    {
      WorkflowDataContext dataContext = context.DataContext;
      object obj1 = dataContext.GetProperties()["workflowItem"].GetValue((object) dataContext);
      object obj2 = dataContext.GetProperties()["operationName"].GetValue((object) dataContext);
      Type type = obj1.GetType();
      if (typeof (Content).IsAssignableFrom(type) || type.FullName.StartsWith("Telerik.Sitefinity.DynamicTypes.Model"))
      {
        IMasterLifecycleFacade masterLifecycleFacade = dataContext.GetProperties()["masterFluent"].GetValue((object) dataContext) as IMasterLifecycleFacade;
        ITempLifecycleFacade tempLifecycleFacade = dataContext.GetProperties()["fluent"].GetValue((object) dataContext) as ITempLifecycleFacade;
        if (masterLifecycleFacade.IsCheckedOut())
        {
          if (obj2 == (object) "ScheduledPublish")
            tempLifecycleFacade.CopyToMaster(true).Publish().SaveChanges();
          else
            tempLifecycleFacade.CheckIn(true).Publish().SaveChanges();
        }
        else
          masterLifecycleFacade.Publish().SaveChanges();
      }
      else
      {
        if (!typeof (PageNode).IsAssignableFrom(type))
          return;
        (dataContext.GetProperties()["fluent"].GetValue((object) dataContext) as PageFacade).AsStandardPage().CheckOut().Publish().SaveChanges();
      }
    }
  }
}
