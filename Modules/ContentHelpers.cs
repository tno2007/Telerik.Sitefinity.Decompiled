// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ContentExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules
{
  public static class ContentExtensions
  {
    public static bool SupportsContentLifeCycle(this Type type)
    {
      if (typeof (DynamicContent).IsAssignableFrom(type))
        return true;
      if (!typeof (Content).IsAssignableFrom(type) || !((IEnumerable<ConstructorInfo>) type.GetConstructors()).Where<ConstructorInfo>((Func<ConstructorInfo, bool>) (cnt => ((IEnumerable<ParameterInfo>) cnt.GetParameters()).Count<ParameterInfo>() == 0)).Any<ConstructorInfo>())
        return false;
      Content instance = Activator.CreateInstance(type) as Content;
      if (typeof (MediaContent).IsAssignableFrom(instance.GetType()))
        ((MediaContent) instance).Uploaded = true;
      return instance.SupportsContentLifecycle;
    }

    /// <summary>
    /// Clears the destination urls and prepare them to copy the source ones
    /// </summary>
    /// <typeparam name="T">Type of the UrlData</typeparam>
    /// <param name="destinationUrls">list of the destination urls</param>
    /// <param name="sourceUrls">list with the source urls (used in multilingual mode)</param>
    /// <param name="deleteDelegate">delegate which deletes the urls</param>
    public static void ClearDestinationUrls<T>(
      this IList<T> destinationUrls,
      IList<T> sourceUrls,
      Action<T> deleteDelegate)
      where T : UrlData, new()
    {
      List<T> objList = new List<T>((IEnumerable<T>) destinationUrls);
      for (int index = 0; index < objList.Count; ++index)
      {
        T obj = objList[index];
        deleteDelegate(obj);
      }
      destinationUrls.Clear();
    }

    /// <summary>
    /// Copies the source urls to the detination onces and makes the necessary assignments
    /// </summary>
    /// <typeparam name="T">UrlDataType</typeparam>
    /// <param name="sourceUrls">source urls to copy from</param>
    /// <param name="destinationUrls">destination urls to copy to</param>
    /// <param name="parent">The parent to be set to each url data</param>
    public static void CopyTo<T>(
      this IList<T> sourceUrls,
      IList<T> destinationUrls,
      IDataItem parent)
      where T : UrlData, new()
    {
      foreach (T sourceUrl in (IEnumerable<T>) sourceUrls)
      {
        T obj = new T();
        obj.Id = parent.GetNewGuid();
        obj.Parent = parent;
        obj.Url = sourceUrl.Url;
        obj.Culture = sourceUrl.Culture;
        obj.ApplicationName = sourceUrl.ApplicationName;
        obj.RedirectToDefault = sourceUrl.RedirectToDefault;
        obj.IsDefault = sourceUrl.IsDefault;
        destinationUrls.Add(obj);
      }
    }

    /// <summary>
    /// Gets the localized UI name of a type. For example for class NewsItem returns "News" in English etc.
    /// </summary>
    /// <param name="typeFullName">Full name of the type.</param>
    /// <returns></returns>
    public static string GetTypeUIPluralName(string typeFullName) => TypeResolutionService.ResolveType(typeFullName).GetTypeUIPluralName();

    /// <summary>
    /// Gets the localized UI name of a type. For example for class NewsItem returns "News" in English etc.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns>the localized UI name of the class or the type class name if the class is not decorated with a TypeUINameAttribute</returns>
    public static string GetTypeUIPluralName(this Type contentType) => ContentExtensions.GetTransaltionAttributeValue(contentType.GetCustomAttributes(typeof (TypeUINameAttribute), true).Cast<TypeUINameAttribute>().LastOrDefault<TypeUINameAttribute>(), contentType.Name, false);

    /// <summary>
    /// Gets the localized UI name of a type. For example for class NewsItem returns "News" in English etc.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns>the localized UI name of the class or the type class name if the class is not decorated with a TypeUINameAttribute</returns>
    public static string GetTypeUISingleName(string typeFullName) => TypeResolutionService.ResolveType(typeFullName).GetTypeUISingleName();

    /// <summary>
    /// Gets the localized UI name of a type. For example for class NewsItem returns "News" in English etc.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns>the localized UI name of the class or the type class name if the class is not decorated with a TypeUINameAttribute</returns>
    public static string GetTypeUISingleName(this Type contentType) => ContentExtensions.GetTransaltionAttributeValue(contentType.GetCustomAttributes(typeof (TypeUINameAttribute), true).Cast<TypeUINameAttribute>().LastOrDefault<TypeUINameAttribute>(), contentType.Name, true);

    public static void MergeTo<T>(
      this IList<T> sourceUrls,
      IList<T> destinationUrls,
      IDataItem parent,
      CultureInfo culture,
      Action<T> deleteDelegate)
      where T : UrlData, new()
    {
      int cultureLCID = AppSettings.CurrentSettings.GetCultureLcid(culture);
      bool isDefCult = culture.Equals((object) SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage);
      List<T> list = destinationUrls.Where<T>((Func<T, bool>) (u =>
      {
        if (u.Culture == cultureLCID)
          return true;
        return isDefCult && u.Culture == CultureInfo.InvariantCulture.LCID;
      })).ToList<T>();
      foreach (T obj1 in sourceUrls.Where<T>((Func<T, bool>) (u =>
      {
        if (u.Culture == cultureLCID)
          return true;
        return isDefCult && u.Culture == CultureInfo.InvariantCulture.LCID;
      })))
      {
        T srcUrl = obj1;
        T obj2 = list.Where<T>((Func<T, bool>) (u => u.Url == srcUrl.Url && u.Culture == srcUrl.Culture)).FirstOrDefault<T>();
        if ((object) obj2 == null)
        {
          T obj3 = new T();
          obj3.Id = parent.GetNewGuid();
          obj3.Parent = parent;
          obj3.Url = ((T) srcUrl).Url;
          obj3.Culture = ((T) srcUrl).Culture;
          obj3.ApplicationName = ((T) srcUrl).ApplicationName;
          obj3.RedirectToDefault = ((T) srcUrl).RedirectToDefault;
          obj3.IsDefault = ((T) srcUrl).IsDefault;
          destinationUrls.Add(obj3);
        }
        else
        {
          if (obj2.RedirectToDefault != ((T) srcUrl).RedirectToDefault)
            obj2.RedirectToDefault = ((T) srcUrl).RedirectToDefault;
          if (obj2.IsDefault != ((T) srcUrl).IsDefault)
            obj2.IsDefault = ((T) srcUrl).IsDefault;
          list.Remove(obj2);
        }
      }
      foreach (T obj in list)
      {
        destinationUrls.Remove(obj);
        deleteDelegate(obj);
      }
    }

    private static string GetTransaltionAttributeValue(
      TypeUINameAttribute transaltionAttribute,
      string defaultValue,
      bool useSingleKey)
    {
      if (transaltionAttribute == null)
        return defaultValue;
      string key = useSingleKey ? transaltionAttribute.SingleKey : transaltionAttribute.PluralKey;
      return !string.IsNullOrEmpty(transaltionAttribute.ResourceClass) ? Res.Get(transaltionAttribute.ResourceClass, key) : key;
    }
  }
}
