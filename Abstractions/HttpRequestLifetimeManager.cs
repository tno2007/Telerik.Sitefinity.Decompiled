// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.HttpRequestLifetimeManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// A <see cref="T:Telerik.Microsoft.Practices.Unity.LifetimeManager" /> that holds the instances given to it,
  /// keeping one instance per thread.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This LifetimeManager does not dispose the instances it holds.
  /// </para>
  /// </remarks>
  public class HttpRequestLifetimeManager : LifetimeManager
  {
    private readonly Guid key;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.HttpRequestLifetimeManager" /> class.
    /// </summary>
    public HttpRequestLifetimeManager() => this.key = Guid.NewGuid();

    /// <summary>
    /// Retrieve a value from the backing store associated with this Lifetime policy for the
    /// current thread.
    /// </summary>
    /// <returns>the object desired, or <see langword="null" /> if no such object is currently
    /// stored for the current thread.</returns>
    public override object GetValue() => SystemManager.CurrentTransactions?[(object) this.key];

    /// <summary>
    /// Stores the given value into backing store for retrieval later when requested
    /// in the current thread.
    /// </summary>
    /// <param name="newValue">The object being stored.</param>
    public override void SetValue(object newValue)
    {
      ContextTransactions currentTransactions = SystemManager.CurrentTransactions;
      if (currentTransactions == null)
        return;
      currentTransactions[(object) this.key] = newValue;
    }

    /// <summary>Remove the given object from backing store.</summary>
    /// <remarks>Not implemented for this lifetime manager.</remarks>
    public override void RemoveValue()
    {
    }
  }
}
