// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Clients;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Pages.Model.PropertyLoaders;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Statistics;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Represents an intermediary between page objects and page data.
  /// </summary>
  public class PageManager : 
    ControlManager<PageDataProvider>,
    ILifecycleManagerPages,
    ILifecycleManager<PageData, PageDraft>,
    IManager,
    IDisposable,
    IProviderResolver,
    ILanguageDataManager,
    ILifecycleManagerTemplates,
    ILifecycleManager<PageTemplate, TemplateDraft>,
    IMultisiteEnabledManager,
    ISupportRecyclingManager
  {
    internal const string MODULE_NAME = "Pages";
    private const string AddContentRelationsKey = "add-content-relations";
    private const string RemoveContentRelationsKey = "remove-content-relations";
    public const string sitefinityPagePermissionsInheritanceTransactionName = "sitefinityPagePermissionsInheritanceTransactionName";
    private IRecycleBinStrategy recycleBin;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageManager" /> class with the default provider.
    /// </summary>
    public PageManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public PageManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public PageManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    internal static bool IsPageDataForNodeStillLocked(Guid pageNodeId, string providerName = null) => PageManager.IsPageDataStillLocked(PageManager.GetManager(providerName).GetPageNode(pageNodeId).GetPageData());

    internal static bool IsPageDataOwnerChanged(PageData pageData)
    {
      PageDraft pageDraft = pageData.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (x => x.IsTempDraft));
      return pageDraft != null && SecurityManager.GetCurrentUserId() != pageDraft.Owner;
    }

    internal static bool IsPageDataStillLocked(PageData pageData) => pageData.LockedBy == Guid.Empty || pageData.LockedBy == SecurityManager.GetCurrentUserId();

    /// <summary>
    /// Makes a shallow copy of the data from the source page node to the target.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyPageNode(PageNode source, PageNode target) => this.CopyPageNode(source, target, (CultureInfo) null, (CultureInfo) null);

    /// <summary>
    /// Makes a shallow copy of the data from the source page node to the target.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// \
    ///             <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    public virtual void CopyPageNode(
      PageNode source,
      PageNode target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage)
    {
      this.CopyPageNode(source, target, sourceLanguage, targetLanguage, true);
    }

    public virtual void CopyPageNode(
      PageNode source,
      PageNode target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool changeParent)
    {
      this.CopyPageNode(source, target, sourceLanguage, targetLanguage, changeParent, true);
    }

    /// <summary>
    /// Makes a shallow copy of the data from the source page node to the target.
    /// </summary>
    public virtual void CopyPageNode(
      PageNode source,
      PageNode target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool changeParent,
      bool copyPermissionsInheritanceMap)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      foreach (KeyValuePair<string, string> attribute in (IEnumerable<KeyValuePair<string, string>>) source.Attributes)
        target.Attributes.Add(attribute);
      target.Description.CopyFrom(source.Description);
      target.Name = source.Name;
      if (changeParent)
        this.ChangeParent(target, source.Parent);
      target.IncludeInSearchIndex = source.IncludeInSearchIndex;
      target.RenderAsLink = source.RenderAsLink;
      target.ShowInNavigation = source.ShowInNavigation;
      target.Title.CopyFrom(source.Title);
      target.UrlName.CopyFrom(source.UrlName);
      target.NodeType = source.NodeType;
      target.RequireSsl = source.RequireSsl;
      target.Crawlable = source.Crawlable;
      target.LinkedNodeId = source.LinkedNodeId;
      target.LinkedNodeProvider = source.LinkedNodeProvider;
      target.RedirectUrl.CopyFrom(source.RedirectUrl);
      target.OpenNewWindow = source.OpenNewWindow;
      target.EnableDefaultCanonicalUrl = source.EnableDefaultCanonicalUrl;
      IDataProviderBase provider1 = ((IDataItem) target).Provider as IDataProviderBase;
      IDataProviderBase provider2 = ((IDataItem) source).Provider as IDataProviderBase;
      if (provider1 != null && provider2 != null)
        target.CopySecurityFrom((ISecuredObject) source, provider1, provider2, copyPermissionsInheritanceMap);
      target.Extension.CopyFrom(source.Extension);
      target.Urls.ClearDestinationUrls<PageUrlData>(source.Urls, new Action<PageUrlData>(((ContentManagerBase<PageDataProvider>) this).Delete));
      source.Urls.CopyTo<PageUrlData>(target.Urls, (IDataItem) target);
      if ((sourceLanguage != null ? 1 : (targetLanguage != null ? 1 : 0)) == 0)
        return;
      LocalizationHelper.CopyLstringProperties((IDynamicFieldsContainer) source, (IDynamicFieldsContainer) target, sourceLanguage, targetLanguage, false, true);
    }

    /// <summary>
    /// Makes a deep copy of the data form the source page to the target.
    /// </summary>
    /// <param name="sourcePage">The source page.</param>
    /// <param name="targetPage">The target page.</param>
    public virtual void CopyPageData(PageData sourcePage, PageData targetPage) => this.CopyPageData(sourcePage, targetPage, (CultureInfo) null, (CultureInfo) null, true);

    /// <summary>
    /// Makes a deep copy of the data form the source page to the target. NOTE: Clears all target language data from the source page.
    /// </summary>
    /// <param name="sourcePage">The source page.</param>
    /// <param name="targetPage">The target page.</param>
    /// <param name="sourceLanguage">The language from which to read data from the source page. If null, data from all available languages in the source page will be copied.</param>
    /// <param name="targetLanguage">The language for which to write data in the target page. If null, the data for all available languages in the source page will be copied.</param>
    /// <param name="copyContentsFromSource">if set to <c>true</c> all content from the source page will be copied to the target page.</param>
    public virtual void CopyPageData(
      PageData sourcePage,
      PageData targetPage,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool copyContentsFromSource)
    {
      this.CopyPageData(sourcePage, targetPage, sourceLanguage, targetLanguage, copyContentsFromSource, true);
    }

    /// <summary>
    /// Makes a deep copy of the data form the source page to the target page.
    /// </summary>
    /// <param name="sourcePage">The source page.</param>
    /// <param name="targetPage">The target page.</param>
    /// <param name="sourceLanguage">The language from which to read data from the source page. If null, data from all available languages in the source page will be copied.</param>
    /// <param name="targetLanguage">The language for which to write data in the target page. If null, the data for all available languages in the source page will be copied.</param>
    /// <param name="copyContentFromSource">if set to <c>true</c> all content from the source page will be copied to the target page.</param>
    /// <param name="clearTargetLanguageDataFromSource">if set to <c>true</c> all data for the target language will be cleared from the source page.</param>
    public virtual void CopyPageData(
      PageData sourcePage,
      PageData targetPage,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool copyContentFromSource,
      bool clearTargetLanguageDataFromSource)
    {
      this.CopyPageData(sourcePage, targetPage, sourceLanguage, targetLanguage, copyContentFromSource, clearTargetLanguageDataFromSource, false);
    }

    /// <summary>
    /// Makes a deep copy of the data form the source page to the target page.
    /// </summary>
    /// <param name="sourcePage">The source page.</param>
    /// <param name="targetPage">The target page.</param>
    /// <param name="sourceLanguage">The language from which to read data from the source page. If null, data from all available languages in the source page will be copied.</param>
    /// <param name="targetLanguage">The language for which to write data in the target page. If null, the data for all available languages in the source page will be copied.</param>
    /// <param name="copyContentFromSource">if set to <c>true</c> all content from the source page will be copied to the target page.</param>
    /// <param name="clearTargetLanguageDataFromSource">if set to <c>true</c> all data for the target language will be cleared from the source page.</param>
    /// <param name="ignorePersonalization">If set true controls' personalized versions will not be copied</param>
    public virtual void CopyPageData(
      PageData sourcePage,
      PageData targetPage,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool copyContentFromSource,
      bool clearTargetLanguageDataFromSource,
      bool ignorePersonalization)
    {
      if (sourcePage == null)
        throw new ArgumentNullException(nameof (sourcePage));
      if (targetPage == null)
        throw new ArgumentNullException(nameof (targetPage));
      foreach (KeyValuePair<string, string> attribute in (IEnumerable<KeyValuePair<string, string>>) sourcePage.Attributes)
        targetPage.Attributes.Add(attribute);
      targetPage.BufferOutput = sourcePage.BufferOutput;
      targetPage.CacheDuration = sourcePage.CacheDuration;
      targetPage.CacheOutput = sourcePage.CacheOutput;
      targetPage.EnableEventValidation = sourcePage.EnableEventValidation;
      targetPage.EnableSessionState = sourcePage.EnableSessionState;
      targetPage.EnableTheming = sourcePage.EnableTheming;
      targetPage.EnableViewState = sourcePage.EnableViewState;
      targetPage.EnableViewStateMac = sourcePage.EnableViewStateMac;
      targetPage.ErrorPage = sourcePage.ErrorPage;
      targetPage.ExpirationDate = sourcePage.ExpirationDate;
      targetPage.ExternalPage = sourcePage.ExternalPage;
      targetPage.HtmlTitle.CopyFrom(sourcePage.HtmlTitle);
      targetPage.Keywords.CopyFrom(sourcePage.Keywords);
      targetPage.Description.CopyFrom(sourcePage.Description);
      targetPage.MaintainScrollPositionOnPostback = sourcePage.MaintainScrollPositionOnPostback;
      targetPage.MasterPage = sourcePage.MasterPage;
      targetPage.Owner = sourcePage.Owner;
      targetPage.PublicationDate = sourcePage.PublicationDate;
      targetPage.ResponseEncoding = sourcePage.ResponseEncoding;
      targetPage.SlidingExpiration = sourcePage.SlidingExpiration;
      targetPage.CodeBehindType = sourcePage.CodeBehindType;
      targetPage.Status = sourcePage.Status;
      targetPage.Trace = sourcePage.Trace;
      targetPage.TraceMode = sourcePage.TraceMode;
      targetPage.ValidateRequest = sourcePage.ValidateRequest;
      targetPage.ViewStateEncryption = sourcePage.ViewStateEncryption;
      targetPage.Visible = sourcePage.Visible;
      targetPage.OutputCacheProfile = sourcePage.OutputCacheProfile;
      targetPage.HeadTagContent = targetPage.HeadTagContent;
      targetPage.LastControlId = sourcePage.LastControlId;
      targetPage.IsAutoCreated = sourcePage.IsAutoCreated;
      if (copyContentFromSource)
        this.CopyPageContents(sourcePage, targetPage, sourceLanguage, targetLanguage, ignorePersonalization);
      else
        this.CopyRendererData((object) sourcePage, (object) targetPage);
      if ((sourceLanguage != null ? 1 : (targetLanguage != null ? 1 : 0)) != 0)
        LocalizationHelper.CopyLstringProperties((IDynamicFieldsContainer) sourcePage, (IDynamicFieldsContainer) targetPage, sourceLanguage, targetLanguage, false, clearTargetLanguageDataFromSource);
      this.CopyPageCulture(sourcePage, targetPage, targetLanguage);
    }

    internal void CopyPageCulture(
      PageData sourcePage,
      PageData targetPage,
      CultureInfo targetLanguage)
    {
      if (targetLanguage != null)
        targetPage.Culture = targetLanguage.Name;
      else
        targetPage.Culture = sourcePage.Culture;
    }

    /// <summary>
    /// Copies the page contents from one page to another. Note that this only copies contents (controls, theme and template) and not all the properties
    /// of the page.
    /// </summary>
    /// <param name="sourcePage">The source page.</param>
    /// <param name="targetPage">The target page.</param>
    /// <param name="sourceLanguage">The language from which to read control data from the source page. If null, data from all available languages in the source controls will be copied.</param>
    /// <param name="targetLanguage">The language for which to write control data in the target page. If null, the data for all available languages in the source controls will be copied.</param>
    public virtual void CopyPageContents(
      PageData sourcePage,
      PageData targetPage,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage)
    {
      this.CopyPageContents(sourcePage, targetPage, sourceLanguage, targetLanguage, false);
    }

    /// <summary>
    /// Copies the page contents from one page to another. Note that this only copies contents (controls, theme and template) and not all the properties
    /// of the page.
    /// </summary>
    /// <param name="sourcePage">The source page.</param>
    /// <param name="targetPage">The target page.</param>
    /// <param name="sourceLanguage">The language from which to read control data from the source page. If null, data from all available languages in the source controls will be copied.</param>
    /// <param name="targetLanguage">The language for which to write control data in the target page. If null, the data for all available languages in the source controls will be copied.</param>
    /// <param name="ignorePersonalization">If set true controls' personalized versions will not be copied</param>
    public virtual void CopyPageContents(
      PageData sourcePage,
      PageData targetPage,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool ignorePersonalization)
    {
      targetPage.Template = sourcePage.Template;
      targetPage.IncludeScriptManager = sourcePage.IncludeScriptManager;
      LocalizationHelper.CopyLstring(sourcePage.Themes, targetPage.Themes);
      this.CopyControls<PageControl, PageControl>((IEnumerable<PageControl>) sourcePage.Controls, targetPage.Controls, sourceLanguage, targetLanguage, CopyDirection.Unspecified, false, ignorePersonalization);
      this.CopyPresentation<PagePresentation, PagePresentation>((IEnumerable<PagePresentation>) sourcePage.Presentation, targetPage.Presentation);
      this.CopyPageCommonData<PageControl, PagePresentation, PageControl, PagePresentation>((IPageCommonData<PageControl, PagePresentation>) sourcePage, (IPageCommonData<PageControl, PagePresentation>) targetPage, sourceLanguage, targetLanguage, CopyDirection.Unspecified);
    }

    /// <summary>Makes full copy of a page</summary>
    /// <param name="sourcePageNode">The page which should be duplicated</param>
    /// <param name="targetPageNode">The duplicated page</param>
    /// <param name="sourceCulture">The culture to duplicate</param>
    /// <param name="targetCulture">The duplicated culture</param>
    /// <param name="parentId">The parent id.</param>
    /// <param name="duplicateHierarchy">If set to <c>true</c> duplicates the whole page hierarchy.</param>
    /// <param name="fullCopy">if set to <c>true</c> makes a full copy.</param>
    /// <returns>The duplicated page <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /></returns>
    internal PageNode DuplicatePage(
      PageNode sourcePageNode,
      PageNode targetPageNode,
      CultureInfo sourceCulture,
      CultureInfo targetCulture,
      Guid parentId,
      bool duplicateHierarchy,
      bool fullCopy)
    {
      if (sourceCulture == null && targetCulture == null && SystemManager.CurrentContext.AppSettings.Multilingual)
        sourceCulture = targetCulture = SystemManager.CurrentContext.Culture;
      if (parentId == Guid.Empty)
        parentId = sourcePageNode.ParentId;
      using (new CultureRegion(targetCulture))
      {
        this.CopyPageNode(sourcePageNode, targetPageNode, (CultureInfo) null, (CultureInfo) null, parentId == sourcePageNode.ParentId, false);
        LocalizationHelper.CopyLstringProperties((IDynamicFieldsContainer) sourcePageNode, (IDynamicFieldsContainer) targetPageNode, sourceCulture, targetCulture, false, !fullCopy, false);
        if (fullCopy)
        {
          targetPageNode.ApprovalWorkflowState.CopyFrom(sourcePageNode.Title);
          targetPageNode.LocalizationStrategy = sourcePageNode.LocalizationStrategy;
        }
        else
        {
          targetPageNode.ApprovalWorkflowState.ClearAllValues(true);
          targetPageNode.ApprovalWorkflowState = (Lstring) "Draft";
          targetPageNode.LocalizationStrategy = LocalizationStrategy.NotSelected;
        }
        PageData pageData = this.GetPageData(sourcePageNode, sourceCulture);
        if (pageData != null)
        {
          IQueryable<PageData> source = this.GetPageDataList(targetPageNode);
          if (sourceCulture != null)
            source = source.Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.Culture == sourceCulture.Name));
          PageData targetPageData = source.FirstOrDefault<PageData>() ?? this.CreatePageData();
          bool ignorePersonalization = sourcePageNode != null && targetPageNode != null && sourcePageNode.RootNodeId != targetPageNode.RootNodeId;
          this.CopySourceDataToTargetData(targetPageNode, sourceCulture, targetCulture, fullCopy, pageData, targetPageData, ignorePersonalization);
          if (!fullCopy)
            targetPageData.Visible = false;
        }
        this.DuplicateContentLinks(sourcePageNode, targetPageNode);
      }
      if (parentId != Guid.Empty)
      {
        PageNode newParent = this.GetPageNode(parentId);
        this.provider.WithSuppressedSecurityChecks((Action) (() => this.ChangeParent(targetPageNode, newParent)));
      }
      targetPageNode.Urls.ClearUrls<PageUrlData>(true);
      if (duplicateHierarchy)
      {
        foreach (PageNode sourcePageNode1 in new List<PageNode>((IEnumerable<PageNode>) sourcePageNode.Nodes))
          this.DuplicatePage(sourcePageNode1, this.CreatePageNode(), sourceCulture, targetCulture, targetPageNode.Id, duplicateHierarchy, fullCopy);
      }
      return targetPageNode;
    }

    private void DuplicateContentLinks(PageNode sourcePageNode, PageNode targetPageNode)
    {
      IContentLinksManager mappedRelatedManager = this.provider.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
      IQueryable<ContentLink> contentLinks = mappedRelatedManager.GetContentLinks();
      Expression<Func<ContentLink, bool>> predicate = (Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == typeof (PageNode).FullName && cl.ParentItemId == sourcePageNode.Id);
      foreach (ContentLink contentLink1 in (IEnumerable<ContentLink>) contentLinks.Where<ContentLink>(predicate))
      {
        ContentLink contentLink2 = mappedRelatedManager.CreateContentLink(contentLink1.ComponentPropertyName, targetPageNode.Id, contentLink1.ChildItemId, contentLink1.ParentItemProviderName, contentLink1.ChildItemProviderName, contentLink1.ParentItemType, contentLink1.ChildItemType);
        contentLink1.CopyTo(contentLink2);
        contentLink2.ParentItemId = targetPageNode.Id;
      }
    }

    private void CopySourceDataToTargetData(
      PageNode targetPageNode,
      CultureInfo sourceCulture,
      CultureInfo targetCulture,
      bool fullCopy,
      PageData sourcePageData,
      PageData targetPageData,
      bool ignorePersonalization = false)
    {
      this.CopyPageData(sourcePageData, targetPageData, sourceCulture, CultureInfo.InvariantCulture, true, false, ignorePersonalization);
      this.CopyPageCulture(sourcePageData, targetPageData, targetCulture);
      LocalizationHelper.CopyLstringProperties((IDynamicFieldsContainer) sourcePageData, (IDynamicFieldsContainer) targetPageData, sourceCulture, targetCulture, false, false);
      this.CopyPageDrafts(sourcePageData, targetPageData, sourceCulture, CultureInfo.InvariantCulture, ignorePersonalization);
      List<IControlsContainer> controlsContainerList = new List<IControlsContainer>();
      controlsContainerList.Add((IControlsContainer) targetPageData);
      controlsContainerList.AddRange((IEnumerable<IControlsContainer>) targetPageData.Drafts);
      foreach (IControlsContainer controlsContainer in controlsContainerList)
      {
        foreach (Telerik.Sitefinity.Pages.Model.ControlData control in controlsContainer.Controls)
        {
          List<ControlProperty> propertiesForExistingCulture = control.GetProperties(sourceCulture, true).ToList<ControlProperty>();
          foreach (ControlProperty controlProperty in propertiesForExistingCulture)
            controlProperty.Language = (string) null;
          foreach (ControlProperty controlProperty in control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => !propertiesForExistingCulture.Contains(x))).ToList<ControlProperty>())
            this.Delete(controlProperty);
          control.Strategy = PropertyPersistenceStrategy.NotTranslatable;
        }
      }
      targetPageData.LastControlId = sourcePageData.LastControlId;
      targetPageData.NavigationNode = targetPageNode;
      targetPageData.Version = 1;
      if (!fullCopy)
        return;
      IEnumerable<string> source = sourcePageData.PublishedTranslations.Distinct<string>();
      foreach (LanguageData sourceLanguageData in (IEnumerable<LanguageData>) sourcePageData.LanguageData)
      {
        string language = sourceLanguageData.Language;
        if (language == null || source.Contains<string>(language))
        {
          LanguageData languageData = this.CreateLanguageData();
          languageData.Language = language;
          languageData.CopyFrom(sourceLanguageData);
          targetPageData.LanguageData.Add(languageData);
          if (language != null)
            targetPageData.PublishedTranslations.Add(language);
        }
      }
    }

    internal PageDraft CreateDraftForPage(PageData parentPage, CultureInfo culture = null) => this.PagesLifecycle.Edit(parentPage, culture);

    internal void CopyTemporaryPage(
      PageDraft sourceTemp,
      PageDraft targetTemp,
      CultureInfo sourceCulture = null,
      CultureInfo targetCulture = null,
      bool ignorePersonalization = false)
    {
      if (targetTemp.TemplateId != sourceTemp.TemplateId)
        targetTemp.TemplateId = sourceTemp.TemplateId;
      CopyDirection copyDirection = this.ResolveDraftsCopyDirection((DraftData) sourceTemp, (DraftData) targetTemp);
      this.CopyPageCommonData<PageDraftControl, PageDraftPresentation, PageDraftControl, PageDraftPresentation>((IPageCommonData<PageDraftControl, PageDraftPresentation>) sourceTemp, (IPageCommonData<PageDraftControl, PageDraftPresentation>) targetTemp, sourceCulture, targetCulture, copyDirection, ignorePersonalization);
    }

    internal void CopyDraftContentsToPage(
      PageDraft sourceTemp,
      PageData targetPage,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      CopyDirection copyDirection)
    {
      if (sourceTemp.TemplateId != Guid.Empty)
      {
        bool suppressSecurityChecks = this.Provider.SuppressSecurityChecks;
        this.Provider.SuppressSecurityChecks = true;
        PageTemplate template = this.GetTemplate(sourceTemp.TemplateId);
        targetPage.Template = template;
        this.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
      else
        targetPage.Template = (PageTemplate) null;
      this.CopyPageCommonData<PageDraftControl, PageDraftPresentation, PageControl, PagePresentation>((IPageCommonData<PageDraftControl, PageDraftPresentation>) sourceTemp, (IPageCommonData<PageControl, PagePresentation>) targetPage, sourceLanguage, targetLanguage, copyDirection);
    }

    /// <summary>Copies common page data from one page to another.</summary>
    /// <param name="sourcePage">The source page.</param>
    /// <param name="targetPage">The target page.</param>
    /// <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <param name="tryToReuseControls">If true, controls in the target object will be reused when possible. If false, all controls in
    /// the target object are first deleted and then controls from the source are copied to the target as new controls.</param>
    public virtual void CopyPageCommonData<TControlA, TPresentationA, TControlB, TPresentationB>(
      IPageCommonData<TControlA, TPresentationA> sourcePage,
      IPageCommonData<TControlB, TPresentationB> targetPage,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      CopyDirection copyDirection)
      where TControlA : Telerik.Sitefinity.Pages.Model.ControlData
      where TPresentationA : PresentationData
      where TControlB : Telerik.Sitefinity.Pages.Model.ControlData
      where TPresentationB : PresentationData
    {
      this.CopyPageCommonData<TControlA, TPresentationA, TControlB, TPresentationB>(sourcePage, targetPage, sourceLanguage, targetLanguage, copyDirection, false);
    }

    /// <summary>Copies common page data from one page to another.</summary>
    /// <param name="sourcePage">The source page.</param>
    /// <param name="targetPage">The target page.</param>
    /// <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <param name="tryToReuseControls">If true, controls in the target object will be reused when possible. If false, all controls in
    /// the target object are first deleted and then controls from the source are copied to the target as new controls.</param>
    /// <param name="ignorePersonalization">If set true controls' personalized versions will not be copied</param>
    public virtual void CopyPageCommonData<TControlA, TPresentationA, TControlB, TPresentationB>(
      IPageCommonData<TControlA, TPresentationA> sourcePage,
      IPageCommonData<TControlB, TPresentationB> targetPage,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      CopyDirection copyDirection,
      bool ignorePersonalization)
      where TControlA : Telerik.Sitefinity.Pages.Model.ControlData
      where TPresentationA : PresentationData
      where TControlB : Telerik.Sitefinity.Pages.Model.ControlData
      where TPresentationB : PresentationData
    {
      LocalizationHelper.CopyLstring(sourcePage.Themes, targetPage.Themes, sourceLanguage.GetLstring(), targetLanguage.GetLstring());
      if (targetPage.Version != sourcePage.Version)
        targetPage.Version = sourcePage.Version;
      if (targetPage.MasterPage != sourcePage.MasterPage)
        targetPage.MasterPage = sourcePage.MasterPage;
      if (targetPage.ExternalPage != sourcePage.ExternalPage)
        targetPage.ExternalPage = sourcePage.ExternalPage;
      if (targetPage.UrlEvaluationMode != sourcePage.UrlEvaluationMode)
        targetPage.UrlEvaluationMode = sourcePage.UrlEvaluationMode;
      if (targetPage.LastControlId != sourcePage.LastControlId)
        targetPage.LastControlId = sourcePage.LastControlId;
      if (targetPage.IncludeScriptManager != sourcePage.IncludeScriptManager)
        targetPage.IncludeScriptManager = sourcePage.IncludeScriptManager;
      if (targetPage.PersonalizationMasterId != sourcePage.PersonalizationMasterId)
        targetPage.PersonalizationMasterId = sourcePage.PersonalizationMasterId;
      if (targetPage.PersonalizationSegmentId != sourcePage.PersonalizationSegmentId)
        targetPage.PersonalizationSegmentId = sourcePage.PersonalizationSegmentId;
      IFlagsContainer flagsContainer1 = targetPage as IFlagsContainer;
      IFlagsContainer flagsContainer2 = sourcePage as IFlagsContainer;
      if (flagsContainer1 != null && flagsContainer2 != null)
        flagsContainer1.Flags = flagsContainer2.Flags;
      this.CopyRendererData((object) sourcePage, (object) targetPage);
      bool optimized = ((int) (SystemManager.CurrentHttpContext.Items[(object) "OptimizedCopy"] as bool?) ?? 0) != 0;
      this.CopyControls<TControlA, TControlB>((IEnumerable<TControlA>) sourcePage.Controls, targetPage.Controls, sourceLanguage, targetLanguage, copyDirection, optimized, ignorePersonalization);
      this.CopyPresentation<TPresentationA, TPresentationB>((IEnumerable<TPresentationA>) sourcePage.Presentation, targetPage.Presentation);
    }

    /// <summary>
    /// Copies the collection of PageDraft objects from the source page to the target page.
    /// </summary>
    /// <param name="sourcePage">The source page.</param>
    /// <param name="targetPage">The target page.</param>
    /// <param name="sourceCulture">The source culture.</param>
    /// <param name="targetCulture">The target culture.</param>
    /// <param name="ignorePersonalization">If set true controls' personalized versions will not be copied</param>
    internal void CopyPageDrafts(
      PageData sourcePage,
      PageData targetPage,
      CultureInfo sourceCulture = null,
      CultureInfo targetCulture = null,
      bool ignorePersonalization = false)
    {
      foreach (PageDraft sourceTemp in sourcePage.Drafts.Where<PageDraft>((Func<PageDraft, bool>) (dr => !dr.IsTempDraft)))
      {
        PageDraft draft = this.CreateDraft<PageDraft>();
        this.CopyTemporaryPage(sourceTemp, draft, sourceCulture, targetCulture, ignorePersonalization);
        targetPage.Drafts.Add(draft);
      }
    }

    /// <summary>
    /// This method is used to initialize an uninitialized split page. If sourceCulture and targetCulture are specified,
    /// the contents of the sourceLanguage version for the same page are copied as targetLanguage in the given node.
    /// </summary>
    /// <param name="targetNode">The node to initialize.</param>
    /// <param name="sourceCulture">The language to copy contents from (null = start from scratch).</param>
    /// <param name="targetCulture">The language to copy contents to. This must be specified if sourceCulture is specified.</param>
    public void InitializeSplitPage(
      PageNode targetNode,
      CultureInfo sourceCulture,
      CultureInfo targetCulture)
    {
      if (sourceCulture != null)
      {
        PageData pageData1 = this.GetPageData(targetNode, sourceCulture);
        if (pageData1 == null)
          throw new ArgumentException("A version of the page was not found for the specified language: " + sourceCulture.Name);
        PageDraft sourceTemp = pageData1.Drafts.Where<PageDraft>((Func<PageDraft, bool>) (d => !d.IsTempDraft)).OrderBy<PageDraft, DateTime>((Func<PageDraft, DateTime>) (d => d.LastModified)).FirstOrDefault<PageDraft>();
        PageData pageData2 = this.GetPageData(targetNode, targetCulture);
        if (sourceTemp != null)
          this.CopyDraftContentsToPage(sourceTemp, pageData2, sourceCulture, targetCulture, CopyDirection.Unspecified);
        else
          this.CopyPageContents(pageData1, pageData2, sourceCulture, targetCulture);
        pageData2.Drafts.Clear();
        pageData2.IsAutoCreated = false;
      }
      else
      {
        PageData pageData = this.GetPageData(targetNode, targetCulture);
        if (pageData != null)
          pageData.IsAutoCreated = false;
      }
      if (targetNode.LocalizationStrategy == LocalizationStrategy.Split)
        return;
      targetNode.LocalizationStrategy = LocalizationStrategy.Split;
    }

    [Obsolete("Use SplitSynchronizedPageData")]
    public PageNode[] SplitSynchronizedPageNode(PageNode pageNode, bool copyDataFromSource)
    {
      this.SplitSynchronizedPageData(pageNode, copyDataFromSource);
      return new PageNode[1]{ pageNode };
    }

    /// <summary>
    /// Splits page's information per culture.
    /// Split page and group page could not be split.
    /// </summary>
    /// <param name="pageNode">The page node to split.</param>
    /// <param name="copyDataFromSource">if set to <c>true</c> [copy data from source].</param>
    /// <returns></returns>
    public PageData[] SplitSynchronizedPageData(PageNode pageNode, bool copyDataFromSource)
    {
      PageData pageData1 = pageNode.GetPageData();
      if (pageData1 == null)
        throw new ArgumentException(Res.Get<PageResources>().CannotSplitGroupPage);
      if (pageNode.LocalizationStrategy == LocalizationStrategy.Split)
        throw new ArgumentException(Res.Get<PageResources>().CannotSplitAlreadySplit);
      CultureInfo nodeInitialLanguage = (CultureInfo) null;
      if (pageData1 == null)
        pageData1 = this.GetPageDataList(pageNode).OrderBy<PageData, DateTime>((Expression<Func<PageData, DateTime>>) (pd => pd.DateCreated)).FirstOrDefault<PageData>();
      PageData pageData2 = pageData1;
      nodeInitialLanguage = CultureInfo.GetCultureInfo(pageData2.Culture);
      CultureInfo[] availableCultures = pageNode.AvailableCultures;
      if (!((IEnumerable<CultureInfo>) availableCultures).Contains<CultureInfo>(nodeInitialLanguage))
      {
        nodeInitialLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
        pageData2.Culture = nodeInitialLanguage.Name;
      }
      pageNode.LocalizationStrategy = LocalizationStrategy.Split;
      IEnumerable<CultureInfo> languages = ((IEnumerable<CultureInfo>) availableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (c => !c.Equals((object) CultureInfo.InvariantCulture) && !c.Equals((object) nodeInitialLanguage)));
      IList<PageData> source = this.SplitPageData(pageNode, pageData2, languages, copyDataFromSource);
      foreach (PageData pageData3 in (IEnumerable<PageData>) source)
      {
        if (pageData2.PublishedTranslations.Contains(pageData3.Culture))
        {
          pageData3.Visible = true;
          pageData3.PublishedTranslations.Add(pageData3.Culture);
        }
        else
          pageData3.Visible = false;
      }
      int num = pageData2.PublishedTranslations.Contains(pageData2.Culture) ? 1 : (pageData2.PublishedTranslations.Count != 0 || !pageData2.Visible ? 0 : (pageData2.Version > 0 ? 1 : 0));
      pageData2.PublishedTranslations.Clear();
      pageData2.Visible = false;
      if (num != 0)
      {
        pageData2.PublishedTranslations.Add(pageData2.Culture);
        pageData2.Visible = true;
      }
      this.ClearPageDataProperties(pageData2);
      return source.ToArray<PageData>();
    }

    internal IList<PageData> SplitPageData(
      PageNode node,
      PageData sourcePageData,
      IEnumerable<CultureInfo> languages,
      bool copyDataFromSource)
    {
      List<PageData> pageDataList = new List<PageData>();
      foreach (CultureInfo language in languages)
      {
        PageData dataLanguageVersion = this.CreatePageDataLanguageVersion(sourcePageData, CultureInfo.GetCultureInfo(sourcePageData.Culture), language, copyDataFromSource);
        dataLanguageVersion.NavigationNode = node;
        pageDataList.Add(dataLanguageVersion);
      }
      return (IList<PageData>) pageDataList;
    }

    private IEnumerable<ControlProperty> ClearControlProperties(
      IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData> controls,
      string dataLang,
      string defaultLang,
      FixMultilingualSiteTask task)
    {
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      foreach (Telerik.Sitefinity.Pages.Model.ControlData control in controls)
      {
        if (control.Strategy == PropertyPersistenceStrategy.BackwardCompatible)
          controlPropertyList.AddRange((IEnumerable<ControlProperty>) task.ClearPropertiesForSplit(this, control, dataLang, defaultLang));
        else if (control.Strategy == PropertyPersistenceStrategy.Translatable)
          controlPropertyList.AddRange(this.ConvertToNonTranslatableControl(dataLang, control));
      }
      return (IEnumerable<ControlProperty>) controlPropertyList;
    }

    private IEnumerable<ControlProperty> ConvertToNonTranslatableControl(
      string dataLang,
      Telerik.Sitefinity.Pages.Model.ControlData control)
    {
      List<ControlProperty> translatableControl = new List<ControlProperty>();
      IEnumerable<ControlProperty> actualProps = control.GetProperties(CultureInfo.GetCultureInfo(dataLang), true);
      IEnumerable<ControlProperty> collection = control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => !actualProps.Contains<ControlProperty>(x)));
      translatableControl.AddRange(collection);
      foreach (ControlProperty controlProperty in actualProps)
      {
        if (controlProperty.Language != null)
          controlProperty.Language = (string) null;
      }
      foreach (ControlProperty controlProperty in translatableControl)
        control.Properties.Remove(controlProperty);
      control.Strategy = PropertyPersistenceStrategy.NotTranslatable;
      return (IEnumerable<ControlProperty>) translatableControl;
    }

    private void ClearPageDataProperties(PageData data)
    {
      FixMultilingualSiteTask task = new FixMultilingualSiteTask();
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      string name = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
      controlPropertyList.AddRange(this.ClearControlProperties((IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>) data.Controls, data.Culture, name, task));
      foreach (PageDraft draft in (IEnumerable<PageDraft>) data.Drafts)
        controlPropertyList.AddRange(this.ClearControlProperties((IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>) draft.Controls, data.Culture, name, task));
      foreach (ControlProperty controlProperty in controlPropertyList)
        this.Delete(controlProperty);
    }

    /// <summary>
    /// Initializes the localization strategy of a page. This method must be invoked when a second language version of a page is created.
    /// </summary>
    /// <param name="pageNode">The page node. Only nodes with uninitialized strategy are valid.</param>
    /// <param name="strategy">The strategy.</param>
    /// <param name="copyData">if set to <c>true</c> [copy data].</param>
    public void InitializePageLocalizationStrategy(
      PageNode pageNode,
      LocalizationStrategy strategy,
      bool copyData)
    {
      if (strategy == LocalizationStrategy.NotSelected)
        throw new ArgumentException("You must specify a localization strategy!");
      if (pageNode.LocalizationStrategy != LocalizationStrategy.NotSelected)
        throw new ArgumentException("The localization strategy for the given node is already selected.");
      if (strategy == LocalizationStrategy.Synced)
      {
        pageNode.LocalizationStrategy = strategy;
        PageData data = pageNode.PageDataList.FirstOrDefault<PageData>();
        if (data != null)
          data.Culture = string.Empty;
        this.TranslateControlProperties(pageNode, data);
      }
      else
        this.SplitSynchronizedPageData(pageNode, copyData);
    }

    private void TranslateControlProperties(PageNode node, PageData data)
    {
      FixMultilingualSiteTask multilingualSiteTask = new FixMultilingualSiteTask();
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      CultureInfo frontendLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
      controlPropertyList.AddRange((IEnumerable<ControlProperty>) multilingualSiteTask.ClearPropertiesForSyncAndNotSelected(this, (IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>) data.Controls, node.AvailableLanguages, frontendLanguage.Name));
      foreach (PageDraft draft in (IEnumerable<PageDraft>) data.Drafts)
        controlPropertyList.AddRange((IEnumerable<ControlProperty>) multilingualSiteTask.ClearPropertiesForSyncAndNotSelected(this, (IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>) draft.Controls, node.AvailableLanguages, frontendLanguage.Name));
      foreach (ControlProperty controlProperty in controlPropertyList)
        this.Delete(controlProperty);
    }

    /// <summary>
    /// Ensures language versions for the given node are created for all defined languages.
    /// </summary>
    /// <param name="node">The node to inspect.</param>
    [Obsolete("This method is not required any more.")]
    public void EnsureLanguageVersionsForSplitNode(PageNode node)
    {
    }

    /// <summary>Creates the system nodes for split page.</summary>
    /// <param name="sourceNode">The source node.</param>
    [Obsolete("This is not required any more.")]
    public void CreateSystemNodesForSplitPage(PageNode sourceNode)
    {
    }

    /// <summary>
    /// Creates the system node(marked as not created by user) for a split page in a given language.
    /// </summary>
    /// <param name="sourceNode">The source node.</param>
    /// <param name="lang">The language of the new node.</param>
    /// <returns></returns>
    [Obsolete("Use CreateSystemPageDataForSplitPage")]
    public PageNode CreateSystemNodeForSplitPage(PageNode sourceNode, CultureInfo lang)
    {
      this.CreateSystemPageDataForSplitPage(sourceNode, lang);
      return sourceNode;
    }

    public PageData CreateSystemPageDataForSplitPage(PageNode sourceNode, CultureInfo lang)
    {
      if (sourceNode.LocalizationStrategy != LocalizationStrategy.Split)
        throw new ArgumentException("The specified node must be in SPLIT mode and not a group page node.");
      PageData pageData = this.CreatePageData();
      pageData.Culture = lang.Name;
      pageData.IsAutoCreated = true;
      pageData.NavigationNode = sourceNode;
      return pageData;
    }

    /// <summary>
    /// Determines the identifier of root page depending on predefined <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageLocation" /> enumeration and sets it as current page at fluent API.
    /// </summary>
    /// <param name="pageLocation">The page location.</param>
    /// <returns></returns>
    public virtual PageNode GetLocationRoot(PageLocation pageLocation)
    {
      if (pageLocation == PageLocation.Frontend)
        return this.GetPageNode(SiteInitializer.CurrentFrontendRootNodeId);
      if (pageLocation == PageLocation.Backend)
        return this.GetPageNode(SiteInitializer.BackendRootNodeId);
      throw new NotSupportedException();
    }

    /// <summary>
    /// Edits the page.
    /// If a temp draft is available for the current user returns it for editing
    /// If the page is locked by another user and <paramref name="lockIt" /> is set to true we remove all the temp drafts (for all users)
    /// and create a new draft for the specified page
    /// </summary>
    /// <param name="pageDataId">The page data id.</param>
    /// <param name="lockIt">if set to <c>true</c> locks the page and prevents other users from editing it.</param>
    /// <returns></returns>
    [Obsolete("The boolean lockIt parameter is now obsolete. Use the EditPage(Guid, CultureInfo) instead.")]
    public virtual PageDraft EditPage(Guid pageDataId, bool lockIt) => this.EditPage(pageDataId, lockIt, (CultureInfo) null);

    /// <summary>
    /// Edits the page.
    /// If a temp draft is available for the current user returns it for editing
    /// If the page is locked by another user and <paramref name="lockIt" /> is set to true we remove all the temp drafts (for all users)
    /// and create a new draft for the specified page
    /// </summary>
    /// <param name="pageDataId">The page data id.</param>
    /// <param name="lockIt">if set to <c>true</c> locks the page and prevents other users from editing it.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    [Obsolete("The boolean lockIt parameter is now obsolete. Use the EditPage(Guid, CultureInfo) instead.")]
    public virtual PageDraft EditPage(Guid pageDataId, bool lockIt, CultureInfo culture = null) => this.EditPage(pageDataId, culture);

    /// <summary>
    /// Edits the page.
    /// If a temp draft is available for the current user returns it for editing
    /// If the page is locked by another user and <paramref name="lockIt" /> is set to true we remove all the temp drafts (for all users)
    /// and create a new draft for the specified page
    /// </summary>
    /// <param name="pageDataId">The page data id.</param>
    /// <returns></returns>
    public virtual PageDraft EditPage(Guid pageDataId) => this.EditPage(pageDataId, (CultureInfo) null);

    /// <summary>
    /// Edits the page.
    /// If a temp draft is available for the current user returns it for editing
    /// If the page is locked by another user and <paramref name="lockIt" /> is set to true we remove all the temp drafts (for all users)
    /// and create a new draft for the specified page
    /// </summary>
    /// <param name="pageDataId">The page data id.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public virtual PageDraft EditPage(Guid pageDataId, CultureInfo culture)
    {
      PageData pageData = this.GetPageData(pageDataId);
      return this.PagesLifecycle.CheckOut(this.PagesLifecycle.GetMaster(pageData) ?? this.PagesLifecycle.Edit(pageData, culture), culture);
    }

    /// <summary>Gets the a draft used for preview of the page.</summary>
    /// <param name="id">The id of the page (PageData id).</param>
    /// <returns>The draft used for preview of the page.</returns>
    public virtual PageDraft GetPreview(Guid id)
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      PageDraft preview1 = this.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (d => d.ParentId == id && d.Owner == currentUserId && d.IsTempDraft == true)).SingleOrDefault<PageDraft>();
      if (preview1 == null)
      {
        PageData pageData = this.GetPageData(id);
        PageDraft preview2 = this.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (d => d.ParentId == id && d.IsTempDraft == false)).OrderBy<PageDraft, DateTime>((Expression<Func<PageDraft, DateTime>>) (d => d.LastModified)).FirstOrDefault<PageDraft>();
        if (preview2 != null)
          return preview2;
        if (preview2 == null)
        {
          using (new ElevatedModeRegion((IManager) this))
          {
            PageDraft draftForPage = this.CreateDraftForPage(pageData);
            pageData.Drafts.Add(draftForPage);
            return draftForPage;
          }
        }
      }
      return preview1;
    }

    /// <summary>Takes the ownership. of draft</summary>
    /// <typeparam name="T">TemplateDraft or PageDraft</typeparam>
    /// <param name="pageDraftId">The page draft id.</param>
    public void TakeDraftOwnership<T>(Guid pageDraftId) where T : DraftData
    {
      Guid guid = SecurityManager.EnsureCurrentUserIsUnrestricted();
      T draft = this.GetDraft<T>(pageDraftId);
      if ((object) draft is PageDraft)
      {
        PageDraft pageDraft = (object) draft as PageDraft;
        pageDraft.ParentPage.LockedBy = guid;
        this.DeletePageTempDrafts(pageDraft.ParentPage);
      }
      else
      {
        TemplateDraft templateDraft = (object) draft is TemplateDraft ? (object) draft as TemplateDraft : throw new ArgumentException("Unsupported draft type " + (object) typeof (T), nameof (T));
        templateDraft.ParentTemplate.LockedBy = guid;
        this.DeleteTemplateTempDrafts(templateDraft.ParentTemplate);
      }
      this.SaveChanges();
    }

    /// <summary>Deletes the specified temp drafts</summary>
    /// <param name="pageData">The page data.</param>
    public virtual void DeletePageTempDrafts(PageData pageData) => this.DeletePageTempDrafts(pageData, (CultureInfo) null);

    /// <summary>Deletes the specified temp drafts</summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="culture">The culture.</param>
    public virtual void DeletePageTempDrafts(PageData pageData, CultureInfo culture) => this.PagesLifecycle.DiscardTemp(pageData, culture);

    /// <summary>Deletes the specified temp drafts</summary>
    /// <param name="pageTemplate">The page template.</param>
    public virtual void DeleteTemplateTempDrafts(PageTemplate pageTemplate) => this.DeleteTemplateTempDrafts(pageTemplate, (CultureInfo) null);

    /// <summary>Deletes the specified temp drafts</summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <param name="culture">The culture.</param>
    public virtual void DeleteTemplateTempDrafts(PageTemplate pageTemplate, CultureInfo culture) => this.TemplatesLifecycle.DiscardTemp(pageTemplate, culture);

    /// <summary>Unlocks a page.</summary>
    /// <param name="pageDataId">The page data id.</param>
    /// <param name="takeOwnership">if set to <c>true</c> the page will be locked to the current user.</param>
    /// <param name="isNonAdminAllowed">if set to <c>true</c> the page will be unlocked, even if the current user is not an administrator. By default set to false</param>
    public void UnlockPage(Guid pageDataId, bool takeOwnership, bool isNonAdminAllowed = false) => this.UnlockPage(pageDataId, takeOwnership, (CultureInfo) null, isNonAdminAllowed);

    /// <summary>Unlocks a page.</summary>
    /// <param name="pageDataId">The page data id.</param>
    /// <param name="takeOwnership">if set to <c>true</c> the page will be locked to the current user.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="isNonAdminAllowed">if set to <c>true</c> the page will be unlocked, even if the current user is not an administrator. By default set to false</param>
    public void UnlockPage(
      Guid pageDataId,
      bool takeOwnership,
      CultureInfo culture,
      bool isNonAdminAllowed = false)
    {
      PageData pageData = this.GetPageData(pageDataId);
      Guid currentUserId = SecurityManager.CurrentUserId;
      if (!isNonAdminAllowed)
      {
        if (pageData != null && pageData.NavigationNode != null)
        {
          if (pageData.NavigationNode.IsGranted("Pages", "Unlock"))
            goto label_6;
        }
        throw new InvalidOperationException(Res.Get<PageResources>().CannotUnlockPage);
      }
      if (pageData.LockedBy != Guid.Empty && pageData.LockedBy != currentUserId)
        throw new InvalidOperationException(Res.Get<PageResources>().CannotDiscardPageDraft);
label_6:
      Guid guid = Guid.Empty;
      if (takeOwnership)
        guid = currentUserId;
      this.PagesLifecycle.DiscardTemp(pageData, culture);
      pageData.LockedBy = guid;
      pageData.SkipNotifyObjectChanged = true;
    }

    /// <summary>Unlocks a template.</summary>
    /// <param name="templateId">The template id.</param>
    /// <param name="takeOwnership">if set to <c>true</c> the template will be locked to the current user.</param>
    public void UnlockTemplate(Guid templateId, bool takeOwnership) => this.UnlockTemplate(templateId, takeOwnership, (CultureInfo) null);

    /// <summary>Unlocks a template.</summary>
    /// <param name="templateId">The template id.</param>
    /// <param name="takeOwnership">if set to <c>true</c> the template will be locked to the current user.</param>
    /// <param name="culture">The culture.</param>
    public void UnlockTemplate(Guid templateId, bool takeOwnership, CultureInfo culture)
    {
      PageTemplate template = this.GetTemplate(templateId);
      if (!template.IsGranted("PageTemplates", "Unlock"))
        throw new InvalidOperationException(Res.Get<PageResources>().CannotUnlockPageTemplate);
      Guid guid = Guid.Empty;
      if (takeOwnership)
        guid = SecurityManager.EnsureCurrentUserIsUnrestricted();
      this.TemplatesLifecycle.DiscardTemp(template, culture);
      template.LockedBy = guid;
      template.SkipNotifyObjectChanged = true;
    }

    /// <summary>Publishes the specified page draft.</summary>
    /// <param name="draftId">The draft pageId.</param>
    /// <param name="makeVisible">if set to <c>true</c> [make visible].</param>
    /// <returns>The page draft.</returns>
    public virtual PageDraft PublishPageDraft(Guid draftId, bool makeVisible)
    {
      PageDraft draft = this.GetDraft<PageDraft>(draftId);
      this.PublishPageDraft(draft, makeVisible, (CultureInfo) null);
      return draft;
    }

    /// <summary>Publishes the specified page draft.</summary>
    /// <param name="draft">The draft.</param>
    /// <param name="makeVisible">if set to <c>true</c> [make visible].</param>
    /// <summary>Publishes the page draft.</summary>
    [Obsolete("The makeVisible attribute is not used anymore. Use the PublishPageDraft(PageDraft, CultureInfo) method instead.")]
    public virtual void PublishPageDraft(PageDraft draft, bool makeVisible) => this.PublishPageDraft(draft, makeVisible, (CultureInfo) null);

    /// <summary>Publishes the specified page draft.</summary>
    /// <param name="draft">The draft.</param>
    /// <param name="makeVisible">if set to <c>true</c> [make visible].</param>
    /// <param name="culture">The culture.</param>
    /// <summary>Publishes the page draft.</summary>
    [Obsolete("The makeVisible attribute is not used anymore. Use the PublishPageDraft(PageDraft, CultureInfo) method instead.")]
    public virtual void PublishPageDraft(PageDraft draft, bool makeVisible, CultureInfo culture) => this.PublishPageDraft(draft, culture);

    /// <summary>Publishes the specified page draft.</summary>
    /// <param name="draft">The draft.</param>
    /// <summary>Publishes the page draft.</summary>
    public virtual void PublishPageDraft(PageDraft draft) => this.PublishPageDraft(draft, (CultureInfo) null);

    /// <summary>Publishes the specified page draft.</summary>
    /// <param name="draft">The draft.</param>
    /// <param name="culture">The culture.</param>
    /// <summary>Publishes the page draft.</summary>
    public virtual void PublishPageDraft(PageDraft draft, CultureInfo culture)
    {
      PageData context = this.PagesLifecycle.Publish(!draft.IsTempDraft ? draft : this.PagesLifecycle.CheckIn(draft, culture, false), culture);
      CultureInfo sitefinityCulture = culture.GetSitefinityCulture();
      string[] languages = new string[1]
      {
        sitefinityCulture == null ? (string) null : sitefinityCulture.GetLanguageKey()
      };
      context.RegisterOperation(OperationStatus.Published, languages);
    }

    /// <summary>Discards the specified page draft.</summary>
    /// <param name="draftId">The draft ID.</param>
    public virtual void DiscardPageDraft(Guid draftId) => this.DiscardPageDraft(this.GetDraft<PageDraft>(draftId));

    /// <summary>Discards the specified temp page draft.</summary>
    /// <param name="draft">The TEMP draft to discard.</param>
    public virtual void DiscardPageDraft(PageDraft draft) => this.PagesLifecycle.DiscardTemp(draft);

    /// <summary>
    /// Discards all page drafts. Any changes are canceled and next editing of page will return the published contents of the page
    /// </summary>
    /// <param name="pageId">The page id.</param>
    public virtual void DiscardAllPageDrafts(Guid pageId) => this.PagesLifecycle.DiscardAllDrafts(this.GetPageData(pageId));

    /// <summary>Merges the page changes.</summary>
    /// <param name="draftId">The draft pageId.</param>
    /// <param name="publish">if set to <c>true</c> [publish].</param>
    public virtual void MergePageChanges(Guid draftId, bool publish)
    {
      PageDraft draft = this.GetDraft<PageDraft>(draftId);
      if (draft.ParentPage.Version == draft.Version)
      {
        if (!publish)
          return;
        this.PublishPageDraft(draftId, false);
      }
      throw new NotImplementedException();
    }

    public virtual void UnpublishPage(Guid pageDataId) => this.UnpublishPage(this.GetPageData(pageDataId), (CultureInfo) null);

    public virtual void UnpublishPage(PageData page) => this.UnpublishPage(page, (CultureInfo) null);

    public virtual void UnpublishPage(PageData page, CultureInfo culture)
    {
      this.RegisterUnpublishedOperation(page, culture);
      ((ILifecycleManager<PageData, PageDraft>) this).Lifecycle.Unpublish(page, culture);
    }

    private void RegisterUnpublishedOperation(PageData page, CultureInfo culture)
    {
      if (culture == null)
        culture = culture.GetSitefinityCulture();
      if (culture != null)
        page.RegisterOperation(OperationStatus.Unpublished, new string[1]
        {
          culture.GetLanguageKey()
        });
      else
        page.RegisterOperation(OperationStatus.Unpublished);
    }

    /// <summary>Creates new page.</summary>
    /// <returns>The new page.</returns>
    public virtual PageNode CreatePageNode() => this.Provider.CreatePageNode();

    /// <summary>Gets a page with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page data item.</returns>
    public virtual PageNode GetPageNode(Guid id) => this.Provider.GetPageNode(id);

    /// <summary>Gets a query for pages.</summary>
    /// <returns>The query for pages.</returns>
    public virtual IQueryable<PageNode> GetPageNodes() => this.Provider.GetPageNodes();

    /// <summary>Creates new page with the specified ID.</summary>
    /// <param name="id">The pageId of the new page.</param>
    /// <returns>The new page.</returns>
    public virtual PageNode CreatePageNode(Guid id) => this.Provider.CreatePageNode(id);

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public virtual void Delete(PageNode item)
    {
      if (!this.Provider.SuppressSecurityChecks && item.Nodes.Count > 0 && !ClaimsManager.IsUnrestricted())
        throw new ArgumentException(Res.Get<PageResources>().PromptMessagePageCannotDeleteChildren);
      string[] availableLanguages = this.GetAvailableLanguages((object) item, false);
      item.RegisterDeletedOperation(availableLanguages);
      this.Provider.Delete(item);
    }

    /// <summary>
    /// This method is used when deleting a split page. In the current architecture, the page is not actually deleted
    /// but is marked as auto generated and its contents are cleared.
    /// </summary>
    /// <param name="pageNode">The page node to reset.</param>
    protected virtual void DeleteSplitPage(PageNode pageNode)
    {
      PageData pageData = pageNode.GetPageData();
      if (pageData == null || pageNode.LocalizationStrategy != LocalizationStrategy.Split)
        throw new ArgumentException("You can only reset a split page!");
      CultureInfo cultureInfo = new CultureInfo(pageData.Culture);
      if (pageData != null)
        this.DeleteLanguageVersion((object) pageData, cultureInfo);
      pageData.IsAutoCreated = true;
      foreach (Telerik.Sitefinity.Pages.Model.ControlData controlData in new List<PageControl>((IEnumerable<PageControl>) pageData.Controls))
        this.Delete(controlData);
      foreach (PageDraft pageDraft in new List<PageDraft>((IEnumerable<PageDraft>) pageData.Drafts))
        this.Delete(pageDraft);
      pageData.Controls.Clear();
      LocalizationHelper.ClearLstringPropertiesForLanguage((object) pageNode, cultureInfo);
      if (Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.LegacyMultilingual)
        LocalizationHelper.ClearLstringPropertiesForLanguage((object) pageNode, CultureInfo.InvariantCulture);
      LocalizationHelper.ClearLstringPropertiesForLanguage((object) pageData, cultureInfo);
      if (Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.LegacyMultilingual)
        LocalizationHelper.ClearLstringPropertiesForLanguage((object) pageNode, CultureInfo.InvariantCulture);
      foreach (UrlData urlData in new List<PageUrlData>((IEnumerable<PageUrlData>) pageNode.Urls))
        this.Delete(urlData);
    }

    /// <summary>
    /// Deletes the specified language version of the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="language">The language to delete</param>
    public override void DeleteItem(object item, CultureInfo language)
    {
      if (item != null && typeof (PageNode).IsAssignableFrom(item.GetType()))
        this.Delete((PageNode) item, language);
      else
        base.DeleteItem(item, language);
    }

    /// <summary>
    /// Deletes the specified language version of the specified page node.
    /// </summary>
    /// <param name="node">The item to delete.</param>
    /// <param name="language">The language.</param>
    public virtual void Delete(PageNode node, CultureInfo language)
    {
      string[] availableLanguages = this.GetAvailableLanguages((object) node, false);
      if (language == null || this.IsTheLastAvailableLanguage(language.Name, availableLanguages))
      {
        this.Delete(node);
      }
      else
      {
        LocalizationHelper.ClearLstringPropertiesForLanguage((object) node, language);
        using (SiteRegion.FromSiteMapRoot(node.RootNodeId))
        {
          int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(language);
          List<PageUrlData> pageUrlDataList = new List<PageUrlData>(node.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => u.Culture == cultureLcid)));
          if (Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.LegacyMultilingual)
          {
            bool isDefaultCulture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Equals((object) language);
            pageUrlDataList.AddRange(node.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => isDefaultCulture && u.Culture == CultureInfo.InvariantCulture.LCID)));
          }
          foreach (UrlData urlData in pageUrlDataList)
            this.Delete(urlData);
        }
        PageData pageData = node.LocalizationStrategy != LocalizationStrategy.Split ? node.GetPageData() : this.GetPageData(node, language);
        this.Delete(node, pageData, language);
        node.RegisterDeletedOperation(new string[1]
        {
          language.GetLanguageKey()
        });
      }
    }

    /// <summary>
    /// Deletes the specified language version of the specified page data.
    /// </summary>
    /// <param name="node">The page node.</param>
    /// <param name="pageData">The page data.</param>
    /// <param name="language">The language.</param>
    internal virtual void Delete(PageNode node, PageData pageData, CultureInfo language)
    {
      if (node.LocalizationStrategy == LocalizationStrategy.Split)
      {
        if (pageData == null)
          return;
        this.Delete(pageData);
      }
      else
      {
        CultureInfo lastLanguageLeft = (CultureInfo) null;
        CultureInfo[] availableCultures = node.AvailableCultures;
        if (((IEnumerable<CultureInfo>) availableCultures).Count<CultureInfo>() == 1)
          lastLanguageLeft = availableCultures[0];
        if (pageData == null)
          return;
        this.DeleteLanguageVersion((object) pageData, language);
        this.ClearPropertiesForLiveSyncedContainer<PageDraft>((IContentWithDrafts<PageDraft>) pageData, language, lastLanguageLeft);
      }
    }

    /// <summary>
    /// Moves the page node passed as first argument to one of the positions predefined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.MoveTo" /> enumeration
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="moveTo">The position to move to.</param>
    public virtual void MovePageNode(PageNode nodeToMove, MoveTo moveTo) => this.Provider.MovePageNode(nodeToMove, moveTo);

    /// <summary>
    /// Moves the page node passed as first argument by the specified number of places, in the direction given by the
    /// <see cref="T:Telerik.Sitefinity.Modules.Pages.Move" /> enumeration.
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="move">A value representing the direction in which the node will be moved.</param>
    /// <param name="numberOfPlaces">The number of places to move.</param>
    public virtual void MovePageNode(PageNode nodeToMove, Move move, int numberOfPlaces) => this.Provider.MovePageNode(nodeToMove, move, numberOfPlaces);

    /// <summary>
    /// Moves the page node passed as first argument to the place defined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.Place" /> enumeration,
    /// relative to the supplied target page node
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="targetNode">An instance of the page that serves as a reference point to the new placing of the page.</param>
    /// <param name="place">A value representing the place to which the page ought to be moved.</param>
    public virtual void MovePageNode(PageNode nodeToMove, PageNode targetNode, Place place) => this.Provider.MovePageNode(nodeToMove, targetNode, place);

    /// <summary>
    /// Moves the page node passed as first argument to the place defined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.Place" /> enumeration,
    /// relative to the supplied target page node
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="targetNodeId">The ID of the page that serves as a reference point to the new placing of the page.</param>
    /// <param name="place">A value representing the direction in which the node will be moved. </param>
    public virtual void MovePageNode(PageNode nodeToMove, Guid targetNodeId, Place place)
    {
      if (nodeToMove.Parent != null)
      {
        PageNode pageNode = this.GetPageNode(targetNodeId);
        if (pageNode.Parent.Id != nodeToMove.Parent.Id)
          this.ChangeParent(nodeToMove, pageNode.Parent);
      }
      this.Provider.MovePageNode(nodeToMove, targetNodeId, place);
    }

    /// <summary>
    /// Make the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> passes as first argument the child of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> passed as second argument
    /// </summary>
    /// <param name="childNode">The <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> whose parent should be changed</param>
    /// <param name="newParent">The <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> representing the new parent</param>
    /// <param name="storeCurrentUrlAsAdditional">Store current URL as additional</param>
    public virtual void ChangeParent(
      PageNode childNode,
      PageNode newParent,
      bool storeCurrentUrlAsAdditional = true)
    {
      if (childNode.Parent != null && !(childNode.Parent.Id != newParent.Id))
        return;
      CultureInfo[] cultureInfoArray = (CultureInfo[]) null;
      if (childNode.Parent != null)
      {
        cultureInfoArray = childNode.AvailableCultures;
        if (storeCurrentUrlAsAdditional && childNode.WasPublished)
        {
          foreach (CultureInfo cultureInfo in cultureInfoArray)
          {
            if (!childNode.HasCustomUrl(cultureInfo))
              this.AddAdditionalUrl(childNode, cultureInfo);
          }
        }
      }
      this.Provider.ChangeParent(childNode, newParent);
      if (cultureInfoArray == null || childNode.Urls.Count <= 0)
        return;
      string childUrl = childNode.BuildUrl(SystemManager.CurrentContext.Culture);
      foreach (CultureInfo cultureInfo in cultureInfoArray)
      {
        if (!childNode.HasCustomUrl(cultureInfo))
        {
          foreach (PageUrlData pageUrlData in this.GetUrlsForLanguage((IEnumerable<PageUrlData>) childNode.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => !u.IsDefault && u.Url == childUrl)).ToList<PageUrlData>(), cultureInfo, childNode.RootNodeId))
          {
            childNode.Urls.Remove(pageUrlData);
            this.provider.Delete((UrlData) pageUrlData);
          }
        }
      }
    }

    /// <summary>Adds the current page node URL as additional.</summary>
    /// <param name="node">The node.</param>
    /// <param name="language">The language.</param>
    /// <param name="redirectToDefault">if set to <c>true</c> [redirect to default].</param>
    protected void AddCurrentPageNodeUrlAsAdditional(
      PageNode node,
      CultureInfo language,
      bool redirectToDefault = true)
    {
      string str = node.GetFullUrl(language, true, false);
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        str = ObjectFactory.Resolve<UrlLocalizationService>().UnResolveUrlReal(str);
      this.AddPageNodeAdditionalUrl(node, language, str, redirectToDefault);
    }

    /// <summary>
    /// Adds the current page node URL as additional, switching the current UI culture before generating the url.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="language">The language.</param>
    /// <param name="redirectToDefault">if set to <c>true</c> [redirect to default].</param>
    protected virtual void AddAdditionalUrl(
      PageNode node,
      CultureInfo language,
      bool redirectToDefault = true)
    {
      if (!node.IsTranslatedIn(language))
        return;
      this.AddCurrentPageNodeUrlAsAdditional(node, language, redirectToDefault);
    }

    protected virtual void ChangeParentInternal(
      PageNode pageNode,
      PageNode parentNode,
      CultureInfo language,
      bool storeCurrentUrlAsAdditional = true)
    {
      if (storeCurrentUrlAsAdditional && pageNode.WasPublished && !pageNode.HasCustomUrl(language))
        this.AddCurrentPageNodeUrlAsAdditional(pageNode, language);
      this.Provider.ChangeParent(pageNode, parentNode);
    }

    /// <summary>Creates a new page depending on the node type.</summary>
    /// <param name="pageLocation">The page location.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="nodeType">Type of the node.</param>
    /// <returns>The new page.</returns>
    public virtual PageNode CreatePage(
      PageLocation pageLocation,
      Guid pageId,
      NodeType nodeType)
    {
      return this.CreatePage(this.GetLocationRoot(pageLocation), pageId, nodeType);
    }

    /// <summary>Creates a new page depending on the node type.</summary>
    /// <param name="parentPage">The parent page.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="nodeType">Type of the node.</param>
    /// <returns>The new page.</returns>
    public virtual PageNode CreatePage(PageNode parentPage, Guid pageId, NodeType nodeType)
    {
      if (parentPage == null)
        throw new ArgumentNullException(nameof (parentPage));
      PageNode childNode = !(pageId == Guid.Empty) ? this.CreatePageNode(pageId) : this.CreatePageNode();
      childNode.NodeType = nodeType;
      this.ChangeParent(childNode, parentPage);
      if (nodeType == NodeType.Standard)
        this.CreatePageData().NavigationNode = childNode;
      return childNode;
    }

    /// <summary>Creates new page.</summary>
    /// <returns>The new page.</returns>
    public virtual PageData CreatePageData()
    {
      PageData pageData = this.Provider.CreatePageData();
      pageData.Culture = SystemManager.CurrentContext.Culture.Name;
      return pageData;
    }

    /// <summary>Creates new page with the specified ID.</summary>
    /// <param name="id">The pageId of the new page.</param>
    /// <returns>The new page.</returns>
    public virtual PageData CreatePageData(Guid id) => this.Provider.CreatePageData(id);

    internal virtual PageData CreatePageDataLanguageVersion(
      PageData sourcePageData,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool copyDataFromSource)
    {
      PageData dataLanguageVersion = sourcePageData.NavigationNode.PageDataList.Where<PageData>((Func<PageData, bool>) (pd => pd.Culture == targetLanguage.Name)).FirstOrDefault<PageData>() ?? this.CreatePageData();
      if (sourcePageData != null)
      {
        PageDraft sourceTemp = sourcePageData.Drafts.Where<PageDraft>((Func<PageDraft, bool>) (d => !d.IsTempDraft)).OrderBy<PageDraft, DateTime>((Func<PageDraft, DateTime>) (d => d.LastModified)).FirstOrDefault<PageDraft>();
        bool copyContentsFromSource = copyDataFromSource && sourceTemp == null;
        this.CopyPageData(sourcePageData, dataLanguageVersion, (CultureInfo) null, (CultureInfo) null, copyContentsFromSource);
        if (sourceTemp != null & copyDataFromSource)
          this.CopyDraftContentsToPage(sourceTemp, dataLanguageVersion, (CultureInfo) null, (CultureInfo) null, CopyDirection.Unspecified);
        LocalizationHelper.ClearLstringPropertiesForLanguage((object) sourcePageData, targetLanguage);
        LocalizationHelper.ClearLstringPropertiesForLanguage((object) dataLanguageVersion, sourceLanguage);
        dataLanguageVersion.Culture = targetLanguage.Name;
        dataLanguageVersion.LockedBy = Guid.Empty;
        dataLanguageVersion.Visible = false;
        dataLanguageVersion.Status = ContentLifecycleStatus.Master;
        this.ClearPageDataProperties(dataLanguageVersion);
      }
      return dataLanguageVersion;
    }

    /// <summary>Gets a page with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page data item.</returns>
    public virtual PageData GetPageData(Guid id) => this.Provider.GetPageData(id);

    /// <summary>Gets a page data for the given node and culture.</summary>
    /// <param name="pageNode">The pageNode of the page data.</param>
    /// <param name="culture">The culture of the page data.</param>
    /// <param name="segmentId">The segment id.</param>
    /// <returns>A page data item.</returns>
    internal virtual PageData GetPageData(
      PageNode pageNode,
      CultureInfo culture = null,
      Guid segmentId = default (Guid))
    {
      PageData pageData = pageNode.GetPageData(culture);
      if (segmentId != Guid.Empty && pageData.IsPersonalized)
      {
        PageData pageData1 = this.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pd => pageData.Id == pd.PersonalizationMasterId && pd.PersonalizationSegmentId == segmentId)).FirstOrDefault<PageData>();
        if (pageData1 != null)
          return pageData1;
      }
      return pageData;
    }

    /// <summary>Gets a query for pages.</summary>
    /// <returns>The query for pages.</returns>
    public virtual IQueryable<PageData> GetPageDataList() => this.Provider.GetPageDataList();

    /// <summary>Gets a query for page data for the given page node.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <returns>The query for pages.</returns>
    public virtual IQueryable<PageData> GetPageDataList(PageNode pageNode)
    {
      IQueryable<PageData> source = this.Provider.GetPageDataList();
      if (pageNode != null)
        source = source.Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.NavigationNode.Id == pageNode.Id));
      return source;
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page to delete.</param>
    /// <param name="deleteOtherLanguageVersions">Delete other language versions.</param>
    public virtual void Delete(PageData item, bool deleteOtherLanguageVersions = true)
    {
      this.Provider.Delete(item);
      if (!deleteOtherLanguageVersions)
        return;
      this.ApplyActionToLanguageRelatedPages(item, (Action<PageData>) (pd => this.Provider.Delete(pd)));
    }

    /// <summary>Changes the owner of a page.</summary>
    /// <param name="node">The page node.</param>
    /// <param name="newOwnerID">The new owner ID.</param>
    public virtual void ChangeOwner(PageNode node, Guid newOwnerID) => this.Provider.ChangeOwner(node, newOwnerID);

    /// <summary>Sets the home page to the specified node</summary>
    /// <param name="pageNodeId">Node to become a home page</param>
    [Obsolete("Use SystemManager.CurrentContext.CurrentSite.SetHomePage(pageNodeId)")]
    public virtual void SetHomePage(Guid pageNodeId) => SystemManager.CurrentContext.CurrentSite.SetHomePage(pageNodeId, this);

    /// <summary>Gets the home page node id.</summary>
    /// <returns>The home page node id.</returns>
    [Obsolete("User SystemManager.CurrentContext.CurrentSite.HomePageId")]
    public virtual Guid GetHomePageNodeId() => SystemManager.CurrentContext.CurrentSite.HomePageId;

    /// <summary>
    /// Converts given page to standard page assigning specified pageData if it hasn't been set already. The method does not change the localization strategy of the page.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="pageData">The page data.</param>
    [Obsolete("Change the node type to standard instead.")]
    public virtual void ConvertToStandardPageRaw(PageNode node, PageData pageData)
    {
      if (node.GetPageData() == null)
        pageData.NavigationNode = node;
      node.NodeType = NodeType.Standard;
    }

    /// <summary>
    /// Converts given page to standard page assigning specified pageData
    /// </summary>
    /// <param name="node">The node.</param>
    public virtual void ConvertToStandardPage(PageNode node)
    {
      PageData pageData = this.CreatePageData();
      pageData.NavigationNode = node;
      this.ConvertToStandardPage(node, pageData);
    }

    /// <summary>
    /// Converts given page to standard page assigning specified pageData.
    /// The method changes the localization strategy depending on the parent and children localization strategy in multilingual
    /// </summary>
    /// <param name="node">The  page node.</param>
    /// <param name="pageData">The page data.</param>
    public virtual void ConvertToStandardPage(PageNode node, PageData pageData)
    {
      node.NodeType = NodeType.Standard;
      node.OpenNewWindow = false;
      Lstring redirectUrl = node.RedirectUrl;
      foreach (CultureInfo culture in redirectUrl.GetAvailableLanguagesIgnoringContext())
        redirectUrl[culture] = (string) null;
      node.LocalizationStrategy = LocalizationStrategy.Synced;
      pageData.Culture = SystemManager.CurrentContext.Culture.Name;
    }

    /// <summary>Checks whether the specified page has children</summary>
    /// <param name="pageNode">The page node.</param>
    /// <returns>A value indicating whether the specified page has children.</returns>
    public virtual bool PageNodeHasChildren(PageNode pageNode)
    {
      Guid parentId = pageNode.Id;
      return this.Provider.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Parent.Id == parentId)).Any<PageNode>();
    }

    /// <summary>
    /// Gets the localization strategy to be used when a page (group) is converted to a standard page.
    /// Returns NotSelected.
    /// </summary>
    /// <param name="node">The page node.</param>
    /// <returns>The result depends on the localization strategy of the parent of the page which is to be converted
    /// and the localization strategy of its children</returns>
    [Obsolete("It is useless from version 7.0 as there is no limitation of creating different types of Localization no matter of their predecessor/ancestor types.")]
    public LocalizationStrategy GetLocalizationStrategyForStandartPageConvert(
      PageNode node)
    {
      return LocalizationStrategy.NotSelected;
    }

    internal TemplateDraft CreateTemplateDraft(
      PageTemplate pageTemplate,
      CultureInfo culture = null)
    {
      return this.TemplatesLifecycle.Edit(pageTemplate, culture);
    }

    public virtual void CopyTemplateData<TControlA, TPresentationA, TControlB, TPresentationB>(
      ITemplateData<TControlA, TPresentationA> templateFrom,
      ITemplateData<TControlB, TPresentationB> templateTo,
      CopyDirection copyDirection,
      CultureInfo sourceLanguage = null,
      CultureInfo targetLanguage = null)
      where TControlA : Telerik.Sitefinity.Pages.Model.ControlData
      where TPresentationA : PresentationData
      where TControlB : Telerik.Sitefinity.Pages.Model.ControlData
      where TPresentationB : PresentationData
    {
      templateTo.LastControlId = templateFrom.LastControlId;
      if (copyDirection != CopyDirection.Unspecified)
        templateTo.Key = templateFrom.Key;
      templateTo.MasterPage = templateFrom.MasterPage;
      templateTo.IncludeScriptManager = templateFrom.IncludeScriptManager;
      this.CopyRendererData((object) templateFrom, (object) templateTo);
      LocalizationHelper.CopyLstring(templateFrom.Themes, templateTo.Themes, sourceLanguage.GetLstring(), targetLanguage.GetLstring());
      this.CopyControls<TControlA, TControlB>((IEnumerable<TControlA>) templateFrom.Controls, templateTo.Controls, sourceLanguage, targetLanguage, copyDirection);
      for (int index = templateTo.Presentation.Count - 1; index >= 0; --index)
        this.DeleteItem((object) templateTo.Presentation[index]);
      this.CopyPresentation<TPresentationA, TPresentationB>((IEnumerable<TPresentationA>) templateFrom.Presentation, templateTo.Presentation);
    }

    /// <summary>Edits the page template.</summary>
    /// <param name="pageId">The pageId of the template.</param>
    /// <param name="lockIt">if set to <c>true</c> locks the template and prevents other users from editing it.</param>
    /// <returns></returns>
    [Obsolete("The boolean lockIt parameter is now obsolete. Use the EditTemplate(Guid, CultureInfo) instead.")]
    public virtual TemplateDraft EditTemplate(Guid pageTemplateId, bool lockIt) => this.EditTemplate(pageTemplateId, lockIt, (CultureInfo) null);

    /// <summary>Edits the page template.</summary>
    /// <param name="pageTemplateId">The page template id.</param>
    /// <param name="lockIt">if set to <c>true</c> locks the template and prevents other users from editing it.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    [Obsolete("The boolean lockIt parameter is now obsolete. Use the EditTemplate(Guid, CultureInfo) instead.")]
    public virtual TemplateDraft EditTemplate(
      Guid pageTemplateId,
      bool lockIt,
      CultureInfo culture)
    {
      return this.EditTemplate(pageTemplateId, culture);
    }

    /// <summary>Edits the page template.</summary>
    /// <param name="pageTemplateId">The page template id.</param>
    /// <returns></returns>
    public virtual TemplateDraft EditTemplate(Guid pageTemplateId) => this.EditTemplate(pageTemplateId, (CultureInfo) null);

    /// <summary>Edits the page template.</summary>
    /// <param name="pageTemplateId">The page template id.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public virtual TemplateDraft EditTemplate(Guid pageTemplateId, CultureInfo culture)
    {
      PageTemplate template = this.GetTemplate(pageTemplateId);
      return this.TemplatesLifecycle.CheckOut(this.TemplatesLifecycle.GetMaster(template) ?? this.TemplatesLifecycle.Edit(template, culture), culture);
    }

    /// <summary>Publishes the template.</summary>
    /// <param name="id">The template id.</param>
    public virtual void PublishTemplateDraft(Guid id)
    {
      CultureInfo culture = (CultureInfo) null;
      this.PublishTemplateDraft(this.GetDraft<TemplateDraft>(id), culture.GetSitefinityCulture());
    }

    /// <summary>Publishes the template.</summary>
    /// <param name="draft">The draft </param>
    public virtual void PublishTemplateDraft(TemplateDraft draft)
    {
      CultureInfo culture = (CultureInfo) null;
      this.PublishTemplateDraft(draft, culture.GetSitefinityCulture());
    }

    /// <summary>Publishes the template.</summary>
    /// <param name="draft">The draft </param>
    /// <param name="culture">The culture.</param>
    public virtual void PublishTemplateDraft(TemplateDraft draft, CultureInfo culture)
    {
      TemplateDraft master = !draft.IsTempDraft ? draft : this.TemplatesLifecycle.CheckIn(draft, culture, false);
      this.TemplatesLifecycle.Publish(master, culture);
      ILifecycleDataItemLive parentItem = master.ParentItem;
      bool multilingual = DataExtensions.AppSettings.ContextSettings.Multilingual;
      string str = culture == null || !multilingual ? culture.GetLanguageKey() : culture.Name;
      string[] strArray;
      if (!string.IsNullOrEmpty(str))
        strArray = new string[1]{ str };
      else
        strArray = (string[]) null;
      string[] languages = strArray;
      this.RegisterOperation((object) parentItem, OperationStatus.Published, languages);
    }

    /// <summary>Cancels the draft.</summary>
    /// <param name="draftId">The draft pageId.</param>
    public virtual void DiscardTemplateDraft(Guid draftId) => ((ILifecycleManager<PageTemplate, TemplateDraft>) this).Lifecycle.DiscardTemp(this.GetDraft<TemplateDraft>(draftId));

    /// <summary>Merges the template changes.</summary>
    /// <param name="draftId">The draft pageId.</param>
    /// <param name="publish">if set to <c>true</c> [publish].</param>
    public virtual void MergeTemplateChanges(Guid draftId, bool publish)
    {
      TemplateDraft draft = this.GetDraft<TemplateDraft>(draftId);
      if (draft.ParentTemplate.Version == draft.Version)
      {
        if (!publish)
          return;
        this.PublishTemplateDraft(draftId);
      }
      throw new NotImplementedException();
    }

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public virtual PageTemplate CreateTemplate() => this.CreateTemplate(this.Provider.GetNewGuid());

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="id">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public virtual PageTemplate CreateTemplate(Guid id)
    {
      PageTemplate template = this.Provider.CreateTemplate(id);
      this.AssignDefaultPermissionsToTemplate(template);
      if (!this.Provider.SuppressSecurityChecks)
        template.Demand("PageTemplates", "Create");
      this.LinkTemplateToSite(template, SystemManager.CurrentContext.CurrentSite.Id);
      return template;
    }

    /// <summary>Links the template to site.</summary>
    /// <param name="template">The template.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal virtual SiteItemLink LinkTemplateToSite(PageTemplate template, Guid siteId) => this.Provider.LinkTemplateToSite(template, siteId);

    /// <summary>Assigns the default permissions to template.</summary>
    /// <param name="template">The template.</param>
    public void AssignDefaultPermissionsToTemplate(PageTemplate template) => this.Provider.CreatePermissionInheritanceAssociation(this.GetSecurityRoot() ?? throw new InvalidOperationException(string.Format(Res.Get<SecurityResources>().NoSecurityRoot, (object) typeof (PageTemplate).AssemblyQualifiedName)), (ISecuredObject) template);

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page template.</returns>
    public virtual PageTemplate GetTemplate(Guid id) => this.Provider.GetTemplate(id);

    /// <summary>
    /// Gets the template of the page with the specified page id
    /// </summary>
    /// <param name="pageId">The page id.</param>
    /// <returns>The template.</returns>
    public virtual PageTemplate GetPageTemplate(Guid pageId)
    {
      PageDataProvider provider = this.Provider;
      PageData pageData = this.GetPageData(pageId);
      bool suppressSecurityChecks = provider.SuppressSecurityChecks;
      provider.SuppressSecurityChecks = true;
      PageDraft pageDraft = pageData.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (d => !d.IsTempDraft));
      PageTemplate pageTemplate = (PageTemplate) null;
      if (pageDraft != null)
      {
        if (pageDraft.TemplateId != Guid.Empty)
          pageTemplate = provider.GetTemplate(pageDraft.TemplateId);
      }
      else
        pageTemplate = pageData.Template;
      provider.SuppressSecurityChecks = suppressSecurityChecks;
      return pageTemplate;
    }

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    public virtual IQueryable<PageTemplate> GetTemplates() => this.Provider.GetTemplates();

    /// <summary>Gets a query for page templates in a specific site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>The query for page templates.</returns>
    internal virtual IQueryable<PageTemplate> GetTemplates(Guid siteId) => this.Provider.GetTemplates(siteId);

    /// <summary>Gets the links for all templates.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    internal virtual IQueryable<SiteItemLink> GetSiteTemplateLinks() => this.Provider.GetSiteTemplateLinks();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page template to delete.</param>
    public virtual void Delete(PageTemplate item)
    {
      if (item.ChildTemplates.Count > 0)
        throw new InvalidOperationException(string.Format(Res.Get<PageResources>().TemplateUsedByOTherTemplates, (object) item.ChildTemplates.Count));
      HashSet<PageNode> pagesBasedOnTemplate = this.GetPagesBasedOnTemplate(item);
      if (pagesBasedOnTemplate.Count<PageNode>() > 0)
      {
        string message = string.Format(Res.Get<PageResources>().TemplateUsedByPages, (object) pagesBasedOnTemplate.Count<PageNode>());
        int num = pagesBasedOnTemplate.Where<PageNode>((Func<PageNode, bool>) (p => p.RootNode != null && p.RootNode.Id == NewslettersModule.standardCampaignRootNodeId)).Count<PageNode>();
        if (num > 0)
          message = num != pagesBasedOnTemplate.Count<PageNode>() ? string.Format(Res.Get<PageResources>().TemplateUsedByPagesAndCampaigns, (object) pagesBasedOnTemplate.Count<PageNode>()) : string.Format(Res.Get<PageResources>().TemplateUsedByCampaigns, (object) pagesBasedOnTemplate.Count<PageNode>());
        throw new InvalidOperationException(message);
      }
      string[] availableLanguages = this.GetAvailableLanguages((object) item, false);
      this.RegisterDeleteOperation((object) item, availableLanguages);
      this.Provider.Delete(item);
    }

    /// <summary>
    /// Deletes the specified language version of the specified page template.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <param name="language">The language.</param>
    public virtual void Delete(PageTemplate template, CultureInfo language)
    {
      if (language == null)
      {
        this.Delete(template);
      }
      else
      {
        this.Provider.FetchAllLanguagesData();
        List<CultureInfo> list = template.GetAvailableLanguagesIgnoringContext().Where<CultureInfo>((Func<CultureInfo, bool>) (ci => ci.LCID != CultureInfo.InvariantCulture.LCID)).ToList<CultureInfo>();
        if (list.Count<CultureInfo>() <= 1 && list.Contains(language) || list.Count<CultureInfo>() == 0 && language.LCID == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.LCID)
        {
          this.Delete(template);
        }
        else
        {
          LocalizationHelper.ClearLstringPropertiesForLanguage((object) template, language);
          list.Remove(language);
          CultureInfo lastLanguageLeft = (CultureInfo) null;
          if (list.Count == 1)
            lastLanguageLeft = list[0];
          this.ClearPropertiesForLiveSyncedContainer<TemplateDraft>((IContentWithDrafts<TemplateDraft>) template, language, lastLanguageLeft);
          this.RegisterDeleteOperation((object) template, new string[1]
          {
            language.Name
          });
        }
      }
    }

    internal void ValidateTemplateConstraints(
      string title,
      Guid templateId,
      Guid categoryId,
      bool duplicate = false)
    {
      int num;
      if (!duplicate && templateId == Guid.Empty)
      {
        if (this.GetTemplates().Any<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category == categoryId && t.Title == (Lstring) title)))
        {
          num = 1;
          goto label_6;
        }
      }
      if (templateId != Guid.Empty)
        num = this.GetTemplates().Any<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category == categoryId && t.Title == (Lstring) title && (duplicate || t.Id != templateId))) ? 1 : 0;
      else
        num = 0;
label_6:
      if (num != 0)
        throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<PageResources>().DuplicatedName, (object) title), (Exception) null);
    }

    /// <summary>Gets the a draft used for preview of the template.</summary>
    /// <param name="id">The id of the template.</param>
    /// <returns>The draft used for preview of the template.</returns>
    internal TemplateDraft GetTemplatePreview(Guid id)
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      TemplateDraft templatePreview = this.GetDrafts<TemplateDraft>().Where<TemplateDraft>((Expression<Func<TemplateDraft, bool>>) (d => d.ParentId == id && d.Owner == currentUserId && d.IsTempDraft == true)).SingleOrDefault<TemplateDraft>();
      if (templatePreview == null)
      {
        templatePreview = this.GetDrafts<TemplateDraft>().Where<TemplateDraft>((Expression<Func<TemplateDraft, bool>>) (d => d.ParentId == id && d.IsTempDraft == false)).OrderBy<TemplateDraft, DateTime>((Expression<Func<TemplateDraft, DateTime>>) (d => d.LastModified)).FirstOrDefault<TemplateDraft>();
        if (templatePreview == null)
        {
          PageTemplate template = this.GetTemplate(id);
          using (new ElevatedModeRegion((IManager) this))
            templatePreview = this.CreateTemplateDraft(template);
        }
      }
      return templatePreview;
    }

    /// <summary>Creates new draft page or template.</summary>
    /// <returns>The new control.</returns>
    public virtual T CreateDraft<T>() where T : DraftData => this.Provider.CreateDraft<T>();

    /// <summary>Creates new draft or page with the specified ID.</summary>
    /// <param name="id">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    public virtual T CreateDraft<T>(Guid id) where T : DraftData => this.Provider.CreateDraft<T>(id);

    /// <summary>
    /// Gets the draft page or template with the specified ID.
    /// </summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public virtual T GetDraft<T>(Guid id) where T : DraftData => this.Provider.GetDraft<T>(id);

    /// <summary>Gets the non-temp page draft.</summary>
    /// <param name="pageDataId">The page data id.</param>
    /// <returns>The non-temp page draft.</returns>
    public virtual PageDraft GetPageDraft(Guid pageDataId) => this.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (d => d.ParentId == pageDataId && d.IsTempDraft == false)).OrderBy<PageDraft, DateTime>((Expression<Func<PageDraft, DateTime>>) (d => d.LastModified)).FirstOrDefault<PageDraft>();

    /// <summary>Gets a query for draft pages or templates.</summary>
    /// <returns>The query for controls.</returns>
    public virtual IQueryable<T> GetDrafts<T>() where T : DraftData => this.Provider.GetDrafts<T>();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public virtual void Delete(DraftData item) => this.Provider.Delete(item);

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public virtual void Delete(PageDraft item) => this.Delete((DraftData) item);

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public virtual void Delete(TemplateDraft item) => this.Delete((DraftData) item);

    /// <summary>Creates new control.</summary>
    /// <param name="isBackendObject">Is backend object.</param>
    /// <returns>The new control.</returns>
    public override T CreateControl<T>(bool isBackendObject = false) => this.Provider.CreateControl<T>(isBackendObject);

    /// <summary>Creates new control with the specified ID.</summary>
    /// <param name="id">The pageId of the new control.</param>
    /// <param name="isBackendObject">Is backend object.</param>
    /// <returns>The new control.</returns>
    public override T CreateControl<T>(Guid id, bool isBackendObject = false) => this.Provider.CreateControl<T>(id, isBackendObject);

    /// <summary>Gets the control with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public override T GetControl<T>(Guid id) => this.Provider.GetControl<T>(id);

    /// <summary>Gets a query for controls.</summary>
    /// <returns>The query for controls.</returns>
    public override IQueryable<T> GetControls<T>() => this.Provider.GetControls<T>();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public override void Delete(Telerik.Sitefinity.Pages.Model.ControlData item) => this.Provider.Delete((ObjectData) item);

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public override ControlProperty CreateProperty() => this.Provider.CreateProperty();

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="id">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public override ControlProperty CreateProperty(Guid id) => this.Provider.CreateProperty(id);

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page template.</returns>
    public override ControlProperty GetProperty(Guid id) => this.Provider.GetProperty(id);

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    public override IQueryable<ControlProperty> GetProperties() => this.Provider.GetProperties();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page template to delete.</param>
    public override void Delete(ControlProperty item) => this.Provider.Delete(item);

    internal void UpdatePropertiesInPage(
      object control,
      PageDraftControl pageDraftControl,
      IEnumerable<KeyValuePair<string, string>> properties)
    {
      foreach (PropertyDescriptor property1 in TypeDescriptor.GetProperties(control))
      {
        PropertyDescriptor desc = property1;
        PropertyPersistenceAttribute attribute = (PropertyPersistenceAttribute) desc.Attributes[typeof (PropertyPersistenceAttribute)];
        if (attribute != null && attribute.PersistInPage)
        {
          PropertyDescriptor property2 = TypeDescriptor.GetProperties((object) pageDraftControl.Page)[attribute.PagePropertyName];
          if (property2 != null && properties.Any<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => x.Key == desc.Name)))
          {
            KeyValuePair<string, string> keyValuePair = properties.SingleOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => x.Key == desc.Name));
            object obj = TypeDescriptor.GetConverter(property2.PropertyType).ConvertFromString(keyValuePair.Value);
            property2.SetValue((object) pageDraftControl.Page, obj);
          }
        }
      }
    }

    /// <summary>
    /// Creates new object for storing presentation information.
    /// </summary>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T CreatePresentationItem<T>()
    {
      T presentationItem = this.Provider.CreatePresentationItem<T>();
      this.LinkPresentationItemToSite((PresentationData) presentationItem, SystemManager.CurrentContext.CurrentSite.Id);
      return presentationItem;
    }

    /// <summary>
    /// Creates new object for storing presentation information with the specified ID.
    /// </summary>
    /// <param name="id">The pageId of the new item.</param>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T CreatePresentationItem<T>(Guid id)
    {
      T presentationItem = this.Provider.CreatePresentationItem<T>(id);
      this.LinkPresentationItemToSite((PresentationData) presentationItem, SystemManager.CurrentContext.CurrentSite.Id);
      return presentationItem;
    }

    /// <summary>Links the presentation item to site.</summary>
    /// <param name="presentationData">The presentation item.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal override SiteItemLink LinkPresentationItemToSite(
      PresentationData presentationData,
      Guid siteId)
    {
      return this.Provider.LinkPresentationItemToSite(presentationData, siteId);
    }

    /// <summary>Gets the item with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T GetPresentationItem<T>(Guid id) => this.Provider.GetPresentationItem<T>(id);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <returns>The query for page templates.</returns>
    public override IQueryable<T> GetPresentationItems<T>() => this.Provider.GetPresentationItems<T>();

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>The query for page templates.</returns>
    internal override IQueryable<T> GetPresentationItems<T>(Guid siteId) => this.Provider.GetPresentationItems<T>(siteId);

    /// <summary>Gets the links for all presentation items.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    internal override IQueryable<SiteItemLink> GetSitePresentationItemLinks<T>() => this.Provider.GetSitePresentationItemLinks<T>();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(PresentationData item) => this.Provider.Delete(item);

    /// <summary>Gets the default provider for this manager.</summary>
    /// <value></value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<PagesConfig>().DefaultProvider);

    /// <summary>The name of the module that this manager belongs to.</summary>
    /// <value></value>
    public override string ModuleName => "Pages";

    /// <summary>Collection of data provider settings.</summary>
    /// <value></value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<PagesConfig>().Providers;

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static PageManager GetManager() => ManagerBase<PageDataProvider>.GetManager<PageManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static PageManager GetManager(string providerName) => ManagerBase<PageDataProvider>.GetManager<PageManager>(providerName);

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <param name="transactionName">Name of a named global transaction.</param>
    /// <returns>The manager instance.</returns>
    public static PageManager GetManager(string providerName, string transactionName) => ManagerBase<PageDataProvider>.GetManager<PageManager>(providerName, transactionName);

    static PageManager()
    {
      ManagerBase<PageDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(PageManager.Provider_Executing);
      ManagerBase<PageDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(PageManager.Provider_Executed);
    }

    /// <summary>
    /// Executes before flushing or committing Page transactions and records which content relations should be updated.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    private static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      PageDataProvider provider = sender as PageDataProvider;
      IList dirtyItems = provider.GetDirtyItems();
      if (dirtyItems.Count == 0)
        return;
      if (!(provider.GetExecutionStateData("add-content-relations") is List<IContentRelation> data1))
        data1 = new List<IContentRelation>();
      if (!(provider.GetExecutionStateData("remove-content-relations") is List<Guid> data2))
        data2 = new List<Guid>();
      if (!(provider.GetExecutionStateData("locatable_item_tracker") is ContentLocationsTracker data3))
        data3 = new ContentLocationsTracker();
      ContentWidgetResolver contentWidgetResolver = new ContentWidgetResolver();
      List<PageNode> pageNodes = new List<PageNode>();
      List<PageUrlData> urls = new List<PageUrlData>();
      List<Guid> guidList = new List<Guid>();
      foreach (object itemInTransaction in (IEnumerable) dirtyItems)
      {
        SecurityConstants.TransactionActionType dirtyItemStatus = provider.GetDirtyItemStatus(itemInTransaction);
        data3.Track(itemInTransaction, (DataProviderBase) provider);
        if (itemInTransaction is Telerik.Sitefinity.Pages.Model.ControlData widget)
        {
          switch (dirtyItemStatus)
          {
            case SecurityConstants.TransactionActionType.New:
            case SecurityConstants.TransactionActionType.Updated:
              if (widget.ObjectType != null && contentWidgetResolver.IsContentWidget(widget))
              {
                IContentRelation relationOrNull = contentWidgetResolver.GetRelationOrNull(widget);
                if (relationOrNull != null)
                {
                  data1.Add(relationOrNull);
                  break;
                }
                data2.Add(widget.Id);
                break;
              }
              break;
            case SecurityConstants.TransactionActionType.Deleted:
              if (contentWidgetResolver.IsContentRelationType(itemInTransaction.GetType().FullName))
              {
                data2.Add(((IDataItem) itemInTransaction).Id);
                break;
              }
              break;
          }
        }
        if (itemInTransaction is PageUrlData entity)
        {
          switch (dirtyItemStatus)
          {
            case SecurityConstants.TransactionActionType.New:
            case SecurityConstants.TransactionActionType.Deleted:
              urls.Add(entity);
              break;
            case SecurityConstants.TransactionActionType.Updated:
              if (provider.IsFieldDirty((object) entity, "Url") || provider.IsFieldDirty((object) entity, "IsDefault"))
                goto case SecurityConstants.TransactionActionType.New;
              else
                break;
          }
        }
        if (itemInTransaction is PageNode pageNode)
        {
          switch (dirtyItemStatus)
          {
            case SecurityConstants.TransactionActionType.New:
            case SecurityConstants.TransactionActionType.Updated:
              pageNodes.Add(pageNode);
              break;
            case SecurityConstants.TransactionActionType.Deleted:
              IContentLinksManager mappedRelatedManager = provider.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
              RelatedDataHelper.DeleteItemRelations((IDataItem) pageNode, mappedRelatedManager);
              guidList.Add(pageNode.Id);
              break;
          }
        }
        if (itemInTransaction is PageTemplate pageTemplate && dirtyItemStatus == SecurityConstants.TransactionActionType.Deleted)
        {
          PageManager.DeletePageTemplateThumbnail(provider, pageTemplate);
          ContentHelper.DeleteVersionItem(provider.GetMappedRelatedManager<Telerik.Sitefinity.Versioning.Model.Item>(string.Empty) as VersionManager, pageTemplate.Id);
        }
        if (itemInTransaction is PageData pageData && dirtyItemStatus == SecurityConstants.TransactionActionType.Deleted)
          ContentHelper.DeleteVersionItem(provider.GetMappedRelatedManager<Telerik.Sitefinity.Versioning.Model.Item>(string.Empty) as VersionManager, pageData.Id);
        if (itemInTransaction is PresentationData presentationData && dirtyItemStatus == SecurityConstants.TransactionActionType.Deleted)
          ContentHelper.DeleteVersionItem(provider.GetMappedRelatedManager<Telerik.Sitefinity.Versioning.Model.Item>(string.Empty) as VersionManager, presentationData.Id);
      }
      PageManager.SyncPageNodesDefaultUrl(provider, (IList<PageNode>) pageNodes, (IList<PageUrlData>) urls);
      if (data3.HasChanges())
        provider.SetExecutionStateData("locatable_item_tracker", (object) data3);
      if (guidList.Any<Guid>())
        provider.SetExecutionStateData("deleted_workflow_items", (object) guidList);
      provider.SetExecutionStateData("add-content-relations", (object) data1);
      provider.SetExecutionStateData("remove-content-relations", (object) data2);
    }

    /// <summary>
    /// Handles the post commit event of the Page provider and updates the content relations if necessary.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    private static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      PageDataProvider provider = sender as PageDataProvider;
      List<IContentRelation> executionStateData1 = provider.GetExecutionStateData("add-content-relations") as List<IContentRelation>;
      provider.SetExecutionStateData("add-content-relations", (object) null);
      List<Guid> executionStateData2 = provider.GetExecutionStateData("remove-content-relations") as List<Guid>;
      provider.SetExecutionStateData("remove-content-relations", (object) null);
      if (executionStateData1 != null && executionStateData1.Count > 0 || executionStateData2 != null && executionStateData2.Count > 0)
      {
        ContentRelationStatisticsClient statisticsClient = new ContentRelationStatisticsClient();
        if (statisticsClient.Service != null)
        {
          if (executionStateData1 != null)
          {
            foreach (IContentRelation contentRelation in executionStateData1)
              statisticsClient.UpdateContentRelation(contentRelation);
          }
          if (executionStateData2 != null)
          {
            foreach (Guid deletedItemId in executionStateData2)
              statisticsClient.RemoveAffectedContentRelations(deletedItemId);
          }
        }
      }
      if (provider.GetExecutionStateData("locatable_item_tracker") is ContentLocationsTracker executionStateData3)
      {
        executionStateData3.SaveChanges();
        provider.SetExecutionStateData("locatable_item_tracker", (object) null);
      }
      IApprovalWorkflowExtensions.DeleteApprovalRecords((DataProviderBase) provider);
    }

    private static void SyncPageNodesDefaultUrl(
      PageDataProvider provider,
      IList<PageNode> pageNodes,
      IList<PageUrlData> urls)
    {
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes)
      {
        using (SiteRegion.FromSiteMapRoot(pageNode.RootNodeId))
        {
          string[] array = ((IEnumerable<CultureInfo>) pageNode.Title.GetAvailableLanguages()).Select<CultureInfo, string>((Func<CultureInfo, string>) (item => item.Name)).ToArray<string>();
          string[] cultures;
          if (provider.IsLstringFieldDirty((object) pageNode, "UrlName", out cultures, array))
          {
            foreach (string name in cultures)
            {
              CultureInfo cultureByName = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByName(name);
              pageNode.UrlChanged = true;
              int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(cultureByName);
              string url = pageNode.UrlName.GetStringNoFallback(cultureByName);
              if (string.IsNullOrEmpty(url))
              {
                string str = (string) null;
                string title = pageNode.Title.GetString(cultureByName, true);
                if (!title.IsNullOrEmpty())
                  url = CommonMethods.TitleToUrl(title);
                if (!string.IsNullOrEmpty(url))
                {
                  pageNode.UrlName.SetString(cultureByName, url);
                  pageNode.Extension.SetString(cultureByName, str);
                }
              }
              if (!string.IsNullOrEmpty(url) && url.StartsWith("~"))
              {
                url += pageNode.Extension.GetString(cultureByName, true);
                PageUrlData pageUrlData = pageNode.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => (u.IsDefault || u.Url.Equals(url)) && u.Culture == cultureLcid)).FirstOrDefault<PageUrlData>();
                if (pageUrlData == null)
                {
                  pageUrlData = provider.CreateUrl<PageUrlData>();
                  pageUrlData.Parent = (IDataItem) pageNode;
                  pageUrlData.Culture = cultureLcid;
                  pageUrlData.RedirectToDefault = false;
                }
                else if (urls.Contains(pageUrlData))
                  urls.Remove(pageUrlData);
                pageUrlData.Url = url;
                pageUrlData.IsDefault = true;
              }
              else
              {
                PageUrlData pageUrlData = pageNode.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => u.IsDefault && u.Culture == cultureLcid)).FirstOrDefault<PageUrlData>();
                if (pageUrlData != null)
                {
                  if (urls.Contains(pageUrlData))
                    urls.Remove(pageUrlData);
                  pageNode.Urls.Remove(pageUrlData);
                  provider.Delete((UrlData) pageUrlData);
                }
              }
            }
          }
        }
      }
      foreach (PageUrlData url in (IEnumerable<PageUrlData>) urls)
      {
        PageUrlData urlData = url;
        SecurityConstants.TransactionActionType dirtyItemStatus = provider.GetDirtyItemStatus((object) urlData);
        PageNode node = urlData.Node;
        if (node == null)
        {
          if (dirtyItemStatus == SecurityConstants.TransactionActionType.Deleted)
            node = provider.GetOriginalValue<PageNode>((object) urlData, "Node");
          if (node == null)
            continue;
        }
        using (SiteRegion.FromSiteMapRoot(node.RootNodeId))
        {
          if (dirtyItemStatus != SecurityConstants.TransactionActionType.Deleted && urlData.IsDefault)
          {
            PageManager.UpdatePageNodeUrlInternal(node, urlData.Url, Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(urlData.Culture));
            node.UrlChanged = true;
          }
          else if (dirtyItemStatus != SecurityConstants.TransactionActionType.New)
          {
            if (provider.GetOriginalValue<bool>((object) urlData, "IsDefault"))
            {
              string stringNoFallback = node.UrlName.GetStringNoFallback(Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(urlData.Culture));
              if (!string.IsNullOrEmpty(stringNoFallback))
              {
                if (stringNoFallback.StartsWith("~"))
                {
                  if (!node.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => u.IsDefault && u.Culture == urlData.Culture)).Any<PageUrlData>())
                  {
                    node.UrlName.SetString(Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(urlData.Culture), CommonMethods.TitleToUrl((string) node.Title));
                    node.UrlChanged = true;
                  }
                }
              }
            }
          }
        }
      }
    }

    internal static void ValidateDuplicateUrl(PageDataProvider provider, PageNode node)
    {
      Dictionary<string, PageNode> checkedNodes = new Dictionary<string, PageNode>();
      PageManager.ValidateDuplicateUrl(provider, node, ref checkedNodes);
    }

    internal static void ValidateDuplicateUrl(
      PageDataProvider provider,
      PageNode node,
      ref Dictionary<string, PageNode> checkedNodes)
    {
      foreach (CultureInfo availableCulture in node.AvailableCultures)
      {
        if (!node.UrlName[availableCulture].IsNullOrEmpty())
        {
          string nodeUrl = node.BuildUrl(availableCulture, true);
          if (!nodeUrl.Equals("~/"))
            PageManager.GuardDuplicateNodeUrl(provider, node, nodeUrl, availableCulture, ref checkedNodes);
        }
      }
    }

    internal static void GuardDuplicateUrls(PageDataProvider provider, IList dirtyItems)
    {
      Dictionary<string, PageNode> checkedNodes = new Dictionary<string, PageNode>();
      HashSet<Guid> guidSet = new HashSet<Guid>();
      foreach (object dirtyItem in (IEnumerable) dirtyItems)
      {
        if (dirtyItem is PageNode pageNode && !pageNode.IsDeleted)
        {
          if (provider.IsFieldDirty((object) pageNode, "Parent"))
          {
            PageManager.ValidateDuplicateUrl(provider, pageNode, ref checkedNodes);
          }
          else
          {
            string[] cultures;
            if (provider.IsLstringFieldDirty((object) pageNode, "UrlName", out cultures))
            {
              foreach (string name in cultures)
              {
                CultureInfo cultureByName = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByName(name);
                string nodeUrl = pageNode.BuildUrl(cultureByName, true);
                PageManager.GuardDuplicateNodeUrl(provider, pageNode, nodeUrl, cultureByName, ref checkedNodes);
              }
            }
          }
          if (provider.IsFieldDirty((object) pageNode, "Urls"))
          {
            foreach (PageUrlData itemInTransaction in pageNode.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => !u.IsDefault)))
            {
              switch (provider.GetDirtyItemStatus((object) itemInTransaction))
              {
                case SecurityConstants.TransactionActionType.New:
                case SecurityConstants.TransactionActionType.Updated:
                  if (!guidSet.Contains(itemInTransaction.Id))
                  {
                    string url = itemInTransaction.Url;
                    PageManager.GuardDuplicateNodeUrl(provider, pageNode, url, Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(itemInTransaction.Culture), ref checkedNodes);
                    guidSet.Add(itemInTransaction.Id);
                    continue;
                  }
                  continue;
                default:
                  continue;
              }
            }
          }
        }
        if (dirtyItem is PageUrlData entity && provider.GetDirtyItemStatus(dirtyItem) == SecurityConstants.TransactionActionType.Updated && (provider.IsFieldDirty((object) entity, "Url") || provider.IsFieldDirty((object) entity, "IsDefault")) && !guidSet.Contains(entity.Id))
        {
          CultureInfo cultureByLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(entity.Culture);
          string url = entity.Url;
          PageManager.GuardDuplicateNodeUrl(provider, entity.Node, url, cultureByLcid, ref checkedNodes);
          guidSet.Add(entity.Id);
        }
      }
    }

    /// <summary>
    /// Checks if there is other node with the given url or a child node that will have a duplicate url
    /// and throws DuplicatePageUrlException.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="node">The node which url will be checked.</param>
    /// <param name="nodeUrl">One of the node URLs.</param>
    /// <param name="culture">The node culture.</param>
    /// <param name="checkedNodes">The checked nodes.</param>
    private static void GuardDuplicateNodeUrl(
      PageDataProvider provider,
      PageNode node,
      string nodeUrl,
      CultureInfo culture,
      ref Dictionary<string, PageNode> checkedNodes)
    {
      if (node.Parent == null)
        return;
      using (SiteRegion.FromSiteMapRoot(node.RootNodeId))
      {
        using (new CultureRegion(culture))
        {
          if (!(SiteMapBase.GetSiteMapProviderForPageNode(node) is SiteMapBase providerForPageNode))
            return;
          string key = culture.Name + "?" + nodeUrl;
          PageNode pageNode1;
          if (checkedNodes.TryGetValue(key, out pageNode1))
          {
            if (!pageNode1.Id.Equals(node.Id))
              return;
          }
          else
            checkedNodes.Add(key, node);
          bool flag = false;
          using (new SiteMapBase.SiteMapContext(providerForPageNode, provider.TransactionName))
          {
            if (providerForPageNode.FindSiteMapNodeByExactUrl(UrlPath.ResolveUrl(nodeUrl), out bool _, true) is PageSiteNode node1)
            {
              if (!(node1.Id == node.Id))
                goto label_19;
            }
            List<Tuple<string, string[]>> list = providerForPageNode.GetAdditionalUrlsByRoot(nodeUrl).Where<ISiteMapAdditionalUrl>((Func<ISiteMapAdditionalUrl, bool>) (x => x.Language == culture.Name)).Where<ISiteMapAdditionalUrl>((Func<ISiteMapAdditionalUrl, bool>) (x => x.Url.Length > nodeUrl.Length)).Select<ISiteMapAdditionalUrl, Tuple<string, string[]>>((Func<ISiteMapAdditionalUrl, Tuple<string, string[]>>) (x => new Tuple<string, string[]>(x.NodeKey, x.Url.Substring(nodeUrl.Length + 1).Split(new char[1]
            {
              '/'
            }, StringSplitOptions.RemoveEmptyEntries)))).Where<Tuple<string, string[]>>((Func<Tuple<string, string[]>, bool>) (x => (uint) x.Item2.Length > 0U)).ToList<Tuple<string, string[]>>();
            if (list.Count > 0)
            {
              PageSiteNode siteMapNodeFromKey = providerForPageNode.FindSiteMapNodeFromKey(node.Id.ToString()) as PageSiteNode;
              node1 = PageManager.CheckChildNodes(providerForPageNode, (IList<Tuple<string, string[]>>) list, siteMapNodeFromKey, culture);
              if (node1 != null)
                flag = true;
            }
          }
label_19:
          if (node1 == null || !(node1.Id != node.Id))
            return;
          PageNode pageNode2 = PageManager.GetManager(node1.PageProviderName).GetPageNode(node1.Id);
          if (pageNode2 == null)
            return;
          string duplicateMessage;
          string url;
          if (!flag)
          {
            duplicateMessage = Res.Get<PageResources>().PageDuplicateMessage;
            url = pageNode2.GetFullTitlesPath(" > ");
          }
          else
          {
            duplicateMessage = Res.Get<PageResources>().ChildPageDuplicateMessage;
            url = node1.GetLiveUrl();
          }
          PageManager.ThrowDuplicatePageUrlException(node, duplicateMessage, url);
        }
      }
    }

    private static void ThrowDuplicatePageUrlException(
      PageNode node,
      string messageFormat,
      string url)
    {
      DuplicatePageUrlException pageUrlException = new DuplicatePageUrlException(node, string.Format(messageFormat, (object) url));
      pageUrlException.Data.Add((object) "Url", (object) url);
      throw pageUrlException;
    }

    private static PageSiteNode CheckChildNodes(
      SiteMapBase sitemap,
      IList<Tuple<string, string[]>> splitUrls,
      PageSiteNode parent,
      CultureInfo culture)
    {
      if (parent == null)
        return (PageSiteNode) null;
      foreach (Tuple<string, string[]> splitUrl in (IEnumerable<Tuple<string, string[]>>) splitUrls)
      {
        string[] segments = splitUrl.Item2;
        string expectedExtension;
        int extensionSegmentIndex;
        sitemap.ResolveExtension(segments, out expectedExtension, out extensionSegmentIndex);
        int regularNodesCount;
        PageSiteNode node = sitemap.ResolveNode(segments, parent, out regularNodesCount);
        if (regularNodesCount >= segments.Length && !node.Key.Equals(splitUrl.Item1, StringComparison.OrdinalIgnoreCase))
        {
          SiteMapNode specificLanguage = sitemap.FindSiteMapNodeForSpecificLanguage((SiteMapNode) node, culture);
          if (specificLanguage != null && (extensionSegmentIndex == -1 && node.Extension.IsNullOrEmpty() || extensionSegmentIndex != -1 && node.Extension.Equals(expectedExtension, StringComparison.CurrentCultureIgnoreCase)))
            return specificLanguage as PageSiteNode;
        }
      }
      return (PageSiteNode) null;
    }

    private static void DeletePageTemplateThumbnail(
      PageDataProvider provider,
      PageTemplate pageTemplate)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PageManager.\u003C\u003Ec__DisplayClass175_0 displayClass1750 = new PageManager.\u003C\u003Ec__DisplayClass175_0();
      // ISSUE: reference to a compiler-generated field
      displayClass1750.pageTemplate = pageTemplate;
      IContentLinksManager mappedRelatedManager = provider.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
      string childItemProviderName = "SystemLibrariesProvider";
      int? totalCount = new int?();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      List<Telerik.Sitefinity.Libraries.Model.Image> list = Queryable.Cast<Telerik.Sitefinity.Libraries.Model.Image>(RelatedDataHelper.GetRelatedItems(displayClass1750.pageTemplate.GetType().FullName, provider.Name, displayClass1750.pageTemplate.Id, PageTemplate.ThumbnailFieldName, new ContentLifecycleStatus?(), string.Empty, string.Empty, new int?(0), new int?(0), ref totalCount, typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName, childItemProviderName)).ToList<Telerik.Sitefinity.Libraries.Model.Image>();
      foreach (Telerik.Sitefinity.Libraries.Model.Image image in list)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PageManager.\u003C\u003Ec__DisplayClass175_1 displayClass1751 = new PageManager.\u003C\u003Ec__DisplayClass175_1();
        // ISSUE: reference to a compiler-generated field
        displayClass1751.CS\u0024\u003C\u003E8__locals1 = displayClass1750;
        // ISSUE: reference to a compiler-generated field
        displayClass1751.relatedThumbnail = image;
        if (list != null)
        {
          int num;
          // ISSUE: reference to a compiler-generated field
          if (!(displayClass1751.relatedThumbnail.Id == PageTemplateHelper.DefaultTemplateThumbnailImageId))
          {
            ParameterExpression parameterExpression;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method reference
            // ISSUE: method reference
            num = mappedRelatedManager.GetContentLinks().Where<ContentLink>(Expression.Lambda<Func<ContentLink, bool>>((Expression) Expression.AndAlso(link.ComponentPropertyName == PageTemplate.ThumbnailFieldName && link.ChildItemId == displayClass1751.relatedThumbnail.Id && link.ParentItemId != displayClass1751.CS\u0024\u003C\u003E8__locals1.pageTemplate.Id, (Expression) Expression.Equal(link.ParentItemType, (Expression) Expression.Property((Expression) Expression.Call(displayClass1751.CS\u0024\u003C\u003E8__locals1.pageTemplate, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.GetType)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Type.get_FullName))))), parameterExpression)).Any<ContentLink>() ? 1 : 0;
          }
          else
            num = 1;
          if (num == 0)
          {
            LibrariesManager librariesManager = LibrariesManager.GetSystemProviderLibrariesManager();
            // ISSUE: reference to a compiler-generated field
            librariesManager.DeleteImage(displayClass1751.relatedThumbnail);
            librariesManager.SaveChanges();
          }
          ParameterExpression parameterExpression1;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          // ISSUE: field reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          // ISSUE: field reference
          // ISSUE: method reference
          // ISSUE: field reference
          ContentLink contentLink = mappedRelatedManager.GetContentLinks().Where<ContentLink>(Expression.Lambda<Func<ContentLink, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.Equal(link.ParentItemType, (Expression) Expression.Property((Expression) Expression.Call(displayClass1751.CS\u0024\u003C\u003E8__locals1.pageTemplate, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.GetType)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Type.get_FullName)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ParentItemId))), (Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) displayClass1751, typeof (PageManager.\u003C\u003Ec__DisplayClass175_1)), FieldInfo.GetFieldFromHandle(__fieldref (PageManager.\u003C\u003Ec__DisplayClass175_1.CS\u0024\u003C\u003E8__locals1))), FieldInfo.GetFieldFromHandle(__fieldref (PageManager.\u003C\u003Ec__DisplayClass175_0.pageTemplate))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PageTemplate.get_Id))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ParentItemProviderName))), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IDataItemExtensions.GetProviderName)), (Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) displayClass1751, typeof (PageManager.\u003C\u003Ec__DisplayClass175_1)), FieldInfo.GetFieldFromHandle(__fieldref (PageManager.\u003C\u003Ec__DisplayClass175_1.CS\u0024\u003C\u003E8__locals1))), FieldInfo.GetFieldFromHandle(__fieldref (PageManager.\u003C\u003Ec__DisplayClass175_0.pageTemplate)))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ComponentPropertyName))), (Expression) Expression.Field((Expression) null, FieldInfo.GetFieldFromHandle(__fieldref (PageTemplate.ThumbnailFieldName))))), parameterExpression1)).FirstOrDefault<ContentLink>();
          if (contentLink != null)
          {
            mappedRelatedManager.Delete(contentLink);
            mappedRelatedManager.SaveChanges();
          }
        }
      }
    }

    /// <summary>
    /// Sets the specified script references for the current <see cref="T:System.Web.UI.ScriptManager" /> and returns an instance of the manager.
    /// </summary>
    /// <param name="page">The page instance to retrieve the <see cref="T:System.Web.UI.ScriptManager" /> from.</param>
    /// <param name="scripts">The scripts.</param>
    /// <returns>
    /// The current <see cref="T:System.Web.UI.ScriptManager" /> instance for the selected <see cref="T:System.Web.UI.Page" /> object.
    /// </returns>
    public static ScriptManager ConfigureScriptManager(Page page, ScriptRef scripts) => PageManager.ConfigureScriptManager(page, scripts, true);

    /// <summary>
    /// Sets the specified script references for the current <see cref="T:System.Web.UI.ScriptManager" /> and returns an instance of the manager.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="scripts">The scripts.</param>
    /// <param name="throwException">if set to <c>true</c> throw exception if there is no ScriptManager in the page.</param>
    /// <returns>An instance of <see cref="T:System.Web.UI.ScriptManager" />.</returns>
    public static ScriptManager ConfigureScriptManager(
      Page page,
      ScriptRef scripts,
      bool throwException)
    {
      ScriptManager scriptManager1 = page != null ? ScriptManager.GetCurrent(page) : throw new ArgumentNullException(nameof (page));
      if (scriptManager1 != null)
      {
        if (Bootstrapper.IsSystemInitialized)
        {
          PageManager.ForceScriptManagerToServeScriptsOverHttps(ref scriptManager1);
          ScriptManagerElement scriptManager2 = Config.Get<PagesConfig>().ScriptManager;
          if (string.IsNullOrEmpty(scriptManager1.ScriptPath))
            scriptManager1.ScriptPath = scriptManager2.ScriptPath;
          if (!page.IsBackend())
          {
            scriptManager1.EnableCdn = scriptManager2.EnableCdn;
            bool? scriptLocalization = scriptManager2.EnableScriptLocalization;
            if (scriptLocalization.HasValue)
            {
              ScriptManager scriptManager3 = scriptManager1;
              scriptLocalization = scriptManager2.EnableScriptLocalization;
              int num = scriptLocalization.Value ? 1 : 0;
              scriptManager3.EnableScriptLocalization = num != 0;
            }
          }
          PageManager.SetScriptReferences((ICollection<ScriptReference>) scriptManager1.Scripts, scriptManager2, scripts);
        }
      }
      else if (throwException)
        throw new HttpException(Res.Get<ErrorMessages>().ScriptManagerIsNull);
      return scriptManager1;
    }

    private static void ForceScriptManagerToServeScriptsOverHttps(ref ScriptManager scriptManager)
    {
      ScriptManagerElement scriptManager1 = Config.Get<PagesConfig>().ScriptManager;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || currentHttpContext.Request == null || !scriptManager1.EnableCdn || !UrlPath.IsSecuredConnection(currentHttpContext) || currentHttpContext.Request.IsSecureConnection)
        return;
      Type type = scriptManager.GetType();
      if (scriptManager is RadScriptManager)
      {
        RadScriptManager radScriptManager = (RadScriptManager) scriptManager;
        PropertyInfo property = radScriptManager.GetType().GetProperty("TelerikCdn", BindingFlags.Instance | BindingFlags.NonPublic);
        if (property != (PropertyInfo) null)
        {
          object obj1 = property.GetValue((object) radScriptManager, new object[0]);
          object obj2 = obj1.GetType().GetProperty("Request", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj1, new object[0]);
          ((IEnumerable<FieldInfo>) obj2.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)).First<FieldInfo>().SetValue(obj2, (object) true);
        }
        type = scriptManager.GetType().BaseType;
      }
      FieldInfo field = type.GetField("_isSecureConnection", BindingFlags.Instance | BindingFlags.NonPublic);
      if (!(field != (FieldInfo) null))
        return;
      field.SetValue((object) scriptManager, (object) true);
    }

    /// <summary>Gets a list of script references.</summary>
    /// <param name="scripts">The scripts.</param>
    /// <returns>A list of script references.</returns>
    public static ScriptReferenceCollection GetScriptReferences(
      ScriptRef scripts)
    {
      ScriptReferenceCollection list = new ScriptReferenceCollection();
      PageManager.SetScriptReferences((ICollection<ScriptReference>) list, Config.Get<PagesConfig>().ScriptManager, scripts);
      return list;
    }

    private static void SetScriptReferences(
      ICollection<ScriptReference> list,
      ScriptManagerElement config,
      ScriptRef scripts)
    {
      if (config == null)
        return;
      ConfigElementDictionary<string, ScriptReferenceElement> scriptReferences = config.ScriptReferences;
      bool enableCdn = config.EnableCdn;
      bool debug = config.Debug;
      if ((scripts & ScriptRef.JQuery) == ScriptRef.JQuery)
      {
        PageManager.AddReference(list, scriptReferences["JQuery"], enableCdn, debug);
        PageManager.AddReference(list, scriptReferences["JQueryMigrate"], enableCdn, debug);
      }
      if ((scripts & ScriptRef.JQueryValidate) == ScriptRef.JQueryValidate)
        PageManager.AddReference(list, scriptReferences["JQueryValidate"], enableCdn, debug);
      if ((scripts & ScriptRef.JQueryUI) == ScriptRef.JQueryUI)
        PageManager.AddReference(list, scriptReferences["JQueryUI"], enableCdn, debug);
      if ((scripts & ScriptRef.JQueryFancyBox) == ScriptRef.JQueryFancyBox)
        PageManager.AddReference(list, scriptReferences["JQueryFancyBox"], enableCdn, debug);
      if ((scripts & ScriptRef.JQueryCookie) == ScriptRef.JQueryCookie)
        PageManager.AddReference(list, scriptReferences["JQueryCookie"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjax) == ScriptRef.MicrosoftAjax)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjax"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxWebForms) == ScriptRef.MicrosoftAjaxWebForms)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxWebForms"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxApplicationServices) == ScriptRef.MicrosoftAjaxApplicationServices)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxApplicationServices"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxComponentModel) == ScriptRef.MicrosoftAjaxComponentModel)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxComponentModel"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxCore) == ScriptRef.MicrosoftAjaxCore)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxCore"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxGlobalization) == ScriptRef.MicrosoftAjaxGlobalization)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxGlobalization"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxHistory) == ScriptRef.MicrosoftAjaxHistory)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxHistory"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxNetwork) == ScriptRef.MicrosoftAjaxNetwork)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxNetwork"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxSerialization) == ScriptRef.MicrosoftAjaxSerialization)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxSerialization"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxTemplates) == ScriptRef.MicrosoftAjaxTemplates)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxTemplates"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxTimer) == ScriptRef.MicrosoftAjaxTimer)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxTimer"], enableCdn, debug);
      if ((scripts & ScriptRef.MicrosoftAjaxWebServices) == ScriptRef.MicrosoftAjaxWebServices)
        PageManager.AddReference(list, scriptReferences["MicrosoftAjaxWebServices"], enableCdn, debug);
      if ((scripts & ScriptRef.Mootools) == ScriptRef.Mootools)
        PageManager.AddReference(list, scriptReferences["Mootools"], enableCdn, debug);
      if ((scripts & ScriptRef.Prototype) == ScriptRef.Prototype)
        PageManager.AddReference(list, scriptReferences["Prototype"], enableCdn, debug);
      if ((scripts & ScriptRef.DialogManager) == ScriptRef.DialogManager)
        PageManager.AddReference(list, scriptReferences["DialogManager"], enableCdn, debug);
      if ((scripts & ScriptRef.TelerikSitefinity) == ScriptRef.TelerikSitefinity)
        PageManager.AddReference(list, scriptReferences["TelerikSitefinity"], enableCdn, debug);
      if ((scripts & ScriptRef.QueryString) == ScriptRef.QueryString)
        PageManager.AddReference(list, scriptReferences["QueryString"], enableCdn, debug);
      if ((scripts & ScriptRef.KendoAll) == ScriptRef.KendoAll)
        PageManager.AddReference(list, scriptReferences["KendoAll"], enableCdn, debug);
      if ((scripts & ScriptRef.KendoWeb) == ScriptRef.KendoWeb)
        PageManager.AddReference(list, scriptReferences["KendoWeb"], enableCdn, debug);
      if ((scripts & ScriptRef.KendoTimezones) == ScriptRef.KendoTimezones)
        PageManager.AddReference(list, scriptReferences["KendoTimezones"], enableCdn, debug);
      if ((scripts & ScriptRef.DecJsClient) == ScriptRef.DecJsClient)
        PageManager.AddReference(list, scriptReferences["DecJsClient"], enableCdn, debug);
      if ((scripts & ScriptRef.AngularJS) != ScriptRef.AngularJS)
        return;
      PageManager.AddReference(list, scriptReferences["AngularJS"], enableCdn, debug);
    }

    private static void AddReference(
      ICollection<ScriptReference> list,
      ScriptReferenceElement element,
      bool useCdnGlobal,
      bool debug)
    {
      bool flag1 = useCdnGlobal;
      bool flag2 = !ControlExtensions.IsBackend() && !SystemManager.IsDesignMode && !SystemManager.IsPreviewMode;
      if (element.EnableCdn.HasValue & flag2)
        flag1 = element.EnableCdn.Value;
      RadScriptReference script;
      if (((!flag1 ? 0 : (!string.IsNullOrEmpty(element.Path) ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
      {
        script = new RadScriptReference();
        if (debug)
          script.Path = string.IsNullOrEmpty(element.DebugPath) ? element.Path : element.DebugPath;
        else
          script.Path = element.Path;
        script.IgnoreScriptPath = element.IgnoreScriptPath;
        script.OutputPosition = element.OutputPosition;
        if (element.Key == "MicrosoftAjax" || element.Key == "MicrosoftAjaxWebForms")
        {
          script.Name = element.Key + ".js";
          script.ScriptMode = ScriptMode.Release;
          PageManager.AddUniqueScript(list, (ScriptReference) script, (Func<ScriptReference, bool>) (s => s.Name == script.Name));
        }
        else
          PageManager.AddUniqueScript(list, (ScriptReference) script, (Func<ScriptReference, bool>) (s => s.Path == script.Path));
      }
      else
      {
        string str = element.Name;
        if (debug && !string.IsNullOrEmpty(element.DebugName))
          str = element.DebugName;
        RadScriptReference radScriptReference = new RadScriptReference();
        radScriptReference.Name = str;
        radScriptReference.Assembly = element.Assembly;
        radScriptReference.IgnoreScriptPath = element.IgnoreScriptPath;
        radScriptReference.Combine = element.Combine;
        radScriptReference.OutputPosition = element.OutputPosition;
        script = radScriptReference;
        PageManager.AddUniqueScript(list, (ScriptReference) script, (Func<ScriptReference, bool>) (s => s.Name == script.Name));
      }
    }

    private static void AddUniqueScript(
      ICollection<ScriptReference> list,
      ScriptReference script,
      Func<ScriptReference, bool> comparer)
    {
      IEnumerable<ScriptReference> source = list.Where<ScriptReference>(comparer);
      if (source == null || source.Count<ScriptReference>() > 0)
        return;
      list.Add(script);
    }

    /// <summary>Saves the page draft.</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public PageDraft SavePageDraft(Guid id) => this.SavePageDraft(this.GetDraft<PageDraft>(id), (CultureInfo) null);

    /// <summary>Saves the page draft.</summary>
    /// <param name="draft">The draft.</param>
    /// <returns></returns>
    public PageDraft SavePageDraft(PageDraft draft) => this.SavePageDraft(draft, (CultureInfo) null);

    /// <summary>Saves the page draft.</summary>
    /// <param name="draft">The draft.</param>
    /// <returns></returns>
    public PageDraft SavePageDraft(PageDraft draft, CultureInfo culture) => this.PagesLifecycle.CheckIn(draft, culture, false);

    /// <summary>Saves the template draft.</summary>
    /// <param name="id">The id.</param>
    public TemplateDraft SaveTemplateDraft(Guid id) => this.SaveTemplateDraft(id, (CultureInfo) null);

    /// <summary>Saves the template draft.</summary>
    /// <param name="id">The id.</param>
    /// <param name="culture">The culture.</param>
    public TemplateDraft SaveTemplateDraft(Guid id, CultureInfo culture) => this.SaveTemplateDraft(this.GetDraft<TemplateDraft>(id), culture);

    /// <summary>Saves the template draft.</summary>
    /// <param name="draft">The template draft.</param>
    public TemplateDraft SaveTemplateDraft(TemplateDraft draft) => this.SaveTemplateDraft(draft, (CultureInfo) null);

    /// <summary>Saves the template draft.</summary>
    /// <param name="culture">The culture.</param>
    /// <param name="draft">The template draft.</param>
    public TemplateDraft SaveTemplateDraft(TemplateDraft draft, CultureInfo culture) => this.TemplatesLifecycle.CheckIn(draft, culture, false);

    /// <summary>Gets the shareable types.</summary>
    Type[] IMultisiteEnabledManager.GetShareableTypes() => new Type[2]
    {
      typeof (PageTemplate),
      typeof (ControlPresentation)
    };

    /// <summary>Gets the site specific types.</summary>
    Type[] IMultisiteEnabledManager.GetSiteSpecificTypes() => new Type[0];

    /// <summary>
    /// Gets the strategy that encapsulates the Recycle Bin operations like moving an item to, and restoring from the Recycle Bin.
    /// </summary>
    public IRecycleBinStrategy RecycleBin
    {
      get
      {
        if (this.recycleBin == null)
          this.recycleBin = RecycleBinStrategyFactory.CreateRecycleBin((IManager) this);
        return this.recycleBin;
      }
    }

    public override IQueryable<TItem> GetItems<TItem>()
    {
      if (typeof (TemplateDraft).IsAssignableFrom(typeof (TItem)))
        return this.GetDrafts<TemplateDraft>() as IQueryable<TItem>;
      if (typeof (PageDraft).IsAssignableFrom(typeof (PageDraft)))
        return this.GetDrafts<PageDraft>() as IQueryable<TItem>;
      if (typeof (PageNode).IsAssignableFrom(typeof (TItem)))
        return this.GetPageNodes() as IQueryable<TItem>;
      if (typeof (PageTemplate).IsAssignableFrom(typeof (TItem)))
        return this.GetTemplates() as IQueryable<TItem>;
      if (typeof (PageData).IsAssignableFrom(typeof (TItem)))
        return this.GetPageDataList() as IQueryable<TItem>;
      throw new ArgumentException("Unable to cast TItem to TemplateDraft or PageDraft or PageNode or PageTemplate or PageData");
    }

    /// <summary>
    /// Returns all pages that are based on the given template.
    /// </summary>
    /// <param name="template">The template to get pages for.</param>
    /// <param name="excludeNonExistingLanguages">Whether to return pages that are in languages that are not defined in the settings.</param>
    /// <returns>All pages that are based on the given template.</returns>
    public virtual HashSet<PageNode> GetPagesBasedOnTemplate(
      PageTemplate template,
      bool excludeNonExistingLanguages = true)
    {
      HashSet<PageNode> pagesBasedOnTemplate = new HashSet<PageNode>();
      IEnumerable<PageNode> source = this.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.Template.Id == template.Id)).ToList<PageData>().Select<PageData, PageNode>((Func<PageData, PageNode>) (p => p.NavigationNode)).AsEnumerable<PageNode>();
      if (excludeNonExistingLanguages)
      {
        IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
        if (appSettings.Multilingual)
        {
          IEnumerable<string> definedCultures = ((IEnumerable<CultureInfo>) appSettings.DefinedFrontendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name));
          source = source.Where<PageNode>((Func<PageNode, bool>) (p => p.LocalizationStrategy != LocalizationStrategy.Split ? ((IEnumerable<string>) p.AvailableLanguages).Intersect<string>(definedCultures).Count<string>() > 0 : definedCultures.Contains<string>(p.GetPageData().Culture)));
        }
      }
      foreach (PageNode pageNode in source)
      {
        if (pageNode != null)
          pagesBasedOnTemplate.Add(pageNode);
      }
      Guid templateId = template.Id;
      IQueryable<PageDraft> drafts = this.GetDrafts<PageDraft>();
      Expression<Func<PageDraft, bool>> predicate = (Expression<Func<PageDraft, bool>>) (p => p.TemplateId == templateId && p.IsTempDraft == false);
      foreach (PageDraft pageDraft in (IEnumerable<PageDraft>) drafts.Where<PageDraft>(predicate))
      {
        PageNode node = pageDraft.ParentPage.NavigationNode;
        if (source.Count<PageNode>((Func<PageNode, bool>) (n => n.Id == node.Id)) == 0 && node != null)
          pagesBasedOnTemplate.Add(node);
      }
      return pagesBasedOnTemplate;
    }

    /// <summary>
    /// Sets the additional urls for the specified page node. Deletes all additional URLs for the node that are not
    /// in the given list. Creates all URLs from the given list that do not exist.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="language">The language.</param>
    /// <param name="additionalPageURLs">The additional page URLs.</param>
    protected internal virtual bool SetPageNodeAdditionalUrls(
      PageNode node,
      CultureInfo language,
      string[] additionalPageURLs)
    {
      if (node.RootNode == null)
        return false;
      using (SiteRegion.FromSiteMapRoot(node.RootNodeId))
      {
        IEnumerable<PageUrlData> urlsForLanguage = this.GetUrlsForLanguage(node.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (url => !url.IsDefault)), language, node.RootNode.Id);
        IEnumerable<string> allExistingUrlNames = urlsForLanguage.Select<PageUrlData, string>((Func<PageUrlData, string>) (url => url.Url));
        List<PageUrlData> list1 = urlsForLanguage.Where<PageUrlData>((Func<PageUrlData, bool>) (url => !((IEnumerable<string>) additionalPageURLs).Contains<string>(url.Url))).ToList<PageUrlData>();
        List<string> list2 = ((IEnumerable<string>) additionalPageURLs).Where<string>((Func<string, bool>) (url => !allExistingUrlNames.Contains<string>(url))).ToList<string>();
        foreach (PageUrlData pageUrlData in list1)
        {
          node.Urls.Remove(pageUrlData);
          this.Provider.Delete((UrlData) pageUrlData);
        }
        bool flag = false;
        foreach (string urlString in list2)
          flag = this.AddPageNodeAdditionalUrl(node, language, urlString, node.AdditionalUrlsRedirectToDefaultOne);
        return flag;
      }
    }

    /// <summary>
    /// Adds the URL to the additional URLs for the given page node.
    /// </summary>
    /// <param name="node">The page node.</param>
    /// <param name="language">The language of the additional URL.</param>
    /// <param name="urlString">The URL as a string.</param>
    public bool AddPageNodeAdditionalUrl(
      PageNode node,
      CultureInfo language,
      string urlString,
      bool redirect)
    {
      if (node.RootNode == null)
        return false;
      if (!this.IsValidPageNodeAdditionalUrl(urlString))
        throw new ArgumentException("The specified URL is not valid: " + urlString);
      bool flag = false;
      using (SiteRegion.FromSiteMapRoot(node.RootNodeId))
      {
        int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(language);
        List<int> intList = new List<int>();
        intList.Add(cultureLcid);
        IEnumerable<PageUrlData> urlsForLanguage = this.GetUrlsForLanguage(node.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (url => url.Url == urlString)).AsEnumerable<PageUrlData>(), language, node.RootNode.Id);
        foreach (int num in intList)
        {
          int langId = num;
          PageUrlData pageUrlData = urlsForLanguage.Where<PageUrlData>((Func<PageUrlData, bool>) (u => u.Culture == langId)).SingleOrDefault<PageUrlData>();
          if (pageUrlData == null)
          {
            PageUrlData url = this.Provider.CreateUrl<PageUrlData>();
            url.Url = urlString;
            url.Culture = langId;
            url.Parent = (IDataItem) node;
            url.RedirectToDefault = redirect;
            if (!node.Urls.Contains(url))
              node.Urls.Add(url);
            flag = true;
          }
          else
          {
            if (pageUrlData.IsDefault)
              pageUrlData.IsDefault = false;
            if (pageUrlData.RedirectToDefault != redirect)
              flag = true;
            pageUrlData.RedirectToDefault = redirect;
          }
        }
      }
      return flag;
    }

    /// <summary>
    /// Removes the specified URL from the additional URLs for the given page node.
    /// </summary>
    /// <param name="node">The page node.</param>
    /// <param name="language">The language of the additional URL.</param>
    /// <param name="urlString">The URL as a string.</param>
    public virtual void RemovePageNodeAdditionalUrl(
      PageNode node,
      CultureInfo language,
      string urlString)
    {
      int lcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(language);
      PageUrlData pageUrlData = node.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (url => url.Url == urlString && url.Culture == lcid)).SingleOrDefault<PageUrlData>();
      if (pageUrlData == null)
        return;
      node.Urls.Remove(pageUrlData);
      this.Provider.Delete((UrlData) pageUrlData);
    }

    /// <summary>
    /// Determines whether the specified URL is a valid additional URL for a page node. This method only checks if the string is valid URL,
    /// it does not check for duplicates or other logic.
    /// </summary>
    /// <param name="url">The URL to check.</param>
    /// <returns>
    ///     <c>true</c> if the specified URL is a valid additional URL for a page node; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsValidPageNodeAdditionalUrl(string url) => new Regex(DefinitionsHelper.UrlRegularExpressionFilterForAdditionalPagesUrlsValidator).IsMatch(url);

    public virtual bool IsValidPageNodeUrl(string url) => new Regex(DefinitionsHelper.UrlRegularExpressionFilterForContentValidator).IsMatch(url);

    /// <summary>
    /// Checks if the specified URL has duplicates among the additional URLs for all nodes under the specified root node.
    /// </summary>
    /// <param name="checkedUrl">The checked URL.</param>
    /// <param name="language">The language.</param>
    /// <param name="rootNode">The root node.</param>
    /// <returns>True if the URL has duplicates; otherwise false.</returns>
    public virtual bool CheckAdditionalUrlsForDuplicates(
      string checkedUrl,
      CultureInfo language,
      PageNode rootNode)
    {
      return this.GetDuplicateAdditionalUrls(checkedUrl, language, rootNode).Any<PageUrlData>();
    }

    /// <summary>
    /// Returns the URLs duplicate to the specified URL among the additional URLs for all nodes under the specified root node.
    /// </summary>
    /// <param name="checkedUrl">The checked URL.</param>
    /// <param name="language">The language.</param>
    /// <param name="rootNode">The root node.</param>
    /// <returns></returns>
    public virtual IEnumerable<PageUrlData> GetDuplicateAdditionalUrls(
      string checkedUrl,
      CultureInfo language,
      PageNode rootNode)
    {
      Guid siteMapRootNodeId = rootNode.RootNodeId;
      return this.GetUrlsForLanguage(this.Provider.GetUrls<PageUrlData>().Where<PageUrlData>((Expression<Func<PageUrlData, bool>>) (url => url.Url == checkedUrl && url.Node.RootNodeId == siteMapRootNodeId)).AsEnumerable<PageUrlData>(), language, rootNode.Id);
    }

    public static void RenderPage(HttpContext context, Guid pageNodeId, Action<Page> pageAction) => PageManager.RenderPage((HttpContextBase) new HttpContextWrapper(context), pageNodeId, pageAction);

    public static void RenderPage(
      HttpContextBase context,
      Guid pageNodeId,
      Action<Page> pageAction)
    {
      PageSiteNode siteMapNodeFromKey = (PageSiteNode) BackendSiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(pageNodeId.ToString(), false);
      RouteHelper.SslRedirectIfNeeded(context, siteMapNodeFromKey);
      RouteValueDictionary dataTokens = context.Request.RequestContext.RouteData.DataTokens;
      if (!dataTokens.ContainsKey("SiteMapNode"))
        dataTokens.Add("SiteMapNode", (object) siteMapNodeFromKey);
      RouteHelper.ApplyThreadCulturesForCurrentUser();
      Page handler = Telerik.Sitefinity.Web.PageRouteHandler.InstantiateHandler(context.Request.RequestContext, siteMapNodeFromKey);
      if (pageAction != null)
        pageAction(handler);
      handler.InitComplete += (EventHandler) ((sender, args) =>
      {
        if (ScriptManager.GetCurrent(handler) == null)
          handler.Form.Controls.AddAt(0, (Control) new RadScriptManager());
        if (SitefinityStyleSheetManager.GetCurrent(handler) != null)
          return;
        handler.Form.Controls.Add((Control) new SitefinityStyleSheetManager());
      });
      handler.ProcessRequest(context.ApplicationInstance.Context);
    }

    /// <summary>Gets all child nodes.</summary>
    /// <returns>The child nodes.</returns>
    public static List<PageNode> GetChildNodes(PageNode page, List<PageNode> list)
    {
      if (page.Nodes.Count == 0)
        return (List<PageNode>) null;
      foreach (PageNode node in (IEnumerable<PageNode>) page.Nodes)
      {
        list.Add(node);
        List<PageNode> childNodes = PageManager.GetChildNodes(node, list);
        if (childNodes != null)
          list = list.Union<PageNode>((IEnumerable<PageNode>) childNodes).ToList<PageNode>();
      }
      return list;
    }

    internal IEnumerable<PageUrlData> GetUrlsForLanguage(
      IEnumerable<PageUrlData> urls,
      CultureInfo language,
      Guid rootNodeId)
    {
      return urls.Where<PageUrlData>((Func<PageUrlData, bool>) (url => url.Culture == Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(language)));
    }

    /// <summary>
    /// Sets the specified URL to the specified node. Adds the old URL as additional URL for the node, if necessary.
    /// </summary>
    /// <param name="node">The node to set URL for.</param>
    /// <param name="newUrlName">The new URL.</param>
    /// <param name="language">The language for the new URL.</param>
    public virtual void SetPageNodeUrlName(PageNode node, string newUrlName, CultureInfo language)
    {
      string str = node.UrlName.GetStringNoFallback(language) + node.Extension.GetStringNoFallback(language);
      if (!(newUrlName != str))
        return;
      if (!string.IsNullOrEmpty(str) && !node.IsBackend && node.IsPublished(language))
      {
        string urlString = node.BuildUrl(language);
        this.AddPageNodeAdditionalUrl(node, language, urlString, node.AdditionalUrlsRedirectToDefaultOne);
      }
      PageManager.UpdatePageNodeUrlInternal(node, newUrlName, language);
    }

    private static void UpdatePageNodeUrlInternal(
      PageNode node,
      string newUrl,
      CultureInfo language)
    {
      ISet<string> knownExtensions = Config.Get<PagesConfig>().KnownExtensions;
      int extIndex;
      string ext;
      if (!newUrl.IsNullOrEmpty() && knownExtensions.Count > 0 && PageManager.HasExtension(newUrl, knownExtensions, out extIndex, out ext))
      {
        node.Extension.SetString(language, ext);
        string str = newUrl.Substring(0, extIndex);
        node.UrlName.SetString(language, str);
      }
      else
      {
        node.Extension.SetString(language, string.Empty);
        node.UrlName.SetString(language, newUrl);
      }
    }

    internal List<UrlPartsStack> GetUrlPartStacks(IEnumerable<PageUrlData> pageUrls)
    {
      List<UrlPartsStack> urlPartStacks = new List<UrlPartsStack>();
      foreach (PageUrlData pageUrl in pageUrls)
      {
        UrlPartsStack urlPartsStack = this.GetUrlPartsStack((PageNode) pageUrl.Parent, pageUrl);
        urlPartStacks.Add(urlPartsStack);
      }
      return urlPartStacks;
    }

    /// <summary>Gets the URL parts stack.</summary>
    /// <param name="node">The node.</param>
    /// <param name="url">The URL to get stacks for. Must be application-relative URL, starting with "~/".</param>
    /// <returns></returns>
    protected virtual UrlPartsStack GetUrlPartsStack(PageNode node, PageUrlData url)
    {
      UrlPartsStack urlPartsStack = new UrlPartsStack(node, url);
      string[] strArray = url.Url.Substring(2).Split(new char[1]
      {
        '/'
      }, StringSplitOptions.None);
      for (int index = strArray.Length - 1; index >= 0; --index)
        urlPartsStack.Push(strArray[index]);
      return urlPartsStack;
    }

    internal static bool FilterUrlSegments(
      IEnumerable<UrlPartsStack> currentSegmentStacks,
      string currentSegment,
      out List<UrlPartsStack> nextLevelStacks)
    {
      bool flag = false;
      nextLevelStacks = new List<UrlPartsStack>();
      foreach (UrlPartsStack currentSegmentStack in currentSegmentStacks)
      {
        if (currentSegmentStack.Count > 0)
        {
          string str = currentSegmentStack.Pop();
          if (currentSegment == str)
          {
            nextLevelStacks.Add(currentSegmentStack);
            flag = true;
          }
        }
      }
      return flag;
    }

    internal static bool HasExtension(
      string segment,
      ISet<string> allowedExtensions,
      out int extIndex,
      out string ext,
      int startIndex = 0)
    {
      extIndex = segment.IndexOf('.', startIndex);
      if (extIndex > 0)
      {
        ext = segment.Substring(extIndex);
        if (allowedExtensions.Contains(ext))
          return true;
        startIndex = extIndex + 1;
        return PageManager.HasExtension(segment, allowedExtensions, out extIndex, out ext, startIndex);
      }
      ext = (string) null;
      return false;
    }

    internal static string RemoveExtension(string url, out string foundExtension)
    {
      foundExtension = (string) null;
      foreach (string knownExtension in (IEnumerable<string>) Config.Get<PagesConfig>().KnownExtensions)
      {
        if (url.EndsWith(knownExtension))
        {
          foundExtension = knownExtension;
          return url.Left(url.Length - knownExtension.Length);
        }
      }
      return url;
    }

    /// <summary>Load all controls of the same type on a page</summary>
    /// <typeparam name="T">Type of the control to load</typeparam>
    /// <param name="pageDataID">Page to get controls from</param>
    /// <returns>All controls of type <typeparamref name="T" /> on a page with <paramref name="pageDataID" /></returns>
    public T[] LoadPageControls<T>(Guid pageDataID) where T : Control => this.LoadPageControls<T>(typeof (T).FullName, pageDataID);

    /// <summary>Load all user controls</summary>
    /// <param name="virtualPath">Virtual path of the control</param>
    /// <param name="pageDataID">Id of the page to get controls from</param>
    /// <returns>All user controls defined in <paramref name="virtualPath" /> that are used on a page with <paramref name="pageDataID" /></returns>
    public UserControl[] LoadPageUserControls(string virtualPath, Guid pageDataID) => this.LoadPageControls<UserControl>(virtualPath, pageDataID);

    private TReturn[] LoadPageControls<TReturn>(string actualTypeName, Guid pageDataID) where TReturn : class => ((IEnumerable<PageControl>) this.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.Page.Id == pageDataID && c.ObjectType == actualTypeName)).ToArray<PageControl>()).Select<PageControl, Control>((Func<PageControl, Control>) (pc => this.LoadControl((ObjectData) pc, (CultureInfo) null))).Cast<TReturn>().ToArray<TReturn>();

    /// <summary>Applies the action to language related pages.</summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="action">The action.</param>
    private void ApplyActionToLanguageRelatedPages(PageData pageData, Action<PageData> action)
    {
      if (pageData.IsPersonalizationPage || pageData.LocalizationStrategy != LocalizationStrategy.Split)
        return;
      foreach (PageData pageData1 in new List<PageData>((IEnumerable<PageData>) pageData.NavigationNode.PageDataList))
      {
        if (pageData1.Id != pageData.Id)
          action(pageData1);
      }
    }

    public LanguageData CreateLanguageData() => this.Provider.CreateItem<LanguageData>();

    public LanguageData CreateLanguageData(Guid id) => this.Provider.CreateItem<LanguageData>(id);

    /// <summary>
    /// Creates language data in the current culture with content state: Published.
    /// </summary>
    /// <returns>The language data.</returns>
    public LanguageData CreatePublishedLanguageData()
    {
      LanguageData languageData = this.CreateLanguageData();
      languageData.Language = SystemManager.CurrentContext.Culture.Name;
      languageData.PublicationDate = DateTime.UtcNow;
      languageData.ContentState = LifecycleState.Published;
      languageData.ApplicationName = this.Provider.ApplicationName;
      return languageData;
    }

    public LanguageData GetLanguageData(Guid id) => this.Provider.GetItem<LanguageData>(id) as LanguageData;

    LifecycleDecorator<PageData, PageDraft> ILifecycleManager<PageData, PageDraft>.Lifecycle => (LifecycleDecorator<PageData, PageDraft>) new LifecycleDecoratorPages(this);

    LifecycleDecorator<PageTemplate, TemplateDraft> ILifecycleManager<PageTemplate, TemplateDraft>.Lifecycle => (LifecycleDecorator<PageTemplate, TemplateDraft>) new LifecycleDecoratorTemplates(this);

    public virtual LifecycleDecorator<PageTemplate, TemplateDraft> TemplatesLifecycle => ((ILifecycleManager<PageTemplate, TemplateDraft>) this).Lifecycle;

    public virtual LifecycleDecorator<PageData, PageDraft> PagesLifecycle => ((ILifecycleManager<PageData, PageDraft>) this).Lifecycle;

    TemplateDraft ILifecycleManager<PageTemplate, TemplateDraft>.CreateDraft() => this.CreateDraft<TemplateDraft>();

    PageDraft ILifecycleManager<PageData, PageDraft>.CreateDraft() => this.CreateDraft<PageDraft>();

    internal IEnumerable<PageData> GetPagesContainingWidget(
      params string[] widgetTypes)
    {
      List<PageData> source1 = new List<PageData>();
      foreach (string widgetType1 in widgetTypes)
      {
        string widgetType = widgetType1;
        source1.AddRange((IEnumerable<PageData>) this.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.Page != default (object) && c.ObjectType.StartsWith(widgetType))).Select<PageControl, PageData>((Expression<Func<PageControl, PageData>>) (c => c.Page)));
        IQueryable<Telerik.Sitefinity.Pages.Model.TemplateControl> source2 = this.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>>) (c => c.Page != default (object) && c.ObjectType.StartsWith(widgetType)));
        Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, PageTemplate>> selector = (Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, PageTemplate>>) (c => c.Page);
        foreach (PageTemplate template in (IEnumerable<PageTemplate>) source2.Select<Telerik.Sitefinity.Pages.Model.TemplateControl, PageTemplate>(selector))
          source1.AddRange((IEnumerable<PageData>) template.Pages());
      }
      return source1.Distinct<PageData>();
    }

    internal static Telerik.Sitefinity.Multisite.ISite GetSite(PageNode pageNode)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      return multisiteContext != null && pageNode != null ? multisiteContext.GetSiteBySiteMapRoot(pageNode.RootNodeId) : SystemManager.CurrentContext.CurrentSite;
    }

    internal void SetMasterSynced(PageDraft master)
    {
      master.SetFlag(1);
      master.Synced = true;
    }

    public bool DeleteTempAfterPublish => false;

    /// <summary>
    /// Copies a variation page data to original page data and clears the IsPersonalized and VariationTypeKey properties.
    /// </summary>
    /// <param name="variationPageData">The variation page data.</param>
    /// <param name="originalPageData">The original page data.</param>
    internal void SetVariationPageDataAsOriginal(
      PageData variationPageData,
      PageData originalPageData)
    {
      originalPageData.IsPersonalized = false;
      originalPageData.VariationTypeKey = (string) null;
      if (!(variationPageData.Id != originalPageData.Id))
        return;
      originalPageData.Drafts.Clear();
      variationPageData.Drafts.Clear();
      this.EditPage(variationPageData.Id);
      this.CopyPageDrafts(variationPageData, originalPageData);
      this.AtachLanguageDataToPageData(originalPageData);
      foreach (PageDraft draft in (IEnumerable<PageDraft>) originalPageData.Drafts)
      {
        draft.Version = originalPageData.Version;
        draft.PersonalizationMasterId = Guid.Empty;
        draft.PersonalizationSegmentId = Guid.Empty;
      }
    }

    /// <summary>
    /// Clears variations from page data by provided id and variation type key.
    /// </summary>
    /// <param name="pageDataId">The page id of the page data.</param>
    /// <param name="variationTypeKey">The variation type key.</param>
    internal void ClearPageVariation(Guid pageDataId, string variationTypeKey)
    {
      IQueryable<PageData> pageDataList = this.GetPageDataList();
      Expression<Func<PageData, bool>> predicate = (Expression<Func<PageData, bool>>) (pd => pageDataId == pd.PersonalizationMasterId && pd.VariationTypeKey == variationTypeKey);
      foreach (PageData pageData in (IEnumerable<PageData>) pageDataList.Where<PageData>(predicate))
      {
        this.DeletePageTempDrafts(pageData);
        this.DeleteItem((object) pageData);
      }
    }

    /// <summary>Attaches the language data to the provided page data.</summary>
    /// <param name="pageData">The page data.</param>
    internal void AtachLanguageDataToPageData(PageData pageData)
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      LanguageData languageData = this.PagesLifecycle.GetOrCreateLanguageData((ILifecycleDataItem) pageData, culture);
      foreach (ILifecycleDataItem draft in (IEnumerable<PageDraft>) pageData.Drafts)
        this.PagesLifecycle.GetOrCreateLanguageData(draft, culture).CopyFrom(languageData);
    }

    private void CopyRendererData(object source, object target)
    {
      IRendererCommonData rendererCommonData1 = target as IRendererCommonData;
      if (!(source is IRendererCommonData rendererCommonData2) || rendererCommonData1 == null)
        return;
      rendererCommonData1.Renderer = rendererCommonData2.Renderer;
      rendererCommonData1.TemplateName = rendererCommonData2.TemplateName;
    }

    /// <summary>
    /// Gets the ids of all segments used for personalization of any controls on the page and on the template.
    /// </summary>
    /// <param name="pageDataId">The page data ID.</param>
    /// <param name="templateIds">The template ids.</param>
    /// <param name="isLivePage">if set to <c>true</c> [is live page].</param>
    /// <returns>
    /// A lookup containing all segments used on the page grouped by control ID
    /// </returns>
    internal ILookup<Guid, Guid> GetControlsSegmentIds(
      Guid pageDataId,
      IList<Guid> templateIds,
      bool isLivePage = true)
    {
      IQueryable<KeyValuePair<Guid, Guid>> source1 = new List<KeyValuePair<Guid, Guid>>().AsQueryable<KeyValuePair<Guid, Guid>>();
      IQueryable<KeyValuePair<Guid, Guid>> source2 = new List<KeyValuePair<Guid, Guid>>().AsQueryable<KeyValuePair<Guid, Guid>>();
      if (isLivePage)
      {
        IQueryable<PageControl> inner = this.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.Page.Id == pageDataId && c.IsPersonalized));
        source1 = this.GetControls<PageControl>().Join<PageControl, PageControl, Guid, KeyValuePair<Guid, Guid>>((IEnumerable<PageControl>) inner, (Expression<Func<PageControl, Guid>>) (o => o.PersonalizationMasterId), (Expression<Func<PageControl, Guid>>) (i => i.Id), (Expression<Func<PageControl, PageControl, KeyValuePair<Guid, Guid>>>) ((o, i) => new KeyValuePair<Guid, Guid>(o.PersonalizationMasterId, o.PersonalizationSegmentId)));
      }
      else
      {
        IQueryable<PageDraftControl> inner = this.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (c => c.Page.Id == pageDataId && c.IsPersonalized));
        source2 = this.GetControls<PageDraftControl>().Join<PageDraftControl, PageDraftControl, Guid, KeyValuePair<Guid, Guid>>((IEnumerable<PageDraftControl>) inner, (Expression<Func<PageDraftControl, Guid>>) (o => o.PersonalizationMasterId), (Expression<Func<PageDraftControl, Guid>>) (i => i.Id), (Expression<Func<PageDraftControl, PageDraftControl, KeyValuePair<Guid, Guid>>>) ((o, i) => new KeyValuePair<Guid, Guid>(o.PersonalizationMasterId, o.PersonalizationSegmentId)));
      }
      IQueryable<KeyValuePair<Guid, Guid>> templatesSegmentIds = this.GetControlsFromTemplatesSegmentIds(templateIds);
      return source1.Union<KeyValuePair<Guid, Guid>>((IEnumerable<KeyValuePair<Guid, Guid>>) source2).Union<KeyValuePair<Guid, Guid>>((IEnumerable<KeyValuePair<Guid, Guid>>) templatesSegmentIds).ToLookup<KeyValuePair<Guid, Guid>, Guid, Guid>((Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Key), (Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Value));
    }

    /// <summary>
    /// Gets the ids of all segments used for personalization of any controls that are on the page template.
    /// </summary>
    /// <param name="templateIds">The template ids.</param>
    /// <returns>
    /// A lookup containing all segments used on the page template grouped by control ID
    /// </returns>
    internal IQueryable<KeyValuePair<Guid, Guid>> GetControlsFromTemplatesSegmentIds(
      IList<Guid> templateIds)
    {
      IQueryable<KeyValuePair<Guid, Guid>> templatesSegmentIds = new List<KeyValuePair<Guid, Guid>>().AsQueryable<KeyValuePair<Guid, Guid>>();
      if (templateIds != null && templateIds.Count > 0)
      {
        IQueryable<Telerik.Sitefinity.Pages.Model.TemplateControl> inner = this.Provider.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>>) (c => templateIds.Contains(c.Page.Id) && c.IsPersonalized));
        templatesSegmentIds = this.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Join<Telerik.Sitefinity.Pages.Model.TemplateControl, Telerik.Sitefinity.Pages.Model.TemplateControl, Guid, KeyValuePair<Guid, Guid>>((IEnumerable<Telerik.Sitefinity.Pages.Model.TemplateControl>) inner, (Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, Guid>>) (o => o.PersonalizationMasterId), (Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, Guid>>) (i => i.Id), (Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, Telerik.Sitefinity.Pages.Model.TemplateControl, KeyValuePair<Guid, Guid>>>) ((o, i) => new KeyValuePair<Guid, Guid>(o.PersonalizationMasterId, o.PersonalizationSegmentId)));
      }
      return templatesSegmentIds;
    }

    internal ILookup<Guid, Guid> GetPageDraftControlsSegmentIds(Guid pageDraftId)
    {
      if (!(pageDraftId != Guid.Empty))
        return new List<KeyValuePair<Guid, Guid>>().ToLookup<KeyValuePair<Guid, Guid>, Guid, Guid>((Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Key), (Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Value));
      IQueryable<PageDraftControl> inner = this.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (c => c.Page.Id == pageDraftId && c.IsPersonalized));
      return this.GetControls<PageDraftControl>().Join<PageDraftControl, PageDraftControl, Guid, KeyValuePair<Guid, Guid>>((IEnumerable<PageDraftControl>) inner, (Expression<Func<PageDraftControl, Guid>>) (o => o.PersonalizationMasterId), (Expression<Func<PageDraftControl, Guid>>) (i => i.Id), (Expression<Func<PageDraftControl, PageDraftControl, KeyValuePair<Guid, Guid>>>) ((o, i) => new KeyValuePair<Guid, Guid>(o.PersonalizationSegmentId, o.PersonalizationMasterId))).ToLookup<KeyValuePair<Guid, Guid>, Guid, Guid>((Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Key), (Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Value));
    }

    internal ILookup<Guid, Guid> GetTemplateDraftControlsSegmentIds(
      Guid templateDraftId)
    {
      if (!(templateDraftId != Guid.Empty))
        return new List<KeyValuePair<Guid, Guid>>().ToLookup<KeyValuePair<Guid, Guid>, Guid, Guid>((Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Key), (Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Value));
      IQueryable<TemplateDraftControl> inner = this.GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (c => c.Page.Id == templateDraftId && c.IsPersonalized));
      return this.GetControls<TemplateDraftControl>().Join<TemplateDraftControl, TemplateDraftControl, Guid, KeyValuePair<Guid, Guid>>((IEnumerable<TemplateDraftControl>) inner, (Expression<Func<TemplateDraftControl, Guid>>) (o => o.PersonalizationMasterId), (Expression<Func<TemplateDraftControl, Guid>>) (i => i.Id), (Expression<Func<TemplateDraftControl, TemplateDraftControl, KeyValuePair<Guid, Guid>>>) ((o, i) => new KeyValuePair<Guid, Guid>(o.PersonalizationSegmentId, o.PersonalizationMasterId))).ToLookup<KeyValuePair<Guid, Guid>, Guid, Guid>((Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Key), (Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Value));
    }

    internal ILookup<Guid, Guid> GetLiveTemplateControlsSegmentIds(Guid templateId)
    {
      if (!(templateId != Guid.Empty))
        return new List<KeyValuePair<Guid, Guid>>().ToLookup<KeyValuePair<Guid, Guid>, Guid, Guid>((Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Key), (Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Value));
      IQueryable<Telerik.Sitefinity.Pages.Model.TemplateControl> inner = this.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>>) (c => c.Page.Id == templateId && c.IsPersonalized));
      return this.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Join<Telerik.Sitefinity.Pages.Model.TemplateControl, Telerik.Sitefinity.Pages.Model.TemplateControl, Guid, KeyValuePair<Guid, Guid>>((IEnumerable<Telerik.Sitefinity.Pages.Model.TemplateControl>) inner, (Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, Guid>>) (o => o.PersonalizationMasterId), (Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, Guid>>) (i => i.Id), (Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, Telerik.Sitefinity.Pages.Model.TemplateControl, KeyValuePair<Guid, Guid>>>) ((o, i) => new KeyValuePair<Guid, Guid>(o.PersonalizationSegmentId, o.PersonalizationMasterId))).ToLookup<KeyValuePair<Guid, Guid>, Guid, Guid>((Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Key), (Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Value));
    }
  }
}
