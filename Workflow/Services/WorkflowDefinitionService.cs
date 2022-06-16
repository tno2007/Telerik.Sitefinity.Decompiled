// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.WorkflowDefinitionService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Configuration.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Workflow.Configuration;
using Telerik.Sitefinity.Workflow.Model;
using Telerik.Sitefinity.Workflow.Services.Data;

namespace Telerik.Sitefinity.Workflow.Services
{
  /// <summary>Service for working with workflow definitions.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class WorkflowDefinitionService : IWorkflowDefinitionService
  {
    /// <summary>
    /// Gets the collection of <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel" /> in JSON format.
    /// </summary>
    /// <param name="provider">The name of the workflow provider from which the workflow definitions should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved workflow definitions.</param>
    /// <param name="skip">The number of workflow definitions to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of workflow definitions to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved workflow definitions.</param>
    /// <param name="workflowFilter">The workflow filter with expression by key and value. The key can be ContentScope, SiteScope and CultureScope and the corresponding value.</param>
    /// <returns>
    ///        <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with workflow definitions and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<WorkflowDefinitionViewModel> GetWorkflowDefinitions(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string workflowFilter)
    {
      return this.GetWorkflowDefinitionsInternal(provider, sortExpression, skip, take, filter, workflowFilter);
    }

    /// <summary>
    /// Gets the collection of <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel" /> in XML format.
    /// </summary>
    /// <param name="provider">The name of the workflow provider from which the workflow definitions should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved workflow definitions.</param>
    /// <param name="skip">The number of workflow definitions to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of workflow definitions to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved workflow definitions.</param>
    /// <param name="workflowFilter">The workflow filter with expression by key and value.The key can be ContentScope, SiteScope and CultureScope and the corresponding value.</param>
    /// <returns>
    ///      <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with workflow definitions and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<WorkflowDefinitionViewModel> GetWorkflowDefinitionsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string workflowFilter)
    {
      return this.GetWorkflowDefinitionsInternal(provider, sortExpression, skip, take, filter, workflowFilter);
    }

    /// <summary>
    /// Saves the workflow definition and returns the saved definition in JSON.
    /// </summary>
    /// <param name="workflowDefinitionViewModel">The view model of the workflow definition.</param>
    /// <param name="workflowDefinitionId">The workflow definition id.</param>
    /// <param name="provider">The provider name through which the workflow definition ought to be saved.</param>
    /// <returns>The saved workflow definition view model object.</returns>
    public WorkflowDefinitionViewModel SaveWorkflowDefinition(
      WorkflowDefinitionViewModel workflowDefinitionViewModel,
      string workflowDefinitionId,
      string provider)
    {
      return this.SaveWorkflowDefinitionInternal(workflowDefinitionViewModel, workflowDefinitionId, provider);
    }

    /// <summary>
    /// Saves the workflow definition and returns the saved definition in XML.
    /// </summary>
    /// <param name="workflowDefinitionViewModel">The view model of the workflow definition.</param>
    /// <param name="workflowDefinitionId">The workflow definition id.</param>
    /// <param name="provider">The provider name through which the workflow definition ought to be saved.</param>
    /// <returns>The saved workflow definition view model object.</returns>
    public WorkflowDefinitionViewModel SaveWorkflowDefinitionInXml(
      WorkflowDefinitionViewModel workflowDefinitionViewModel,
      string workflowDefinitionId,
      string provider)
    {
      return this.SaveWorkflowDefinitionInternal(workflowDefinitionViewModel, workflowDefinitionId, provider);
    }

    /// <summary>Deletes the specified workflow definitions.</summary>
    /// <param name="workflowDefinitionIds">Ids of the workflow definitions to be deleted.</param>
    /// <param name="provider">The name of the workflow provider from which the workflow definition should be deleted.</param>
    public void DeleteWorkflowDefinitions(string[] workflowDefinitionIds, string provider) => this.DeleteWorkflowDefinitionsInternal(workflowDefinitionIds, provider);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public CollectionContext<CultureViewModel> GetLanguagesForSites(
      Guid[] siteIds,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      List<CultureInfo> source = new List<CultureInfo>();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null)
      {
        if (siteIds == null || siteIds != null && siteIds.Length == 0)
        {
          source = multisiteContext.GetSites().SelectMany<Telerik.Sitefinity.Multisite.ISite, CultureInfo>((Func<Telerik.Sitefinity.Multisite.ISite, IEnumerable<CultureInfo>>) (x => (IEnumerable<CultureInfo>) x.PublicContentCultures)).ToList<CultureInfo>();
        }
        else
        {
          foreach (Guid siteId in siteIds)
          {
            Telerik.Sitefinity.Multisite.ISite siteById = multisiteContext.GetSiteById(siteId);
            source.AddRange((IEnumerable<CultureInfo>) siteById.PublicContentCultures);
          }
        }
      }
      else
        source = ((IEnumerable<CultureInfo>) SystemManager.CurrentContext.CurrentSite.PublicContentCultures).ToList<CultureInfo>();
      int? totalCount = new int?(0);
      return new CollectionContext<CultureViewModel>((IEnumerable<CultureViewModel>) DataProviderBase.SetExpressions<CultureViewModel>(source.Distinct<CultureInfo>().Select<CultureInfo, CultureViewModel>((Func<CultureInfo, CultureViewModel>) (x => new CultureViewModel(x))).AsQueryable<CultureViewModel>(), filter, (string) null, new int?(skip), new int?(take), ref totalCount).ToList<CultureViewModel>())
      {
        TotalCount = totalCount.Value
      };
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public CollectionContext<WorkflowTypeScopeViewModel> GetContentScope(
      int skip,
      int take,
      string filter)
    {
      List<WorkflowElement> workflowContentTypes = WorkflowManager.GetWorkflowContentTypes();
      List<WorkflowTypeScopeViewModel> source = new List<WorkflowTypeScopeViewModel>();
      foreach (WorkflowElement workflowElement in workflowContentTypes)
      {
        string scopeUiTitle = WorkflowManager.GetScopeUITitle(workflowElement.ContentType);
        if (!string.IsNullOrEmpty(scopeUiTitle))
          source.Add(new WorkflowTypeScopeViewModel()
          {
            Title = scopeUiTitle,
            ContentType = workflowElement.ContentType,
            IncludeChildren = true
          });
      }
      int? totalCount = new int?(0);
      return new CollectionContext<WorkflowTypeScopeViewModel>((IEnumerable<WorkflowTypeScopeViewModel>) DataProviderBase.SetExpressions<WorkflowTypeScopeViewModel>(source.AsQueryable<WorkflowTypeScopeViewModel>(), filter, (string) null, new int?(skip), new int?(take), ref totalCount))
      {
        TotalCount = totalCount.Value
      };
    }

    private WorkflowDefinitionViewModel SaveWorkflowDefinitionInternal(
      WorkflowDefinitionViewModel workflowDefinitionViewModel,
      string workflowDefinitionId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      WorkflowManager manager = WorkflowManager.GetManager(provider);
      Guid workflowDefinitionIdGuid = new Guid(workflowDefinitionId);
      WorkflowDefinition workflowDefinition = manager.GetWorkflowDefinitions().Where<WorkflowDefinition>((Expression<Func<WorkflowDefinition, bool>>) (wd => wd.Id == workflowDefinitionIdGuid)).SingleOrDefault<WorkflowDefinition>() ?? manager.CreateWorkflowDefinition();
      WorkflowManager.CopyToWorkflowDefinition(workflowDefinitionViewModel, workflowDefinition, manager);
      manager.SaveChanges();
      WorkflowDefinitionViewModel target = new WorkflowDefinitionViewModel();
      WorkflowManager.CopyToWorkflowDefinitionViewModel(workflowDefinition, target);
      return target;
    }

    private CollectionContext<WorkflowDefinitionViewModel> GetWorkflowDefinitionsInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string workflowFilter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      IEnumerable<WorkflowDefinitionProxy> workflowDefinitionProxies = WorkflowDefinitionsCache.GetCache().GetAll();
      int? totalCount = new int?(0);
      List<WorkflowDefinitionViewModel> items = new List<WorkflowDefinitionViewModel>();
      if (!string.IsNullOrEmpty(workflowFilter))
        workflowDefinitionProxies = WorkflowDefinitionService.FilterWorkflowScopes(workflowFilter, workflowDefinitionProxies);
      foreach (WorkflowDefinitionProxy source in (IEnumerable<WorkflowDefinitionProxy>) DataProviderBase.SetExpressions<WorkflowDefinitionProxy>(workflowDefinitionProxies.AsQueryable<WorkflowDefinitionProxy>(), filter, sortExpression, new int?(skip), new int?(take), ref totalCount).ToList<WorkflowDefinitionProxy>())
      {
        WorkflowDefinitionViewModel definitionViewModel = new WorkflowDefinitionViewModel();
        WorkflowDefinitionViewModel target = definitionViewModel;
        WorkflowManager.CopyToWorkflowDefinitionViewModel(source, target);
        items.Add(definitionViewModel);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<WorkflowDefinitionViewModel>((IEnumerable<WorkflowDefinitionViewModel>) items)
      {
        TotalCount = totalCount.Value
      };
    }

    private static IEnumerable<WorkflowDefinitionProxy> FilterWorkflowScopes(
      string workflowFilter,
      IEnumerable<WorkflowDefinitionProxy> workflowDefinitions)
    {
      string[] separator = new string[1]{ "=" };
      string[] strArray = workflowFilter.Split(separator, StringSplitOptions.RemoveEmptyEntries);
      List<WorkflowDefinitionProxy> workflowDefinitionProxyList = new List<WorkflowDefinitionProxy>();
      foreach (WorkflowDefinitionProxy workflowDefinition in workflowDefinitions)
      {
        List<Guid> list = workflowDefinition.Scopes.Where<IWorkflowExecutionScope>((Func<IWorkflowExecutionScope, bool>) (s => s.SiteId != Guid.Empty)).Select<IWorkflowExecutionScope, Guid>((Func<IWorkflowExecutionScope, Guid>) (s => s.SiteId)).ToList<Guid>();
        IEnumerable<string> collection1 = workflowDefinition.Scopes.SelectMany<IWorkflowExecutionScope, string>((Func<IWorkflowExecutionScope, IEnumerable<string>>) (s => s.Cultures)).ToList<string>().Distinct<string>();
        IEnumerable<string> collection2 = workflowDefinition.Scopes.SelectMany<IWorkflowExecutionScope, WorkflowTypeScopeProxy>((Func<IWorkflowExecutionScope, IEnumerable<WorkflowTypeScopeProxy>>) (s => (IEnumerable<WorkflowTypeScopeProxy>) s.TypeScopes)).Select<WorkflowTypeScopeProxy, string>((Func<WorkflowTypeScopeProxy, string>) (ts => ts.ContentType)).ToList<string>().Distinct<string>();
        bool flag = false;
        if (strArray[0] == "SiteScope")
          flag = WorkflowDefinitionService.Filter<Guid>((IEnumerable<Guid>) list, Guid.Parse(strArray[1]));
        else if (strArray[0] == "CultureScope")
        {
          HashSet<CultureInfo> cultureInfoSet = new HashSet<CultureInfo>();
          if (list.Count<Guid>() > 0)
          {
            foreach (Guid siteId in list)
            {
              foreach (CultureInfo publicContentCulture in SystemManager.CurrentContext.MultisiteContext.GetSiteById(siteId).PublicContentCultures)
                cultureInfoSet.Add(publicContentCulture);
            }
            if (cultureInfoSet.Contains(CultureInfo.GetCultureInfo(strArray[1])))
              flag = WorkflowDefinitionService.Filter<string>(collection1, strArray[1]);
          }
          else
            flag = WorkflowDefinitionService.Filter<string>(collection1, strArray[1]);
        }
        else if (strArray[0] == "ContentScope")
          flag = WorkflowDefinitionService.Filter<string>(collection2, strArray[1]);
        if (flag)
          workflowDefinitionProxyList.Add(workflowDefinition);
      }
      return (IEnumerable<WorkflowDefinitionProxy>) workflowDefinitionProxyList;
    }

    private static bool Filter<T>(IEnumerable<T> collection, T argument) => !collection.Any<T>() || collection.Contains<T>(argument);

    private void DeleteWorkflowDefinitionsInternal(string[] workflowDefinitionIds, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      WorkflowManager manager = WorkflowManager.GetManager(provider);
      foreach (string workflowDefinitionId1 in workflowDefinitionIds)
      {
        Guid workflowDefinitionId2 = new Guid(workflowDefinitionId1);
        manager.Delete(manager.GetWorkflowDefinition(workflowDefinitionId2));
      }
      manager.SaveChanges();
    }
  }
}
