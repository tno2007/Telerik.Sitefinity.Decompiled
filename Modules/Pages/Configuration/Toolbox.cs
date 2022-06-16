// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.Toolbox
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
  /// Represents configuration element for Sitefinity toolbox.
  /// Toolbox is a container of controls used to build user interfaces such as pages, forms, newsletters and etc.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxElementDescription", Title = "ToolboxElementTitle")]
  public class Toolbox : ConfigElement, IToolbox
  {
    private IDictionary<string, ToolboxItem> tools;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public Toolbox(ConfigElement parent)
      : base(parent)
    {
    }

    /// <inheritdoc />
    IEnumerable<IToolboxSection> IToolbox.Sections => (IEnumerable<IToolboxSection>) this.Sections.AsEnumerable().OrderBy<ToolboxSection, float>((Func<ToolboxSection, float>) (s => s.Ordinal));

    /// <inheritdoc />
    IToolboxSection IToolbox.GetSection(string name) => (IToolboxSection) this.GetSection(name);

    /// <summary>
    /// Gets or sets the programmatic name of the toolbox item.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
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

    /// <summary>
    /// Describes the type and the purpose of the controls contained in this toolbox.
    /// </summary>
    /// <value>The description.</value>
    [ConfigurationProperty("description", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxDescription", Title = "ItemDescriptionCaption")]
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

    /// <summary>Gets a collection of toolbox sections.</summary>
    [ConfigurationProperty("sections")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxSectionsDescription", Title = "ToolboxSectionsTitle")]
    public virtual ConfigElementList<ToolboxSection> Sections => (ConfigElementList<ToolboxSection>) this["sections"];

    internal ToolboxSection GetSection(string sectionName) => this.Sections.GetElementByKey(sectionName) as ToolboxSection;

    internal void AddSection(ToolboxSection section) => this.Sections.Add(section);

    /// <summary>Gets all tools from all sections.</summary>
    /// <value>The tools.</value>
    internal IDictionary<string, ToolboxItem> Tools
    {
      get
      {
        if (this.tools == null)
        {
          this.tools = (IDictionary<string, ToolboxItem>) new Dictionary<string, ToolboxItem>();
          foreach (ToolboxSection section in this.Sections)
          {
            foreach (ToolboxItem tool in section.Tools)
            {
              if (!this.tools.ContainsKey(tool.Name))
                this.tools.Add(tool.Name, tool);
            }
          }
        }
        return this.tools;
      }
    }
  }
}
