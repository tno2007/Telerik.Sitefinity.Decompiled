// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ModuleImporterHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.Modules
{
  internal class ModuleImporterHelper
  {
    private static ConfigManager configurationManager = ConfigManager.GetManager();
    private static ConfigSection[] allConfigurationSections = ModuleImporterHelper.configurationManager.GetAllConfigSections();
    private static readonly ConcurrentProperty<ModuleImporterHelper> Instance = new ConcurrentProperty<ModuleImporterHelper>((Func<ModuleImporterHelper>) (() => new ModuleImporterHelper()));

    private ModuleImporterHelper()
    {
    }

    public static ModuleImporterHelper GetInstance() => ModuleImporterHelper.Instance.Value;

    internal MetaType DeserializeMetaType(Stream stream)
    {
      MetaType metaType = new MetaType();
      stream.Position = 0L;
      using (stream)
      {
        XmlDictionaryReader textReader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
        metaType = (MetaType) new DataContractSerializer(typeof (MetaType)).ReadObject(textReader, true);
        textReader.Close();
      }
      return metaType;
    }

    internal void UpdateMetaData(
      MetadataManager metadataManager,
      IList<MetaType> metaTypes,
      IList<MetaType> persistedTypes)
    {
      foreach (MetaType metaType1 in (IEnumerable<MetaType>) metaTypes)
      {
        MetaType metaType = metaType1;
        MetaType persistedType = persistedTypes.SingleOrDefault<MetaType>((Func<MetaType, bool>) (t => t.FullTypeName == metaType.FullTypeName));
        if (persistedType == null)
        {
          persistedType = metadataManager.CreateMetaType(metaType.Namespace, metaType.ClassName);
          persistedType.Origin = metaType.Origin;
        }
        this.UpdateMetaType(metadataManager, metaType, persistedType);
      }
    }

    internal void DeleteMetaTypes(
      MetadataManager metadataManager,
      IList<MetaType> metaTypesToDelete)
    {
      foreach (MetaType metaType in (IEnumerable<MetaType>) metaTypesToDelete)
        metaType.IsDeleted = true;
    }

    internal void UpdateMetaType(
      MetadataManager metadataManager,
      MetaType metaType,
      MetaType persistedType)
    {
      string currentOrigin = StructureTransferBase.CurrentOrigin;
      List<MetaField> list1 = metaType.Fields.Where<MetaField>((Func<MetaField, bool>) (f => !persistedType.Fields.Any<MetaField>((Func<MetaField, bool>) (pf => pf.FieldName == f.FieldName)))).ToList<MetaField>();
      List<MetaField> list2 = metaType.Fields.Where<MetaField>((Func<MetaField, bool>) (f => persistedType.Fields.Any<MetaField>((Func<MetaField, bool>) (pf =>
      {
        if (!(pf.FieldName == f.FieldName))
          return false;
        return StructureTransferBase.CurrentAddOnName == null || string.IsNullOrEmpty(pf.Origin) || pf.Origin == currentOrigin;
      })))).ToList<MetaField>();
      List<MetaField> list3 = persistedType.Fields.Where<MetaField>((Func<MetaField, bool>) (f => StructureTransferBase.IsDeleteAllowedForItem((IHasOrigin) f) && !metaType.Fields.Any<MetaField>((Func<MetaField, bool>) (pf => pf.FieldName == f.FieldName)))).ToList<MetaField>();
      metadataManager.CopyMetaType(metaType, persistedType);
      metadataManager.CopyMetaTypeAttributes(metaType, persistedType);
      foreach (MetaField source in list1)
      {
        MetaField metafield = metadataManager.CreateMetafield(source.FieldName);
        persistedType.Fields.Add(metafield);
        MetadataManager.CopyMetafield((IMetaField) source, metafield);
        metafield.ApplicationName = metadataManager.Provider.ApplicationName;
        metafield.Origin = source.Origin;
        metadataManager.CopyMetaFieldAttributes(source, metafield);
      }
      foreach (MetaField metaField in list2)
      {
        MetaField field = metaField;
        MetaField target = persistedType.Fields.Single<MetaField>((Func<MetaField, bool>) (f => f.FieldName == field.FieldName));
        MetadataManager.CopyMetafield((IMetaField) field, target);
        target.ApplicationName = metadataManager.Provider.ApplicationName;
        metadataManager.CopyMetaFieldAttributes(field, target);
      }
      foreach (MetaField metaField in list3)
        metaField.IsDeleted = true;
    }

    /// <summary>
    /// This method exports only static module configurations and returns them exported into the memory stream passed
    /// </summary>
    /// <param name="moduleConfig">The base configuration of a given static module.</param>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="memoryStream">The stream in which to export the configurations.</param>
    /// <param name="exportProviders">A value indicating whether to export providers as well.</param>
    /// <param name="exportMode">The export mode.</param>
    internal void ExportContentModuleConfigurations(
      ConfigSection moduleConfig,
      string moduleName,
      Stream memoryStream,
      bool exportProviders = false,
      ExportMode exportMode = ExportMode.Default)
    {
      this.ExportContentModuleWorkflowConfiguration(moduleName, memoryStream, exportMode);
      this.ExportContentModuleToolboxConfiguration(moduleName, memoryStream, exportMode);
      this.ExportContentModuleContentViewConfiguration(moduleName, memoryStream);
      this.ExportContentModuleBaseConfiguration(moduleConfig, exportProviders, memoryStream, exportMode);
    }

    /// <summary>
    /// Exports the workflow config of a given static module and adds it to the passed memory stream.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="memoryStream">The memory stream.</param>
    /// <param name="exportMode">The export mode.</param>
    internal void ExportContentModuleWorkflowConfiguration(
      string moduleName,
      Stream memoryStream,
      ExportMode exportMode)
    {
      IEnumerable<WorkflowElement> configElementsToBeExported = Telerik.Sitefinity.Configuration.Config.Get<WorkflowConfig>().Workflows.Elements.Where<WorkflowElement>((Func<WorkflowElement, bool>) (e => e != null && e.ModuleName == moduleName));
      this.WriteToStream(ModuleImporterHelper.configurationManager.Export((IEnumerable<ConfigElement>) configElementsToBeExported, exportMode: exportMode), memoryStream);
    }

    /// <summary>
    /// Exports the toolbox config of a given static module and adds it to the passed memory stream.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="memoryStream">The memory stream.</param>
    /// <param name="exportMode">The export mode.</param>
    internal void ExportContentModuleToolboxConfiguration(
      string moduleName,
      Stream memoryStream,
      ExportMode exportMode)
    {
      IEnumerable<ToolboxItem> configElementsToBeExported = Telerik.Sitefinity.Configuration.Config.Get<ToolboxesConfig>().Toolboxes["PageControls"].Sections.SelectMany<ToolboxSection, ToolboxItem>((Func<ToolboxSection, IEnumerable<ToolboxItem>>) (s => s.Tools.Elements.Where<ToolboxItem>((Func<ToolboxItem, bool>) (t => t != null && t.ModuleName == moduleName))));
      this.WriteToStream(ModuleImporterHelper.configurationManager.Export((IEnumerable<ConfigElement>) configElementsToBeExported, exportMode: exportMode), memoryStream);
    }

    /// <summary>
    /// Exports the content view configuration of a given static module and adds it to the passed memory stream.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="memoryStream">The memory stream.</param>
    internal void ExportContentModuleContentViewConfiguration(
      string moduleName,
      Stream memoryStream)
    {
      ContentViewConfig contentViewConfig = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>();
      List<ConfigElement> configElementsToBeExported = new List<ConfigElement>();
      foreach (string key in (IEnumerable<string>) contentViewConfig.ContentViewControls.Keys)
      {
        IConfigElementItem configElementItem;
        if (contentViewConfig.ContentViewControls.TryGetItem(key, out configElementItem) && configElementItem is IConfigElementLink configElementLink && configElementLink.Element != null && configElementLink.ModuleName == moduleName)
        {
          configElementLink.Element.LinkModuleName = configElementLink.ModuleName;
          configElementsToBeExported.Add(configElementLink.Element);
        }
      }
      this.WriteToStream(ModuleImporterHelper.configurationManager.Export(typeof (ContentViewConfig), (IEnumerable<ConfigElement>) configElementsToBeExported, true), memoryStream);
    }

    /// <summary>
    /// Exports the base configuration, containing all content view control elements of a given static module and adds it to the passed memory stream.
    /// </summary>
    /// <param name="moduleConfig">The module configuration.</param>
    /// <param name="exportProviders">if set to <c>true</c> [export providers].</param>
    /// <param name="memoryStream">The memory stream.</param>
    /// <param name="exportMode">The export mode.</param>
    internal void ExportContentModuleBaseConfiguration(
      ConfigSection moduleConfig,
      bool exportProviders,
      Stream memoryStream,
      ExportMode exportMode)
    {
      IEnumerable<ConfigElement> configurationElements = this.GetContentModuleBaseConfigurationElements(moduleConfig, exportProviders);
      bool skipLoadFromFile = !exportProviders;
      this.ExportContentModuleConfigurationElements(configurationElements, memoryStream, skipLoadFromFile, exportMode);
    }

    /// <summary>
    /// Exports the base configuration, containing all content view control elements of a given static module and adds it to the passed memory stream.
    /// </summary>
    /// <param name="moduleConfig">The base configuration of a given static module.</param>
    /// <param name="exportProviders">A value indicating whether to export providers as well.</param>
    /// <returns>A collection of configuration elements from the base configuration of a given static module.</returns>
    internal IEnumerable<ConfigElement> GetContentModuleBaseConfigurationElements(
      ConfigSection moduleConfig,
      bool exportProviders)
    {
      ContentModuleConfigBase section = moduleConfig.Section as ContentModuleConfigBase;
      List<ConfigElement> configurationElements = new List<ConfigElement>();
      configurationElements.AddRange((IEnumerable<ConfigElement>) section.ContentViewControls.Elements.ToList<ContentViewControlElement>());
      if (exportProviders)
        configurationElements.AddRange((IEnumerable<ConfigElement>) section.Providers.Elements);
      return (IEnumerable<ConfigElement>) configurationElements;
    }

    /// <summary>
    /// Exports the base configuration, containing all content view control elements of a given static module and adds it to the passed memory stream.
    /// </summary>
    /// <param name="configElementsToBeExported">The configuration elements to be exported.</param>
    /// <param name="memoryStream">The memory stream.</param>
    /// <param name="skipLoadFromFile">if set to <c>true</c> [skip load from file].</param>
    /// <param name="exportMode">The export mode.</param>
    internal virtual void ExportContentModuleConfigurationElements(
      IEnumerable<ConfigElement> configElementsToBeExported,
      Stream memoryStream,
      bool skipLoadFromFile,
      ExportMode exportMode)
    {
      this.WriteToStream(ModuleImporterHelper.configurationManager.Export(configElementsToBeExported, skipLoadFromFile, exportMode), memoryStream);
    }

    internal void ReadConfig(XmlReader xmlReader, IDictionary<Type, string> configurationsToImport)
    {
      ConfigSection configSection = ((IEnumerable<ConfigSection>) ModuleImporterHelper.configurationManager.GetAllConfigSections()).FirstOrDefault<ConfigSection>((Func<ConfigSection, bool>) (s => s.TagName == xmlReader.Name));
      if (configSection != null)
        configurationsToImport[configSection.GetType()] = xmlReader.ReadOuterXml();
      else
        xmlReader.Read();
    }

    /// <summary>
    /// Writes the content from the provided string into the provided stream.
    /// </summary>
    /// <param name="inputString">A string from which to read content and pass it to the output stream.</param>
    /// <param name="outputStream">The stream in which to write the content of the input string.</param>
    internal void WriteToStream(string inputString, Stream outputStream)
    {
      using (Stream streamFromString = ModuleImporterHelper.GetStreamFromString(inputString))
      {
        byte[] buffer = new byte[4096];
        int count;
        while ((count = streamFromString.Read(buffer, 0, buffer.Length)) > 0)
          outputStream.Write(buffer, 0, count);
      }
    }

    /// <summary>
    /// Creates new Stream from the content of the provided string.
    /// </summary>
    /// <param name="s">A string to create new stream from.</param>
    /// <returns>A new memory stream from the content of the provided string.</returns>
    private static Stream GetStreamFromString(string s)
    {
      MemoryStream streamFromString = new MemoryStream();
      StreamWriter streamWriter = new StreamWriter((Stream) streamFromString);
      streamWriter.Write(s);
      streamWriter.Flush();
      streamFromString.Position = 0L;
      return (Stream) streamFromString;
    }
  }
}
