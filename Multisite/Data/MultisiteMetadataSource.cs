// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Data.MultisiteMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Multisite.Model.OpenAccessMapping;

namespace Telerik.Sitefinity.Multisite.Data
{
  public class MultisiteMetadataSource : ContentBaseMetadataSource
  {
    public MultisiteMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
    {
      IList<IOpenAccessFluentMapping> accessFluentMappingList = base.BuildCustomMappings();
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new MultisiteFluentMapping(this.Context));
      return accessFluentMappingList;
    }
  }
}
