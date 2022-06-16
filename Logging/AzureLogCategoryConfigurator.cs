// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Logging.AzureLogCategoryConfigurator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.WindowsAzure.Diagnostics;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Telerik.Sitefinity.Logging
{
  /// <summary>
  /// The built-in <see cref="T:Telerik.Sitefinity.Logging.ISitefinityLogCategoryConfigurator" /> implementation
  /// configuring Windows Azure Diagnostics (WAD) logging.
  /// </summary>
  internal class AzureLogCategoryConfigurator : ISitefinityLogCategoryConfigurator
  {
    /// <inheritdoc />
    public void Configure(SitefinityLogCategory category)
    {
      if (null != null)
        return;
      category.Configuration.WithOptions.SendTo.SystemDiagnosticsListener(category.Name).ForTraceListenerType<DiagnosticMonitorTraceListener>();
    }
  }
}
