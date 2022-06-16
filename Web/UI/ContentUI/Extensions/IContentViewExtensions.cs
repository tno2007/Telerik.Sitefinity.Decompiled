// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Extensions.IContentViewExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Extensions
{
  internal static class IContentViewExtensions
  {
    internal static bool TryGetFallbackItem(
      this IContentView contentView,
      Func<ILifecycleDataItem> getItem,
      out ILifecycleDataItem resolvedItem)
    {
      if (SystemManager.CurrentContext.AppSettings.Multilingual && contentView.MasterViewDefinition != null && contentView.MasterViewDefinition.ItemLanguageFallback.HasValue && contentView.MasterViewDefinition.ItemLanguageFallback.Value)
      {
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        CultureInfo frontendLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
        while (true)
        {
          if (culture.Equals((object) CultureInfo.InvariantCulture))
            culture = frontendLanguage;
          if (((IEnumerable<CultureInfo>) AppSettings.CurrentSettings.DefinedFrontendLanguages).Contains<CultureInfo>(culture))
          {
            using (new CultureRegion(culture))
            {
              ILifecycleDataItem lifecycleDataItem = getItem();
              if (lifecycleDataItem != null)
              {
                if (lifecycleDataItem.PublishedTranslations.Contains(culture.Name))
                {
                  resolvedItem = lifecycleDataItem;
                  return true;
                }
              }
            }
          }
          if (!culture.Equals((object) frontendLanguage))
            culture = culture.Parent;
          else
            break;
        }
      }
      resolvedItem = (ILifecycleDataItem) null;
      return false;
    }

    internal static void AdaptMultilingualFilterExpression(this IContentViewMasterDefinition master)
    {
      if (master == null || !SystemManager.CurrentContext.AppSettings.Multilingual)
        return;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      bool? languageFallback = master.ItemLanguageFallback;
      int num;
      if (languageFallback.HasValue)
      {
        languageFallback = master.ItemLanguageFallback;
        num = languageFallback.Value ? 1 : 0;
      }
      else
        num = 0;
      bool withFallback = num != 0;
      master.FilterExpression = ContentHelper.AdaptMultilingualFilterExpressionRaw(master.FilterExpression, culture, withFallback);
    }
  }
}
