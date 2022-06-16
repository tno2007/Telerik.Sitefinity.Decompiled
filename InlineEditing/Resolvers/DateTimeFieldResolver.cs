// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Resolvers.DateTimeFieldResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.InlineEditing.Resolvers
{
  internal class DateTimeFieldResolver : InlineEditingResolverBase, IInlineEditingResolver
  {
    private string fieldType = "DateTime";

    public object GetFieldValue(Guid id, string itemType, string fieldName, string provider)
    {
      object component = (ManagerBase.GetMappedManager(itemType, provider) as ILifecycleManager).GetItem(TypeResolutionService.ResolveType(itemType), id);
      object sitefinityUiTime = TypeDescriptor.GetProperties(component)[fieldName].GetValue(component);
      if (sitefinityUiTime != null)
        sitefinityUiTime = (object) ((DateTime) sitefinityUiTime).ToSitefinityUITime();
      return sitefinityUiTime;
    }

    public string GetFieldType() => this.fieldType;
  }
}
