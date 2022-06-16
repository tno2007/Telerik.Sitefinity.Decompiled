// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.RouteRegistration
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Routing;

namespace Telerik.Sitefinity.Abstractions
{
  internal class RouteRegistration
  {
    internal RouteRegistration(
      string routeName,
      RouteBase route,
      string moduleName,
      bool requireBasicAuthentication)
    {
      this.RouteName = routeName;
      this.Route = route;
      this.ModuleName = moduleName;
      this.RequireBasicAuthentication = requireBasicAuthentication;
    }

    internal string RouteName { get; private set; }

    internal RouteBase Route { get; private set; }

    internal string ModuleName { get; private set; }

    internal bool RequireBasicAuthentication { get; private set; }
  }
}
