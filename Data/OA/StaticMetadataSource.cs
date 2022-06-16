// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.StaticMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.OpenAccess.Metadata;

namespace Telerik.Sitefinity.Data.OA
{
  internal class StaticMetadataSource : MetadataSource
  {
    private MetadataContainer currentModel;

    public StaticMetadataSource(MetadataSource metadataSource)
      : this(metadataSource.GetModel())
    {
    }

    public StaticMetadataSource(MetadataContainer metadataContainer) => this.currentModel = metadataContainer;

    protected override MetadataContainer CreateModel() => this.currentModel;
  }
}
