// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Data.AccessTokenMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ProtectionShield.Model;

namespace Telerik.Sitefinity.ProtectionShield.Data
{
  /// <summary>Access tokens metadata source</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Data.OA.SitefinityMetadataSourceBase" />
  internal class AccessTokenMetadataSource : SitefinityMetadataSourceBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Data.AccessTokenMetadataSource" /> class.
    /// </summary>
    public AccessTokenMetadataSource()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Data.AccessTokenMetadataSource" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public AccessTokenMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    /// <inheritdoc />
    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings() => (IList<IOpenAccessFluentMapping>) new List<IOpenAccessFluentMapping>()
    {
      (IOpenAccessFluentMapping) new AccessTokenFluentMapping(this.Context)
    };
  }
}
