// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.ContentView.Plugins.MarkupCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telerik.Sitefinity.Web.Configuration.ContentView.Plugins
{
  /// <summary>
  /// Collection of strings that are properly html-encoded and html-decoded. These strings represent
  /// markup - HTML, XML, etc.
  /// </summary>
  public class MarkupCollection : ICollection<string>, IEnumerable<string>, IEnumerable
  {
    /// <summary>Quick implementation for ICollection helper</summary>
    private List<string> data;
    /// <summary>Use the same-named property instead</summary>
    private static IEqualityComparer<string> stringComparer;

    /// <summary>
    /// Get the equality comparer that is used to compare strings internally
    /// </summary>
    public static IEqualityComparer<string> StringEqualityComparer
    {
      get
      {
        if (MarkupCollection.stringComparer == null)
          MarkupCollection.stringComparer = (IEqualityComparer<string>) new MarkupCollection.OrdinalIgnoreCaseStringComparer();
        return MarkupCollection.stringComparer;
      }
    }

    /// <summary>Creates an empty collection</summary>
    public MarkupCollection() => this.data = new List<string>();

    /// <summary>
    /// Creates a new collection that is initialized with an <paramref name="initialCollection" />
    /// </summary>
    /// <param name="initialCollection">Initial contents of the collection</param>
    public MarkupCollection(IEnumerable<string> initialCollection) => this.data = new List<string>(initialCollection);

    /// <summary>
    /// HTML-encodes <paramref name="item" /> and adds it to the collection
    /// </summary>
    /// <param name="item">Markup (XML, HTML, etc.) to add to the collection</param>
    public void Add(string item) => this.data.Add(HttpUtility.HtmlEncode(item));

    /// <summary>Removes all elements from the collection</summary>
    public void Clear() => this.data.Clear();

    /// <summary>
    /// HTML-encodes <paramref name="item" /> and performs ordinal comparison (ignoring case) to chek if there is such an item
    /// </summary>
    /// <param name="item">Markup (XML, HTML, etc.) to check for presence in the collection</param>
    /// <returns>Result of the check whether the collection contains the given <paramref name="item" />.</returns>
    public bool Contains(string item) => this.data.Contains<string>(HttpUtility.HtmlEncode(item), MarkupCollection.StringEqualityComparer);

    /// <summary>
    /// Copies the entire collection to a compatible one-dimensional
    /// array, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">
    /// The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements
    /// copied from the collection. The <see cref="T:System.Array" /> must have
    /// zero-based indexing.
    /// </param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is <c>null</c></exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <list class="nobullet">
    ///     <listItem>
    ///         <paramref name="arrayIndex" /> is equal to or greater than the length of array.
    ///     </listItem>
    ///     <listItem>
    ///         The number of elements in the source collection is greater than
    ///         the available space from <paramref name="arrayIndex" /> to the end of the
    ///         destination array
    ///     </listItem>
    /// </list>
    /// </exception>
    public void CopyTo(string[] array, int arrayIndex) => this.data.CopyTo(array, arrayIndex);

    /// <summary>Number of items actually contained in the collection.</summary>
    public int Count => this.data.Count;

    /// <summary>Returns whether</summary>
    public bool IsReadOnly => false;

    /// <summary>
    ///     <para>Remove an <paramref name="item" /> from the collection.</para>
    ///     <para>
    ///         Html-encodes <paramref name="item" />; uses ordinal comparison and ignores case
    ///         when finding the item to remove.
    ///     </para>
    /// </summary>
    /// <param name="item">Item to remove.</param>
    /// <returns>Whether the removal was successfull.</returns>
    public bool Remove(string item)
    {
      string y = HttpUtility.HtmlEncode(item);
      int index1 = -1;
      for (int index2 = 0; index2 < this.data.Count; ++index2)
      {
        if (MarkupCollection.StringEqualityComparer.Equals(this.data[index2], y))
        {
          index1 = index2;
          break;
        }
      }
      if (index1 == -1)
        return false;
      this.data.RemoveAt(index1);
      return true;
    }

    /// <summary>
    /// Get an enumerator that can iterate through the collection
    /// </summary>
    /// <returns>Enumerator that can iterate through the collection</returns>
    public IEnumerator<string> GetEnumerator()
    {
      foreach (string s in this.data)
        yield return HttpUtility.HtmlDecode(s);
    }

    /// <summary>
    /// Get an enumerator that can iterate through the collection
    /// </summary>
    /// <returns>Enumerator that can iterate through the collection</returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    /// <summary>
    /// Performs ordinal comparison between two strings, ignoring case.
    /// </summary>
    public class OrdinalIgnoreCaseStringComparer : IEqualityComparer<string>
    {
      /// <summary>
      /// Checks <paramref name="x" /> and <paramref name="y" /> for equality
      /// </summary>
      /// <param name="x">String to compare with <paramref name="y" /></param>
      /// <param name="y">String to compare with <paramref name="x" /></param>
      /// <returns>Result of the comparison.Returns <c>true</c> if both strings are equal to <c>null</c>.</returns>
      public bool Equals(string x, string y)
      {
        if (x != null)
          return x.Equals(y, StringComparison.OrdinalIgnoreCase);
        return y == null || y.Equals(x, StringComparison.OrdinalIgnoreCase);
      }

      /// <summary>
      /// Returns <paramref name="obj" />.GetHashCode() if obj is not <c>null</c>, otherwize returns 0.
      /// </summary>
      /// <param name="obj">String to get the hash code from.</param>
      /// <returns>Hash code of <paramref name="obj" /> or 0 if it is <c>null</c>.</returns>
      public int GetHashCode(string obj) => obj == null ? 0 : obj.GetHashCode();
    }
  }
}
