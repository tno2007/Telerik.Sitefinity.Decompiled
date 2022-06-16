// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ItemSelectorDefinitionBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Selectors;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>
  /// Implements IItemSelectorDefinition in a way that is suitable for using in a template
  /// </summary>
  [ParseChildren(ChildrenAsProperties = true, DefaultProperty = "DataMembers")]
  public abstract class ItemSelectorDefinitionBase : 
    DefinitionBase,
    IItemSelectorDefinition,
    IDefinition
  {
    private string allItemsText;
    private bool allowMultipleSelection;
    private bool aallowSearching;
    private bool allowSorting;
    private string dataKeyNames;
    private List<IDataMemberInfo> dataMembers;
    private string itemSurrogateType;
    private string itemType;
    private string providerName;
    private string selectedItemsText;
    private bool showSelectedFilter;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public ItemSelectorDefinitionBase()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ItemSelectorDefinitionBase(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ItemSelectorDefinitionBase GetDefinition() => this;

    /// <summary>Gets or sets the text of "all items" filter button</summary>
    /// <value></value>
    public string AllItemsText
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be sorted
    /// </summary>
    /// <value></value>
    public bool AllowMultipleSelection
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be searched
    /// </summary>
    /// <value></value>
    public bool AllowSearching
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be sorted
    /// </summary>
    /// <value></value>
    public bool AllowSorting
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the data key names used by the selector data source
    /// </summary>
    /// <value></value>
    public string DataKeyNames
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the comma delimited list of data members to be be displayed in the selector
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
    public List<IDataMemberInfo> DataMembers => throw new NotImplementedException();

    /// <summary>
    /// Gets or sets the name of the type that is used in place of original
    /// type - generally for the serialization purposes
    /// </summary>
    /// <value></value>
    public string ItemSurrogateType
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the name of the type that is to be selected
    /// </summary>
    /// <value></value>
    public string ItemType
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the name of the provider that selector ought to use
    /// </summary>
    /// <value></value>
    public string ProviderName
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the text of "selected items" filter button
    /// </summary>
    /// <value></value>
    public string SelectedItemsText
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the value determining whether all items / selected items filter should be
    /// displayed.
    /// </summary>
    /// <value></value>
    public bool ShowSelectedFilter
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }
  }
}
