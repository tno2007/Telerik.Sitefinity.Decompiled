﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.ResponsiveDesignMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.OpenAccessMapping;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  /// <summary>
  /// This class implements the Events module OpenAccess data provider ORM mappings
  /// </summary>
  public class ResponsiveDesignMetadataSource : SitefinityMetadataSourceBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:BlogsMetadataSource" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public ResponsiveDesignMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings() => (IList<IOpenAccessFluentMapping>) new List<IOpenAccessFluentMapping>()
    {
      (IOpenAccessFluentMapping) new ResponsiveDesignFluentMapping(this.Context)
    };
  }
}
