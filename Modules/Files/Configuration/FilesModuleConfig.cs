// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Files.Configuration.FilesModuleConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Modules.Files.Configuration
{
  public class FilesModuleConfig : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Files.Configuration.FilesModuleConfig" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public FilesModuleConfig(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the size of the max file.</summary>
    /// <value>The size of the max file.</value>
    [ConfigurationProperty("maxFileSize", DefaultValue = 204800)]
    public virtual int MaxFileSize
    {
      get => (int) this["maxFileSize"];
      set => this["maxFileSize"] = (object) value;
    }
  }
}
