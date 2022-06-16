// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.SiteSyncHeader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.SiteSync
{
  public static class SiteSyncHeader
  {
    /// <summary>
    /// The value of the source server incremental GUIDs "range" byte.
    /// </summary>
    public const string SourceIncrementalGuidRange = "SourceIncrementalGuidRange";
    /// <summary>
    /// The type of sync request - one of the constants in <see cref="T:Telerik.Sitefinity.SiteSync.SiteSyncRequestType" />.
    /// </summary>
    public const string RequestType = "RequestType";
    /// <summary>
    /// The "type" of the exporting snap-in, used to match the importing snap-in, if present.
    /// </summary>
    public const string SnapInType = "SnapInType";
    /// <summary>
    /// The "DisablePermissionsSync" configuration from the source server.
    /// </summary>
    public const string DisablePermissionsSync = "DisablePermissionsSync";
    /// <summary>
    /// The name of the target site during single- to multisite migration; <c>null</c> if none.
    /// </summary>
    public const string MultisiteMigrationTarget = "MultisiteMigrationTarget";
    /// <summary>
    /// The name of the source site during single- to multisite migration; <c>null</c> if none.
    /// </summary>
    public const string MultisiteMigrationSource = "MultisiteMigrationSource";
    /// <summary>The title of the log entry that is being transferred.</summary>
    public const string LogEntryTitle = "LogEntryTitle";
    /// <summary>
    /// The ID of the ValidationKey from the SecurityConfig of the source site.
    /// </summary>
    public const string ValidationKeySource = "ValidationKeySource";
  }
}
