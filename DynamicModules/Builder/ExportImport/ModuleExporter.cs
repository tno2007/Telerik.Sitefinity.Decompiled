// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ExportImport.ModuleExporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Utilities.Zip;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.DynamicModules.Builder.ExportImport
{
  /// <summary>Provides a functionality to export Dynamic Modules</summary>
  public static class ModuleExporter
  {
    /// <summary>
    /// Exports dynamic module structure and configurations in stream
    /// </summary>
    /// <param name="dynamicModule">Module to export</param>
    /// <param name="outputStream">Stream to write the exported module</param>
    public static void ExportModule(DynamicModule dynamicModule, MemoryStream outputStream)
    {
      string xmlString = Serializer.SerializeObjectToXmlString((object) dynamicModule);
      ModuleExporter.WriteToStream("<root>", (Stream) outputStream);
      MemoryStream outputStream1 = outputStream;
      ModuleExporter.WriteToStream(xmlString, (Stream) outputStream1);
      ModuleExporter.ExportModuleConfigurations(dynamicModule, outputStream);
      ModuleExporter.WriteToStream("</root>", (Stream) outputStream);
    }

    /// <summary>Returns exported module zip file</summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="outputStream">The output stream.</param>
    /// <returns>The module as a zip file</returns>
    internal static ZipFile ExportModuleToZip(
      DynamicModule dynamicModule,
      Stream outputStream)
    {
      if (dynamicModule == null)
        throw new ArgumentNullException("Dynamic module argument is null!");
      ZipFile zip = new ZipFile();
      using (MemoryStream outputStream1 = new MemoryStream())
      {
        string xmlString = Serializer.SerializeObjectToXmlString((object) dynamicModule);
        ModuleExporter.WriteToStream("<root>", (Stream) outputStream1);
        MemoryStream outputStream2 = outputStream1;
        ModuleExporter.WriteToStream(xmlString, (Stream) outputStream2);
        ModuleExporter.WriteToStream("</root>", (Stream) outputStream1);
        zip.AddFileStream("install.sf", string.Empty, (Stream) outputStream1);
        zip.Save(outputStream);
      }
      return zip;
    }

    /// <summary>
    /// Exports dynamic module structure and configurations in zip package
    /// </summary>
    /// <param name="dynamicModule">Module to export</param>
    /// <param name="outputStream">Stream to write the exported zip package</param>
    internal static void ExportModuleAsZip(DynamicModule dynamicModule, Stream outputStream)
    {
      if (dynamicModule == null)
        throw new ArgumentNullException("Dynamic module argument is null!");
      using (ZipFile zipFile = new ZipFile())
      {
        using (MemoryStream outputStream1 = new MemoryStream())
        {
          ModuleExporter.ExportModule(dynamicModule, outputStream1);
          zipFile.AddFileStream("install.sf", string.Empty, (Stream) outputStream1);
          zipFile.Save(outputStream);
        }
      }
    }

    internal static void ExportModuleConfigurations(
      DynamicModule module,
      MemoryStream memoryStream,
      bool exportModuleProviders = false,
      ExportMode exportMode = ExportMode.Default)
    {
      List<string> list = ((IEnumerable<DynamicModuleType>) module.Types).SelectMany<DynamicModuleType, string>((Func<DynamicModuleType, IEnumerable<string>>) (t => (IEnumerable<string>) new List<string>()
      {
        t.TypeNamespace + "." + t.TypeName,
        t.TypeNamespace + "." + t.TypeName + "_MVC"
      })).ToList<string>();
      IEnumerable<string> allTypesDefinitionKeys = list.Select<string, string>((Func<string, string>) (t => ModuleNamesGenerator.GenerateContentViewDefinitionName(t)));
      if (list.Count <= 0)
        return;
      ConfigManager manager = ConfigManager.GetManager();
      ModuleExporter.ExportWorkflowConfiguration(list, exportMode, manager, memoryStream);
      ModuleExporter.ExportToolboxConfiguration(module, list, exportMode, manager, memoryStream);
      ModuleExporter.ExportDynamicModulesConfiguration(allTypesDefinitionKeys, exportModuleProviders, module, exportMode, manager, memoryStream);
      ModuleExporter.ExportContentViewConfiguration(allTypesDefinitionKeys, manager, memoryStream);
    }

    internal static void WriteToStream(string inputString, Stream outputStream)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) memoryStream))
        {
          streamWriter.Write(inputString);
          streamWriter.Flush();
          memoryStream.Position = 0L;
          byte[] buffer = new byte[4096];
          int count;
          while ((count = memoryStream.Read(buffer, 0, buffer.Length)) > 0)
            outputStream.Write(buffer, 0, count);
        }
      }
    }

    internal static event ModuleExporter.DynamicModulesConfigLoadedHandler DynamicModulesConfigLoaded;

    private static void ExportContentViewConfiguration(
      IEnumerable<string> allTypesDefinitionKeys,
      ConfigManager configurationManager,
      MemoryStream memoryStream)
    {
      ContentViewConfig contentViewConfig = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>();
      List<ConfigElement> configElementsToBeExported = new List<ConfigElement>();
      foreach (string key in contentViewConfig.ContentViewControls.Keys.Where<string>((Func<string, bool>) (k => k != null && allTypesDefinitionKeys.Contains<string>(k))).ToList<string>())
      {
        IConfigElementItem configElementItem;
        if (contentViewConfig.ContentViewControls.TryGetItem(key, out configElementItem))
        {
          if (configElementItem is IConfigElementLink configElementLink)
          {
            if (configElementLink.Element != null)
            {
              configElementLink.Element.LinkModuleName = configElementLink.ModuleName;
              configElementsToBeExported.Add(configElementLink.Element);
            }
          }
          else
            configElementsToBeExported.Add(configElementItem.Element);
        }
      }
      ModuleExporter.WriteToStream(configurationManager.Export(typeof (ContentViewConfig), (IEnumerable<ConfigElement>) configElementsToBeExported, true), (Stream) memoryStream);
    }

    private static void ExportDynamicModulesConfiguration(
      IEnumerable<string> allTypesDefinitionKeys,
      bool exportModuleProviders,
      DynamicModule module,
      ExportMode exportMode,
      ConfigManager configurationManager,
      MemoryStream memoryStream)
    {
      DynamicModulesConfig dynamicModulesConfig = Telerik.Sitefinity.Configuration.Config.Get<DynamicModulesConfig>();
      List<ConfigElement> configElementsToBeExported = new List<ConfigElement>();
      List<ContentViewControlElement> list1 = dynamicModulesConfig.ContentViewControls.Elements.Where<ContentViewControlElement>((Func<ContentViewControlElement, bool>) (k => k != null && allTypesDefinitionKeys.Contains<string>(k.GetKey()))).ToList<ContentViewControlElement>();
      if (ModuleExporter.DynamicModulesConfigLoaded != null)
        ModuleExporter.DynamicModulesConfigLoaded(new ModuleExporter.DynamicModulesConfigLoadedEventArgs()
        {
          ControlElements = list1
        });
      if (exportModuleProviders)
      {
        List<DataProviderSettings> list2 = dynamicModulesConfig.Providers.Elements.Where<DataProviderSettings>((Func<DataProviderSettings, bool>) (p => p != null && p.Parameters["moduleName"] == module.Name)).ToList<DataProviderSettings>();
        if (list2.Count<DataProviderSettings>() != 0)
          configElementsToBeExported.AddRange((IEnumerable<ConfigElement>) list2);
      }
      if (list1.Count<ContentViewControlElement>() != 0)
        configElementsToBeExported.AddRange((IEnumerable<ConfigElement>) list1);
      ModuleExporter.WriteToStream(configurationManager.Export((IEnumerable<ConfigElement>) configElementsToBeExported, true, exportMode), (Stream) memoryStream);
    }

    private static void ExportToolboxConfiguration(
      DynamicModule module,
      List<string> allTypes,
      ExportMode exportMode,
      ConfigManager configurationManager,
      MemoryStream memoryStream)
    {
      Toolbox toolbox = Telerik.Sitefinity.Configuration.Config.Get<ToolboxesConfig>().Toolboxes["PageControls"];
      List<ConfigElement> configElementsToBeExported = new List<ConfigElement>();
      string moduleSectionName = ModuleBuilderManager.GetModuleNamespace(module.Name);
      IEnumerable<ToolboxSection> toolboxSections = toolbox.Sections.Where<ToolboxSection>((Func<ToolboxSection, bool>) (s => s.Name == moduleSectionName));
      IEnumerable<ToolboxItem> source = toolboxSections.SelectMany<ToolboxSection, ToolboxItem>((Func<ToolboxSection, IEnumerable<ToolboxItem>>) (ts => (IEnumerable<ToolboxItem>) ts.Tools));
      IEnumerable<ToolboxItem> toolboxItems = toolbox.Sections.SelectMany<ToolboxSection, ToolboxItem>((Func<ToolboxSection, IEnumerable<ToolboxItem>>) (s => s.Tools.Elements.Where<ToolboxItem>((Func<ToolboxItem, bool>) (t => t != null && allTypes.Contains(t.GetKey())))));
      configElementsToBeExported.AddRange((IEnumerable<ConfigElement>) toolboxSections);
      foreach (ToolboxItem toolboxItem in toolboxItems)
      {
        if (!source.Contains<ToolboxItem>(toolboxItem))
          configElementsToBeExported.Add((ConfigElement) toolboxItem);
      }
      ModuleExporter.WriteToStream(configurationManager.Export((IEnumerable<ConfigElement>) configElementsToBeExported, true, exportMode), (Stream) memoryStream);
    }

    private static void ExportWorkflowConfiguration(
      List<string> allTypes,
      ExportMode exportMode,
      ConfigManager configurationManager,
      MemoryStream memoryStream)
    {
      List<WorkflowElement> list = Telerik.Sitefinity.Configuration.Config.Get<WorkflowConfig>().Workflows.Elements.Where<WorkflowElement>((Func<WorkflowElement, bool>) (e => e != null && allTypes.Contains(e.GetKey()))).ToList<WorkflowElement>();
      ModuleExporter.WriteToStream(configurationManager.Export((IEnumerable<ConfigElement>) list, true, exportMode), (Stream) memoryStream);
    }

    internal delegate void DynamicModulesConfigLoadedHandler(
      ModuleExporter.DynamicModulesConfigLoadedEventArgs e);

    internal class DynamicModulesConfigLoadedEventArgs : EventArgs
    {
      public List<ContentViewControlElement> ControlElements { get; set; }
    }
  }
}
