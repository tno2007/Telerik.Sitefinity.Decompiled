// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomiesStructureTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Events;
using Telerik.Sitefinity.Packaging.Logging;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  internal class TaxonomiesStructureTransfer : StaticStructureTransferBase
  {
    public const string AreaName = "Classifications";
    private IEnumerable<ExportType> supportedTypes;
    private IEnumerable<string> dependencies;

    /// <inheritdoc />
    public override IEnumerable<IPackageTransferObject> Export(
      IDictionary<string, string> configuration)
    {
      if (!this.AllowToExport(configuration))
        return Enumerable.Empty<IPackageTransferObject>();
      return (IEnumerable<IPackageTransferObject>) new List<IPackageTransferObject>()
      {
        (IPackageTransferObject) new StaticModuleTransferObject(new Func<IExportableModule, DateTime, bool, Stream>(this.ExportTaxonomies), (IExportableModule) null, "taxonomies.sf")
      };
    }

    /// <inheritdoc />
    public override void Import(
      IEnumerable<IPackageTransferObject> packageTransferObjects)
    {
      using (SiteRegion.SingleSiteMode())
      {
        string transactionName = "ImportModuleStructure";
        IPackageTransferObject packageTransferObject = packageTransferObjects.FirstOrDefault<IPackageTransferObject>((Func<IPackageTransferObject, bool>) (o => o.Name.EndsWith("taxonomies.sf")));
        if (packageTransferObject == null)
          return;
        using (Stream stream = packageTransferObject.GetStream())
        {
          if (!stream.CanRead || stream.Length <= 0L)
            return;
          stream.Position = 0L;
          IEnumerable<TaxonomyExportObject> taxonomyExportObjects = this.Serializer.Deserialize<IEnumerable<TaxonomyExportObject>>(stream);
          TaxonomyManager manager = TaxonomyManager.GetManager((string) null, transactionName);
          foreach (TaxonomyExportObject taxonomyExportObject in taxonomyExportObjects)
          {
            TaxonomyExportObject taxonomy = taxonomyExportObject;
            bool flag = false;
            Taxonomy taxonomy1 = manager.GetTaxonomies<Taxonomy>().FirstOrDefault<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Id == taxonomy.Id));
            if (taxonomy1 == null)
            {
              flag = true;
              if (manager.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Name == taxonomy.Name)).SingleOrDefault<Taxonomy>() != null)
                throw new ArgumentException("A taxonomy with the same name and different Id already exists.");
              if (taxonomy.Type == typeof (FlatTaxonomy).FullName)
              {
                taxonomy1 = (Taxonomy) manager.CreateTaxonomy<FlatTaxonomy>(taxonomy.Id);
              }
              else
              {
                if (!(taxonomy.Type == typeof (HierarchicalTaxonomy).FullName))
                  throw new NotSupportedException();
                taxonomy1 = (Taxonomy) manager.CreateTaxonomy<HierarchicalTaxonomy>(taxonomy.Id);
              }
            }
            taxonomy1.Name = taxonomy.Name;
            TaxonomyExportObject.CopyLstringValuesToLstringObject(taxonomy.Description, taxonomy1.Description);
            TaxonomyExportObject.CopyLstringValuesToLstringObject(taxonomy.Title, taxonomy1.Title);
            TaxonomyExportObject.CopyLstringValuesToLstringObject(taxonomy.TaxonName, taxonomy1.TaxonName);
            if (flag)
              this.OnItemImported(new ItemImportedEventArgs()
              {
                ItemId = taxonomy1.Id,
                ItemProvider = (string) null,
                ItemType = taxonomy1.GetType().FullName,
                TransactionName = transactionName
              });
          }
          TransactionManager.CommitTransaction(transactionName);
        }
      }
    }

    /// <inheritdoc />
    public override void Count(
      DirectoryInfo directory,
      ScanOperation operation,
      AddOnEntryType type)
    {
    }

    /// <inheritdoc />
    public override void Uninstall(string sourceName) => this.DeleteAddonTaxonomies(sourceName);

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
    public override IEnumerable<string> Dependencies
    {
      get
      {
        if (this.dependencies == null)
          this.dependencies = (IEnumerable<string>) new List<string>();
        return this.dependencies;
      }
    }

    /// <inheritdoc />
    public override string Area => "Classifications";

    /// <summary>
    /// Gets the taxonomies content of the package transfer object
    /// </summary>
    /// <param name="module">The module.</param>
    /// <param name="lastModified">The date that this package was updated for the last time.</param>
    /// <param name="forceExport">A flag indicating whether to export regardless lastModified.</param>
    /// <returns>The taxonomies content of the package transfer object.</returns>
    internal Stream ExportTaxonomies(
      IExportableModule module,
      DateTime lastModified,
      bool forceExport)
    {
      List<TaxonomyExportObject> list = TaxonomyManager.GetManager().GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Id != SiteInitializer.PageTemplatesTaxonomyId)).Select<Taxonomy, TaxonomyExportObject>((Expression<Func<Taxonomy, TaxonomyExportObject>>) (t => new TaxonomyExportObject(t))).ToList<TaxonomyExportObject>();
      if (list.Count <= 0)
        return (Stream) null;
      MemoryStream output = new MemoryStream();
      DateTime dateTime = list.Max<TaxonomyExportObject, DateTime>((Func<TaxonomyExportObject, DateTime>) (t => t.LastModified));
      if (forceExport || dateTime > lastModified)
        this.Serializer.Serialize((object) list, (Stream) output);
      return (Stream) output;
    }

    private void DeleteAddonTaxonomies(string sourceName)
    {
      TaxonomyManager manager1 = TaxonomyManager.GetManager();
      IEnumerable<Taxonomy> addonTaxonomies = this.GetAddonTaxonomies(sourceName, manager1);
      MetadataManager manager2 = MetadataManager.GetManager();
      foreach (Taxonomy taxonomy1 in addonTaxonomies)
      {
        Taxonomy taxonomy = taxonomy1;
        try
        {
          IQueryable<MetaField> metafields = manager2.GetMetafields();
          Expression<Func<MetaField, bool>> predicate = (Expression<Func<MetaField, bool>>) (f => f.TaxonomyId == taxonomy.Id);
          foreach (MetaField metaField in (IEnumerable<MetaField>) metafields.Where<MetaField>(predicate))
          {
            Type clrType = metaField.Parent.ClrType;
            CustomFieldsContext customFieldsContext = new CustomFieldsContext(clrType);
            customFieldsContext.RemoveCustomFields((IList<string>) new List<string>()
            {
              metaField.FieldName
            }, clrType.FullName);
            customFieldsContext.SaveChanges();
          }
          manager1.Delete((ITaxonomy) taxonomy);
        }
        catch (InvalidOperationException ex)
        {
          PackagingLog.Log((object) ex.Message);
        }
      }
      manager1.SaveChanges();
    }

    private IEnumerable<Taxonomy> GetAddonTaxonomies(
      string sourceName,
      TaxonomyManager taxonomyManager)
    {
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return Enumerable.Empty<Taxonomy>();
      IQueryable<Guid> taxonomiesIds = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && (l.ItemType == typeof (FlatTaxonomy).FullName || l.ItemType == typeof (HierarchicalTaxonomy).FullName))).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId));
      return (IEnumerable<Taxonomy>) taxonomyManager.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => taxonomiesIds.Contains<Guid>(t.Id)));
    }
  }
}
