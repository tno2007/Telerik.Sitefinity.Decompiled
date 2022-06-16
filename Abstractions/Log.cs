// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.Log
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Hosting;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Logging;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Configuration;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Facade for writing a log entry to one or more Sitefinity <see cref="T:System.Diagnostics.TraceListener" />s.  This class is sealed.
  /// </summary>
  public static class Log
  {
    private static object sync = new object();
    private static volatile LogWriter writer;
    private static readonly ICollection<string> emptyCategoriesList = (ICollection<string>) new List<string>(0);
    private static bool? isElmahLoggingEnabled;
    private const int DefaultPriority = -1;
    private const TraceEventType DefaultSeverity = TraceEventType.Information;
    private const int DefaultEventId = 1;
    private const string DefaultTitle = "";
    internal const string LogDirectory = "~/App_Data/Sitefinity/Logs/";

    /// <summary>
    /// Raised before Sitefinity logging configuration starts.
    /// </summary>
    /// <remarks>
    /// <seealso cref="T:Telerik.Sitefinity.Abstractions.LogConfiguringEventArgs" />
    /// </remarks>
    public static event EventHandler<LogConfiguringEventArgs> Configuring;

    /// <summary>
    /// Raised after Sitefinity logging configuration is completed.
    /// </summary>
    /// <remarks>
    /// <seealso cref="T:Telerik.Sitefinity.Abstractions.LogConfiguringEventArgs" />
    /// </remarks>
    public static event EventHandler<LogConfiguredEventArgs> Configured;

    internal static void Configure(IConfigurationSourceBuilder configBuilder)
    {
      configBuilder.ConfigureInstrumentation().EnableLogging();
      ILoggingConfigurationStart logging = configBuilder.ConfigureLogging();
      LogConfiguringEventArgs e1 = (LogConfiguringEventArgs) null;
      if (Log.Configuring != null)
      {
        LogConfiguringEventArgs configuringEventArgs = new LogConfiguringEventArgs();
        configuringEventArgs.Configuration = logging;
        configuringEventArgs.ConfigurationBuilder = configBuilder;
        e1 = configuringEventArgs;
        Log.Configuring((object) null, e1);
      }
      if (e1 != null && e1.Cancel)
        return;
      Log.ConfigureCategories(logging);
      if (Log.Configured == null)
        return;
      LogConfiguredEventArgs configuredEventArgs = new LogConfiguredEventArgs();
      configuredEventArgs.Configuration = logging;
      configuredEventArgs.ConfigurationBuilder = configBuilder;
      LogConfiguredEventArgs e2 = configuredEventArgs;
      Log.Configured((object) null, e2);
    }

    private static void ConfigureCategories(ILoggingConfigurationStart logging)
    {
      IFormatterBuilder formatBuilder1 = Log.LogFormatTemplate("TimeStampSimpleFormatter", "Timestamp: {timestamp}{newline}\nMessage: {message}{newline}\n");
      IFormatterBuilder formatBuilder2 = (IFormatterBuilder) new FormatterBuilder().TextFormatterNamed("Errors");
      ISitefinityLogCategoryConfigurator configurator = ObjectFactory.Resolve<ISitefinityLogCategoryConfigurator>();
      Log.ConfigureCategory(ConfigurationPolicy.Default, "Trace", (ILoggingConfigurationContd) logging, configurator, formatBuilder1, true);
      Log.ConfigureCategory(ConfigurationPolicy.Trace, "Trace", (ILoggingConfigurationContd) logging, configurator, formatBuilder1);
      Log.ConfigureCategory(ConfigurationPolicy.ErrorLog, "Error", (ILoggingConfigurationContd) logging, configurator, formatBuilder2);
      Log.ConfigureCategory(ConfigurationPolicy.Debug, "Debug", (ILoggingConfigurationContd) logging, configurator, formatBuilder1);
      Log.ConfigureCategory(ConfigurationPolicy.UpgradeTrace, "UpgradeTrace", (ILoggingConfigurationContd) logging, configurator, formatBuilder1);
      Log.ConfigureCategory(ConfigurationPolicy.Migration, "Migration", (ILoggingConfigurationContd) logging, configurator, formatBuilder1);
      Log.ConfigureCategory(ConfigurationPolicy.Synchronization, "Synchronization", (ILoggingConfigurationContd) logging, configurator, Log.LogFormatTemplate("SiteSync", "[{timestamp}] {message}"));
      Log.ConfigureCategory(ConfigurationPolicy.HealthCheckLog, "HealthCheck", (ILoggingConfigurationContd) logging, configurator, formatBuilder1);
      Log.ConfigureCategory(ConfigurationPolicy.Authentication, "Authentication", (ILoggingConfigurationContd) logging, configurator, formatBuilder2);
      Log.ConfigureCategory(ConfigurationPolicy.WebClientLog, "WebClient", (ILoggingConfigurationContd) logging, configurator, formatBuilder2);
    }

    internal static ILoggingConfigurationContd ConfigureCategory(
      ILoggingConfigurationContd logConfiguration,
      string categoryName,
      string fileName,
      IFormatterBuilder formatBuilder)
    {
      ISitefinityLogCategoryConfigurator configurator = ObjectFactory.Resolve<ISitefinityLogCategoryConfigurator>();
      return Log.ConfigureCategory(categoryName, fileName, logConfiguration, configurator, formatBuilder);
    }

    private static ILoggingConfigurationContd ConfigureCategory(
      ConfigurationPolicy policy,
      string fileName,
      ILoggingConfigurationContd log,
      ISitefinityLogCategoryConfigurator configurator,
      IFormatterBuilder formatBuilder,
      bool isDefault = false)
    {
      return Log.ConfigureCategory(policy.ToString(), fileName, log, configurator, formatBuilder, isDefault);
    }

    private static ILoggingConfigurationContd ConfigureCategory(
      string categoryName,
      string fileName,
      ILoggingConfigurationContd log,
      ISitefinityLogCategoryConfigurator configurator,
      IFormatterBuilder formatBuilder,
      bool isDefault = false)
    {
      ILoggingConfigurationCustomCategoryStart categoryNamed = log.LogToCategoryNamed(categoryName);
      if (isDefault)
        categoryNamed.WithOptions.SetAsDefaultCategory();
      SitefinityLogCategory category = new SitefinityLogCategory()
      {
        Configuration = categoryNamed,
        Name = categoryName,
        FileName = fileName,
        FormatBuilder = formatBuilder
      };
      configurator.Configure(category);
      return log;
    }

    internal static IFormatterBuilder LogFormatTemplate(
      string name,
      string template)
    {
      return new FormatterBuilder().TextFormatterNamed(name).UsingTemplate(template);
    }

    /// <summary>
    /// Gets the default category configurator by checking sequentially all the available configurators and returns the first that is enabled in the list.
    /// </summary>
    /// <returns>The default enabled configurator.</returns>
    internal static ISitefinityLogCategoryConfigurator GetDefaultCategoryConfigurator() => Log.GetDefaultCategoryConfigurator(false);

    /// <summary>
    /// Gets the default category configurator by checking sequentially all the available configurators and returns the first that is enabled in the list.
    /// </summary>
    /// <param name="safeMode">If true and exception is thrown during configurator check, it will be considered disabled and will skip to the next configurator.</param>
    /// <returns>The default enabled configurator.</returns>
    internal static ISitefinityLogCategoryConfigurator GetDefaultCategoryConfigurator(
      bool safeMode)
    {
      if (Log.EvaluateExpression((Func<bool>) (() => AzureRuntime.IsRunning), safeMode))
        return (ISitefinityLogCategoryConfigurator) new AzureLogCategoryConfigurator();
      return Log.EvaluateExpression((Func<bool>) (() => Log.IsElmahLoggingEnabled()), safeMode) ? (ISitefinityLogCategoryConfigurator) new ElmahLogCategoryConfigurator() : (ISitefinityLogCategoryConfigurator) new FileLogCategoryConfigurator();
    }

    /// <summary>Evaluates the delegate expression.</summary>
    /// <param name="expression">The delegate</param>
    /// <param name="safeMode">If true and error is thrown during expression evaluation, returns false.</param>
    /// <returns>The evaluated value or false if safeMode flag is true and error is thrown during evaluation of the expression</returns>
    private static bool EvaluateExpression(Func<bool> expression, bool safeMode)
    {
      try
      {
        return expression();
      }
      catch (Exception ex)
      {
        if (!safeMode)
          throw;
      }
      return false;
    }

    /// <summary>
    /// Indicates whether the Elmah logging configuration is enabled.
    /// By default Elmah is disabled.
    /// </summary>
    /// <returns></returns>
    private static bool IsElmahLoggingEnabled()
    {
      if (!Log.isElmahLoggingEnabled.HasValue)
      {
        ElmahSettings uiElmahSettings = Config.Get<SystemConfig>().UIElmahSettings;
        if (uiElmahSettings == null)
          Log.isElmahLoggingEnabled = new bool?(false);
        Log.isElmahLoggingEnabled = new bool?(uiElmahSettings.IsElmahLoggingTurnedOn);
      }
      return Log.isElmahLoggingEnabled.Value;
    }

    /// <summary>Query whether logging is enabled.</summary>
    /// <returns><code>true</code> if logging is enabled.</returns>
    public static bool IsLoggingEnabled() => Log.Writer.IsLoggingEnabled();

    /// <summary>
    /// Query whether a <see cref="T:Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry" /> should be logged.
    /// </summary>
    /// <param name="log">The log entry to check</param>
    /// <returns>Returns <code>true</code> if the entry should be logged.</returns>
    public static bool ShouldLog(LogEntry log) => Log.Writer.ShouldLog(log);

    /// <summary>
    /// Maps a Sitefinity log file name to its physical location.
    /// </summary>
    /// <param name="fileName">The name of the log file.</param>
    /// <returns>The physical path of the file specified by <paramref name="fileName" />.</returns>
    public static string MapLogFilePath(string fileName) => HostingEnvironment.MapPath("~/App_Data/Sitefinity/Logs/" + fileName);

    /// <summary>
    /// Public for testing purposes.
    /// Reset the writer used by the <see cref="T:Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.Logger" /> facade.
    /// </summary>
    /// <remarks>
    /// Threads that already acquired the reference to the old writer will fail when it gets disposed.
    /// </remarks>
    public static void Reset()
    {
      lock (Log.sync)
      {
        LogWriter writer = Log.writer;
        Log.writer = (LogWriter) null;
        writer?.Dispose();
      }
    }

    /// <summary>
    /// Add a key/value pair to the <see cref="T:System.Runtime.Remoting.Messaging.CallContext" /> dictionary.
    /// Context items will be recorded with every log entry.
    /// </summary>
    /// <param name="key">Hashtable key</param>
    /// <param name="value">Value.  Objects will be serialized.</param>
    /// <example>The following example demonstrates use of the AddContextItem method.
    /// <code>Logger.SetContextItem("SessionID", myComponent.SessionId);</code></example>
    public static void SetContextItem(object key, object value) => Log.Writer.SetContextItem(key, value);

    /// <summary>Empty the context items dictionary.</summary>
    public static void FlushContextItems() => Log.Writer.FlushContextItems();

    /// <summary>
    /// Gets the instance of <see cref="T:Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.LogWriter" /> used by the facade.
    /// </summary>
    /// <remarks>
    /// The lifetime of this instance is managed by the facade.
    /// </remarks>
    public static LogWriter Writer
    {
      get
      {
        LogWriter writer = Log.writer;
        if (writer == null)
        {
          lock (Log.sync)
          {
            writer = Log.writer;
            if (writer == null)
            {
              writer = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();
              Log.writer = writer;
            }
          }
        }
        return writer;
      }
    }

    /// <summary>Write a new log entry to the default category.</summary>
    /// <example>The following example demonstrates use of the Write method with
    /// one required parameter, message.
    /// <code>Logger.Write("My message body");</code></example>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    public static void Write(object message) => Log.Write(message, Log.emptyCategoriesList, -1, 1, TraceEventType.Information, "", (IDictionary<string, object>) null);

    /// <summary>Write a new log entry to the default category.</summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="severity">The severity of the entry.</param>
    /// <example>The following example demonstrates use of the Write method with
    /// one required parameter, message.
    /// <code>Logger.Write("My message body", TraceEventType.Error);</code></example>
    public static void Write(object message, TraceEventType severity = TraceEventType.Information) => Log.Write(message, Log.emptyCategoriesList, -1, 1, severity, "", (IDictionary<string, object>) null);

    /// <summary>Write a new log entry to a specific category.</summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
    public static void Write(object message, string category) => Log.Write(message, category, -1, 1, TraceEventType.Information, "", (IDictionary<string, object>) null);

    /// <summary>Write a new log entry to a specific category.</summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
    public static void Write(object message, ConfigurationPolicy category) => Log.Write(message, Enum.GetName(typeof (ConfigurationPolicy), (object) category));

    /// <summary>
    /// Write a new log entry with a specific category and priority.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    public static void Write(object message, string category, int priority) => Log.Write(message, category, priority, 1, TraceEventType.Information, "", (IDictionary<string, object>) null);

    /// <summary>
    /// Write a new log entry with a specific category, priority and event pageId.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="eventId">Event number or identifier.</param>
    public static void Write(object message, string category, int priority, int eventId) => Log.Write(message, category, priority, eventId, TraceEventType.Information, "", (IDictionary<string, object>) null);

    /// <summary>
    /// Write a new log entry with a specific category, priority, event pageId and severity.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="eventId">Event number or identifier.</param>
    /// <param name="severity">Log entry severity as a <see cref="T:System.Diagnostics.TraceEventType" /> enumeration. (Unspecified, Information, Warning or Error).</param>
    public static void Write(
      object message,
      string category,
      int priority,
      int eventId,
      TraceEventType severity)
    {
      Log.Write(message, category, priority, eventId, severity, "", (IDictionary<string, object>) null);
    }

    /// <summary>
    /// Write a new log entry with a specific category, priority, event pageId, severity
    /// and title.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="eventId">Event number or identifier.</param>
    /// <param name="severity">Log message severity as a <see cref="T:System.Diagnostics.TraceEventType" /> enumeration. (Unspecified, Information, Warning or Error).</param>
    /// <param name="title">Additional description of the log entry message</param>
    public static void Write(
      object message,
      string category,
      int priority,
      int eventId,
      TraceEventType severity,
      string title)
    {
      Log.Write(message, category, priority, eventId, severity, title, (IDictionary<string, object>) null);
    }

    /// <summary>
    /// Write a new log entry and a dictionary of extended properties.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="properties">Dictionary of key/value pairs to log.</param>
    public static void Write(object message, IDictionary<string, object> properties) => Log.Write(message, Log.emptyCategoriesList, -1, 1, TraceEventType.Information, "", properties);

    /// <summary>
    /// Write a new log entry to a specific category with a dictionary
    /// of extended properties.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
    /// <param name="properties">Dictionary of key/value pairs to log.</param>
    public static void Write(
      object message,
      string category,
      IDictionary<string, object> properties)
    {
      Log.Write(message, category, -1, 1, TraceEventType.Information, "", properties);
    }

    /// <summary>
    /// Write a new log entry to with a specific category, priority and a dictionary
    /// of extended properties.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="properties">Dictionary of key/value pairs to log.</param>
    public static void Write(
      object message,
      string category,
      int priority,
      IDictionary<string, object> properties)
    {
      Log.Write(message, category, priority, 1, TraceEventType.Information, "", properties);
    }

    /// <summary>
    /// Write a new log entry with a specific category, priority, event Id, severity
    /// title and dictionary of extended properties.
    /// </summary>
    /// <example>The following example demonstrates use of the Write method with
    /// a full set of parameters.
    /// <code></code></example>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="eventId">Event number or identifier.</param>
    /// <param name="severity">Log message severity as a <see cref="T:System.Diagnostics.TraceEventType" /> enumeration. (Unspecified, Information, Warning or Error).</param>
    /// <param name="title">Additional description of the log entry message.</param>
    /// <param name="properties">Dictionary of key/value pairs to log.</param>
    public static void Write(
      object message,
      string category,
      int priority,
      int eventId,
      TraceEventType severity,
      string title,
      IDictionary<string, object> properties)
    {
      Log.Write(message, (ICollection<string>) new string[1]
      {
        category
      }, priority, eventId, severity, title, properties);
    }

    /// <summary>
    /// Write a new log entry to a specific collection of categories.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
    public static void Write(object message, ICollection<string> categories) => Log.Write(message, categories, -1, 1, TraceEventType.Information, "", (IDictionary<string, object>) null);

    /// <summary>
    /// Write a new log entry with a specific collection of categories and priority.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    public static void Write(object message, ICollection<string> categories, int priority) => Log.Write(message, categories, priority, 1, TraceEventType.Information, "", (IDictionary<string, object>) null);

    /// <summary>
    /// Write a new log entry with a specific collection of categories, priority and event pageId.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="eventId">Event number or identifier.</param>
    public static void Write(
      object message,
      ICollection<string> categories,
      int priority,
      int eventId)
    {
      Log.Write(message, categories, priority, eventId, TraceEventType.Information, "", (IDictionary<string, object>) null);
    }

    /// <summary>
    /// Write a new log entry with a specific collection of categories, priority, event pageId and severity.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="eventId">Event number or identifier.</param>
    /// <param name="severity">Log entry severity as a <see cref="T:System.Diagnostics.TraceEventType" /> enumeration. (Unspecified, Information, Warning or Error).</param>
    public static void Write(
      object message,
      ICollection<string> categories,
      int priority,
      int eventId,
      TraceEventType severity)
    {
      Log.Write(message, categories, priority, eventId, severity, "", (IDictionary<string, object>) null);
    }

    /// <summary>
    /// Write a new log entry with a specific collection of categories, priority, event pageId, severity
    /// and title.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="eventId">Event number or identifier.</param>
    /// <param name="severity">Log message severity as a <see cref="T:System.Diagnostics.TraceEventType" /> enumeration. (Unspecified, Information, Warning or Error).</param>
    /// <param name="title">Additional description of the log entry message</param>
    public static void Write(
      object message,
      ICollection<string> categories,
      int priority,
      int eventId,
      TraceEventType severity,
      string title)
    {
      Log.Write(message, categories, priority, eventId, severity, title, (IDictionary<string, object>) null);
    }

    /// <summary>
    /// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
    /// <param name="properties">Dictionary of key/value pairs to log.</param>
    public static void Write(
      object message,
      ICollection<string> categories,
      IDictionary<string, object> properties)
    {
      Log.Write(message, categories, -1, 1, TraceEventType.Information, "", properties);
    }

    /// <summary>
    /// Write a new log entry to with a specific collection of categories, priority and a dictionary
    /// of extended properties.
    /// </summary>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="properties">Dictionary of key/value pairs to log.</param>
    public static void Write(
      object message,
      ICollection<string> categories,
      int priority,
      IDictionary<string, object> properties)
    {
      Log.Write(message, categories, priority, 1, TraceEventType.Information, "", properties);
    }

    /// <summary>
    /// Write a new log entry with a specific category, priority, event Id, severity
    /// title and dictionary of extended properties.
    /// </summary>
    /// <example>The following example demonstrates use of the Write method with
    /// a full set of parameters.
    /// <code></code></example>
    /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
    /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
    /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
    /// <param name="eventId">Event number or identifier.</param>
    /// <param name="severity">Log message severity as a <see cref="T:System.Diagnostics.TraceEventType" /> enumeration. (Unspecified, Information, Warning or Error).</param>
    /// <param name="title">Additional description of the log entry message.</param>
    /// <param name="properties">Dictionary of key/value pairs to log.</param>
    public static void Write(
      object message,
      ICollection<string> categories,
      int priority,
      int eventId,
      TraceEventType severity,
      string title,
      IDictionary<string, object> properties)
    {
      Log.Write(new LogEntry()
      {
        Message = message.ToString(),
        Categories = categories,
        Priority = priority,
        EventId = eventId,
        Severity = severity,
        Title = title,
        ExtendedProperties = properties
      });
    }

    /// <summary>
    /// Write a new log entry as defined in the <see cref="T:Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry" /> parameter.
    /// </summary>
    /// <example>The following examples demonstrates use of the Write method using
    /// a <see cref="T:Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry" /> type.
    /// <code>
    /// LogEntry log = new LogEntry();
    /// log.Category = "MyCategory1";
    /// log.Message = "My message body";
    /// log.Severity = TraceEventType.Error;
    /// log.Priority = 100;
    /// Logger.Write(log);</code></example>
    /// <param name="log">Log entry object to write.</param>
    public static void Write(LogEntry log) => Log.Writer.Write(log);

    /// <summary>
    /// Returns the filter of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The type of filter required.</typeparam>
    /// <returns>The instance of <typeparamref name="T" /> in the filters collection, or <see langword="null" />
    /// if there is no such instance.</returns>
    public static T GetFilter<T>() where T : class, ILogFilter => Log.Writer.GetFilter<T>();

    /// <summary>
    /// Returns the filter of type <typeparamref name="T" /> named <paramref name="name" />.
    /// </summary>
    /// <typeparam name="T">The type of filter required.</typeparam>
    /// <param name="name">The name of the filter required.</param>
    /// <returns>The instance of <typeparamref name="T" /> named <paramref name="name" /> in
    /// the filters collection, or <see langword="null" /> if there is no such instance.</returns>
    public static T GetFilter<T>(string name) where T : class, ILogFilter => Log.Writer.GetFilter<T>(name);

    /// <summary>
    /// Returns the filter named <paramref name="name" />.
    /// </summary>
    /// <param name="name">The name of the filter required.</param>
    /// <returns>The filter named <paramref name="name" /> in
    /// the filters collection, or <see langword="null" /> if there is no such filter.</returns>
    public static ILogFilter GetFilter(string name) => Log.Writer.GetFilter(name);

    [Conditional("DEBUG")]
    internal static void Debug(string format, params object[] args) => Log.WriteFormat(ConfigurationPolicy.Debug.ToString(), format, args);

    internal static void Trace(string format, params object[] args) => Log.WriteFormat(ConfigurationPolicy.Trace.ToString(), format, args);

    internal static void Error(string format, params object[] args) => Log.WriteFormat(ConfigurationPolicy.ErrorLog.ToString(), format, args);

    internal static void Upgrade(string format, params object[] args) => Log.WriteFormat(ConfigurationPolicy.UpgradeTrace.ToString(), format, args);

    internal static void WebClientError(string text) => Log.Write((object) text, ConfigurationPolicy.WebClientLog.ToString());

    internal static void WriteFormat(string category, string format, params object[] args) => Log.Write((object) string.Format(format, args), category);
  }
}
