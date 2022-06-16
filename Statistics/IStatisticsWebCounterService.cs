// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Statistics.IStatisticsWebCounterService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Statistics
{
  /// <summary>
  /// An interface for subscribing for Web actions like 'Visit', 'Vote' etc,
  /// </summary>
  internal interface IStatisticsWebCounterService
  {
    /// <summary>Notifies the subscribers.</summary>
    /// <param name="objectType">Type of the object. Example "News". The parameter is case-insensitive.</param>
    /// <param name="actionName">Name of the action. Example "View". The parameter is case-insensitive.</param>
    /// <param name="objectId">The object id, e.g. a GUID. The parameter is case-sensitive.</param>
    void NotifySubscribers(string objectType, string actionName, string objectId);

    void SubscribeForWebAction(
      string objectType,
      string actionName,
      Action<object, WebActionEventArgs> subscriptionHandler);

    /// <summary>
    /// Unsubscribes for web action. Removes all actions from the subscribers equal to the passed <paramref name="subscriptionHandler" />.
    /// </summary>
    /// <param name="objectType">Type of the object. Example "News". The parameter is case-insensitive.</param>
    /// <param name="actionName">Name of the action. Example "View". The parameter is case-insensitive.</param>
    /// <param name="subscriptionHandler">A subscription handler which was already registered. The comparison is by .Equals()</param>
    void UnsubscribeForWebAction(
      string objectType,
      string actionName,
      Action<object, WebActionEventArgs> subscriptionHandler);

    /// <summary>Get Subscribers count.</summary>
    /// <returns>Subscribers count</returns>
    int SubscribersCount();
  }
}
