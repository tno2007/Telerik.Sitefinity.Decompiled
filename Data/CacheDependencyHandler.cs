// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.CacheDependencyHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Represents cache dependency handler that resides only in RAM of the current application and
  /// cannot be used for cross application notifications.
  /// </summary>
  public class CacheDependencyHandler : ICacheDependencyHandler, IDisposable
  {
    /// <summary>
    /// Types for which the cache dependency will fire (except ICacheItem and IDataItem)
    /// </summary>
    private static Type[] cacheDependencyKnownTypes = new Type[17]
    {
      typeof (Permission),
      typeof (Content),
      typeof (PageData),
      typeof (DraftData),
      typeof (ConfigSection),
      typeof (ControlPresentation),
      typeof (PageNode),
      typeof (PageData),
      typeof (PageTemplate),
      typeof (SecurityRoot),
      typeof (DynamicContent),
      typeof (XmlConfigItem),
      typeof (WorkflowDefinition),
      typeof (DynamicModule),
      typeof (DynamicModuleType),
      typeof (DynamicModuleField),
      typeof (Addon)
    };
    private Timer pollTimer;
    private object syncLock = new object();
    private CacheDependencyHandler.SubscribedObjectsCollection items = new CacheDependencyHandler.SubscribedObjectsCollection();

    /// <summary>Gets the subscribed objects.</summary>
    /// <value>The subscribed objects.</value>
    protected CacheDependencyHandler.SubscribedObjectsCollection SubscribedObjects => this.items;

    /// <summary>
    /// Gets a value indicating whether this instance is polling a process of cleaning dead references.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is polling; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsPolling => this.pollTimer != null;

    /// <summary>
    /// Gets or sets the poll interval for cleaning dead references.
    /// The interval is in milliseconds.
    /// The default value is 60 000.
    /// </summary>
    /// <value>The poll interval.</value>
    public int PollInterval { get; set; }

    /// <summary>Subscribes the specified item type.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="callback">The callback.</param>
    public virtual void Subscribe(Type itemType, ChangedCallback callback) => this.Subscribe(itemType, (string) null, callback);

    /// <summary>Subscribes the specified item.</summary>
    /// <param name="item">The item.</param>
    /// <param name="callback">The callback.</param>
    public virtual void Subscribe(object item, ChangedCallback callback) => this.Subscribe(item.GetType(), this.GetItemKey(item), callback);

    /// <summary>Subscribes the specified item type.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="key">The key.</param>
    /// <param name="callback">The callback.</param>
    public virtual void Subscribe(Type itemType, string key, ChangedCallback callback)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      if (key == string.Empty)
        key = (string) null;
      CacheDependencyKey key1 = new CacheDependencyKey()
      {
        Key = key,
        Type = itemType
      };
      CacheDependencyHandler.NotifiedObjectWrapper notifiedObjectWrapper;
      if (!this.items.TryGetValue(key1, out notifiedObjectWrapper))
      {
        lock (this.syncLock)
        {
          if (!this.items.TryGetValue(key1, out notifiedObjectWrapper))
          {
            notifiedObjectWrapper = new CacheDependencyHandler.NotifiedObjectWrapper()
            {
              Key = key1
            };
            notifiedObjectWrapper.AddCallback(callback);
            this.items.Add(notifiedObjectWrapper);
            return;
          }
        }
      }
      lock (notifiedObjectWrapper)
        notifiedObjectWrapper.AddCallback(callback);
    }

    /// <summary>
    /// Determines whether the specified item type is subscribed with the provided callback for change notification.
    /// </summary>
    /// <param name="itemType">The type to be notified about.</param>
    /// <param name="callback">The callback.</param>
    /// <returns>
    /// 	<c>true</c> if the specified type is subscribed with the provided callback; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsSubscribed(Type itemType, ChangedCallback callback) => this.IsSubscribed(itemType, (string) null, callback);

    /// <summary>
    /// Determines whether the specified item is subscribed with the provided callback for change notification.
    /// </summary>
    /// <param name="item">The item to be notified about.</param>
    /// <param name="callback">The callback.</param>
    /// <returns>
    /// 	<c>true</c> if the specified item is subscribed with the provided callback; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsSubscribed(object item, ChangedCallback callback) => this.IsSubscribed(item.GetType(), this.GetItemKey(item), callback);

    /// <summary>
    /// Determines whether the specified type and key combination is subscribed with the provided callback for change notification.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="key">The primary key of the item.</param>
    /// <param name="callback">The callback.</param>
    /// <returns>
    /// 	<c>true</c> if the specified type and key combination is subscribed; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsSubscribed(Type itemType, string key, ChangedCallback callback)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      if (key == string.Empty)
        key = (string) null;
      CacheDependencyKey key1 = new CacheDependencyKey()
      {
        Key = key,
        Type = itemType
      };
      CacheDependencyHandler.NotifiedObjectWrapper notifiedObjectWrapper;
      bool flag;
      lock (this.syncLock)
        flag = this.items.TryGetValue(key1, out notifiedObjectWrapper);
      if (!flag)
        return false;
      lock (notifiedObjectWrapper)
        return notifiedObjectWrapper.HasCallback(callback);
    }

    /// <summary>Unsubscribe the specified item type.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="callback">The callback.</param>
    public virtual void Unsubscribe(Type itemType, ChangedCallback callback) => this.Unsubscribe(itemType, (string) null, callback);

    /// <summary>Unsubscribe the specified item.</summary>
    /// <param name="item">The item.</param>
    /// <param name="callback">The callback.</param>
    public virtual void Unsubscribe(object item, ChangedCallback callback) => this.Unsubscribe(item.GetType(), this.GetItemKey(item), callback);

    /// <summary>Unsubscribe the specified item type.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="key">The key.</param>
    /// <param name="callback">The callback.</param>
    public virtual void Unsubscribe(Type itemType, string key, ChangedCallback callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      if (key == string.Empty)
        key = (string) null;
      CacheDependencyKey key1 = new CacheDependencyKey()
      {
        Key = key,
        Type = itemType
      };
      CacheDependencyHandler.NotifiedObjectWrapper wrapper;
      bool flag;
      lock (this.syncLock)
        flag = this.items.TryGetValue(key1, out wrapper);
      if (!flag)
        return;
      lock (wrapper)
        wrapper.RemoveCallback(callback);
      this.RemoveIfEmpty(wrapper);
    }

    /// <summary>Notifies the specified item type.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    public virtual IList<CacheDependencyKey> Notify(Type itemType) => this.Notify(itemType, (string) null);

    /// <summary>Notifies the specified item.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public virtual IList<CacheDependencyKey> Notify(object item) => this.Notify(item.GetType(), this.GetItemKey(item));

    /// <summary>Notifies the specified item type.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public virtual IList<CacheDependencyKey> Notify(
      Type itemType,
      string key)
    {
      return this.Notify(new CacheDependencyKey()
      {
        Type = itemType,
        Key = key
      });
    }

    /// <summary>Notifies the specified item type.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public virtual IList<CacheDependencyKey> Notify(
      CacheDependencyKey keyObject)
    {
      if (keyObject.Type == (Type) null)
      {
        string key = !string.IsNullOrEmpty(keyObject.Key) ? keyObject.Key : throw new ArgumentException("keyObject.Key", "When keyObject.Type is not specified the Key property is required.");
        List<CacheDependencyHandler.NotifiedObjectWrapper> list;
        lock (this.syncLock)
          list = this.items.Where<CacheDependencyHandler.NotifiedObjectWrapper>((Func<CacheDependencyHandler.NotifiedObjectWrapper, bool>) (cd => cd.Key.Key == key)).ToList<CacheDependencyHandler.NotifiedObjectWrapper>();
        List<CacheDependencyKey> notified = new List<CacheDependencyKey>(list.Count);
        for (int index = 0; index < list.Count; ++index)
          this.NotifyObjectWrapper(list[index], keyObject, (IList<CacheDependencyKey>) notified, true);
        return (IList<CacheDependencyKey>) notified;
      }
      List<CacheDependencyKey> notified1 = new List<CacheDependencyKey>(2);
      if (string.IsNullOrEmpty(keyObject.Key))
      {
        if (keyObject.Key == string.Empty)
          keyObject.Key = (string) null;
        List<CacheDependencyHandler.NotifiedObjectWrapper> list;
        lock (this.syncLock)
          list = this.items.Where<CacheDependencyHandler.NotifiedObjectWrapper>((Func<CacheDependencyHandler.NotifiedObjectWrapper, bool>) (cd => cd.Key.Type == keyObject.Type)).ToList<CacheDependencyHandler.NotifiedObjectWrapper>();
        for (int index = list.Count - 1; index >= 0; --index)
          this.NotifyObjectWrapper(list[index], keyObject, (IList<CacheDependencyKey>) notified1, false);
        return (IList<CacheDependencyKey>) notified1;
      }
      this.NotifyInternal(keyObject, keyObject, (IList<CacheDependencyKey>) notified1);
      this.NotifyInternal(new CacheDependencyKey()
      {
        Type = keyObject.Type
      }, keyObject, (IList<CacheDependencyKey>) notified1);
      return (IList<CacheDependencyKey>) notified1;
    }

    private void NotifyInternal(
      CacheDependencyKey keyObject,
      CacheDependencyKey message,
      IList<CacheDependencyKey> notified)
    {
      CacheDependencyHandler.NotifiedObjectWrapper wrapper;
      bool flag;
      lock (this.syncLock)
        flag = this.items.TryGetValue(keyObject, out wrapper);
      if (!flag)
        return;
      this.NotifyObjectWrapper(wrapper, message, notified, false);
    }

    private void NotifyObjectWrapper(
      CacheDependencyHandler.NotifiedObjectWrapper wrapper,
      CacheDependencyKey message,
      IList<CacheDependencyKey> notified,
      bool keyOnly)
    {
      IList<CacheDependencyHandler.NotifiedObjectWrapper.IInvokable> invokableList = (IList<CacheDependencyHandler.NotifiedObjectWrapper.IInvokable>) null;
      lock (wrapper)
        invokableList = wrapper.GetInvokables();
      Type itemType = keyOnly ? (Type) null : message.Type;
      for (int index = invokableList.Count - 1; index >= 0; --index)
        invokableList[index].Invoke((ICacheDependencyHandler) this, itemType, message.OriginalKey);
      this.RemoveIfEmpty(wrapper);
      notified.Add(wrapper.Key);
    }

    /// <summary>Notifies all subscribed items.</summary>
    /// <returns></returns>
    public virtual IList<CacheDependencyKey> NotifyAll()
    {
      List<CacheDependencyHandler.NotifiedObjectWrapper> list;
      lock (this.syncLock)
        list = this.items.ToList<CacheDependencyHandler.NotifiedObjectWrapper>();
      List<CacheDependencyKey> notified = new List<CacheDependencyKey>(list.Count);
      for (int index = list.Count - 1; index >= 0; --index)
        this.NotifyObjectWrapper(list[index], list[index].Key, (IList<CacheDependencyKey>) notified, false);
      return (IList<CacheDependencyKey>) notified;
    }

    /// <summary>Initializes this instance.</summary>
    /// <param name="config">Name-value configuration.</param>
    public virtual void Initialize(NameValueCollection config)
    {
      string s = config["pollInterval"];
      this.PollInterval = !string.IsNullOrEmpty(s) ? int.Parse(s) : 60000;
      this.StartPolling();
    }

    /// <summary>Starts polling process of cleaning dead references.</summary>
    public void StartPolling()
    {
      if (this.pollTimer != null)
        return;
      this.pollTimer = new Timer(new System.Threading.TimerCallback(this.TimerCallback), (object) null, this.PollInterval, this.PollInterval);
    }

    /// <summary>Stop the polling process.</summary>
    public void StopPolling()
    {
      if (this.pollTimer == null)
        return;
      this.pollTimer.Dispose();
      this.pollTimer = (Timer) null;
    }

    /// <summary>
    /// Cleans all weak references to items that have been disposed.
    /// </summary>
    public virtual void CleanDeadReferences()
    {
      lock (this.syncLock)
      {
        for (int index = this.items.Count - 1; index >= 0; --index)
        {
          CacheDependencyHandler.NotifiedObjectWrapper notifiedObjectWrapper = this.items[index];
          lock (notifiedObjectWrapper)
          {
            notifiedObjectWrapper.RemoveDeadCallbacks();
            if (notifiedObjectWrapper.IsEmpty())
              this.items.RemoveAt(index);
          }
        }
      }
    }

    private void RemoveIfEmpty(
      CacheDependencyHandler.NotifiedObjectWrapper wrapper)
    {
      if (!wrapper.IsEmpty())
        return;
      lock (this.syncLock)
      {
        lock (wrapper)
        {
          if (!wrapper.IsEmpty())
            return;
          this.items.Remove(wrapper);
        }
      }
    }

    /// <summary>Gets the item key.</summary>
    /// <param name="item">The item.</param>
    /// <returns>The key of the item.</returns>
    [Obsolete("Use CacheDependency.GetItemKey instead.")]
    public virtual string GetItemKey(object item) => CacheDependency.GetItemKey(item);

    /// <summary>
    /// Resolves the key from the specified item if the item is assignable from one of the
    /// dependencies well known types or implements ICacheITem interface
    /// </summary>
    /// <param name="item">item to get the key from</param>
    /// <param name="key">key</param>
    /// <returns>true of the key was resolved</returns>
    [Obsolete("Use CacheDependency.TryGetWellKnownItemKey instead.")]
    public static bool TryGetWellKnownItemKey(object item, out string key) => CacheDependency.TryGetWellKnownItemKey(item, out key);

    private void TimerCallback(object notUsed) => ThreadPool.QueueUserWorkItem((WaitCallback) (unused => this.BackgroundWork(new Action(this.CleanDeadReferences))));

    private void BackgroundWork(Action work)
    {
      try
      {
        work();
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      if (this.pollTimer == null)
        return;
      this.pollTimer.Dispose();
    }

    [DebuggerDisplay("[NotifiedObjectWrapper] Key.Teyp={Key.Type}, Key.Key={Key.Key}, Callbacks.Count={Callbacks.Count}")]
    protected class NotifiedObjectWrapper
    {
      private IList<CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper> callbacks = (IList<CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper>) new List<CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper>();

      public CacheDependencyKey Key { get; set; }

      internal void AddCallback(ChangedCallback toAdd) => this.callbacks.Add(new CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper(toAdd));

      internal void RemoveCallback(ChangedCallback toRemove)
      {
        CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper callbackWrapper = new CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper(toRemove);
        for (int index = this.callbacks.Count - 1; index >= 0; --index)
        {
          CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper callback = this.callbacks[index];
          if (!callback.IsLive() || callback.Equals((object) callbackWrapper))
            this.callbacks.RemoveAt(index);
        }
      }

      internal bool HasCallback(ChangedCallback callback)
      {
        CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper callbackWrapper = new CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper(callback);
        return this.callbacks.Any<CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper>((Func<CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper, bool>) (c => c.Equals((object) callbackWrapper)));
      }

      /// <summary>
      /// Checks whether there are callbacks for this notifier object.
      /// </summary>
      internal bool IsEmpty() => this.callbacks.Count == 0;

      internal void RemoveDeadCallbacks()
      {
        for (int index = this.callbacks.Count - 1; index >= 0; --index)
        {
          if (!this.callbacks[index].IsLive())
            this.callbacks.RemoveAt(index);
        }
      }

      /// <summary>Returns a collection of invokable callbacks.</summary>
      internal IList<CacheDependencyHandler.NotifiedObjectWrapper.IInvokable> GetInvokables()
      {
        IList<CacheDependencyHandler.NotifiedObjectWrapper.IInvokable> invokables = (IList<CacheDependencyHandler.NotifiedObjectWrapper.IInvokable>) new List<CacheDependencyHandler.NotifiedObjectWrapper.IInvokable>(this.callbacks.Count);
        for (int index = this.callbacks.Count - 1; index >= 0; --index)
        {
          CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper callback = this.callbacks[index];
          if (callback.IsLive())
            invokables.Add((CacheDependencyHandler.NotifiedObjectWrapper.IInvokable) callback);
          else
            this.callbacks.Remove(callback);
        }
        return invokables;
      }

      internal interface IInvokable
      {
        void Invoke(ICacheDependencyHandler caller, Type itemType, string itemKey);
      }

      /// <summary>
      /// Wraps <see cref="T:Telerik.Sitefinity.Data.ChangedCallback" /> delegate, so that even if this delegate
      /// is gargabe collected, this instance is still invokable.
      /// It will become uninvokable when delegate's target is garbage collected and
      /// this is not a static delegate.
      /// </summary>
      private class CallbackWrapper : CacheDependencyHandler.NotifiedObjectWrapper.IInvokable
      {
        private WeakReference instance;
        private MethodInfo method;

        internal CallbackWrapper(ChangedCallback callback)
        {
          this.instance = new WeakReference(callback.Target);
          this.method = callback.Method;
        }

        internal bool IsLive() => this.instance.Target != null || this.method.IsStatic;

        /// <summary>Invokes wrapped delegate if it's not dead.</summary>
        public void Invoke(ICacheDependencyHandler caller, Type itemType, string itemKey)
        {
          object target = this.instance.Target;
          if (!this.IsLive())
            return;
          object[] parameters = new object[3]
          {
            (object) caller,
            (object) itemType,
            (object) itemKey
          };
          this.method.Invoke(target, parameters);
        }

        /// <summary>Compares delegates for equality.</summary>
        public override bool Equals(object obj) => obj is CacheDependencyHandler.NotifiedObjectWrapper.CallbackWrapper callbackWrapper && this.instance.Target == callbackWrapper.instance.Target && this.method == callbackWrapper.method;

        public override int GetHashCode()
        {
          object target = this.instance.Target;
          return target != null ? target.GetHashCode() + this.method.GetHashCode() : this.method.GetHashCode();
        }
      }
    }

    protected class SubscribedObjectsCollection : 
      KeyedCollection<CacheDependencyKey, CacheDependencyHandler.NotifiedObjectWrapper>
    {
      protected override CacheDependencyKey GetKeyForItem(
        CacheDependencyHandler.NotifiedObjectWrapper item)
      {
        return item.Key;
      }

      public bool TryGetValue(
        CacheDependencyKey key,
        out CacheDependencyHandler.NotifiedObjectWrapper item)
      {
        if (this.Dictionary != null)
          return this.Dictionary.TryGetValue(key, out item);
        foreach (CacheDependencyHandler.NotifiedObjectWrapper notifiedObjectWrapper in (IEnumerable<CacheDependencyHandler.NotifiedObjectWrapper>) this.Items)
        {
          if (this.Comparer.Equals(this.GetKeyForItem(notifiedObjectWrapper), key))
          {
            item = notifiedObjectWrapper;
            return true;
          }
        }
        item = (CacheDependencyHandler.NotifiedObjectWrapper) null;
        return false;
      }
    }
  }
}
