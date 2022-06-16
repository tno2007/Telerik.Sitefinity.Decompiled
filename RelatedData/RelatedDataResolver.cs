// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.RelatedDataResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.RelatedData
{
  internal class RelatedDataResolver : IRelatedDataResolver
  {
    /// <inheritdoc />
    public object GetRelatedItem(IDataItem item, string fieldName, Type childItemType) => (object) RelatedDataResolver.GetRelatedItemsQuery(item, fieldName, childItemType).FirstOrDefault<IDataItem>();

    /// <inheritdoc />
    public IList<IDataItem> GetRelatedItems(
      IDataItem item,
      string fieldName,
      Type childItemType)
    {
      return (IList<IDataItem>) RelatedDataResolver.GetRelatedItemsQuery(item, fieldName, childItemType).ToList<IDataItem>();
    }

    private static IQueryable<IDataItem> GetRelatedItemsQuery(
      IDataItem item,
      string fieldName,
      Type childItemType)
    {
      if (childItemType != (Type) null && childItemType.ImplementsInterface(typeof (ILifecycleDataItemGeneric)))
      {
        ContentLifecycleStatus status = RelatedDataExtensions.GetStatus((object) item);
        IQueryable<ILifecycleDataItemGeneric> source = item.GetRelatedItems<ILifecycleDataItemGeneric>(fieldName).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (i => (int) i.Status == (int) status));
        CultureInfo culture = (CultureInfo) null;
        if (!SystemManager.IsBackendRequest(out culture))
        {
          source = source.Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (i => i.Visible == true));
          if (SystemManager.CurrentContext.AppSettings.Multilingual)
          {
            CultureInfo uiCulture = SystemManager.CurrentContext.Culture;
            CultureInfo defaultCulture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
            source = source.Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (i => !i.PublishedTranslations.Any<string>() && uiCulture.Equals(defaultCulture) || i.PublishedTranslations.Contains(uiCulture.Name)));
          }
        }
        return (IQueryable<IDataItem>) source;
      }
      if (!(childItemType != (Type) null) || !childItemType.IsAssignableFrom(typeof (PageNode)))
        return item.GetRelatedItems(fieldName);
      IQueryable<PageNode> source1 = item.GetRelatedItems<PageNode>(fieldName);
      if (!SystemManager.IsBackendRequest())
        source1 = source1.Where<PageNode>((Expression<Func<PageNode, bool>>) (x => (int) x.NodeType != 0 || x.PageDataList.Any<PageData>((Func<PageData, bool>) (d => d.Visible))));
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        CultureInfo uiCulture = SystemManager.CurrentContext.Culture;
        CultureInfo defaultCulture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
        source1 = source1.ToList<PageNode>().Where<PageNode>((Func<PageNode, bool>) (x => x.NodeType != NodeType.Standard && ((IEnumerable<CultureInfo>) x.AvailableCultures).Contains<CultureInfo>(uiCulture) || x.PageDataList.Any<PageData>((Func<PageData, bool>) (d => !d.PublishedTranslations.Any<string>() && uiCulture.Equals((object) defaultCulture) || d.PublishedTranslations.Contains(uiCulture.Name))))).AsQueryable<PageNode>();
      }
      return (IQueryable<IDataItem>) source1;
    }
  }
}
