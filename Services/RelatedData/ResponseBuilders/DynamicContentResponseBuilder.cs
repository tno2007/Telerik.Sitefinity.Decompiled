// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.ResponseBuilders.DynamicContentResponseBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.RelatedData.Responses;

namespace Telerik.Sitefinity.Services.RelatedData.ResponseBuilders
{
  /// <summary>
  /// Represents a class containing methods for building responses for Dynamic content items.
  /// </summary>
  internal class DynamicContentResponseBuilder : IResponseBuilder
  {
    /// <inheritdoc />
    public Type GetItemType() => typeof (DynamicContent);

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
      List<DynamicContent> list = items.Cast<DynamicContent>().ToList<DynamicContent>();
      if (list.Count < 1)
        return (IEnumerable<RelatedItemResponse>) response;
      string name1 = SystemManager.CurrentContext.Culture.Name;
      RelatedDataHelper.PopulateLifecycleInformation(list, name1);
      bool toPlural = list.Count != 1;
      foreach (DynamicContent dynamicContent in list)
      {
        DynamicContent dynamicItem = dynamicContent;
        string name2 = ((IDataProviderBase) dynamicItem.Provider).Name;
        Type type = dynamicItem.GetType();
        ContentLink contentLink = contentLinks.FirstOrDefault<ContentLink>((Func<ContentLink, bool>) (cl => cl.ChildItemId == dynamicItem.Id));
        List<RelatedDataItemResponse> dataItemResponseList = response;
        RelatedDataItemResponse dataItemResponse1 = new RelatedDataItemResponse();
        dataItemResponse1.Id = dynamicItem.OriginalContentId == Guid.Empty ? dynamicItem.Id : dynamicItem.OriginalContentId;
        dataItemResponse1.Title = DynamicContentExtensions.GetTitle(dynamicItem);
        dataItemResponse1.SubTitle = string.Empty;
        dataItemResponse1.ProviderName = name2;
        dataItemResponse1.ContentTypeName = RelatedDataHelper.GetItemTypeTitle(type.Name, type.FullName, toPlural);
        dataItemResponse1.LastModified = dynamicItem.LastModified.ToSitefinityUITime();
        dataItemResponse1.Owner = dynamicItem.GetUserDisplayName();
        dataItemResponse1.Status = dynamicItem.Status;
        dataItemResponse1.PreviewUrl = RelatedDataResponseHelper.GetItemViewUrl(dynamicItem.Id.ToString(), type.FullName, name2, name1);
        dataItemResponse1.DetailsViewUrl = RelatedDataResponseHelper.GetDetailsViewUrl(type);
        dataItemResponse1.LifecycleStatus = new LifecycleResponse()
        {
          WorkflowStatus = dynamicItem.Lifecycle.WorkflowStatus,
          DisplayStatus = dynamicItem.Lifecycle.Message
        };
        dataItemResponse1.AvailableLanguages = dynamicItem != null ? dynamicItem.AvailableLanguages : (string[]) null;
        dataItemResponse1.Ordinal = contentLink != null ? contentLink.Ordinal : 0.0f;
        dataItemResponse1.IsRelated = true;
        dataItemResponse1.IsEditable = (!dynamicItem.IsGranted("General", "Modify") ? 0 : (!dynamicItem.Lifecycle.IsLocked ? 1 : (dynamicItem.Lifecycle.IsLockedByMe ? 1 : 0))) != 0;
        RelatedDataItemResponse dataItemResponse2 = dataItemResponse1;
        dataItemResponseList.Add(dataItemResponse2);
      }
      return (IEnumerable<RelatedItemResponse>) response;
    }
  }
}
