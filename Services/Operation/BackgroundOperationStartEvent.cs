// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.BackgroundOperationStartEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// A context operation bound to starting of a specific background task.
  /// </summary>
  internal class BackgroundOperationStartEvent : 
    IContextOperationStartEvent,
    IContextOperationEvent,
    IEvent
  {
    private readonly string operationKey;
    internal const string DefaultCategoryName = "BackgroundTask";
    internal const string NotificationServiceType = "NotificationService";
    internal const string ScheduledType = "Scheduled";
    internal const string BackgroundType = "Background";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.BackgroundOperationStartEvent" /> class.
    /// </summary>
    /// <param name="operation">The key of the operation.</param>
    /// <param name="taskType">Type of the task.</param>
    public BackgroundOperationStartEvent(string operation, string taskType)
    {
      this.operationKey = operation;
      this.TaskType = taskType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.BackgroundOperationStartEvent" /> class.
    /// </summary>
    /// <param name="operation">The operation.</param>
    /// <param name="className">Name of the class.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="taskId">The task identifier.</param>
    /// <param name="taskType">Type of the task.</param>
    public BackgroundOperationStartEvent(
      string operation,
      string className,
      string methodName,
      string taskId,
      string taskType)
      : this(operation, taskType)
    {
      this.ClassName = className;
      this.MethodName = methodName;
      this.TaskId = taskId;
    }

    /// <summary>
    /// Gets or sets a value specifying the origin of the event.
    /// </summary>
    public string Origin { get; set; }

    /// <summary>Gets the operation key.</summary>
    /// <value>The operation key.</value>
    public string OperationKey => this.operationKey;

    /// <summary>
    /// Gets the category of the started operation
    /// Could be an unique string specific for the operation of this type, e.g. HttpRequest, Background, etc..
    /// </summary>
    /// <value>The category.</value>
    public string Category => "BackgroundTask";

    /// <summary>Gets the type of the task.</summary>
    /// <value>The type of the task.</value>
    public string TaskType { get; private set; }

    /// <summary>Gets or sets the name of the class.</summary>
    /// <value>The name of the class.</value>
    public string ClassName { get; set; }

    /// <summary>Gets or sets the name of the method.</summary>
    /// <value>The name of the method.</value>
    public string MethodName { get; set; }

    /// <summary>Gets or sets the task identifier.</summary>
    /// <value>The task identifier.</value>
    public string TaskId { get; set; }
  }
}
