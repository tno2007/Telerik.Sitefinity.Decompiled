// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider.FolderOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider
{
  internal class FolderOperationProvider : IOperationProvider
  {
    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if (!typeof (Library).IsAssignableFrom(clrType) && !typeof (MediaContent).IsAssignableFrom(clrType))
        return Enumerable.Empty<OperationData>();
      OperationData operationData1 = OperationData.Create<Guid?, IEnumerable<ExtendedFolder>>(new Func<OperationContext, Guid?, IEnumerable<ExtendedFolder>>(this.FoldersRecursiveSearch));
      operationData1.OperationType = OperationType.Collection;
      OperationData operationData2 = OperationData.Create<Guid, ExtendedFolder>(new Func<OperationContext, Guid, ExtendedFolder>(this.FolderSearchById));
      operationData2.OperationType = OperationType.Collection;
      return (IEnumerable<OperationData>) new OperationData[2]
      {
        operationData1,
        operationData2
      };
    }

    private IEnumerable<ExtendedFolder> FoldersRecursiveSearch(
      OperationContext context,
      Guid? parentId)
    {
      string providerName = context.GetProviderName();
      Type clrType = context.GetClrType();
      context.GetCulture();
      IDictionary<string, string> queryParams = context.GetQueryParams();
      FolderProvider folderProvider = new FolderProvider(LibrariesManager.GetManager(providerName), clrType);
      string queryParam1 = ODataParamsUtil.GetQueryParam<string>(queryParams, "$count");
      bool flag = false;
      ref bool local = ref flag;
      bool.TryParse(queryParam1, out local);
      int? totalCount = new int?();
      if (flag)
        totalCount = new int?(0);
      string queryParam2 = ODataParamsUtil.GetQueryParam<string>(queryParams, "$skip");
      string queryParam3 = ODataParamsUtil.GetQueryParam<string>(queryParams, "$top");
      string queryParam4 = ODataParamsUtil.GetQueryParam<string>(queryParams, "$search");
      string queryParam5 = ODataParamsUtil.GetQueryParam<string>(queryParams, "$orderby");
      bool queryParam6 = ODataParamsUtil.GetQueryParam<bool>(queryParams, "recursive");
      bool queryParam7 = ODataParamsUtil.GetQueryParam<bool>(queryParams, "includeParent");
      Guid? queryParam8 = ODataParamsUtil.GetQueryParam<Guid?>(queryParams, "excludedRootId");
      bool queryParam9 = ODataParamsUtil.GetQueryParam<bool>(queryParams, "filterByCreateChildPermissions");
      string queryParam10 = ODataParamsUtil.GetQueryParam<string>(queryParams, "excludedItemIds");
      bool queryParam11 = ODataParamsUtil.GetQueryParam<bool>(queryParams, "getMediaItems");
      bool queryParam12 = ODataParamsUtil.GetQueryParam<bool>(queryParams, "useLiveData");
      IEnumerable<string> excludedItemIds = (IEnumerable<string>) null;
      if (!string.IsNullOrWhiteSpace(queryParam10))
        excludedItemIds = (IEnumerable<string>) ((IEnumerable<string>) queryParam10.Split(',')).ToList<string>();
      IEnumerable<ExtendedFolder> folders = folderProvider.GetFolders(parentId, queryParam4, queryParam3, queryParam2, ref totalCount, queryParam6, queryParam7, queryParam8, queryParam5, queryParam9, excludedItemIds, queryParam11, queryParam12);
      return !flag ? (IEnumerable<ExtendedFolder>) folders.ToList<ExtendedFolder>() : (IEnumerable<ExtendedFolder>) new TotalCountList<ExtendedFolder>(folders, (long) totalCount.Value);
    }

    private ExtendedFolder FolderSearchById(OperationContext context, Guid id)
    {
      string providerName = context.GetProviderName();
      Type clrType = context.GetClrType();
      return new FolderProvider(LibrariesManager.GetManager(providerName), clrType).GetFolderById(id);
    }
  }
}
