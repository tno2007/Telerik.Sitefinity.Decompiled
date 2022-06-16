// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.CommonMethods
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Workflow.Activities;

namespace Telerik.Sitefinity.Fluent
{
  internal class CommonMethods
  {
    internal const string UrlNameCharsToReplace = "[^\\w\\-\\!\\$\\'\\(\\)\\=\\@\\d_]+";
    internal const string UrlNameReplaceString = "-";
    private static readonly Regex cultureFilterRegex = new Regex("(?:(?:AND|OR)[\\s]+)?Culture[\\s]?==[\\s]?([a-zA-Z]{2,3}(-[a-zA-Z0-9]{1,8}){0,8})", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static Regex taxonFilterRegex = new Regex("TaxonId.(\\w+)==(^?[\\da-f]{8}-([\\da-f]{4}-){3}[\\da-f]{12}?$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static Regex validUrlPartRegex = new Regex(DefinitionsHelper.UrlRegularExpressionDotNetValidator, RegexOptions.Compiled);
    private static Regex andOrFilterRegex = new Regex("^(?:[\\s]*(?:(?:AND|OR)[\\s]+))");

    /// <summary>Recompiles the item urls.</summary>
    /// <param name="item">The item.</param>
    /// <param name="manager">The manager.</param>
    internal static void RecompileItemUrls(Content item, IManager manager)
    {
      if (item is ILocatable && manager is IContentManager)
      {
        IContentManager contentManager = manager as IContentManager;
        ILocatable locatable1 = item as ILocatable;
        if (string.IsNullOrEmpty(locatable1.UrlName.Value))
          locatable1.UrlName = (Lstring) CommonMethods.TitleToUrl((string) item.Title);
        ILocatable locatable2 = locatable1;
        contentManager.RecompileItemUrls<ILocatable>(locatable2);
      }
      CommonMethods.ValidateUrlConstraints<Content>(manager, item);
    }

    /// <summary>Recompiles the item urls.</summary>
    /// <param name="item">The item.</param>
    /// <param name="manager">The manager.</param>
    internal static void RecompileItemUrls(Content item, DataProviderBase provider)
    {
      if (!(item is ILocatable))
        return;
      IContentManager mappedManager = (IContentManager) ManagerBase.GetMappedManager(item.GetType(), provider.Name);
      ILocatable locatable = (ILocatable) item;
      if (string.IsNullOrEmpty(locatable.UrlName.Value))
        locatable.UrlName = (Lstring) CommonMethods.TitleToUrl((string) item.Title);
      mappedManager.RecompileItemUrls<ILocatable>(locatable);
      CommonMethods.ValidateUrlConstraints<Content>((IManager) mappedManager, item);
    }

    /// <summary>
    /// Recompiles the item urls using the UrlName of the content item as default url and urlNames as additional urls.
    /// </summary>
    /// <param name="content">The content item.</param>
    /// <param name="manager">The manager to use.</param>
    /// <param name="additionalUrlNames">The full list additonal URL names that will be added to the content item url data collelction.</param>
    internal static void RecompileItemUrls(
      Content content,
      IManager manager,
      List<string> additionalUrlNames,
      bool additionalUrlsRedirectToDefault,
      bool excludeDefault = true)
    {
      if (!(content is ILocatable) || !(manager is IContentManager))
        return;
      IContentManager contentManager = manager as IContentManager;
      ILocatable locatableItem = content as ILocatable;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        contentManager.RemoveItemUrls<ILocatable>(locatableItem, (Func<UrlData, bool>) (urlData =>
        {
          int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
          bool flag = !urlData.IsDefault;
          if (!excludeDefault)
            flag = true;
          if (urlData.Culture == cultureLcid & flag)
            return true;
          return urlData.Culture == CultureInfo.InvariantCulture.LCID & flag && SystemManager.CurrentContext.Culture.Equals((object) SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage);
        }));
      else
        contentManager.ClearItemUrls<ILocatable>(locatableItem, excludeDefault);
      List<string> stringList = new List<string>();
      foreach (string additionalUrlName in additionalUrlNames)
      {
        string str = CommonMethods.ValidateUrl(additionalUrlName);
        stringList.Add(str);
      }
      stringList.ForEach((Action<string>) (url => contentManager.AddItemUrl<ILocatable>(locatableItem, url, false, additionalUrlsRedirectToDefault)));
      if (string.IsNullOrEmpty(locatableItem.UrlName.Value))
        locatableItem.UrlName = (Lstring) CommonMethods.TitleToUrl((string) content.Title);
      contentManager.RecompileItemUrls<ILocatable>(locatableItem);
      CommonMethods.ValidateUrlConstraints<Content>(manager, content);
    }

    internal static string ValidateUrl(string url)
    {
      string str1 = !url.IsNullOrWhitespace() ? Regex.Replace(url, "/+", "/") : throw new ArgumentException("Empty Urls are not allowed");
      string str2 = str1;
      char[] chArray = new char[1]{ '/' };
      foreach (string input in str2.Split(chArray))
      {
        if (!input.IsNullOrWhitespace() && !CommonMethods.validUrlPartRegex.IsMatch(input))
          throw new ArgumentException(string.Format("The URL: '{0}' is not valid.", (object) str1));
      }
      return str1;
    }

    /// <summary>Matches the culture filter.</summary>
    /// <param name="filter">The filter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    internal static bool MatchCultureFilter(ref string filter, out CultureInfo culture)
    {
      culture = (CultureInfo) null;
      if (string.IsNullOrEmpty(filter))
        return false;
      Match match = CommonMethods.cultureFilterRegex.Match(filter);
      int num = match == null || !match.Groups[0].Success ? 0 : (match.Groups[1].Success ? 1 : 0);
      if (num == 0)
        return num != 0;
      string lowerInvariant = match.Groups[1].ToString().ToLowerInvariant();
      culture = CultureInfo.GetCultureInfo(lowerInvariant);
      filter = CommonMethods.cultureFilterRegex.Replace(filter, string.Empty);
      filter = CommonMethods.andOrFilterRegex.Replace(filter, string.Empty);
      return num != 0;
    }

    internal static bool MatchTaxonFilter(
      ref string filter,
      out Guid taxonId,
      out string propertyName)
    {
      taxonId = Guid.Empty;
      propertyName = string.Empty;
      Match match = CommonMethods.taxonFilterRegex.Match(filter);
      int num = match == null || !match.Groups[0].Success ? 0 : (match.Groups[1].Success ? 1 : 0);
      if (num == 0)
        return num != 0;
      taxonId = new Guid(match.Groups[2].ToString());
      propertyName = match.Groups[1].ToString();
      filter = CommonMethods.taxonFilterRegex.Replace(filter, string.Empty);
      return num != 0;
    }

    internal static string GetUserName(Guid userId) => userId != Guid.Empty ? CommonMethods.GetUser(userId) : string.Empty;

    internal static string GetUser(Guid id) => UserProfilesHelper.GetUserDisplayName(id);

    internal static string GetValidUrlName(string urlName) => Regex.Replace(urlName.ToLower(), "[^\\w\\-\\!\\$\\'\\(\\)\\=\\@\\d_]+", "-");

    /// <summary>
    /// Generates an expression that filters items for the specified culture if the site is in multilingual mode.
    /// </summary>
    /// <param name="uiCulture">The UI culture to be filtered.</param>
    /// <returns>The generated filter expression.</returns>
    /// <remarks>
    /// generates complex expression for a the property
    /// an example expression: "(Title["en"] != null OR (Title[""] != null AND Title["en"] = null AND Title["fr"] = null))"
    /// </remarks>
    internal static string GenerateSpecificCultureFilter(string uiCulture, string propertyName)
    {
      string specificCultureFilter = (string) null;
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("({0}[\"{1}\"] != null", (object) propertyName, (object) uiCulture);
        if (uiCulture == appSettings.DefaultFrontendLanguage.Name)
        {
          stringBuilder.AppendFormat(" OR ({0}[\"\"] != null", (object) propertyName);
          foreach (string str in ((IEnumerable<CultureInfo>) appSettings.DefinedFrontendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (l => l.Name)))
            stringBuilder.AppendFormat(" AND {0}[\"{1}\"] = null", (object) propertyName, (object) str);
          stringBuilder.Append(")");
        }
        stringBuilder.Append(")");
        specificCultureFilter = stringBuilder.ToString();
      }
      return specificCultureFilter;
    }

    /// <summary>
    /// Throws an exception if the specified <paramref name="item" /> has a conflicting URL.
    /// Auto generation of URLs is not considered, meaning that items which URLs have
    /// the auto generate property will still throw exception on conflict.
    /// </summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="manager">The manager.</param>
    /// <param name="item">The item.</param>
    /// <exception cref="M:Telerik.Sitefinity.Fluent.CommonMethods.ThrowDuplicateUrlException(System.String)">Throws an exception if the specified <paramref name="item" /> has a conflicting URL.</exception>
    internal static void ValidateContentUrl<TContent>(IManager manager, TContent item) where TContent : Content => CommonMethods.ValidateUrlConstraints<TContent>(manager, item, false, false);

    /// <summary>
    /// Validates the URL constraints.
    /// Throw exception if the item has a conflicting URL with an existing item.
    /// </summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="manager">The manager.</param>
    /// <param name="item">The item which URL should be validated.</param>
    /// <param name="revertChangesOnError">Specifies whether to revert changes before throwing the URL duplication exception.</param>
    /// <param name="recompileAutoGenerateUrlOnConflict">Specifies whether to throw exception for URL conflicts of auto generate URL items.</param>
    /// <exception cref="M:Telerik.Sitefinity.Fluent.CommonMethods.ThrowDuplicateUrlException(System.String)">Throw exception if the item has a conflicting URL with an existing item.</exception>
    internal static void ValidateUrlConstraints<TContent>(
      IManager manager,
      TContent item,
      bool revertChangesOnError = true,
      bool recompileAutoGenerateUrlOnConflict = true)
      where TContent : Content
    {
      if (!((object) item is ILocatable) || !(manager.Provider is ContentDataProviderBase) || !(manager is IContentManager))
        return;
      ILocatable locatable = (ILocatable) (object) item;
      ContentDataProviderBase provider = (ContentDataProviderBase) manager.Provider;
      Guid id = item.Id;
      List<UrlData> list = locatable.Urls.ToList<UrlData>();
      if (list.Count <= 0)
        return;
      Type type = item.GetType();
      Type urlTypeFor = provider.GetUrlTypeFor(type);
      foreach (UrlData urlData1 in list)
      {
        UrlData urlData = urlData1;
        string url = urlData.Url;
        IEnumerable<UrlData> source = ((IEnumerable<UrlData>) provider.GetUrls(urlTypeFor).Where<UrlData>((Expression<Func<UrlData, bool>>) (u => u.Url == url && u.Culture == urlData.Culture && u.Parent.Id != id)).ToArray<UrlData>()).Where<UrlData>((Func<UrlData, bool>) (u => u.Parent != null && ((Content) u.Parent).Status == ContentLifecycleStatus.Master));
        if (item.Status != ContentLifecycleStatus.Master)
          source = source.Where<UrlData>((Func<UrlData, bool>) (u => ((Content) u.Parent).Id != ((TContent) item).OriginalContentId));
        if (source.FirstOrDefault<UrlData>() != null)
        {
          if (recompileAutoGenerateUrlOnConflict && locatable.AutoGenerateUniqueUrl)
          {
            provider.Delete(urlData);
            locatable.UrlName = (Lstring) (CommonMethods.TitleToUrl((string) item.Title) + item.Id.ToString().Replace("-", string.Empty));
            (manager as IContentManager).RecompileItemUrls<ILocatable>(locatable);
          }
          else
          {
            if (revertChangesOnError)
            {
              CommonMethods.RevertStatisticsChanges<TContent>(item);
              CommonMethods.CancelChanges(manager);
            }
            CommonMethods.ThrowDuplicateUrlException(url);
          }
        }
      }
    }

    internal static bool IsFolderUrlDuplicate(IFolderManager manager, object item)
    {
      try
      {
        Folder folder = item as Folder;
        manager.ValidateFolderUrl(folder);
      }
      catch (WebProtocolException ex)
      {
        return true;
      }
      return false;
    }

    internal static bool IsUrlDuplicate(DataProviderBase provider, object item, string url)
    {
      ILocatable locatable = item as ILocatable;
      if (locatable == null)
        throw new InvalidOperationException("The provided item is not an instance of ILocatable");
      IQueryable<ILocatable> query = provider is UrlDataProviderBase dataProviderBase ? Queryable.Cast<ILocatable>(dataProviderBase.GetItems(item.GetType(), (string) null, (string) null, 0, 0) as IQueryable) : throw new InvalidOperationException("The provider is not an instance of UrlDataProviderBase");
      CommonMethods.ExecuteMlLogic<CultureInfo>((Action<CultureInfo>) (x =>
      {
        int currentSiteCultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.CurrentSite.DefaultCulture);
        // ISSUE: reference to a compiler-generated field
        query = query.Where<ILocatable>((Expression<Func<ILocatable, bool>>) (i => i.Urls.Any<UrlData>((Func<UrlData, bool>) (y => y.Url == this.url && y.Culture == currentSiteCultureLcid))));
      }), (Action<CultureInfo>) (x =>
      {
        int invariantLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(CultureInfo.InvariantCulture);
        int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(x);
        // ISSUE: reference to a compiler-generated field
        query = query.Where<ILocatable>((Expression<Func<ILocatable, bool>>) (i => i.Urls.Any<UrlData>((Func<UrlData, bool>) (y => y.Url == this.url && (y.Culture == invariantLcid || y.Culture == cultureLcid)))));
      }), (Action<CultureInfo>) (x =>
      {
        int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(x);
        // ISSUE: reference to a compiler-generated field
        query = query.Where<ILocatable>((Expression<Func<ILocatable, bool>>) (i => i.Urls.Any<UrlData>((Func<UrlData, bool>) (y => y.Url == this.url && y.Culture == cultureLcid))));
      }), SystemManager.CurrentContext.Culture);
      ILifecycleDataItemGeneric lifecycleItem = item as ILifecycleDataItemGeneric;
      bool flag;
      if (lifecycleItem != null)
      {
        ContentLifecycleStatus status = lifecycleItem.Status;
        IQueryable<ILifecycleDataItemGeneric> source1 = Queryable.Cast<ILifecycleDataItemGeneric>(query).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (i => (int) i.Status == 0));
        IQueryable<ILifecycleDataItemGeneric> source2;
        if (status == ContentLifecycleStatus.Master)
          source2 = source1.Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (x => x.Id != lifecycleItem.Id));
        else
          source2 = source1.Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (x => x.Id != lifecycleItem.OriginalContentId));
        flag = source2.Any<ILifecycleDataItemGeneric>();
      }
      else
      {
        query = query.Where<ILocatable>((Expression<Func<ILocatable, bool>>) (x => x.Id != locatable.Id));
        flag = query.Any<ILocatable>();
      }
      return flag;
    }

    internal static bool IsUrlDuplicate(DataProviderBase provider, object item)
    {
      if (!(item is ILocatable locatable))
        throw new InvalidOperationException("The provided item is not an instance of ILocatable");
      string url = provider is UrlDataProviderBase dataProviderBase ? dataProviderBase.CompileItemUrl<ILocatable>(locatable) : throw new InvalidOperationException("The provider is not an instance of UrlDataProviderBase");
      return CommonMethods.IsUrlDuplicate(provider, item, url);
    }

    public static void ExecuteMlLogic<TState>(
      Action<TState> monoAction = null,
      Action<TState> multilingualDefaultAction = null,
      Action<TState> multilingualNonDefaultAction = null,
      TState state = null)
      where TState : class
    {
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.LCID == SystemManager.CurrentContext.Culture.LCID)
        {
          if (multilingualDefaultAction == null)
            return;
          multilingualDefaultAction(state);
        }
        else
        {
          if (multilingualNonDefaultAction == null)
            return;
          multilingualNonDefaultAction(state);
        }
      }
      else
      {
        if (monoAction == null)
          return;
        monoAction(state);
      }
    }

    /// <summary>
    /// Reverts the changes that were mede in the Organizer when the item was created.
    /// </summary>
    /// <param name="item">The item.</param>
    private static void RevertStatisticsChanges<TContent>(TContent item) where TContent : Content => item.Organizer.ClearAll();

    internal static void ThrowDuplicateUrlException(string url)
    {
      WebProtocolException protocolException = new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<ContentResources>().DuplicateUrlException, (object) url), (Exception) null);
      protocolException.Data.Add((object) "Url", (object) url);
      throw protocolException;
    }

    internal static void ThrowDuplicateNameException(string name) => throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<ContentResources>().DuplicateNameException, (object) name), (Exception) null);

    private static void CancelChanges(DataProviderBase provider)
    {
      if (provider.TransactionName.IsNullOrWhitespace())
        provider.RollbackTransaction();
      else
        TransactionManager.RollbackTransaction(provider.TransactionName);
    }

    private static void CancelChanges(IManager manager)
    {
      if (manager.Provider.TransactionName.IsNullOrWhitespace())
        manager.CancelChanges();
      else
        TransactionManager.RollbackTransaction(manager.Provider.TransactionName);
    }

    internal static string TitleToUrl(string title)
    {
      ITextTransformationSettings urlTransformations = (ITextTransformationSettings) Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().SiteUrlSettings.ServerUrlTransformations;
      return CommonMethods.TitleToUrl(title, urlTransformations);
    }

    internal static string TitleToUrl(string title, ITextTransformationSettings settings) => CommonMethods.TitleToUrl(title, settings.RegularExpressionFilter, settings.ReplaceWith, settings.Trim, settings.ToLower);

    internal static string TitleToUrl(
      string title,
      string patternToReplace,
      string replaceString,
      bool trim,
      bool toLower)
    {
      string input = title;
      if (replaceString != string.Empty)
      {
        string pattern = string.Format("{0}{1}{2}", (object) "^(", (object) patternToReplace, (object) ")");
        input = Regex.Replace(Regex.Replace(input, pattern, string.Empty), string.Format("{0}{1}{2}", (object) "(", (object) patternToReplace, (object) ")$"), string.Empty);
      }
      if (trim)
        input = input.Trim();
      if (toLower)
        input = input.ToLower();
      return Regex.Replace(input, patternToReplace, replaceString);
    }

    internal static void FillContextBagFromCurrentRequest(IDictionary<string, string> contextBag)
    {
      string str = SystemManager.CurrentHttpContext.Request.QueryString[nameof (contextBag)];
      if (string.IsNullOrEmpty(str))
        return;
      foreach (KeyValuePair<string, string> keyValuePair in JsonSerializer.DeserializeFromString<List<KeyValuePair<string, string>>>(str))
      {
        if (!contextBag.ContainsKey(keyValuePair.Key))
          contextBag.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }

    internal static bool TryUpdateItemBeforeWorkflowScheduleOperation(
      IDataItem item,
      IDictionary<string, string> contextBag)
    {
      bool flag = false;
      if (item is IApprovalWorkflowItem approvalWorkflowItem && approvalWorkflowItem.ApprovalWorkflowState == (Lstring) "Published")
      {
        approvalWorkflowItem.ApprovalWorkflowState = (Lstring) "Draft";
        flag = true;
      }
      IScheduleable scheduleable = !(item is PageNode) ? item as IScheduleable : (IScheduleable) ((PageNode) item).GetPageData();
      if (scheduleable != null)
      {
        DateTime? date1;
        if (ScheduleWorkflowCallActivity.TryGetScheduledDate(contextBag, "PublicationDate", out date1) && date1.HasValue && date1.Value < DateTime.UtcNow)
        {
          scheduleable.PublicationDate = date1.Value;
          flag = true;
        }
        DateTime? date2;
        if (ScheduleWorkflowCallActivity.TryGetScheduledDate(contextBag, "ExpirationDate", out date2))
        {
          scheduleable.ExpirationDate = new DateTime?(date2.Value);
          flag = true;
        }
        else if (scheduleable.ExpirationDate.HasValue)
        {
          scheduleable.ExpirationDate = new DateTime?();
          flag = true;
        }
      }
      return flag;
    }
  }
}
