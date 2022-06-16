// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.IOpenAccessCustomContextProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>
  /// Defines properties and methods for OpenAccess based data providers which provides custom open access context.
  /// </summary>
  public interface IOpenAccessCustomContextProvider
  {
    /// <summary>Gets the Open Access Context context.</summary>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="backendConfiguration">The backend configuration.</param>
    /// <param name="metadataContainer">The metadata container.</param>
    /// <returns></returns>
    SitefinityOAContext GetContext(
      string connectionString,
      BackendConfiguration backendConfiguration,
      MetadataContainer metadataContainer);
  }
}
