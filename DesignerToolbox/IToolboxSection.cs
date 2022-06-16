// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DesignerToolbox.IToolboxSection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.DesignerToolbox
{
  /// <summary>
  /// Defines the interface that a toolbox section implements. Toolbox section is used to group
  /// similar tools inside of the toolbox.
  /// </summary>
  public interface IToolboxSection
  {
    /// <summary>
    /// Gets or sets the programmatic name of the toolbox section; this is how toolbox sections
    /// are identified inside of the toolbox.
    /// </summary>
    /// <value>The name of the toolbox section.</value>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the title of the toolbox section. This value will be used in the user interface
    /// when representing the toolbox section; also this value can be localized.
    /// </summary>
    /// <value>The title of the toolbox section.</value>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates weather the section should be rendered in the toolbox.
    /// </summary>
    /// <value>True if section should be available; otherwise false.</value>
    bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the description of the toolbox section, which describes the purpose of the tools
    /// that section contains.
    /// </summary>
    /// <value>The description of the toolbox section.</value>
    string Description { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the global resource class used to localize the strings
    /// of the toolbox section.
    /// </summary>
    /// <value>The id of the resource class used for localization.</value>
    string ResourceClassId { get; set; }

    /// <summary>Gets the list of tools contained in the section.</summary>
    /// <value>
    /// A list of <see cref="T:Telerik.Sitefinity.DesignerToolbox.IToolboxItem" /> items.
    /// </value>
    IEnumerable<IToolboxItem> Tools { get; }

    /// <summary>Gets a tool by name.</summary>
    /// <param name="toolName">Name of the tool.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.DesignerToolbox.IToolboxItem" /> or <c>null</c>, if not found.</returns>
    IToolboxItem GetTool(string toolName);

    /// <summary>
    /// A set of tags that describe the current section.
    /// One example of their use would be the toolbox section filtering.
    /// </summary>
    ISet<string> Tags { get; set; }
  }
}
