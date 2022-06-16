// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ItemSelectorElementBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Selectors;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Implementation of IItemSelectorDefinition that is suitable for using
  /// </summary>
  public abstract class ItemSelectorElementBase : 
    DefinitionConfigElement,
    IItemSelectorDefinition,
    IDefinition
  {
    private List<IDataMemberInfo> dataMembers;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ItemSelectorElementBase" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ItemSelectorElementBase(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the text of "all items" filter button</summary>
    /// <value></value>
    [ConfigurationProperty("allItemsText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemSelectorElementBaseAllItemsTextDescription", Title = "ItemSelectorElementBaseAllItemsTextTitle")]
    public string AllItemsText
    {
      get => (string) this["allItemsText"];
      set => this["allItemsText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be sorted
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("allowMultipleSelection")]
    public bool AllowMultipleSelection
    {
      get => (bool) this["allowMultipleSelection"];
      set => this["allowMultipleSelection"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be searched
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("allowSearching")]
    public bool AllowSearching
    {
      get => (bool) this["allowSearching"];
      set => this["allowSearching"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be sorted
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("allowSorting")]
    public bool AllowSorting
    {
      get => (bool) this["allowSorting"];
      set => this["allowSorting"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the data key names used by the selector data source
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("dataKeyNames")]
    public string DataKeyNames
    {
      get => (string) this["dataKeyNames"];
      set => this["dataKeyNames"] = (object) value;
    }

    /// <summary>
    /// Propxy for DataMembers that is used in configuration instead
    /// </summary>
    [ConfigurationProperty("dataMembers")]
    [ConfigurationCollection(typeof (ItemSelectorDataMemberElement), AddItemName = "member")]
    public ConfigElementList<ItemSelectorDataMemberElement> DataMembersConfig => (ConfigElementList<ItemSelectorDataMemberElement>) this["items"];

    /// <summary>
    /// Gets or sets the comma delimited list of data members to be be displayed in the selector
    /// </summary>
    /// <remarks>Using this method directly can be a slow operation</remarks>
    public List<IDataMemberInfo> DataMembers
    {
      get
      {
        if (this.dataMembers == null && this.DataMembersConfig != null)
          this.dataMembers = this.DataMembersConfig.Cast<IDataMemberInfo>().ToList<IDataMemberInfo>();
        return this.dataMembers;
      }
    }

    /// <summary>
    /// Gets or sets the name of the type that is used in place of original
    /// type - generally for the serialization purposes
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("itemSurrogateType")]
    public string ItemSurrogateType
    {
      get => (string) this["itemSurrogateType"];
      set => this["itemSurrogateType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the type that is to be selected
    /// </summary>
    [ConfigurationProperty("itemType")]
    public string ItemType
    {
      get => (string) this["itemType"];
      set => this["itemType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the provider that selector ought to use
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("providerName")]
    public string ProviderName
    {
      get => (string) this["providerName"];
      set => this["providerName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the text of "selected items" filter button
    /// </summary>
    [ConfigurationProperty("selectedItemsText")]
    public string SelectedItemsText
    {
      get => (string) this["selectedItemsText"];
      set => this["selectedItemsText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value determining whether all items / selected items filter should be
    /// displayed.
    /// </summary>
    [ConfigurationProperty("showSelectedFilter")]
    public bool ShowSelectedFilter
    {
      get => (bool) this["showSelectedFilter"];
      set => this["showSelectedFilter"] = (object) value;
    }

    /// <summary>Constants for configration property names</summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      /// <summary>Config property name for "AllItemsText"</summary>
      public const string AllItemsText = "allItemsText";
      /// <summary>Config property name for "AllowMultipleSelection"</summary>
      public const string AllowMultipleSelection = "allowMultipleSelection";
      /// <summary>Config property name for "AllowSearching"</summary>
      public const string AllowSearching = "allowSearching";
      /// <summary>Config property name for "AllowSorting"</summary>
      public const string AllowSorting = "allowSorting";
      /// <summary>Config property name for "DataKeyNames"</summary>
      public const string DataKeyNames = "dataKeyNames";
      /// <summary>Config property name for "DataMembers"</summary>
      public const string DataMembers = "dataMembers";
      /// <summary>
      /// Name of the "add" element in the DataMembers collection
      /// </summary>
      public const string DataMembersItem = "member";
      /// <summary>Config property name for "ItemSurrogateType"</summary>
      public const string ItemSurrogateType = "itemSurrogateType";
      /// <summary>Config property name for "ItemType"</summary>
      public const string ItemType = "itemType";
      /// <summary>Config property name for "ProviderName"</summary>
      public const string ProviderName = "providerName";
      /// <summary>Config property name for "SelectedItemsText"</summary>
      public const string SelectedItemsText = "selectedItemsText";
      /// <summary>Config property name for "ShowSelectedFilter"</summary>
      public const string ShowSelectedFilter = "showSelectedFilter";
    }
  }
}
