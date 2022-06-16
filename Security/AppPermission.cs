// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.AppPermission
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents a helper class for working with application wide security permissions.
  /// </summary>
  public static class AppPermission
  {
    private static readonly ConcurrentProperty<StaticRoot> secRoot = new ConcurrentProperty<StaticRoot>(new Func<StaticRoot>(AppPermission.BuildSecRoot));
    private static Telerik.Sitefinity.Security.Configuration.Permission permission = Config.Get<SecurityConfig>().Permissions["Backend"];
    public static Guid AppPermissionsSubscriptionId = new Guid("f172c567-f8cc-4995-9870-16f0c71140c0");

    public static bool IsGranted(params string[] actions)
    {
      int actions1 = 0;
      foreach (string action in actions)
        actions1 |= AppPermission.permission.Actions[action].Value;
      return AppPermission.IsGranted(actions1);
    }

    /// <summary>
    /// Checks if the required permissions are granted to the current user.
    /// </summary>
    /// <param name="actions">The actions to check.</param>
    /// <returns>
    /// true if the requested actions are permitted to the current user; otherwise, false.
    /// </returns>
    public static bool IsGranted(params AppAction[] actions)
    {
      int actions1 = 0;
      foreach (AppAction action in actions)
        actions1 |= AppPermission.permission.Actions[action.ToString()].Value;
      return AppPermission.IsGranted(actions1);
    }

    /// <summary>
    /// Checks if the required permissions are granted to the current user.
    /// </summary>
    /// <param name="actions">The actions to check.</param>
    /// <returns>
    /// true if the requested actions are permitted to the current user; otherwise, false.
    /// </returns>
    public static bool IsGranted(int actions) => AppPermission.Root.IsGranted("Backend", actions);

    /// <summary>
    /// Throws a security exception if the minimum required permissions are not met .
    /// </summary>
    /// <param name="actions">The actions to assure.</param>
    public static void Demand(params AppAction[] actions)
    {
      int actions1 = 0;
      foreach (AppAction action in actions)
        actions1 |= AppPermission.permission.Actions[action.ToString()].Value;
      AppPermission.Demand(actions1);
    }

    /// <summary>
    /// Throws a security exception if the minimum required permissions are not met .
    /// </summary>
    /// <param name="actions">The actions to assure.</param>
    public static void Demand(int actions) => AppPermission.Root.Demand("Backend", actions);

    /// <summary>Gets the login URL.</summary>
    /// <value>The login URL.</value>
    public static string LoginUrl => AppPermission.permission.LoginUrl;

    /// <summary>Gets the backend permission settings.</summary>
    /// <value>The settings.</value>
    public static Telerik.Sitefinity.Security.Configuration.Permission Settings => AppPermission.permission;

    /// <summary>Gets the security root for the current application.</summary>
    /// <value>The root.</value>
    public static StaticRoot Root => AppPermission.secRoot.Value;

    private static StaticRoot BuildSecRoot()
    {
      CacheDependency.Subscribe(typeof (SecurityRoot), AppPermission.AppPermissionsSubscriptionId.ToString(), AppPermission.Expire);
      bool disableSecurityChecks = ConfigProvider.DisableSecurityChecks;
      ConfigProvider.DisableSecurityChecks = true;
      SecurityManager manager = SecurityManager.GetManager();
      manager.Provider.SuppressSecurityChecks = true;
      SecurityRoot securityRoot = manager.GetSecurityRoot("ApplicationSecurityRoot");
      if (securityRoot == null)
      {
        try
        {
          securityRoot = manager.CreateSecurityRoot("ApplicationSecurityRoot", "Backend");
          manager.SaveChanges();
        }
        catch (DuplicateKeyException ex)
        {
          securityRoot = manager.GetSecurityRoot("ApplicationSecurityRoot");
        }
      }
      manager.Provider.SuppressSecurityChecks = false;
      securityRoot.DataProviderName = manager.Provider.Name;
      securityRoot.ManagerType = manager.GetType();
      StaticRoot staticRoot = new StaticRoot(securityRoot);
      ConfigProvider.DisableSecurityChecks = disableSecurityChecks;
      return staticRoot;
    }

    public static ChangedCallback Expire => new ChangedCallback(AppPermission.OnPermissonChanged);

    private static void OnPermissonChanged(ICacheDependencyHandler handler, Type item, string key)
    {
      if (string.IsNullOrEmpty(key) || !key.Equals(AppPermission.AppPermissionsSubscriptionId.ToString(), StringComparison.OrdinalIgnoreCase))
        return;
      CacheDependency.Unsubscribe(handler.GetType(), typeof (SecurityRoot), key, new ChangedCallback(AppPermission.OnPermissonChanged));
      AppPermission.secRoot.Reset();
    }
  }
}
