// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider.MediaOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider
{
  internal class MediaOperationProvider : IOperationProvider
  {
    private const string GetForCurrentFileQueryParam = "getForCurrentFile";

    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if (!typeof (MediaContent).IsAssignableFrom(clrType))
        return Enumerable.Empty<OperationData>();
      List<OperationData> operations = new List<OperationData>();
      OperationData operationData1 = OperationData.Create<IEnumerable<ExtendedMediaLink>>(new Func<OperationContext, IEnumerable<ExtendedMediaLink>>(this.GetMediaFileLinks));
      operationData1.OperationType = OperationType.PerItem;
      operations.Add(operationData1);
      if (typeof (Image).IsAssignableFrom(clrType))
      {
        OperationData operationData2 = OperationData.Create<object>(new Func<OperationContext, object>(this.GetItemWithFallback));
        operationData2.OperationType = OperationType.PerItem;
        operations.Add(operationData2);
      }
      return (IEnumerable<OperationData>) operations;
    }

    private object GetItemWithFallback(OperationContext context)
    {
      string key = context.GetKey();
      context.GetClrType();
      LibrariesManager manager = LibrariesManager.GetManager();
      ILifecycleDataItem live = manager.Lifecycle.GetLive((ILifecycleDataItem) (manager.GetMediaItem(Guid.Parse(key)) ?? throw new ItemNotFoundException(string.Format("Media content with id: '{0}' is not found.", (object) key))));
      if (live != null && (live.PublishedTranslations.Count > 0 || live.Visible))
        return (object) live;
      throw new ItemNotFoundException(string.Format("Media content with id: '{0}' is not found.", (object) key));
    }

    private IEnumerable<ExtendedMediaLink> GetMediaFileLinks(
      OperationContext context)
    {
      string key = context.GetKey();
      int cultureLcid = AppSettings.CurrentSettings.GetCultureLcid(context.GetCulture());
      LibrariesManager manager = LibrariesManager.GetManager(context.GetProviderName());
      MediaContent masterItem = manager.GetMaster((Content) (manager.GetMediaItem(Guid.Parse(key)) ?? throw new ItemNotFoundException(string.Format("Media content with id: '{0}' is not found.", (object) key)))) as MediaContent;
      if (masterItem == null)
        throw new ItemNotFoundException(string.Format("Master item for media content with id: '{0}' is not found.", (object) key));
      MediaFileLink mediaFileLink = masterItem.MediaFileLinks.FirstOrDefault<MediaFileLink>((Func<MediaFileLink, bool>) (x => x.Culture == cultureLcid));
      Guid currentFileId = mediaFileLink == null ? Guid.Empty : mediaFileLink.FileId;
      IEnumerable<MediaFileLink> mediaFileLinks = (IEnumerable<MediaFileLink>) masterItem.MediaFileLinks;
      return (!ODataParamsUtil.GetQueryParam<bool>(context.GetQueryParams(), "getForCurrentFile") ? mediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (x => x.FileId != currentFileId)) : mediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (x => x.FileId == masterItem.FileId && x.Culture != cultureLcid))).GroupBy<MediaFileLink, Guid>((Func<MediaFileLink, Guid>) (ml => ml.FileId)).Select(g => new
      {
        FileId = g.Key,
        Count = g.Count<MediaFileLink>(),
        MediaLink = g.FirstOrDefault<MediaFileLink>(),
        Cultures = g.Select<MediaFileLink, int>((Func<MediaFileLink, int>) (mfl => mfl.Culture))
      }).OrderBy(grp => grp.Count).Select(g => new ExtendedMediaLink(g.MediaLink, g.Cultures));
    }
  }
}
