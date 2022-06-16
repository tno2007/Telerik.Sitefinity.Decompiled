// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.IItemFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>Represents a contract for all facades.</summary>
  public interface IItemFacade<TFacade, TDataItem> : IFacade<TFacade>
  {
    /// <summary>
    /// Creates a new instance of type <typeparamref name="TDataItem" />
    /// </summary>
    /// <returns>An instance of <typeparamref name="TFacade" />.</returns>
    TFacade CreateNew();

    /// <summary>
    /// Creates a new instance of type <typeparamref name="TDataItem" /> with specified id.
    /// </summary>
    /// <returns>An instance of <typeparamref name="TFacade" />.</returns>
    TFacade CreateNew(Guid itemId);

    /// <summary>
    /// Returns instance of type <typeparamref name="TDataItem" /> currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of <typeparamref name="TDataItem" /> object.</returns>
    TDataItem Get();

    /// <summary>
    /// Sets an instance of type <typeparamref name="TDataItem" /> on currently loaded fluent API.
    /// </summary>
    /// <returns>An instance of <typeparamref name="TFacade" /> object.</returns>
    TFacade Set(TDataItem item);

    /// <summary>
    /// Performs an arbitrary action on the <typeparamref name="TDataItem" /> object.
    /// </summary>
    /// <param name="setAction">An action to be performed on the <typeparamref name="TDataItem" /> object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the <typeparamref name="TDataItem" /> has not been initialized either through Facade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of  <typeparamref name="TFacade" />object.</returns>
    TFacade Do(Action<TDataItem> setAction);
  }
}
