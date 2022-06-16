// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Model.EmptyMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.OpenAccess.Metadata.Fluent;

namespace Telerik.Sitefinity.Model
{
  public class EmptyMetadataSource : FluentMetadataSource
  {
    protected override IList<MappingConfiguration> PrepareMapping() => (IList<MappingConfiguration>) new List<MappingConfiguration>();
  }
}
