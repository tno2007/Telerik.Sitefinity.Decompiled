// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.ICacheContentLink
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.ContentLinks
{
  /// <summary>
  /// Contains minimum content link information that will be cached.
  /// </summary>
  internal interface ICacheContentLink : ICacheableItem
  {
    /// <summary>Gets or sets the id of the parent data item</summary>
    /// <value>The data item id.</value>
    Guid ParentItemId { get; }

    /// <summary>
    /// Gets or sets the name of component property which links to the child item
    /// </summary>
    /// <value>
    /// The name of component property which links to the child item.
    /// </value>
    string ComponentPropertyName { get; }

    /// <summary>
    /// Gets or sets the name of the provider for the child item
    /// </summary>
    /// <value>The name of the provider.</value>
    string ChildItemProviderName { get; }

    /// <summary>Gets or sets the id of the item that is linked</summary>
    /// <value>The linked item id.</value>
    Guid ChildItemId { get; }
  }
}
