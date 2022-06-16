// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ISiteSyncExportContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.SiteSync
{
  /// <summary>
  /// Interface for the context used when exporting items with SiteSync
  /// </summary>
  public interface ISiteSyncExportContext : ISiteSyncContext
  {
    /// <summary>Gets the setver id</summary>
    string ServerId { get; }

    /// <summary>
    /// Gets or sets the target microsite. Used only in single site to multisite mode.
    /// </summary>
    SiteSyncSiteInfo TargetMicrosite { get; set; }

    /// <summary>
    /// Gets or sets the source site name. Used only in single site to multisite mode.
    /// </summary>
    string SourceSiteName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sync operation is executed for a single item.
    /// </summary>
    bool IsForSpecificItem { get; set; }

    /// <summary>Gets or sets the sites related to the sync operation</summary>
    IList<Guid> Sites { get; set; }

    /// <summary>Gets the types that will be synchronized</summary>
    IEnumerable<string> Types { get; }

    /// <summary>
    /// Gets the type filters for the items that will be synchronized.
    /// </summary>
    IDictionary<string, string> TypeFilters { get; }

    /// <summary>
    /// Gets or sets the additional items that will be included in the sync operation, for example dependencies.
    /// </summary>
    IDictionary<string, ICollection<Guid>> AdditionalItems { get; set; }

    /// <summary>Reports the current operation progress</summary>
    /// <param name="progress">The progress</param>
    /// <param name="message">The message</param>
    void ReportProgress(double progress, string message);

    /// <summary>
    /// Gets a mapping for a value, transfered from single to multisite
    /// </summary>
    /// <param name="typeName">The type</param>
    /// <param name="value">The value</param>
    /// <param name="propertyName">The property name</param>
    /// <returns>The mapping</returns>
    object GetMapping(string typeName, object value, string propertyName = "ItemId");

    /// <summary>Gets the batch size for the current operation</summary>
    /// <returns>The batch size</returns>
    int GetSyncBatchSize();
  }
}
