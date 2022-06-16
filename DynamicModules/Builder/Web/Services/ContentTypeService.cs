// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.Services.ContentTypeService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Builder.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.Services
{
  /// <summary>Web service for working with content types.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  [AuthenticateWcf(true)]
  internal class ContentTypeService : IContentTypeService
  {
    private IHtmlSanitizer sanitizer;
    private bool disableHtmlSanitization = Config.Get<SecurityConfig>().DisableHtmlSanitization;

    /// <summary>
    /// Checks if module with the specified name already exist
    /// </summary>
    /// <param name="moduleName">Name of the module, which should be checked.</param>
    /// <returns>Json data representing the result of checking if the specified module name already exists.</returns>
    public ModuleNameContext CheckIfModuleWithSpecifiedNameAlreadyExists(
      string moduleName)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      return new ModuleNameContext()
      {
        IsContained = manager.ModuleExists(moduleName)
      };
    }

    /// <summary>
    /// Gets a module with the specified id from the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="moduleId"></param>
    /// <param name="provider">Name of the provider from which the module ought to be retrieved.</param>
    /// <returns>
    ///   <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> object.
    /// </returns>
    public ContentTypeContext GetModule(string moduleId, string provider) => this.GetModuleInternal(moduleId, provider);

    /// <summary>
    /// Gets a module with the specified id from the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="moduleId"></param>
    /// <param name="provider">Name of the provider from which the module ought to be retrieved.</param>
    /// <returns>
    ///   <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> object.
    /// </returns>
    public ContentTypeContext GetModuleInXml(string moduleId, string provider) => this.GetModuleInternal(moduleId, provider);

    /// <summary>
    /// Gets all the content types of the content types for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content types.</param>
    /// <param name="skip">Number of content types to skip in result set. (used for paging)</param>
    /// <param name="take">Number of content types to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.</returns>
    public CollectionContext<ContentTypeContext> GetModules(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetModulesInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all the content types of the content types for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content types.</param>
    /// <param name="skip">Number of content types to skip in result set. (used for paging)</param>
    /// <param name="take">Number of content types to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.</returns>
    public CollectionContext<ContentTypeContext> GetModulesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetModulesInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all the content types registered in the specified module with the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content types.</param>
    /// <param name="skip">Number of content types to skip in result set. (used for paging)</param>
    /// <param name="take">Number of content types to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    public CollectionContext<ContentTypeContext> GetContentTypes(
      string moduleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetContentTypesInternal(moduleId, provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all the content types registered in the specified module with the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content types.</param>
    /// <param name="skip">Number of content types to skip in result set. (used for paging)</param>
    /// <param name="take">Number of content types to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    public CollectionContext<ContentTypeContext> GetContentTypesInXml(
      string moduleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetContentTypesInternal(moduleId, provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all content types available to be set as parent to the desired content type. The results are returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="contentTypeId">Id of the desired content type.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    public CollectionContext<ContentTypeContext> GetContentTypeAvailableParents(
      string moduleId,
      string contentTypeId,
      string provider)
    {
      return this.GetContentTypeAvailableParentsInternal(moduleId, contentTypeId, provider);
    }

    /// <summary>
    /// Gets all content types available to be set as parent to the desired content type. The results are returned in XML format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="contentTypeId">Id of the desired content type.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    public CollectionContext<ContentTypeContext> GetContentTypeAvailableParentsInXml(
      string moduleId,
      string contentTypeId,
      string provider)
    {
      return this.GetContentTypeAvailableParentsInternal(moduleId, contentTypeId, provider);
    }

    /// <summary>
    /// Gets content types tree for all content types registered in the specified module with the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    public CollectionContext<ContentTypeTreeItemContext> GetContentTypesTree(
      string moduleId,
      string provider,
      string filter)
    {
      return this.GetContentTypesTreeInternal(moduleId, provider, filter);
    }

    /// <summary>
    /// Gets content types tree for all content types registered in the specified module with the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    public CollectionContext<ContentTypeTreeItemContext> GetContentTypesTreeInXml(
      string moduleId,
      string provider,
      string filter)
    {
      return this.GetContentTypesTreeInternal(moduleId, provider, filter);
    }

    /// <summary>
    /// Gets a content type with the specified id from the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="moduleId"></param>
    /// <param name="contentTypeId">Id of the desired content type.</param>
    /// <param name="provider">Name of the provider from which the content type ought to be retrieved.</param>
    /// <returns>
    ///   <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> object.
    /// </returns>
    public ContentTypeContext GetContentType(
      string moduleId,
      string contentTypeId,
      string provider)
    {
      return this.GetContentTypeInternal(moduleId, contentTypeId, provider);
    }

    /// <summary>
    /// Gets a content type with the specified id from the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="moduleId"></param>
    /// <param name="contentTypeId">Id of the desired content type.</param>
    /// <param name="provider">Name of the provider from which the content type ought to be retrieved.</param>
    /// <returns>
    ///   <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> object.
    /// </returns>
    public ContentTypeContext GetContentTypeInXml(
      string moduleId,
      string contentTypeId,
      string provider)
    {
      return this.GetContentTypeInternal(moduleId, contentTypeId, provider);
    }

    /// <summary>
    /// Saves a content types. If the content type with specified id exists that content type will be updated; otherwise new content type will be created.
    /// The saved content type is returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The Id of the module to which the content type belongs to.</param>
    /// <param name="contentType">The content type context that is going to be saved.</param>
    /// <param name="provider">The provider through which the product ought to be saved.</param>
    /// <param name="updateWidgetTemplates">if set to <c>true</c> [update widget templates].</param>
    /// <returns>The content type context.</returns>
    public ContentTypeContext SaveContentType(
      string moduleId,
      ContentTypeContext contentType,
      string provider,
      bool updateWidgetTemplates)
    {
      return this.SaveContentTypeInternal(moduleId, contentType, provider, updateWidgetTemplates);
    }

    /// <summary>
    /// Saves a content types. If the content type with specified id exists that content type will be updated; otherwise new content type will be created.
    /// The saved content type is returned in XML format.
    /// </summary>
    /// <param name="moduleId">The Id of the module to which the content type belongs to.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="provider">The provider through which the product ought to be saved.</param>
    /// <param name="updateWidgetTemplates">if set to <c>true</c> [update widget templates].</param>
    /// <returns>The content type context.</returns>
    public ContentTypeContext SaveContentTypeInXml(
      string moduleId,
      ContentTypeContext contentType,
      string provider,
      bool updateWidgetTemplates)
    {
      return this.SaveContentTypeInternal(moduleId, contentType, provider, updateWidgetTemplates);
    }

    /// <summary>
    /// Gets all the content types under specific content type registered in the specified module with the given provider. The saved content type is returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="contentTypeId">The content type id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public CollectionContext<ContentTypeContext> GetContentTypeChildren(
      string moduleId,
      string contentTypeId,
      string provider)
    {
      return this.GetContentTypeChildrenInternal(moduleId, contentTypeId, provider);
    }

    /// <summary>
    /// Gets all the content types under specific content type registered in the specified module with the given provider. The saved content type is returned in XML format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="contentTypeId">The content type id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public CollectionContext<ContentTypeContext> GetContentTypeChildrenInXml(
      string moduleId,
      string contentTypeId,
      string provider)
    {
      return this.GetContentTypeChildrenInternal(moduleId, contentTypeId, provider);
    }

    /// <summary>
    /// Activates the module. The module is returned in JSON format.
    /// </summary>
    /// <param name="moduleId"></param>
    /// <param name="provider">The provider through which the module ought to be activated.</param>
    /// <returns>The module context.</returns>
    public ContentTypeContext ActivateModule(string moduleId, string provider) => this.ActivateContentTypeInternal(moduleId, provider);

    /// <summary>Activates the module.</summary>
    /// <param name="moduleId"></param>
    /// <param name="provider">The provider through which the module ought to be activated.</param>
    /// <returns>The module context.</returns>
    public ContentTypeContext ActivateContentTypeInXml(
      string moduleId,
      string provider)
    {
      return this.ActivateContentTypeInternal(moduleId, provider);
    }

    /// <summary>Deactivates the module.</summary>
    /// <param name="moduleId"></param>
    /// <param name="provider">The provider through which the module ought to be deactivated.</param>
    /// <returns>The module context.</returns>
    public ContentTypeContext DeactivateModule(string moduleId, string provider) => this.DeactivateContentTypeInternal(moduleId, provider);

    /// <summary>Deactivates the module.</summary>
    /// <param name="moduleId"></param>
    /// <param name="provider">The provider through which the module ought to be deactivated.</param>
    /// <returns>The module context.</returns>
    public ContentTypeContext DeactivateModuleInXml(
      string moduleId,
      string provider)
    {
      return this.DeactivateContentTypeInternal(moduleId, provider);
    }

    /// <summary>Updates content type name and description.</summary>
    /// <param name="contentTypeId">Id of the content type to be updated.</param>
    /// <param name="contentTypeContext">The content type context that is going to be saved.</param>
    /// <returns>
    /// The saved content type name and description is returned in JSON format.
    /// </returns>
    public ContentTypeSimpleContext UpdateModuleNameAndDescription(
      string moduleId,
      ContentTypeSimpleContext contentTypeContext)
    {
      return this.UpdateModuleNameAndDescriptionInternal(moduleId, contentTypeContext);
    }

    /// <summary>Deletes the specified module.</summary>
    /// <param name="moduleId"></param>
    /// <param name="provider">The provider.</param>
    /// <param name="deleteData">Determines whether the module data should be deleted</param>
    /// <returns></returns>
    public DeleteContentTypeContext DeleteModule(
      string moduleId,
      string provider,
      bool deleteData)
    {
      return this.DeleteContentTypeInternal(moduleId, provider, deleteData);
    }

    /// <summary>Deletes the specified module.</summary>
    /// <param name="moduleId"></param>
    /// <param name="provider">The provider.</param>
    /// <param name="deleteData">Determines whether the module data should be deleted</param>
    /// <returns></returns>
    public DeleteContentTypeContext DeleteModuleInXml(
      string moduleId,
      string provider,
      bool deleteData)
    {
      return this.DeleteContentTypeInternal(moduleId, provider, deleteData);
    }

    /// <summary>Deletes the specified dynamic content type.</summary>
    /// <param name="contentTypeId">The dynamic content type id.</param>
    /// <param name="provider">The provider.</param>
    public DeleteContentTypeContext DeleteDynamicContentType(
      string contentTypeId,
      string moduleId,
      string provider)
    {
      return this.DeleteDynamicContentTypeInternal(contentTypeId, moduleId, provider);
    }

    private ContentTypeContext GetModuleInternal(string moduleId, string provider)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(provider);
      Guid moduleGuid = new Guid(moduleId);
      DynamicModule dynamicModule = manager.GetDynamicModule(moduleGuid);
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleGuid)).FirstOrDefault<DynamicModuleType>();
      ServiceUtility.DisableCache();
      return ContentTypeService.GetContentTypeContext((IDynamicModule) dynamicModule, dynamicModuleType);
    }

    private CollectionContext<ContentTypeContext> GetModulesInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      IQueryable<IDynamicModule> source1 = ModuleBuilderManager.GetModules().AsQueryable<IDynamicModule>();
      int num = source1.Count<IDynamicModule>();
      IQueryable<IDynamicModule> source2;
      if (!string.IsNullOrEmpty(sortExpression))
        source2 = source1.OrderBy<IDynamicModule>(sortExpression);
      else
        source2 = (IQueryable<IDynamicModule>) source1.OrderBy<IDynamicModule, DateTime>((Expression<Func<IDynamicModule, DateTime>>) (m => m.LastModified));
      if (!string.IsNullOrEmpty(filter))
        source2 = source2.Where<IDynamicModule>(filter);
      if (skip > 0)
        source2 = source2.Skip<IDynamicModule>(skip);
      if (take > 0)
        source2 = source2.Take<IDynamicModule>(take);
      List<ContentTypeContext> items = new List<ContentTypeContext>();
      foreach (IDynamicModule dynamicModule in (IEnumerable<IDynamicModule>) source2)
      {
        ContentTypeContext contentTypeContext = ContentTypeService.GetContentTypeContext(dynamicModule, (DynamicModuleType) null);
        items.Add(contentTypeContext);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<ContentTypeContext>((IEnumerable<ContentTypeContext>) items)
      {
        TotalCount = num
      };
    }

    private CollectionContext<ContentTypeContext> GetContentTypesInternal(
      string moduleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      Guid moduleGuid = moduleId.IsGuid() ? new Guid(moduleId) : throw new ArgumentException("ModuleId must be a valid GUID.");
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(provider);
      DynamicModule dynamicModule = manager.GetDynamicModule(moduleGuid);
      IQueryable<DynamicModuleType> source1 = manager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleGuid));
      int num = source1.Count<DynamicModuleType>();
      IQueryable<DynamicModuleType> source2;
      if (!string.IsNullOrEmpty(sortExpression))
        source2 = source1.OrderBy<DynamicModuleType>(sortExpression);
      else
        source2 = (IQueryable<DynamicModuleType>) source1.OrderBy<DynamicModuleType, DateTime>((Expression<Func<DynamicModuleType, DateTime>>) (m => m.LastModified));
      if (!string.IsNullOrEmpty(filter))
        source2 = source2.Where<DynamicModuleType>(filter);
      if (skip > 0)
        source2 = source2.Skip<DynamicModuleType>(skip);
      if (take > 0)
        source2 = source2.Take<DynamicModuleType>(take);
      List<ContentTypeContext> items = new List<ContentTypeContext>();
      foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) source2)
      {
        ContentTypeContext contentTypeContext = ContentTypeService.GetContentTypeContext((IDynamicModule) dynamicModule, dynamicModuleType);
        items.Add(contentTypeContext);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<ContentTypeContext>((IEnumerable<ContentTypeContext>) items)
      {
        TotalCount = num
      };
    }

    public CollectionContext<ContentTypeContext> GetContentTypeAvailableParentsInternal(
      string moduleId,
      string contentTypeId,
      string provider)
    {
      if (!moduleId.IsGuid())
        throw new ArgumentException("ModuleId must be a valid GUID.");
      if (!contentTypeId.IsGuid())
        throw new ArgumentException("ContentTypeId must be a valid GUID.");
      Guid moduleGuid = new Guid(moduleId);
      Guid contentTypeGuid = new Guid(contentTypeId);
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(provider);
      DynamicModule dynamicModule = manager.GetDynamicModule(moduleGuid);
      List<DynamicModuleType> list = manager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleGuid && !t.Id.Equals(contentTypeGuid) && !t.IsSelfReferencing)).ToList<DynamicModuleType>();
      IEnumerable<Guid> successorIds = this.GetTypeSuccessors(contentTypeGuid, (IEnumerable<DynamicModuleType>) list).Select<DynamicModuleType, Guid>((Func<DynamicModuleType, Guid>) (s => s.Id));
      List<ContentTypeContext> contentTypeContextList = new List<ContentTypeContext>();
      foreach (DynamicModuleType rootType in list.Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t =>
      {
        Guid parentModuleTypeId = t.ParentModuleTypeId;
        return t.ParentModuleTypeId == Guid.Empty;
      })))
      {
        IEnumerable<DynamicModuleType> dynamicModuleTypes = this.GetTypeHierarchy(rootType, (IEnumerable<DynamicModuleType>) list).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => !successorIds.Contains<Guid>(t.Id) && !t.Id.Equals(contentTypeGuid)));
        Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
        foreach (DynamicModuleType dynamicModuleType in dynamicModuleTypes)
        {
          ContentTypeContext contentTypeContext = ContentTypeService.GetContentTypeContext((IDynamicModule) dynamicModule, dynamicModuleType);
          int count = dictionary.ContainsKey(dynamicModuleType.ParentModuleTypeId) ? dictionary[dynamicModuleType.ParentModuleTypeId] + 1 : 0;
          dictionary[dynamicModuleType.Id] = count;
          contentTypeContext.ContentTypeItemTitle = string.Concat(Enumerable.Repeat<string>("— ", count)) + contentTypeContext.ContentTypeItemTitle;
          contentTypeContextList.Add(contentTypeContext);
        }
      }
      ServiceUtility.DisableCache();
      int num = contentTypeContextList.Count<ContentTypeContext>();
      return new CollectionContext<ContentTypeContext>((IEnumerable<ContentTypeContext>) contentTypeContextList)
      {
        TotalCount = num
      };
    }

    private IEnumerable<DynamicModuleType> GetTypeHierarchy(
      DynamicModuleType rootType,
      IEnumerable<DynamicModuleType> moduleTypes)
    {
      List<DynamicModuleType> typeHierarchy = new List<DynamicModuleType>();
      Stack<DynamicModuleType> dynamicModuleTypeStack = new Stack<DynamicModuleType>();
      dynamicModuleTypeStack.Push(rootType);
      while (dynamicModuleTypeStack.Count > 0)
      {
        DynamicModuleType currentType = dynamicModuleTypeStack.Pop();
        typeHierarchy.Add(currentType);
        foreach (DynamicModuleType dynamicModuleType in moduleTypes.Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == currentType.Id)).Reverse<DynamicModuleType>())
          dynamicModuleTypeStack.Push(dynamicModuleType);
      }
      return (IEnumerable<DynamicModuleType>) typeHierarchy;
    }

    private IEnumerable<DynamicModuleType> GetTypeSuccessors(
      Guid contentTypeGuid,
      IEnumerable<DynamicModuleType> contentTypes)
    {
      List<DynamicModuleType> typeSuccessors = new List<DynamicModuleType>();
      if (Guid.Empty.Equals(contentTypeGuid))
        return (IEnumerable<DynamicModuleType>) typeSuccessors;
      IEnumerable<DynamicModuleType> dynamicModuleTypes = contentTypes.Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (ct => ct.ParentModuleTypeId == contentTypeGuid));
      if (dynamicModuleTypes != null)
      {
        foreach (DynamicModuleType dynamicModuleType in dynamicModuleTypes)
        {
          typeSuccessors.Add(dynamicModuleType);
          typeSuccessors.AddRange(this.GetTypeSuccessors(dynamicModuleType.Id, contentTypes));
        }
      }
      return (IEnumerable<DynamicModuleType>) typeSuccessors;
    }

    private CollectionContext<ContentTypeTreeItemContext> GetContentTypesTreeInternal(
      string moduleId,
      string provider,
      string filter)
    {
      Guid moduleGuid = moduleId.IsGuid() ? new Guid(moduleId) : throw new ArgumentException("ModuleId must be a valid GUID.");
      IQueryable<DynamicModuleType> source = ModuleBuilderManager.GetManager(provider).GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleGuid));
      int num = source.Count<DynamicModuleType>();
      if (!string.IsNullOrEmpty(filter))
        source = source.Where<DynamicModuleType>(filter);
      List<ContentTypeTreeItemContext> contentTypeTreeItems = new List<ContentTypeTreeItemContext>();
      foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) source)
      {
        ContentTypeTreeItemContext contentTypeTreeItem = this.GetContentTypeTreeItem(dynamicModuleType);
        contentTypeTreeItems.Add(contentTypeTreeItem);
      }
      IEnumerable<ContentTypeTreeItemContext> items = this.BuildContentTypeTreeHierarchy((IEnumerable<ContentTypeTreeItemContext>) contentTypeTreeItems);
      ServiceUtility.DisableCache();
      return new CollectionContext<ContentTypeTreeItemContext>(items)
      {
        TotalCount = num
      };
    }

    private ContentTypeContext GetContentTypeInternal(
      string moduleId,
      string contentTypeId,
      string provider)
    {
      if (!moduleId.IsGuid())
        throw new ArgumentException("ModuleId must be a valid GUID.");
      if (!contentTypeId.IsGuid())
        throw new ArgumentException("ContentTypeId must be a valid GUID.");
      Guid moduleGuid = new Guid(moduleId);
      Guid contentTypeGuid = new Guid(contentTypeId);
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(provider);
      DynamicModule dynamicModule = manager.GetDynamicModule(moduleGuid);
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleGuid && t.Id == contentTypeGuid)).FirstOrDefault<DynamicModuleType>();
      manager.LoadDynamicModuleGraph(dynamicModule, true, true);
      ContentTypeContext contentTypeContext = ContentTypeService.GetContentTypeContext((IDynamicModule) dynamicModule, dynamicModuleType, true);
      contentTypeContext.IsDeletable = manager.CanDeleteDynamicModuleType(dynamicModule, dynamicModuleType);
      ServiceUtility.DisableCache();
      return contentTypeContext;
    }

    internal static ContentTypeContext GetContentTypeContext(
      IDynamicModule dynamicModule,
      DynamicModuleType dynamicModuleType,
      bool loadFields = false)
    {
      ContentTypeContext contentTypeContext = new ContentTypeContext();
      contentTypeContext.ModuleId = dynamicModule.Id;
      contentTypeContext.ContentTypeName = dynamicModule.Name;
      contentTypeContext.ContentTypeTitle = dynamicModule.Title;
      contentTypeContext.ContentTypeStatus = dynamicModule.Status.ToString();
      contentTypeContext.Owner = UserProfilesHelper.GetUserDisplayName(dynamicModule.Owner);
      switch (dynamicModule.Status)
      {
        case DynamicModuleStatus.NotInstalled:
          contentTypeContext.ContentTypeStatusTitle = Res.Get<ModuleBuilderResources>().Inactive;
          break;
        case DynamicModuleStatus.Active:
          contentTypeContext.ContentTypeStatusTitle = Res.Get<ModuleBuilderResources>().Active;
          break;
        case DynamicModuleStatus.Inactive:
          contentTypeContext.ContentTypeStatusTitle = Res.Get<ModuleBuilderResources>().Inactive;
          break;
      }
      contentTypeContext.ContentTypeDescription = dynamicModule.Description;
      contentTypeContext.LastModified = dynamicModule.LastModified.ToString("MMM dd, yyyy");
      if (dynamicModuleType != null)
      {
        contentTypeContext.ContentTypeItemTitle = dynamicModuleType.DisplayName;
        contentTypeContext.ContentTypeItemName = dynamicModuleType.TypeName;
        contentTypeContext.ContentTypeId = dynamicModuleType.Id;
        contentTypeContext.MainShortTextFieldName = dynamicModuleType.MainShortTextFieldName;
        contentTypeContext.ParentContentTypeId = dynamicModuleType.ParentModuleTypeId;
        contentTypeContext.IsSelfReferencing = dynamicModuleType.IsSelfReferencing;
        contentTypeContext.CheckFieldPermissions = dynamicModuleType.CheckFieldPermissions;
        contentTypeContext.ContentTypePageId = dynamicModuleType.PageId;
        contentTypeContext.Origin = dynamicModuleType.Origin;
        if (loadFields)
          contentTypeContext.Fields = ContentTypeService.GetContentTypeFieldsContext(dynamicModuleType);
      }
      ServiceUtility.DisableCache();
      return contentTypeContext;
    }

    private static ContentTypeItemFieldContext[] GetContentTypeFieldsContext(
      DynamicModuleType dynamicModuleType)
    {
      List<ContentTypeItemFieldContext> itemFieldContextList = new List<ContentTypeItemFieldContext>();
      foreach (DynamicModuleField field in dynamicModuleType.Fields)
        itemFieldContextList.Add(ContentTypeService.GetContentTypeFieldContext(field));
      return itemFieldContextList.ToArray();
    }

    private static ContentTypeItemFieldContext GetContentTypeFieldContext(
      DynamicModuleField dynamicField)
    {
      return new ContentTypeItemFieldContext()
      {
        AllowImageLibrary = dynamicField.AllowImageLibrary,
        AllowMultipleFiles = dynamicField.AllowMultipleFiles,
        AllowMultipleImages = dynamicField.AllowMultipleImages,
        AllowMultipleVideos = dynamicField.AllowMultipleVideos,
        AllowNulls = dynamicField.AllowNulls,
        CanCreateItemsWhileSelecting = dynamicField.CanCreateItemsWhileSelecting,
        CanSelectMultipleItems = dynamicField.CanSelectMultipleItems,
        CheckedByDefault = dynamicField.CheckedByDefault,
        ChoiceRenderType = dynamicField.ChoiceRenderType,
        Choices = dynamicField.Choices,
        ClassificationId = dynamicField.ClassificationId,
        ColumnName = dynamicField.ColumnName,
        DBLength = dynamicField.DBLength,
        DBType = dynamicField.DBType,
        DecimalPlacesCount = dynamicField.DecimalPlacesCount,
        DefaultValue = dynamicField.DefaultValue,
        DropDownListDefaulValueIndex = dynamicField.DdlChoiceDefaultValueIndex,
        FileExtensions = dynamicField.FileExtensions,
        FileMaxSize = dynamicField.FileMaxSize,
        Id = dynamicField.Id,
        ImageExtensions = dynamicField.ImageExtensions,
        ImageMaxSize = dynamicField.ImageMaxSize,
        IncludeInIndexes = dynamicField.IncludeInIndexes,
        InstructionalChoice = dynamicField.InstructionalChoice,
        InstructionalText = dynamicField.InstructionalText,
        IsHiddenField = dynamicField.IsHiddenField,
        IsRequired = dynamicField.IsRequired,
        IsRequiredToSelectCheckbox = dynamicField.IsRequiredToSelectCheckbox,
        IsRequiredToSelectDdlValue = dynamicField.IsRequiredToSelectDdlValue,
        IsTransient = dynamicField.IsTransient,
        LengthValidationMessage = dynamicField.LengthValidationMessage,
        MaxLength = new int?(dynamicField.MaxLength),
        MaxNumberRange = dynamicField.MaxNumberRange,
        MediaType = dynamicField.MediaType,
        MinLength = new int?(dynamicField.MinLength),
        MinNumberRange = dynamicField.MinNumberRange,
        Name = dynamicField.Name,
        NumberUnit = dynamicField.NumberUnit,
        Ordinal = dynamicField.Ordinal,
        RegularExpression = dynamicField.RegularExpression,
        Title = dynamicField.Title,
        TypeName = dynamicField.FieldType.ToString(),
        TypeUIName = dynamicField.TypeUIName,
        DisableLinkParser = dynamicField.DisableLinkParser,
        VideoExtensions = dynamicField.VideoExtensions,
        VideoMaxSize = dynamicField.VideoMaxSize,
        WidgetTypeName = dynamicField.WidgetTypeName,
        IsLocalizable = dynamicField.IsLocalizable,
        AddressFieldMode = Enum.GetName(typeof (AddressFieldMode), (object) dynamicField.AddressFieldMode),
        ParentSectionId = dynamicField.ParentSectionId,
        GridColumnOrdinal = dynamicField.GridColumnOrdinal,
        ShowInGrid = dynamicField.ShowInGrid,
        FrontendWidgetTypeName = dynamicField.FrontendWidgetTypeName,
        FrontendWidgetLabel = dynamicField.FrontendWidgetLabel,
        RelatedDataProvider = dynamicField.RelatedDataProvider,
        RelatedDataType = dynamicField.RelatedDataType,
        Origin = dynamicField.Origin,
        RecommendedCharactersCount = dynamicField.RecommendedCharactersCount
      };
    }

    private ContentTypeTreeItemContext GetContentTypeTreeItem(
      DynamicModuleType dynamicModuleType)
    {
      return new ContentTypeTreeItemContext()
      {
        ContentTypeId = dynamicModuleType.Id,
        Text = dynamicModuleType.DisplayName,
        ParentContentTypeId = dynamicModuleType.ParentModuleTypeId
      };
    }

    private IEnumerable<ContentTypeTreeItemContext> BuildContentTypeTreeHierarchy(
      IEnumerable<ContentTypeTreeItemContext> contentTypeTreeItems)
    {
      foreach (ContentTypeTreeItemContext contentTypeTreeItem1 in contentTypeTreeItems)
      {
        ContentTypeTreeItemContext contentTypeTreeItem = contentTypeTreeItem1;
        contentTypeTreeItem.Items = contentTypeTreeItems.Where<ContentTypeTreeItemContext>((Func<ContentTypeTreeItemContext, bool>) (ct => ct.ParentContentTypeId == contentTypeTreeItem.ContentTypeId)).ToArray<ContentTypeTreeItemContext>();
      }
      return contentTypeTreeItems.Where<ContentTypeTreeItemContext>((Func<ContentTypeTreeItemContext, bool>) (ct =>
      {
        Guid parentContentTypeId = ct.ParentContentTypeId;
        return Guid.Empty.Equals(ct.ParentContentTypeId);
      }));
    }

    private CollectionContext<ContentTypeContext> GetContentTypeChildrenInternal(
      string moduleId,
      string contentTypeId,
      string provider)
    {
      if (!moduleId.IsGuid())
        throw new ArgumentException("ModuleId must be a valid GUID.");
      if (!contentTypeId.IsGuid())
        throw new ArgumentException("ContentTypeId must be a valid GUID.");
      Guid moduleIdGuid = new Guid(moduleId);
      Guid contentTypeIdGuid = new Guid(contentTypeId);
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(provider);
      DynamicModule dynamicModule = manager.GetDynamicModule(moduleIdGuid);
      IQueryable<DynamicModuleType> source = manager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleIdGuid && t.ParentModuleTypeId == contentTypeIdGuid));
      int num = source.Count<DynamicModuleType>();
      List<ContentTypeContext> items = new List<ContentTypeContext>();
      foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) source)
      {
        ContentTypeContext contentTypeContext = ContentTypeService.GetContentTypeContext((IDynamicModule) dynamicModule, dynamicModuleType);
        items.Add(contentTypeContext);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<ContentTypeContext>((IEnumerable<ContentTypeContext>) items)
      {
        TotalCount = num
      };
    }

    private ContentTypeContext SaveContentTypeInternal(
      string moduleId,
      ContentTypeContext context,
      string provider,
      bool updateWidgetTemplates)
    {
      if (!moduleId.IsGuid())
        throw new ArgumentException("ModuleId must be a valid GUID.");
      if (context == null)
        throw new ArgumentNullException("contentType");
      this.ValidateDatabasePermissions(moduleId, context.ContentTypeId);
      this.SanitizeContext(context);
      context = ModuleBuilderHelper.SaveContentType(new Guid(moduleId), context, provider, updateWidgetTemplates);
      ServiceUtility.DisableCache();
      return context;
    }

    private ContentTypeSimpleContext UpdateModuleNameAndDescriptionInternal(
      string moduleId,
      ContentTypeSimpleContext contentTypeContext)
    {
      if (contentTypeContext == null)
        throw new ArgumentNullException("Content type context cannot be null.");
      if (!moduleId.IsGuid())
        throw new ArgumentException("ModuleId must be a valid GUID.");
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      Guid moduleId1 = new Guid(moduleId);
      if (!manager.ModuleExists(moduleId1))
        throw new ArgumentException("Invalid content type id.");
      contentTypeContext = manager.UpdateDynamicModuleNameAndDescription(moduleId1, contentTypeContext);
      manager.SaveChanges();
      return contentTypeContext;
    }

    private ContentTypeContext ActivateContentTypeInternal(
      string moduleId,
      string providerName)
    {
      Guid guid = moduleId.IsGuid() ? new Guid(moduleId) : throw new ArgumentException("ModuleId must be a valid GUID.");
      string transactionName = ModuleInstaller.GetTransactionName(guid);
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(providerName, transactionName);
      DynamicModule module = manager.GetDynamicModule(guid);
      ModuleInstaller moduleInstaller = new ModuleInstaller(providerName, transactionName);
      manager.LoadDynamicModuleGraph(module);
      DynamicModule module1 = module;
      moduleInstaller.InstallModule(module1);
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == module.Id)).FirstOrDefault<DynamicModuleType>();
      ServiceUtility.DisableCache();
      return ContentTypeService.GetContentTypeContext((IDynamicModule) module, dynamicModuleType);
    }

    private ContentTypeContext DeactivateContentTypeInternal(
      string moduleId,
      string providerName)
    {
      Guid guid = moduleId.IsGuid() ? new Guid(moduleId) : throw new ArgumentException("ModuleId must be a valid GUID.");
      string transactionName = ModuleInstaller.GetTransactionName(guid);
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(providerName, transactionName);
      DynamicModule module = manager.GetDynamicModule(guid);
      new ModuleInstaller(providerName, transactionName).UninstallModule(module);
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == module.Id)).FirstOrDefault<DynamicModuleType>();
      ServiceUtility.DisableCache();
      return ContentTypeService.GetContentTypeContext((IDynamicModule) module, dynamicModuleType);
    }

    private DeleteContentTypeContext DeleteContentTypeInternal(
      string moduleId,
      string providerName,
      bool deleteData)
    {
      if (!moduleId.IsGuid())
        throw new ArgumentException("ModuleId must be a valid GUID.");
      this.ValidateDatabasePermissions(moduleId);
      DeleteContentTypeContext contentTypeContext = new DeleteContentTypeContext();
      Guid guid = new Guid(moduleId);
      string transactionName = ModuleInstaller.GetTransactionName(guid);
      DynamicModule dynamicModule = ModuleBuilderManager.GetManager(providerName, transactionName).GetDynamicModule(guid);
      ModuleInstaller moduleInstaller = new ModuleInstaller(providerName, transactionName);
      DeleteModuleContext settings = new DeleteModuleContext()
      {
        DeleteModuleData = deleteData
      };
      try
      {
        if (deleteData)
          moduleInstaller.DeleteModuleData(dynamicModule, settings);
      }
      catch (Exception ex)
      {
        contentTypeContext.DeleteContentTypeStatus = DeleteStatus.DeleteDataFailed;
        contentTypeContext.ErrorMessage = ex.Message;
        return contentTypeContext;
      }
      try
      {
        moduleInstaller.DeleteModule(dynamicModule, settings);
        contentTypeContext.DeleteContentTypeStatus = DeleteStatus.Succeeded;
        ServiceUtility.DisableCache();
      }
      catch (Exception ex)
      {
        contentTypeContext.DeleteContentTypeStatus = DeleteStatus.Failed;
        contentTypeContext.ErrorMessage = ex.Message;
      }
      return contentTypeContext;
    }

    private DeleteContentTypeContext DeleteDynamicContentTypeInternal(
      string contentTypeId,
      string moduleId,
      string provider)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ContentTypeService.\u003C\u003Ec__DisplayClass44_0 cDisplayClass440 = new ContentTypeService.\u003C\u003Ec__DisplayClass44_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass440.contentTypeId = contentTypeId;
      // ISSUE: reference to a compiler-generated field
      if (!cDisplayClass440.contentTypeId.IsGuid())
        throw new ArgumentException("ContentTypeId must be a valid GUID.");
      if (!moduleId.IsGuid())
        throw new ArgumentException("ModuleId must be a valid GUID.");
      // ISSUE: reference to a compiler-generated field
      this.ValidateDatabasePermissions(moduleId, Guid.Parse(cDisplayClass440.contentTypeId));
      string transactionName = ModuleInstaller.GetTransactionName(Guid.Parse(moduleId));
      DeleteContentTypeContext contentTypeContext = new DeleteContentTypeContext();
      PageManager.GetManager(string.Empty, transactionName).GetPageNodes();
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager(provider, transactionName);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleTypes().Where<DynamicModuleType>(Expression.Lambda<Func<DynamicModuleType, bool>>((Expression) Expression.Call((Expression) Expression.Call(t.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.Equals)), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass440, typeof (ContentTypeService.\u003C\u003Ec__DisplayClass44_0)), FieldInfo.GetFieldFromHandle(__fieldref (ContentTypeService.\u003C\u003Ec__DisplayClass44_0.contentTypeId)))), parameterExpression)).FirstOrDefault<DynamicModuleType>();
      DynamicModule dynamicModule = manager.GetDynamicModule(Guid.Parse(moduleId));
      ModuleInstaller moduleInstaller = new ModuleInstaller(provider, transactionName);
      if (dynamicModuleType != null)
      {
        manager.LoadDynamicModuleGraph(dynamicModule);
        if (!manager.CanDeleteDynamicModuleType(dynamicModule, dynamicModuleType))
        {
          contentTypeContext.DeleteContentTypeStatus = DeleteStatus.Failed;
          contentTypeContext.ErrorMessage = "Deleting content type is not allowed";
          return contentTypeContext;
        }
        try
        {
          moduleInstaller.DeleteModuleTypeData(dynamicModule, dynamicModuleType, false);
        }
        catch (Exception ex)
        {
          contentTypeContext.DeleteContentTypeStatus = DeleteStatus.DeleteDataFailed;
          contentTypeContext.ErrorMessage = ex.Message;
          return contentTypeContext;
        }
        try
        {
          string fullTypeName = dynamicModuleType.GetFullTypeName();
          moduleInstaller.DeleteModuleType(dynamicModule, dynamicModuleType, true, true, true);
          moduleInstaller.DeleteModuleTypeConfiguration(fullTypeName);
          Type type = TypeResolutionService.ResolveType(fullTypeName, false);
          if (type != (Type) null)
            TypeResolutionService.UnregisterType(type);
          contentTypeContext.DeleteContentTypeStatus = DeleteStatus.Succeeded;
        }
        catch (Exception ex)
        {
          contentTypeContext.DeleteContentTypeStatus = DeleteStatus.Failed;
          contentTypeContext.ErrorMessage = ex.Message;
        }
      }
      else
      {
        contentTypeContext.DeleteContentTypeStatus = DeleteStatus.Failed;
        contentTypeContext.ErrorMessage = "Type doesn't exist";
      }
      ServiceUtility.DisableCache();
      return contentTypeContext;
    }

    private void ValidateDatabasePermissions(string moduleId) => this.ValidateDatabasePermissions(moduleId, Guid.Empty);

    private void ValidateDatabasePermissions(string moduleId, Guid contentTypeId)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      if (!(Guid.Parse(moduleId) != Guid.Empty))
        return;
      DynamicModule module = manager.GetDynamicModule(Guid.Parse(moduleId));
      DynamicModuleType dynamicModuleType1;
      if (!(contentTypeId == Guid.Empty))
        dynamicModuleType1 = manager.GetDynamicModuleType(contentTypeId);
      else
        dynamicModuleType1 = manager.GetDynamicModuleTypes().FirstOrDefault<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (dmt => dmt.ParentModuleId == module.Id));
      DynamicModuleType dynamicModuleType2 = dynamicModuleType1;
      if (!(module.Provider is ModuleBuilderDataProvider provider) || dynamicModuleType2 == null)
        return;
      if (!provider.HasAlterTablePermissions(dynamicModuleType2))
        throw new Exception(Res.Get<ModuleBuilderResources>().NoDatabasePermissions);
      if (!provider.HasDropTablePermissions(dynamicModuleType2))
        throw new Exception(Res.Get<ModuleBuilderResources>().NoDatabasePermissions);
    }

    /// <summary>
    /// Sanitizes the given HTML string so that it is safe to display as unencoded HTML.
    /// </summary>
    /// <param name="html">HTML object to be sanitized.</param>
    /// <returns>The sanitized HTML string.</returns>
    private string Sanitize(string input) => !this.disableHtmlSanitization ? this.Sanitizer.Sanitize(input) : input;

    private void SanitizeContext(ContentTypeContext context)
    {
      foreach (ContentTypeItemFieldContext field in context.Fields)
      {
        field.Title = this.Sanitize(field.Title);
        field.InstructionalText = this.Sanitize(field.InstructionalText);
        field.InstructionalChoice = this.Sanitize(field.InstructionalChoice);
        field.NumberUnit = this.Sanitize(field.NumberUnit);
      }
    }

    protected IHtmlSanitizer Sanitizer
    {
      get
      {
        if (this.sanitizer == null)
          this.sanitizer = ObjectFactory.Resolve<IHtmlSanitizer>();
        return this.sanitizer;
      }
    }
  }
}
