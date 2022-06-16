// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.IApprovalWorkflowEventExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// Contains extension methods on <see cref="T:Telerik.Sitefinity.Data.Events.IApprovalWorkflowEvent" /> objects.
  /// </summary>
  internal static class IApprovalWorkflowEventExtensions
  {
    /// <summary>
    /// Sets the <see cref="T:Telerik.Sitefinity.Data.Events.IApprovalWorkflowEvent" /> properties from the tracking context
    /// of the specified <paramref name="dataItem" />.
    /// </summary>
    /// <param name="evt">The <see cref="T:Telerik.Sitefinity.Data.Events.IApprovalWorkflowEvent" /> which properties will be set.</param>
    /// <param name="dataItem">
    /// The data item which context will be used
    /// to fill in the <see cref="T:Telerik.Sitefinity.Data.Events.IApprovalWorkflowEvent" /> properties.
    /// </param>
    public static void SetIApprovalWorkflowEventPropertiesFromTrackingContext(
      this IApprovalWorkflowEvent evt,
      IDataItem dataItem)
    {
      if (!(dataItem is IHasTrackingContext context) || !context.HasOperation(OperationStatus.Unpublished))
        return;
      evt.ApprovalWorkflowState = OperationStatus.Unpublished.ToString();
    }
  }
}
