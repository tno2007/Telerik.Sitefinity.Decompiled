// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.ModuleInstaller
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Configuration;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  /// <summary>
  /// This class provides functionality for installing dynamic modules.
  /// </summary>
  internal class ModuleInstaller
  {
    private string transactionName;
    private string providerName;
    private ModuleBuilderManager moduleBuilderManager;
    private IMetadataManager metadataManager;
    private PageManager pageManager;
    private MultisiteManager multisiteManager;
    private DataStructureInstaller dataStructureInstaller;
    private BackendDefinitionInstaller backendDefinitionInstaller;
    private WidgetInstaller widgetInstaller;
    private ModulePagesInstaller modulePagesInstaller;
    private PermissionsInstaller permissionsInstaller;
    internal const string DefaultTransactionName = "Module Installer";
    internal const string OperationName = "Operation";
    internal const string TypesName = "TypesNames";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Install.ModuleInstaller" /> class.
    /// </summary>
    public ModuleInstaller() => this.transactionName = "Module Installer";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Install.ModuleInstaller" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public ModuleInstaller(string providerName, string transactionName)
    {
      this.providerName = providerName;
      this.transactionName = transactionName.IsNullOrEmpty() ? "Module Installer" : transactionName;
    }

    /// <summary>Raised when a module is installed.</summary>
    public static event EventHandler<DynamicModuleEventArgs> ModuleInstalled;

    /// <summary>Raised when a module is uninstalled.</summary>
    public static event EventHandler<DynamicModuleEventArgs> ModuleUninstalled;

    /// <summary>Raised when a module is deleted.</summary>
    public static event EventHandler<DynamicModuleEventArgs> ModuleDeleted;

    /// <summary>Raised when a content type is installed.</summary>
    public static event EventHandler<DynamicModuleTypeEventArgs> ContentTypeInstalled;

    /// <summary>Raised when a content type is updated.</summary>
    public static event EventHandler<DynamicModuleTypeEventArgs> ContentTypeUpdated;

    /// <summary>Raised when a content type is deleted.</summary>
    public static event EventHandler<DynamicModuleTypeEventArgs> ContentTypeDeleted;

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> to be used when installing
    /// the module.
    /// </summary>
    private ModuleBuilderManager ModuleBuilderMngr
    {
      get
      {
        if (this.moduleBuilderManager == null)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager(this.providerName, this.transactionName);
        return this.moduleBuilderManager;
      }
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.IMetadataManager" /> to be used when installing the
    /// module.
    /// </summary>
    private IMetadataManager MetadataMngr
    {
      get
      {
        if (this.metadataManager == null)
          this.metadataManager = (IMetadataManager) MetadataManager.GetManager(string.Empty, this.transactionName);
        return this.metadataManager;
      }
    }

    /// <summary>Gets the page manager.</summary>
    private PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager(string.Empty, this.transactionName);
        return this.pageManager;
      }
    }

    /// <summary>Gets the multi site manager.</summary>
    private MultisiteManager MultisiteManager
    {
      get
      {
        if (this.multisiteManager == null)
          this.multisiteManager = MultisiteManager.GetManager(string.Empty, this.transactionName);
        return this.multisiteManager;
      }
    }

    /// <summary>
    /// Gets the configured instance of the <see cref="P:Telerik.Sitefinity.DynamicModules.Builder.Install.ModuleInstaller.DataStructureInstaller" /> type used
    /// for working with the data structure of the dynamic module.
    /// </summary>
    private DataStructureInstaller DataStructureInstaller
    {
      get
      {
        if (this.dataStructureInstaller == null)
          this.dataStructureInstaller = new DataStructureInstaller(this.MetadataMngr, this.ModuleBuilderMngr);
        return this.dataStructureInstaller;
      }
    }

    /// <summary>Gets the backend definition installer.</summary>
    private BackendDefinitionInstaller BackendDefinitionInstaller
    {
      get
      {
        if (this.backendDefinitionInstaller == null)
          this.backendDefinitionInstaller = new BackendDefinitionInstaller(this.ModuleBuilderMngr);
        return this.backendDefinitionInstaller;
      }
    }

    /// <summary>Gets the widget installer.</summary>
    private WidgetInstaller WidgetInstaller
    {
      get
      {
        if (this.widgetInstaller == null)
          this.widgetInstaller = new WidgetInstaller(this.PageManager, this.ModuleBuilderMngr);
        return this.widgetInstaller;
      }
    }

    /// <summary>Gets the module pages installer.</summary>
    private ModulePagesInstaller ModulePagesInstaller
    {
      get
      {
        if (this.modulePagesInstaller == null)
          this.modulePagesInstaller = new ModulePagesInstaller(this.transactionName);
        return this.modulePagesInstaller;
      }
    }

    private PermissionsInstaller PermissionsInstaller
    {
      get
      {
        if (this.permissionsInstaller == null)
          this.permissionsInstaller = new PermissionsInstaller(this.ModuleBuilderMngr);
        return this.permissionsInstaller;
      }
    }

    /// <summary>Gets the name of the transaction.</summary>
    /// <value>The name of the transaction.</value>
    public string TransactionName => this.transactionName;

    /// <summary>
    /// This method installs the dynamic module in Sitefinity.
    /// </summary>
    /// <param name="moduleId">Id of the module to be installed.</param>
    /// <param name="commitTransaction">The commit transaction.</param>
    /// <param name="configureForDefaultSite">if set to <c>true</c> [configure for default site].</param>
    public void InstallModule(Guid moduleId, bool commitTransaction = true, bool configureForDefaultSite = true) => this.InstallModule(this.ModuleBuilderMngr.GetDynamicModule(moduleId), commitTransaction, configureForDefaultSite);

    /// <summary>
    /// This method installs the dynamic module in Sitefinity.
    /// </summary>
    /// <param name="module">Instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> that represents the module to be installed.</param>
    /// <param name="commitTransaction">The commit transaction.</param>
    /// <param name="configureForDefaultSite">if set to <c>true</c> [configure for default site].</param>
    public void InstallModule(
      DynamicModule module,
      bool commitTransaction = true,
      bool configureForDefaultSite = true)
    {
      if (module.Types == null)
        this.ModuleBuilderMngr.LoadDynamicModuleGraph(module);
      try
      {
        if (module.Status == DynamicModuleStatus.NotInstalled)
        {
          this.ModulePagesInstaller.CreateModulePages(module, this.PageManager);
          this.DataStructureInstaller.CreateDataStructure(module);
          this.BackendDefinitionInstaller.CreateDefaultBackendTypeDefinitionsIfNotExisting(module);
          this.WidgetInstaller.Install(module, configureForDefaultSite);
          ModuleInstallerHelper.RegisterWorkflowsIfNotExisting(module);
          this.PermissionsInstaller.InstallModulePermissions(module);
          this.UpdateModuleResources(module);
          module.Status = DynamicModuleStatus.Active;
        }
        else if (module.Status == DynamicModuleStatus.Inactive)
          module.Status = DynamicModuleStatus.Active;
        if (configureForDefaultSite)
          this.ConfigureForDefaultSite(module);
        if (commitTransaction)
          TransactionManager.CommitTransaction(this.transactionName);
        SiteMapBase.cache.Flush();
        ModuleBuilderManager.ClearModulesCache();
        foreach (IDynamicModuleType type in ModuleBuilderManager.GetModules().Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Id == module.Id)).First<IDynamicModule>().Types)
          this.BackendDefinitionInstaller.GenerateDefaultContentViewDefinition(module, type);
        this.ModuleBuilderMngr.InvalidateFieldPermissionsCheckCache();
        ModuleBuilderModule.RegisterDynamicModule((IDynamicModule) module);
        ModuleBuilderModule.LoadModule((IDynamicModule) module);
        ModuleInstaller.RaiseModuleInstalled(module);
        this.DispatchMessage(module.Id.ToString(), "InvalidateDynamicModuleCacheKey", "DynamicModuleInstall");
        BackendDefinitionInstaller.InvalidateBackendDefinitions();
      }
      catch (Exception ex)
      {
        this.HandleException(ex, commitTransaction);
      }
      ModuleInstallerHelper.InstallSearch(module);
    }

    /// <summary>Installs new content type in existing module.</summary>
    /// <param name="module">The module.</param>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="commitTransaction">if set to <c>true</c> [commit transaction].</param>
    public void InstallContentType(
      DynamicModule module,
      DynamicModuleType moduleType,
      bool commitTransaction = true,
      bool installConfigs = true)
    {
      if (moduleType.Fields == null)
        this.ModuleBuilderMngr.LoadDynamicModuleTypeGraph(moduleType, false);
      try
      {
        this.DataStructureInstaller.CreateDataStructure(moduleType);
        bool showInNavigation = moduleType.ParentModuleTypeId == Guid.Empty || moduleType == null;
        Guid pageId = moduleType.PageId == Guid.Empty ? PageManager.GetManager().Provider.GetNewGuid() : moduleType.PageId;
        moduleType.PageId = pageId;
        this.PermissionsInstaller.InstallModuleTypePermissions(module, moduleType);
        this.ModuleBuilderMngr.InvalidateFieldPermissionsCheckCache();
        ModuleInstaller.UpdateModuleResources(moduleType);
        this.ModulePagesInstaller.CreateContentTypePage(pageId, module, moduleType, showInNavigation);
        if (installConfigs)
          this.InstallModuleTypeConfigurations(module, moduleType);
        if (commitTransaction)
          TransactionManager.CommitTransaction(this.transactionName);
        ModuleBuilderManager.ClearModulesCache();
        this.BackendDefinitionInstaller.GenerateDefaultContentViewDefinition(module, (IDynamicModuleType) moduleType);
        ModuleBuilderModule.RegisterDynamicModuleType((IDynamicModuleType) moduleType);
        ModuleBuilderModule.LoadModule((IDynamicModule) module);
        EventHandler<DynamicModuleTypeEventArgs> contentTypeInstalled = ModuleInstaller.ContentTypeInstalled;
        if (contentTypeInstalled != null)
          contentTypeInstalled((object) null, new DynamicModuleTypeEventArgs(module.Name, moduleType.GetFullTypeName(), this.transactionName));
        this.DispatchMessage(module.Id.ToString(), "InvalidateDynamicModuleCacheKey", "DynamicModuleTypeInstall");
      }
      catch (Exception ex)
      {
        this.HandleException(ex, commitTransaction);
      }
      ModuleInstallerHelper.InstallSearch((IDynamicModuleType) moduleType, true);
    }

    /// <summary>
    /// Generates default configurations for provided module types
    /// </summary>
    public void InstallModuleTypeConfigurations(DynamicModule module, DynamicModuleType moduleType)
    {
      if (moduleType.Fields == null)
        this.ModuleBuilderMngr.LoadDynamicModuleTypeGraph(moduleType, false);
      this.BackendDefinitionInstaller.CreateDefaultBackendTypeDefinitions(module, moduleType);
      Guid parentModuleTypeId = moduleType.ParentModuleTypeId;
      if (!moduleType.ParentModuleTypeId.Equals(Guid.Empty))
        this.ReinstallBackendDefinition(module, moduleType.ParentModuleTypeId);
      this.WidgetInstaller.Install(module, moduleType);
      ModuleInstallerHelper.RegisterWorkflows(moduleType);
    }

    /// <summary>Updates module type's configurations.</summary>
    public void UpdateModuleTypeConfigurations(
      DynamicModule module,
      DynamicModuleType moduleType,
      string moduleTypeDisplayName = null,
      Guid originalParentTypeId = default (Guid),
      bool forceUpdateToolboxItem = false)
    {
      this.ReinstallBackendDefinitions(module, moduleType, moduleTypeDisplayName, originalParentTypeId);
      this.UpdateToolboxItems(module, moduleType, originalParentTypeId, forceUpdateToolboxItem);
    }

    /// <summary>
    /// Validates master and detail template keys for all toolbox items for specified moduleType
    /// </summary>
    public void ValidateToolboxItemTemplateKeys(DynamicModule module, DynamicModuleType moduleType) => this.WidgetInstaller.ValidateToolboxItemTemplateKeys(module, moduleType);

    /// <summary>
    /// Moves module group page under 'Types of content' if there is only one root content type
    /// </summary>
    public void ValidateModuleGroupPagePosition(DynamicModule module) => this.ModulePagesInstaller.ValidateModuleGroupPagePosition(module);

    /// <summary>Updates the already created module.</summary>
    /// <param name="module">Instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> that should be updated.</param>
    public void UpdateModuleType(
      DynamicModule module,
      DynamicModuleType moduleType,
      string moduleTypeDisplayName = null,
      Guid originalParentTypeId = default (Guid),
      bool updateConfigs = true,
      Guid rootPageId = default (Guid),
      int rootTypesCount = 0)
    {
      try
      {
        this.DataStructureInstaller.UpdateDataStructure(module, moduleType);
        this.RemoveDeletedDynamicModuleTypeFields(moduleType);
        if (updateConfigs)
          this.UpdateModuleTypeConfigurations(module, moduleType, moduleTypeDisplayName, originalParentTypeId);
        ModuleInstaller.UpdateModuleResources(moduleType);
        Guid parentModuleTypeId = moduleType.ParentModuleTypeId;
        bool noParent = moduleType.ParentModuleTypeId == Guid.Empty;
        string displayName = moduleType.DisplayName;
        rootTypesCount = rootTypesCount != 0 ? rootTypesCount : ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == Guid.Empty)).Count<DynamicModuleType>();
        rootPageId = rootPageId != new Guid() ? rootPageId : ModulePagesInstaller.GetRootPageId(module);
        this.ModulePagesInstaller.UpdateModuleTypePage(module, moduleType, rootPageId, displayName, rootTypesCount, noParent);
        this.ModuleBuilderMngr.InvalidateFieldPermissionsCheckCache();
        this.ModuleBuilderMngr.InvalidateModuleNamePerTypeCache();
        ModuleBuilderManager.ClearModulesCache();
        ModuleBuilderModule.UpdateDynamicModuleType((IDynamicModuleType) moduleType);
        string fullTypeName = moduleType.GetFullTypeName();
        EventHandler<DynamicModuleTypeEventArgs> contentTypeUpdated = ModuleInstaller.ContentTypeUpdated;
        if (contentTypeUpdated == null)
          return;
        contentTypeUpdated((object) null, new DynamicModuleTypeEventArgs(module.Name, fullTypeName, this.transactionName));
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.ModuleBuilder);
        throw;
      }
    }

    /// <summary>
    /// Delete all the dynamic fields that are marked for deletion
    /// </summary>
    public void RemoveDeletedDynamicModuleTypeFields(DynamicModuleType moduleType)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ModuleInstaller.\u003C\u003Ec__DisplayClass48_0 cDisplayClass480 = new ModuleInstaller.\u003C\u003Ec__DisplayClass48_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass480.moduleType = moduleType;
      // ISSUE: reference to a compiler-generated field
      foreach (DynamicModuleField dynamicModuleField in ((IEnumerable<DynamicModuleField>) cDisplayClass480.moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.SpecialType == FieldSpecialType.None)))
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ModuleInstaller.\u003C\u003Ec__DisplayClass48_1 cDisplayClass481 = new ModuleInstaller.\u003C\u003Ec__DisplayClass48_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass481.CS\u0024\u003C\u003E8__locals1 = cDisplayClass480;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass481.dynamicField = dynamicModuleField;
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass481.dynamicField.FieldStatus == DynamicModuleFieldStatus.Removed)
        {
          // ISSUE: reference to a compiler-generated field
          this.ModuleBuilderMngr.Delete(cDisplayClass481.dynamicField);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (cDisplayClass481.dynamicField.FieldType == FieldType.Address || cDisplayClass481.dynamicField.FieldType == FieldType.RelatedMedia || cDisplayClass481.dynamicField.FieldType == FieldType.RelatedData)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            ModuleInstaller.\u003C\u003Ec__DisplayClass48_2 cDisplayClass482 = new ModuleInstaller.\u003C\u003Ec__DisplayClass48_2();
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            cDisplayClass482.section = this.ModuleBuilderMngr.GetFieldsBackendSection(cDisplayClass481.dynamicField.ParentSectionId);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated method
            if (cDisplayClass482.section != null && ((IEnumerable<DynamicModuleField>) cDisplayClass481.CS\u0024\u003C\u003E8__locals1.moduleType.Fields).Where<DynamicModuleField>(new Func<DynamicModuleField, bool>(cDisplayClass482.\u003CRemoveDeletedDynamicModuleTypeFields\u003Eb__1)).Count<DynamicModuleField>() == 0)
            {
              // ISSUE: reference to a compiler-generated field
              this.ModuleBuilderMngr.Delete(cDisplayClass482.section);
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated method
              FieldsBackendSection fieldsBackendSection = ((IEnumerable<FieldsBackendSection>) cDisplayClass481.CS\u0024\u003C\u003E8__locals1.moduleType.Sections).FirstOrDefault<FieldsBackendSection>(new Func<FieldsBackendSection, bool>(cDisplayClass481.\u003CRemoveDeletedDynamicModuleTypeFields\u003Eb__2));
              if (fieldsBackendSection != null)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                List<FieldsBackendSection> list = ((IEnumerable<FieldsBackendSection>) cDisplayClass481.CS\u0024\u003C\u003E8__locals1.moduleType.Sections).ToList<FieldsBackendSection>();
                list.Remove(fieldsBackendSection);
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                cDisplayClass481.CS\u0024\u003C\u003E8__locals1.moduleType.Sections = list.ToArray();
              }
            }
          }
          // ISSUE: reference to a compiler-generated field
          if (cDisplayClass481.dynamicField.FieldType == FieldType.RelatedMedia)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            RelatedDataHelper.DeleteFieldRelations(this.ModuleBuilderMngr.Provider.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager, cDisplayClass481.CS\u0024\u003C\u003E8__locals1.moduleType.GetFullTypeName(), cDisplayClass481.dynamicField.Name);
          }
          // ISSUE: reference to a compiler-generated field
          if (cDisplayClass481.dynamicField.FieldType == FieldType.Classification)
          {
            TaxonomyManager relatedManager = this.ModuleBuilderMngr.Provider.GetRelatedManager<TaxonomyManager>(string.Empty);
            IQueryable<TaxonomyStatistic> statistics = relatedManager.GetStatistics();
            ParameterExpression parameterExpression;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method reference
            // ISSUE: method reference
            // ISSUE: field reference
            // ISSUE: method reference
            // ISSUE: method reference
            Expression<Func<TaxonomyStatistic, bool>> predicate = Expression.Lambda<Func<TaxonomyStatistic, bool>>((Expression) Expression.AndAlso((Expression) Expression.Equal(s.DataItemType, (Expression) Expression.Call(cDisplayClass481.CS\u0024\u003C\u003E8__locals1.moduleType, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (DynamicModuleType.GetFullTypeName)), Array.Empty<Expression>())), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaxonomyStatistic.get_TaxonomyId))), (Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass481, typeof (ModuleInstaller.\u003C\u003Ec__DisplayClass48_1)), FieldInfo.GetFieldFromHandle(__fieldref (ModuleInstaller.\u003C\u003Ec__DisplayClass48_1.dynamicField))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (DynamicModuleField.get_ClassificationId))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality)))), parameterExpression);
            foreach (TaxonomyStatistic statistic in (IEnumerable<TaxonomyStatistic>) statistics.Where<TaxonomyStatistic>(predicate))
              relatedManager.DeleteStatistic(statistic);
          }
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (cDisplayClass481.CS\u0024\u003C\u003E8__locals1.moduleType.CheckFieldPermissions && (cDisplayClass481.dynamicField.Permissions == null || cDisplayClass481.dynamicField.Permissions.Count == 0))
          {
            // ISSUE: reference to a compiler-generated field
            this.PermissionsInstaller.InstallModuleTypeFieldPermissions(cDisplayClass481.dynamicField);
          }
        }
      }
    }

    /// <summary>
    /// This method uninstalls the dynamic module in Sitefinity.
    /// </summary>
    /// <param name="module">
    /// Instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> that represents the module to be uninstalled.
    /// </param>
    public void UninstallModule(DynamicModule module, bool commitTransaction = true)
    {
      this.ModuleBuilderMngr.LoadDynamicModuleGraph(module);
      List<string> contentTypeNames = ModuleInstallerHelper.GetContentTypeNames(module);
      string name = module.Name;
      try
      {
        module.Status = DynamicModuleStatus.Inactive;
        if (commitTransaction)
          TransactionManager.CommitTransaction(this.transactionName);
      }
      catch (Exception ex)
      {
        this.HandleException(ex, commitTransaction);
      }
      ModuleBuilderModule.UnregisterDynamicModule((IDynamicModule) module);
      ModuleBuilderModule.UnloadModule(name, contentTypeNames);
      ModuleInstallerHelper.UninstallSearch(contentTypeNames);
      SiteMapBase.cache.Flush();
      this.ModuleBuilderMngr.InvalidateFieldPermissionsCheckCache();
      this.ModuleBuilderMngr.InvalidateModuleNamePerTypeCache();
      ModuleInstallerHelper.RemoveModulePublishingPoints(contentTypeNames, "SearchPublishingProvider");
      ModuleInstaller.RaiseModuleUninstalled(name, contentTypeNames);
      this.DispatchMessage(name, "InvalidateDynamicModuleCacheKey", "DynamicModuleUninstall", JsonConvert.SerializeObject((object) contentTypeNames));
    }

    /// <summary>This method deletes dynamic module type.</summary>
    public void DeleteModuleType(
      DynamicModule module,
      DynamicModuleType moduleType,
      bool commitTransaction = false,
      bool validateModuleGroupPagePosition = false,
      bool updateParent = false)
    {
      try
      {
        string fullTypeName = moduleType.GetFullTypeName();
        if (module.Status != DynamicModuleStatus.NotInstalled)
        {
          this.DeleteInstalledModuleTypeStructure(module, moduleType);
          List<DynamicModuleType> dynamicModuleTypeList = new List<DynamicModuleType>((IEnumerable<DynamicModuleType>) module.Types);
          dynamicModuleTypeList.Remove(moduleType);
          module.Types = dynamicModuleTypeList.ToArray();
        }
        ModuleBuilderModule.UnregisterDynamicModuleType((IDynamicModuleType) moduleType);
        this.DeleteModuleTypeRegistration(moduleType);
        this.ModuleBuilderMngr.InvalidateFieldPermissionsCheckCache();
        this.ModuleBuilderMngr.InvalidateModuleNamePerTypeCache();
        this.WidgetInstaller.UnregisterTemplates(fullTypeName);
        if (validateModuleGroupPagePosition && module.Status != DynamicModuleStatus.NotInstalled)
          this.ValidateModuleGroupPagePosition(module);
        if (updateParent && moduleType.ParentModuleTypeId != Guid.Empty)
        {
          DynamicModuleType type = ((IEnumerable<DynamicModuleType>) module.Types).FirstOrDefault<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == moduleType.ParentModuleTypeId));
          if (type != null)
          {
            UpdateContentTypeOperationSettings operationSettings = new UpdateContentTypeOperationSettings();
            operationSettings.LoadDynamicModuleGraph = false;
            operationSettings.KeepIds = true;
            operationSettings.UpdateWidgetTemplates = false;
            operationSettings.Transaction = this.transactionName;
            operationSettings.UpdateModuleConfiguration = true;
            UpdateContentTypeOperationSettings settings = operationSettings;
            ModuleBuilderHelper.UpdateType(module, type, settings);
          }
        }
        if (commitTransaction)
          TransactionManager.CommitTransaction(this.transactionName);
        ModuleBuilderModule.LoadModule((IDynamicModule) module);
        EventHandler<DynamicModuleTypeEventArgs> contentTypeDeleted = ModuleInstaller.ContentTypeDeleted;
        if (contentTypeDeleted != null)
          contentTypeDeleted((object) null, new DynamicModuleTypeEventArgs(module.Name, fullTypeName, this.transactionName));
        this.DispatchMessage(module.Id.ToString(), "InvalidateDynamicModuleCacheKey", "DynamicModuleTypeDelete");
      }
      catch (Exception ex)
      {
        this.HandleException(ex, commitTransaction);
      }
    }

    /// <summary>Deletes the module type data.</summary>
    public void DeleteModuleTypeData(
      DynamicModule module,
      DynamicModuleType moduleType,
      bool commitTransaction = true)
    {
      if (module.Status == DynamicModuleStatus.NotInstalled)
        return;
      try
      {
        this.ModuleBuilderMngr.LoadDynamicModuleGraph(module);
        foreach (DataProviderBase allProvider in DynamicModuleManager.GetManager(string.Empty, this.transactionName).GetAllProviders(module.Name))
          DynamicModuleManager.GetManager(allProvider.Name, this.transactionName).DeleteDataItems(TypeResolutionService.ResolveType(moduleType.GetFullTypeName()));
        this.EnsureCorrectProvidersOrder();
      }
      catch (Exception ex)
      {
        this.HandleException(ex, commitTransaction);
      }
    }

    /// <summary>
    /// This method deletes the dynamic module from Sitefinity.
    /// </summary>
    /// <param name="module">The module.</param>
    /// <param name="commitTransaction">if set to <c>true</c> [commit transaction].</param>
    public void DeleteModule(
      DynamicModule module,
      DeleteModuleContext settings,
      bool commitTransaction = true)
    {
      SystemManager.CurrentContext.MultisiteContext?.GetSites();
      this.ModuleBuilderMngr.LoadDynamicModuleGraph(module);
      List<string> contentTypeNames = ModuleInstallerHelper.GetContentTypeNames(module);
      string name = module.Name;
      List<Type> typeList = new List<Type>();
      try
      {
        if (module.Status != DynamicModuleStatus.NotInstalled)
        {
          foreach (DynamicModuleType type1 in module.Types)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            ModuleInstaller.\u003C\u003Ec__DisplayClass52_0 cDisplayClass520 = new ModuleInstaller.\u003C\u003Ec__DisplayClass52_0();
            // ISSUE: reference to a compiler-generated field
            cDisplayClass520.dt = type1;
            // ISSUE: reference to a compiler-generated field
            Type type2 = TypeResolutionService.ResolveType(cDisplayClass520.dt.GetFullTypeName(), false);
            if (type2 != (Type) null)
            {
              typeList.Add(type2);
              TaxonomyManager relatedManager = this.ModuleBuilderMngr.Provider.GetRelatedManager<TaxonomyManager>(string.Empty);
              IQueryable<TaxonomyStatistic> statistics = relatedManager.GetStatistics();
              ParameterExpression parameterExpression;
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method reference
              Expression<Func<TaxonomyStatistic, bool>> predicate = Expression.Lambda<Func<TaxonomyStatistic, bool>>((Expression) Expression.Equal(s.DataItemType, (Expression) Expression.Call(cDisplayClass520.dt, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (DynamicModuleType.GetFullTypeName)), Array.Empty<Expression>())), parameterExpression);
              foreach (TaxonomyStatistic statistic in (IEnumerable<TaxonomyStatistic>) statistics.Where<TaxonomyStatistic>(predicate))
                relatedManager.DeleteStatistic(statistic);
            }
          }
          this.DeleteInstalledModuleStructure(module, settings, contentTypeNames);
        }
        this.DeleteModuleRegistration(module);
        ModuleBuilderModule.UnregisterDynamicModule((IDynamicModule) module);
        ModuleBuilderModule.UnloadModule(name, contentTypeNames);
        if (commitTransaction)
          TransactionManager.CommitTransaction(this.transactionName);
      }
      catch (Exception ex)
      {
        this.HandleException(ex, commitTransaction);
      }
      this.ModuleBuilderMngr.InvalidateFieldPermissionsCheckCache();
      this.ModuleBuilderMngr.InvalidateModuleNamePerTypeCache();
      this.DeleteModuleConfiguration(contentTypeNames, name);
      foreach (Type type in typeList)
        TypeResolutionService.UnregisterType(type);
      ModuleInstaller.RaiseModuleDeleted(name, contentTypeNames);
      this.DispatchMessage(name, "InvalidateDynamicModuleCacheKey", "DynamicModuleDelete", JsonConvert.SerializeObject((object) contentTypeNames));
    }

    /// <summary>Deletes the module data.</summary>
    /// <param name="module">The module.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="commitTransaction">if set to <c>true</c> [commit transaction].</param>
    public void DeleteModuleData(
      DynamicModule module,
      DeleteModuleContext settings,
      bool commitTransaction = true)
    {
      if (module.Status == DynamicModuleStatus.NotInstalled)
        return;
      try
      {
        this.ModuleBuilderMngr.LoadDynamicModuleGraph(module);
        foreach (DataProviderBase allProvider in DynamicModuleManager.GetManager(string.Empty, this.transactionName).GetAllProviders(module.Name))
        {
          DynamicModuleManager manager = DynamicModuleManager.GetManager(allProvider.Name, this.transactionName);
          foreach (DynamicModuleType type in module.Types)
          {
            Type itemType = TypeResolutionService.ResolveType(type.GetFullTypeName(), false);
            if (itemType != (Type) null)
              manager.DeleteDataItems(itemType);
          }
        }
        this.EnsureCorrectProvidersOrder();
        if (!commitTransaction)
          return;
        TransactionManager.CommitTransaction(this.ModuleBuilderMngr.TransactionName);
      }
      catch (Exception ex)
      {
        this.HandleException(ex, commitTransaction);
      }
    }

    internal static void RaiseModuleInstalled(DynamicModule module)
    {
      EventHandler<DynamicModuleEventArgs> moduleInstalled = ModuleInstaller.ModuleInstalled;
      if (moduleInstalled == null)
        return;
      moduleInstalled((object) null, new DynamicModuleEventArgs(module.Name, ModuleInstallerHelper.GetContentTypeNames(module)));
    }

    internal static void RaiseModuleDeleted(string moduleName, List<string> typeNames)
    {
      EventHandler<DynamicModuleEventArgs> moduleDeleted = ModuleInstaller.ModuleDeleted;
      if (moduleDeleted == null)
        return;
      moduleDeleted((object) null, new DynamicModuleEventArgs(moduleName, typeNames));
    }

    internal static void RaiseModuleUninstalled(string moduleName, List<string> typeNames)
    {
      EventHandler<DynamicModuleEventArgs> moduleUninstalled = ModuleInstaller.ModuleUninstalled;
      if (moduleUninstalled == null)
        return;
      moduleUninstalled((object) null, new DynamicModuleEventArgs(moduleName, typeNames));
    }

    /// <summary>Gets the name of the transaction.</summary>
    /// <param name="moduleId">The module id.</param>
    public static string GetTransactionName(Guid moduleId) => string.Format("{0} {1}", (object) "Module Installer", (object) moduleId.ToString());

    /// <summary>Gets the name of the transaction.</summary>
    /// <param name="module">The module.</param>
    public static string GetTransactionName(DynamicModule module) => ModuleInstaller.GetTransactionName(module.Id);

    public void DeleteModuleTypeConfiguration(string contentTypeName)
    {
      CacheDependency.Notify(new object[1]
      {
        (object) Config.Get<DynamicModulesConfig>()
      });
      this.BackendDefinitionInstaller.DeleteBackendTypeDefinitions(contentTypeName);
      this.WidgetInstaller.Uninstall(contentTypeName);
      ModuleInstallerHelper.UnregisterWorkflows(contentTypeName);
      ModuleInstallerHelper.UninstallSearch(contentTypeName);
      ModuleInstallerHelper.RemoveModulePublishingPoints(contentTypeName, "SearchPublishingProvider");
      ModuleInstallerHelper.RemoveModulePublishingPoints(contentTypeName, "OAPublishingProvider");
    }

    private void DeleteModuleConfiguration(List<string> contentTypeNames, string moduleName)
    {
      foreach (string contentTypeName in contentTypeNames)
        this.DeleteModuleTypeConfiguration(contentTypeName);
      ModuleInstallerHelper.RemoveModuleProviders(moduleName);
    }

    private void DeleteInstalledModuleTypeStructure(
      DynamicModule module,
      DynamicModuleType moduleType)
    {
      this.DataStructureInstaller.DeleteModuleTypeStructure(moduleType);
      this.DeleteModuleTypePersistentData(moduleType);
      this.ModulePagesInstaller.DeleteModuleTypePage(module, moduleType);
    }

    private void DeleteInstalledModuleStructure(
      DynamicModule module,
      DeleteModuleContext context,
      List<string> contentTypeNames)
    {
      this.MultisiteManager.DeleteSiteDataSourceLinks(module.Name);
      this.DataStructureInstaller.DeleteDataStructure(module);
      this.DeleteModulePersistentData(module);
      this.WidgetInstaller.UnregisterTemplates(contentTypeNames);
      this.ModulePagesInstaller.DeleteModulePage(module);
    }

    private void DeleteModulePersistentData(DynamicModule module)
    {
      foreach (DynamicModuleType type in module.Types)
        this.DeleteModuleTypePersistentData(type);
    }

    private void DeleteModuleTypePersistentData(DynamicModuleType moduleType)
    {
      foreach (DynamicModuleField field in moduleType.Fields)
        this.ModuleBuilderMngr.Delete(field);
      this.ModuleBuilderMngr.Delete(moduleType);
    }

    private void DeleteModuleRegistration(DynamicModule module) => this.ModuleBuilderMngr.Delete(module);

    private void DeleteModuleTypeRegistration(DynamicModuleType moduleType) => this.ModuleBuilderMngr.Delete(moduleType);

    private void HandleException(Exception ex, bool rollbackTransaction)
    {
      if (rollbackTransaction)
        TransactionManager.RollbackTransaction(this.transactionName);
      Exceptions.HandleException(ex, ExceptionPolicyName.ModuleBuilder);
      throw ex;
    }

    private void ReinstallBackendDefinitions(
      DynamicModule module,
      DynamicModuleType moduleType,
      string moduleTypeDisplayName,
      Guid originalParentTypeId)
    {
      this.BackendDefinitionInstaller.ReinstallBackendGridDefinitions(module, moduleType, moduleTypeDisplayName);
      if (originalParentTypeId != moduleType.ParentModuleTypeId)
      {
        this.ReinstallBackendDefinition(module, moduleType.ParentModuleTypeId);
        this.ReinstallBackendDefinition(module, originalParentTypeId);
      }
      this.BackendDefinitionInstaller.ReinstallBackendDetailDefinitions(moduleType, moduleTypeDisplayName);
    }

    private void ReinstallBackendDefinition(DynamicModule module, Guid originalParentTypeId)
    {
      if (!(originalParentTypeId != Guid.Empty))
        return;
      DynamicModuleType moduleType = ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == originalParentTypeId)).First<DynamicModuleType>();
      this.BackendDefinitionInstaller.ReinstallBackendGridDefinitions(module, moduleType);
    }

    private void UpdateToolboxItems(
      DynamicModule module,
      DynamicModuleType moduleType,
      Guid originalParentTypeId,
      bool forceUpdate = false)
    {
      if (!(originalParentTypeId != moduleType.ParentModuleTypeId | forceUpdate))
        return;
      this.WidgetInstaller.UpdateToolboxItem(module, moduleType);
      if (originalParentTypeId != Guid.Empty)
      {
        DynamicModuleType dynamicModuleType = ((IEnumerable<DynamicModuleType>) module.Types).FirstOrDefault<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == originalParentTypeId));
        this.WidgetInstaller.UpdateToolboxItem(module, dynamicModuleType);
      }
      if (!(moduleType.ParentModuleTypeId != Guid.Empty))
        return;
      this.WidgetInstaller.UpdateToolboxItem(module, moduleType.ParentModuleType);
    }

    private void UpdateModuleResources(DynamicModule module)
    {
      if (module.Types == null)
        this.ModuleBuilderMngr.LoadDynamicModuleGraph(module);
      foreach (DynamicModuleType type in module.Types)
        ModuleInstaller.UpdateModuleResources(type);
    }

    private static void UpdateModuleResources(DynamicModuleType type)
    {
      HashSet<CultureInfo> cultureInfoSet = new HashSet<CultureInfo>();
      foreach (CultureInfo frontendLanguage in AppSettings.CurrentSettings.DefinedFrontendLanguages)
        cultureInfoSet.Add(frontendLanguage);
      foreach (CultureInfo definedBackendLanguage in AppSettings.CurrentSettings.DefinedBackendLanguages)
        cultureInfoSet.Add(definedBackendLanguage);
      cultureInfoSet.Add(CultureInfo.InvariantCulture);
      ResourceManager manager = Res.GetManager();
      foreach (DynamicModuleField field in type.Fields)
      {
        if (field.FieldType == FieldType.Choices)
        {
          foreach (XElement element in XDocument.Parse(field.Choices.Trim()).Root.Elements((XName) "choice"))
          {
            string input = element.Attribute((XName) "text").Value;
            if (ResourceParserHelper.IsResourceRegex.IsMatch(input))
            {
              string classId = ResourceParserHelper.ResourceFileNameRegex.Match(input).Groups[1].Value.Trim();
              string key = ResourceParserHelper.ResourceKeyRegex.Match(input).Groups[1].Value.Trim();
              foreach (CultureInfo culture in cultureInfoSet)
              {
                if (manager.GetResources(culture, classId).SingleOrDefault<ResourceEntry>((Expression<Func<ResourceEntry, bool>>) (ke => key.Equals(ke.Key, StringComparison.InvariantCultureIgnoreCase))) == null)
                {
                  manager.AddItem(culture, classId, key, key, string.Empty);
                  manager.SaveChanges();
                }
              }
            }
          }
        }
      }
    }

    private void ConfigureForDefaultSite(DynamicModule module)
    {
      if (!SystemManager.CurrentContext.IsOneSiteMode)
        return;
      Site site = this.MultisiteManager.GetSites().First<Site>();
      if (site.SiteDataSourceLinks.Any<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (link => link.DataSource.Name == module.Name && link.Site.Id == site.Id)))
        return;
      string defaultProviderName = ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName();
      SiteDataSourceLink siteDataSourceLink = this.MultisiteManager.CreateSiteDataSourceLink();
      siteDataSourceLink.DataSource = this.MultisiteManager.GetOrCreateDataSource(module.Name, defaultProviderName, site.Name);
      siteDataSourceLink.IsDefault = true;
      site.SiteDataSourceLinks.Add(siteDataSourceLink);
    }

    /// <summary>
    /// This method ensures that dynamic module providers will be flushed before module builder providers.
    /// </summary>
    private void EnsureCorrectProvidersOrder()
    {
      TransactionManager.Connections source;
      if (!TransactionManager.GetTransactions().TryGetValue(this.TransactionName, out source))
        return;
      TransactionManager.Wrapper baseItem = source.FirstOrDefault<TransactionManager.Wrapper>((Func<TransactionManager.Wrapper, bool>) (w => w.Provider is ModuleBuilderDataProvider));
      if (baseItem == null)
        return;
      foreach (TransactionManager.Wrapper itemToMove in source.Where<TransactionManager.Wrapper>((Func<TransactionManager.Wrapper, bool>) (w => w.Provider is DynamicModuleDataProvider)).ToArray<TransactionManager.Wrapper>())
        source.MoveBefore(baseItem, itemToMove);
    }

    private void DispatchMessage(
      string moduleId,
      string key,
      string operationKey,
      string typeNames = null)
    {
      SystemMessageBase msg = new SystemMessageBase()
      {
        MessageData = moduleId,
        Key = key
      };
      msg.AdditionalInfo.Add("Operation", operationKey);
      if (!string.IsNullOrEmpty(typeNames))
        msg.AdditionalInfo.Add("TypesNames", typeNames);
      SystemMessageDispatcher.QueueSystemMessage(msg);
    }
  }
}
