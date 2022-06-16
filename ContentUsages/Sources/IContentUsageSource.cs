// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentUsages.Sources.IContentUsageSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.ContentUsages.Filters;
using Telerik.Sitefinity.ContentUsages.Model;

namespace Telerik.Sitefinity.ContentUsages.Sources
{
  /// <summary>Defines functions for resolving content usages</summary>
  internal interface IContentUsageSource
  {
    /// <summary>Gets all content usages for specified source.</summary>
    /// <param name="filter">Filtering options.</param>
    /// <returns>Return collection of content usages.</returns>
    IEnumerable<IContentItemUsage> GetContentUsages(
      ContentUsageFilter filter);
  }
}
