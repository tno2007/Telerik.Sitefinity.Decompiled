﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Configuration.PropertyPath
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.SiteSettings.Configuration
{
  /// <summary>
  /// The class is used to persist property paths in configurations that mark those properties as able to persist different values for different sites.
  /// </summary>
  public class PropertyPath : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSettings.Configuration.PropertyPath" /> class. It is a Configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public PropertyPath(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the path to the configuration property that will be able to persist site context values.
    /// </summary>
    [ConfigurationProperty("path", IsKey = true)]
    public string Path
    {
      get => (string) this["path"];
      set => this["path"] = (object) value;
    }
  }
}
