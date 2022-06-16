// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ABTesting.IABTestClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.ABTesting.Model;

namespace Telerik.Sitefinity.Modules.ABTesting
{
  /// <summary>Exposes an interface of AB tests client API</summary>
  internal interface IABTestClient
  {
    /// <summary>Gets AB test by id</summary>
    /// <param name="id">AB test id</param>
    /// <returns>The created AB test</returns>
    ABTest Get(Guid id);

    /// <summary>Deletes AB test by Id</summary>
    /// <param name="id">Test id</param>
    void Delete(Guid id);

    /// <summary>Returns a collection of AB tests</summary>
    /// <param name="loadOptions">The load options</param>
    /// <returns>Collection of AB tests</returns>
    CollectionResponse<ABTestAll> GetAll(LoadOptions loadOptions);

    /// <summary>
    /// Makes request to check connectivity to Sitefinity Insight. Throws exception if not connected.
    /// </summary>
    /// <exception cref="T:NoConnectionException">When connection fails.</exception>
    void Ping();

    /// <summary>Creates new AB test</summary>
    /// <param name="test">The AB test</param>
    /// <returns>returns the created AB test</returns>
    ABTest Create(ABTest test);

    /// <summary>Updates an AB test</summary>
    /// <param name="test">The AB test</param>
    /// <returns>Returns the updated AB test</returns>
    ABTest Update(ABTest test);

    /// <summary>Gets the API key for site.</summary>
    /// <param name="siteName">Name of the site.</param>
    /// <returns>The API key</returns>
    string GetApiKeyForSite(string siteName);

    /// <summary>Gets the API key for current site.</summary>
    /// <returns>The API key</returns>
    string GetApiKeyForCurrentSite();

    /// <summary>
    /// Gets A/B testing compatible Sitefinity Insight conversions
    /// </summary>
    /// <returns>Collection of compatible Sitefinity Insight conversions</returns>
    IEnumerable<ABTestConversion> GetCompatibleDecConversions();
  }
}
