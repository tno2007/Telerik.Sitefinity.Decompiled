// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.Exceptions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Web;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;
using Telerik.Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Telerik.Sitefinity.Abstractions.Formatters;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>Represents helper class for exception handling.</summary>
  public static class Exceptions
  {
    private static ExceptionManager manager;

    /// <summary>
    /// Raised before Sitefinity default exception handling configuration starts.
    /// </summary>
    public static event EventHandler<ExceptionHandlingConfiguringEventArgs> Configuring;

    /// <summary>
    /// Raised after Sitefinity default exception handling configuration is completed.
    /// </summary>
    public static event EventHandler<ExceptionHandlingConfiguredEventArgs> Configured;

    /// <summary>
    /// Handles the specified <see cref="T:System.Exception" />
    /// object according to the rules configured for <paramref name="policy" />.
    /// </summary>
    /// <param name="exceptionToHandle">An <see cref="T:System.Exception" /> object.</param>
    /// <param name="policy">Specifies the policy to handle.</param>
    /// <returns>Whether or not a rethrow is recommended.</returns>
    /// <example>
    /// The following code shows the usage of the
    /// exception handling framework.
    /// <code>
    /// try
    /// {
    /// 	Foo();
    /// }
    /// catch (Exception e)
    /// {
    /// 	if (Exceptions.HandleException(e, ExceptionPolicy.Global)) throw;
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="M:Telerik.Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionManager.Process(System.Action,System.String)" />
    public static bool HandleException(Exception exceptionToHandle, ExceptionPolicyName policy) => Exceptions.HandleException(exceptionToHandle, policy, out Exception _);

    /// <summary>
    /// Handles the specified <see cref="T:System.Exception" />
    /// object according to the rules configured for <paramref name="policy" />.
    /// </summary>
    /// <param name="exceptionToHandle">An <see cref="T:System.Exception" /> object.</param>
    /// <param name="policy">Specifies the policy to handle.</param>
    /// <param name="exceptionToThrow">The new <see cref="T:System.Exception" /> to throw, if any.</param>
    /// <remarks>
    /// If a rethrow is recommended and <paramref name="exceptionToThrow" /> is <see langword="null" />,
    /// then the original exception <paramref name="exceptionToHandle" /> should be rethrown; otherwise,
    /// the exception returned in <paramref name="exceptionToThrow" /> should be thrown.
    /// </remarks>
    /// <returns>Whether or not a rethrow is recommended.</returns>
    /// <example>
    /// The following code shows the usage of the
    /// exception handling framework.
    /// <code>
    /// try
    /// {
    /// 	Foo();
    /// }
    /// catch (Exception e)
    /// {
    ///     Exception exceptionToThrow;
    /// 	if (Exceptions.HandleException(e, Policies.Global, out exceptionToThrow))
    /// 	{
    /// 	  if(exceptionToThrow == null)
    /// 	    throw;
    /// 	  else
    /// 	    throw exceptionToThrow;
    /// 	}
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="M:Telerik.Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionManager.HandleException(System.Exception,System.String)" />
    public static bool HandleException(
      Exception exceptionToHandle,
      ExceptionPolicyName policy,
      out Exception exceptionToThrow)
    {
      try
      {
        return Exceptions.Manager.HandleException(exceptionToHandle, policy.ToString(), out exceptionToThrow);
      }
      catch
      {
        throw exceptionToHandle;
      }
    }

    /// <summary>
    /// Handles the specified <see cref="T:System.Exception" />
    /// object according to the rules configured for <paramref name="policy" />.
    /// </summary>
    /// <param name="exceptionToHandle">An <see cref="T:System.Exception" /> object.</param>
    /// <param name="policyName">Specifies the policy to handle by its name.</param>
    /// <returns>Whether or not a rethrow is recommended.</returns>
    /// <example>
    /// The following code shows the usage of the
    /// exception handling framework.
    /// <code>
    /// try
    /// {
    /// 	Foo();
    /// }
    /// catch (Exception e)
    /// {
    /// 	if (Exceptions.HandleException(e, ExceptionPolicy.Global)) throw;
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="M:Telerik.Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionManager.Process(System.Action,System.String)" />
    public static bool HandleException(Exception exceptionToHandle, string policyName) => Exceptions.HandleException(exceptionToHandle, policyName, out Exception _);

    /// <summary>
    /// Handles the specified <see cref="T:System.Exception" />
    /// object according to the rules configured for <paramref name="policy" />.
    /// </summary>
    /// <param name="exceptionToHandle">An <see cref="T:System.Exception" /> object.</param>
    /// <param name="policyName">Specifies the policy to handle by its name.</param>
    /// <param name="exceptionToThrow">The new <see cref="T:System.Exception" /> to throw, if any.</param>
    /// <remarks>
    /// If a rethrow is recommended and <paramref name="exceptionToThrow" /> is <see langword="null" />,
    /// then the original exception <paramref name="exceptionToHandle" /> should be rethrown; otherwise,
    /// the exception returned in <paramref name="exceptionToThrow" /> should be thrown.
    /// </remarks>
    /// <returns>Whether or not a rethrow is recommended.</returns>
    /// <example>
    /// The following code shows the usage of the
    /// exception handling framework.
    /// <code>
    /// try
    /// {
    /// 	Foo();
    /// }
    /// catch (Exception e)
    /// {
    ///     Exception exceptionToThrow;
    /// 	if (Exceptions.HandleException(e, "MyCustomPolicy", out exceptionToThrow))
    /// 	{
    /// 	  if(exceptionToThrow == null)
    /// 	    throw;
    /// 	  else
    /// 	    throw exceptionToThrow;
    /// 	}
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="M:Telerik.Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionManager.HandleException(System.Exception,System.String)" />
    public static bool HandleException(
      Exception exceptionToHandle,
      string policyName,
      out Exception exceptionToThrow)
    {
      try
      {
        return Exceptions.Manager.HandleException(exceptionToHandle, policyName, out exceptionToThrow);
      }
      catch
      {
        throw exceptionToHandle;
      }
    }

    /// <summary>
    /// Executes the supplied delegate <paramref name="action" /> and handles
    /// any thrown exception according to the rules configured for <paramref name="policy" />.
    /// </summary>
    /// <param name="action">The delegate to execute.</param>
    /// <param name="policy">Specifies the policy to handle.</param>
    /// <example>
    /// The following code shows the usage of this method.
    /// <code>
    /// 	Exceptions.Process(() =&gt; { Foo(); }, Policies.Global);
    /// </code>
    /// </example>
    /// <seealso cref="M:Telerik.Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionManager.HandleException(System.Exception,System.String)" />
    public static void Process(Action action, ConfigurationPolicy policy) => Exceptions.Manager.Process(action, policy.ToString());

    /// <summary>
    /// Executes the supplied delegate <paramref name="action" /> and handles
    /// any thrown exception according to the rules configured for <paramref name="policy" />.
    /// </summary>
    /// <param name="action">The delegate to execute.</param>
    /// <param name="policyName">Specifies the policy to handle by a string.</param>
    /// <example>
    /// The following code shows the usage of this method.
    /// <code>
    /// 	Exceptions.Process(() =&gt; { Foo(); }, "MyCustomPolicy");
    /// </code>
    /// </example>
    /// <seealso cref="M:Telerik.Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionManager.HandleException(System.Exception,System.String)" />
    public static void Process(Action action, string policyName) => Exceptions.Manager.Process(action, policyName);

    internal static void Configure(ConfigurationSourceBuilder configBuilder)
    {
      IExceptionConfigurationGivenPolicyWithName excepConfig = configBuilder.ConfigureExceptionHandling();
      ExceptionHandlingConfiguringEventArgs configuringEventArgs = new ExceptionHandlingConfiguringEventArgs();
      configuringEventArgs.ConfigurationBuilder = (IConfigurationSourceBuilder) configBuilder;
      configuringEventArgs.Configuration = excepConfig;
      ExceptionHandlingConfiguringEventArgs e1 = configuringEventArgs;
      if (Exceptions.Configuring != null)
        Exceptions.Configuring((object) null, e1);
      if (!e1.Cancel)
        Exceptions.ConfigureExceptionHandlingDefaults(excepConfig);
      if (Exceptions.Configured == null)
        return;
      ExceptionHandlingConfiguredEventArgs configuredEventArgs = new ExceptionHandlingConfiguredEventArgs();
      configuredEventArgs.ConfigurationBuilder = (IConfigurationSourceBuilder) configBuilder;
      configuredEventArgs.Configuration = excepConfig;
      ExceptionHandlingConfiguredEventArgs e2 = configuredEventArgs;
      Exceptions.Configured((object) null, e2);
    }

    private static void ConfigureExceptionHandlingDefaults(
      IExceptionConfigurationGivenPolicyWithName excepConfig)
    {
      Type enumType1 = typeof (ExceptionPolicyName);
      Type enumType2 = typeof (ConfigurationPolicy);
      string name1 = Enum.GetName(enumType2, (object) ConfigurationPolicy.ErrorLog);
      string name2 = Enum.GetName(enumType1, (object) ExceptionPolicyName.IgnoreExceptions);
      excepConfig.GivenPolicyWithName(name2).ForExceptionType<Exception>().LogToCategory(Enum.GetName(enumType2, (object) ConfigurationPolicy.ErrorLog)).UsingExceptionFormatter<TextWebExceptionFormatter>().WithSeverity(TraceEventType.Warning).UsingEventId(9010).ThenDoNothing();
      string name3 = Enum.GetName(enumType1, (object) ExceptionPolicyName.UnhandledExceptions);
      excepConfig.GivenPolicyWithName(name3).ForExceptionType<Exception>().LogToCategory(name1).UsingExceptionFormatter<TextWebExceptionFormatter>().WithSeverity(TraceEventType.Error).UsingEventId(90000).ThenNotifyRethrow();
      string name4 = Enum.GetName(enumType1, (object) ExceptionPolicyName.Global);
      excepConfig.GivenPolicyWithName(name4).ForExceptionType<Exception>().LogToCategory(name1).UsingExceptionFormatter<TextWebExceptionFormatter>().WithSeverity(TraceEventType.Error).UsingEventId(90020).ThenNotifyRethrow();
      string name5 = Enum.GetName(enumType1, (object) ExceptionPolicyName.DataProviders);
      excepConfig.GivenPolicyWithName(name5).ForExceptionType<Exception>().LogToCategory(name1).UsingExceptionFormatter<TextWebExceptionFormatter>().WithSeverity(TraceEventType.Error).UsingEventId(90030).ThenNotifyRethrow();
      string name6 = Enum.GetName(enumType1, (object) ExceptionPolicyName.Http404);
      excepConfig.GivenPolicyWithName(name6).ForExceptionType<HttpException>().ThenNotifyRethrow();
      string name7 = Enum.GetName(enumType1, (object) ExceptionPolicyName.Migration);
      excepConfig.GivenPolicyWithName(name7).ForExceptionType<Exception>().LogToCategory(Enum.GetName(enumType2, (object) ConfigurationPolicy.Migration)).UsingExceptionFormatter<TextWebExceptionFormatter>().WithSeverity(TraceEventType.Error).UsingEventId(90050).ThenNotifyRethrow();
      string name8 = Enum.GetName(enumType1, (object) ExceptionPolicyName.ModuleBuilder);
      excepConfig.GivenPolicyWithName(name8).ForExceptionType<Exception>().LogToCategory(name1).UsingExceptionFormatter<TextWebExceptionFormatter>().WithSeverity(TraceEventType.Error).UsingEventId(90060).ThenNotifyRethrow();
    }

    private static ExceptionManager Manager
    {
      get
      {
        if (Exceptions.manager == null)
          Exceptions.manager = ObjectFactory.Resolve<ExceptionManager>();
        return Exceptions.manager;
      }
    }
  }
}
