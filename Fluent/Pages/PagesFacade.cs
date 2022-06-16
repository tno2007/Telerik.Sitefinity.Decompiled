// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.PagesFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Fluent.Pages
{
  /// <summary>
  /// Fluent API that provides most common functionality needed to work with a collection of Sitefinity pages.
  /// </summary>
  public class PagesFacade : 
    BaseFacadeWithManager,
    ICollectionFacade<PagesFacade, PageNode>,
    IFacade<PagesFacade>
  {
    private IQueryable<PageData> pageDataList;
    private IQueryable<PageNode> pageNodes;
    private readonly Telerik.Sitefinity.Fluent.AppSettings appSettings;
    private PageManager pageManager;
    private const string ApprovalWorkflowStateDraft = "Draft";
    private CultureInfo cultureFilter;
    private bool anyCulture;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public PagesFacade(Telerik.Sitefinity.Fluent.AppSettings appSettings)
      : base(appSettings)
    {
      this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));
    }

    /// <summary>Needed for mocking with JustMock.</summary>
    internal PagesFacade()
    {
    }

    /// <summary>
    /// Gets an instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PagesFacade.PageManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PagesFacade.PageManager" /> class.</value>
    protected internal virtual PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager(this.appSettings.PagesProviderName, this.appSettings.TransactionName);
        return this.pageManager;
      }
    }

    /// <summary>
    /// Gets or sets the query of all page taxa in the given taxonomy provider. This query is used
    /// by the fluent API and all methods are executed on this query.
    /// </summary>
    protected internal virtual IQueryable<PageNode> PageNodes
    {
      get
      {
        if (this.pageNodes == null)
          this.pageNodes = this.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.IsDeleted == false));
        return this.pageNodes;
      }
      set => this.pageNodes = value;
    }

    /// <summary>
    /// Gets or sets the query of all page data items provider.
    /// </summary>
    private IQueryable<PageData> PageDataList
    {
      get
      {
        if (this.pageDataList == null)
          this.pageDataList = this.PageManager.GetPageDataList();
        return this.pageDataList;
      }
      set => this.pageDataList = value;
    }

    /// <summary>
    /// Gets an instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PagesFacade.VersionManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PagesFacade.VersionManager" /> class.</value>
    protected internal virtual VersionManager VersionManager => VersionManager.GetManager(this.appSettings.PagesProviderName, this.appSettings.TransactionName);

    internal FluentSitefinity Fluent => App.Prepare(this.appSettings).WorkWith();

    internal PagesFacade FetchAllLanguages()
    {
      this.PageManager.Provider.FetchAllLanguagesData();
      return this;
    }

    /// <summary>
    /// Deletes all the pages currently selected by this instance of the pages fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" />.</returns>
    public virtual PagesFacade Delete()
    {
      FluentSitefinity fluent = this.Fluent;
      foreach (PageNode pageNode in (IEnumerable<PageNode>) this.PageNodes)
      {
        fluent.Page(pageNode).Delete();
        this.appSettings.TrackDeletedItem((IDataItem) pageNode);
      }
      return this;
    }

    /// <summary>
    /// Performs a specified action on each page selected by this instance of the pages fluent API.
    /// </summary>
    /// <param name="action">An action to be performed on each page.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" />.</returns>
    public virtual PagesFacade ForEach(Action<PageNode> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      foreach (PageNode pageNode in (IEnumerable<PageNode>) this.PageNodes)
        action(pageNode);
      return this;
    }

    /// <summary>
    /// Makes all currently selected pages children of the specified page.
    /// </summary>
    /// <param name="pageId">An id of the page that is to become the parent of selected pages.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade MakeChildrenOf(Guid pageId)
    {
      if (pageId == Guid.Empty)
        throw new ArgumentException(nameof (pageId));
      FluentSitefinity fluent = this.Fluent;
      foreach (PageNode pageNode in (IEnumerable<PageNode>) this.PageNodes)
        fluent.Page(pageNode).MakeChildOf(pageId);
      return this;
    }

    /// <summary>
    /// Makes all currently selected pages children of the specified page.
    /// </summary>
    /// <param name="page">An instance of the page that is to become the parent of selected pages.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade MakeChildrenOf(PageNode page) => page != null ? this.MakeChildrenOf(page.Id) : throw new ArgumentNullException(nameof (page));

    /// <summary>
    /// Moves all the selected pages one place down inside of their respective levels.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade MoveDown() => throw new NotImplementedException();

    /// <summary>
    /// Moves all the selected pages one place up inside of their respective levels.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade MoveUp() => throw new NotImplementedException();

    /// <summary>
    /// Publishes all the pages currently selected by this instance of the pages fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade Publish() => throw new NotImplementedException();

    /// <summary>
    /// Schedules all the selected pages to be published at the specified date and time.
    /// </summary>
    /// <param name="publicationDate">The date and time at which the pages ought to be published.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade Schedule(DateTime publicationDate) => throw new NotImplementedException();

    /// <summary>
    /// Schedules all the selected pages to be published and then archived at respective dates and times.
    /// </summary>
    /// <param name="publicationDate">The date and time at which the pages ought to be published.</param>
    /// <param name="expirationDate">The date and time at which the pages ought to be archived.</param>
    /// <returns></returns>
    public virtual PagesFacade Schedule(
      DateTime publicationDate,
      DateTime expirationDate)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the query with pages with all currently selected pages.
    /// </summary>
    /// <returns>An instance of IQueryable[PageTaxon] objects.</returns>
    public virtual IQueryable<PageNode> Get()
    {
      if (this.pageDataList == null)
        return this.PageNodes;
      IQueryable<PageData> queryable = this.pageDataList;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        queryable = queryable.Where<PageData>((Expression<Func<PageData, bool>>) (pageData => pageData.PersonalizationMasterId == Guid.Empty && pageData.NavigationNodeId != Guid.Empty));
        if (!this.anyCulture)
        {
          string language = (this.cultureFilter != null ? this.cultureFilter : SystemManager.CurrentContext.Culture).GetLanguageKeyRaw();
          queryable = queryable.Where<PageData>((Expression<Func<PageData, bool>>) (pageData => (int) pageData.NavigationNode.LocalizationStrategy != 1 || pageData.Culture == language || pageData.Culture == default (string) && SystemManager.CurrentContext.Culture == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage));
        }
      }
      return this.PageNodes.Join<PageNode, PageData, Guid, PageNode>((IEnumerable<PageData>) queryable, (Expression<Func<PageNode, Guid>>) (pn => pn.Id), (Expression<Func<PageData, Guid>>) (pd => pd.NavigationNodeId), (Expression<Func<PageNode, PageData, PageNode>>) ((pn, pd) => pn)).Distinct<PageNode>();
    }

    /// <summary>
    /// Filters the pages to include only the pages that are owned by the specified user.
    /// </summary>
    /// <param name="user">The user to which the pages belong to.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade ThatAreOwnedBy(User user) => this.ThatAreOwnedBy(user.Id);

    /// <summary>
    /// Filters the pages to include only the pages that are owned by the specified user.
    /// </summary>
    /// <param name="userId">The id of the user to whom the pages belong to.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade ThatAreOwnedBy(Guid userId)
    {
      this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.Owner == userId));
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that are in the draft state.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade ThatAreDrafts() => this.ThatAreDrafts((CultureInfo) null);

    /// <summary>
    /// Filters the pages to include only the pages that are in the draft state.
    /// </summary>
    /// <param name="culture">Culture info.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade ThatAreDrafts(CultureInfo culture)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PagesFacade.\u003C\u003Ec__DisplayClass28_0 cDisplayClass280 = new PagesFacade.\u003C\u003Ec__DisplayClass28_0();
        CultureInfo frontendLanguage = appSettings.DefaultFrontendLanguage;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass280.currentLanguage = culture.GetSitefinityCulture();
        // ISSUE: reference to a compiler-generated field
        if (!cDisplayClass280.currentLanguage.Equals((object) frontendLanguage))
        {
          this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (x => x.ApprovalWorkflowState == (Lstring) "Draft"));
          // ISSUE: reference to a compiler-generated field
          this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (x => !x.LanguageData.Any<LanguageData>() || x.LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (y => y.Language == cDisplayClass280.currentLanguage.GetLanguageKeyRaw()))));
        }
        else
        {
          this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (x => x.ApprovalWorkflowState == (Lstring) "Draft"));
          ParameterExpression parameterExpression1;
          ParameterExpression parameterExpression2;
          // ISSUE: method reference
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: type reference
          this.PageDataList = this.PageDataList.Where<PageData>(Expression.Lambda<Func<PageData, bool>>((Expression) Expression.OrElse(!x.LanguageData.Any<LanguageData>(), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            x.LanguageData,
            (Expression) Expression.Lambda<Func<LanguageData, bool>>((Expression) Expression.OrElse(y.Language == cDisplayClass280.currentLanguage.GetLanguageKeyRaw(), (Expression) Expression.AndAlso(y.Language == default (string), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PageData.get_LanguageData))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ICollection<LanguageData>.get_Count), __typeref (ICollection<LanguageData>))), (Expression) Expression.Constant((object) 1, typeof (int))))), parameterExpression2)
          })), parameterExpression1));
        }
      }
      else
        this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.ApprovalWorkflowState == (Lstring) "Draft"));
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that are in published state in all languages.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade ThatArePublishedForAllTranslations()
    {
      this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (x => x.Version > 0 && x.Visible));
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that are in published state.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade ThatArePublished() => this.ThatArePublished((CultureInfo) null);

    /// <summary>
    /// Filters the pages to include only the pages that are in published state.
    /// </summary>
    /// <param name="culture">Culture info.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade ThatArePublished(CultureInfo culture)
    {
      IQueryable<PageNode> source1 = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (x => (int) x.NodeType == 1 || (int) x.NodeType == 3 || (int) x.NodeType == 4));
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PagesFacade.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new PagesFacade.\u003C\u003Ec__DisplayClass31_0();
        CultureInfo frontendLanguage = appSettings.DefaultFrontendLanguage;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass310.currentLanguage = culture.GetSitefinityCulture();
        // ISSUE: reference to a compiler-generated field
        if (!cDisplayClass310.currentLanguage.Equals((object) frontendLanguage))
        {
          this.PageNodes = source1.Union<PageNode>((IEnumerable<PageNode>) this.PageNodes);
          // ISSUE: reference to a compiler-generated field
          this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (x => x.PublishedTranslations.Contains(cDisplayClass310.currentLanguage.GetLanguageKeyRaw())));
        }
        else if (culture != null)
        {
          this.PageNodes = source1.Union<PageNode>((IEnumerable<PageNode>) this.PageNodes);
          ParameterExpression parameterExpression1;
          ParameterExpression parameterExpression2;
          // ISSUE: method reference
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          this.PageDataList = this.PageDataList.Where<PageData>(Expression.Lambda<Func<PageData, bool>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            x.PublishedTranslations,
            (Expression) Expression.Lambda<Func<string, bool>>((Expression) Expression.OrElse(y == cDisplayClass310.currentLanguage.GetLanguageKeyRaw(), (Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.Not((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PageData.get_PublishedTranslations))))), (Expression) Expression.GreaterThan((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PageData.get_Version))), (Expression) Expression.Constant((object) 0, typeof (int)))), (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PageData.get_Visible))))), parameterExpression2)
          }), parameterExpression1));
        }
        else
        {
          this.PageNodes = source1.Union<PageNode>((IEnumerable<PageNode>) this.PageNodes);
          this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (x => x.PublishedTranslations.Any<string>() || x.Version > 0 && x.Visible));
        }
      }
      else
      {
        this.PageNodes = source1.Union<PageNode>((IEnumerable<PageNode>) this.PageNodes);
        this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (x => x.Version > 0 && x.Visible));
      }
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that are waiting for approval.
    /// </summary>
    /// <returns>an instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade ThatAreWaitingForApproval() => this.ThatAreWaitingForApproval((CultureInfo) null);

    /// <summary>
    /// Filters the pages to include only the pages that are waiting for approval.
    /// </summary>
    /// <param name="culture">Culture info.</param>
    /// <returns>an instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade ThatAreWaitingForApproval(CultureInfo culture)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PagesFacade.\u003C\u003Ec__DisplayClass33_0 cDisplayClass330 = new PagesFacade.\u003C\u003Ec__DisplayClass33_0();
        CultureInfo frontendLanguage = appSettings.DefaultFrontendLanguage;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass330.currentLanguage = culture.GetSitefinityCulture();
        // ISSUE: reference to a compiler-generated field
        if (!cDisplayClass330.currentLanguage.Equals((object) frontendLanguage))
        {
          this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (x => x.ApprovalWorkflowState == (Lstring) "AwaitingApproval"));
          // ISSUE: reference to a compiler-generated field
          this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (x => !x.LanguageData.Any<LanguageData>() || x.LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (y => y.Language == cDisplayClass330.currentLanguage.GetLanguageKeyRaw()))));
        }
        else
        {
          this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (x => x.ApprovalWorkflowState == (Lstring) "AwaitingApproval"));
          ParameterExpression parameterExpression1;
          ParameterExpression parameterExpression2;
          // ISSUE: method reference
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: type reference
          this.PageDataList = this.PageDataList.Where<PageData>(Expression.Lambda<Func<PageData, bool>>((Expression) Expression.OrElse(!x.LanguageData.Any<LanguageData>(), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            x.LanguageData,
            (Expression) Expression.Lambda<Func<LanguageData, bool>>((Expression) Expression.OrElse(y.Language == cDisplayClass330.currentLanguage.GetLanguageKeyRaw(), (Expression) Expression.AndAlso(y.Language == default (string), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PageData.get_LanguageData))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ICollection<LanguageData>.get_Count), __typeref (ICollection<LanguageData>))), (Expression) Expression.Constant((object) 1, typeof (int))))), parameterExpression2)
          })), parameterExpression1));
        }
      }
      else
        this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (x => x.ApprovalWorkflowState == (Lstring) "AwaitingApproval"));
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that are waiting for approval for given culture. If no culture is passed takes into account the current request context culture.
    /// </summary>
    /// <param name="culture">Culture info.</param>
    /// <returns>an instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade FilterByCulture(CultureInfo culture = null)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PagesFacade.\u003C\u003Ec__DisplayClass34_0 cDisplayClass340 = new PagesFacade.\u003C\u003Ec__DisplayClass34_0();
        CultureInfo frontendLanguage = appSettings.DefaultFrontendLanguage;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass340.currentLanguage = culture.GetSitefinityCulture();
        // ISSUE: reference to a compiler-generated field
        if (!cDisplayClass340.currentLanguage.Equals((object) frontendLanguage))
        {
          // ISSUE: reference to a compiler-generated field
          this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (x => !x.LanguageData.Any<LanguageData>() || x.LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (y => y.Language == cDisplayClass340.currentLanguage.GetLanguageKeyRaw()))));
        }
        else
        {
          ParameterExpression parameterExpression1;
          ParameterExpression parameterExpression2;
          // ISSUE: method reference
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: type reference
          this.PageDataList = this.PageDataList.Where<PageData>(Expression.Lambda<Func<PageData, bool>>((Expression) Expression.OrElse(!x.LanguageData.Any<LanguageData>(), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            x.LanguageData,
            (Expression) Expression.Lambda<Func<LanguageData, bool>>((Expression) Expression.OrElse(y.Language == cDisplayClass340.currentLanguage.GetLanguageKeyRaw(), (Expression) Expression.AndAlso(y.Language == default (string), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PageData.get_LanguageData))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ICollection<LanguageData>.get_Count), __typeref (ICollection<LanguageData>))), (Expression) Expression.Constant((object) 1, typeof (int))))), parameterExpression2)
          })), parameterExpression1));
        }
      }
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that are in scheduled state.
    /// </summary>
    /// <returns>an instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade ThatAreScheduled()
    {
      this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (x => (int) x.Status == 2));
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that belong to a specified page.
    /// </summary>
    /// <param name="parentPage">An instance of the page to which the filtered pages belong to.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade ThatBelongTo(PageNode parentPage) => parentPage != null ? this.ThatBelongTo(parentPage.Id) : throw new ArgumentNullException(nameof (parentPage));

    /// <summary>
    /// Filters the pages to include only the pages that belong to a specified page.
    /// </summary>
    /// <param name="parentPageId">Id of the page to which the filtered pages belong to.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade ThatBelongTo(Guid parentPageId)
    {
      if (parentPageId == Guid.Empty)
        throw new ArgumentNullException(nameof (parentPageId));
      this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.Parent != default (object) && pt.Parent.Id == parentPageId));
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that belong to a specified page.
    /// </summary>
    /// <param name="pageLocation">One of the predefined page locations.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade LocatedIn(PageLocation pageLocation)
    {
      Guid empty = Guid.Empty;
      Guid rootId;
      if (pageLocation != PageLocation.Frontend)
      {
        if (pageLocation != PageLocation.Backend)
          throw new NotSupportedException();
        rootId = SiteInitializer.BackendRootNodeId;
      }
      else
        rootId = SiteInitializer.CurrentFrontendRootNodeId;
      return this.LocatedIn(rootId);
    }

    /// <summary>Sets the current facade page for a root node id.</summary>
    /// <param name="rootId">Page root node id.</param>
    /// <returns>The facade instance.</returns>
    public virtual PagesFacade LocatedIn(Guid rootId)
    {
      this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (pt => pt.RootNodeId == rootId));
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that have no descriptions.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade ThatHaveNoDescription()
    {
      this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (pd => string.IsNullOrEmpty((string) pd.Description)));
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that have no keywords specified.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade ThatHaveNoKeywords()
    {
      this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (pd => string.IsNullOrEmpty((string) pd.Keywords)));
      return this;
    }

    /// <summary>
    /// Filters the pages to include only the pages that have no title specified.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade ThatHaveNoTitles()
    {
      this.PageDataList = this.PageDataList.Where<PageData>((Expression<Func<PageData, bool>>) (pd => string.IsNullOrEmpty((string) pd.HtmlTitle)));
      return this;
    }

    /// <summary>
    /// Filters the pages to get all the pages in a specific culture or if there is no such a translations gets the pages in the default culture or the fist one that is available.
    /// </summary>
    /// <param name="culture">Culture info.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade ThatAreForLanguage(CultureInfo language)
    {
      this.cultureFilter = language;
      return this;
    }

    /// <summary>
    /// Filters the pages to get all the pages with a translations in any culture.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    internal virtual PagesFacade ThatAreForAnyLanguage()
    {
      this.anyCulture = true;
      return this;
    }

    /// <summary>
    /// Filters the pages having versions after specified date.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual Dictionary<PageNode, List<Change>> VersionsAfter(
      DateTime date)
    {
      return this.InternalGetVersionsForPages((Func<Change, bool>) (change => change.LastModified > date));
    }

    /// <summary>
    /// Filters the page having versions before specified date.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual Dictionary<PageNode, List<Change>> VersionsBefore(
      DateTime date)
    {
      return this.InternalGetVersionsForPages((Func<Change, bool>) (change => change.LastModified < date));
    }

    /// <summary>
    /// Filters the page having versions between specified start and end dates.
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual Dictionary<PageNode, List<Change>> VersionsBetween(
      DateTime startDate,
      DateTime endDate)
    {
      return this.InternalGetVersionsForPages((Func<Change, bool>) (change => change.LastModified > startDate && change.LastModified < endDate));
    }

    /// <summary>
    /// Gets the count of items in collection at current facade.
    /// </summary>
    /// <param name="result">The count of items.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade Count(out int result)
    {
      result = this.PageNodes.Count<PageNode>();
      return this;
    }

    /// <summary>
    ///  Orders the items of collection in ascending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade OrderBy<TKey>(Expression<Func<PageNode, TKey>> keySelector)
    {
      this.PageNodes = keySelector != null ? (IQueryable<PageNode>) this.PageNodes.OrderBy<PageNode, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Orders the items of collection in descending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade OrderByDescending<TKey>(
      Expression<Func<PageNode, TKey>> keySelector)
    {
      this.PageNodes = keySelector != null ? (IQueryable<PageNode>) this.PageNodes.OrderBy<PageNode, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>Sets the specified query.</summary>
    /// <param name="query">The query.</param>
    /// <returns>The facade instance.</returns>
    public virtual PagesFacade Set(IQueryable<PageNode> query)
    {
      this.PageNodes = query != null ? query : throw new ArgumentNullException(nameof (query));
      return this;
    }

    /// <summary>
    /// Bypasses a specified number of items of collection and then returns the remaining elements.
    /// </summary>
    /// <param name="count">The number of items to bypass.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade Skip(int count)
    {
      this.PageNodes = count >= 0 ? this.PageNodes.Skip<PageNode>(count) : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Takes items from collection number of which is specified with the count parameter.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade Take(int count)
    {
      this.PageNodes = count >= 0 ? this.PageNodes.Take<PageNode>(count) : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Filters items of the collection by specified where filter expression.
    /// </summary>
    /// <param name="filterExpression">The filter expression.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade Where(string filterExpression)
    {
      if (!string.IsNullOrEmpty(filterExpression))
        this.PageNodes = this.PageNodes.Where<PageNode>(filterExpression).AsQueryable<PageNode>();
      return this;
    }

    /// <summary>
    /// Filters items of the collection by specified where clause at predicate parameter.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PagesFacade Where(Func<PageNode, bool> predicate)
    {
      this.PageNodes = predicate != null ? this.PageNodes.Where<PageNode>(predicate).AsQueryable<PageNode>() : throw new ArgumentNullException(nameof (predicate));
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade SaveChanges()
    {
      base.SaveChanges();
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> object.</returns>
    public virtual PagesFacade CancelChanges()
    {
      base.CancelChanges();
      return this;
    }

    /// <summary>Specifies a scope of pages to search in.</summary>
    /// <param name="pageIds">The page ids.</param>
    /// <returns>The facade instance.</returns>
    public PagesFacade InPages(IEnumerable<Guid> pageIds)
    {
      Guid[] pageIdsArray = pageIds.ToArray<Guid>();
      this.PageNodes = this.PageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (x => pageIdsArray.Contains<Guid>(x.Id)));
      return this;
    }

    internal PagesWorkflowFacade UseWorkflowFiltering() => new PagesWorkflowFacade(this, (IWorkflowUtils) new WorkflowUtils());

    private Dictionary<PageNode, List<Change>> InternalGetVersionsForPages(
      Func<Change, bool> where)
    {
      Dictionary<PageNode, List<Change>> versionsForPages = new Dictionary<PageNode, List<Change>>();
      foreach (PageNode pageNode in (IEnumerable<PageNode>) this.PageNodes)
      {
        Guid pageId = pageNode.GetPageData().Id;
        List<Change> list = this.VersionManager.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (change => change.Parent.Id == pageId)).Where<Change>(where).ToList<Change>();
        versionsForPages.Add(pageNode, list);
      }
      return versionsForPages;
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) this.PageManager;
  }
}
