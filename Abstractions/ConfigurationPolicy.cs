// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ConfigurationPolicy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>Defines policy names for application blocks.</summary>
  public enum ConfigurationPolicy
  {
    /// <summary>Defines the name of the default logging category.</summary>
    [Description("Default Logging Category")] Default,
    /// <summary>
    /// Use <see cref="M:Telerik.Sitefinity.Abstractions.Log.Debug(System.String,System.Object[])" /> to log during debugging only (when <c>DEBUG</c> is defined).
    /// </summary>
    [Description("Debug Log")] Debug,
    /// <summary>
    /// Defines the name of the logging category for tracing information.
    /// </summary>
    [Description("Trace Logging Category")] Trace,
    /// <summary>Defines category for logging errors.</summary>
    [Description("Error Log")] ErrorLog,
    /// <summary>Defines category for logging Iris errors.</summary>
    [Description("WebClient Log")] WebClientLog,
    /// <summary>Specifies flat file listener.</summary>
    [Description("Flat File")] FlatFile,
    /// <summary>Defines text formatter for error logs.</summary>
    [Description("Exception Formatter")] ExceptionFormatter,
    /// <summary>
    /// Defines the name of the logging category for tracing upgrade and install information.
    /// </summary>
    [Description("Upgrade Trace Logging Category")] UpgradeTrace,
    /// <summary>
    /// Defines the name of the logging category for migration errors
    /// </summary>
    [Description("Migration")] Migration,
    /// <summary>
    /// Defines the name of the logging category for tracing information during tests
    /// </summary>
    [Description("Test Tracing")] TestTracing,
    /// <summary>
    /// Defines the name of the logging category for tracing synchronization.
    /// </summary>
    [Description("Synchronization")] Synchronization,
    /// <summary>
    /// Defines the name of the logging category for tracing mobile application information.
    /// </summary>
    [Obsolete, Description("MobileApplication")] MobileApplication,
    /// <summary>
    /// Defines the name of the logging category for tracing lightning information.
    /// </summary>
    [Obsolete, Description("Digital Asset Management")] DAM,
    /// <summary>
    /// Defines the name of the logging category for tracing content export.
    /// </summary>
    [Description("ContentExport"), Obsolete("Use ConfigurationPolicy.PackagingTrace")] ContentExport,
    /// <summary>
    /// Defines the name of the logging category for tracing content import.
    /// </summary>
    [Description("ContentImport"), Obsolete("Use ConfigurationPolicy.PackagingTrace")] ContentImport,
    /// <summary>
    /// Defines the name of the logging category for tracing package import/export.
    /// </summary>
    [Description("PackagingTrace")] PackagingTrace,
    /// <summary>
    /// Defines the name of the logging category for authentication errors
    /// </summary>
    [Description("Authentication Error Log")] Authentication,
    /// <summary>
    /// Defines the name of the logging category for tracing HealthCheck logs.
    /// </summary>
    [Description("HealthCheckLog")] HealthCheckLog,
    /// <summary>
    /// Defines the name of the logging category for tracing Eloqua connector logs.
    /// </summary>
    [Description("EloquaLog")] EloquaLog,
    /// <summary>
    /// Defines the name of the logging category for tracing HubSpot connector logs.
    /// </summary>
    [Description("HubSpotLog")] HubSpotLog,
    /// <summary>
    /// Defines the name of the logging category for tracing AB Testing connector logs.
    /// </summary>
    [Description("AB testing trace")] ABTestingTrace,
  }
}
