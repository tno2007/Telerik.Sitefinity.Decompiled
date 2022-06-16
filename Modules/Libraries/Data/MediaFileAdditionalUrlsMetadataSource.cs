// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.MediaFileAdditionalUrlsMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Reflection;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.OpenAccessMapping;

namespace Telerik.Sitefinity.Modules.Libraries
{
  internal class MediaFileAdditionalUrlsMetadataSource : SitefinityMetadataSourceBase
  {
    public MediaFileAdditionalUrlsMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    public override Assembly[] Assemblies => new Assembly[1]
    {
      this.GetType().Assembly
    };

    public override DynamicTypeInfo[] DynamicTypes => (DynamicTypeInfo[]) null;

    protected override bool SupportDynamicTypes => false;

    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings() => (IList<IOpenAccessFluentMapping>) new List<IOpenAccessFluentMapping>()
    {
      (IOpenAccessFluentMapping) new MediaFileAdditionalUrlFluentMapping(this.Context)
    };
  }
}
