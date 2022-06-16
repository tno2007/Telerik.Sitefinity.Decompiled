// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.IItemStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.Services.Contracts.ItemMeta;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal interface IItemStrategy
  {
    object GetItem(Guid key);

    IQueryable<object> GetItems(ICollection<Guid> keys);

    IQueryable GetQuery();

    void Init(
      IDictionary<string, IPropertyRelationResolver> resolvers,
      ITypeSettings typeSettings);

    object Create(IDictionary<string, object> properties);

    void Update(Guid key, IDictionary<string, object> properties);

    void Delete(Guid key, bool deleteAllTranslations);

    OperationResult BulkDelete(IEnumerable<Guid> keys, bool deleteAllTranslations);

    void Save();

    object GetCopy();

    IDictionary<string, object> Validate(
      IDictionary<string, object> properties,
      bool forceFullValidation);

    bool CanEdit(object item, ref ItemMetaData metadata);
  }
}
