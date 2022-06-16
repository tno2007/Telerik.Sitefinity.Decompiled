// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.BasicContentLocationFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.ContentLocations.Model;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Defines the content filter that will be applied when showing a content items.
  /// </summary>
  public class BasicContentLocationFilter : IContentLocationGroupFilter, IContentLocationFilter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.BasicContentLocationFilter" /> class.
    /// </summary>
    public BasicContentLocationFilter()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.BasicContentLocationFilter" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public BasicContentLocationFilter(string value) => this.Value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.BasicContentLocationFilter" /> class.
    /// </summary>
    /// <param name="contentFilter">The content filter.</param>
    internal BasicContentLocationFilter(ContentLocationFilterDataItem contentFilter)
    {
      this.Name = contentFilter.Name;
      this.Value = contentFilter.Value;
    }

    /// <inheritdoc />
    public string Value { get; set; }

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public string GetExpression() => this.Value;
  }
}
