// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.ResponseBuilders.StaticContentResponseBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services.RelatedData.Responses;

namespace Telerik.Sitefinity.Services.RelatedData.ResponseBuilders
{
  /// <summary>
  /// Represents a class containing methods for building responses for static content items, such as News and Events.
  /// </summary>
  internal class StaticContentResponseBuilder : IResponseBuilder
  {
    /// <inheritdoc />
    public Type GetItemType() => typeof (IContent);

    /// <inheritdoc />
    public RelatedItemResponse GetResponse(
      IDataItem item,
      ContentLink contentLink,
      ILifecycleManager manager)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IEnumerable<RelatedItemResponse> GetResponse(
      List<IDataItem> items,
      List<ContentLink> contentLinks,
      ILifecycleManager manager)
    {
      List<RelatedDataItemResponse> response = new List<RelatedDataItemResponse>();
      List<IContent> list = items.Cast<IContent>().ToList<IContent>();
      if (list.Count < 1)
        return (IEnumerable<RelatedItemResponse>) response;
      string name1 = SystemManager.CurrentContext.Culture.Name;
      bool toPlural = list.Count != 1;
      foreach (IContent content1 in list)
      {
        IContent content = content1;
        string name2 = ((IDataProviderBase) content.Provider).Name;
        ContentLink contentLink = contentLinks.FirstOrDefault<ContentLink>((Func<ContentLink, bool>) (cl => cl.ChildItemId == content.Id));
        Type type = content.GetType();
        ISecuredObject secObj = content as ISecuredObject;
        LifecycleResponse lifecycleResponse = StaticContentResponseBuilder.GetStaticItemLifecycleResponse(content);
        List<RelatedDataItemResponse> dataItemResponseList = response;
        RelatedDataItemResponse dataItemResponse = new RelatedDataItemResponse();
        dataItemResponse.Id = content.Id;
        dataItemResponse.Title = content is IHasTitle ? ((IHasTitle) content).GetTitle() : content.Title.ToString();
        dataItemResponse.SubTitle = string.Empty;
        dataItemResponse.ProviderName = name2;
        dataItemResponse.LastModified = content.LastModified.ToSitefinityUITime();
        dataItemResponse.Owner = content.GetUserDisplayName();
        dataItemResponse.Status = content is ILifecycleDataItem ? ((ILifecycleDataItem) content).Status : ContentLifecycleStatus.Live;
        dataItemResponse.ContentTypeName = RelatedDataHelper.GetItemTypeTitle(type.Name, type.FullName, toPlural);
        dataItemResponse.PreviewUrl = RelatedDataResponseHelper.GetItemViewUrl(content.Id.ToString(), content.GetType().FullName, name2, name1);
        dataItemResponse.DetailsViewUrl = RelatedDataResponseHelper.GetDetailsViewUrl(type);
        dataItemResponse.LifecycleStatus = lifecycleResponse;
        dataItemResponse.AvailableLanguages = content is ILocalizable ? ((ILocalizable) content).AvailableLanguages : (string[]) null;
        dataItemResponse.Ordinal = contentLink != null ? contentLink.Ordinal : 0.0f;
        dataItemResponse.IsRelated = true;
        dataItemResponse.IsEditable = RelatedDataHelper.IsContentItemEditable(secObj) && (!lifecycleResponse.IsLocked || lifecycleResponse.IsLockedByMe);
        dataItemResponseList.Add(dataItemResponse);
      }
      return (IEnumerable<RelatedItemResponse>) response;
    }

    private static LifecycleResponse GetStaticItemLifecycleResponse(
      IContent content)
    {
      if (content is ILifecycleDataItem lifecycleDataItem)
      {
        string name = ((IDataProviderBase) content.Provider).Name;
        ILifecycleManager mappedManager = ManagerBase.GetMappedManager(content.GetType(), name) as ILifecycleManager;
        return RelatedDataHelper.GetItemLifecycleStatus(lifecycleDataItem, mappedManager);
      }
      return new LifecycleResponse()
      {
        DisplayStatus = string.Empty,
        WorkflowStatus = string.Empty
      };
    }
  }
}
