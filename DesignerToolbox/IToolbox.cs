// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DesignerToolbox.IToolbox
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.DesignerToolbox
{
  /// <summary>
  /// Defines an interface any Sitefinity toolbox implementes. An example of toolbox could be
  /// page editor toolbox or form designer toolbox.
  /// </summary>
  public interface IToolbox
  {
    /// <summary>
    /// Gets or sets the programmatic name of the toolbox item; this is how toolboxes are identified.
    /// </summary>
    /// <value>The name of the toolbox.</value>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the title of the toolbox. This value will be displayed in the user interface when
    /// displaying a toolbox.
    /// </summary>
    /// <remarks>
    /// This value does not have to be unique; also it can be localizable.
    /// </remarks>
    /// <value>The title of the toolbox.</value>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the description or purpose of the tools in the toolbox.
    /// </summary>
    /// <value>The description of the toolbox.</value>
    string Description { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the global resource class used to localize the strings of the toolbox.
    /// </summary>
    /// <value>The id of the resource class used for localization.</value>
    string ResourceClassId { get; set; }

    /// <summary>
    /// Gets the list of sections that hold (organize) the tools inside of the toolbox.
    /// </summary>
    /// <value>List of the toolbox sections.</value>
    IEnumerable<IToolboxSection> Sections { get; }

    /// <summary>Gets a toolbox section by its name.</summary>
    /// <param name="name">Section name.</param>
    /// <returns>An <see cref="T:Telerik.Sitefinity.DesignerToolbox.IToolboxSection" /> instance or <c>null</c>, if not found.</returns>
    IToolboxSection GetSection(string name);
  }
}
