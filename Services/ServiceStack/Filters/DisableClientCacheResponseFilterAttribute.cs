// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.Filters.DisableClientCacheResponseFilterAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Web;
using System.Collections.Generic;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.ServiceStack.Filters
{
  internal class DisableClientCacheResponseFilterAttribute : ResponseFilterAttribute
  {
    public override void Execute(IRequest req, IResponse res, object responseDto)
    {
      foreach (KeyValuePair<string, string> cacheHeader in (IEnumerable<KeyValuePair<string, string>>) ServiceUtility.GetCacheHeaders())
        res.AddHeader(cacheHeader.Key, cacheHeader.Value);
    }
  }
}
