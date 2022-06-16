// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ToolboxSection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DesignerToolbox;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Represents configuration element for toolbox items section.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxSectionElementDescription", Title = "ToolboxSectionElementTitle")]
  public class ToolboxSection : ConfigElement, IToolboxSection
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ToolboxSection(ConfigElement parent)
      : base(parent)
    {
    }

    internal ToolboxSection()
      : base(false)
    {
    }

    /// <inheritdoc />
    IEnumerable<IToolboxItem> IToolboxSection.Tools => (IEnumerable<IToolboxItem>) this.Tools.AsEnumerable().OrderBy<ToolboxItem, float>((Func<ToolboxItem, float>) (t => t.Ordinal));

    /// <inheritdoc />
    IToolboxItem IToolboxSection.GetTool(string toolName) => (IToolboxItem) this.GetTool(toolName);

    /// <inheritdoc />
    ISet<string> IToolboxSection.Tags
    {
      get => ToolboxTags.Parse(this.Tags);
      set => this.Tags = ToolboxTags.ToString(value);
    }

    /// <summary>
    /// Gets or sets a Boolean value indicating whether this section should appear in the toolbox.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("enabled", DefaultValue = true, IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxSectionEnabledDescription", Title = "EnabledCaption")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the programmatic name of the toolbox item.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemName", Title = "ItemNameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Defines what name will be displayed for the item on the user interface.
    /// </summary>
    /// <value>The title.</value>
    [ConfigurationProperty("title", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemTitle", Title = "ItemTitleCaption")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Describes the toolbox items section.</summary>
    /// <value>The description.</value>
    [ConfigurationProperty("description", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxSectionDescription", Title = "ItemDescriptionCaption")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>
    /// Defines global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class ID.</value>
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>A comma separated list of tags.</summary>
    [ConfigurationProperty("tags")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxTagsDescription", Title = "ToolboxTagsTitle")]
    public string Tags
    {
      get => (string) this["tags"];
      set => this["tags"] = (object) value;
    }

    /// <summary>The value used for sorting.</summary>
    [ConfigurationProperty("ordinal", DefaultValue = 0.0f)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxOrdinalDescription", Title = "ToolboxOrdinalTitle")]
    public float Ordinal
    {
      get => (float) this["ordinal"];
      set => this["ordinal"] = (object) value;
    }

    /// <summary>Gets a collection of toolbox items.</summary>
    [ConfigurationProperty("tools")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolsDescription", Title = "ToolsTitle")]
    public ConfigElementList<ToolboxItem> Tools => (ConfigElementList<ToolboxItem>) this["tools"];

    internal ToolboxItem GetTool(string toolName) => this.Tools.GetElementByKey(toolName) as ToolboxItem;
  }
}
