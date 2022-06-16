// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Res
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Helper class for retrieving localizable resources.</summary>
  public static class Res
  {
    private static CultureInfo[] siteUiCultures;
    private static CultureInfo[] siteCultures;
    private static ResourceManager instance = new ResourceManager();

    /// <summary>Gets the name of default provider for this manager.</summary>
    public static string DefaultProviderName => Config.Get<ResourcesConfig>().DefaultProvider;

    /// <summary>
    /// Gets a collection of defined <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /> objects.
    /// </summary>
    public static ProvidersCollection<ResourceDataProvider> DataProviders => Res.instance.StaticProviders;

    /// <summary>Gets the current configured backend culture.</summary>
    /// <value>The current configured backend culture.</value>
    public static CultureInfo CurrentBackendCulture => SystemManager.CurrentContext.BackendCulture;

    /// <summary>Gets the current default frontend culture.</summary>
    /// <value>The current configured backend culture.</value>
    [Obsolete("Use SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage instead.")]
    public static CultureInfo CurrentFrontendCulture => SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static ResourceManager GetManager() => ManagerBase<ResourceDataProvider>.GetManager<ResourceManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static ResourceManager GetManager(string providerName) => ManagerBase<ResourceDataProvider>.GetManager<ResourceManager>(providerName);

    /// <summary>Resolves the localized value.</summary>
    /// <param name="resourceClass">The resource class.</param>
    /// <param name="resourceKeyValue">The resource key value.</param>
    /// <returns></returns>
    public static string ResolveLocalizedValue(string resourceClass, string resourceKeyValue) => Res.ResolveLocalizedValue(resourceClass, resourceKeyValue, (CultureInfo) null);

    /// <summary>Resolves the localized value.</summary>
    /// <param name="resourceClass">The resource class.</param>
    /// <param name="resourceKeyValue">The resource key value.</param>
    /// <returns></returns>
    public static string ResolveLocalizedValue(
      string resourceClass,
      string resourceKeyValue,
      CultureInfo language)
    {
      if (string.IsNullOrEmpty(resourceClass))
        return resourceKeyValue;
      return string.IsNullOrEmpty(resourceKeyValue) ? "" : Res.Get(resourceClass, resourceKeyValue, language, true, false) ?? string.Format("Invalid resource:{0}.{1}", (object) resourceClass, (object) resourceKeyValue);
    }

    [Obsolete("Use Res.Expression method instead.")]
    public static string FormatResourceValue(string resourceId, string resourceKey) => "$Resources: " + resourceId + "," + resourceKey;

    /// <summary>
    /// Returns an ASP.NET resource expression, in the <c>$Resources: class, key</c> format.
    /// </summary>
    /// <param name="resourceClassId">The name of the resource class.</param>
    /// <param name="resourceKey">The resource key.</param>
    public static string Expression(string resourceClassId, string resourceKey) => "$Resources: " + resourceClassId + "," + resourceKey;

    /// <summary>
    /// Determines whether the specified value is a valid resource expression.
    /// </summary>
    /// <param name="resVal">The resource value.</param>
    /// <returns></returns>
    internal static bool IsValidExpression(string resVal) => !resVal.IsNullOrEmpty() && resVal.TrimStart().StartsWith("$Resources:") && resVal.Contains<char>(',');

    /// <summary>Gets the evaled resource value.</summary>
    /// <param name="resVal">The resource value.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="fallback">if set to <c>true</c> [fallback].</param>
    /// <returns>The evaluated string</returns>
    internal static string GetEvaledExpression(string resVal, CultureInfo culture = null, bool fallback = false)
    {
      if (!Res.IsValidExpression(resVal))
        return resVal;
      string[] strArray = resVal.Trim().Replace("$Resources:", "").Split(',');
      return Res.Get(strArray[0].Trim(), strArray[1].Trim(), culture, fallback, false);
    }

    /// <summary>
    /// Returns an ASP.NET resource expression, in the <c>$Resources: class, key</c> format.
    /// </summary>
    /// <typeparam name="TResourceClass">The resource class.</typeparam>
    /// <param name="resourceKey">The resource key.</param>
    internal static string Expression<TResourceClass>(string resourceKey) where TResourceClass : Resource => Res.Expression(typeof (TResourceClass).Name, resourceKey);

    /// <summary>
    /// Gets resource class for managing localizable strings for the current culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <typeparam name="T">The type of the resource class.</typeparam>
    /// <returns>An instance of the resource class.</returns>
    public static T Get<T>() where T : Resource, new() => ObjectFactory.Resolve<T>(typeof (T).Name);

    /// <summary>
    /// Gets localized string for the provided key using the current culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <typeparam name="T">The type of the resource class.</typeparam>
    /// <param name="key">The key for the resource entry.</param>
    /// <returns>The localized string.</returns>
    public static string Get<T>(string key) where T : Resource, new() => ObjectFactory.Resolve<T>(typeof (T).Name).Get(key);

    /// <summary>
    /// Gets localized string for the provided key using the current culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <typeparam name="T">The type of the resource class.</typeparam>
    /// <param name="key">The key for the resource entry.</param>
    /// <param name="args">An <see cref="T:System.Object" /> to format.</param>
    /// <returns>The localized string.</returns>
    public static string Get<T>(string key, params object[] args) where T : Resource, new() => ObjectFactory.Resolve<T>(typeof (T).Name).Get(key, args);

    /// <summary>
    /// Gets localized string for the provided key using the specified culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <typeparam name="T">The type of the resource class.</typeparam>
    /// <param name="key">The key for the resource entry.</param>
    /// <param name="culture">The culture for the resource.</param>
    /// <returns>The localized string.</returns>
    public static string Get<T>(string key, CultureInfo culture) where T : Resource, new() => ObjectFactory.Resolve<T>(typeof (T).Name).Get(key, culture);

    /// <summary>
    /// Gets localized string for the provided key using the specified culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <typeparam name="T">The type of the resource class.</typeparam>
    /// <param name="key">The key for the resource entry.</param>
    /// <param name="culture">The culture for the resource.</param>
    /// <param name="args">An <see cref="T:System.Object" /> to format.</param>
    /// <returns>The localized string.</returns>
    public static string Get<T>(string key, CultureInfo culture, params object[] args) where T : Resource, new() => ObjectFactory.Resolve<T>(typeof (T).Name).Get(key, culture, args);

    /// <summary>
    /// Gets localized string for the provided key form the specified resource using the current culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <param name="type">The type of the resource class.</param>
    /// <param name="key">The key for the resource entry.</param>
    /// <returns>The localized string.</returns>
    public static string Get(Type type, string key) => ((Resource) ObjectFactory.Resolve(type, type.Name)).Get(key);

    /// <summary>
    /// Gets localized string for the provided key form the specified resource using the current culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <param name="type">The type of the resource class.</param>
    /// <param name="key">The key for the resource entry.</param>
    /// <param name="args">An <see cref="T:System.Object" /> to format.</param>
    /// <returns>The localized string.</returns>
    public static string Get(Type type, string key, params object[] args) => ((Resource) ObjectFactory.Resolve(type, type.Name)).Get(key, args);

    /// <summary>
    /// Gets localized string for the provided key form the specified resource using the specified culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <param name="type">The type of the resource class.</param>
    /// <param name="key">The key for the resource entry.</param>
    /// <param name="culture">The culture for the resource.</param>
    /// <returns>The localized string.</returns>
    public static string Get(Type type, string key, CultureInfo culture) => ((Resource) ObjectFactory.Resolve(type, type.Name)).Get(key, culture);

    /// <summary>
    /// Gets localized string for the provided key form the specified resource using the specified culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <param name="type">The type of the resource class.</param>
    /// <param name="key">The key for the resource entry.</param>
    /// <param name="culture">The culture for the resource.</param>
    /// <param name="args">An <see cref="T:System.Object" /> to format.</param>
    /// <returns>The localized string.</returns>
    public static string Get(Type type, string key, CultureInfo culture, params object[] args) => ((Resource) ObjectFactory.Resolve(type, type.Name)).Get(key, culture, args);

    /// <summary>
    /// Gets localized string for the provided key form the specified resource using the current culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <param name="classId">The class ID of the resource.</param>
    /// <param name="key">The key for the resource entry.</param>
    /// <returns>The localized string.</returns>
    public static string Get(string classId, string key) => Res.Get(classId, key, (CultureInfo) null);

    /// <summary>
    /// Gets localized string for the provided key form the specified resource using the specified culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <param name="classId">The class ID of the resource.</param>
    /// <param name="key">The key for the resource entry.</param>
    /// <param name="culture">The culture for the resource.</param>
    /// <returns>The localized string.</returns>
    public static string Get(string classId, string key, CultureInfo culture) => Res.Get(classId, key, culture, true, true);

    /// <summary>
    /// Gets localized string for the provided key form the specified resource using the specified culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <param name="classId">The class ID of the resource.</param>
    /// <param name="key">The key for the resource entry.</param>
    /// <param name="culture">The culture for the resource.</param>
    /// <param name="fallback">if set to <c>true</c> [fallback].</param>
    /// <param name="throws">if set to <c>true</c> an exception will be thrown if either the class ID or the key is not found.</param>
    /// <returns>The localized string.</returns>
    public static string Get(
      string classId,
      string key,
      CultureInfo culture,
      bool fallback,
      bool throws)
    {
      if (Res.instance == null)
        return string.Empty;
      PropertyCollection properties = Res.GetProperties(classId, false);
      return properties != null && properties.Contains(key) ? Resource.GetString(Res.instance.Provider, classId, properties[key], culture, fallback) : Resource.GetString(Res.instance.Provider, classId, key, culture, fallback, throws);
    }

    /// <summary>Tries the get a resource value by a given key.</summary>
    /// <param name="classId">The class identifier.</param>
    /// <param name="key">The key.</param>
    /// <param name="resourceValue">The resource value.</param>
    /// <returns></returns>
    public static bool TryGet(string classId, string key, out string resourceValue) => Res.TryGet(classId, key, CultureInfo.CurrentCulture, out resourceValue);

    /// <summary>
    /// Tries the get a resource value by a given key and culture.
    /// </summary>
    /// <param name="classId">The class identifier.</param>
    /// <param name="key">The key.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="resourceValue">The resource value.</param>
    /// <returns></returns>
    public static bool TryGet(
      string classId,
      string key,
      CultureInfo culture,
      out string resourceValue)
    {
      return Res.TryGet(classId, key, culture, true, out resourceValue);
    }

    /// <summary>
    /// Tries the get a resource value by a given key, culture and an option to fallback to the default culture if needed.
    /// </summary>
    /// <param name="classId">The class identifier.</param>
    /// <param name="key">The key.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="fallback">if set to <c>true</c> [fallback].</param>
    /// <param name="resourceValue">The resource value.</param>
    /// <returns></returns>
    public static bool TryGet(
      string classId,
      string key,
      CultureInfo culture,
      bool fallback,
      out string resourceValue)
    {
      resourceValue = (string) null;
      if (Res.instance == null)
        return false;
      PropertyCollection properties = Res.GetProperties(classId, false);
      if (properties != null && properties.Contains(key))
      {
        resourceValue = Resource.GetString(Res.instance.Provider, classId, properties[key], culture, fallback);
        return resourceValue != null;
      }
      resourceValue = Resource.GetString(Res.instance.Provider, classId, key, culture, fallback, false);
      return resourceValue != null;
    }

    /// <summary>
    /// Returns <paramref name="text" />, if <paramref name="classId" /> is <c>null</c> or empty;
    /// otherwise <paramref name="text" /> is used as a key for the resource class, as in <c>Res.Get(classId, text)</c>.
    /// </summary>
    /// <param name="text">Unlocalized text or resource key for the given resource class.</param>
    /// <param name="classId">Optional resource class ID.</param>
    /// <returns>
    /// Resource value or <paramref name="text" />'s value unchanged.
    /// </returns>
    public static string GetLocalizable(string text, string classId) => !string.IsNullOrEmpty(classId) ? Res.Get(classId, text) : text;

    /// <summary>
    /// Gets <see cref="T:Telerik.Sitefinity.Localization.PropertyCollection" /> for the specified class.
    /// </summary>
    /// <param name="classId">The key for the resource entry.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Localization.PropertyCollection" /></returns>
    public static PropertyCollection GetProperties(string classId) => Res.GetProperties(classId, true);

    /// <summary>
    /// Gets <see cref="T:Telerik.Sitefinity.Localization.PropertyCollection" /> for the specified class.
    /// </summary>
    /// <param name="classId">The key for the resource entry.</param>
    /// <param name="throws">Throws an exception if true.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Localization.PropertyCollection" /></returns>
    public static PropertyCollection GetProperties(string classId, bool throws)
    {
      if (string.IsNullOrEmpty(classId))
        throw new ArgumentNullException(nameof (classId));
      PropertyCollection properties;
      if (!Resource.PropertyBags.TryGetValue(classId, out properties))
      {
        Type namedType = ObjectFactory.GetNamedType(classId, typeof (Resource));
        if (namedType == (Type) null)
        {
          if (throws)
            throw new ArgumentException(Res.Get<ErrorMessages>("InvalidClassId", (object) classId), nameof (classId));
          return (PropertyCollection) null;
        }
        properties = ((Resource) ObjectFactory.Resolve(namedType, classId)).Properties;
      }
      return properties;
    }

    /// <summary>Registers a resource class with the type system.</summary>
    /// <typeparam name="TResource">The type of the resource class to register</typeparam>
    public static void RegisterResource<TResource>() where TResource : Resource, new() => Res.RegisterResource(typeof (TResource));

    /// <summary>Registers a resource class with the type system.</summary>
    /// <param name="resourceType">The type of the resource class to register.</param>
    public static void RegisterResource(Type resourceType)
    {
      string resourceClassId = Res.GetResourceClassId(resourceType);
      ObjectFactory.Container.RegisterType(resourceType, resourceClassId, (LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
    }

    /// <summary>
    /// Gets the resource class ID for the provided resource type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static string GetResourceClassId(Type type)
    {
      object[] customAttributes = type.GetCustomAttributes(typeof (ObjectInfoAttribute), false);
      string name;
      if (customAttributes.Length != 0)
      {
        name = ((ObjectInfoAttribute) customAttributes[0]).Name;
        if (string.IsNullOrEmpty(name))
          name = type.Name;
      }
      else
        name = type.Name;
      return name;
    }

    /// <summary>
    /// Sets all culture values for the given key to the provided <see cref="T:Telerik.Sitefinity.Model.Lstring" /> property.
    /// </summary>
    /// <param name="property">The <see cref="T:Telerik.Sitefinity.Model.Lstring" /> property.</param>
    /// <param name="classId">The global resource class pageId.</param>
    /// <param name="key">The key.</param>
    public static void SetLstring(Lstring property, string classId, string key)
    {
      if (property == (Lstring) null)
        throw new ArgumentNullException(nameof (property));
      if (string.IsNullOrEmpty(classId))
        throw new ArgumentNullException(nameof (classId));
      if (string.IsNullOrEmpty(key))
        throw new ArgumentNullException(nameof (key));
      if (Res.SiteCultures.Length != 0)
      {
        foreach (CultureInfo siteUiCulture in Res.SiteUICultures)
          property[siteUiCulture] = Res.Get(classId, key, siteUiCulture);
      }
      else
        property[""] = Res.Get(classId, key);
    }

    /// <summary>
    /// Sets all culture values for the given key to the provided <see cref="T:Telerik.Sitefinity.Model.Lstring" /> property.
    /// </summary>
    /// <param name="property">The <see cref="T:Telerik.Sitefinity.Model.Lstring" /> property.</param>
    /// <param name="classId">The global resource class pageId.</param>
    /// <param name="key">The key.</param>
    public static void SetLstring(Lstring property, Type classType, string key)
    {
      if (property == (Lstring) null)
        throw new ArgumentNullException(nameof (property));
      if (classType == (Type) null)
        throw new ArgumentNullException(nameof (classType));
      property[""] = !string.IsNullOrEmpty(key) ? Res.Get(classType, key) : throw new ArgumentNullException(nameof (key));
    }

    /// <summary>
    /// Gets the an array of <see cref="T:System.Globalization.CultureInfo" /> objects for all defined site cultures.
    /// For monolingual sites this property will return empty array.
    /// </summary>
    /// <value>The site cultures.</value>
    public static CultureInfo[] SiteCultures
    {
      get
      {
        if (Res.siteCultures == null)
        {
          ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
          if (SystemManager.CurrentContext.AppSettings.Multilingual)
          {
            int num = 0;
            Res.siteCultures = new CultureInfo[resourcesConfig.Cultures.Count];
            foreach (CultureElement cultureElement in (IEnumerable<CultureElement>) resourcesConfig.Cultures.Values)
              Res.siteCultures[num++] = CultureInfo.GetCultureInfo(cultureElement.Culture);
          }
          else
            Res.siteCultures = new CultureInfo[0];
        }
        return Res.siteCultures;
      }
    }

    /// <summary>
    /// Gets the an array of <see cref="T:System.Globalization.CultureInfo" /> objects for all defined site UI cultures.
    /// For monolingual sites this property will return empty array.
    /// </summary>
    /// <value>The site UI cultures.</value>
    /// <remarks>UI cultures are used by lstrings (translation), while cultures are used for formatting</remarks>
    public static CultureInfo[] SiteUICultures
    {
      get
      {
        if (Res.siteUiCultures == null)
          Res.siteUiCultures = !SystemManager.CurrentContext.AppSettings.Multilingual ? new CultureInfo[0] : SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages;
        return Res.siteUiCultures;
      }
    }
  }
}
