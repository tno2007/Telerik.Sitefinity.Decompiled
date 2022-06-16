// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.Common.TaxonServiceHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Configuration;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Web.Services.Common
{
  public static class TaxonServiceHelper
  {
    internal static string GetSynonyms(Taxon taxon)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<Synonym> synonyms = TaxonServiceHelper.FilterSynonymsForCurrentContext((IEnumerable<Synonym>) taxon.Synonyms);
      string str = " ";
      string synonymsSeparator = Config.Get<TaxonomyConfig>().TaxonSynonymsSeparator;
      if (!string.IsNullOrEmpty(synonymsSeparator))
        str = synonymsSeparator;
      foreach (Synonym synonym in synonyms)
        stringBuilder.Append(synonym.Value).Append(str);
      return stringBuilder.ToString();
    }

    internal static void SetTaxonSynonyms(
      Taxon taxon,
      IWcfTaxon wcfTaxon,
      TaxonomyManager taxonomyManager)
    {
      if (wcfTaxon.Synonyms == null)
        wcfTaxon.Synonyms = string.Empty;
      string synonymsSeparator = Config.Get<TaxonomyConfig>().TaxonSynonymsSeparator;
      string str1 = string.IsNullOrEmpty(synonymsSeparator) ? " " : synonymsSeparator;
      List<string> list = ((IEnumerable<string>) wcfTaxon.Synonyms.Split(new string[1]
      {
        str1
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (p => p.Trim())).ToList<string>();
      IEnumerable<Synonym> source = TaxonServiceHelper.FilterSynonymsForCurrentContext((IEnumerable<Synonym>) taxon.Synonyms);
      for (int index = 0; index < source.Count<Synonym>(); ++index)
      {
        Synonym synonym1 = source.ElementAt<Synonym>(index);
        if (synonym1.Culture == 0)
        {
          foreach (CultureInfo culture in ((IEnumerable<CultureInfo>) taxon.GetAvailableCultures()).Where<CultureInfo>((Func<CultureInfo, bool>) (x => !object.Equals((object) x, (object) SystemManager.CurrentContext.Culture) && !object.Equals((object) x, (object) CultureInfo.InvariantCulture))).ToList<CultureInfo>())
          {
            Synonym synonym2 = taxonomyManager.CreateSynonym();
            synonym2.Value = synonym1.Value;
            synonym2.Culture = AppSettings.CurrentSettings.GetCultureLcid(culture);
            taxon.Synonyms.Add(synonym2);
          }
          synonym1.Culture = AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage);
        }
        if (!list.Contains(synonym1.Value))
        {
          taxon.Synonyms.Remove(synonym1);
          taxonomyManager.Delete(synonym1);
          --index;
        }
        else
          list.Remove(synonym1.Value);
      }
      foreach (string str2 in list)
      {
        Synonym synonym = taxonomyManager.CreateSynonym();
        synonym.Culture = AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
        synonym.Value = str2;
        taxon.Synonyms.Add(synonym);
      }
    }

    internal static IEnumerable<Synonym> FilterSynonymsForCurrentContext(
      IEnumerable<Synonym> synonyms)
    {
      int lcid = AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
      synonyms = synonyms.Where<Synonym>((Func<Synonym, bool>) (x => x.Culture == lcid || x.Culture == 0));
      return synonyms;
    }

    internal static IWcfTaxon CreateWcfTaxonObject(
      Taxon taxon,
      TaxonomyManager manager,
      WcTaxonDataFlags flags,
      Type dataItemType)
    {
      string[] array = ((IEnumerable<CultureInfo>) taxon.GetAvailableCultures((CultureInfo) null)).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)).ToArray<string>();
      IWcfTaxon wcfTaxonObject;
      if (taxon is HierarchicalTaxon)
      {
        HierarchicalTaxon hierarchicalTaxon1 = (HierarchicalTaxon) taxon;
        WcfHierarchicalTaxon hierarchicalTaxon2 = new WcfHierarchicalTaxon()
        {
          Id = hierarchicalTaxon1.Id,
          Description = (string) hierarchicalTaxon1.Description,
          Name = hierarchicalTaxon1.Name,
          UrlName = (string) hierarchicalTaxon1.UrlName,
          Title = (string) hierarchicalTaxon1.Title,
          Ordinal = hierarchicalTaxon1.Ordinal,
          UrlPath = hierarchicalTaxon1.Id.ToString(),
          AvailableLanguages = array,
          LastModified = hierarchicalTaxon1.LastModified
        };
        if (TaxonServiceHelper.UseTaxonDataFlag(flags, WcTaxonDataFlags.SetSynonyms))
          hierarchicalTaxon2.Synonyms = TaxonServiceHelper.GetSynonyms((Taxon) hierarchicalTaxon1);
        if (TaxonServiceHelper.UseTaxonDataFlag(flags, WcTaxonDataFlags.SetTitlePath))
        {
          HierarchicalTaxon parent = hierarchicalTaxon1.Parent;
          List<string> stringList = new List<string>();
          for (; parent != null; parent = parent.Parent)
          {
            string str = parent.Title.ToString();
            if (!str.IsNullOrEmpty())
              stringList.Add(str);
          }
          stringList.Reverse();
          string str1 = string.Join(" > ", stringList.ToArray());
          hierarchicalTaxon2.TitlesPath = str1;
        }
        wcfTaxonObject = (IWcfTaxon) hierarchicalTaxon2;
      }
      else
      {
        FlatTaxon flatTaxon = (FlatTaxon) taxon;
        WcfFlatTaxon wcfFlatTaxon = new WcfFlatTaxon()
        {
          Id = flatTaxon.Id,
          TaxonomyId = flatTaxon.Taxonomy.Id,
          TaxonomyName = TaxonServiceHelper.GetTaxonomyName((Taxon) flatTaxon),
          Description = (string) flatTaxon.Description,
          Name = flatTaxon.Name,
          UrlName = (string) flatTaxon.UrlName,
          Title = (string) flatTaxon.Title,
          UrlPath = flatTaxon.Id.ToString(),
          AvailableLanguages = array,
          LastModified = flatTaxon.LastModified
        };
        if (TaxonServiceHelper.UseTaxonDataFlag(flags, WcTaxonDataFlags.SetSynonyms))
          wcfFlatTaxon.Synonyms = TaxonServiceHelper.GetSynonyms((Taxon) flatTaxon);
        wcfTaxonObject = (IWcfTaxon) wcfFlatTaxon;
      }
      Guid guid = taxon.Taxonomy.IsRootTaxonomy() ? taxon.Taxonomy.Id : taxon.Taxonomy.RootTaxonomyId.Value;
      wcfTaxonObject.RootTaxonomyId = guid;
      wcfTaxonObject.Attributes = new DictionaryObjectViewModel(taxon.Attributes);
      if (TaxonServiceHelper.UseTaxonDataFlag(flags, WcTaxonDataFlags.SetMarkedItemsCount))
        wcfTaxonObject.ItemsCount = !(dataItemType != (Type) null) ? manager.GetTaxonItemsCount(wcfTaxonObject.Id, ContentLifecycleStatus.Master) : manager.GetTaxonItemsCount(dataItemType, wcfTaxonObject.Id, ContentLifecycleStatus.Master);
      wcfTaxonObject.AdditionalStatus = StatusResolver.Resolve(taxon.GetType(), taxon.GetProviderName(), taxon.Id);
      return wcfTaxonObject;
    }

    internal static string GetTaxonomyName(Taxon flatTaxon) => flatTaxon.Taxonomy.RootTaxonomy == null ? flatTaxon.Taxonomy.Name : flatTaxon.Taxonomy.RootTaxonomy.Name;

    internal static IWcfTaxon CreateFullWcfTaxonObject(
      Taxon taxon,
      TaxonomyManager manager,
      Type dataItemType)
    {
      IWcfTaxon wcfTaxonObject = TaxonServiceHelper.CreateWcfTaxonObject(taxon, manager, WcTaxonDataFlags.All, dataItemType);
      if (!(wcfTaxonObject is WcfHierarchicalTaxon hierarchicalTaxon1))
        return wcfTaxonObject;
      HierarchicalTaxon hierarchicalTaxon2 = (HierarchicalTaxon) taxon;
      if (hierarchicalTaxon2.Parent != null)
      {
        hierarchicalTaxon1.ParentTaxonId = new Guid?(hierarchicalTaxon2.Parent.Id);
        hierarchicalTaxon1.ParentTaxonTitle = (string) hierarchicalTaxon2.Parent.Title;
      }
      if (hierarchicalTaxon2.Taxonomy == null)
        return wcfTaxonObject;
      hierarchicalTaxon1.TaxonomyId = new Guid?(hierarchicalTaxon2.Taxonomy.Id);
      hierarchicalTaxon1.TaxonomyName = TaxonServiceHelper.GetTaxonomyName(taxon);
      return wcfTaxonObject;
    }

    internal static WcTaxonDataFlags GetFlags(string mode)
    {
      if (string.IsNullOrEmpty(mode))
        return WcTaxonDataFlags.All;
      WcTaxonDataFlags flags = WcTaxonDataFlags.None;
      if (!(mode == "TitlePath"))
      {
        if (!(mode == "AutoComplete"))
        {
          if (mode == "Simple")
            ;
        }
        else
          flags = WcTaxonDataFlags.AutoComplete;
      }
      else
        flags = WcTaxonDataFlags.SetMarkedItemsCount | WcTaxonDataFlags.SetTitlePath;
      return flags;
    }

    internal static SiteContextMode GetSiteContextMode(string siteContextMode)
    {
      if (siteContextMode == "currentSiteContext")
        return SiteContextMode.CurrentSiteContext;
      if (siteContextMode == "skipSiteContext")
        return SiteContextMode.SkipSiteContext;
      return siteContextMode == "allSitesContext" ? SiteContextMode.AllSitesContext : SiteContextMode.CurrentSiteContext;
    }

    /// <summary>
    /// Gets the taxa in the specified site context mode belonging to the specified taxonomy.
    /// </summary>
    /// <remarks>If the specified mode is 'current site context', then all taxa accessible for the current site for the specified taxonomy  are returned.
    /// If the specified mode is 'skip site context' then the taxa from the specified taxonomy are returned.
    /// If the specified mode is 'all sites context' then all the taxa belonging to the root or any of the split taxonomies are returned.
    /// </remarks>
    /// <typeparam name="TTaxonomy">The type of the T taxonomy.</typeparam>
    /// <typeparam name="TTaxon">The type of the T taxon.</typeparam>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    /// <param name="siteContext">The site context.</param>
    /// <param name="taxonomyId">The taxonomy id.</param>
    /// <returns>Query of taxa which which belong to the specified taxonomy.</returns>
    internal static IQueryable<TTaxon> GetSiteContextTaxa<TTaxon>(
      TaxonomyManager taxonomyManager,
      SiteContextMode siteContext,
      Guid taxonomyId)
      where TTaxon : Taxon
    {
      IQueryable<TTaxon> source = taxonomyManager.GetTaxa<TTaxon>().Include<TTaxon>((Expression<Func<TTaxon, object>>) (x => x.Taxonomy)).Include<TTaxon>((Expression<Func<TTaxon, object>>) (x => x.Attributes));
      IQueryable<TTaxon> siteContextTaxa;
      switch (siteContext)
      {
        case SiteContextMode.CurrentSiteContext:
          taxonomyId = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(taxonomyManager).ResolveSiteTaxonomyId(taxonomyId);
          goto default;
        case SiteContextMode.AllSitesContext:
          Guid[] taxonomyNodes = taxonomyManager.GetTaxonomyNodes(taxonomyId);
          siteContextTaxa = source.Where<TTaxon>((Expression<Func<TTaxon, bool>>) (t => taxonomyNodes.Contains<Guid>(t.Taxonomy.Id)));
          break;
        default:
          siteContextTaxa = source.Where<TTaxon>((Expression<Func<TTaxon, bool>>) (t => t.Taxonomy.Id == taxonomyId));
          break;
      }
      return siteContextTaxa;
    }

    private static int CalculateTaxonsLevel(HierarchicalTaxon taxon, int level) => taxon.Parent == null ? level : TaxonServiceHelper.CalculateTaxonsLevel(taxon.Parent, level + 1);

    internal static bool UseTaxonDataFlag(WcTaxonDataFlags flags, WcTaxonDataFlags flagToCheck) => (flags & flagToCheck) == flagToCheck;
  }
}
