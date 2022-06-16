// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.ResponseBuilders.PagesResponseBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.RelatedData.Responses;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Services.RelatedData.ResponseBuilders
{
  /// <summary>
  /// Represents a class containing methods for building responses for pages.
  /// </summary>
  internal class PagesResponseBuilder : IResponseBuilder
  {
    /// <inheritdoc />
    Type IResponseBuilder.GetItemType() => typeof (PageNode);

    /// <inheritdoc />
    public RelatedItemResponse GetResponse(
      IDataItem item,
      ContentLink contentLink,
      ILifecycleManager manager)
    {
      return (RelatedItemResponse) this.GetResponse(new List<IDataItem>()
      {
        item
      }, new List<ContentLink>() { contentLink }, manager).Cast<RelatedDataItemResponse>().FirstOrDefault<RelatedDataItemResponse>();
    }

    /// <inheritdoc />
    public IEnumerable<RelatedItemResponse> GetResponse(
      List<IDataItem> items,
      List<ContentLink> contentLinks,
      ILifecycleManager manager)
    {
      List<RelatedDataItemResponse> response = new List<RelatedDataItemResponse>();
      List<PageNode> list = items.Cast<PageNode>().ToList<PageNode>();
      if (list.Count < 1)
        return (IEnumerable<RelatedItemResponse>) response;
      string name = ((DataProviderBase) ((IDataItem) list.First<PageNode>()).Provider).Name;
      Type itemType = typeof (PageNode);
      bool toPlural = list.Count != 1;
      foreach (PageNode pageNode1 in list)
      {
        PageNode pageNode = pageNode1;
        ContentLink contentLink = contentLinks.FirstOrDefault<ContentLink>((Func<ContentLink, bool>) (cl => cl.ChildItemId == pageNode.Id));
        LifecycleResponse pageLifecycleStatus = RelatedDataHelper.GetPageLifecycleStatus(pageNode);
        PageSiteNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(pageNode.Id.ToString()) as PageSiteNode;
        List<RelatedDataItemResponse> dataItemResponseList = response;
        RelatedDataItemResponse dataItemResponse1 = new RelatedDataItemResponse();
        dataItemResponse1.Id = pageNode.Id;
        dataItemResponse1.Title = (string) pageNode.Title;
        dataItemResponse1.SubTitle = string.Empty;
        dataItemResponse1.ProviderName = name;
        dataItemResponse1.LastModified = pageNode.LastModified.ToSitefinityUITime();
        dataItemResponse1.Owner = pageNode.GetUserDisplayName();
        dataItemResponse1.Status = pageNode.GetPageData() != null ? pageNode.GetPageData().Status : ContentLifecycleStatus.Live;
        dataItemResponse1.ContentTypeName = RelatedDataHelper.GetItemTypeTitle(itemType.Name, itemType.FullName, toPlural);
        dataItemResponse1.PreviewUrl = siteMapNodeFromKey != null ? RouteHelper.ResolveUrl(siteMapNodeFromKey.GetPageViewUrl(), UrlResolveOptions.Absolute) : string.Empty;
        dataItemResponse1.DetailsViewUrl = RelatedDataResponseHelper.GetDetailsViewUrl(itemType);
        dataItemResponse1.LifecycleStatus = pageLifecycleStatus;
        dataItemResponse1.AvailableLanguages = pageNode != null ? pageNode.AvailableLanguages : (string[]) null;
        dataItemResponse1.Ordinal = contentLink != null ? contentLink.Ordinal : 0.0f;
        dataItemResponse1.IsRelated = true;
        dataItemResponse1.IsEditable = (!pageNode.IsGranted("Pages", "Modify") ? 0 : (!pageLifecycleStatus.IsLocked ? 1 : (pageLifecycleStatus.IsLockedByMe ? 1 : 0))) != 0;
        RelatedDataItemResponse dataItemResponse2 = dataItemResponse1;
        dataItemResponseList.Add(dataItemResponse2);
      }
      return (IEnumerable<RelatedItemResponse>) response;
    }
  }
}
