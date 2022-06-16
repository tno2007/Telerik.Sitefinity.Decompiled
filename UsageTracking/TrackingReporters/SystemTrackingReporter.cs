// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.TrackingReporters.SystemTrackingReporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.SiteSettings.Configuration;
using Telerik.Sitefinity.UsageTracking.Model;
using Telerik.Sitefinity.UsageTracking.Utilities;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.UsageTracking.TrackingReporters
{
  internal class SystemTrackingReporter : ITrackingReporter
  {
    private const string PagesModuleName = "PagesModule";
    private const string WorkflowsModuleName = "WorkflowsModule";
    internal const string WidgetEditorsModuleName = "WidgetEdiotrs";
    private readonly PagesReportGenerator pagesReportGenerator;
    private readonly TaxonomiesReportGenerator taxonomiesReportGenerator;
    private readonly WebConfigReader webConfigReader;

    public SystemTrackingReporter()
    {
      this.pagesReportGenerator = new PagesReportGenerator();
      this.taxonomiesReportGenerator = new TaxonomiesReportGenerator();
      this.webConfigReader = new WebConfigReader("~/web.config");
    }

    internal SystemTrackingReporter(
      PagesReportGenerator pagesReportGenerator,
      TaxonomiesReportGenerator taxonomiesReportGenerator,
      WebConfigReader webconfigReader)
    {
      this.pagesReportGenerator = pagesReportGenerator;
      this.taxonomiesReportGenerator = taxonomiesReportGenerator;
      this.webConfigReader = webconfigReader;
    }

    public object GetReport()
    {
      string str = Assembly.GetAssembly(this.GetType()).GetName().Version.ToString();
      LicenseInfo licenseInfo = this.GetLicenseInfo();
      string dataBaseType = this.GetDataBaseType();
      IQueryable<UserProfile> userProfiles = UserProfilesHelper.GetUserProfileManager(typeof (SitefinityProfile)).GetUserProfiles();
      int num1 = UserManager.GetManager().GetUsers().Where<User>((Expression<Func<User, bool>>) (i => i.IsBackendUser == false)).Count<User>();
      List<string> languagesInTheSystem1 = this.GetFrontLanguagesInTheSystem();
      List<string> languagesInTheSystem2 = this.GetBackendLanguagesInTheSystem();
      bool legacyMultilingual = this.GetIsSystemUpgradedFromLegacyMultilingual();
      bool languageFallback = this.GetIsSystemUsingLanguageFallback();
      string defaultSystemCulture1 = this.GetDefaultSystemCulture();
      List<string> backendLanguages = languagesInTheSystem2;
      int num2 = legacyMultilingual ? 1 : 0;
      int num3 = languageFallback ? 1 : 0;
      string defaultSystemCulture2 = defaultSystemCulture1;
      LanguagesReportModel languagesReportModel = new LanguagesReportModel(languagesInTheSystem1, backendLanguages, num2 != 0, num3 != 0, defaultSystemCulture2);
      TrackingReportModel report = new TrackingReportModel()
      {
        ClientId = licenseInfo.LicenseId,
        Url = this.GetAbsolutePathRootUrlOfFirstRequest(),
        DbType = dataBaseType,
        Date = DateTime.UtcNow,
        SitefinityLicense = licenseInfo.LicenseType,
        SitefinityLicenseAddons = licenseInfo.Addons,
        SitefinityVersion = str,
        IsTrial = licenseInfo.IsTrial,
        CompanyName = licenseInfo.Customer.CompanyName,
        Domains = licenseInfo.AllLicensedDomains.ToList<string>(),
        ActiveModules = (IEnumerable<string>) this.GetModules().Select<KeyValuePair<string, IModule>, string>((Func<KeyValuePair<string, IModule>, string>) (m => m.Key)).OrderBy<string, string>((Func<string, string>) (m => m)),
        ModulesInfo = new List<object>(),
        ActiveUsersCount = userProfiles.Count<UserProfile>(),
        CustomErrorPages = this.GetCustomErrorPagesReport(),
        EmailMessageTemplates = this.GetEmailMessageTemplatesReport(),
        ActiveFrontendUsersCount = num1,
        IsDebuggingEnabled = SystemManager.CurrentHttpContext.IsDebuggingEnabled,
        Languages = languagesReportModel,
        CacheProvider = this.GetOutputCacheProvider(),
        SiteOptimizations = this.GetSiteOptimizations(),
        ConfigStorageMode = this.GetConfigStorageMode(),
        NonGlobalUserGroupsCount = this.GetNonGlobalUserGroupsCount(),
        CountOfSiteSpecificPropertiesPerConfigSection = this.GetCountOfSiteSpecificPropertiesPerConfigSection(),
        FilterQueriesByViewPermissions = Config.Get<SecurityConfig>().FilterQueriesByViewPermissions
      };
      this.AppendPagesModuleInfo(report);
      this.AppendWidgetEditorsInfo(report);
      this.AppendWorkflowsInfo(report);
      this.AppendTaxonomiesModuleInfo(report);
      return (object) report;
    }

    private int GetNonGlobalUserGroupsCount() => UserManager.GetManager().GetProviderNames(ProviderBindingOptions.NoFilter).Count<string>((Func<string, bool>) (p => !SecurityManager.IsGlobalUserProvider(p)));

    private Dictionary<string, int> GetCountOfSiteSpecificPropertiesPerConfigSection() => Config.Get<SiteSettingsConfig>().SiteSpecificProperties.Values.GroupBy<PropertyPath, string>((Func<PropertyPath, string>) (p => p.Path.Split('/')[0])).ToDictionary<IGrouping<string, PropertyPath>, string, int>((Func<IGrouping<string, PropertyPath>, string>) (x => x.Key), (Func<IGrouping<string, PropertyPath>, int>) (x => x.Count<PropertyPath>()));

    private SiteOptimizationsModel GetSiteOptimizations() => new SiteOptimizationsModel()
    {
      ErrorPagesConfiguredInWebConfig = this.webConfigReader.AreCustomErrorsConfigured,
      StatciContentCacheSetToUseMaxAge = this.webConfigReader.StaticContentCacheSetToUseMaxMage,
      HasInformationWCFServiceTracing = this.webConfigReader.HasInformationWCFServiceTracing
    };

    private void AppendWorkflowsInfo(TrackingReportModel report)
    {
      WorkflowsReportModel workflowsReportModel = new WorkflowsReportModel();
      workflowsReportModel.ModuleName = "WorkflowsModule";
      workflowsReportModel.Workflows = new List<WorkflowsReportModel.WorkflowReportModel>();
      foreach (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition in (IEnumerable<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition>) WorkflowManager.GetManager().GetWorkflowDefinitions())
      {
        WorkflowsReportModel.WorkflowReportModel workflowReportModel = new WorkflowsReportModel.WorkflowReportModel();
        workflowReportModel.LevelsCount = workflowDefinition.Levels.Count;
        workflowReportModel.Scopes = new List<WorkflowsReportModel.WorkflowScopeReportModel>();
        foreach (WorkflowScope scope in (IEnumerable<WorkflowScope>) workflowDefinition.Scopes)
          workflowReportModel.Scopes.Add(new WorkflowsReportModel.WorkflowScopeReportModel()
          {
            ContentTypes = scope.TypeScopes.Select<WorkflowTypeScope, string>((Func<WorkflowTypeScope, string>) (t => t.ContentType)).ToList<string>(),
            ForSpecificLanguage = !string.IsNullOrEmpty(scope.Language),
            ForSpecificSite = scope.GetScopeSiteId() != Guid.Empty
          });
        workflowsReportModel.Workflows.Add(workflowReportModel);
      }
      report.ModulesInfo.Add((object) workflowsReportModel);
    }

    /// <summary>
    /// Tracks information whether the new AdminApp based widget property editors are enabled.
    /// </summary>
    /// <param name="report">The tracking report model.</param>
    private void AppendWidgetEditorsInfo(TrackingReportModel report)
    {
      PagesConfig pagesConfig = Config.Get<PagesConfig>();
      report.ModulesInfo.Add((object) new WidgetEditorsReportModel()
      {
        ModuleName = "WidgetEdiotrs",
        IsAdminAppWidgetEditorEnabled = pagesConfig.EnableAdminAppWidgetEditors,
        WhitelistedAdminAppWidgetEditors = pagesConfig.WhitelistedAdminAppWidgetEditors
      });
    }

    private string GetAbsolutePathRootUrlOfFirstRequest() => SystemManager.AbsolutePathRootUrlOfFirstRequest;

    private LicenseInfo GetLicenseInfo() => LicenseState.Current.LicenseInfo;

    private IDictionary<string, IModule> GetModules() => SystemManager.ApplicationModules;

    private string GetDataBaseType()
    {
      DataConfig dataConfig = Config.Get<DataConfig>();
      string empty = string.Empty;
      IConnectionStringSettings connectionStringSettings;
      ref IConnectionStringSettings local = ref connectionStringSettings;
      if (dataConfig.TryGetConnectionString("Sitefinity", out local))
        empty = connectionStringSettings.DatabaseType.ToString();
      return empty;
    }

    private void AppendPagesModuleInfo(TrackingReportModel report)
    {
      PagesReportModel report1 = this.pagesReportGenerator.GenerateReport();
      report.ModulesInfo.Add((object) report1);
    }

    private void AppendTaxonomiesModuleInfo(TrackingReportModel report)
    {
      TaxonomiesReportModel report1 = this.taxonomiesReportGenerator.GenerateReport();
      report.ModulesInfo.Add((object) report1);
    }

    private string GetOutputCacheProvider() => Config.Get<SystemConfig>().CacheSettings.DefaultCacheProvider.ToString();

    private List<string> GetBackendLanguagesInTheSystem() => ((IEnumerable<CultureInfo>) AppSettings.CurrentSettings.DefinedBackendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (lang => lang.Name)).ToList<string>();

    private bool GetIsSystemUsingLanguageFallback() => AppSettings.CurrentSettings.LanguageFallback;

    private bool GetIsSystemUpgradedFromLegacyMultilingual() => AppSettings.CurrentSettings.LegacyMultilingual;

    private string GetDefaultSystemCulture() => AppSettings.CurrentSettings.DefaultFrontendLanguage.Name;

    private List<object> GetEmailMessageTemplatesReport()
    {
      List<object> messageTemplatesReport = new List<object>();
      IEnumerable<IMessageTemplateResponse> source = SystemManager.GetNotificationService().GetMessageTemplates((ServiceContext) null, (QueryParameters) null).Where<IMessageTemplateResponse>((Func<IMessageTemplateResponse, bool>) (x => !x.ResolveKey.IsNullOrEmpty()));
      if (source.Any<IMessageTemplateResponse>())
        messageTemplatesReport.AddRange((IEnumerable<object>) source.Select<IMessageTemplateResponse, EmailMessageTemplatesReportModel>((Func<IMessageTemplateResponse, EmailMessageTemplatesReportModel>) (x => new EmailMessageTemplatesReportModel()
        {
          ModuleName = x.ModuleName,
          ResolveKey = x.ResolveKey,
          Subject = x.Subject,
          LastModified = x.LastModified,
          TemplateSenderName = x.TemplateSenderName
        })));
      return messageTemplatesReport;
    }

    private CustomErrorPagesReportModel GetCustomErrorPagesReport() => new CustomErrorPagesReporter().GetCustomErrorPagesReport();

    private PageReportModel GetPageReport(
      IQueryable<PageNode> standardPages,
      PageTemplateFramework framework,
      bool includeItemsWithoutTemplate = false)
    {
      int num1 = standardPages.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => (int) pd.Template.Framework == (int) framework)))).Count<PageNode>();
      int num2 = standardPages.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => pd.Visible && (int) pd.Template.Framework == (int) framework)))).Count<PageNode>();
      if (includeItemsWithoutTemplate)
      {
        int num3 = standardPages.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => pd.Template == default (object))))).Count<PageNode>();
        int num4 = standardPages.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => pd.Visible && pd.Template == default (object))))).Count<PageNode>();
        num1 += num3;
        num2 += num4;
      }
      return new PageReportModel()
      {
        TotalCount = num1,
        LiveCount = num2
      };
    }

    private List<string> GetFrontLanguagesInTheSystem() => ((IEnumerable<CultureInfo>) AppSettings.CurrentSettings.DefinedFrontendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (lang => lang.Name)).ToList<string>();

    private string GetConfigStorageMode() => Config.ConfigStorageMode.ToString();
  }
}
