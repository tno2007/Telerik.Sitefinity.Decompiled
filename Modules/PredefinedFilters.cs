// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.PredefinedFilters
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq.Expressions;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Modules
{
  public class PredefinedFilters
  {
    /// <summary>Publisheded items regardless sheduled date filter.</summary>
    /// <returns>The filter</returns>
    public static string PublishedItemsRegardlessSheduledDateFilter() => "Visible = true && Status = Live";

    /// <summary>Published items</summary>
    /// <returns>The filter for published items</returns>
    public static string PublishedItems() => "Visible = true && Status = Live && PublicationDate <= DateTime.UtcNow";

    /// <summary>All items except temp</summary>
    /// <returns>The filter for all items except temp</returns>
    public static string AllItemsExceptTemp() => "(Status = Live && Visible = true) || Status = Master";

    /// <summary>Publisheds the items filter.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> PublishedItemsFilter<T>() where T : Content => (Expression<Func<T, bool>>) (item => item.Visible == true && (int) item.Status == 2 && item.PublicationDate <= DateTime.UtcNow && (item.ExpirationDate == new DateTime?() || item.ExpirationDate > (DateTime?) DateTime.UtcNow));

    public static Expression<Func<T, bool>> PublishedDrafts<T>() where T : Content => (Expression<Func<T, bool>>) (item => (int) item.Status == 0 && item.ContentState == "PUBLISHED");

    /// <summary>Nots the published drafts.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> NotPublishedDrafts<T>() where T : Content => (Expression<Func<T, bool>>) (item => !((int) item.Status == 0 && (item.ContentState == "" || item.ContentState == default (string))));

    /// <summary>Scheduleds the drafts.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> ScheduledDrafts<T>() where T : Content => (Expression<Func<T, bool>>) (item => item.Visible == true && (int) item.Status == 2 && item.ContentState == "SCHEDULED");
  }
}
