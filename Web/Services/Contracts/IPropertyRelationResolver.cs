// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.IPropertyRelationResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Linq;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal interface IPropertyRelationResolver
  {
    void Init(NameValueCollection parameters);

    IQueryable GetRelatedItems(object item);

    object GetRelatedItem(object item, Guid relatedItemKey);

    void CreateRelation(
      object item,
      Guid relatedItemKey,
      string relatedItemProvider,
      object persistentItem);

    void DeleteRelation(object item, Guid relatedItemKey);

    void DeleteAllRelations(object item);

    Type RelatedType { get; }

    string RelatedProviders { get; }

    bool IsMultipleRelation { get; }

    bool IsParentReference { get; }
  }
}
