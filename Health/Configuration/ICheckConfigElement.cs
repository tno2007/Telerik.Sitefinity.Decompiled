// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.Configuration.ICheckConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;

namespace Telerik.Sitefinity.Health.Configuration
{
  /// <summary>
  /// Defines functionality for health checks config element.
  /// </summary>
  public interface ICheckConfigElement
  {
    /// <summary>Gets or sets the friendly name for the Health check.</summary>
    /// <value>The Health check name.</value>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the check is critical.
    /// </summary>
    /// <value>The Health criticality.</value>
    bool Critical { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the check is enabled or disabled.
    /// </summary>
    /// <value>The Health check is enabled or disabled.</value>
    bool Enabled { get; set; }

    /// <summary>Gets or sets Health check groups.</summary>
    /// <value>The Health check groups.</value>
    string Groups { get; set; }

    /// <summary>Gets or sets the parameters for the Health Check.</summary>
    /// <value>The Health check parameters.</value>
    NameValueCollection Parameters { get; set; }

    /// <summary>
    /// Gets or sets the timeout for the Health check in seconds.
    /// </summary>
    /// <value>Health timeout.</value>
    int TimeoutSeconds { get; set; }

    /// <summary>Gets or sets Health check type.</summary>
    /// <value>The Health check type.</value>
    string Type { get; set; }
  }
}
