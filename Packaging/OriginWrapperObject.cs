// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.OriginWrapperObject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Packaging
{
  /// <summary>Class representing origin object</summary>
  internal class OriginWrapperObject
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.OriginWrapperObject" /> class.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [JsonConstructor]
    public OriginWrapperObject(string key, string value)
    {
      this.Key = key;
      this.Value = value;
    }

    /// <summary>Converts Origin object JSON to array of Origins JSON</summary>
    /// <param name="origin">The origin JSON.</param>
    /// <returns>JSON of array of Origins containing the given origin.</returns>
    public static string ToArrayJson(string origin)
    {
      if (string.IsNullOrWhiteSpace(origin))
        return (string) null;
      try
      {
        return JsonConvert.SerializeObject((object) new OriginWrapperObject[1]
        {
          JsonConvert.DeserializeObject<OriginWrapperObject>(origin)
        });
      }
      catch (Exception ex)
      {
        return (string) null;
      }
    }

    /// <summary>Parses the json array.</summary>
    /// <param name="jsonArray">The json array.</param>
    /// <returns>List of <see cref="T:Telerik.Sitefinity.Packaging.OriginWrapperObject" /> objects</returns>
    public static IEnumerable<OriginWrapperObject> ParseJsonArray(
      string jsonArray)
    {
      if (string.IsNullOrWhiteSpace(jsonArray))
        return Enumerable.Empty<OriginWrapperObject>();
      try
      {
        return JsonConvert.DeserializeObject<IEnumerable<OriginWrapperObject>>(jsonArray);
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
        return Enumerable.Empty<OriginWrapperObject>();
      }
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() => JsonConvert.SerializeObject((object) this);

    /// <summary>Gets or sets the key.</summary>
    /// <value>The key.</value>
    public string Key { get; set; }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    public string Value { get; set; }
  }
}
