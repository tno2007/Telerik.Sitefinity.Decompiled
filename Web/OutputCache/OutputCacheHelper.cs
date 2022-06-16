// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.OutputCacheHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.Security;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal sealed class OutputCacheHelper
  {
    private const int MaxPostKeyLength = 15000;
    private const string NullVarybyValue = "+n+";
    private const string OutputcacheKeyprefixPost = "a1";
    private const string OutputcacheKeyprefixGet = "a2";
    private const string Identity = "identity";
    private const string Asterisk = "*";
    private static OutputCacheProviderAsync outputCacheProvider = (OutputCacheProviderAsync) SitefinityOutputCacheProvider.Instance;
    private static Converter converter = new Converter();
    private readonly char[] fieldSeparators = new char[2]
    {
      ',',
      ' '
    };
    private HttpContext context;

    public OutputCacheHelper(HttpContext httpContext) => this.context = httpContext;

    public async Task CacheResponseAsync()
    {
      string cachedVaryItemKey = this.GenerateCachedVaryItemKey();
      this.UpdateCachedHeaders();
      HttpCachePolicySettings currentSettings = this.GetCurrentSettings();
      CachedVary requestCachedVary = this.GetRequestCachedVary(currentSettings);
      string keyRawResponse;
      if (currentSettings.VaryByContentEncodings == null && currentSettings.VaryByHeaders == null && currentSettings.VaryByParams == null && currentSettings.VaryByCustom == null)
      {
        keyRawResponse = cachedVaryItemKey;
      }
      else
      {
        keyRawResponse = this.GenerateCachedItemKey(requestCachedVary);
        if (keyRawResponse == null)
          return;
      }
      await this.InsertResponseAsync(cachedVaryItemKey, keyRawResponse, requestCachedVary, currentSettings);
    }

    private static bool ShouldIncludeSiteIdInRequestKey(HttpRequestBase request) => SitefinityRoute.IsKnownPageExtension(request.CurrentExecutionFilePathExtension);

    private CachedVary GetRequestCachedVary(HttpCachePolicySettings settings)
    {
      string[] varyByHeaders = settings.VaryByHeaders;
      string[] strArray1 = settings.VaryByParams;
      if (settings.IgnoreParams)
        strArray1 = (string[]) null;
      string[] strArray2 = strArray1;
      if (varyByHeaders != null)
      {
        int index = 0;
        for (int length = varyByHeaders.Length; index < length; ++index)
          varyByHeaders[index] = "HTTP_" + CultureInfo.InvariantCulture.TextInfo.ToUpper(varyByHeaders[index].Replace('-', '_'));
      }
      bool flag = false;
      if (strArray2 != null)
      {
        flag = strArray2.Length == 1 && strArray2[0] == "*";
        if (!flag)
        {
          int index = 0;
          for (int length = strArray2.Length; index < length; ++index)
            strArray2[index] = CultureInfo.InvariantCulture.TextInfo.ToLower(strArray2[index]);
        }
        else
          strArray2 = (string[]) null;
      }
      IList<ICustomOutputCacheVariation> outputCacheVariationList = (IList<ICustomOutputCacheVariation>) null;
      PageRouteHandler.CustomOutputCacheVariationsRegistry outputCacheVariations = PageRouteHandler.GetCustomOutputCacheVariations(this.context.Items);
      if (outputCacheVariations != null && outputCacheVariations.Validated)
        outputCacheVariationList = outputCacheVariations.Variations;
      string str1 = (string) null;
      string str2 = (string) null;
      if (this.context.Items[(object) "ServedPageNode"] is PageSiteNode pageSiteNode && !pageSiteNode.IsBackend)
      {
        str1 = SystemManager.CurrentContext.CurrentSite.Id.ToString();
        str2 = SystemManager.CurrentContext.Culture.Name;
      }
      return new CachedVary()
      {
        CustomVariationParams = outputCacheVariationList,
        ContentEncodings = settings.VaryByContentEncodings,
        Headers = varyByHeaders,
        Params = strArray2,
        VaryByAllParams = flag,
        VaryByCustom = settings.VaryByCustom,
        SiteId = str1,
        Culture = str2
      };
    }

    public bool CheckCachedVary(CachedVary cachedVary, HttpCachePolicySettings settings) => cachedVary == null && !settings.IgnoreParams && (this.context.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) || this.context.Request.QueryString.Count > 0);

    public bool CheckHeaders(HttpCachePolicySettings settings)
    {
      if (settings.HasValidationPolicy())
        return false;
      if (this.context.Request.Headers["Cache-Control"] != null)
      {
        foreach (string directive in this.context.Request.Headers["Cache-Control"].Split(this.fieldSeparators))
        {
          if (this.CheckMaxAge(directive, settings))
            return true;
        }
      }
      string header = this.context.Request.Headers["Pragma"];
      return header != null && ((IEnumerable<string>) header.Split(this.fieldSeparators)).Any<string>((Func<string, bool>) (t => t == null || t.Equals("no-cache", StringComparison.OrdinalIgnoreCase)));
    }

    private bool? CheckIfModifiedSince(HttpCachePolicySettings settings)
    {
      string header = this.context.Request.Headers["If-Modified-Since"];
      if (header == null)
        return new bool?();
      if (settings.UtcLastModified != DateTime.MinValue)
      {
        DateTime dateTime = HttpDate.UtcParse(header);
        if (settings.UtcLastModified <= dateTime && dateTime <= this.context.Timestamp.ToUniversalTime())
          return new bool?(true);
      }
      return new bool?(false);
    }

    private bool? CheckIfNoneMatch(string etag)
    {
      string header = this.context.Request.Headers["If-None-Match"];
      if (header == null || string.IsNullOrEmpty(etag))
        return new bool?();
      string[] strArray = header.Split(this.fieldSeparators);
      int index = 0;
      for (int length = strArray.Length; index < length; ++index)
      {
        if (index == 0 && strArray[index].Equals("*"))
          return new bool?(true);
        if (strArray[index].Equals(etag))
          return new bool?(true);
      }
      return new bool?(false);
    }

    private bool CheckMaxAge(string directive, HttpCachePolicySettings settings)
    {
      if (directive.Equals("no-cache", StringComparison.OrdinalIgnoreCase) || directive.Equals("no-store", StringComparison.OrdinalIgnoreCase))
        return true;
      if (directive.StartsWith("max-age="))
      {
        int result;
        try
        {
          if (!int.TryParse(directive.Substring(8), out result))
            result = -1;
        }
        catch (ArgumentOutOfRangeException ex)
        {
          result = -1;
        }
        if (result < 0)
          return false;
        DateTime dateTime = this.context.Timestamp;
        long ticks1 = dateTime.Ticks;
        dateTime = settings.UtcTimestampCreated;
        long ticks2 = dateTime.Ticks;
        return (int) ((ticks1 - ticks2) / 10000000L) >= result;
      }
      if (!directive.StartsWith("min-fresh="))
        return false;
      int result1;
      try
      {
        if (!int.TryParse(directive.Substring(10), out result1))
          result1 = -1;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        result1 = -1;
      }
      return result1 >= 0 && !(settings.UtcExpires == DateTime.MinValue) && !settings.SlidingExpiration && (int) ((settings.UtcExpires.Ticks - this.context.Timestamp.Ticks) / 10000000L) < result1;
    }

    public async Task<bool> CheckValidityAsync(string key, HttpCachePolicySettings settings)
    {
      bool flag;
      if (settings.ValidationCallbackInfo == null || !settings.ValidationCallbackInfo.Any<KeyValuePair<HttpCacheValidateHandler, object>>())
      {
        flag = false;
      }
      else
      {
        HttpValidationStatus validationStatus1 = HttpValidationStatus.Valid;
        HttpValidationStatus validationStatus2 = validationStatus1;
        foreach (KeyValuePair<HttpCacheValidateHandler, object> keyValuePair in settings.ValidationCallbackInfo)
        {
          keyValuePair.Key(this.context.ApplicationInstance.Context, keyValuePair.Value, ref validationStatus1);
          switch (validationStatus1)
          {
            case HttpValidationStatus.Invalid:
              await this.RemoveAsync(key);
              return true;
            case HttpValidationStatus.IgnoreThisRequest:
              validationStatus2 = HttpValidationStatus.IgnoreThisRequest;
              continue;
            case HttpValidationStatus.Valid:
              continue;
            default:
              validationStatus1 = validationStatus2;
              continue;
          }
        }
        flag = validationStatus2 == HttpValidationStatus.IgnoreThisRequest;
        IEnumerator<KeyValuePair<HttpCacheValidateHandler, object>> enumerator = (IEnumerator<KeyValuePair<HttpCacheValidateHandler, object>>) null;
      }
      return flag;
    }

    private bool ContainsNonShareableCookies()
    {
      HttpCookieCollection cookies = this.context.Response.Cookies;
      for (int index = 0; index < cookies.Count; ++index)
      {
        HttpCookie httpCookie = cookies[index];
        if (httpCookie != null && !httpCookie.Shareable)
          return true;
      }
      return false;
    }

    public string GenerateCachedVaryItemKey() => OutputCacheHelper.GenerateRequestKey((HttpContextBase) new HttpContextWrapper(this.context));

    internal static string GenerateVariationsKey(IList<ICustomOutputCacheVariation> cacheVariations)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (ICustomOutputCacheVariation cacheVariation in (IEnumerable<ICustomOutputCacheVariation>) cacheVariations)
      {
        stringBuilder.Append("K");
        stringBuilder.Append(cacheVariation.Key);
        stringBuilder.Append("V");
        stringBuilder.Append(cacheVariation.GetValue() ?? "+n+");
      }
      return stringBuilder.ToString();
    }

    internal static string GenerateRequestKey(HttpContextBase context)
    {
      HttpRequestBase request = context.Request;
      string path = request.Path;
      StringBuilder stringBuilder = request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) ? new StringBuilder("a1", path.Length + "a1".Length) : new StringBuilder("a2", path.Length + "a2".Length);
      stringBuilder.Append('/');
      if (OutputCacheHelper.ShouldIncludeSiteIdInRequestKey(request))
      {
        PageRouteHandler.EnsureCurrentSiteContext(context);
        stringBuilder.Append((object) SystemManager.CurrentContext.CurrentSite.Id);
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
          stringBuilder.Append(SystemManager.CurrentContext.Culture.Name);
      }
      stringBuilder.Append(CultureInfo.InvariantCulture.TextInfo.ToLower(path));
      return stringBuilder.ToString().ComputeSha256Hash();
    }

    public string GenerateCachedItemKey(CachedVary cachedVary = null)
    {
      if (cachedVary == null)
        cachedVary = this.GetRequestCachedVary(this.GetCurrentSettings());
      string outputCacheItemKey = this.GetOutputCacheItemKey(this.context.Request.Path, this.context.Request.HttpMethod, cachedVary);
      if (!cachedVary.SiteId.IsNullOrEmpty())
        outputCacheItemKey += cachedVary.SiteId;
      if (!cachedVary.Culture.IsNullOrEmpty())
        outputCacheItemKey += cachedVary.Culture;
      return outputCacheItemKey.ComputeSha256Hash();
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    private string GetOutputCacheItemKey(string path, string verb, CachedVary cachedVary)
    {
      System.Web.HttpRequest request = this.context.Request;
      StringBuilder stringBuilder = verb.Equals("POST", StringComparison.OrdinalIgnoreCase) ? new StringBuilder("a1", path.Length + "a1".Length) : new StringBuilder("a2", path.Length + "a2".Length);
      stringBuilder.Append(CultureInfo.InvariantCulture.TextInfo.ToLower(path));
      for (int index1 = 0; index1 <= 2; ++index1)
      {
        string[] strArray = (string[]) null;
        NameValueCollection nameValueCollection = (NameValueCollection) null;
        bool flag = false;
        switch (index1)
        {
          case 0:
            stringBuilder.Append("H");
            strArray = cachedVary.Headers;
            if (strArray != null)
            {
              nameValueCollection = request.ServerVariables;
              break;
            }
            break;
          case 1:
            stringBuilder.Append("Q");
            strArray = cachedVary.Params;
            if (strArray != null || cachedVary.VaryByAllParams)
            {
              nameValueCollection = request.QueryString;
              flag = cachedVary.VaryByAllParams;
              break;
            }
            break;
          default:
            stringBuilder.Append("F");
            if (verb.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
              strArray = cachedVary.Params;
              if (strArray != null || cachedVary.VaryByAllParams)
              {
                nameValueCollection = request.Form;
                flag = cachedVary.VaryByAllParams;
                break;
              }
              break;
            }
            break;
        }
        if (flag && nameValueCollection.Count > 0)
        {
          strArray = nameValueCollection.AllKeys;
          for (int index2 = strArray.Length - 1; index2 >= 0; --index2)
          {
            if (strArray[index2] != null)
              strArray[index2] = CultureInfo.InvariantCulture.TextInfo.ToLower(strArray[index2]);
          }
          Array.Sort((Array) strArray, (IComparer) InvariantComparer.Default);
        }
        if (strArray != null)
        {
          int index3 = 0;
          for (int length = strArray.Length; index3 < length; ++index3)
          {
            string name = strArray[index3];
            string str = nameValueCollection != null ? nameValueCollection[name] ?? "+n+" : "+n+";
            stringBuilder.Append("N");
            stringBuilder.Append(name);
            stringBuilder.Append("V");
            stringBuilder.Append(str);
          }
        }
      }
      string str2 = (string) this.context.Items[(object) "sf_output_cache_variation_key"];
      if (!str2.IsNullOrEmpty())
      {
        stringBuilder.Append("V");
        stringBuilder.Append(str2);
      }
      stringBuilder.Append("C");
      if (cachedVary.VaryByCustom != null)
      {
        stringBuilder.Append("N");
        stringBuilder.Append(cachedVary.VaryByCustom);
        stringBuilder.Append("V");
        string str3 = this.context.ApplicationInstance.GetVaryByCustomString(this.context, cachedVary.VaryByCustom) ?? "+n+";
        stringBuilder.Append(str3);
      }
      stringBuilder.Append("D");
      if (verb.Equals("POST", StringComparison.OrdinalIgnoreCase) && cachedVary.VaryByAllParams && request.Form.Count == 0)
      {
        int contentLength = request.ContentLength;
        if (contentLength > 15000 || contentLength < 0)
          return (string) null;
        if (contentLength > 0)
        {
          using (MemoryStream destination = new MemoryStream())
          {
            request.InputStream.CopyTo((Stream) destination);
            string base64String = Convert.ToBase64String(CryptoUtil.ComputeHash(destination.ToArray()));
            stringBuilder.Append(base64String);
          }
        }
      }
      stringBuilder.Append("E");
      string[] contentEncodings = cachedVary.ContentEncodings;
      if (contentEncodings == null)
        return stringBuilder.ToString();
      if (this.context.Request.Headers["Accept-Encoding"] != null)
      {
        string header = this.context.Request.Headers["Accept-Encoding"];
        char[] chArray = new char[1]{ ',' };
        foreach (string str4 in header.Split(chArray))
        {
          string str1 = str4;
          if (((IEnumerable<string>) contentEncodings).Any<string>((Func<string, bool>) (t => t.Equals(str1, StringComparison.OrdinalIgnoreCase))))
          {
            stringBuilder.Append(str1);
            break;
          }
        }
      }
      return stringBuilder.ToString();
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Generic large method")]
    public int GetAcceptableEncoding(
      string[] contentEncodings,
      int startIndex,
      string acceptEncoding)
    {
      if (string.IsNullOrEmpty(acceptEncoding))
        return -1;
      if (acceptEncoding.IndexOf(',') != -1)
      {
        int acceptableEncoding1 = -1;
        double num = 0.0;
        for (int acceptableEncoding2 = startIndex; acceptableEncoding2 < contentEncodings.Length; ++acceptableEncoding2)
        {
          double acceptableEncodingHelper = this.GetAcceptableEncodingHelper(contentEncodings[acceptableEncoding2], acceptEncoding);
          if (acceptableEncodingHelper == 1.0)
            return acceptableEncoding2;
          if (acceptableEncodingHelper > num)
          {
            acceptableEncoding1 = acceptableEncoding2;
            num = acceptableEncodingHelper;
          }
        }
        if (acceptableEncoding1 == -1 && !this.IsIdentityAcceptable(acceptEncoding))
          acceptableEncoding1 = -2;
        return acceptableEncoding1;
      }
      string b = acceptEncoding;
      int num1 = acceptEncoding.IndexOf(';');
      if (num1 > -1)
      {
        int num2 = acceptEncoding.IndexOf(' ');
        if (num2 > -1 && num2 < num1)
          num1 = num2;
        b = acceptEncoding.Substring(0, num1);
        if (this.ParseWeight(acceptEncoding, num1) == 0.0)
          return (b.Equals("identity", StringComparison.OrdinalIgnoreCase) ? 1 : (b == "*" ? 1 : 0)) != 0 ? -2 : -1;
      }
      if (b == "*")
        return 0;
      for (int acceptableEncoding = startIndex; acceptableEncoding < contentEncodings.Length; ++acceptableEncoding)
      {
        if (string.Equals(contentEncodings[acceptableEncoding], b, StringComparison.OrdinalIgnoreCase))
          return acceptableEncoding;
      }
      return -1;
    }

    private double GetAcceptableEncodingHelper(string coding, string acceptEncoding)
    {
      double acceptableEncodingHelper = -1.0;
      int startIndex = 0;
      int length1 = coding.Length;
      int length2 = acceptEncoding.Length;
      int num1 = length2 - length1;
      while (startIndex < num1)
      {
        int num2 = acceptEncoding.IndexOf(coding, startIndex, StringComparison.OrdinalIgnoreCase);
        switch (num2)
        {
          case -1:
            return acceptableEncodingHelper;
          case 0:
label_7:
            int num3 = num2 + length1;
            char minValue = char.MinValue;
            if (num3 < length2)
            {
              minValue = acceptEncoding[num3];
              while (minValue == ' ' && ++num3 < length2)
                minValue = acceptEncoding[num3];
              if (minValue != ' ' && minValue != ',' && minValue != ';')
              {
                startIndex = num2 + 1;
                continue;
              }
            }
            return minValue == ';' ? this.ParseWeight(acceptEncoding, num3) : 1.0;
          default:
            switch (acceptEncoding[num2 - 1])
            {
              case ' ':
              case ',':
                goto label_7;
              default:
                startIndex = num2 + 1;
                continue;
            }
        }
      }
      return acceptableEncodingHelper;
    }

    public async Task<CachedRawResponse> GetCachedResponse(
      CachedVary cachedVary)
    {
      CachedRawResponse cachedResponse = (CachedRawResponse) null;
      string cachedItemKey = this.GenerateCachedItemKey(cachedVary);
      if (cachedItemKey != null)
      {
        SitefinityOutputCacheProvider.StoreCachedVaryInContext(cachedVary, this.context);
        if (cachedVary.ContentEncodings != null)
        {
          bool flag = true;
          string item = this.context.Request.Headers["Accept-Encoding"];
          if (item != null)
          {
            string[] contentEncodings = cachedVary.ContentEncodings;
            int num = 0;
            bool flag1 = false;
            while (!flag1)
            {
              flag1 = true;
              int acceptableEncoding = this.GetAcceptableEncoding(contentEncodings, num, item);
              if (acceptableEncoding <= -1)
              {
                if (acceptableEncoding == -2)
                  flag = false;
              }
              else
              {
                flag = false;
                cachedResponse = await this.GetCachedResponseAsync(cachedItemKey);
                if (cachedResponse == null)
                {
                  num = acceptableEncoding + 1;
                  if (num < contentEncodings.Length)
                    flag1 = false;
                }
              }
            }
            contentEncodings = (string[]) null;
            contentEncodings = (string[]) null;
          }
          if (cachedResponse == null & flag)
            cachedResponse = await this.GetCachedResponseAsync(cachedItemKey);
          item = (string) null;
          item = (string) null;
        }
        else
          cachedResponse = await this.GetCachedResponseAsync(cachedItemKey);
      }
      return cachedResponse;
    }

    public async Task<CachedVary> GetCachedVaryAsync(string key) => await OutputCacheHelper.outputCacheProvider.GetAsync(key) as CachedVary;

    private async Task<CachedRawResponse> GetCachedResponseAsync(string key)
    {
      if (!(await OutputCacheHelper.outputCacheProvider.GetAsync(key) is OutputCacheEntry async))
        return (CachedRawResponse) null;
      DateTime utcNow = DateTime.UtcNow;
      if (async.Settings.SlidingExpiration && utcNow < async.Settings.UtcExpires)
      {
        DateTime utcExpiry = utcNow + async.Settings.MaxAge;
        async.Settings.UtcExpires = utcExpiry;
        OutputCacheHelper.outputCacheProvider.SetAsync(key, (object) async, utcExpiry);
      }
      return OutputCacheHelper.converter.CreateCachedRawResponse(async);
    }

    private HttpCachePolicySettings GetCurrentSettings()
    {
      System.Web.HttpResponse response = this.context.Response;
      HttpCachePolicy cache = response.Cache;
      return new HttpCachePolicySettings()
      {
        Cacheability = cache.GetCacheability(),
        ValidationCallbackInfo = OutputCacheUtility.GetValidationCallbacks(response),
        VaryByContentEncodings = cache.VaryByContentEncodings.GetContentEncodings(),
        VaryByHeaders = cache.VaryByHeaders.GetHeaders(),
        VaryByParams = cache.VaryByParams.GetParams(),
        VaryByCustom = cache.GetVaryByCustom(),
        UtcExpires = cache.GetExpires(),
        MaxAge = cache.GetMaxAge(),
        SlidingExpiration = cache.HasSlidingExpiration(),
        IgnoreRangeRequests = cache.GetIgnoreRangeRequests(),
        UtcLastModified = cache.GetUtcLastModified(),
        ETag = cache.GetETag(),
        UtcTimestampCreated = cache.UtcTimestampCreated,
        OmitVaryStar = cache.GetOmitVaryStar()
      };
    }

    private HttpRawResponse GetSnapshot()
    {
      NameValueCollection nameValueCollection = new NameValueCollection();
      System.Web.HttpResponse response = this.context.Response;
      if (response.HeadersWritten)
        throw new HttpException("Cannot get snapshot if not buffered.");
      foreach (string allKey in response.Headers.AllKeys)
      {
        if (!allKey.Equals("Server", StringComparison.OrdinalIgnoreCase) && !allKey.Equals("Set-Cookie", StringComparison.OrdinalIgnoreCase) && !allKey.Equals("Cache-Control", StringComparison.OrdinalIgnoreCase) && !allKey.Equals("Expires", StringComparison.OrdinalIgnoreCase) && !allKey.Equals("Last-Modified", StringComparison.OrdinalIgnoreCase) && !allKey.Equals("ETag", StringComparison.OrdinalIgnoreCase) && !allKey.Equals("Vary", StringComparison.OrdinalIgnoreCase))
          nameValueCollection.Add(allKey, response.Headers[allKey]);
      }
      return new HttpRawResponse()
      {
        StatusCode = response.StatusCode,
        StatusDescription = response.StatusDescription,
        Headers = nameValueCollection,
        Buffers = OutputCacheUtility.GetContentBuffers(response),
        SubstitutionInfo = SitefinityOutputCacheProvider.GetSubstitutionInfo()
      };
    }

    private async Task InsertResponseAsync(
      string key,
      string keyRawResponse,
      CachedVary cachedVary,
      HttpCachePolicySettings settings)
    {
      if (!(settings.UtcExpires > DateTime.UtcNow))
        return;
      HttpRawResponse snapshot = this.GetSnapshot();
      string str = (string) null;
      if (this.IsKernelCacheAPISupported())
        str = OutputCacheUtility.SetupKernelCaching((string) null, this.context.Response);
      CachedRawResponse rawResponse = new CachedRawResponse()
      {
        RawResponse = snapshot,
        CachePolicy = settings,
        KernelCacheUrl = str
      };
      await this.InsertResponseAsync(key, cachedVary, keyRawResponse, rawResponse, settings.UtcExpires);
    }

    private async Task InsertResponseAsync(
      string cachedVaryKey,
      CachedVary cachedVary,
      string rawResponseKey,
      CachedRawResponse rawResponse,
      DateTime absExp)
    {
      if (!rawResponse.CachePolicy.IsValidationCallbackSerializable())
        throw new NotSupportedException("Provider does not support policy for responses.");
      if (cachedVary != null)
      {
        SitefinityOutputCacheProvider.StoreCacheVaryKeyInContext(cachedVaryKey, this.context);
        object obj = await OutputCacheHelper.outputCacheProvider.AddAsync(cachedVaryKey, (object) cachedVary, Cache.NoAbsoluteExpiration);
      }
      OutputCacheEntry outputCacheEntry = OutputCacheHelper.converter.CreateOutputCacheEntry(rawResponse);
      object obj1 = await OutputCacheHelper.outputCacheProvider.AddAsync(rawResponseKey, (object) outputCacheEntry, absExp);
    }

    public bool IsAcceptableEncoding(string contentEncoding, string acceptEncoding)
    {
      if (string.IsNullOrEmpty(contentEncoding))
        contentEncoding = "identity";
      if (string.IsNullOrEmpty(acceptEncoding))
        return contentEncoding.Equals("identity", StringComparison.OrdinalIgnoreCase);
      double acceptableEncodingHelper = this.GetAcceptableEncodingHelper(contentEncoding, acceptEncoding);
      if (acceptableEncodingHelper == 0.0)
        return false;
      return acceptableEncodingHelper > 0.0 || this.GetAcceptableEncodingHelper("*", acceptEncoding) != 0.0;
    }

    private bool IsCacheableEncoding(
      string headerContentEncodings,
      HttpCacheVaryByContentEncodings varyByContentEncodings)
    {
      if (varyByContentEncodings == null || headerContentEncodings == null)
        return true;
      string str1 = headerContentEncodings;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        string str = str2;
        if (((IEnumerable<string>) varyByContentEncodings.GetContentEncodings()).Any<string>((Func<string, bool>) (varyByContentEncoding => varyByContentEncoding.Equals(str, StringComparison.OrdinalIgnoreCase))))
          return true;
      }
      return false;
    }

    public bool IsContentEncodingAcceptable(CachedVary cachedVary, HttpRawResponse rawResponse)
    {
      if (cachedVary != null && cachedVary.ContentEncodings != null)
        return true;
      string header = this.context.Request.Headers["Accept-Encoding"];
      NameValueCollection headers = rawResponse.Headers;
      return headers == null ? this.IsAcceptableEncoding((string) null, header) : this.IsAcceptableEncoding(headers.Cast<string>().FirstOrDefault<string>((Func<string, bool>) (h => h.Equals("Content-Encoding", StringComparison.OrdinalIgnoreCase))), header);
    }

    public bool IsHttpMethodSupported()
    {
      string upper = this.context.Request.HttpMethod.ToUpper();
      return upper == "HEAD" || upper == "GET" || upper == "POST";
    }

    private bool IsIdentityAcceptable(string acceptEncoding)
    {
      double acceptableEncodingHelper = this.GetAcceptableEncodingHelper("identity", acceptEncoding);
      return acceptableEncodingHelper != 0.0 && (acceptableEncodingHelper > 0.0 || this.GetAcceptableEncodingHelper("*", acceptEncoding) != 0.0);
    }

    public bool IsKernelCacheAPISupported()
    {
      Assembly assembly = typeof (ResponseElement).Assembly;
      if (assembly != (Assembly) null)
      {
        Type type = assembly.GetType("System.Web.Caching.OutputCacheUtility");
        if (type != (Type) null && type.GetMethod("FlushKernelCache") != (MethodInfo) null)
          return true;
      }
      return false;
    }

    public bool IsRangeRequest() => this.context.Request.Headers["Range"].StartsWith("bytes", StringComparison.OrdinalIgnoreCase);

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Generic large method")]
    public bool IsResponseCacheable()
    {
      HttpCachePolicy cache = this.context.Response.Cache;
      if (!cache.IsModified() || this.context.Response.StatusCode != 200 && !SitefinityOutputCacheProvider.IgnoreStatusCodeValidation() || !this.IsHttpMethodSupported() || this.context.Response.HeadersWritten)
        return false;
      switch (cache.GetCacheability())
      {
        case HttpCacheability.Server:
        case HttpCacheability.Public:
        case HttpCacheability.ServerAndPrivate:
          if (cache.GetNoServerCaching() || this.ContainsNonShareableCookies())
            return false;
          bool flag1 = !cache.HasSlidingExpiration() && (cache.GetExpires() != DateTime.MinValue || cache.GetMaxAge() != TimeSpan.Zero);
          bool flag2 = cache.GetLastModifiedFromFileDependencies() || cache.GetETagFromFileDependencies() || OutputCacheUtility.GetValidationCallbacks(this.context.Response).Any<KeyValuePair<HttpCacheValidateHandler, object>>() || cache.IsValidUntilExpires() && !cache.HasSlidingExpiration();
          if (!flag1 && !flag2 || cache.VaryByHeaders["*"])
            return false;
          if (!cache.VaryByParams.IgnoreParams)
          {
            if (object.Equals((object) cache.VaryByParams.GetParams(), (object) new string[1]
            {
              "*"
            }))
            {
              if (false && (this.context.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) || this.context.Request.QueryString.Count > 0))
                return false;
              return cache.VaryByContentEncodings.GetContentEncodings() == null || this.IsCacheableEncoding(this.context.Request.Headers["Accept-Encoding"], cache.VaryByContentEncodings);
            }
            if (cache.VaryByParams.GetParams() == null)
            {
              if (this.context.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) || this.context.Request.QueryString.Count > 0)
                return false;
              return cache.VaryByContentEncodings.GetContentEncodings() == null || this.IsCacheableEncoding(this.context.Request.Headers["Accept-Encoding"], cache.VaryByContentEncodings);
            }
            if (!((IEnumerable<string>) cache.VaryByParams.GetParams()).Any<string>() && (this.context.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) || this.context.Request.QueryString.Count > 0))
              return false;
            return cache.VaryByContentEncodings.GetContentEncodings() == null || this.IsCacheableEncoding(this.context.Request.Headers["Accept-Encoding"], cache.VaryByContentEncodings);
          }
          if (false && (this.context.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) || this.context.Request.QueryString.Count > 0))
            return false;
          return cache.VaryByContentEncodings.GetContentEncodings() == null || this.IsCacheableEncoding(this.context.Request.Headers["Accept-Encoding"], cache.VaryByContentEncodings);
        default:
          return false;
      }
    }

    private double ParseWeight(string acceptEncoding, int startIndex)
    {
      double weight = 1.0;
      int num1 = acceptEncoding.IndexOf(',', startIndex);
      if (num1 == -1)
        num1 = acceptEncoding.Length;
      int startIndex1 = acceptEncoding.IndexOf('q', startIndex);
      if (startIndex1 <= -1 || startIndex1 >= num1)
        return weight;
      int num2 = acceptEncoding.IndexOf('=', startIndex1);
      double result;
      if (num2 <= -1 || num2 >= num1 || !double.TryParse(acceptEncoding.Substring(num2 + 1, num1 - (num2 + 1)), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        return weight;
      weight = result < 0.0 || result > 1.0 ? 1.0 : result;
      return weight;
    }

    private async Task RemoveAsync(string key) => await OutputCacheHelper.outputCacheProvider.RemoveAsync(key);

    private void ResetFromHttpCachePolicySettings(HttpCachePolicySettings settings)
    {
      HttpCachePolicy cache = this.context.Response.Cache;
      cache.SetCacheability(settings.Cacheability);
      cache.VaryByContentEncodings.SetContentEncodings(settings.VaryByContentEncodings);
      cache.VaryByHeaders.SetHeaders(settings.VaryByHeaders);
      cache.VaryByParams.SetParams(settings.VaryByParams);
      if (settings.OmitVaryStar != -1)
        cache.SetOmitVaryStar(settings.OmitVaryStar == 1);
      if (settings.VaryByCustom != null)
        cache.SetVaryByCustom(settings.VaryByCustom);
      cache.SetExpires(settings.UtcExpires);
      if (settings.MaxAge != TimeSpan.Zero)
        cache.SetMaxAge(settings.MaxAge);
      cache.SetSlidingExpiration(settings.SlidingExpiration);
      cache.UtcTimestampCreated = settings.UtcTimestampCreated;
      cache.SetValidUntilExpires(settings.ValidUntilExpires);
      if (settings.UtcLastModified != DateTime.MinValue)
        cache.SetLastModified(settings.UtcLastModified);
      if (settings.ETag != null)
        cache.SetETag(settings.ETag);
      if (settings.ValidationCallbackInfo == null)
        return;
      foreach (KeyValuePair<HttpCacheValidateHandler, object> keyValuePair in settings.ValidationCallbackInfo)
        cache.AddValidationCallback(keyValuePair.Key, keyValuePair.Value);
    }

    private void UpdateCachedHeaders()
    {
      if (!(this.context.Response.Cache.UtcTimestampCreated == DateTime.MinValue))
        return;
      this.context.Response.Cache.UtcTimestampCreated = this.context.Timestamp.ToUniversalTime();
    }

    public void UpdateCachedResponse(HttpCachePolicySettings settings, HttpRawResponse rawResponse)
    {
      bool? nullable1 = this.CheckIfNoneMatch(settings.ETag);
      bool? nullable2 = this.CheckIfModifiedSince(settings);
      if (!nullable1.HasValue && !nullable2.HasValue || !nullable1.HasValue && !nullable2.Value || !nullable2.HasValue && !nullable1.Value)
      {
        this.UseSnapshot(rawResponse, !this.context.Request.HttpMethod.Equals("HEAD", StringComparison.OrdinalIgnoreCase));
      }
      else
      {
        if (this.context.Response.HeadersWritten)
          this.context.Response.ClearHeaders();
        this.context.Response.Clear();
        this.context.Response.StatusCode = 304;
      }
      this.ResetFromHttpCachePolicySettings(settings);
    }

    private void UseSnapshot(HttpRawResponse rawResponse, bool sendBody)
    {
      System.Web.HttpResponse response = this.context.Response;
      if (response.HeadersWritten)
        throw new HttpException(string.Format("Cannot use snapshot after headers sent. Response status code: {0}", (object) this.context.Response.StatusCode));
      response.Clear();
      response.ClearHeaders();
      response.StatusCode = rawResponse.StatusCode;
      response.StatusDescription = rawResponse.StatusDescription;
      foreach (string allKey in rawResponse.Headers.AllKeys)
        response.Headers.Add(allKey, rawResponse.Headers[allKey]);
      SitefinityOutputCacheProvider.SetSubstitutionInfoInContext(rawResponse.SubstitutionInfo);
      OutputCacheUtility.SetContentBuffers(response, rawResponse.Buffers);
      response.SuppressContent = !sendBody;
    }
  }
}
