// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Resolvers.YesNoFieldResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.InlineEditing.Resolvers
{
  internal class YesNoFieldResolver : InlineEditingResolverBase, IInlineEditingResolver
  {
    private string fieldType = "YesNo";

    public object GetFieldValue(Guid id, string itemType, string fieldName, string provider)
    {
      ILifecycleDataItem component = (ManagerBase.GetMappedManager(itemType, provider) as ILifecycleManager).GetItem(TypeResolutionService.ResolveType(itemType), id) as ILifecycleDataItem;
      bool flag = (bool) TypeDescriptor.GetProperties((object) component)[fieldName].GetValue((object) component);
      string str = string.Empty;
      if (this.GetFieldDefinition(component, fieldName) is ChoiceFieldElement fieldDefinition && fieldDefinition.Choices.Count == 1)
        str = fieldDefinition.Choices[0].Text;
      return (object) new Hashtable()
      {
        [(object) "Selected"] = (object) flag,
        [(object) "Text"] = (object) str
      };
    }

    public string GetFieldType() => this.fieldType;
  }
}
