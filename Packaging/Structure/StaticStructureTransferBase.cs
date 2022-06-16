// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Structure.StaticStructureTransferBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Builder.ExportImport;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services.Contracts;

namespace Telerik.Sitefinity.Packaging.Structure
{
  internal abstract class StaticStructureTransferBase : StructureTransferBase, IMultisiteTransfer
  {
    private IEnumerable<string> dependencies;
    private IList<MetaType> moduleMetaTypes;

    /// <inheritdoc />
    public override IEnumerable<IPackageTransferObject> Export(
      IDictionary<string, string> configuration)
    {
      IExportableModule module = this.GetModule();
      if (!this.AllowToExport(configuration))
        return Enumerable.Empty<IPackageTransferObject>();
      this.SetModuleMetaTypes(module.GetModuleMetaTypes());
      return (IEnumerable<IPackageTransferObject>) this.GetPackageTransferObjects(module);
    }

    /// <inheritdoc />
    public override bool AllowToDelete(string directory) => false;

    /// <inheritdoc />
    public override void Import(
      IEnumerable<IPackageTransferObject> packageTransferObjects)
    {
      string transactionName = "ImportModuleStructure";
      this.SetModuleMetaTypes(this.GetModule().GetModuleMetaTypes());
      if (ObjectFactory.Resolve<IStructureTransfer>("Classifications") is TaxonomiesStructureTransfer structureTransfer)
        structureTransfer.Import(packageTransferObjects);
      this.ImportPackageTransferObjects(packageTransferObjects, transactionName);
      TransactionManager.CommitTransaction(transactionName);
      TransactionManager.DisposeTransaction(transactionName);
    }

    /// <inheritdoc />
    public override void ImportCompleted()
    {
    }

    /// <inheritdoc />
    public override void Uninstall(string sourceName)
    {
      this.DeleteControlPresentations(sourceName);
      this.DeleteCustomFields(sourceName);
    }

    /// <inheritdoc />
    public override string GroupName => string.Empty;

    /// <inheritdoc />
    public override IEnumerable<string> Dependencies
    {
      get
      {
        if (this.dependencies == null)
          this.dependencies = (IEnumerable<string>) new List<string>()
          {
            "Classifications",
            "Dynamic modules"
          };
        return this.dependencies;
      }
    }

    /// <inheritdoc />
    public void Activate(string sourceName, Guid siteId) => this.UpdateControlPresentationLinks(sourceName, siteId);

    /// <inheritdoc />
    public void Activate(ICollection<ItemLink> itemLinks, Guid siteId) => this.UpdateControlPresentationLinks(itemLinks, siteId);

    /// <inheritdoc />
    public void Deactivate(string sourceName, Guid siteId) => this.UpdateControlPresentationLinks(sourceName, siteId, true);

    internal virtual IList<string> MvcWidgetTemplatesAreaNames => (IList<string>) new List<string>();

    internal virtual Stream ExportModule(
      IExportableModule module,
      DateTime lastModified,
      bool forceExport)
    {
      IList<MetaType> moduleMetaTypes = this.GetModuleMetaTypes(module);
      MemoryStream output = new MemoryStream();
      if (moduleMetaTypes.Count > 0)
      {
        DateTime dateTime = moduleMetaTypes.Max<MetaType, DateTime>((Func<MetaType, DateTime>) (t => t.LastModified));
        if (forceExport || dateTime > lastModified)
        {
          List<MetaType> source = new List<MetaType>();
          MetadataManager manager = MetadataManager.GetManager();
          foreach (MetaType metaType1 in (IEnumerable<MetaType>) moduleMetaTypes)
          {
            MetaType metaType2 = this.CopyMetaType(metaType1, manager);
            foreach (MetaField field in (IEnumerable<MetaField>) metaType1.Fields)
            {
              if (string.IsNullOrEmpty(field.Origin) || this.ExportMode != ExportMode.Deployment)
              {
                MetaField target = new MetaField(field.ApplicationName, field.Id);
                MetadataManager.CopyMetafield((IMetaField) field, target);
                target.ChoiceFieldDefinition = field.ChoiceFieldDefinition;
                manager.CopyMetaFieldAttributes(field, target);
                if ((this.ExportMode == ExportMode.AddOn || this.ExportMode == ExportMode.Archive) && field.ClrType == typeof (RelatedItems).FullName)
                {
                  MetaFieldAttribute metaFieldAttribute = field.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => a.Name == DynamicAttributeNames.RelatedProviders));
                  if (metaFieldAttribute != null && metaFieldAttribute.Value != "sf-any-site-provider")
                    metaFieldAttribute.Value = "sf-site-default-provider";
                }
                target.Origin = (string) null;
                metaType2.Fields.Add(target);
              }
            }
            metaType2.Origin = (string) null;
            source.Add(metaType2);
          }
          this.Serializer.Serialize((object) source, (Stream) output);
        }
      }
      return (Stream) output;
    }

    /// <summary>
    /// Gets the control presentations content of the package transfer object
    /// </summary>
    /// <param name="module">The module.</param>
    /// <param name="lastModified">The date that this package was updated for the last time.</param>
    /// <param name="forceExport">A flag indicating whether to export regardless lastModified.</param>
    /// <returns>The control presentations content of the package transfer object.</returns>
    internal virtual Stream ExportControlPresentations(
      IExportableModule module,
      DateTime lastModified,
      bool forceExport)
    {
      IEnumerable<string> moduleTypeNames = StaticStructureTransferBase.GetModuleTypeNames(module);
      if (moduleTypeNames != null && moduleTypeNames.Count<string>() > 0)
      {
        List<ControlPresentation> controlPresentations = this.GetControlPresentations(moduleTypeNames);
        if (controlPresentations.Count > 0)
        {
          MemoryStream output = new MemoryStream();
          DateTime dateTime = controlPresentations.Max<ControlPresentation, DateTime>((Func<ControlPresentation, DateTime>) (c => c.LastModified));
          if (forceExport || dateTime > lastModified)
            this.Serializer.Serialize((object) controlPresentations, (Stream) output);
          return (Stream) output;
        }
      }
      return (Stream) null;
    }

    /// <summary>
    /// Gets the module configurations content of the package transfer object
    /// </summary>
    /// <param name="module">The module.</param>
    /// <param name="lastModified">The date that this package was updated for the last time.</param>
    /// <param name="forceExport">A flag indicating whether to export regardless lastModified.</param>
    /// <returns>The module configurations content of the package transfer object.</returns>
    internal virtual Stream ExportModuleConfigs(
      IExportableModule module,
      DateTime lastModified,
      bool forceExport)
    {
      MemoryStream memoryStream = new MemoryStream();
      IList<MetaType> moduleMetaTypes = this.GetModuleMetaTypes(module);
      if (moduleMetaTypes.Count > 0)
      {
        DateTime dateTime = moduleMetaTypes.Max<MetaType, DateTime>((Func<MetaType, DateTime>) (t => t.LastModified));
        if (forceExport || dateTime > lastModified)
        {
          ModuleExporter.WriteToStream("<root>", (Stream) memoryStream);
          this.ExportModuleConfigs(module, memoryStream);
          ModuleExporter.WriteToStream("</root>", (Stream) memoryStream);
          if (this.ExportMode == ExportMode.AddOn || this.ExportMode == ExportMode.Archive)
            this.ReplaceRelatedDataProviderWithDefaultProvider(ref memoryStream);
        }
      }
      return (Stream) memoryStream;
    }

    internal virtual void ImportModule(
      IEnumerable<IPackageTransferObject> packageTransferObjects,
      string providerName,
      string transactionName)
    {
      IPackageTransferObject packageTransferObject = packageTransferObjects.FirstOrDefault<IPackageTransferObject>((Func<IPackageTransferObject, bool>) (o => !o.Name.EndsWith("taxonomies.sf") && !o.Name.EndsWith("widgetTemplates.sf") && !o.Name.EndsWith("configs.sf") && !o.Name.EndsWith("version.sf")));
      if (packageTransferObject == null)
        return;
      IExportableModule module = this.GetModule();
      if (module == null)
        return;
      using (Stream stream = packageTransferObject.GetStream())
      {
        if (!stream.CanRead || stream.Length <= 0L)
          return;
        stream.Position = 0L;
        IList<MetaType> moduleMetaTypes = this.Serializer.Deserialize<IList<MetaType>>(stream);
        this.SetOrigin(moduleMetaTypes);
        MetadataManager manager = MetadataManager.GetManager((string) null, transactionName);
        Guid[] typeIds = this.GetModuleMetaTypes(module).Where<MetaType>((Func<MetaType, bool>) (t => t != null)).Select<MetaType, Guid>((Func<MetaType, Guid>) (t => t.Id)).ToArray<Guid>();
        List<MetaType> list1 = manager.GetMetaTypes().Where<MetaType>((Expression<Func<MetaType, bool>>) (t => typeIds.Contains<Guid>(t.Id))).ToList<MetaType>();
        if (moduleMetaTypes.Count > 0)
          this.UpdateModuleMetaData(manager, moduleMetaTypes, list1);
        if (list1.Count <= 0)
          return;
        List<MetaType> list2 = list1.Where<MetaType>((Func<MetaType, bool>) (pt => pt.Origin == StructureTransferBase.CurrentOrigin && !moduleMetaTypes.Any<MetaType>((Func<MetaType, bool>) (mt => mt.FullTypeName == pt.FullTypeName)))).ToList<MetaType>();
        this.DeleteMetaData(manager, list2);
      }
    }

    /// <summary>Imports the module configurations.</summary>
    /// <param name="packageTransferObjects">The package transfer objects.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    internal virtual void ImportModuleConfigurations(
      IEnumerable<IPackageTransferObject> packageTransferObjects,
      string providerName,
      string transactionName)
    {
      if (this.GetModule() == null)
        return;
      Dictionary<Type, string> configurationsToImport = (Dictionary<Type, string>) null;
      IPackageTransferObject packageTransferObject = packageTransferObjects.FirstOrDefault<IPackageTransferObject>((Func<IPackageTransferObject, bool>) (o => o.Name.EndsWith("configs.sf")));
      if (packageTransferObject == null)
        return;
      using (Stream stream = packageTransferObject.GetStream())
      {
        stream.Position = 0L;
        ModuleImporter.ReadConfig(stream, out configurationsToImport);
      }
      ConfigManager manager = ConfigManager.GetManager();
      if (configurationsToImport == null)
        return;
      foreach (KeyValuePair<Type, string> config in configurationsToImport)
        this.ImportConfiguration(config, manager, transactionName);
    }

    /// <summary>Gets the package transfer objects.</summary>
    /// <param name="module">The module.</param>
    /// <returns>The package transfer objects</returns>
    protected virtual List<IPackageTransferObject> GetPackageTransferObjects(
      IExportableModule module)
    {
      List<IPackageTransferObject> packageTransferObjects = new List<IPackageTransferObject>();
      string name = this.Area + ".sf";
      packageTransferObjects.Add((IPackageTransferObject) new StaticModuleTransferObject(new Func<IExportableModule, DateTime, bool, Stream>(this.ExportModule), module, name));
      packageTransferObjects.Add((IPackageTransferObject) new StaticModuleTransferObject(new Func<IExportableModule, DateTime, bool, Stream>(this.ExportModuleConfigs), module, "configs.sf"));
      packageTransferObjects.Add((IPackageTransferObject) new StaticModuleTransferObject(new Func<IExportableModule, DateTime, bool, Stream>(this.ExportControlPresentations), module, "widgetTemplates.sf"));
      return packageTransferObjects;
    }

    /// <summary>Imports the package transfer objects.</summary>
    /// <param name="packageTransferObjects">The package transfer objects.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    protected virtual void ImportPackageTransferObjects(
      IEnumerable<IPackageTransferObject> packageTransferObjects,
      string transactionName)
    {
      this.ImportControlPresentations(packageTransferObjects, (string) null, transactionName);
      this.ImportModule(packageTransferObjects, (string) null, transactionName);
      this.ImportModuleConfigurations(packageTransferObjects, (string) null, transactionName);
    }

    /// <summary>Copies the meta type.</summary>
    /// <param name="metaType">Type of the meta.</param>
    /// <param name="metadataManager">The metadata manager.</param>
    /// <returns>Copy of the provided meta type</returns>
    protected virtual MetaType CopyMetaType(
      MetaType metaType,
      MetadataManager metadataManager)
    {
      MetaType target = new MetaType(metaType.ApplicationName, metaType.Id);
      metadataManager.CopyMetaType(metaType, target);
      metadataManager.CopyMetaTypeAttributes(metaType, target);
      return target;
    }

    /// <summary>Updates the module meta data.</summary>
    /// <param name="metadataManager">The metadata manager.</param>
    /// <param name="importedMetaTypes">The imported meta types.</param>
    /// <param name="persistedMetaTypes">The persisted meta types.</param>
    protected virtual void UpdateModuleMetaData(
      MetadataManager metadataManager,
      IList<MetaType> importedMetaTypes,
      List<MetaType> persistedMetaTypes)
    {
      ModuleImporterHelper.GetInstance().UpdateMetaData(metadataManager, importedMetaTypes, (IList<MetaType>) persistedMetaTypes);
    }

    /// <summary>Deletes the meta data.</summary>
    /// <param name="metadataManager">The metadata manager.</param>
    /// <param name="deletedTypes">The deleted types.</param>
    protected virtual void DeleteMetaData(
      MetadataManager metadataManager,
      List<MetaType> deletedTypes)
    {
      ModuleImporterHelper.GetInstance().DeleteMetaTypes(metadataManager, (IList<MetaType>) deletedTypes);
    }

    /// <summary>Sets the origin.</summary>
    /// <param name="metaTypes">The meta types.</param>
    protected virtual void SetOrigin(IList<MetaType> metaTypes)
    {
      string currentOrigin = StructureTransferBase.CurrentOrigin;
      if (currentOrigin == null)
        return;
      foreach (MetaType metaType in (IEnumerable<MetaType>) metaTypes)
      {
        foreach (MetaField field in (IEnumerable<MetaField>) metaType.Fields)
          field.Origin = currentOrigin;
      }
    }

    /// <summary>
    /// Exports the provided <see cref="T:Telerik.Sitefinity.Modules.IExportableModule" /> module's configurations
    /// into the provided <see cref="T:System.IO.MemoryStream" /> memoryStream.
    /// </summary>
    /// <param name="module">The module.</param>
    /// <param name="memoryStream">The memory stream that is going to be used to store the exported configurations.</param>
    protected virtual void ExportModuleConfigs(IExportableModule module, MemoryStream memoryStream)
    {
      bool exportProviders = this.ExportMode == ExportMode.Deployment;
      ModuleImporterHelper.GetInstance().ExportContentModuleConfigurations(module.ModuleConfig, module.ModuleName, (Stream) memoryStream, exportProviders, this.ExportMode);
    }

    /// <summary>
    /// Replaces all instances of related data provider with the default related data site provider in the provided memory stream's content.
    /// </summary>
    /// <param name="memoryStream">The memory stream in the content of which will the data be replaced.</param>
    protected virtual void ReplaceRelatedDataProviderWithDefaultProvider(
      ref MemoryStream memoryStream)
    {
      memoryStream.Position = 0L;
      string inputString = Regex.Replace(new StreamReader((Stream) memoryStream, Encoding.UTF8).ReadToEnd(), "relatedDataProvider=(?!\"sf-any-site-provider\")\"[^\"]*\"", string.Format("relatedDataProvider=\"{0}\"", (object) "sf-site-default-provider"));
      memoryStream = new MemoryStream();
      MemoryStream outputStream = memoryStream;
      ModuleExporter.WriteToStream(inputString, (Stream) outputStream);
    }

    protected virtual List<ControlPresentation> GetControlPresentations(
      IEnumerable<string> moduleTypeNames)
    {
      return PageManager.GetManager().GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (c => moduleTypeNames.Contains<string>(c.Condition) || (moduleTypeNames.Contains<string>(c.AreaName) || c.AreaName == this.Area || this.MvcWidgetTemplatesAreaNames.Contains(c.AreaName)) && c.Condition.Contains("Controller"))).ToList<ControlPresentation>();
    }

    protected virtual IExportableModule GetModule() => SystemManager.GetModule(this.Area) as IExportableModule;

    protected virtual IList<MetaType> GetModuleMetaTypes(IExportableModule module)
    {
      if (this.moduleMetaTypes == null)
        this.moduleMetaTypes = module.GetModuleMetaTypes();
      return this.moduleMetaTypes;
    }

    protected virtual void SetModuleMetaTypes(IList<MetaType> metaTypes) => this.moduleMetaTypes = metaTypes;

    private static IEnumerable<string> GetModuleTypeNames(IExportableModule module)
    {
      IEnumerable<string> moduleTypeNames1 = (IEnumerable<string>) null;
      if (module is ITypeSettingsProvider settingsProvider)
        moduleTypeNames1 = settingsProvider.GetTypeSettings().Keys.SelectMany<string, string>((Func<string, IEnumerable<string>>) (c =>
        {
          IList<string> moduleTypeNames2 = (IList<string>) new List<string>();
          Type type = TypeResolutionService.ResolveType(c, false);
          if (type != (Type) null)
          {
            string fullName = type.FullName;
            string name = type.Name;
            string plural = PluralsResolver.Instance.ToPlural(type.Name);
            moduleTypeNames2.Add(fullName);
            moduleTypeNames2.Add(name);
            moduleTypeNames2.Add(plural);
            moduleTypeNames2.Add(Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.GetControlKey(fullName, module.ModuleName));
          }
          else
            moduleTypeNames2.Add(c);
          return (IEnumerable<string>) moduleTypeNames2;
        }));
      return moduleTypeNames1;
    }

    private IEnumerable<ControlPresentation> GetControlPresentations(
      string addonName)
    {
      IEnumerable<string> moduleTypeNames = StaticStructureTransferBase.GetModuleTypeNames(this.GetModule());
      if (moduleTypeNames != null && moduleTypeNames.Count<string>() > 0)
      {
        PackagingManager manager = PackagingManager.GetManager();
        Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == addonName));
        if (addon != null)
        {
          IQueryable<Guid> controlPresentationIds = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (ControlPresentation).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId));
          return this.GetControlPresentations(moduleTypeNames).Where<ControlPresentation>((Func<ControlPresentation, bool>) (c => controlPresentationIds.Contains<Guid>(c.Id)));
        }
      }
      return Enumerable.Empty<ControlPresentation>();
    }

    private IEnumerable<ControlPresentation> GetControlPresentations(
      ICollection<ItemLink> itemLinks)
    {
      IEnumerable<string> moduleTypeNames = StaticStructureTransferBase.GetModuleTypeNames(this.GetModule());
      if (moduleTypeNames == null || moduleTypeNames.Count<string>() <= 0)
        return Enumerable.Empty<ControlPresentation>();
      IEnumerable<Guid> controlPresentationIds = itemLinks.Where<ItemLink>((Func<ItemLink, bool>) (l => l.ItemType == typeof (ControlPresentation).FullName)).Select<ItemLink, Guid>((Func<ItemLink, Guid>) (l => l.ItemId));
      return this.GetControlPresentations(moduleTypeNames).Where<ControlPresentation>((Func<ControlPresentation, bool>) (c => controlPresentationIds.Contains<Guid>(c.Id)));
    }

    private void DeleteCustomFields(string sourceName)
    {
      IExportableModule module = this.GetModule();
      if (module == null)
        return;
      IList<MetaType> moduleMetaTypes = this.GetModuleMetaTypes(module);
      if (moduleMetaTypes == null || moduleMetaTypes.Count <= 0)
        return;
      string origin = new AddonOrigin(sourceName, (string) null).ToString();
      foreach (MetaType metaType in (IEnumerable<MetaType>) moduleMetaTypes)
      {
        Type contentType = TypeResolutionService.ResolveType(metaType.FullTypeName, false);
        if (contentType != (Type) null)
        {
          IList<string> list = (IList<string>) metaType.Fields.Where<MetaField>((Func<MetaField, bool>) (f => AddonOrigin.AddonNamesEqual(f.Origin, origin))).Select<MetaField, string>((Func<MetaField, string>) (f => f.FieldName)).ToList<string>();
          CustomFieldsContext customFieldsContext = new CustomFieldsContext(contentType);
          customFieldsContext.RemoveCustomFields(list, metaType.FullTypeName);
          customFieldsContext.SaveChanges();
        }
      }
    }

    private void DeleteControlPresentations(string sourceName)
    {
      IEnumerable<ControlPresentation> controlPresentations = this.GetControlPresentations(sourceName);
      if (controlPresentations.Count<ControlPresentation>() <= 0)
        return;
      PageManager manager = PageManager.GetManager();
      foreach (ControlPresentation controlPresentation in controlPresentations)
        manager.Delete((PresentationData) controlPresentation);
      manager.SaveChanges();
    }

    private void UpdateControlPresentationLinks(string sourceName, Guid siteId, bool removeLinks = false)
    {
      PageManager manager = PageManager.GetManager();
      IEnumerable<ControlPresentation> controlPresentations = this.GetControlPresentations(sourceName);
      this.UpdateControlPresentationLinks(siteId, removeLinks, controlPresentations, manager);
    }

    private void UpdateControlPresentationLinks(
      ICollection<ItemLink> itemLinks,
      Guid siteId,
      bool removeLinks = false)
    {
      PageManager manager = PageManager.GetManager();
      IEnumerable<ControlPresentation> controlPresentations = this.GetControlPresentations(itemLinks);
      this.UpdateControlPresentationLinks(siteId, removeLinks, controlPresentations, manager);
    }

    private void UpdateControlPresentationLinks(
      Guid siteId,
      bool removeLinks,
      IEnumerable<ControlPresentation> addonControlPresentations,
      PageManager pageManager)
    {
      foreach (ControlPresentation controlPresentation1 in addonControlPresentations)
      {
        ControlPresentation controlPresentation = controlPresentation1;
        SiteItemLink link = pageManager.GetSitePresentationItemLinks<ControlPresentation>().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == siteId && l.ItemId == controlPresentation.Id)).FirstOrDefault<SiteItemLink>();
        if (!removeLinks && link == null)
          pageManager.LinkPresentationItemToSite((PresentationData) controlPresentation, siteId);
        else if (removeLinks && link != null)
          pageManager.Delete(link);
      }
      pageManager.SaveChanges();
    }
  }
}
