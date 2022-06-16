﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.ContentLinksMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.ContentLinks
{
  /// <summary>This class implements the content links mappings</summary>
  public class ContentLinksMetadataSource : SitefinityMetadataSourceBase
  {
    public ContentLinksMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings() => (IList<IOpenAccessFluentMapping>) new List<IOpenAccessFluentMapping>()
    {
      (IOpenAccessFluentMapping) new ContentLinksFluentMapping(this.Context)
    };

    protected override bool SupportDynamicTypes => false;
  }
}
