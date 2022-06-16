// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Telerik.Sitefinity.Fluent.AnyContent.InitialPrototype.IAnyVersioningFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Fluent.AnyContent.Telerik.Sitefinity.Fluent.AnyContent.InitialPrototype
{
  internal interface IAnyVersioningFacade : IAnyBaseFacade
  {
    IAnyParentFacade CreateNewVersion(VersionType versionType);

    IAnyParentFacade Delete();

    IAnyParentFacade Delete(int versionNumber);

    IAnyParentFacade Delete(Func<Change, bool> filter);

    IList<Change> Get();

    IAnyParentFacade RevertTo(int versionNumber);

    IAnyParentFacade RevertTo(Func<IQueryable<Change>, Change> filter);

    IAnyParentFacade TryRevertTo(int versionNumber);

    IAnyParentFacade TryRevertTo(int versionNumber, out bool result);

    IAnyParentFacade TryRevertTo(Func<IQueryable<Change>, Change> filter);

    IAnyParentFacade TryRevertTo(
      Func<IQueryable<Change>, Change> filter,
      out bool result);
  }
}
