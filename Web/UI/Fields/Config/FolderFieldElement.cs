// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.FolderFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes a folder field.
  /// </summary>
  public class FolderFieldElement : PageFieldElement
  {
    private const string ItemNamePropertyName = "itemName";
    private const string CreateLibraryServiceUrlPropName = "createLibraryServiceUrl";
    private const string ShowCreateNewLibraryButtonPropName = "showCreateNewLibraryButton";
    private const string LibraryTypeNamePronName = "libraryTypeName";

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public FolderFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new FolderFieldDefinition((ConfigElement) this);

    /// <summary>Gets or sets the item name.</summary>
    /// <value>The item name.</value>
    [ConfigurationProperty("itemName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FolderFieldItemNameDescription", Title = "FolderFieldItemNameCaption")]
    public string ItemName
    {
      get => (string) this["itemName"];
      set => this["itemName"] = (object) value;
    }

    [ConfigurationProperty("createLibraryServiceUrl")]
    public string CreateLibraryServiceUrl
    {
      get => (string) this["createLibraryServiceUrl"];
      set => this["createLibraryServiceUrl"] = (object) value;
    }

    [ConfigurationProperty("showCreateNewLibraryButton")]
    public bool ShowCreateNewLibraryButton
    {
      get => (bool) this["showCreateNewLibraryButton"];
      set => this["showCreateNewLibraryButton"] = (object) value;
    }

    /// <summary>Gets or sets the name of the library type.</summary>
    /// <value>The name of the library type.</value>
    [ConfigurationProperty("libraryTypeName")]
    public string LibraryTypeName
    {
      get => (string) this["libraryTypeName"];
      set => this["libraryTypeName"] = (object) value;
    }
  }
}
