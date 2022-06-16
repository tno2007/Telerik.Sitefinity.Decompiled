// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.ISitefinityMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Reflection;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>
  /// Provides interface for extending Sitefinity with new mappings
  /// </summary>
  public interface ISitefinityMetadataSource
  {
    /// <summary>Gets the dynamic types.</summary>
    /// <value>The dynamic types.</value>
    DynamicTypeInfo[] DynamicTypes { get; }

    /// <summary>Gets the assemblies used by persistant types.</summary>
    /// <value>The assemblies.</value>
    Assembly[] Assemblies { get; }

    /// <summary>Gets the type of the metadatasource.</summary>
    /// <returns></returns>
    Type GetType();
  }
}
