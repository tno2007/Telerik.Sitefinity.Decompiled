// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.QueryStringBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// A chainable query string helper class.
  /// Example usage :
  /// string strQuery = QueryString.Current.Add("id", "179").ToString();
  /// string strQuery = new QueryString().Add("id", "179").ToString();
  /// </summary>
  public class QueryStringBuilder : NameValueCollection
  {
    private bool htmlEncoding;

    public QueryStringBuilder()
    {
    }

    public QueryStringBuilder(string queryString)
    {
      if (this.IsHtmlEncoded(queryString))
        this.HtmlEncoding = true;
      this.FillFromString(queryString);
    }

    public QueryStringBuilder(string queryString, bool htmlEncoding)
    {
      this.HtmlEncoding = htmlEncoding;
      this.FillFromString(queryString);
    }

    public static QueryStringBuilder Current => new QueryStringBuilder().FromCurrent();

    /// <summary>
    /// Property that indicates if the incoming queryString is HTML encoded and if the resulting string from the ToString method should be HTML encoded
    /// </summary>
    public bool HtmlEncoding
    {
      get => this.htmlEncoding;
      set => this.htmlEncoding = value;
    }

    /// <summary>Extracts a querystring from a full URL</summary>
    /// <param name="s">The string to extract the querystring from</param>
    /// <returns>A string representing only the querystring</returns>
    public string ExtractQuerystring(string s) => !string.IsNullOrEmpty(s) && s.Contains("?") ? s.Substring(s.IndexOf("?") + 1) : s;

    /// <summary>Returns a querystring object based on a string</summary>
    /// <param name="s">The string to parse</param>
    /// <returns>The QueryString object </returns>
    public QueryStringBuilder FillFromString(string s)
    {
      this.Clear();
      if (string.IsNullOrEmpty(s))
        return this;
      if (this.HtmlEncoding)
        s = HttpUtility.HtmlDecode(s);
      string querystring = this.ExtractQuerystring(s);
      char[] chArray = new char[1]{ '&' };
      foreach (string str in querystring.Split(chArray))
      {
        if (!string.IsNullOrEmpty(str))
        {
          string[] source = str.Split('=');
          base.Add(source[0], source.Length >= 2 ? string.Join("=", ((IEnumerable<string>) source).Skip<string>(1)) : "");
        }
      }
      return this;
    }

    /// <summary>
    /// Returns a QueryString object based on the current querystring of the request
    /// </summary>
    /// <returns>The QueryString object </returns>
    public QueryStringBuilder FromCurrent()
    {
      if (SystemManager.CurrentHttpContext != null)
        return this.FillFromString(SystemManager.CurrentHttpContext.Request.QueryString.ToString());
      this.Clear();
      return this;
    }

    /// <summary>Add a name value pair to the collection</summary>
    /// <param name="name">The name</param>
    /// <param name="value">The value associated to the name</param>
    /// <returns>The QueryString object </returns>
    public QueryStringBuilder Add(string name, string value) => this.Add(name, value, false);

    /// <summary>Adds a name value pair to the collection</summary>
    /// <param name="name">The name</param>
    /// <param name="value">The value associated to the name</param>
    /// <param name="isUnique">True if the name is unique within the querystring. This allows us to override existing values</param>
    /// <returns>The QueryString object </returns>
    public QueryStringBuilder Add(string name, string value, bool isUnique)
    {
      if (string.IsNullOrEmpty(base[name]))
        base.Add(name, HttpUtility.UrlEncodeUnicode(value));
      else if (isUnique)
      {
        this[name] = HttpUtility.UrlEncodeUnicode(value);
      }
      else
      {
        string name1 = name;
        this[name1] = base[name1] + "," + HttpUtility.UrlEncodeUnicode(value);
      }
      return this;
    }

    /// <summary>
    /// Removes a name value pair from the querystring collection
    /// </summary>
    /// <param name="name">Name of the querystring value to remove</param>
    /// <returns>The QueryString object</returns>
    public QueryStringBuilder Remove(string name)
    {
      if (!string.IsNullOrEmpty(base[name]))
        base.Remove(name);
      return this;
    }

    /// <summary>Clears the collection</summary>
    /// <returns>The QueryString object </returns>
    public QueryStringBuilder Reset()
    {
      this.Clear();
      return this;
    }

    /// <summary>Overrides the default indexer</summary>
    /// <param name="name"></param>
    /// <returns>The associated decoded value for the specified name</returns>
    public new string this[string name] => HttpUtility.UrlDecode(base[name]);

    /// <summary>Overrides the default indexer</summary>
    /// <param name="index"></param>
    /// <returns>The associated decoded value for the specified index</returns>
    public new string this[int index] => HttpUtility.UrlDecode(base[index]);

    /// <summary>
    /// Checks if a name already exists within the query string collection
    /// </summary>
    /// <param name="name">The name to check</param>
    /// <returns>A boolean if the name exists</returns>
    public bool Contains(string name) => !string.IsNullOrEmpty(base[name]);

    /// <summary>Outputs the querystring object to a string</summary>
    /// <returns>The encoded querystring as it would appear in a browser</returns>
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.Keys.Count; ++index)
      {
        if (!string.IsNullOrEmpty(this.Keys[index]))
        {
          string str1 = base[this.Keys[index]];
          char[] chArray = new char[1]{ ',' };
          foreach (string str2 in str1.Split(chArray))
            stringBuilder.Append(stringBuilder.Length == 0 ? "?" : "&").Append(HttpUtility.UrlEncodeUnicode(this.Keys[index])).Append("=").Append(str2);
        }
      }
      string s = stringBuilder.ToString();
      if (this.HtmlEncoding)
        s = HttpUtility.HtmlEncode(s);
      return s;
    }

    private bool IsHtmlEncoded(string text) => HttpUtility.HtmlDecode(text) != text;
  }
}
