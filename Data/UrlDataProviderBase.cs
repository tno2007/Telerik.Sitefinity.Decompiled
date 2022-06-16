// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.UrlDataProviderBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Represents base class for Generic Content data providers.
  /// </summary>
  [UrlProviderDecorator(typeof (OpenAccessUrlProviderDecorator))]
  public abstract class UrlDataProviderBase : DataProviderBase, IUrlProvider
  {
    private string urlFormat;
    private string urlRegEx;
    private IUrlProviderDecorator urlDecorator;

    /// <summary>Gets or sets the URL regular expression.</summary>
    /// <value>The URL regular expression.</value>
    protected string UrlRegEx
    {
      get => this.urlRegEx;
      set => this.urlRegEx = value;
    }

    /// <summary>
    /// Gets the url format for the specified data item that implements <see cref="T:Telerik.Sitefinity.GenericContent.Model.ILocatable" /> interface.
    /// </summary>
    /// <param name="item">The locatable item for which the url format should be returned.</param>
    /// <returns>Regular expression used to format the url.</returns>
    public virtual string GetUrlFormat(ILocatable item) => this.urlFormat;

    /// <summary>
    /// Gets the URL of the content item for the current UI culture.
    /// </summary>
    /// <param name="item">The content item.</param>
    /// <returns>The URL for the item.</returns>
    public virtual string GetItemUrl(ILocatable item) => this.GetItemUrl(item, SystemManager.CurrentContext.Culture);

    /// <summary>
    /// Gets the URL of the content item for the specified culture.
    /// </summary>
    /// <param name="item">The content item.</param>
    /// <param name="culture">The culture to retrieve the URL for.</param>
    /// <returns>The URL for the item.</returns>
    public virtual string GetItemUrl(ILocatable item, CultureInfo culture)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (item is ILocatableExtended locatableExtended)
      {
        string itemUrl = locatableExtended.ItemDefaultUrl.GetString(culture, true);
        if (!string.IsNullOrEmpty(itemUrl))
          return itemUrl;
      }
      return this.GetItemUrlInternal(item, culture);
    }

    internal string GetItemUrlInternal(ILocatable item, CultureInfo culture)
    {
      Guid id = item.Id;
      Type urlTypeFor = this.GetUrlTypeFor(item.GetType());
      int lcid1 = CultureInfo.InvariantCulture.LCID;
      List<int> intList = new List<int>();
      for (CultureInfo culture1 = culture; culture1 != null; culture1 = culture1.Parent)
      {
        int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(culture1);
        if (!intList.Contains(cultureLcid))
        {
          intList.Add(cultureLcid);
          if (culture1.Equals((object) CultureInfo.InvariantCulture))
            break;
        }
        else
          break;
      }
      int[] culturesArray = intList.ToArray();
      List<UrlData> list = this.GetUrls(urlTypeFor).Where<UrlData>((Expression<Func<UrlData, bool>>) (u => u.Parent.Id == id && u.IsDefault == true)).ToList<UrlData>().Where<UrlData>((Func<UrlData, bool>) (r => ((IEnumerable<int>) culturesArray).Contains<int>(r.Culture))).ToList<UrlData>();
      if (list.Count > 0)
      {
        foreach (int num in intList)
        {
          int lcid = num;
          UrlData urlData = list.FirstOrDefault<UrlData>((Func<UrlData, bool>) (u => u.Culture == lcid));
          if (urlData != null)
            return urlData.Url;
        }
      }
      return string.Empty;
    }

    /// <summary>Gets default url or throws exception.</summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The url.</returns>
    protected virtual string GetDefaultUrlOrThrowException(ILocatable item, CultureInfo culture)
    {
      Content cnt = this is ContentDataProviderBase && item is Content ? item as Content : throw new InvalidDataException("The item does not have default url.");
      ContentDataProviderBase dataProviderBase = this as ContentDataProviderBase;
      Content contentMasterBase = dataProviderBase.GetContentMasterBase(cnt);
      if (cnt.Status == ContentLifecycleStatus.Live && cnt.Visible)
      {
        Content liveContentBase = dataProviderBase.GetLiveContentBase(contentMasterBase);
        IQueryable<UrlData> source;
        if (liveContentBase == null)
        {
          source = item.Urls.AsQueryable<UrlData>();
        }
        else
        {
          string appName = liveContentBase.ApplicationName;
          Guid id = liveContentBase.Id;
          source = this.GetUrls(this.GetUrlTypeFor(cnt.GetType())).Where<UrlData>((Expression<Func<UrlData, bool>>) (urlData => urlData.Parent.Id == id && urlData.Parent.ApplicationName == appName));
        }
        UrlData urlData1 = source.FirstOrDefault<UrlData>();
        if (urlData1 != null)
          return urlData1.Url;
      }
    }

    /// <summary>Recompiles the URLs of the item.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The content item.</param>
    /// <remarks>
    /// Adds UrlData to the URLs field of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.ILocatable" /> compiled from the item's Provider urlFormat.
    /// </remarks>
    public virtual void RecompileItemUrls<T>(T item) where T : ILocatable => this.RecompileItemUrls<T>(item, SystemManager.CurrentContext.Culture);

    public virtual void RecompileItemUrls<T>(T item, CultureInfo culture) where T : ILocatable => this.AddItemUrlInternal<T>(item, new Func<T, CultureInfo, string>(this.CompileItemUrl<T>), culture: culture);

    /// <summary>
    /// Adds an <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> item to the current URLs collection for this item.
    /// </summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="isDefault">Whether the url is default.</param>
    /// <param name="redirectToDefault">Whether the url should redirect to default.</param>
    public virtual void AddItemUrl<T>(T item, string url, bool isDefault = true, bool redirectToDefault = false) where T : ILocatable
    {
      Func<T, CultureInfo, string> generateUrlFunction = (Func<T, CultureInfo, string>) ((locatableItem, culture) => !url.StartsWith("~/") && !url.StartsWith("/") ? "/" + url : url);
      this.AddItemUrlInternal<T>(item, generateUrlFunction, isDefault, redirectToDefault);
    }

    /// <summary>Clears the URLs collection for the specified item.</summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="excludeDefault">if set to <c>true</c> default URLs will not be cleared.</param>
    public virtual void ClearItemUrls<TItem>(TItem item, bool excludeDefault = false) where TItem : ILocatable => this.RemoveItemUrls<TItem>(item, (Func<UrlData, bool>) (u => !excludeDefault || !u.IsDefault));

    /// <summary>
    /// Removes all URLs from the item satisfying the condition that is checked in the predicate function.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    public virtual void RemoveItemUrls<T>(T item, Func<UrlData, bool> predicate) where T : ILocatable
    {
      List<UrlData> list = item.Urls.Where<UrlData>(predicate).ToList<UrlData>();
      item.RemoveUrls(predicate);
      foreach (UrlData urlData in list)
        this.Delete(urlData);
    }

    /// <summary>Recompiles children urls</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="contentContainer">The content container.</param>
    protected virtual void RecompileChildrenUrls<T>(IHasContentChildren contentContainer) where T : ILocatable
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      IDictionary items = SystemManager.CurrentHttpContext.Items;
      if (!items.Contains((object) "RecompiledAdditionalItemsTypeKey"))
        items.Add((object) "RecompiledAdditionalItemsTypeKey", (object) new List<string>()
        {
          typeof (T).FullName
        });
      foreach (Content childContentItem in contentContainer.ChildContentItems)
        this.RecompileChildItemUrl<T>(childContentItem as ILocatable, culture);
    }

    /// <summary>Recompiles the URLs of all successors of the item.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The hierarchical item.</param>
    /// <remarks>
    /// Adds UrlData to the URLs field of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.ILocatable" /> compiled from the item's Provider urlFormat.
    /// </remarks>
    public virtual void RecompileChildrenUrlsHierarchically<T>(IHierarchicalItem item) where T : ILocatable
    {
      if (item.Children == null)
        return;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      foreach (IHierarchicalItem child in (IEnumerable<IHierarchicalItem>) item.Children)
      {
        if (!(child is IRecyclableDataItem recyclableDataItem) || !recyclableDataItem.IsDeleted)
        {
          this.RecompileChildItemUrl<T>(child as ILocatable, culture);
          this.RecompileChildrenUrlsHierarchically<T>(child);
        }
      }
    }

    /// <summary>Compiles the item URL for the current UI culture.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The item.</param>
    /// <returns>The compiled URL string.</returns>
    public virtual string CompileItemUrl<T>(T item) where T : ILocatable => this.CompileItemUrl<T>(item, SystemManager.CurrentContext.Culture);

    /// <summary>Compiles the item URL for the specified UI culture.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The compiled URL string.</returns>
    public virtual string CompileItemUrl<T>(T item, CultureInfo culture) where T : ILocatable => this.CompileItemUrl<T>(item, culture, this.GetUrlFormat((ILocatable) item));

    /// <summary>Compiles the item URL for the specified UI culture.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="urlFormat">Format of the url</param>
    /// <returns>The compiled URL string.</returns>
    protected internal virtual string CompileItemUrl<T>(
      T item,
      CultureInfo culture,
      string urlFormat)
      where T : ILocatable
    {
      if ((object) item == null)
        throw new ArgumentNullException(nameof (item));
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      MatchEvaluator evaluator = (MatchEvaluator) (m => this.EvaluateUrlPart<T>(m, item, culture));
      return Regex.Replace(urlFormat, this.urlRegEx, evaluator).Replace("//", "/");
    }

    /// <summary>Gets the first item that matches the specified URL.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="url">The URL to match.</param>
    /// <param name="redirectUrl">The URL to redirect to if there is newer URL.</param>
    /// <returns>The content item that matches the URL.</returns>
    public virtual T GetItemFromUrl<T>(string url, out string redirectUrl) where T : ILocatable => (T) this.GetItemFromUrl(typeof (T), url, out redirectUrl);

    /// <summary>Gets the first item that matches the specified URL.</summary>
    /// <param name="itemType">Type of the content item.</param>
    /// <param name="url">The URL to match.</param>
    /// <param name="redirectUrl">The URL to redirect to if there is newer URL.</param>
    /// <returns>The content item that matches the URL.</returns>
    public virtual IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      out string redirectUrl)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (string.IsNullOrEmpty(url))
        throw new ArgumentNullException(nameof (url));
      int num = CultureInfo.InvariantCulture.LCID;
      if (SystemManager.CurrentContext.AppSettings.Multilingual && !SystemManager.CurrentContext.Culture.Equals((object) SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage))
        num = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
      List<UrlData> list = this.GetUrls(this.GetUrlTypeFor(itemType)).Where<UrlData>((Expression<Func<UrlData, bool>>) (u => u.Url == url)).ToList<UrlData>();
      bool flag = list.Any<UrlData>((Func<UrlData, bool>) (u => u.Parent != null && u.Parent.GetType() == itemType));
      UrlData urlData1 = (UrlData) null;
      foreach (UrlData urlData2 in list)
      {
        if (!(urlData2.Parent is Content parent2))
        {
          if (flag)
          {
            if (urlData2.Parent != null)
            {
              if (urlData2.Parent is ILifecycleDataItem parent1 && parent1.Status == ContentLifecycleStatus.Master && urlData2.Parent.GetType() == itemType)
                urlData1 = urlData2;
              else if (urlData2.Parent.GetType() == itemType)
                urlData1 = urlData2;
              else
                continue;
            }
            else
              continue;
          }
          else
            urlData1 = urlData2;
        }
        else if (parent2.Status == ContentLifecycleStatus.Master)
          urlData1 = urlData2;
        else
          continue;
        if (urlData1 != null)
        {
          if (urlData1.Culture == num)
            break;
        }
      }
      if (urlData1 != null)
      {
        IDataItem parent = urlData1.Parent;
        redirectUrl = !urlData1.RedirectToDefault ? (string) null : this.GetItemUrl((ILocatable) parent, Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(urlData1.Culture));
        if (parent != null)
          parent.Provider = (object) this;
        return parent;
      }
      redirectUrl = (string) null;
      return (IDataItem) null;
    }

    /// <summary>
    /// Retrieve a content item by its url, optionally returning only items that are visible on the public side
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="published">If true, will get only Published/Scheduled items - those that are typically visible on the public side.</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    public virtual IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return this.GetItemFromUrl(itemType, url, published, out redirectUrl, out int _);
    }

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="published">The published.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <param name="resolvedCultureId">The resolved culture id.</param>
    /// <returns>The item.</returns>
    internal IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl,
      out int resolvedCultureId)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (string.IsNullOrEmpty(url))
        throw new ArgumentNullException(nameof (url));
      Type urlTypeFor = this.GetUrlTypeFor(itemType);
      int num1 = CultureInfo.InvariantCulture.LCID;
      if (SystemManager.CurrentContext.AppSettings.Multilingual && !SystemManager.CurrentContext.Culture.Equals((object) SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage))
        num1 = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
      UrlData urlData1 = (UrlData) null;
      if (published)
      {
        UrlData[] array = this.GetUrls(urlTypeFor).Where<UrlData>((Expression<Func<UrlData, bool>>) (u => u.Url == url)).ToArray<UrlData>();
        for (int index = 0; index < array.Length; ++index)
        {
          UrlData urlData2 = array[index];
          if (urlData2.Parent != null && !this.IsDeletedItem(urlData2.Parent))
          {
            if (urlData2.Parent is Content parent && parent.SupportsContentLifecycle)
            {
              if (parent.Status == ContentLifecycleStatus.Live && parent.Visible)
              {
                bool flag;
                if (parent is ILifecycleDataItem)
                {
                  flag = true;
                  if (SystemManager.CurrentContext.AppSettings.Multilingual && !(this is LibrariesDataProvider))
                  {
                    ILifecycleDataItem lifecycleDataItem = (ILifecycleDataItem) parent;
                    if (lifecycleDataItem.PublishedTranslations.Count != 0)
                      flag = lifecycleDataItem.IsPublishedInCulture();
                  }
                }
                else
                {
                  int num2;
                  if (parent.PublicationDate <= DateTime.UtcNow)
                  {
                    DateTime? expirationDate = parent.ExpirationDate;
                    if (expirationDate.HasValue)
                    {
                      expirationDate = parent.ExpirationDate;
                      DateTime utcNow = DateTime.UtcNow;
                      num2 = expirationDate.HasValue ? (expirationDate.GetValueOrDefault() > utcNow ? 1 : 0) : 0;
                    }
                    else
                      num2 = 1;
                  }
                  else
                    num2 = 0;
                  flag = num2 != 0;
                }
                if (flag)
                  urlData1 = array[index];
              }
              else
                continue;
            }
            else
              urlData1 = array[index];
            if (urlData1 != null && urlData1.Culture == num1 && this.MatchUrlData(urlData1, itemType))
              break;
          }
        }
      }
      else
      {
        UrlData[] array = this.GetUrls(urlTypeFor).Where<UrlData>((Expression<Func<UrlData, bool>>) (u => u.Url == url)).ToArray<UrlData>();
        for (int index = 0; index < array.Length; ++index)
        {
          UrlData urlData3 = array[index];
          if (urlData3.Parent == null || !this.IsDeletedItem(urlData3.Parent))
          {
            urlData1 = array[index];
            if (urlData1 != null && urlData1.Culture == num1 && this.MatchUrlData(urlData1, itemType))
              break;
          }
        }
      }
      if (urlData1 != null)
      {
        IDataItem itemFromUrl = urlData1.Parent;
        if (!published && itemFromUrl is Content cnt && cnt.SupportsContentLifecycle && cnt.OriginalContentId != Guid.Empty && cnt.Status != ContentLifecycleStatus.Master)
          itemFromUrl = (IDataItem) (this as ContentDataProviderBase).GetContentMasterBase(cnt);
        redirectUrl = !urlData1.RedirectToDefault ? (string) null : this.GetItemUrl((ILocatable) itemFromUrl, Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(urlData1.Culture));
        if (itemFromUrl != null)
          itemFromUrl.Provider = (object) this;
        resolvedCultureId = urlData1.Culture;
        return itemFromUrl;
      }
      redirectUrl = (string) null;
      resolvedCultureId = 0;
      return (IDataItem) null;
    }

    protected virtual bool MatchUrlData(UrlData urlData, Type itemType) => true;

    /// <summary>
    /// Retrieve a content item by its url, optionally returning only items that are visible on the public side
    /// </summary>
    /// <typeparam name="T">Type of the item to get</typeparam>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="published">If true, will get only Published/Scheduled items - those that are typically visible on the public side.</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    public virtual IDataItem GetItemFromUrl<T>(
      string url,
      bool published,
      out string redirectUrl)
      where T : IDataItem, ILocatable
    {
      return this.GetItemFromUrl(typeof (T), url, published, out redirectUrl);
    }

    /// <summary>Evaluates a URL part for a content item.</summary>
    /// <typeparam name="T">The type of the content.</typeparam>
    /// <param name="match">The matched group.</param>
    /// <param name="item">The content item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The URL part.</returns>
    protected internal virtual string EvaluateUrlPart<T>(Match match, T item, CultureInfo culture) where T : ILocatable
    {
      string str1 = match.ToString();
      string str2 = match.Groups[1].ToString();
      string empty = string.Empty;
      if (!str1.StartsWith("["))
        return string.Empty;
      int length = str2.IndexOf(",");
      string key;
      string format;
      if (length != -1)
      {
        key = str2.Substring(0, length).Trim();
        format = str2.Substring(length + 1).Trim();
      }
      else
      {
        key = str2.Trim();
        format = (string) null;
      }
      CultureInfo culture1 = SystemManager.CurrentContext.Culture;
      try
      {
        SystemManager.CurrentContext.Culture = culture;
        return this.GetUrlPart<T>(key, format, item);
      }
      finally
      {
        SystemManager.CurrentContext.Culture = culture1;
      }
    }

    /// <summary>Gets the URL part.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="key">The key.</param>
    /// <param name="format">The format.</param>
    /// <param name="item">The item.</param>
    /// <returns>The URL part.</returns>
    protected internal virtual string GetUrlPart<T>(string key, string format, T item) where T : ILocatable => DataBinder.Eval((object) item, key, format);

    /// <summary>
    /// Gets the actual type of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> implementation for the specified content type.
    /// </summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <returns>The URL type.</returns>
    public virtual Type GetUrlTypeFor<TContent>() where TContent : IDataItem => this.GetUrlTypeFor(typeof (TContent));

    /// <summary>
    /// Gets the actual type of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> implementation for the specified content type.
    /// </summary>
    /// <param name="itemType">Type of the content item.</param>
    /// <returns>The URL type.</returns>
    public abstract Type GetUrlTypeFor(Type itemType);

    /// <summary>Creates new UrlData.</summary>
    /// <param name="urlType">The CLR type of the URL data object.</param>
    /// <returns>The new UrlData object.</returns>
    public virtual UrlData CreateUrl(Type urlType) => this.urlDecorator != null ? this.urlDecorator.CreateUrl(urlType) : throw new MissingDecoratorException((DataProviderBase) this, "CreateUrl<T>()");

    /// <summary>Creates new UrlData with the specified ID.</summary>
    /// <param name="urlType">The CLR type of the URL data object.</param>
    /// <param name="id">The pageId of the new UrlData.</param>
    /// <returns>The new UrlData object.</returns>
    public virtual UrlData CreateUrl(Type urlType, Guid id)
    {
      if (this.urlDecorator == null)
        throw new MissingDecoratorException((DataProviderBase) this, "CreateUrl<T>()");
      return this.urlDecorator.CreateUrl(urlType, id);
    }

    /// <summary>Creates new UrlData.</summary>
    /// <typeparam name="T">Type of the URL.</typeparam>
    /// <returns>The new UrlData object.</returns>
    public virtual T CreateUrl<T>() where T : UrlData, new() => this.urlDecorator != null ? this.urlDecorator.CreateUrl<T>() : throw new MissingDecoratorException((DataProviderBase) this, "CreateUrl<T>()");

    /// <summary>Creates new UrlData with the specified ID.</summary>
    /// <typeparam name="T">Type of the URL.</typeparam>
    /// <param name="id">The ID of the new UrlData.</param>
    /// <returns>The new UrlData object.</returns>
    public virtual T CreateUrl<T>(Guid id) where T : UrlData, new() => this.urlDecorator != null ? this.urlDecorator.CreateUrl<T>(id) : throw new MissingDecoratorException((DataProviderBase) this, "CreateUrl<T>(Guid id)");

    /// <summary>Gets a UrlData with the specified ID.</summary>
    /// <typeparam name="T">Type of the URL.</typeparam>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A UrlData entry.</returns>
    public virtual T GetUrl<T>(Guid id) where T : UrlData => this.urlDecorator != null ? this.urlDecorator.GetUrl<T>(id) : throw new MissingDecoratorException((DataProviderBase) this, "GetUrl<T>(Guid id)");

    /// <summary>Gets a query for UrlData.</summary>
    /// <typeparam name="T">Type of the URL.</typeparam>
    /// <returns>The query for UrlData.</returns>
    public virtual IQueryable<T> GetUrls<T>() where T : UrlData => this.urlDecorator != null ? this.urlDecorator.GetUrls<T>() : throw new MissingDecoratorException((DataProviderBase) this, "GetUrls<T>()");

    /// <summary>Gets a query for UrlData.</summary>
    /// <param name="urlType">The URL type.</param>
    /// <returns>The query for UrlData.</returns>
    public virtual IQueryable<UrlData> GetUrls(Type urlType) => this.urlDecorator != null ? this.urlDecorator.GetUrls(urlType) : throw new MissingDecoratorException((DataProviderBase) this, "GetUrls(Type urlType)");

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The UrlData to delete.</param>
    public virtual void Delete(UrlData item)
    {
      if (this.urlDecorator == null)
        throw new MissingDecoratorException((DataProviderBase) this, "Delete(UrlData item)");
      this.urlDecorator.Delete(item);
    }

    /// <summary>
    /// Adds an <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> object to the item URLs collection using the supplied function for url generation.
    /// </summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="generateUrlFunction">The function that will generate the new item url.</param>
    /// <param name="isDefault">Whether the URL is default.</param>
    /// <param name="redirectToDefault">Whether the URL should redirect to default.</param>
    private void AddItemUrlInternal<T>(
      T item,
      Func<T, CultureInfo, string> generateUrlFunction,
      bool isDefault = true,
      bool redirectToDefault = false,
      CultureInfo culture = null)
      where T : ILocatable
    {
      if ((object) item == null)
        throw new ArgumentNullException(nameof (item));
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      Lstring urlName = item.UrlName;
      if (urlName != (Lstring) null && urlName.GetString(culture, false).IsNullOrEmpty() && (object) item is IHasTitle)
      {
        string title = ((IHasTitle) (object) item).GetTitle();
        if (!title.IsNullOrEmpty())
          urlName.SetString(culture, CommonMethods.TitleToUrl(title));
      }
      IList<UrlData> previousDefaultUrls;
      this.MakeItemUrlsNonDefault<T>(item, isDefault, culture, out previousDefaultUrls);
      string sUrl = generateUrlFunction(item, culture);
      int cultureLCID = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(culture);
      UrlData url = item.Urls.FirstOrDefault<UrlData>((Func<UrlData, bool>) (u => u.Culture == cultureLCID && u.Url.Equals(sUrl, StringComparison.CurrentCultureIgnoreCase)));
      if (url == null)
        this.CreateUrl<T>(item, sUrl, cultureLCID, isDefault, redirectToDefault);
      else
        this.ConfigureUrl(url, isDefault, redirectToDefault);
      this.RemoveNonLiveUrls((ILocatable) item, previousDefaultUrls, culture);
    }

    private void EnsureDefaultUrl<T>(
      T item,
      CultureInfo cult,
      string sUrl,
      bool isDefault,
      bool redirectToDefault)
      where T : ILocatable
    {
      UrlData url = item.Urls.SingleOrDefault<UrlData>((Func<UrlData, bool>) (u => u.Culture == CultureInfo.InvariantCulture.LCID && u.Url.ToUpper(cult) == sUrl.ToUpper(cult)));
      if (url == null)
        this.CreateUrl<T>(item, sUrl, CultureInfo.InvariantCulture.LCID, isDefault, redirectToDefault);
      else
        this.ConfigureUrl(url, isDefault, redirectToDefault);
    }

    private void UpdateUrlInSpecificCulture<T>(
      T item,
      CultureInfo cult,
      string sUrl,
      bool isDefault,
      bool redirectToDefault)
      where T : ILocatable
    {
      int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(cult);
      UrlData url = item.Urls.SingleOrDefault<UrlData>((Func<UrlData, bool>) (u => u.Url.ToUpper(cult) == sUrl.ToUpper(cult) && u.Culture == cultureLcid));
      if (url == null)
        this.CreateUrl<T>(item, sUrl, cultureLcid, isDefault, redirectToDefault);
      else
        this.ConfigureUrl(url, isDefault, redirectToDefault);
    }

    private void MakeItemUrlsNonDefault<T>(
      T item,
      bool isDefault,
      CultureInfo cult,
      out IList<UrlData> previousDefaultUrls)
      where T : ILocatable
    {
      previousDefaultUrls = (IList<UrlData>) new List<UrlData>();
      if (!isDefault)
        return;
      UrlData urlData = item.Urls.Where<UrlData>((Func<UrlData, bool>) (u => !u.IsDefault)).FirstOrDefault<UrlData>();
      bool flag = true;
      if (urlData != null)
        flag = urlData.RedirectToDefault;
      int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(cult);
      foreach (UrlData url in item.Urls)
      {
        if (url.Culture == cultureLcid)
        {
          if (url.IsDefault)
            previousDefaultUrls.Add(url);
          url.IsDefault = false;
          url.RedirectToDefault = flag;
        }
      }
    }

    /// <summary>
    /// Determines whether the given IDataItem has been deleted.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <returns>True if the item has been deleted. Otherwise false.</returns>
    protected bool IsDeletedItem(IDataItem dataItem) => dataItem is IRecyclableDataItem recyclableDataItem && recyclableDataItem.IsDeleted;

    private void ConfigureUrl(UrlData url, bool isDefault, bool redirectoToDefault)
    {
      url.IsDefault = isDefault;
      url.RedirectToDefault = redirectoToDefault;
      if (!isDefault || !(url.Parent is ILocatableExtended parent))
        return;
      parent.ItemDefaultUrl = (Lstring) url.Url;
    }

    internal void RecompileChildItemUrl<T>(ILocatable childItem, CultureInfo currentCutlure) where T : ILocatable
    {
      if (!(childItem is T obj))
        return;
      this.RecompileItemUrls<T>(obj);
      int currentCultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(currentCutlure);
      foreach (int cultureId in obj.Urls.Where<UrlData>((Func<UrlData, bool>) (url => url.IsDefault && url.Culture != currentCultureLcid)).Select<UrlData, int>((Func<UrlData, int>) (url => url.Culture)).ToList<int>().Distinct<int>())
      {
        using (new CultureRegion(cultureId))
          this.RecompileItemUrls<T>(obj);
      }
    }

    internal virtual UrlData CreateUrl<T>(
      T item,
      string url,
      int culture,
      bool isDefault = true,
      bool redirectoToDefault = false)
      where T : ILocatable
    {
      UrlData url1 = this.CreateUrl(this.GetUrlTypeFor(item.GetType()));
      url1.Url = url;
      url1.Culture = culture;
      url1.Parent = (IDataItem) item;
      url1.IsDefault = isDefault;
      url1.RedirectToDefault = redirectoToDefault;
      ILocatableExtended locatableExtended = (object) item as ILocatableExtended;
      if (!isDefault || locatableExtended == null)
        return url1;
      CultureInfo cultureByLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(culture);
      locatableExtended.ItemDefaultUrl[cultureByLcid] = url;
      return url1;
    }

    private void RemoveNonLiveUrls(
      ILocatable item,
      IList<UrlData> previousDefaultUrls,
      CultureInfo culture)
    {
      List<UrlData> list = previousDefaultUrls.Where<UrlData>((Func<UrlData, bool>) (u => !u.IsDefault)).ToList<UrlData>();
      if (list.Count == 0 || !(item is ILifecycleDataItemGeneric lifecycleDataItemGeneric1))
        return;
      ILifecycleDataItemGeneric lifecycleDataItemGeneric2 = lifecycleDataItemGeneric1.Status == ContentLifecycleStatus.Live ? lifecycleDataItemGeneric1 : this.GetLive(lifecycleDataItemGeneric1);
      ILocatable locatable = lifecycleDataItemGeneric2 as ILocatable;
      bool flag1 = lifecycleDataItemGeneric2 != null;
      bool flag2 = lifecycleDataItemGeneric2 != null && culture != null && (lifecycleDataItemGeneric2.PublishedTranslations.Any<string>((Func<string, bool>) (x => x == culture.GetLanguageKey())) || lifecycleDataItemGeneric2.PublishedTranslations.Count == 0);
      foreach (UrlData urlData in list)
      {
        UrlData url = urlData;
        if ((!flag2 ? 1 : (locatable == null ? 0 : (!locatable.Urls.Any<UrlData>((Func<UrlData, bool>) (u => u.Url == url.Url && u.Culture == url.Culture)) ? 1 : 0))) != 0)
          this.Delete(url);
      }
    }

    private ILifecycleDataItemGeneric GetLive(
      ILifecycleDataItemGeneric item)
    {
      if (item.Status == ContentLifecycleStatus.Live)
        return (ILifecycleDataItemGeneric) null;
      Guid guid;
      switch (item.Status)
      {
        case ContentLifecycleStatus.Master:
          guid = item.Id;
          break;
        case ContentLifecycleStatus.Temp:
        case ContentLifecycleStatus.PartialTemp:
          guid = item.OriginalContentId;
          break;
        default:
          throw new NotSupportedException("Status {0} is not supported.".Arrange((object) item.Status));
      }
      string filterExpression = string.Format("OriginalContentId = {0} && Status = Live", (object) guid);
      return (this.GetItems(item.GetType(), filterExpression, string.Empty, 0, 0) as IEnumerable<ILifecycleDataItemGeneric>).FirstOrDefault<ILifecycleDataItemGeneric>();
    }

    /// <summary>Initializes the specified provider name.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="config">The config.</param>
    /// <param name="managerType">Type of the manager.</param>
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      base.Initialize(providerName, config, managerType);
      this.urlFormat = config["urlFormat"];
      if (string.IsNullOrEmpty(this.urlFormat))
        this.urlFormat = "/[PublicationDate, {0:yyyy'/'MM'/'dd}]/[UrlName]";
      config.Remove("urlFormat");
      this.urlRegEx = config["urlRegEx"];
      if (string.IsNullOrEmpty(this.urlRegEx))
        this.urlRegEx = "\\[([^\\]]*)]|{([^}]*)}";
      config.Remove("urlRegEx");
      UrlProviderDecoratorAttribute attribute = (UrlProviderDecoratorAttribute) TypeDescriptor.GetAttributes((object) this)[typeof (UrlProviderDecoratorAttribute)];
      if (attribute == null)
        return;
      this.urlDecorator = ObjectFactory.Resolve<IUrlProviderDecorator>(attribute.DecoratorType.FullName);
      this.urlDecorator.DataProvider = this;
    }

    public override object Clone()
    {
      UrlDataProviderBase dataProviderBase = (UrlDataProviderBase) base.Clone();
      if (this.urlDecorator != null)
      {
        dataProviderBase.urlDecorator = (IUrlProviderDecorator) this.urlDecorator.Clone();
        dataProviderBase.urlDecorator.DataProvider = dataProviderBase;
      }
      return (object) dataProviderBase;
    }
  }
}
