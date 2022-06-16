// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Analytics.Server.Infrastructure.Web.Services.AnalyticsOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.Services.Contracts;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.Analytics.Server.Infrastructure.Web.Services
{
  internal class AnalyticsOperationProvider : IOperationProvider
  {
    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      List<OperationData> operations = new List<OperationData>();
      OperationData operationData1 = OperationData.Create<AnalyticsDto>(new Func<OperationContext, AnalyticsDto>(this.Analytics));
      operationData1.OperationType = OperationType.PerItem;
      operations.Add(operationData1);
      OperationData operationData2 = OperationData.Create<Guid[], IEnumerable<AnalyticsDto>>(new Func<OperationContext, Guid[], IEnumerable<AnalyticsDto>>(this.Analytics));
      operationData2.OperationType = OperationType.Collection;
      operations.Add(operationData2);
      OperationData operationData3 = OperationData.Create<AnalyticsSettingsDto>(new Func<OperationContext, AnalyticsSettingsDto>(this.AnalyticsSettings));
      operationData3.OperationType = OperationType.Collection;
      operations.Add(operationData3);
      return (IEnumerable<OperationData>) operations;
    }

    private IEnumerable<AnalyticsDto> Analytics(
      OperationContext context,
      Guid[] keys)
    {
      IAnalyticsApiAccessManager apiAccessManager = this.GetAnalyticsApiAccessManager();
      IItemStrategy strategy = context.GetStrategy();
      IManagerStrategy managerStrategy = strategy as IManagerStrategy;
      string name = managerStrategy.Manager.Provider.Name;
      Type clrType = context.GetClrType();
      List<object> objectList = !typeof (IGetItemsByLifecycleStatusItemStrategy).IsAssignableFrom(strategy.GetType()) ? strategy.GetItems((ICollection<Guid>) keys).ToList<object>() : ((IGetItemsByLifecycleStatusItemStrategy) strategy).GetItems((ICollection<Guid>) keys, LifecycleStatus.Live).ToList<object>();
      CultureInfo culture = context.GetCulture();
      List<AnalyticsDto> analyticsDtoList = new List<AnalyticsDto>();
      foreach (IDataItem dataItem in objectList)
      {
        string title = (dataItem as IHasTitle).GetTitle();
        AnalyticsDto analyticsDto = (AnalyticsDto) null;
        if (dataItem is ILifecycleDataItemGeneric lifecycleDataItemGeneric)
        {
          if (LifecycleExtensions.WasItemPublished<ILifecycleDataItemGeneric>(lifecycleDataItemGeneric, managerStrategy.Manager, culture))
            analyticsDto = new AnalyticsDto()
            {
              AnalyticsUrl = apiAccessManager.GetLinkForItem(title, lifecycleDataItemGeneric.OriginalContentId.ToString(), clrType, name),
              Id = lifecycleDataItemGeneric.OriginalContentId
            };
          else
            continue;
        }
        if (dataItem is PageNode page)
        {
          if ((page.NodeType != NodeType.Standard ? 0 : (page.GetSiteMapNode().CurrentPageDataItem.IsPublished(SystemManager.CurrentContext.Culture) ? 1 : 0)) != 0)
            analyticsDto = new AnalyticsDto()
            {
              AnalyticsUrl = apiAccessManager.GetLinkForItem(title, page.Id.ToString(), clrType, name),
              Id = page.Id
            };
          else
            continue;
        }
        analyticsDtoList.Add(analyticsDto);
      }
      return (IEnumerable<AnalyticsDto>) analyticsDtoList;
    }

    private AnalyticsDto Analytics(OperationContext context)
    {
      string key = context.GetKey();
      IAnalyticsApiAccessManager apiAccessManager = this.GetAnalyticsApiAccessManager();
      IItemStrategy strategy = context.GetStrategy();
      string name = (strategy as IManagerStrategy).Manager.Provider.Name;
      Type clrType = context.GetClrType();
      IDataItem dataItem = strategy.GetItem(Guid.Parse(key)) as IDataItem;
      CultureInfo culture = context.GetCulture();
      IManagerStrategy managerStrategy;
      switch (dataItem)
      {
        case ILifecycleDataItem lifecycleDataItem when !LifecycleExtensions.WasItemPublished<ILifecycleDataItem>(lifecycleDataItem, managerStrategy.Manager, culture):
          return (AnalyticsDto) null;
        case PageNode page:
          if ((page.NodeType != NodeType.Standard ? 0 : (page.GetSiteMapNode().CurrentPageDataItem.IsPublished(SystemManager.CurrentContext.Culture) ? 1 : 0)) == 0)
            return (AnalyticsDto) null;
          break;
      }
      string title = (dataItem as IHasTitle).GetTitle();
      string linkForItem = apiAccessManager.GetLinkForItem(title, key, clrType, name);
      return new AnalyticsDto()
      {
        AnalyticsUrl = linkForItem
      };
    }

    public AnalyticsSettingsDto AnalyticsSettings(OperationContext context)
    {
      bool flag1 = SystemManager.IsModuleEnabled("Analytics");
      bool flag2 = false;
      bool flag3 = false;
      if (flag1)
      {
        Type clrType = context.GetClrType();
        IAnalyticsApiAccessManager apiAccessManager = this.GetAnalyticsApiAccessManager();
        flag3 = apiAccessManager.DoesItemSupportAnalytics(clrType);
        flag2 = apiAccessManager.IsConfigured;
      }
      return new AnalyticsSettingsDto()
      {
        Installed = flag1,
        Configured = flag2,
        ItemSupport = flag3
      };
    }

    private IAnalyticsApiAccessManager GetAnalyticsApiAccessManager()
    {
      try
      {
        return ObjectFactory.Resolve<IAnalyticsApiAccessManager>();
      }
      catch (Exception ex)
      {
        throw new ItemNotFoundException("The analytics manager is not found.");
      }
    }
  }
}
