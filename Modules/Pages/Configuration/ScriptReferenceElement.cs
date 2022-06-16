// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ScriptReferenceElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Represents configuration element for script references.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptReferenceElementDescription", Title = "ScriptReferenceElementTitle")]
  public class ScriptReferenceElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.ScriptReferenceElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ScriptReferenceElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the key for the configuration entry.</summary>
    /// <value>The name.</value>
    [ConfigurationProperty("key", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Key
    {
      get => (string) this["key"];
      set => this["key"] = (object) value;
    }

    /// <summary>Gets or sets the name of the resource.</summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemName", Title = "ItemNameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the resource to use when in debug mode.
    /// </summary>
    /// <value>The debug name.</value>
    [ConfigurationProperty("debugName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DebugNameDescription", Title = "DebugNameCaption")]
    public string DebugName
    {
      get => (string) this["debugName"];
      set => this["debugName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path of the referenced client script file, relative to the Web page or absolute for CDN (Content Delivery Network) location.
    /// </summary>
    /// <value>The path.</value>
    [ConfigurationProperty("path", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptReferencePathDescription", Title = "ScriptReferencePathTitle")]
    public string Path
    {
      get => (string) this["path"];
      set => this["path"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path of the debug version of the referenced client script file, relative to the Web page or absolute for CDN (Content Delivery Network) location.
    /// </summary>
    /// <value>The path.</value>
    [ConfigurationProperty("debugPath", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptReferenceDebugPathDescription", Title = "ScriptReferenceDebugPathTitle")]
    public string DebugPath
    {
      get => (string) this["debugPath"];
      set => this["debugPath"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the assembly that contains the client script file as an embedded resource.
    /// </summary>
    /// <value>The fully qualified or partially qualified name of the assembly that contains a client script file as an embedded resource.</value>
    [ConfigurationProperty("assembly", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptReferenceAssemblyDescription", Title = "ScriptReferenceAssemblyTitle")]
    public string Assembly
    {
      get => (string) this["assembly"];
      set => this["assembly"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the ScriptManager.ScriptPath property is included in the URL when you register a client script file from a resource.
    /// </summary>
    /// <value><c>true</c> if the script path is not used when you register the client script; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("ignoreScriptPath", DefaultValue = false, IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptReferenceIgnorePathDescription", Title = "ScriptReferenceIgnorePathTitle")]
    public bool IgnoreScriptPath
    {
      get => (bool) this["ignoreScriptPath"];
      set => this["ignoreScriptPath"] = (object) value;
    }

    /// <summary>
    /// Determines whether the current script will be loaded from CDN
    /// </summary>
    /// <value><c>true</c> if client script references are loaded from CDN paths, otherwise, <c>false</c>.</value>
    [ConfigurationProperty("enableCdn", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptManagerCdnDescription", Title = "ScriptManagerCdnTitle")]
    public bool? EnableCdn
    {
      get => (bool?) this["enableCdn"];
      set => this["enableCdn"] = (object) value;
    }

    [ConfigurationProperty("combine", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CombineDescription", Title = "CombineTitle")]
    public bool Combine
    {
      get => (bool) this["combine"];
      set => this["combine"] = (object) value;
    }

    [ConfigurationProperty("outputposition", DefaultValue = ScriptReferenceOutputPosition.Same)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OutputPositionDescription", Title = "OutputPositionTitle")]
    public ScriptReferenceOutputPosition OutputPosition
    {
      get => (ScriptReferenceOutputPosition) this["outputposition"];
      set => this["outputposition"] = (object) value;
    }
  }
}
