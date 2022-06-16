// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.IUserPropertiesStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Gives metadata for user profile properties</summary>
  internal interface IUserPropertiesStrategy
  {
    /// <summary>
    /// Returns the profile properties which are mapped to claims
    /// </summary>
    /// <param name="profileType">Profile type</param>
    /// <param name="externalProviderName">External provider name</param>
    /// <returns>Array of string elements</returns>
    IEnumerable<string> GetReadOnlyFields(
      string profileType,
      string externalProviderName);

    /// <summary>
    /// Returns the profile properties which are mapped to claims new
    /// </summary>
    /// <returns>Mapped fields for each profile per provider</returns>
    IDictionary<string, Dictionary<string, List<string>>> GetExternalProvidersMapping();
  }
}
