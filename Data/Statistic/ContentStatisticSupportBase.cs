// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Statistic.ContentStatisticSupportBase`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.Statistic
{
  internal abstract class ContentStatisticSupportBase<TManagerBase, TItemBase> : 
    IContentStatisticSupport
    where TManagerBase : IManager
    where TItemBase : IDataItem
  {
    private Type managerType;

    public ContentStatisticSupportBase(Type managerType) => this.managerType = managerType;

    public virtual bool IsReusable => true;

    public virtual string GetDefaultProviderName(string moduleName = null) => this.GetManager().Provider.Name;

    public virtual IEnumerable<string> GetProviderNames(string moduleName = null) => this.GetManager().GetSiteProviders().Select<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Name));

    public abstract StatisticResult GetStatistic(
      Type type,
      string statisticKind,
      string provider,
      string filter = null);

    public virtual IEnumerable<IStatisticSupportTypeInfo> GetTypeInfos(
      string moduleName = null)
    {
      IModule module = (IModule) null;
      if (!string.IsNullOrEmpty(moduleName))
      {
        module = SystemManager.GetModule(moduleName);
        if (module is IStatisticSupportModule statisticSupportModule)
          return statisticSupportModule.GetTypeInfos();
      }
      StatisticSupportTypeInfo[] array = ((IEnumerable<Type>) this.GetManager().Provider.GetKnownTypes()).Where<Type>((Func<Type, bool>) (t => typeof (TItemBase).IsAssignableFrom(t))).Select<Type, StatisticSupportTypeInfo>((Func<Type, StatisticSupportTypeInfo>) (t => new StatisticSupportTypeInfo(t, new string[1]
      {
        "Count"
      }))).ToArray<StatisticSupportTypeInfo>();
      if (module != null && array.Length != 0)
      {
        bool flag = false;
        if (module is ContentModuleBase)
        {
          IDictionary<Type, Guid> landingPagesMapping = ((ContentModuleBase) module).GetTypeLandingPagesMapping();
          if (landingPagesMapping != null)
          {
            flag = true;
            foreach (KeyValuePair<Type, Guid> keyValuePair in (IEnumerable<KeyValuePair<Type, Guid>>) landingPagesMapping)
            {
              KeyValuePair<Type, Guid> pair = keyValuePair;
              StatisticSupportTypeInfo statisticSupportTypeInfo = ((IEnumerable<StatisticSupportTypeInfo>) array).FirstOrDefault<StatisticSupportTypeInfo>((Func<StatisticSupportTypeInfo, bool>) (t => t.Type.Equals(pair.Key)));
              if (statisticSupportTypeInfo != null)
                statisticSupportTypeInfo.LandingPages = (IEnumerable<StatisticLandingPageInfo>) new StatisticLandingPageInfo[1]
                {
                  new StatisticLandingPageInfo(pair.Value)
                };
            }
          }
        }
        if (!flag && module.LandingPageId != Guid.Empty)
          array[0].LandingPages = (IEnumerable<StatisticLandingPageInfo>) new StatisticLandingPageInfo[1]
          {
            new StatisticLandingPageInfo(module.LandingPageId)
          };
      }
      return (IEnumerable<IStatisticSupportTypeInfo>) array;
    }

    protected TManagerBase GetManager(string provider = null) => (TManagerBase) ManagerBase.GetManager(this.managerType, provider);
  }
}
