// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ExportImport.Deserializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Utilities.Zip;

namespace Telerik.Sitefinity.DynamicModules.Builder.ExportImport
{
  internal static class Deserializer
  {
    /// <summary>
    /// Deserialization dynamic module from a .xml file on the file system
    /// </summary>
    public static DynamicModule DeserializeModule(ZipFile dynamicModulezip)
    {
      DynamicModule dynamicModule = new DynamicModule();
      MemoryStream outputStream = new MemoryStream();
      dynamicModulezip.Extract("install.sf", (Stream) outputStream);
      outputStream.Position = 0L;
      using (outputStream)
      {
        XmlDictionaryReader textReader = XmlDictionaryReader.CreateTextReader((Stream) outputStream, new XmlDictionaryReaderQuotas());
        dynamicModule = (DynamicModule) new DataContractSerializer(typeof (DynamicModule)).ReadObject(textReader, true);
        textReader.Close();
      }
      ModuleBuilderHelper.SetDynamicModuleIds(dynamicModule);
      return dynamicModule;
    }

    public static DynamicModule DeserializeModule(Stream moduleStream)
    {
      DynamicModule dynamicModule = new DynamicModule();
      moduleStream.Position = 0L;
      using (moduleStream)
      {
        XmlDictionaryReader textReader = XmlDictionaryReader.CreateTextReader(moduleStream, XmlDictionaryReaderQuotas.Max);
        dynamicModule = (DynamicModule) new DataContractSerializer(typeof (DynamicModule)).ReadObject(textReader, true);
        textReader.Close();
      }
      ModuleBuilderHelper.SetDynamicModuleIds(dynamicModule);
      return dynamicModule;
    }
  }
}
