// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.DynamicStructureTransfer
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
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.DynamicModules.Builder.ExportImport;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Multisite.Web.Services;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Events;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>Imports and exports dynamic module packages</summary>
  internal class DynamicStructureTransfer : StructureTransferBase, IMultisiteTransfer
  {
    private static readonly object SupportedTypesCacheSync = new object();
    private IEnumerable<ExportType> supportedTypes;
    private Guid currentSiteId;
    private const string Group = "Dynamic modules";
    private const string AreaName = "Dynamic modules";
    private IEnumerable<string> dependencies;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.DynamicStructureTransfer" /> class.
    /// </summary>
    public DynamicStructureTransfer()
    {
      CacheDependency.Subscribe(typeof (DynamicModule), new ChangedCallback(this.SupportedTypesCacheExpired));
      CacheDependency.Subscribe(typeof (DynamicModuleType), new ChangedCallback(this.SupportedTypesCacheExpired));
      CacheDependency.Subscribe(typeof (DynamicModuleField), new ChangedCallback(this.SupportedTypesCacheExpired));
      CacheDependency.Subscribe(typeof (SiteDataSourceLink), new ChangedCallback(this.SupportedTypesCacheExpired));
    }

    /// <inheritdoc />
    public void Activate(string sourceName, Guid siteId)
    {
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      IQueryable<Guid> dynamicModuleIds = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (DynamicModule).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId));
      this.UpdateSiteDataSourceLinks(siteId, (IEnumerable<Guid>) dynamicModuleIds);
      this.UpdateControlPresentationLinks(siteId, dynamicModuleIds, manager, addon);
    }

    /// <inheritdoc />
    public void Activate(ICollection<ItemLink> itemLinks, Guid siteId)
    {
      if (itemLinks == null || itemLinks.Count <= 0)
        return;
      IQueryable<Guid> dynamicModuleIds = itemLinks.Where<ItemLink>((Func<ItemLink, bool>) (l => l.ItemType == typeof (DynamicModule).FullName)).Select<ItemLink, Guid>((Func<ItemLink, Guid>) (l => l.ItemId)).AsQueryable<Guid>();
      this.UpdateSiteDataSourceLinks(siteId, (IEnumerable<Guid>) dynamicModuleIds);
      this.UpdateControlPresentationLinks(siteId, dynamicModuleIds, itemLinks);
    }

    /// <inheritdoc />
    public void Deactivate(string sourceName, Guid siteId)
    {
      PackagingManager manager1 = PackagingManager.GetManager();
      Addon addon = manager1.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      IQueryable<Guid> dynamicModuleIds = manager1.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (DynamicModule).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId));
      IEnumerable<string> strings = ModuleBuilderManager.GetModules().Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => dynamicModuleIds.Contains<Guid>(m.Id))).Select<IDynamicModule, string>((Func<IDynamicModule, string>) (m => m.Name));
      MultisiteManager manager2 = MultisiteManager.GetManager();
      Site site = manager2.GetSite(siteId);
      foreach (string str in strings)
      {
        string moduleName = str;
        IDataSource dataSource = SystemManager.DataSourceRegistry.GetDataSources().SingleOrDefault<IDataSource>((Func<IDataSource, bool>) (ds => ds.Name == moduleName));
        if (dataSource != null)
        {
          IQueryable<SiteDataSourceLink> siteDataSourceLinks = manager2.GetSiteDataSourceLinks();
          Expression<Func<SiteDataSourceLink, bool>> predicate = (Expression<Func<SiteDataSourceLink, bool>>) (l => l.Site.Id == siteId && l.DataSource.Name == dataSource.Name);
          foreach (SiteDataSourceLink link in (IEnumerable<SiteDataSourceLink>) siteDataSourceLinks.Where<SiteDataSourceLink>(predicate))
          {
            site.SiteDataSourceLinks.Remove(link);
            manager2.Delete(link);
          }
        }
      }
      manager2.SaveChanges();
      this.UpdateControlPresentationLinks(siteId, dynamicModuleIds, manager1, addon, true);
    }

    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        Guid siteId = SystemManager.CurrentContext.CurrentSite.Id;
        if (this.supportedTypes == null || this.currentSiteId != siteId)
        {
          lock (DynamicStructureTransfer.SupportedTypesCacheSync)
          {
            if (this.supportedTypes != null)
            {
              if (!(this.currentSiteId != siteId))
                goto label_15;
            }
            this.currentSiteId = siteId;
            IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
            foreach (IDynamicModule module1 in ModuleBuilderManager.GetModules())
            {
              IDynamicModule module = module1;
              bool isExportable = SystemManager.CurrentContext.CurrentSite.SiteDataSourceLinks.Any<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (s => s.DataSourceName == module.Name && s.SiteId == siteId));
              ExportType moduleExportType = this.GetModuleExportType(module, isExportable);
              exportTypeList.Add(moduleExportType);
            }
            this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
          }
        }
label_15:
        return this.supportedTypes;
      }
    }

    public override string Area => "Dynamic modules";

    /// <inheritdoc />
    public override string GroupName => "Dynamic modules";

    /// <inheritdoc />
    public override IEnumerable<string> Dependencies
    {
      get
      {
        if (this.dependencies == null)
          this.dependencies = (IEnumerable<string>) new List<string>()
          {
            "Classifications"
          };
        return this.dependencies;
      }
    }

    /// <inheritdoc />
    public override IEnumerable<IPackageTransferObject> Export(
      IDictionary<string, string> configuration)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      IQueryable<DynamicModule> source = manager.GetDynamicModules().Where<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => (int) m.Status != 0));
      if (!this.AllowToExport(configuration))
        return Enumerable.Empty<IPackageTransferObject>();
      if (configuration != null)
      {
        ICollection<string> moduleNames = configuration.Keys;
        source = source.Where<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => moduleNames.Contains(m.Name)));
      }
      List<IPackageTransferObject> packageTransferObjectList = new List<IPackageTransferObject>();
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) source)
      {
        manager.LoadDynamicModuleGraph(dynamicModule);
        string name1 = dynamicModule.Name + (object) this.Separator + dynamicModule.Name + ".sf";
        DynamicModuleTransferObject moduleTransferObject1 = new DynamicModuleTransferObject(new Func<DynamicModule, DateTime, bool, Stream>(this.ExportModule), dynamicModule, name1);
        packageTransferObjectList.Add((IPackageTransferObject) moduleTransferObject1);
        string name2 = dynamicModule.Name + (object) this.Separator + "configs.sf";
        DynamicModuleTransferObject moduleTransferObject2 = new DynamicModuleTransferObject(new Func<DynamicModule, DateTime, bool, Stream>(this.ExportModuleConfigs), dynamicModule, name2);
        packageTransferObjectList.Add((IPackageTransferObject) moduleTransferObject2);
        string name3 = dynamicModule.Name + (object) this.Separator + "widgetTemplates.sf";
        DynamicModuleTransferObject moduleTransferObject3 = new DynamicModuleTransferObject(new Func<DynamicModule, DateTime, bool, Stream>(this.ExportControlPresentations), dynamicModule, name3);
        packageTransferObjectList.Add((IPackageTransferObject) moduleTransferObject3);
      }
      return (IEnumerable<IPackageTransferObject>) packageTransferObjectList;
    }

    /// <inheritdoc />
    public override void Import(
      IEnumerable<IPackageTransferObject> packageTransferObjects)
    {
      string transactionName = Guid.NewGuid().ToString();
      if (ObjectFactory.Resolve<IStructureTransfer>("Classifications") is TaxonomiesStructureTransfer structureTransfer)
        structureTransfer.Import(packageTransferObjects);
      this.ImportControlPresentations(packageTransferObjects, (string) null, transactionName);
      this.ImportModule(packageTransferObjects, (string) null, transactionName);
    }

    /// <inheritdoc />
    public override void ImportCompleted()
    {
      if (this.ModuleNamesToActivate.Count <= 0)
        return;
      foreach (string str in (IEnumerable<string>) this.ModuleNamesToActivate)
      {
        string moduleName = str;
        try
        {
          string transactionName = Guid.NewGuid().ToString();
          ModuleBuilderManager manager = ModuleBuilderManager.GetManager(string.Empty, transactionName);
          ModuleInstaller moduleInstaller = new ModuleInstaller(string.Empty, transactionName);
          DynamicModule dynamicModule = manager.GetDynamicModules().Where<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Name == moduleName)).FirstOrDefault<DynamicModule>();
          if (dynamicModule != null)
          {
            manager.LoadDynamicModuleGraph(dynamicModule, true);
            moduleInstaller.InstallModule(dynamicModule, configureForDefaultSite: SystemManager.CurrentContext.IsOneSiteMode);
          }
          TransactionManager.CommitTransaction(transactionName);
        }
        catch (Exception ex)
        {
          PackagingOperations.SetStructureImportErrorStatus(true);
          Log.Write((object) ex, ConfigurationPolicy.PackagingTrace);
          Exceptions.HandleException(ex, ExceptionPolicyName.Global);
        }
      }
      this.ModuleNamesToActivate.Clear();
    }

    /// <inheritdoc />
    public override bool AllowToDelete(string directory) => directory.Contains(this.GroupName + "\\");

    /// <inheritdoc />
    public override void Delete(string moduleName, out bool moduleDeleted)
    {
      moduleDeleted = false;
      string empty = string.Empty;
      string transactionName = moduleName;
      ModuleInstaller moduleInstaller = new ModuleInstaller(empty, transactionName);
      DynamicModule module = ModuleBuilderManager.GetManager(empty, transactionName).GetDynamicModules().Where<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Name == moduleName)).FirstOrDefault<DynamicModule>();
      if (module == null)
        return;
      if ((StructureTransferBase.PackageType != PackageType.Deployment ? 0 : (string.IsNullOrWhiteSpace(module.Origin) ? 1 : 0)) == 0 && !AddonOrigin.AddonNamesEqual(StructureTransferBase.CurrentOrigin, module.Origin))
        return;
      moduleInstaller.DeleteModule(module, new DeleteModuleContext()
      {
        DeleteModuleData = true
      }, false);
      TransactionManager.CommitTransaction(transactionName);
      moduleDeleted = true;
    }

    /// <inheritdoc />
    public override void Uninstall(string sourceName)
    {
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      IQueryable<Guid> dynamicModuleIds = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (DynamicModule).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId));
      string currentOrigin = new AddonOrigin(sourceName, (string) null).ToString();
      this.UninstallDynamicModules(dynamicModuleIds, currentOrigin);
      this.UninstallControlPresentations(manager, addon, dynamicModuleIds);
    }

    /// <inheritdoc />
    internal void ImportModule(
      IEnumerable<IPackageTransferObject> packageTransferObjects,
      string providerName,
      string transactionName)
    {
      IPackageTransferObject packageTransferObject1 = packageTransferObjects.FirstOrDefault<IPackageTransferObject>((Func<IPackageTransferObject, bool>) (o => !o.Name.EndsWith("taxonomies.sf") && !o.Name.EndsWith("widgetTemplates.sf") && !o.Name.EndsWith("configs.sf") && !o.Name.EndsWith("version.sf")));
      if (packageTransferObject1 == null)
        return;
      using (Stream stream1 = packageTransferObject1.GetStream())
      {
        if (!stream1.CanRead || stream1.Length <= 0L)
          return;
        stream1.Position = 0L;
        DynamicModule dynamicModule = this.Serializer.Deserialize<DynamicModule>(stream1);
        ModuleBuilderHelper.SetDynamicModuleIds(dynamicModule);
        this.SetOrigin(dynamicModule);
        Guid updateModuleId = Guid.Empty;
        if (ModuleBuilderManager.GetModules().Any<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Id == dynamicModule.Id)))
          updateModuleId = dynamicModule.Id;
        Dictionary<Type, string> configurationsToImport = (Dictionary<Type, string>) null;
        IPackageTransferObject packageTransferObject2 = packageTransferObjects.FirstOrDefault<IPackageTransferObject>((Func<IPackageTransferObject, bool>) (o => o.Name.EndsWith("configs.sf")));
        if (packageTransferObject2 != null)
        {
          using (Stream stream2 = packageTransferObject2.GetStream())
          {
            stream2.Position = 0L;
            ModuleImporter.ReadConfig(stream2, out configurationsToImport);
          }
        }
        this.FireItemImportedForModuleItems((IDynamicModule) dynamicModule, providerName, transactionName);
        DynamicModule module = ModuleImporter.ImportModule(dynamicModule, updateModuleId, configurationsToImport, transactionName, generateDefaultConfigsOnUpdate: false, configureForDefaultSite: false, origin: StructureTransferBase.CurrentOrigin);
        if (configurationsToImport != null)
        {
          foreach (KeyValuePair<Type, string> keyValuePair in configurationsToImport)
            this.RaiseItemImportedForConfigSection(keyValuePair.Key.FullName, transactionName);
        }
        if (dynamicModule.Status == DynamicModuleStatus.Inactive && module.Status == DynamicModuleStatus.Active)
          new ModuleInstaller(string.Empty, transactionName).UninstallModule(module);
        if (dynamicModule.Status != DynamicModuleStatus.Active || module.Status == DynamicModuleStatus.Active)
          return;
        this.ModuleNamesToActivate.Add(module.Name);
      }
    }

    /// <summary>Gets the content of the package transfer object</summary>
    /// <param name="module">The dynamic module.</param>
    /// <param name="lastModified">The date that this package was updated for the last time.</param>
    /// <param name="forceExport">A flag indicating whether to export regardless lastModified.</param>
    /// <returns>The content of the package transfer object.</returns>
    internal Stream ExportModule(
      DynamicModule module,
      DateTime lastModified,
      bool forceExport)
    {
      MemoryStream output = new MemoryStream();
      if (forceExport || module.LastModified > lastModified)
      {
        List<DynamicModuleType> dynamicModuleTypeList = new List<DynamicModuleType>();
        foreach (DynamicModuleType type in module.Types)
        {
          type.Origin = (string) null;
          List<DynamicModuleField> dynamicModuleFieldList = new List<DynamicModuleField>();
          foreach (DynamicModuleField field in type.Fields)
          {
            if (string.IsNullOrEmpty(field.Origin) || this.ExportMode != ExportMode.Deployment)
            {
              field.Origin = (string) null;
              dynamicModuleFieldList.Add(field);
            }
            if ((this.ExportMode == ExportMode.AddOn || this.ExportMode == ExportMode.Archive) && !string.IsNullOrEmpty(field.RelatedDataProvider) && field.RelatedDataProvider != "sf-any-site-provider")
              field.RelatedDataProvider = "sf-site-default-provider";
          }
          type.Fields = dynamicModuleFieldList.ToArray();
          dynamicModuleTypeList.Add(type);
        }
        module.Types = dynamicModuleTypeList.ToArray();
        module.Origin = (string) null;
        this.Serializer.Serialize((object) module, (Stream) output);
      }
      return (Stream) output;
    }

    /// <summary>
    /// Gets the module configurations content of the package transfer object
    /// </summary>
    /// <param name="module">The dynamic module.</param>
    /// <param name="lastModified">The date that this package was updated for the last time.</param>
    /// <param name="forceExport">A flag indicating whether to export regardless lastModified.</param>
    /// <returns>The module configurations content of the package transfer object.</returns>
    internal Stream ExportModuleConfigs(
      DynamicModule module,
      DateTime lastModified,
      bool forceExport)
    {
      MemoryStream memoryStream = new MemoryStream();
      if (forceExport || module.LastModified > lastModified)
      {
        bool exportModuleProviders = this.ExportMode == ExportMode.Deployment;
        ModuleExporter.WriteToStream("<root>", (Stream) memoryStream);
        ModuleExporter.ExportModuleConfigurations(module, memoryStream, exportModuleProviders, this.ExportMode);
        ModuleExporter.WriteToStream("</root>", (Stream) memoryStream);
      }
      return (Stream) memoryStream;
    }

    /// <summary>
    /// Gets the control presentations content of the package transfer object
    /// </summary>
    /// <param name="module">The dynamic module.</param>
    /// <param name="lastModified">The date that this package was updated for the last time.</param>
    /// <param name="forceExport">A flag indicating whether to export regardless lastModified.</param>
    /// <returns>The control presentations content of the package transfer object.</returns>
    internal Stream ExportControlPresentations(
      DynamicModule module,
      DateTime lastModified,
      bool forceExport)
    {
      PageManager manager = PageManager.GetManager();
      IEnumerable<ControlPresentation> controlPresentations = this.GetControlPresentations(((IEnumerable<DynamicModuleType>) module.Types).Select<DynamicModuleType, string>((Func<DynamicModuleType, string>) (t => t.GetFullTypeName())), manager);
      if (controlPresentations.Count<ControlPresentation>() <= 0)
        return (Stream) null;
      MemoryStream output = new MemoryStream();
      DateTime dateTime = controlPresentations.Max<ControlPresentation, DateTime>((Func<ControlPresentation, DateTime>) (c => c.LastModified));
      if (forceExport || dateTime > lastModified)
        this.Serializer.Serialize((object) controlPresentations, (Stream) output);
      return (Stream) output;
    }

    private void UpdateSiteDataSourceLinks(Guid siteId, IEnumerable<Guid> dynamicModuleIds)
    {
      IEnumerable<string> strings = ModuleBuilderManager.GetModules().Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => dynamicModuleIds.Contains<Guid>(m.Id))).Select<IDynamicModule, string>((Func<IDynamicModule, string>) (m => m.Name));
      MultisiteManager manager = MultisiteManager.GetManager();
      manager.GetSite(siteId);
      foreach (string dataSourceName in strings)
        MultisiteService.CreateOrUpdateSiteDataSourceLink(siteId, dataSourceName, multisiteManager: manager);
      manager.SaveChanges();
    }

    private ExportType GetModuleExportType(IDynamicModule module, bool isExportable)
    {
      ExportType moduleExportType = new ExportType(module.Title, module.Name, isExportable);
      Dictionary<Guid, ExportType> dictionary = new Dictionary<Guid, ExportType>();
      foreach (IDynamicModuleType type in module.Types)
        dictionary.Add(type.Id, new ExportType(type.DisplayName, type.GetFullTypeName(), isExportable));
      foreach (IDynamicModuleType dynamicModuleType in module.Types.Where<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.ParentTypeId == Guid.Empty)))
        moduleExportType.ChildTypes.Add(dictionary[dynamicModuleType.Id]);
      foreach (IDynamicModuleType dynamicModuleType in module.Types.Where<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.ParentTypeId != Guid.Empty)))
      {
        ExportType exportType;
        if (dictionary.TryGetValue(dynamicModuleType.ParentTypeId, out exportType))
          exportType.ChildTypes.Add(dictionary[dynamicModuleType.Id]);
      }
      return moduleExportType;
    }

    private void FireItemImportedForModuleItems(
      IDynamicModule module,
      string providerName,
      string transaction)
    {
      this.OnItemImported(new ItemImportedEventArgs()
      {
        ItemId = module.Id,
        ItemProvider = providerName,
        ItemType = module.GetType().FullName,
        TransactionName = transaction
      });
    }

    private void UpdateControlPresentationLinks(
      Guid siteId,
      IQueryable<Guid> dynamicModuleIds,
      ICollection<ItemLink> itemLinks,
      bool removeLinks = false)
    {
      PageManager manager = PageManager.GetManager();
      IEnumerable<ControlPresentation> controlPresentations = this.GetControlPresentations(itemLinks.Where<ItemLink>((Func<ItemLink, bool>) (l => l.ItemType == typeof (ControlPresentation).FullName)).Select<ItemLink, Guid>((Func<ItemLink, Guid>) (l => l.ItemId)), dynamicModuleIds, manager);
      this.UpdateControlPresentationLinks(siteId, removeLinks, manager, controlPresentations);
    }

    private void UpdateControlPresentationLinks(
      Guid siteId,
      IQueryable<Guid> dynamicModuleIds,
      PackagingManager packagingManager,
      Addon addon,
      bool removeLinks = false)
    {
      PageManager manager = PageManager.GetManager();
      IEnumerable<ControlPresentation> controlPresentations = this.GetControlPresentations((IEnumerable<Guid>) packagingManager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (ControlPresentation).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId)), dynamicModuleIds, manager);
      this.UpdateControlPresentationLinks(siteId, removeLinks, manager, controlPresentations);
    }

    private void UpdateControlPresentationLinks(
      Guid siteId,
      bool removeLinks,
      PageManager pageManager,
      IEnumerable<ControlPresentation> allControlPresentations)
    {
      foreach (ControlPresentation controlPresentation1 in allControlPresentations)
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

    private IEnumerable<ControlPresentation> GetControlPresentations(
      IEnumerable<Guid> controlPresentationIds,
      IQueryable<Guid> dynamicModuleIds,
      PageManager pageManager)
    {
      return this.GetControlPresentations(ModuleBuilderManager.GetModules().Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => dynamicModuleIds.Contains<Guid>(m.Id))).SelectMany<IDynamicModule, string>((Func<IDynamicModule, IEnumerable<string>>) (m => m.Types.Select<IDynamicModuleType, string>((Func<IDynamicModuleType, string>) (t => t.GetFullTypeName())))), pageManager).Where<ControlPresentation>((Func<ControlPresentation, bool>) (c => controlPresentationIds.Contains<Guid>(c.Id)));
    }

    private IEnumerable<ControlPresentation> GetControlPresentations(
      IEnumerable<string> typeNames,
      PageManager pageManager)
    {
      IEnumerable<string> moduleTypeNames = typeNames.SelectMany<string, string>((Func<string, IEnumerable<string>>) (t =>
      {
        List<string> controlPresentations = new List<string>();
        string str1 = t;
        string str2 = string.Format("{0} AND MVC", (object) str1);
        controlPresentations.Add(str1);
        controlPresentations.Add(str2);
        return (IEnumerable<string>) controlPresentations;
      }));
      return (IEnumerable<ControlPresentation>) pageManager.GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (c => moduleTypeNames.Contains<string>(c.Condition) || c.Condition.Contains("DynamicContentController") && c.ControlType.Contains("DynamicContentController"))).ToList<ControlPresentation>();
    }

    private void SupportedTypesCacheExpired(ICacheDependencyHandler caller, Type type, string path)
    {
      lock (DynamicStructureTransfer.SupportedTypesCacheSync)
        this.supportedTypes = (IEnumerable<ExportType>) null;
    }

    private void SetOrigin(DynamicModule module)
    {
      string currentOrigin = StructureTransferBase.CurrentOrigin;
      if (currentOrigin == null)
        return;
      module.Origin = currentOrigin;
      foreach (DynamicModuleType type in module.Types)
      {
        type.Origin = currentOrigin;
        foreach (DynamicModuleField field in type.Fields)
          field.Origin = currentOrigin;
      }
    }

    private void UninstallControlPresentations(
      PackagingManager packagingManager,
      Addon addon,
      IQueryable<Guid> dynamicModuleIds)
    {
      PageManager manager = PageManager.GetManager();
      IEnumerable<ControlPresentation> controlPresentations = this.GetControlPresentations((IEnumerable<Guid>) packagingManager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (ControlPresentation).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId)), dynamicModuleIds, manager);
      foreach (ControlPresentation controlPresentation in controlPresentations)
        manager.Delete((PresentationData) controlPresentation);
      if (controlPresentations.Count<ControlPresentation>() <= 0)
        return;
      manager.SaveChanges();
    }

    private void UninstallDynamicModules(IQueryable<Guid> dynamicModuleIds, string currentOrigin)
    {
      string transactionName = Guid.NewGuid().ToString();
      PageManager.GetManager(string.Empty, transactionName).GetPageNodes();
      ModuleInstaller moduleInstaller = new ModuleInstaller((string) null, transactionName);
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager((string) null, transactionName);
      IQueryable<DynamicModule> dynamicModules = manager.GetDynamicModules();
      Expression<Func<DynamicModule, bool>> predicate = (Expression<Func<DynamicModule, bool>>) (m => dynamicModuleIds.Contains<Guid>(m.Id));
      foreach (DynamicModule module in dynamicModules.Where<DynamicModule>(predicate).ToList<DynamicModule>().Where<DynamicModule>((Func<DynamicModule, bool>) (m => AddonOrigin.AddonNamesEqual(currentOrigin, m.Origin))))
        moduleInstaller.DeleteModule(module, new DeleteModuleContext()
        {
          DeleteModuleData = true
        });
      this.DeleteDynamicModuleTypes(manager, currentOrigin, moduleInstaller, transactionName);
      this.DeleteDynamicModuleFields(manager, currentOrigin, moduleInstaller, transactionName);
    }

    /// <summary>
    /// Deletes the dynamic module fields from Dynamic Types that do not belong to current add-on
    /// </summary>
    /// <param name="moduleBuilderManager">The module builder manager.</param>
    /// <param name="currentOrigin">The current origin.</param>
    /// <param name="moduleInstaller">The module installer.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    private void DeleteDynamicModuleFields(
      ModuleBuilderManager moduleBuilderManager,
      string currentOrigin,
      ModuleInstaller moduleInstaller,
      string transactionName)
    {
      List<IGrouping<Guid, DynamicModuleField>> list = moduleBuilderManager.GetDynamicModuleFields().Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => !string.IsNullOrEmpty(f.Origin))).ToList<DynamicModuleField>().Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => AddonOrigin.AddonNamesEqual(f.Origin, currentOrigin))).GroupBy<DynamicModuleField, Guid>((Func<DynamicModuleField, Guid>) (f => f.ParentTypeId)).ToList<IGrouping<Guid, DynamicModuleField>>();
      foreach (IGrouping<Guid, DynamicModuleField> source in list)
      {
        Guid key = source.Key;
        DynamicModuleType dynamicModuleType = moduleBuilderManager.GetDynamicModuleType(key);
        DynamicModule dynamicModule = moduleBuilderManager.GetDynamicModule(dynamicModuleType.ParentModuleId);
        moduleBuilderManager.LoadDynamicModuleGraph(dynamicModule, false);
        foreach (DynamicModuleField field1 in dynamicModuleType.Fields)
        {
          DynamicModuleField field = field1;
          if (source.Any<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.Id == field.Id)))
            field.FieldStatus = DynamicModuleFieldStatus.Removed;
        }
        if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
          moduleInstaller.UpdateModuleType(dynamicModule, dynamicModuleType, dynamicModuleType.DisplayName, dynamicModuleType.ParentModuleTypeId, false);
        else
          moduleInstaller.RemoveDeletedDynamicModuleTypeFields(dynamicModuleType);
      }
      if (list.Count <= 0)
        return;
      TransactionManager.CommitTransaction(transactionName);
    }

    /// <summary>
    /// Deletes the dynamic module types from Dynamic Modules that do not belong to current add-on
    /// </summary>
    /// <param name="moduleBuilderManager">The module builder manager.</param>
    /// <param name="currentOrigin">The current origin.</param>
    /// <param name="moduleInstaller">The module installer.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    private void DeleteDynamicModuleTypes(
      ModuleBuilderManager moduleBuilderManager,
      string currentOrigin,
      ModuleInstaller moduleInstaller,
      string transactionName)
    {
      IQueryable<DynamicModuleType> dynamicModuleTypes = moduleBuilderManager.GetDynamicModuleTypes();
      Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => !string.IsNullOrEmpty(t.Origin));
      foreach (IGrouping<Guid, DynamicModuleType> source in dynamicModuleTypes.Where<DynamicModuleType>(predicate).ToList<DynamicModuleType>().Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => AddonOrigin.AddonNamesEqual(t.Origin, currentOrigin))).GroupBy<DynamicModuleType, Guid>((Func<DynamicModuleType, Guid>) (t => t.ParentModuleId)))
      {
        DynamicModule dynamicModule = moduleBuilderManager.GetDynamicModule(source.Key);
        ModuleImporter.DeleteModuleTypes(source.ToList<DynamicModuleType>(), dynamicModule, moduleInstaller, transactionName, out List<string> _);
      }
    }
  }
}
