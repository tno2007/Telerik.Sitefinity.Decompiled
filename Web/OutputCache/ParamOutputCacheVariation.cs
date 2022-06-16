// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.ParamOutputCacheVariation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Web;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.OutputCache
{
  [Serializable]
  internal class ParamOutputCacheVariation : CustomOutputCacheVariationBase
  {
    private CacheVariationParamValidator validator;
    private CacheVariationParameterSource source;

    public static string GenerateKeyPrefix(CacheVariationParameterSource source) => "sf-" + Enum.GetName(typeof (CacheVariationParameterSource), (object) source) + "-";

    public static string GenerateKey(string parameterName, CacheVariationParameterSource source) => ParamOutputCacheVariation.GenerateKeyPrefix(source) + parameterName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.ParamOutputCacheVariation" /> class.
    /// </summary>
    public ParamOutputCacheVariation()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.ParamOutputCacheVariation" /> class.
    /// </summary>
    /// <param name="key">The query string parameter name.</param>
    /// <param name="source">The source of the parameter (query string or params) .</param>
    /// <param name="validator">The validation method.</param>
    public ParamOutputCacheVariation(
      string key,
      CacheVariationParameterSource source,
      CacheVariationParamValidator validator = null)
    {
      if (validator != null && !validator.GetType().IsSerializable)
        throw new ArgumentException("The validator should be serializable.", nameof (validator));
      if (!key.StartsWith("sf-"))
        key = ParamOutputCacheVariation.GenerateKey(key, source);
      this.source = source;
      this.Key = key;
      this.validator = validator;
    }

    /// <inheritdoc />
    public override string GetValue()
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext != null)
      {
        string name = this.Key.Substring(ParamOutputCacheVariation.GenerateKeyPrefix(this.source).Length);
        string str = this.GetCollection(currentHttpContext.Request)[name];
        if (!str.IsNullOrEmpty() && this.Validate(str))
          return str;
      }
      return string.Empty;
    }

    protected virtual NameValueCollection GetCollection(HttpRequestBase request)
    {
      switch (this.source)
      {
        case CacheVariationParameterSource.QueryString:
          return request.QueryString;
        default:
          return request.Params;
      }
    }

    private bool Validate(string value) => this.validator == null || this.validator.Validate(value);
  }
}
