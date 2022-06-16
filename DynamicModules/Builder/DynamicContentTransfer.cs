// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.DynamicContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>
  /// Implements functionality for converting items from dynamic content in transferable format.
  /// </summary>
  internal class DynamicContentTransfer : ContentTransferBase
  {
    private static readonly object SupportedTypesCacheSync = new object();
    private const string AreaName = "Dynamic modules";
    private IEnumerable<ExportType> supportedTypes;
    private Guid currentSiteId;
    private bool dependenciesPopulated;
    private readonly Lazy<DynamicContentImporter> itemsImporter = new Lazy<DynamicContentImporter>((Func<DynamicContentImporter>) (() =>
    {
      return new DynamicContentImporter("Export/Import")
      {
        Serializer = (ISiteSyncSerializer) new SiteSyncSerializer("Export/Import")
      };
    }));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.DynamicContentTransfer" /> class.
    /// </summary>
    public DynamicContentTransfer()
    {
      CacheDependency.Subscribe(typeof (DynamicModule), new ChangedCallback(this.SupportedTypesCacheExpired));
      CacheDependency.Subscribe(typeof (DynamicModuleType), new ChangedCallback(this.SupportedTypesCacheExpired));
      CacheDependency.Subscribe(typeof (DynamicModuleField), new ChangedCallback(this.SupportedTypesCacheExpired));
      CacheDependency.Subscribe(typeof (SiteDataSourceLink), new ChangedCallback(this.SupportedTypesCacheExpired));
    }

    /// <inheritdoc />
    public override SiteSyncImporter ItemsImporter => (SiteSyncImporter) this.itemsImporter.Value;

    /// <inheritdoc />
    public override string Area => "Dynamic modules";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        Guid siteId = SystemManager.CurrentContext.CurrentSite.Id;
        if (this.supportedTypes == null || this.currentSiteId != siteId)
        {
          lock (DynamicContentTransfer.SupportedTypesCacheSync)
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

    /// <inheritdoc />
    public override IDictionary<string, IEnumerable<string>> Dependencies
    {
      get
      {
        if (!this.dependenciesPopulated)
          this.PopulateDependencies();
        return base.Dependencies;
      }
    }

    /// <inheritdoc />
    public override string GetExportFolderName(ExportParams parameters)
    {
      string exportFolderName = this.Area;
      foreach (IDynamicModule module in ModuleBuilderManager.GetModules())
      {
        if (module.Types.Any<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.GetFullTypeName() == parameters.TypeName)))
        {
          exportFolderName = exportFolderName + (object) Path.DirectorySeparatorChar + module.Name;
          return exportFolderName;
        }
      }
      return exportFolderName;
    }

    /// <inheritdoc />
    public override void Delete(string sourceName)
    {
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      IQueryable<Guid> dynamicModuleIds = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (DynamicModule).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (a => a.ItemId));
      List<Type> list = ModuleBuilderManager.GetModules().Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => dynamicModuleIds.Contains<Guid>(m.Id))).SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (m => m.Types)).Select<IDynamicModuleType, Type>((Func<IDynamicModuleType, Type>) (t => TypeResolutionService.ResolveType(t.GetFullTypeName()))).ToList<Type>();
      this.DeleteImportedData(sourceName, typeof (DynamicModuleManager), (IList<Type>) list);
    }

    /// <inheritdoc />
    public override IEnumerable<IQueryable<object>> GetItemsQueries(
      ExportParams parameters)
    {
      Type type = TypeResolutionService.ResolveType(parameters.TypeName);
      string moduleName = this.GetModuleName(parameters.TypeName);
      if (string.IsNullOrEmpty(parameters.ProviderName))
        parameters.ProviderName = DynamicModuleManager.GetDefaultProviderName(moduleName);
      yield return DynamicContentTransfer.GetSingleTypeItemsQuery(DynamicModuleManager.GetManager(parameters.ProviderName), type, parameters.ItemsFilterExpression, parameters.ItemsSortExpression);
    }

    /// <summary>Builds the transferable object.</summary>
    /// <param name="item">The item.</param>
    /// <param name="parameters">The export parameters.</param>
    /// <param name="language">The language to use.</param>
    /// <returns>The transferable object.</returns>
    protected override WrapperObject PreProcessExportObject(
      object item,
      ExportParams parameters,
      string language)
    {
      WrapperObject wrapperObject = base.PreProcessExportObject(item, parameters, language);
      if (wrapperObject != null)
      {
        string moduleName = this.GetModuleName(parameters.TypeName);
        if (!string.IsNullOrEmpty(moduleName))
          wrapperObject.AddProperty("ModuleName", (object) moduleName);
      }
      return wrapperObject;
    }

    /// <summary>Gets the name of the module.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>The module name</returns>
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Not needed")]
    protected string GetModuleName(string itemType) => ModuleBuilderManager.GetModules().SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (m => m.Types)).FirstOrDefault<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.GetFullTypeName().Equals(itemType)))?.ModuleName;

    /// <summary>Modifies the import object.</summary>
    /// <param name="transferableObject">The transferable object.</param>
    /// <param name="parameters">The import parameters.</param>
    /// <returns>The modified transferable object.</returns>
    protected override WrapperObject OnItemImporting(
      WrapperObject transferableObject,
      ImportParams parameters)
    {
      string propertyOrDefault = transferableObject.GetPropertyOrDefault<string>("ModuleName", (string) null);
      if (string.IsNullOrEmpty(parameters.ProviderName) && !string.IsNullOrEmpty(propertyOrDefault))
        parameters.ProviderName = DynamicModuleManager.GetDefaultProviderName(propertyOrDefault);
      return base.OnItemImporting(transferableObject, parameters);
    }

    private static bool IsModuleActive(string moduleName)
    {
      IDynamicModule dynamicModule = ModuleBuilderManager.GetModules().Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Name == moduleName)).SingleOrDefault<IDynamicModule>();
      if (dynamicModule == null)
        throw new ArgumentException("Invalid module name");
      return dynamicModule.Status == DynamicModuleStatus.Active;
    }

    private static IQueryable<object> GetSingleTypeItemsQuery(
      DynamicModuleManager dynamicModuleManager,
      Type type,
      string filterExpression,
      string sortExpression)
    {
      IQueryable<DynamicContent> source = dynamicModuleManager.GetDataItems(type).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => (int) i.Status == 2 && i.Visible));
      if (!string.IsNullOrEmpty(filterExpression))
        source = source.Where<DynamicContent>(filterExpression);
      return (IQueryable<object>) source.OrderBy<DynamicContent>(sortExpression);
    }

    private ExportType GetModuleExportType(IDynamicModule module, bool isExportable)
    {
      ExportType moduleExportType = new ExportType(module.Title, module.Name, false);
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

    private void PopulateDependencies()
    {
      IEnumerable<ExportType> supportedTypes = this.SupportedTypes;
      Queue<ExportType> exportTypeQueue = new Queue<ExportType>(this.SupportedTypes);
      while (exportTypeQueue.Count > 0)
      {
        ExportType exportType = exportTypeQueue.Dequeue();
        foreach (ExportType childType in (IEnumerable<ExportType>) exportType.ChildTypes)
        {
          this.AddDependencies(childType.TypeName, exportType.TypeName);
          exportTypeQueue.Enqueue(childType);
        }
      }
      this.dependenciesPopulated = true;
    }

    private void SupportedTypesCacheExpired(ICacheDependencyHandler caller, Type type, string path)
    {
      lock (DynamicContentTransfer.SupportedTypesCacheSync)
        this.supportedTypes = (IEnumerable<ExportType>) null;
    }
  }
}
