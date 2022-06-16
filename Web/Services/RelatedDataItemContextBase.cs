// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.RelatedDataItemContextBase`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Services.RelatedData.Messages;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Base class that provides an item context with information about changed related data
  /// </summary>
  /// <typeparam name="T"></typeparam>
  [DataContract]
  public class RelatedDataItemContextBase<T> : ItemContext<T>
  {
    /// <summary>Contains the changes made to all related data fields</summary>
    [DataMember]
    public ContentLinkChange[] ChangedRelatedData { get; set; }

    /// <summary>
    /// Gets or sets the id of the item. It is used as a parent id when the item will be duplicated.
    /// </summary>
    /// <value>The item id.</value>
    [DataMember]
    public Guid ItemId { get; set; }
  }
}
