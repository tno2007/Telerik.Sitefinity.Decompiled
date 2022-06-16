// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.MetadataOAContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data.OA;

namespace Telerik.Sitefinity.Data.Metadata
{
  public class MetadataOAContext : SitefinityOAContext
  {
    public MetadataOAContext(
      string connectionString,
      BackendConfiguration backendConfiguration,
      MetadataContainer metadataContainer)
      : base(connectionString, backendConfiguration, metadataContainer)
    {
    }

    public override void Commit() => base.Commit();
  }
}
