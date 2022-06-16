// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.OperationEventConstants
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// This class contains constants for <see cref="T:Telerik.Sitefinity.Services.IContextOperationEvent" /> events.
  /// </summary>
  internal static class OperationEventConstants
  {
    /// <summary>The key for storing Root operation type.</summary>
    public const string RootOperationKey = "RootOperation";
    /// <summary>The key for storing the current operation.</summary>
    public const string CurrentOperationKey = "CurrentOperation";
    /// <summary>The key for storing Response type information.</summary>
    public const string ResponseTypeKey = "ResponseType";
    /// <summary>The value describing HTTP request origin.</summary>
    public const string HttpRequestOrigin = "HttpRequest";
    /// <summary>The task type key</summary>
    public const string TaskTypeKey = "TaskType";
    /// <summary>he key for storing class name.</summary>
    public const string ClassNameKey = "ClassName";
    /// <summary>he key for storing method name.</summary>
    public const string MethodNameKey = "MethodName";
    /// <summary>he key for storing task identifier.</summary>
    public const string TaskIdKey = "TaskId";
    /// <summary>The value describing Background task origin</summary>
    public const string BackgroundTaskOrigin = "BackgroundTask";
  }
}
