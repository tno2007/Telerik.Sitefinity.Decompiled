// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Events.EventService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Services.Events
{
  /// <summary>
  /// Provides the ability to potential subscribers to subscribe and unsubscribe for event notification.
  /// </summary>
  public class EventService : IEventService
  {
    private readonly ConcurrentDictionary<Type, EventService.HandlerList> handlerLists = new ConcurrentDictionary<Type, EventService.HandlerList>();

    /// <inheritdoc />
    public void Raise(IEvent @event, bool throwExceptions)
    {
      foreach (Type assignableEventType in EventTypesCache.GetAllAssignableEventTypes(@event.GetType()))
      {
        EventService.HandlerList handlerList;
        if (this.handlerLists.TryGetValue(assignableEventType, out handlerList))
          handlerList.Invoke(@event, throwExceptions);
      }
    }

    /// <inheritdoc />
    public void Subscribe<TEvent>(SitefinityEventHandler<TEvent> handler) where TEvent : IEvent => this.Subscribe(typeof (TEvent), (Delegate) handler);

    /// <inheritdoc />
    public void Unsubscribe<TEvent>(SitefinityEventHandler<TEvent> handler) where TEvent : IEvent => this.Unsubscribe(typeof (TEvent), (Delegate) handler);

    private void Subscribe(Type eventType, Delegate handler) => this.handlerLists.GetOrAdd(eventType, new EventService.HandlerList()).Add(handler);

    private void Unsubscribe(Type eventType, Delegate handler)
    {
      EventService.HandlerList handlerList;
      if (!this.handlerLists.TryGetValue(eventType, out handlerList))
        return;
      handlerList.Remove(handler);
    }

    internal class HandlerList
    {
      private readonly IList<Delegate> handlers = (IList<Delegate>) new List<Delegate>();
      private readonly ReaderWriterLockSlim handlersLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
      private static readonly TimeSpan handlersLockTimeout = TimeSpan.FromSeconds(60.0);

      public void Add(Delegate handler)
      {
        this.handlersLock.TryEnterWriteLock(EventService.HandlerList.handlersLockTimeout);
        try
        {
          this.handlers.Add(handler);
        }
        finally
        {
          this.handlersLock.ExitWriteLock();
        }
      }

      public void Remove(Delegate handler)
      {
        this.handlersLock.TryEnterWriteLock(EventService.HandlerList.handlersLockTimeout);
        try
        {
          this.handlers.Remove(handler);
        }
        finally
        {
          this.handlersLock.ExitWriteLock();
        }
      }

      public void Invoke(IEvent @event, bool throwExceptions)
      {
        this.handlersLock.TryEnterReadLock(EventService.HandlerList.handlersLockTimeout);
        try
        {
          foreach (Delegate handler in (IEnumerable<Delegate>) this.handlers)
          {
            if ((object) handler != null)
            {
              try
              {
                handler.DynamicInvoke((object) @event);
              }
              catch (Exception ex)
              {
                if (throwExceptions)
                  throw new EventHandlerInvocationException(ex);
                Log.Write((object) ex, ConfigurationPolicy.ErrorLog);
              }
            }
          }
        }
        finally
        {
          this.handlersLock.ExitReadLock();
        }
      }
    }
  }
}
