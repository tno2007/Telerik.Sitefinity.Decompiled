// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Cleaner.VersioningCleanerUtils
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Versioning.Cleaner.Configuration;
using Telerik.Sitefinity.Versioning.Configuration;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Versioning.Cleaner
{
  /// <summary>Provides methods for revision history cleanup.</summary>
  internal class VersioningCleanerUtils
  {
    private CleanerConfig CleanerConfig => Config.Get<VersionConfig>().Cleaner;

    /// <summary>
    /// Returns only changes for the given item that are considered stale and are eligible for deletion.
    /// </summary>
    /// <param name="item">The revision history item for which to filter changes.</param>
    /// <returns>The stale changes.</returns>
    public IEnumerable<Change> FilterChangesToKeep(Item item)
    {
      if (item == null)
        throw new ArgumentException("Item to filter version changes for can not be null.");
      IEnumerable<Change> first = Enumerable.Empty<Change>();
      if (item.Changes == null)
        return first;
      foreach (int num in item.Changes.Select<Change, int>((Func<Change, int>) (x => x.Culture)).Distinct<int>())
      {
        int culture = num;
        IEnumerable<Change> changes = item.Changes.Where<Change>((Func<Change, bool>) (c => c.Culture == culture));
        first = first.Union<Change>(this.FilterChangesToKeep(changes));
      }
      return first;
    }

    private IEnumerable<Change> FilterChangesToKeep(IEnumerable<Change> changes)
    {
      int publishedVersionsToKeep = this.GetPublishedVersionsToKeep(changes);
      return changes.OrderByDescending<Change, bool>((Func<Change, bool>) (c => c.IsPublishedVersion)).ThenByDescending<Change, int>((Func<Change, int>) (c => c.Version)).Skip<Change>(publishedVersionsToKeep).Where<Change>((Func<Change, bool>) (c => c.LastModified < this.GetDateToKeepAfter()));
    }

    private int GetPublishedVersionsToKeep(IEnumerable<Change> changes)
    {
      int num = changes.Count<Change>((Func<Change, bool>) (x => x.IsPublishedVersion));
      int majorVersionsToKeep = this.CleanerConfig.MajorVersionsToKeep;
      return num < majorVersionsToKeep ? num : majorVersionsToKeep;
    }

    private DateTime GetDateToKeepAfter() => DateTime.UtcNow - this.CleanerConfig.GetPeriodToKeep();
  }
}
