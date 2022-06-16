// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentUsages.Sources.ContentLinksUsageSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.ContentUsages.Filters;
using Telerik.Sitefinity.ContentUsages.Model;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.RelatedData;

namespace Telerik.Sitefinity.ContentUsages.Sources
{
  internal class ContentLinksUsageSource : IContentUsageSource
  {
    public IEnumerable<IContentItemUsage> GetContentUsages(
      ContentUsageFilter filter)
    {
      IEnumerable<IContentItemUsage> contentUsages = Enumerable.Empty<IContentItemUsage>();
      switch (filter.ItemTypeFilter)
      {
        case ItemTypeFilter.All:
        case ItemTypeFilter.Content:
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          contentUsages = (IEnumerable<IContentItemUsage>) RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemId == filter.ItemId && cl.ChildItemType == filter.ItemType && cl.ChildItemProviderName == filter.ItemProvider)), RelationDirection.Child), string.Empty, new ContentLifecycleStatus?(ContentLifecycleStatus.Master)).Select<ContentLink, ContentItemUsage>(Expression.Lambda<Func<ContentLink, ContentItemUsage>>((Expression) Expression.MemberInit(Expression.New(typeof (ContentItemUsage)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentItemUsage.set_Culture)), )))); //unable to render the statement
          break;
      }
      return contentUsages;
    }
  }
}
