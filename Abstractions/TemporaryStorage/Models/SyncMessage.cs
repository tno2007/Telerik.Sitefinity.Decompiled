// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.TemporaryStorage.Models.SyncMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Abstractions.TemporaryStorage.Models
{
  internal class SyncMessage
  {
    public SyncMessage(string key, string data, SyncOperation operation, DateTime expiresAtUtc)
    {
      if (string.IsNullOrWhiteSpace(key))
        throw new ArgumentException("message", nameof (key));
      if (data == null)
        throw new ArgumentNullException("message", nameof (data));
      this.Key = key;
      this.Operation = operation;
      this.Data = data;
      this.ExpiresAtUtc = expiresAtUtc;
    }

    public string Key { get; set; }

    public string Data { get; set; }

    public SyncOperation Operation { get; set; }

    public DateTime ExpiresAtUtc { get; set; }
  }
}
