// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.CollectionContext`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Data context which is used to send collections together with their context (such as total items
  /// count) to the client.
  /// </summary>
  [DataContract]
  [KnownType(typeof (FoldersCollectionContext))]
  [KnownType(typeof (TaxonomyCollectionContext))]
  public class CollectionContext<T>
  {
    private IEnumerable<T> items;
    private static CollectionContext<T> empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:CollectionContext" /> class.
    /// </summary>
    public CollectionContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> class.
    /// </summary>
    /// <param name="items">The items of the collection.</param>
    public CollectionContext(IEnumerable<T> items) => this.items = items;

    /// <summary>Gets or sets the total count of items.</summary>
    /// <remarks>
    /// Items collection will usually contain only the slice/page of the items. This property
    /// is used to get the total number of the items.
    /// </remarks>
    [DataMember]
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets/Sets if a collection contains generic items ex. <value>string</value>
    /// </summary>
    [DataMember]
    public bool IsGeneric { get; set; }

    /// <summary>
    /// Gets/Sets additional context items that will be displayed in the UI.
    /// </summary>
    [DataMember]
    public IDictionary<string, string> Context { get; set; }

    /// <summary>Gets the items of the collection.</summary>
    /// <value>The items.</value>
    [DataMember]
    public IEnumerable<T> Items
    {
      get
      {
        if (this.items == null)
          this.items = (IEnumerable<T>) new Collection<T>();
        return this.items;
      }
      set => this.items = value;
    }

    /// <summary>Returns an empty collection with no items</summary>
    public static CollectionContext<T> Empty
    {
      get
      {
        if (CollectionContext<T>.empty == null)
          CollectionContext<T>.empty = new CollectionContext<T>((IEnumerable<T>) new T[0])
          {
            TotalCount = 0
          };
        return CollectionContext<T>.empty;
      }
    }
  }
}
