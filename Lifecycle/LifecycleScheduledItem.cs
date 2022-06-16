// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleScheduledItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Lifecycle
{
  internal class LifecycleScheduledItem : IScheduleable
  {
    public DateTime? ExpirationDate { get; set; }

    public DateTime PublicationDate { get; set; }

    public LifecycleScheduledItem(ILifecycleDataItem item, CultureInfo culture)
    {
      LifecycleScheduledItem lifecycleScheduledItem = this;
      CommonMethods.ExecuteMlLogic<ILifecycleDataItem>((Action<ILifecycleDataItem>) (itemInner =>
      {
        LanguageData languageData = itemInner.LanguageData.FirstOrDefault<LanguageData>((Func<LanguageData, bool>) (x => string.IsNullOrEmpty(x.Language)));
        if (languageData == null)
          return;
        lifecycleScheduledItem.Init(languageData.PublicationDate, languageData.ExpirationDate);
      }), (Action<ILifecycleDataItem>) (itemInner =>
      {
        LanguageData languageData = itemInner.LanguageData.FirstOrDefault<LanguageData>((Func<LanguageData, bool>) (x => x.Language == culture.Name)) ?? itemInner.LanguageData.FirstOrDefault<LanguageData>((Func<LanguageData, bool>) (x => string.IsNullOrEmpty(x.Language)));
        if (languageData == null)
          return;
        lifecycleScheduledItem.Init(languageData.PublicationDate, languageData.ExpirationDate);
      }), (Action<ILifecycleDataItem>) (itemInner =>
      {
        LanguageData languageData = itemInner.LanguageData.FirstOrDefault<LanguageData>((Func<LanguageData, bool>) (x => x.Language == culture.Name));
        if (languageData == null)
          return;
        lifecycleScheduledItem.Init(languageData.PublicationDate, languageData.ExpirationDate);
      }), item);
    }

    private void Init(DateTime publicationDate, DateTime? expirationDate)
    {
      this.ExpirationDate = expirationDate;
      this.PublicationDate = publicationDate;
    }
  }
}
