// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Data.DynamicContentComparer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Model;

namespace Telerik.Sitefinity.DynamicModules.Data
{
  internal class DynamicContentComparer : IEqualityComparer<DynamicContent>
  {
    public bool Equals(DynamicContent x, DynamicContent y) => x.Id.Equals(y.Id);

    public int GetHashCode(DynamicContent dynamicContent) => dynamicContent.Id.GetHashCode();
  }
}
