// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.IHasPublicAndTempFacade`4
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Inteface that prototypes some of a draft facade's members
  /// </summary>
  /// <typeparam name="TPublicFacade">Type of the public facade</typeparam>
  /// <typeparam name="TTempFacade">Type of the temp facade</typeparam>
  /// <typeparam name="TCurrentFacade">Type of the implementation (draft) facade</typeparam>
  /// <typeparam name="TContentItem">Type of the content item</typeparam>
  public interface IHasPublicAndTempFacade<TPublicFacade, TTempFacade, TCurrentFacade, TContentItem>
    where TPublicFacade : BaseFacade
    where TTempFacade : BaseFacade
    where TCurrentFacade : BaseFacade
    where TContentItem : Content
  {
    /// <summary>
    /// Publish the content item by making the item visible on the frontend an identical copy of this draft
    /// </summary>
    /// <returns>Public facade</returns>
    TPublicFacade Publish();

    /// <summary>
    /// Publish the content item by making the item visible on the frontend an identical copy of this draft
    /// </summary>
    /// <param name="excludePipeInvocation">if set to <c>true</c> [exclude pipe invocation].</param>
    /// <param name="excludeVersioning">if set to <c>true</c> [exclude versioning].</param>
    /// <returns>Public facade</returns>
    TPublicFacade Publish(bool excludeVersioning);

    /// <summary>Gets this instance.</summary>
    /// <returns></returns>
    TContentItem Get();

    /// <summary>
    /// Sets an instance of the content type to currently loaded fluent API.
    /// </summary>
    /// <returns>An instance of the current facade type.</returns>
    TCurrentFacade Set(TContentItem item);

    /// <summary>
    /// Checks out the content item and return a temp item that is an identical copy if this draft. The item becomes locked.
    /// </summary>
    /// <returns>Temp facade</returns>
    TTempFacade CheckOut();

    /// <summary>
    /// Create a public version for an item that will be visible for a period of time
    /// </summary>
    /// <param name="pubDate">Date from which the item will be visible on the public side</param>
    /// <param name="expDate">Date untill which the item will be visible on the public side. Use null if the item should not expire</param>
    /// <returns>Public facade</returns>
    /// <exception cref="T:System.ArgumentException">When <paramref name="pubDate" /> is either DateTime.Min or DateTime.Max</exception>
    TPublicFacade Schedule(DateTime pubDate, DateTime? expDate);
  }
}
