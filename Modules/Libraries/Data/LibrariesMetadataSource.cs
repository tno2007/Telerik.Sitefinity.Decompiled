// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Data.LibrariesMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.OpenAccessMapping;

namespace Telerik.Sitefinity.Modules.Libraries.Data
{
  /// <summary>
  /// This class implements the Libraries module OpenAccess ORM mappings
  /// </summary>
  public class LibrariesMetadataSource : ContentBaseMetadataSource
  {
    public LibrariesMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
    {
      IList<IOpenAccessFluentMapping> accessFluentMappingList = base.BuildCustomMappings();
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new LibrariesFluentMapping(this.Context));
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new CounterFluentMapping(this.Context));
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new FolderFluentMapping(this.Context));
      return accessFluentMappingList;
    }
  }
}
