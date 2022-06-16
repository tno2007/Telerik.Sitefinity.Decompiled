// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.Services;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  internal class ModuleBuilderHelper
  {
    /// <summary>Saves the type of the content.</summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="context">The context.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="updateWidgetTemplates">The update widget templates.</param>
    /// <returns></returns>
    internal static ContentTypeContext SaveContentType(
      Guid moduleId,
      ContentTypeContext context,
      string provider,
      bool updateWidgetTemplates)
    {
      string transactionName = context != null ? ModuleInstaller.GetTransactionName(context.ContentTypeId) : throw new ArgumentNullException("contentType");
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(provider, transactionName);
      if (!manager.ModuleExists(context.ModuleId))
      {
        context = !ModuleBuilderHelper.ContainsFieldsWithEqualNames(context) ? manager.CreateModuleWithContentType(context) : throw new ArgumentException("Cannot have fields with same names.");
        TransactionManager.CommitTransaction(transactionName);
      }
      else if (context.ContentTypeId != Guid.Empty)
      {
        UpdateContentTypeOperationSettings operationSettings = new UpdateContentTypeOperationSettings();
        operationSettings.LoadDynamicModuleGraph = true;
        operationSettings.UpdateWidgetTemplates = updateWidgetTemplates;
        operationSettings.UpdateModuleConfiguration = true;
        operationSettings.Transaction = transactionName;
        UpdateContentTypeOperationSettings settings = operationSettings;
        DynamicModule dynamicModule = manager.GetDynamicModule(moduleId);
        UpdateContentTypeOperationResult updateResult = ModuleBuilderHelper.UpdateExistingContentType(provider, dynamicModule, context, settings);
        TransactionManager.CommitTransaction(transactionName);
        ModuleBuilderHelper.UpdateContentTypeParentItems(manager, dynamicModule, updateResult, transactionName);
        SystemManager.ClearCurrentTransactions();
      }
      else
      {
        AddContentTypeOperationSettings settings = new AddContentTypeOperationSettings()
        {
          KeepIds = false,
          Transaction = ModuleInstaller.GetTransactionName(context.ContentTypeId),
          CommitTransaction = true
        };
        DynamicModule dynamicModule = manager.GetDynamicModule(moduleId);
        ModuleBuilderHelper.AddNewContentType(provider, dynamicModule, context, settings);
      }
      return context;
    }

    /// <summary>
    /// Update item's parent. Commits the transaction after items are updated for each data provider.
    /// </summary>
    internal static void UpdateContentTypesParentItems(
      ModuleBuilderManager moduleBuilderTransactionMgr,
      DynamicModule dynamicModule,
      IList<UpdateContentTypeOperationResult> updateResults,
      string transactionName)
    {
      ModuleBuilderManager manager1 = ModuleBuilderManager.GetManager(string.Empty, transactionName);
      foreach (string providerName in DynamicModuleManager.GetManager(string.Empty, transactionName).GetProviderNames(ProviderBindingOptions.NoFilter))
      {
        DynamicModuleManager manager2 = DynamicModuleManager.GetManager(providerName, transactionName);
        foreach (UpdateContentTypeOperationResult updateResult1 in (IEnumerable<UpdateContentTypeOperationResult>) updateResults)
        {
          UpdateContentTypeOperationResult updateResult = updateResult1;
          DynamicModuleType dynamicType = ((IEnumerable<DynamicModuleType>) dynamicModule.Types).FirstOrDefault<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == updateResult.ModuleTypeId));
          if (updateResult.UpdateContentTypeParent)
            manager2.MoveDataItemsToDefaultParentItem(dynamicType);
          else if (updateResult.UpdateSelfReferencing)
          {
            string systemParentType = dynamicType.ParentModuleType == null ? (string) null : dynamicType.ParentModuleType.GetFullTypeName();
            string fullTypeName = dynamicType.GetFullTypeName();
            Guid newParentTypeId = updateResult.NewParentTypeId;
            string typeName = fullTypeName;
            string transactionName1 = transactionName;
            ModuleBuilderManager moduleBuilderManager = moduleBuilderTransactionMgr;
            DynamicModuleManager dynamicModuleManager = manager2;
            ModuleBuilderHelper.UpdateSelfReferencingItems(systemParentType, newParentTypeId, typeName, transactionName1, moduleBuilderManager, dynamicModuleManager);
          }
        }
        TransactionManager.FlushTransaction(transactionName);
        foreach (UpdateContentTypeOperationResult updateResult2 in (IEnumerable<UpdateContentTypeOperationResult>) updateResults)
        {
          UpdateContentTypeOperationResult updateResult = updateResult2;
          if (updateResult.UpdateContentTypeParent)
          {
            DynamicModuleType dynamicModuleType = ((IEnumerable<DynamicModuleType>) dynamicModule.Types).FirstOrDefault<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == updateResult.ModuleTypeId));
            Type itemType = manager1.ResolveDynamicClrType(dynamicModuleType.GetFullTypeName());
            manager2.RecompileDataItemsUrls(itemType);
          }
        }
        TransactionManager.CommitTransaction(transactionName);
      }
    }

    /// <summary>
    /// Update item's parent. Commits the transaction after items are updated for each data provider.
    /// </summary>
    internal static void UpdateContentTypeParentItems(
      ModuleBuilderManager moduleBuilderTransactionMgr,
      DynamicModule dynamicModule,
      UpdateContentTypeOperationResult updateResult,
      string transactionName)
    {
      DynamicModuleType moduleType = ((IEnumerable<DynamicModuleType>) dynamicModule.Types).FirstOrDefault<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == updateResult.ModuleTypeId));
      if (moduleType == null)
        return;
      if (updateResult.UpdateContentTypeParent)
      {
        ModuleBuilderHelper.UpdateContentTypeParent(moduleType, transactionName);
      }
      else
      {
        if (!updateResult.UpdateSelfReferencing)
          return;
        string systemParentType = moduleType.ParentModuleType == null ? (string) null : moduleType.ParentModuleType.GetFullTypeName();
        string fullTypeName = moduleType.GetFullTypeName();
        DynamicModuleManager manager = DynamicModuleManager.GetManager(string.Empty, transactionName);
        Guid newParentTypeId = updateResult.NewParentTypeId;
        string typeName = fullTypeName;
        string transactionName1 = transactionName;
        ModuleBuilderManager moduleBuilderManager = moduleBuilderTransactionMgr;
        DynamicModuleManager dynamicModuleManager = manager;
        ModuleBuilderHelper.UpdateSelfReferencingItems(systemParentType, newParentTypeId, typeName, transactionName1, moduleBuilderManager, dynamicModuleManager);
      }
    }

    /// <summary>Adds the new type of the content.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    internal static void AddNewContentType(
      string provider,
      DynamicModule dynamicModule,
      ContentTypeContext context,
      AddContentTypeOperationSettings settings,
      Dictionary<string, Guid> defaultSectionIds = null,
      bool createConfigs = true)
    {
      string transaction = settings.Transaction;
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(provider, transaction);
      manager.LoadDynamicModuleGraph(dynamicModule);
      if (((IEnumerable<DynamicModuleType>) dynamicModule.Types).Select<DynamicModuleType, string>((Func<DynamicModuleType, string>) (x => x.TypeName.ToLower())).Contains<string>(context.ContentTypeItemName.ToLower()))
        throw new ArgumentException(string.Format("Type with developer name '{0}' already exists in this module.", (object) context.ContentTypeItemName));
      if (((IEnumerable<DynamicModuleType>) dynamicModule.Types).Select<DynamicModuleType, string>((Func<DynamicModuleType, string>) (x => x.DisplayName.ToLower())).Contains<string>(context.ContentTypeItemTitle.ToLower()))
        throw new ArgumentException(string.Format("Type with content type name '{0}' already exists in this module.", (object) context.ContentTypeItemTitle));
      DynamicModuleType typePersistentType = manager.CreateContentTypePersistentType(dynamicModule, context, settings.KeepIds, defaultSectionIds);
      if (dynamicModule.Status == DynamicModuleStatus.NotInstalled)
        return;
      ModuleInstaller moduleInstaller = new ModuleInstaller(provider, transaction);
      moduleInstaller.InstallContentType(dynamicModule, typePersistentType, settings.CommitTransaction, createConfigs);
      if (createConfigs)
        return;
      moduleInstaller.ValidateToolboxItemTemplateKeys(dynamicModule, typePersistentType);
    }

    /// <summary>
    /// Updates the type of the existing content. Does not commit transaction
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    internal static UpdateContentTypeOperationResult UpdateExistingContentType(
      string provider,
      DynamicModule dynamicModule,
      ContentTypeContext context,
      UpdateContentTypeOperationSettings settings)
    {
      string transaction = settings.Transaction;
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(provider, transaction);
      if (settings.LoadDynamicModuleGraph)
        manager.LoadDynamicModuleGraph(dynamicModule);
      DynamicModuleType dynamicModuleType = ((IEnumerable<DynamicModuleType>) dynamicModule.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (dt => dt.Id == context.ContentTypeId)).Single<DynamicModuleType>();
      string displayName = dynamicModuleType.DisplayName;
      Guid parentModuleTypeId1 = dynamicModuleType.ParentModuleTypeId;
      context = manager.UpdateContentType(context, dynamicModuleType, dynamicModule, settings.KeepIds);
      Guid parentModuleTypeId2 = dynamicModuleType.ParentModuleTypeId;
      DynamicModuleStatus status = dynamicModule.Status;
      if (settings.UpdateWidgetTemplates)
        new WidgetInstaller(PageManager.GetManager(string.Empty, transaction), manager).UpdateWidgetTemplates(dynamicModule, dynamicModuleType);
      dynamicModuleType.ShouldUpdateWidgetTemplates = settings.UpdateWidgetTemplates;
      if (settings.UpdateModuleConfiguration)
        new ModuleInstaller(provider, transaction).UpdateModuleType(dynamicModule, dynamicModuleType, displayName, parentModuleTypeId1);
      return new UpdateContentTypeOperationResult()
      {
        UpdateContentTypeParent = status != DynamicModuleStatus.NotInstalled && parentModuleTypeId1 != parentModuleTypeId2,
        UpdateSelfReferencing = status != DynamicModuleStatus.NotInstalled && dynamicModuleType.IsSelfReferencing && !context.IsSelfReferencing,
        ModuleTypeId = dynamicModuleType.Id,
        NewParentTypeId = parentModuleTypeId2
      };
    }

    /// <summary>Set dynamic module ids after module is deserialized</summary>
    internal static DynamicModule SetDynamicModuleIds(DynamicModule dynamicModule)
    {
      dynamicModule.SetId(dynamicModule.GetId());
      foreach (DynamicModuleType type in dynamicModule.Types)
      {
        type.SetId(type.GetId());
        foreach (DynamicModuleField field in type.Fields)
          field.SetId(field.GetId());
        foreach (FieldsBackendSection section in type.Sections)
          section.SetId(section.GetId());
      }
      return dynamicModule;
    }

    /// <summary>Updates the content type parent.</summary>
    internal static void UpdateContentTypeParent(DynamicModuleType moduleType, string transaction)
    {
      ModuleBuilderManager manager1 = ModuleBuilderManager.GetManager(string.Empty, transaction);
      foreach (string providerName in DynamicModuleManager.GetManager(string.Empty, transaction).GetProviderNames(ProviderBindingOptions.NoFilter))
      {
        DynamicModuleManager manager2 = DynamicModuleManager.GetManager(providerName, transaction);
        manager2.MoveDataItemsToDefaultParentItem(moduleType);
        TransactionManager.FlushTransaction(transaction);
        manager2.RecompileDataItemsUrls(manager1.ResolveDynamicClrType(moduleType.GetFullTypeName()));
        TransactionManager.CommitTransaction(transaction);
      }
    }

    /// <summary>
    /// Determines whether [contains fields with equal names] [the specified content type].
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    private static bool ContainsFieldsWithEqualNames(ContentTypeContext contentType)
    {
      bool flag = false;
      HashSet<string> stringSet = new HashSet<string>();
      foreach (ContentTypeItemFieldContext field in contentType.Fields)
      {
        if (!stringSet.Contains(field.Name))
        {
          stringSet.Add(field.Name);
        }
        else
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>Updates the self referencing items.</summary>
    /// <param name="systemParentType">Type of the system parent.</param>
    /// <param name="parentId">The parent id.</param>
    /// <param name="typeName">Name of the type.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <param name="moduleBuilderManager">The module builder manager.</param>
    /// <param name="dynamicModuleManager">The dynamic module manager.</param>
    private static void UpdateSelfReferencingItems(
      string systemParentType,
      Guid parentId,
      string typeName,
      string transactionName,
      ModuleBuilderManager moduleBuilderManager,
      DynamicModuleManager dynamicModuleManager)
    {
      Type itemType = moduleBuilderManager.ResolveDynamicClrType(typeName);
      List<DynamicContent> list = dynamicModuleManager.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.SystemParentType == systemParentType)).ToList<DynamicContent>();
      dynamicModuleManager.MoveChildItemsToParentItem(list, parentId, systemParentType);
      TransactionManager.CommitTransaction(transactionName);
    }

    /// <summary>
    /// Checks if DynamicModule is changed via checking its types and its types' fields
    /// </summary>
    internal static bool IsModuleChanged(DynamicModule oldState, DynamicModule newState)
    {
      if (ModuleBuilderHelper.AreBasePropertiesChanged((object) oldState, (object) newState) || ((IEnumerable<DynamicModuleType>) oldState.Types).Count<DynamicModuleType>() != ((IEnumerable<DynamicModuleType>) newState.Types).Count<DynamicModuleType>())
        return true;
      for (int index = 0; index < ((IEnumerable<DynamicModuleType>) oldState.Types).Count<DynamicModuleType>(); ++index)
      {
        if (ModuleBuilderHelper.IsModuleTypeChanged(oldState.Types[index], newState.Types[index]))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Checks if DynamicModule is changed via checking its types and its types' fields
    /// </summary>
    internal static bool IsModuleTypeChanged(DynamicModuleType oldType, DynamicModuleType newType)
    {
      if (ModuleBuilderHelper.AreBasePropertiesChanged((object) oldType, (object) newType) || ((IEnumerable<DynamicModuleField>) oldType.Fields).Count<DynamicModuleField>() != ((IEnumerable<DynamicModuleField>) newType.Fields).Count<DynamicModuleField>())
        return true;
      for (int index = 0; index < ((IEnumerable<DynamicModuleField>) oldType.Fields).Count<DynamicModuleField>(); ++index)
      {
        if (ModuleBuilderHelper.AreBasePropertiesChanged((object) oldType.Fields[index], (object) newType.Fields[index]))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Checks if base properties of dynamic objects are changed. Comparable Types include String, Boolean, Integer, GUID
    /// </summary>
    internal static bool AreBasePropertiesChanged(object oldState, object newState)
    {
      Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>()
      {
        {
          typeof (DynamicModule).FullName,
          new List<string>()
          {
            "ApplicationName",
            "InheritsPermissions",
            "CanInheritPermissions"
          }
        },
        {
          typeof (DynamicModuleType).FullName,
          new List<string>()
          {
            "ApplicationName",
            "ModuleName",
            "InheritsPermissions",
            "CanInheritPermissions"
          }
        },
        {
          typeof (DynamicModuleField).FullName,
          new List<string>()
          {
            "ApplicationName",
            "ModuleName",
            "ParentSectionId"
          }
        }
      };
      List<Type> source = new List<Type>()
      {
        typeof (string),
        typeof (bool),
        typeof (int),
        typeof (Guid)
      };
      Type type = oldState.GetType();
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(type))
      {
        PropertyDescriptor prop = property;
        if (source.Any<Type>((Func<Type, bool>) (t => t.FullName == prop.PropertyType.FullName)) && (!dictionary.ContainsKey(type.FullName) || !dictionary[type.FullName].Contains(prop.Name)) && (!(oldState is DynamicModuleField) || !((DynamicModuleField) oldState).IsTransient))
        {
          object obj1 = prop.GetValue(oldState);
          object obj2 = prop.GetValue(newState);
          if (obj1 == null && obj2 != null || obj1 != null && !obj1.Equals(obj2))
            return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Installs dynamic module type structure, without modifying the configurations
    /// </summary>
    internal static void InstallType(
      DynamicModule module,
      DynamicModuleType type,
      string transaction,
      Dictionary<string, Guid> defaultSectionIds,
      bool createConfigs)
    {
      string provider1 = module.Provider is IDataProviderBase provider2 ? provider2.Name : string.Empty;
      AddContentTypeOperationSettings operationSettings = new AddContentTypeOperationSettings()
      {
        KeepIds = true,
        Transaction = transaction,
        CommitTransaction = true
      };
      ContentTypeContext contentTypeContext = ContentTypeService.GetContentTypeContext((IDynamicModule) module, type, true);
      contentTypeContext.Fields = ((IEnumerable<ContentTypeItemFieldContext>) contentTypeContext.Fields).Where<ContentTypeItemFieldContext>((Func<ContentTypeItemFieldContext, bool>) (f => !f.IsTransient)).ToArray<ContentTypeItemFieldContext>();
      DynamicModule dynamicModule = module;
      ContentTypeContext context = contentTypeContext;
      AddContentTypeOperationSettings settings = operationSettings;
      Dictionary<string, Guid> defaultSectionIds1 = defaultSectionIds;
      int num = createConfigs ? 1 : 0;
      ModuleBuilderHelper.AddNewContentType(provider1, dynamicModule, context, settings, defaultSectionIds1, num != 0);
    }

    /// <summary>
    /// Updates dynamic module type structure, without modifying the configurations
    /// </summary>
    internal static UpdateContentTypeOperationResult UpdateType(
      DynamicModule module,
      DynamicModuleType type,
      UpdateContentTypeOperationSettings settings)
    {
      string provider1 = module.Provider is IDataProviderBase provider2 ? provider2.Name : string.Empty;
      ContentTypeContext contentTypeContext = ContentTypeService.GetContentTypeContext((IDynamicModule) module, type, true);
      DynamicModule dynamicModule = module;
      ContentTypeContext context = contentTypeContext;
      UpdateContentTypeOperationSettings settings1 = settings;
      return ModuleBuilderHelper.UpdateExistingContentType(provider1, dynamicModule, context, settings1);
    }

    /// <summary>Validates the type of the dynamic module.</summary>
    internal static void ValidateDynamicModuleTaxonomyFields(
      DynamicModule module,
      string transactionName)
    {
      if (module == null)
        throw new ArgumentNullException(nameof (module));
      TaxonomyManager manager = TaxonomyManager.GetManager(string.Empty, transactionName);
      foreach (DynamicModuleType type in module.Types)
      {
        foreach (DynamicModuleField field1 in type.Fields)
        {
          DynamicModuleField field = field1;
          if (field.FieldType == FieldType.Classification)
          {
            if (!manager.GetTaxonomies<Taxonomy>().Any<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Id == field.ClassificationId)))
              throw new Exception(string.Format(Res.Get<ModuleBuilderResources>().TaxonomyWithIdDoesntExists, (object) field.ClassificationId));
          }
        }
      }
    }
  }
}
