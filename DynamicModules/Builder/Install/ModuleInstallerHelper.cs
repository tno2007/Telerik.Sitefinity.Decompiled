// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.ModuleInstallerHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Configuration;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.PublishingSystem;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  internal class ModuleInstallerHelper
  {
    /// <summary>
    /// Removes publishing points for the specified types in the module.
    /// </summary>
    /// <param name="moduleTypeNames">The names of types registered in the module.</param>
    /// <param name="provider">The provider.</param>
    public static void RemoveModulePublishingPoints(List<string> moduleTypeNames, string provider)
    {
      foreach (string moduleTypeName in moduleTypeNames)
        ModuleInstallerHelper.RemoveModulePublishingPoints(moduleTypeName, provider);
    }

    /// <summary>
    /// Removes publishing points for the specified type in the module.
    /// </summary>
    /// <param name="moduleType">The name of a type registered in the module.</param>
    /// <param name="provider">The provider.</param>
    public static void RemoveModulePublishingPoints(string moduleType, string provider)
    {
      string transactionName = "RemoveModulePublishingPoint_" + (object) Guid.NewGuid();
      try
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ModuleInstallerHelper.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new ModuleInstallerHelper.\u003C\u003Ec__DisplayClass1_0();
        PublishingManager manager = PublishingManager.GetManager(provider, transactionName);
        // ISSUE: reference to a compiler-generated field
        cDisplayClass10.pipeName = ModuleNamesGenerator.GeneratePipeName(moduleType);
        // ISSUE: reference to a compiler-generated field
        List<PublishingPoint> list = manager.GetPublishingPoints().Where<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (p => p.PipeSettings.Select<PipeSettings, string>((Func<PipeSettings, string>) (s => s.PipeName)).Contains<string>(cDisplayClass10.pipeName))).ToList<PublishingPoint>();
        foreach (PublishingPoint publishingPoint in list)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          PipeSettings pipeSettings = publishingPoint.PipeSettings.Where<PipeSettings>(cDisplayClass10.\u003C\u003E9__2 ?? (cDisplayClass10.\u003C\u003E9__2 = new Func<PipeSettings, bool>(cDisplayClass10.\u003CRemoveModulePublishingPoints\u003Eb__2))).First<PipeSettings>();
          publishingPoint.PipeSettings.Remove(pipeSettings);
          manager.DeletePipeSettings(pipeSettings);
        }
        if (list.Count <= 0)
          return;
        TransactionManager.CommitTransaction(transactionName);
      }
      catch (Exception ex)
      {
        TransactionManager.RollbackTransaction(transactionName);
        Log.Error("Failed to remove module publishing point Exception: {0}", (object) ex);
      }
    }

    /// <summary>
    /// Registers the workflows for the specified <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="module">The module.</param>
    public static void RegisterWorkflowsIfNotExisting(DynamicModule module)
    {
      ConfigManager manager = ConfigManager.GetManager();
      WorkflowConfig section = manager.GetSection<WorkflowConfig>();
      foreach (DynamicModuleType type in module.Types)
      {
        string fullTypeName = type.GetFullTypeName();
        if (!section.Workflows.ContainsKey(fullTypeName))
        {
          WorkflowElement element = new WorkflowElement()
          {
            ContentType = fullTypeName,
            Title = type.DisplayName,
            ModuleName = module.Name
          };
          section.Workflows.Add(element);
        }
      }
      manager.SaveSection((ConfigSection) section);
    }

    /// <summary>
    /// Registers the workflows for the specified <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> registered in a <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="dynamicType">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> registered in a <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /></param>
    public static void RegisterWorkflows(DynamicModuleType dynamicType)
    {
      ConfigManager manager = ConfigManager.GetManager();
      WorkflowConfig section = manager.GetSection<WorkflowConfig>();
      string fullTypeName = dynamicType.GetFullTypeName();
      if (!section.Workflows.ContainsKey(fullTypeName))
      {
        WorkflowElement workflowElement = new WorkflowElement();
        workflowElement.ContentType = fullTypeName;
        workflowElement.ServiceUrl = WorkflowConfig.GetDefaultWorkflowUrl(typeof (DynamicContent));
        workflowElement.Title = dynamicType.DisplayName;
        workflowElement.Origin = OriginWrapperObject.ToArrayJson(dynamicType.Origin);
        WorkflowElement element = workflowElement;
        section.Workflows.Add(element);
      }
      manager.SaveSection((ConfigSection) section);
    }

    /// <summary>
    /// Unregisters the workflows for the specified types registered in a module.
    /// </summary>
    /// <param name="contentTypeNames">The names of types in a <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</param>
    public static void UnregisterWorkflows(List<string> contentTypeNames)
    {
      foreach (string contentTypeName in contentTypeNames)
        ModuleInstallerHelper.UnregisterWorkflows(contentTypeName);
    }

    /// <summary>
    /// Unregisters the workflows for the specified type registered in a module.
    /// </summary>
    /// <param name="contentTypeName">The name of a type in a <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /></param>
    public static void UnregisterWorkflows(string contentTypeName)
    {
      ConfigManager manager = ConfigManager.GetManager();
      WorkflowConfig section = manager.GetSection<WorkflowConfig>();
      ConfigElement elementByKey = section.Workflows.GetElementByKey(contentTypeName);
      if (elementByKey != null)
        section.Workflows.Remove(elementByKey);
      manager.SaveSection((ConfigSection) section);
    }

    /// <summary>
    /// Gets the names of the types registered in a <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />
    /// </summary>
    /// <param name="module">The module.</param>
    /// <returns>A collection of names of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> registered in the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /></returns>
    public static List<string> GetContentTypeNames(DynamicModule module)
    {
      List<string> contentTypeNames = new List<string>();
      foreach (DynamicModuleType type in module.Types)
        contentTypeNames.Add(type.GetFullTypeName());
      return contentTypeNames;
    }

    /// <summary>Gets the field names.</summary>
    /// <param name="module">The module.</param>
    /// <returns>A dictionary of field permission set names</returns>
    public static Dictionary<string, List<string>> GetFieldPermissionSetNames(
      DynamicModule module)
    {
      Dictionary<string, List<string>> permissionSetNames = new Dictionary<string, List<string>>();
      foreach (DynamicModuleType type in module.Types)
      {
        List<string> stringList = new List<string>();
        foreach (DynamicModuleField field in type.Fields)
        {
          if (!field.IsTransient)
            stringList.Add(field.GetPermissionSetName());
        }
        permissionSetNames.Add(type.GetFullTypeName(), stringList);
      }
      return permissionSetNames;
    }

    /// <summary>
    /// Installs the search option for the specified <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="module">The module.</param>
    public static void InstallSearch(DynamicModule module)
    {
      foreach (IDynamicModuleType type in module.Types)
        ModuleInstallerHelper.InstallSearch(type, true);
    }

    /// <summary>
    /// Installs the search option for the specified <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    public static void InstallSearch(IDynamicModuleType moduleType)
    {
      ModuleInstallerHelper.RegisterPipeMappings(moduleType);
      ModuleInstallerHelper.RegisterPipes(moduleType);
      ModuleInstallerHelper.RegisterPipeSettings(moduleType);
      ModuleInstallerHelper.RegisterPipeDefinitions(moduleType);
    }

    /// <summary>
    /// Installs the search option for the specified <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="updatePublishingPointsBasedOnTemplate">if set to <c>true</c> [update publishing points based on template].</param>
    public static void InstallSearch(
      IDynamicModuleType moduleType,
      bool updatePublishingPointsBasedOnTemplate)
    {
      ModuleInstallerHelper.InstallSearch(moduleType);
      if (!updatePublishingPointsBasedOnTemplate)
        return;
      PublishingSystemFactory.UpdatePublishingPointsBasedOnTemplate();
    }

    /// <summary>
    /// Uninstalls search indexes for the specified types registered in a module.
    /// </summary>
    /// <param name="contentTypeNames">The names of the module types.</param>
    public static void UninstallSearch(List<string> contentTypeNames)
    {
      foreach (string contentTypeName in contentTypeNames)
        ModuleInstallerHelper.UninstallSearch(contentTypeName);
    }

    /// <summary>
    /// Uninstalls search indexes for the specified module type.
    /// </summary>
    /// <param name="moduleTypeName">Name of the module type.</param>
    public static void UninstallSearch(string moduleTypeName)
    {
      string pipeName = ModuleNamesGenerator.GeneratePipeName(moduleTypeName);
      PublishingSystemFactory.UnregisterPipe(pipeName);
      PublishingSystemFactory.UnregisterPipeSettings(pipeName);
      PublishingSystemFactory.UnregisterTemplatePipe("SearchItemTemplate", pipeName);
      PublishingSystemFactory.UnregisterTemplatePipe("BackendSearchItemTemplate", pipeName);
      PublishingSystemFactory.UnregisterPipeDefinitions(pipeName);
    }

    /// <summary>
    /// Removes the providers for the specified dynamic module.
    /// </summary>
    /// <param name="moduleName">The name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /></param>
    public static void RemoveModuleProviders(string moduleName)
    {
      DynamicModuleManager manager1 = DynamicModuleManager.GetManager();
      ConfigManager manager2 = ConfigManager.GetManager();
      DynamicModulesConfig section = manager2.GetSection<DynamicModulesConfig>();
      foreach (DataProviderSettings providerSettings in section.Providers.Values.Where<DataProviderSettings>((Func<DataProviderSettings, bool>) (p => p.Parameters[nameof (moduleName)] == moduleName)))
      {
        manager1.RemoveProvider(providerSettings.Name);
        section.Providers.Remove(providerSettings);
      }
      manager2.SaveSection((ConfigSection) section);
    }

    /// <summary>
    /// Checks whether the dynamic module type is multilingual. It can be in multilingual only if the system is
    /// in multilingual and it has at least one multilingual field.
    /// </summary>
    /// <param name="typeFields">The type fields.</param>
    /// <returns>Value indicating whether the collection contains localizable fields</returns>
    public static bool ContainsLocalizableFields(IEnumerable<IDynamicModuleField> typeFields) => typeFields != null && typeFields.Any<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (f => (f.FieldType == FieldType.ShortText || f.FieldType == FieldType.LongText || f.FieldType == FieldType.Number || f.FieldType == FieldType.DateTime) && f.IsLocalizable && f.FieldStatus != DynamicModuleFieldStatus.Removed));

    internal static void RegisterPipes(IDynamicModuleType dynamicModuleType) => PublishingSystemFactory.RegisterPipe(ModuleNamesGenerator.GeneratePipeName(dynamicModuleType), typeof (DynamicContentInboundPipe));

    internal static void RegisterPipeSettings(IDynamicModuleType dynamicModuleType)
    {
      string pipeName = ModuleNamesGenerator.GeneratePipeName(dynamicModuleType);
      SitefinityContentPipeSettings settings = new SitefinityContentPipeSettings();
      settings.ContentTypeName = dynamicModuleType.GetFullTypeName();
      settings.UIName = dynamicModuleType.DisplayName;
      settings.IsInbound = true;
      settings.PipeName = pipeName;
      settings.IsActive = true;
      settings.MaxItems = 0;
      settings.InvocationMode = PipeInvokationMode.Push;
      PublishingSystemFactory.RegisterPipeSettings(pipeName, (PipeSettings) settings);
      PublishingSystemFactory.RegisterPipeForAllContentTemplates(PublishingSystemFactory.GetPipeSettings(pipeName), (Predicate<PipeSettings>) (ps => ps.PipeName == pipeName));
    }

    internal static void RegisterPipeDefinitions(IDynamicModuleType dynamicModuleType)
    {
      List<IDefinitionField> source = new List<IDefinitionField>();
      string pipeName = ModuleNamesGenerator.GeneratePipeName(dynamicModuleType);
      source.Add((IDefinitionField) new SimpleDefinitionField("Id", "Id"));
      source.Add((IDefinitionField) new SimpleDefinitionField("LastModified", "Last modified"));
      source.Add((IDefinitionField) new SimpleDefinitionField("Status", "Status"));
      source.Add((IDefinitionField) new SimpleDefinitionField("Username", "Author username"));
      source.Add((IDefinitionField) new SimpleDefinitionField("OwnerFirstName", "Author first name"));
      source.Add((IDefinitionField) new SimpleDefinitionField("OwnerLastName", "Author last name"));
      source.Add((IDefinitionField) new SimpleDefinitionField("OwnerEmail", "Author email"));
      source.Add((IDefinitionField) new SimpleDefinitionField("Link", "Link"));
      source.Add((IDefinitionField) new SimpleDefinitionField("PipeId", "Pipe id"));
      source.Add((IDefinitionField) new SimpleDefinitionField("ContentType", "Content type"));
      source.Add((IDefinitionField) new SimpleDefinitionField("ItemHash", "Item hash"));
      source.Add((IDefinitionField) new SimpleDefinitionField("OriginalContentId", "Original content id"));
      source.Add((IDefinitionField) new SimpleDefinitionField("PublicationDate", "Publication date"));
      source.Add((IDefinitionField) new SimpleDefinitionField("ExpirationDate", "Expiration date"));
      foreach (IDynamicModuleField field in dynamicModuleType.Fields)
        source.Add((IDefinitionField) new SimpleDefinitionField(field.Name, field.Title));
      PublishingSystemFactory.RegisterPipeDefinitions(pipeName, (IList<IDefinitionField>) source.OrderBy<IDefinitionField, string>((Func<IDefinitionField, string>) (d => d.Title)).ToList<IDefinitionField>());
    }

    internal static void RegisterPipeMappings(IDynamicModuleType moduleType)
    {
      string pipeName = ModuleNamesGenerator.GeneratePipeName(moduleType);
      List<Mapping> mappingList = new List<Mapping>();
      string shortTextFieldName = moduleType.MainShortTextFieldName;
      mappingList.Add(PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, shortTextFieldName));
      mappingList.Add(PublishingSystemFactory.CreateMapping("PublicationDate", "TransparentTranslator", false, "PublicationDate"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("LastModified", "TransparentTranslator", true, "LastModified"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("Link", "TransparentTranslator", true, "Link"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("Username", "TransparentTranslator", false, "Username"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("OwnerFirstName", "TransparentTranslator", false, "OwnerFirstName"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("OwnerLastName", "TransparentTranslator", false, "OwnerLastName"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("OwnerEmail", "TransparentTranslator", false, "OwnerEmail"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("Id", "TransparentTranslator", true, "Id"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("OriginalItemId", "TransparentTranslator", true, "OriginalContentId"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("ExpirationDate", "TransparentTranslator", false, "ExpirationDate"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", false, "PipeId"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("ContentType", "TransparentTranslator", false, "ContentType"));
      mappingList.Add(PublishingSystemFactory.CreateMapping("LifecycleStatus", "TransparentTranslator", false, "LifecycleStatus"));
      foreach (IDynamicModuleField field in moduleType.Fields)
      {
        string moduleTypeFieldName = field.Name;
        if (!mappingList.Any<Mapping>((Func<Mapping, bool>) (m => m.DestinationPropertyName == moduleTypeFieldName)))
          mappingList.Add(PublishingSystemFactory.CreateMapping(moduleTypeFieldName, "TransparentTranslator", false, moduleTypeFieldName));
      }
      PublishingSystemFactory.RegisterPipeMappings(pipeName, true, (IList<Mapping>) mappingList);
    }
  }
}
