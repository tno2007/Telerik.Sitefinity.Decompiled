// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Logging.FileLogCategoryConfigurator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Logging
{
  /// <summary>
  /// The built-in <see cref="T:Telerik.Sitefinity.Logging.ISitefinityLogCategoryConfigurator" /> implementation
  /// configuring file system logs.
  /// </summary>
  internal class FileLogCategoryConfigurator : ISitefinityLogCategoryConfigurator
  {
    /// <inheritdoc />
    public void Configure(SitefinityLogCategory category)
    {
      string filename = Log.MapLogFilePath(category.FileName + ".log");
      category.Configuration.WithOptions.SendTo.RollingFile(category.Name).ToFile(filename).RollEvery(RollInterval.Day).RollAfterSize(1024).WhenRollFileExists(RollFileExistsBehavior.Increment).FormatWith(category.FormatBuilder);
    }
  }
}
