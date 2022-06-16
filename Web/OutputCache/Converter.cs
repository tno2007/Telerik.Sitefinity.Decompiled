// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Converter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web;
using System.Web.Caching;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal class Converter
  {
    private Type httpFileResponseElementType;
    private Type httpSubstBlockResponseElementType;
    private Type httpResponseElementType;
    private Type httpResponseBufferElementType;
    private ConstructorInfo httpResponseBufferElementCtor;
    private ConstructorInfo httpSubstBlockResponseElementCtor;
    private ConstructorInfo httpFileResponseElementCtor;
    private MethodInfo httpResponseElementGetBytes;
    private MethodInfo httpResponseElementGetSize;
    private FieldInfo httpFileResponseElementFileName;
    private FieldInfo httpFileResponseElementOffset;
    private FieldInfo httpFileResponseElementIsImpersonating;
    private FieldInfo httpFileResponseElementUseTransmitFile;
    private FieldInfo httpSubstBlockResponseElementCallback;

    public Converter()
    {
      Assembly assembly = typeof (ResponseElement).Assembly;
      this.httpFileResponseElementType = assembly.GetType("System.Web.HttpFileResponseElement");
      this.httpSubstBlockResponseElementType = assembly.GetType("System.Web.HttpSubstBlockResponseElement");
      this.httpResponseElementType = assembly.GetType("System.Web.IHttpResponseElement");
      this.httpResponseBufferElementType = assembly.GetType("System.Web.HttpResponseBufferElement");
      this.httpResponseBufferElementCtor = this.httpResponseBufferElementType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new Type[2]
      {
        typeof (byte[]),
        typeof (int)
      }, (ParameterModifier[]) null);
      this.httpSubstBlockResponseElementCtor = this.httpSubstBlockResponseElementType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new Type[1]
      {
        typeof (HttpResponseSubstitutionCallback)
      }, (ParameterModifier[]) null);
      this.httpFileResponseElementCtor = this.httpFileResponseElementType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new Type[5]
      {
        typeof (string),
        typeof (long),
        typeof (long),
        typeof (bool),
        typeof (bool)
      }, (ParameterModifier[]) null);
      this.httpResponseElementGetBytes = this.httpResponseElementType.GetMethod("GetBytes");
      this.httpResponseElementGetSize = this.httpResponseElementType.GetMethod("GetSize");
      this.httpFileResponseElementFileName = this.httpFileResponseElementType.GetField("_filename", BindingFlags.Instance);
      this.httpFileResponseElementOffset = this.httpFileResponseElementType.GetField("_offset", BindingFlags.Instance | BindingFlags.NonPublic);
      this.httpFileResponseElementIsImpersonating = this.httpFileResponseElementType.GetField("_isImpersonating", BindingFlags.Instance | BindingFlags.NonPublic);
      this.httpFileResponseElementUseTransmitFile = this.httpFileResponseElementType.GetField("_useTransmitFile", BindingFlags.Instance | BindingFlags.NonPublic);
      this.httpSubstBlockResponseElementCallback = this.httpSubstBlockResponseElementType.GetField("_callback", BindingFlags.Instance | BindingFlags.NonPublic);
    }

    [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed")]
    public CachedRawResponse CreateCachedRawResponse(OutputCacheEntry oce)
    {
      ArrayList arrayList = new ArrayList();
      foreach (ResponseElement responseBuffer in oce.ResponseBuffers)
      {
        object obj = (object) null;
        switch (responseBuffer)
        {
          case FileResponseElement _:
            obj = this.CreateHttpFileResponseElement(responseBuffer);
            break;
          case SubstitutionResponseElement _:
            obj = this.CreateHttpSubstBlockResponseElement(responseBuffer);
            break;
          case MemoryResponseElement _:
            obj = this.CreateHttpResponseBufferElement((MemoryResponseElement) responseBuffer);
            break;
        }
        if (obj != null)
          arrayList.Add(obj);
      }
      return new CachedRawResponse()
      {
        RawResponse = new HttpRawResponse()
        {
          StatusCode = oce.StatusCode,
          StatusDescription = oce.StatusDescription,
          Headers = oce.HeaderElements,
          Buffers = arrayList,
          SubstitutionInfo = oce.SubstitutionInfo
        },
        CachePolicy = oce.Settings,
        KernelCacheUrl = oce.KernelCacheUrl
      };
    }

    private ResponseElement CreateFileResponseElement(object o) => (ResponseElement) new OutputCacheFileResponseElement((string) this.httpFileResponseElementFileName.GetValue(o), (long) this.httpFileResponseElementOffset.GetValue(o), (long) this.httpResponseElementGetSize.Invoke(o, new object[0]), (bool) this.httpFileResponseElementIsImpersonating.GetValue(o), (bool) this.httpFileResponseElementUseTransmitFile.GetValue(o));

    private object CreateHttpFileResponseElement(ResponseElement e)
    {
      OutputCacheFileResponseElement fileResponseElement = (OutputCacheFileResponseElement) e;
      return this.httpFileResponseElementCtor.Invoke(new object[5]
      {
        (object) fileResponseElement.Path,
        (object) fileResponseElement.Offset,
        (object) fileResponseElement.Length,
        (object) fileResponseElement.IsImpersonating,
        (object) fileResponseElement.SupportsLongTransmitFile
      });
    }

    private object CreateHttpResponseBufferElement(MemoryResponseElement e)
    {
      int int32 = Convert.ToInt32(e.Length);
      return this.httpResponseBufferElementCtor.Invoke(new object[2]
      {
        (object) e.Buffer,
        (object) int32
      });
    }

    private object CreateHttpSubstBlockResponseElement(ResponseElement e) => this.httpSubstBlockResponseElementCtor.Invoke(new object[1]
    {
      (object) ((SubstitutionResponseElement) e).Callback
    });

    private ResponseElement CreateMemoryResponseElement(object o)
    {
      byte[] buffer = (byte[]) this.httpResponseElementGetBytes.Invoke(o, new object[0]);
      long length = buffer != null ? (long) buffer.Length : 0L;
      return (ResponseElement) new MemoryResponseElement(buffer, length);
    }

    public OutputCacheEntry CreateOutputCacheEntry(
      CachedRawResponse cachedRawResponse)
    {
      List<ResponseElement> responseElementList = new List<ResponseElement>();
      foreach (object buffer in cachedRawResponse.RawResponse.Buffers)
      {
        Type type = buffer.GetType();
        ResponseElement responseElement = !(type != this.httpFileResponseElementType) ? this.CreateFileResponseElement(buffer) : (type != this.httpSubstBlockResponseElementType ? this.CreateMemoryResponseElement(buffer) : this.CreateSubstBlockResponseElement(buffer));
        if (responseElement != null)
          responseElementList.Add(responseElement);
      }
      return new OutputCacheEntry()
      {
        Settings = cachedRawResponse.CachePolicy,
        KernelCacheUrl = cachedRawResponse.KernelCacheUrl,
        StatusCode = cachedRawResponse.RawResponse.StatusCode,
        StatusDescription = cachedRawResponse.RawResponse.StatusDescription,
        HeaderElements = cachedRawResponse.RawResponse.Headers,
        ResponseBuffers = (IEnumerable<ResponseElement>) responseElementList,
        SubstitutionInfo = cachedRawResponse.RawResponse.SubstitutionInfo
      };
    }

    private ResponseElement CreateSubstBlockResponseElement(object o) => (ResponseElement) new SubstitutionResponseElement((HttpResponseSubstitutionCallback) this.httpSubstBlockResponseElementCallback.GetValue(o));
  }
}
