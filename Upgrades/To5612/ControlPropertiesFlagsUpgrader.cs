// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Upgrades.To5612.ControlPropertiesFlagsUpgrader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Data.Common;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Upgrades.To5612
{
  internal class ControlPropertiesFlagsUpgrader
  {
    private const string UpdateListItemsFlagTSql = "\r\nUPDATE CP\r\nSET flags = CP.flags | (2 + 1)\r\nFROM sf_control_properties CP\r\nINNER JOIN sf_object_data OD ON OD.parent_prop_id = CP.id\r\n";
    private const string UpdateChildPropertiesFlagTSql = "\r\nUPDATE CP\r\nSET flags = CP.flags | (4 + 1)\r\nFROM sf_control_properties CPChild\r\nINNER JOIN sf_control_properties CP ON CPChild.prnt_prop_id = CP.id\r\n";
    private const string UpdatePresentationDataFlagTSql = "\r\nUPDATE CP\r\nSET flags = CP.flags | (8 + 1)\r\nFROM sf_control_properties CP\r\nINNER JOIN sf_presentation_data PD ON PD.item_id = CP.id\r\n";
    private const int CommandTimeout = 1000;

    public void Upgrade(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (context == null || context.Connection == null)
        return;
      switch (context.Connection.DbType)
      {
        case DatabaseType.MsSql:
        case DatabaseType.SqlAzure:
        case DatabaseType.SqlCE:
          ControlPropertiesFlagsUpgrader.ExecuteUpdateScript("\r\nUPDATE CP\r\nSET flags = CP.flags | (2 + 1)\r\nFROM sf_control_properties CP\r\nINNER JOIN sf_object_data OD ON OD.parent_prop_id = CP.id\r\n", context);
          ControlPropertiesFlagsUpgrader.ExecuteUpdateScript("\r\nUPDATE CP\r\nSET flags = CP.flags | (4 + 1)\r\nFROM sf_control_properties CPChild\r\nINNER JOIN sf_control_properties CP ON CPChild.prnt_prop_id = CP.id\r\n", context);
          ControlPropertiesFlagsUpgrader.ExecuteUpdateScript("\r\nUPDATE CP\r\nSET flags = CP.flags | (8 + 1)\r\nFROM sf_control_properties CP\r\nINNER JOIN sf_presentation_data PD ON PD.item_id = CP.id\r\n", context);
          break;
      }
    }

    private static void ExecuteUpdateScript(string updateScript, UpgradingContext context)
    {
      string str = "ControlProperties optimization - Upgrade to {0}".Arrange((object) SitefinityVersion.Sitefinity7_3_HF2.Build.ToString());
      try
      {
        using (OACommand command = ((OpenAccessContextBase) context).Connection.CreateCommand())
        {
          context.Connection.Backend.ConnectionPool.ActiveConnectionTimeout = 300;
          command.CommandTimeout = 300;
          command.CommandText = updateScript;
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("Failed : {0}, Script: {1}. You can run it manually on the database.", (object) str, (object) updateScript), ConfigurationPolicy.UpgradeTrace);
        throw;
      }
    }
  }
}
