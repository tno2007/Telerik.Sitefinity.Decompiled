// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.RoleProviderPair
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>
  /// A simple helper class to pair a role with its provider item
  /// </summary>
  public class RoleProviderPair
  {
    /// <summary>A role object</summary>
    public Role RoleItem;
    /// <summary>A role provider object</summary>
    public RoleDataProvider ProviderItem;

    /// <summary>Constructor</summary>
    /// <param name="roleItem">A role object</param>
    /// <param name="providerItem">A role provider object</param>
    public RoleProviderPair(Role roleItem, RoleDataProvider providerItem)
    {
      this.RoleItem = roleItem;
      this.ProviderItem = providerItem;
    }
  }
}
