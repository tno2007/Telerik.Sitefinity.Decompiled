// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.MetadataSourceAggregator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Exceptions;
using Telerik.OpenAccess.FetchOptimization;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Telerik.OpenAccess.Metadata.Fluent.Artificial;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>
  /// 
  /// </summary>
  public class MetadataSourceAggregator
  {
    private MetadataContainer currentModel;
    internal const string metaCacheKey = "sf_meta_types_cache";
    internal const string ArteficialParentPropertyName = "sfParent";
    internal static readonly object metaCacheLock = new object();

    public MetadataSourceAggregator()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.MetadataSourceAggregator" /> class.
    /// </summary>
    /// <param name="metadataSource">The metadata source.</param>
    public MetadataSourceAggregator(MetadataSource metadataSource)
      : this(metadataSource.GetModel())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.MetadataSourceAggregator" /> class.
    /// </summary>
    /// <param name="metadataContainer">The metadata container.</param>
    public MetadataSourceAggregator(MetadataContainer metadataContainer) => this.currentModel = metadataContainer;

    /// <summary>
    /// Extends the current metadata container with new metadatasource specified sf metadata source.
    /// </summary>
    /// <param name="sfMetadataSource">The sf metadata source.</param>
    /// <returns></returns>
    public void Extend(MetadataSource metadataSource)
    {
      if (this.currentModel != null)
        metadataSource = (MetadataSource) new AggregateMetadataSource((MetadataSource) new StaticMetadataSource(this.currentModel), metadataSource, AggregationOptions.Late);
      this.currentModel = metadataSource.GetModel();
    }

    /// <summary>Extends the specified metadata container.</summary>
    /// <param name="metadataContainer">The metadata container.</param>
    public void Extend(MetadataContainer metadataContainer) => this.Extend((MetadataSource) new StaticMetadataSource(metadataContainer));

    /// <summary>Gets the current aggregated metadata container.</summary>
    /// <value>The current model.</value>
    public MetadataContainer CurrentModel => this.currentModel;

    internal static IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType> GetMetaTypes(
      MetadataManager manager = null)
    {
      if (!Config.Get<DataConfig>().DisableMetaTypeCache)
        return MetadataSourceAggregator.GetMetaCache(manager);
      return manager == null ? (IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType>) Meta.GetMetaTypes() : (IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType>) manager.GetMetaTypes();
    }

    private static IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType> GetMetaCache(
      MetadataManager manager = null)
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
      if (!(cacheManager["sf_meta_types_cache"] is IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType> detachedCopy1))
      {
        if (manager == null)
        {
          string transactionName = Guid.NewGuid().ToString();
          manager = MetadataManager.GetManager((string) null, transactionName);
          TransactionManager.DisposeTransaction(transactionName);
        }
        if (!(manager.Provider.GetTransaction() is SitefinityOAContext transaction))
          return (IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType>) manager.GetMetaTypes();
        lock (MetadataSourceAggregator.metaCacheLock)
        {
          if (!(cacheManager["sf_meta_types_cache"] is IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType> detachedCopy1))
          {
            using (new MethodPerformanceRegion("Create meta cache"))
            {
              string appName = manager.Provider.ApplicationName;
              FetchStrategy fetchStrategy1 = transaction.FetchStrategy;
              FetchStrategy fetchStrategy2 = new FetchStrategy();
              fetchStrategy2.LoadWith<Telerik.Sitefinity.Metadata.Model.MetaType>((Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, object>>) (x => x.MetaAttributes));
              fetchStrategy2.LoadWith<MetaField>((Expression<Func<MetaField, object>>) (x => x.MetaAttributes));
              transaction.FetchStrategy = fetchStrategy2;
              Dictionary<Guid, List<MetaField>> dictionary = transaction.CreateDetachedCopy<MetaField>((IEnumerable<MetaField>) transaction.GetAll<MetaField>().OrderBy<MetaField, Guid>((Expression<Func<MetaField, Guid>>) (x => x.Id)).ToList<MetaField>(), fetchStrategy2).GroupBy<MetaField, Guid, MetaField>((Func<MetaField, Guid>) (f => f.MetaTypeId), (Func<MetaField, MetaField>) (f => f)).ToDictionary<IGrouping<Guid, MetaField>, Guid, List<MetaField>>((Func<IGrouping<Guid, MetaField>, Guid>) (g => g.Key), (Func<IGrouping<Guid, MetaField>, List<MetaField>>) (g => g.OrderBy<MetaField, Guid>((Func<MetaField, Guid>) (f => f.Id)).ToList<MetaField>()));
              detachedCopy1 = transaction.CreateDetachedCopy<Telerik.Sitefinity.Metadata.Model.MetaType>((IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType>) transaction.GetAll<Telerik.Sitefinity.Metadata.Model.MetaType>().Where<Telerik.Sitefinity.Metadata.Model.MetaType>((Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>>) (x => x.ApplicationName == appName)).ToList<Telerik.Sitefinity.Metadata.Model.MetaType>(), fetchStrategy2);
              foreach (Telerik.Sitefinity.Metadata.Model.MetaType metaType in detachedCopy1)
              {
                List<MetaField> metaFieldList;
                if (dictionary.TryGetValue(metaType.Id, out metaFieldList))
                {
                  foreach (MetaField metaField in metaFieldList)
                    metaField.OverridenParent = metaType;
                }
                else
                  metaFieldList = new List<MetaField>();
                metaType.Fields = (IList<MetaField>) metaFieldList;
              }
              transaction.FetchStrategy = fetchStrategy1;
              ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[4]
              {
                (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds(1200.0)),
                (ICacheItemExpiration) new DataItemCacheDependency(typeof (Telerik.Sitefinity.Metadata.Model.MetaType), (string) null),
                (ICacheItemExpiration) new DataItemCacheDependency(typeof (MetaField), (string) null),
                (ICacheItemExpiration) new DataItemCacheDependency(typeof (MetaAttribute), (string) null)
              };
              cacheManager.Add("sf_meta_types_cache", (object) detachedCopy1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
            }
          }
        }
      }
      return detachedCopy1;
    }

    /// <summary>
    /// Adds the artificial fields to the mapping configuration.
    /// </summary>
    /// <param name="metadataManager">The metadata manager.</param>
    /// <param name="mapping">The mapping.</param>
    /// <param name="type">The type.</param>
    /// <param name="context">The context.</param>
    public static bool AddArtificialFields(
      MetadataManager metadataManager,
      MappingConfiguration mapping,
      Type type,
      IDatabaseMappingContext context)
    {
      DatabaseMappingOptions options = new DatabaseMappingOptions();
      options.LoadDefaults();
      return MetadataSourceAggregator.AddArtificialFields(metadataManager.GetMetaType(type), mapping, type, context, (IDatabaseMappingOptions) options, MetadataSourceAggregator.GetConfiguredCultures(), metadataManager, out IEnumerable<LstringFiedInfo> _);
    }

    /// <summary>Adds the artificial fields for the specified type</summary>
    /// <param name="metadataManager">The metadata manager.</param>
    /// <param name="mapping">The mapping.</param>
    /// <param name="type">The type.</param>
    /// <param name="context">The context.</param>
    /// <param name="notAddedMapppings">The not added mapppings, which have to be added to the inheritors of this type on a second pass.</param>
    /// <returns></returns>
    internal static bool AddArtificialFields(
      Telerik.Sitefinity.Metadata.Model.MetaType metaType,
      MetadataManager metadataManager,
      MappingConfiguration mapping,
      Type type,
      IDatabaseMappingContext context,
      IDatabaseMappingOptions options,
      out IEnumerable<LstringFiedInfo> notAddedMapppings)
    {
      return MetadataSourceAggregator.AddArtificialFields(metaType, mapping, type, context, options, MetadataSourceAggregator.GetConfiguredCultures(), metadataManager, out notAddedMapppings);
    }

    /// <summary>
    /// Adds the artificial fields from a metatype to a OpenAccess fluent mapping configuration.
    /// </summary>
    /// <param name="metaType">The metatype.</param>
    /// <param name="mapping">The fluent mapping configuration.</param>
    /// <param name="type">The persistent type.</param>
    /// <param name="context">The Database context (oracle,mysql, mssql, postgresql).</param>
    /// <param name="cultures">The cultures that are to be supported for Lstring properties (multilingual mode).</param>
    /// <param name="metadataManager">The metadata manager.</param>
    /// <param name="options">The options.</param>
    /// <param name="notAddedMapppings">The not added mapppings.</param>
    /// <returns></returns>
    internal static bool AddArtificialFields(
      Telerik.Sitefinity.Metadata.Model.MetaType metaType,
      MappingConfiguration mapping,
      Type type,
      IDatabaseMappingContext context,
      IDatabaseMappingOptions options,
      CultureInfo[] cultures,
      MetadataManager metadataManager,
      out IEnumerable<LstringFiedInfo> notAddedMapppings)
    {
      bool flag = false;
      List<LstringFiedInfo> notAddedMapppings1 = new List<LstringFiedInfo>();
      IEnumerable<PropertyInfo> lstringProperties = MetadataSourceAggregator.GetLstringProperties(type);
      string requiredPropertyName = ILocalizableExtensions.GetRequiredPropertyName(type);
      foreach (PropertyInfo propInfo in lstringProperties)
      {
        bool isMainProperty = false;
        if (!requiredPropertyName.IsNullOrEmpty() && requiredPropertyName.Equals(propInfo.Name))
          isMainProperty = true;
        HashSet<string> addedFields = new HashSet<string>();
        MetadataSourceAggregator.SetLStringPropertyMapping(mapping, type, propInfo, CultureInfo.InvariantCulture, context, options, ref addedFields, ref notAddedMapppings1, isMainProperty);
        foreach (CultureInfo culture in cultures)
          MetadataSourceAggregator.SetLStringPropertyMapping(mapping, type, propInfo, culture, context, options, ref addedFields, ref notAddedMapppings1, isMainProperty);
        flag = true;
      }
      notAddedMapppings = notAddedMapppings1.Count <= 0 ? (IEnumerable<LstringFiedInfo>) null : (IEnumerable<LstringFiedInfo>) notAddedMapppings1;
      if (metaType != null)
      {
        foreach (MetaField field in (IEnumerable<MetaField>) metaType.Fields)
        {
          if (!field.IsInternal)
          {
            PropertyConfiguration configuration = MetadataSourceAggregator.AddArtificialField(mapping, metaType, field, context, options, cultures, metadataManager);
            if (field.IsDeleted && configuration != null)
              configuration.Drop<PropertyConfiguration>();
            flag = true;
          }
        }
      }
      return flag;
    }

    private static IEnumerable<PropertyInfo> GetLstringProperties(Type type) => ((IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => typeof (Lstring).IsAssignableFrom(p.PropertyType)));

    public static DynamicTypeInfo[] AddArtificialTypes(
      MetadataManager metadataManager,
      IList<MappingConfiguration> mappings,
      Type baseType,
      IDatabaseMappingContext context)
    {
      return MetadataSourceAggregator.AddArtificialTypes(metadataManager, mappings, baseType, context, (IDatabaseMappingOptions) null);
    }

    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable")]
    internal static DynamicTypeInfo[] AddArtificialTypes(
      MetadataManager metadataManager,
      IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType> metaTypesQuery,
      IList<MappingConfiguration> mappings,
      Type baseType,
      IDatabaseMappingContext context,
      IDatabaseMappingOptions options)
    {
      if (context != null)
      {
        string moduleName = context.ModuleName;
        string baseTypeName = baseType.FullName;
        metaTypesQuery = moduleName != null || !baseType.Equals(typeof (DynamicTypeBase)) && !baseType.Equals(typeof (DynamicContent)) ? metaTypesQuery.Where<Telerik.Sitefinity.Metadata.Model.MetaType>((Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>) (t =>
        {
          if (!t.IsDynamic || !(t.BaseClassName == baseTypeName))
            return false;
          return t.ModuleName == null || t.ModuleName == moduleName;
        })) : metaTypesQuery.Where<Telerik.Sitefinity.Metadata.Model.MetaType>((Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>) (t =>
        {
          if (!t.IsDynamic || t.ModuleName != null)
            return false;
          return t.BaseClassName == null || t.BaseClassName == baseTypeName;
        }));
        List<Telerik.Sitefinity.Metadata.Model.MetaType> list = metaTypesQuery.ToList<Telerik.Sitefinity.Metadata.Model.MetaType>();
        if (list.Count > 0)
        {
          List<DynamicTypeInfo> dynamicTypeInfoList = new List<DynamicTypeInfo>();
          IEnumerable<MetadataMapping> metadataMappings = (IEnumerable<MetadataMapping>) null;
          foreach (Telerik.Sitefinity.Metadata.Model.MetaType type in list)
          {
            MappingConfiguration mappingConfiguration = new MappingConfiguration(type.ClassName, type.Namespace);
            mappingConfiguration.HasBaseType(baseType);
            EntityMap entityMap = mappingConfiguration.MapType().Inheritance(MetadataSourceAggregator.GetInheritanceStrategy(type.DatabaseInheritance));
            if ((baseType.IsAssignableFrom(typeof (DynamicContent)) || baseType.IsAssignableFrom(typeof (FormEntry))) && !type.MetaAttributes.Any<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => "useAutoGeneratedTableName".Equals(a.Name, StringComparison.InvariantCultureIgnoreCase))))
            {
              if (metadataMappings == null && list.Count > 1)
              {
                IEnumerable<string> classNames = list.Select<Telerik.Sitefinity.Metadata.Model.MetaType, string>((Func<Telerik.Sitefinity.Metadata.Model.MetaType, string>) (x => x.ClassName));
                metadataMappings = (IEnumerable<MetadataMapping>) metadataManager.GetMetadataMappings().Where<MetadataMapping>((Expression<Func<MetadataMapping, bool>>) (x => classNames.Contains<string>(x.TypeName))).ToList<MetadataMapping>();
              }
              string tableName = MetadataSourceAggregator.GetTableName(metadataManager, type, (MetaField) null, context, metadataMappings);
              entityMap.ToTable((TableName) tableName);
            }
            string str = type.MetaAttributes.Where<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => "mainPropertyName".Equals(a.Name))).Select<MetaTypeAttribute, string>((Func<MetaTypeAttribute, string>) (a => a.Value)).FirstOrDefault<string>();
            foreach (MetaField field in (IEnumerable<MetaField>) type.Fields)
            {
              if (!field.IsInternal)
              {
                bool isMainProperty = false;
                if (!str.IsNullOrEmpty() && str.Equals(field.FieldName))
                  isMainProperty = true;
                PropertyConfiguration configuration = MetadataSourceAggregator.AddArtificialField(mappingConfiguration, type, field, context, options, MetadataSourceAggregator.GetConfiguredCultures(), metadataManager, metadataMappings, isMainProperty);
                if (field.IsDeleted && configuration != null)
                  configuration.Drop<PropertyConfiguration>();
              }
            }
            if (type.IsDeleted)
              mappingConfiguration.Drop<MappingConfiguration>();
            mappings.Add(mappingConfiguration);
            dynamicTypeInfoList.Add(new DynamicTypeInfo()
            {
              MetaTypeId = type.Id,
              Name = type.Namespace + "." + type.ClassName,
              IsArtificial = true,
              IsDeleted = type.IsDeleted
            });
          }
          foreach (Telerik.Sitefinity.Metadata.Model.MetaType metaType1 in list)
          {
            Telerik.Sitefinity.Metadata.Model.MetaType metaType = metaType1;
            MappingConfiguration configuration = mappings.FirstOrDefault<MappingConfiguration>((Func<MappingConfiguration, bool>) (x => x.ConfiguredType.FullName == metaType.FullTypeName));
            if (metaType.ParentTypeId != Guid.Empty)
            {
              Telerik.Sitefinity.Metadata.Model.MetaType parentMetaType = list.FirstOrDefault<Telerik.Sitefinity.Metadata.Model.MetaType>((Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>) (x => x.Id == metaType.ParentTypeId));
              MappingConfiguration mappingConfiguration = mappings.FirstOrDefault<MappingConfiguration>((Func<MappingConfiguration, bool>) (x => x.ConfiguredType.FullName == parentMetaType.FullTypeName));
              if (mappingConfiguration != null)
              {
                configuration.HasArtificialAssociation("sfParent", mappingConfiguration.ConfiguredType).ToColumn("parent_id");
                configuration.HasArtificialPrimitiveProperty<Guid>("parent_id");
              }
            }
          }
          return dynamicTypeInfoList.ToArray();
        }
      }
      return (DynamicTypeInfo[]) null;
    }

    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable")]
    internal static DynamicTypeInfo[] AddArtificialTypes(
      MetadataManager metadataManager,
      IList<MappingConfiguration> mappings,
      Type baseType,
      IDatabaseMappingContext context,
      IDatabaseMappingOptions options)
    {
      if (context == null)
        return (DynamicTypeInfo[]) null;
      string moduleName = context.ModuleName;
      string fullName = baseType.FullName;
      IQueryable<Telerik.Sitefinity.Metadata.Model.MetaType> metaTypes = metadataManager.GetMetaTypes();
      return MetadataSourceAggregator.AddArtificialTypes(metadataManager, (IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType>) metaTypes, mappings, baseType, context, options);
    }

    /// <summary>
    /// Method should be used only for manipulating artifical fields - additional columns for different language versions of Lstring. It does not include the system default language.
    /// </summary>
    /// <returns></returns>
    internal static CultureInfo[] GetConfiguredCultures()
    {
      CultureInfo[] cultureInfoArray = Config.Get<ResourcesConfig>().FrontendAndBackendCultures;
      if (!AppSettings.CurrentSettings.LegacyMultilingual)
        cultureInfoArray = ((IEnumerable<CultureInfo>) cultureInfoArray).Except<CultureInfo>((IEnumerable<CultureInfo>) new CultureInfo[1]
        {
          AppSettings.CurrentSettings.DefaultFrontendLanguage
        }).ToArray<CultureInfo>();
      return ((IEnumerable<CultureInfo>) cultureInfoArray).ToArray<CultureInfo>();
    }

    private static InheritanceStrategy GetInheritanceStrategy(
      DatabaseInheritanceType databaseInheritance)
    {
      switch (databaseInheritance)
      {
        case DatabaseInheritanceType.flat:
          return InheritanceStrategy.Flat;
        case DatabaseInheritanceType.vertical:
          return InheritanceStrategy.Vertical;
        case DatabaseInheritanceType.horizontal:
          return InheritanceStrategy.Horizontal;
        default:
          return InheritanceStrategy.Default;
      }
    }

    private static PropertyConfiguration AddArtificialField(
      MappingConfiguration mapping,
      Telerik.Sitefinity.Metadata.Model.MetaType type,
      MetaField field,
      IDatabaseMappingContext context,
      IDatabaseMappingOptions options,
      CultureInfo[] cultures,
      MetadataManager metadataManager,
      IEnumerable<MetadataMapping> preLoadedMappings = null,
      bool isMainProperty = false)
    {
      PropertyConfiguration propertyConfiguration1;
      if (!string.IsNullOrEmpty(field.TaxonomyProvider))
        propertyConfiguration1 = !field.IsSingleTaxon ? (PropertyConfiguration) mapping.HasArtificialAssociation(field.FieldName, typeof (TrackedList<Guid>)).HasFieldName<ArtificialNavigationPropertyConfiguration>(field.FieldName) : (PropertyConfiguration) mapping.HasArtificialPrimitiveProperty(field.FieldName, typeof (Guid)).HasFieldName<PrimitivePropertyConfiguration>(field.FieldName);
      else if (!string.IsNullOrEmpty(field.LinkedContentProvider))
      {
        propertyConfiguration1 = (PropertyConfiguration) null;
      }
      else
      {
        Type type1 = TypeResolutionService.ResolveType(field.ClrType);
        if (type1.Equals(typeof (Lstring)))
        {
          propertyConfiguration1 = MetadataSourceAggregator.SetLStringFieldMapping(mapping, field, CultureInfo.InvariantCulture, context, options, isMainProperty);
          bool flag = (MetadataManager.GetMetaFieldFlags(field) & MetaFieldFlags.PersisteInMainTable) > MetaFieldFlags.None;
          foreach (CultureInfo culture in cultures)
            MetadataSourceAggregator.SetLStringFieldMapping(mapping, field, culture, context, options, isMainProperty, new bool?(flag));
        }
        else if (type1.Equals(typeof (string)) || type1.Equals(typeof (ChoiceOption)))
        {
          StringPropertyConfiguration propertyConfiguration2 = mapping.HasArtificialStringProperty(field.FieldName).HasFieldName<StringPropertyConfiguration>(field.FieldName);
          if (!string.IsNullOrEmpty(field.ColumnName))
            propertyConfiguration2.ToColumn<StringPropertyConfiguration>(field.ColumnName);
          int? nullable = new int?();
          int result;
          if (!string.IsNullOrEmpty(field.DBLength) && int.TryParse(field.DBLength, out result))
            nullable = new int?(result);
          DatabaseColumnMapping mapping1 = context.GetMapping(field.DBType, type1, new DatabaseColumnMapping()
          {
            Length = nullable
          });
          propertyConfiguration1 = (PropertyConfiguration) propertyConfiguration2.ApplyColumnMapping(mapping1);
        }
        else if (type1.Equals(typeof (Address)))
        {
          ArtificialNavigationPropertyConfiguration propertyConfiguration3 = mapping.HasArtificialAssociation(field.FieldName, typeof (Address)).HasFieldName<ArtificialNavigationPropertyConfiguration>(field.FieldName);
          if (!string.IsNullOrEmpty(field.ColumnName))
            propertyConfiguration3.ToColumn(field.ColumnName);
          propertyConfiguration1 = (PropertyConfiguration) propertyConfiguration3;
        }
        else if (type1.IsArray)
        {
          Type propertyType = type1 == typeof (ChoiceOption[]) ? typeof (string[]) : type1;
          propertyConfiguration1 = (PropertyConfiguration) mapping.HasArtificialAssociation(field.FieldName, propertyType).HasFieldName<ArtificialNavigationPropertyConfiguration>(field.FieldName);
          if (type1.IsAssignableFrom(typeof (ContentLink[])) && !field.MetaAttributes.Any<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => "useAutoGeneratedTableName".Equals(a.Name, StringComparison.InvariantCultureIgnoreCase))))
          {
            string tableName = MetadataSourceAggregator.GetTableName(metadataManager, type, field, context, preLoadedMappings);
            ((ArtificialNavigationPropertyConfiguration) propertyConfiguration1).MapJoinTable((TableName) tableName);
          }
        }
        else
        {
          if (type1.IsGenericType && type1.GetGenericTypeDefinition() == typeof (Nullable<>))
          {
            Type underlyingType = Nullable.GetUnderlyingType(type1);
            if (object.Equals((object) underlyingType, (object) typeof (DateTime)) || object.Equals((object) underlyingType, (object) typeof (Decimal)))
            {
              IList<PropertyConfiguration> source = MetadataSourceAggregator.SetLocalizableFieldMapping(mapping, context, options, field);
              if (field.IsDeleted)
              {
                foreach (PropertyConfiguration configuration in (IEnumerable<PropertyConfiguration>) source)
                  configuration.Drop<PropertyConfiguration>();
              }
              return source.FirstOrDefault<PropertyConfiguration>();
            }
          }
          PrimitivePropertyConfiguration propertyConfiguration4 = mapping.HasArtificialPrimitiveProperty(field.FieldName, type1).HasFieldName<PrimitivePropertyConfiguration>(field.FieldName);
          DatabaseColumnMapping fieldColumnMapping = MetadataSourceAggregator.GetMetaFieldColumnMapping(field.DBLength, field.DBScale);
          DatabaseColumnMapping mapping2 = context.GetMapping(field.DBType, type1, fieldColumnMapping);
          PrimitivePropertyConfiguration configuration1 = propertyConfiguration4.ApplyColumnMapping(mapping2);
          if (!string.IsNullOrEmpty(field.ColumnName))
            configuration1 = configuration1.ToColumn<PrimitivePropertyConfiguration>(field.ColumnName);
          if (!field.Required)
            configuration1 = configuration1.IsNullable<PrimitivePropertyConfiguration>();
          propertyConfiguration1 = (PropertyConfiguration) configuration1;
        }
      }
      return propertyConfiguration1;
    }

    private static DatabaseColumnMapping GetMetaFieldColumnMapping(
      string dbLength,
      string dbScale)
    {
      DatabaseColumnMapping fieldColumnMapping = new DatabaseColumnMapping();
      int result1;
      if (!string.IsNullOrEmpty(dbLength) && int.TryParse(dbLength, out result1))
      {
        fieldColumnMapping.Length = new int?(result1);
        fieldColumnMapping.Precision = new int?(result1);
      }
      int result2;
      if (!string.IsNullOrEmpty(dbScale) && int.TryParse(dbScale, out result2))
        fieldColumnMapping.Scale = new int?(result2);
      return fieldColumnMapping;
    }

    private static PropertyConfiguration SetLStringPropertyMapping(
      MappingConfiguration mapping,
      Type classType,
      PropertyInfo propInfo,
      CultureInfo language,
      IDatabaseMappingContext context,
      IDatabaseMappingOptions options,
      ref HashSet<string> addedFields,
      ref List<LstringFiedInfo> notAddedMapppings,
      bool isMainProperty = false)
    {
      LstringFiedInfo lstringFiedInfo = MetadataSourceAggregator.GetLstringFiedInfo(propInfo, language, isMainProperty);
      if (lstringFiedInfo == null || addedFields.Contains(lstringFiedInfo.FieldName))
        return (PropertyConfiguration) null;
      PropertyConfiguration mappingConfig;
      if (!MetadataSourceAggregator.TrySetLocalizablePropertyMapping(mapping, context, options, (LPropertyFieldInfo) lstringFiedInfo, out mappingConfig))
        notAddedMapppings.Add(lstringFiedInfo);
      return mappingConfig;
    }

    internal static bool TrySetLocalizablePropertyMapping(
      MappingConfiguration mapping,
      IDatabaseMappingContext context,
      IDatabaseMappingOptions options,
      LPropertyFieldInfo field,
      out PropertyConfiguration mappingConfig)
    {
      if (options == null)
      {
        DatabaseMappingOptions databaseMappingOptions = new DatabaseMappingOptions();
        databaseMappingOptions.LoadDefaults();
        options = (IDatabaseMappingOptions) databaseMappingOptions;
      }
      LstringFiedInfo lstringFiedInfo = field as LstringFiedInfo;
      bool flag = false;
      if (field.Language == null || field.Language.Equals((object) CultureInfo.InvariantCulture) || !options.UseMultilingualSplitTables || options.SplitTablesIgnoredCultures.Contains(field.Language.Name) || field.IgnoreSplitTables && !options.MainFieldsIgnoredCultures.Contains(field.Language.Name))
      {
        if (lstringFiedInfo != null)
        {
          mappingConfig = (PropertyConfiguration) mapping.HasArtificialStringProperty(lstringFiedInfo.FieldName).HasFieldName<StringPropertyConfiguration>(lstringFiedInfo.FieldName).IsText(context, lstringFiedInfo.IsLong, lstringFiedInfo.IsUnicode, lstringFiedInfo.DBLength);
          if (!field.ColumnName.IsNullOrEmpty())
            ((StringPropertyConfiguration) mappingConfig).ToColumn<StringPropertyConfiguration>(field.ColumnName);
        }
        else
        {
          mappingConfig = (PropertyConfiguration) mapping.HasArtificialPrimitiveProperty(field.FieldName, field.ClrType).HasFieldName<PrimitivePropertyConfiguration>(field.FieldName);
          DatabaseColumnMapping fieldColumnMapping = MetadataSourceAggregator.GetMetaFieldColumnMapping(field.DBLength.ToString(), field.DBScale);
          DatabaseColumnMapping mapping1 = context.GetMapping(field.DBType, field.ClrType, fieldColumnMapping);
          mappingConfig = (PropertyConfiguration) ((PrimitivePropertyConfiguration) mappingConfig).ApplyColumnMapping(mapping1);
          if (!field.ColumnName.IsNullOrEmpty())
            ((PrimitivePropertyConfiguration) mappingConfig).ToColumn<PrimitivePropertyConfiguration>(field.ColumnName);
        }
        if (!field.IsMainProperty && field.Language != null && !field.Language.Equals((object) CultureInfo.InvariantCulture))
          flag = true;
      }
      else
      {
        object obj = typeof (MappingConfiguration).GetProperty("TableName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue((object) mapping, (object[]) null);
        if (obj == null)
        {
          string name = mapping.ConfiguredType.Name;
          if (name.StartsWith("sf_ec_prdct_"))
          {
            obj = (object) new TableName(name, (string) null);
          }
          else
          {
            mappingConfig = (PropertyConfiguration) null;
            return false;
          }
        }
        string columnName = !field.ColumnName.IsNullOrEmpty() ? field.ColumnName : field.FieldName;
        string originalName = ((TableName) obj).ShortName + field.Extension;
        string andUniqueTableName = new BackendNamingHelper(context.DatabaseType).GetShortAndUniqueTableName(originalName, (IList<string>) new List<string>());
        if (lstringFiedInfo != null)
        {
          mappingConfig = (PropertyConfiguration) mapping.HasArtificialStringProperty(lstringFiedInfo.FieldName).HasFieldName<StringPropertyConfiguration>(lstringFiedInfo.FieldName).IsText(context, lstringFiedInfo.IsLong, lstringFiedInfo.IsUnicode, lstringFiedInfo.DBLength);
          ((StringPropertyConfiguration) mappingConfig).ToColumn<StringPropertyConfiguration>(columnName, (TableName) andUniqueTableName);
        }
        else
        {
          mappingConfig = (PropertyConfiguration) mapping.HasArtificialPrimitiveProperty(field.FieldName, field.ClrType).HasFieldName<PrimitivePropertyConfiguration>(field.FieldName);
          DatabaseColumnMapping fieldColumnMapping = MetadataSourceAggregator.GetMetaFieldColumnMapping(field.DBLength.ToString(), field.DBScale);
          DatabaseColumnMapping mapping2 = context.GetMapping(field.DBType, field.ClrType, fieldColumnMapping);
          mappingConfig = (PropertyConfiguration) ((PrimitivePropertyConfiguration) mappingConfig).ApplyColumnMapping(mapping2);
          ((PrimitivePropertyConfiguration) mappingConfig).ToColumn<PrimitivePropertyConfiguration>(columnName, (TableName) andUniqueTableName);
        }
        flag = true;
      }
      if (flag && options.UseMultilingualFetchStrategy)
      {
        if (lstringFiedInfo != null)
          ((StringPropertyConfiguration) mappingConfig).WithLoadBehavior<StringPropertyConfiguration>(LoadBehavior.Lazy);
        else
          ((PrimitivePropertyConfiguration) mappingConfig).WithLoadBehavior<PrimitivePropertyConfiguration>(LoadBehavior.Lazy);
      }
      if (!field.DBType.IsNullOrEmpty())
      {
        DatabaseColumnMapping mapping3 = context.GetMapping(field.DBType, typeof (string));
        if (lstringFiedInfo != null)
          ((StringPropertyConfiguration) mappingConfig).ApplyColumnMapping(mapping3);
        else
          ((PrimitivePropertyConfiguration) mappingConfig).ApplyColumnMapping(mapping3);
      }
      return true;
    }

    private static IList<PropertyConfiguration> SetLocalizableFieldMapping(
      MappingConfiguration mapping,
      IDatabaseMappingContext context,
      IDatabaseMappingOptions options,
      MetaField metaField)
    {
      List<PropertyConfiguration> propertyConfigurationList = new List<PropertyConfiguration>();
      LPropertyFieldInfo lpropertyFieldInfo1 = MetadataSourceAggregator.GetLPropertyFieldInfo(metaField, CultureInfo.InvariantCulture, false);
      PropertyConfiguration mappingConfig;
      MetadataSourceAggregator.TrySetLocalizablePropertyMapping(mapping, context, options, lpropertyFieldInfo1, out mappingConfig);
      propertyConfigurationList.Add(mappingConfig);
      if (metaField.IsLocalizable)
      {
        foreach (CultureInfo frontendLanguage in AppSettings.CurrentSettings.DefinedFrontendLanguages)
        {
          LPropertyFieldInfo lpropertyFieldInfo2 = MetadataSourceAggregator.GetLPropertyFieldInfo(metaField, frontendLanguage, false);
          MetadataSourceAggregator.TrySetLocalizablePropertyMapping(mapping, context, options, lpropertyFieldInfo2, out mappingConfig);
          propertyConfigurationList.Add(mappingConfig);
        }
      }
      return (IList<PropertyConfiguration>) propertyConfigurationList;
    }

    private static LstringFiedInfo GetLstringFiedInfo(
      PropertyInfo propInfo,
      CultureInfo language,
      bool isMainProperty)
    {
      LstringFiedInfo lstringFiedInfo1 = new LstringFiedInfo();
      lstringFiedInfo1.IsUnicode = true;
      lstringFiedInfo1.IsLong = false;
      lstringFiedInfo1.Language = language;
      lstringFiedInfo1.IsMainProperty = isMainProperty;
      lstringFiedInfo1.IgnoreSplitTables = isMainProperty;
      LstringFiedInfo lstringFiedInfo2 = lstringFiedInfo1;
      string str1 = (string) null;
      string str2 = (string) null;
      if (((IEnumerable<object>) propInfo.GetCustomAttributes(false)).FirstOrDefault<object>((Func<object, bool>) (x => x is LStringPropertyAttribute)) is LStringPropertyAttribute propertyAttribute && propertyAttribute.Transient)
        return (LstringFiedInfo) null;
      if (language == null || language.Equals((object) CultureInfo.InvariantCulture))
      {
        if (propertyAttribute != null)
        {
          if (!propertyAttribute.CreateMonolingualMapping)
            return (LstringFiedInfo) null;
          str1 = propertyAttribute.InvariantCultureField;
          if (!string.IsNullOrEmpty(propertyAttribute.ColumnName))
            lstringFiedInfo2.ColumnName = propertyAttribute.ColumnName;
        }
        else
          str2 = "_";
      }
      if (str1 == null)
      {
        if (str2 == null)
          str2 = "_" + LstringPropertyDescriptor.GetCultureSuffix(language);
        object[] customAttributes = propInfo.GetCustomAttributes(typeof (MetadataMappingAttribute), false);
        if (customAttributes.Length != 0)
        {
          MetadataMappingAttribute mappingAttribute = (MetadataMappingAttribute) customAttributes[0];
          str1 = (string.IsNullOrEmpty(mappingAttribute.FieldName) ? propInfo.Name : mappingAttribute.FieldName) + str2;
          lstringFiedInfo2.IsLong = mappingAttribute.IsLong;
          lstringFiedInfo2.IsUnicode = mappingAttribute.IsUnicode;
          if (!mappingAttribute.IsLong && mappingAttribute.DBLength > 0)
            lstringFiedInfo2.DBLength = new int?(mappingAttribute.DBLength);
        }
        else
          str1 = propInfo.Name + str2;
      }
      lstringFiedInfo2.FieldName = str1;
      lstringFiedInfo2.Extension = str2;
      return lstringFiedInfo2;
    }

    private static LPropertyFieldInfo GetLPropertyFieldInfo(
      MetaField metaField,
      CultureInfo language,
      bool ignoreSplitTables)
    {
      LPropertyFieldInfo lpropertyFieldInfo = new LPropertyFieldInfo()
      {
        Language = language,
        IsMainProperty = false,
        IgnoreSplitTables = ignoreSplitTables,
        DBScale = metaField.DBScale,
        ColumnName = metaField.ColumnName
      };
      string str1 = "_" + LstringPropertyDescriptor.GetCultureSuffix(language);
      if (!lpropertyFieldInfo.ColumnName.IsNullOrEmpty() && !object.Equals((object) CultureInfo.InvariantCulture, (object) language))
        lpropertyFieldInfo.ColumnName += str1;
      string str2 = metaField.FieldName;
      if (!object.Equals((object) language, (object) CultureInfo.InvariantCulture))
        str2 = metaField.FieldName + str1;
      Type type = TypeResolutionService.ResolveType(metaField.ClrType);
      int result;
      if (!string.IsNullOrEmpty(metaField.DBLength) && int.TryParse(metaField.DBLength, out result))
        lpropertyFieldInfo.DBLength = new int?(result);
      lpropertyFieldInfo.ClrType = type;
      lpropertyFieldInfo.FieldName = str2;
      lpropertyFieldInfo.Extension = str1;
      return lpropertyFieldInfo;
    }

    private static LstringFiedInfo GetLstringFiedInfo(
      MetaField field,
      CultureInfo language,
      bool isMainProperty,
      bool ignoreSplitTables)
    {
      LstringFiedInfo lstringFiedInfo1 = new LstringFiedInfo();
      lstringFiedInfo1.IsUnicode = true;
      lstringFiedInfo1.IsLong = false;
      lstringFiedInfo1.Language = language;
      lstringFiedInfo1.IsMainProperty = isMainProperty;
      lstringFiedInfo1.IgnoreSplitTables = ignoreSplitTables;
      LstringFiedInfo lstringFiedInfo2 = lstringFiedInfo1;
      if (language == null || language.Equals((object) CultureInfo.InvariantCulture))
      {
        lstringFiedInfo2.FieldName = field.FieldName;
        if (!string.IsNullOrEmpty(field.ColumnName))
          lstringFiedInfo2.ColumnName = field.ColumnName;
      }
      else
      {
        string str = "_" + LstringPropertyDescriptor.GetCultureSuffix(language);
        lstringFiedInfo2.Extension = str;
        lstringFiedInfo2.FieldName = field.FieldName + str;
      }
      int result;
      if (!string.IsNullOrEmpty(field.DBLength) && int.TryParse(field.DBLength, out result))
        lstringFiedInfo2.DBLength = new int?(result);
      lstringFiedInfo2.DBType = field.DBType;
      return lstringFiedInfo2;
    }

    private static PropertyConfiguration SetLStringFieldMapping(
      MappingConfiguration mapping,
      MetaField metaField,
      CultureInfo language,
      IDatabaseMappingContext context,
      IDatabaseMappingOptions options,
      bool isMainProperty,
      bool? ignoreSplitTables = null)
    {
      LstringFiedInfo lstringFiedInfo = MetadataSourceAggregator.GetLstringFiedInfo(metaField, language, isMainProperty, ignoreSplitTables.HasValue ? ignoreSplitTables.Value : isMainProperty);
      PropertyConfiguration mappingConfig;
      MetadataSourceAggregator.TrySetLocalizablePropertyMapping(mapping, context, options, (LPropertyFieldInfo) lstringFiedInfo, out mappingConfig);
      if (metaField.IsDeleted)
        mappingConfig.Drop<PropertyConfiguration>();
      return mappingConfig;
    }

    private static string GetTableName(
      MetadataManager metadataManager,
      Telerik.Sitefinity.Metadata.Model.MetaType type,
      MetaField field,
      IDatabaseMappingContext context,
      IEnumerable<MetadataMapping> mappings = null)
    {
      MetaTypeAttribute metaTypeAttribute = type.MetaAttributes.FirstOrDefault<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => a.Name == "moduleName"));
      string fieldName = field == null ? "#NA" : field.FieldName;
      string str1 = field == null ? string.Empty : field.FieldName;
      string moduleName = metaTypeAttribute == null ? "#NA" : metaTypeAttribute.Value;
      string str2 = metaTypeAttribute == null ? string.Empty : metaTypeAttribute.Value;
      MetadataMapping metadataMapping = (MetadataMapping) null;
      if (mappings != null)
        metadataMapping = mappings.FirstOrDefault<MetadataMapping>((Func<MetadataMapping, bool>) (m => m.ModuleName == moduleName && m.TypeName == type.ClassName && m.FieldName == fieldName));
      if (metadataMapping == null)
        metadataMapping = metadataManager.GetMetadataMapping(moduleName, type.ClassName, fieldName);
      if (metadataMapping == null)
      {
        int num = 5;
        while (num > 0)
        {
          try
          {
            List<string> list = metadataManager.GetMetadataMappings().Select<MetadataMapping, string>((Expression<Func<MetadataMapping, string>>) (m => m.TableName)).ToList<string>();
            string andUniqueTableName = new BackendNamingHelper(context.DatabaseType).GetShortAndUniqueTableName(string.Format("{0}_{1}_{2}", (object) str2, (object) type.ClassName, (object) str1).ToLower().Trim('_'), (IList<string>) list);
            metadataMapping = metadataManager.CreateMetadataMapping();
            metadataMapping.ModuleName = moduleName;
            metadataMapping.TypeName = type.ClassName;
            metadataMapping.FieldName = fieldName;
            metadataMapping.TableName = andUniqueTableName;
            metadataManager.Provider.CommitTransaction();
            num = 0;
          }
          catch (OptimisticVerificationException ex)
          {
            metadataManager.Provider.RollbackTransaction();
            --num;
          }
        }
      }
      return metadataMapping.TableName;
    }
  }
}
