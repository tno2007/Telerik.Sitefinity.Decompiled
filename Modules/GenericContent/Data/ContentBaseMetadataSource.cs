// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Data.ContentBaseMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.GenericContent.Data
{
  /// <summary>
  /// Base open access metadatasource for Sitefinity Content based data providers
  /// This class providers the basic mappings for the content, security and workflow that need to be present in content based modules
  /// </summary>
  public class ContentBaseMetadataSource : SecuredProviderMetadataSource
  {
    public ContentBaseMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    /// <summary>Builds the custom mappings for the data provider.</summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
    {
      IList<IOpenAccessFluentMapping> accessFluentMappingList = base.BuildCustomMappings();
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new CommonFluentMapping(this.Context));
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new ContentBaseFluentMapping(this.Context));
      return accessFluentMappingList;
    }
  }
}
