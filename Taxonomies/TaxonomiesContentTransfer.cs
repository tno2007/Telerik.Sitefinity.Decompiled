// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomiesContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// Implements functionality for converting items from news content in transferable format.
  /// </summary>
  internal class TaxonomiesContentTransfer : StaticContentTransfer
  {
    private const string AreaName = "Classifications";
    private IEnumerable<ExportType> supportedTypes;
    private readonly Guid[] hierarchicalTaxonomiesToExclude = new Guid[1]
    {
      SiteInitializer.PageTemplatesTaxonomyId
    };
    private readonly Lazy<TaxonomiesImporter> itemsImporter = new Lazy<TaxonomiesImporter>((Func<TaxonomiesImporter>) (() =>
    {
      return new TaxonomiesImporter("Export/Import")
      {
        Serializer = (ISiteSyncSerializer) new SiteSyncSerializer("Export/Import")
      };
    }));

    /// <inheritdoc />
    public override IEnumerable<IQueryable<object>> GetItemsQueries(
      ExportParams parameters)
    {
      TaxonomiesContentTransfer taxonomiesContentTransfer = this;
      TaxonomyManager taxonomiesManager = TaxonomyManager.GetManager(parameters.ProviderName);
      foreach (FlatTaxonomy taxonomy1 in (IEnumerable<FlatTaxonomy>) taxonomiesManager.GetTaxonomies<FlatTaxonomy>())
      {
        FlatTaxonomy taxonomy = taxonomy1;
        yield return (IQueryable<object>) taxonomiesManager.GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.Taxonomy.Id == taxonomy.Id));
      }
      IQueryable<HierarchicalTaxonomy> taxonomies = taxonomiesManager.GetTaxonomies<HierarchicalTaxonomy>();
      Expression<Func<HierarchicalTaxonomy, bool>> predicate1 = (Expression<Func<HierarchicalTaxonomy, bool>>) (t => !taxonomiesContentTransfer.hierarchicalTaxonomiesToExclude.Contains<Guid>(t.Id));
      foreach (HierarchicalTaxonomy hierarchicalTaxonomy in (IEnumerable<HierarchicalTaxonomy>) taxonomies.Where<HierarchicalTaxonomy>(predicate1))
      {
        HierarchicalTaxonomy taxonomy = hierarchicalTaxonomy;
        Queue<IQueryable<HierarchicalTaxon>> queue = new Queue<IQueryable<HierarchicalTaxon>>((IEnumerable<IQueryable<HierarchicalTaxon>>) new IQueryable<HierarchicalTaxon>[1]
        {
          taxonomiesManager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Taxonomy.Id == taxonomy.Id && t.Parent == default (object)))
        });
        while (queue.Any<IQueryable<HierarchicalTaxon>>())
        {
          IQueryable<HierarchicalTaxon> next = queue.Dequeue();
          yield return (IQueryable<object>) next;
          IQueryable<HierarchicalTaxon> source = next;
          Expression<Func<HierarchicalTaxon, Guid>> selector = (Expression<Func<HierarchicalTaxon, Guid>>) (pn => pn.Id);
          foreach (Guid guid in (IEnumerable<Guid>) source.Select<HierarchicalTaxon, Guid>(selector))
          {
            Guid parentId = guid;
            IQueryable<HierarchicalTaxon> taxa = taxonomiesManager.GetTaxa<HierarchicalTaxon>();
            Expression<Func<HierarchicalTaxon, bool>> predicate2 = (Expression<Func<HierarchicalTaxon, bool>>) (t => t.Taxonomy.Id == taxonomy.Id && t.Parent.Id == parentId);
            queue.Enqueue(taxa.Where<HierarchicalTaxon>(predicate2));
          }
          next = (IQueryable<HierarchicalTaxon>) null;
        }
        queue = (Queue<IQueryable<HierarchicalTaxon>>) null;
      }
    }

    /// <inheritdoc />
    public override void Count(Stream fileStream, ScanOperation operation)
    {
      int num = 0;
      if (fileStream.Length > 0L)
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(fileStream);
        XmlElement documentElement = xmlDocument.DocumentElement;
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
        nsmgr.AddNamespace("cmisra", "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
        nsmgr.AddNamespace("cmis", "http://docs.oasis-open.org/ns/cmis/core/200908/");
        foreach (object obj in new List<string>()
        {
          typeof (FlatTaxon).FullName,
          typeof (HierarchicalTaxon).FullName
        })
        {
          string xpath = string.Format("./cmisra:object/cmis:properties[./cmis:propertyString[./cmis:value/text() = '{0}' and @propertyDefinitionId = 'cmis:objectTypeId']]/cmis:propertyId[@propertyDefinitionId='sf:Id']/cmis:value/text()", obj);
          IEnumerable<Guid> source = documentElement.SelectNodes(xpath, nsmgr).Cast<XmlNode>().Where<XmlNode>((Func<XmlNode, bool>) (i => Guid.TryParse(i.Value, out Guid _))).Select<XmlNode, Guid>((Func<XmlNode, Guid>) (i => Guid.Parse(i.Value)));
          num += source.Distinct<Guid>().Count<Guid>();
        }
        if (num > 0)
        {
          AddOnEntry addOnEntry = new AddOnEntry()
          {
            DisplayName = "Classification items",
            AddonEntryType = AddOnEntryType.Content,
            Count = num
          };
          operation.AddOnInfo.Entries.Add(addOnEntry);
        }
      }
      this.AddTaxonomyEntries(operation);
    }

    /// <inheritdoc />
    public override string Area => "Classifications";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("Classifications", typeof (Taxonomy).FullName);
          exportTypeList.Add(exportType);
          this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
        }
        return this.supportedTypes;
      }
    }

    /// <inheritdoc />
    public override SiteSyncImporter ItemsImporter => (SiteSyncImporter) this.itemsImporter.Value;

    /// <inheritdoc />
    public override void Delete(string sourceName) => this.DeleteImportedData(sourceName, typeof (TaxonomyManager), (IList<Type>) new List<Type>()
    {
      typeof (FlatTaxon),
      typeof (HierarchicalTaxon)
    });

    /// <inheritdoc />
    public override IEnumerable<ExportType> GetSupportTypesList() => (IEnumerable<ExportType>) new List<ExportType>()
    {
      new ExportType("Simple Classification", typeof (FlatTaxonomy).FullName),
      new ExportType("Hierarchical Classification", typeof (HierarchicalTaxonomy).FullName),
      new ExportType("Simple Classification Item", typeof (FlatTaxon).FullName),
      new ExportType("Hierarchical Classification Item", typeof (HierarchicalTaxon).FullName)
    };

    /// <inheritdoc />
    protected override void DeleteItems(
      Type managerTypeName,
      Type itemType,
      string provider,
      IList<Guid> addonLinkIds)
    {
      addonLinkIds.Remove(TaxonomyManager.TagsTaxonomyId);
      addonLinkIds.Remove(TaxonomyManager.CategoriesTaxonomyId);
      addonLinkIds.Remove(TaxonomyManager.DepartmentsTaxonomyId);
      IManager manager = ManagerBase.GetManager(managerTypeName, string.Empty);
      foreach (IDataItem dataItem in manager.GetItems(itemType, (string) null, (string) null, 0, 0).Cast<IDataItem>().Where<IDataItem>((Func<IDataItem, bool>) (m => addonLinkIds.Contains(m.Id))).ToList<IDataItem>())
        manager.DeleteItem((object) dataItem);
      manager.SaveChanges();
    }

    private void AddTaxonomyEntries(ScanOperation operation)
    {
      IQueryable<Guid> itemIds = (IQueryable<Guid>) null;
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == operation.AddOnInfo.Name));
      if (addon != null)
      {
        List<string> types = new List<string>()
        {
          typeof (FlatTaxonomy).FullName,
          typeof (HierarchicalTaxonomy).FullName
        };
        itemIds = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (a => types.Contains(a.ItemType) && a.AddonId == addon.Id)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (a => a.ItemId));
      }
      if (!(itemIds != null & itemIds.Count<Guid>() > 0))
        return;
      IQueryable<Taxonomy> source = TaxonomyManager.GetManager().GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (f => itemIds.Contains<Guid>(f.Id)));
      Expression<Func<Taxonomy, string>> selector = (Expression<Func<Taxonomy, string>>) (f => f.FieldValue<string>("Title_"));
      foreach (string str in (IEnumerable<string>) source.Select<Taxonomy, string>(selector))
      {
        AddOnEntry addOnEntry = new AddOnEntry()
        {
          DisplayName = str,
          AddonEntryType = AddOnEntryType.Classification
        };
        operation.AddOnInfo.Entries.Add(addOnEntry);
      }
    }
  }
}
