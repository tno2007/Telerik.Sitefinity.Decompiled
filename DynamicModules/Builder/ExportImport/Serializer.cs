// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ExportImport.Serializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Utilities.Zip;

namespace Telerik.Sitefinity.DynamicModules.Builder.ExportImport
{
  internal static class Serializer
  {
    /// <summary>
    /// Serialize the dynamic module to a install.sf file and add it to a Zip archive
    /// </summary>
    public static ZipFile SerializeDynamicModule(DynamicModule module, Stream outputStream)
    {
      Stream stream = (Stream) new MemoryStream();
      using (stream)
      {
        new DataContractSerializer(typeof (DynamicModule)).WriteObject(stream, (object) module);
        string str = module.Name + ".zip";
        ZipFile zipFile = new ZipFile();
        zipFile.AddFileStream("install.sf", "", stream);
        zipFile.Save(outputStream);
        return zipFile;
      }
    }

    public static string SerializeObjectToXmlString(object objectToSerialize)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractSerializer(objectToSerialize.GetType()).WriteObject((Stream) memoryStream, objectToSerialize);
        return Encoding.UTF8.GetString(memoryStream.ToArray());
      }
    }
  }
}
