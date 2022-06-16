// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Data.SiteSettingsMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.SiteSettings.Model;

namespace Telerik.Sitefinity.SiteSettings.Data
{
  internal class SiteSettingsMetadataSource : SitefinityMetadataSourceBase
  {
    public SiteSettingsMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    protected override bool SupportDynamicTypes => false;

    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings() => (IList<IOpenAccessFluentMapping>) new List<IOpenAccessFluentMapping>()
    {
      (IOpenAccessFluentMapping) new SiteSettingsFluentMapping(this.Context)
    };
  }
}
