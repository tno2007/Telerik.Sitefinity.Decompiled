// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.ReviewsStatisticsGetRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>ReviewsStatisticsGetRequest</c>Represents a filter for retrieving the reviews statistics of <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects.
  /// </summary>
  /// <remarks>
  /// Currently allows filtering based on collection of thread keys.
  /// </remarks>
  public class ReviewsStatisticsGetRequest
  {
    /// <summary>
    /// Gets or sets the keys of <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> objects.
    /// </summary>
    public List<string> ThreadKey { get; set; }
  }
}
