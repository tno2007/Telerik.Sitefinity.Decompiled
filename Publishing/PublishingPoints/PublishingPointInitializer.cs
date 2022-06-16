// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingPoints.PublishingPointInitializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Publishing.PublishingPoints
{
  internal abstract class PublishingPointInitializer : IPublishingPointInitializer
  {
    /// <summary>Gets the publishing point.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="title">The title.</param>
    /// <returns></returns>
    public virtual PublishingPoint GetPublishingPoint(
      string providerName,
      string title)
    {
      return PublishingManager.GetManager(providerName).GetPublishingPoints().Where<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (p => p.Name == title)).FirstOrDefault<PublishingPoint>();
    }

    /// <summary>
    /// Configures an instance of type <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPointDetailViewModel" /> that is used to transfer data when publishing point is being created.
    /// </summary>
    /// <returns>
    /// An instance of type <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPointDetailViewModel" /> that is used to transfer data when publishing point is being created.
    /// </returns>
    public abstract PublishingPointDetailViewModel ConfigurePublishingPoint();

    /// <summary>Creates the publishing point.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="templateName">Name of the template.</param>
    public virtual void CreatePublishingPoint(string providerName, string templateName) => this.CreatePublishingPoint(this.ConfigurePublishingPoint() ?? throw new ArgumentException("Publishing point is null!"), providerName, templateName);

    /// <summary>Creates the publishing point.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="templateName">Name of the template.</param>
    public virtual void CreatePublishingPoint(
      PublishingPointDetailViewModel publishingPoint,
      string providerName,
      string templateName)
    {
      string publishingPointName = publishingPoint.Title;
      PublishingManager manager = PublishingManager.GetManager(providerName);
      if (manager.GetPublishingPoints().Any<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (p => p.Name == publishingPointName)))
        return;
      PublishingPoint publishingPoint1 = manager.CreatePublishingPoint();
      List<SimpleDefinitionField> publishingPointDefinition = publishingPoint.PublishingPointDefinition;
      if (publishingPointDefinition != null && publishingPointDefinition.Any<SimpleDefinitionField>())
        this.CreatePublishingPointDataItem((IEnumerable<SimpleDefinitionField>) publishingPointDefinition, (IPublishingPoint) publishingPoint1);
      publishingPoint1.Name = publishingPointName;
      publishingPoint1.Description = (Lstring) publishingPoint.Description;
      publishingPoint1.IsActive = publishingPoint.IsActive;
      publishingPoint1.PublishingPointBusinessObjectName = publishingPoint.PublishingPointBusinessObjectName;
      PublishingPointInitializer.CreatePipeSettings(publishingPoint1, templateName, manager);
      manager.SaveChanges();
      MetadataManager.GetManager().SaveChanges();
    }

    /// <summary>Creates the pipe settings.</summary>
    /// <param name="point">The point.</param>
    /// <param name="templateName">Name of the template.</param>
    /// <param name="manager">The manager.</param>
    internal static void CreatePipeSettings(
      PublishingPoint point,
      string templateName,
      PublishingManager manager)
    {
      foreach (PipeSettings templatePipe in PublishingSystemFactory.GetTemplatePipes(templateName))
        PublishingPointInitializer.CreatePipeSettings(point, templatePipe, manager);
    }

    /// <summary>Creates the pipe settings.</summary>
    /// <param name="point">The point.</param>
    /// <param name="pipeSettings">The pipe settings.</param>
    /// <param name="manager">The manager.</param>
    internal static void CreatePipeSettings(
      PublishingPoint point,
      PipeSettings pipeSettings,
      PublishingManager manager)
    {
      PipeSettings pipeSettings1 = manager.CreatePipeSettings(pipeSettings.GetType());
      string applicationName = pipeSettings1.ApplicationName;
      pipeSettings.CopySettings(pipeSettings1, true);
      pipeSettings1.ApplicationName = applicationName;
      foreach (Mapping mapping in (IEnumerable<Mapping>) pipeSettings1.Mappings.Mappings)
        mapping.Id = manager.Provider.GetNewGuid();
      point.PipeSettings.Add(pipeSettings1);
    }

    /// <summary>
    /// Upgrades the pipe settings of an existing publishing point.
    /// </summary>
    /// <param name="publishingPointName">Name of the publishing point.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="templateName">Name of the template.</param>
    internal static void UpgradePublishingPointPipeSettings(
      string publishingPointName,
      string providerName,
      string templateName)
    {
      PublishingManager manager = PublishingManager.GetManager(providerName);
      PublishingPoint point = manager.GetPublishingPoints().FirstOrDefault<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (p => p.Name == publishingPointName));
      if (point == null)
        return;
      foreach (PipeSettings pipeSetting in (IEnumerable<PipeSettings>) point.PipeSettings)
      {
        manager.DeleteMappingSettings(pipeSetting.Mappings);
        manager.DeletePipeSettings(pipeSetting);
      }
      PublishingPointInitializer.CreatePipeSettings(point, templateName, manager);
      manager.SaveChanges();
    }

    /// <summary>
    /// Creates the publishing point, based on a collection of IDefinitionField objects.
    /// </summary>
    /// <param name="fields">The fields to convert to metafields.</param>
    /// <param name="point">The publishing point.</param>
    protected void CreatePublishingPointDataItem(
      IEnumerable<SimpleDefinitionField> fields,
      IPublishingPoint point)
    {
      MetaType dynamicType = PublishingPointDynamicTypeManager.CreateDynamicType(point);
      MetadataManager manager = MetadataManager.GetManager();
      Type type = TypeResolutionService.ResolveType(dynamicType.BaseClassName);
      foreach (IDefinitionField field in fields)
      {
        IDefinitionField srcField = field;
        int num = dynamicType.Fields.Where<MetaField>((Func<MetaField, bool>) (f => f.FieldName == srcField.Name)).FirstOrDefault<MetaField>() != null ? 1 : 0;
        bool flag = ((IEnumerable<PropertyInfo>) type.GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.Name == srcField.Name)).FirstOrDefault<PropertyInfo>() != (PropertyInfo) null;
        if (num == 0 && !flag)
        {
          MetaField metafield = manager.CreateMetafield(srcField.Name);
          MetaField metaField = this.PopulateMetaFieldProperties(srcField, metafield, dynamicType);
          dynamicType.Fields.Add(metaField);
        }
      }
    }

    /// <summary>Populates the meta field properties.</summary>
    /// <param name="srcField">The SRC field.</param>
    /// <param name="fieldToPopulate">The field to populate.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns></returns>
    protected virtual MetaField PopulateMetaFieldProperties(
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
  }
}
