// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.JsonDictionary`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>
  /// Dictionary with a few utility methods for conversion to and from JSON-formatted strings. Dictionary key is always string.
  /// </summary>
  /// <typeparam name="T">Type of the dictionary value</typeparam>
  public class JsonDictionary<T> : Dictionary<string, T>
  {
    /// <summary>
    /// Parse JSON-formatted string and convert it back to a dictionary
    /// </summary>
    /// <param name="json">JSON-formatted string</param>
    /// <returns>Result of the JSON parsing</returns>
    public static JsonDictionary<T> Parse(string json) => new JavaScriptSerializer().Deserialize<JsonDictionary<T>>(json);

    /// <summary>Serialzies the dictionary to JSON-formatted string</summary>
    /// <returns>JSON-formatted string</returns>
    public string ToJson() => new JavaScriptSerializer().Serialize((object) this);

    /// <summary>Explicitly converts to string</summary>
    /// <param name="dic">Dictionary instance</param>
    /// <returns>JSON-formatted string</returns>
    public static explicit operator string(JsonDictionary<T> dic) => dic.ToJson();

    /// <summary>Parses JSON-formatted string and returns a dictionary</summary>
    /// <param name="json">JSON-formatted string</param>
    /// <returns>Dictionary, which is the result of the JSON-formatted string parsing</returns>
    public static explicit operator JsonDictionary<T>(string json) => JsonDictionary<T>.Parse(json);

    /// <summary>
    /// Calls <see cref="M:Telerik.Sitefinity.Data.WcfHelpers.JsonDictionary`1.ToJson" />
    /// </summary>
    /// <returns>Dictionary serialized to JSON-formatted string</returns>
    public override string ToString() => this.ToJson();
  }
}
