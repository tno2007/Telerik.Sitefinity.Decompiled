// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.PermissionActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Activities;
using System.ComponentModel;
using Telerik.Sitefinity.Workflow.Activities.Designers;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>
  /// Activity which evaluate if the user have the permission to perform certain action.
  /// </summary>
  [Designer(typeof (PermissionActivityDesigner))]
  public class PermissionActivity : CodeActivity
  {
    /// <summary>
    /// Gets or sets the name of the action for which the user must have permission
    /// to perform.
    /// </summary>
    public InArgument<string> ActionName { get; set; }

    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
    }
  }
}
