// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.CultureHelpers
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization
{
  internal class CultureHelpers
  {
    internal static CultureInfo GetRequestCultureOrDefault()
    {
      string culture = CultureHelpers.GetCultureNameFromRequest();
      return string.IsNullOrEmpty(culture) ? SystemManager.CurrentContext.Culture : ((IEnumerable<CultureInfo>) CultureInfo.GetCultures(CultureTypes.AllCultures)).FirstOrDefault<CultureInfo>((Func<CultureInfo, bool>) (x => x.Name == culture)) ?? SystemManager.CurrentContext.Culture;
    }

    internal static string GetCultureNameFromRequest()
    {
      NameValueCollection nameValueCollection = SystemManager.CurrentHttpContext.Request.Params;
      string[] strArray = new string[4]
      {
        "language",
        "lang",
        "sf_culture",
        "culture"
      };
      foreach (string name in strArray)
      {
        if (nameValueCollection[name] != null)
          return nameValueCollection[name];
      }
      return string.Empty;
    }
  }
}
