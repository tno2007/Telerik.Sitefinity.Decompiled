// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.SerializedCollectionContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// This class is an alternative for the generic CollectionContext class. Instead of carrying a collection
  /// of serialized objects, this class carryies objects as a JSON string.
  /// </summary>
  [DataContract]
  public class SerializedCollectionContext
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.SerializedCollectionContext" />.
    /// </summary>
    /// <param name="serializedCollection">
    /// JSON string representing the collection that the instance of this class
    /// ought to carry.
    /// </param>
    public SerializedCollectionContext(string serializedCollection) => this.SerializedCollection = serializedCollection;

    /// <summary>Gets or sets the total count of items.</summary>
    /// <remarks>
    /// Items collection will usually contain only the slice/page of the items. This property
    /// is used to get the total number of the items.
    /// </remarks>
    [DataMember]
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets the JSON representation of the collection of item carried by instance of this class.
    /// </summary>
    [DataMember]
    public string SerializedCollection { get; private set; }
  }
}
