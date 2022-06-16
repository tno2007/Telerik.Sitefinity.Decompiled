// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Logging.SitefinityLogCategory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;

namespace Telerik.Sitefinity.Logging
{
  /// <summary>
  /// Represents an Enterprise Library log category built into/by Sitefinity,
  /// including a reference to the fluent configuration API, allowing to
  /// configure custom trace listeners and others.
  /// </summary>
  public class SitefinityLogCategory
  {
    /// <summary>Gets the name of the category.</summary>
    public string Name { get; internal set; }

    /// <summary>
    /// Gets the log file name used by this category by default, which in some cases differ
    /// from the <see cref="P:Telerik.Sitefinity.Logging.SitefinityLogCategory.Name" />.
    /// </summary>
    public string FileName { get; internal set; }

    /// <summary>
    /// Gets the default format builder for this category.
    /// It is safe to ignore it and specify your own format.
    /// </summary>
    public IFormatterBuilder FormatBuilder { get; internal set; }

    /// <summary>
    /// Gets the fluent configuration interface for this log category, allowing further chaining of fluent configuration calls.
    /// </summary>
    public ILoggingConfigurationCustomCategoryStart Configuration { get; internal set; }
  }
}
