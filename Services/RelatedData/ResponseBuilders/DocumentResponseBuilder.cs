// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.ResponseBuilders.DocumentResponseBuilder
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
  /// Represents a class containing methods for building responses for Document items.
  /// </summary>
  internal class DocumentResponseBuilder : IResponseBuilder
  {
    /// <inheritdoc />
    Type IResponseBuilder.GetItemType() => typeof (Document);

    /// <inheritdoc />
    RelatedItemResponse IResponseBuilder.GetResponse(
      IDataItem item,
      ContentLink contentLink,
      ILifecycleManager manager)
    {
      return (RelatedItemResponse) this.GetResponse(new List<IDataItem>()
      {
        item
      }, new List<ContentLink>() { contentLink }, manager).Cast<RelatedDocumentResponse>().FirstOrDefault<RelatedDocumentResponse>();
    }

    /// <inheritdoc />
    public IEnumerable<RelatedItemResponse> GetResponse(
      List<IDataItem> items,
      List<ContentLink> contentLinks,
      ILifecycleManager manager)
    {
      List<RelatedDocumentResponse> response = new List<RelatedDocumentResponse>();
      List<Document> list = items.Cast<Document>().ToList<Document>();
      Type itemType = typeof (Document);
      bool toPlural = list.Count != 1;
      foreach (Document document1 in list)
      {
        Document document = document1;
        ContentLink contentLink = contentLinks.FirstOrDefault<ContentLink>((Func<ContentLink, bool>) (cl => cl.ChildItemId == document.Id));
        LifecycleResponse itemLifecycleStatus = RelatedDataHelper.GetItemLifecycleStatus((ILifecycleDataItem) document, manager);
        List<RelatedDocumentResponse> documentResponseList = response;
        RelatedDocumentResponse documentResponse = new RelatedDocumentResponse();
        documentResponse.Id = document.OriginalContentId == Guid.Empty ? document.Id : document.OriginalContentId;
        documentResponse.ProviderName = ((IDataProviderBase) document.Provider).Name;
        documentResponse.Status = document.Status;
        documentResponse.ContentTypeName = RelatedDataHelper.GetItemTypeTitle(itemType.Name, itemType.FullName, toPlural);
        documentResponse.Title = ((IHasTitle) document).GetTitle();
        documentResponse.DetailsViewUrl = RelatedDataResponseHelper.GetDetailsViewUrl(itemType);
        documentResponse.Extension = document.Extension;
        documentResponse.TotalSize = document.TotalSize;
        documentResponse.Ordinal = contentLink != null ? contentLink.Ordinal : 0.0f;
        documentResponse.LifecycleStatus = itemLifecycleStatus;
        documentResponse.IsEditable = RelatedDataHelper.IsContentItemEditable((ISecuredObject) document) && (!itemLifecycleStatus.IsLocked || itemLifecycleStatus.IsLockedByMe);
        documentResponse.AvailableLanguages = document.AvailableLanguages;
        documentResponseList.Add(documentResponse);
      }
      return (IEnumerable<RelatedItemResponse>) response;
    }
  }
}
