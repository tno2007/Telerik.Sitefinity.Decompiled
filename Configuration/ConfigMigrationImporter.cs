// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigMigrationImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Transactions;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Configuration.Model;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Configuration
{
  internal class ConfigMigrationImporter
  {
    internal const string XmlConfigItemsPath = "~/App_Data/Sitefinity/Migration/sf_xml_config_items.xml";

    public bool CanHandle(
      IDictionary<string, SystemManager.ModuleVersionInfo> moduleVersions)
    {
      if (moduleVersions == null)
        throw new ArgumentNullException(nameof (moduleVersions));
      if (!moduleVersions.ContainsKey("f2984670-c099-4157-9fad-f6915db28ad6"))
        return false;
      string path = SystemManager.CurrentHttpContext.Server.MapPath("~/App_Data/Sitefinity/Migration/sf_xml_config_items.xml");
      return this.GetStorageMode() == ConfigStorageMode.Auto && File.Exists(path);
    }

    public void Handle(OpenAccessContext context, MetadataManager manager)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      if (manager == null)
        throw new ArgumentNullException(nameof (manager));
      this.ImportConfigurationData(context);
      this.DeleteConfigMigrationModuleRecord(manager);
    }

    protected virtual ConfigStorageMode GetStorageMode() => Config.ConfigStorageMode;

    private static string GetTableName(OpenAccessContext context) => context.Metadata.PersistentTypes.Single<MetaPersistentType>((Func<MetaPersistentType, bool>) (pt => string.Equals(pt.FullName, typeof (XmlConfigItem).FullName))).Table.Name;

    private void ImportConfigurationData(OpenAccessContext context)
    {
      using (DataTable dataTable = new DataTable())
      {
        string str = SystemManager.CurrentHttpContext.Server.MapPath("~/App_Data/Sitefinity/Migration/sf_xml_config_items.xml");
        if (!File.Exists(str))
          return;
        int num = (int) dataTable.ReadXml(str);
        using (TransactionScope transactionScope = new TransactionScope())
        {
          DbProviderFactory propertyValue = context.Connection.StoreConnection.GetPropertyValue("DbProviderFactory") as DbProviderFactory;
          DbDataAdapter dataAdapter = propertyValue.CreateDataAdapter();
          using (DbCommandBuilder commandBuilder = propertyValue.CreateCommandBuilder())
          {
            commandBuilder.DataAdapter = dataAdapter;
            DbCommand command = context.Connection.StoreConnection.CreateCommand();
            command.CommandText = "SELECT * FROM " + ConfigMigrationImporter.GetTableName(context);
            dataAdapter.SelectCommand = command;
            dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            dataAdapter.UpdateBatchSize = 1000;
            dataAdapter.Update(dataTable);
          }
          transactionScope.Complete();
        }
      }
    }

    private void DeleteConfigMigrationModuleRecord(MetadataManager manager)
    {
      ModuleVersion moduleVersion = manager.GetModuleVersion("f2984670-c099-4157-9fad-f6915db28ad6");
      if (moduleVersion == null)
        return;
      manager.DeleteModuleVersion(moduleVersion);
      manager.SaveChanges();
    }
  }
}
