// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.IFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>Represents a base contract for all facades.</summary>
  public interface IFacade<TFacade>
  {
    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade SaveChanges();

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade CancelChanges();

    /// <summary>Deletes object.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <typeparamref name="TFacade" /> object.</returns>
    TFacade Delete();
  }
}
