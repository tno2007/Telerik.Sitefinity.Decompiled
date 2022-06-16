// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.IContentLocationInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Provides all the required information for manipulating content location.
  /// </summary>
  /// <remarks>Used by the content item controls that can show content items.</remarks>
  public interface IContentLocationInfo
  {
    /// <summary>Gets the default priority for this content location.</summary>
    /// <value>The priority.</value>
    ContentLocationPriority Priority { get; set; }

    /// <summary>
    /// Gets the id of the external page that is able to show the content item.
    /// </summary>
    Guid RedirectPageId { get; set; }

    /// <summary>Gets the filters.</summary>
    /// <value>The filters.</value>
    IEnumerable<IContentLocationFilter> Filters { get; }

    /// <summary>Gets the type of the content.</summary>
    /// <value>The type of the content.</value>
    Type ContentType { get; }

    /// <summary>Gets the name of the content provider.</summary>
    /// <value>The name of the provider.</value>
    string ProviderName { get; }
  }
}
