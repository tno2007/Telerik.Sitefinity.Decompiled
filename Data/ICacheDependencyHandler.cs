// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ICacheDependencyHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.Data
{
  /// <summary>Represents a contract for cache dependency handlers.</summary>
  /// <summary>Represents a contract for cache dependency handlers.</summary>
  public interface ICacheDependencyHandler
  {
    void Subscribe(Type itemType, ChangedCallback callback);

    void Subscribe(Type itemType, string key, ChangedCallback callback);

    void Subscribe(object item, ChangedCallback callback);

    bool IsSubscribed(Type itemType, ChangedCallback callback);

    bool IsSubscribed(Type itemType, string key, ChangedCallback callback);

    bool IsSubscribed(object item, ChangedCallback callback);

    void Unsubscribe(Type itemType, ChangedCallback callback);

    void Unsubscribe(Type itemType, string key, ChangedCallback callback);

    void Unsubscribe(object item, ChangedCallback callback);

    [Obsolete("Use method that accepts CacheDependencyKey parameter")]
    IList<CacheDependencyKey> Notify(Type itemType, string key);

    [Obsolete("Use method that accepts CacheDependencyKey parameter")]
    IList<CacheDependencyKey> Notify(Type itemType);

    [Obsolete("Use method that accepts CacheDependencyKey parameter")]
    IList<CacheDependencyKey> Notify(object item);

    IList<CacheDependencyKey> Notify(CacheDependencyKey key);

    IList<CacheDependencyKey> NotifyAll();

    [Obsolete("Not used. Build CacheDependencyKey insted and use CacheDependency.GetItemKey")]
    string GetItemKey(object item);

    /// <summary>Initializes this instance.</summary>
    void Initialize(NameValueCollection config);
  }
}
