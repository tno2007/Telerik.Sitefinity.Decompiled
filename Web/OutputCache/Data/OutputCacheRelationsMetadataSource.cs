// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Reflection;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.OpenAccessMapping;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Web.OutputCache.Data
{
  /// <summary>Defines the output cache relations mappings</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Data.OA.SitefinityMetadataSourceBase" />
  internal class OutputCacheRelationsMetadataSource : SitefinityMetadataSourceBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsMetadataSource" /> class.
    /// </summary>
    public OutputCacheRelationsMetadataSource()
      : base((IDatabaseMappingContext) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsMetadataSource" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public OutputCacheRelationsMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    /// <summary>
    /// Gets the assemblies of the types mapped in this MetadataSource.
    /// </summary>
    /// <value>The assemblies.</value>
    public override Assembly[] Assemblies => new Assembly[1]
    {
      typeof (OutputCacheRelationsFluentMapping).Assembly
    };

    /// <summary>Gets the dynamic types mapped in this MetadataSource.</summary>
    /// <value>The dynamic types.</value>
    public override DynamicTypeInfo[] DynamicTypes => (DynamicTypeInfo[]) null;

    /// <summary>
    /// Gets a value indicating whether [support dynamic types].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [support dynamic types]; otherwise, <c>false</c>.
    /// </value>
    protected override bool SupportDynamicTypes => false;

    /// <summary>Builds the custom mappings.</summary>
    /// <returns>The mappings for output cache relations.</returns>
    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings() => (IList<IOpenAccessFluentMapping>) new List<IOpenAccessFluentMapping>()
    {
      (IOpenAccessFluentMapping) new OutputCacheRelationsFluentMapping(this.Context),
      (IOpenAccessFluentMapping) new SyncLockFluentMapping(this.Context)
    };
  }
}
