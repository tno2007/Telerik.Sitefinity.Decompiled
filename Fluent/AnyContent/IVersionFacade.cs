// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.IVersionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Fluent.AnyContent
{
  public interface IVersionFacade<TParentFacade> where TParentFacade : class
  {
    bool ReturnSuccess();

    bool SaveChanges();

    bool CancelChanges();

    IVersionFacade<TParentFacade> SaveAndContinue();

    IVersionFacade<TParentFacade> CancelAndContinue();

    TParentFacade Done();

    TParentFacade CreateNewVersion(VersionType versionType);

    TParentFacade Delete();

    TParentFacade Delete(int versionNumber);

    TParentFacade Delete(Func<Change, bool> filter);

    IList<Change> Get();

    TParentFacade RevertTo(int versionNumber);

    TParentFacade RevertTo(Func<IQueryable<Change>, Change> filter);

    TParentFacade TryRevertTo(int versionNumber);

    TParentFacade TryRevertTo(int versionNumber, out bool result);

    TParentFacade TryRevertTo(Func<IQueryable<Change>, Change> filter);

    TParentFacade TryRevertTo(Func<IQueryable<Change>, Change> filter, out bool result);
  }
}
