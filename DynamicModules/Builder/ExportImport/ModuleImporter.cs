// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ExportImport.ModuleImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Exceptions;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Utilities.Zip;

namespace Telerik.Sitefinity.DynamicModules.Builder.ExportImport
{
  /// <summary>Provides a functionality to import Dynamic Modules</summary>
  public static class ModuleImporter
  {
    private static ConfigManager configurationManager = ConfigManager.GetManager();
    private static ConfigSection[] allConfigurationSections = ModuleImporter.configurationManager.GetAllConfigSections();
    internal const string MainShortTextFieldsDictionaryKey = "ImportedMainShortTextFields";

    /// <summary>Imports module from stream</summary>
    /// <param name="deserializedModuleMemoryStream">The stream containing the module with the configurations.</param>
    /// <param name="updateModuleId">The module id, if trying to update an existing module.</param>
    /// <param name="activateModule">If the module needs to be activated after the import.</param>
    /// <returns>The imported dynamic module.</returns>
    public static DynamicModule ImportModule(
      Stream deserializedModuleMemoryStream,
      Guid updateModuleId,
      bool activateModule = false)
    {
      DynamicModule dynamicModule = (DynamicModule) null;
      try
      {
        Dictionary<Type, string> configurationsToImport = new Dictionary<Type, string>();
        dynamicModule = new DynamicModule();
        using (deserializedModuleMemoryStream)
        {
          XmlReader xmlReader = XmlReader.Create((TextReader) new StreamReader(deserializedModuleMemoryStream, Encoding.UTF8), new XmlReaderSettings()
          {
            IgnoreWhitespace = true
          });
          while (!xmlReader.EOF)
          {
            if (xmlReader.NodeType == XmlNodeType.Element)
            {
              if (xmlReader.Name.ToLower() == "dynamicmodule")
                dynamicModule = Deserializer.DeserializeModule((Stream) new MemoryStream(Encoding.UTF8.GetBytes(xmlReader.ReadOuterXml())));
              else
                ModuleImporter.ReadConfig(xmlReader, (IDictionary<Type, string>) configurationsToImport);
            }
            else
              xmlReader.Read();
          }
          xmlReader.Close();
        }
        dynamicModule = ModuleImporter.ImportModule(dynamicModule, updateModuleId, configurationsToImport, string.Empty, activateModule);
      }
      catch (ModulePackageException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.ModuleBuilder))
          throw;
      }
      return dynamicModule;
    }

    /// <summary>Imports the module.</summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="updateModuleId">The update module identifier.</param>
    /// <param name="configurationsToImport">The configurations to import.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <param name="activateModule">if set to <c>true</c> [activate module].</param>
    /// <param name="generateDefaultConfigsOnUpdate">if set to <c>true</c> [generate default configurations on update].</param>
    /// <param name="configureForDefaultSite">if set to <c>true</c> [configure for default site].</param>
    /// <param name="origin">The origin of the module.</param>
    /// <returns>The imported module.</returns>
    /// <exception cref="T:Telerik.Sitefinity.DynamicModules.Builder.Exceptions.ModulePackageException">When importing the module fails due to invalid package.</exception>
    internal static DynamicModule ImportModule(
      DynamicModule dynamicModule,
      Guid updateModuleId,
      Dictionary<Type, string> configurationsToImport,
      string transactionName,
      bool activateModule = false,
      bool generateDefaultConfigsOnUpdate = true,
      bool configureForDefaultSite = true,
      string origin = null)
    {
      if (string.IsNullOrEmpty(transactionName))
        transactionName = Guid.NewGuid().ToString();
      if (configurationsToImport == null)
        configurationsToImport = new Dictionary<Type, string>();
      PageManager.GetManager(string.Empty, transactionName).GetPageNodes();
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(string.Empty, transactionName);
      List<DynamicModuleType> updatedTypes = new List<DynamicModuleType>();
      List<string> deletedTypeNames = new List<string>();
      if (updateModuleId == Guid.Empty)
      {
        ModuleImporter.ValidateModuleNameAndId(dynamicModule, manager);
        dynamicModule = ModuleImporter.CreateModule(manager, dynamicModule);
      }
      else
      {
        DynamicModule dynamicModule1 = manager.GetDynamicModules().FirstOrDefault<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Id == updateModuleId));
        if (dynamicModule1 == null)
          throw new ModulePackageException(Telerik.Sitefinity.Localization.Res.Get<ModuleBuilderResources>().ErrorUpdatingIdsDontMatch);
        manager.LoadDynamicModuleGraph(dynamicModule1, true);
        bool hasConfigs = configurationsToImport.Count > 0 || !generateDefaultConfigsOnUpdate;
        dynamicModule = ModuleImporter.UpdateDynamicModule(manager, dynamicModule, dynamicModule1, hasConfigs, out updatedTypes, out deletedTypeNames);
      }
      foreach (KeyValuePair<Type, string> keyValuePair in configurationsToImport)
        ModuleImporter.configurationManager.Import(keyValuePair.Key, keyValuePair.Value, origin);
      ModuleInstaller moduleInstaller = new ModuleInstaller(string.Empty, transactionName);
      if (configurationsToImport.Count > 0)
        ModuleImporter.ValidateToolboxItemTemplateKeys(moduleInstaller, dynamicModule, updatedTypes);
      ModuleImporter.RemoveDeletedTypesConfigurations(moduleInstaller, deletedTypeNames);
      if (activateModule && dynamicModule.Status != DynamicModuleStatus.Active)
      {
        manager.LoadDynamicModuleGraph(dynamicModule, true);
        moduleInstaller.InstallModule(dynamicModule, configureForDefaultSite: configureForDefaultSite);
      }
      return dynamicModule;
    }

    /// <summary>Imports dynamic module zip package</summary>
    /// <param name="dynamicModuleZip">Module package structure to import</param>
    /// <param name="updateModuleId">If set, will update dynamic module with specified Id</param>
    /// <param name="activateModule">Value indicating whether to activate the module.</param>
    /// <returns>The imported dynamic module.</returns>
    internal static DynamicModule ImportModule(
      ZipFile dynamicModuleZip,
      Guid updateModuleId = default (Guid),
      bool activateModule = false)
    {
      if (dynamicModuleZip == null)
        throw new ArgumentNullException("Dynamic module zip file is null");
      if (dynamicModuleZip.Count != 1)
        throw new ArgumentException("Zip file items count is different from 1");
      if (dynamicModuleZip.Entries[0].FileName != "install.sf")
        throw new ArgumentException("Installation file differs from install.sf");
      MemoryStream memoryStream = new MemoryStream();
      dynamicModuleZip.Extract("install.sf", (Stream) memoryStream);
      memoryStream.Position = 0L;
      return ModuleImporter.ImportModule((Stream) memoryStream, updateModuleId, activateModule);
    }

    internal static void DeleteModuleTypes(
      List<DynamicModuleType> deletedTypes,
      DynamicModule persistedDynamicModule,
      ModuleInstaller moduleInstaller,
      string transactionName,
      out List<string> deletedTypeNames)
    {
      deletedTypeNames = new List<string>();
      foreach (DynamicModuleType deletedType in deletedTypes)
      {
        string fullTypeName = deletedType.GetFullTypeName();
        deletedTypeNames.Add(fullTypeName);
        moduleInstaller.DeleteModuleTypeData(persistedDynamicModule, deletedType, false);
        moduleInstaller.DeleteModuleType(persistedDynamicModule, deletedType);
      }
      if (persistedDynamicModule.Status != DynamicModuleStatus.NotInstalled)
        moduleInstaller.ValidateModuleGroupPagePosition(persistedDynamicModule);
      TransactionManager.CommitTransaction(transactionName);
      foreach (string name in deletedTypeNames)
      {
        Type type = TypeResolutionService.ResolveType(name, false);
        if (type != (Type) null)
          TypeResolutionService.UnregisterType(type);
      }
    }

    internal static void ReadConfig(
      Stream configsStream,
      out Dictionary<Type, string> configurationsToImport)
    {
      configurationsToImport = new Dictionary<Type, string>();
      using (configsStream)
      {
        XmlReader xmlReader = XmlReader.Create((TextReader) new StreamReader(configsStream, Encoding.UTF8), new XmlReaderSettings()
        {
          IgnoreWhitespace = true
        });
        while (!xmlReader.EOF)
        {
          if (xmlReader.NodeType == XmlNodeType.Element)
            ModuleImporter.ReadConfig(xmlReader, (IDictionary<Type, string>) configurationsToImport);
          else
            xmlReader.Read();
        }
        xmlReader.Close();
      }
    }

    private static void ValidateModuleNameAndId(
      DynamicModule dynamicModule,
      ModuleBuilderManager moduleBuilderManager)
    {
      if (moduleBuilderManager.GetDynamicModules().Any<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Id == dynamicModule.Id && m.Name == dynamicModule.Name)))
      {
        ModulePackageException packageException = new ModulePackageException(Telerik.Sitefinity.Localization.Res.Get<ModuleBuilderResources>().ModuleAlreadyExists);
        packageException.Data[(object) "ModuleId"] = (object) dynamicModule.Id;
        throw packageException;
      }
      if (moduleBuilderManager.GetDynamicModules().Any<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Id != dynamicModule.Id && m.Name == dynamicModule.Name)))
        throw new ModulePackageException(string.Format(Telerik.Sitefinity.Localization.Res.Get<ModuleBuilderResources>().ModuleWithNameAlreadyExists, (object) dynamicModule.Name));
      if (moduleBuilderManager.GetDynamicModules().Any<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Id == dynamicModule.Id && m.Name != dynamicModule.Name)))
        throw new ModulePackageException(string.Format(Telerik.Sitefinity.Localization.Res.Get<ModuleBuilderResources>().ModuleWithIdAlreadyExists, (object) dynamicModule.Id));
    }

    private static void RemoveDeletedTypesConfigurations(
      ModuleInstaller moduleInstaller,
      List<string> deletedTypeNames)
    {
      foreach (string deletedTypeName in deletedTypeNames)
        moduleInstaller.DeleteModuleTypeConfiguration(deletedTypeName);
    }

    /// <summary>Creates dynamic module structure</summary>
    /// <param name="moduleBuilderManager">The module builder manager.</param>
    /// <param name="deserializedModule">The deserialized module.</param>
    /// <returns>The created module.</returns>
    private static DynamicModule CreateModule(
      ModuleBuilderManager moduleBuilderManager,
      DynamicModule deserializedModule)
    {
      string transactionName = moduleBuilderManager.TransactionName;
      ModuleBuilderHelper.ValidateDynamicModuleTaxonomyFields(deserializedModule, transactionName);
      DynamicModule dynamicModule = moduleBuilderManager.CreateDynamicModule(deserializedModule.Id);
      moduleBuilderManager.CopyModuleProperties(deserializedModule, dynamicModule);
      moduleBuilderManager.CopyDynamicModuleTypes(deserializedModule, dynamicModule);
      TransactionManager.CommitTransaction(transactionName);
      BackendDefinitionInstaller.InvalidateBackendDefinitions();
      return dynamicModule;
    }

    /// <summary>Updates specific dynamic module</summary>
    /// <param name="moduleBuilderManager">The module builder manager.</param>
    /// <param name="deserializedModule">Module with new structure</param>
    /// <param name="persistedDynamicModule">Existing dynamic module to update</param>
    /// <param name="hasConfigs">If configurations are provided in the imported ZIP file, they will be imported, so we do not need to create or update them.</param>
    /// <param name="updatedTypes">The updated types.</param>
    /// <param name="deletedTypeNames">The deleted type names.</param>
    /// <returns>The updated dynamic module.</returns>
    private static DynamicModule UpdateDynamicModule(
      ModuleBuilderManager moduleBuilderManager,
      DynamicModule deserializedModule,
      DynamicModule persistedDynamicModule,
      bool hasConfigs,
      out List<DynamicModuleType> updatedTypes,
      out List<string> deletedTypeNames)
    {
      if (deserializedModule.Id != persistedDynamicModule.Id)
        throw new ModulePackageException(Telerik.Sitefinity.Localization.Res.Get<ModuleBuilderResources>().ErrorUpdatingIdsDontMatch);
      if (deserializedModule.Name != persistedDynamicModule.Name)
        throw new ModulePackageException(Telerik.Sitefinity.Localization.Res.Get<ModuleBuilderResources>().ErrorUpdatingNamesDontMatch);
      string transactionName = moduleBuilderManager.TransactionName;
      if (!ModuleBuilderHelper.IsModuleChanged(persistedDynamicModule, deserializedModule))
      {
        TransactionManager.RollbackTransaction(transactionName);
        updatedTypes = new List<DynamicModuleType>();
        deletedTypeNames = new List<string>();
        return persistedDynamicModule;
      }
      ModuleBuilderHelper.ValidateDynamicModuleTaxonomyFields(deserializedModule, transactionName);
      if (persistedDynamicModule.Title != deserializedModule.Title)
      {
        ContentTypeSimpleContext context = new ContentTypeSimpleContext()
        {
          ContentTypeId = deserializedModule.Id,
          ContentTypeDescription = deserializedModule.Description,
          ContentTypeTitle = deserializedModule.Title
        };
        moduleBuilderManager.UpdateDynamicModuleNameAndDescription(persistedDynamicModule.Id, context);
      }
      moduleBuilderManager.CopyModuleProperties(deserializedModule, persistedDynamicModule, false, true);
      moduleBuilderManager.LoadDynamicModuleGraph(persistedDynamicModule, true);
      List<DynamicModuleType> addedTypes;
      List<DynamicModuleType> deletedTypes;
      ModuleImporter.GetModifiedTypes(deserializedModule, persistedDynamicModule, out updatedTypes, out addedTypes, out deletedTypes);
      ModuleImporter.AddNewTypes(addedTypes, persistedDynamicModule, transactionName, moduleBuilderManager);
      List<UpdateContentTypeOperationResult> updateResults = new List<UpdateContentTypeOperationResult>();
      ModuleInstaller moduleInstaller = new ModuleInstaller(string.Empty, transactionName);
      ModuleImporter.UpdateExistingTypes(deserializedModule, deletedTypes, persistedDynamicModule, updatedTypes, moduleBuilderManager, transactionName, updateResults, moduleInstaller);
      ModuleImporter.DeleteModuleTypes(deletedTypes, persistedDynamicModule, moduleInstaller, transactionName, out deletedTypeNames);
      moduleBuilderManager.LoadDynamicModuleGraph(persistedDynamicModule, true);
      if (updateResults.Count > 0)
        ModuleBuilderHelper.UpdateContentTypesParentItems(moduleBuilderManager, persistedDynamicModule, (IList<UpdateContentTypeOperationResult>) updateResults, transactionName);
      BackendDefinitionInstaller.InvalidateBackendDefinitions();
      if (!hasConfigs)
        ModuleImporter.RegenerateDefaultConfigs(moduleInstaller, persistedDynamicModule, addedTypes, updatedTypes, deletedTypeNames);
      return persistedDynamicModule;
    }

    private static void GetModifiedTypes(
      DynamicModule deserializedModule,
      DynamicModule persistedDynamicModule,
      out List<DynamicModuleType> updatedTypes,
      out List<DynamicModuleType> addedTypes,
      out List<DynamicModuleType> deletedTypes)
    {
      IEnumerable<DynamicModuleType> orderedTypes = ModuleImporter.GetOrderedModuleTypesList((IEnumerable<DynamicModuleType>) deserializedModule.Types);
      addedTypes = orderedTypes.Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => !((IEnumerable<DynamicModuleType>) persistedDynamicModule.Types).Any<DynamicModuleType>((Func<DynamicModuleType, bool>) (pt => pt.Id == t.Id)))).ToList<DynamicModuleType>();
      updatedTypes = orderedTypes.Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => ((IEnumerable<DynamicModuleType>) persistedDynamicModule.Types).Any<DynamicModuleType>((Func<DynamicModuleType, bool>) (pt => pt.Id == t.Id)))).ToList<DynamicModuleType>();
      deletedTypes = ((IEnumerable<DynamicModuleType>) persistedDynamicModule.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => StructureTransferBase.IsDeleteAllowedForItem((IHasOrigin) t) && !orderedTypes.Any<DynamicModuleType>((Func<DynamicModuleType, bool>) (dt => dt.Id == t.Id)))).ToList<DynamicModuleType>();
      foreach (DynamicModuleType dynamicModuleType in orderedTypes)
      {
        DynamicModuleType type = dynamicModuleType;
        if (type.ParentModuleTypeId != Guid.Empty)
          type.ParentModuleType = ((IEnumerable<DynamicModuleType>) deserializedModule.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == type.ParentModuleTypeId)).FirstOrDefault<DynamicModuleType>();
      }
      if (SystemManager.CurrentHttpContext == null || SystemManager.CurrentHttpContext.Items == null || addedTypes.Count <= 0)
        return;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (DynamicModuleType dynamicModuleType in addedTypes)
        dictionary[dynamicModuleType.GetFullTypeName()] = dynamicModuleType.MainShortTextFieldName;
      SystemManager.CurrentHttpContext.Items[(object) "ImportedMainShortTextFields"] = (object) dictionary;
    }

    private static void AddNewTypes(
      List<DynamicModuleType> addedTypes,
      DynamicModule persistedDynamicModule,
      string transactionName,
      ModuleBuilderManager moduleBuilderManager)
    {
      foreach (DynamicModuleType addedType in addedTypes)
      {
        DynamicModuleType newType = addedType;
        Dictionary<string, Guid> defaultSectionIds = new Dictionary<string, Guid>();
        ((IEnumerable<FieldsBackendSection>) newType.Sections).ToList<FieldsBackendSection>().ForEach((Action<FieldsBackendSection>) (s => defaultSectionIds.Add(s.Name, s.Id)));
        ModuleBuilderHelper.InstallType(persistedDynamicModule, newType, transactionName, defaultSectionIds, true);
        DynamicModuleType targetType = ((IEnumerable<DynamicModuleType>) persistedDynamicModule.Types).FirstOrDefault<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == newType.Id));
        if (targetType != null)
          moduleBuilderManager.CopyBackendSections(newType, targetType);
      }
    }

    private static void UpdateExistingTypes(
      DynamicModule deserializedModule,
      List<DynamicModuleType> deletedTypes,
      DynamicModule persistedDynamicModule,
      List<DynamicModuleType> updatedTypes,
      ModuleBuilderManager moduleBuilderManager,
      string transactionName,
      List<UpdateContentTypeOperationResult> updateResults,
      ModuleInstaller moduleInstaller)
    {
      IEnumerable<Guid> newRootTypesIds = ((IEnumerable<DynamicModuleType>) deserializedModule.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == Guid.Empty)).Select<DynamicModuleType, Guid>((Func<DynamicModuleType, Guid>) (t => t.Id));
      IEnumerable<Guid> newChildTypesIds = ((IEnumerable<DynamicModuleType>) deserializedModule.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId != Guid.Empty)).Select<DynamicModuleType, Guid>((Func<DynamicModuleType, Guid>) (t => t.Id));
      int num = deletedTypes.Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == Guid.Empty)).Count<DynamicModuleType>();
      int rootTypesCount = ((IEnumerable<DynamicModuleType>) persistedDynamicModule.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t =>
      {
        if (newRootTypesIds.Contains<Guid>(t.Id))
          return true;
        return t.ParentModuleTypeId == Guid.Empty && !newChildTypesIds.Contains<Guid>(t.Id);
      })).Count<DynamicModuleType>() - num;
      Guid rootPageId = ModulePagesInstaller.GetRootPageId(deserializedModule);
      foreach (DynamicModuleType updatedType in updatedTypes)
      {
        DynamicModuleType typeToUpdate = updatedType;
        DynamicModuleType dynamicModuleType = ((IEnumerable<DynamicModuleType>) persistedDynamicModule.Types).FirstOrDefault<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == typeToUpdate.Id));
        if (ModuleBuilderHelper.IsModuleTypeChanged(dynamicModuleType, typeToUpdate))
        {
          moduleBuilderManager.CopyBackendSections(typeToUpdate, dynamicModuleType);
          UpdateContentTypeOperationSettings operationSettings = new UpdateContentTypeOperationSettings();
          operationSettings.LoadDynamicModuleGraph = false;
          operationSettings.KeepIds = true;
          operationSettings.UpdateWidgetTemplates = false;
          operationSettings.Transaction = transactionName;
          operationSettings.UpdateModuleConfiguration = false;
          UpdateContentTypeOperationSettings settings = operationSettings;
          updateResults.Add(ModuleBuilderHelper.UpdateType(persistedDynamicModule, typeToUpdate, settings));
          DynamicModuleType moduleType = ((IEnumerable<DynamicModuleType>) persistedDynamicModule.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (dt => dt.Id == typeToUpdate.Id)).Single<DynamicModuleType>();
          if (persistedDynamicModule.Status != DynamicModuleStatus.NotInstalled)
            moduleInstaller.UpdateModuleType(persistedDynamicModule, moduleType, typeToUpdate.DisplayName, typeToUpdate.ParentModuleTypeId, false, rootPageId, rootTypesCount);
          else
            moduleInstaller.RemoveDeletedDynamicModuleTypeFields(moduleType);
        }
      }
    }

    private static void ValidateToolboxItemTemplateKeys(
      ModuleInstaller moduleInstaller,
      DynamicModule dynamicModule,
      List<DynamicModuleType> updatedTypes)
    {
      foreach (DynamicModuleType updatedType in updatedTypes)
        moduleInstaller.ValidateToolboxItemTemplateKeys(dynamicModule, updatedType);
      updatedTypes.Clear();
    }

    private static void RegenerateDefaultConfigs(
      ModuleInstaller moduleInstaller,
      DynamicModule dynamicModule,
      List<DynamicModuleType> addedTypes,
      List<DynamicModuleType> updatedTypes,
      List<string> deletedTypeNames)
    {
      foreach (DynamicModuleType addedType in addedTypes)
        moduleInstaller.InstallModuleTypeConfigurations(dynamicModule, addedType);
      foreach (DynamicModuleType updatedType in updatedTypes)
        moduleInstaller.UpdateModuleTypeConfigurations(dynamicModule, updatedType, updatedType.DisplayName, updatedType.ParentModuleTypeId, true);
      TransactionManager.CommitTransaction(moduleInstaller.TransactionName);
    }

    private static void ReadConfig(
      XmlReader xmlReader,
      IDictionary<Type, string> configurationsToImport)
    {
      ConfigSection configSection = ((IEnumerable<ConfigSection>) ModuleImporter.allConfigurationSections).FirstOrDefault<ConfigSection>((Func<ConfigSection, bool>) (s => s.TagName == xmlReader.Name));
      if (configSection != null)
        configurationsToImport[configSection.GetType()] = xmlReader.ReadOuterXml();
      else
        xmlReader.Read();
    }

    private static IEnumerable<DynamicModuleType> GetOrderedModuleTypesList(
      IEnumerable<DynamicModuleType> moduleTypes)
    {
      Stack<DynamicModuleType> types = new Stack<DynamicModuleType>(moduleTypes.Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (mt => mt.ParentModuleTypeId == Guid.Empty)));
      while (types.Any<DynamicModuleType>())
      {
        DynamicModuleType type = types.Pop();
        yield return type;
        foreach (DynamicModuleType dynamicModuleType in moduleTypes.Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (mt => mt.ParentModuleTypeId == type.Id)))
          types.Push(dynamicModuleType);
      }
    }
  }
}
