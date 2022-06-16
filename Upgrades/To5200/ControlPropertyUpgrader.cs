// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Upgrades.To5200.ControlPropertyUpgrader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Upgrades.To5200
{
  internal class ControlPropertyUpgrader
  {
    public void PrepareSchema(UpgradingContext context)
    {
      context.Connection.Backend.ConnectionPool.ActiveConnectionTimeout = 1800;
      context.Connection.Backend.Runtime.CommandTimeout = 1800;
      this.WithErrorHandling(new Func<DatabaseType, string>(this.GetControlPropsUpgradeScript), context, "OpenAccessPageProvider: Update control properties' flag field.");
      this.WithErrorHandling(new Func<DatabaseType, string>(this.GetObjectDataUpgradeScript), context, "OpenAccessPageProvider: Update object data version field.");
    }

    private string GetControlPropsUpgradeScript(DatabaseType databaseType)
    {
      string propsUpgradeScript = string.Empty;
      switch (databaseType)
      {
        case DatabaseType.MsSql:
          propsUpgradeScript = "ALTER TABLE sf_control_properties ADD flags int NOT NULL DEFAULT '0'";
          break;
        case DatabaseType.Oracle:
          propsUpgradeScript = "ALTER TABLE \"sf_control_properties\" ADD (flags INTEGER default 0 NOT NULL)";
          break;
        case DatabaseType.MySQL:
          propsUpgradeScript = "ALTER TABLE sf_control_properties ADD COLUMN flags INT NOT NULL DEFAULT 0";
          break;
      }
      return propsUpgradeScript;
    }

    private string GetObjectDataUpgradeScript(DatabaseType databaseType)
    {
      string dataUpgradeScript = string.Empty;
      switch (databaseType)
      {
        case DatabaseType.MsSql:
          dataUpgradeScript = "ALTER TABLE sf_object_data ADD vrsn int NOT NULL DEFAULT '0'";
          break;
        case DatabaseType.Oracle:
          dataUpgradeScript = "ALTER TABLE \"sf_object_data\" ADD (vrsn INTEGER default 0 NOT NULL)";
          break;
        case DatabaseType.MySQL:
          dataUpgradeScript = "ALTER TABLE sf_object_data ADD COLUMN vrsn INT NOT NULL DEFAULT 0";
          break;
      }
      return dataUpgradeScript;
    }

    private void WithErrorHandling(
      Func<DatabaseType, string> action,
      UpgradingContext context,
      string upgradeMessage)
    {
      try
      {
        string commandText = action(context.DatabaseContext.DatabaseType);
        if (string.IsNullOrEmpty(commandText))
          return;
        context.ExecuteNonQuery(commandText);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) upgradeMessage, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }
  }
}
