// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.IDataItemFacade.LanguageDataManagerDataItemFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Fluent.IDataItemFacade
{
  internal class LanguageDataManagerDataItemFacade : ILanguageDataManagerDataItemFacade
  {
    private ILifecycleDataItem item;
    private LanguageData languageData;

    public LanguageDataManagerDataItemFacade(ILifecycleDataItem item, ILanguageDataManager manager) => this.SetInitialState(item, manager);

    public void SetInitialState(ILifecycleDataItem item, ILanguageDataManager manager)
    {
      FacadeHelper.AssertArgumentNotNull<ILifecycleDataItem>(item, nameof (item));
      FacadeHelper.AssertArgumentNotNull<ILanguageDataManager>(manager, nameof (manager));
      this.item = item;
      this.Manager = manager;
    }

    public ILanguageDataManager Manager { get; set; }

    public ILanguageDataManagerDataItemFacade CreateLanguageData(
      string language)
    {
      if (this.IsMultilingualOn() && !this.item.PublishedTranslations.Contains(language))
      {
        if (this.item.PublishedTranslations.Count == 0 && this.item.Visible)
        {
          string languageKey = LocalizationHelper.GetDefaultLanguageForObject((object) this.item).GetLanguageKey();
          if (!this.item.PublishedTranslations.Contains(languageKey))
            this.item.PublishedTranslations.Add(languageKey);
        }
        if (!string.IsNullOrEmpty(language))
          this.AddPublishedTranslation(language);
      }
      this.languageData = this.Manager.CreateLanguageData();
      if (language != null)
        this.languageData.Language = language;
      this.item.LanguageData.Add(this.languageData);
      return (ILanguageDataManagerDataItemFacade) this;
    }

    /// <summary>
    /// Adds the language to the collection of the published translation of the item,
    /// synchronizing the published translation of split pages when the item is a split page
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="languageKey">The language key.</param>
    public void AddPublishedTranslation(string languageKey)
    {
      if (this.item is PageData pageData && (pageData.NavigationNode == null || pageData.NavigationNode.IsSplitPage))
        return;
      this.item.PublishedTranslations.Add(languageKey);
    }

    public ILanguageDataManagerDataItemFacade CreateLanguageData()
    {
      this.languageData = this.Manager.CreateLanguageData();
      this.item.LanguageData.Add(this.languageData);
      return (ILanguageDataManagerDataItemFacade) this;
    }

    public LanguageData Get(string language = null)
    {
      this.languageData = this.item.LanguageData.Where<LanguageData>((Func<LanguageData, bool>) (lg => lg.Language == language)).FirstOrDefault<LanguageData>();
      return this.languageData;
    }

    protected bool IsMultilingualOn() => SystemManager.CurrentContext.AppSettings.Multilingual;
  }
}
