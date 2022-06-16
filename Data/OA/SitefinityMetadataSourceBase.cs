// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.SitefinityMetadataSourceBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>
  /// This class provides additional functionality over the OpenAccess FluentMetadataSource which allows to
  /// alter mappings according to the targeted database(using IOAFluentMappingContext).
  /// It also automatically adds any registered artificial fields for the mapped classes
  /// </summary>
  public abstract class SitefinityMetadataSourceBase : 
    FluentMetadataSource,
    ISitefinityMetadataSource
  {
    private Assembly[] assemblies;
    private IList<MappingConfiguration> mapConfigs;
    private DatabaseMappingOptions mappingOptions;
    private DynamicTypeInfo[] dynamicTypes;
    private readonly IDatabaseMappingContext context;
    private MetadataContainer model;
    private IList<IOpenAccessFluentMapping> customMappings;
    private MetadataManager metadataManager;
    private const string TransactionName = "Sf_MetadataSource_Transaction";

    protected SitefinityMetadataSourceBase()
      : this((IDatabaseMappingContext) null)
    {
    }

    protected SitefinityMetadataSourceBase(IDatabaseMappingContext context) => this.context = context;

    /// <summary>Gets the mapping context in runtime.</summary>
    /// <value>The context.</value>
    public IDatabaseMappingContext Context => this.context;

    /// <summary>Gets the dynamic types mapped in this MetadataSource.</summary>
    /// <value>The dynamic types.</value>
    public virtual DynamicTypeInfo[] DynamicTypes
    {
      get
      {
        if (this.dynamicTypes == null)
          this.CreateModel();
        return this.dynamicTypes;
      }
    }

    /// <summary>
    /// Gets the assemblies of the types mapped in this MetadataSource.
    /// </summary>
    /// <value>The assemblies.</value>
    public virtual Assembly[] Assemblies
    {
      get
      {
        if (this.assemblies == null)
        {
          List<Assembly> assemblyList = new List<Assembly>();
          foreach (MappingConfiguration mappingConfiguration in (IEnumerable<MappingConfiguration>) this.CustomMappingConfiguration)
          {
            Assembly assembly = mappingConfiguration.ConfiguredType.Assembly;
            if (!assemblyList.Contains(assembly))
              assemblyList.Add(assembly);
          }
          assemblyList.Sort((Comparison<Assembly>) ((x, y) => x.FullName.CompareTo(y.FullName)));
          this.assemblies = assemblyList.ToArray();
        }
        return ((IEnumerable<Assembly>) this.assemblies).ToArray<Assembly>();
      }
    }

    protected virtual bool SupportDynamicTypes => true;

    private MetadataManager MetadataManager
    {
      get
      {
        if (this.metadataManager == null)
          this.metadataManager = MetadataManager.GetManager((string) null, "Sf_MetadataSource_Transaction");
        return this.metadataManager;
      }
    }

    protected internal virtual IList<IOpenAccessFluentMapping> CustomMappings
    {
      get
      {
        if (this.customMappings == null)
          this.customMappings = this.BuildCustomMappings();
        return this.customMappings;
      }
    }

    internal IList<MappingConfiguration> CustomMappingConfiguration
    {
      get
      {
        if (this.mapConfigs == null)
        {
          this.mapConfigs = (IList<MappingConfiguration>) new List<MappingConfiguration>();
          foreach (IOpenAccessFluentMapping customMapping in (IEnumerable<IOpenAccessFluentMapping>) this.CustomMappings)
          {
            IList<MappingConfiguration> mapping = customMapping.GetMapping();
            if (mapping != null)
            {
              foreach (MappingConfiguration mappingConfiguration in (IEnumerable<MappingConfiguration>) mapping)
                this.mapConfigs.Add(mappingConfiguration);
            }
          }
        }
        return this.mapConfigs;
      }
    }

    internal IDatabaseMappingOptions MappingOptions
    {
      get
      {
        if (this.mappingOptions == null)
        {
          this.mappingOptions = new DatabaseMappingOptions();
          this.mappingOptions.LoadDefaults();
        }
        return (IDatabaseMappingOptions) this.mappingOptions;
      }
    }

    protected override IList<MappingConfiguration> PrepareMapping()
    {
      List<MappingConfiguration> source = new List<MappingConfiguration>((IEnumerable<MappingConfiguration>) this.CustomMappingConfiguration);
      if (this.Context != null && this.SupportDynamicTypes)
      {
        List<MappingConfiguration> mappingConfigurationList = new List<MappingConfiguration>();
        List<DynamicTypeInfo> dynamicTypeInfoList = new List<DynamicTypeInfo>();
        List<Assembly> assemblyList = new List<Assembly>();
        IDictionary<Type, IEnumerable<LstringFiedInfo>> dictionary = (IDictionary<Type, IEnumerable<LstringFiedInfo>>) null;
        IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType> metaTypes = MetadataSourceAggregator.GetMetaTypes(this.MetadataManager);
        foreach (MappingConfiguration mappingConfiguration in source)
        {
          MappingConfiguration mapping = mappingConfiguration;
          Type configuredType = mapping.ConfiguredType;
          TypeCacheStrategy cacheStrategy;
          if (this.MappingOptions.L2CacheTypeMappings.TryGetValue(configuredType.FullName, out cacheStrategy))
            this.SetCacheStrategy(mapping, cacheStrategy);
          if (typeof (IDynamicFieldsContainer).IsAssignableFrom(configuredType))
          {
            DynamicTypeInfo dynamicTypeInfo = new DynamicTypeInfo()
            {
              Name = configuredType.FullName,
              IsDeleted = false
            };
            Telerik.Sitefinity.Metadata.Model.MetaType metaType = metaTypes.FirstOrDefault<Telerik.Sitefinity.Metadata.Model.MetaType>((Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>) (x => x.Namespace == mapping.ConfiguredType.Namespace && x.ClassName == mapping.ConfiguredType.Name));
            if (metaType != null)
            {
              dynamicTypeInfo.MetaTypeId = metaType.Id;
              dynamicTypeInfo.IsDeleted = metaType.IsDeleted;
            }
            dynamicTypeInfoList.Add(dynamicTypeInfo);
            IEnumerable<LstringFiedInfo> notAddedMapppings;
            MetadataSourceAggregator.AddArtificialFields(metaType, this.MetadataManager, mapping, configuredType, this.Context, this.MappingOptions, out notAddedMapppings);
            if (notAddedMapppings != null)
            {
              if (dictionary == null)
                dictionary = (IDictionary<Type, IEnumerable<LstringFiedInfo>>) new Dictionary<Type, IEnumerable<LstringFiedInfo>>();
              dictionary.Add(configuredType, notAddedMapppings);
            }
            DynamicTypeInfo[] collection = MetadataSourceAggregator.AddArtificialTypes(this.MetadataManager, metaTypes, (IList<MappingConfiguration>) mappingConfigurationList, configuredType, this.Context, this.MappingOptions);
            if (collection != null)
              dynamicTypeInfoList.AddRange((IEnumerable<DynamicTypeInfo>) collection);
          }
        }
        if (dictionary != null)
        {
          foreach (KeyValuePair<Type, IEnumerable<LstringFiedInfo>> keyValuePair in (IEnumerable<KeyValuePair<Type, IEnumerable<LstringFiedInfo>>>) dictionary)
          {
            KeyValuePair<Type, IEnumerable<LstringFiedInfo>> pair = keyValuePair;
            foreach (MappingConfiguration mapping in source.Where<MappingConfiguration>((Func<MappingConfiguration, bool>) (m => pair.Key.IsAssignableFrom(m.ConfiguredType) && !pair.Key.Equals(m.ConfiguredType))))
            {
              foreach (LstringFiedInfo field in pair.Value)
              {
                if (!MetadataSourceAggregator.TrySetLocalizablePropertyMapping(mapping, this.Context, this.MappingOptions, (LPropertyFieldInfo) field, out PropertyConfiguration _))
                  Log.Write((object) "Lstring property mapping was not added for field: {0} and type: {1}".Arrange((object) field.FieldName, (object) mapping.ConfiguredType.FullName), ConfigurationPolicy.Trace);
              }
            }
          }
        }
        if (mappingConfigurationList.Count > 0)
          source.AddRange((IEnumerable<MappingConfiguration>) mappingConfigurationList);
        this.dynamicTypes = dynamicTypeInfoList.ToArray();
        if (this.metadataManager != null)
          TransactionManager.DisposeTransaction("Sf_MetadataSource_Transaction");
      }
      return (IList<MappingConfiguration>) source;
    }

    private bool TryParseToOaStrategy(TypeCacheStrategy strategy, out CacheStrategy oaStrategy) => Enum.TryParse<CacheStrategy>(Enum.GetName(typeof (TypeCacheStrategy), (object) strategy), true, out oaStrategy);

    private void SetCacheStrategy(MappingConfiguration mapConfig, TypeCacheStrategy cacheStrategy)
    {
      CacheStrategy oaStrategy;
      if (!this.TryParseToOaStrategy(cacheStrategy, out oaStrategy))
        return;
      typeof (MappingConfiguration).GetField(nameof (cacheStrategy), BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) mapConfig, (object) oaStrategy);
    }

    protected override MetadataContainer CreateModel()
    {
      if (this.model == null)
      {
        lock (this)
        {
          if (this.model == null)
          {
            try
            {
              if (AppDomain.CurrentDomain.FriendlyName != "Enhancer.Temporary")
              {
                using (new MethodPerformanceRegion("Prepare mappings for {0}".Arrange((object) this.GetType().Name)))
                  this.model = base.CreateModel();
              }
              else
                this.model = base.CreateModel();
            }
            catch (ReflectionTypeLoadException ex)
            {
              if (((IEnumerable<Exception>) ex.LoaderExceptions).Count<Exception>() > 0)
                throw ex.LoaderExceptions[0];
              throw ex;
            }
            foreach (IOpenAccessFluentMapping customMapping in (IEnumerable<IOpenAccessFluentMapping>) this.CustomMappings)
              customMapping.ModifyModel(this.model);
          }
        }
      }
      return this.model;
    }

    protected abstract IList<IOpenAccessFluentMapping> BuildCustomMappings();

    internal void Combine(SitefinityMetadataSourceBase parasite)
    {
      IList<IOpenAccessFluentMapping> customMappings = this.CustomMappings;
      foreach (IOpenAccessFluentMapping customMapping in (IEnumerable<IOpenAccessFluentMapping>) parasite.CustomMappings)
        customMappings.Add(customMapping);
    }

    Type ISitefinityMetadataSource.GetType() => this.GetType();
  }
}
