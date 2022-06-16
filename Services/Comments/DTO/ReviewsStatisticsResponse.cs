﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.ReviewsStatisticsResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>ReviewsStatisticsResponse</c> Represents the reviews statistics of <see cref="!:IComments" /> objects.
  /// </summary>
  public class ReviewsStatisticsResponse
  {
    /// <summary>
    /// Gets or sets the key of the item associated with <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the count of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the average rating of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects
    /// </summary>
    public Decimal? AverageRating { get; set; }
  }
}
