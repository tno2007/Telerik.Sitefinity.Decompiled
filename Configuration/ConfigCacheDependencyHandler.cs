// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigCacheDependencyHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>Represents security cache dependency handler.</summary>
  public class ConfigCacheDependencyHandler : CacheDependencyHandler
  {
    /// <summary>Subscribes the specified item type.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="key">The key.</param>
    /// <param name="callback">The callback.</param>
    public override void Subscribe(Type itemType, string key, ChangedCallback callback)
    {
      if (!typeof (ConfigSection).IsAssignableFrom(itemType))
        return;
      base.Subscribe(itemType, key, callback);
    }

    /// <summary>Gets the item key.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public override string GetItemKey(object item) => item is ConfigSection configSection ? configSection.GetPath() : (string) null;

    /// <summary>Notifies the specified item type.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public override IList<CacheDependencyKey> Notify(
      Type itemType,
      string key)
    {
      if (!typeof (ConfigSection).IsAssignableFrom(itemType))
        return (IList<CacheDependencyKey>) new CacheDependencyKey[0];
      CacheDependencyKey cacheDependencyKey = new CacheDependencyKey();
      cacheDependencyKey.Type = itemType;
      cacheDependencyKey.Key = key;
      try
      {
        Config.RegisterSection(itemType);
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw;
      }
      return (IList<CacheDependencyKey>) new CacheDependencyKey[1]
      {
        cacheDependencyKey
      };
    }

    public override IList<CacheDependencyKey> Notify(
      CacheDependencyKey keyObject)
    {
      return this.Notify(keyObject.Type, keyObject.Key);
    }
  }
}
