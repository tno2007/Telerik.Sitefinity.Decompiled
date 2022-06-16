// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.DynamicLists.Providers.UsersDynamicListProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.DynamicLists.Providers
{
  /// <summary>Dynamic lists provider for Sitefinity users.</summary>
  public class UsersDynamicListProvider : IDynamicListProvider
  {
    public const string providerName = "UsersDynamicListProvider";

    /// <summary>
    /// Gets all the dynamic lists provided by the instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.DynamicLists.IDynamicListProvider" />.
    /// </summary>
    public IEnumerable<DynamicListInfo> GetDynamicLists()
    {
      ConfigElementDictionary<string, DataProviderSettings> membershipProviders = this.GetSecurityConfig().MembershipProviders;
      List<DynamicListInfo> dynamicLists = new List<DynamicListInfo>();
      foreach (DataProviderSettings providerSettings in (IEnumerable<DataProviderSettings>) membershipProviders.Values)
        dynamicLists.Add(new DynamicListInfo(nameof (UsersDynamicListProvider), this.GetKey(providerSettings.Name), string.Format("Users ({0})", (object) providerSettings.Name)));
      return (IEnumerable<DynamicListInfo>) dynamicLists;
    }

    /// <summary>
    /// Gets all the available properties of the items inside of the dynamic list.
    /// </summary>
    /// <param name="listKey">The key used to identify the list.</param>
    /// <returns>An enumerable of the property descriptor objects.</returns>
    public IList<MergeTag> GetAvailableProperties(string listKey)
    {
      List<MergeTag> availableProperties = new List<MergeTag>();
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(typeof (User)))
        availableProperties.Add(new MergeTag(property.Name, property.Name, property.ComponentType.Name));
      return (IList<MergeTag>) availableProperties;
    }

    /// <summary>Gets all the subscribers from the dynamic list.</summary>
    /// <param name="listKey">The key of the list.</param>
    /// <returns>An enumerable of objects representing the subscribers.</returns>
    public IEnumerable<object> GetSubscribers(string listKey) => (IEnumerable<object>) this.GetMembershipFromListKey(listKey).GetUsers();

    /// <summary>
    /// Gets the subscribers from the dynamic list by given filter expression and paging.
    /// </summary>
    /// <param name="listKey">The list key.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>An enumerable of objects representing the subscribers.</returns>
    public IEnumerable<object> GetSubscribers(
      string listKey,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<User> source = this.GetMembershipFromListKey(listKey).GetUsers();
      if (!string.IsNullOrEmpty(filterExpression))
        source = source.Where<User>(filterExpression);
      totalCount = new int?(source.Count<User>());
      if (!string.IsNullOrEmpty(orderExpression))
        source = source.OrderBy<User>(orderExpression);
      int? nullable;
      if (skip.HasValue)
      {
        nullable = skip;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Skip<User>(skip.Value);
      }
      if (take.HasValue)
      {
        nullable = take;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Take<User>(take.Value);
      }
      return (IEnumerable<object>) source;
    }

    /// <summary>Resolves the merge tag to the actual value.</summary>
    /// <param name="mergeTag">Name of the merge tag.</param>
    /// <param name="instance">Instance from which the merge tag should be resolved.</param>
    /// <returns>The value of the resolved merge tag.</returns>
    public object ResolveMergeTag(MergeTag mergeTag, object instance) => TypeDescriptor.GetProperties(instance)[mergeTag.PropertyName].GetValue(instance);

    /// <summary>Gets the number of subscribers in dynamic list.</summary>
    /// <returns>Number of subscribers in dynamic list.</returns>
    public int SubscribersCount(string listKey) => this.GetSubscribers(listKey).Count<object>();

    protected SecurityConfig GetSecurityConfig() => Config.Get<SecurityConfig>();

    private string GetKey(string providerName) => "Users|" + providerName;

    private UserManager GetMembershipFromListKey(string listKey)
    {
      string providerName = listKey.Substring(listKey.IndexOf("|") + 1);
      return providerName == "Default" ? UserManager.GetManager() : UserManager.GetManager(providerName);
    }
  }
}
