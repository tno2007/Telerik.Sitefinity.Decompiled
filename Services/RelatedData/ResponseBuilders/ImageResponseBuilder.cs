// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.ResponseBuilders.ImageResponseBuilder
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
  /// Represents a class containing methods for building responses for Image items.
  /// </summary>
  internal class ImageResponseBuilder : IResponseBuilder
  {
    /// <inheritdoc />
    public Type GetItemType() => typeof (Image);

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
      List<Image> list = items.Cast<Image>().ToList<Image>();
      Type itemType = typeof (Image);
      bool toPlural = list.Count != 1;
      foreach (Image image1 in list)
      {
        Image image = image1;
        ContentLink contentLink = contentLinks.FirstOrDefault<ContentLink>((Func<ContentLink, bool>) (cl => cl.ChildItemId == image.Id));
        LifecycleResponse itemLifecycleStatus = RelatedDataHelper.GetItemLifecycleStatus((ILifecycleDataItem) image, manager);
        List<RelatedMediaResponse> relatedMediaResponseList = response;
        RelatedMediaResponse relatedMediaResponse = new RelatedMediaResponse();
        relatedMediaResponse.Id = image.OriginalContentId == Guid.Empty ? image.Id : image.OriginalContentId;
        relatedMediaResponse.ProviderName = ((IDataProviderBase) image.Provider).Name;
        relatedMediaResponse.Status = image.Status;
        relatedMediaResponse.ContentTypeName = RelatedDataHelper.GetItemTypeTitle(itemType.Name, itemType.FullName, toPlural);
        relatedMediaResponse.Title = ((IHasTitle) image).GetTitle();
        relatedMediaResponse.MediaUrl = image.MediaUrl;
        relatedMediaResponse.ThumbnailUrl = image.ThumbnailUrl;
        relatedMediaResponse.DetailsViewUrl = RelatedDataResponseHelper.GetDetailsViewUrl(itemType);
        relatedMediaResponse.Ordinal = contentLink != null ? contentLink.Ordinal : 0.0f;
        relatedMediaResponse.LifecycleStatus = itemLifecycleStatus;
        relatedMediaResponse.IsEditable = RelatedDataHelper.IsContentItemEditable((ISecuredObject) image) && (!itemLifecycleStatus.IsLocked || itemLifecycleStatus.IsLockedByMe);
        relatedMediaResponse.AvailableLanguages = image.AvailableLanguages;
        relatedMediaResponseList.Add(relatedMediaResponse);
      }
      return (IEnumerable<RelatedItemResponse>) response;
    }
  }
}
