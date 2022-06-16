// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowItemScopeResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Contains method to resolve whether particular content item belongs to a granular scope
  /// </summary>
  public interface IWorkflowItemScopeResolver
  {
    /// <summary>
    /// Determines whether given workflow includes specific item.
    /// </summary>
    /// <param name="context">Item's context</param>
    /// <param name="scope">Workflow scope</param>
    /// <returns>Whether item belongs to the scope.</returns>
    bool ResolveItem(IWorkflowResolutionContext context, IWorkflowExecutionTypeScope scope);
  }
}
