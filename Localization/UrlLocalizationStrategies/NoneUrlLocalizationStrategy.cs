// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.NoneUrlLocalizationStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Threading;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  internal class NoneUrlLocalizationStrategy : IUrlLocalizationStrategy
  {
    public void Initialize(IUrlLocalizationContext context)
    {
    }

    public string UnResolveUrl(string url, out CultureInfo culture, out CultureInfo uiCulture)
    {
      culture = Thread.CurrentThread.CurrentCulture;
      uiCulture = SystemManager.CurrentContext.Culture;
      return url;
    }

    public string ResolveUrl(string url) => url;

    public string ResolveUrl(string url, CultureInfo targetCulture) => url;
  }
}
