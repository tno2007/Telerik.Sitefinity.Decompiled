// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.LibraryWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents the configuration element for the LibraryWidget
  /// </summary>
  public class LibraryWidgetElement : 
    ContentItemWidgetElement,
    ILibraryWidgetDefinition,
    IContentItemWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.LibraryWidgetElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public LibraryWidgetElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new LibraryWidgetDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets a value indicating whether the action menu with commands about the displayed library should be visible.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the action menu with commands about the displayed library is visible; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("showActionMenu", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibraryWidgetShowActionMenuDescription", Title = "LibraryWidgetShowActionMenuCaption")]
    public bool ShowActionMenu
    {
      get => (bool) this["showActionMenu"];
      set => this["showActionMenu"] = (object) value;
    }

    /// <summary>Gets or sets the name of a single item in a library.</summary>
    /// <value>The name of the item.</value>
    [ConfigurationProperty("itemName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibraryWidgetItemNameDescription", Title = "LibraryWidgetItemNameCaption")]
    public string ItemName
    {
      get => (string) this["itemName"];
      set => this["itemName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the items in a library in plural.
    /// </summary>
    /// <value>The name of the items.</value>
    [ConfigurationProperty("itemsName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibraryWidgetItemsNameDescription", Title = "LibraryWidgetItemsNameCaption")]
    public string ItemsName
    {
      get => (string) this["itemsName"];
      set => this["itemsName"] = (object) value;
    }

    /// <summary>Gets or sets the name of the library.</summary>
    /// <value>The name of the library.</value>
    [ConfigurationProperty("libraryName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibraryWidgetLibraryNameDescription", Title = "LibraryWidgetLibraryNameCaption")]
    public string LibraryName
    {
      get => (string) this["libraryName"];
      set => this["libraryName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether widget supports reordering.
    /// </summary>
    /// <value><c>true</c> if widget supports reordering; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("supportsReordering", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibraryWidgetSupportsReorderingDescription", Title = "LibraryWidgetSupportsReorderingCaption")]
    public bool SupportsReordering
    {
      get => (bool) this["supportsReordering"];
      set => this["supportsReordering"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct LibraryWidgetProps
    {
      public const string showActionMenu = "showActionMenu";
      public const string itemName = "itemName";
      public const string itemsName = "itemsName";
      public const string libraryName = "libraryName";
      public const string supportsReordering = "supportsReordering";
    }
  }
}
