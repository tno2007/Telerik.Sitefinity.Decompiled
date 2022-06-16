// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.Services.VersionOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.Versioning.Web.Services
{
  internal class VersionOperationProvider : IOperationProvider
  {
    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if (!typeof (IVersionSerializable).IsAssignableFrom(clrType))
        return (IEnumerable<OperationData>) new OperationData[0];
      OperationData operationData = OperationData.Create<string, object>(new Func<OperationContext, string, object>(this.Version));
      operationData.OperationType = OperationType.PerItem;
      return (IEnumerable<OperationData>) new OperationData[1]
      {
        operationData
      };
    }

    private object Version(OperationContext context, string id)
    {
      Guid key = (Guid) context.Data["key"];
      object obj = context.GetStrategy().GetItem(key);
      this.TransformToRequestedVersion(obj, id);
      return obj;
    }

    [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "The object item is changed between the two casts so it cannot be cached")]
    private void TransformToRequestedVersion(object item, string versionId)
    {
      ContentLifecycleStatus? nullable = new ContentLifecycleStatus?();
      Content content = (Content) null;
      if (item is ILifecycleDataItem lifecycleDataItem)
        nullable = new ContentLifecycleStatus?(lifecycleDataItem.Status);
      if (item is IHasParent hasParent1)
        content = hasParent1.Parent;
      VersionManager manager = VersionManager.GetManager();
      Guid guid = Guid.Parse(versionId);
      object targetObject = item;
      Guid changeId = guid;
      manager.GetSpecificVersionByChangeId(targetObject, changeId);
      if (nullable.HasValue)
        lifecycleDataItem.Status = nullable.Value;
      if (!(item is IHasParent hasParent2) || hasParent2.Parent != null || content == null)
        return;
      hasParent2.Parent = content;
    }
  }
}
