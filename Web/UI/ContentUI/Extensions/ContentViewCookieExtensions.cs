// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Extensions.ContentViewCookieExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Extensions
{
  public static class ContentViewCookieExtensions
  {
    /// <summary>Gets the cookie value.</summary>
    public static IDictionary<string, string> GetCookieValue(
      this IContentViewDefinition defintion,
      Type type)
    {
      string cookieKey = defintion.GetCookieKey(type);
      HttpCookie httpCookie = new HttpCookie(cookieKey);
      return ContentViewCookieExtensions.ParseCookieValue(SystemManager.CurrentHttpContext.Request.Cookies[cookieKey]);
    }

    /// <summary>Gets the value of the property stored in the cookie.</summary>
    public static KeyValuePair<string, string> GetPropertyValueFromCookie(
      this IContentViewDefinition defintion,
      Type type,
      string property,
      out bool hasValue)
    {
      IDictionary<string, string> cookieValue = defintion.GetCookieValue(type);
      KeyValuePair<string, string> propertyValueFromCookie = new KeyValuePair<string, string>();
      hasValue = false;
      if (cookieValue != null)
      {
        propertyValueFromCookie = cookieValue.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (c => c.Key == property));
        if (!propertyValueFromCookie.Equals((object) new KeyValuePair<string, string>()) && !string.IsNullOrEmpty(propertyValueFromCookie.Value))
          hasValue = true;
      }
      return propertyValueFromCookie;
    }

    /// <summary>Gets the cookie key.</summary>
    public static string GetCookieKey(this IContentViewDefinition defintion, Type type) => string.Format("{0}{1}{2}", (object) defintion.GetDefinition().ConfigDefinitionPath, (object) type.Namespace, (object) type.Name);

    private static IDictionary<string, string> ParseCookieValue(HttpCookie cookie)
    {
      Dictionary<string, string> cookieValue = (Dictionary<string, string>) null;
      if (cookie != null)
        cookieValue = ContentViewCookieExtensions.PropertyListToDictionary((List<ContentViewCookieExtensions.Property>) new JavaScriptSerializer().Deserialize(Uri.UnescapeDataString(cookie.Value), typeof (List<ContentViewCookieExtensions.Property>)));
      return (IDictionary<string, string>) cookieValue;
    }

    private static Dictionary<string, string> PropertyListToDictionary(
      List<ContentViewCookieExtensions.Property> properties)
    {
      Dictionary<string, string> dictionary = (Dictionary<string, string>) null;
      if (properties != null && properties.Count > 0)
      {
        dictionary = new Dictionary<string, string>();
        foreach (ContentViewCookieExtensions.Property property in properties)
          dictionary.Add(property.Key, property.Value);
      }
      return dictionary;
    }

    private struct Property
    {
      public string Key;
      public string Value;
    }
  }
}
