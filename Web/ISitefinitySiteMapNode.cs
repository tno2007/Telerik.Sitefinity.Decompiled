// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ISitefinitySiteMapNode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Provides extended properties for Sitefinity SiteMapNodes.
  /// </summary>
  public interface ISitefinitySiteMapNode
  {
    /// <summary>Gets the pageId of the node (Page / Taxon).</summary>
    /// <value>The pageId.</value>
    Guid Id { get; }

    /// <summary>
    /// Gets a name value collection of additional attributes.
    /// </summary>
    /// <value>The attributes.</value>
    NameValueCollection Attributes { get; }

    /// <summary>Gets or sets the ordinal number of the node.</summary>
    /// <value>The ordinal.</value>
    float Ordinal { get; set; }
  }
}
