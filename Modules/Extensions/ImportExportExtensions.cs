// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Extensions.ImportExportExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Modules.Extensions
{
  /// <summary>
  /// Provides extension methods used from import and export modules with configurations API
  /// </summary>
  [Obsolete("Use packaging module")]
  public static class ImportExportExtensions
  {
    /// <summary>Exports module meta types and configurations</summary>
    /// <param name="module">Module to be exported</param>
    /// <param name="outputStream">memory stream in which the module will be exported to</param>
    public static void ExportModuleStructure(this IExportableModule module, Stream outputStream)
    {
      ModuleImporterHelper instance = ModuleImporterHelper.GetInstance();
      string inputString = module.ExportModuleMetaData();
      instance.WriteToStream(string.Format("<{0}>", (object) module.ModuleName), outputStream);
      instance.WriteToStream(inputString, outputStream);
      instance.ExportContentModuleConfigurations(module.ModuleConfig, module.ModuleName, outputStream);
      instance.WriteToStream(string.Format("</{0}>", (object) module.ModuleName), outputStream);
    }

    /// <summary>Imports module meta types and configurations</summary>
    /// <param name="module">Module to be imported</param>
    /// <param name="deserializedModuleMemoryStream">deserialized module memory stream</param>
    public static void ImportModuleStructure(
      this IExportableModule module,
      Stream deserializedModuleMemoryStream)
    {
      List<MetaType> metaTypes = new List<MetaType>();
      Dictionary<Type, string> configurationsToImport = new Dictionary<Type, string>();
      ModuleImporterHelper instance = ModuleImporterHelper.GetInstance();
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
            if (xmlReader.Name.ToLower() == "metatype")
            {
              MetaType metaType = instance.DeserializeMetaType((Stream) new MemoryStream(Encoding.UTF8.GetBytes(xmlReader.ReadOuterXml())));
              metaTypes.Add(metaType);
            }
            else
              instance.ReadConfig(xmlReader, (IDictionary<Type, string>) configurationsToImport);
          }
          else
            xmlReader.Read();
        }
        xmlReader.Close();
      }
      string transactionName = nameof (ImportModuleStructure);
      MetadataManager manager1 = MetadataManager.GetManager((string) null, transactionName);
      IList<MetaType> moduleMetaTypes = module.GetModuleMetaTypes();
      if (metaTypes.Count > 0)
        instance.UpdateMetaData(manager1, (IList<MetaType>) metaTypes, moduleMetaTypes);
      if (moduleMetaTypes.Count > 0)
      {
        List<MetaType> list = moduleMetaTypes.Where<MetaType>((Func<MetaType, bool>) (pt => !metaTypes.Any<MetaType>((Func<MetaType, bool>) (mt => mt.FullTypeName == pt.FullTypeName)))).ToList<MetaType>();
        instance.DeleteMetaTypes(manager1, (IList<MetaType>) list);
      }
      TransactionManager.CommitTransaction(transactionName);
      TransactionManager.DisposeTransaction(transactionName);
      ConfigManager manager2 = ConfigManager.GetManager();
      manager2.RestoreSection(module.ModuleConfig);
      foreach (KeyValuePair<Type, string> keyValuePair in configurationsToImport)
        manager2.Import(keyValuePair.Key, keyValuePair.Value);
    }

    private static string ExportModuleMetaData(this IExportableModule module)
    {
      IList<MetaType> moduleMetaTypes = module.GetModuleMetaTypes();
      DataContractSerializer contractSerializer = new DataContractSerializer(typeof (MetaType));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        foreach (MetaType graph in (IEnumerable<MetaType>) moduleMetaTypes)
          contractSerializer.WriteObject((Stream) memoryStream, (object) graph);
        return Encoding.Default.GetString(memoryStream.ToArray());
      }
    }
  }
}
