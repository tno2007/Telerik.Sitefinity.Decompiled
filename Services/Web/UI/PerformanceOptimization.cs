// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Web.UI.PerformanceOptimization
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Data.Common;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Services.Web.UI
{
  /// <summary>Represents the performance optimization backend UI</summary>
  public class PerformanceOptimization : SimpleView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.PerformanceOptimization.PerformanceOptimization.ascx");
    private const int OperationTimeoutSeconds = 7200;
    private const string TSqlRebuildIndexes = "\r\nDECLARE @fillFactor tinyint\r\nDECLARE @tablePrefix nvarchar(100)\r\n\r\nSET @fillFactor = {0}\r\nSET @tablePrefix = '{1}'\r\n\r\nSET NOCOUNT ON\r\nDECLARE @objectid int\r\nDECLARE @indexid int\r\nDECLARE @partitioncount bigint\r\nDECLARE @schemaname nvarchar(130) \r\nDECLARE @objectname nvarchar(130) \r\nDECLARE @indexname nvarchar(130) \r\nDECLARE @partitionnum bigint\r\nDECLARE @partitions bigint\r\nDECLARE @frag float\r\nDECLARE @command nvarchar(4000) \r\nSELECT\r\n\tobject_id AS objectid,\r\n\tindex_id AS indexid,\r\n\tpartition_number AS partitionnum,\r\n\tavg_fragmentation_in_percent AS frag\r\nINTO #work_to_do\r\nFROM sys.dm_db_index_physical_stats (DB_ID(), NULL, NULL , NULL, 'LIMITED')\r\nWHERE \r\n\tindex_id > 0\r\n\r\nDECLARE partitions CURSOR FOR SELECT * FROM #work_to_do\r\n\r\nOPEN partitions\r\n\r\nWHILE (1=1)\r\nBEGIN\r\n\tFETCH NEXT\r\n\tFROM partitions\r\n\tINTO @objectid, @indexid, @partitionnum, @frag\r\n\r\n\tIF @@FETCH_STATUS < 0 BREAK\r\n\r\n\tSELECT @objectname = QUOTENAME(o.name), @schemaname = QUOTENAME(s.name)\r\n\tFROM sys.objects AS o\r\n\tJOIN sys.schemas as s ON s.schema_id = o.schema_id\r\n\tWHERE o.object_id = @objectid\r\n\r\n\tIF (Substring(@objectname, 2, Len(@tablePrefix)) = @tablePrefix)\r\n\tBEGIN\r\n\t\tSELECT @indexname = QUOTENAME(name)\r\n\t\tFROM sys.indexes\r\n\t\tWHERE object_id = @objectid AND index_id = @indexid\r\n\r\n\t\tSELECT @partitioncount = count (*)\r\n\t\tFROM sys.partitions\r\n\t\tWHERE object_id = @objectid AND index_id = @indexid\r\n\r\n\t\tSET @command = N'\r\n\t\t\tALTER INDEX ' + @indexname + N' ON ' + @schemaname + N'.' + @objectname + \r\n\t\t\t\tN' REBUILD WITH (FILLFACTOR = ' + CAST(@fillFactor AS nvarchar) +', SORT_IN_TEMPDB = ON) '\r\n\r\n\t\tIF @partitioncount > 1\r\n\t\t\tSET @command = @command + N' PARTITION=' + CAST(@partitionnum AS nvarchar(10))\r\n\r\n\t\tEXEC sp_ExecuteSQL @command\r\n\t\t\r\n\t\tSET @command = ' \r\n\t\t\tUPDATE STATISTICS ' + @schemaname + N'.' + @objectname + ''\r\n\t\tEXEC sp_ExecuteSQL @command\r\n\tEND\r\nEND\r\n\r\nCLOSE partitions\r\nDEALLOCATE partitions\r\n\r\nDROP TABLE #work_to_do\r\n";
    private const string RebuildIndexOperationName = "RebuildIndex operation";
    private static readonly object maintenanceLock = new object();

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = PerformanceOptimization.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets a reference to the startOptimizationDiv</summary>
    protected virtual HtmlGenericControl StartOptimizationDiv => this.Container.GetControl<HtmlGenericControl>("startOptimizationDiv", true);

    /// <summary>Gets a reference to the perfOptimizationLiteral1</summary>
    protected virtual LiteralControl PerfOptimizationLiteral1 => this.Container.GetControl<LiteralControl>("perfOptimizationLiteral1", true);

    /// <summary>Gets a reference to the perfOptimizationLiteral2</summary>
    protected virtual LiteralControl PerfOptimizationLiteral2 => this.Container.GetControl<LiteralControl>("perfOptimizationLiteral2", true);

    /// <summary>Gets a reference to the startOptimizationButton</summary>
    protected virtual LinkButton StartOptimizationButton => this.Container.GetControl<LinkButton>("startOptimizationButton", true);

    /// <summary>Gets a reference to the goToAdministrationLink</summary>
    protected virtual HtmlAnchor GoToAdministrationLink => this.Container.GetControl<HtmlAnchor>("goToAdministrationLink", true);

    /// <summary>Gets a reference to the perfOptimizationCompletedDiv</summary>
    protected virtual HtmlGenericControl PerfOptimizationCompletedDiv => this.Container.GetControl<HtmlGenericControl>("perfOptimizationCompletedDiv", true);

    /// <summary>Gets a reference to the errorMessageDiv</summary>
    protected virtual HtmlGenericControl ErrorMessageDiv => this.Container.GetControl<HtmlGenericControl>("errorMessageDiv", true);

    /// <summary>Gets a reference to the errorLiteral</summary>
    protected virtual Literal ErrorLiteral => (Literal) this.ErrorMessageDiv.FindControl("errorLiteral");

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.StartOptimizationButton.Click += new EventHandler(this.StartOptimizationButton_Click);
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(Config.Get<PagesConfig>().BackendHomePageId, false);
      if (siteMapNode != null)
        this.GoToAdministrationLink.HRef = siteMapNode.Url;
      else
        this.GoToAdministrationLink.HRef = "~/Sitefinity";
    }

    private void StartOptimizationButton_Click(object sender, EventArgs e)
    {
      SystemManager.CurrentHttpContext.Server.ScriptTimeout = 7200;
      lock (PerformanceOptimization.maintenanceLock)
      {
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        SystemManager.CurrentContext.Culture = CultureInfo.InvariantCulture;
        StringBuilder stringBuilder = new StringBuilder();
        bool flag = true;
        string errorMessage;
        if (PerformanceOptimization.TryRebuildIndexes(out errorMessage))
        {
          Log.Write((object) string.Format("PASSED : {0}", (object) "RebuildIndex operation"), ConfigurationPolicy.UpgradeTrace);
        }
        else
        {
          flag = false;
          stringBuilder.AppendLine(errorMessage);
          Log.Write((object) string.Format("Failed : {0} - {1}", (object) "RebuildIndex operation", (object) errorMessage), ConfigurationPolicy.UpgradeTrace);
        }
        if (!flag)
          this.ShowError(stringBuilder.ToString());
        else
          this.ShowSuccess();
        SystemManager.CurrentContext.Culture = culture;
        SystemManager.RestartApplication(OperationReason.FromKey("PerformanceOptimizationTask"), SystemRestartFlags.AttemptFullRestart);
      }
    }

    /// <summary>Executes the non query command.</summary>
    /// <param name="oaContext">The oa context.</param>
    /// <param name="cmdText">The CMD text.</param>
    /// <param name="cmdTimeout">The CMD timeout in seconds.</param>
    /// <returns></returns>
    internal static int ExecuteNonQueryCommand(
      OpenAccessContext oaContext,
      string cmdText,
      int? cmdTimeout = null)
    {
      OACommand command = oaContext.Connection.CreateCommand();
      command.CommandText = cmdText;
      if (cmdTimeout.HasValue)
        command.CommandTimeout = cmdTimeout.Value;
      return command.ExecuteNonQuery();
    }

    /// <summary>
    /// Tries to rebuild the indexes. Executes on MS database only.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="fillFactor">The fill factor.</param>
    /// <param name="tablePrefix">The table prefix.</param>
    /// <returns></returns>
    internal static bool TryRebuildIndexes(
      out string errorMessage,
      int fillFactor = 90,
      string tablePrefix = "sf_")
    {
      errorMessage = string.Empty;
      bool flag = true;
      try
      {
        if (PageManager.GetManager().Provider is IOpenAccessDataProvider provider)
        {
          SitefinityOAContext context = provider.GetContext();
          if (context != null)
          {
            using (UpgradingContext oaContext = new UpgradingContext(context))
            {
              switch (oaContext.Connection.DbType)
              {
                case DatabaseType.MsSql:
                case DatabaseType.SqlAzure:
                case DatabaseType.SqlCE:
                  string rebuildIndexScript = PerformanceOptimization.GetTSqlRebuildIndexScript(fillFactor, tablePrefix);
                  PerformanceOptimization.ExecuteNonQueryCommand((OpenAccessContext) oaContext, rebuildIndexScript, new int?(7200));
                  break;
                case DatabaseType.Oracle:
                case DatabaseType.MySQL:
                case DatabaseType.PostgreSql:
                  break;
                default:
                  throw new ArgumentException(string.Format("Unsupported database type: {0}", (object) Enum.GetName(typeof (DatabaseType), (object) oaContext.Connection.DbType)));
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        flag = false;
        errorMessage = ex.Message;
        Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);
      }
      return flag;
    }

    private void ShowSuccess() => this.ShowFinal();

    private void ShowError(string errorText)
    {
      this.ShowFinal(true);
      this.ErrorLiteral.Text = errorText;
    }

    private void ShowFinal(bool error = false)
    {
      this.StartOptimizationDiv.Visible = false;
      this.GoToAdministrationLink.Visible = true;
      this.PerfOptimizationCompletedDiv.Visible = !error;
      this.ErrorMessageDiv.Visible = error;
    }

    internal static string GetTSqlRebuildIndexScript(int fillFactor = 90, string tablePrefix = "sf_") => string.Format("\r\nDECLARE @fillFactor tinyint\r\nDECLARE @tablePrefix nvarchar(100)\r\n\r\nSET @fillFactor = {0}\r\nSET @tablePrefix = '{1}'\r\n\r\nSET NOCOUNT ON\r\nDECLARE @objectid int\r\nDECLARE @indexid int\r\nDECLARE @partitioncount bigint\r\nDECLARE @schemaname nvarchar(130) \r\nDECLARE @objectname nvarchar(130) \r\nDECLARE @indexname nvarchar(130) \r\nDECLARE @partitionnum bigint\r\nDECLARE @partitions bigint\r\nDECLARE @frag float\r\nDECLARE @command nvarchar(4000) \r\nSELECT\r\n\tobject_id AS objectid,\r\n\tindex_id AS indexid,\r\n\tpartition_number AS partitionnum,\r\n\tavg_fragmentation_in_percent AS frag\r\nINTO #work_to_do\r\nFROM sys.dm_db_index_physical_stats (DB_ID(), NULL, NULL , NULL, 'LIMITED')\r\nWHERE \r\n\tindex_id > 0\r\n\r\nDECLARE partitions CURSOR FOR SELECT * FROM #work_to_do\r\n\r\nOPEN partitions\r\n\r\nWHILE (1=1)\r\nBEGIN\r\n\tFETCH NEXT\r\n\tFROM partitions\r\n\tINTO @objectid, @indexid, @partitionnum, @frag\r\n\r\n\tIF @@FETCH_STATUS < 0 BREAK\r\n\r\n\tSELECT @objectname = QUOTENAME(o.name), @schemaname = QUOTENAME(s.name)\r\n\tFROM sys.objects AS o\r\n\tJOIN sys.schemas as s ON s.schema_id = o.schema_id\r\n\tWHERE o.object_id = @objectid\r\n\r\n\tIF (Substring(@objectname, 2, Len(@tablePrefix)) = @tablePrefix)\r\n\tBEGIN\r\n\t\tSELECT @indexname = QUOTENAME(name)\r\n\t\tFROM sys.indexes\r\n\t\tWHERE object_id = @objectid AND index_id = @indexid\r\n\r\n\t\tSELECT @partitioncount = count (*)\r\n\t\tFROM sys.partitions\r\n\t\tWHERE object_id = @objectid AND index_id = @indexid\r\n\r\n\t\tSET @command = N'\r\n\t\t\tALTER INDEX ' + @indexname + N' ON ' + @schemaname + N'.' + @objectname + \r\n\t\t\t\tN' REBUILD WITH (FILLFACTOR = ' + CAST(@fillFactor AS nvarchar) +', SORT_IN_TEMPDB = ON) '\r\n\r\n\t\tIF @partitioncount > 1\r\n\t\t\tSET @command = @command + N' PARTITION=' + CAST(@partitionnum AS nvarchar(10))\r\n\r\n\t\tEXEC sp_ExecuteSQL @command\r\n\t\t\r\n\t\tSET @command = ' \r\n\t\t\tUPDATE STATISTICS ' + @schemaname + N'.' + @objectname + ''\r\n\t\tEXEC sp_ExecuteSQL @command\r\n\tEND\r\nEND\r\n\r\nCLOSE partitions\r\nDEALLOCATE partitions\r\n\r\nDROP TABLE #work_to_do\r\n", (object) fillFactor, (object) tablePrefix);
  }
}
