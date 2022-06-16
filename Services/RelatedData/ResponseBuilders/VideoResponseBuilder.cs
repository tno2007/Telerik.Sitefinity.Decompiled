// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.ResponseBuilders.VideoResponseBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services.RelatedData.Responses;

namespace Telerik.Sitefinity.Services.RelatedData.ResponseBuilders
{
  /// <summary>
  /// Represents a class containing methods for building responses for Video items.
  /// </summary>
  internal class VideoResponseBuilder : IResponseBuilder
  {
    /// <inheritdoc />
    public Type GetItemType() => typeof (Video);

    /// <inheritdoc />
    public RelatedItemResponse GetResponse(
      IDataItem item,
      ContentLink contentLink,
      ILifecycleManager manager)
    {
      return (RelatedItemResponse) this.GetResponse(new List<IDataItem>()
      {
        item
      }, new List<ContentLink>() { contentLink }, manager).Cast<RelatedMediaResponse>().FirstOrDefault<RelatedMediaResponse>();
    }

    /// <inheritdoc />
    public IEnumerable<RelatedItemResponse> GetResponse(
      List<IDataItem> items,
      List<ContentLink> contentLinks,
      ILifecycleManager manager)
    {
      List<RelatedMediaResponse> response = new List<RelatedMediaResponse>();
      List<Video> list = items.Cast<Video>().ToList<Video>();
      Type itemType = typeof (Video);
      bool toPlural = list.Count != 1;
      foreach (Video video1 in list)
      {
        Video video = video1;
        ContentLink contentLink = contentLinks.FirstOrDefault<ContentLink>((Func<ContentLink, bool>) (cl => cl.ChildItemId == video.Id));
        LifecycleResponse itemLifecycleStatus = RelatedDataHelper.GetItemLifecycleStatus((ILifecycleDataItem) video, manager);
        List<RelatedMediaResponse> relatedMediaResponseList = response;
        RelatedMediaResponse relatedMediaResponse = new RelatedMediaResponse();
        relatedMediaResponse.Id = video.OriginalContentId == Guid.Empty ? video.Id : video.OriginalContentId;
        relatedMediaResponse.ProviderName = ((IDataProviderBase) video.Provider).Name;
        relatedMediaResponse.Status = video.Status;
        relatedMediaResponse.ContentTypeName = RelatedDataHelper.GetItemTypeTitle(itemType.Name, itemType.FullName, toPlural);
        relatedMediaResponse.Title = ((IHasTitle) video).GetTitle();
        relatedMediaResponse.ThumbnailUrl = video.ThumbnailUrl;
        relatedMediaResponse.DetailsViewUrl = RelatedDataResponseHelper.GetDetailsViewUrl(itemType);
        relatedMediaResponse.Ordinal = contentLink != null ? contentLink.Ordinal : 0.0f;
        relatedMediaResponse.LifecycleStatus = itemLifecycleStatus;
        relatedMediaResponse.IsEditable = RelatedDataHelper.IsContentItemEditable((ISecuredObject) video) && (!itemLifecycleStatus.IsLocked || itemLifecycleStatus.IsLockedByMe);
        relatedMediaResponse.AvailableLanguages = video.AvailableLanguages;
        relatedMediaResponseList.Add(relatedMediaResponse);
      }
      return (IEnumerable<RelatedItemResponse>) response;
    }
  }
}
