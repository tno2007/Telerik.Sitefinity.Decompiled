// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IDataProviderEventsCall
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// An interface for types that are intercepted
  /// and invoke an instance of type <see cref="T:Telerik.Sitefinity.Data.DataEventsCallHandler" /> that trigger data events (Executing and Executed).
  /// </summary>
  public interface IDataProviderEventsCall
  {
    /// <summary>
    /// Raises the <see cref="E:Executing" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    void OnExecuting(ExecutingEventArgs args);

    /// <summary>
    /// Raises the <see cref="E:Executed" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    void OnExecuted(ExecutedEventArgs args);

    /// <summary>Gets the dirty items from the current transaction.</summary>
    /// <returns>An array of dirty items.</returns>
    IList GetDirtyItems();

    /// <summary>
    /// Fired before executing data method. The method can be canceled.
    /// </summary>
    event EventHandler<ExecutingEventArgs> Executing;

    /// <summary>Fired after executing data method.</summary>
    event EventHandler<ExecutedEventArgs> Executed;
  }
}
