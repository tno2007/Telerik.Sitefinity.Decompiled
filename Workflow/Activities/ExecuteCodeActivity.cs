// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.ExecuteCodeActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Activities;
using System.ComponentModel;
using Telerik.Sitefinity.Workflow.Activities.Designers;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>
  /// This activity evaluate the expression in the designer and hence executes the arbitrary code.
  /// </summary>
  [Designer(typeof (ExecuteCodeActivityDesigner))]
  public class ExecuteCodeActivity : CodeActivity
  {
    /// <summary>
    /// Result from the execution of the code specified in the "Execute code"
    /// <example>
    /// App.WorkWith().Pages().Get().
    /// </example>
    /// </summary>
    public InArgument<bool> CodeExecutionResult { get; set; }

    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
    }
  }
}
