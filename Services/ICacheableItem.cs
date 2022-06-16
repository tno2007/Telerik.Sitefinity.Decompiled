// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ICacheableItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Provides the ability to store/read information in/from a cached item.
  /// </summary>
  internal interface ICacheableItem
  {
    /// <summary>
    /// Tries to get the item for a given key. Returns true if the item has been retrieved successfully.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique key of the item.</param>
    /// <param name="value">The item that will be get.</param>
    /// <returns>Returns true if the item has been retrieved successfully. Otherwise false.</returns>
    bool TryGetValue<T>(string key, out T value);

    /// <summary>Stores the given value with the specified key.</summary>
    /// <param name="key">The key by which the item will be retrieved.</param>
    /// <param name="value">The item that will be stored.</param>
    void SetValue(string key, object value);
  }
}
