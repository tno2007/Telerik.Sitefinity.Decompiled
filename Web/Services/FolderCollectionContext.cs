// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.FoldersCollectionContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Data context which is used to send collections together with their context (such as total items
  /// count) to the client.
  /// </summary>
  [DataContract]
  [KnownType(typeof (FolderViewModel))]
  [KnownType(typeof (HierarchyLevelState))]
  public class FoldersCollectionContext : CollectionContext<FolderViewModel>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.FoldersCollectionContext" /> class.
    /// </summary>
    /// <param name="items">The items of the collection.</param>
    public FoldersCollectionContext(IEnumerable<FolderViewModel> items)
      : base(items)
    {
    }

    /// <summary>
    /// Gets or sets the HierarchyContext for the current CollectionContext
    /// </summary>
    [DataMember]
    public IDictionary<string, HierarchyLevelState> FoldersContext { get; set; }
  }
}
