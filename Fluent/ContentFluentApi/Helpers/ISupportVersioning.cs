// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.ISupportVersioning`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Provides a common interface for facades that support versioning
  /// </summary>
  /// <typeparam name="TParentFacade">Type of the facade which implements this interface</typeparam>
  public interface ISupportVersioning<TParentFacade> where TParentFacade : BaseFacade
  {
    /// <summary>Manage this item's versions</summary>
    /// <returns>Versioning facade</returns>
    VersioningFacade<TParentFacade> Versioning();
  }
}
