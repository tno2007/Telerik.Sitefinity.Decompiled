// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Data.ConfigImportContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Packaging;

namespace Telerik.Sitefinity.Configuration.Data
{
  /// <summary>Configurations import context</summary>
  internal class ConfigImportContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Data.ConfigImportContext" /> class.
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <param name="overrideElementsWithSameOrigin">if set to <c>true</c> [override elements with same origin].</param>
    /// <param name="pathsOfElementsToOverride">The paths of elements to override.</param>
    public ConfigImportContext(
      string origin = null,
      bool overrideElementsWithSameOrigin = false,
      IEnumerable<string> pathsOfElementsToOverride = null)
    {
      this.Origin = OriginWrapperObject.ToArrayJson(origin);
      this.OverrideElementsWithSameOrigin = overrideElementsWithSameOrigin;
      this.PathsOfElementsToOverride = pathsOfElementsToOverride;
    }

    /// <summary>Gets the origin.</summary>
    /// <value>The origin.</value>
    public string Origin { get; private set; }

    /// <summary>
    /// Gets a value indicating whether [override elements with same origin].
    /// </summary>
    /// <value>
    /// <c>true</c> if [override elements with same origin]; otherwise, <c>false</c>.
    /// </value>
    public bool OverrideElementsWithSameOrigin { get; private set; }

    /// <summary>
    /// Gets the paths of the elements to override. If null, all elements with same origin will be overridden.
    /// </summary>
    /// <value>The paths of the elements to override.</value>
    public IEnumerable<string> PathsOfElementsToOverride { get; private set; }
  }
}
