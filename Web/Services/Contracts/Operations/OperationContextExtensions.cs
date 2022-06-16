// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.OperationContextExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Multisite;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations
{
  /// <summary>
  /// Extension method which simplifies working with <see cref="T:Telerik.Sitefinity.Web.Services.Contracts.Operations.OperationContext" />
  /// </summary>
  public static class OperationContextExtensions
  {
    /// <summary>Gets the name of the provider.</summary>
    /// <param name="context">The operation context.</param>
    /// <returns>The provider's name.</returns>
    public static string GetProviderName(this OperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      object providerName = (object) null;
      context.Data.TryGetValue("provider", out providerName);
      return (string) providerName;
    }

    /// <summary>Gets the culture name.</summary>
    /// <param name="context">The operation context.</param>
    /// <returns>The culture name.</returns>
    public static CultureInfo GetCulture(this OperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      object culture = (object) null;
      context.Data.TryGetValue("culture", out culture);
      return (CultureInfo) culture;
    }

    /// <summary>Gets the entity's CLR type.</summary>
    /// <param name="context">The operation context.</param>
    /// <returns>The entity's CLR type.</returns>
    public static Type GetClrType(this OperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      object clrType = (object) null;
      context.Data.TryGetValue("clrType", out clrType);
      return (Type) clrType;
    }

    /// <summary>Gets the query params of the request.</summary>
    /// <param name="context">The operation context.</param>
    /// <returns>The query params of the request.</returns>
    public static IDictionary<string, string> GetQueryParams(
      this OperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      object queryParams = (object) null;
      context.Data.TryGetValue("queryParams", out queryParams);
      return (IDictionary<string, string>) queryParams;
    }

    /// <summary>Gets the target site of the request.</summary>
    /// <param name="context">The operation context.</param>
    /// <returns>The request's target site.</returns>
    public static ISite GetSite(this OperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      object site = (object) null;
      context.Data.TryGetValue("site", out site);
      return (ISite) site;
    }

    /// <summary>Gets the ID of the target entity.</summary>
    /// <param name="context">The operation context.</param>
    /// <returns>The ID of the target entity.</returns>
    public static string GetKey(this OperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      object obj = (object) null;
      context.Data.TryGetValue("key", out obj);
      return obj?.ToString();
    }

    internal static IItemStrategy GetStrategy(this OperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      object strategy = (object) null;
      context.Data.TryGetValue("strategy", out strategy);
      return (IItemStrategy) strategy;
    }

    internal static IQueryable GetQuery(this OperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      object obj = (object) null;
      context.Data.TryGetValue("query", out obj);
      return ((Lazy<IQueryable>) obj).Value;
    }

    internal static IQueryable GetFilteredQuery(this OperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      object obj = (object) null;
      context.Data.TryGetValue("filteredQuery", out obj);
      return ((Lazy<IQueryable>) obj).Value;
    }
  }
}
