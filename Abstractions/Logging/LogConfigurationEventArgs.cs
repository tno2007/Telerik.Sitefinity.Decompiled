// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.LogConfigurationEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// The base class of the <see cref="E:Telerik.Sitefinity.Abstractions.Log.Configuring" /> and <see cref="E:Telerik.Sitefinity.Abstractions.Log.Configured" /> event arguments.
  /// </summary>
  public class LogConfigurationEventArgs : EventArgs
  {
    /// <summary>
    /// Gets the fluent logging configuration interface, allowing chaining of fluent configuration calls.
    /// </summary>
    public ILoggingConfigurationStart Configuration { get; internal set; }

    /// <summary>
    /// Gets the configuration source builder, allowing raw access to the configuration sections being built.
    /// </summary>
    public IConfigurationSourceBuilder ConfigurationBuilder { get; internal set; }
  }
}
