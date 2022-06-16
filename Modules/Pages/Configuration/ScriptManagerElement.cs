// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ScriptManagerElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Represents configuration element for ScriptManger.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptManagerElementDescription", Title = "ScriptManagerElementTitle")]
  public class ScriptManagerElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.ScriptManagerElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ScriptManagerElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the root path of the location that is used to build the paths to ASP.NET AJAX and custom script files.
    /// </summary>
    /// <value>
    /// The location where script files are stored. The default value is an empty string (""), which is interpreted as a relative path.
    /// </value>
    [ConfigurationProperty("scriptPath", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptManagerPathDescription", Title = "ScriptManagerPathTitle")]
    public string ScriptPath
    {
      get => (string) this["scriptPath"];
      set => this["scriptPath"] = (object) value;
    }

    /// <summary>
    /// Determines whether the current page loads client script references from CDN (Content Delivery Network) paths.
    /// </summary>
    /// <value><c>true</c> if client script references are loaded from CDN paths, otherwise, <c>false</c>.</value>
    [ConfigurationProperty("enableCdn", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptManagerCdnDescription", Title = "ScriptManagerCdnTitle")]
    public bool EnableCdn
    {
      get => (bool) this["enableCdn"];
      set => this["enableCdn"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether uncompressed versions of the script files should be used.
    /// </summary>
    /// <value><c>true</c> if debug; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("debug", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptManagerDebugDescription", Title = "ScriptManagerDebugTitle")]
    public bool Debug
    {
      get => (bool) this["debug"];
      set => this["debug"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the ScriptManager control renders localized versions of script files.
    /// </summary>
    /// <value><c>true</c> if localized script files will be rendered; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("enableScriptLocalization", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptManagerEnableScriptLocalizationDescription", Title = "ScriptManagerEnableScriptLocalizationTitle")]
    public bool? EnableScriptLocalization
    {
      get => (bool?) this["enableScriptLocalization"];
      set => this["enableScriptLocalization"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of script references, each of which represents a script file.
    /// </summary>
    /// <value>A dictionary script references.</value>
    [ConfigurationProperty("scriptReferences")]
    [ConfigurationCollection(typeof (ScriptReferenceElement), AddItemName = "reference")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptManagerScriptsDescription", Title = "ScriptManagerScriptsTitle")]
    public ConfigElementDictionary<string, ScriptReferenceElement> ScriptReferences => (ConfigElementDictionary<string, ScriptReferenceElement>) this["scriptReferences"];
  }
}
