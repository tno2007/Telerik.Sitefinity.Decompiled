// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.GlobalResourceReader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Resources;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Provides the base functionality to read data from resource files.
  /// </summary>
  public class GlobalResourceReader : IResourceReader, IEnumerable, IDisposable
  {
    private string classId;

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.GlobalResourceReader" /> with the provided calss ID.
    /// </summary>
    /// <param name="classId">The class ID that will be used to retrieve resources.</param>
    public GlobalResourceReader(string classId) => this.classId = classId;

    /// <summary>
    /// Releases all resources used by the <see cref="T:System.Resources.ResourceReader" />.
    /// </summary>
    public void Close() => this.Dispose();

    /// <summary>
    /// Returns an enumerator for the current <see cref="T:System.Resources.ResourceReader" /> object.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the current  <see cref="T:System.Resources.ResourceReader" /> object.
    /// </returns>
    public IDictionaryEnumerator GetEnumerator() => (IDictionaryEnumerator) new GlobalResourceReader.ResourceEnumerator(this.classId);

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    /// <summary>Defines a method to release allocated resources.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Resources.ResourceReader" /> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">
    /// True to release both managed and unmanaged resources; false to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      int num = disposing ? 1 : 0;
    }

    internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private string classId;
      private int position = -1;
      private PropertyCollection collection;

      public ResourceEnumerator(string classId)
      {
        this.classId = classId;
        this.collection = Resource.PropertyBags[classId];
      }

      public DictionaryEntry Entry
      {
        get
        {
          string key = (string) this.Key;
          return new DictionaryEntry((object) key, (object) Res.Get(this.classId, key, SystemManager.CurrentContext.Culture));
        }
      }

      public object Key => (object) this.collection[this.position].Key;

      public object Value => (object) Res.Get(this.classId, (string) this.Key, SystemManager.CurrentContext.Culture);

      public object Current => (object) this.Entry;

      public bool MoveNext()
      {
        if (this.position >= this.collection.Count - 1)
          return false;
        ++this.position;
        return true;
      }

      public void Reset() => this.position = -1;

      object IEnumerator.Current => this.Current;

      bool IEnumerator.MoveNext() => this.MoveNext();

      void IEnumerator.Reset() => this.Reset();
    }
  }
}
