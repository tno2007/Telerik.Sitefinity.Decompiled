// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Data.WorkflowNewModelUpgradeScript
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow.Data
{
  internal class WorkflowNewModelUpgradeScript
  {
    private OpenAccessWorkflowProvider provider;
    private UpgradingContext context;
    private int upgradedFromSchemaVersionNumber;

    public WorkflowNewModelUpgradeScript(
      OpenAccessWorkflowProvider provider,
      UpgradingContext context,
      int upgradedFromSchemaVersionNumber)
    {
      this.provider = provider;
      this.context = context;
      this.upgradedFromSchemaVersionNumber = upgradedFromSchemaVersionNumber;
    }

    internal static void UpgradeDefaultAdministratorsPermissions_Old80Upgrade()
    {
      ApplicationRole adminRoleConfig = Config.Get<SecurityConfig>().ApplicationRoles["Administrators"];
      WorkflowManager manager = WorkflowManager.GetManager();
      foreach (WorkflowLevel workflowLevel in (IEnumerable<WorkflowLevel>) manager.GetWorkflowLevels())
      {
        WorkflowLevel level = workflowLevel;
        if (!level.Permissions.Where<WorkflowPermission>((Func<WorkflowPermission, bool>) (p => p.ActionName == level.ActionName && p.PrincipalId == adminRoleConfig.Id.ToString())).Any<WorkflowPermission>())
        {
          Guid guid = Guid.NewGuid();
          WorkflowPermission workflowPermission = new WorkflowPermission()
          {
            Id = guid,
            Level = level,
            ActionName = level.ActionName,
            PrincipalName = "Administrators",
            PrincipalId = adminRoleConfig.Id.ToString(),
            PrincipalType = WorkflowPrincipalType.Role,
            ApplicationName = level.ApplicationName
          };
        }
      }
      manager.SaveChanges();
    }

    internal void Upgrade_To_Sitefinity_11_1()
    {
      if (this.upgradedFromSchemaVersionNumber >= 6800)
        return;
      string upgradeDescription = "Upgrading Workflow model...";
      try
      {
        Log.Write((object) string.Format("START : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
        this.UpgradeWorkflowDefinitions(ref upgradeDescription);
        this.UpgradeWorkflowDefinitionSitefinityPermissions(ref upgradeDescription);
        this.UpgradeWorkflowDefinitionPermissionSetObjectTitle(ref upgradeDescription);
        this.UpgradeWorkflowDefinitionSupportedPermissionSets(ref upgradeDescription);
        this.UpgradeFirstLevelsOfDefinitions(ref upgradeDescription);
        this.UpgradeSecondLevelsOfDefinitions(ref upgradeDescription);
        this.UpgradeWorkflowPermissionsForFirstLevels(ref upgradeDescription);
        this.UpgradeWorkflowPermissionsForSecondLevels(ref upgradeDescription);
        this.UpgradeCustomMailRecipientsForFirstLevels(ref upgradeDescription);
        this.UpgradeCustomMailRecipientsForSecondLevels(ref upgradeDescription);
        this.UpgradeWorkflowScopes(ref upgradeDescription);
        this.UpgradeWorkflowTypeScopes(ref upgradeDescription);
        this.UpgradeSiteItemLinksForScopes(ref upgradeDescription);
        this.UpgradeWorkflowScopesPerSiteAndLanguage(ref upgradeDescription);
        this.context.SaveChanges();
        upgradeDescription = "Upgraded Workflow Model";
        Log.Write((object) string.Format("PASSED : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        this.context.ClearChanges();
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) upgradeDescription, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "For better view of the SQL query")]
    private void UpgradeWorkflowDefinitions(ref string upgradeDescription)
    {
      upgradeDescription = "Create workflow definitions.";
      string commandText = "INSERT INTO sf_wfl_definition\r\n                                        (id\r\n                                        ,title_\r\n                                        ,is_active\r\n                                        ,workflow_type\r\n                                        ,ownr\r\n                                        ,allow_notes\r\n                                        ,custom_xamlx_url\r\n                                        ,can_inherit_permissions\r\n                                        ,inherits_permissions\r\n                                        ,application_name\r\n                                        ,allw_pblshers_to_skip_workflow\r\n                                        ,allw_dmnstrtrs_t_skip_workflow\r\n                                        ,voa_version\r\n                                        ,date_created\r\n                                        ,last_modified)\r\n                                SELECT  id\r\n                                        ,title_\r\n                                        ,is_active\r\n                                        ,workflow_type\r\n                                        ,ownr\r\n                                        ,0\r\n                                        ,custom_xamlx_url\r\n                                        ,can_inherit_permissions\r\n                                        ,inherits_permissions\r\n                                        ,application_name\r\n                                        ,allw_pblshers_to_skip_workflow\r\n                                        ,allw_dmnstrtrs_t_skip_workflow\r\n                                        ,voa_version\r\n                                        ,date_created\r\n                                        ,last_modified\r\n                                FROM sf_workflow_definition";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_definition\"\r\n                                        (\"id\"\r\n                                        ,\"title_\"\r\n                                        ,\"is_active\"\r\n                                        ,\"workflow_type\"\r\n                                        ,\"ownr\"\r\n                                        ,\"allow_notes\"\r\n                                        ,\"custom_xamlx_url\"\r\n                                        ,\"can_inherit_permissions\"\r\n                                        ,\"inherits_permissions\"\r\n                                        ,\"application_name\"\r\n                                        ,\"allw_pblshers_to_skip_workflow\"\r\n                                        ,\"allw_dmnstrtrs_t_skip_workflow\"\r\n                                        ,\"voa_version\"\r\n                                        ,\"date_created\"\r\n                                        ,\"last_modified\")\r\n                                SELECT  \"id\"\r\n                                        ,\"title_\"\r\n                                        ,\"is_active\"\r\n                                        ,\"workflow_type\"\r\n                                        ,\"ownr\"\r\n                                        ,'0'\r\n                                        ,\"custom_xamlx_url\"\r\n                                        ,\"can_inherit_permissions\"\r\n                                        ,\"inherits_permissions\"\r\n                                        ,\"application_name\"\r\n                                        ,\"allw_pblshers_to_skip_workflow\"\r\n                                        ,\"allw_dmnstrtrs_t_skip_workflow\"\r\n                                        ,\"voa_version\"\r\n                                        ,\"date_created\"\r\n                                        ,\"last_modified\"\r\n                                FROM \"sf_workflow_definition\"";
      else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        commandText = "INSERT INTO sf_wfl_definition\n                                        (id\n                                        , title_\n                                        , is_active\n                                        , workflow_type\n                                        , ownr\n                                        , allow_notes\n                                        , custom_xamlx_url\n                                        , can_inherit_permissions\n                                        , inherits_permissions\n                                        , application_name\n                                        , allow_publishers_to_skip_workflow\n                                        , allow_administrators_to_skip_workflow\n                                        , voa_version\n                                        , date_created\n                                        , last_modified)\n                                SELECT  id\n                                        ,title_\n                                        ,is_active\n                                        ,workflow_type\n                                        ,ownr\n                                        ,0\n                                        ,custom_xamlx_url\n                                        ,can_inherit_permissions\n                                        ,inherits_permissions\n                                        ,application_name\n                                        ,allow_publishers_to_skip_workflow\n                                        ,allow_administrators_to_skip_workflow\n                                        ,voa_version\n                                        ,date_created\n                                        ,last_modified\n                                FROM sf_workflow_definition";
      if (this.upgradedFromSchemaVersionNumber < 5700)
      {
        commandText = "INSERT INTO sf_wfl_definition\r\n                                        (id\r\n                                        ,title_\r\n                                        ,is_active\r\n                                        ,workflow_type\r\n                                        ,ownr\r\n                                        ,allow_notes\r\n                                        ,custom_xamlx_url\r\n                                        ,can_inherit_permissions\r\n                                        ,inherits_permissions\r\n                                        ,application_name\r\n                                        ,allw_pblshers_to_skip_workflow\r\n                                        ,allw_dmnstrtrs_t_skip_workflow\r\n                                        ,voa_version\r\n                                        ,date_created\r\n                                        ,last_modified)\r\n                                SELECT  id\r\n                                        ,title_\r\n                                        ,is_active\r\n                                        ,workflow_type\r\n                                        ,ownr\r\n                                        ,0\r\n                                        ,''\r\n                                        ,can_inherit_permissions\r\n                                        ,inherits_permissions\r\n                                        ,application_name\r\n                                        ,0\r\n                                        ,allw_dmnstrtrs_t_skip_workflow\r\n                                        ,voa_version\r\n                                        ,date_created\r\n                                        ,last_modified\r\n                                FROM sf_workflow_definition";
        if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
          commandText = "INSERT INTO \"sf_wfl_definition\"\r\n                                        (\"id\"\r\n                                        ,\"title_\"\r\n                                        ,\"is_active\"\r\n                                        ,\"workflow_type\"\r\n                                        ,\"ownr\"\r\n                                        ,\"allow_notes\"\r\n                                        ,\"custom_xamlx_url\"\r\n                                        ,\"can_inherit_permissions\"\r\n                                        ,\"inherits_permissions\"\r\n                                        ,\"application_name\"\r\n                                        ,\"allw_pblshers_to_skip_workflow\"\r\n                                        ,\"allw_dmnstrtrs_t_skip_workflow\"\r\n                                        ,\"voa_version\"\r\n                                        ,\"date_created\"\r\n                                        ,\"last_modified\")\r\n                                SELECT  \"id\"\r\n                                        ,\"title_\"\r\n                                        ,\"is_active\"\r\n                                        ,\"workflow_type\"\r\n                                        ,\"ownr\"\r\n                                        ,'0'\r\n                                        ,''\r\n                                        ,\"can_inherit_permissions\"\r\n                                        ,\"inherits_permissions\"\r\n                                        ,\"application_name\"\r\n                                        ,'0'\r\n                                        ,\"allw_dmnstrtrs_t_skip_workflow\"\r\n                                        ,\"voa_version\"\r\n                                        ,\"date_created\"\r\n                                        ,\"last_modified\"\r\n                                FROM \"sf_workflow_definition\"";
        else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
          commandText = "INSERT INTO sf_wfl_definition\n                                        (id\n                                        , title_\n                                        , is_active\n                                        , workflow_type\n                                        , ownr\n                                        , allow_notes\n                                        , custom_xamlx_url\n                                        , can_inherit_permissions\n                                        , inherits_permissions\n                                        , application_name\n                                        , allow_publishers_to_skip_workflow\n                                        , allow_administrators_to_skip_workflow\n                                        , voa_version\n                                        , date_created\n                                        , last_modified)\n                                SELECT  id\n                                        ,title_\n                                        ,is_active\n                                        ,workflow_type\n                                        ,ownr\n                                        ,0\n                                        ,''\n                                        ,can_inherit_permissions\n                                        ,inherits_permissions\n                                        ,application_name\n                                        ,0\n                                        ,allow_administrators_to_skip_workflow\n                                        ,voa_version\n                                        ,date_created\n                                        ,last_modified\n                                FROM sf_workflow_definition";
      }
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeWorkflowDefinitionSitefinityPermissions(ref string upgradeDescription)
    {
      upgradeDescription = "Upgrade workflow definition sitefinity permissions.";
      string commandText = "INSERT INTO sf_wfl_dfnition_sf_permissions\r\n                                            (id, id2)\r\n                                        SELECT id, id2                                            \r\n                                        FROM sf_wrkflw_dfntn_sf_permissions";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_dfnition_sf_permissions\"\r\n                                            (\"id\", \"id2\")\r\n                                        SELECT \"id\", \"id2\"\r\n                                        FROM \"sf_wrkflw_dfntn_sf_permissions\"";
      else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        commandText = "INSERT INTO sf_wfl_definition_sf_permissions\r\n                                            (id, id2)\r\n                                        SELECT id, id2                                            \r\n                                        FROM sf_workflow_definition_sf_permissions";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeWorkflowDefinitionPermissionSetObjectTitle(ref string upgradeDescription)
    {
      upgradeDescription = "Upgrade workflow definition permission set object title.";
      string commandText = "INSERT INTO sf_wfl_dfntn_prmssnst_bjct_ttl\r\n                                            (id, mapkey, val)\r\n                                        SELECT id, mapkey, val                                            \r\n                                        FROM sf_wrkflw_dfntn_prmssnst_bjct_";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_dfntn_prmssnst_bjct_ttl\"\r\n                                            (\"id\", \"mapkey\", \"val\")\r\n                                        SELECT \"id\", \"mapkey\", \"val\"\r\n                                        FROM \"sf_wrkflw_dfntn_prmssnst_bjct_\"";
      else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        commandText = "INSERT INTO sf_wfl_definition_permissionset_object_title_res_keys\r\n                                                                        (id, mapkey, val)\r\n                                                                    SELECT id, mapkey, val                                            \r\n                                                                    FROM sf_workflow_definition_permissionset_object_title_res_keys";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeWorkflowDefinitionSupportedPermissionSets(ref string upgradeDescription)
    {
      upgradeDescription = "Upgrade workflow definition supported permission sets.";
      string commandText = "INSERT INTO sf_wfl_dfntn_spprtd_prmssn_sts\r\n                                            (id, seq, val)\r\n                                        SELECT id, seq, val                                            \r\n                                        FROM sf_wrkflw_dfntn_spprtd_prmssn_";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_dfntn_spprtd_prmssn_sts\"\r\n                                            (\"id\", \"seq\", \"val\")\r\n                                        SELECT \"id\", \"seq\", \"val\"\r\n                                        FROM \"sf_wrkflw_dfntn_spprtd_prmssn_\"";
      else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        commandText = "INSERT INTO sf_wfl_definition_supported_permission_sets\r\n                                                                        (id, seq, val)\r\n                                                                    SELECT id, seq, val                                            \r\n                                                                    FROM sf_workflow_definition_supported_permission_sets";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "For better view of the SQL query")]
    private void UpgradeFirstLevelsOfDefinitions(ref string upgradeDescription)
    {
      upgradeDescription = "Create first level of workflow definitions.";
      string commandText = "INSERT INTO sf_wfl_level\r\n                                            (notify_approvers\r\n                                            ,ordinal\r\n                                            ,notify_administrators\r\n                                            ,last_modified\r\n                                            ,id\r\n                                            ,workflow_definition_id\r\n                                            ,app_name\r\n                                            ,action_name\r\n                                            ,voa_version)\r\n                                        SELECT snd_frst_lvl_mail_notification\r\n                                            ,1\r\n                                            ,snd_frst_lvl_mail_notification\r\n                                            ,last_modified\r\n                                            ,NEWID()\r\n                                            ,id\r\n                                            ,'/Workflow'\r\n                                            ,'Approve'\r\n                                            ,1\r\n                                        FROM sf_workflow_definition\r\n                                        WHERE workflow_type = '1' OR workflow_type = '2'";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_level\"\r\n                                            (\"notify_approvers\"\r\n                                            ,\"ordinal\"\r\n                                            ,\"notify_administrators\"\r\n                                            ,\"last_modified\"\r\n                                            ,\"id\"\r\n                                            ,\"workflow_definition_id\"\r\n                                            ,\"app_name\"\r\n                                            ,\"action_name\"\r\n                                            ,\"voa_version\")\r\n                                        SELECT \"snd_frst_lvl_mail_notification\"\r\n                                            ,'1'\r\n                                            ,\"snd_frst_lvl_mail_notification\"\r\n                                            ,\"last_modified\"\r\n                                            ,LOWER(REGEXP_REPLACE(rawtohex(sys_guid()),\r\n                                             '([A-F0-9]{8})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{12})',\r\n                                             '\\1-\\2-\\3-\\4-\\5')) \"REGEXP_REPLACE\"\r\n                                            ,\"id\"\r\n                                            ,'/Workflow'\r\n                                            ,'Approve'\r\n                                            ,'1'\r\n                                        FROM \"sf_workflow_definition\"\r\n                                        WHERE \"workflow_type\" = '1' OR \"workflow_type\" = '2'";
      else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        commandText = "INSERT INTO sf_wfl_level\n                                            (notify_approvers\n                                            , ordinal\n                                            , notify_administrators\n                                            , last_modified\n                                            , id\n                                            , workflow_definition_id\n                                            , app_name\n                                            , action_name\n                                            , voa_version)\n                                        SELECT send_first_level_email_notification\n                                            , 1\n                                            , send_first_level_email_notification\n                                            , last_modified\n                                            , uuid()\n                                            , id\n                                            , '/Workflow'\n                                            , 'Approve'\n                                            , 1\n                                        FROM sf_workflow_definition\n                                        WHERE workflow_type = '1' OR workflow_type = '2'";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "For better view of the SQL query")]
    private void UpgradeSecondLevelsOfDefinitions(ref string upgradeDescription)
    {
      upgradeDescription = "Create second level of workflow definitions.";
      string commandText = "INSERT INTO sf_wfl_level\r\n                                            (notify_approvers\r\n                                            ,ordinal\r\n                                            ,notify_administrators\r\n                                            ,last_modified\r\n                                            ,id\r\n                                            ,workflow_definition_id\r\n                                            ,app_name\r\n                                            ,action_name\r\n                                            ,voa_version)\r\n                                        SELECT snd_scnd_lvl_mail_notification\r\n                                            ,2\r\n                                            ,snd_scnd_lvl_mail_notification\r\n                                            ,last_modified\r\n                                            ,NEWID()\r\n                                            ,id\r\n                                            ,'/Workflow'\r\n                                            ,'Publish'\r\n                                            ,1\r\n                                        FROM sf_workflow_definition\r\n                                        WHERE workflow_type = '2'";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_level\"\r\n                                            (\"notify_approvers\"\r\n                                            ,\"ordinal\"\r\n                                            ,\"notify_administrators\"\r\n                                            ,\"last_modified\"\r\n                                            ,\"id\"\r\n                                            ,\"workflow_definition_id\"\r\n                                            ,\"app_name\"\r\n                                            ,\"action_name\"\r\n                                            ,\"voa_version\")\r\n                                        SELECT \"snd_scnd_lvl_mail_notification\"\r\n                                            ,'2'\r\n                                            ,\"snd_scnd_lvl_mail_notification\"\r\n                                            ,\"last_modified\"\r\n                                            ,LOWER(REGEXP_REPLACE(rawtohex(sys_guid()),\r\n                                             '([A-F0-9]{8})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{12})',\r\n                                             '\\1-\\2-\\3-\\4-\\5')) \"REGEXP_REPLACE\"\r\n                                            ,\"id\"\r\n                                            ,'/Workflow'\r\n                                            ,'Publish'\r\n                                            ,'1'\r\n                                        FROM \"sf_workflow_definition\"\r\n                                        WHERE \"workflow_type\" = '2'";
      else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        commandText = "INSERT INTO sf_wfl_level\n                                            (notify_approvers\n                                            , ordinal\n                                            , notify_administrators\n                                            , last_modified\n                                            , id\n                                            , workflow_definition_id\n                                            , app_name\n                                            , action_name\n                                            , voa_version)\n                                        SELECT send_second_level_email_notification\n                                            , 2\n                                            , send_second_level_email_notification\n                                            , last_modified\n                                            , uuid()\n                                            , id\n                                            , '/Workflow'\n                                            , 'Publish'\n                                            , 1\n                                        FROM sf_workflow_definition\n                                        WHERE workflow_type = '2'";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeWorkflowPermissionsForFirstLevels(ref string upgradeDescription)
    {
      upgradeDescription = "Create workflow permissions for first level of workflow definitions.";
      string commandText = "INSERT INTO sf_wfl_permission\r\n                                            (principal_type\r\n                                            ,principal_name\r\n                                            ,principal_id\r\n                                            ,last_modified\r\n                                            ,id\r\n                                            ,app_name\r\n                                            ,action_name\r\n                                            ,voa_version\r\n                                            ,workflow_level_id)\r\n                                        SELECT p.principal_type\r\n                                            ,p.principal_name\r\n                                            ,p.principal_id\r\n                                            ,p.last_modified\r\n                                            ,p.id\r\n                                            ,p.app_name\r\n                                            ,p.action_name\r\n                                            ,p.voa_version\r\n                                            ,l.id\r\n                                        FROM sf_workflow_permission p\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN sf_workflow_definition d\r\n\t\t\t\t\t\t\t\t\t\ton p.id2 = d.id AND p.action_name = 'Approve' AND (d.workflow_type = '1' OR d.workflow_type = '2')\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN sf_wfl_level l\r\n\t\t\t\t\t\t\t\t\t\ton l.workflow_definition_id = d.id AND l.ordinal = 1 AND l.action_name = 'Approve'";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_permission\"\r\n                                            (\"principal_type\"\r\n                                            ,\"principal_name\"\r\n                                            ,\"principal_id\"\r\n                                            ,\"last_modified\"\r\n                                            ,\"id\"\r\n                                            ,\"app_name\"\r\n                                            ,\"action_name\"\r\n                                            ,\"voa_version\"\r\n                                            ,\"workflow_level_id\")\r\n                                        SELECT \"p\".\"principal_type\"\r\n                                            ,\"p\".\"principal_name\"\r\n                                            ,\"p\".\"principal_id\"\r\n                                            ,\"p\".\"last_modified\"\r\n                                            ,\"p\".\"id\"\r\n                                            ,\"p\".\"app_name\"\r\n                                            ,\"p\".\"action_name\"\r\n                                            ,\"p\".\"voa_version\"\r\n                                            ,\"l\".\"id\"\r\n                                        FROM \"sf_workflow_permission\" \"p\"\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN \"sf_workflow_definition\" \"d\"\r\n\t\t\t\t\t\t\t\t\t\ton \"p\".\"id2\" = \"d\".\"id\" AND \"p\".\"action_name\" = 'Approve' AND (\"d\".\"workflow_type\" = '1' OR \"d\".\"workflow_type\" = '2')\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN \"sf_wfl_level\" \"l\"\r\n\t\t\t\t\t\t\t\t\t\ton \"l\".\"workflow_definition_id\" = \"d\".\"id\" AND \"l\".\"ordinal\" = '1' AND \"l\".\"action_name\" = 'Approve'";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeWorkflowPermissionsForSecondLevels(ref string upgradeDescription)
    {
      upgradeDescription = "Create workflow permissions for second level of workflow definitions.";
      string commandText = "INSERT INTO sf_wfl_permission\r\n                                            (principal_type\r\n                                            ,principal_name\r\n                                            ,principal_id\r\n                                            ,last_modified\r\n                                            ,id\r\n                                            ,app_name\r\n                                            ,action_name\r\n                                            ,voa_version\r\n                                            ,workflow_level_id)\r\n                                        SELECT p.principal_type\r\n                                            , p.principal_name\r\n                                            , p.principal_id\r\n                                            , p.last_modified\r\n                                            , p.id\r\n                                            , p.app_name\r\n                                            , p.action_name\r\n                                            , p.voa_version\r\n                                            , l.id\r\n                                        FROM sf_workflow_permission p\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN sf_workflow_definition d\r\n\t\t\t\t\t\t\t\t\t\ton p.id2 = d.id AND d.workflow_type = '2' AND p.action_name = 'Publish'\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN sf_wfl_level l\r\n\t\t\t\t\t\t\t\t\t\ton l.workflow_definition_id = d.id AND l.ordinal = 2 AND l.action_name = 'Publish'";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_permission\"\r\n                                            (\"principal_type\"\r\n                                            ,\"principal_name\"\r\n                                            ,\"principal_id\"\r\n                                            ,\"last_modified\"\r\n                                            ,\"id\"\r\n                                            ,\"app_name\"\r\n                                            ,\"action_name\"\r\n                                            ,\"voa_version\"\r\n                                            ,\"workflow_level_id\")\r\n                                        SELECT \"p\".\"principal_type\"\r\n                                            ,\"p\".\"principal_name\"\r\n                                            ,\"p\".\"principal_id\"\r\n                                            ,\"p\".\"last_modified\"\r\n                                            ,\"p\".\"id\"\r\n                                            ,\"p\".\"app_name\"\r\n                                            ,\"p\".\"action_name\"\r\n                                            ,\"p\".\"voa_version\"\r\n                                            ,\"l\".\"id\"\r\n                                        FROM \"sf_workflow_permission\" \"p\"\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN \"sf_workflow_definition\" \"d\"\r\n\t\t\t\t\t\t\t\t\t\ton \"p\".\"id2\" = \"d\".\"id\" AND \"d\".\"workflow_type\" = '2' AND \"p\".\"action_name\" = 'Publish'\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN \"sf_wfl_level\" \"l\"\r\n\t\t\t\t\t\t\t\t\t\ton \"l\".\"workflow_definition_id\" = \"d\".\"id\" AND \"l\".\"ordinal\" = '2' AND \"l\".\"action_name\" = 'Publish'";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeCustomMailRecipientsForFirstLevels(ref string upgradeDescription)
    {
      if (this.upgradedFromSchemaVersionNumber <= 5700)
        return;
      upgradeDescription = "Create custom mail recipients for first level of workflow definitions.";
      string commandText = "INSERT INTO sf_wfl_lvl_cstm_mil_recipients(id, seq, val)\r\n                                SELECT l.id, flr.seq, flr.val\r\n                                  FROM sf_wfl_level l\r\n                                  INNER JOIN sf_workflow_definition d \r\n                                  on l.workflow_definition_id = d.id AND l.ordinal = 1 \r\n                                  INNER JOIN sf_wrkflw_dfntn_ddtnl_frst_lvl flr\r\n                                  on d.id = flr.id";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_lvl_cstm_mil_recipients\"(\"id\", \"seq\", \"val\")\r\n                                SELECT \"l\".\"id\", \"flr\".\"seq\", \"flr\".\"val\"\r\n                                  FROM \"sf_wfl_level\" \"l\"\r\n                                  INNER JOIN \"sf_workflow_definition\" \"d\"\r\n                                  on \"l\".\"workflow_definition_id\" = \"d\".\"id\" AND \"l\".\"ordinal\" = '1'\r\n                                  INNER JOIN \"sf_wrkflw_dfntn_ddtnl_frst_lvl\" \"flr\"\r\n                                  on \"d\".\"id\" = \"flr\".\"id\"";
      else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        commandText = "INSERT INTO sf_wfl_level_custom_email_recipients(id, seq, val)\n                                SELECT l.id, flr.seq, flr.val\n                                  FROM sf_wfl_level l\n                                  INNER JOIN sf_workflow_definition d\n                                  on l.workflow_definition_id = d.id AND l.ordinal = 1\n                                  INNER JOIN sf_workflow_definition_additional_first_level_recipients flr\n                                  on d.id = flr.id";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeCustomMailRecipientsForSecondLevels(ref string upgradeDescription)
    {
      if (this.upgradedFromSchemaVersionNumber <= 5700)
        return;
      upgradeDescription = "Create custom mail recipients for second level of workflow definitions.";
      string commandText = "INSERT INTO sf_wfl_lvl_cstm_mil_recipients(id, seq, val)\r\n                                SELECT l.id, slr.seq, slr.val\r\n                                  FROM sf_wfl_level l\r\n                                  INNER JOIN sf_workflow_definition d\r\n                                  on l.workflow_definition_id = d.id AND l.ordinal = 2\r\n                                  INNER JOIN sf_wrkflw_dfntn_ddtnl_scnd_lvl slr\r\n                                  on d.id = slr.id";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_lvl_cstm_mil_recipients\"(\"id\", \"seq\", \"val\")\r\n                                SELECT \"l\".\"id\", \"slr\".\"seq\", \"slr\".\"val\"\r\n                                  FROM \"sf_wfl_level\" \"l\"\r\n                                  INNER JOIN \"sf_workflow_definition\" \"d\"\r\n                                  on \"l\".\"workflow_definition_id\" = \"d\".\"id\" AND \"l\".\"ordinal\" = '2'\r\n                                  INNER JOIN \"sf_wrkflw_dfntn_ddtnl_scnd_lvl\" \"slr\"\r\n                                  on \"d\".\"id\" = \"slr\".\"id\"";
      else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        commandText = "INSERT INTO sf_wfl_level_custom_email_recipients(id, seq, val)\n                                SELECT l.id, slr.seq, slr.val\n                                  FROM sf_wfl_level l\n                                  INNER JOIN sf_workflow_definition d\n                                  on l.workflow_definition_id = d.id AND l.ordinal = 2\n                                  INNER JOIN sf_workflow_definition_additional_second_level_recipients slr\n                                  on d.id = slr.id";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeWorkflowScopes(ref string upgradeDescription)
    {
      upgradeDescription = "Create workflow scopes for workflow definitions.";
      string commandText = "INSERT INTO sf_wfl_scope (id, workflow_definition_id, lnguage, app_name, voa_version, last_modified)\r\n                                    SELECT NEWID(), d.id, d.culture_scope, d.application_name, d.voa_version, d.last_modified\r\n                                    FROM sf_workflow_definition d";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_scope\" (\"id\", \"workflow_definition_id\", \"lnguage\", \"app_name\", \"voa_version\", \"last_modified\")\r\n                                    SELECT LOWER(REGEXP_REPLACE(rawtohex(sys_guid()),\r\n                                             '([A-F0-9]{8})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{12})',\r\n                                             '\\1-\\2-\\3-\\4-\\5')) \"REGEXP_REPLACE\", \"d\".\"id\", \"d\".\"culture_scope\", \"d\".\"application_name\", \"d\".\"voa_version\", \"d\".\"last_modified\"\r\n                                    FROM \"sf_workflow_definition\" \"d\"";
      else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        commandText = "INSERT INTO sf_wfl_scope (id, workflow_definition_id, lnguage, app_name, voa_version, last_modified)\r\n                                    SELECT uuid(), d.id, d.culture_scope, d.application_name, d.voa_version, d.last_modified\r\n                                    FROM sf_workflow_definition d";
      if (this.upgradedFromSchemaVersionNumber < 5700)
      {
        commandText = "INSERT INTO sf_wfl_scope (id, workflow_definition_id, lnguage, app_name, voa_version, last_modified)\r\n                                    SELECT NEWID(), d.id, '', d.application_name, d.voa_version, d.last_modified\r\n                                    FROM sf_workflow_definition d";
        if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
          commandText = "INSERT INTO \"sf_wfl_scope\" (\"id\", \"workflow_definition_id\", \"lnguage\", \"app_name\", \"voa_version\", \"last_modified\")\r\n                                    SELECT LOWER(REGEXP_REPLACE(rawtohex(sys_guid()),\r\n                                             '([A-F0-9]{8})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{12})',\r\n                                             '\\1-\\2-\\3-\\4-\\5')) \"REGEXP_REPLACE\", \"d\".\"id\", '', \"d\".\"application_name\", \"d\".\"voa_version\", \"d\".\"last_modified\"\r\n                                    FROM \"sf_workflow_definition\" \"d\"";
        else if (this.context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
          commandText = "INSERT INTO sf_wfl_scope (id, workflow_definition_id, lnguage, app_name, voa_version, last_modified)\r\n                                    SELECT uuid(), d.id, '', d.application_name, d.voa_version, d.last_modified\r\n                                    FROM sf_workflow_definition d";
      }
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeSiteItemLinksForScopes(ref string upgradeDescription)
    {
      upgradeDescription = "Create site item links for workflow scopes.";
      string commandText = "INSERT INTO sf_site_item_links\r\n                                       (site_id\r\n                                       ,item_type\r\n                                       ,item_id\r\n                                       ,application_name\r\n                                       ,voa_version)\r\n                                 SELECT sl.site_id\r\n\t\t\t                             ,'Telerik.Sitefinity.Workflow.Model.WorkflowScope'\r\n\t\t\t                             ,s.id\r\n\t\t\t                             ,sl.application_name\r\n\t\t\t                             ,sl.voa_version\r\n                                 FROM sf_site_item_links sl\r\n                                 INNER JOIN sf_wfl_definition d\r\n                                 on sl.item_id = d.id AND sl.item_type = 'Telerik.Sitefinity.Workflow.Model.WorkflowDefinition'\r\n                                 INNER JOIN sf_wfl_scope s\r\n                                 on s.workflow_definition_id = d.id";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_site_item_links\"\r\n                                       (\"site_id\"\r\n                                       ,\"item_type\"\r\n                                       ,\"item_id\"\r\n                                       ,\"application_name\"\r\n                                       ,\"voa_version\")\r\n                                 SELECT \"sl\".\"site_id\"\r\n\t\t\t                             ,'Telerik.Sitefinity.Workflow.Model.WorkflowScope'\r\n\t\t\t                             ,\"s\".\"id\"\r\n\t\t\t                             ,\"sl\".\"application_name\"\r\n\t\t\t                             ,\"sl\".\"voa_version\"\r\n                                 FROM \"sf_site_item_links\" \"sl\"\r\n                                 INNER JOIN \"sf_wfl_definition\" \"d\"\r\n                                 on \"sl\".\"item_id\" = \"d\".\"id\" AND \"sl\".\"item_type\" = 'Telerik.Sitefinity.Workflow.Model.WorkflowDefinition'\r\n                                 INNER JOIN \"sf_wfl_scope\" \"s\"\r\n                                 on \"s\".\"workflow_definition_id\" = \"d\".\"id\"";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeWorkflowTypeScopes(ref string upgradeDescription)
    {
      upgradeDescription = "Create workflow type scopes for workflow scopes.";
      string commandText = "INSERT INTO sf_wfl_type_scope (id, scope_id, content_type, content_filter, include_children, app_name, voa_version, last_modified)\r\n                                SELECT s.id, ns.id, s.content_type, '', 0, s.app_name, s.voa_version, s.last_modified\r\n                                FROM sf_workflow_scope s\r\n                                INNER JOIN sf_workflow_definition d\r\n                                on s.id2 = d.id\r\n                                INNER JOIN sf_wfl_scope ns\r\n                                on ns.workflow_definition_id = d.id";
      if (this.context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        commandText = "INSERT INTO \"sf_wfl_type_scope\" (\"id\", \"scope_id\", \"content_type\", \"content_filter\", \"include_children\", \"app_name\", \"voa_version\", \"last_modified\")\r\n                                SELECT \"s\".\"id\", \"ns\".\"id\", \"s\".\"content_type\", '', '0', \"s\".\"app_name\", \"s\".\"voa_version\", \"s\".\"last_modified\"\r\n                                FROM \"sf_workflow_scope\" \"s\"\r\n                                INNER JOIN \"sf_workflow_definition\" \"d\"\r\n                                on \"s\".\"id2\" = \"d\".\"id\"\r\n                                INNER JOIN \"sf_wfl_scope\" \"ns\"\r\n                                on \"ns\".\"workflow_definition_id\" = \"d\".\"id\"";
      this.context.ExecuteNonQuery(commandText);
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void UpgradeWorkflowScopesPerSiteAndLanguage(ref string upgradeDescription)
    {
      upgradeDescription = "Create workflow scopes per site and language.";
      foreach (WorkflowScope workflowScope in (IEnumerable<WorkflowScope>) this.context.GetAll<WorkflowScope>())
      {
        WorkflowScope scope = workflowScope;
        string[] source1 = new string[0];
        if (!scope.Language.IsNullOrEmpty())
          source1 = scope.Language.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
        IQueryable<SiteItemLink> source2 = this.context.GetAll<SiteItemLink>().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (i => i.ItemId == scope.Id && i.ItemType == typeof (WorkflowScope).FullName));
        if (source2.Count<SiteItemLink>() > 1)
        {
          foreach (SiteItemLink siteItemLink in (IEnumerable<SiteItemLink>) source2)
          {
            if (((IEnumerable<string>) source1).Count<string>() > 0)
            {
              foreach (string language in source1)
                this.CreateScope(scope, siteItemLink, language);
            }
            else
              this.CreateScope(scope, siteItemLink);
            this.context.Delete((object) siteItemLink);
          }
          this.context.Delete((object) scope);
        }
        else if (((IEnumerable<string>) source1).Count<string>() > 1)
        {
          foreach (string language in source1)
            this.CreateScope(scope, language: language);
          this.context.Delete((object) scope);
        }
      }
      Log.Write((object) string.Format("DONE : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
    }

    private void CreateScope(WorkflowScope scope, SiteItemLink siteLink = null, string language = "")
    {
      WorkflowScope entity1 = new WorkflowScope()
      {
        Id = this.GetNewGuid(),
        ApplicationName = scope.ApplicationName,
        Definition = scope.Definition,
        Language = language
      };
      foreach (WorkflowTypeScope typeScope in (IEnumerable<WorkflowTypeScope>) scope.TypeScopes)
      {
        WorkflowTypeScope entity2 = new WorkflowTypeScope()
        {
          Id = this.GetNewGuid(),
          ContentType = typeScope.ContentType,
          ContentFilter = typeScope.ContentFilter
        };
        entity1.TypeScopes.Add(entity2);
        this.context.Add((object) entity2);
      }
      this.context.Add((object) entity1);
      if (siteLink == null)
        return;
      this.context.Add((object) new SiteItemLink()
      {
        SiteId = siteLink.SiteId,
        ItemType = siteLink.ItemType,
        ApplicationName = siteLink.ApplicationName,
        ItemId = entity1.Id
      });
    }

    private Guid GetNewGuid() => Guid.NewGuid();
  }
}
