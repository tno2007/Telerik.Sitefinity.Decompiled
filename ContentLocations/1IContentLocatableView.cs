// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.IContentLocatableView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Defines the common information that should be provided by each control
  /// that is able to show content items.
  /// </summary>
  public interface IContentLocatableView
  {
    /// <summary>
    /// Gets the information for all of the content types that a control is able to show.
    /// </summary>
    /// <returns>The locations.</returns>
    IEnumerable<IContentLocationInfo> GetLocations();

    /// <summary>
    /// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
    /// If the value is not set, the settings from SystemConfig -&gt; ContentLocationsSettings -&gt; DisableCanonicalURLs will be used.
    /// </summary>
    /// <value>The disable canonical URLs.</value>
    bool? DisableCanonicalUrlMetaTag { get; set; }
  }
}
