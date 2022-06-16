// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.DataResolving.DataResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.DataResolving
{
  /// <summary>Resolves data properties for data binding.</summary>
  public static class DataResolver
  {
    private static IDictionary<string, IDataResolver> resolversCache = SystemManager.CreateStaticCache<string, IDataResolver>();

    /// <summary>Resolves the specified data item.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="resolverName">Name of the resolver.</param>
    /// <returns></returns>
    public static string Resolve(object dataItem, string resolverName) => DataResolver.Resolve(dataItem, resolverName, (string) null, (string) null);

    /// <summary>Resolves the specified data item.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="resolverName">Name of the resolver.</param>
    /// <param name="format">The format.</param>
    /// <returns></returns>
    public static string Resolve(object dataItem, string resolverName, string format) => DataResolver.Resolve(dataItem, resolverName, format, (string) null);

    /// <summary>Resolves the specified data item.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="resolverName">Name of the resolver.</param>
    /// <param name="format">The format.</param>
    /// <param name="args">The args.</param>
    /// <returns></returns>
    public static string Resolve(object dataItem, string resolverName, string format, string args) => DataResolver.GetResolver(resolverName).Resolve(dataItem, format, args);

    /// <summary>Resolves the current data bound item.</summary>
    /// <param name="control">The control.</param>
    /// <param name="resolverName">Name of the resolver.</param>
    /// <returns></returns>
    public static string ResolveData(this Control control, string resolverName) => control.ResolveData(resolverName, (string) null, (string) null);

    /// <summary>Resolves the current data bound item.</summary>
    /// <param name="control">The control.</param>
    /// <param name="resolverName">Name of the resolver.</param>
    /// <param name="formatExpression">The format expression.</param>
    /// <returns></returns>
    public static string ResolveData(
      this Control control,
      string resolverName,
      string formatExpression)
    {
      return control.ResolveData(resolverName, formatExpression, (string) null);
    }

    /// <summary>Resolves the current data bound item.</summary>
    /// <param name="control">The control.</param>
    /// <param name="resolverName">Name of the resolver.</param>
    /// <param name="formatExpression">The format expression.</param>
    /// <returns></returns>
    public static string ResolveData(
      this Control control,
      string resolverName,
      string formatExpression,
      string args)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      if (control.Page == null)
        throw new InvalidOperationException("Cannot bind data before the control is added to the Page controls collection.");
      return DataResolver.Resolve(control.Page.GetDataItem(), resolverName, formatExpression, args);
    }

    /// <summary>Resolves the current data bound item.</summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public static IDataResolver GetResolver(string name)
    {
      IDataResolver resolver1;
      if (!DataResolver.resolversCache.TryGetValue(name, out resolver1))
      {
        lock (DataResolver.resolversCache)
        {
          if (!DataResolver.resolversCache.TryGetValue(name, out resolver1))
          {
            DataConfig dataConfig = Config.Get<DataConfig>();
            if (dataConfig.Resolvers.ContainsKey(name))
            {
              DataProviderSettings resolver2 = dataConfig.Resolvers[name];
              resolver1 = (IDataResolver) ObjectFactory.Resolve(resolver2.ProviderType);
              resolver1.Initialize(resolver2.Parameters);
            }
            else
              resolver1 = ObjectFactory.Resolve<IDataResolver>(name);
            DataResolver.resolversCache.Add(name, resolver1);
          }
        }
      }
      return resolver1;
    }
  }
}
