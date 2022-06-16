// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IItemSelectorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Selectors;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>An extraction of ItemSelector's properties</summary>
  public interface IItemSelectorDefinition : IDefinition
  {
    /// <summary>Gets or sets the text of "all items" filter button</summary>
    string AllItemsText { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be sorted
    /// </summary>
    bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be searched
    /// </summary>
    bool AllowSearching { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be sorted
    /// </summary>
    bool AllowSorting { get; set; }

    /// <summary>
    /// Gets or sets the data key names used by the selector data source
    /// </summary>
    string DataKeyNames { get; set; }

    /// <summary>
    /// Gets or sets the comma delimited list of data members to be be displayed in the selector
    /// </summary>
    List<IDataMemberInfo> DataMembers { get; }

    /// <summary>
    /// Gets or sets the name of the type that is used in place of original
    /// type - generally for the serialization purposes
    /// </summary>
    string ItemSurrogateType { get; set; }

    /// <summary>
    /// Gets or sets the name of the type that is to be selected
    /// </summary>
    string ItemType { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider that selector ought to use
    /// </summary>
    string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the text of "selected items" filter button
    /// </summary>
    string SelectedItemsText { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether all items / selected items filter should be
    /// displayed.
    /// </summary>
    bool ShowSelectedFilter { get; set; }
  }
}
