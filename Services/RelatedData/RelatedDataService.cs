// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.RelatedDataService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using ServiceStack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services.RelatedData.Messages;
using Telerik.Sitefinity.Services.RelatedData.ResponseBuilders;
using Telerik.Sitefinity.Services.RelatedData.Responses;
using Telerik.Sitefinity.Services.ServiceStack.Filters;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.RelatedData
{
  /// <summary>
  /// This class represents Service used from the Related Data feature in Sitefinity
  /// </summary>
  [RequestBackendAuthenticationFilter(false)]
  public class RelatedDataService : Service
  {
    internal const string WebServiceUrl = "restapi/sitefinity/related-data";

    /// <summary>Applies temp relation changes to specific item</summary>
    /// <param name="message">The message.</param>
    /// <returns>Returns null.</returns>
    /// 
    ///             PUT: "/sitefinity/related-data/relations"
    public object Put(RelationChangeMessage message)
    {
      Type itemType = TypeResolutionService.ResolveType(message.ItemType);
      if (itemType == typeof (PageNode))
      {
        PageManager manager = PageManager.GetManager();
        Guid parentItemId = new Guid(message.ItemId);
        PageNode pageNode = manager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == parentItemId));
        if (pageNode != null)
        {
          RelatedDataHelper.SaveRelatedDataChanges((IManager) manager, (IDataItem) pageNode, message.RelationChanges);
          manager.SaveChanges();
        }
      }
      else if (ManagerBase.GetMappedManager(itemType, message.ItemProvider) is ILifecycleManager mappedManager)
      {
        Guid id = new Guid(message.ItemId);
        switch (mappedManager.GetItemOrDefault(itemType, id))
        {
          case ILifecycleDataItem cnt:
            ILifecycleDataItem temp = mappedManager.Lifecycle.GetTemp(cnt);
            if (temp != null)
            {
              RelatedDataHelper.SaveRelatedDataChanges((IManager) mappedManager, (IDataItem) temp, message.RelationChanges);
              mappedManager.SaveChanges();
              break;
            }
            break;
          case IDataItem dataItem:
            RelatedDataHelper.SaveRelatedDataChanges((IManager) mappedManager, dataItem, message.RelationChanges);
            mappedManager.SaveChanges();
            break;
        }
      }
      return (object) null;
    }

    /// <summary>Gets related field values for specific item</summary>
    /// <param name="message">The message.</param>
    /// <returns>Returns a <see cref="T:Telerik.Sitefinity.Services.RelatedData.Responses.RelatedItemsResponse" /> object.</returns>
    /// 
    ///             GET: "/sitefinity/related-data/child-items"
    public object Get(ParentItemMessage message)
    {
      Guid parentItemId = new Guid(message.ParentItemId);
      int? totalCount = new int?(0);
      List<IDataItem> list = Queryable.OfType<IDataItem>(RelatedDataHelper.GetRelatedItems(message.ParentItemType, message.ParentProviderName, parentItemId, message.FieldName, message.Status, message.Filter, message.Order, new int?(message.Skip), new int?(message.Take), ref totalCount, message.ChildItemType, message.ChildProviderName)).ToList<IDataItem>();
      IEnumerable<Guid> relatedItemsIds = list.Select<IDataItem, Guid>((Func<IDataItem, Guid>) (i => i.Id));
      IQueryable<ContentLink> source = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == message.ParentItemType && cl.ParentItemId == parentItemId && cl.ParentItemProviderName == message.ParentProviderName && cl.ComponentPropertyName == message.FieldName && relatedItemsIds.Contains<Guid>(cl.ChildItemId)));
      IEnumerable<RelatedItemResponse> relatedItemsResponse = this.GetRelatedItemsResponse(list, source.ToList<ContentLink>());
      return (object) new RelatedItemsResponse()
      {
        Items = (IEnumerable) relatedItemsResponse,
        TotalCount = totalCount.Value
      };
    }

    /// <summary>Gets related field values for specific item</summary>
    /// <param name="message">The message.</param>
    /// <returns>Returns a list of <see cref="T:Telerik.Sitefinity.Services.RelatedData.Responses.DataTypeResponse" /> objects.</returns>
    /// 
    ///             GET: "/sitefinity/related-data/data-types"
    public List<DataTypeResponse> Get(DataTypeMessage message)
    {
      List<DataTypeResponse> response = new List<DataTypeResponse>();
      this.SetStaticModules(response);
      this.SetDynamicTypes(response);
      this.SetProductTypes(response);
      ServiceUtility.DisableCache();
      return response;
    }

    /// <summary>Gets relating items for specific item</summary>
    /// <param name="message">The message.</param>
    /// <returns>Returns a <see cref="T:Telerik.Sitefinity.Services.RelatedData.Responses.RelatingItemsResponse" /> object.</returns>
    /// 
    ///             GET: "/sitefinity/relateddata/parent-items"
    public object Get(ChildItemMessage message)
    {
      Guid childItemId = new Guid(message.ChildItemId);
      IEnumerable<IDataItem> source1 = RelatedDataHelper.GetRelatedParentItems(message.ChildItemType, message.ChildProviderName, childItemId, string.Empty, ContentLifecycleStatus.Master).Cast<IDataItem>().ToList<IDataItem>().Distinct<IDataItem>();
      List<RelatedItemResponse> relatedItemResponseList = new List<RelatedItemResponse>();
      foreach (IEnumerable<IDataItem> source2 in source1.GroupBy<IDataItem, Type>((Func<IDataItem, Type>) (i => i.GetType())))
      {
        IEnumerable<RelatedItemResponse> relatedItemsResponse = this.GetRelatedItemsResponse(source2.ToList<IDataItem>(), Enumerable.Empty<ContentLink>().ToList<ContentLink>());
        relatedItemResponseList.AddRange((IEnumerable<RelatedItemResponse>) relatedItemsResponse.ToList<RelatedItemResponse>());
      }
      return (object) new RelatingItemsResponse()
      {
        Items = (IEnumerable<RelatedItemResponse>) relatedItemResponseList,
        TotalCount = relatedItemResponseList.Count
      };
    }

    private IEnumerable<RelatedItemResponse> GetRelatedItemsResponse(
      List<IDataItem> relatedItems,
      List<ContentLink> contentLinks)
    {
      IEnumerable<RelatedItemResponse> relatedItemsResponse = Enumerable.Empty<RelatedItemResponse>();
      if (relatedItems != null)
      {
        IEnumerable<IResponseBuilder> source = ObjectFactory.Container.ResolveAll(typeof (IResponseBuilder)).Cast<IResponseBuilder>();
        IDataItem firstItem = relatedItems.FirstOrDefault<IDataItem>();
        if (firstItem != null)
        {
          ILifecycleManager mappedManager = ManagerBase.GetMappedManager(firstItem.GetType()) as ILifecycleManager;
          IResponseBuilder responseBuilder = source.FirstOrDefault<IResponseBuilder>((Func<IResponseBuilder, bool>) (rb => rb.GetItemType().IsAssignableFrom(firstItem.GetType())));
          if (responseBuilder != null)
            relatedItemsResponse = responseBuilder.GetResponse(relatedItems, contentLinks, mappedManager);
        }
      }
      return relatedItemsResponse;
    }

    private List<ProviderModel> SetProviders(string dataSourceName)
    {
      IEnumerable<DataProviderInfo> providerInfos = (IEnumerable<DataProviderInfo>) new List<DataProviderInfo>().AsQueryable<DataProviderInfo>();
      IEnumerable<ISiteDataSource> source = Enumerable.Empty<ISiteDataSource>();
      if (SystemManager.MultisiteContext is MultisiteContext multisiteContext)
        source = multisiteContext.GetDataSourcesByName(dataSourceName);
      IDataSource dataSource = SystemManager.DataSourceRegistry.GetDataSources().FirstOrDefault<IDataSource>((Func<IDataSource, bool>) (ds => ds.Name == dataSourceName));
      if (dataSource != null)
        providerInfos = dataSource.ProviderInfos;
      List<ProviderModel> list1 = source.Select<ISiteDataSource, ProviderModel>(new Func<ISiteDataSource, ProviderModel>(this.Map)).ToList<ProviderModel>();
      List<ProviderModel> list2 = providerInfos.Select<DataProviderInfo, ProviderModel>((Func<DataProviderInfo, ProviderModel>) (pi => new ProviderModel()
      {
        Name = pi.ProviderName,
        Title = pi.ProviderTitle
      })).Concat<ProviderModel>((IEnumerable<ProviderModel>) list1.Where<ProviderModel>((Func<ProviderModel, bool>) (ds => !providerInfos.Any<DataProviderInfo>((Func<DataProviderInfo, bool>) (pi => pi.ProviderName.Equals(ds.Name))))).AsQueryable<ProviderModel>()).ToList<ProviderModel>();
      this.AddDefaultProvider(list2);
      this.AddAnyProvider(list2);
      return list2;
    }

    private void AddDefaultProvider(List<ProviderModel> providers) => providers.Add(new ProviderModel()
    {
      Name = "sf-site-default-provider",
      Title = "Default source for the current site"
    });

    private void AddAnyProvider(List<ProviderModel> providers) => providers.Add(new ProviderModel()
    {
      Name = "sf-any-site-provider",
      Title = "All sources for the current site"
    });

    private List<ProviderModel> SetPageProviders() => PageManager.GetManager().ProviderInfos.Select<DataProviderInfo, ProviderModel>(new Func<DataProviderInfo, ProviderModel>(this.Map)).ToList<ProviderModel>();

    private ProviderModel Map(DataProviderInfo providerInfo) => new ProviderModel()
    {
      Name = providerInfo.ProviderName,
      Title = providerInfo.ProviderTitle
    };

    private ProviderModel Map(ISiteDataSource siteDataSource) => new ProviderModel()
    {
      Name = siteDataSource.Provider,
      Title = siteDataSource.Title
    };

    private DataTypeResponse Map(DynamicModuleType moduleType) => new DataTypeResponse()
    {
      Name = string.Format("{0} ({1})", (object) PluralsResolver.Instance.ToPlural(moduleType.DisplayName), (object) moduleType.ModuleName),
      ParentName = moduleType.ModuleName,
      ClrType = moduleType.GetFullTypeName(),
      Providers = this.SetProviders(moduleType.ModuleName)
    };

    private void SetStaticModules(List<DataTypeResponse> response)
    {
      response.Add(new DataTypeResponse()
      {
        Name = "News",
        ClrType = "Telerik.Sitefinity.News.Model.NewsItem",
        Providers = this.SetProviders("Telerik.Sitefinity.Modules.News.NewsManager")
      });
      response.Add(new DataTypeResponse()
      {
        Name = "Blog Posts",
        ClrType = "Telerik.Sitefinity.Blogs.Model.BlogPost",
        Providers = this.SetProviders("Telerik.Sitefinity.Modules.Blogs.BlogsManager")
      });
      response.Add(new DataTypeResponse()
      {
        Name = "Events",
        ClrType = "Telerik.Sitefinity.Events.Model.Event",
        Providers = this.SetProviders("Telerik.Sitefinity.Modules.Events.EventsManager")
      });
      response.Add(new DataTypeResponse()
      {
        Name = Res.Get<PageResources>().Pages,
        ParentName = Res.Get<PageResources>().Page,
        ClrType = typeof (PageNode).FullName,
        Providers = this.SetPageProviders()
      });
    }

    private void SetDynamicTypes(List<DataTypeResponse> response)
    {
      if (!SystemManager.IsModuleEnabled("ModuleBuilder"))
        return;
      IEnumerable<DataTypeResponse> collection = ModuleBuilderManager.GetManager().GetDynamicModuleTypes().Select<DynamicModuleType, DataTypeResponse>(new Func<DynamicModuleType, DataTypeResponse>(this.Map));
      response.AddRange(collection);
    }

    private void SetProductTypes(List<DataTypeResponse> response)
    {
      if (!SystemManager.IsModuleEnabled("Ecommerce"))
        return;
      Type itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.ProductType");
      foreach (object obj1 in (IEnumerable) ManagerBase.GetMappedManager(itemType).GetItems(itemType, string.Empty, string.Empty, 0, 0).OfType<object>().Where<object>((Func<object, bool>) (pt =>
      {
        // ISSUE: reference to a compiler-generated field
        if (RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (RelatedDataService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target = RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p1 = RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Visible", typeof (RelatedDataService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__0, pt);
        return target((CallSite) p1, obj);
      })).OrderBy<object, object>((Func<object, object>) (pt =>
      {
        // ISSUE: reference to a compiler-generated field
        if (RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Title", typeof (RelatedDataService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        return RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__2.Target((CallSite) RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__2, pt);
      })))
      {
        List<DataTypeResponse> dataTypeResponseList = response;
        DataTypeResponse dataTypeResponse1 = new DataTypeResponse();
        DataTypeResponse dataTypeResponse2 = dataTypeResponse1;
        // ISSUE: reference to a compiler-generated field
        if (RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (RelatedDataService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> target1 = RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> p4 = RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__4;
        // ISSUE: reference to a compiler-generated field
        if (RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Title", typeof (RelatedDataService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__3.Target((CallSite) RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__3, obj1);
        string str1 = target1((CallSite) p4, obj2);
        dataTypeResponse2.Name = str1;
        DataTypeResponse dataTypeResponse3 = dataTypeResponse1;
        // ISSUE: reference to a compiler-generated field
        if (RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (RelatedDataService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> target2 = RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__6.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> p6 = RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__6;
        // ISSUE: reference to a compiler-generated field
        if (RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ClrType", typeof (RelatedDataService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__5.Target((CallSite) RelatedDataService.\u003C\u003Eo__14.\u003C\u003Ep__5, obj1);
        string str2 = target2((CallSite) p6, obj3);
        dataTypeResponse3.ClrType = str2;
        dataTypeResponse1.Providers = this.SetProviders("Telerik.Sitefinity.Modules.Ecommerce.Catalog.CatalogManager");
        DataTypeResponse dataTypeResponse4 = dataTypeResponse1;
        dataTypeResponseList.Add(dataTypeResponse4);
      }
    }
  }
}
