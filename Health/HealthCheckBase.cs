// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.HealthCheckBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Health.Helpers;

namespace Telerik.Sitefinity.Health
{
  /// <summary>Defines a base abstraction for a health check.</summary>
  public abstract class HealthCheckBase : IHealthCheck
  {
    private TimeSpan? timeout = new TimeSpan?(new TimeSpan(0, 0, 30));
    private NameValueCollection parameters;
    private ILog log;
    private readonly string failedMessage = " failed!";
    private readonly string timeoutFailedMessage = " failed with timeout!";
    private readonly string logMessage = "Task name: {0}, Passed: {1}, Message: {2}, Last execution time: {3}, Current time: {4}, Total time: {5} milliseconds";

    /// <inheritdoc />
    public string Name { get; internal set; }

    /// <inheritdoc />
    public TimeSpan? Timeout
    {
      get => this.timeout;
      internal set => this.timeout = value;
    }

    /// <inheritdoc />
    public bool Critical { get; internal set; }

    /// <inheritdoc />
    public NameValueCollection Parameters
    {
      get => this.parameters ?? new NameValueCollection();
      internal set => this.parameters = value;
    }

    /// <inheritdoc />
    public IEnumerable<string> Groups { get; internal set; }

    /// <summary>Gets or sets an instance for writing a log entry.</summary>
    internal virtual ILog Log
    {
      get
      {
        if (this.log == null)
          this.log = (ILog) new Telerik.Sitefinity.Health.Helpers.Log();
        return this.log;
      }
      set => this.log = value;
    }

    /// <inheritdoc />
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not needed")]
    async Task<HealthCheckResult> IHealthCheck.Run()
    {
      DateTime lastExecutionTime = DateTime.UtcNow;
      Stopwatch sw = new Stopwatch();
      sw.Start();
      HealthCheckResult result;
      try
      {
        if (this.Timeout.HasValue && this.Timeout.Value > TimeSpan.Zero)
          result = await this.RunCore().SetTimeout<HealthCheckResult>(this.Timeout.Value).ConfigureAwait(false);
        else
          result = await this.RunCore().ConfigureAwait(false);
      }
      catch (TimeoutException ex)
      {
        this.HandleException((Exception) ex);
        result = HealthCheckResult.Unhealthy(this.Name, this.Name + this.timeoutFailedMessage);
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        result = HealthCheckResult.Unhealthy(this.Name, this.Name + this.failedMessage);
      }
      sw.Stop();
      result.Critical = this.Critical;
      result.LastExecutionTime = lastExecutionTime;
      result.ElapsedMs = sw.Elapsed.TotalMilliseconds;
      result.Groups = this.Groups;
      this.LogHealthCheckResult(result);
      return result;
    }

    /// <summary>Handles the exception</summary>
    /// <param name="ex">An instance of the original exception that was thrown.</param>
    internal virtual void HandleException(Exception ex) => Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);

    /// <summary>
    /// Handles the logging of <see cref="T:Telerik.Sitefinity.Health.HealthCheckResult" />
    /// </summary>
    /// <param name="result">An instance of <see cref="T:Telerik.Sitefinity.Health.HealthCheckResult" />.</param>
    internal virtual void LogHealthCheckResult(HealthCheckResult result) => this.Log.WriteInfo(string.Format((IFormatProvider) CultureInfo.InvariantCulture, this.logMessage, (object) result.Operation, (object) result.Passed, (object) result.Message, (object) result.LastExecutionTime.ToString("HH:mm:ss:ffffff", (IFormatProvider) CultureInfo.InvariantCulture), (object) DateTime.UtcNow.ToString("HH:mm:ss:ffffff", (IFormatProvider) CultureInfo.InvariantCulture), (object) result.ElapsedMs), (result.Passed ? 1 : 0) != 0);

    internal void InitializeInternal(
      string name,
      bool critical,
      string groups,
      int timeoutSeconds,
      NameValueCollection parameters)
    {
      this.Critical = critical;
      this.Name = name;
      this.Timeout = new TimeSpan?(new TimeSpan(0, 0, timeoutSeconds));
      this.Parameters = new NameValueCollection(parameters);
      if (!groups.IsNullOrWhitespace())
        this.Groups = ((IEnumerable<string>) groups.Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (p => p.Trim()));
      this.Initialize(parameters);
    }

    /// <summary>Initializes the health check.</summary>
    /// <param name="parameters">The parameters.</param>
    public virtual void Initialize(NameValueCollection parameters)
    {
    }

    /// <summary>
    /// Executes the health check and returns the aggregated result.
    /// </summary>
    /// <returns>The health check result.</returns>
    protected abstract Task<HealthCheckResult> RunCore();
  }
}
