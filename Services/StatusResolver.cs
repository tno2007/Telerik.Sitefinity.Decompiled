// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.StatusResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Status Resolver class.</summary>
  /// <typeparam name="TItem">Item type for status</typeparam>
  internal class StatusResolver
  {
    public StatusResolver(Type type, string provider, Guid itemId, string rootKey)
    {
      this.ItemType = type;
      this.ItemProvider = provider;
      this.ItemId = itemId;
      this.RootKey = rootKey;
    }

    /// <summary>Gets or sets item id</summary>
    public Guid ItemId { get; private set; }

    public Type ItemType { get; private set; }

    public string ItemProvider { get; private set; }

    public string RootKey { get; private set; }

    /// <summary>Resolve status by status text</summary>
    /// <param name="statusText">Current status text</param>
    /// <returns>Status text</returns>
    public Status ResolveStatus() => StatusResolver.Resolve((IEnumerable<StatusInfo>) SystemManager.StatusProviderRegistry.GetItemStatuses(this.ItemId, this.ItemType, this.ItemProvider, this.RootKey, statusBehaviour: StatusBehaviour.Additional).ToList<StatusInfo>());

    /// <summary>Helper method to resolve status</summary>
    /// <param name="id">The Item id</param>
    /// <param name="status">Current status</param>
    /// <returns>Status text</returns>
    public static Status Resolve(Type type, string provider, Guid id, string rootKey = null) => new StatusResolver(type, provider, id, rootKey).ResolveStatus();

    internal static Status Resolve(IEnumerable<StatusInfo> statusInfo)
    {
      if (!statusInfo.Any<StatusInfo>())
        return (Status) null;
      string str = string.Empty;
      foreach (StatusInfo statusInfo1 in statusInfo)
      {
        if (!statusInfo1.StatusText.IsNullOrEmpty())
          str = string.IsNullOrEmpty(str) ? statusInfo1.StatusText : statusInfo1.StatusText + " | " + str;
      }
      return new Status()
      {
        Text = str,
        PrimaryProvider = statusInfo.First<StatusInfo>().Source
      };
    }
  }
}
