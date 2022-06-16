// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigMigrationExporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Data;
using System.IO;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Configuration.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Configuration
{
  internal class ConfigMigrationExporter
  {
    internal const string XmlConfigItemsPath = "~/App_Data/Sitefinity/Migration/sf_xml_config_items.xml";

    public void Handle(OpenAccessContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      using (OACommand command = context.Connection.CreateCommand())
      {
        string tableName = ConfigMigrationExporter.GetTableName(context);
        command.CommandText = "SELECT * FROM " + tableName;
        using (OADataReader reader = command.ExecuteReader())
        {
          DataTable dataTable = new DataTable(tableName);
          dataTable.Load((IDataReader) reader);
          dataTable.WriteXml(ConfigMigrationExporter.GetOrCreatePath(), XmlWriteMode.WriteSchema);
        }
      }
    }

    private static string GetTableName(OpenAccessContext context) => context.Metadata.PersistentTypes.Single<MetaPersistentType>((Func<MetaPersistentType, bool>) (pt => string.Equals(pt.FullName, typeof (XmlConfigItem).FullName))).Table.Name;

    private static string GetOrCreatePath()
    {
      string path = SystemManager.CurrentHttpContext.Server.MapPath("~/App_Data/Sitefinity/Migration/sf_xml_config_items.xml");
      string directoryName = Path.GetDirectoryName(path);
      if (Directory.Exists(directoryName))
        return path;
      Directory.CreateDirectory(directoryName);
      return path;
    }
  }
}
