// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Resolvers.ChoiceFieldResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Services.InlineEditing;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.InlineEditing.Resolvers
{
  internal class ChoiceFieldResolver : InlineEditingResolverBase, IInlineEditingResolver
  {
    private string fieldType = "Choice";

    public object GetFieldValue(Guid id, string itemType, string fieldName, string provider)
    {
      ILifecycleDataItem component = (ManagerBase.GetMappedManager(itemType, provider) as ILifecycleManager).GetItem(TypeResolutionService.ResolveType(itemType), id) as ILifecycleDataItem;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) component);
      string empty = string.Empty;
      List<ChoiceOptionModel> choiceOptionModelList = new List<ChoiceOptionModel>();
      if (this.GetFieldDefinition(component, fieldName) is ChoiceFieldElement fieldDefinition && fieldDefinition.Choices.Count > 0)
      {
        List<string> source = new List<string>();
        object obj = properties[fieldName].GetValue((object) component);
        if (obj != null)
        {
          Type type = obj.GetType();
          if (!type.IsArray)
          {
            string str = string.Empty;
            if (typeof (ChoiceOption).IsAssignableFrom(type))
            {
              ChoiceOption choiceOption = (ChoiceOption) properties[fieldName].GetValue((object) component);
              if (choiceOption != null)
                str = choiceOption.PersistedValue;
            }
            else if (type.FullName == typeof (string).FullName)
              str = (string) properties[fieldName].GetValue((object) component);
            source.Add(str);
          }
          else
          {
            string[] collection = (string[]) null;
            if (typeof (ChoiceOption[]).IsAssignableFrom(type))
              collection = ((IEnumerable<ChoiceOption>) (ChoiceOption[]) properties[fieldName].GetValue((object) component)).Select<ChoiceOption, string>((Func<ChoiceOption, string>) (c => c.PersistedValue)).ToArray<string>();
            else if (type.FullName == typeof (string[]).FullName)
              collection = (string[]) properties[fieldName].GetValue((object) component);
            source.AddRange((IEnumerable<string>) collection);
          }
        }
        foreach (IChoiceDefinition choice in fieldDefinition.Choices)
        {
          IChoiceDefinition option = choice;
          choiceOptionModelList.Add(new ChoiceOptionModel()
          {
            Text = HttpUtility.HtmlDecode(option.Text),
            Value = option.Value,
            Selected = source.Any<string>((Func<string, bool>) (c => c == option.Value))
          });
        }
      }
      return (object) new ChoiceFieldModel()
      {
        OptionsDataSource = choiceOptionModelList.ToArray(),
        GroupName = (fieldName + "Group")
      };
    }

    public string GetFieldType() => this.fieldType;
  }
}
