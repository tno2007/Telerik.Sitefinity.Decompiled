// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.HttpClients.HttpClientExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Http;
using Microsoft.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Telerik.Sitefinity.Services.HttpClients
{
  /// <summary>
  /// Class containing extension methods for various objects inside the Microsoft.Http assembly.
  /// </summary>
  public static class HttpClientExtensions
  {
    /// <summary>Cleans the expired cookies from the collection.</summary>
    /// <returns>A reference to the cleaned collection.</returns>
    public static HeaderValues<Cookie> CleanExpired(
      this HeaderValues<Cookie> headerValues)
    {
      DateTime utcNow = DateTime.UtcNow;
      HeaderValues<Cookie> headerValues1 = new HeaderValues<Cookie>();
      foreach (Cookie headerValue in (IEnumerable<Cookie>) headerValues)
      {
        DateTime? expires = headerValue.Expires;
        if (expires.HasValue)
        {
          expires = headerValue.Expires;
          if (!(expires.Value > utcNow))
            continue;
        }
        headerValues1.Add(headerValue);
      }
      return headerValues1;
    }

    /// <summary>
    /// Prepares a SetCookie collection for sending by stacking cookies that have the same name and removes unnecessary attributes like expires, httponly etc.
    /// </summary>
    /// <returns>A reference to the stacked collection.</returns>
    public static HeaderValues<Cookie> StackRepeatedCookiesByName(
      this HeaderValues<Cookie> headerValues)
    {
      HeaderValues<Cookie> headerValues1 = new HeaderValues<Cookie>();
      Dictionary<string, Cookie> dictionary = new Dictionary<string, Cookie>(headerValues.Count);
      foreach (Cookie headerValue in (IEnumerable<Cookie>) headerValues)
      {
        string name = headerValue.TryGetName();
        if (!string.IsNullOrEmpty(name))
        {
          Cookie cookie = Cookie.Parse(string.Format("{0}={1};", (object) name, (object) headerValue[name]));
          dictionary[name] = cookie;
        }
      }
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, Cookie> keyValuePair in dictionary)
      {
        stringBuilder.Append((object) keyValuePair.Value);
        stringBuilder.Append("; ");
      }
      headerValues1.Add(Cookie.Parse(stringBuilder.ToString()));
      return headerValues1;
    }

    /// <summary>Tires to get the name of the cookie</summary>
    /// <param name="cookie">The cookie.</param>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public static string TryGetName(this Cookie cookie)
    {
      if (cookie == null)
        throw new ArgumentNullException(nameof (cookie));
      string cookieName = (string) null;
      string str1 = cookie.ToString();
      if (!string.IsNullOrEmpty(str1))
      {
        string str2 = ((IEnumerable<string>) str1.Split(';')).FirstOrDefault<string>();
        if (!string.IsNullOrEmpty(str2))
        {
          int length = str2.IndexOf('=');
          if (length >= 0)
          {
            cookieName = str2.Substring(0, length).Trim();
            if (HttpClientExtensions.IsReservedCookieWord(cookieName))
              cookieName = (string) null;
          }
        }
      }
      return cookieName;
    }

    /// <summary>
    /// Creates an <see cref="T:Microsoft.Http.HttpContent" /> instance from a given object.
    /// </summary>
    /// <param name="graph">The object to be serialized as JSON into the <see cref="T:Microsoft.Http.HttpContent" /> result.</param>
    /// <returns></returns>
    public static HttpContent CreateContentAsJsonFrom<T>(this HttpClient client, T graph)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(graph.GetType()).WriteObject((Stream) memoryStream, (object) graph);
        memoryStream.Position = 0L;
        byte[] buffer = new byte[memoryStream.Length];
        memoryStream.Read(buffer, 0, (int) memoryStream.Length);
        return HttpContent.Create(buffer, "application/json");
      }
    }

    private static bool IsReservedCookieWord(string cookieName) => ((IEnumerable<string>) new string[5]
    {
      "expires",
      "domain",
      "path",
      "secure",
      "httponly"
    }).Contains<string>(cookieName, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
  }
}
