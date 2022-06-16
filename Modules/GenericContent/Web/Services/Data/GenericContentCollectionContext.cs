// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data.GenericContentCollectionContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data
{
  /// <summary>
  /// A specialized CollectionContext for the GenericContent module
  /// </summary>
  [DataContract]
  public class GenericContentCollectionContext : CollectionContext<WebContentItem>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data.GenericContentCollectionContext" /> class.
    /// </summary>
    /// <param name="items">The items of the collection.</param>
    public GenericContentCollectionContext(IEnumerable<WebContentItem> items)
      : base(items)
    {
    }
  }
}
