// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Represents string resources for the data UI and messages.
  /// </summary>
  [ObjectInfo("DataResources", ResourceClassId = "DataResources")]
  public sealed class DataResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Data.DataResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public DataResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Data.DataResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public DataResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>phrase: Data resources</summary>
    [ResourceEntry("DataResourcesTitle", Description = "The title of this class.", LastModified = "2009/12/11", Value = "Data")]
    public string DataResourcesTitle => this[nameof (DataResourcesTitle)];

    /// <summary>phrase: Data resources</summary>
    [ResourceEntry("DataResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2009/12/11", Value = "Data")]
    public string DataResourcesTitlePlural => this[nameof (DataResourcesTitlePlural)];

    /// <summary>
    /// phrase: Contains localizable resources for the UI and messages of data related work.
    /// </summary>
    [ResourceEntry("DataResourcesDescription", Description = "The description of this class.", LastModified = "2009/12/11", Value = "Contains localizable resources for the UI and messages of data related work.")]
    public string DataResourcesDescription => this[nameof (DataResourcesDescription)];

    /// <summary>
    /// message: No data provider decorator could be found for the provider '{0}'. The method '{1}' has to be overridden and implemented.
    /// </summary>
    [ResourceEntry("MissingProviderDecorator", Description = "The error message displayed when decorator is missing for a data provider.", LastModified = "2009/12/11", Value = "No data provider decorator could be found for the provider '{0}'. The method '{1}' has to be overridden and implemented.")]
    public string MissingProviderDecorator => this[nameof (MissingProviderDecorator)];

    /// <summary>
    /// Determines and compiles the most appropriate URL for a locatable item for the current request.
    /// </summary>
    [ResourceEntry("UrlResolverDescription", Description = "The description of the URL resolver.", LastModified = "2010/02/02", Value = "Determines and compiles the most appropriate URL for a locatable item for the current request.")]
    public string UrlResolverDescription => this[nameof (UrlResolverDescription)];

    /// <summary>
    /// Retrieves and constructs the author name based on the information provided by the data item.
    /// </summary>
    [ResourceEntry("AuthorResolverDescription", Description = "The description of the Author resolver.", LastModified = "2010/02/02", Value = "Retrieves and constructs the author name based on the information provided by the data item.")]
    public string AuthorResolverDescription => this[nameof (AuthorResolverDescription)];

    /// <summary>
    /// Evaluates a date in the URL that is in the following format: YYYY/MM/DD
    /// </summary>
    [ResourceEntry("DateEvaluatorDescription", Description = "The description of the Date URL Evaluator.", LastModified = "2010/02/02", Value = "Evaluates a date in the URL that is in the following format: YYYY/MM/DD")]
    public string DateEvaluatorDescription => this[nameof (DateEvaluatorDescription)];

    /// <summary>
    /// Evaluates a page number in the URL. The default format is: /Page/4/
    /// </summary>
    [ResourceEntry("PageNumberEvaluatorDescription", Description = "The description of the Page-number URL Evaluator.", LastModified = "2010/02/02", Value = "Evaluates a page number in the URL. The default format is: /Page/4/")]
    public string PageNumberEvaluatorDescription => this[nameof (PageNumberEvaluatorDescription)];

    /// <summary>
    /// Evaluates a page number and department in the URL. The default format is: /-in-Department/Departments/animals/Page/4/
    /// </summary>
    [ResourceEntry("DepartmentPageNumberEvaluatorDescription", Description = "The description of the Page-number and Department URL Evaluator.", LastModified = "2010/02/02", Value = "Evaluates a page number and department in the URL. The default format is: /-in-Department/Departments/animals/Page/4/")]
    public string DepartmentPageNumberEvaluatorDescription => this[nameof (DepartmentPageNumberEvaluatorDescription)];

    /// <summary>
    /// Evaluates the number of items per page. The default format is: /Show/4/
    /// </summary>
    [ResourceEntry("ItemsPerPageEvaluatorDescription", Description = "The description of the Items-per-page URL Evaluator.", LastModified = "2011/06/22", Value = "Evaluates the items per page in the URL. The default format is: /Show/4/")]
    public string ItemsPerPageEvaluatorDescription => this[nameof (ItemsPerPageEvaluatorDescription)];

    /// <summary>
    /// Extracts the username form the URL string. Always the first segment in the URL is assumed.
    /// </summary>
    [ResourceEntry("AuthorEvaluatorDescription", Description = "The description of the Author URL Evaluator.", LastModified = "2010/02/02", Value = "Extracts the username form the URL string. Always the first segment in the URL is assumed.")]
    public string AuthorEvaluatorDescription => this[nameof (AuthorEvaluatorDescription)];

    /// <summary>
    /// Extracts the taxonomy and taxon form the URL string. Assumed that it starts after marked segment at url till the end.
    /// E.g. /-in-/Category/Cat1/Cat2, where '-in-' is marker for start of Taxonomy evaluator, first segment after marker is taxonomy and next segments contain full URL of taxon.
    /// </summary>
    [ResourceEntry("TaxonomyEvaluatorDescription", Description = "The description of the Taxonomy URL Evaluator.", LastModified = "2012/01/05", Value = "Extracts the taxonomy and taxon form the URL string. Assumed that it starts after marked segment at url till the end. E.g. /-in-/Category/Cat1/Cat2, where '-in-' is marker for start of Taxonomy evaluator, first segment after marker is taxonomy and next segments contain full URL of taxon.")]
    public string TaxonomyEvaluatorDescription => this[nameof (TaxonomyEvaluatorDescription)];

    /// <summary>The description of the Pager URL Evaluator.</summary>
    /// <value>Extracts the current page number and the items per page in the url. The format is /page/2/show/3. The parameters can be used separately (E.g /show/2 or /page/3). Does not support the format /show/2/page/3.</value>
    [ResourceEntry("PagerEvaluatorDescription", Description = "The description of the Pager URL Evaluator.", LastModified = "2014/01/14", Value = "Extracts the current page number and the items per page in the url. The format is /page/2/show/3. The parameters can be used separately (E.g /show/2 or /page/3). Does not support the format /show/2/page/3.")]
    public string PagerEvaluatorDescription => this[nameof (PagerEvaluatorDescription)];
  }
}
