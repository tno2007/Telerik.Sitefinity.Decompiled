// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IOpenAccessMetadataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Defines properties and methods for OpenAccess based data providers.
  /// </summary>
  public interface IOpenAccessMetadataProvider
  {
    /// <summary>Gets the meta data source.</summary>
    /// <returns></returns>
    MetadataSource GetMetaDataSource(IDatabaseMappingContext context);

    /// <summary>
    /// Gets the name of the module which uniquely identifies MetadataSource provided to the connection.
    /// If the module name is null or empty, the current type will be used as a key.
    /// </summary>
    /// <value>The name of the module.</value>
    string ModuleName { get; }
  }
}
