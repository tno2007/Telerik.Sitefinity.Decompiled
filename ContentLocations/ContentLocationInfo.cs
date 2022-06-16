// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Contains the required information that a control should give to describe the content location for a given type.
  /// </summary>
  /// <remarks>This is a straight forward implementation of the <see cref="T:Telerik.Sitefinity.ContentLocations.IContentLocationInfo" /> interface.</remarks>
  public class ContentLocationInfo : IContentLocationInfo
  {
    private readonly List<IContentLocationFilter> filters = new List<IContentLocationFilter>();

    /// <inheritdoc />
    public ContentLocationPriority Priority { get; set; }

    /// <inheritdoc />
    public Type ContentType { get; set; }

    /// <inheritdoc />
    public string ProviderName { get; set; }

    /// <inheritdoc />
    public Guid RedirectPageId { get; set; }

    /// <summary>Gets the filters.</summary>
    /// <value>The filters.</value>
    public List<IContentLocationFilter> Filters => this.filters;

    /// <inheritdoc />
    IEnumerable<IContentLocationFilter> IContentLocationInfo.Filters => (IEnumerable<IContentLocationFilter>) this.Filters;
  }
}
