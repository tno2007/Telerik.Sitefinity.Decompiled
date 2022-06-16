// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Model.SiteOptimizationsModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.UsageTracking.Model
{
  /// <summary>
  /// Contains information about the best practices recommended by Sitefinity
  /// https://www.progress.com/documentation/sitefinity-cms/best-practices-configure-your-project-for-a-production-environment#static-content-cache-expiration
  /// </summary>
  internal class SiteOptimizationsModel
  {
    public bool ErrorPagesConfiguredInWebConfig { get; set; }

    public bool StatciContentCacheSetToUseMaxAge { get; set; }

    public bool HasInformationWCFServiceTracing { get; set; }
  }
}
