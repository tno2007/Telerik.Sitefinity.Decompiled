// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.TaxonomyEvaluator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Evaluates a url that contains a flat or hierachical taxon:
  /// 1.Hierachical taxonomy has following format: Category/category1/category2, where 'category1/category2' is full path of taxon
  /// 2.Flat taxonom has following format Tag/tag1, where - 'tag1' is the name of the flat taxon.
  /// </summary>
  public class TaxonomyEvaluator : IUrlEvaluator<IEnumerable<TaxonomyFilterInfo>>, IUrlEvaluator
  {
    private const string taxaEvaluatorMarker = "-in-";
    private string regularExpression;
    private Taxonomy taxonomy;
    private PropertyDescriptor fieldPropertyDescriptor;
    private Type contentType;
    private string propertyName;

    /// <summary>Gets or sets the taxonomy.</summary>
    /// <value>The taxonomy.</value>
    protected internal virtual Taxonomy Taxonomy { get; set; }

    /// <summary>Gets or sets the taxon.</summary>
    /// <value>The taxon.</value>
    protected internal virtual Taxon Taxon { get; set; }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public virtual Type ContentType
    {
      get => this.contentType;
      set
      {
        if (!(this.contentType != value))
          return;
        this.contentType = value;
        this.fieldPropertyDescriptor = (PropertyDescriptor) null;
      }
    }

    /// <summary>
    /// Gets or sets the name of the property that contains the taxonomy
    /// </summary>
    public virtual string PropertyName
    {
      get => this.propertyName;
      set
      {
        if (!(this.propertyName != value))
          return;
        this.propertyName = value;
        this.fieldPropertyDescriptor = (PropertyDescriptor) null;
      }
    }

    /// <summary>
    /// Returns the property descriptor of the specified Property name and Content type.
    /// </summary>
    protected internal virtual PropertyDescriptor FieldPropertyDescriptor
    {
      get
      {
        if (this.fieldPropertyDescriptor == null && !string.IsNullOrEmpty(this.PropertyName))
          this.fieldPropertyDescriptor = TypeDescriptor.GetProperties(this.ContentType).Find(this.PropertyName, true);
        return this.fieldPropertyDescriptor;
      }
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of the property the will be filtered.</param>
    /// <param name="values">The values the should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public string Evaluate(
      string url,
      string propertyName,
      string urlKeyPrefix,
      out object[] values)
    {
      return this.Evaluate(url, propertyName, (Type) null, urlKeyPrefix, out values);
    }

    /// <summary>Evaluates the specified URL.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="values">The values.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    public string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      string urlKeyPrefix,
      out object[] values)
    {
      return this.Evaluate(url, propertyName, contentType, UrlEvaluationMode.Default, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">Name of the property that will be filtered.</param>
    /// <param name="contentType">The content type of the filtered items.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <param name="values">The values that should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out object[] values)
    {
      Guid taxonId = Guid.Empty;
      string str = this.Evaluate(url, contentType, urlEvaluationMode, urlKeyPrefix, out taxonId);
      values = new object[1]{ (object) taxonId };
      return str;
    }

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public string BuildUrl(object data, string urlKeyPrefix) => this.BuildUrl((IEnumerable<TaxonomyFilterInfo>) data, urlKeyPrefix);

    /// <summary>
    /// Builds a URL string based on the provided data and url evaluation mode.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns>
    /// The URL build based on the provided data and url evaluation mode.
    /// </returns>
    public string BuildUrl(object data, UrlEvaluationMode urlEvaluationMode, string urlKeyPrefix) => this.BuildUrl((IEnumerable<TaxonomyFilterInfo>) data, urlEvaluationMode, urlKeyPrefix);

    /// <summary>Builds the URL.</summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public string BuildUrl(IEnumerable<TaxonomyFilterInfo> data, string urlKeyPrefix) => this.BuildUrl(data, UrlEvaluationMode.Default, urlKeyPrefix);

    /// <summary>Builds the URL.</summary>
    /// <param name="data">The data.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns></returns>
    public string BuildUrl(
      IEnumerable<TaxonomyFilterInfo> data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      int num = data != null ? data.Count<TaxonomyFilterInfo>() : throw new ArgumentNullException(nameof (data));
      if (num > 1 || num == 1 && data.Single<TaxonomyFilterInfo>().Taxons.Count > 1)
        throw new NotSupportedException("Multiple taxon filtering not supported.");
      if (num != 1 || data.Single<TaxonomyFilterInfo>().Taxons.Count != 1)
        return string.Empty;
      TaxonomyFilterInfo taxonomyFilterInfo = data.Single<TaxonomyFilterInfo>();
      return this.BuildUrl(taxonomyFilterInfo.TaxonomyName, taxonomyFilterInfo.Taxons.Single<TaxonInfo>().Name, "", taxonomyFilterInfo.TaxonType, urlKeyPrefix);
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="taxon">The values the should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public virtual string Evaluate(
      string url,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out Guid taxonId)
    {
      this.ContentType = contentType;
      taxonId = Guid.Empty;
      string taxonomyName = (string) null;
      string taxonName = (string) null;
      this.ParseTaxonomyParams(urlEvaluationMode, url, urlKeyPrefix, out taxonName, out taxonomyName);
      if (string.IsNullOrEmpty(taxonName) || string.IsNullOrEmpty(taxonomyName))
        return string.Empty;
      Taxon taxonByName = this.GetTaxonByName(taxonomyName, taxonName);
      if (taxonByName != null)
        taxonId = taxonByName.Id;
      if (!(this.FieldPropertyDescriptor is TaxonomyPropertyDescriptor propertyDescriptor))
        return string.Empty;
      int num = propertyDescriptor.MetaField.IsSingleTaxon ? 1 : 0;
      StringBuilder stringBuilder = new StringBuilder();
      string format = num != 0 ? "({0}==(@0)) }" : "({0}.Contains((@0)))";
      stringBuilder.AppendFormat(format, (object) this.PropertyName);
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the taxonomy parameters (the name of the taxon and taxonomy) from the supplied url
    /// </summary>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <param name="url">The URL.</param>
    /// <param name="urlKeyPrefix">The URL key prefix.</param>
    /// <param name="taxonName">Name of the taxon.</param>
    /// <param name="taxonomyName">Name of the taxonomy.</param>
    public void ParseTaxonomyParams(
      UrlEvaluationMode urlEvaluationMode,
      string url,
      string urlKeyPrefix,
      out string taxonName,
      out string taxonomyName)
    {
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
        this.ParseTaxonomyParamsFromQueryString(url, urlKeyPrefix, out taxonName, out taxonomyName);
      else
        this.PareseTaxonomyParamsFromUrlPath(url, urlKeyPrefix, out taxonName, out taxonomyName);
    }

    /// <summary>Builds the URL.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="taxon">The taxon.</param>
    /// <param name="propertyName">The name of property.</param>
    /// <returns></returns>
    public virtual string BuildUrl(
      string taxonomy,
      string taxon,
      string propertyName,
      TaxonBuildOptions options,
      string urlKeyPrefix)
    {
      return this.BuildUrl(taxonomy, taxon, propertyName, options, UrlEvaluationMode.Default, urlKeyPrefix);
    }

    /// <summary>Builds the URL.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="taxon">The taxon.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="options">The options.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns></returns>
    public virtual string BuildUrl(
      string taxonomy,
      string taxon,
      string propertyName,
      TaxonBuildOptions options,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      if (!string.IsNullOrEmpty(taxonomy))
        taxonomy = taxonomy.ToLower();
      if (!string.IsNullOrEmpty(propertyName))
        propertyName = propertyName.ToLower();
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
      {
        string name1 = urlKeyPrefix + nameof (taxonomy);
        string name2 = urlKeyPrefix + nameof (propertyName);
        string name3 = urlKeyPrefix + nameof (taxon);
        return QueryStringBuilder.Current.Add(name1, taxonomy, true).Add(name2, propertyName, true).Add(name3, taxon, true).Remove(urlKeyPrefix + "page").ToString();
      }
      StringBuilder stringBuilder = new StringBuilder("/");
      if (!string.IsNullOrEmpty(urlKeyPrefix))
      {
        stringBuilder.Append("!");
        stringBuilder.Append(urlKeyPrefix);
        stringBuilder.Append("/");
      }
      stringBuilder.AppendFormat("{0}{1}/{2}", (object) "-in-", (object) propertyName, (object) taxonomy);
      if (options == TaxonBuildOptions.Flat)
        stringBuilder.Append("/");
      stringBuilder.Append(taxon);
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Initializes this instance with the provided configuration information.
    /// </summary>
    /// <param name="config"></param>
    public virtual void Initialize(NameValueCollection config)
    {
      this.regularExpression = config != null ? config["regularExpression"] : throw new ArgumentNullException(nameof (config));
      if (!string.IsNullOrEmpty(this.regularExpression))
        return;
      this.regularExpression = string.Format("(/?!(?<urlPrefix>\\w+)/)?(?<taxonomyMarker>{0}(?<propertyName>(\\w+)))/(?<taxonomy>([\\w\\-]+))(?<taxon>(/((\\w|[!\\$\\'\\(\\)\\=\\@\\-])+))+)", (object) "-in-");
    }

    /// <summary>Gets a taxon by its name and taxonomy name.</summary>
    /// <param name="taxonomyName">Name of the taxonomy.</param>
    /// <param name="taxonName">Name of the taxon.</param>
    /// <returns></returns>
    public Taxon GetTaxonByName(string taxonomyName, string taxonName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TaxonomyEvaluator.\u003C\u003Ec__DisplayClass28_0 cDisplayClass280 = new TaxonomyEvaluator.\u003C\u003Ec__DisplayClass28_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass280.taxonomyName = taxonomyName;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass280.taxonName = taxonName;
      Taxon taxonByName = (Taxon) null;
      TaxonomyManager manager = TaxonomyManager.GetManager(ManagerBase<TaxonomyDataProvider>.GetDefaultProviderName());
      // ISSUE: reference to a compiler-generated field
      string[] strArray = cDisplayClass280.taxonName.Split(new char[1]
      {
        '/'
      }, StringSplitOptions.RemoveEmptyEntries);
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass280.taxonName != null)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        TaxonomyEvaluator.\u003C\u003Ec__DisplayClass28_1 cDisplayClass281 = new TaxonomyEvaluator.\u003C\u003Ec__DisplayClass28_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass281.CS\u0024\u003C\u003E8__locals1 = cDisplayClass280;
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: field reference
        this.taxonomy = manager.GetTaxonomies<Taxonomy>().Where<Taxonomy>(Expression.Lambda<Func<Taxonomy, bool>>((Expression) Expression.Equal((Expression) Expression.Call(t.Name, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass281, typeof (TaxonomyEvaluator.\u003C\u003Ec__DisplayClass28_1)), FieldInfo.GetFieldFromHandle(__fieldref (TaxonomyEvaluator.\u003C\u003Ec__DisplayClass28_1.CS\u0024\u003C\u003E8__locals1))), FieldInfo.GetFieldFromHandle(__fieldref (TaxonomyEvaluator.\u003C\u003Ec__DisplayClass28_0.taxonomyName)))), parameterExpression)).SingleOrDefault<Taxonomy>();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass281.taxonomyIdGuid = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(manager).ResolveSiteTaxonomyId(this.taxonomy.Id);
        if (this.taxonomy != null)
        {
          if (this.taxonomy is FlatTaxonomy)
          {
            string urlName = strArray[0];
            // ISSUE: reference to a compiler-generated field
            taxonByName = (Taxon) manager.GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.Taxonomy.Id == cDisplayClass281.taxonomyIdGuid && t.UrlName == (Lstring) urlName)).FirstOrDefault<FlatTaxon>();
          }
          else if (this.taxonomy is HierarchicalTaxonomy)
          {
            string urlName = strArray[0];
            // ISSUE: reference to a compiler-generated field
            taxonByName = (Taxon) manager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Taxonomy.Id == cDisplayClass281.taxonomyIdGuid && t.UrlName == (Lstring) urlName)).FirstOrDefault<HierarchicalTaxon>();
            if (taxonByName != null && strArray.Length > 1)
            {
              Taxon taxon = taxonByName;
              for (int index = 1; index < strArray.Length; ++index)
              {
                string parentTaxonName = taxonByName.Name;
                taxonName = strArray[index];
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                taxonByName = (Taxon) manager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent.Name == parentTaxonName && t.UrlName == (Lstring) cDisplayClass281.CS\u0024\u003C\u003E8__locals1.taxonName)).FirstOrDefault<HierarchicalTaxon>();
                if (taxonByName != null)
                {
                  taxon = taxonByName;
                }
                else
                {
                  int result = 0;
                  if (index == strArray.Length - 1 && int.TryParse(taxonName, out result) && result > 0)
                    taxonByName = taxon;
                }
              }
            }
          }
        }
      }
      return taxonByName;
    }

    private void PareseTaxonomyParamsFromUrlPath(
      string url,
      string urlKeyPrefix,
      out string taxonName,
      out string taxonomyName)
    {
      taxonomyName = (string) null;
      taxonName = (string) null;
      if (string.IsNullOrWhiteSpace(url))
        return;
      MatchCollection matchCollection = Regex.Matches(url, this.regularExpression);
      Match match = (Match) null;
      if (matchCollection.Count == 1)
        match = matchCollection[0];
      if (matchCollection.Count == 0 || !(match.Groups["urlPrefix"].Value == urlKeyPrefix) && (!string.IsNullOrEmpty(match.Groups["urlPrefix"].Value) || !string.IsNullOrEmpty(urlKeyPrefix)))
        return;
      this.PropertyName = match.Groups["propertyName"].Value;
      taxonomyName = match.Groups["taxonomy"].Value;
      taxonName = match.Groups["taxon"].Value;
    }

    private void ParseTaxonomyParamsFromQueryString(
      string url,
      string urlKeyPrefix,
      out string taxonName,
      out string taxonomyName)
    {
      string parameterName1 = urlKeyPrefix + "taxonomy";
      string parameterName2 = urlKeyPrefix + "propertyName";
      string parameterName3 = urlKeyPrefix + "taxon";
      this.PropertyName = SystemManager.CurrentHttpContext.Request.QueryStringGet(parameterName2);
      taxonomyName = SystemManager.CurrentHttpContext.Request.QueryStringGet(parameterName1);
      taxonName = SystemManager.CurrentHttpContext.Request.QueryStringGet(parameterName3);
    }
  }
}
