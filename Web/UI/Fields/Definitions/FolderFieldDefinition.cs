// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.FolderFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  ///  A class that provides all information needed to construct a folder field control.
  /// </summary>
  public class FolderFieldDefinition : PageFieldDefinition
  {
    private string itemName;
    private bool showCreateNewLibraryButton;
    private string createLibraryServiceUrl;
    private string libraryTypeName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.FolderFieldDefinition" /> class.
    /// </summary>
    public FolderFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.FolderFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public FolderFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the item name.</summary>
    public string ItemName
    {
      get => this.ResolveProperty<string>(nameof (ItemName), this.itemName);
      set => this.itemName = value;
    }

    /// <summary>
    /// Gets or sets whether to show the button for creating new library.
    /// </summary>
    /// <value>The show create new library button.</value>
    public bool ShowCreateNewLibraryButton
    {
      get => this.ResolveProperty<bool>(nameof (ShowCreateNewLibraryButton), this.showCreateNewLibraryButton);
      set => this.showCreateNewLibraryButton = value;
    }

    /// <summary>Gets or sets the create library service URL.</summary>
    /// <value>The create library service URL.</value>
    public string CreateLibraryServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (CreateLibraryServiceUrl), this.createLibraryServiceUrl);
      set => this.createLibraryServiceUrl = value;
    }

    /// <summary>Gets or sets the name of the library type.</summary>
    /// <value>The name of the library type.</value>
    public string LibraryTypeName
    {
      get => this.ResolveProperty<string>(nameof (LibraryTypeName), this.libraryTypeName);
      set => this.libraryTypeName = value;
    }
  }
}
