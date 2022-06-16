// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.LoadBalancingConstants
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>Constants used at load balancing.</summary>
  public static class LoadBalancingConstants
  {
    public const string InvalidateAllChacheKey = "InvalidateAllCacheKey";
    public const string InvalidateCacheKey = "invalidateCacheKey";
    public const string ResetApplicationKey = "ResetApplicationKey";
    public const string RescheduleNextRunKey = "RescheduleNextRunKey";
    public const string ResetModel = "ResetModel";
    public const string InvalidateLicenseKey = "InvalidateLicenseKey";
    public const string SchedulingKey = "SchedulingKey";
    public const string InvalidateSitemapCache = "InvalidateSitemapCacheKey";
    internal const string InvalidateDynamicModuleCache = "InvalidateDynamicModuleCacheKey";
    public const string FileSystemChangesKey = "FileSystemChanges";
    public const string InvalidateOpenAccessL2CacheItems = "InvalidateOpenAccessL2CacheItems";
    internal const string SentFromDifferentRegion = "SentFromDifferentRegion";
    public static readonly string HandlersCacheKey = "class:" + typeof (SystemMessageDispatcher).Name + ":handlers";
    public static readonly string SendersCacheKey = "class:" + typeof (SystemMessageDispatcher).Name + ":senders";
    /// <summary>
    /// The name of the service method that handles one system message.
    /// </summary>
    public const string HandleMessageMethodName = "HandleMessage";
    /// <summary>
    /// The Uri template of the method that handles one system message.
    /// </summary>
    public const string HandleMessageMethodUriTemplate = "/HandleMessage";
    /// <summary>
    /// The name of the service method that handles system messages.
    /// </summary>
    public const string HandleMessagesMethodName = "HandleMessages";
    /// <summary>
    /// The Uri template of the method that handles system messages.
    /// </summary>
    public const string HandleMessagesMethodUriTemplate = "/HandleMessages";
    /// <summary>The http method name used to handle system messages.</summary>
    public const string HandleMessagesHttpMehod = "PUT";
  }
}
