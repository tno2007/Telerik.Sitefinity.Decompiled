// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Implementation.VersionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Fluent.AnyContent.Implementation
{
  public class VersionFacade<TParentFacade> : IVersionFacade<TParentFacade> where TParentFacade : class
  {
    public bool ReturnSuccess() => throw new NotImplementedException();

    public bool SaveChanges() => throw new NotImplementedException();

    public bool CancelChanges() => throw new NotImplementedException();

    public IVersionFacade<TParentFacade> SaveAndContinue() => throw new NotImplementedException();

    public IVersionFacade<TParentFacade> CancelAndContinue() => throw new NotImplementedException();

    public TParentFacade Done() => throw new NotImplementedException();

    public TParentFacade CreateNewVersion(VersionType versionType) => throw new NotImplementedException();

    public TParentFacade Delete() => throw new NotImplementedException();

    public TParentFacade Delete(int versionNumber) => throw new NotImplementedException();

    public TParentFacade Delete(Func<Change, bool> filter) => throw new NotImplementedException();

    public IList<Change> Get() => throw new NotImplementedException();

    public TParentFacade RevertTo(int versionNumber) => throw new NotImplementedException();

    public TParentFacade RevertTo(Func<IQueryable<Change>, Change> filter) => throw new NotImplementedException();

    public TParentFacade TryRevertTo(int versionNumber) => throw new NotImplementedException();

    public TParentFacade TryRevertTo(int versionNumber, out bool result) => throw new NotImplementedException();

    public TParentFacade TryRevertTo(Func<IQueryable<Change>, Change> filter) => throw new NotImplementedException();

    public TParentFacade TryRevertTo(Func<IQueryable<Change>, Change> filter, out bool result) => throw new NotImplementedException();
  }
}
