// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Utils.LandingPageResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Extensions;
using Telerik.Sitefinity.Modules.Pages.Utils;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Utils
{
  /// <summary>
  /// Helper class to resolve the backend home page for a given Sitefinity content type
  /// </summary>
  internal class LandingPageResolver
  {
    /// <summary>
    /// Resolves the landing backend page id for the given Sitefinity content type full name or returns null if no such is found or the type is not supported or is not Sitefinity content type.
    /// </summary>
    /// <param name="typeFullName">The CLR full name of the type</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> id or null.</returns>
    public Guid ResolveTypeLandingPage(string typeFullName)
    {
      Guid empty = Guid.Empty;
      IModule module = new ModuleResolver().ResolveModule(typeFullName);
      if (module == null)
        return empty;
      Type key = TypeResolutionService.ResolveType(typeFullName);
      IDictionary<Type, Guid> landingPageMappings = module.GetTypeLandingPageMappings();
      if (landingPageMappings.ContainsKey(key))
        empty = landingPageMappings[key];
      return empty;
    }

    /// <summary>
    /// Resolves the landing backend page url for the given Sitefinity content type full name or returns null if no such is found or the type is not supported or is not Sitefinity content type.
    /// </summary>
    /// <param name="typeFullName">The CLR full name of the type</param>
    /// <returns>The url string or null.</returns>
    public string ResolveTypeLandingUrl(string typeFullName) => PagePathUtils.ResolveBackendPageNodeUrl(this.ResolveTypeLandingPage(typeFullName));
  }
}
