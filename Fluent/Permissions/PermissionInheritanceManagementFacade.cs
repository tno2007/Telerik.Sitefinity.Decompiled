// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.PermissionInheritanceManagementFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  internal class PermissionInheritanceManagementFacade
  {
    public virtual void BreakInheritance(ISecuredObject securedObject)
    {
      IDataItem dataItem = securedObject != null ? (IDataItem) securedObject : throw new ArgumentNullException("The securedObject for which we break the inheritance cannot be null.");
      if (!securedObject.InheritsPermissions)
        return;
      ((DataProviderBase) dataItem.Provider).BreakPermiossionsInheritance(securedObject);
    }
  }
}
