// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.ResponseBuilders.IResponseBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Services.RelatedData.Responses;

namespace Telerik.Sitefinity.Services.RelatedData.ResponseBuilders
{
  /// <summary>
  /// Interface providing methods used from response builders
  /// </summary>
  public interface IResponseBuilder
  {
    /// <summary>
    /// Returns the type of the item that the current builder supports
    /// </summary>
    Type GetItemType();

    /// <summary>Returns response for specific item</summary>
    /// <param name="item">the item for which we create the response</param>
    /// <param name="contentLink">the content link</param>
    /// <param name="manager">item`s manager</param>
    RelatedItemResponse GetResponse(
      IDataItem item,
      ContentLink contentLink,
      ILifecycleManager manager);

    /// <summary>Returns response for collection of items</summary>
    /// <param name="items"></param>
    /// <param name="contentLink">items collection for which we create the response</param>
    /// <param name="contentLink">the content link</param>
    /// <param name="manager">item`s manager</param>
    IEnumerable<RelatedItemResponse> GetResponse(
      List<IDataItem> items,
      List<ContentLink> contentLinks,
      ILifecycleManager manager);
  }
}
