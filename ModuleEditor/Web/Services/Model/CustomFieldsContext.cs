// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.CustomFieldsContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.ModuleEditor.WidgetTemplates;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  public class CustomFieldsContext
  {
    internal const string CustomFieldsSegment = "CustomFields.";
    private const string ExpandableCssClass = "sfExpandableForm";
    private ConfigManager configManager;
    private MetadataManager metadataManager;
    private Dictionary<string, ConfigSection> configs = new Dictionary<string, ConfigSection>();
    private Type contentType;
    private MetaType metaType;
    /// <summary>Deatil form view elements.</summary>
    protected List<DetailFormViewElement> views;
    /// <summary>Content type full name.</summary>
    protected string contentTypeFullName;
    /// <summary>The custom fields section name</summary>
    public static readonly string CustomFieldsSectionName = "CustomFieldsSection";
    /// <summary>The custom fields section name</summary>
    public static readonly string customFieldsSectionName = CustomFieldsContext.CustomFieldsSectionName;
    /// <summary>The products main section name</summary>
    public static readonly string ProductsMainSectionName = "MainSection";
    /// <summary>The products main section name</summary>
    public static readonly string productsMainSectionName = CustomFieldsContext.ProductsMainSectionName;
    /// <summary>The related media section name</summary>
    public static readonly string RelatedMediaSectionName = "RelatedMediaSection";
    /// <summary>The related media section name</summary>
    public static readonly string relatedMediaSectionName = CustomFieldsContext.RelatedMediaSectionName;
    /// <summary>The related data section name</summary>
    public static readonly string RelatedDataSectionName = "RelatedDataSection";
    /// <summary>The related data section name</summary>
    public static readonly string relatedDataSectionName = CustomFieldsContext.RelatedDataSectionName;

    public CustomFieldsContext(string contentTypeFullName, MetadataManager manager = null)
    {
      this.metadataManager = manager;
      if (manager == null)
        this.metadataManager = MetadataManager.GetManager();
      this.contentTypeFullName = contentTypeFullName;
      this.configManager = ConfigManager.GetManager();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.Services.Model.CustomFieldsContext" /> class.
    /// </summary>
    public CustomFieldsContext(Type contentType, MetadataManager manager = null)
    {
      this.configManager = ConfigManager.GetManager();
      this.metadataManager = manager;
      if (manager == null)
        this.metadataManager = MetadataManager.GetManager();
      this.contentType = contentType;
      this.contentTypeFullName = this.contentType.FullName;
    }

    /// <summary>Gets the reserved field names</summary>
    internal static List<string> ReservedFieldNames => new List<string>()
    {
      "Keywords"
    };

    /// <summary>Adds or update custom fields.</summary>
    /// <param name="fields">The fields.</param>
    public void AddOrUpdateCustomFields(
      IDictionary<string, WcfField> fields,
      string contentTypeName)
    {
      foreach (WcfField wcfField in (IEnumerable<WcfField>) fields.Values)
      {
        ICustomFieldBuilder customFieldBuilder = (ICustomFieldBuilder) null;
        if (ObjectFactory.IsTypeRegistered<ICustomFieldBuilder>(wcfField.FieldTypeKey))
          customFieldBuilder = ObjectFactory.Resolve<ICustomFieldBuilder>(wcfField.FieldTypeKey);
        string fieldName = wcfField.Name;
        UserFriendlyDataType mappingKey = this.GetMappingKey(wcfField);
        if (wcfField.IsCustom)
        {
          MetaField metaField = this.MetaType.Fields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (f => string.Compare(f.FieldName, fieldName, true, CultureInfo.InvariantCulture) == 0));
          if (metaField != null && metaField.IsInternal && metaField.ClrType != typeof (RelatedItems).FullName)
            CustomFieldsContext.Validate(wcfField, this.GetActualType());
          PropertyDescriptor propertyDescriptor = FieldHelper.GetFields(this.GetActualType()).FirstOrDefault<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (f => string.Compare(f.Name, fieldName, true, CultureInfo.InvariantCulture) == 0));
          if (metaField != null)
          {
            metaField.Title = wcfField.Definition.Title;
            this.UpdateMetaField(wcfField, metaField, mappingKey);
          }
          else if (propertyDescriptor == null)
          {
            CustomFieldsContext.Validate(wcfField, this.GetActualType());
            if (customFieldBuilder != null)
            {
              CustomFieldsContext.Validate(wcfField, this.GetActualType());
              metaField = customFieldBuilder.CreateCustomMetaField(this.MetaType, wcfField, this.metadataManager, this.GetActualType());
              this.MetaType.Fields.Add(metaField);
            }
            else
            {
              metaField = this.metadataManager.CreateMetafield(fieldName);
              metaField.Title = wcfField.Definition.Title;
              this.SaveField(metaField, mappingKey, wcfField);
            }
            CustomFieldsContext.AddCustomFieldAttributes(metaField, mappingKey.ToString());
          }
          else
            throw new Exception(Res.Get<ModuleEditorResources>().DuplicateFieldException.Arrange((object) this.contentType, (object) fieldName));
          if (!string.IsNullOrEmpty(wcfField.Definition.Example))
            metaField.Description = wcfField.Definition.Example;
        }
        else if (mappingKey != UserFriendlyDataType.Unknown && mappingKey != UserFriendlyDataType.Image)
        {
          MetaField metaField = this.MetaType.Fields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (f => string.Compare(f.FieldName, fieldName, true, CultureInfo.InvariantCulture) == 0));
          if (metaField == null)
          {
            MetaField metafield = this.metadataManager.CreateMetafield(fieldName);
            metafield.Title = wcfField.Definition.Title;
            metafield.IsInternal = true;
            this.SaveField(metafield, mappingKey, wcfField);
          }
          else
          {
            metaField.Title = wcfField.Definition.Title;
            this.UpdateMetaField(wcfField, metaField, mappingKey);
          }
        }
        if (mappingKey == UserFriendlyDataType.Classification)
          wcfField.Definition.DefaultStringValue = wcfField.Definition.DefaultValue = string.Empty;
        this.SaveFieldDefinition(wcfField, this.GetActualType().Name, customFieldBuilder);
      }
    }

    /// <summary>Adds the attributes required for all custom fields</summary>
    /// <param name="metaField"></param>
    /// <param name="fieldTypeKey"></param>
    public static void AddCustomFieldAttributes(MetaField metaField, string fieldTypeKey)
    {
      IList<MetaFieldAttribute> metaAttributes1 = metaField.MetaAttributes;
      MetaFieldAttribute metaFieldAttribute1 = new MetaFieldAttribute();
      metaFieldAttribute1.Name = "UserFriendlyDataType";
      metaFieldAttribute1.Value = fieldTypeKey;
      metaAttributes1.Add(metaFieldAttribute1);
      IList<MetaFieldAttribute> metaAttributes2 = metaField.MetaAttributes;
      MetaFieldAttribute metaFieldAttribute2 = new MetaFieldAttribute();
      metaFieldAttribute2.Name = DynamicAttributeNames.IsCommonProperty;
      metaFieldAttribute2.Value = "true";
      metaAttributes2.Add(metaFieldAttribute2);
    }

    /// <summary>
    /// Verifies if a field is already defined in the type or field is using reserved field name
    /// </summary>
    /// <param name="fieldToAdd"></param>
    public static void Validate(WcfField fieldToAdd, Type contentType)
    {
      string fName = fieldToAdd.Name.ToLowerInvariant();
      if (FieldHelper.GetFields(contentType).Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (p => p.Name.ToLowerInvariant() == fName)).Any<PropertyDescriptor>() || fName == "Categories".ToLowerInvariant())
        throw new Exception(Res.Get<ModuleEditorResources>().DuplicateFieldException.Arrange((object) contentType, (object) fName));
      if (CustomFieldsContext.ReservedFieldNames.Select<string, string>((Func<string, string>) (p => p.ToLowerInvariant())).Contains<string>(fName))
        throw new Exception(Res.Get<ModuleEditorResources>().ReservedFieldNameErrorMessage.Arrange((object) fName));
    }

    /// <summary>Sets the database mappings for a meta field.</summary>
    /// <param name="metaField">The meta field.</param>
    /// <param name="metaType">The meta type for the meta field.</param>
    /// <param name="mappingKey">The mapping key.</param>
    /// <param name="field">The WCF field view model.</param>
    /// <param name="manager">A reference to a MetadataManager.</param>
    public static void SetFieldDatabaseMappings(
      MetaField metaField,
      MetaType metaType,
      UserFriendlyDataType mappingKey,
      WcfField field,
      MetadataManager manager)
    {
      string key = CustomFieldsContext.ResolveFieldTypeKey(mappingKey, field);
      DatabaseMappingsElement databaseMappingsElement;
      if (!Telerik.Sitefinity.Configuration.Config.Get<MetadataConfig>().DatabaseMappings.TryGetValue(key, out databaseMappingsElement))
        throw new ArgumentException("DB mapping for field type '{0}' was not found in the Metadata configuration.".Arrange((object) key));
      IDatabaseMapping databaseMapping = (IDatabaseMapping) field.DatabaseMapping ?? (IDatabaseMapping) databaseMappingsElement;
      metaField.ClrType = !field.IsCustom || mappingKey != UserFriendlyDataType.ShortText && mappingKey != UserFriendlyDataType.LongText ? databaseMappingsElement.ClrType : (field.Definition.IsLocalizable ? typeof (Lstring).FullName : typeof (string).FullName);
      metaField.DBType = databaseMapping.DbType;
      metaField.DBSqlType = databaseMapping.DbSqlType;
      metaField.DBLength = databaseMapping.DbLength;
      metaField.DBScale = databaseMapping.DbScale;
      if (field.Definition != null && field.Definition.ValidatorDefinition != null)
      {
        metaField.Required = ((int) field.Definition.ValidatorDefinition.Required ?? (!databaseMapping.Nullable ? 1 : 0)) != 0;
        metaField.RecommendedCharactersCount = field.Definition.ValidatorDefinition.RecommendedCharactersCount;
      }
      else
        metaField.Required = !databaseMapping.Nullable;
      if (!databaseMapping.ColumnName.IsNullOrEmpty())
        metaField.ColumnName = databaseMapping.ColumnName;
      switch (mappingKey)
      {
        case UserFriendlyDataType.ShortText:
        case UserFriendlyDataType.LongText:
        case UserFriendlyDataType.DateAndTime:
        case UserFriendlyDataType.Number:
          metaField.IsLocalizable = field.Definition.IsLocalizable;
          break;
        case UserFriendlyDataType.Choices:
          metaField.ClrType = field.Definition.AllowMultipleSelection ? typeof (ChoiceOption[]).FullName : typeof (ChoiceOption).FullName;
          metaField.ChoiceFieldDefinition = field.Definition.Choices;
          metaField.DBSqlType = databaseMapping.DbSqlType == string.Empty ? (string) null : databaseMapping.DbSqlType;
          metaField.DBScale = databaseMapping.DbScale == string.Empty ? "0" : databaseMapping.DbScale;
          metaField.DefaultValue = string.Empty;
          break;
      }
      if (!databaseMapping.Indexed)
        return;
      string indexName = "sf_idx_" + metaType.ClassName + "_" + metaField.FieldName;
      MetaIndex metaIndex = manager.CreateMetaIndex(indexName);
      metaField.Index = metaIndex;
    }

    private UserFriendlyDataType GetMappingKey(WcfField field)
    {
      UserFriendlyDataType result;
      if (!System.Enum.TryParse<UserFriendlyDataType>(field.FieldTypeKey, out result))
        result = UserFriendlyDataType.Unknown;
      return result;
    }

    private static string ResolveFieldTypeKey(UserFriendlyDataType mappingKey, WcfField field) => mappingKey != UserFriendlyDataType.Unknown ? mappingKey.ToString() : field.FieldTypeKey;

    private void ValidateTaxonomy(ITaxonomy taxonomy)
    {
      TaxonomyPropertyDescriptor propertyDescriptor = TaxonomyManager.GetPropertyDescriptor(this.GetActualType(), taxonomy);
      if (propertyDescriptor != null)
        throw new Exception(Res.Get<ModuleEditorResources>().DuplicateClassificationException.Arrange((object) this.GetActualType(), (object) taxonomy.Name, (object) propertyDescriptor.Name));
    }

    private void SaveField(MetaField metaField, UserFriendlyDataType mappingKey, WcfField field)
    {
      if (mappingKey == UserFriendlyDataType.YesNo)
        field.Definition.FieldType = typeof (ChoiceField).FullName;
      if (mappingKey == UserFriendlyDataType.Classification)
      {
        ITaxonomy taxonomy = field.Definition.Taxonomy;
        if (taxonomy != null)
        {
          this.ValidateTaxonomy(taxonomy);
          metaField.TaxonomyProvider = ((DataProviderBase) taxonomy.Provider).Name;
          metaField.TaxonomyId = taxonomy.Id;
          metaField.IsSingleTaxon = false;
          if (taxonomy is HierarchicalTaxonomy)
            field.Definition.FieldType = typeof (HierarchicalTaxonField).FullName;
          else if (taxonomy is FlatTaxonomy)
            field.Definition.FieldType = typeof (FlatTaxonField).FullName;
          string fieldControlTemplate = TaxonomyManager.GetTaxonomyFieldControlTemplate(field.Name, taxonomy);
          if (fieldControlTemplate != null)
          {
            MetaFieldAttribute metaFieldAttribute1 = new MetaFieldAttribute();
            metaFieldAttribute1.Name = DynamicAttributeNames.ControlTag;
            metaFieldAttribute1.Value = fieldControlTemplate;
            MetaFieldAttribute metaFieldAttribute2 = metaFieldAttribute1;
            metaField.MetaAttributes.Add(metaFieldAttribute2);
          }
          field.Definition.AllowMultipleSelection = true;
          this.UpdateMetaField(field, metaField, mappingKey);
        }
      }
      else
      {
        CustomFieldsContext.SetFieldDatabaseMappings(metaField, this.MetaType, mappingKey, field, this.metadataManager);
        if (mappingKey == UserFriendlyDataType.LongText)
        {
          IList<MetaFieldAttribute> metaAttributes = metaField.MetaAttributes;
          MetaFieldAttribute metaFieldAttribute = new MetaFieldAttribute();
          metaFieldAttribute.Name = DynamicAttributeNames.ControlTag;
          metaFieldAttribute.Value = string.Format("<sitefinity:HtmlField runat=\"server\" DisplayMode=\"Read\" Value='<%# ControlUtilities.Sanitize(Eval(\"{0}\"))%>' />", (object) field.Name);
          metaAttributes.Add(metaFieldAttribute);
        }
        else if (mappingKey == UserFriendlyDataType.RelatedMedia)
        {
          bool isMasterView = false;
          string mediaType = field.Definition.MediaType;
          if (!(mediaType == "image"))
          {
            if (!(mediaType == "video"))
            {
              if (mediaType == "file")
              {
                field.Definition.RelatedDataType = typeof (Document).FullName;
                isMasterView = field.Definition.AllowMultipleFiles;
              }
            }
            else
            {
              field.Definition.RelatedDataType = typeof (Video).FullName;
              isMasterView = field.Definition.AllowMultipleVideos;
            }
          }
          else
          {
            field.Definition.RelatedDataType = typeof (Image).FullName;
            isMasterView = field.Definition.AllowMultipleImages;
          }
          metaField.ClrType = typeof (RelatedItems).FullName;
          metaField.IsInternal = true;
          metaField.AllowMultipleRelations = isMasterView;
          MetaField metaField1 = metaField;
          int num;
          if (!isMasterView)
          {
            bool? required = field.Definition.ValidatorDefinition.Required;
            if (!required.HasValue)
            {
              num = 0;
            }
            else
            {
              required = field.Definition.ValidatorDefinition.Required;
              num = required.Value ? 1 : 0;
            }
          }
          else
            num = 0;
          metaField1.IsProtectedRelation = num != 0;
          MetaFieldAttribute metaFieldAttribute3 = new MetaFieldAttribute();
          metaFieldAttribute3.Name = DynamicAttributeNames.RelatedType;
          metaFieldAttribute3.Value = field.Definition.RelatedDataType;
          MetaFieldAttribute metaFieldAttribute4 = metaFieldAttribute3;
          metaField.MetaAttributes.Add(metaFieldAttribute4);
          MetaFieldAttribute metaFieldAttribute5 = new MetaFieldAttribute();
          metaFieldAttribute5.Name = DynamicAttributeNames.RelatedProviders;
          metaFieldAttribute5.Value = field.Definition.RelatedDataProvider;
          MetaFieldAttribute metaFieldAttribute6 = metaFieldAttribute5;
          metaField.MetaAttributes.Add(metaFieldAttribute6);
          MetaFieldAttribute metaFieldAttribute7 = new MetaFieldAttribute();
          metaFieldAttribute7.Name = DynamicAttributeNames.ControlTag;
          metaFieldAttribute7.Value = FieldTemplateBuilder.BuildRelatedDataFieldTemplate(field.Definition.FrontendWidgetTypeName, field.Definition.FrontendWidgetLabel, this.MetaType.FullTypeName, field.Definition.RelatedDataType, field.Definition.RelatedDataProvider, field.Name, isMasterView);
          MetaFieldAttribute metaFieldAttribute8 = metaFieldAttribute7;
          metaField.MetaAttributes.Add(metaFieldAttribute8);
          metaField.Description = field.Definition.Description;
        }
        else if (mappingKey == UserFriendlyDataType.RelatedData)
        {
          metaField.ClrType = typeof (RelatedItems).FullName;
          metaField.IsInternal = true;
          metaField.AllowMultipleRelations = field.Definition.AllowMultipleSelection;
          MetaField metaField2 = metaField;
          int num;
          if (!field.Definition.AllowMultipleSelection)
          {
            bool? required = field.Definition.ValidatorDefinition.Required;
            if (!required.HasValue)
            {
              num = 0;
            }
            else
            {
              required = field.Definition.ValidatorDefinition.Required;
              num = required.Value ? 1 : 0;
            }
          }
          else
            num = 0;
          metaField2.IsProtectedRelation = num != 0;
          MetaFieldAttribute metaFieldAttribute9 = new MetaFieldAttribute();
          metaFieldAttribute9.Name = DynamicAttributeNames.RelatedType;
          metaFieldAttribute9.Value = field.Definition.RelatedDataType;
          MetaFieldAttribute metaFieldAttribute10 = metaFieldAttribute9;
          metaField.MetaAttributes.Add(metaFieldAttribute10);
          MetaFieldAttribute metaFieldAttribute11 = new MetaFieldAttribute();
          metaFieldAttribute11.Name = DynamicAttributeNames.RelatedProviders;
          metaFieldAttribute11.Value = field.Definition.RelatedDataProvider;
          MetaFieldAttribute metaFieldAttribute12 = metaFieldAttribute11;
          metaField.MetaAttributes.Add(metaFieldAttribute12);
          MetaFieldAttribute metaFieldAttribute13 = new MetaFieldAttribute();
          metaFieldAttribute13.Name = DynamicAttributeNames.ControlTag;
          metaFieldAttribute13.Value = FieldTemplateBuilder.BuildRelatedDataFieldTemplate(field.Definition.FrontendWidgetTypeName, field.Definition.FrontendWidgetLabel, this.MetaType.FullTypeName, field.Definition.RelatedDataType, field.Definition.RelatedDataProvider, field.Name, field.Definition.AllowMultipleSelection);
          MetaFieldAttribute metaFieldAttribute14 = metaFieldAttribute13;
          metaField.MetaAttributes.Add(metaFieldAttribute14);
          metaField.Description = field.Definition.Description;
        }
      }
      this.UpdateMetaFieldProperties(field, metaField, mappingKey);
      this.metaType.Fields.Add(metaField);
    }

    public static object ResolveDefaultValue(string clrType, string stringValue)
    {
      if (clrType.IsNullOrEmpty() || stringValue.IsNullOrEmpty())
        return (object) null;
      Type type = TypeResolutionService.ResolveType(clrType, false);
      if (type != (Type) null)
      {
        if (type == typeof (string))
          return (object) stringValue;
        if (type == typeof (DateTime?))
          return (object) CustomFieldsContext.ParseToNullableDateTime(stringValue);
        if (type == typeof (DateTime))
          return (object) DateTime.Parse(stringValue, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        if (type == typeof (float) || type == typeof (double) || type == typeof (Decimal))
          return (object) float.Parse(stringValue, (IFormatProvider) CultureInfo.InvariantCulture);
        TypeConverter converter = TypeDescriptor.GetConverter(type);
        if (converter != null && converter.CanConvertFrom(typeof (string)))
          return converter.ConvertFromString(stringValue);
      }
      return (object) null;
    }

    public void SaveFieldDefinition(
      WcfField field,
      string contentTypeName,
      ICustomFieldBuilder customFieldBuilder)
    {
      Control control = (Control) null;
      UserFriendlyDataType mappingKey = this.GetMappingKey(field);
      string clrType;
      if (field.DatabaseMapping == null)
      {
        DatabaseMappingsElement databaseMappingsElement;
        if (!Telerik.Sitefinity.Configuration.Config.Get<MetadataConfig>().DatabaseMappings.TryGetValue(mappingKey.ToString(), out databaseMappingsElement))
          throw new ArgumentException("Unkown DB mapping.");
        clrType = databaseMappingsElement.ClrType;
      }
      else
      {
        clrType = field.DatabaseMapping.ClrType;
        if (string.IsNullOrEmpty(clrType) && (mappingKey != UserFriendlyDataType.Unknown || field.IsCustom))
        {
          DatabaseMappingsElement databaseMappingsElement;
          if (!Telerik.Sitefinity.Configuration.Config.Get<MetadataConfig>().DatabaseMappings.TryGetValue(mappingKey.ToString(), out databaseMappingsElement))
            throw new ArgumentException("Unkown DB mapping.");
          clrType = databaseMappingsElement.ClrType;
        }
      }
      string fieldVirtualPath = field.Definition.FieldVirtualPath;
      if (!string.IsNullOrEmpty(fieldVirtualPath))
      {
        control = (Control) ControlUtilities.LoadControl(fieldVirtualPath);
        if (!typeof (IField).IsAssignableFrom(control.GetType()))
          throw new InvalidOperationException(string.Format("The control of type '{0}' does not implement IField interface. All fields must implement IField interface.", (object) control.GetType().FullName));
        field.Definition.FieldType = (string) null;
      }
      Type fieldControlType = (Type) null;
      if (control == null)
        fieldControlType = TypeResolutionService.ResolveType(field.Definition.FieldType);
      foreach (DetailFormViewElement view in this.Views)
      {
        FieldDefinitionElement definitionElement1 = (FieldDefinitionElement) null;
        string sectionName = field.Definition.SectionName;
        if (mappingKey == UserFriendlyDataType.RelatedMedia)
          sectionName = CustomFieldsContext.RelatedMediaSectionName;
        if (mappingKey == UserFriendlyDataType.RelatedData)
          sectionName = CustomFieldsContext.RelatedDataSectionName;
        ContentViewSectionElement section = !SocialMediaSeoTagHelpers.IsSocialMediaSeoField(field.Name) ? CustomFieldsContext.GetSection(view, sectionName, contentTypeName) : this.GetMetadataFieldsSection(field.Name, view, contentTypeName);
        if (section != null)
        {
          this.TryHandleRelatedMappingKey(field.Name, mappingKey, section);
          bool isNew = true;
          if (section.Fields.TryGetValue(field.Name, out definitionElement1))
          {
            if (fieldControlType != (Type) null)
              field.Definition.FieldType = definitionElement1.FieldType.FullName;
            if (definitionElement1 is TaxonFieldDefinitionElement)
            {
              field.Definition.TaxonomyId = ((TaxonFieldDefinitionElement) definitionElement1).TaxonomyId.ToString();
              field.Definition.AllowMultipleSelection = true;
            }
            isNew = false;
          }
          string str1 = (string) null;
          string str2 = (string) null;
          string str3 = string.Empty;
          if (definitionElement1 != null)
          {
            str3 = definitionElement1.ResourceClassId;
            if (!str3.IsNullOrEmpty())
            {
              str1 = definitionElement1.Title;
              str2 = definitionElement1.Example;
              if (!field.Definition.ResourceClassId.IsNullOrEmpty() && field.Definition.ResourceClassId.Equals(str3) && !field.Definition.ResourcesUpdated)
              {
                field.Definition.ResourcesUpdated = true;
                bool flag1 = false;
                bool flag2 = false;
                if (!str1.IsNullOrEmpty() && Res.Get(str3, str1, SystemManager.CurrentContext.Culture, true, false) != field.Definition.Title)
                  flag1 = true;
                if (!field.Definition.Example.IsNullOrEmpty() && (str2.IsNullOrEmpty() ? (string) null : Res.Get(str3, str2, SystemManager.CurrentContext.Culture, true, false)) != field.Definition.Example)
                  flag2 = true;
                if (flag1 | flag2)
                {
                  field.Definition.ResourceClassId = string.Empty;
                }
                else
                {
                  field.Definition.Title = str1;
                  field.Definition.Example = field.Definition.Example.IsNullOrEmpty() ? (string) null : str2;
                }
              }
            }
          }
          definitionElement1 = customFieldBuilder == null ? this.CreateOrUpdateDynamicDefinitionElement(field, fieldControlType, (ConfigElement) section.Fields, (ConfigElement) definitionElement1, isNew) : customFieldBuilder.CreateOrUpdateDynamicDefinitionElement(field, fieldControlType, (ConfigElement) section.Fields, (ConfigElement) definitionElement1);
          if (isNew && definitionElement1 is TaxonFieldDefinitionElement)
            CustomFieldsContext.CreateOrUpdateResource(str1, str2, str3, definitionElement1, contentTypeName);
          definitionElement1.Hidden = !this.IsFieldVisibleInView(field, view) ? new bool?(true) : new bool?();
          string defaultValue = field.Definition.DefaultValue;
          if (defaultValue != null && definitionElement1 is FieldControlDefinitionElement definitionElement2)
          {
            if (clrType != null)
            {
              object obj = CustomFieldsContext.ResolveDefaultValue(clrType, defaultValue);
              definitionElement2.Value = obj;
            }
            else
              definitionElement2.Value = (object) defaultValue;
          }
          if (isNew)
            section.Fields.Add(definitionElement1);
        }
      }
    }

    private void TryHandleRelatedMappingKey(
      string fieldName,
      UserFriendlyDataType mappingKey,
      ContentViewSectionElement section)
    {
      if (SocialMediaSeoTagHelpers.IsSocialMediaSeoField(fieldName))
        return;
      if (mappingKey == UserFriendlyDataType.RelatedMedia)
      {
        section.Title = Res.Get<ModuleEditorResources>().RelatedMediaSectionTitle;
        section.CssClass = "sfExpandableForm";
      }
      if (mappingKey != UserFriendlyDataType.RelatedData)
        return;
      section.Title = Res.Get<ModuleEditorResources>().RelatedDataSectionTitle;
      section.CssClass = "sfExpandableForm";
    }

    /// <summary>
    /// Creates or update resource with specified key for title or example when resource class is specified.
    /// The method fixes the issue 103384: when resource class is specified, attempt to retrieve the resource with specified resource key is made.
    /// Since the resource is not found an exception is thrown.
    /// </summary>
    /// <param name="titleKey">The resource key of title.</param>
    /// <param name="exampleKey">The resource key of example.</param>
    /// <param name="resourceClassId">The resource class.</param>
    /// <param name="fieldDefinitionElement">Definition element.</param>
    /// <param name="contentType">The content type</param>
    /// <remarks>
    /// The logic of this method goes as follows:
    /// If specified key exists at resources update the resource value, else adds the resource with key 'ContentType_FieldName_Property' (e.g. 'News_Tags_Example') and value.
    /// At definition element property (title or example) the specified resource key is updated.
    /// </remarks>
    public static void CreateOrUpdateResource(
      string titleKey,
      string exampleKey,
      string resourceClassId,
      FieldDefinitionElement fieldDefinitionElement,
      string contentType)
    {
      if (resourceClassId.IsNullOrEmpty())
        return;
      string title = fieldDefinitionElement.Title;
      string example = fieldDefinitionElement.Example;
      if (!title.IsNullOrEmpty())
        titleKey = CustomFieldsContext.BuildResourceKey("Title", fieldDefinitionElement.FieldName, contentType);
      if (!example.IsNullOrEmpty())
        exampleKey = CustomFieldsContext.BuildResourceKey("Example", fieldDefinitionElement.FieldName, contentType);
      if (titleKey.IsNullOrEmpty() && exampleKey.IsNullOrEmpty())
        return;
      ResourceManager manager = Res.GetManager();
      if (!titleKey.IsNullOrEmpty())
      {
        CustomFieldsContext.AddResourceEntry(manager, titleKey, title, resourceClassId);
        fieldDefinitionElement.Title = titleKey;
      }
      if (exampleKey.IsNullOrEmpty())
        return;
      CustomFieldsContext.AddResourceEntry(manager, exampleKey, example, resourceClassId);
      fieldDefinitionElement.Example = exampleKey;
    }

    public static void AddResourceEntry(
      ResourceManager manager,
      string key,
      string value,
      string resourceClassId)
    {
      if (AppSettings.CurrentSettings.IsBackendMultilingual)
        CustomFieldsContext.AddResourceEntry(manager, key, value, resourceClassId, SystemManager.CurrentContext.Culture);
      CustomFieldsContext.AddResourceEntry(manager, key, value, resourceClassId, CultureInfo.InvariantCulture);
    }

    private static void AddResourceEntry(
      ResourceManager manager,
      string key,
      string value,
      string resourceClassId,
      CultureInfo culture)
    {
      ResourceEntry resourceEntry = manager.GetResources(culture, resourceClassId).Where<ResourceEntry>("Key = \"" + key + "\"").SingleOrDefault<ResourceEntry>();
      bool flag;
      if (resourceEntry == null)
      {
        resourceEntry = manager.AddItem(culture, resourceClassId, key, string.Empty, string.Empty);
        flag = true;
      }
      else
        flag = resourceEntry.Value != value;
      if (!flag)
        return;
      resourceEntry.Value = value;
      try
      {
        manager.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().WCFErrorOnSave, ex.InnerException);
      }
    }

    public static string BuildResourceKey(string propetyName, string fieldName, string contentType) => "{0}_{1}_{2}".Arrange((object) contentType, (object) fieldName, (object) propetyName);

    private bool IsFieldVisibleInView(WcfField field, DetailFormViewElement view)
    {
      if (field.Definition.VisibleViews == null || ((IEnumerable<string>) field.Definition.VisibleViews).Count<string>() == 0)
        return !field.Definition.Hidden;
      string str = view.ControlDefinitionName + " > " + view.ViewName;
      return ((IEnumerable<string>) field.Definition.VisibleViews).Contains<string>(str);
    }

    /// <summary>Removes the custom fields.</summary>
    /// <param name="fields">The fields.</param>
    public void RemoveCustomFields(IList<string> fields, string contentTypeName)
    {
      foreach (string field in (IEnumerable<string>) fields)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        CustomFieldsContext.\u003C\u003Ec__DisplayClass20_1 cDisplayClass201 = new CustomFieldsContext.\u003C\u003Ec__DisplayClass20_1()
        {
          CS\u0024\u003C\u003E8__locals1 = new CustomFieldsContext.\u003C\u003Ec__DisplayClass20_0()
          {
            \u003C\u003E4__this = this,
            fieldName = field
          }
        };
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        cDisplayClass201.metaField = this.MetaType.Fields.SingleOrDefault<MetaField>(new Func<MetaField, bool>(cDisplayClass201.CS\u0024\u003C\u003E8__locals1.\u003CRemoveCustomFields\u003Eb__0));
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass201.metaField != null)
        {
          // ISSUE: reference to a compiler-generated field
          cDisplayClass201.metaField.IsDeleted = true;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          RelatedDataHelper.DeleteFieldRelations(this.metadataManager.Provider.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager, this.GetActualType().FullName, cDisplayClass201.CS\u0024\u003C\u003E8__locals1.fieldName);
          TaxonomyManager relatedManager = this.metadataManager.Provider.GetRelatedManager<TaxonomyManager>(string.Empty);
          IQueryable<TaxonomyStatistic> statistics = relatedManager.GetStatistics();
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          // ISSUE: method reference
          // ISSUE: method reference
          Expression<Func<TaxonomyStatistic, bool>> predicate = Expression.Lambda<Func<TaxonomyStatistic, bool>>((Expression) Expression.AndAlso((Expression) Expression.Equal(s.DataItemType, (Expression) Expression.Property((Expression) Expression.Call(this, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CustomFieldsContext.GetActualType)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Type.get_FullName)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaxonomyStatistic.get_TaxonomyId))), (Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass201, typeof (CustomFieldsContext.\u003C\u003Ec__DisplayClass20_1)), FieldInfo.GetFieldFromHandle(__fieldref (CustomFieldsContext.\u003C\u003Ec__DisplayClass20_1.metaField))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MetaField.get_TaxonomyId))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality)))), parameterExpression);
          foreach (TaxonomyStatistic statistic in (IEnumerable<TaxonomyStatistic>) statistics.Where<TaxonomyStatistic>(predicate))
            relatedManager.DeleteStatistic(statistic);
        }
        foreach (DetailFormViewElement view in this.Views)
        {
          try
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated method
            ContentViewSectionElement viewSectionElement = view.Sections.Values.FirstOrDefault<ContentViewSectionElement>(cDisplayClass201.CS\u0024\u003C\u003E8__locals1.\u003C\u003E9__2 ?? (cDisplayClass201.CS\u0024\u003C\u003E8__locals1.\u003C\u003E9__2 = new Func<ContentViewSectionElement, bool>(cDisplayClass201.CS\u0024\u003C\u003E8__locals1.\u003CRemoveCustomFields\u003Eb__2)));
            if (viewSectionElement != null)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              viewSectionElement.Fields.Remove(cDisplayClass201.CS\u0024\u003C\u003E8__locals1.fieldName);
              if (viewSectionElement.Fields.Count == 0)
                ((ConfigElementCollection) viewSectionElement.Parent).Remove((ConfigElement) viewSectionElement);
            }
          }
          catch (Exception ex)
          {
            if (Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions))
              throw;
          }
        }
      }
    }

    /// <summary>Saves the changes.</summary>
    /// <returns>Returns true if the application needs to be restarted</returns>
    public bool SaveChanges()
    {
      bool flag = false;
      foreach (ConfigSection section in this.configs.Values)
        this.configManager.SaveSection(section);
      if (this.metadataManager != null)
      {
        if (!this.metadataManager.TransactionName.IsNullOrEmpty())
          TransactionManager.CommitTransaction(this.metadataManager.TransactionName);
        else
          this.metadataManager.SaveChanges();
        this.metadataManager = (MetadataManager) null;
        flag = true;
      }
      return flag;
    }

    /// <summary>Gets a collection of detail views.</summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    public static List<DetailFormViewElement> GetViews(
      string contentTypeFullName)
    {
      List<DetailFormViewElement> views1 = new List<DetailFormViewElement>();
      ContentViewConfig contentViewConfig = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>();
      if (contentTypeFullName.Contains(".sf_ec_prdct_"))
      {
        string str1 = "edit_" + contentTypeFullName.Substring(contentTypeFullName.LastIndexOf('.') + 1);
        string str2 = "insert_" + contentTypeFullName.Substring(contentTypeFullName.LastIndexOf('.') + 1);
        foreach (ContentViewControlElement viewControlElement in contentViewConfig.ContentViewControls.Values.Where<ContentViewControlElement>((Func<ContentViewControlElement, bool>) (prodDef => prodDef.ContentType != (Type) null && prodDef.ContentType.Name == "Product")))
        {
          foreach (ContentViewDefinitionElement definitionElement in (IEnumerable<ContentViewDefinitionElement>) viewControlElement.ViewsConfig.Values)
          {
            if (!definitionElement.IsMasterView && (definitionElement.ViewName == str1 || definitionElement.ViewName == str2))
              views1.Add((DetailFormViewElement) definitionElement);
          }
        }
        return views1;
      }
      foreach (ContentViewControlElement viewControlElement in (IEnumerable<ContentViewControlElement>) contentViewConfig.ContentViewControls.Values)
      {
        if (viewControlElement.ContentType != (Type) null && viewControlElement.ContentType.ToString().Equals(contentTypeFullName))
        {
          IList<DetailFormViewElement> views2 = CustomFieldsContext.GetViews(viewControlElement.ViewsConfig.Values);
          views1.AddRange((IEnumerable<DetailFormViewElement>) views2);
        }
      }
      return views1;
    }

    /// <summary>Gets the section for the specified view.</summary>
    /// <param name="detailsFormView">The details form view.</param>
    /// <returns></returns>
    public static ContentViewSectionElement GetSection(
      DetailFormViewElement detailsFormView,
      string sectionName,
      string contentTypeName,
      string sectionTitle = null)
    {
      if (string.IsNullOrEmpty(sectionName))
        sectionName = !contentTypeName.StartsWith("sf_ec_prdct_") ? CustomFieldsContext.CustomFieldsSectionName : CustomFieldsContext.ProductsMainSectionName;
      ContentViewSectionElement element;
      if (!detailsFormView.Sections.TryGetValue(sectionName, out element))
      {
        element = new ContentViewSectionElement((ConfigElement) detailsFormView.Sections)
        {
          Name = sectionName,
          ExpandableDefinitionConfig = {
            Expanded = new bool?(true)
          },
          Title = sectionTitle
        };
        element.DisplayMode = new FieldDisplayMode?(detailsFormView.DisplayMode);
        detailsFormView.Sections.Add(element);
      }
      return element;
    }

    private ContentViewSectionElement GetMetadataFieldsSection(
      string fieldName,
      DetailFormViewElement view,
      string contentTypeName)
    {
      string sectionName = Res.Get<ModuleEditorResources>().SEOSectionName;
      string sectionTitle = Res.Get<ModuleEditorResources>().SEOSectionTitle;
      if (SocialMediaSeoTagHelpers.IsSocialMediaTagField(fieldName))
      {
        sectionName = Res.Get<ModuleEditorResources>().SocialMediaSectionName;
        sectionTitle = Res.Get<ModuleEditorResources>().SocialMediaSectionTitle;
      }
      ContentViewSectionElement section = CustomFieldsContext.GetSection(view, sectionName, contentTypeName, sectionTitle);
      section.ExpandableDefinition.Expanded = new bool?(true);
      section.CssClass = "sfExpandableForm";
      return section;
    }

    private FieldDefinitionElement CreateOrUpdateDynamicDefinitionElement(
      WcfField field,
      Type fieldControlType,
      ConfigElement parent,
      ConfigElement instance,
      bool isNew)
    {
      FieldDefinitionElement definitionElement = DefinitionBuilder.CreateOrUpdateDefinitionElement(fieldControlType, (object) field.Definition, parent, true, instance) as FieldDefinitionElement;
      object obj1 = (object) new DynamicBindingProxy((object) definitionElement);
      if (fieldControlType != (Type) null)
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Type, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "FieldType", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__0, obj1, fieldControlType);
      }
      UserFriendlyDataType mappingKey = this.GetMappingKey(field);
      bool flag = this.GetActualType() == typeof (PageNode);
      if (field.IsCustom && (mappingKey == UserFriendlyDataType.ShortText || mappingKey == UserFriendlyDataType.LongText))
      {
        if (field.Definition.IsLocalizable)
        {
          if (flag)
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "DataFieldName", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj3 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__1.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__1, obj1, "CustomFields." + field.Definition.FieldName + ".PersistedValue");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__2 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "DataFieldName", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj4 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__2.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__2, obj1, field.Definition.FieldName + ".PersistedValue");
          }
        }
        else if (flag)
        {
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "DataFieldName", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj5 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__3.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__3, obj1, "CustomFields." + field.Definition.FieldName);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "DataFieldName", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj6 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__4.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__4, obj1, field.Definition.FieldName);
        }
        if (mappingKey == UserFriendlyDataType.LongText && fieldControlType == typeof (TextField))
        {
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Rows", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj7 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__5.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__5, obj1, 5);
        }
      }
      else if (isNew)
      {
        if (flag)
        {
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "DataFieldName", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj8 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__6.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__6, obj1, "CustomFields." + field.Definition.FieldName);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "DataFieldName", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj9 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__7.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__7, obj1, field.Definition.FieldName);
        }
      }
      if (field.IsCustom && mappingKey == UserFriendlyDataType.Number)
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "CssClass", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__8.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__8, obj1, "sfNumberField");
      }
      if (!field.Definition.DefaultValue.IsNullOrEmpty())
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj11 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__9.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__9, obj1, field.Definition.DefaultValue);
      }
      if (fieldControlType == typeof (HierarchicalTaxonField))
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "AllowMultipleSelection", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj12 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__10.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__10, obj1, field.Definition.AllowMultipleSelection);
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, Guid, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "TaxonomyId", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj13 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__11.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__11, obj1, field.Definition.Taxonomy.Id);
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "WebServiceUrl", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj14 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__12.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__12, obj1, "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc");
      }
      else if (fieldControlType == typeof (FlatTaxonField))
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, Guid, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "TaxonomyId", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj15 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__13.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__13, obj1, field.Definition.Taxonomy.Id);
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "WebServiceUrl", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj16 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__14.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__14, obj1, "~/Sitefinity/Services/Taxonomies/FlatTaxon.svc");
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "AllowMultipleSelection", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj17 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__15.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__15, obj1, field.Definition.AllowMultipleSelection);
      }
      else if (!(fieldControlType == typeof (HtmlField)))
      {
        if (fieldControlType == typeof (ChoiceField))
        {
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__18 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, bool> target1 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__18.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, bool>> p18 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__18;
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__17 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, RenderChoicesAs, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, RenderChoicesAs, object> target2 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__17.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, RenderChoicesAs, object>> p17 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__17;
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__16 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "RenderChoiceAs", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj18 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__16.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__16, obj1);
          object obj19 = target2((CallSite) p17, obj18, RenderChoicesAs.SingleCheckBox);
          if (target1((CallSite) p18, obj19))
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__19 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__19 = CallSite<Func<CallSite, object, RenderChoicesAs, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "RenderChoiceAs", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj20 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__19.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__19, obj1, RenderChoicesAs.RadioButtons);
          }
          if (field.Definition.SortAlphabetically)
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__21 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__21 = CallSite<Func<CallSite, object, IList<ChoiceElement>>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (IList<ChoiceElement>), typeof (CustomFieldsContext)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, IList<ChoiceElement>> target3 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__21.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, IList<ChoiceElement>>> p21 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__21;
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__20 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__20 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ChoicesConfig", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj21 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__20.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__20, obj1);
            IList<ChoiceElement> choices = target3((CallSite) p21, obj21);
            List<ChoiceElement> list = choices.OrderBy<ChoiceElement, string>((Func<ChoiceElement, string>) (c => c.Text)).ToList<ChoiceElement>();
            choices.Clear();
            Action<ChoiceElement> action = (Action<ChoiceElement>) (c => choices.Add(c));
            list.ForEach(action);
          }
        }
        else if (typeof (RelatedMediaField).IsAssignableFrom(fieldControlType))
        {
          string lower = field.Definition.MediaType.ToLower();
          if (!(lower == "image"))
          {
            if (!(lower == "video"))
            {
              if (lower == "file")
              {
                if (field.Definition.AllowMultipleFiles)
                {
                  // ISSUE: reference to a compiler-generated field
                  if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__26 == null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__26 = CallSite<Func<CallSite, object, AssetsWorkMode, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "WorkMode", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                    {
                      CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                      CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
                    }));
                  }
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  object obj22 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__26.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__26, obj1, AssetsWorkMode.MultipleDocuments);
                }
                else
                {
                  // ISSUE: reference to a compiler-generated field
                  if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__27 == null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__27 = CallSite<Func<CallSite, object, AssetsWorkMode, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "WorkMode", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                    {
                      CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                      CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
                    }));
                  }
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  object obj23 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__27.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__27, obj1, AssetsWorkMode.SingleDocument);
                }
              }
            }
            else if (field.Definition.AllowMultipleVideos)
            {
              // ISSUE: reference to a compiler-generated field
              if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__24 == null)
              {
                // ISSUE: reference to a compiler-generated field
                CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__24 = CallSite<Func<CallSite, object, AssetsWorkMode, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "WorkMode", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj24 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__24.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__24, obj1, AssetsWorkMode.MultipleVideos);
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__25 == null)
              {
                // ISSUE: reference to a compiler-generated field
                CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__25 = CallSite<Func<CallSite, object, AssetsWorkMode, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "WorkMode", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj25 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__25.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__25, obj1, AssetsWorkMode.SingleVideo);
            }
          }
          else if (field.Definition.AllowMultipleImages)
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__22 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__22 = CallSite<Func<CallSite, object, AssetsWorkMode, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "WorkMode", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj26 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__22.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__22, obj1, AssetsWorkMode.MultipleImages);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__23 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__23 = CallSite<Func<CallSite, object, AssetsWorkMode, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "WorkMode", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj27 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__23.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__23, obj1, AssetsWorkMode.SingleImage);
          }
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__28 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__28 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "AllowMultipleImages", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj28 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__28.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__28, obj1, field.Definition.AllowMultipleImages);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__29 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__29 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "AllowMultipleVideos", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj29 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__29.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__29, obj1, field.Definition.AllowMultipleVideos);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__30 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__30 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "AllowMultipleFiles", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj30 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__30.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__30, obj1, field.Definition.AllowMultipleFiles);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__31 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__31 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "AllowedExtensions", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj31 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__31.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__31, obj1, field.Definition.FileExtensions);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__32 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__32 = CallSite<Func<CallSite, object, int?, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "MaxFileSize", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, int?, object> target = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__32.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, int?, object>> p32 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__32;
          object obj32 = obj1;
          int? maxFileSize = field.Definition.MaxFileSize;
          int? nullable = maxFileSize.HasValue ? new int?(maxFileSize.GetValueOrDefault() * 1024 * 1024) : new int?();
          object obj33 = target((CallSite) p32, obj32, nullable);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__33 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__33 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Description", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj34 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__33.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__33, obj1, field.Definition.Description);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__34 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__34 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "FrontendWidgetLabel", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj35 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__34.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__34, obj1, field.Definition.FrontendWidgetLabel);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__35 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__35 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "CssClass", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj36 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__35.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__35, obj1, "sfFormSeparator");
          if (field.Definition.FrontendWidgetTypeName.StartsWith("~/") || "inline".Equals(field.Definition.FrontendWidgetTypeName))
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__36 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__36 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "FrontendWidgetVirtualPath", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj37 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__36.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__36, obj1, field.Definition.FrontendWidgetTypeName);
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__37 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__37 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "FrontendWidgetType", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj38 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__37.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__37, obj1, (object) null);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__38 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__38 = CallSite<Func<CallSite, object, Type, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "FrontendWidgetType", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj39 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__38.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__38, obj1, TypeResolutionService.ResolveType(field.Definition.FrontendWidgetTypeName));
          }
        }
        else if (typeof (RelatedDataField).IsAssignableFrom(fieldControlType))
        {
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__39 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__39 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "AllowMultipleSelection", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj40 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__39.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__39, obj1, field.Definition.AllowMultipleSelection);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__40 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__40 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Description", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj41 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__40.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__40, obj1, field.Definition.Description);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__41 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__41 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "FrontendWidgetLabel", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj42 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__41.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__41, obj1, field.Definition.FrontendWidgetLabel);
          // ISSUE: reference to a compiler-generated field
          if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__42 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__42 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "CssClass", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj43 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__42.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__42, obj1, "sfFormSeparator");
          if (field.Definition.FrontendWidgetTypeName.StartsWith("~/") || "inline".Equals(field.Definition.FrontendWidgetTypeName))
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__43 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__43 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "FrontendWidgetVirtualPath", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj44 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__43.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__43, obj1, field.Definition.FrontendWidgetTypeName);
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__44 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__44 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "FrontendWidgetType", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj45 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__44.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__44, obj1, (object) null);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__45 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__45 = CallSite<Func<CallSite, object, Type, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "FrontendWidgetType", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj46 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__45.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__45, obj1, TypeResolutionService.ResolveType(field.Definition.FrontendWidgetTypeName));
          }
        }
      }
      // ISSUE: reference to a compiler-generated field
      if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__49 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__49 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target4 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__49.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p49 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__49;
      object obj47;
      if (isNew)
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__48 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__48 = CallSite<Func<CallSite, bool, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, bool, object, object> target5 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__48.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, bool, object, object>> p48 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__48;
        int num = isNew ? 1 : 0;
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__47 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__47 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target6 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__47.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p47 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__47;
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__46 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__46 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ValidatorConfig", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj48 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__46.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__46, obj1);
        object obj49 = target6((CallSite) p47, obj48, (object) null);
        obj47 = target5((CallSite) p48, num != 0, obj49);
      }
      else
        obj47 = (object) isNew;
      if (target4((CallSite) p49, obj47))
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__51 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__51 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "MessageCssClass", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string, object> target7 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__51.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string, object>> p51 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__51;
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__50 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__50 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ValidatorConfig", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj50 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__50.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__50, obj1);
        object obj51 = target7((CallSite) p51, obj50, "sfError");
      }
      if (field.Definition.ValidatorDefinition != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__52 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__52 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "AllowNulls", typeof (CustomFieldsContext), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj52 = CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__52.Target((CallSite) CustomFieldsContext.\u003C\u003Eo__25.\u003C\u003Ep__52, obj1, !field.Definition.ValidatorDefinition.Required.HasValue || !field.Definition.ValidatorDefinition.Required.Value);
      }
      return definitionElement;
    }

    private MetaType MetaType
    {
      get
      {
        if (this.metaType == null)
        {
          this.metaType = this.metadataManager.GetMetaType(this.GetActualType());
          if (this.metaType == null)
            this.metaType = this.metadataManager.CreateMetaType(this.GetActualType());
        }
        return this.metaType;
      }
    }

    /// <summary>
    /// Gets a list of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DetailFormViewElement" />
    /// </summary>
    public virtual List<DetailFormViewElement> Views
    {
      get
      {
        if (this.views == null)
        {
          this.views = new List<DetailFormViewElement>();
          CustomFieldsContext.GetViews(this.contentTypeFullName).ForEach((Action<DetailFormViewElement>) (v => this.AddView(this.views, v)));
        }
        return this.views;
      }
    }

    /// <summary>Adds a view</summary>
    /// <param name="viewsList">The view list.</param>
    /// <param name="detailsFormView">The details form view.</param>
    public void AddView(
      List<DetailFormViewElement> viewsList,
      DetailFormViewElement detailsFormView)
    {
      viewsList.Add(detailsFormView);
      ConfigSection section = detailsFormView.Section;
      if (this.configs.ContainsKey(section.TagName))
        return;
      this.configs.Add(section.TagName, section);
    }

    /// <summary>Gets a list of views</summary>
    /// <param name="viewDefinitionElements">The view definition elements.</param>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DetailFormViewElement" /></returns>
    public static IList<DetailFormViewElement> GetViews(
      ICollection<ContentViewDefinitionElement> viewDefinitionElements)
    {
      List<DetailFormViewElement> views = new List<DetailFormViewElement>();
      foreach (ContentViewDefinitionElement definitionElement in (IEnumerable<ContentViewDefinitionElement>) viewDefinitionElements)
      {
        if (definitionElement is DetailFormViewElement detailFormViewElement && !detailFormViewElement.IsMasterView)
          views.Add(detailFormViewElement);
      }
      return (IList<DetailFormViewElement>) views;
    }

    /// <summary>Parses a string to nullable date time.</summary>
    /// <param name="s">The string to parse.</param>
    /// <returns>A date time.</returns>
    public static DateTime? ParseToNullableDateTime(string s)
    {
      DateTime result;
      return DateTime.TryParse(s, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result) ? new DateTime?(result) : new DateTime?();
    }

    private Type GetActualType() => this.contentType;

    /// <summary>
    /// Modifies the meta type in order to ensure that the model will be updated
    /// </summary>
    private void MarkMetaTypeDirty()
    {
      string className = this.MetaType.ClassName;
      this.MetaType.ClassName = string.Empty;
      this.MetaType.ClassName = className;
    }

    private void UpdateMetaFieldProperties(
      WcfField field,
      MetaField metaField,
      UserFriendlyDataType type)
    {
      if (field.Definition == null || field.Definition.ValidatorDefinition == null)
        return;
      metaField.Required = ((int) field.Definition.ValidatorDefinition.Required ?? 0) != 0;
      metaField.RecommendedCharactersCount = field.Definition.ValidatorDefinition.RecommendedCharactersCount;
      metaField.MinValue = (string) null;
      metaField.MaxValue = (string) null;
      if (type == UserFriendlyDataType.ShortText || type == UserFriendlyDataType.LongText || type == UserFriendlyDataType.Number)
      {
        int num;
        if (field.Definition.ValidatorDefinition.MinLength != 0)
        {
          MetaField metaField1 = metaField;
          num = field.Definition.ValidatorDefinition.MinLength;
          string str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          metaField1.MinValue = str;
        }
        if (field.Definition.ValidatorDefinition.MaxLength != 0)
        {
          MetaField metaField2 = metaField;
          num = field.Definition.ValidatorDefinition.MaxLength;
          string str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          metaField2.MaxValue = str;
        }
      }
      if (type != UserFriendlyDataType.Number)
        return;
      if (field.Definition.ValidatorDefinition.MinValue != null)
        metaField.MinValue = field.Definition.ValidatorDefinition.MinValue.ToString();
      if (field.Definition.ValidatorDefinition.MaxValue == null)
        return;
      metaField.MaxValue = field.Definition.ValidatorDefinition.MaxValue.ToString();
    }

    private void UpdateMetaField(WcfField field, MetaField metaField, UserFriendlyDataType type)
    {
      this.UpdateMetaFieldProperties(field, metaField, type);
      switch (type)
      {
        case UserFriendlyDataType.ShortText:
        case UserFriendlyDataType.LongText:
          if (metaField.ClrType == typeof (Lstring).FullName == field.Definition.IsLocalizable)
            break;
          metaField.ClrType = field.Definition.IsLocalizable ? typeof (Lstring).FullName : typeof (string).FullName;
          this.MarkMetaTypeDirty();
          break;
        case UserFriendlyDataType.MultipleChoice:
        case UserFriendlyDataType.Choices:
          metaField.ChoiceFieldDefinition = field.Definition.Choices;
          this.MarkMetaTypeDirty();
          break;
        case UserFriendlyDataType.DateAndTime:
        case UserFriendlyDataType.Number:
          metaField.IsLocalizable = field.Definition.IsLocalizable;
          this.MarkMetaTypeDirty();
          break;
        case UserFriendlyDataType.RelatedMedia:
          bool isMaster = false;
          string mediaType = field.Definition.MediaType;
          if (!(mediaType == "image"))
          {
            if (!(mediaType == "video"))
            {
              if (mediaType == "file")
              {
                field.Definition.RelatedDataType = typeof (Document).FullName;
                isMaster = field.Definition.AllowMultipleFiles;
              }
            }
            else
            {
              field.Definition.RelatedDataType = typeof (Video).FullName;
              isMaster = field.Definition.AllowMultipleVideos;
            }
          }
          else
          {
            field.Definition.RelatedDataType = typeof (Image).FullName;
            isMaster = field.Definition.AllowMultipleImages;
          }
          metaField.Description = field.Definition.Description;
          metaField.AllowMultipleRelations = isMaster;
          MetaField metaField1 = metaField;
          int num1;
          if (!isMaster)
          {
            bool? required = field.Definition.ValidatorDefinition.Required;
            if (!required.HasValue)
            {
              num1 = 0;
            }
            else
            {
              required = field.Definition.ValidatorDefinition.Required;
              num1 = required.Value ? 1 : 0;
            }
          }
          else
            num1 = 0;
          metaField1.IsProtectedRelation = num1 != 0;
          this.UpdateRelatedProviderAttribute(field, metaField);
          this.UpdateControlTagAttribute(field, metaField, isMaster);
          break;
        case UserFriendlyDataType.RelatedData:
          metaField.Description = field.Definition.Description;
          metaField.AllowMultipleRelations = field.Definition.AllowMultipleSelection;
          MetaField metaField2 = metaField;
          int num2;
          if (!field.Definition.AllowMultipleSelection)
          {
            bool? required = field.Definition.ValidatorDefinition.Required;
            if (!required.HasValue)
            {
              num2 = 0;
            }
            else
            {
              required = field.Definition.ValidatorDefinition.Required;
              num2 = required.Value ? 1 : 0;
            }
          }
          else
            num2 = 0;
          metaField2.IsProtectedRelation = num2 != 0;
          this.UpdateRelatedProviderAttribute(field, metaField);
          this.UpdateControlTagAttribute(field, metaField, field.Definition.AllowMultipleSelection);
          break;
      }
    }

    private void UpdateControlTagAttribute(WcfField field, MetaField metaField, bool isMaster)
    {
      MetaFieldAttribute metaFieldAttribute1 = metaField.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => a.Name == DynamicAttributeNames.ControlTag));
      bool flag;
      if (metaFieldAttribute1 != null)
      {
        string str = FieldTemplateBuilder.BuildRelatedDataFieldTemplate(field.Definition.FrontendWidgetTypeName, field.Definition.FrontendWidgetLabel, this.MetaType.FullTypeName, field.Definition.RelatedDataType, field.Definition.RelatedDataProvider, field.Name, isMaster);
        flag = str != metaFieldAttribute1.Value;
        metaFieldAttribute1.Value = str;
      }
      else
      {
        MetaFieldAttribute metaFieldAttribute2 = new MetaFieldAttribute();
        metaFieldAttribute2.Name = DynamicAttributeNames.ControlTag;
        metaFieldAttribute2.Value = FieldTemplateBuilder.BuildRelatedDataFieldTemplate(field.Definition.FrontendWidgetTypeName, field.Definition.FrontendWidgetLabel, this.MetaType.FullTypeName, field.Definition.RelatedDataType, field.Definition.RelatedDataProvider, field.Name, isMaster);
        MetaFieldAttribute metaFieldAttribute3 = metaFieldAttribute2;
        metaField.MetaAttributes.Add(metaFieldAttribute3);
        flag = true;
      }
      if (!flag)
        return;
      this.MarkMetaTypeDirty();
    }

    private void UpdateRelatedProviderAttribute(WcfField field, MetaField metaField)
    {
      MetaFieldAttribute metaFieldAttribute1 = metaField.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => a.Name == DynamicAttributeNames.RelatedProviders));
      bool flag;
      if (metaFieldAttribute1 != null)
      {
        flag = field.Definition.RelatedDataProvider != metaFieldAttribute1.Value;
        metaFieldAttribute1.Value = field.Definition.RelatedDataProvider;
      }
      else
      {
        MetaFieldAttribute metaFieldAttribute2 = new MetaFieldAttribute();
        metaFieldAttribute2.Name = DynamicAttributeNames.RelatedProviders;
        metaFieldAttribute2.Value = field.Definition.RelatedDataProvider;
        MetaFieldAttribute metaFieldAttribute3 = metaFieldAttribute2;
        metaField.MetaAttributes.Add(metaFieldAttribute3);
        flag = true;
      }
      if (!flag)
        return;
      this.MarkMetaTypeDirty();
    }
  }
}
