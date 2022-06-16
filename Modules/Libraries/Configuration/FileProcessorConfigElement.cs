// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.FileProcessorConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// Provides configuration information about a FileProcessors in the Libraries module
  /// </summary>
  [Obsolete("Deprecated, please use Telerik.Sitefinity.Processors.Configuration.ProcessorConfigElement")]
  public class FileProcessorConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.FileProcessorConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public FileProcessorConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.FileProcessorConfigElement" /> class.
    /// </summary>
    public FileProcessorConfigElement()
      : base(false)
    {
    }

    /// <inheritdoc />
    [ConfigurationProperty("enabled", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "EnableFileProcessor")]
    public virtual bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>Gets or sets the name for the FileProcessor.</summary>
    /// <value>The FileProcessor name.</value>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "FileProcessorName")]
    public virtual string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the description for the FileProcessor.</summary>
    /// <value>The FileProcessor description.</value>
    [ConfigurationProperty("description")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "FileProcessorDescription")]
    public virtual string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("type", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "FileProcessorType")]
    public virtual string Type
    {
      get => (string) this["type"];
      set => this["type"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("parameters")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "FileProcessorsParameters")]
    public virtual NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"] ?? new NameValueCollection();
      set => this["parameters"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropNames
    {
      public const string Enabled = "enabled";
      public const string Name = "name";
      public const string Description = "description";
      public const string Type = "type";
      public const string Parameters = "parameters";
    }
  }
}
