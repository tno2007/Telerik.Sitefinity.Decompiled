// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.Filters.EnableCacheAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;

namespace Telerik.Sitefinity.Services.ServiceStack.Filters
{
  /// <summary>
  /// Adding this filter to your ServiceStack service class will enable output cache for it.
  /// </summary>
  internal class EnableCacheAttribute : 
    Attribute,
    IHasRequestFilter,
    IRequestFilterBase,
    IHasResponseFilter,
    IResponseFilterBase
  {
    /// <inheritdoc />
    IRequestFilterBase IRequestFilterBase.Copy() => (IRequestFilterBase) this.MemberwiseClone();

    /// <inheritdoc />
    int IRequestFilterBase.Priority => -1;

    /// <inheritdoc />
    void IHasRequestFilter.RequestFilter(
      IRequest req,
      IResponse res,
      object requestDto)
    {
      this.RequestDto = requestDto;
      this.InitOutputCache();
    }

    /// <inheritdoc />
    IResponseFilterBase IResponseFilterBase.Copy() => (IResponseFilterBase) this.MemberwiseClone();

    /// <inheritdoc />
    int IResponseFilterBase.Priority => -1;

    /// <inheritdoc />
    void IHasResponseFilter.ResponseFilter(
      IRequest req,
      IResponse res,
      object response)
    {
      this.SetCacheDependencies();
    }

    /// <summary>Gets or sets the request DTO.</summary>
    /// <value>The request DTO.</value>
    protected virtual object RequestDto { get; set; }

    /// <summary>Initializes the output cache.</summary>
    /// <exception cref="T:System.ArgumentException">Invalid output cache profile specified: \{0}\..Arrange(config.CacheSettings.DefaultProfile)</exception>
    protected virtual void InitOutputCache()
    {
      SystemConfig systemConfig = Config.Get<SystemConfig>();
      if (systemConfig.CacheSettings.EnableOutputCache)
      {
        OutputCacheProfileElement profile;
        if (!systemConfig.CacheSettings.Profiles.TryGetValue(systemConfig.CacheSettings.DefaultProfile, out profile))
          throw new ArgumentException("Invalid output cache profile specified: \"{0}\".".Arrange((object) systemConfig.CacheSettings.DefaultProfile));
        PageRouteHandler pageRouteHandler = ObjectFactory.Resolve<PageRouteHandler>();
        if (pageRouteHandler == null || !pageRouteHandler.ApplyServerCache(SystemManager.CurrentHttpContext, profile, (PageSiteNode) null))
          return;
        SystemManager.CurrentHttpContext.Response.Cache.AddValidationCallback(new HttpCacheValidateHandler(this.ValidateCacheOutput), this.RequestDto);
        SystemManager.CurrentHttpContext.Items[(object) PageRouteHandler.AddCacheDependencies] = (object) true;
      }
      else
        SystemManager.CurrentHttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    /// <summary>Sets the cache dependencies.</summary>
    protected virtual void SetCacheDependencies()
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      object obj = currentHttpContext.Items[(object) PageRouteHandler.AddCacheDependencies];
      if (obj == null || !(obj.ToString().ToLower() == "true") || !(this.RequestDto is IHasCacheDependency))
        return;
      IList<CacheDependencyKey> dependencyObjects = ((IHasCacheDependency) this.RequestDto).GetCacheDependencyObjects();
      if (dependencyObjects.Count <= 0)
        return;
      CompositeCacheDependency compositeCacheDependency = new CompositeCacheDependency();
      compositeCacheDependency.AddCacheDependencyKeys(dependencyObjects);
      currentHttpContext.Response.AddCacheDependency((System.Web.Caching.CacheDependency) compositeCacheDependency);
    }

    /// <summary>Validates the cache output.</summary>
    /// <param name="context">The context.</param>
    /// <param name="data">The data.</param>
    /// <param name="status">The status.</param>
    protected virtual void ValidateCacheOutput(
      HttpContext context,
      object data,
      ref HttpValidationStatus status)
    {
      status = HttpValidationStatus.Valid;
    }
  }
}
