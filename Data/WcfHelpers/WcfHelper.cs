// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.WcfHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>Provides helpers to work with Si</summary>
  public static class WcfHelper
  {
    private static readonly IDataContractSurrogate surrogate = (IDataContractSurrogate) new DynamicFieldsDataContractSurrogate();
    private static readonly Dictionary<string, string> encodeMap = new Dictionary<string, string>();
    private static readonly Dictionary<string, string> decodeMap;

    static WcfHelper()
    {
      WcfHelper.encodeMap["."] = "__dot__";
      WcfHelper.encodeMap["]"] = "__rsb__";
      WcfHelper.encodeMap["["] = "__lsb__";
      WcfHelper.encodeMap["}"] = "__rcb__";
      WcfHelper.encodeMap["{"] = "__lcb__";
      WcfHelper.encodeMap["`"] = "__gr__";
      WcfHelper.encodeMap[","] = "__cm__";
      WcfHelper.encodeMap[" "] = "__sp__";
      WcfHelper.encodeMap["="] = "__eq__";
      WcfHelper.encodeMap["#"] = "__pd__";
      WcfHelper.encodeMap["?"] = "__qm__";
      WcfHelper.encodeMap[":"] = "__cl__";
      WcfHelper.encodeMap["%"] = "__pct__";
      WcfHelper.decodeMap = new Dictionary<string, string>();
      foreach (KeyValuePair<string, string> encode in WcfHelper.encodeMap)
        WcfHelper.decodeMap.Add(encode.Value, encode.Key);
    }

    /// <summary>
    /// Encode a string so that it will work as a url of a wcf service
    /// </summary>
    /// <param name="wcfString">string to encode</param>
    /// <returns>Encoded type name</returns>
    public static string EncodeWcfString(string wcfString)
    {
      if (string.IsNullOrEmpty(wcfString))
        return wcfString;
      string str = wcfString;
      foreach (string key in WcfHelper.encodeMap.Keys)
        str = str.Replace(key, WcfHelper.encodeMap[key]);
      return str;
    }

    /// <summary>
    /// Decode a previously encoded string to its original form
    /// </summary>
    /// <param name="encodedWcfString"></param>
    /// <returns>Decoded string</returns>
    public static string DecodeWcfString(string encodedWcfString)
    {
      if (string.IsNullOrEmpty(encodedWcfString))
        return encodedWcfString;
      string str = encodedWcfString;
      foreach (string key in WcfHelper.decodeMap.Keys)
        str = str.Replace(key, WcfHelper.decodeMap[key]);
      return str;
    }

    /// <summary>Resolves the type of an encoded type</summary>
    /// <param name="encodedTypeName">encoded type name</param>
    /// <returns>Resovled type</returns>
    public static Type ResolveEncodedTypeName(string encodedTypeName) => WcfHelper.ResolveEncodedTypeName(encodedTypeName, true, false);

    /// <summary>
    /// Decodee <paramref name="encodedTypeName" /> and call <see cref="T:Telerik.Sitefinity.Utilities.TypeConverters.TypeResolutionService" /> with proper arguments
    /// </summary>
    /// <param name="encodedTypeName">WCF-encoded string representing a type name</param>
    /// <param name="throwOnError">True to thow on type resolution failure, false to return null on error instead.</param>
    /// <returns>Resolved type or <c>null</c> if <paramref name="thorwOnError" /> is <c>true</c> and the type could not be resolved.</returns>
    public static Type ResolveEncodedTypeName(string encodedTypeName, bool throwOnError) => WcfHelper.ResolveEncodedTypeName(encodedTypeName, throwOnError, false);

    /// <summary>
    /// Decodee <paramref name="encodedTypeName" /> and call <see cref="T:Telerik.Sitefinity.Utilities.TypeConverters.TypeResolutionService" /> with proper arguments
    /// </summary>
    /// <param name="encodedTypeName">WCF-encoded string representing a type name</param>
    /// <param name="throwOnError">True to thow on type resolution failure, false to return null on error instead.</param>
    /// <param name="ignoreCase">Determine whether to ignore casing errors or not.</param>
    /// <returns>Resolved type or <c>null</c> if <paramref name="thorwOnError" /> is <c>true</c> and the type could not be resolved.</returns>
    public static Type ResolveEncodedTypeName(
      string encodedTypeName,
      bool throwOnError,
      bool ignoreCase)
    {
      if (encodedTypeName.IsNullOrWhitespace())
        throw new ArgumentNullException(nameof (encodedTypeName));
      return TypeResolutionService.ResolveType(WcfHelper.DecodeWcfString(encodedTypeName), throwOnError, ignoreCase);
    }

    /// <summary>
    /// Create a json serializer with data contract surrogate; otherize - use defaults for the rest
    /// </summary>
    /// <param name="clrType">type that the serializer will work with</param>
    /// <returns>JSON DataContract serializer</returns>
    private static DataContractJsonSerializer GetSerializer(Type clrType) => new DataContractJsonSerializer(clrType, (IEnumerable<Type>) Type.EmptyTypes, int.MaxValue, false, WcfHelper.surrogate, false);

    /// <summary>
    /// Deserialize a string and return an actual CLR object instance
    /// </summary>
    /// <param name="json">JSON string to deserialize</param>
    /// <param name="clrType">CLR type of the object to deserialize</param>
    /// <returns>CLR object from the deserialized JSON string</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="clrType" /> is null.</exception>
    public static object DeserializeFromJson(string json, Type clrType)
    {
      DataContractJsonSerializer contractJsonSerializer = !(clrType == (Type) null) ? WcfHelper.GetSerializer(clrType) : throw new ArgumentNullException(nameof (clrType));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) memoryStream))
        {
          streamWriter.Write(json);
          streamWriter.Flush();
        }
        memoryStream.Position = 0L;
        return contractJsonSerializer.ReadObject((Stream) memoryStream);
      }
    }

    /// <summary>Serialize CLR object to a JSON string</summary>
    /// <param name="clrObject">CLR object graph to be serialized into JSON</param>
    /// <param name="clrType">type of the CLR object that needs to be serialized</param>
    /// <returns>System.String containing the JSON serialization of <paramref name="clrObject" /></returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="clrType" /> is null.</exception>
    public static string SerializeToJson(object clrObject, Type clrType)
    {
      if (clrType == (Type) null)
        throw new ArgumentNullException(nameof (clrType));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        WcfHelper.GetSerializer(clrType).WriteObject((Stream) memoryStream, clrObject);
        memoryStream.Position = 0L;
        return new StreamReader((Stream) memoryStream).ReadToEnd();
      }
    }
  }
}
