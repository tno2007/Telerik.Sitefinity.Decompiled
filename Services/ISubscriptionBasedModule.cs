// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ISubscriptionBasedModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>Marks module that are subscription based</summary>
  public interface ISubscriptionBasedModule
  {
    /// <summary>Gets the module expiration message</summary>
    /// <returns>The module expiration message</returns>
    string GetSubscriptionMessage();

    /// <summary>Gets the module subscription status</summary>
    /// <returns>the module subscription status</returns>
    SubscriptionBasedModuleStatusType GetSubscriptionStatus();
  }
}
