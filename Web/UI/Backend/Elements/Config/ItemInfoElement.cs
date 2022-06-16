// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ItemInfoElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>The configuration element for the ItemInfoDefinition</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemInfoConfigDescription", Title = "ItemInfoConfigCaption")]
  public class ItemInfoElement : DefinitionConfigElement, IItemInfoDefinition, IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ItemInfoElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ItemInfoElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ItemInfoDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the name of the provider to retrieve the content element.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("providerName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemInfoConfigProviderNameDescription", Title = "ItemInfoConfigProviderNameTitle")]
    public string ProviderName
    {
      get => (string) this["providerName"];
      set => this["providerName"] = (object) value;
    }

    /// <summary>Gets or sets the ID of the content element.</summary>
    /// <value></value>
    [ConfigurationProperty("itemId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemInfoConfigItemIdDescription", Title = "ItemInfoConfigItemIdTitle")]
    public Guid ItemId
    {
      get => (Guid) this["itemId"];
      set => this["itemId"] = (object) value;
    }

    /// <summary>Gets or sets the title of the content element.</summary>
    /// <value></value>
    [ConfigurationProperty("title")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemInfoConfigTitleDescription", Title = "ItemInfoConfigTitleTitle")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }
  }
}
