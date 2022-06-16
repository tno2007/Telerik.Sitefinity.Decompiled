// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.PageTemplatesService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>
  /// The WCF web service that is used for page templates management
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class PageTemplatesService : IPageTemplatesService
  {
    /// <summary>The regex pattern for stripping template titles.</summary>
    public const string TemplateTitleStripingRegexPattern = "[\\\\/><\\:\\?\\\"\\*|]+|\\.+$ ";
    /// <summary>The template title incorrect characters substitute</summary>
    public const string TemplateTitleInvalidCharactersSubstitute = "_";

    /// <summary>
    /// Gets the collection of page templates and returns the result in JSON format. Returns the templates in ascedning order by their title.
    /// </summary>
    /// <returns>A collection context that contains page templates.</returns>
    public CollectionContext<PageTemplateViewModel> GetPageTemlatesInCondition(
      string pageFilter,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root)
    {
      return this.GetTemplatesInternal(pageFilter, sortExpression, skip, take, filter, root);
    }

    /// <summary>
    /// Gets the collection of page templates and returns the result in XML format.  Returns the templates in ascedning order by their title.
    /// </summary>
    /// <returns>A collection context that contains page templates.</returns>
    public CollectionContext<PageTemplateViewModel> GetPageTemlatesInConditionInXml(
      string pageFilter,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root)
    {
      return this.GetTemplatesInternal(pageFilter, sortExpression, skip, take, filter, root);
    }

    /// <summary>
    /// Gets the single page template and returs it in JSON format.
    /// </summary>
    /// <param name="templateId"></param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content item to be retrieved.
    /// </returns>
    public WcfPageTemplateContext GetPageTemplate(
      string templateId,
      string providerName)
    {
      return this.GetPageTemplateInternal(templateId);
    }

    /// <summary>Gets the single page and returs it in XML format.</summary>
    /// <param name="templateId"></param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public WcfPageTemplateContext GetPageTemplateInXml(
      string templateId,
      string providerName)
    {
      return this.GetPageTemplateInternal(templateId);
    }

    /// <summary>
    /// Saves the page template and returns the result in JSON format.
    /// </summary>
    /// <param name="templateContext">The template context.</param>
    /// <param name="templateId"></param>
    /// <param name="itemType"></param>
    /// <param name="providerName"></param>
    /// <param name="managerType"></param>
    /// <returns></returns>
    public WcfPageTemplateContext SavePageTemplate(
      WcfPageTemplateContext templateContext,
      string templateId,
      string itemType,
      string providerName,
      string managerType,
      bool duplicate)
    {
      return this.SavePageTemplateInternal(templateContext, duplicate);
    }

    /// <summary>
    /// Saves the page template and returns the result in XML format.
    /// </summary>
    /// <param name="templateContext">The template context.</param>
    /// <param name="templateId"></param>
    /// <param name="itemType"></param>
    /// <param name="providerName"></param>
    /// <param name="managerType"></param>
    /// <returns>A context that contains saved page template.</returns>
    public WcfPageTemplateContext SavePageTemplateInXml(
      WcfPageTemplateContext templateContext,
      string templateId,
      string itemType,
      string providerName,
      string managerType,
      bool duplicate)
    {
      return this.SavePageTemplateInternal(templateContext, duplicate);
    }

    /// <summary>
    /// Deletes the page template and returns true if the page has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="templateId">The template id.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page template.</param>
    /// <returns>
    /// true if the page template has been deleted; otherwise false.
    /// </returns>
    public bool DeletePageTemplate(string templateId, string providerName, string deletedLanguage) => this.DeletePageTemplateInternal(templateId, deletedLanguage);

    /// <summary>
    /// Deletes the page template and returns true if the page template has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="templateId">The template id.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page template.</param>
    /// <returns>
    /// true if the page template has been deleted; otherwise false.
    /// </returns>
    public bool DeletePageTemplateInXml(
      string templateId,
      string providerName,
      string deletedLanguage)
    {
      return this.DeletePageTemplateInternal(templateId, deletedLanguage);
    }

    /// <summary>
    /// Deletes an array of pages templates.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the page templates to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page templates.</param>
    /// <returns>true if the page templates has been deleted; otherwise false.</returns>
    public bool BatchDeletePageTemplates(string[] Ids, string providerName, string deletedLanguage) => this.BatchDeletePageTemplateInternal(Ids, deletedLanguage);

    /// <summary>
    /// Deletes an array of pages templates.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the page templates to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page templates.</param>
    /// <returns>true if the page templates has been deleted; otherwise false.</returns>
    public bool BatchDeletePageTemplatesInXml(
      string[] Ids,
      string providerName,
      string deletedLanguage)
    {
      return this.BatchDeletePageTemplateInternal(Ids, deletedLanguage);
    }

    /// <summary>
    /// Changes the parent template.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="templateId">The template id whoose parent is going to be changed.</param>
    /// <param name="newTemplateId">The new template id.</param>
    /// <returns></returns>
    public bool ChangeTemplate(string templateId, string newTemplateId) => this.ChangeTemplateInternal(templateId, newTemplateId);

    /// <summary>
    /// Changes the parent template.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="templateId">The template id whoose parent is going to be changed.</param>
    /// <param name="newTemplateId">The new template id.</param>
    /// <returns></returns>
    public bool ChangeTemplateInXml(string templateId, string newTemplateId) => this.ChangeTemplateInternal(templateId, newTemplateId);

    /// <summary>Publishes the specified templates.</summary>
    /// <param name="pageIDs">The template IDs.</param>
    public void BatchPublishDraft(string[] templateIDs) => this.BatchPublishDraftInternal(templateIDs);

    /// <summary>Publishes the specified templates.</summary>
    /// <param name="pageIDs">The template IDs.</param>
    public void BatchPublishDraftInXml(string[] templateIDs) => this.BatchPublishDraftInternal(templateIDs);

    /// <inheritdoc />
    public CollectionContext<SiteItemLinkViewModel> GetSharedSites(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetSharedSitesInternal(templateId, sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public CollectionContext<SiteItemLinkViewModel> GetSharedSitesInXml(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetSharedSitesInternal(templateId, sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public bool SaveSharedSites(string templateId, string[] sharedSiteIDs) => this.SaveSharedSitesInternal(templateId, sharedSiteIDs);

    /// <inheritdoc />
    public bool SaveSharedSitesInXml(string templateId, string[] sharedSiteIDs) => this.SaveSharedSitesInternal(templateId, sharedSiteIDs);

    /// <summary>
    /// Gets the templates internal. Returns the templates in ascedning order by their title.
    /// </summary>
    /// <param name="pageFilterString">The page filter string.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="root">The root.</param>
    /// <returns></returns>
    private CollectionContext<PageTemplateViewModel> GetTemplatesInternal(
      string pageFilterString,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageTemplatesFacade facade = App.WorkWith().PageTemplates();
      PageTemplatesFacade pageTemplatesFacade = this.FilterTemplates(pageFilterString, facade);
      IQueryable<PageTemplate> query = !(root == "backend") ? PageTemplateHelper.FilterFrameworkSpecificTemplates(pageTemplatesFacade.GetNotInCategory(SiteInitializer.BackendTemplatesCategoryId)) : pageTemplatesFacade.GetByCategory(SiteInitializer.BackendTemplatesCategoryId);
      int? totalCount = new int?(0);
      if (sortExpression == null)
        sortExpression = "Title ASC";
      IQueryable<PageTemplate> source = DataProviderBase.SetExpressions<PageTemplate>(query, filter, sortExpression, new int?(skip), new int?(take), ref totalCount);
      List<PageTemplateViewModel> items = new List<PageTemplateViewModel>();
      foreach (PageTemplate pageTemplate in source.ToArray<PageTemplate>())
      {
        bool isUnlockable = pageTemplate.IsGranted("PageTemplates", "Unlock");
        bool isEditable = this.IsPageEditable(pageTemplate);
        PageTemplateViewModel viewModel = new PageTemplateViewModel(pageTemplate, isEditable, isUnlockable);
        EventHub.Raise((IEvent) new PageTemplateViewModelCreatedEvent(pageTemplate, viewModel));
        items.Add(viewModel);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<PageTemplateViewModel>((IEnumerable<PageTemplateViewModel>) items)
      {
        TotalCount = totalCount.Value
      };
    }

    /// <summary>Filters the templates.</summary>
    /// <param name="pageFilterString">The page filter string.</param>
    /// <param name="facade">The facade.</param>
    /// <returns></returns>
    private PageTemplatesFacade FilterTemplates(
      string pageFilterString,
      PageTemplatesFacade facade)
    {
      if (!(pageFilterString == "AllTemplates"))
      {
        if (!(pageFilterString == "NotSharedTemplates"))
        {
          if (!(pageFilterString == "MyTemplates"))
          {
            if (pageFilterString == "ThisSiteTemplates")
            {
              facade = facade.GetInSite(SystemManager.CurrentContext.CurrentSite.Id);
            }
            else
            {
              IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
              if (multisiteContext != null)
                facade = facade.GetInSite(multisiteContext.CurrentSite.Id);
              if (!string.IsNullOrEmpty(pageFilterString))
              {
                string name = PageManager.GetManager().Provider.Name;
                IEnumerable<Guid> result;
                if (SystemManager.StatusProviderRegistry.TryGetMatchingFilterItemIds(pageFilterString, typeof (PageTemplate), name, out result))
                  facade = facade.InTemplates(result);
              }
            }
          }
          else
            facade = facade.ThatAreOwnedBy(ClaimsManager.GetCurrentUserId());
        }
        else
          facade = facade.GetNotShared();
      }
      return facade;
    }

    /// <summary>Batches the publish draft internal.</summary>
    /// <param name="ids">The ids.</param>
    private void BatchPublishDraftInternal(string[] ids)
    {
      PageManager manager = PageManager.GetManager();
      foreach (string id in ids)
      {
        TemplateDraft templateDraft = manager.GetTemplate(new Guid(id)).Drafts.Where<TemplateDraft>((Func<TemplateDraft, bool>) (d => !d.IsTempDraft)).FirstOrDefault<TemplateDraft>();
        if (templateDraft != null)
          manager.PublishTemplateDraft(templateDraft.Id);
      }
      manager.SaveChanges();
    }

    /// <summary>Batches the delete page template internal.</summary>
    /// <param name="Ids">The ids.</param>
    /// <param name="deletedLanguage">The deleted language.</param>
    /// <returns></returns>
    private bool BatchDeletePageTemplateInternal(string[] Ids, string deletedLanguage)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      CultureInfo language = (CultureInfo) null;
      if (!string.IsNullOrEmpty(deletedLanguage) && SystemManager.CurrentContext.AppSettings.Multilingual)
        language = new CultureInfo(deletedLanguage);
      using (FluentSitefinity fluentSitefinity = App.WorkWith())
      {
        foreach (string id in Ids)
          fluentSitefinity.PageTemplate(new Guid(id)).Delete(language);
        fluentSitefinity.SaveChanges();
      }
      return true;
    }

    /// <summary>Deletes the page template internal.</summary>
    /// <param name="templateId">The template id.</param>
    /// <param name="deletedLanguage">The deleted language.</param>
    /// <returns></returns>
    private bool DeletePageTemplateInternal(string templateId, string deletedLanguage)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (!string.IsNullOrEmpty(deletedLanguage) && SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        CultureInfo cultureInfo = new CultureInfo(deletedLanguage);
      }
      using (FluentSitefinity fluentSitefinity = App.WorkWith())
      {
        fluentSitefinity.PageTemplate(new Guid(templateId)).Delete();
        fluentSitefinity.SaveChanges();
      }
      return true;
    }

    /// <summary>Saves the page template internal.</summary>
    /// <param name="ctx">The CTX.</param>
    /// <returns></returns>
    private WcfPageTemplateContext SavePageTemplateInternal(
      WcfPageTemplateContext ctx,
      bool duplicate)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageTemplateViewModel proxy = ctx.Item;
      Guid category = proxy.Category;
      if (proxy.Id == Guid.Empty)
        category = proxy.Template == null ? SiteInitializer.CustomTemplatesCategoryId : ((RootTaxonType) System.Enum.Parse(typeof (RootTaxonType), proxy.Template.RootTaxonType) != RootTaxonType.Backend ? SiteInitializer.CustomTemplatesCategoryId : SiteInitializer.BackendTemplatesCategoryId);
      PageManager manager1 = PageManager.GetManager();
      string str = duplicate ? proxy.DuplicateTitle : proxy.Title;
      string name = Regex.Replace(str, "[\\\\/><\\:\\?\\\"\\*|]+|\\.+$ ", "_");
      manager1.ValidateTemplateConstraints(str, proxy.Id, category, duplicate);
      int num;
      if (duplicate)
        num = manager1.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (tpl => tpl.Id == proxy.Id)).Framework != proxy.Framework ? 1 : 0;
      else
        num = 0;
      if (num != 0)
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<PageResources>().DuplicatedFrameworkIsDifferent, (Exception) null);
      string masterPage = string.Empty;
      if (proxy.Template != null && proxy.Template.NotCreateTemplateForMasterPage)
        masterPage = VirtualPathUtility.ToAppRelative(proxy.Template.MasterPage);
      bool multilingual = SystemManager.CurrentContext.AppSettings.Multilingual;
      bool flag = proxy.SourceLanguagePageId != Guid.Empty;
      using (FluentSitefinity fluentSitefinity = App.WorkWith())
      {
        PageTemplateFacade facade = (PageTemplateFacade) null;
        if (multilingual && flag)
        {
          proxy.Id = Guid.Empty;
          facade = fluentSitefinity.PageTemplate(ctx.Item.SourceLanguagePageId);
          facade.Do((Action<PageTemplate>) (t => t.Title = (Lstring) proxy.Title));
          if (!string.IsNullOrEmpty(proxy.SourceLanguage))
            facade.Do((Action<PageTemplate>) (t =>
            {
              CultureInfo sourceLanguage = new CultureInfo(proxy.SourceLanguage);
              foreach (TemplateControl control in (IEnumerable<TemplateControl>) t.Controls)
              {
                if (control.IsTranslatable)
                  facade.PageManager.CopyProperties((ObjectData) control, sourceLanguage, SystemManager.CurrentContext.Culture, true);
              }
              TemplateDraft master = facade.PageManager.TemplatesLifecycle.GetMaster(t);
              if (master == null)
                return;
              LocalizationHelper.CopyLstring(master.Themes, master.Themes, sourceLanguage, SystemManager.CurrentContext.Culture);
              foreach (TemplateDraftControl control in (IEnumerable<TemplateDraftControl>) master.Controls)
              {
                if (control.IsTranslatable)
                  facade.PageManager.CopyProperties((ObjectData) control, sourceLanguage, SystemManager.CurrentContext.Culture, true);
              }
            }));
        }
        if (facade == null)
        {
          if (proxy.Id == Guid.Empty)
          {
            facade = fluentSitefinity.PageTemplate().CreateNew();
            facade.Do((Action<PageTemplate>) (t =>
            {
              t.Title = (Lstring) proxy.Title;
              t.Framework = proxy.Template != null ? proxy.Template.Framework : proxy.Framework;
              t.Category = category;
              t.ShowInNavigation = proxy.ShowInNavigation;
              t.MasterPage = masterPage;
              t.Name = string.IsNullOrEmpty(proxy.Name) ? name : proxy.Name;
              t.Renderer = proxy.Renderer;
              t.TemplateName = proxy.TemplateName;
            }));
          }
          else
          {
            facade = fluentSitefinity.PageTemplate(proxy.Id);
            if (duplicate)
            {
              PageTemplate source = facade.Get();
              facade.Duplicate(proxy.DuplicateTitle);
              facade.Do((Action<PageTemplate>) (t => t.Name = proxy.Name)).Get().CopySecurityFrom((ISecuredObject) source);
            }
            else
              facade.Do((Action<PageTemplate>) (t =>
              {
                t.Title = (Lstring) proxy.Title;
                t.Name = proxy.Name;
              }));
          }
          PageTemplateHelper.CheckCreateOrMockAndReturnBasicEmptyTemplate(proxy.Template == null ? Guid.Empty : proxy.Template.Id, true);
          facade.SetParentTemplateTo(proxy.Template == null ? Guid.Empty : proxy.Template.Id);
        }
        if (multilingual)
          facade.Do((Action<PageTemplate>) (t =>
          {
            t.Culture = CultureInfo.CurrentCulture.Name;
            t.UiCulture = SystemManager.CurrentContext.Culture.Name;
          }));
        PageTemplate pageTemplate = facade.Get();
        RelatedDataHelper.SaveRelatedDataChanges((IManager) facade.PageManager, (IDataItem) pageTemplate, ctx.ChangedRelatedData);
        facade.SaveChanges();
        if (proxy.Id == Guid.Empty | duplicate)
        {
          TemplateDraft templateDraft = manager1.EditTemplate(pageTemplate.Id);
          manager1.TemplatesLifecycle.CheckIn(templateDraft, (CultureInfo) null, false).ParentItem.LockedBy = Guid.Empty;
          VersionManager manager2 = VersionManager.GetManager();
          manager2.CreateVersion((object) templateDraft, pageTemplate.Id, false);
          manager1.SaveChanges();
          manager2.SaveChanges();
        }
        WcfPageTemplateContext pageTemplateContext = new WcfPageTemplateContext();
        pageTemplateContext.Item = new PageTemplateViewModel(pageTemplate);
        return pageTemplateContext;
      }
    }

    /// <summary>Gets the page template internal.</summary>
    /// <param name="templateIdStr">The template id.</param>
    /// <returns></returns>
    private WcfPageTemplateContext GetPageTemplateInternal(
      string templateIdStr)
    {
      Guid guid = new Guid(templateIdStr);
      PageTemplate pageTemplate = App.WorkWith().PageTemplate(guid).Get();
      WcfPageTemplateContext templateInternal = new WcfPageTemplateContext();
      templateInternal.Item = new PageTemplateViewModel(pageTemplate);
      templateInternal.Warnings = SystemManager.StatusProviderRegistry.GetWarnings(guid, typeof (PageTemplate), pageTemplate.GetProviderName()).Select<WarningInfo, ItemWarning>((Func<WarningInfo, ItemWarning>) (w => new ItemWarning(w)));
      return templateInternal;
    }

    /// <summary>Changes the parent template .</summary>
    /// <param name="templateIdStr">The template id.</param>
    /// <param name="parentTemplateIdStr">The new template id.</param>
    /// <returns></returns>
    private bool ChangeTemplateInternal(string templateIdStr, string parentTemplateIdStr)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid guid = new Guid(templateIdStr);
      Guid result;
      Guid.TryParse(parentTemplateIdStr, out result);
      PageManager manager1 = PageManager.GetManager();
      PageTemplate template = manager1.GetTemplate(guid);
      TemplateDraft templateDraft = manager1.EditTemplate(template.Id);
      templateDraft.MasterPage = string.Empty;
      if (result != Guid.Empty)
      {
        if (PageTemplateHelper.IsOnDemandTempalteId(result))
          PageTemplateHelper.CheckCreateOrMockAndReturnBasicEmptyTemplate(result, true);
        templateDraft.SetParentBaseTemplate(manager1.GetTemplate(result) ?? throw new ArgumentException("There is no template with ID \"{0}\".".Arrange((object) result)));
      }
      else
      {
        if (parentTemplateIdStr.Contains("."))
        {
          string[] strArray = parentTemplateIdStr.Split(new char[1]
          {
            '.'
          }, StringSplitOptions.RemoveEmptyEntries);
          string str1 = strArray[0];
          string str2 = strArray[1];
          ((IRendererCommonData) templateDraft).TemplateName = str2;
          ((IRendererCommonData) templateDraft).Renderer = str1;
        }
        templateDraft.SetParentBaseTemplate((PageTemplate) null);
      }
      manager1.TemplatesLifecycle.CheckIn(templateDraft, (CultureInfo) null, false);
      templateDraft.ParentTemplate.LockedBy = Guid.Empty;
      VersionManager manager2 = VersionManager.GetManager();
      manager2.CreateVersion((object) templateDraft, guid, false);
      manager1.SaveChanges();
      manager2.SaveChanges();
      return true;
    }

    /// <summary>
    /// Determines whether [is page editable] [the specified template].
    /// </summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    private bool IsPageEditable(PageTemplate template)
    {
      bool flag = true;
      if (template.GetType().GetInterface(typeof (ISecuredObject).FullName) != (Type) null && !template.IsEditable("PageTemplates", "Modify"))
        flag = false;
      return flag;
    }

    private CollectionContext<SiteItemLinkViewModel> GetSharedSitesInternal(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PageTemplatesService.\u003C\u003Ec__DisplayClass27_0 cDisplayClass270 = new PageTemplatesService.\u003C\u003Ec__DisplayClass27_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass270.templateGuid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(templateId);
      PageManager manager = PageManager.GetManager();
      IQueryable<ISite> source = SystemManager.CurrentContext.MultisiteContext.GetSites().AsQueryable<ISite>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass270.siteLinks = (IEnumerable<SiteItemLink>) null;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass270.templateGuid != Guid.Empty)
      {
        // ISSUE: reference to a compiler-generated field
        SiteItemLink[] array = manager.GetSiteTemplateLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == cDisplayClass270.templateGuid)).ToArray<SiteItemLink>();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass270.siteLinks = (IEnumerable<SiteItemLink>) array;
      }
      if (!string.IsNullOrEmpty(sortExpression))
        source = source.OrderBy<ISite>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        source = source.Where<ISite>(filter);
      int num = source.Count<ISite>();
      if (skip > 0)
        source = source.Skip<ISite>(skip);
      if (take > 0)
        source = source.Take<ISite>(take);
      ServiceUtility.DisableCache();
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      return ; //unable to render the statement
    }

    private bool SaveSharedSitesInternal(string templateId, string[] sharedSiteIDs)
    {
      Guid templateGuid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(templateId);
      IEnumerable<Guid> source = ((IEnumerable<string>) sharedSiteIDs).ToList<string>().Select<string, Guid>((Func<string, Guid>) (id => Telerik.Sitefinity.Utilities.Utility.StringToGuid(id)));
      PageManager manager = PageManager.GetManager();
      if (!(templateGuid != Guid.Empty))
        return false;
      SiteItemLink[] array = manager.GetSiteTemplateLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (link => link.ItemId == templateGuid)).ToArray<SiteItemLink>();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        if (!source.Contains<Guid>(array[index].SiteId))
          manager.Delete(array[index]);
      }
      PageTemplate template = new PageTemplate()
      {
        Id = templateGuid
      };
      foreach (Guid guid in source)
      {
        Guid siteId = guid;
        if (siteId != Guid.Empty && !((IEnumerable<SiteItemLink>) array).Any<SiteItemLink>((Func<SiteItemLink, bool>) (l => l.SiteId == siteId)))
          manager.LinkTemplateToSite(template, siteId);
      }
      manager.SaveChanges();
      return true;
    }
  }
}
