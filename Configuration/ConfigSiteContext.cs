// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigSiteContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// The class contains any context information when persisting or reading configurations.
  /// </summary>
  public class ConfigSiteContext
  {
    /// <summary>
    /// Gets or sets the id of the site for which any operations of configurations are performed.
    /// </summary>
    public Guid SiteId { get; set; }
  }
}
