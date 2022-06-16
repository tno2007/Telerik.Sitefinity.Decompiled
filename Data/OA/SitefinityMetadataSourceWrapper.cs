// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.SitefinityMetadataSourceWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.OA
{
  public class SitefinityMetadataSourceWrapper : FluentMetadataSource, ISitefinityMetadataSource
  {
    private MetadataContainer model;
    private List<MappingConfiguration> mappings;
    private MetadataContainer existingContainer;
    private IDatabaseMappingContext context;
    private DynamicTypeInfo[] dynamicTypes;
    private Assembly[] assemblies;
    private MetadataManager metadataManager;
    private Type originalType;

    public SitefinityMetadataSourceWrapper(
      MetadataSource metadataSource,
      IDatabaseMappingContext context)
      : this(metadataSource.GetModel(), context, metadataSource.GetType())
    {
    }

    public SitefinityMetadataSourceWrapper(
      MetadataContainer existingContainer,
      IDatabaseMappingContext context)
      : this(existingContainer, context, existingContainer.GetType())
    {
    }

    internal SitefinityMetadataSourceWrapper(
      MetadataContainer existingContainer,
      IDatabaseMappingContext context,
      Type originalType)
    {
      this.originalType = originalType;
      this.existingContainer = existingContainer;
      this.context = context;
      this.Initialize();
    }

    Type ISitefinityMetadataSource.GetType() => this.originalType;

    /// <summary>Gets the dynamic types mapped in this MetadataSource.</summary>
    /// <value>The dynamic types.</value>
    public DynamicTypeInfo[] DynamicTypes => this.dynamicTypes;

    /// <summary>
    /// Gets the assemblies of the types mapped in this MetadataSource.
    /// </summary>
    /// <value>The assemblies.</value>
    public Assembly[] Assemblies => this.assemblies;

    private MetadataManager MetadataManager
    {
      get
      {
        if (this.metadataManager == null)
          this.metadataManager = MetadataManager.GetManager();
        return this.metadataManager;
      }
    }

    private void Initialize()
    {
      this.mappings = new List<MappingConfiguration>();
      List<DynamicTypeInfo> dynamicTypeInfoList = new List<DynamicTypeInfo>();
      List<Assembly> assemblyList = new List<Assembly>();
      foreach (MetaPersistentType persistentType in (IEnumerable<MetaPersistentType>) this.existingContainer.PersistentTypes)
      {
        Type type = TypeResolutionService.ResolveType(persistentType.FullName);
        if (typeof (IDynamicFieldsContainer).IsAssignableFrom(type))
        {
          dynamicTypeInfoList.Add(new DynamicTypeInfo()
          {
            Name = type.FullName
          });
          if (!persistentType.IsArtificial)
          {
            MappingConfiguration mapping = new MappingConfiguration(type.Name, type.Namespace);
            string str = persistentType.Table == null ? (string) null : persistentType.Table.Name;
            mapping.MapType().Inheritance(persistentType.InheritanceStrategy).ToTable((TableName) str);
            bool flag = MetadataSourceAggregator.AddArtificialFields(this.MetadataManager, mapping, type, this.context);
            List<MappingConfiguration> mappingConfigurationList = new List<MappingConfiguration>();
            DynamicTypeInfo[] collection = MetadataSourceAggregator.AddArtificialTypes(this.MetadataManager, (IList<MappingConfiguration>) mappingConfigurationList, type, this.context);
            if (collection != null)
            {
              this.mappings.AddRange((IEnumerable<MappingConfiguration>) mappingConfigurationList);
              dynamicTypeInfoList.AddRange((IEnumerable<DynamicTypeInfo>) collection);
              flag = true;
            }
            if (flag)
              this.mappings.Add(mapping);
          }
          else
            continue;
        }
        if (!assemblyList.Contains(type.Assembly))
          assemblyList.Add(type.Assembly);
      }
      assemblyList.Sort((Comparison<Assembly>) ((a1, a2) => a1.FullName.CompareTo(a2.FullName)));
      this.assemblies = assemblyList.ToArray();
      this.dynamicTypes = dynamicTypeInfoList.ToArray();
    }

    protected override IList<MappingConfiguration> PrepareMapping() => (IList<MappingConfiguration>) this.mappings;

    protected override MetadataContainer CreateModel()
    {
      if (this.model == null)
      {
        lock (this)
        {
          if (this.model == null)
            this.model = this.mappings.Count <= 0 ? this.existingContainer : new AggregateMetadataSource((MetadataSource) new StaticMetadataSource(this.existingContainer), (MetadataSource) new StaticMetadataSource(base.CreateModel()), AggregationOptions.Late).GetModel();
        }
      }
      return this.model;
    }
  }
}
