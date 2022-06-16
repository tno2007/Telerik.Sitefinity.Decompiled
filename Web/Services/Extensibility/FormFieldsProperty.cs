// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.FormFieldsProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Data;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class FormFieldsProperty : CalculatedProperty
  {
    public override Type ReturnType => typeof (IEnumerable<FormField>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (FormDraft key in items ?? (IEnumerable) Enumerable.Empty<ControlData>())
      {
        if (key != null)
        {
          IEnumerable<ControlData> controlDatas = OpenAccessFormsProvider.SortFormControls((IEnumerable<ControlData>) key.Controls);
          List<FormField> formFieldList = new List<FormField>();
          foreach (ControlData controlData in controlDatas)
          {
            if (controlData != null)
            {
              FormField formField = new FormField();
              ControlProperty controlProperty = controlData.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID"));
              if (controlProperty != null)
                formField.Id = controlProperty.Value;
              object behaviorObject = ControlUtilities.BehaviorResolver.GetBehaviorObject((manager as FormsManager).LoadControl((ObjectData) controlData, (CultureInfo) null));
              if (behaviorObject != null)
              {
                this.GetFieldType(behaviorObject, ref formField);
                if (behaviorObject is ISupportRules supportRules)
                {
                  List<Operator> operatorList = new List<Operator>();
                  foreach (KeyValuePair<ConditionOperator, string> keyValuePair in (IEnumerable<KeyValuePair<ConditionOperator, string>>) supportRules.Operators)
                    operatorList.Add(new Operator()
                    {
                      Key = keyValuePair.Key,
                      Value = keyValuePair.Value
                    });
                  formField.Operators = operatorList;
                  formField.Title = supportRules.Title;
                }
                PropertyInfo property = behaviorObject.GetType().GetProperty("Model");
                if (property != (PropertyInfo) null)
                {
                  string inputType = formField.InputType;
                  bool flag = inputType != null && inputType.Equals((object) TextType.Hidden);
                  if (property.GetValue(behaviorObject) is IHideable && !flag)
                    formField.Hideable = true;
                }
              }
              if (string.IsNullOrWhiteSpace(formField.Title))
                formField.Title = Res.Get<FormsResources>().Untitled;
              formFieldList.Add(formField);
            }
          }
          values.Add((object) key, (object) formFieldList);
        }
      }
      return (IDictionary<object, object>) values;
    }

    private void GetFieldType(object controlObject, ref FormField formField)
    {
      if (typeof (ITextField).IsAssignableFrom(controlObject.GetType()))
      {
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.Textbox);
        ITextField textField = controlObject as ITextField;
        formField.InputType = textField.InputType.ToString();
      }
      else if (typeof (ICheckboxFormField).IsAssignableFrom(controlObject.GetType()))
      {
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.Checkbox);
        ICheckboxFormField checkboxFormField = controlObject as ICheckboxFormField;
        formField.Values = checkboxFormField.Choices;
      }
      else if (typeof (IMultipleChoiceFormField).IsAssignableFrom(controlObject.GetType()))
      {
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.MultipleChoice);
        IMultipleChoiceFormField multipleChoiceFormField = controlObject as IMultipleChoiceFormField;
        formField.Values = multipleChoiceFormField.Choices;
      }
      else if (typeof (IDropDownFormField).IsAssignableFrom(controlObject.GetType()))
      {
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.DropDown);
        IDropDownFormField dropDownFormField = controlObject as IDropDownFormField;
        formField.Values = dropDownFormField.Choices;
      }
      else if (typeof (IParagraphFormField).IsAssignableFrom(controlObject.GetType()))
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.Paragraph);
      else if (typeof (ISubmitFormField).IsAssignableFrom(controlObject.GetType()))
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.Submit);
      else if (typeof (ISectionHeaderFormField).IsAssignableFrom(controlObject.GetType()))
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.SectionHeader);
      else if (typeof (IHiddenFormField).IsAssignableFrom(controlObject.GetType()))
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.Hidden);
      else if (typeof (ICaptchaFormField).IsAssignableFrom(controlObject.GetType()))
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.Captcha);
      else if (typeof (IFormPageBreak).IsAssignableFrom(controlObject.GetType()))
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.PageBreak);
      else if (typeof (IFormNavigation).IsAssignableFrom(controlObject.GetType()))
      {
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.FormNavigation);
      }
      else
      {
        if (!typeof (IFileFormField).IsAssignableFrom(controlObject.GetType()))
          return;
        formField.Type = Enum.GetName(typeof (FormFieldType), (object) FormFieldType.FileUpload);
      }
    }
  }
}
