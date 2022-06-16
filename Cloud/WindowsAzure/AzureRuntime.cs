// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Cloud.WindowsAzure
{
  /// <summary>
  /// Abstracts out parts of the Windows Azure service runtime. A special care is taken to avoid referencing
  /// Microsoft.WindowsAzure.* assemblies, when <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsInstalled" /> is <c>false</c>,
  /// which might cause errors loading them.
  /// </summary>
  /// <remarks>
  /// <para>
  /// The .NET runtime loads referenced assemblies not earlier than entering a method, that reference types
  /// from these assemblies, which allows <see cref="T:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime" /> do its trick - to not invoke any
  /// Windows Azure specific API, thus avoiding the loading of any of the <c>Microsoft.WindowsAzure.*</c> assemblies,
  /// when not on Windows Azure (or the Compute Emulator).</para>
  /// <para>
  /// Note to implementers: To keep this behavior when adding publicly visible properties or methods, always GUARD the code
  /// with <c>if (!IsRunning) return ...;</c> and always keep the Windows Azure specific code in a SEPARATE method,
  /// which is called only when actually running on Windows Azure.
  /// </para>
  /// <para>
  /// Some additional information about delayed loading of assemblies can be found here: http://innovatian.com/2009/06/delay-loading-net-assemblies/
  /// </para>
  /// </remarks>
  public static class AzureRuntime
  {
    /// <summary>
    /// The name of Sitefinity's internal endpoint, used for multi-instance synchronization.
    /// <c>SitefinityWebApp</c> must also be bound to this endpoint.
    /// </summary>
    public const string InternalEndpointName = "SitefinityInternalEndpoint";
    /// <summary>
    /// The name of Sitefinity's public endpoint. Used only when no input endpoint on port 80 is found.
    /// </summary>
    public const string InputEndpointName = "WebEndpoint";
    /// <summary>
    /// The name of Sitefinity's local storage, used for libraries temporary storage.
    /// </summary>
    public const string LibrariesTempLocalStorageName = "LibrariesTemp";
    private static bool? isInstalled;

    /// <summary>
    /// Indicates whether the current Siteifnity instance is running in Windows Azure environment
    /// (including the Compute Emulator).
    /// </summary>
    /// <remarks>
    /// A web application may be running in such environment as a normal web application (instead of as a web role),
    /// in which case <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsInstalled" /> would be <c>true</c>, but <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsRunning" /> would be <c>false</c>. Because of this
    /// always use <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsRunning" /> to control the execution of Windows Azure specific code!
    /// </remarks>
    public static bool IsInstalled
    {
      get
      {
        if (!AzureRuntime.isInstalled.HasValue)
        {
          SitefinityEnvironmentElement environment = Config.SectionHandler.Environment;
          AzureRuntime.isInstalled = new bool?(environment.Platform == SitefinityEnvironmentPlatform.WindowsAzure || environment.Platform == SitefinityEnvironmentPlatform.WindowsAzureWebSite);
        }
        return AzureRuntime.isInstalled.Value;
      }
    }

    /// <summary>
    /// Indicates whether the current Sitefinity instance is running AS A WEB ROLE in Windows Azure environment
    /// (including the Compute Emulator).
    /// </summary>
    /// <remarks>
    /// A web application may be running in such environment as a normal web application (instead of as a web role),
    /// in which case <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsInstalled" /> would be <c>true</c>, but <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsRunning" /> would be <c>false</c>. Because of this
    /// always use <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsRunning" /> to control the execution of Windows Azure specific code!
    /// </remarks>
    public static bool IsRunning
    {
      get
      {
        if (AzureRuntime.IsRunningMock.HasValue)
          return AzureRuntime.IsRunningMock.Value;
        return AzureRuntime.IsInstalled && AzureRuntime.GetRoleEnvironmentIsAvailable();
      }
    }

    /// <summary>
    /// The number of web role instances running; <c>0</c>, when not on Windows Azure.
    /// </summary>
    public static int InstanceCount
    {
      get
      {
        if (AzureRuntime.InstanceCountMock.HasValue)
          return AzureRuntime.InstanceCountMock.Value;
        return !AzureRuntime.IsRunning ? 0 : AzureRuntime.GetInstanceCount();
      }
    }

    /// <summary>
    /// The current role instance; <c>null</c>, when not on Windows Azure.
    /// </summary>
    public static RoleInstance CurrentRoleInstance => !AzureRuntime.IsRunning ? (RoleInstance) null : AzureRuntime.GetCurrentRoleInstance();

    /// <summary>
    /// The base URI (protocol, host and port) of Sitefinity's internal endpoint of the current instance.
    /// It is named after the value of <see cref="F:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.InternalEndpointName" />.
    /// <c>null</c>, when not on Windows Azure.
    /// </summary>
    public static Uri CurrentRoleInternalEndpointUri => !AzureRuntime.IsRunning ? (Uri) null : AzureRuntime.GetCurrentRoleInternalEndpointUri();

    /// <summary>
    /// The base URI (protocol, host and port) of Sitefinity's public (input) endpoint of the current instance.
    /// It is defined as the first endpoint bound to port 80 or, if none, the one named after the value of <see cref="F:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.InputEndpointName" />.
    /// <c>null</c>, when not on Windows Azure.
    /// </summary>
    public static Uri CurrentRolePublicEndpointUri => !AzureRuntime.IsRunning ? (Uri) null : AzureRuntime.GetCurrentRolePublicEndpointUri();

    /// <summary>
    /// The path of the local storage folder used by Sitefinity's libraries (named after the value of <see cref="F:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.LibrariesTempLocalStorageName" />);
    /// <c>null</c>, when not on Windows Azure.
    /// </summary>
    public static string LibrariesTempFolderPath => !AzureRuntime.IsRunning ? (string) null : AzureRuntime.GetLocalStoragePath("LibrariesTemp");

    /// <summary>
    /// This is a workaround for Windows Azure 1.3 Integrated Pipeline, as it sets the internal HttpContext.HideRequestResponse property to true
    /// while Sitefinity configuration initialization is using a JavaScriptSerializer, which relies on the current Reponse object.
    /// This causes the following exception to be thrown during startup: "System.Web.HttpException: Request is not available in this context"
    /// For more information about this issue and the workaround go to http://social.msdn.microsoft.com/Forums/en-US/windowsazure/thread/b7ba3eb3-f73e-4485-9dd9-453229927d3f/
    /// </summary>
    internal static void RevealHttpRequestResponse()
    {
      FieldInfo field = HttpContext.Current.GetType().GetField("HideRequestResponse", BindingFlags.Instance | BindingFlags.NonPublic);
      if (!(field != (FieldInfo) null))
        return;
      field.SetValue((object) HttpContext.Current, (object) false);
    }

    /// <summary>
    /// When non-<c>null</c>, <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsRunning" /> returns the value set.
    /// </summary>
    internal static bool? IsRunningMock { get; set; }

    /// <summary>
    /// When non-<c>null</c>, <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.InstanceCount" /> returns the value set.
    /// </summary>
    internal static int? InstanceCountMock { get; set; }

    /// <summary>
    /// Executes <paramref name="action" />, with <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsRunning" /> temporarily set to <paramref name="isRunning" />
    /// and <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.InstanceCount" /> set to <paramref name="instanceCount" />.
    /// </summary>
    /// <param name="isRunning">The value teporariliy set to <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.IsRunning" /></param>
    /// <param name="instanceCount">The value teporariliy set to <see cref="P:Telerik.Sitefinity.Cloud.WindowsAzure.AzureRuntime.InstanceCount" />, only if <paramref name="isRunning" /> is <c>true</c>.</param>
    /// <param name="action">An <see cref="T:System.Action" /> to be executed in the mocked environment.</param>
    internal static void Mock(bool isRunning, int? instanceCount, Action action)
    {
      AzureRuntime.IsRunningMock = new bool?(isRunning);
      AzureRuntime.InstanceCountMock = isRunning ? instanceCount : new int?();
      try
      {
        action();
      }
      finally
      {
        AzureRuntime.IsRunningMock = new bool?();
        AzureRuntime.InstanceCountMock = new int?();
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static bool GetRoleEnvironmentIsAvailable() => RoleEnvironment.IsAvailable;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static RoleInstance GetCurrentRoleInstance() => RoleEnvironment.CurrentRoleInstance;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int GetInstanceCount() => RoleEnvironment.CurrentRoleInstance.Role.Instances.Count;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static Uri GetCurrentRoleInternalEndpointUri()
    {
      RoleInstanceEndpoint instanceEndpoint;
      return !RoleEnvironment.CurrentRoleInstance.InstanceEndpoints.TryGetValue("SitefinityInternalEndpoint", out instanceEndpoint) ? (Uri) null : new Uri("http://" + (object) instanceEndpoint.IPEndpoint);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static Uri GetCurrentRolePublicEndpointUri()
    {
      RoleInstanceEndpoint instanceEndpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints.Values.FirstOrDefault<RoleInstanceEndpoint>((Func<RoleInstanceEndpoint, bool>) (e => e.IPEndpoint.Port == 80));
      return instanceEndpoint == null && !RoleEnvironment.CurrentRoleInstance.InstanceEndpoints.TryGetValue("WebEndpoint", out instanceEndpoint) ? (Uri) null : new Uri("http://" + (object) instanceEndpoint.IPEndpoint);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static string GetLocalStoragePath(string storageName)
    {
      try
      {
        return RoleEnvironment.GetLocalResource(storageName).RootPath;
      }
      catch (RoleEnvironmentException ex)
      {
        Exception exceptionToHandle = new Exception("Unable to get local storage '{0}': {1}.".Arrange((object) storageName, (object) ex.Message), (Exception) ex);
        if (Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
          throw exceptionToHandle;
        return (string) null;
      }
    }
  }
}
