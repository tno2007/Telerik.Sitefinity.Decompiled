// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Statistics.StatisticsWebCounterService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Statistics
{
  /// <summary>
  /// A service providing registering and notifying event subscribers to web counters event
  /// </summary>
  internal class StatisticsWebCounterService : IStatisticsWebCounterService
  {
    protected static string eventHandlerIdPattern = "A_{0}-O_{1}";
    protected static Dictionary<string, List<Action<object, WebActionEventArgs>>> eventHandlers = new Dictionary<string, List<Action<object, WebActionEventArgs>>>();

    /// <inheritdoc />
    public virtual int SubscribersCount() => StatisticsWebCounterService.eventHandlers.Count;

    /// <inheritdoc />
    public virtual void NotifySubscribers(string objectType, string actionName, string objectId)
    {
      if (objectType == null || objectId == null)
        return;
      if (actionName.IsNullOrWhitespace())
        actionName = "visit";
      string uniqueEventId = this.GenerateUniqueEventId(actionName, objectType);
      lock (StatisticsWebCounterService.eventHandlers)
      {
        if (!StatisticsWebCounterService.eventHandlers.Keys.Contains<string>(uniqueEventId))
          return;
        List<Action<object, WebActionEventArgs>> eventHandler = StatisticsWebCounterService.eventHandlers[uniqueEventId];
        WebActionEventArgs webActionEventArgs = new WebActionEventArgs(actionName, objectType, objectId);
        foreach (Action<object, WebActionEventArgs> action in eventHandler)
        {
          try
          {
            action((object) this, webActionEventArgs);
          }
          catch
          {
          }
        }
      }
    }

    /// <inheritdoc />
    public virtual void SubscribeForWebAction(
      string objectType,
      string actionName,
      Action<object, WebActionEventArgs> subscriptionHandler)
    {
      if (objectType == null || actionName == null || subscriptionHandler == null)
        throw new ArgumentException("objectType, actionName, subscriptionHandler can not be null");
      string uniqueEventId = this.GenerateUniqueEventId(actionName, objectType);
      lock (StatisticsWebCounterService.eventHandlers)
      {
        if (!StatisticsWebCounterService.eventHandlers.Keys.Contains<string>(uniqueEventId))
        {
          StatisticsWebCounterService.eventHandlers.Add(uniqueEventId, new List<Action<object, WebActionEventArgs>>(1));
          StatisticsWebCounterService.eventHandlers[uniqueEventId].Add(subscriptionHandler);
        }
        else
        {
          List<Action<object, WebActionEventArgs>> eventHandler = StatisticsWebCounterService.eventHandlers[uniqueEventId];
          if (eventHandler.Where<Action<object, WebActionEventArgs>>((Func<Action<object, WebActionEventArgs>, bool>) (e => e.Equals((object) subscriptionHandler))).Any<Action<object, WebActionEventArgs>>())
            return;
          eventHandler.Add(subscriptionHandler);
        }
      }
    }

    /// <inheritdoc />
    public virtual void UnsubscribeForWebAction(
      string objectType,
      string actionName,
      Action<object, WebActionEventArgs> subscriptionHandler)
    {
      if (objectType == null || actionName == null || subscriptionHandler == null)
        throw new ArgumentException("objectType, actionName, subscriptionHandler can not be null");
      string uniqueEventId = this.GenerateUniqueEventId(actionName, objectType);
      lock (StatisticsWebCounterService.eventHandlers)
      {
        if (!StatisticsWebCounterService.eventHandlers.Keys.Contains<string>(uniqueEventId))
          return;
        StatisticsWebCounterService.eventHandlers[uniqueEventId].RemoveAll((Predicate<Action<object, WebActionEventArgs>>) (e => e.Equals((object) subscriptionHandler)));
      }
    }

    protected virtual string GenerateUniqueEventId(string actionName, string objectType)
    {
      actionName = actionName.ToLower();
      objectType = objectType.ToLower();
      return string.Format(StatisticsWebCounterService.eventHandlerIdPattern, (object) actionName, (object) objectType);
    }
  }
}
