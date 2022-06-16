// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.MailingListExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Modules.Newsletters.DynamicLists;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>
  /// Extension methods which extend Sitefinity type <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> with helper methods.
  /// </summary>
  public static class MailingListExtensions
  {
    /// <summary>
    /// Extension method which extend Sitefinity type <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> with helper methods.
    /// </summary>
    public static IQueryable<Subscriber> Subscribers(
      this MailingList mailingList)
    {
      if (mailingList.Provider is NewslettersDataProvider provider)
        return provider.GetMailingListSubscribers(mailingList.Id);
      throw new InvalidOperationException("The MailingList must have a Provider set before it can query its subscribers!");
    }

    /// <summary>
    /// Returns the number of all subscribers in the Newsletter module for the given mailing list.
    /// </summary>
    /// <param name="mailingList">The mailing list.</param>
    /// <returns></returns>
    public static int SubscribersCount(this MailingList mailingList)
    {
      if (!(mailingList.Provider is NewslettersDataProvider provider))
        throw new InvalidOperationException("The MailingList must have a Provider set before it can query its subscribers!");
      return NewslettersManager.GetManager(provider.Name).GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (sub => sub.Lists.Contains(mailingList))).Count<Subscriber>();
    }

    /// <summary>
    /// Returns the number of all subscribers comming from the connected sources.
    /// </summary>
    /// <param name="mailingList">The mailing list.</param>
    /// <returns></returns>
    public static int DynamicSubscribersCount(this MailingList mailingList)
    {
      if (!(mailingList.Provider is NewslettersDataProvider provider))
        throw new InvalidOperationException("The MailingList must have a Provider set before it can query its subscribers!");
      NewslettersManager manager = NewslettersManager.GetManager(provider.Name);
      int num = 0;
      foreach (DynamicListSettings dynamicList in (IEnumerable<DynamicListSettings>) mailingList.DynamicLists)
      {
        IDynamicListProvider dynamicListProvider = manager.GetDynamicListProvider(dynamicList.DynamicListProviderName);
        num += dynamicListProvider.SubscribersCount(dynamicList.ListKey);
      }
      return num;
    }
  }
}
