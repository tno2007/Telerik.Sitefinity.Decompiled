// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingPoints.PublishingPointFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Publishing.PublishingPoints
{
  /// <summary>Factory for creating publishing points.</summary>
  public class PublishingPointFactory
  {
    public static PublishingPointBuilder CreatePublishingPointBuilder() => new PublishingPointBuilder();

    /// <summary>
    /// Creates the publishing point, based on a collection of IDefinitionField objects.
    /// </summary>
    /// <param name="fields">The fields to convert to meta fields.</param>
    /// <param name="point">The publishing point.</param>
    public static void CreatePublishingPointDataItem(
      IEnumerable<IDefinitionField> fields,
      IPublishingPoint point,
      MetadataManager metadataManager = null)
    {
      bool flag1 = false;
      if (metadataManager == null)
      {
        metadataManager = MetadataManager.GetManager();
        flag1 = true;
      }
      MetaType dynamicType = PublishingPointDynamicTypeManager.CreateDynamicType(point, metadataManager);
      Type type = TypeResolutionService.ResolveType(dynamicType.BaseClassName);
      foreach (IDefinitionField field in fields)
      {
        IDefinitionField srcField = field;
        int num = dynamicType.Fields.Where<MetaField>((Func<MetaField, bool>) (f => f.FieldName == srcField.Name)).FirstOrDefault<MetaField>() != null ? 1 : 0;
        bool flag2 = ((IEnumerable<PropertyInfo>) type.GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.Name == srcField.Name)).FirstOrDefault<PropertyInfo>() != (PropertyInfo) null;
        if (num == 0 && !flag2)
        {
          MetaField metafield = metadataManager.CreateMetafield(srcField.Name);
          MetaField metaField = PublishingPointFactory.PopulateMetaFieldProperties(srcField, metafield, dynamicType);
          dynamicType.Fields.Add(metaField);
        }
      }
      if (!flag1)
        return;
      metadataManager.SaveChanges();
    }

    /// <summary>
    /// Updates the publishing point definition fields, based on a collection of IDefinitionField objects.
    /// </summary>
    /// <param name="newlySavedFields">The newly saved fields for the publishing point.</param>
    /// <param name="originalPublishingPoint">The original publishing point.</param>
    /// <returns>True if the publishing point was updated; False otherwise.</returns>
    public static bool UpdatePublishingPointDataItem(
      IEnumerable<IDefinitionField> newlySavedFields,
      IPublishingPoint originalPublishingPoint,
      MetadataManager metadataManager = null)
    {
      bool flag1 = false;
      if (metadataManager == null)
        metadataManager = MetadataManager.GetManager();
      Type type = TypeResolutionService.ResolveType(originalPublishingPoint.StorageTypeName);
      MetaType metaType = metadataManager.GetMetaType(type);
      List<string> addedFieldNames = new List<string>();
      foreach (IDefinitionField definitionField in newlySavedFields.Where<IDefinitionField>((Func<IDefinitionField, bool>) (f => f != null && f.IsModified)))
      {
        IDefinitionField updatedField = definitionField;
        MetaField fieldToPopulate = metaType.Fields.Where<MetaField>((Func<MetaField, bool>) (metaField => metaField.FieldName == updatedField.Name)).FirstOrDefault<MetaField>();
        if (fieldToPopulate == null)
        {
          MetaField metafield = metadataManager.CreateMetafield(updatedField.Name);
          MetaField metaField = PublishingPointFactory.PopulateMetaFieldProperties(updatedField, metafield, metaType);
          metaType.Fields.Add(metaField);
          addedFieldNames.Add(metaField.FieldName);
          flag1 = true;
        }
        else
        {
          PublishingPointFactory.PopulateMetaFieldProperties(updatedField, fieldToPopulate, metaType);
          flag1 = true;
        }
      }
      MetaField[] array = metaType.Fields.Where<MetaField>((Func<MetaField, bool>) (dbField => !addedFieldNames.Contains(dbField.FieldName) && !newlySavedFields.Any<IDefinitionField>((Func<IDefinitionField, bool>) (vmField => vmField.Name == dbField.FieldName)))).ToArray<MetaField>();
      bool flag2 = flag1 | (uint) array.Length > 0U;
      for (int index = 0; index < array.Length; ++index)
      {
        MetaField metafield = array[index];
        metadataManager.Delete(metafield);
      }
      return flag2;
    }

    /// <summary>
    /// Populates a meta field based on an IDefinitionField object.
    /// </summary>
    /// <param name="srcField">The source field.</param>
    /// <param name="fieldToPopulate">The meta field to populate.</param>
    /// <param name="parentType">The parent meta type of this meta field.</param>
    /// <returns>The updated meta field.</returns>
    private static MetaField PopulateMetaFieldProperties(
      IDefinitionField srcField,
      MetaField fieldToPopulate,
      MetaType parentType)
    {
      fieldToPopulate.ClrType = srcField.ClrType;
      fieldToPopulate.Title = srcField.Title;
      fieldToPopulate.DBSqlType = srcField.SQLDBType;
      fieldToPopulate.DBType = srcField.DBType;
      fieldToPopulate.DefaultValue = srcField.DefaultValue;
      fieldToPopulate.MaxLength = srcField.MaxLength;
      fieldToPopulate.TaxonomyId = string.IsNullOrWhiteSpace(srcField.TaxonomyId) ? Guid.Empty : new Guid(srcField.TaxonomyId);
      fieldToPopulate.TaxonomyProvider = srcField.TaxonomyProviderName;
      fieldToPopulate.IsSingleTaxon = !srcField.AllowMultipleTaxons;
      fieldToPopulate.Parent = parentType;
      return fieldToPopulate;
    }

    /// <summary>Gets the default RSS publishing structure fields.</summary>
    /// <returns>The default RSS publishing structure fields.</returns>
    public static IDefinitionField[] GetDefaultRssPublishingStructureFields() => new List<IDefinitionField>()
    {
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "Title",
        Name = "Title",
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "PublicationDate",
        Name = "PublicationDate",
        ClrType = typeof (DateTime).FullName
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "Link",
        Name = "Link",
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "Content",
        Name = "Content",
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR(MAX)",
        DBType = "LONGVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "Summary",
        Name = "Summary",
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "Username",
        Name = "Username",
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "OwnerFirstName",
        Name = "OwnerFirstName",
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "OwnerLastName",
        Name = "OwnerLastName",
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "OwnerEmail",
        Name = "OwnerEmail",
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "Categories",
        Name = "Categories",
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "ItemHash",
        Name = "ItemHash",
        ClrType = typeof (string).FullName
      },
      (IDefinitionField) new SimpleDefinitionField()
      {
        Title = "ExpirationDate",
        Name = "ExpirationDate",
        ClrType = typeof (DateTime).FullName
      }
    }.ToArray();
  }
}
