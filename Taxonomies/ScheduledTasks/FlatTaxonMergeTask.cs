// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ScheduledTasks.FlatTaxonMergeTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Configuration;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.Services;
using Telerik.Sitefinity.Taxonomies.Web.Services.Common;

namespace Telerik.Sitefinity.Taxonomies.ScheduledTasks
{
  /// <summary>
  /// Background task for moving items to different classifications (tags and categories)
  /// </summary>
  internal class FlatTaxonMergeTask : TaxonTask
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.FlatTaxonMergeTask" /> class. Asynchronous task for moving items labeled with a taxon, to another taxon.
    /// </summary>
    public FlatTaxonMergeTask()
      : base((ITaxonomy) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.FlatTaxonMergeTask" /> class. Asynchronous task for moving items labeled with a taxon, to another taxon.
    /// </summary>
    /// <param name="taxonomy">Parent taxonomy of taxa that are being merged</param>
    public FlatTaxonMergeTask(ITaxonomy taxonomy)
      : base(taxonomy)
    {
    }

    internal override void BeforeTaxonItemsUpdate() => this.SourceTaxa = this.SourceTaxa.Where<TaxonTaskStateItem>((Func<TaxonTaskStateItem, bool>) (p => p.Id != this.TargetTaxon.Id)).ToList<TaxonTaskStateItem>();

    internal override void AfterTaxonItemsUpdate()
    {
      TaxonomyManager manager = TaxonomyManager.GetManager();
      Taxon taxon1 = manager.GetTaxon(this.TargetTaxon.Id) as Taxon;
      foreach (TaxonTaskStateItem sourceTaxon in this.SourceTaxa)
      {
        try
        {
          if (manager.GetTaxon(sourceTaxon.Id) is Taxon taxon2)
          {
            if (SystemManager.CurrentContext.AppSettings.Multilingual)
            {
              foreach (CultureInfo availableLanguage in taxon1.Title.GetAvailableLanguages())
              {
                if (taxon2.Title.GetString(availableLanguage, false) != null)
                {
                  using (new CultureRegion(availableLanguage))
                    this.AddSynonyms(taxon1, taxon2, manager);
                }
              }
            }
            else
              this.AddSynonyms(taxon1, taxon2, manager);
            manager.Delete((ITaxon) taxon2);
          }
        }
        catch (NoSuchObjectException ex)
        {
        }
      }
      manager.SaveChanges();
    }

    private void AddSynonyms(Taxon targetTaxon, Taxon sourceTaxon, TaxonomyManager taxonomyManager)
    {
      string synonyms1 = TaxonServiceHelper.GetSynonyms(targetTaxon);
      string synonyms2 = TaxonServiceHelper.GetSynonyms(sourceTaxon);
      string synonymsSeparator = Config.Get<TaxonomyConfig>().TaxonSynonymsSeparator;
      string separator1 = string.IsNullOrEmpty(synonymsSeparator) ? " " : synonymsSeparator;
      string[] separator2 = new string[1]{ separator1 };
      IEnumerable<string> values = ((IEnumerable<string>) synonyms1.Split(separator2, StringSplitOptions.RemoveEmptyEntries)).ToList<string>().Union<string>((IEnumerable<string>) ((IEnumerable<string>) synonyms2.Split(separator2, StringSplitOptions.RemoveEmptyEntries)).ToList<string>()).Union<string>((IEnumerable<string>) new List<string>()
      {
        (string) sourceTaxon.Title
      });
      TaxonServiceHelper.SetTaxonSynonyms(targetTaxon, (IWcfTaxon) new WcfFlatTaxon()
      {
        Id = targetTaxon.Id,
        Synonyms = string.Join(separator1, values)
      }, taxonomyManager);
    }
  }
}
