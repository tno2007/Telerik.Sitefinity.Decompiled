// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.PaginationParams
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Used by the <see cref="T:Telerik.Sitefinity.Web.UrlEvaluation.PagerEvaluator" /> to evaluate and build urls.
  /// </summary>
  public class PaginationParams
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UrlEvaluation.PaginationParams" /> class.
    /// </summary>
    public PaginationParams()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UrlEvaluation.PaginationParams" /> class.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    public PaginationParams(int pageNumber) => this.PageNumber = pageNumber;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UrlEvaluation.PaginationParams" /> class.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="itemsPerPage">The items per page.</param>
    public PaginationParams(int pageNumber, int itemsPerPage)
    {
      this.PageNumber = pageNumber;
      this.ItemsPerPage = itemsPerPage;
    }

    /// <summary>Parses the specified data.</summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public static PaginationParams Parse(object data)
    {
      switch (data)
      {
        case int pageNumber:
          return new PaginationParams(pageNumber);
        case int[] _:
          int[] numArray = data as int[];
          if (numArray.Length == 2)
            return new PaginationParams(numArray[0], numArray[1]);
          break;
        case PaginationParams _:
          return data as PaginationParams;
      }
      return (PaginationParams) null;
    }

    /// <summary>Gets or sets the items per page.</summary>
    public int ItemsPerPage { get; set; }

    /// <summary>Gets or sets the page number.</summary>
    public int PageNumber { get; set; }
  }
}
