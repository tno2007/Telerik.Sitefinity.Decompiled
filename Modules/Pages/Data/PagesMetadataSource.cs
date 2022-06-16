// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Data.PagesMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.OpenAccessMapping;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.Data
{
  /// <summary>
  /// This class implements the Pages module OpenAccess ORM mappings
  /// </summary>
  public class PagesMetadataSource : ContentBaseMetadataSource
  {
    public PagesMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
    {
      IList<IOpenAccessFluentMapping> accessFluentMappingList = base.BuildCustomMappings();
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new ObjectDataFluentMapping(this.Context));
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new SiteItemLinksMapping(this.Context));
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new PagesFluentMapping(this.Context));
      return accessFluentMappingList;
    }
  }
}
