// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Upgrades.To5100.PageMigrator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Data.Common;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.SqlGenerators;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Upgrades.To5100
{
  internal class PageMigrator
  {
    public PageMigrator() => this.TransactionName = "PagesRefactoring" + Guid.NewGuid().ToString();

    protected string TransactionName { get; set; }

    public bool UpgradePages()
    {
      bool result = false;
      SystemManager.RunWithElevatedPrivilege((SystemManager.RunWithElevatedPrivilegeDelegate) (args => result = this.UpdatePageNodes()));
      return result;
    }

    private bool UpdatePageNodes()
    {
      bool trackingDisabled = SystemManager.DataTrackingDisabled;
      SystemManager.DataTrackingDisabled = true;
      try
      {
        PageManager manager = PageManager.GetManager((string) null, this.TransactionName);
        using (new ElevatedModeRegion((IManager) manager))
        {
          if (manager.Provider is IOpenAccessDataProvider provider)
          {
            this.UpdateLinkedPagesSetDefaultProvider(provider, provider.Name);
            SqlGenerator sqlGenerator = SqlGenerator.Get(provider.GetContext().OpenAccessConnection.DbType);
            Table table1 = new Table("sf_page_data");
            Table table2 = new Table("sf_page_language_link");
            Column column1 = new Column("content_id", table1);
            Column column2 = new Column("link_id", table2);
            Column column3 = new Column("page_languagelink_id", table1);
            Column column4 = new Column("date_created", table1);
            List<Column> columnList = new List<Column>()
            {
              column1,
              column2
            };
            JoinTableClause joinTableClause = new JoinTableClause(table2, new Dictionary<Column, Column>()
            {
              {
                column3,
                column2
              }
            });
            List<JoinTableClause> joinTableClauseList = new List<JoinTableClause>();
            joinTableClauseList.Add(joinTableClause);
            OrderByClause orderByClause = new OrderByClause(column4);
            Table fromTable = table1;
            List<Column> columnNames = columnList;
            List<JoinTableClause> joinClauses = joinTableClauseList;
            OrderByClause orderBy = orderByClause;
            string select = sqlGenerator.GetSelect(fromTable, columnNames, joinClauses, orderBy);
            Dictionary<Guid, List<Guid>> linkedPages = new Dictionary<Guid, List<Guid>>();
            SitefinityOAContext context = provider.GetContext();
            using (OACommand command = context.Connection.CreateCommand())
            {
              command.CommandText = select;
              using (OADataReader reader = command.ExecuteReader())
              {
                while (reader.Read())
                {
                  Guid guid1 = this.GetGuid(context.OpenAccessConnection.DbType, reader, 0);
                  if (!reader.IsDBNull(1))
                  {
                    Guid guid2 = this.GetGuid(context.OpenAccessConnection.DbType, reader, 1);
                    if (!linkedPages.Keys.Contains<Guid>(guid2))
                      linkedPages.Add(guid2, new List<Guid>()
                      {
                        guid1
                      });
                    else
                      linkedPages[guid2].Add(guid1);
                  }
                }
                reader.Close();
              }
            }
            bool dropLinksTable = false;
            manager.Provider.WithSuppressedValidationOnCommit((System.Action) (() => dropLinksTable = this.UpdateLinkedPages(manager, this.TransactionName, linkedPages)));
            if (dropLinksTable)
            {
              SqlGenerator gen = SqlGenerator.Get(context.OpenAccessConnection.DbType);
              try
              {
                context.ExecuteNonQuery(gen.GetDropIndex("sf_page_data", "idx_sf_pg_dt_pg_lnguagelink_id"));
                context.ExecuteNonQuery(gen.GetDropColumn("sf_page_data", "page_languagelink_id"));
                context.SaveChanges();
              }
              catch
              {
                context.ClearChanges();
              }
              PageMigrator.DropPageLanguageLinkTable((OpenAccessContext) context, gen);
            }
          }
        }
      }
      finally
      {
        SystemManager.DataTrackingDisabled = trackingDisabled;
        TransactionManager.DisposeTransaction(this.TransactionName);
      }
      return false;
    }

    private Guid GetGuid(DatabaseType dbType, OADataReader reader, int columnIndex) => dbType == DatabaseType.Oracle ? Guid.Parse(reader.GetString(columnIndex)) : reader.GetGuid(columnIndex);

    private void UpdateLinkedPagesSetDefaultProvider(
      IOpenAccessDataProvider oaPageProvider,
      string defaultProviderName)
    {
      oaPageProvider.GetContext().GetAll<PageNode>().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => (Guid?) pn.LinkedNodeId != new Guid?() && pn.LinkedNodeProvider == default (string))).UpdateAll<PageNode>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<PageNode>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<PageNode>>>) (ud => ud.Set<string>((Expression<Func<PageNode, string>>) (pn => pn.LinkedNodeProvider), (Expression<Func<PageNode, string>>) (pn => defaultProviderName))));
    }

    private bool UpdateLinkedPages(
      PageManager manager,
      string transactionName,
      Dictionary<Guid, List<Guid>> linkedPages)
    {
      Dictionary<Guid, List<PageMigrator.PageNodeMigrationInfo>> mergedNodeLanguageNodes = new Dictionary<Guid, List<PageMigrator.PageNodeMigrationInfo>>();
      bool crawlableDefault = true;
      bool requireSslDefault = false;
      if (linkedPages.Count<KeyValuePair<Guid, List<Guid>>>() > 0)
        PageMigrator.SetEnableSearchForReferencedNodes();
      bool flag1 = true;
      foreach (KeyValuePair<Guid, List<Guid>> linkedPage in linkedPages)
      {
        KeyValuePair<Guid, List<Guid>> links = linkedPage;
        bool flag2 = false;
        List<PageMigrator.PageNodeMigrationInfo> nodeMigrationInfoList = new List<PageMigrator.PageNodeMigrationInfo>();
        HashSet<string> stringSet = new HashSet<string>();
        PageMigrator.PageNodeMigrationInfo nodeMigrationInfo1 = (PageMigrator.PageNodeMigrationInfo) null;
        try
        {
          List<PageData> list = manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pd => links.Value.Contains(pd.Id))).OrderBy<PageData, DateTime>((Expression<Func<PageData, DateTime>>) (pd => pd.DateCreated)).ToList<PageData>();
          PageData pageData1 = list.Where<PageData>((Func<PageData, bool>) (pd => !pd.IsAutoCreated && pd.NavigationNode != null)).FirstOrDefault<PageData>();
          if (pageData1 == null)
          {
            if (list.Count > 0)
            {
              foreach (PageData pageData2 in list)
              {
                try
                {
                  if (pageData2.NavigationNode != null)
                    manager.Delete(pageData2.NavigationNode);
                  else
                    manager.Delete(pageData2);
                  manager.Provider.CommitTransaction();
                }
                catch
                {
                  manager.Provider.RollbackTransaction();
                }
              }
            }
          }
          else
          {
            PageNode navigationNode1 = pageData1.NavigationNode;
            this.EnsurePageDataCulture(pageData1, navigationNode1);
            bool crawlable = navigationNode1.Crawlable;
            bool requireSsl = navigationNode1.RequireSsl;
            nodeMigrationInfo1 = new PageMigrator.PageNodeMigrationInfo(navigationNode1, pageData1.Culture);
            List<PageNode> pageNodeList = new List<PageNode>();
            foreach (PageData pageData3 in list)
            {
              if (pageData3 != pageData1)
              {
                if (pageData3.NavigationNode == null)
                {
                  manager.Delete(pageData3);
                }
                else
                {
                  PageNode navigationNode2 = pageData3.NavigationNode;
                  this.EnsurePageDataCulture(pageData3, navigationNode2);
                  if (stringSet.Contains(pageData3.Culture))
                    pageData3.IsAutoCreated = true;
                  else
                    stringSet.Add(pageData3.Culture);
                  nodeMigrationInfo1 = (PageMigrator.PageNodeMigrationInfo) null;
                  flag2 = ((flag2 ? 1 : 0) | (crawlable != navigationNode2.Crawlable ? 1 : (requireSsl != navigationNode2.RequireSsl ? 1 : 0))) != 0;
                  PageMigrator.PageNodeMigrationInfo nodeMigrationInfo2 = new PageMigrator.PageNodeMigrationInfo(navigationNode2, pageData3.Culture);
                  if (nodeMigrationInfo2 != null)
                    nodeMigrationInfoList.Add(nodeMigrationInfo2);
                  if (pageData3.NavigationNode != navigationNode1)
                  {
                    PageMigrator.MoveChildNodes(navigationNode2, navigationNode1);
                    if (!pageData3.IsAutoCreated)
                    {
                      navigationNode2.PageDataList.Clear();
                      pageData3.NavigationNode = navigationNode1;
                      PageMigrator.MovePageNodeProperties(manager, navigationNode2, navigationNode1, CultureInfo.GetCultureInfo(pageData3.Culture), crawlableDefault, requireSslDefault);
                      manager.Provider.FlushTransaction();
                    }
                    pageNodeList.Add(navigationNode2);
                  }
                }
              }
            }
            foreach (PageNode pageNode1 in pageNodeList)
            {
              PageNode node = pageNode1;
              try
              {
                if (node.Nodes.Count > 0)
                  node.Nodes.Clear();
                IList<Permission> permissions = node.Permissions;
                HashSet<Guid> guidSet = new HashSet<Guid>();
                foreach (Permission permission in (IEnumerable<Permission>) permissions)
                {
                  foreach (ISecuredObject permissionsInheritor in manager.Provider.GetPermissionsInheritors((ISecuredObject) node, false, typeof (PageNode)))
                  {
                    if (permissionsInheritor.InheritsPermissions)
                    {
                      manager.BreakPermiossionsInheritance(permissionsInheritor);
                      guidSet.Add(permissionsInheritor.Id);
                    }
                    if (permissionsInheritor.Permissions.Contains(permission))
                      permissionsInheritor.Permissions.Remove(permission);
                  }
                }
                foreach (Permission permission1 in node.Permissions.Where<Permission>((Func<Permission, bool>) (x => x.ObjectId == node.Id)))
                {
                  Permission permission = permission1;
                  IQueryable<PageNode> pageNodes = manager.GetPageNodes();
                  Expression<Func<PageNode, bool>> predicate = (Expression<Func<PageNode, bool>>) (x => x.Permissions.Contains(permission));
                  foreach (PageNode pageNode2 in (IEnumerable<PageNode>) pageNodes.Where<PageNode>(predicate))
                  {
                    if (!(pageNode2.Id == node.Id) && !guidSet.Contains(pageNode2.Id) && pageNode2.Permissions.Contains(permission))
                      pageNode2.Permissions.Remove(permission);
                  }
                }
                foreach (ApprovalTrackingRecord approvalRecord in (IEnumerable<ApprovalTrackingRecord>) node.GetApprovalRecords())
                  approvalRecord.WorkflowItemId = navigationNode1.Id;
                manager.Delete(node);
              }
              catch (Exception ex)
              {
              }
            }
            this.DeletePageLanguageLink(manager, links.Key);
            if (flag2)
              mergedNodeLanguageNodes.Add(links.Key, nodeMigrationInfoList);
            TransactionManager.CommitTransaction(transactionName);
          }
        }
        catch (Exception ex)
        {
          flag1 = false;
          TransactionManager.RollbackTransaction(transactionName);
          Log.Write((object) string.Format("FAILED merging split pages: {0} - {1}", (object) string.Join<Guid>(",", (IEnumerable<Guid>) links.Value), (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      this.LogMergeConflicts(mergedNodeLanguageNodes, crawlableDefault, requireSslDefault);
      return flag1;
    }

    private void DeletePageLanguageLink(PageManager manager, Guid id)
    {
      SitefinityOAContext context = (manager.Provider as IOpenAccessDataProvider).GetContext();
      SqlGenerator sqlGenerator = SqlGenerator.Get(context.OpenAccessConnection.DbType);
      context.ExecuteNonQuery(string.Format("DELETE \r\nFROM {0} \r\nWHERE {1} = '{2}'", (object) sqlGenerator.GetTableName("sf_page_language_link"), (object) sqlGenerator.GetColumnName("link_id"), (object) id.ToString()));
    }

    private void EnsurePageDataCulture(PageData pageData, PageNode pageNode)
    {
      List<CultureInfo> list = ((IEnumerable<CultureInfo>) LocalizationHelper.GetAvailableLanguagesForObject((ILocalizable) pageNode)).Where<CultureInfo>((Func<CultureInfo, bool>) (c => !c.Equals((object) CultureInfo.InvariantCulture))).ToList<CultureInfo>();
      if (list.Count == 1)
        pageData.Culture = list[0].Name;
      if (pageData.Culture != null)
        return;
      pageData.Culture = SystemManager.CurrentContext.CurrentSite.DefaultCulture.Name;
    }

    private static void MovePageNodeProperties(
      PageManager manager,
      PageNode sourceNode,
      PageNode destinationNode,
      CultureInfo sourceCulture,
      bool crawlableDefault,
      bool requireSslDefault)
    {
      PageMigrator.MoveLinkedNodes(manager, sourceNode, destinationNode);
      destinationNode.Title.SetString(sourceCulture, sourceNode.Title.GetString(sourceCulture, false));
      destinationNode.Description.SetString(sourceCulture, sourceNode.Description.GetString(sourceCulture, false));
      destinationNode.RedirectUrl.SetString(sourceCulture, sourceNode.RedirectUrl.GetString(sourceCulture, false));
      destinationNode.Extension.SetString(sourceCulture, sourceNode.Extension.GetString(sourceCulture, false));
      destinationNode.ApprovalWorkflowState.SetString(sourceCulture, sourceNode.ApprovalWorkflowState.GetString(sourceCulture, false));
      destinationNode.UrlName.SetString(sourceCulture, sourceNode.UrlName.GetString(sourceCulture, false));
      if (destinationNode.Crawlable != sourceNode.Crawlable)
        destinationNode.Crawlable = !crawlableDefault;
      destinationNode.IncludeInSearchIndex = destinationNode.Crawlable;
      if (destinationNode.RequireSsl != sourceNode.RequireSsl)
        destinationNode.RequireSsl = !requireSslDefault;
      PageNodeReference pageNodeReference = new PageNodeReference(destinationNode.ApplicationName, sourceNode.Id);
      destinationNode.PageNodeReferences.Add(pageNodeReference);
    }

    private static void MoveLinkedNodes(
      PageManager manager,
      PageNode sourceNode,
      PageNode destinationNode)
    {
      ObjectFactory.Resolve<PageHelperImplementation>().LinkingPageNodesDo(sourceNode, manager, (System.Action<PageManager, PageNode>) ((pageManager, linkingNode) =>
      {
        linkingNode.LinkedNodeId = destinationNode.Id;
        linkingNode.LinkedNodeProvider = destinationNode.LinkedNodeProvider;
      }));
    }

    private static void MoveChildNodes(PageNode sourceNode, PageNode destinationNode)
    {
      foreach (PageNode pageNode in new List<PageNode>((IEnumerable<PageNode>) sourceNode.Nodes))
      {
        sourceNode.Nodes.Remove(pageNode);
        pageNode.Parent = destinationNode;
      }
    }

    private void DeleteOrphanedAutoCreatedNodes(
      OpenAccessContext context,
      SqlGenerator sqlGen,
      List<string> orphanedAutoCreatedNodes)
    {
      this.DeleteFromTable(context, orphanedAutoCreatedNodes, sqlGen.GetTableName("sf_page_node_attrbutes"), sqlGen.GetColumnName("id"));
      this.DeleteFromTable(context, orphanedAutoCreatedNodes, sqlGen.GetTableName("sf_page_node_sf_permissions"), sqlGen.GetColumnName("id"));
      this.DeleteFromTable(context, orphanedAutoCreatedNodes, sqlGen.GetTableName("sf_page_node_translation_siblings"), sqlGen.GetColumnName("id"));
      this.DeleteFromTable(context, orphanedAutoCreatedNodes, sqlGen.GetTableName("sf_page_node_translation_siblings"), sqlGen.GetColumnName("id2"));
      this.DeleteFromTable(context, orphanedAutoCreatedNodes, sqlGen.GetTableName("sf_pg_nd_prmssnst_bjct_ttl_rs_"), sqlGen.GetColumnName("id"));
      this.DeleteFromTable(context, orphanedAutoCreatedNodes, sqlGen.GetTableName("sf_page_node"), sqlGen.GetColumnName("id"));
    }

    /// <summary>
    /// Deletes the rows from the specified table that have the specified values for the given column.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="columnValues">The column values.</param>
    /// <param name="tableName">Name of the table.</param>
    /// <param name="columnName">Name of the column.</param>
    private void DeleteFromTable(
      OpenAccessContext context,
      List<string> columnValues,
      string tableName,
      string columnName)
    {
      string format = "DELETE FROM {0} WHERE {1} IN ({2})";
      StringBuilder stringBuilder = new StringBuilder();
      int num = 0;
      int count = columnValues.Count;
      foreach (string columnValue in columnValues)
      {
        stringBuilder.AppendFormat("'{0}'", (object) columnValue);
        if (++num % 10 == 0 || num == count)
        {
          try
          {
            context.ExecuteNonQuery(string.Format(format, (object) tableName, (object) columnName, (object) stringBuilder.ToString()));
            context.SaveChanges();
          }
          catch (Exception ex)
          {
            context.ClearChanges();
            Log.Write((object) string.Format("FAILED: {0} - {1}", (object) format, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw;
          }
          stringBuilder.Clear();
        }
        else
          stringBuilder.Append(",");
      }
    }

    private static void DropPageLanguageLinkTable(OpenAccessContext context, SqlGenerator gen)
    {
      string dropTable = gen.GetDropTable("sf_page_language_link");
      try
      {
        context.ExecuteNonQuery(dropTable);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        context.ClearChanges();
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) dropTable, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    private static void DeleteOrphanedPageData(OpenAccessContext context, SqlGenerator gen)
    {
      using (OACommand command = context.Connection.CreateCommand())
      {
        string str = "DELETE FROM {0} WHERE ({1} IS NULL AND {2} = '00000000-0000-0000-0000-000000000000') OR {3} = 1".Arrange((object) gen.GetTableName("sf_page_data"), (object) gen.GetColumnName("page_node_id"), (object) gen.GetColumnName("personalization_master_id"), (object) gen.GetColumnName("is_auto_created"));
        try
        {
          command.CommandText = str;
          command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            return;
          throw;
        }
      }
    }

    private static void DeleteOrphanedPageDataRelatedTable(
      OpenAccessContext context,
      string tableName,
      SqlGenerator gen)
    {
      string commandText = "DELETE FROM {0}\r\n\t\t\t\t\tWHERE {1} IN \r\n\t\t\t\t\t\t(SELECT {1} FROM {2} WHERE \r\n                            ({3} IS NULL AND {4} = '00000000-0000-0000-0000-000000000000') OR {5} = 1)".Arrange((object) gen.GetTableName(tableName), (object) gen.GetColumnName("content_id"), (object) gen.GetTableName("sf_page_data"), (object) gen.GetColumnName("page_node_id"), (object) gen.GetColumnName("personalization_master_id"), (object) gen.GetColumnName("is_auto_created"));
      try
      {
        context.ExecuteNonQuery(commandText);
        context.SaveChanges();
      }
      catch (Exception ex)
      {
        context.ClearChanges();
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) commandText, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    private void DeleteOrphanedNodes(
      Dictionary<Guid, Guid> orphanedMergedNodesDestinationNodes)
    {
      Guid guid = Guid.Empty;
      try
      {
        PageManager manager = PageManager.GetManager((string) null, this.TransactionName);
        using (new ElevatedModeRegion((IManager) manager))
        {
          foreach (Guid key in orphanedMergedNodesDestinationNodes.Keys)
          {
            guid = key;
            try
            {
              PageNode pageNode1 = manager.GetPageNode(orphanedMergedNodesDestinationNodes[key]);
              if (pageNode1 != null)
              {
                PageNodeReference pageNodeReference = new PageNodeReference(pageNode1.ApplicationName, key);
                pageNode1.PageNodeReferences.Add(pageNodeReference);
              }
              PageNode pageNode2 = manager.GetPageNode(key);
              foreach (Permission permission in (IEnumerable<Permission>) pageNode2.Permissions)
              {
                foreach (ISecuredObject permissionsInheritor in manager.Provider.GetPermissionsInheritors((ISecuredObject) pageNode2, false, typeof (PageNode)))
                {
                  if (permissionsInheritor.Permissions.Contains(permission))
                    permissionsInheritor.Permissions.Remove(permission);
                }
              }
              pageNode2.Parent = (PageNode) null;
              manager.Delete(pageNode2);
            }
            catch
            {
            }
          }
          TransactionManager.CommitTransaction(this.TransactionName);
          Log.Write((object) "PASSED: DeleteOrphanedNodes.", ConfigurationPolicy.UpgradeTrace);
        }
      }
      catch (Exception ex)
      {
        if (guid != Guid.Empty)
          Log.Write((object) "Failed to delete node: {0}".Arrange((object) guid), ConfigurationPolicy.UpgradeTrace);
        TransactionManager.RollbackTransaction(this.TransactionName);
        Log.Write((object) string.Format("FAILED: DeleteOrphanedNodes - {0}. See the error log for more details", (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
      finally
      {
        TransactionManager.DisposeTransaction(this.TransactionName);
      }
    }

    private static void SetEnableSearchForReferencedNodes()
    {
    }

    private void LogMergeConflicts(
      Dictionary<Guid, List<PageMigrator.PageNodeMigrationInfo>> mergedNodeLanguageNodes,
      bool crawlableDefault,
      bool requireSslDefault)
    {
      StringBuilder conflictsInfo = (StringBuilder) null;
      foreach (KeyValuePair<Guid, List<PageMigrator.PageNodeMigrationInfo>> nodeLanguageNode in mergedNodeLanguageNodes)
      {
        if (conflictsInfo == null)
        {
          conflictsInfo = new StringBuilder(1000);
          conflictsInfo.AppendLine("List of PageNodes that have conflicting values during merge operation of split nodes and their values has been discarded as they are the default ones (the manually set value on other page nodes prevails):");
        }
        List<PageMigrator.PageNodeMigrationInfo> source = nodeLanguageNode.Value;
        if (source != null)
        {
          int num1 = source.Select<PageMigrator.PageNodeMigrationInfo, bool>((Func<PageMigrator.PageNodeMigrationInfo, bool>) (pn => pn.Crawlable)).Distinct<bool>().Count<bool>() > 1 ? 1 : 0;
          int num2 = source.Select<PageMigrator.PageNodeMigrationInfo, bool>((Func<PageMigrator.PageNodeMigrationInfo, bool>) (pn => pn.RequireSsl)).Distinct<bool>().Count<bool>() > 1 ? 1 : 0;
          string upgradeConflictMessage = "PageNode: {0}, id: {1}, culture: {2}. There is a conflicting value for column {3}:{4}. It will be discarded.";
          if (num2 != 0)
            source.Where<PageMigrator.PageNodeMigrationInfo>(closure_0 ?? (closure_0 = (Func<PageMigrator.PageNodeMigrationInfo, bool>) (pni => pni.RequireSsl == requireSslDefault))).ToList<PageMigrator.PageNodeMigrationInfo>().ForEach((System.Action<PageMigrator.PageNodeMigrationInfo>) (pni =>
            {
              conflictsInfo.AppendFormat(upgradeConflictMessage, (object) pni.TitlesPath, (object) pni.Id, (object) pni.Culture, (object) "require_ssl", (object) requireSslDefault);
              conflictsInfo.AppendLine();
            }));
          if (num1 != 0)
            source.Where<PageMigrator.PageNodeMigrationInfo>(closure_1 ?? (closure_1 = (Func<PageMigrator.PageNodeMigrationInfo, bool>) (pni => pni.Crawlable == crawlableDefault))).ToList<PageMigrator.PageNodeMigrationInfo>().ForEach((System.Action<PageMigrator.PageNodeMigrationInfo>) (pni =>
            {
              conflictsInfo.AppendFormat(upgradeConflictMessage, (object) pni.TitlesPath, (object) pni.Id, (object) pni.Culture, (object) "crawlable", (object) crawlableDefault);
              conflictsInfo.AppendLine();
            }));
        }
      }
      if (conflictsInfo == null)
        return;
      Log.Write((object) conflictsInfo.ToString(), ConfigurationPolicy.UpgradeTrace);
    }

    private class PageNodeMigrationInfo
    {
      public PageNodeMigrationInfo(PageNode node, string culture)
      {
        this.Id = node.Id;
        this.Culture = culture;
        this.Crawlable = node.Crawlable;
        this.RequireSsl = node.RequireSsl;
        if (!string.IsNullOrEmpty(culture))
        {
          CultureInfo cultureInfo = CultureInfo.GetCultureInfo(culture);
          this.RedirectUrl = node.RedirectUrl.GetString(cultureInfo, false);
        }
        else
          this.RedirectUrl = node.RedirectUrl.Value;
        this.LinkedNodeId = node.LinkedNodeId;
        this.TitlesPath = node.BuildFullTitlesPath(culture);
      }

      public Guid Id { get; set; }

      public string Culture { get; set; }

      public string TitlesPath { get; set; }

      public bool RequireSsl { get; set; }

      public bool Crawlable { get; set; }

      public string RedirectUrl { get; set; }

      public Guid LinkedNodeId { get; set; }
    }
  }
}
