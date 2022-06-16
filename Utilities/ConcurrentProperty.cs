// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.ConcurrentProperty`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Sitefinity.Utilities
{
  /// <summary>
  /// Implements a pattern for thread-safe lazy getter.
  /// We are creating this helper class, because the complexity of this scenario is often underestimated,
  /// and therefore it is unusually often implemented with subtle race conditions.
  /// </summary>
  /// <typeparam name="T">Type of the contained property</typeparam>
  public class ConcurrentProperty<T>
  {
    private volatile ConcurrentProperty<T>.Holder value;
    private volatile bool invalidated = true;
    private readonly bool nullIsValid;
    private readonly object sync = new object();
    private readonly Func<T> factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Utilities.ConcurrentProperty`1" /> class.
    /// </summary>
    /// <param name="factory">Method to be used for initialization of the contained property.</param>
    /// <param name="nullIsValid">Whether we should treat null values returned by <paramref name="factory" /> as successful.</param>
    public ConcurrentProperty(Func<T> factory, bool nullIsValid = false)
    {
      this.nullIsValid = nullIsValid;
      this.factory = factory;
    }

    /// <summary>
    /// Gets the contained property.
    /// Will initialize it if needed.
    /// </summary>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2102:PropertyMustNotContainMoreLinesThan", Justification = "Reviewed")]
    public T Value
    {
      get
      {
        ConcurrentProperty<T>.Holder holder = this.value;
        if (holder == null || this.invalidated)
        {
          lock (this.sync)
          {
            holder = this.value;
            if (holder != null)
            {
              if (!this.invalidated)
                goto label_10;
            }
            this.value = (ConcurrentProperty<T>.Holder) null;
            this.invalidated = false;
            T obj = this.factory();
            if ((object) obj == null && !this.nullIsValid)
            {
              this.invalidated = true;
              return obj;
            }
            holder = new ConcurrentProperty<T>.Holder(obj);
            this.value = holder;
          }
        }
label_10:
        return holder.Value;
      }
    }

    /// <summary>
    /// Marks the contained property as not initialized, which will force initializing factory
    /// to be called upon next property get.
    /// </summary>
    public void Reset()
    {
      this.invalidated = true;
      this.value = (ConcurrentProperty<T>.Holder) null;
    }

    private class Holder
    {
      internal Holder(T value) => this.Value = value;

      internal T Value { get; private set; }
    }
  }
}
