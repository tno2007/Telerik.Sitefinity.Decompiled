// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.Templates.UsedOnPagesProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Services.Extensibility.Templates.Models;

namespace Telerik.Sitefinity.Web.Services.Extensibility.Templates
{
  /// <summary>
  /// A property for retrieving page templates' UsedOnPages property.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class UsedOnPagesProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (UsedOnPagesModel);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null)
        return (IDictionary<object, object>) values;
      foreach (PageTemplate pageTemplate in items)
      {
        IQueryable<PageNode> queryable1 = pageTemplate.GetPageDataBasedOnTemplate().Select<PageData, PageNode>((Expression<Func<PageData, PageNode>>) (x => x.NavigationNode));
        IQueryable<PageNode> queryable2 = pageTemplate.GetDraftPagesBasedOnTemplate().Select<PageDraft, PageNode>((Expression<Func<PageDraft, PageNode>>) (d => d.ParentPage.NavigationNode));
        int pageCount = queryable1.Count<PageNode>() + queryable2.Count<PageNode>();
        IQueryable<PageNode> source = queryable1.Concat<PageNode>((IEnumerable<PageNode>) queryable2).GroupBy<PageNode, Guid>((Expression<Func<PageNode, Guid>>) (x => x.RootNodeId)).Select<IGrouping<Guid, PageNode>, PageNode>((Expression<Func<IGrouping<Guid, PageNode>, PageNode>>) (x => x.First<PageNode>()));
        IQueryable<Guid> rootNodeIds = source.Select<PageNode, Guid>((Expression<Func<PageNode, Guid>>) (x => x.Id));
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        IQueryable<UsedOnSiteModel> queryable3 = source.Select<PageNode, PageSiteNode>((Expression<Func<PageNode, PageSiteNode>>) (x => PropertyHelpers.GetSiteMapNode(x))).Select<PageSiteNode, UsedOnSiteModel>(Expression.Lambda<Func<PageSiteNode, UsedOnSiteModel>>((Expression) Expression.MemberInit(Expression.New(typeof (UsedOnSiteModel)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (UsedOnSiteModel.set_Id)), )))); //unable to render the statement
        string statisticsText = PageTemplateViewModel.GetStatisticsText(pageCount, (IEnumerable<Guid>) rootNodeIds);
        values.Add((object) pageTemplate, (object) new UsedOnPagesModel()
        {
          Count = pageCount,
          Tooltip = statisticsText,
          Sites = (IEnumerable<UsedOnSiteModel>) queryable3
        });
      }
      return (IDictionary<object, object>) values;
    }
  }
}
