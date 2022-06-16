// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Configuration.CustomFieldsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Designers;

namespace Telerik.Sitefinity.ModuleEditor.Configuration
{
  /// <summary>Represents a configuration section for custom fields.</summary>
  public class CustomFieldsConfig : ConfigSection
  {
    /// <summary>Gets a collection of field type elements.</summary>
    /// <value>a collection of field type elements.</value>
    [ConfigurationProperty("fieldTypes")]
    public ConfigElementDictionary<string, FieldTypeElement> FieldTypes => (ConfigElementDictionary<string, FieldTypeElement>) this["fieldTypes"];

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      ConfigElementDictionary<string, FieldTypeElement> fieldTypes = this.FieldTypes;
      FieldTypeElement element1 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.ShortText.ToString(),
        Title = UserFriendlyDataType.ShortText.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element1.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (TextField).FullName,
        DesignerType = typeof (TextFieldDesigner).FullName,
        Title = "TextField",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element1);
      FieldTypeElement element2 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.LongText.ToString(),
        Title = UserFriendlyDataType.LongText.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element2.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (HtmlField).FullName,
        Title = "HtmlField",
        DesignerType = typeof (TextFieldDesigner).FullName,
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      element2.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (TextField).FullName,
        DesignerType = typeof (TextFieldDesigner).FullName,
        Title = "TextArea",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element2);
      FieldTypeElement element3 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.MultipleChoice.ToString(),
        Title = UserFriendlyDataType.MultipleChoice.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element3.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (ChoiceField).FullName,
        Title = "ChoiceField",
        DesignerType = typeof (MultipleChoiceFieldDesigner).FullName,
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element3);
      FieldTypeElement element4 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.Choices.ToString(),
        Title = UserFriendlyDataType.Choices.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element4.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (DynamicChoiceField).FullName,
        Title = "RadioButtons",
        DesignerType = typeof (ChoiceFieldDesigner).FullName,
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      element4.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (DynamicChoiceField).FullName,
        Title = "DropDownList",
        DesignerType = typeof (ChoiceFieldDesigner).FullName,
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      element4.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (DynamicChoiceField).FullName,
        Title = "Checkboxes",
        DesignerType = typeof (ChoiceFieldDesigner).FullName,
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element4);
      FieldTypeElement element5 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.YesNo.ToString(),
        Title = UserFriendlyDataType.YesNo.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element5.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (ChoiceField).FullName,
        Title = "ChoiceField",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element5);
      FieldTypeElement element6 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.Currency.ToString(),
        Title = UserFriendlyDataType.Currency.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element6.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (TextField).FullName,
        DesignerType = typeof (TextFieldDesigner).FullName,
        Title = "TextField",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element6);
      FieldTypeElement element7 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.DateAndTime.ToString(),
        Title = UserFriendlyDataType.DateAndTime.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element7.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (DateField).FullName,
        Title = "TextField",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element7);
      FieldTypeElement element8 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.Number.ToString(),
        Title = UserFriendlyDataType.Number.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element8.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (TextField).FullName,
        DesignerType = typeof (TextFieldDesigner).FullName,
        Title = "TextField",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element8);
      FieldTypeElement element9 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.Classification.ToString(),
        Title = UserFriendlyDataType.Classification.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element9.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (FlatTaxonField).FullName,
        DesignerType = typeof (TextFieldDesigner).FullName,
        Title = "FlatTaxonField",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      element9.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (HierarchicalTaxonField).FullName,
        DesignerType = typeof (TextFieldDesigner).FullName,
        Title = "HierarchicalTaxonField",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element9);
      FieldTypeElement element10 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.Image.ToString(),
        Title = UserFriendlyDataType.Image.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element10.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (ImageField).FullName,
        DesignerType = typeof (ImageFieldDesigner).FullName,
        Title = "ImageField",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element10);
      FieldTypeElement element11 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.RelatedData.ToString(),
        Title = UserFriendlyDataType.RelatedData.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element11.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (AssetsField).FullName,
        DesignerType = typeof (ImageFieldDesigner).FullName,
        Title = "RelatedData",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element11);
      FieldTypeElement element12 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.RelatedMedia.ToString(),
        Title = UserFriendlyDataType.RelatedMedia.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element12.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (RelatedMediaField).FullName,
        DesignerType = typeof (ImageFieldDesigner).FullName,
        Title = "RelatedMedia",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element12);
      FieldTypeElement element13 = new FieldTypeElement()
      {
        Name = UserFriendlyDataType.Unknown.ToString(),
        Title = UserFriendlyDataType.Unknown.ToString(),
        ResourceClassId = typeof (ModuleEditorResources).Name
      };
      element13.Controls.Add(new FieldControlElement()
      {
        FieldTypeOrPath = typeof (TextField).FullName,
        DesignerType = typeof (TextFieldDesigner).FullName,
        Title = "TextField",
        ResourceClassId = typeof (ModuleEditorResources).Name
      });
      fieldTypes.Add(element13);
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string fieldTypes = "fieldTypes";
    }
  }
}
