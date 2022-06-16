// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.Data.WorkflowVisualElementsContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Workflow.Services.Data
{
  /// <summary>Context data for the collection of visual elements</summary>
  [DataContract]
  public class WorkflowVisualElementsContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowVisualElementsContext" /> class.
    /// </summary>
    /// <param name="commandToExecute">The command to execute on the client</param>
    /// <param name="lastApprovalTrackingRecord">The last approval tracking record.</param>
    public WorkflowVisualElementsContext(
      string commandToExecute,
      string operationName,
      string positiveMessage,
      string negativeMessage,
      string status,
      WcfApprovalTrackingRecord lastApprovalTrackingRecord)
    {
      this.CommandToExecute = commandToExecute;
      this.OperationName = operationName;
      this.PositiveMessage = positiveMessage;
      this.NegativeMessage = negativeMessage;
      this.Status = status;
      this.LastApprovalTrackingRecord = lastApprovalTrackingRecord;
    }

    /// <summary>Gets or sets the command to execute on the client</summary>
    /// <value>The command to execute on the client</value>
    [DataMember]
    public string CommandToExecute { get; set; }

    /// <summary>
    /// Gets or sets the operation name to be executed together with the command
    /// </summary>
    [DataMember]
    public string OperationName { get; set; }

    /// <summary>
    /// Gets or sets the positive message to be displayed on the client
    /// </summary>
    /// <value>The positive message.</value>
    [DataMember]
    public string PositiveMessage { get; set; }

    /// <summary>
    /// Gets or sets the negative message to be displayed on the client
    /// </summary>
    /// <value>The negative message.</value>
    [DataMember]
    public string NegativeMessage { get; set; }

    /// <summary>
    /// Gets or sets the current status of the item in the approval workflow.
    /// </summary>
    /// <value>The status.</value>
    [DataMember]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the last approval tracking record for the current content item.
    /// </summary>
    /// <value>The last approval tracking record for the current content item.</value>
    [DataMember]
    public WcfApprovalTrackingRecord LastApprovalTrackingRecord { get; set; }
  }
}
