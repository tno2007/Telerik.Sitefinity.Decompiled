// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider.DocumentOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services.Contracts;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider
{
  internal class DocumentOperationProvider : IOperationProvider
  {
    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if (!typeof (Document).IsAssignableFrom(clrType))
        return Enumerable.Empty<OperationData>();
      OperationData operationData = OperationData.Create<Guid, IList<Document>>(new Func<OperationContext, Guid, IList<Document>>(this.DocumentsRecursiveSearch));
      operationData.OperationType = OperationType.Collection;
      return (IEnumerable<OperationData>) new OperationData[1]
      {
        operationData
      };
    }

    private IList<Document> DocumentsRecursiveSearch(
      OperationContext context,
      Guid rootLibraryId)
    {
      context.GetProviderName();
      LibrariesManager manager = (context.GetStrategy() as IManagerStrategy).Manager as LibrariesManager;
      IFolder folder = manager.GetFolder(rootLibraryId);
      return this.GetDescendants(context, manager, folder);
    }

    private IList<Document> GetDescendants(
      OperationContext context,
      LibrariesManager manager,
      IFolder parentFolder)
    {
      IDictionary<string, string> queryParams = context.GetQueryParams();
      int queryParam1 = ODataParamsUtil.GetQueryParam<int>(queryParams, "$skip");
      int queryParam2 = ODataParamsUtil.GetQueryParam<int>(queryParams, "$top");
      int count = queryParam2 == 0 ? 50 : queryParam2;
      string queryParam3 = ODataParamsUtil.GetQueryParam<string>(queryParams, "$search");
      CultureInfo culture = context.GetCulture();
      IQueryable source = this.ApplySearchFilter(context.GetQuery(), queryParam3, culture);
      return (IList<Document>) Queryable.Cast<Document>(manager.GetDescendantsFromQuery<Document>(Queryable.Cast<Document>(source), parentFolder).Skip(queryParam1).Take(count)).ToList<Document>();
    }

    private IQueryable ApplySearchFilter(
      IQueryable query,
      string search,
      CultureInfo culture)
    {
      if (!string.IsNullOrEmpty(search))
      {
        string empty = string.Empty;
        string predicate = !SystemManager.CurrentContext.AppSettings.Multilingual ? string.Format("Title.ToUpper().Contains(\"{0}\")", (object) search.ToUpper()) : string.Format("Title[\"{0}\"].ToUpper().Contains(\"{1}\") OR Title[\"\"].ToUpper().Contains(\"{1}\")", (object) culture.Name, (object) search.ToUpper());
        query = query.Where(predicate);
      }
      return query;
    }
  }
}
