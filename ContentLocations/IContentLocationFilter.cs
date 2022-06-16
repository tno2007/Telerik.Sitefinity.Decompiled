// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.IContentLocationFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Represents the filter applied to the content location.
  /// </summary>
  public interface IContentLocationFilter
  {
    /// <summary>Gets or sets the value of the filter.</summary>
    /// <value>The value.</value>
    string Value { get; set; }

    /// <summary>Gets or sets the filter name.</summary>
    /// <value>The filter name.</value>
    string Name { get; set; }
  }
}
