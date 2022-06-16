// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RouteManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Claims;

namespace Telerik.Sitefinity.Services
{
  internal static class RouteManager
  {
    private static readonly RouteCollection routes;
    private static readonly List<RouteRegistration> routeRegistrations = new List<RouteRegistration>();
    private static List<RouteRegistration> basicAuthenticationRoutes = new List<RouteRegistration>();
    private static readonly List<IBearerTokenAuthenticationRoute> bearerTokenAuthenticationRoutes = new List<IBearerTokenAuthenticationRoute>();

    public static event EventHandler<ExecutedEventArgs> WebApiRoutesChanged;

    private static void OnWebApiRoutesChanged(object sender, ExecutedEventArgs args)
    {
      if (RouteManager.WebApiRoutesChanged == null)
        return;
      RouteManager.WebApiRoutesChanged(sender, args);
    }

    static RouteManager() => RouteManager.routes = RouteTable.Routes;

    internal static bool RouteExistingFiles
    {
      get => RouteManager.routes.RouteExistingFiles;
      set => RouteManager.routes.RouteExistingFiles = value;
    }

    internal static void RegisterRoute(
      string routeName,
      RouteBase route,
      string moduleName,
      bool supportsBasicAuthentication)
    {
      RouteManager.RegisterRoute(new RouteRegistration(routeName, route, moduleName, supportsBasicAuthentication));
    }

    internal static void RegisterRoute(
      string routeName,
      RouteBase route,
      string moduleName,
      bool supportsBasicAuthentication,
      int index)
    {
      RouteManager.RegisterRoute(new RouteRegistration(routeName, route, moduleName, supportsBasicAuthentication), index);
    }

    internal static void RegisterRoute(RouteRegistration routeRegistration)
    {
      using (RouteManager.routes.GetWriteLock())
      {
        if (RouteManager.routeRegistrations.Any<RouteRegistration>((Func<RouteRegistration, bool>) (r => r.RouteName.Equals(routeRegistration.RouteName))))
          return;
        RouteManager.routes.Add(routeRegistration.RouteName, routeRegistration.Route);
        RouteManager.routeRegistrations.Add(routeRegistration);
        if (!RouteManager.RequireBasicAuthentication(routeRegistration))
          return;
        RouteManager.basicAuthenticationRoutes.Add(routeRegistration);
      }
    }

    internal static void RegisterRoute(RouteRegistration routeRegistration, int index)
    {
      using (RouteManager.routes.GetWriteLock())
      {
        if (RouteManager.routeRegistrations.Any<RouteRegistration>((Func<RouteRegistration, bool>) (r => r.RouteName.Equals(routeRegistration.RouteName))))
          return;
        RouteManager.routes.Insert(index, routeRegistration.Route);
        RouteManager.routeRegistrations.Insert(index, routeRegistration);
        if (!RouteManager.RequireBasicAuthentication(routeRegistration))
          return;
        RouteManager.basicAuthenticationRoutes.Add(routeRegistration);
      }
    }

    internal static void UnregisterRoute(string routeName) => RouteManager.UnregisterRoute(RouteManager.routeRegistrations.Single<RouteRegistration>((Func<RouteRegistration, bool>) (r => r.RouteName.Equals(routeName))));

    internal static void UnregisterRoute(RouteRegistration routeRegistration)
    {
      using (RouteManager.routes.GetWriteLock())
      {
        RouteManager.routes.Remove(routeRegistration.Route);
        RouteManager.routeRegistrations.Remove(routeRegistration);
        if (!RouteManager.RequireBasicAuthentication(routeRegistration))
          return;
        RouteManager.basicAuthenticationRoutes.Remove(routeRegistration);
      }
    }

    internal static void UnregisterRoutes(string moduleName)
    {
      foreach (RouteRegistration routeRegistration in RouteManager.routeRegistrations.Where<RouteRegistration>((Func<RouteRegistration, bool>) (r => r.ModuleName.Equals(moduleName))).ToArray<RouteRegistration>())
        RouteManager.UnregisterRoute(routeRegistration);
    }

    private static bool RequireBasicAuthentication(RouteRegistration routeRegistration) => routeRegistration.RequireBasicAuthentication && routeRegistration.ModuleName != "System" && !typeof (ProtectedRoute).IsAssignableFrom(routeRegistration.Route.GetType());

    internal static void ClearRegisteredRoutes()
    {
      using (RouteManager.routes.GetWriteLock())
      {
        RouteManager.routes.Clear();
        RouteManager.routeRegistrations.Clear();
        RouteManager.bearerTokenAuthenticationRoutes.Clear();
        RouteManager.basicAuthenticationRoutes.Clear();
      }
    }

    internal static List<RouteRegistration> GetRegisteredRoutes() => new List<RouteRegistration>((IEnumerable<RouteRegistration>) RouteManager.routeRegistrations);

    internal static bool RequireBasicAuthentication(HttpContextBase context) => RouteManager.basicAuthenticationRoutes.Any<RouteRegistration>((Func<RouteRegistration, bool>) (r => r.Route.GetRouteData(context) != null));

    internal static int GetIndexOf(string routeName)
    {
      RouteRegistration routeRegistration = RouteManager.routeRegistrations.SingleOrDefault<RouteRegistration>((Func<RouteRegistration, bool>) (r => r.RouteName.Equals(routeName)));
      return routeRegistration != null ? RouteManager.routeRegistrations.IndexOf(routeRegistration) : -1;
    }

    internal static IEnumerable<IBearerTokenAuthenticationRoute> GetBearerTokenAuthenticationRoutes() => (IEnumerable<IBearerTokenAuthenticationRoute>) RouteManager.bearerTokenAuthenticationRoutes;

    internal static void AddBearerTokenAuthenticationRoute(IBearerTokenAuthenticationRoute route) => RouteManager.bearerTokenAuthenticationRoutes.Add(route);

    internal static void InvalidateApiRoutes() => RouteManager.OnWebApiRoutesChanged((object) null, new ExecutedEventArgs(nameof (InvalidateApiRoutes), (object) EventArgs.Empty));
  }
}
