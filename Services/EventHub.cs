// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.EventHub
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// A utility that interfaces the <see cref="T:Telerik.Sitefinity.Services.Events.IEventService" />,
  /// providing easy way to subscribe/unsubscribe to events and raise them.
  /// </summary>
  public class EventHub
  {
    internal const string EventOriginProviderExecutionStateKey = "EventOriginKey";

    /// <summary>Raises the specified event.</summary>
    /// <param name="event">The event.</param>
    public static void Raise(IEvent @event) => EventHub.Raise(@event, true);

    /// <summary>Raises the specified event.</summary>
    /// <param name="event">The event.</param>
    /// <param name="throwExceptions">When <c>false</c> event handler exceptions are caught.</param>
    public static void Raise(IEvent @event, bool throwExceptions) => EventHub.GetEventService()?.Raise(@event, throwExceptions);

    /// <summary>Subscribes the specified handler.</summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <param name="handler">The handler.</param>
    public static void Subscribe<TEvent>(SitefinityEventHandler<TEvent> handler) where TEvent : IEvent => EventHub.GetEventService()?.Subscribe<TEvent>(handler);

    /// <summary>Unsubscribes the specified handler.</summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <param name="handler">The handler.</param>
    public static void Unsubscribe<TEvent>(SitefinityEventHandler<TEvent> handler) where TEvent : IEvent => EventHub.GetEventService()?.Unsubscribe<TEvent>(handler);

    /// <summary>Gets the event service.</summary>
    /// <returns></returns>
    private static IEventService GetEventService()
    {
      try
      {
        return ObjectFactory.Resolve<IEventService>();
      }
      catch (ResolutionFailedException ex)
      {
        return (IEventService) null;
      }
    }
  }
}
