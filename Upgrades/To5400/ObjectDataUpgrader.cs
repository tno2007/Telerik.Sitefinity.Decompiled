// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Upgrades.To5400.ObjectDataUpgrader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Upgrades.To5400
{
  internal class ObjectDataUpgrader
  {
    public void PrepareSchema(UpgradingContext context)
    {
      context.Connection.Backend.ConnectionPool.ActiveConnectionTimeout = 1800;
      context.Connection.Backend.Runtime.CommandTimeout = 1800;
      this.WithErrorHandling(new Func<DatabaseType, string>(this.GetObjectDataUpgradeScript), context, "OpenAccessPageProvider: Update object data version field.");
    }

    private string GetObjectDataUpgradeScript(DatabaseType databaseType)
    {
      string dataUpgradeScript = string.Empty;
      if (databaseType == DatabaseType.MsSql)
        dataUpgradeScript = "ALTER TABLE sf_object_data ADD strategy smallint NOT NULL DEFAULT '0'";
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
      }
    }
  }
}
