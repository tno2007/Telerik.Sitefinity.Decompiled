// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.DataStructureInstaller
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.ModuleEditor.WidgetTemplates;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Taxonomies.Configuration;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  /// <summary>
  /// <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Install.DataStructureInstaller" /> class provides functionality for creating data structure
  /// of the module, which includes dynamic types and fields.
  /// </summary>
  internal class DataStructureInstaller
  {
    private readonly IMetadataManager metadataManager;
    private readonly ModuleBuilderManager moduleBuilderManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Install.DataStructureInstaller" /> class with specified metadata manager.
    /// </summary>
    /// <param name="metadataManager">
    /// Instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.IMetadataManager" /> through which the data structure of the dynamic
    /// module will be created.
    /// </param>
    /// <param name="moduleBuilderManager">
    /// Instance of the <see cref="!:IModuleBuilderManager" /> through which the information about dynamic
    /// module's data structure can be discovered.
    /// </param>
    public DataStructureInstaller(
      IMetadataManager metadataManager,
      ModuleBuilderManager moduleBuilderManager)
    {
      if (metadataManager == null)
        throw new ArgumentNullException(nameof (metadataManager));
      if (moduleBuilderManager == null)
        throw new ArgumentNullException(nameof (moduleBuilderManager));
      this.metadataManager = metadataManager;
      this.moduleBuilderManager = moduleBuilderManager;
    }

    /// <summary>
    /// Creates the data structure for the specified <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="module">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> which contains the information about the data structure
    /// that should be created.
    /// </param>
    /// <returns>
    /// Returns the collection of <see cref="!:MetaTypes" /> that represent the modules data structure.
    /// </returns>
    public IList<MetaType> CreateDataStructure(DynamicModule module)
    {
      if (module == null)
        throw new ArgumentNullException(nameof (module));
      this.moduleBuilderManager.LoadDynamicModuleGraph(module);
      if (module.Types == null || module.Types.Length == 0)
        return (IList<MetaType>) null;
      List<MetaType> dataStructure1 = new List<MetaType>();
      foreach (DynamicModuleType type in module.Types)
      {
        MetaType dataStructure2 = this.CreateDataStructure(type);
        dataStructure1.Add(dataStructure2);
      }
      return (IList<MetaType>) dataStructure1;
    }

    /// <summary>
    /// Creates the data structure for the specified <see cref="!:DinamicModuleType" />.
    /// </summary>
    /// <param name="moduleType">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> which contains the information about the data structure
    /// that should be created.
    /// </param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> that represent the content type structure.</returns>
    public MetaType CreateDataStructure(DynamicModuleType moduleType)
    {
      MetaType dataStructure = moduleType != null ? this.CreateMetaType(moduleType) : throw new ArgumentNullException(nameof (moduleType));
      dataStructure.ParentTypeId = moduleType.ParentModuleTypeId;
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        if (!field.IsTransient)
        {
          MetaField metaField = this.CreateMetaField(field);
          if (moduleType.MainShortTextFieldName == metaField.FieldName)
            MetadataManager.SetMetaFieldFlags(metaField, MetaFieldFlags.PersisteInMainTable);
          dataStructure.Fields.Add(metaField);
        }
      }
      return dataStructure;
    }

    /// <summary>
    /// Updates the data structure for the specified <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="module">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> which contains the information about the data structure
    /// that should be updated.</param>
    /// <param name="moduleType">Type of the module.</param>
    /// <returns>
    /// Returns the collection of <see cref="!:MetaTypes" /> that represent the modules data structure.
    /// </returns>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    public IList<MetaType> UpdateDataStructure(
      DynamicModule module,
      DynamicModuleType moduleType)
    {
      if (module == null)
        throw new ArgumentNullException(nameof (module));
      if (module.Types == null || module.Types.Length == 0)
        return (IList<MetaType>) null;
      MetaType metaType = this.GetMetaType(moduleType);
      if (metaType.ParentTypeId != moduleType.ParentModuleTypeId)
        metaType.ParentTypeId = moduleType.ParentModuleTypeId;
      MetaTypeAttribute metaTypeAttribute1 = metaType.MetaAttributes.FirstOrDefault<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => a.Name.Equals("mainPropertyName")));
      if (metaTypeAttribute1 == null)
      {
        MetaTypeAttribute metaTypeAttribute2 = new MetaTypeAttribute();
        metaTypeAttribute2.Name = "mainPropertyName";
        metaTypeAttribute2.Value = moduleType.MainShortTextFieldName;
        metaTypeAttribute1 = metaTypeAttribute2;
        metaType.MetaAttributes.Add(metaTypeAttribute1);
      }
      if (metaTypeAttribute1.Value != moduleType.MainShortTextFieldName)
        metaTypeAttribute1.Value = moduleType.MainShortTextFieldName;
      List<MetaType> metaTypeList = new List<MetaType>();
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        DynamicModuleField moduleField = field;
        if (moduleField.FieldStatus == DynamicModuleFieldStatus.Added)
        {
          MetaField metaField = this.CreateMetaField(moduleField);
          if (moduleType.MainShortTextFieldName == metaField.FieldName)
            MetadataManager.SetMetaFieldFlags(metaField, MetaFieldFlags.PersisteInMainTable);
          metaType.Fields.Add(metaField);
        }
        else if (moduleField.FieldStatus == DynamicModuleFieldStatus.Removed)
        {
          if (moduleField.SpecialType == FieldSpecialType.None)
          {
            MetaField metaField = this.metadataManager.GetMetafields().Where<MetaField>((Expression<Func<MetaField, bool>>) (f => f.Id == moduleField.Id)).SingleOrDefault<MetaField>();
            if (metaField != null)
            {
              metaField.IsDeleted = true;
              if (moduleField.FieldType == FieldType.RelatedMedia || moduleField.FieldType == FieldType.RelatedData)
                RelatedDataHelper.DeleteFieldRelations(this.metadataManager.Provider.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager, moduleType.GetFullTypeName(), moduleField.Name);
            }
          }
        }
        else if (moduleField.SpecialType == FieldSpecialType.None)
        {
          MetaField metaField = metaType.Fields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (f => f.Id == moduleField.Id));
          if (metaField != null)
            this.UpdateMetaField(moduleField, metaField);
        }
      }
      metaTypeList.Add(metaType);
      return (IList<MetaType>) metaTypeList;
    }

    /// <summary>
    /// Deletes the data structure of the specified module - meta types and fields definitions.
    /// </summary>
    /// <param name="module">The module.</param>
    public void DeleteDataStructure(DynamicModule module)
    {
      if (module == null)
        throw new ArgumentNullException(nameof (module));
      this.moduleBuilderManager.LoadDynamicModuleGraph(module);
      if (module.Types == null || module.Types.Length == 0)
        return;
      foreach (DynamicModuleType type in module.Types)
        this.DeleteModuleTypeStructure(type);
    }

    /// <summary>
    /// Deletes the data structure of the specified dynamic module type
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    public void DeleteModuleTypeStructure(DynamicModuleType moduleType)
    {
      MetaType metaType = moduleType != null ? this.GetMetaType(moduleType) : throw new ArgumentNullException("DynamicModuleType cannot be null!");
      if (metaType == null)
        return;
      metaType.IsDeleted = true;
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Dividing switch/case statement will actually worsen the code")]
    internal MetaField CreateMetaField(DynamicModuleField moduleField)
    {
      if (moduleField == null)
        throw new ArgumentNullException(nameof (moduleField));
      MetaField metafield = this.metadataManager.CreateMetafield(moduleField.Name, moduleField.Id);
      switch (moduleField.FieldType)
      {
        case FieldType.Unknown:
          metafield.ClrType = typeof (string).FullName;
          goto case FieldType.Classification;
        case FieldType.ShortText:
        case FieldType.LongText:
          metafield.IsLocalizable = moduleField.IsLocalizable;
          metafield.ClrType = !moduleField.IsLocalizable ? typeof (string).FullName : typeof (Lstring).FullName;
          goto case FieldType.Classification;
        case FieldType.MultipleChoice:
          metafield.ClrType = typeof (string[]).FullName;
          goto case FieldType.Classification;
        case FieldType.YesNo:
          metafield.ClrType = typeof (bool).FullName;
          goto case FieldType.Classification;
        case FieldType.Currency:
          metafield.ClrType = typeof (Decimal).FullName;
          goto case FieldType.Classification;
        case FieldType.DateTime:
          metafield.ClrType = !moduleField.AllowNulls ? typeof (DateTime).FullName : typeof (DateTime?).FullName;
          metafield.MinValue = moduleField.MinNumberRange;
          metafield.MaxValue = moduleField.MaxNumberRange;
          metafield.IsLocalizable = moduleField.IsLocalizable;
          goto case FieldType.Classification;
        case FieldType.Number:
          metafield.ClrType = !moduleField.AllowNulls ? typeof (Decimal).FullName : typeof (Decimal?).FullName;
          metafield.IsLocalizable = moduleField.IsLocalizable;
          metafield.MinValue = moduleField.MinNumberRange;
          metafield.MaxValue = moduleField.MaxNumberRange;
          goto case FieldType.Classification;
        case FieldType.Classification:
          if (moduleField.FieldType == FieldType.Classification)
          {
            metafield.Title = moduleField.Title;
            metafield.TaxonomyProvider = Config.Get<TaxonomyConfig>().DefaultProvider;
            metafield.TaxonomyId = moduleField.ClassificationId;
            metafield.IsSingleTaxon = false;
            return metafield;
          }
          if (moduleField.FieldType != FieldType.MultipleChoice || moduleField.FieldType != FieldType.Choices)
          {
            metafield.ColumnName = moduleField.Name;
            metafield.DefaultValue = moduleField.DefaultValue;
            metafield.DBType = moduleField.DBType;
            metafield.Required = moduleField.AllowNulls;
            if (moduleField.FieldType != FieldType.Number)
              metafield.DBLength = moduleField.DBLength;
            metafield.DBScale = moduleField.DecimalPlacesCount.ToString();
          }
          metafield.Origin = moduleField.Origin;
          return metafield;
        case FieldType.Media:
          metafield.ClrType = typeof (ContentLink[]).FullName;
          MetaFieldAttribute metaFieldAttribute1 = new MetaFieldAttribute();
          metaFieldAttribute1.Name = DynamicAttributeNames.ControlTag;
          metaFieldAttribute1.Value = WidgetTemplateInstaller.GetMediaFieldTypeTemplate(moduleField);
          MetaFieldAttribute metaFieldAttribute2 = metaFieldAttribute1;
          metafield.MetaAttributes.Add(metaFieldAttribute2);
          goto case FieldType.Classification;
        case FieldType.Guid:
          metafield.ClrType = typeof (Guid).FullName;
          goto case FieldType.Classification;
        case FieldType.GuidArray:
          metafield.ClrType = typeof (Guid[]).FullName;
          goto case FieldType.Classification;
        case FieldType.Choices:
          metafield.ClrType = moduleField.CanSelectMultipleItems ? typeof (ChoiceOption[]).FullName : typeof (ChoiceOption).FullName;
          metafield.ChoiceFieldDefinition = moduleField.Choices;
          goto case FieldType.Classification;
        case FieldType.Address:
          metafield.ClrType = typeof (Address).FullName;
          goto case FieldType.Classification;
        case FieldType.RelatedMedia:
          bool isMaster = false;
          string mediaType = moduleField.MediaType;
          if (!(mediaType == "image"))
          {
            if (!(mediaType == "video"))
            {
              if (mediaType == "file")
              {
                moduleField.RelatedDataType = typeof (Document).FullName;
                isMaster = moduleField.AllowMultipleFiles;
              }
            }
            else
            {
              moduleField.RelatedDataType = typeof (Video).FullName;
              isMaster = moduleField.AllowMultipleVideos;
            }
          }
          else
          {
            moduleField.RelatedDataType = typeof (Image).FullName;
            isMaster = moduleField.AllowMultipleImages;
          }
          metafield.ClrType = typeof (RelatedItems).FullName;
          metafield.IsInternal = true;
          metafield.AllowMultipleRelations = isMaster;
          metafield.IsProtectedRelation = !isMaster && moduleField.IsRequired;
          MetaFieldAttribute metaFieldAttribute3 = new MetaFieldAttribute();
          metaFieldAttribute3.Name = DynamicAttributeNames.RelatedType;
          metaFieldAttribute3.Value = moduleField.RelatedDataType;
          MetaFieldAttribute metaFieldAttribute4 = metaFieldAttribute3;
          metafield.MetaAttributes.Add(metaFieldAttribute4);
          MetaFieldAttribute metaFieldAttribute5 = new MetaFieldAttribute();
          metaFieldAttribute5.Name = DynamicAttributeNames.RelatedProviders;
          metaFieldAttribute5.Value = moduleField.RelatedDataProvider;
          MetaFieldAttribute metaFieldAttribute6 = metaFieldAttribute5;
          metafield.MetaAttributes.Add(metaFieldAttribute6);
          MetaFieldAttribute metaFieldAttribute7 = new MetaFieldAttribute();
          metaFieldAttribute7.Name = DynamicAttributeNames.ControlTag;
          metaFieldAttribute7.Value = DataStructureInstaller.GetControlTagValue(moduleField, isMaster);
          MetaFieldAttribute metaFieldAttribute8 = metaFieldAttribute7;
          metafield.MetaAttributes.Add(metaFieldAttribute8);
          metafield.Description = moduleField.InstructionalText;
          goto case FieldType.Classification;
        case FieldType.RelatedData:
          metafield.ClrType = typeof (RelatedItems).FullName;
          metafield.IsInternal = true;
          metafield.AllowMultipleRelations = moduleField.CanSelectMultipleItems;
          metafield.IsProtectedRelation = !moduleField.CanSelectMultipleItems && moduleField.IsRequired;
          MetaFieldAttribute metaFieldAttribute9 = new MetaFieldAttribute();
          metaFieldAttribute9.Name = DynamicAttributeNames.RelatedType;
          metaFieldAttribute9.Value = moduleField.RelatedDataType;
          MetaFieldAttribute metaFieldAttribute10 = metaFieldAttribute9;
          metafield.MetaAttributes.Add(metaFieldAttribute10);
          MetaFieldAttribute metaFieldAttribute11 = new MetaFieldAttribute();
          metaFieldAttribute11.Name = DynamicAttributeNames.RelatedProviders;
          metaFieldAttribute11.Value = moduleField.RelatedDataProvider;
          MetaFieldAttribute metaFieldAttribute12 = metaFieldAttribute11;
          metafield.MetaAttributes.Add(metaFieldAttribute12);
          MetaFieldAttribute metaFieldAttribute13 = new MetaFieldAttribute();
          metaFieldAttribute13.Name = DynamicAttributeNames.ControlTag;
          metaFieldAttribute13.Value = DataStructureInstaller.GetControlTagValue(moduleField, moduleField.CanSelectMultipleItems);
          MetaFieldAttribute metaFieldAttribute14 = metaFieldAttribute13;
          metafield.MetaAttributes.Add(metaFieldAttribute14);
          metafield.Description = moduleField.InstructionalText;
          goto case FieldType.Classification;
        default:
          throw new NotSupportedException();
      }
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    internal void UpdateMetaField(DynamicModuleField moduleField, MetaField metaField)
    {
      switch (moduleField.FieldType)
      {
        case FieldType.ShortText:
        case FieldType.LongText:
          metaField.IsLocalizable = moduleField.IsLocalizable;
          if (metaField.ClrType == typeof (Lstring).FullName != moduleField.IsLocalizable)
          {
            metaField.ClrType = moduleField.IsLocalizable ? typeof (Lstring).FullName : typeof (string).FullName;
            DataStructureInstaller.MarkMetaTypeDirty(metaField.Parent);
          }
          metaField.Description = moduleField.InstructionalText;
          break;
        case FieldType.DateTime:
          metaField.MinValue = moduleField.MinNumberRange;
          metaField.MaxValue = moduleField.MaxNumberRange;
          metaField.Description = moduleField.InstructionalText;
          metaField.IsLocalizable = moduleField.IsLocalizable;
          break;
        case FieldType.Number:
          metaField.IsLocalizable = moduleField.IsLocalizable;
          break;
        case FieldType.Classification:
          metaField.Title = moduleField.Title;
          break;
        case FieldType.Media:
          if (!"image".Equals(moduleField.MediaType))
            break;
          this.UpdateControlTagAttribute(moduleField, metaField, false);
          break;
        case FieldType.Choices:
          metaField.ChoiceFieldDefinition = moduleField.Choices;
          break;
        case FieldType.RelatedMedia:
          bool isMaster = false;
          string mediaType = moduleField.MediaType;
          if (!(mediaType == "image"))
          {
            if (!(mediaType == "video"))
            {
              if (mediaType == "file")
                isMaster = moduleField.AllowMultipleFiles;
            }
            else
              isMaster = moduleField.AllowMultipleVideos;
          }
          else
            isMaster = moduleField.AllowMultipleImages;
          metaField.AllowMultipleRelations = isMaster;
          metaField.IsProtectedRelation = !isMaster && moduleField.IsRequired;
          metaField.Description = moduleField.InstructionalText;
          this.UpdateControlTagAttribute(moduleField, metaField, isMaster);
          this.UpdateRelatedProvidersAttribute(moduleField, metaField);
          break;
        case FieldType.RelatedData:
          metaField.AllowMultipleRelations = moduleField.CanSelectMultipleItems;
          metaField.IsProtectedRelation = !moduleField.CanSelectMultipleItems && moduleField.IsRequired;
          metaField.Description = moduleField.InstructionalText;
          this.UpdateControlTagAttribute(moduleField, metaField, moduleField.CanSelectMultipleItems);
          this.UpdateRelatedProvidersAttribute(moduleField, metaField);
          break;
      }
    }

    /// <summary>
    /// Modifies the meta type in order to ensure that the model will be updated
    /// </summary>
    /// <param name="metaType">Type of the meta.</param>
    private static void MarkMetaTypeDirty(MetaType metaType)
    {
      string className = metaType.ClassName;
      metaType.ClassName = string.Empty;
      metaType.ClassName = className;
    }

    private static string GetControlTagValue(DynamicModuleField moduleField, bool isMaster)
    {
      string controlTagValue = string.Empty;
      switch (moduleField.FieldType)
      {
        case FieldType.Media:
          controlTagValue = WidgetTemplateInstaller.GetImageFieldTypeTemplate(moduleField, false);
          break;
        case FieldType.RelatedMedia:
        case FieldType.RelatedData:
          controlTagValue = FieldTemplateBuilder.BuildRelatedDataFieldTemplate(moduleField.FrontendWidgetTypeName, moduleField.FrontendWidgetLabel, moduleField.FieldNamespace, moduleField.RelatedDataType, moduleField.RelatedDataProvider, moduleField.Name, isMaster);
          break;
      }
      return controlTagValue;
    }

    private MetaType CreateMetaType(DynamicModuleType moduleType)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      MetaType metaType = this.metadataManager.CreateMetaType(moduleType.TypeNamespace, moduleType.TypeName, moduleType.Id);
      metaType.IsDynamic = true;
      metaType.BaseClassName = typeof (DynamicContent).FullName;
      metaType.DatabaseInheritance = DatabaseInheritanceType.vertical;
      metaType.Origin = moduleType.Origin;
      IList<MetaTypeAttribute> metaAttributes1 = metaType.MetaAttributes;
      MetaTypeAttribute metaTypeAttribute1 = new MetaTypeAttribute();
      metaTypeAttribute1.Name = "moduleName";
      metaTypeAttribute1.Value = ModuleBuilderManager.ModuleNameValidationRegex.Replace(moduleType.ModuleName, string.Empty);
      metaAttributes1.Add(metaTypeAttribute1);
      IList<MetaTypeAttribute> metaAttributes2 = metaType.MetaAttributes;
      MetaTypeAttribute metaTypeAttribute2 = new MetaTypeAttribute();
      metaTypeAttribute2.Name = "mainPropertyName";
      metaTypeAttribute2.Value = moduleType.MainShortTextFieldName;
      metaAttributes2.Add(metaTypeAttribute2);
      return metaType;
    }

    private MetaType GetMetaType(DynamicModuleType moduleType)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      return this.metadataManager.GetMetaTypes().Where<MetaType>((Expression<Func<MetaType, bool>>) (mt => mt.ClassName == moduleType.TypeName && mt.Namespace == moduleType.TypeNamespace)).SingleOrDefault<MetaType>();
    }

    private void UpdateControlTagAttribute(
      DynamicModuleField moduleField,
      MetaField metaField,
      bool isMaster)
    {
      MetaFieldAttribute metaFieldAttribute1 = metaField.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => a.Name == DynamicAttributeNames.ControlTag));
      bool flag;
      if (metaFieldAttribute1 != null)
      {
        string controlTagValue = DataStructureInstaller.GetControlTagValue(moduleField, isMaster);
        flag = controlTagValue != metaFieldAttribute1.Value;
        metaFieldAttribute1.Value = controlTagValue;
      }
      else
      {
        MetaFieldAttribute metaFieldAttribute2 = new MetaFieldAttribute();
        metaFieldAttribute2.Name = DynamicAttributeNames.ControlTag;
        metaFieldAttribute2.Value = DataStructureInstaller.GetControlTagValue(moduleField, isMaster);
        MetaFieldAttribute metaFieldAttribute3 = metaFieldAttribute2;
        metaField.MetaAttributes.Add(metaFieldAttribute3);
        flag = true;
      }
      if (!flag)
        return;
      DataStructureInstaller.MarkMetaTypeDirty(metaField.Parent);
    }

    private void UpdateRelatedProvidersAttribute(
      DynamicModuleField moduleField,
      MetaField metaField)
    {
      MetaFieldAttribute metaFieldAttribute1 = metaField.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => a.Name == DynamicAttributeNames.RelatedProviders));
      bool flag;
      if (metaFieldAttribute1 != null)
      {
        flag = moduleField.RelatedDataProvider != metaFieldAttribute1.Value;
        metaFieldAttribute1.Value = moduleField.RelatedDataProvider;
      }
      else
      {
        MetaFieldAttribute metaFieldAttribute2 = new MetaFieldAttribute();
        metaFieldAttribute2.Name = DynamicAttributeNames.RelatedProviders;
        metaFieldAttribute2.Value = moduleField.RelatedDataProvider;
        MetaFieldAttribute metaFieldAttribute3 = metaFieldAttribute2;
        metaField.MetaAttributes.Add(metaFieldAttribute3);
        flag = true;
      }
      if (!flag)
        return;
      DataStructureInstaller.MarkMetaTypeDirty(metaField.Parent);
    }
  }
}
