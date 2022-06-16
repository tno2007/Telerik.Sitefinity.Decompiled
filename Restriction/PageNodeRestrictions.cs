// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Restriction.PageNodeRestrictions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Restriction
{
  internal class PageNodeRestrictions
  {
    public static bool IsRestricted(Guid pageSiteNodeID)
    {
      foreach (IPageNodeRestrictionStrategy restrictionStrategy in ObjectFactory.Container.ResolveAll<IPageNodeRestrictionStrategy>())
      {
        if (restrictionStrategy.IsRestricted(pageSiteNodeID))
          return true;
      }
      return false;
    }
  }
}
