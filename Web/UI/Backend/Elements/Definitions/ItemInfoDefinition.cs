// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ItemInfoDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an Item Info.
  /// </summary>
  [TypeConverter(typeof (JsonTypeConverter<ItemInfoDefinition>))]
  public class ItemInfoDefinition : DefinitionBase, IItemInfoDefinition, IDefinition
  {
    private string providerName;
    private Guid itemId;
    private string title;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ItemInfoDefinition" /> class.
    /// </summary>
    public ItemInfoDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ItemInfoDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ItemInfoDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ItemInfoDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the name of the provider to retrieve the content element.
    /// </summary>
    /// <value></value>
    public string ProviderName
    {
      get => this.ResolveProperty<string>(nameof (ProviderName), this.providerName);
      set => this.providerName = value;
    }

    /// <summary>Gets or sets the ID of the content element.</summary>
    /// <value></value>
    public Guid ItemId
    {
      get => this.ResolveProperty<Guid>(nameof (ItemId), this.itemId);
      set => this.itemId = value;
    }

    /// <summary>Gets or sets the title of the content element.</summary>
    /// <value></value>
    public string Title
    {
      get => this.ResolveProperty<string>(nameof (Title), this.title);
      set => this.title = value;
    }
  }
}
