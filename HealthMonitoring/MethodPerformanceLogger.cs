// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.MethodPerformanceLogger
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.HealthMonitoring
{
  internal abstract class MethodPerformanceLogger : IMethodPerformanceLogger
  {
    public const string ContextKeyPrefix = "sf_method_";
    private static readonly TimeSpan LogTimеТhreshold;
    private static volatile IMethodPerformanceLogger logger;
    private static object syncLock = new object();
    private readonly string contextKey;
    private readonly string stackKey;

    static MethodPerformanceLogger()
    {
      string s = ConfigurationManager.AppSettings.Get("sf:methodPerformanceLogThresholdInMiliseconds");
      MethodPerformanceLogger.LogTimеТhreshold = TimeSpan.FromMilliseconds(!string.IsNullOrEmpty(s) ? (double) int.Parse(s) : 500.0);
    }

    public MethodPerformanceLogger()
    {
      this.contextKey = "sf_method_" + "log_" + this.GetType().FullName;
      this.stackKey = "sf_method_" + "stack_" + this.GetType().FullName;
    }

    internal static IMethodPerformanceLogger Current
    {
      get
      {
        if (MethodPerformanceLogger.logger == null)
        {
          lock (MethodPerformanceLogger.syncLock)
          {
            if (MethodPerformanceLogger.logger == null)
            {
              string str = ConfigurationManager.AppSettings.Get("sf:methodPerformanceLoggingPath");
              if (!string.IsNullOrEmpty(str))
              {
                if (str.StartsWith("~/"))
                  str = HostingEnvironment.MapPath(str);
                MethodPerformanceLogger.logger = (IMethodPerformanceLogger) new FileSystemMethodPerformanceLogger(str);
              }
              else
              {
                MethodPerformanceLogger.logger = (IMethodPerformanceLogger) new InMemoryMethodPerformanceLogger();
                Bootstrapper.Bootstrapped += new EventHandler<EventArgs>(MethodPerformanceLogger.Bootstrapper_Bootstrapped);
              }
            }
          }
        }
        return MethodPerformanceLogger.logger;
      }
      set => MethodPerformanceLogger.logger = value;
    }

    protected virtual IList<MethodExecutionTime> Log
    {
      get
      {
        string contextkKey = this.GetContextkKey();
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        IList<MethodExecutionTime> data = currentHttpContext == null ? CallContext.GetData(contextkKey) as IList<MethodExecutionTime> : currentHttpContext.Items[(object) contextkKey] as IList<MethodExecutionTime>;
        if (data == null)
        {
          data = (IList<MethodExecutionTime>) new List<MethodExecutionTime>();
          if (currentHttpContext != null)
            currentHttpContext.Items[(object) contextkKey] = (object) data;
          else
            CallContext.SetData(contextkKey, (object) data);
        }
        return data;
      }
      set
      {
        string contextkKey = this.GetContextkKey();
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null)
          currentHttpContext.Items[(object) contextkKey] = (object) value;
        else
          CallContext.SetData(contextkKey, (object) value);
      }
    }

    protected virtual System.Collections.Generic.Stack<MethodExecutionTime> Stack
    {
      get
      {
        string stackKey = this.GetStackKey();
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        System.Collections.Generic.Stack<MethodExecutionTime> data = currentHttpContext == null ? CallContext.GetData(stackKey) as System.Collections.Generic.Stack<MethodExecutionTime> : currentHttpContext.Items[(object) stackKey] as System.Collections.Generic.Stack<MethodExecutionTime>;
        if (data == null)
        {
          data = new System.Collections.Generic.Stack<MethodExecutionTime>();
          if (currentHttpContext != null)
            currentHttpContext.Items[(object) stackKey] = (object) data;
          else
            CallContext.SetData(stackKey, (object) data);
        }
        return data;
      }
      set
      {
        string stackKey = this.GetStackKey();
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null)
          currentHttpContext.Items[(object) stackKey] = (object) value;
        else
          CallContext.SetData(stackKey, (object) value);
      }
    }

    /// <summary>Persists the data.</summary>
    public abstract void PersistData();

    /// <summary>Clears the data.</summary>
    public void ClearData()
    {
      this.Log = (IList<MethodExecutionTime>) null;
      this.Stack = (System.Collections.Generic.Stack<MethodExecutionTime>) null;
    }

    /// <summary>Starts measuring labeled with the specified key.</summary>
    /// <param name="key">The key.</param>
    public void Start(string key) => this.Start(key, (string) null, (IDictionary<string, object>) null);

    /// <summary>Starts measuring labeled with the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <param name="category">The category of the performance sample.</param>
    public void Start(string key, string category) => this.Start(key, category, (IDictionary<string, object>) null);

    /// <summary>Starts measuring labeled with the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <param name="category">The category of the performance sample.</param>
    /// <param name="additionalData">Additional data to be recorded together with method performance metrics.</param>
    public virtual void Start(
      string key,
      string category,
      IDictionary<string, object> additionalData)
    {
      System.Collections.Generic.Stack<MethodExecutionTime> stack = this.Stack;
      if (stack == null)
        return;
      MethodExecutionTime methodExecutionTime = new MethodExecutionTime(key);
      methodExecutionTime.Category = category;
      methodExecutionTime.AdditionalData = additionalData;
      if (stack.Count > 0)
        stack.Peek().Children.Add(methodExecutionTime);
      stack.Push(methodExecutionTime);
    }

    /// <summary>Stops measuring the last operation in the stack.</summary>
    public virtual void Stop()
    {
      System.Collections.Generic.Stack<MethodExecutionTime> stack = this.Stack;
      if (stack == null || stack.Count <= 0)
        return;
      MethodExecutionTime methodExecutionTime = stack.Pop();
      methodExecutionTime.EndTime = DateTime.UtcNow;
      if (stack.Count != 0)
        return;
      this.Log.Add(methodExecutionTime);
      if (!(methodExecutionTime.ExecutionTime > MethodPerformanceLogger.LogTimеТhreshold))
        return;
      this.PersistData();
    }

    /// <summary>Gets the current frames.</summary>
    /// <returns>The current frames.</returns>
    public System.Collections.Generic.Stack<MethodExecutionTime> GetCurrentFrames() => new System.Collections.Generic.Stack<MethodExecutionTime>(this.Stack.Reverse<MethodExecutionTime>());

    private static void Bootstrapper_Bootstrapped(object sender, EventArgs e)
    {
      Bootstrapper.Bootstrapped -= new EventHandler<EventArgs>(MethodPerformanceLogger.Bootstrapper_Bootstrapped);
      if (!(MethodPerformanceLogger.Current is InMemoryMethodPerformanceLogger))
        return;
      MethodPerformanceLogger.Current.ClearData();
      MethodPerformanceLogger.Current = (IMethodPerformanceLogger) new DummyMethodPerfomanceLogger();
    }

    private string GetStackKey() => this.stackKey + (object) Thread.CurrentThread.ManagedThreadId;

    private string GetContextkKey() => this.contextKey + (object) Thread.CurrentThread.ManagedThreadId;
  }
}
