// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.IDynamicContentBeforeCommitEventFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// Interface for factory that constructs events for dynamic content that are raised before commit transaction.
  /// </summary>
  public interface IDynamicContentBeforeCommitEventFactory
  {
    /// <summary>Constructs event from item.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="actionType">State of the item (new, updated, deleted).</param>
    /// <param name="dataProviderName">Name of the data provide for the item.</param>
    /// <returns>An <see cref="T:Telerik.Sitefinity.Data.Events.IDataEvent" /> constructed from the data item.</returns>
    IDataEvent CreateEvent(
      DynamicContent dataItem,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName);

    /// <summary>Constructs event from item for specific language.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="actionType">State of the item (new, updated, deleted).</param>
    /// <param name="dataProviderName">Name of the data provide for the item.</param>
    /// <param name="language">The language for which the event is constructed.</param>
    /// <returns>An <see cref="T:Telerik.Sitefinity.Data.Events.IDataEvent" /> constructed from the data item for specified language.</returns>
    IDataEvent CreateEvent(
      DynamicContent dataItem,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName,
      string language);
  }
}
