// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.DynamicLists.IDynamicListProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Newsletters.Composition;

namespace Telerik.Sitefinity.Modules.Newsletters.DynamicLists
{
  /// <summary>
  /// This is the interface that needs to be implemented by every dynamic list provider.
  /// </summary>
  public interface IDynamicListProvider
  {
    /// <summary>
    /// Gets all the dynamic lists provided by the instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.DynamicLists.IDynamicListProvider" />.
    /// </summary>
    IEnumerable<DynamicListInfo> GetDynamicLists();

    /// <summary>
    /// Gets all the available merge tags of the items inside of the dynamic list. The key of the
    /// dictionary is the caption or title or the property, while the value is the property name
    /// that will be used by Type descriptors to obtain the value.
    /// </summary>
    /// <param name="listKey">The key used to identify the list.</param>
    /// <returns>An enumerable of the string.</returns>
    IList<MergeTag> GetAvailableProperties(string listKey);

    /// <summary>Gets all the subscribers from the dynamic list.</summary>
    /// <param name="listKey">The key of the list.</param>
    /// <returns>An enumerable of objects representing the subscribers.</returns>
    IEnumerable<object> GetSubscribers(string listKey);

    /// <summary>
    /// Gets the subscribers from the dynamic list by given filter expression and paging.
    /// </summary>
    /// <param name="listKey">The list key.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns></returns>
    IEnumerable<object> GetSubscribers(
      string listKey,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount);

    /// <summary>Resolves the merge tag to the actual value.</summary>
    /// <param name="mergeTag">Name of the merge tag.</param>
    /// <param name="instance">Instance from which the merge tag should be resolved.</param>
    /// <returns>The value of the resolved merge tag.</returns>
    object ResolveMergeTag(MergeTag mergeTag, object instance);

    /// <summary>Gets the number of subscribers in dynamic list.</summary>
    /// <returns>Number of subscribers in dynamic list.</returns>
    int SubscribersCount(string listKey);
  }
}
