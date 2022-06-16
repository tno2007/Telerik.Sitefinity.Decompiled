// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IDataEventProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// A contract for providers that will notify subscribers for changed data items.
  /// The subscribers are notified
  /// </summary>
  public interface IDataEventProvider
  {
    /// <summary>
    /// Flag that specifies if the events for changed data items will be raised.
    /// </summary>
    /// <returns></returns>
    bool DataEventsEnabled { get; }

    /// <summary>
    /// The filter that specifies what type of changed items should be send as arguments of an event notification.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    bool ApplyDataEventItemFilter(IDataItem item);
  }
}
