// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.TaxonomiesImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.SiteSync
{
  internal class TaxonomiesImporter : SiteSyncImporter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.TaxonomiesImporter" /> class.
    /// </summary>
    public TaxonomiesImporter()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.TaxonomiesImporter" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public TaxonomiesImporter(string registrationPrefix) => this.RegistrationPrefix = registrationPrefix;

    internal void ImportItemInternal(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction)
    {
      this.ImportItem(transactionName, itemType, itemId, item, provider, importTransaction);
    }

    protected override void ValidateDataItemConstraints(
      IDataItem dataItem,
      IManager manager,
      ISiteSyncImportTransaction importTransaction)
    {
      if (!(manager is TaxonomyManager taxonomyManager))
        return;
      switch (dataItem)
      {
        case FlatTaxon _:
          taxonomyManager.ValidateConstraints((FlatTaxon) dataItem);
          break;
        case HierarchicalTaxon _:
          taxonomyManager.ValidateConstraints((HierarchicalTaxon) dataItem);
          break;
        case Taxonomy _:
          taxonomyManager.ValidateConstraints((Taxonomy) dataItem);
          break;
      }
    }

    protected override void SetAdditionalValues(
      IDataItem dataItem,
      string provider,
      WrapperObject wrapperObject,
      FluentSitefinity fluent,
      ISiteSyncImportTransaction transaction)
    {
      bool flag = transaction.Headers.ContainsKey("MultisiteMigrationTarget");
      string str1 = flag ? transaction.Headers["MultisiteMigrationSource"] : string.Empty;
      string transactionName = fluent.AppSettings.TransactionName;
      TaxonomyManager manager = TaxonomyManager.GetManager(provider, transactionName);
      Taxon taxon = dataItem as Taxon;
      if (taxon != null)
      {
        Guid property = wrapperObject.GetProperty<Guid>("TaxonomyId");
        Type itemType1 = TypeResolutionService.ResolveType(wrapperObject.GetProperty<string>("TaxonomyType"));
        object propertyOrNull = wrapperObject.GetPropertyOrNull("ParentId");
        if (propertyOrNull != null && (Guid) propertyOrNull != Guid.Empty)
        {
          Type itemType2 = TypeResolutionService.ResolveType(wrapperObject.GetProperty<string>("ParentType"));
          fluent.DataItemFacade(itemType2, (Guid) propertyOrNull, provider).DoAs<Taxon>((Action<Taxon>) (tax => ((Taxon) dataItem).Parent = tax));
        }
        fluent.DataItemFacade(itemType1, property, provider).DoAs<Taxonomy>((Action<Taxonomy>) (tax =>
        {
          tax.Taxa.Add((Taxon) dataItem);
          ((Taxon) dataItem).Taxonomy = tax;
        }));
        if (flag && taxon.ParentId == Guid.Empty)
        {
          string urlName = taxon.UrlName.ToString();
          if (manager.GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (t => t.Id != taxon.Id && t.Taxonomy.Id == taxon.Taxonomy.Id && t.ParentId == Guid.Empty)).Any<Taxon>((Expression<Func<Taxon, bool>>) (s => s.UrlName == (Lstring) urlName)))
          {
            string str2 = str1 + " " + (string) taxon.Title;
            string url = CommonMethods.TitleToUrl(str1 + " " + urlName);
            taxon.Title = (Lstring) str2;
            taxon.UrlName = (Lstring) url;
            taxon.Name = url;
          }
        }
      }
      Taxonomy taxonomy = dataItem as Taxonomy;
      if (taxonomy != null)
      {
        if (flag)
        {
          if (manager.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Id != taxonomy.Id && t.Name == taxonomy.Name)).Any<Taxonomy>())
          {
            string str3 = (str1 + (string) taxonomy.Title).Replace(" ", string.Empty);
            string title = str1 + taxonomy.Name;
            taxonomy.Name = CommonMethods.TitleToUrl(title);
            taxonomy.Title = (Lstring) str3;
          }
        }
        else
        {
          List<Guid> propertyOrDefault = wrapperObject.GetPropertyOrDefault<List<Guid>>("SiteIds");
          this.LinkItemToSites((IDataItem) taxonomy, (IList<Guid>) propertyOrDefault, fluent, typeof (Taxonomy).FullName);
        }
      }
      base.SetAdditionalValues(dataItem, provider, wrapperObject, fluent, transaction);
    }

    protected override void RemoveDataItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      string provider,
      string language)
    {
      TaxonomyManager manager = TaxonomyManager.GetManager(provider, transactionName);
      object itemOrDefault = manager.GetItemOrDefault(itemType, itemId);
      if (itemOrDefault == null)
        return;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      if (typeof (Taxonomy).IsAssignableFrom(itemType))
        manager.Delete((ITaxonomy) itemOrDefault, culture);
      if (!typeof (Taxon).IsAssignableFrom(itemType))
        return;
      manager.Delete((ITaxon) itemOrDefault, culture);
    }
  }
}
