// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Model.TrackingReportModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.UsageTracking.Model
{
  internal class TrackingReportModel
  {
    public string ClientId { get; set; }

    public string Url { get; set; }

    public string DbType { get; set; }

    public DateTime Date { get; set; }

    public string SitefinityLicense { get; set; }

    public IEnumerable<string> SitefinityLicenseAddons { get; set; }

    public bool IsTrial { get; set; }

    public string CompanyName { get; set; }

    public bool IsDebuggingEnabled { get; set; }

    public string SitefinityVersion { get; set; }

    public List<string> Domains { get; set; }

    public IEnumerable<string> ActiveModules { get; set; }

    public List<object> ModulesInfo { get; set; }

    public int ActiveUsersCount { get; set; }

    public CustomErrorPagesReportModel CustomErrorPages { get; set; }

    public List<object> EmailMessageTemplates { get; set; }

    public int ActiveFrontendUsersCount { get; set; }

    public string CacheProvider { get; set; }

    public LanguagesReportModel Languages { get; set; }

    public SiteOptimizationsModel SiteOptimizations { get; set; }

    public string ConfigStorageMode { get; set; }

    public int NonGlobalUserGroupsCount { get; set; }

    public bool FilterQueriesByViewPermissions { get; set; }

    public Dictionary<string, int> CountOfSiteSpecificPropertiesPerConfigSection { get; set; }
  }
}
