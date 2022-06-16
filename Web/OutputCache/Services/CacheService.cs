// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Services.CacheService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.OutputCache.Services.Model;

namespace Telerik.Sitefinity.Web.OutputCache.Services
{
  /// <summary>
  /// Service which provides functionality for working with Sitefinity output cache.
  /// </summary>
  [CacheServiceAuthFilter]
  internal class CacheService : Service
  {
    /// <summary>Clear output cache items</summary>
    /// <param name="request">Request object.</param>
    /// <returns>Response object.</returns>
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Post method do not need any parameters and also method should have request object.")]
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Intended")]
    public ClearCacheResponse Post(ClearCacheRequest request)
    {
      ClearCacheResponse clearCacheResponse = new ClearCacheResponse()
      {
        Result = true,
        Message = string.Empty
      };
      try
      {
        OutputCacheWorker.ExpireAllOutputCacheItems();
        this.HandleTrace("CacheService - ClearCache: Successfully cleared output cache items.");
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        this.HandleTrace("CacheService - ClearCache: Exception occurred while clearing output cache items. Check error logs for details.");
        clearCacheResponse.Result = false;
        clearCacheResponse.Message = ex.Message;
      }
      return clearCacheResponse;
    }

    internal void HandleException(Exception ex) => Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);

    internal void HandleTrace(string message) => Log.Write((object) message, ConfigurationPolicy.Trace);
  }
}
