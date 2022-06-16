// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.MetadataOpenAccessConnection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data.Configuration;

namespace Telerik.Sitefinity.Data.OA
{
  internal class MetadataOpenAccessConnection : OpenAccessConnection
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.MetadataOpenAccessConnection" /> class.
    /// </summary>
    /// <param name="connectionSettings">The connection settings.</param>
    public MetadataOpenAccessConnection(IConnectionStringSettings connectionSettings)
      : base(connectionSettings)
    {
      this.isMetadataContainer = true;
    }
  }
}
