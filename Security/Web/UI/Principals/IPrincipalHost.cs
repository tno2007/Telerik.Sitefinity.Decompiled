// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.IPrincipalHost
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>
  /// Interface exposes methods for accessing UserManager of Principal Views (Users, Roles)
  /// </summary>
  public interface IPrincipalHost
  {
    /// <summary>Gets the user manager.</summary>
    /// <value>The manager.</value>
    UserManager Manager { get; }
  }
}
