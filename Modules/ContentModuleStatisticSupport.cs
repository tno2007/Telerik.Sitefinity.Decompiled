// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ContentModuleStatisticSupport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Modules
{
  internal class ContentModuleStatisticSupport : 
    ContentStatisticSupportBase<IContentManager, Content>
  {
    public ContentModuleStatisticSupport(Type managerType)
      : base(managerType)
    {
    }

    public override StatisticResult GetStatistic(
      Type type,
      string statisticKind,
      string provider,
      string filter = null)
    {
      if (statisticKind.Equals("Count"))
      {
        IContentManager manager = this.GetManager(provider);
        IQueryable<Content> source = this.GetItemsQuery(manager, type);
        if (source != null)
        {
          if (!typeof (ILifecycleDataItemLive).IsAssignableFrom(type))
            source = source.Where<Content>((Expression<Func<Content, bool>>) (c => (int) c.Status == 0));
          return new StatisticResult()
          {
            Kind = "Count",
            Value = (object) source.Count<Content>(),
            CacheDependency = (ICacheItemExpiration) new DataItemCacheDependency(type, manager.Provider.ApplicationName)
          };
        }
      }
      return (StatisticResult) null;
    }

    private IQueryable<Content> GetItemsQuery(
      IContentManager manager,
      Type itemType)
    {
      MethodInfo methodInfo = ((IEnumerable<MethodInfo>) manager.GetType().GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "GetItems")).Select(m => new
      {
        Method = m,
        Params = m.GetParameters(),
        Args = m.GetGenericArguments()
      }).Where(x => x.Params.Length == 0 && x.Args.Length == 1 && typeof (IContent).IsAssignableFrom(x.Args[0])).Select(x => x.Method).FirstOrDefault<MethodInfo>();
      if (!(methodInfo != (MethodInfo) null))
        return (IQueryable<Content>) null;
      return methodInfo.MakeGenericMethod(itemType).Invoke((object) manager, new object[0]) as IQueryable<Content>;
    }
  }
}
