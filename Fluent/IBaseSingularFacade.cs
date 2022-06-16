// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.IBaseSingularFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// Inteface that prototypes some of a single item facade's members
  /// </summary>
  /// <typeparam name="TCurrentFacade"></typeparam>
  /// <typeparam name="TContent"></typeparam>
  public interface IBaseSingularFacade<TCurrentFacade, TDataItem>
    where TCurrentFacade : BaseFacade
    where TDataItem : IDataItem
  {
    /// <summary>Delete the currently loaded item</summary>
    /// <returns>This facade</returns>
    TCurrentFacade Delete();

    /// <summary>Performs an arbitrary action on the content object.</summary>
    /// <param name="setAction">An action to be performed on the content object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the content object has not been initialized either through constructor, CreateNew() or Set() method.
    /// </exception>
    /// <returns>An instance of the current facade type.</returns>
    TCurrentFacade Do(Action<TDataItem> setAction);

    /// <summary>
    /// Returns the instance of the content type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the content type.</returns>
    TDataItem Get();

    /// <summary>
    /// Sets an instance of the content type to currently loaded fluent API.
    /// </summary>
    /// <param name="item">Item to set as the new internal state of the facade</param>
    /// <returns>An instance of the current facade type.</returns>
    TCurrentFacade Set(TDataItem item);
  }
}
