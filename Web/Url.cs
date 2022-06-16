// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Url
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// A helper class used to easily manipulation url properties
  /// </summary>
  public class Url
  {
    private string scheme;
    private string user;
    private string host;
    private string port;
    private string path;
    private Dictionary<string, string> query;
    private string fragment;
    private static Regex urlRegex = new Regex("^\r\n(# Scheme\r\n (?<scheme>[a-z][a-z0-9+\\-.]*):\r\n (# Authority & path\r\n  //\r\n  (?<user>[a-z0-9\\-._~%!$&'()*+,;=]+@)?              # User\r\n  (?<host>[a-z0-9\\-._~%]+                            # Named host\r\n  |       \\[[a-f0-9:.]+\\]                            # IPv6 host\r\n  |       \\[v[a-f0-9][a-z0-9\\-._~%!$&'()*+,;=:]+\\])  # IPvFuture host\r\n  (?<port>:[0-9]+)?                                  # Port\r\n  (?<path>(/[a-z0-9\\-._~%!$&'()*+,;=:@]+)*/?)        # Path\r\n )\r\n|# Relative URL (no scheme or authority)\r\n (?<path>[a-z0-9\\-._~%!$&'()*+,;=@]+(/[a-z0-9\\-._~%!$&'()*+,;=:@]+)*/?  # Relative path\r\n |(/[a-z0-9\\-._~%!$&'()*+,;=:@]+)+/?)                                   # Absolute path\r\n)\r\n# Query\r\n(?<query>\\?[a-z0-9\\-._~%!$&'()*+,;=:@/?]*)?\r\n# Fragment\r\n(?<fragment>\\#[a-z0-9\\-._~%!$&'()*+,;=:@/?]*)?\r\n$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Url" /> class.
    /// </summary>
    /// <param name="urlAsString">The URL as string.</param>
    public Url(string urlAsString)
    {
      if (urlAsString.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (urlAsString), "Can't parse a null url");
      GroupCollection groups = Url.urlRegex.Match(urlAsString).Groups;
      this.scheme = groups[nameof (scheme)].Value;
      this.user = groups[nameof (user)].Value;
      this.host = groups[nameof (host)].Value;
      this.port = groups[nameof (port)].Value;
      this.path = groups[nameof (path)].Value;
      this.query = this.ParseQueryString(groups[nameof (query)].Value);
      this.fragment = groups[nameof (fragment)].Value;
    }

    /// <summary>Gets or sets the scheme portion of the url if any.</summary>
    /// <value>The scheme.</value>
    public string Scheme
    {
      get => this.scheme;
      set => this.scheme = value;
    }

    /// <summary>Gets or sets the user portion of the url if any.</summary>
    /// <value>The user.</value>
    public string User
    {
      get => this.user;
      set => this.user = value;
    }

    /// <summary>Gets or sets the host portion of the url if any.</summary>
    /// <value>The host.</value>
    public string Host
    {
      get => this.host;
      set => this.host = value;
    }

    /// <summary>Gets or sets the port portion of the url if any.</summary>
    /// <value>The port.</value>
    public string Port
    {
      get => this.port;
      set => this.port = value;
    }

    /// <summary>Gets or sets the path portion of the url if any.</summary>
    /// <value>The path.</value>
    public string Path
    {
      get => this.path;
      set => this.path = value;
    }

    /// <summary>Gets or sets the fragments portion of the url if any.</summary>
    /// <value>The path.</value>
    public string Fragment
    {
      get => this.fragment;
      set => this.fragment = value;
    }

    /// <summary>
    /// Gets the query portion of the url as a dictionary of string keys and values.
    /// </summary>
    public Dictionary<string, string> Query => this.query;

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (!this.scheme.IsNullOrEmpty())
      {
        stringBuilder.Append(this.scheme);
        stringBuilder.Append("://");
      }
      if (!this.user.IsNullOrEmpty())
        stringBuilder.Append(this.user);
      if (!this.host.IsNullOrEmpty())
        stringBuilder.Append(this.host);
      if (!this.port.IsNullOrEmpty())
        stringBuilder.Append(this.port);
      if (!this.path.IsNullOrEmpty())
        stringBuilder.Append(this.path);
      bool flag = false;
      foreach (KeyValuePair<string, string> keyValuePair in this.Query)
      {
        if (flag)
        {
          stringBuilder.Append('&');
        }
        else
        {
          stringBuilder.Append('?');
          flag = true;
        }
        stringBuilder.Append(keyValuePair.Key);
        stringBuilder.Append('=');
        stringBuilder.Append(keyValuePair.Value);
      }
      if (!this.fragment.IsNullOrEmpty())
      {
        stringBuilder.Append('#');
        stringBuilder.Append(this.fragment);
      }
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Combines a url with provided url segments with slashes "/"
    /// </summary>
    /// <param name="baseUrl">The base URL.</param>
    /// <param name="urlSegments">The URL segments.</param>
    /// <returns>url like http://baseUrl/segment1/segment2 etc</returns>
    public static string Combine(string baseUrl, params string[] urlSegments)
    {
      if (string.IsNullOrEmpty(baseUrl))
        throw new ArgumentNullException(nameof (baseUrl));
      if (urlSegments == null)
        throw new ArgumentNullException(nameof (urlSegments));
      if (urlSegments.Length == 0)
        return baseUrl;
      StringBuilder stringBuilder = new StringBuilder();
      if (baseUrl.EndsWith("/"))
        stringBuilder.Append(baseUrl);
      else
        stringBuilder.AppendFormat("{0}/", (object) baseUrl);
      foreach (string urlSegment in urlSegments)
        stringBuilder.AppendFormat("{0}/", (object) urlSegment);
      string str = stringBuilder.ToString();
      return str.Substring(0, str.Length - 1);
    }

    /// <summary>Appends the URL parameters to the base URL.</summary>
    /// <param name="baseUrl">The base URL.</param>
    /// <param name="urlParams">The URL params.</param>
    /// <returns></returns>
    internal static string AppendUrlParameters(string baseUrl, Dictionary<string, string> urlParams)
    {
      string str1 = baseUrl.TrimEnd('/');
      if (!string.IsNullOrEmpty(baseUrl))
      {
        string str2 = string.Format("{0}/?", (object) str1);
        foreach (KeyValuePair<string, string> urlParam in urlParams)
        {
          if (!string.IsNullOrEmpty(urlParam.Value))
            str2 = string.Format("{0}{1}={2}&", (object) str2, (object) urlParam.Key, (object) urlParam.Value);
        }
        str1 = str2.TrimEnd('&', '?');
      }
      return str1;
    }

    /// <summary>Appends the URL parameter to the base url.</summary>
    /// <param name="baseUrl">The base URL.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <param name="paramValue">The parameter value.</param>
    /// <returns></returns>
    internal static string AppendUrlParameter(string baseUrl, string paramName, string paramValue)
    {
      string str = "?";
      if (baseUrl.Contains(str))
        str = "&";
      baseUrl += string.Format("{0}{1}={2}", (object) str, (object) paramName, (object) paramValue);
      return baseUrl;
    }

    private Dictionary<string, string> ParseQueryString(string queryPortion)
    {
      Dictionary<string, string> queryString = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      if (!string.IsNullOrEmpty(queryPortion))
      {
        queryPortion = queryPortion.TrimStart('?');
        string str1 = queryPortion;
        char[] chArray1 = new char[1]{ '&' };
        foreach (string str2 in str1.Split(chArray1))
        {
          char[] chArray2 = new char[1]{ '=' };
          string[] strArray = str2.Split(chArray2);
          string key = (string) null;
          string str3 = "";
          if (strArray.Length != 0)
            key = strArray[0];
          if (strArray.Length > 1)
            str3 = strArray[1];
          if (!string.IsNullOrEmpty(key))
            queryString.Add(key, str3);
        }
      }
      return queryString;
    }
  }
}
